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
            Console.WriteLine("COM Ports:");
            foreach (string s in SerialPort.GetPortNames())
            {
                Console.WriteLine(" {0}", s);
            }
            Console.WriteLine("Using {0}", Settings.Default.TallyComPort);
            _serialPort.PortName = Settings.Default.TallyComPort;
            _serialPort.PinChanged += new SerialPinChangedEventHandler(PinChangedEventHandler);
            _serialPort.Open();
        }

        /// <summary>
        /// Pin Chnaged Event Handler.
        /// </summary>
        /// <param name="sender">SerialPort object</param>
        /// <param name="e">SerialPinChangedEventArgs object</param>
        private void PinChangedEventHandler(object sender, SerialPinChangedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            Console.WriteLine("Pin Change {0} - BRK {1} DCD {2} CTS {3} DSR {4}",
                e.EventType, sp.BreakState, sp.CDHolding, sp.CtsHolding, sp.DsrHolding);

            //Assuming CTS for now
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
