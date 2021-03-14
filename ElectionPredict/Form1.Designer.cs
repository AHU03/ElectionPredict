namespace ElectionPredict
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panel1 = new System.Windows.Forms.Panel();
            this.listYears = new System.Windows.Forms.ListBox();
            this.listPublications = new System.Windows.Forms.ListBox();
            this.SubtitleHistorical = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.Title = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.RemarkLabel = new System.Windows.Forms.Label();
            this.DemVotesPanel = new System.Windows.Forms.Panel();
            this.DemVotesLabel = new System.Windows.Forms.Label();
            this.RepVotesPanel = new System.Windows.Forms.Panel();
            this.RepVotesLabel = new System.Windows.Forms.Label();
            this.CandidatesLabel = new System.Windows.Forms.Label();
            this.TitleLabel = new System.Windows.Forms.Label();
            this.mappanel = new System.Windows.Forms.Panel();
            this.KeyGroupBox = new System.Windows.Forms.GroupBox();
            this.ExitLabel = new System.Windows.Forms.Label();
            this.MinimizeLabel = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.DemVotesPanel.SuspendLayout();
            this.RepVotesPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.panel1.Controls.Add(this.listYears);
            this.panel1.Controls.Add(this.listPublications);
            this.panel1.Controls.Add(this.SubtitleHistorical);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.Title);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(206, 783);
            this.panel1.TabIndex = 0;
            // 
            // listYears
            // 
            this.listYears.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.listYears.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listYears.ForeColor = System.Drawing.Color.FloralWhite;
            this.listYears.FormattingEnabled = true;
            this.listYears.ItemHeight = 18;
            this.listYears.Items.AddRange(new object[] {
            " "});
            this.listYears.Location = new System.Drawing.Point(141, 107);
            this.listYears.Name = "listYears";
            this.listYears.Size = new System.Drawing.Size(59, 130);
            this.listYears.TabIndex = 5;
            // 
            // listPublications
            // 
            this.listPublications.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.listPublications.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listPublications.ForeColor = System.Drawing.Color.FloralWhite;
            this.listPublications.FormattingEnabled = true;
            this.listPublications.ItemHeight = 18;
            this.listPublications.Items.AddRange(new object[] {
            "Real Results",
            "Literary Digest",
            "Gallup"});
            this.listPublications.Location = new System.Drawing.Point(4, 107);
            this.listPublications.Name = "listPublications";
            this.listPublications.Size = new System.Drawing.Size(131, 130);
            this.listPublications.TabIndex = 4;
            this.listPublications.SelectedIndexChanged += new System.EventHandler(this.listPublications_SelectedIndexChanged);
            // 
            // SubtitleHistorical
            // 
            this.SubtitleHistorical.AutoSize = true;
            this.SubtitleHistorical.Font = new System.Drawing.Font("Consolas", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SubtitleHistorical.ForeColor = System.Drawing.Color.FloralWhite;
            this.SubtitleHistorical.Location = new System.Drawing.Point(2, 81);
            this.SubtitleHistorical.Name = "SubtitleHistorical";
            this.SubtitleHistorical.Size = new System.Drawing.Size(110, 22);
            this.SubtitleHistorical.TabIndex = 3;
            this.SubtitleHistorical.Text = "Historical";
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.FloralWhite;
            this.button1.Location = new System.Drawing.Point(5, 727);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(196, 50);
            this.button1.TabIndex = 1;
            this.button1.Text = "Show Results";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Title
            // 
            this.Title.AutoSize = true;
            this.Title.Dock = System.Windows.Forms.DockStyle.Top;
            this.Title.Font = new System.Drawing.Font("Consolas", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Title.ForeColor = System.Drawing.Color.FloralWhite;
            this.Title.Location = new System.Drawing.Point(0, 0);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(175, 68);
            this.Title.TabIndex = 0;
            this.Title.Text = "Election\r\nSimulation";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.panel2.Controls.Add(this.KeyGroupBox);
            this.panel2.Controls.Add(this.RemarkLabel);
            this.panel2.Controls.Add(this.DemVotesPanel);
            this.panel2.Controls.Add(this.RepVotesPanel);
            this.panel2.Controls.Add(this.CandidatesLabel);
            this.panel2.Controls.Add(this.TitleLabel);
            this.panel2.Controls.Add(this.mappanel);
            this.panel2.Location = new System.Drawing.Point(181, 24);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(994, 759);
            this.panel2.TabIndex = 1;
            // 
            // RemarkLabel
            // 
            this.RemarkLabel.AutoSize = true;
            this.RemarkLabel.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RemarkLabel.ForeColor = System.Drawing.Color.FloralWhite;
            this.RemarkLabel.Location = new System.Drawing.Point(45, 736);
            this.RemarkLabel.Name = "RemarkLabel";
            this.RemarkLabel.Size = new System.Drawing.Size(14, 15);
            this.RemarkLabel.TabIndex = 0;
            this.RemarkLabel.Text = " ";
            // 
            // DemVotesPanel
            // 
            this.DemVotesPanel.Controls.Add(this.DemVotesLabel);
            this.DemVotesPanel.Location = new System.Drawing.Point(793, 178);
            this.DemVotesPanel.Name = "DemVotesPanel";
            this.DemVotesPanel.Size = new System.Drawing.Size(135, 28);
            this.DemVotesPanel.TabIndex = 9;
            // 
            // DemVotesLabel
            // 
            this.DemVotesLabel.AutoSize = true;
            this.DemVotesLabel.Dock = System.Windows.Forms.DockStyle.Right;
            this.DemVotesLabel.Font = new System.Drawing.Font("Consolas", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DemVotesLabel.ForeColor = System.Drawing.Color.FloralWhite;
            this.DemVotesLabel.Location = new System.Drawing.Point(97, 0);
            this.DemVotesLabel.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.DemVotesLabel.Name = "DemVotesLabel";
            this.DemVotesLabel.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.DemVotesLabel.Size = new System.Drawing.Size(38, 28);
            this.DemVotesLabel.TabIndex = 7;
            this.DemVotesLabel.Text = "  ";
            this.DemVotesLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // RepVotesPanel
            // 
            this.RepVotesPanel.Controls.Add(this.RepVotesLabel);
            this.RepVotesPanel.Location = new System.Drawing.Point(45, 178);
            this.RepVotesPanel.Name = "RepVotesPanel";
            this.RepVotesPanel.Size = new System.Drawing.Size(45, 29);
            this.RepVotesPanel.TabIndex = 8;
            // 
            // RepVotesLabel
            // 
            this.RepVotesLabel.AutoSize = true;
            this.RepVotesLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.RepVotesLabel.Font = new System.Drawing.Font("Consolas", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RepVotesLabel.ForeColor = System.Drawing.Color.FloralWhite;
            this.RepVotesLabel.Location = new System.Drawing.Point(0, 0);
            this.RepVotesLabel.Name = "RepVotesLabel";
            this.RepVotesLabel.Size = new System.Drawing.Size(25, 28);
            this.RepVotesLabel.TabIndex = 6;
            this.RepVotesLabel.Text = " ";
            // 
            // CandidatesLabel
            // 
            this.CandidatesLabel.AutoSize = true;
            this.CandidatesLabel.Font = new System.Drawing.Font("Consolas", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CandidatesLabel.ForeColor = System.Drawing.Color.FloralWhite;
            this.CandidatesLabel.Location = new System.Drawing.Point(43, 99);
            this.CandidatesLabel.Name = "CandidatesLabel";
            this.CandidatesLabel.Size = new System.Drawing.Size(25, 28);
            this.CandidatesLabel.TabIndex = 5;
            this.CandidatesLabel.Text = " ";
            // 
            // TitleLabel
            // 
            this.TitleLabel.AutoSize = true;
            this.TitleLabel.Font = new System.Drawing.Font("Consolas", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TitleLabel.ForeColor = System.Drawing.Color.FloralWhite;
            this.TitleLabel.Location = new System.Drawing.Point(42, 57);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(31, 34);
            this.TitleLabel.TabIndex = 4;
            this.TitleLabel.Text = " ";
            // 
            // mappanel
            // 
            this.mappanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.mappanel.Location = new System.Drawing.Point(45, 195);
            this.mappanel.Name = "mappanel";
            this.mappanel.Size = new System.Drawing.Size(883, 538);
            this.mappanel.TabIndex = 0;
            this.mappanel.Visible = false;
            // 
            // KeyGroupBox
            // 
            this.KeyGroupBox.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.KeyGroupBox.ForeColor = System.Drawing.Color.FloralWhite;
            this.KeyGroupBox.Location = new System.Drawing.Point(782, 537);
            this.KeyGroupBox.Name = "KeyGroupBox";
            this.KeyGroupBox.Size = new System.Drawing.Size(179, 130);
            this.KeyGroupBox.TabIndex = 0;
            this.KeyGroupBox.TabStop = false;
            this.KeyGroupBox.Text = "Key";
            this.KeyGroupBox.Visible = false;
            // 
            // ExitLabel
            // 
            this.ExitLabel.AutoSize = true;
            this.ExitLabel.Font = new System.Drawing.Font("Consolas", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExitLabel.ForeColor = System.Drawing.Color.FloralWhite;
            this.ExitLabel.Location = new System.Drawing.Point(1150, -3);
            this.ExitLabel.Name = "ExitLabel";
            this.ExitLabel.Size = new System.Drawing.Size(25, 28);
            this.ExitLabel.TabIndex = 2;
            this.ExitLabel.Text = "x";
            this.ExitLabel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ExitLabel_MouseClick);
            this.ExitLabel.MouseEnter += new System.EventHandler(this.ExitLabel_MouseEnter);
            this.ExitLabel.MouseLeave += new System.EventHandler(this.ExitLabel_MouseLeave);
            // 
            // MinimizeLabel
            // 
            this.MinimizeLabel.AutoSize = true;
            this.MinimizeLabel.Font = new System.Drawing.Font("Consolas", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinimizeLabel.ForeColor = System.Drawing.Color.FloralWhite;
            this.MinimizeLabel.Location = new System.Drawing.Point(1131, -8);
            this.MinimizeLabel.Name = "MinimizeLabel";
            this.MinimizeLabel.Size = new System.Drawing.Size(25, 28);
            this.MinimizeLabel.TabIndex = 3;
            this.MinimizeLabel.Text = "_";
            this.MinimizeLabel.Click += new System.EventHandler(this.MinimizeLabel_Click);
            this.MinimizeLabel.MouseEnter += new System.EventHandler(this.MinimizeLabel_MouseEnter);
            this.MinimizeLabel.MouseLeave += new System.EventHandler(this.MinimizeLabel_MouseLeave);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.ClientSize = new System.Drawing.Size(1175, 783);
            this.Controls.Add(this.MinimizeLabel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ExitLabel);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.DemVotesPanel.ResumeLayout(false);
            this.DemVotesPanel.PerformLayout();
            this.RepVotesPanel.ResumeLayout(false);
            this.RepVotesPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label Title;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label ExitLabel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel mappanel;
        private System.Windows.Forms.Label TitleLabel;
        private System.Windows.Forms.Label CandidatesLabel;
        private System.Windows.Forms.Label SubtitleHistorical;
        private System.Windows.Forms.ListBox listYears;
        private System.Windows.Forms.ListBox listPublications;
        private System.Windows.Forms.Label RepVotesLabel;
        private System.Windows.Forms.Label DemVotesLabel;
        private System.Windows.Forms.Panel DemVotesPanel;
        private System.Windows.Forms.Panel RepVotesPanel;
        private System.Windows.Forms.Label MinimizeLabel;
        private System.Windows.Forms.Label RemarkLabel;
        private System.Windows.Forms.GroupBox KeyGroupBox;
    }
}

