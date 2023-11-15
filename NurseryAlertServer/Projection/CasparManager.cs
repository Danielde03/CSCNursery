/*
 *   CSC Nursery
 *    Caspar Manager
 *
 *   This program is free software; you can redistribute it and/or
 *   modify it under the terms of the GNU General Public License
 *   as published by the Free Software Foundation; either version 2
 *   of the License, or (at your option) any later version.
 *
 *   This program is distributed in the hope that it will be useful,
 *   but WITHOUT ANY WARRANTY; without even the implied warranty of
 *   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *   GNU General Public License for more details.
 *
 *   You should have received a copy of the GNU General Public License
 *   along with this program; if not, write to the Free Software
 *   Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
 *
 *   Author: Danielde03
 *
 *   Copyright (c) Holy Spirit
 *
 */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using NurseryAlertServer.Properties;
using System.IO;
using System.Windows;

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
            // get default values
            getValues();

            connect();

        }

        // variables
        private TcpClient casparClient;
        StreamWriter writer;

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
        private void getValues()
        {
            port = Settings.Default.CasparCG_Port;
            server = Settings.Default.CasparCG_IP;
            channel = Settings.Default.Graphic_Channel;
            layer = Settings.Default.Graphic_Layer;
            template = Settings.Default.Caspar_Template;
            textField = Settings.Default.Graphic_Text_Field;
        }

        /// <summary>
        /// Write text to CasparCG
        /// </summary>
        /// /// <param name="message">Message to display</param>
        public void writeToCaspar(string message)
        {

            if (writer == null)
            {
                MessageBox.Show("Failed connecting to CapsarCG\n");
                return;
            }
            
            string displayText = String.Format("CG {0}-{1} ADD 1 \"{2}\" 1 \"<templateData><componentData id =\\\"{3}\\\"><data id=\\\"text\\\" value=\\\"{4}\\\"/></componentData></templateData>\"\\r\\n", channel, layer, template, textField, message);
            writer.WriteLine(displayText);
            writer.Flush();
        }

        /// <summary>
        /// Clear the screen
        /// </summary>
        public void clear()
        {
            if (writer == null)
            {
                MessageBox.Show("Failed connecting to CapsarCG\n");
                return;
            }

            writer.WriteLine("CG 1-20 NEXT 1\r\n");
            writer.Flush();
        }

        /// <summary>
        /// Connect to CasparCG
        /// </summary>
        public void connect()
        {
            bool reconnecting = false;

            // if not null, trying to reconnect by new values
            if (casparClient != null)
            {
                reconnecting = true;
                casparClient.Close();

                casparClient = null;
                writer = null;

                getValues();

            }
            

            // get the connection
            casparClient = new TcpClient();
            try
            {
                
                casparClient.Connect(server, port);
                Console.WriteLine("Connected to CasparCG");

                // get the writer
                writer = new StreamWriter(casparClient.GetStream());
            }
            catch
            {
                MessageBox.Show("Failed connecting to CapsarCG\n");
                return;
            }
            if (reconnecting)
            {
                MessageBox.Show("Connected to CasparCG\n");
            }
        }

    }
}
