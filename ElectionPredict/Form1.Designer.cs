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
            this.DLabelCompare = new System.Windows.Forms.Label();
            this.RLabelCompare = new System.Windows.Forms.Label();
            this.ZeroLabelCompare = new System.Windows.Forms.Label();
            this.TrackBarTrackerCompare = new System.Windows.Forms.Label();
            this.ShiftResultPanelCompare = new System.Windows.Forms.Panel();
            this.ShiftResultsTrackBarCompare = new System.Windows.Forms.TrackBar();
            this.DLabel = new System.Windows.Forms.Label();
            this.RLabel = new System.Windows.Forms.Label();
            this.ZeroLabel = new System.Windows.Forms.Label();
            this.TrackBarTracker = new System.Windows.Forms.Label();
            this.ShiftResultPanel = new System.Windows.Forms.Panel();
            this.ShiftResultLabel = new System.Windows.Forms.Label();
            this.ShiftResultsTrackBar = new System.Windows.Forms.TrackBar();
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
            ((System.ComponentModel.ISupportInitialize)(this.ShiftResultsTrackBarCompare)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ShiftResultsTrackBar)).BeginInit();
            this.panel2.SuspendLayout();
            this.DemVotesPanel.SuspendLayout();
            this.RepVotesPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.panel1.Controls.Add(this.DLabelCompare);
            this.panel1.Controls.Add(this.RLabelCompare);
            this.panel1.Controls.Add(this.ZeroLabelCompare);
            this.panel1.Controls.Add(this.TrackBarTrackerCompare);
            this.panel1.Controls.Add(this.ShiftResultPanelCompare);
            this.panel1.Controls.Add(this.DLabel);
            this.panel1.Controls.Add(this.RLabel);
            this.panel1.Controls.Add(this.ZeroLabel);
            this.panel1.Controls.Add(this.TrackBarTracker);
            this.panel1.Controls.Add(this.ShiftResultPanel);
            this.panel1.Controls.Add(this.ShiftResultLabel);
            this.panel1.Controls.Add(this.ShiftResultsTrackBar);
            this.panel1.Controls.Add(this.ErrorLabel);
            this.panel1.Controls.Add(this.listYearsCompare);
            this.panel1.Controls.Add(this.listPublicationsCompare);
            this.panel1.Controls.Add(this.CompareLabel);
            this.panel1.Controls.Add(this.listYearsHistorical);
            this.panel1.Controls.Add(this.listPublicationsHistorical);
            this.panel1.Controls.Add(this.SubtitleHistorical);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.Title);
            this.panel1.Controls.Add(this.ShiftResultsTrackBarCompare);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(206, 710);
            this.panel1.TabIndex = 0;
            // 
            // DLabelCompare
            // 
            this.DLabelCompare.AutoSize = true;
            this.DLabelCompare.Font = new System.Drawing.Font("Consolas", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DLabelCompare.ForeColor = System.Drawing.Color.FloralWhite;
            this.DLabelCompare.Location = new System.Drawing.Point(-1, 376);
            this.DLabelCompare.Name = "DLabelCompare";
            this.DLabelCompare.Size = new System.Drawing.Size(49, 13);
            this.DLabelCompare.TabIndex = 21;
            this.DLabelCompare.Text = " +20% D";
            this.DLabelCompare.Visible = false;
            // 
            // RLabelCompare
            // 
            this.RLabelCompare.AutoSize = true;
            this.RLabelCompare.Font = new System.Drawing.Font("Consolas", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RLabelCompare.ForeColor = System.Drawing.Color.FloralWhite;
            this.RLabelCompare.Location = new System.Drawing.Point(156, 375);
            this.RLabelCompare.Name = "RLabelCompare";
            this.RLabelCompare.Size = new System.Drawing.Size(49, 13);
            this.RLabelCompare.TabIndex = 20;
            this.RLabelCompare.Text = " +20% R";
            this.RLabelCompare.Visible = false;
            // 
            // ZeroLabelCompare
            // 
            this.ZeroLabelCompare.AutoSize = true;
            this.ZeroLabelCompare.Font = new System.Drawing.Font("Consolas", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ZeroLabelCompare.ForeColor = System.Drawing.Color.FloralWhite;
            this.ZeroLabelCompare.Location = new System.Drawing.Point(90, 374);
            this.ZeroLabelCompare.Name = "ZeroLabelCompare";
            this.ZeroLabelCompare.Size = new System.Drawing.Size(19, 13);
            this.ZeroLabelCompare.TabIndex = 16;
            this.ZeroLabelCompare.Text = " 0";
            this.ZeroLabelCompare.Visible = false;
            // 
            // TrackBarTrackerCompare
            // 
            this.TrackBarTrackerCompare.AutoSize = true;
            this.TrackBarTrackerCompare.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TrackBarTrackerCompare.ForeColor = System.Drawing.Color.FloralWhite;
            this.TrackBarTrackerCompare.Location = new System.Drawing.Point(92, 346);
            this.TrackBarTrackerCompare.Name = "TrackBarTrackerCompare";
            this.TrackBarTrackerCompare.Size = new System.Drawing.Size(20, 22);
            this.TrackBarTrackerCompare.TabIndex = 18;
            this.TrackBarTrackerCompare.Text = "▼";
            this.TrackBarTrackerCompare.Visible = false;
            this.TrackBarTrackerCompare.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TrackBarTracker_MouseDown);
            this.TrackBarTrackerCompare.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TrackBarTracker_MouseMove);
            this.TrackBarTrackerCompare.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TrackBarTracker_MouseUp);
            // 
            // ShiftResultPanelCompare
            // 
            this.ShiftResultPanelCompare.Location = new System.Drawing.Point(3, 346);
            this.ShiftResultPanelCompare.Name = "ShiftResultPanelCompare";
            this.ShiftResultPanelCompare.Size = new System.Drawing.Size(196, 22);
            this.ShiftResultPanelCompare.TabIndex = 19;
            this.ShiftResultPanelCompare.Visible = false;
            // 
            // ShiftResultsTrackBarCompare
            // 
            this.ShiftResultsTrackBarCompare.Location = new System.Drawing.Point(4, 346);
            this.ShiftResultsTrackBarCompare.Maximum = 20;
            this.ShiftResultsTrackBarCompare.Minimum = -20;
            this.ShiftResultsTrackBarCompare.Name = "ShiftResultsTrackBarCompare";
            this.ShiftResultsTrackBarCompare.Size = new System.Drawing.Size(196, 45);
            this.ShiftResultsTrackBarCompare.TabIndex = 17;
            this.ShiftResultsTrackBarCompare.Visible = false;
            // 
            // DLabel
            // 
            this.DLabel.AutoSize = true;
            this.DLabel.Font = new System.Drawing.Font("Consolas", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DLabel.ForeColor = System.Drawing.Color.FloralWhite;
            this.DLabel.Location = new System.Drawing.Point(-1, 335);
            this.DLabel.Name = "DLabel";
            this.DLabel.Size = new System.Drawing.Size(49, 13);
            this.DLabel.TabIndex = 15;
            this.DLabel.Text = " +20% D";
            this.DLabel.Visible = false;
            // 
            // RLabel
            // 
            this.RLabel.AutoSize = true;
            this.RLabel.Font = new System.Drawing.Font("Consolas", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RLabel.ForeColor = System.Drawing.Color.FloralWhite;
            this.RLabel.Location = new System.Drawing.Point(156, 334);
            this.RLabel.Name = "RLabel";
            this.RLabel.Size = new System.Drawing.Size(49, 13);
            this.RLabel.TabIndex = 14;
            this.RLabel.Text = " +20% R";
            this.RLabel.Visible = false;
            // 
            // ZeroLabel
            // 
            this.ZeroLabel.AutoSize = true;
            this.ZeroLabel.Font = new System.Drawing.Font("Consolas", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ZeroLabel.ForeColor = System.Drawing.Color.FloralWhite;
            this.ZeroLabel.Location = new System.Drawing.Point(90, 333);
            this.ZeroLabel.Name = "ZeroLabel";
            this.ZeroLabel.Size = new System.Drawing.Size(19, 13);
            this.ZeroLabel.TabIndex = 10;
            this.ZeroLabel.Text = " 0";
            this.ZeroLabel.Visible = false;
            // 
            // TrackBarTracker
            // 
            this.TrackBarTracker.AutoSize = true;
            this.TrackBarTracker.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TrackBarTracker.ForeColor = System.Drawing.Color.FloralWhite;
            this.TrackBarTracker.Location = new System.Drawing.Point(92, 305);
            this.TrackBarTracker.Name = "TrackBarTracker";
            this.TrackBarTracker.Size = new System.Drawing.Size(20, 22);
            this.TrackBarTracker.TabIndex = 12;
            this.TrackBarTracker.Text = "▼";
            this.TrackBarTracker.Visible = false;
            this.TrackBarTracker.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TrackBarTracker_MouseDown);
            this.TrackBarTracker.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TrackBarTracker_MouseMove);
            this.TrackBarTracker.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TrackBarTracker_MouseUp);
            // 
            // ShiftResultPanel
            // 
            this.ShiftResultPanel.Location = new System.Drawing.Point(3, 305);
            this.ShiftResultPanel.Name = "ShiftResultPanel";
            this.ShiftResultPanel.Size = new System.Drawing.Size(196, 22);
            this.ShiftResultPanel.TabIndex = 13;
            this.ShiftResultPanel.Visible = false;
            // 
            // ShiftResultLabel
            // 
            this.ShiftResultLabel.AutoSize = true;
            this.ShiftResultLabel.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ShiftResultLabel.ForeColor = System.Drawing.Color.FloralWhite;
            this.ShiftResultLabel.Location = new System.Drawing.Point(3, 280);
            this.ShiftResultLabel.Name = "ShiftResultLabel";
            this.ShiftResultLabel.Size = new System.Drawing.Size(160, 22);
            this.ShiftResultLabel.TabIndex = 11;
            this.ShiftResultLabel.Text = "Shift Results ▲";
            this.ShiftResultLabel.Click += new System.EventHandler(this.ShiftResultLabel_Click);
            // 
            // ShiftResultsTrackBar
            // 
            this.ShiftResultsTrackBar.Location = new System.Drawing.Point(4, 305);
            this.ShiftResultsTrackBar.Maximum = 20;
            this.ShiftResultsTrackBar.Minimum = -20;
            this.ShiftResultsTrackBar.Name = "ShiftResultsTrackBar";
            this.ShiftResultsTrackBar.Size = new System.Drawing.Size(196, 45);
            this.ShiftResultsTrackBar.TabIndex = 10;
            this.ShiftResultsTrackBar.Visible = false;
            // 
            // ErrorLabel
            // 
            this.ErrorLabel.AutoSize = true;
            this.ErrorLabel.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ErrorLabel.ForeColor = System.Drawing.Color.Maroon;
            this.ErrorLabel.Location = new System.Drawing.Point(12, 555);
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
            this.CandidatesLabel.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CandidatesLabel.ForeColor = System.Drawing.Color.FloralWhite;
            this.CandidatesLabel.Location = new System.Drawing.Point(43, 99);
            this.CandidatesLabel.Name = "CandidatesLabel";
            this.CandidatesLabel.Size = new System.Drawing.Size(22, 24);
            this.CandidatesLabel.TabIndex = 5;
            this.CandidatesLabel.Text = " ";
            // 
            // TitleLabel
            // 
            this.TitleLabel.AutoSize = true;
            this.TitleLabel.Font = new System.Drawing.Font("Consolas", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TitleLabel.ForeColor = System.Drawing.Color.FloralWhite;
            this.TitleLabel.Location = new System.Drawing.Point(42, 57);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(30, 32);
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
            this.Controls.Add(this.ExitLabel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1175, 783);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ShiftResultsTrackBarCompare)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ShiftResultsTrackBar)).EndInit();
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
        private System.Windows.Forms.TrackBar ShiftResultsTrackBar;
        private System.Windows.Forms.Label ShiftResultLabel;
        private System.Windows.Forms.Label TrackBarTracker;
        private System.Windows.Forms.Panel ShiftResultPanel;
        private System.Windows.Forms.Label DLabel;
        private System.Windows.Forms.Label RLabel;
        private System.Windows.Forms.Label ZeroLabel;
        private System.Windows.Forms.TrackBar ShiftResultsTrackBarCompare;
        private System.Windows.Forms.Label DLabelCompare;
        private System.Windows.Forms.Label RLabelCompare;
        private System.Windows.Forms.Label ZeroLabelCompare;
        private System.Windows.Forms.Label TrackBarTrackerCompare;
        private System.Windows.Forms.Panel ShiftResultPanelCompare;
    }
}

