using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NurseryAlertServer
{
    /// <summary>
    /// The main window class provides the central gui.
    /// </summary>
    public partial class MainWindow : Form
    {
        private static MainWindow _instance;
        private static readonly object singletonPadlock = new object();

        /// <summary>
        /// Private constructor
        /// </summary>
        private MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// The main window class provides the central gui.
        /// </summary>
        /// 
        public static MainWindow Instance
        {
            get
            {
                lock (singletonPadlock)
                {
                    return _instance ?? (_instance = new MainWindow());
                }
            }
        }
    }
}
