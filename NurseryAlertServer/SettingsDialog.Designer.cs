using NurseryAlertServer.Properties;

namespace NurseryAlertServer
{
    partial class SettingsDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.numericUpDownWebPort = new System.Windows.Forms.NumericUpDown();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.textBoxNotification = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBoxTallyAdd = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.numericUpDownCasparPort = new System.Windows.Forms.NumericUpDown();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.textBoxTemplate = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.textBoxCasparIp = new System.Windows.Forms.TextBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.textBoxGraphicChannel = new System.Windows.Forms.TextBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.textBoxGraphicLayer = new System.Windows.Forms.TextBox();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.textBoxTextField = new System.Windows.Forms.TextBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.textBoxtslport = new System.Windows.Forms.TextBox();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.textBoxthreshold = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWebPort)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCasparPort)).BeginInit();
            this.groupBox8.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.groupBox11.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(13, 13);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(328, 231);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox11);
            this.tabPage1.Controls.Add(this.groupBox10);
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(320, 205);
            this.tabPage1.TabIndex = 1;
            this.tabPage1.Text = "Settings";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.numericUpDownWebPort);
            this.groupBox3.Location = new System.Drawing.Point(157, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(154, 55);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Web Port";
            // 
            // numericUpDownWebPort
            // 
            this.numericUpDownWebPort.Location = new System.Drawing.Point(7, 20);
            this.numericUpDownWebPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numericUpDownWebPort.Name = "numericUpDownWebPort";
            this.numericUpDownWebPort.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownWebPort.TabIndex = 0;
            this.numericUpDownWebPort.Value = new decimal(new int[] {
            80,
            0,
            0,
            0});
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonBrowse);
            this.groupBox2.Controls.Add(this.textBoxNotification);
            this.groupBox2.Location = new System.Drawing.Point(6, 122);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(305, 80);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Notification Sound";
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.Location = new System.Drawing.Point(224, 48);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(75, 23);
            this.buttonBrowse.TabIndex = 1;
            this.buttonBrowse.Text = "Browse...";
            this.buttonBrowse.UseVisualStyleBackColor = true;
            this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
            // 
            // textBoxNotification
            // 
            this.textBoxNotification.Location = new System.Drawing.Point(10, 22);
            this.textBoxNotification.Name = "textBoxNotification";
            this.textBoxNotification.Size = new System.Drawing.Size(289, 20);
            this.textBoxNotification.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBoxTallyAdd);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(145, 55);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tally Address";
            // 
            // comboBoxTallyAdd
            // 
            this.comboBoxTallyAdd.Location = new System.Drawing.Point(7, 20);
            this.comboBoxTallyAdd.Name = "comboBoxTallyAdd";
            this.comboBoxTallyAdd.Size = new System.Drawing.Size(121, 20);
            this.comboBoxTallyAdd.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox4);
            this.tabPage2.Controls.Add(this.groupBox8);
            this.tabPage2.Controls.Add(this.groupBox5);
            this.tabPage2.Controls.Add(this.groupBox6);
            this.tabPage2.Controls.Add(this.groupBox7);
            this.tabPage2.Controls.Add(this.groupBox9);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(320, 205);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "CasparCG";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.numericUpDownCasparPort);
            this.groupBox4.Location = new System.Drawing.Point(165, 15);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(147, 55);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Port";
            // 
            // numericUpDownCasparPort
            // 
            this.numericUpDownCasparPort.Location = new System.Drawing.Point(7, 20);
            this.numericUpDownCasparPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numericUpDownCasparPort.Name = "numericUpDownCasparPort";
            this.numericUpDownCasparPort.Size = new System.Drawing.Size(133, 20);
            this.numericUpDownCasparPort.TabIndex = 0;
            this.numericUpDownCasparPort.Value = new decimal(new int[] {
            80,
            0,
            0,
            0});
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.textBoxTemplate);
            this.groupBox8.Location = new System.Drawing.Point(7, 76);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(146, 55);
            this.groupBox8.TabIndex = 1;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Graphic HTML Template";
            // 
            // textBoxTemplate
            // 
            this.textBoxTemplate.Location = new System.Drawing.Point(10, 26);
            this.textBoxTemplate.Name = "textBoxTemplate";
            this.textBoxTemplate.Size = new System.Drawing.Size(130, 20);
            this.textBoxTemplate.TabIndex = 0;
            this.textBoxTemplate.TextChanged += new System.EventHandler(this.textBoxTemplate_TextChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.textBoxCasparIp);
            this.groupBox5.Location = new System.Drawing.Point(6, 15);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(147, 55);
            this.groupBox5.TabIndex = 1;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "IP Address";
            // 
            // textBoxCasparIp
            // 
            this.textBoxCasparIp.Location = new System.Drawing.Point(10, 22);
            this.textBoxCasparIp.Name = "textBoxCasparIp";
            this.textBoxCasparIp.Size = new System.Drawing.Size(130, 20);
            this.textBoxCasparIp.TabIndex = 0;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.textBoxGraphicChannel);
            this.groupBox6.Location = new System.Drawing.Point(166, 76);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(146, 55);
            this.groupBox6.TabIndex = 1;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Graphic Channel";
            // 
            // textBoxGraphicChannel
            // 
            this.textBoxGraphicChannel.Location = new System.Drawing.Point(10, 22);
            this.textBoxGraphicChannel.Name = "textBoxGraphicChannel";
            this.textBoxGraphicChannel.Size = new System.Drawing.Size(130, 20);
            this.textBoxGraphicChannel.TabIndex = 0;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.textBoxGraphicLayer);
            this.groupBox7.Location = new System.Drawing.Point(7, 137);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(146, 55);
            this.groupBox7.TabIndex = 1;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Graphic Layer";
            this.groupBox7.Enter += new System.EventHandler(this.groupBox7_Enter);
            // 
            // textBoxGraphicLayer
            // 
            this.textBoxGraphicLayer.Location = new System.Drawing.Point(10, 19);
            this.textBoxGraphicLayer.Name = "textBoxGraphicLayer";
            this.textBoxGraphicLayer.Size = new System.Drawing.Size(130, 20);
            this.textBoxGraphicLayer.TabIndex = 0;
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.textBoxTextField);
            this.groupBox9.Location = new System.Drawing.Point(166, 137);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(146, 55);
            this.groupBox9.TabIndex = 1;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Graphic Text Field";
            this.groupBox9.Enter += new System.EventHandler(this.groupBox9_Enter);
            // 
            // textBoxTextField
            // 
            this.textBoxTextField.Location = new System.Drawing.Point(11, 22);
            this.textBoxTextField.Name = "textBoxTextField";
            this.textBoxTextField.Size = new System.Drawing.Size(129, 20);
            this.textBoxTextField.TabIndex = 0;
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(263, 250);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(182, 250);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 2;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.textBoxtslport);
            this.groupBox10.Location = new System.Drawing.Point(6, 67);
            this.groupBox10.Name = "tslport";
            this.groupBox10.Size = new System.Drawing.Size(145, 55);
            this.groupBox10.TabIndex = 1;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "TSL Port";
            this.groupBox10.Enter += new System.EventHandler(this.groupBox10_Enter);
            // 
            // textBoxtslport
            // 
            this.textBoxtslport.Location = new System.Drawing.Point(7, 20);
            this.textBoxtslport.Name = "textBoxtslport";
            this.textBoxtslport.Size = new System.Drawing.Size(121, 20);
            this.textBoxtslport.TabIndex = 0;
            // 
            // groupBox11
            // 
            this.groupBox11.Controls.Add(this.textBoxthreshold);
            this.groupBox11.Location = new System.Drawing.Point(157, 67);
            this.groupBox11.Name = "threshold";
            this.groupBox11.Size = new System.Drawing.Size(154, 55);
            this.groupBox11.TabIndex = 2;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "Number Threshold";
            // 
            // textBoxthreshold
            // 
            this.textBoxthreshold.Location = new System.Drawing.Point(7, 20);
            this.textBoxthreshold.Name = "textBoxthreshold";
            this.textBoxthreshold.Size = new System.Drawing.Size(121, 20);
            this.textBoxthreshold.TabIndex = 0;
            // 
            // SettingsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(353, 280);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.tabControl1);
            this.Name = "SettingsDialog";
            this.Text = "Settings";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWebPort)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCasparPort)).EndInit();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            this.groupBox11.ResumeLayout(false);
            this.groupBox11.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.NumericUpDown numericUpDownWebPort;
        private System.Windows.Forms.NumericUpDown numericUpDownCasparPort;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.TextBox textBoxNotification;
        private System.Windows.Forms.TextBox textBoxCasparIp;
        private System.Windows.Forms.TextBox textBoxGraphicChannel;
        private System.Windows.Forms.TextBox textBoxGraphicLayer;
        private System.Windows.Forms.TextBox textBoxTemplate;
        private System.Windows.Forms.TextBox textBoxTextField;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox comboBoxTallyAdd;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.GroupBox groupBox11;
        private System.Windows.Forms.TextBox textBoxthreshold;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.TextBox textBoxtslport;
    }
}