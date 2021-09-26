#region Using Directives
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

using System.Threading;
using System.Threading.Tasks;
#endregion

namespace WiFiPass
{
    /// <summary>
    /// "Lazy Singleton" implementation that reads Wi-Fi network
    /// data from the Windows netsh command-line application
    /// and parses the data using regex so it can be interpreted
    /// and displayed in a user-friendly format
    /// </summary>
    /// <remarks>
    /// This "lazy singleton" or "semi-static" design is kind of
    /// ugly but for this simple app it works fine (does exactly
    /// what it's supposed to do) and reduces the amount of code
    /// needed in consuming classes. Its more complex actions are
    /// hidden away and encapsulated inside the class, so that
    /// consuming objects only need to call Initialize and then
    /// GetNetworkInfo ...
    /// </remarks>
    public sealed class WifiDataReader
    {
        #region Static Fields
        static WifiDataReader wifiDataReader = null;
        static readonly string procFileName = "netsh";
        static readonly string procArguments = "wlan show profile";
        static readonly string wlanCommand = "wlan show profile";
        #endregion

        #region Static Properties

        /// <summary>
        /// Gets the singleton instance
        /// </summary>
        public static WifiDataReader SingletonInstance 
            => wifiDataReader;

        public static List<NetworkData> NetworkDataList =>
            SingletonInstance.networkData;

        #endregion



        List<NetworkData> networkData = new List<NetworkData>();



        #region Private Methods

        /// <summary>
        /// Gets list of saved Wi-Fi networks from netsh
        /// </summary>
        /// <returns>netsh output</returns>
        string _getnetshWifiListString ( ) {

            // Set up netsh proc with cmd line :
            var netshProc = new Process()
            {
                StartInfo = new ProcessStartInfo(procFileName, procArguments)
                {
                    WindowStyle = ProcessWindowStyle.Hidden,
                    UseShellExecute = false,
                    RedirectStandardError = true,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true,
                }
            };

            netshProc.Start();

            var output = netshProc.StandardOutput.ReadToEnd();
            var errors = netshProc.StandardError.ReadToEnd();

            netshProc.WaitForExit();

            return output;
        }

        /// <summary>
        /// Parses each line of netsh's output and retrieves
        /// the profile info for each Wi-Fi network
        /// </summary>
        /// <param name="netshStr">Network list output from netsh</param>
        void _parse_netsh_lines ( string netshStr ) {

            using ( StringReader reader = new StringReader( netshStr ) ) {

                string nextLine;
                while ( ( nextLine = reader.ReadLine() ) != null ) {

                    if ( string.IsNullOrEmpty( nextLine ) )
                        continue;

                    if ( !nextLine.Contains( "All User Profile" ) )
                        continue;

                    NetworkData data = null;
                    if ( ( data = _regex_ssidpassword( nextLine ) ) != null )
                        networkData.Add( data );
                }
            }
        }

        /// <summary>
        /// Finds the name of a saved Wi-Fi network and runs
        /// netsh with the wlan command to get complete profile
        /// info for the network. Then regex logic is used to
        /// retrieve the Wi-Fi password and authentication type
        /// from netsh output and a NetworkData instance is returned
        /// storing the information
        /// </summary>
        /// <param name="nextLine">The next line from netsh output</param>
        /// <returns>NetworkData object storing relevant network info</returns>
        /// <remarks>
        /// The regex search for the Wi-Fi password and the authentication
        /// (security) type take place on their own threads for a bit of
        /// added efficiency. The method waits for each thread to complete
        /// and come back with a valid result. Since GetNetworkInfo is called
        /// on its own thread from the MainForm class there is no need to worry
        /// about any UI thread blocking.
        /// </remarks>
        NetworkData _regex_ssidpassword ( string nextLine ) {

            //! Find next Wi-Fi network SSID :
            var regex = new Regex(@"All User Profile * : (?<after>.*)");
            var match = regex.Match(nextLine);

            if ( match.Success ) {

                var ssid = match.Groups["after"].Value;

                var netshProfileStr = _getnetshWifiProfileStr( ssid );

                //! Run regex searches on separate threads :
                string password = "", security = "";

                var getPwTask = new Task<string>( ( ) => { 
                    return _regex_Password( netshProfileStr ); });

                var getSecTask = new Task<string>( ( ) => { 
                    return _regex_AuthType( netshProfileStr ); });

                getPwTask.Start();
                getSecTask.Start();

                //! Wait for tasks to complete with 5ms sleep :
                while ( !getPwTask.IsCompleted || !getSecTask.IsCompleted )
                    Thread.Sleep( 5 );

                //! Copy the results :
                password = getPwTask.Result;
                security = getSecTask.Result;

                var data = new NetworkData()
                {
                    SSID = ssid,
                    Password = password,
                    SecurityType = security,
                };

                return data;
            }

            else
                return null;
        }

