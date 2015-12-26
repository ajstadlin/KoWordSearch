//*************************************************************
// Korean Word Search - Print Worksheet Dialog
//=============================================================
// 12/25/15  Development
// 12/14/15  Created
//*************************************************************

using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace KoWordSearch
{
    public partial class PrintPreviewDLG : Form
    {
        #region [ Members ]

        // Print Properties
        public PageSettings XPageSettings = null;
        public PrintDocument XPrintDocument = null;

        public string Report_Title1 = "Korean Word Search";
        public string Report_HeadingSubText = "";

        public bool Report_PrintDate = false;
        public string Report_DateFormat = "MM/dd/yyyy HH:mm";
        public string Report_Date = "";

        private const string FONTNAME_BATANG = "Batang";

        private Font Header_Font;
        private Font Matrix_Font;
        private Brush Matrix_Brush;
        private Brush Transparent_Brush;
        private Font Data_Font;
        private Brush Data_Brush;
        private Pen Grid_Pen;
        private Font Footer_Font;

        // Graphics variables
        public float Origin_Left = 0.0f;
        public float Origin_Top = 0.0f;

        public float Header_Height = 0.0f;
        public float Footer_Height = 16.0f;

        // Calculated
        public float Matrix_Height = 0.0f;
        public float Data_Height = 0.0f;
        public float Dgv_Scale = 1.0f;

        public bool XPS_Print = false;
        public string XPS_FilePath = "";

        private const string CRLF = "\x000d\x000a";

        private DataTable m_MatrixTbl = null;
        private DataTable m_VocabTbl = null;

        #endregion


        #region [ Construct / Destruct ]

        public PrintPreviewDLG(DataTable tblMatrix, DataTable tblVocab)
        {
            InitializeComponent();

            this.PrintPreviewCtrl.Paint += PrintPreviewCtrl_Paint;
            this.ZoomFitPageRadio.CheckedChanged += ZoomRadio_CheckedChanged;
            this.ZoomWidthRadio.CheckedChanged += ZoomRadio_CheckedChanged;

            this.PrinterTDdl.Items.Clear();

            // Do not do the Printer Setup Stuff in Visual Studio Design Mode
            if (!System.Reflection.Assembly.GetExecutingAssembly().Location.Contains("VisualStudio"))
            {
                // Load the Printer Selection List
                foreach (string sPrinterName in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
                {
                    this.PrinterTDdl.Items.Add(sPrinterName);
                }
                this.PrinterTDdl.SelectedIndex = -1;
                if (this.PrinterTDdl.SelectedIndex < 0)
                {
                    // Set the Selected Printer to the Default System Printer
                    System.Drawing.Printing.PrintDocument pd = new System.Drawing.Printing.PrintDocument();
                    this.PrinterTDdl.SelectedIndex = this.PrinterTDdl.Items.IndexOf(pd.PrinterSettings.PrinterName);
                    pd.Dispose();
                }
            }
            Layout_ToolBars();

            if (System.Reflection.Assembly.GetExecutingAssembly().Location.Contains("VisualStudio"))
            {
                // Abort if IDE Design Mode
                return;
            }

            m_MatrixTbl = tblMatrix.Copy();
            ReportTools.ReportXPropertiesAdd(ref m_MatrixTbl, true, 0.0f, "CM", 0.0f, 0.0f, 0.0f, 0.0f, "");

            m_VocabTbl = tblVocab.Copy();
            ReportTools.ReportXPropertiesAdd(ref m_VocabTbl, true, 0.0f, "CM", 0.0f, 0.0f, 0.0f, 0.0f, "");

            // Default Page Settings
            XPageSettings = new PrinterSettings().DefaultPageSettings;
            XPageSettings.Margins = new Margins(50, 50, 50, 50);
            XPageSettings.Landscape = false;

            this.PrintTBtn.Visible = true;

            this.Width = Convert.ToInt32(Application.OpenForms[0].Width * 0.80);
            this.Height = Convert.ToInt32(Application.OpenForms[0].Height * 0.80);

            // Generate the Preview
            Preview_Puzzle();
        }

        #endregion


        #region [ Properties ]

        public PrintDocument Document
        {
            get
            {
                return this.PrintPreviewCtrl.Document;
            }
            set
            {
                this.PrintPreviewCtrl.Document = value;
            }
        }


        public Int32 StartPage
        {
            get
            {
                return this.PrintPreviewCtrl.StartPage;
            }
            set
            {
                this.PrintPreviewCtrl.StartPage = value;
            }
        }


        public bool View_AutoZoom
        {
            get
            {
                return this.PrintPreviewCtrl.AutoZoom;
            }
            set
            {
                this.PrintPreviewCtrl.AutoZoom = value;
            }
        }


        public Int32 View_Columns
        {
            get
            {
                return this.PrintPreviewCtrl.Columns;
            }
            set
            {
                this.PrintPreviewCtrl.Columns = value;
            }
        }


        public Int32 View_Rows
        {
            get
            {
                return this.PrintPreviewCtrl.Rows;
            }
            set
            {
                this.PrintPreviewCtrl.Rows = value;
            }
        }



        public string Printer_Name
        {
            get
            {
                return this.PrinterTDdl.Text;
            }
            set
            {
                // Reload the Printer List
                this.PrinterTDdl.Items.Clear();
                this.PrinterTDdl.Text = "";
                foreach (string sPrinterName in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
                {
                    this.PrinterTDdl.Items.Add(sPrinterName);
                }
                // Select the Printer
                this.PrinterTDdl.SelectedItem = value;
                if (this.PrinterTDdl.SelectedIndex < 0)
                {
                    // Set the Selected Printer to the Default System Printer
                    System.Drawing.Printing.PrintDocument pd = new System.Drawing.Printing.PrintDocument();
                    this.PrinterTDdl.SelectedIndex = this.PrinterTDdl.Items.IndexOf(pd.PrinterSettings.PrinterName);
                    pd.Dispose();
                }
            }
        }

        #endregion


        #region [ Methods ] 

        void Print_Mgr_PageCountChanged_Event(int iNewPageCount)
        {
          this.View_Reset(iNewPageCount, this.ZoomFitPageRadio.Checked);
        }


        public void View_Reset(int iPageCount, bool bFitPage)
        {
          // Microsoft .NET BUG:  Cannot change the PreviewPrintController!  
          if (this.PrintPreviewCtrl.StartPage != 0)
          {
            this.PrintPreviewCtrl.StartPage = 0;
          }
          if (bFitPage)
          {
            Zoom_Fit_Page();
          }
          else
          {
            Zoom_Fit_Width();
          }
        }


        private void PrintPreviewCtrl_Paint(object sender, PaintEventArgs e)
        {
          base.OnPaint(e);
          Zoom_Reset((decimal)this.PrintPreviewCtrl.Zoom);
        }


        private void ZoomNud_ValueChanged(object sender, EventArgs e)
        {
          if (this.ZoomNud.Value >= (decimal)0.50)
          {
            this.ZoomNud.Increment = (decimal)0.10;
          }
          else
          {
            this.ZoomNud.Increment = (decimal)0.02;
          }
          if (this.ZoomNud.Value == (decimal)this.PrintPreviewCtrl.Zoom) { return; }
          this.PrintPreviewCtrl.AutoZoom = false;
          this.PrintPreviewCtrl.Zoom = (double)this.ZoomNud.Value;
          this.PrintPreviewCtrl.Refresh();
        }


        public void Zoom_Fit_Page()
        {
          this.ZoomFitPageRadio.CheckedChanged -= ZoomRadio_CheckedChanged;
          this.ZoomWidthRadio.CheckedChanged -= ZoomRadio_CheckedChanged;
          this.ZoomFitPageRadio.Checked = true;
          this.PrintPreviewCtrl.AutoZoom = true;
          this.PrintPreviewCtrl.Columns = 1;
          this.PrintPreviewCtrl.Rows = 1;
          this.PrintPreviewCtrl.Refresh();
          Zoom_Reset((decimal)this.PrintPreviewCtrl.Zoom);
          this.ZoomFitPageRadio.CheckedChanged += ZoomRadio_CheckedChanged;
          this.ZoomWidthRadio.CheckedChanged += ZoomRadio_CheckedChanged;
        }


        public void Zoom_Fit_Width()
        {
          this.ZoomFitPageRadio.CheckedChanged -= ZoomRadio_CheckedChanged;
          this.ZoomWidthRadio.CheckedChanged -= ZoomRadio_CheckedChanged;
          this.ZoomWidthRadio.Checked = true;
          this.PrintPreviewCtrl.AutoZoom = false;
          if (PrintPreviewCtrl.Document != null)
          {
            Zoom_Reset((decimal)PrintPreviewCtrl.Width / (decimal)PrintPreviewCtrl.Document.DefaultPageSettings.Bounds.Width);
          }
          this.ZoomFitPageRadio.CheckedChanged += ZoomRadio_CheckedChanged;
          this.ZoomWidthRadio.CheckedChanged += ZoomRadio_CheckedChanged;
        }


        private void ZoomRadio_CheckedChanged(object sender, EventArgs e)
        {
          if (!((RadioButton)sender).Checked) { return; }
          if (ZoomFitPageRadio.Checked)
          {
            Zoom_Fit_Page();
          }
          else if (ZoomWidthRadio.Checked)
          {
            Zoom_Fit_Width();
          }
        }


        private void ZoomNud_Click(object sender, EventArgs e)
        {
          ZoomWidthRadio.Checked = false;
          ZoomFitPageRadio.Checked = false;
        }


        private void ZoomNud_Enter(object sender, EventArgs e)
        {
          ZoomWidthRadio.Checked = false;
          ZoomFitPageRadio.Checked = false;
        }


        private void Zoom_Reset(decimal dZoomFactor)
        {
          if ((dZoomFactor >= this.ZoomNud.Minimum)
              && (dZoomFactor <= this.ZoomNud.Maximum))
          {
            this.ZoomNud.Value = dZoomFactor;
          }
        }


        private void PageSetupTBtn_Click(object sender, EventArgs e)
        {
            // Launch the Page Setup Dialog
            try
            {
                PageSetupDialog psDlg = new PageSetupDialog();
                psDlg.PrinterSettings = this.PrintPreviewCtrl.Document.PrinterSettings;
                psDlg.PageSettings = this.PrintPreviewCtrl.Document.DefaultPageSettings;
                psDlg.AllowMargins = true;
                psDlg.AllowOrientation = true;
                psDlg.EnableMetric = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Print Page Setup Error" + CRLF + ex.Message, "Print Page Setup Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void PreviewDlgTBtn_Click(object sender, EventArgs e)
        {
            // Launch the Print Preview Dialog
            try
            {
                PrintPreviewDialog oPvDlg = new PrintPreviewDialog();
                oPvDlg.UseAntiAlias = true;
                oPvDlg.Document = this.PrintPreviewCtrl.Document;
                oPvDlg.Left = Convert.ToInt32(((float)Screen.PrimaryScreen.WorkingArea.Width - (float)Screen.PrimaryScreen.WorkingArea.Width * 0.8f) / 2.0f);
                oPvDlg.Top = Convert.ToInt32(((float)Screen.PrimaryScreen.WorkingArea.Top - (float)Screen.PrimaryScreen.WorkingArea.Top * 0.8f) / 2.0f);
                oPvDlg.Width = Convert.ToInt32((float)Screen.PrimaryScreen.WorkingArea.Width * 0.8f);
                oPvDlg.Height = Convert.ToInt32((float)Screen.PrimaryScreen.WorkingArea.Height * 0.8f);
                oPvDlg.ShowDialog();
                // Microsoft .NET BUG:  Cannot change the PreviewPrintController!  
            }
            catch (Exception ex)
            {
                MessageBox.Show("Print Preview Error" + CRLF + ex.Message, "Print Preview Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void PrinterSelectLb_Click(object sender, EventArgs e)
        {
              this.PrinterTDdl.Focus();
              this.PrinterTDdl.DroppedDown = true;
        }


        private void PrinterTDdl_DropDown(object sender, EventArgs e)
        {
          // Save_Cfg the currently selected printer
          object oSave = this.PrinterTDdl.SelectedItem;

          // Reload the Printer List
          this.PrinterTDdl.Items.Clear();
          this.PrinterTDdl.Text = "";
          foreach (string sPrinterName in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
          {
            this.PrinterTDdl.Items.Add(sPrinterName);
          }

          // Restore previously selected printer
          this.PrinterTDdl.SelectedItem = oSave;

          if (this.PrinterTDdl.SelectedIndex < 0)
          {
            // Set the Selected Printer to the Default System Printer
            System.Drawing.Printing.PrintDocument pd = new System.Drawing.Printing.PrintDocument();
            this.PrinterTDdl.SelectedIndex = this.PrinterTDdl.Items.IndexOf(pd.PrinterSettings.PrinterName);
            pd.Dispose();
          }
        }


        private void Layout_ToolBars()
        {
          int iLeftOffset = 0;
          iLeftOffset = this.PrintTBar.Width + 2;
          this.PrinterTBar.Left = iLeftOffset;
        }


        private void TBar_VisibleChanged(object sender, EventArgs e)
        {
          Layout_ToolBars();
        }


        private void PrintTBtn_Click(object sender, EventArgs e)
        {
            Print_Puzzle();
        }


        private void Preview_Puzzle()
        {
            try
            {
                Compose_Worksheet(false, true);
                this.PrintPreviewCtrl.Document = XPrintDocument;
                Zoom_Fit_Width();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Worksheet Preview Error" + CRLF + ex.Message, "Worksheet Preview Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Print_Puzzle()
        {
            try
            {
                Compose_Worksheet(false, true);
                XPS_Print = false;
                XPrintDocument.Print();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Worksheet Print Error" + CRLF + ex.Message, "Worksheet Print Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Compose_Worksheet(bool bReload, bool bPreview)
        {
            if (this.XPrintDocument != null)
            {
                this.XPrintDocument.BeginPrint -= On_BeginPrint;
                this.XPrintDocument.QueryPageSettings -= On_QueryPageSettings;
                this.XPrintDocument.PrintPage -= On_PrintPage;
                this.XPrintDocument.EndPrint -= On_EndPrint;
                this.XPrintDocument.Dispose();
                this.XPrintDocument = null;
                GC.Collect();
            }

            this.XPrintDocument = new PrintDocument();
            this.XPrintDocument.PrinterSettings = new PrinterSettings();

            if (XPS_Print)
            {
                this.XPrintDocument.PrinterSettings.PrinterName = "Microsoft XPS Document Writer";
                this.XPrintDocument.PrinterSettings.PrintToFile = true;
                this.XPrintDocument.PrinterSettings.PrintFileName = XPS_FilePath;
            }
            else
            {
                this.XPrintDocument.PrinterSettings.PrinterName = Printer_Name;
            }

            /// ** START Page Settings Management
            // Save vital XPageSettings
            Margins mMargins = XPageSettings.Margins;
            bool bIsLandscape = XPageSettings.Landscape;
            // Update XPageSettings with the selected printer's DefaultPageSettings
            XPageSettings = this.XPrintDocument.DefaultPageSettings;
            //Update the Printer's Default Page Settings with vital XPageSettings
            XPageSettings.Margins = mMargins;
            XPageSettings.Landscape = bIsLandscape;
            this.XPrintDocument.DefaultPageSettings = XPageSettings;
            /// ** END Page Settings Management

            this.XPrintDocument.BeginPrint += On_BeginPrint;
            this.XPrintDocument.QueryPageSettings += On_QueryPageSettings;
            this.XPrintDocument.PrintPage += On_PrintPage;
            this.XPrintDocument.EndPrint += On_EndPrint;

            this.XPrintDocument.DocumentName = Report_Title1;
        }



        void On_BeginPrint(object sender, PrintEventArgs e)
        {
            Header_Font = new Font(FONTNAME_BATANG, 8, FontStyle.Bold);
            Matrix_Font = new Font(FONTNAME_BATANG, 16, FontStyle.Regular);
            Matrix_Brush = new System.Drawing.SolidBrush(Color.Black);
            Data_Font = new Font(FONTNAME_BATANG, 9, FontStyle.Regular);
            Data_Brush = new System.Drawing.SolidBrush(Color.Black);
            Grid_Pen = new Pen(Color.DarkGray, 0);
            Footer_Font = new Font(FONTNAME_BATANG, 8, FontStyle.Regular);
            Transparent_Brush = new System.Drawing.SolidBrush(Color.Transparent);
            Report_Date = DateTime.Now.ToString(Report_DateFormat);
        }


        void On_QueryPageSettings(object sender, QueryPageSettingsEventArgs e)
        {
        }


        void On_PrintPage(object sender, PrintPageEventArgs ppea)
        {
            //e.Graphics.PageUnit = GraphicsUnit.Inch;
            //e.Graphics.PageScale = 0.01F;

            IntPtr hdc = ppea.Graphics.GetHdc();
            Int32 HorzRes = WinApi.GetDeviceCaps(hdc, (Int32)WinApi.DevCapParm.HORZRES);
            Int32 VertRes = WinApi.GetDeviceCaps(hdc, (Int32)WinApi.DevCapParm.VERTRES);
            Int32 PhysicalXOffset = WinApi.GetDeviceCaps(hdc, (Int32)WinApi.DevCapParm.PHYSICALOFFSETX);
            Int32 PhysicalYOffset = WinApi.GetDeviceCaps(hdc, (Int32)WinApi.DevCapParm.PHYSICALOFFSETY);
            Int32 PhysicalWidth = WinApi.GetDeviceCaps(hdc, (Int32)WinApi.DevCapParm.PHYSICALWIDTH);
            Int32 PhysicalHeight = WinApi.GetDeviceCaps(hdc, (Int32)WinApi.DevCapParm.PHYSICALHEIGHT);
            ppea.Graphics.ReleaseHdc(hdc);
            //-- Bitmap bm = new Bitmap(PhysicalWidth, PhysicalHeight, ppea.Graphics);
            //-- Graphics g = Graphics.FromImage(bm);
            //Graphics g = ppea.Graphics;

            // PageSettings PageSet_Margins are in 0.01" units
            if (this.XPrintDocument.PrintController.IsPreview)
            {
                Origin_Left = ppea.MarginBounds.Left;
                Origin_Top = ppea.MarginBounds.Top;
            }
            else
            {
                //  Graphics will be sent to the Printer.  Adjust Origin for the Physical Offsets
                //  Pixels / DPI = Inches 
                Origin_Left = ppea.MarginBounds.Left - Convert.ToInt32(((float)PhysicalXOffset / (float)ppea.Graphics.DpiX) * 80.0f);
                Origin_Top = ppea.MarginBounds.Top - Convert.ToInt32(((float)PhysicalYOffset / (float)ppea.Graphics.DpiY) * 80.0f);
            }

            Data_Height = ppea.MarginBounds.Height - Header_Height - Matrix_Height - Footer_Height - 8.0f;
            Dgv_Scale = 1.0f;

            SizeF szfHeader = new SizeF(ppea.MarginBounds.Width, Header_Height);
            Print_Header(ppea, szfHeader);

            float fPageY = Origin_Top + szfHeader.Height;
            try
            {
                SizeF szfMatrix = new SizeF(ppea.MarginBounds.Width, Matrix_Height);
                fPageY = Print_Matrix(ppea, szfMatrix, fPageY);

                fPageY += 8.0f;

                SizeF szfData = new SizeF(ppea.MarginBounds.Width, ppea.MarginBounds.Height - fPageY - Footer_Height);
                fPageY = Print_Data(ppea, szfData, Origin_Left + ppea.MarginBounds.Width, fPageY);

                // Print the Data
                ppea.HasMorePages = false;  // in case there is no data or there is an error
            }
            catch (Exception ex)
            {
                //Log.Err(ex, this.Name, "OnPrintPage1", Log.LogDevices.CON_LOG_DLG);
            }
            Print_PageFooter(ppea, 1, 1);
            ppea.HasMorePages = false;
        }


        void On_EndPrint(object sender, PrintEventArgs e)
        {
            Header_Font.Dispose();
            Matrix_Font.Dispose();
            Matrix_Brush.Dispose();
            Data_Font.Dispose();
            Data_Brush.Dispose();
            Grid_Pen.Dispose();
            Footer_Font.Dispose();
        }


        public void Print_Header(PrintPageEventArgs ppea, SizeF szfRegion)
        {
        }


        public float Print_Matrix(PrintPageEventArgs ppea, SizeF szfRegion, float fYpos)
        {
            try
            {
                // Draw the Region Border
                // ppea.Graphics.DrawRectangle(penMatrix, Origin_Left, fYpos, szfRegion.Width, szfRegion.Height);

                foreach (DataColumn dc in m_MatrixTbl.Columns)
                {
                    dc.ExtendedProperties[ReportTools.EP_WIDTH] = Matrix_Font.Size * 2.2f; //(float)szfRegion.Width / (float)m_MatrixTbl.Columns.Count;
                    dc.ExtendedProperties[ReportTools.EP_MARGIN_TOP] = 4.0f;
                    dc.ExtendedProperties[ReportTools.EP_MARGIN_BOTTOM] = 0f;
                }

                float rowWidth = Matrix_Font.Size * 2.2f * (float)m_MatrixTbl.Columns.Count;
                float rowHeight = Matrix_Font.Size * 2.0f;

                for (int rrow = 0; rrow < m_MatrixTbl.Rows.Count; rrow++)
                {
                    SizeF szfRow =  ReportTools.Print_TableRow(ppea.Graphics, 1.0f, Origin_Left + ((ppea.MarginBounds.Width - rowWidth) / 2.0f), fYpos, rowHeight,
                                                               Matrix_Font, Matrix_Brush, Transparent_Brush, Grid_Pen, m_MatrixTbl.DefaultView, rrow);
                    fYpos += szfRow.Height;
                }
            }
            catch (Exception ex)
            {
                //Log.Err(ex, this.Name, "Print_Region_2", Log.LogDevices.CON_LOG_DLG);
            }
            return fYpos;
        }


        public float Print_Data(PrintPageEventArgs ppea, SizeF szfRegion, float fXpos, float fYpos)
        {
            try
            {
                int rowsPerColumn = Convert.ToInt32(Math.Floor(Convert.ToDouble(szfRegion.Height / ((float)Data_Font.Size * 1.7f))));
                int columnsRequired = Convert.ToInt32(Math.Ceiling(Convert.ToDouble((float)m_VocabTbl.Rows.Count / (float)rowsPerColumn)));
                float rowHeight = Data_Font.Size * 2.0f;
                float colWidth = ppea.MarginBounds.Width / ((float)columnsRequired);

                int indexTable = 0;
                int indexRow = 0;
                int indexColumn = 0;
                foreach (DataRowView drv in m_VocabTbl.DefaultView)
                {
                    if (indexRow >= rowsPerColumn)
                    {
                        indexRow = 0;
                        indexColumn++;
                    }
                    string vocabString = "ㅁ " + drv[MainForm.VOC_ENWORD].ToString();
                    ppea.Graphics.DrawString(vocabString, Data_Font, Data_Brush,
                                             Origin_Left + 8.0f + (indexColumn * colWidth),
                                             fYpos + (indexRow * rowHeight));
                    indexRow++;
                    indexTable++;
                }
            }
            catch (Exception ex)
            {
            //    Log.Err(ex, this.Name, "Print_Region_3", Log.LogDevices.CON_LOG_DLG);
            }
            return fYpos;
        }


        public void Print_PageFooter(PrintPageEventArgs ppea, Int32 iPageNbr, Int32 iPageCount)
        {
            // Print the Page Footer, Page Number, and Date Printed      
            Pen penFooter = new Pen(Color.Black, 0);
            Font fntFooter = new Font("Batang", 7, FontStyle.Regular);
            try
            {
                // Draw the Page Footer Border
                //ppea.Graphics.DrawRectangle(penFooter, Origin_Left, Origin_Top + ppea.MarginBounds.Height - Footer_Height,
                //                            ppea.MarginBounds.Width, Footer_Height);

                float fYpos = Origin_Top + ppea.MarginBounds.Height - Footer_Height + 2.0f;

                ppea.Graphics.DrawString("Korean Word Search by 독수리", fntFooter, Brushes.Black,
                                         Origin_Left + 10.0F, fYpos);
            }
            catch (Exception ex)
            {
                ppea.HasMorePages = false;
                //Log.Err(ex, this.Name, "Print_PageFooter", Log.LogDevices.CON_LOG_DLG);
            }
            fntFooter.Dispose(); 
            penFooter.Dispose(); 
        }

        #endregion

    }
}
