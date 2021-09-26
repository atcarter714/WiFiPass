#region Using Directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#endregion

namespace WiFiPass
{
    public sealed class NetworkData
    {
        public NetworkData():
            this( "", "", "" ) { }

        public NetworkData( string ssid ):
            this( ssid, "", "Open" ) { }

        public NetworkData( string ssid, string key, string securityType ) {
            this.SSID = ssid;
            this.Password = key;
            this.SecurityType = securityType;
        }


        /// <summary>
        /// Gets or sets the network name/SSID string
        /// </summary>
        public string SSID { get; set; }

        /// <summary>
        /// Gets or sets the network key (password) string
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the network authentication/security type string
        /// </summary>
        public string SecurityType { get; set; }

        //public bool IsConnected { get; set; }

        //public int SignalStrength { get; set; }
    }
}