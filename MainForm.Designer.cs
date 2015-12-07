namespace KoWordSearch
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

        #region Windows Form Designer generated code

        /// <summary>
        /// This method is required for Windows Forms designer support.
        /// Do not change the method contents inside the source code editor. The Forms designer might
        /// not be able to load this method if it was changed manually.
        /// </summary>
        private void InitializeComponent()
		{
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.StatusLb = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.PreviewTBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.RunTBtn = new System.Windows.Forms.ToolStripButton();
            this.MainTabs = new System.Windows.Forms.TabControl();
            this.MatrixTab = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.MatrixDgv = new System.Windows.Forms.DataGridView();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel2 = new System.Windows.Forms.Panel();
            this.VocabListBox = new System.Windows.Forms.CheckedListBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.HangulRadio = new System.Windows.Forms.RadioButton();
            this.EnglishRadio = new System.Windows.Forms.RadioButton();
            this.VocabTab = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.VocabDgv = new System.Windows.Forms.DataGridView();
            this.Column6 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.MatrixRowsNud = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.MatrixColsNud = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.SaveTBtn = new System.Windows.Forms.ToolStripButton();
            this.OpenTBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.MainTabs.SuspendLayout();
            this.MatrixTab.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MatrixDgv)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.VocabTab.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VocabDgv)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MatrixRowsNud)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MatrixColsNud)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLb});
            this.statusStrip1.Location = new System.Drawing.Point(0, 606);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1075, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // StatusLb
            // 
            this.StatusLb.Name = "StatusLb";
            this.StatusLb.Size = new System.Drawing.Size(1058, 17);
            this.StatusLb.Spring = true;
            this.StatusLb.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1075, 26);
            this.panel1.TabIndex = 2;
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PreviewTBtn,
            this.toolStripSeparator2,
            this.RunTBtn,
            this.toolStripSeparator3,
            this.OpenTBtn,
            this.toolStripSeparator4,
            this.SaveTBtn});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1075, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // PreviewTBtn
            // 
            this.PreviewTBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.PreviewTBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.PreviewTBtn.Name = "PreviewTBtn";
            this.PreviewTBtn.Size = new System.Drawing.Size(52, 22);
            this.PreviewTBtn.Text = "Pre&view";
            this.PreviewTBtn.Click += new System.EventHandler(this.PreviewTBtnClick);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // RunTBtn
            // 
            this.RunTBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.RunTBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RunTBtn.Name = "RunTBtn";
            this.RunTBtn.Size = new System.Drawing.Size(32, 22);
            this.RunTBtn.Text = "&Run";
            this.RunTBtn.Click += new System.EventHandler(this.RunTBtnClick);
            // 
            // MainTabs
            // 
            this.MainTabs.Controls.Add(this.MatrixTab);
            this.MainTabs.Controls.Add(this.VocabTab);
            this.MainTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainTabs.Location = new System.Drawing.Point(0, 26);
            this.MainTabs.Name = "MainTabs";
            this.MainTabs.SelectedIndex = 0;
            this.MainTabs.Size = new System.Drawing.Size(1075, 580);
            this.MainTabs.TabIndex = 3;
            // 
            // MatrixTab
            // 
            this.MatrixTab.Controls.Add(this.groupBox2);
            this.MatrixTab.Controls.Add(this.splitter1);
            this.MatrixTab.Controls.Add(this.panel2);
            this.MatrixTab.Location = new System.Drawing.Point(4, 23);
            this.MatrixTab.Name = "MatrixTab";
            this.MatrixTab.Padding = new System.Windows.Forms.Padding(3);
            this.MatrixTab.Size = new System.Drawing.Size(1067, 529);
            this.MatrixTab.TabIndex = 0;
            this.MatrixTab.Text = "Matrix";
            this.MatrixTab.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.MatrixDgv);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(904, 523);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            // 
            // MatrixDgv
            // 
            this.MatrixDgv.AllowUserToAddRows = false;
            this.MatrixDgv.AllowUserToDeleteRows = false;
            this.MatrixDgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.MatrixDgv.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.MatrixDgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.MatrixDgv.ColumnHeadersVisible = false;
            this.MatrixDgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MatrixDgv.Location = new System.Drawing.Point(3, 17);
            this.MatrixDgv.Name = "MatrixDgv";
            this.MatrixDgv.RowHeadersVisible = false;
            this.MatrixDgv.RowTemplate.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(8);
            this.MatrixDgv.Size = new System.Drawing.Size(898, 503);
            this.MatrixDgv.TabIndex = 1;
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter1.Location = new System.Drawing.Point(907, 3);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(4, 523);
            this.splitter1.TabIndex = 3;
            this.splitter1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.VocabListBox);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(911, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(153, 523);
            this.panel2.TabIndex = 2;
            // 
            // VocabListBox
            // 
            this.VocabListBox.CheckOnClick = true;
            this.VocabListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.VocabListBox.FormattingEnabled = true;
            this.VocabListBox.Location = new System.Drawing.Point(0, 28);
            this.VocabListBox.Name = "VocabListBox";
            this.VocabListBox.Size = new System.Drawing.Size(153, 495);
            this.VocabListBox.TabIndex = 14;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.HangulRadio);
            this.panel3.Controls.Add(this.EnglishRadio);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(153, 28);
            this.panel3.TabIndex = 13;
            // 
            // HangulRadio
            // 
            this.HangulRadio.AutoSize = true;
            this.HangulRadio.Location = new System.Drawing.Point(86, 4);
            this.HangulRadio.Name = "HangulRadio";
            this.HangulRadio.Size = new System.Drawing.Size(58, 18);
            this.HangulRadio.TabIndex = 1;
            this.HangulRadio.Text = "한구어";
            this.HangulRadio.UseVisualStyleBackColor = true;
            this.HangulRadio.CheckedChanged += new System.EventHandler(this.LanguageRadio_CheckedChanged);
            // 
            // EnglishRadio
            // 
            this.EnglishRadio.AutoSize = true;
            this.EnglishRadio.Checked = true;
            this.EnglishRadio.Location = new System.Drawing.Point(6, 4);
            this.EnglishRadio.Name = "EnglishRadio";
            this.EnglishRadio.Size = new System.Drawing.Size(74, 18);
            this.EnglishRadio.TabIndex = 0;
            this.EnglishRadio.TabStop = true;
            this.EnglishRadio.Text = "&English";
            this.EnglishRadio.UseVisualStyleBackColor = true;
            this.EnglishRadio.CheckedChanged += new System.EventHandler(this.LanguageRadio_CheckedChanged);
            // 
            // VocabTab
            // 
            this.VocabTab.Controls.Add(this.groupBox3);
            this.VocabTab.Controls.Add(this.groupBox1);
            this.VocabTab.Location = new System.Drawing.Point(4, 23);
            this.VocabTab.Name = "VocabTab";
            this.VocabTab.Padding = new System.Windows.Forms.Padding(3);
            this.VocabTab.Size = new System.Drawing.Size(1067, 553);
            this.VocabTab.TabIndex = 1;
            this.VocabTab.Text = "Vocabulary";
            this.VocabTab.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.VocabDgv);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(3, 84);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1061, 466);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            // 
            // VocabDgv
            // 
            this.VocabDgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.VocabDgv.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.VocabDgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.VocabDgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column6,
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5});
            this.VocabDgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.VocabDgv.Location = new System.Drawing.Point(3, 17);
            this.VocabDgv.Name = "VocabDgv";
            this.VocabDgv.RowHeadersWidth = 24;
            this.VocabDgv.Size = new System.Drawing.Size(1055, 446);
            this.VocabDgv.TabIndex = 0;
            // 
            // Column6
            // 
            this.Column6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column6.DataPropertyName = "IsPicked";
            this.Column6.FalseValue = "false";
            this.Column6.HeaderText = "";
            this.Column6.Name = "Column6";
            this.Column6.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column6.TrueValue = "true";
            this.Column6.Width = 40;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "KoWord";
            this.Column1.HeaderText = "Korean";
            this.Column1.MinimumWidth = 200;
            this.Column1.Name = "Column1";
            this.Column1.Width = 200;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "EnWord";
            this.Column2.HeaderText = "English";
            this.Column2.MinimumWidth = 200;
            this.Column2.Name = "Column2";
            this.Column2.Width = 200;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column3.DataPropertyName = "Orient";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Column3.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column3.HeaderText = "Horiz";
            this.Column3.Name = "Column3";
            this.Column3.Width = 67;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "X";
            this.Column4.HeaderText = "Col";
            this.Column4.Name = "Column4";
            this.Column4.Width = 53;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "Y";
            this.Column5.HeaderText = "Row";
            this.Column5.Name = "Column5";
            this.Column5.Width = 53;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.MatrixRowsNud);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.MatrixColsNud);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1061, 81);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            // 
            // MatrixRowsNud
            // 
            this.MatrixRowsNud.Location = new System.Drawing.Point(92, 48);
            this.MatrixRowsNud.Name = "MatrixRowsNud";
            this.MatrixRowsNud.Size = new System.Drawing.Size(57, 21);
            this.MatrixRowsNud.TabIndex = 4;
            this.MatrixRowsNud.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.MatrixRowsNud.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.MatrixRowsNud.ValueChanged += new System.EventHandler(this.RowsNudValueChanged);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(13, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Rows";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // MatrixColsNud
            // 
            this.MatrixColsNud.Location = new System.Drawing.Point(92, 20);
            this.MatrixColsNud.Name = "MatrixColsNud";
            this.MatrixColsNud.Size = new System.Drawing.Size(57, 21);
            this.MatrixColsNud.TabIndex = 2;
            this.MatrixColsNud.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.MatrixColsNud.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.MatrixColsNud.ValueChanged += new System.EventHandler(this.ColumnsNudValueChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(13, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Columns";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // SaveTBtn
            // 
            this.SaveTBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.SaveTBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SaveTBtn.Name = "SaveTBtn";
            this.SaveTBtn.Size = new System.Drawing.Size(35, 22);
            this.SaveTBtn.Text = "&Save";
            this.SaveTBtn.Click += new System.EventHandler(this.SaveMnuClick);
            // 
            // OpenTBtn
            // 
            this.OpenTBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.OpenTBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.OpenTBtn.Name = "OpenTBtn";
            this.OpenTBtn.Size = new System.Drawing.Size(40, 22);
            this.OpenTBtn.Text = "&Open";
            this.OpenTBtn.Click += new System.EventHandler(this.LoadVocabFileMnuClick);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1075, 628);
            this.Controls.Add(this.MainTabs);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Font = new System.Drawing.Font("Source Code Pro", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "MainForm";
            this.Text = "KoWordSearch";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.MainTabs.ResumeLayout(false);
            this.MatrixTab.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MatrixDgv)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.VocabTab.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.VocabDgv)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MatrixRowsNud)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MatrixColsNud)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel StatusLb;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton RunTBtn;
        private System.Windows.Forms.TabControl MainTabs;
        private System.Windows.Forms.TabPage MatrixTab;
        private System.Windows.Forms.TabPage VocabTab;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView VocabDgv;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown MatrixRowsNud;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown MatrixColsNud;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripButton PreviewTBtn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton HangulRadio;
        private System.Windows.Forms.RadioButton EnglishRadio;
        private System.Windows.Forms.CheckedListBox VocabListBox;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView MatrixDgv;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton OpenTBtn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton SaveTBtn;
    }
}
