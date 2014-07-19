namespace NurseryAlertServer
{
    partial class MainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.listViewEntries = new System.Windows.Forms.ListView();
            this.colEntryId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colEmergency = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDateRecv = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDateDisplay = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.preferencesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.preserencesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonAddEntry = new System.Windows.Forms.ToolStripButton();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listViewEntries
            // 
            this.listViewEntries.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colEntryId,
            this.colEmergency,
            this.colDateRecv,
            this.colDateDisplay});
            this.listViewEntries.GridLines = true;
            this.listViewEntries.Location = new System.Drawing.Point(12, 81);
            this.listViewEntries.Name = "listViewEntries";
            this.listViewEntries.Size = new System.Drawing.Size(439, 201);
            this.listViewEntries.TabIndex = 0;
            this.listViewEntries.UseCompatibleStateImageBehavior = false;
            this.listViewEntries.View = System.Windows.Forms.View.Details;
            // 
            // colEntryId
            // 
            this.colEntryId.Text = "Entry ID";
            // 
            // colEmergency
            // 
            this.colEmergency.Text = "E";
            this.colEmergency.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colEmergency.Width = 22;
            // 
            // colDateRecv
            // 
            this.colDateRecv.Text = "Received";
            this.colDateRecv.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colDateRecv.Width = 100;
            // 
            // colDateDisplay
            // 
            this.colDateDisplay.Text = "Last Displayed";
            this.colDateDisplay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colDateDisplay.Width = 100;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.preferencesToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(461, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // preferencesToolStripMenuItem
            // 
            this.preferencesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.preserencesToolStripMenuItem});
            this.preferencesToolStripMenuItem.Name = "preferencesToolStripMenuItem";
            this.preferencesToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.preferencesToolStripMenuItem.Text = "Settings";
            // 
            // preserencesToolStripMenuItem
            // 
            this.preserencesToolStripMenuItem.Name = "preserencesToolStripMenuItem";
            this.preserencesToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.preserencesToolStripMenuItem.Text = "Preferences...";
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(40, 40);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonAddEntry});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(461, 47);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonAddEntry
            // 
            this.toolStripButtonAddEntry.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAddEntry.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAddEntry.Image")));
            this.toolStripButtonAddEntry.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAddEntry.Name = "toolStripButtonAddEntry";
            this.toolStripButtonAddEntry.Size = new System.Drawing.Size(44, 44);
            this.toolStripButtonAddEntry.Text = "Add an entry";
            this.toolStripButtonAddEntry.Click += new System.EventHandler(this.toolStripButtonAddEntry_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(461, 294);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.listViewEntries);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainWindow";
            this.Text = "Nursery Alert Server";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listViewEntries;
        private System.Windows.Forms.ColumnHeader colEntryId;
        private System.Windows.Forms.ColumnHeader colEmergency;
        private System.Windows.Forms.ColumnHeader colDateRecv;
        private System.Windows.Forms.ColumnHeader colDateDisplay;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem preferencesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem preserencesToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonAddEntry;
    }
}

