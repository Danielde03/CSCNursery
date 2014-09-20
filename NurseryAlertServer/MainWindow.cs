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

        private int tallyState;

        /// <summary>
        /// Private constructor
        /// </summary>
        private MainWindow()
        {
            InitializeComponent();
            tallyState = 0;
        }

        /// <summary>
        /// Provide singleton access to the Main GUI Window
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

            PagerList.Instance.PagerListUpdated += new PagerList.PagerListUpdate(PagerListUpdateHandler);

            Tally.TallyManager.Instance.TallyChanged += new Tally.TallyManager.TallyChange(TallyChangedHandler);
            try
            {
                Tally.TallyManager.Instance.OpenTallyPort();
            }
            catch (IOException ex)
            {
                MessageBox.Show("Failed opening COM port\n" + ex.Message);
            }

            Web.HttpManager.Instance.StartServer();
        }

        /// <summary>
        /// Close the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            Web.HttpManager.Instance.StopServer();
        }

        /// <summary>
        /// Display the Pager text on the screen output
        /// </summary>
        /// <param name="text">Text to display</param>
        private void DisplayPagerText(String text)
        {
            var lt = new Projection.LiveText(text);
            Projection.ProjectionManager.Instance.DisplayLayer(2, lt);
            toolStripStatusLabelCurrentDisplay.Text = text;
        }

        /// <summary>
        /// Callback indicating a tally change has occurred
        ///  This runs in the serial port thread
        /// </summary>
        /// <param name="e">TallyChangedEventArgs containing the tally state</param>
        private void TallyChangedHandler(Tally.TallyManager.TallyChangedEventArgs e)
        {
            //Invoke a delgate to run under the UI thread
            this.Invoke((MethodInvoker)delegate
            {
                tallyStateChangeUIDelegate(e.state);
            });
        }

        /// <summary>
        /// Callback indicating the Pager list has changed
        ///  This may be running in a non-UI thread
        /// </summary>
        private void PagerListUpdateHandler()
        {
            this.Invoke((MethodInvoker)delegate
            {
                UpdatePagerList();
            });
        }

        /// <summary>
        /// Update the UI with the Pager list.
        /// </summary>
        private void UpdatePagerList()
        {
            List<PagerList.PagerEntry> plist = new List<PagerList.PagerEntry>();
            String text = "";
            PagerList.Instance.GetState(ref plist, ref text);
            listViewEntries.Items.Clear();
            foreach (var entry in plist)
            {
                ListViewItem item = new ListViewItem(entry.pagerText);
                item.SubItems.Add(entry.emergency);
                item.SubItems.Add(entry.outstanding);
                item.SubItems.Add(entry.time);
                item.SubItems.Add(entry.displayTime);
                listViewEntries.Items.Insert(0, item);
            }
            DisplayPagerText(text);
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
                PagerList.Instance.AddEntry(dlg.EntryText, dlg.EntryEmergency);
            }
        }

        /// <summary>
        /// Handler for the Mark Displayed button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonMarkDisplayed_Click(object sender, EventArgs e)
        {
            PagerList.Instance.ClearDisplayedItems();
        }

        /// <summary>
        /// Handler for the Remove All button
        ///  Clears the pager list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonRemoveAll_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to remove all items?", "Confirm Remove All",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                == DialogResult.Yes)
            {
                PagerList.Instance.ClearList();
            }
        }

        /// <summary>
        /// UI-safe function to update the tally state
        /// </summary>
        /// <param name="newState">New tally state value</param>
        private void tallyStateChangeUIDelegate(int newState)
        {
            int lastState = tallyState;
            tallyState = newState;
            if ((tallyState == 1) && (lastState == 0))
            {
                //Screen is being displayed
                toolStripButtonRemoveAll.Enabled = false;
            }
            else if ((tallyState == 0) && (lastState == 1))
            {
                //Screen has stopped displaying
                toolStripButtonRemoveAll.Enabled = true;
            }
        }

        /// <summary>
        /// Handler for the Settings menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void preferencesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SettingsDialog dlg = new SettingsDialog();
            dlg.ShowDialog();

            if (dlg.DialogResult == DialogResult.OK)
            {
                Console.WriteLine("Settings Updated");
            }
        }

    }
}
