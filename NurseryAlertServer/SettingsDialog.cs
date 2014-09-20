using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Controls;
using NurseryAlertServer.Properties;

namespace NurseryAlertServer
{
    public partial class SettingsDialog : Form
    {
        public SettingsDialog()
        {
            InitializeComponent();
            UpdateUI();
        }

        private void UpdateUI()
        {
            numericUpDownWebPort.Value = Settings.Default.WebPort;
            textBoxNotification.Text = Settings.Default.NotificationSound;

            String[] PortList = Tally.TallyManager.Instance.GetPorts();
            comboBoxComPort.DataSource = PortList;
            if(PortList.Contains(Settings.Default.TallyComPort))
                comboBoxComPort.Text = Settings.Default.TallyComPort;
        }

        private void UpdateSettings()
        {
            Settings.Default.WebPort = (int)numericUpDownWebPort.Value;
            Settings.Default.NotificationSound = textBoxNotification.Text;
            Settings.Default.TallyComPort = comboBoxComPort.Text;
            Settings.Default.Save();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            UpdateSettings();
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = textBoxNotification.Text;
            dlg.Filter = "wav files (*.wav)|*.wav|All files (*.*)|*.*";
            dlg.FilterIndex = 1;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                textBoxNotification.Text = dlg.FileName;
            }
        }
    }
}