        /// <summary>
        /// Runs the netsh wlan command and retrieves the full
        /// Wi-Fi profile information for the network with the
        /// given SSID (name)
        /// </summary>
        /// <param name="ssid">The SSID/name of the saved network</param>
        /// <returns>Full netsh wlan profile output</returns>
        /// <remarks>
        /// The speed of the application could be increased by building
        /// an SSID list containing all network names and running the
        /// netsh wlan command in multi-threaded batches, but that may be
        /// over-engineering for this simple application (unless there
        /// are a large number of saved networks and performance begins
        /// to suffer dramatically). During testing, the process of
        /// refreshing the NetworkData list only takes 2 or 3 seconds
        /// for about a dozen saved networks on an average laptop, which
        /// seems perfectly acceptable. If performance enhancement is
        /// needed, this one be an important place to consider splitting
        /// the work off into multiple worker threads. Our regex logic
        /// is already performed on parralel Task threads.
        /// </remarks>
        string _getnetshWifiProfileStr ( string ssid ) {

            //! Set-up the netsh command line with SSID and retrieve profile :
            var argument = $"{wlanCommand} name=\"{ssid}\" key=clear";

            Process processWifi = new Process()
            {
                StartInfo = new ProcessStartInfo()
                {
                    WindowStyle = ProcessWindowStyle.Hidden,
                    FileName = procFileName,
                    Arguments = argument,
                    UseShellExecute = false,
                    RedirectStandardError = true,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true,
                }
            };

            //! Run the process ...
            processWifi.Start();

            string output = processWifi.StandardOutput.ReadToEnd();
            string err = processWifi.StandardError.ReadToEnd();

            processWifi.WaitForExit();

            return output;
        }

        /// <summary>
        /// Reads the netsh wlan profile output and uses regex logic
        /// to retrieve the key/password of the saved network
        /// </summary>
        /// <param name="netshProfileStr">Output from netsh wlan</param>
        /// <returns>Saved password, or empty string for open network</returns>
        string _regex_Password ( string netshProfileStr ) {

            using ( StringReader reader = new StringReader( netshProfileStr ) ) {

                string line;
                while ( ( line = reader.ReadLine() ) != null ) {

                    var regex_key = new Regex(@"Key Content * : (?<after>.*)");

                    var match_key = regex_key.Match( line );

                    if ( match_key.Success ) {
                        string current_password = match_key.Groups["after"].Value;
                        return current_password;
                    }
                }
            }

            return "";
        }

        /// <summary>
        /// Reads the netsh wlan profile output and uses regex logic
        /// to retrieve the security authentication type of the saved network
        /// </summary>
        /// <param name="netshProfileStr">Output from netsh wlan</param>
        /// <returns>Authentication/security type as string, or empty string if none found</returns>
        string _regex_AuthType ( string netshProfileStr ) {

            using ( StringReader reader = new StringReader( netshProfileStr ) ) {

                string line;
                while ( ( line = reader.ReadLine() ) != null ) {

                    var regex_sec = new Regex(@"Authentication * : (?<after>.*)");

                    var match_sec = regex_sec.Match( line );

                    if ( match_sec.Success ) {
                        string current_password = match_sec.Groups["after"].Value;
                        return current_password;
                    }
                }
            }

            return "";
        }

        #endregion



        /// <summary>
        /// Initializes a singleton instance of WifiDataReader
        /// so it can be used to retrieve information about
        /// saved Wi-Fi networks
        /// </summary>
        /// <returns></returns>
        public static WifiDataReader Initialize ( ) {

            if ( wifiDataReader == null )
                wifiDataReader = new WifiDataReader();

            return wifiDataReader;
        }

        /// <summary>
        /// Retrieves a List containing NetworkData for all saved
        /// Wi-Fi networks on this operating system from netsh
        /// </summary>
        /// <returns>List of all NetworkData for saved Wi-Fi networks</returns>
        /// <remarks>
        /// Spawn a separate Thread or Task to run this on because it
        /// takes a second and will block the UI thread otherwise ...
        /// </remarks>
        public static List<NetworkData> GetNetworkInfo ( ) {

            NetworkDataList.Clear();

            var allwifinames = wifiDataReader._getnetshWifiListString();
            
            wifiDataReader._parse_netsh_lines( allwifinames );

            return wifiDataReader.networkData;
        }

    }
}