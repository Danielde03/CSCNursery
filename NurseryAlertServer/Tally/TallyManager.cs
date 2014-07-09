using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

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
        }
    }
}
