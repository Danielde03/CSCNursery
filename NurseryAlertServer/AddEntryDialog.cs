/*
 *   CSC Nursery
 *    Add Entry Dialog
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

namespace NurseryAlertServer
{
    public partial class AddEntryDialog : Form
    {
        public string EntryText
        {
            get
            {
                return textBox1.Text;
            }
        }

        public bool EntryEmergency
        {
            get
            {
                return checkBoxEmergency.Checked;
            }
        }

        public AddEntryDialog()
        {
            InitializeComponent();
            this.AcceptButton = buttonSend;
            textBox1.TextChanged += new EventHandler(textBox1_TextChanged);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                buttonSend.Enabled = false;
            }
            else
            {
                buttonSend.Enabled = true;
            }

        }
    }
}
