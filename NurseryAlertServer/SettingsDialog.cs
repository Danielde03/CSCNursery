/*
 *   CSC Nursery
 *    Settings Dialog
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
            numericUpDownCasparPort.Value = Settings.Default.CasparCG_Port;
            textBoxCasparIp.Text = Settings.Default.CasparCG_IP;
            textBoxGraphicChannel.Text = Settings.Default.Graphic_Channel;
            textBoxGraphicLayer.Text = Settings.Default.Graphic_Layer;
            textBoxTemplate.Text = Settings.Default.Caspar_Template;
            textBoxTextField.Text = Settings.Default.Graphic_Text_Field;
            comboBoxTSLPort.Text = Settings.Default.Tally_Address;
        }

        private void UpdateSettings()
        {
            Settings.Default.WebPort = (int)numericUpDownWebPort.Value;
            Settings.Default.NotificationSound = textBoxNotification.Text;
            Settings.Default.CasparCG_Port = (int)numericUpDownCasparPort.Value;
            Settings.Default.CasparCG_IP = textBoxCasparIp.Text;
            Settings.Default.Graphic_Channel = textBoxGraphicChannel.Text;
            Settings.Default.Graphic_Layer = textBoxGraphicLayer.Text;
            Settings.Default.Caspar_Template = textBoxTemplate.Text;
            Settings.Default.Graphic_Text_Field = textBoxTextField.Text;
            Settings.Default.Tally_Address = comboBoxTSLPort.Text;

            // reconnect to CasparCG
            Projection.CasparManager.Instance.connect();
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
