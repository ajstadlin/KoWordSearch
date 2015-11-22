/*
 * Created by SharpDevelop.
 * User: AJ
 * Date: 11/2/2015
 * Time: 7:15 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace KoWordSearch
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem FileMenu;
		private System.Windows.Forms.ToolStripMenuItem QuitMnu;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton RunTBtn;
		private System.Windows.Forms.TabControl MainTabs;
		private System.Windows.Forms.TabPage MatrixTab;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.DataGridView MatrixDgv;
		private System.Windows.Forms.TabPage VocabTab;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.DataGridView VocabDgv;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
		private System.Windows.Forms.NumericUpDown RowsNud;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown ColumnsNud;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ToolStripMenuItem SaveMnu;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
		private System.Windows.Forms.ToolStripButton PreviewTBtn;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		
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
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.FileMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.SaveMnu = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.QuitMnu = new System.Windows.Forms.ToolStripMenuItem();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.RunTBtn = new System.Windows.Forms.ToolStripButton();
			this.MainTabs = new System.Windows.Forms.TabControl();
			this.MatrixTab = new System.Windows.Forms.TabPage();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.MatrixDgv = new System.Windows.Forms.DataGridView();
			this.VocabTab = new System.Windows.Forms.TabPage();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.VocabDgv = new System.Windows.Forms.DataGridView();
			this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.RowsNud = new System.Windows.Forms.NumericUpDown();
			this.label2 = new System.Windows.Forms.Label();
			this.ColumnsNud = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.PreviewTBtn = new System.Windows.Forms.ToolStripButton();
			this.menuStrip1.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.panel1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.MainTabs.SuspendLayout();
			this.MatrixTab.SuspendLayout();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.MatrixDgv)).BeginInit();
			this.VocabTab.SuspendLayout();
			this.groupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.VocabDgv)).BeginInit();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.RowsNud)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ColumnsNud)).BeginInit();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.FileMenu});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
			this.menuStrip1.Size = new System.Drawing.Size(953, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// FileMenu
			// 
			this.FileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.SaveMnu,
			this.toolStripSeparator1,
			this.QuitMnu});
			this.FileMenu.Name = "FileMenu";
			this.FileMenu.Size = new System.Drawing.Size(37, 20);
			this.FileMenu.Text = "&File";
			// 
			// SaveMnu
			// 
			this.SaveMnu.Name = "SaveMnu";
			this.SaveMnu.Size = new System.Drawing.Size(98, 22);
			this.SaveMnu.Text = "&Save";
			this.SaveMnu.Click += new System.EventHandler(this.SaveMnuClick);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(95, 6);
			// 
			// QuitMnu
			// 
			this.QuitMnu.Name = "QuitMnu";
			this.QuitMnu.Size = new System.Drawing.Size(98, 22);
			this.QuitMnu.Text = "&Quit";
			this.QuitMnu.Click += new System.EventHandler(this.QuitMnuClick);
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.toolStripStatusLabel1});
			this.statusStrip1.Location = new System.Drawing.Point(0, 606);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
			this.statusStrip1.Size = new System.Drawing.Size(953, 22);
			this.statusStrip1.TabIndex = 1;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// toolStripStatusLabel1
			// 
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new System.Drawing.Size(936, 17);
			this.toolStripStatusLabel1.Spring = true;
			this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.toolStrip1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 24);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(953, 26);
			this.panel1.TabIndex = 2;
			// 
			// toolStrip1
			// 
			this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.PreviewTBtn,
			this.toolStripSeparator2,
			this.RunTBtn});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(953, 25);
			this.toolStrip1.TabIndex = 0;
			this.toolStrip1.Text = "toolStrip1";
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
			this.MainTabs.Location = new System.Drawing.Point(0, 50);
			this.MainTabs.Name = "MainTabs";
			this.MainTabs.SelectedIndex = 0;
			this.MainTabs.Size = new System.Drawing.Size(953, 556);
			this.MainTabs.TabIndex = 3;
			// 
			// MatrixTab
			// 
			this.MatrixTab.Controls.Add(this.groupBox2);
			this.MatrixTab.Location = new System.Drawing.Point(4, 23);
			this.MatrixTab.Name = "MatrixTab";
			this.MatrixTab.Padding = new System.Windows.Forms.Padding(3);
			this.MatrixTab.Size = new System.Drawing.Size(945, 529);
			this.MatrixTab.TabIndex = 0;
			this.MatrixTab.Text = "Matrix";
			this.MatrixTab.UseVisualStyleBackColor = true;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.MatrixDgv);
			this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox2.Font = new System.Drawing.Font("Batang", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.groupBox2.Location = new System.Drawing.Point(3, 3);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(939, 523);
			this.groupBox2.TabIndex = 8;
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
			this.MatrixDgv.Location = new System.Drawing.Point(3, 28);
			this.MatrixDgv.Name = "MatrixDgv";
			this.MatrixDgv.RowHeadersVisible = false;
			this.MatrixDgv.RowTemplate.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(8);
			this.MatrixDgv.Size = new System.Drawing.Size(933, 492);
			this.MatrixDgv.TabIndex = 0;
			// 
			// VocabTab
			// 
			this.VocabTab.Controls.Add(this.groupBox3);
			this.VocabTab.Controls.Add(this.groupBox1);
			this.VocabTab.Location = new System.Drawing.Point(4, 23);
			this.VocabTab.Name = "VocabTab";
			this.VocabTab.Padding = new System.Windows.Forms.Padding(3);
			this.VocabTab.Size = new System.Drawing.Size(945, 529);
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
			this.groupBox3.Size = new System.Drawing.Size(939, 442);
			this.groupBox3.TabIndex = 9;
			this.groupBox3.TabStop = false;
			// 
			// VocabDgv
			// 
			this.VocabDgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
			this.VocabDgv.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
			this.VocabDgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.VocabDgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
			this.Column1,
			this.Column2,
			this.Column3,
			this.Column4,
			this.Column5});
			this.VocabDgv.Dock = System.Windows.Forms.DockStyle.Fill;
			this.VocabDgv.Location = new System.Drawing.Point(3, 17);
			this.VocabDgv.Name = "VocabDgv";
			this.VocabDgv.RowHeadersWidth = 24;
			this.VocabDgv.Size = new System.Drawing.Size(933, 422);
			this.VocabDgv.TabIndex = 0;
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
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
			this.Column3.DefaultCellStyle = dataGridViewCellStyle1;
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
			this.groupBox1.Controls.Add(this.RowsNud);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.ColumnsNud);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
			this.groupBox1.Location = new System.Drawing.Point(3, 3);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(939, 81);
			this.groupBox1.TabIndex = 8;
			this.groupBox1.TabStop = false;
			// 
			// RowsNud
			// 
			this.RowsNud.Location = new System.Drawing.Point(92, 48);
			this.RowsNud.Name = "RowsNud";
			this.RowsNud.Size = new System.Drawing.Size(57, 21);
			this.RowsNud.TabIndex = 4;
			this.RowsNud.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.RowsNud.Value = new decimal(new int[] {
			16,
			0,
			0,
			0});
			this.RowsNud.ValueChanged += new System.EventHandler(this.RowsNudValueChanged);
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
			// ColumnsNud
			// 
			this.ColumnsNud.Location = new System.Drawing.Point(92, 20);
			this.ColumnsNud.Name = "ColumnsNud";
			this.ColumnsNud.Size = new System.Drawing.Size(57, 21);
			this.ColumnsNud.TabIndex = 2;
			this.ColumnsNud.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.ColumnsNud.Value = new decimal(new int[] {
			16,
			0,
			0,
			0});
			this.ColumnsNud.ValueChanged += new System.EventHandler(this.ColumnsNudValueChanged);
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
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
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
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(953, 628);
			this.Controls.Add(this.MainTabs);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.menuStrip1);
			this.Font = new System.Drawing.Font("Source Code Pro", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "MainForm";
			this.Text = "KoWordSearch";
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
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
			this.VocabTab.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.VocabDgv)).EndInit();
			this.groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.RowsNud)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ColumnsNud)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
	}
}
