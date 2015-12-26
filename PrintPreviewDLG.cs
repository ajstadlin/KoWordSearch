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

        private Font Header_Font;
        private Font Matrix_Font;
        private Font Data_Font;
        private Font Footer_Font;

        public int RepCursor = 0;
        public int RowsPerPage = 9;

        // Graphics variables
        public float Origin_Left = 0.0f;
        public float Origin_Top = 0.0f;

        public float Header_Height = 50.0f;
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
            Header_Font = new Font(this.Font.Name, 8, FontStyle.Bold);
            Matrix_Font = new Font("Batang", 16, FontStyle.Regular);
            Data_Font = new Font(this.Font.Name, 8, FontStyle.Regular);
            Footer_Font = new Font(this.Font.Name, 8, FontStyle.Regular);

            Report_Date = DateTime.Now.ToString(Report_DateFormat);
            RepCursor = 0;
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
            Pen penGrid = new Pen(Color.DarkGray, 0);
            try
            {
                SizeF szfMatrix = new SizeF(ppea.MarginBounds.Width, Matrix_Height);
                Print_Matrix(ppea, szfMatrix, fPageY);

                SizeF szfData = new SizeF(ppea.MarginBounds.Width - ppea.MarginBounds.Width, Matrix_Height);
                Print_Data(ppea, szfData, Origin_Left + ppea.MarginBounds.Width, fPageY);

                fPageY += szfData.Height;

                //Data_Height = szfRegion4.Height - 26.0F;
                //float fRowHeight = Data_Height / RowsPerPage;
                //float fDataBottom = ppea.MarginBounds.Height - 24.0f;

                // Print the Data
                ppea.HasMorePages = false;  // in case there is no data or there is an error

                fPageY += 20.0f;

                // For each row on the page ...
                //bool bRowProcessing = true;
                //while (bRowProcessing && (RepCursor < t_ProjItem.Rows.Count))
                //{
                //    // Measure the Maximum Height of the Header Cells' Text and Calculate the Y coordinate of the cell bottom
                //    if ((fPageY + fRowHeight + 4.0f) >= fDataBottom)
                //    {
                //        // Printing this Row will overflow the page.
                //        // Defer to next page
                //        ppea.HasMorePages = true;
                //        break;
                //    }

                //    if (b_PageInRange)
                //    {
                //        if (PackList_Style == Report_Types.RT_PACKLIST_0)
                //        {
                //            ppea.Graphics.DrawLine(Pens.Black, Origin_Left + 100.0f, fPageY, Origin_Left + 100.0f, fPageY + fRowHeight);
                //            ppea.Graphics.DrawLine(Pens.Black, Origin_Left + ppea.MarginBounds.Width - 90.0f, fPageY,
                //                                   Origin_Left + ppea.MarginBounds.Width - 90.0f, fPageY + fRowHeight);
                //        }
                //        ppea.Graphics.DrawLine(Pens.Black, Origin_Left, fPageY + fRowHeight,
                //                               Origin_Left + ppea.MarginBounds.Width, fPageY + fRowHeight);
                //    }

                //    fPageY += 4.0f;
                //    SizeF szfArea = new SizeF(80.0f, fRowHeight);
                //    if (b_PageInRange)
                //    {
                //        Report_Tools.DrawText_AlignLeft(ppea.Graphics, t_ProjItem.DefaultView[RepCursor][PackingListDB.C_ITM_PO_LINE_ITEM].ToString(),
                //                                        Data_Font, Brushes.Black, szfArea, Origin_Left + 10.0f, fPageY);
                //    }

                //    szfArea = new SizeF(630.0f, fRowHeight);
                //    string sCustomDscr = t_ProjItem.DefaultView[RepCursor][PackingListDB.C_CUSTOM_DSCR].ToString();
                //    //t_ProjItem.DefaultView[RepCursor][PackingList_DB.C_DWGPART].ToString() + " - "
                //    //+ Tools.ITF(t_ProjItem.DefaultView[RepCursor][PackingList_DB.C_DWGPARTMOD].ToString() == "", "", 
                //    //            t_ProjItem.DefaultView[RepCursor][PackingList_DB.C_DWGPARTMOD].ToString() +  ", ")
                //    //+ t_ProjItem.DefaultView[RepCursor][PackingList_DB.C_PART_DESCRIPTION].ToString() + Log.CRLF
                //    //+ t_ProjItem.DefaultView[RepCursor][PackingList_DB.C_ITM_NOTES].ToString().Trim();
                //    if (b_PageInRange)
                //    {
                //        Report_Tools.DrawText_AlignLeft(ppea.Graphics, sCustomDscr,
                //                                        Data_Font, Brushes.Black, szfArea, Origin_Left + 110.0f, fPageY);
                //    }

                //    szfArea = new SizeF(80.0f, fRowHeight);
                //    if (b_PageInRange)
                //    {
                //        Report_Tools.DrawText_AlignLeft(ppea.Graphics, t_ProjItem.DefaultView[RepCursor][PackingListDB.C_ITM_QTY].ToString(),
                //                                        Data_Font, Brushes.Black, szfArea, Origin_Left + ppea.MarginBounds.Width - 80.0f, fPageY);
                //    }

                //    if ((PackList_Style == Report_Types.RT_PACKLIST_1)
                //        || (PackList_Style == Report_Types.RT_PACKLIST_2))
                //    {
                //        string sCaption = "Line Item";
                //        SizeF szfCaption = ppea.Graphics.MeasureString(sCaption, BarcodeCap_Font);
                //        if (b_PageInRange)
                //        {
                //            ppea.Graphics.DrawString(sCaption, BarcodeCap_Font, Brushes.Black, Origin_Left + 16f, fPageY + 32f);

                //            sCaption = t_ProjItem.DefaultView[RepCursor][PackingListDB.C_ITM_PO_LINE_ITEM].ToString().Trim();
                //            string sBarcode = Barcode.StrToBarcode128(sCaption);
                //            SizeF szfBarcode = ppea.Graphics.MeasureString(sBarcode, Barcode_Font);
                //            PointF xyLoc = new PointF(Origin_Left + 10f, fPageY + 30f + szfCaption.Height);
                //            ppea.Graphics.DrawString(sBarcode, Barcode_Font, Brushes.Black, xyLoc);
                //            ppea.Graphics.DrawString(sCaption, BarcodeCap_Font, Brushes.Black,
                //                                     xyLoc.X + 10.0f, xyLoc.Y + szfBarcode.Height - 2.0f);

                //            sCaption = "Part Number";
                //            szfCaption = ppea.Graphics.MeasureString(sCaption, BarcodeCap_Font);
                //            ppea.Graphics.DrawString(sCaption, BarcodeCap_Font, Brushes.Black, Origin_Left + 206.0f, fPageY + 32f);

                //            sCaption = t_ProjItem.DefaultView[RepCursor][PackingListDB.C_CUSTOM_PART].ToString().Trim();
                //            sBarcode = Barcode.StrToBarcode128(sCaption);
                //            szfBarcode = ppea.Graphics.MeasureString(sBarcode, Barcode_Font);
                //            xyLoc = new PointF(Origin_Left + 200.0f, fPageY + 30f + szfCaption.Height);
                //            ppea.Graphics.DrawString(sBarcode, Barcode_Font, Brushes.Black, xyLoc);
                //            ppea.Graphics.DrawString(sCaption, BarcodeCap_Font, Brushes.Black,
                //                                     xyLoc.X + 10.0f, xyLoc.Y + szfBarcode.Height - 2.0f);

                //            if (PackList_Style == Report_Types.RT_PACKLIST_2)
                //            {
                //                sCaption = t_ProjItem.DefaultView[RepCursor][PackingListDB.C_BARCODE1_CAPTION].ToString().Trim(); ;
                //                szfCaption = ppea.Graphics.MeasureString(sCaption, BarcodeCap_Font);
                //                ppea.Graphics.DrawString(sCaption, BarcodeCap_Font, Brushes.Black, Origin_Left + 532.0f, fPageY + 32f);

                //                sCaption = t_ProjItem.DefaultView[RepCursor][PackingListDB.C_BARCODE1_DATA].ToString().Trim();
                //                sBarcode = Barcode.StrToBarcode128(sCaption);
                //                szfBarcode = ppea.Graphics.MeasureString(sBarcode, Barcode_Font);
                //                xyLoc = new PointF(Origin_Left + 526.0f, fPageY + 30f + szfCaption.Height);
                //                ppea.Graphics.DrawString(sBarcode, Barcode_Font, Brushes.Black, xyLoc);
                //                ppea.Graphics.DrawString(sCaption, BarcodeCap_Font, Brushes.Black,
                //                                         xyLoc.X + 10.0f, xyLoc.Y + szfBarcode.Height - 2.0f);
                //            }
                //        }
                //    }
                //    fPageY += fRowHeight - 4.0f;
                //    ppea.HasMorePages = true;
                //    RepCursor++;
                //}
            }
            catch (Exception ex)
            {
                //Log.Err(ex, this.Name, "OnPrintPage1", Log.LogDevices.CON_LOG_DLG);
            }
            Print_PageFooter(ppea, 1, 1);
            penGrid.Dispose();
            ppea.HasMorePages = false;
        }


        void On_EndPrint(object sender, PrintEventArgs e)
        {
            Header_Font.Dispose();
            Matrix_Font.Dispose();
            Data_Font.Dispose();
            Footer_Font.Dispose();
        }


        public void Print_Header(PrintPageEventArgs ppea, SizeF szfRegion)
        {
            Pen penHeader = new Pen(Color.Black, 1);
            //Font fntTitle = new Font(this.Font.Name, 16, FontStyle.Bold | FontStyle.Italic);   //GraphicLib.FONTNAME_DEJAVU_SANS_MONO, 16);
            //Font fntReturnName = new Font(this.Font.Name, 14, FontStyle.Bold);
            //Font fntReturnAddress = new Font(this.Font.Name, 10, FontStyle.Regular);
            try
            {
                // Draw the Page Border
                // ppea.Graphics.DrawRectangle(penHeader, Origin_Left, Origin_Top, ppea.MarginBounds.Width, ppea.MarginBounds.Height);

                // Draw the Region_1 Border
                //ppea.Graphics.DrawRectangle(penHeader, Origin_Left, Origin_Top, ppea.MarginBounds.Width, szfRegion.Height);

                //    string sPackingList = "Packing List";
                //    SizeF szf1 = ppea.Graphics.MeasureString(sPackingList, fntTitle);
                //    ppea.Graphics.DrawString(sPackingList, fntTitle, Brushes.Black,
                //                             Origin_Left + ppea.MarginBounds.Width - szf1.Width - 12.0f, Origin_Top + 4.0f);

                //    string sReturnName = t_Project.DefaultView[0][PackingListDB.C_RETURN_NAME].ToString();
                //    SizeF szf2 = ppea.Graphics.MeasureString(sReturnName, fntReturnName);
                //    ppea.Graphics.DrawString(sReturnName, fntReturnName, Brushes.Black,
                //                             Origin_Left + 20.0f, Origin_Top + 4.0f);

                //    string sReturnAddress = t_Project.DefaultView[0][PackingListDB.C_RETURN_ADDRESS].ToString();
                //    SizeF szf3 = ppea.Graphics.MeasureString(sReturnAddress, fntReturnAddress);
                //    ppea.Graphics.DrawString(sReturnAddress, fntReturnAddress, Brushes.Black,
                //                             Origin_Left + 20.0f, Origin_Top + 4.0f + szf2.Height + 0.8f);
            }
            catch (Exception ex)
            {
                //Log.Err(ex, this.Name, "Print_Region_1", Log.LogDevices.CON_LOG_DLG);
            }
            //fntTitle.Dispose(); fntTitle = null;
            //fntReturnName.Dispose(); fntReturnName = null;
            //fntReturnAddress.Dispose(); fntReturnAddress = null;
            penHeader.Dispose(); 
        }


        public void Print_Matrix(PrintPageEventArgs ppea, SizeF szfRegion, float fYpos)
        {
            Pen penMatrix = new Pen(Color.Black, 1);
            Brush brushData = new System.Drawing.SolidBrush(Color.Black);
            Brush brushFill = new System.Drawing.SolidBrush(Color.Transparent);
            try
            {
                // Draw the Region_2 Border
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
                                                               Matrix_Font, brushData, brushFill, penMatrix, m_MatrixTbl.DefaultView, rrow);
                    fYpos += szfRow.Height;
                }
            }
            catch (Exception ex)
            {
                //Log.Err(ex, this.Name, "Print_Region_2", Log.LogDevices.CON_LOG_DLG);
            }
            brushData.Dispose();
            brushFill.Dispose();
            penMatrix.Dispose(); 
        }


        public void Print_Data(PrintPageEventArgs ppea, SizeF szfRegion, float fXpos, float fYpos)
        {
            Pen penData = new Pen(Color.Black, 1);
            //Font fontLabel1 = new Font(this.Font.Name, 12, FontStyle.Bold);
            //Font fontAddress = new Font(this.Font.Name, 12, FontStyle.Regular);
            //Font fontLabel2 = new Font(this.Font.Name, 11, FontStyle.Bold);   //GraphicLib.FONTNAME_DEJAVU_SANS_MONO, 16);
            //Font fontNote = new Font(this.Font.Name, 11, FontStyle.Regular);
            try
            {
                // Draw the Region_3 Border
                ppea.Graphics.DrawRectangle(penData, fXpos, fYpos, szfRegion.Width, szfRegion.Height);

            //    string sLabel1 = "Ship To:";
            //    SizeF szf1 = ppea.Graphics.MeasureString(sLabel1, fontLabel1);
            //    ppea.Graphics.DrawString(sLabel1, fontLabel1, Brushes.Black,
            //                             fXpos + 4.0f, fYpos + 8.0f);

            //    string sAddress = t_Project.DefaultView[0][PackingListDB.C_PROJ_SHIP_TO].ToString();
            //    SizeF szf2 = ppea.Graphics.MeasureString(sAddress, fontAddress);
            //    ppea.Graphics.DrawString(sAddress, fontAddress, Brushes.Black,
            //                             fXpos + szf1.Width + 10.0f, fYpos + 8.0f);

            //    // Divider Line
            //    ppea.Graphics.DrawLine(pen1, fXpos, fYpos + 150.0f, fXpos + szfRegion.Width, fYpos + 150.0f);

            //    SizeF szf3 = ppea.Graphics.MeasureString(PackList_Note, fontNote);
            //    ppea.Graphics.DrawString(PackList_Note, fontNote, Brushes.Black,
            //                             fXpos + 8.0f, fYpos + 8.0f);

            //    if ((PackList_Style == Report_Types.RT_PACKLIST_1)
            //        || (PackList_Style == Report_Types.RT_PACKLIST_2))
            //    {
            //        string sPoNbr = "PO #";
            //        SizeF szf4 = ppea.Graphics.MeasureString(sPoNbr, fontLabel2);
            //        ppea.Graphics.DrawString(sPoNbr, fontLabel2, Brushes.Black,
            //                                 fXpos + 8.0f, fYpos + 240.0f);

            //        string sPoBarcode = Barcode.StrToBarcode128(t_Project.DefaultView[0][PackingListDB.C_ORD_PO_NUMBER].ToString().Trim());
            //        SizeF szf5 = ppea.Graphics.MeasureString(sPoBarcode, Barcode_Font);
            //        PointF xyLoc = new PointF(fXpos + szf4.Width + 20.0f, fYpos + 240.0f);
            //        ppea.Graphics.DrawString(sPoBarcode, Barcode_Font, Brushes.Black, xyLoc);
            //        ppea.Graphics.DrawString(t_Project.DefaultView[0][PackingListDB.C_ORD_PO_NUMBER].ToString().Trim(), BarcodeCap_Font, Brushes.Black,
            //                                 xyLoc.X + 10.0f, xyLoc.Y + szf5.Height);
            //    }

            }
            catch (Exception ex)
            {
            //    Log.Err(ex, this.Name, "Print_Region_3", Log.LogDevices.CON_LOG_DLG);
            }
            //fontLabel1.Dispose(); fontLabel1 = null;
            //fontAddress.Dispose(); fontAddress = null;
            //fontNote.Dispose(); fontNote = null;
            //fontLabel2.Dispose(); fontLabel2 = null;
            penData.Dispose(); 
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
