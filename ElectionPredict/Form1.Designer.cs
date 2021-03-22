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
            this.ErrorLabel = new System.Windows.Forms.Label();
            this.listYearsCompare = new System.Windows.Forms.ListBox();
            this.listPublicationsCompare = new System.Windows.Forms.ListBox();
            this.CompareLabel = new System.Windows.Forms.Label();
            this.listYearsHistorical = new System.Windows.Forms.ListBox();
            this.listPublicationsHistorical = new System.Windows.Forms.ListBox();
            this.SubtitleHistorical = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.Title = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.KeyGroupBox = new System.Windows.Forms.GroupBox();
            this.RemarkLabel = new System.Windows.Forms.Label();
            this.DemVotesPanel = new System.Windows.Forms.Panel();
            this.DemVotesLabel = new System.Windows.Forms.Label();
            this.RepVotesPanel = new System.Windows.Forms.Panel();
            this.RepVotesLabel = new System.Windows.Forms.Label();
            this.CandidatesLabel = new System.Windows.Forms.Label();
            this.TitleLabel = new System.Windows.Forms.Label();
            this.mappanel = new System.Windows.Forms.Panel();
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
            this.panel1.Controls.Add(this.ErrorLabel);
            this.panel1.Controls.Add(this.listYearsCompare);
            this.panel1.Controls.Add(this.listPublicationsCompare);
            this.panel1.Controls.Add(this.CompareLabel);
            this.panel1.Controls.Add(this.listYearsHistorical);
            this.panel1.Controls.Add(this.listPublicationsHistorical);
            this.panel1.Controls.Add(this.SubtitleHistorical);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.Title);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(206, 710);
            this.panel1.TabIndex = 0;
            // 
            // ErrorLabel
            // 
            this.ErrorLabel.AutoSize = true;
            this.ErrorLabel.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ErrorLabel.ForeColor = System.Drawing.Color.Maroon;
            this.ErrorLabel.Location = new System.Drawing.Point(7, 633);
            this.ErrorLabel.Name = "ErrorLabel";
            this.ErrorLabel.Size = new System.Drawing.Size(0, 15);
            this.ErrorLabel.TabIndex = 9;
            // 
            // listYearsCompare
            // 
            this.listYearsCompare.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.listYearsCompare.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listYearsCompare.ForeColor = System.Drawing.Color.FloralWhite;
            this.listYearsCompare.FormattingEnabled = true;
            this.listYearsCompare.ItemHeight = 18;
            this.listYearsCompare.Items.AddRange(new object[] {
            " "});
            this.listYearsCompare.Location = new System.Drawing.Point(141, 276);
            this.listYearsCompare.Name = "listYearsCompare";
            this.listYearsCompare.Size = new System.Drawing.Size(59, 130);
            this.listYearsCompare.TabIndex = 8;
            this.listYearsCompare.Visible = false;
            // 
            // listPublicationsCompare
            // 
            this.listPublicationsCompare.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.listPublicationsCompare.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listPublicationsCompare.ForeColor = System.Drawing.Color.FloralWhite;
            this.listPublicationsCompare.FormattingEnabled = true;
            this.listPublicationsCompare.ItemHeight = 18;
            this.listPublicationsCompare.Location = new System.Drawing.Point(4, 276);
            this.listPublicationsCompare.Name = "listPublicationsCompare";
            this.listPublicationsCompare.Size = new System.Drawing.Size(131, 130);
            this.listPublicationsCompare.TabIndex = 7;
            this.listPublicationsCompare.Visible = false;
            this.listPublicationsCompare.SelectedIndexChanged += new System.EventHandler(this.ListPublications_SelectedIndexChanged);
            // 
            // CompareLabel
            // 
            this.CompareLabel.AutoSize = true;
            this.CompareLabel.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CompareLabel.ForeColor = System.Drawing.Color.FloralWhite;
            this.CompareLabel.Location = new System.Drawing.Point(3, 251);
            this.CompareLabel.Name = "CompareLabel";
            this.CompareLabel.Size = new System.Drawing.Size(100, 22);
            this.CompareLabel.TabIndex = 6;
            this.CompareLabel.Text = "Compare ▲";
            this.CompareLabel.Click += new System.EventHandler(this.CompareLabel_Click);
            // 
            // listYearsHistorical
            // 
            this.listYearsHistorical.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.listYearsHistorical.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listYearsHistorical.ForeColor = System.Drawing.Color.FloralWhite;
            this.listYearsHistorical.FormattingEnabled = true;
            this.listYearsHistorical.ItemHeight = 18;
            this.listYearsHistorical.Items.AddRange(new object[] {
            " "});
            this.listYearsHistorical.Location = new System.Drawing.Point(141, 107);
            this.listYearsHistorical.Name = "listYearsHistorical";
            this.listYearsHistorical.Size = new System.Drawing.Size(59, 130);
            this.listYearsHistorical.TabIndex = 5;
            // 
            // listPublicationsHistorical
            // 
            this.listPublicationsHistorical.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.listPublicationsHistorical.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listPublicationsHistorical.ForeColor = System.Drawing.Color.FloralWhite;
            this.listPublicationsHistorical.FormattingEnabled = true;
            this.listPublicationsHistorical.ItemHeight = 18;
            this.listPublicationsHistorical.Location = new System.Drawing.Point(4, 107);
            this.listPublicationsHistorical.Name = "listPublicationsHistorical";
            this.listPublicationsHistorical.Size = new System.Drawing.Size(131, 130);
            this.listPublicationsHistorical.TabIndex = 4;
            this.listPublicationsHistorical.SelectedIndexChanged += new System.EventHandler(this.ListPublications_SelectedIndexChanged);
            // 
            // SubtitleHistorical
            // 
            this.SubtitleHistorical.AutoSize = true;
            this.SubtitleHistorical.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.button1.Location = new System.Drawing.Point(4, 655);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(196, 50);
            this.button1.TabIndex = 1;
            this.button1.Text = "Show Results";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
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
            this.panel2.Controls.Add(this.RemarkLabel);
            this.panel2.Controls.Add(this.DemVotesPanel);
            this.panel2.Controls.Add(this.RepVotesPanel);
            this.panel2.Controls.Add(this.CandidatesLabel);
            this.panel2.Controls.Add(this.TitleLabel);
            this.panel2.Controls.Add(this.mappanel);
            this.panel2.Location = new System.Drawing.Point(181, 24);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(996, 699);
            this.panel2.TabIndex = 1;
            // 
            // KeyGroupBox
            // 
            this.KeyGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.KeyGroupBox.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.KeyGroupBox.ForeColor = System.Drawing.Color.FloralWhite;
            this.KeyGroupBox.Location = new System.Drawing.Point(924, 529);
            this.KeyGroupBox.Name = "KeyGroupBox";
            this.KeyGroupBox.Size = new System.Drawing.Size(230, 187);
            this.KeyGroupBox.TabIndex = 0;
            this.KeyGroupBox.TabStop = false;
            this.KeyGroupBox.Text = "Key:";
            this.KeyGroupBox.Visible = false;
            // 
            // RemarkLabel
            // 
            this.RemarkLabel.AutoSize = true;
            this.RemarkLabel.Font = new System.Drawing.Font("Consolas", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RemarkLabel.ForeColor = System.Drawing.Color.FloralWhite;
            this.RemarkLabel.Location = new System.Drawing.Point(45, 668);
            this.RemarkLabel.Name = "RemarkLabel";
            this.RemarkLabel.Size = new System.Drawing.Size(13, 13);
            this.RemarkLabel.TabIndex = 0;
            this.RemarkLabel.Text = " ";
            // 
            // DemVotesPanel
            // 
            this.DemVotesPanel.Controls.Add(this.DemVotesLabel);
            this.DemVotesPanel.Location = new System.Drawing.Point(836, 178);
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
            this.mappanel.Location = new System.Drawing.Point(45, 180);
            this.mappanel.Name = "mappanel";
            this.mappanel.Size = new System.Drawing.Size(742, 504);
            this.mappanel.TabIndex = 0;
            this.mappanel.Visible = false;
            // 
            // ExitLabel
            // 
            this.ExitLabel.AutoSize = true;
            this.ExitLabel.Font = new System.Drawing.Font("Consolas", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExitLabel.ForeColor = System.Drawing.Color.FloralWhite;
            this.ExitLabel.Location = new System.Drawing.Point(1152, -3);
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
            this.MinimizeLabel.Location = new System.Drawing.Point(1133, -8);
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
            this.ClientSize = new System.Drawing.Size(1175, 710);
            this.Controls.Add(this.KeyGroupBox);
            this.Controls.Add(this.MinimizeLabel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ExitLabel);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1175, 783);
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
        private System.Windows.Forms.ListBox listYearsHistorical;
        private System.Windows.Forms.ListBox listPublicationsHistorical;
        private System.Windows.Forms.Label RepVotesLabel;
        private System.Windows.Forms.Label DemVotesLabel;
        private System.Windows.Forms.Panel DemVotesPanel;
        private System.Windows.Forms.Panel RepVotesPanel;
        private System.Windows.Forms.Label MinimizeLabel;
        private System.Windows.Forms.Label RemarkLabel;
        private System.Windows.Forms.GroupBox KeyGroupBox;
        private System.Windows.Forms.Label CompareLabel;
        private System.Windows.Forms.ListBox listYearsCompare;
        private System.Windows.Forms.ListBox listPublicationsCompare;
        private System.Windows.Forms.Label ErrorLabel;
    }
}

