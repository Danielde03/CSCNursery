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
