using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
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

        private string displayText;

        /// <summary>
        /// Private constructor
        /// </summary>
        private MainWindow()
        {
            InitializeComponent();
            displayText = "";
        }

        /// <summary>
        /// The main window class provides the central gui.
        /// </summary>
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

        /// <summary>
        /// Initialize the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_Load(object sender, EventArgs e)
        {
            Console.WriteLine("MainWindow Load");

            Projection.ProjectionManager.Instance.ShowProjectionWindow();

            Tally.TallyManager.Instance.TallyChanged += new Tally.TallyManager.TallyChange(TallyChangedHandler);
            try
            {
                Tally.TallyManager.Instance.OpenTallyPort();
            }
            catch (IOException ex)
            {
                MessageBox.Show("Failed opening COM port\n" + ex.Message);
            }
        }

        /// <summary>
        /// Display some text on the screen
        /// </summary>
        /// <param name="entryText">Entry ID text string to display</param>
        private void DisplayText(string entryText)
        {
            if (String.IsNullOrEmpty(displayText))
            {
                displayText = entryText;
            }
            else
            {
                displayText += " , ";
                displayText += entryText;
            }
            var lt = new Projection.LiveText(displayText);
            Projection.ProjectionManager.Instance.DisplayLayer(2, lt);
        }

        /// <summary>
        /// Callback indicating a tally change has occurred
        /// </summary>
        /// <param name="e">TallyChangedEventArgs containing the tally state</param>
        private void TallyChangedHandler(Tally.TallyManager.TallyChangedEventArgs e)
        {
            Console.WriteLine("Tally {0}",e.state);
        }

        /// <summary>
        /// Handler for the Add Entry button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonAddEntry_Click(object sender, EventArgs e)
        {
            AddEntryDialog dlg = new AddEntryDialog();
            dlg.ShowDialog();

            if (dlg.DialogResult == DialogResult.OK)
            {
                AddEntry(dlg.EntryText, dlg.EntryEmergency);
            }
        }

        /// <summary>
        /// Add an Entry to the entries window
        /// </summary>
        /// <param name="entryText">Entry ID text string to add</param>
        /// <param name="emergency">Indicates if emergency</param>
        private void AddEntry(string entryText, bool emergency)
        {
            ListViewItem item = new ListViewItem(entryText);
            item.SubItems.Add(emergency ? "Yes" : "");
            item.SubItems.Add("Yes");
            DateTime current = DateTime.Now;
            item.SubItems.Add(current.ToString("h:mm:ss tt"));
            item.SubItems.Add("--Never--");
            listViewEntries.Items.Insert(0,item);
            DisplayText(entryText);
        }

    }
}
