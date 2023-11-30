/*
 *   CSC Nursery
 *    Tally Manager
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
 *   Author: jdramer
 *
 *   Copyright (c) Holy Spirit
 *
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Net.Sockets;
using System.Net;
using NurseryAlertServer.Properties;
using System.Windows;
using System.Threading;

namespace NurseryAlertServer.Tally
{
    class TallyManager
    {
        /// <summary>
        /// Singleton variable
        /// </summary>
        private static TallyManager _instance;

        /// <summary>
        /// Gets the singleton of this class (field alternative)
        /// </summary>
        public static TallyManager Instance
        {
            get { return _instance ?? (_instance = new TallyManager()); }
        }

        private static UdpClient _udpclient;

        private int _tallyState;

        private byte[] bytRecieved;
        private bool isActive = false;

        // Tally change detection event
        public delegate void TallyChange(TallyChangedEventArgs e);
        public event TallyChange TallyChanged;
        public class TallyChangedEventArgs : EventArgs
        {
            public int state { get; protected internal set; }
        }

        /// <summary>
        /// The constructor
        /// </summary>
        private TallyManager()
        {
            _udpclient = new UdpClient();
            _tallyState = 0;
        }

        /// <summary>
        /// Start monitoring the tally port
        /// </summary>
        public void OpenTallyPort()
        {
            try
            {

                // open port
                _udpclient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                _udpclient.Client.Bind(new IPEndPoint(IPAddress.Any, Settings.Default.TSL_Port));
                Console.WriteLine("Tally Port open on port {0}", Settings.Default.TSL_Port);

                // open thread to listen for tallies.
                Thread thread = new Thread(new ThreadStart(TallyManager.Instance.Process));
                isActive = true;
                thread.Start();

            }
            catch (Exception e)
            {
                MessageBox.Show("Tally Port Error\n");
                Console.WriteLine(e.Message);
                return;
            }


        }

        /// <summary>
        /// Reopen the tally port
        /// </summary>
        public void ReopenTallyPort()
        {
            // if (!_serialPort.PortName.Equals(Settings.Default.TallyComPort))
            //{

            //}
        }

        /// <summary>
        /// Listener for TSL tally data
        /// </summary>
        /// <param name="sender">SerialPort object</param>
        /// <param name="e">SerialPinChangedEventArgs object</param>
        private void Process()
        {
            while (isActive)
            {
                
                // clear recieved data
                bytRecieved = null;

                // get recieved data
                IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
                try
                {
                    bytRecieved = _udpclient.Receive(ref RemoteIpEndPoint);
                    Console.WriteLine("Data recieved: {0}", Encoding.UTF8.GetString(bytRecieved, 0, bytRecieved.Length));
                }
                catch (Exception e)
                {
                    MessageBox.Show("Tally Client Error\n");
                    Console.WriteLine(e.Message);
                    return;
                }


                // get byte data TODO: swap out 'false' for the actualy tally data.

                int newState = false ? 1 : 0;

                if (newState != _tallyState && TallyChanged != null)
                {
                    TallyChangedEventArgs args = new TallyChangedEventArgs();
                    args.state = newState;
                    TallyChanged(args);
                }
                _tallyState = newState;

            }

        }
    }
}
