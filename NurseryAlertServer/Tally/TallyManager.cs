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

        /// <summary>
        /// The constructor
        /// </summary>
        private TallyManager()
        {
            _serialPort = new SerialPort();
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

        private static void PinChangedEventHandler(object sender, SerialPinChangedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            Console.WriteLine("Pin Change {0} - BRK {1} DCD {2} CTS {3} DSR {4}",
                e.EventType, sp.BreakState, sp.CDHolding, sp.CtsHolding, sp.DsrHolding);
        }
    }
}
