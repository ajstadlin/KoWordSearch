namespace KoWordSearch
{
    partial class PrintPreviewDLG
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
            this.panPreviewToolBar = new System.Windows.Forms.Panel();
            this.PrinterTBar = new System.Windows.Forms.ToolStrip();
            this.PrinterSelectLb = new System.Windows.Forms.ToolStripLabel();
            this.PrinterTDdl = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.PrintTBar = new System.Windows.Forms.ToolStrip();
            this.PrintTBtn = new System.Windows.Forms.ToolStripButton();
            this.PageSetupDlgTBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ZoomWidthRadio = new System.Windows.Forms.RadioButton();
            this.ZoomFitPageRadio = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.ZoomNud = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.PrintPreviewCtrl = new System.Windows.Forms.PrintPreviewControl();
            this.panPreviewToolBar.SuspendLayout();
            this.PrinterTBar.SuspendLayout();
            this.PrintTBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ZoomNud)).BeginInit();
            this.SuspendLayout();
            // 
            // panPreviewToolBar
            // 
            this.panPreviewToolBar.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panPreviewToolBar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panPreviewToolBar.Controls.Add(this.PrinterTBar);
            this.panPreviewToolBar.Controls.Add(this.PrintTBar);
            this.panPreviewToolBar.Controls.Add(this.ZoomWidthRadio);
            this.panPreviewToolBar.Controls.Add(this.ZoomFitPageRadio);
            this.panPreviewToolBar.Controls.Add(this.label6);
            this.panPreviewToolBar.Controls.Add(this.ZoomNud);
            this.panPreviewToolBar.Controls.Add(this.label4);
            this.panPreviewToolBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panPreviewToolBar.Location = new System.Drawing.Point(0, 0);
            this.panPreviewToolBar.Name = "panPreviewToolBar";
            this.panPreviewToolBar.Size = new System.Drawing.Size(980, 26);
            this.panPreviewToolBar.TabIndex = 4;
            // 
            // PrinterTBar
            // 
            this.PrinterTBar.AutoSize = false;
            this.PrinterTBar.BackColor = System.Drawing.Color.Transparent;
            this.PrinterTBar.CanOverflow = false;
            this.PrinterTBar.Dock = System.Windows.Forms.DockStyle.None;
            this.PrinterTBar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.PrinterTBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PrinterSelectLb,
            this.PrinterTDdl,
            this.toolStripSeparator2});
            this.PrinterTBar.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.PrinterTBar.Location = new System.Drawing.Point(114, 1);
            this.PrinterTBar.Name = "PrinterTBar";
            this.PrinterTBar.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.PrinterTBar.Size = new System.Drawing.Size(286, 25);
            this.PrinterTBar.TabIndex = 25;
            // 
            // PrinterSelectLb
            // 
            this.PrinterSelectLb.BackColor = System.Drawing.Color.Transparent;
            this.PrinterSelectLb.Margin = new System.Windows.Forms.Padding(4, 3, 4, 2);
            this.PrinterSelectLb.Name = "PrinterSelectLb";
            this.PrinterSelectLb.Size = new System.Drawing.Size(42, 15);
            this.PrinterSelectLb.Text = "&Printer";
            this.PrinterSelectLb.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.PrinterSelectLb.Click += new System.EventHandler(this.PrinterSelectLb_Click);
            // 
            // PrinterTDdl
            // 
            this.PrinterTDdl.AutoSize = false;
            this.PrinterTDdl.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PrinterTDdl.Name = "PrinterTDdl";
            this.PrinterTDdl.Size = new System.Drawing.Size(220, 21);
            this.PrinterTDdl.DropDown += new System.EventHandler(this.PrinterTDdl_DropDown);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Margin = new System.Windows.Forms.Padding(2, 0, 0, 0);
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 23);
            // 
            // PrintTBar
            // 
            this.PrintTBar.AutoSize = false;
            this.PrintTBar.BackColor = System.Drawing.Color.Transparent;
            this.PrintTBar.CanOverflow = false;
            this.PrintTBar.Dock = System.Windows.Forms.DockStyle.None;
            this.PrintTBar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.PrintTBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PrintTBtn,
            this.PageSetupDlgTBtn,
            this.toolStripSeparator1});
            this.PrintTBar.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.PrintTBar.Location = new System.Drawing.Point(0, 1);
            this.PrintTBar.Name = "PrintTBar";
            this.PrintTBar.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.PrintTBar.Size = new System.Drawing.Size(56, 25);
            this.PrintTBar.TabIndex = 23;
            // 
            // PrintTBtn
            // 
            this.PrintTBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.PrintTBtn.Image = global::KoWordSearch.Properties.Resources.glyphicons_16_print;
            this.PrintTBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.PrintTBtn.Name = "PrintTBtn";
            this.PrintTBtn.Size = new System.Drawing.Size(23, 20);
            this.PrintTBtn.ToolTipText = "Print";
            this.PrintTBtn.Click += new System.EventHandler(this.PrintTBtn_Click);
            // 
            // PageSetupDlgTBtn
            // 
            this.PageSetupDlgTBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.PageSetupDlgTBtn.Image = global::KoWordSearch.Properties.Resources.glyphicons_281_settings;
            this.PageSetupDlgTBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.PageSetupDlgTBtn.Name = "PageSetupDlgTBtn";
            this.PageSetupDlgTBtn.Size = new System.Drawing.Size(23, 20);
            this.PageSetupDlgTBtn.ToolTipText = "Page Setup...";
            this.PageSetupDlgTBtn.Click += new System.EventHandler(this.PageSetupTBtn_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 23);
            // 
            // ZoomWidthRadio
            // 
            this.ZoomWidthRadio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ZoomWidthRadio.Checked = true;
            this.ZoomWidthRadio.Location = new System.Drawing.Point(799, 3);
            this.ZoomWidthRadio.Name = "ZoomWidthRadio";
            this.ZoomWidthRadio.Size = new System.Drawing.Size(97, 16);
            this.ZoomWidthRadio.TabIndex = 11;
            this.ZoomWidthRadio.TabStop = true;
            this.ZoomWidthRadio.Text = "Fit Width";
            this.ZoomWidthRadio.UseVisualStyleBackColor = true;
            this.ZoomWidthRadio.Click += new System.EventHandler(this.ZoomRadio_CheckedChanged);
            // 
            // ZoomFitPageRadio
            // 
            this.ZoomFitPageRadio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ZoomFitPageRadio.Location = new System.Drawing.Point(902, 3);
            this.ZoomFitPageRadio.Name = "ZoomFitPageRadio";
            this.ZoomFitPageRadio.Size = new System.Drawing.Size(70, 16);
            this.ZoomFitPageRadio.TabIndex = 10;
            this.ZoomFitPageRadio.Text = "Fit Page";
            this.ZoomFitPageRadio.UseVisualStyleBackColor = true;
            this.ZoomFitPageRadio.Click += new System.EventHandler(this.ZoomRadio_CheckedChanged);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.Location = new System.Drawing.Point(763, 4);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(14, 14);
            this.label6.TabIndex = 9;
            this.label6.Text = "x";
            // 
            // ZoomNud
            // 
            this.ZoomNud.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ZoomNud.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ZoomNud.DecimalPlaces = 2;
            this.ZoomNud.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.ZoomNud.Location = new System.Drawing.Point(695, 2);
            this.ZoomNud.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            65536});
            this.ZoomNud.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.ZoomNud.Name = "ZoomNud";
            this.ZoomNud.Size = new System.Drawing.Size(66, 20);
            this.ZoomNud.TabIndex = 8;
            this.ZoomNud.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ZoomNud.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.ZoomNud.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ZoomNud.ValueChanged += new System.EventHandler(this.ZoomNud_ValueChanged);
            this.ZoomNud.Click += new System.EventHandler(this.ZoomNud_ValueChanged);
            this.ZoomNud.Enter += new System.EventHandler(this.ZoomNud_Enter);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.Location = new System.Drawing.Point(640, 5);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 14);
            this.label4.TabIndex = 7;
            this.label4.Text = "Zoom";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // PrintPreviewCtrl
            // 
            this.PrintPreviewCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PrintPreviewCtrl.Location = new System.Drawing.Point(0, 26);
            this.PrintPreviewCtrl.Name = "PrintPreviewCtrl";
            this.PrintPreviewCtrl.Size = new System.Drawing.Size(980, 512);
            this.PrintPreviewCtrl.TabIndex = 5;
            // 
            // PrintPreviewDLG
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(980, 538);
            this.Controls.Add(this.PrintPreviewCtrl);
            this.Controls.Add(this.panPreviewToolBar);
            this.Name = "PrintPreviewDLG";
            this.Text = "PrintPreviewDLG";
            this.panPreviewToolBar.ResumeLayout(false);
            this.PrinterTBar.ResumeLayout(false);
            this.PrinterTBar.PerformLayout();
            this.PrintTBar.ResumeLayout(false);
            this.PrintTBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ZoomNud)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panPreviewToolBar;
        private System.Windows.Forms.ToolStrip PrinterTBar;
        private System.Windows.Forms.ToolStripLabel PrinterSelectLb;
        private System.Windows.Forms.ToolStripComboBox PrinterTDdl;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStrip PrintTBar;
        public System.Windows.Forms.ToolStripButton PrintTBtn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.RadioButton ZoomWidthRadio;
        private System.Windows.Forms.RadioButton ZoomFitPageRadio;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown ZoomNud;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.PrintPreviewControl PrintPreviewCtrl;
        public System.Windows.Forms.ToolStripButton PageSetupDlgTBtn;
    }
}