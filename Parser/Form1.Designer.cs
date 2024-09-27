namespace Parser
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            inputToolStripMenuItem = new ToolStripMenuItem();
            yourModelsToolStripMenuItem = new ToolStripMenuItem();
            yourCompetitorsToolStripMenuItem = new ToolStripMenuItem();
            yourSiteToolStripMenuItem = new ToolStripMenuItem();
            outputToolStripMenuItem = new ToolStripMenuItem();
            updateDataInTableToolStripMenuItem = new ToolStripMenuItem();
            manualToolStripMenuItem = new ToolStripMenuItem();
            dataGridView1 = new DataGridView();
            saveFileDialog1 = new SaveFileDialog();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { inputToolStripMenuItem, outputToolStripMenuItem, updateDataInTableToolStripMenuItem, manualToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1440, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // inputToolStripMenuItem
            // 
            inputToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { yourModelsToolStripMenuItem, yourCompetitorsToolStripMenuItem, yourSiteToolStripMenuItem });
            inputToolStripMenuItem.Name = "inputToolStripMenuItem";
            inputToolStripMenuItem.Size = new Size(47, 20);
            inputToolStripMenuItem.Text = "Input";
            // 
            // yourModelsToolStripMenuItem
            // 
            yourModelsToolStripMenuItem.Name = "yourModelsToolStripMenuItem";
            yourModelsToolStripMenuItem.Size = new Size(165, 22);
            yourModelsToolStripMenuItem.Text = "Your models";
            yourModelsToolStripMenuItem.Click += yourModelsToolStripMenuItem_Click;
            // 
            // yourCompetitorsToolStripMenuItem
            // 
            yourCompetitorsToolStripMenuItem.Name = "yourCompetitorsToolStripMenuItem";
            yourCompetitorsToolStripMenuItem.Size = new Size(165, 22);
            yourCompetitorsToolStripMenuItem.Text = "Your competitors";
            yourCompetitorsToolStripMenuItem.Click += yourCompetitorsToolStripMenuItem_Click;
            // 
            // yourSiteToolStripMenuItem
            // 
            yourSiteToolStripMenuItem.Name = "yourSiteToolStripMenuItem";
            yourSiteToolStripMenuItem.Size = new Size(165, 22);
            yourSiteToolStripMenuItem.Text = "Your site";
            yourSiteToolStripMenuItem.Click += yourSiteToolStripMenuItem_Click;
            // 
            // outputToolStripMenuItem
            // 
            outputToolStripMenuItem.Name = "outputToolStripMenuItem";
            outputToolStripMenuItem.Size = new Size(57, 20);
            outputToolStripMenuItem.Text = "Output";
            outputToolStripMenuItem.Click += outputToolStripMenuItem_Click;
            // 
            // updateDataInTableToolStripMenuItem
            // 
            updateDataInTableToolStripMenuItem.Name = "updateDataInTableToolStripMenuItem";
            updateDataInTableToolStripMenuItem.Size = new Size(125, 20);
            updateDataInTableToolStripMenuItem.Text = "Update data in table";
            updateDataInTableToolStripMenuItem.Click += updateDataInTableToolStripMenuItem_Click;
            // 
            // manualToolStripMenuItem
            // 
            manualToolStripMenuItem.Name = "manualToolStripMenuItem";
            manualToolStripMenuItem.Size = new Size(59, 20);
            manualToolStripMenuItem.Text = "Manual";
            manualToolStripMenuItem.Click += manualToolStripMenuItem_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(0, 27);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(1440, 764);
            dataGridView1.TabIndex = 1;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1440, 792);
            Controls.Add(dataGridView1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "Parser";
            Load += Form1_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem inputToolStripMenuItem;
        private ToolStripMenuItem yourModelsToolStripMenuItem;
        private ToolStripMenuItem yourCompetitorsToolStripMenuItem;
        private ToolStripMenuItem yourSiteToolStripMenuItem;
        private ToolStripMenuItem outputToolStripMenuItem;
        private DataGridView dataGridView1;
        private SaveFileDialog saveFileDialog1;
        private ToolStripMenuItem updateDataInTableToolStripMenuItem;
        private ToolStripMenuItem manualToolStripMenuItem;
    }
}
