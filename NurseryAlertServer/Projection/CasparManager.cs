using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using NurseryAlertServer.Properties;
using System.IO;

namespace NurseryAlertServer.Projection
{
    class CasparManager
    {
        /// <summary>
        /// Singleton variable
        /// </summary>
        private static CasparManager _instance;

        /// <summary>
        /// Gets the singleton of this class (field alternative)
        /// </summary>
        public static CasparManager Instance
        {
            get { return _instance ?? (_instance = new CasparManager()); }
        }

        /// <summary>
        /// The constructor
        /// </summary>
        private CasparManager()
        {
            // assign values to the CasparCG variables
            getValues();

            // get the connection
             client = new TcpClient(server, port);
             stream = new BinaryWriter(client.GetStream());

            // TESTING
            stream.Write("VERSION");
        }

        // variables
        private BinaryWriter stream;
        private TcpClient client;

        private int port;
        private string server;
        private string channel;
        private string layer;
        private string template;
        private string textField;


        // methods

        /// <summary>
        /// Get all the CasparCG values based on the Settings.settings values.
        /// The values can be changed in the GUI settings
        /// </summary>
        public void getValues()
        {
            port = Settings.Default.CasparCG_Port;
            server = Settings.Default.CasparCG_IP;
            channel = Settings.Default.Graphic_Channel;
            layer = Settings.Default.Graphic_Layer;
            template = Settings.Default.Caspar_Template;
            textField = Settings.Default.Graphic_Text_Field;
        }

    }
}
