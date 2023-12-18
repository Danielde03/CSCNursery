/*
 *   CSC Nursery
 *    Main Window Form
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
 *   Author: jdramer, Danielde03
 *
 *   Copyright (c) Holy Spirit
 *
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using NurseryAlertServer.Tally;

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

            Projection.CasparManager.Instance.clear(); // clear CasparCG

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
            Tally.TallyManager.Instance.CloseTallyPort();
        }

        /// <summary>
        /// Display the Pager text on the screen output
        /// </summary>
        /// <param name="text">Text to display</param>
        public void DisplayPagerText(String text)
        {
            Projection.CasparManager.Instance.writeToCaspar(text);
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
            //DisplayPagerText(text);
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
        /// Handler for the Mark Displayed button. If none are selected, mark all in display queue as displayed. 
        /// If some are selected, mark only those as displayed and remove only those from display queue.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonMarkDisplayed_Click(object sender, EventArgs e)
        {
            bool itemsSelected = false;

            // check if any selected
            for (int i = 0; i < listViewEntries.Items.Count; i++)
            {
                if (listViewEntries.Items[i].Selected)
                {
                    itemsSelected = true;
                    break;
                }
            }

            if (itemsSelected)
            {
                // remove specific items
                PagerList.Instance.ClearSelectedDisplayedItems();
            } else
            {
                // if none are selected
                PagerList.Instance.ClearDisplayedItems();
            }
            
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
                DisplayPagerText("");
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
                PagerList.Instance.LoadSettings();
                Web.HttpManager.Instance.PortChange();
            }
        }

        /// <summary>
        /// Handler for the About menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox dlg = new AboutBox();
            dlg.ShowDialog();
        }

    }
}
