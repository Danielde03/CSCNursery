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
using NurseryAlertServer.Properties;

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

        private static SerialPort _serialPort;

        private int _tallyState;

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
            _serialPort = new SerialPort();
            _tallyState = 0;
        }

        /// <summary>
        /// Start monitoring the tally port
        /// </summary>
        public void OpenTallyPort()
        {
            String[] PortList = GetPorts();
            String port;
            if (PortList.Length <= 0)
            {
                port = Settings.Default.TallyComPort;
            }
            else
            {
                if (PortList.Contains(Settings.Default.TallyComPort))
                {
                    Console.WriteLine("Use selected");
                    port = Settings.Default.TallyComPort;
                }
                else
                {
                    Console.WriteLine("Use first");
                    port = PortList[0];
                }
            }
            Console.WriteLine("using {0}", port);
            _serialPort.PortName = port;
            _serialPort.PinChanged += new SerialPinChangedEventHandler(PinChangedEventHandler);
            _serialPort.Open();
            _serialPort.RtsEnable = true;
        }

        /// <summary>
        /// Reopen the tally port
        /// </summary>
        public void ReopenTallyPort()
        {
            if (!_serialPort.PortName.Equals(Settings.Default.TallyComPort))
            {
                Console.WriteLine("Reopen COM port");
                _serialPort.Close();
                _serialPort.PortName = Settings.Default.TallyComPort;
                _serialPort.Open();
                _serialPort.RtsEnable = true;
            }
        }

        /// <summary>
        /// Gets a list of available COM ports
        /// </summary>
        public String[] GetPorts()
        {
            String[] portList = SerialPort.GetPortNames();
            Console.WriteLine("COM Ports:");
            foreach (string s in portList)
            {
                Console.WriteLine(" {0}", s);
            }

            return portList;
        }

        /// <summary>
        /// Pin Changed Event Handler.
        /// </summary>
        /// <param name="sender">SerialPort object</param>
        /// <param name="e">SerialPinChangedEventArgs object</param>
        private void PinChangedEventHandler(object sender, SerialPinChangedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            //Console.WriteLine("Pin Change {0} - BRK {1} DCD {2} CTS {3} DSR {4}",
            //    e.EventType, sp.BreakState, sp.CDHolding, sp.CtsHolding, sp.DsrHolding);

            int newState = sp.CtsHolding ? 1 : 0;

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
