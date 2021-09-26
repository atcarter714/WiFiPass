#region Using Directives
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
#endregion

namespace WiFiPass
{
    /// <summary>
    /// The main Form of the application
    /// </summary>
    public partial class MainForm : Form
    {
        public MainForm ( ) {
            InitializeComponent();
            WifiDataReader.Initialize();
        }



        #region String Constants
        static readonly string _LOADING_STR = "Loading ...";
        static readonly string _NO_PASSWORD = "(None)";
        static readonly string _NO_SECURITY = "Open Network";
        static readonly string _NO_SAVED_NETWORKS = "No saved networks";
        static readonly string _WAITING_FOR_PASSWORD = "...";
        static readonly string _EMPTY_FIELD = "-";
        #endregion

        #region Private Fields

        /// <summary>
        /// Indicates if list of NetworkData
        /// is being loaded/refreshed
        /// </summary>
        bool isReloading = false;

        #endregion



        #region Winform Event Handlers

        void onRefreshButtonClicked ( object sender, EventArgs e ) {
            _refreshNetworkList();
        }

        void onCopyPasswordClicked ( object sender, EventArgs e ) {

            if ( string.IsNullOrEmpty( textBox_Password.Text ) )
                return;

            else {
                var password = textBox_Password.Text;

                if ( password != _NO_PASSWORD )
                    Clipboard.SetText( password );
            }
        }

        void onSelectedNetworkChanged ( object sender, EventArgs e ) {

            //! If we don't have a valid network list ...
            if ( listBox_NetworkList.Items.Count <= 0 || 
                ( listBox_NetworkList.Items.Count == 1 && 
                listBox_NetworkList.Items[0].ToString() == _NO_SAVED_NETWORKS ) ) {

                button_CopyPassword.Enabled = false;
                textBox_Password.Text = _EMPTY_FIELD;
                label_SecurityTypeText.Text = _EMPTY_FIELD;
                return;
            }

            int index = listBox_NetworkList.SelectedIndex;
            var data = WifiDataReader.NetworkDataList[index];

            //! Set password field :
            if ( !string.IsNullOrEmpty( data.Password ) ) {
                textBox_Password.Text = data.Password;
                button_CopyPassword.Enabled = true;
            }
            else {
                textBox_Password.Text = _NO_PASSWORD;
                button_CopyPassword.Enabled = false;
            }

            //! Set the security type field :
            if ( !string.IsNullOrEmpty( data.SecurityType ) ) {
                label_SecurityTypeText.Text = data.SecurityType;
            }
            else {
                label_SecurityTypeText.Text = _NO_SECURITY;
                button_CopyPassword.Enabled = false;
            }
        }

        void onMenuStripMouseEntering ( object sender, EventArgs e ) {
            this.UseWaitCursor = false;
        }

        void onMenuStripMouseLeaving ( object sender, EventArgs e ) {

            if ( isReloading ) {
                this.UseWaitCursor = true;
            }
        }

        void menustripitem_Application_Exit_Click ( object sender, EventArgs e ) {
            Application.Exit();
        }

        void menustripitem_Application_About_Click ( object sender, EventArgs e ) {
            var aboutBox = new AboutBoxWiFiPass();
            aboutBox.ShowDialog( this );
        }

        #endregion

        #region Base Overrides
        protected override void OnShown ( EventArgs e ) {
            _refreshNetworkList();
        }

        #endregion



        /// <summary>
        /// Asynchronously refreshes and repopulates the
        /// network list and reloads all NetworkData
        /// </summary>
        /// <remarks>
        /// Here we spawn a thread with a Task object to
        /// prevent blocking on the UI thread while the
        /// network data is being retrieved from the
        /// netsh process by WifiDataReader
        /// </remarks>
        async void _refreshNetworkList ( ) {

            isReloading = true;

            //! Clear the list :
            listBox_NetworkList.Items.Clear();
            listBox_NetworkList.Items.Add( _LOADING_STR );
            button_CopyPassword.Enabled = false;

            //! Get all saved Wi-Fi network data :
            List<NetworkData> networkInfo = null;

            textBox_Password.Text = _WAITING_FOR_PASSWORD;
            label_SecurityTypeText.Text = $"({_LOADING_STR})";

            this.UseWaitCursor = true;
            networkInfo = await Task.Run( WifiDataReader.GetNetworkInfo );
            this.UseWaitCursor = false;

            listBox_NetworkList.Items.Clear();

            //! If process fails or no results display "No saved networks" :
            if ( networkInfo == null || networkInfo.Count == 0 ) {
                listBox_NetworkList.Items.Add( _NO_SAVED_NETWORKS );
            }
            else {
                //! Add networks to the list :
                foreach ( var data in networkInfo ) {
                    listBox_NetworkList.Items.Add( data.SSID );
                } 
            }

            listBox_NetworkList.SelectedIndex = 0;
            listBox_NetworkList.Focus();
            button_CopyPassword.Enabled = true;

            isReloading = false;
        }
    }
}