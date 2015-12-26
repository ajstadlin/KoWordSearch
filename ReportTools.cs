//=============================================================
// ReportTools
//-------------------------------------------------------------
// 09/11/15 17.0.90.12/5.200.12  DrawTextBox_AlignCenter
// 09/22/14 17.0.39.0/5.150.2  Print_LetterHead added
//=============================================================

using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;


namespace KoWordSearch
{
    public class ReportTools
    {
        private const string THIS_NAME = "ReportTools";

        /// How to load a font at runtime:
        //PrivateFontCollection foo = new PrivateFontCollection();
        //foo.AddFontFile(Application.StartupPath + @"\Fonts\c128att.ttf");
        //Font fontBarcode = new Font((System.Drawing.FontFamily)foo.Families[0], 24, FontStyle.Regular);

        //put an image file under a Images directory within your application root
        //Bitmap bitmap = new Bitmap(Convert.ToInt32(szf5.Width)*10, Convert.ToInt32(szf5.Height)*10, ppea.Graphics);
        //Graphics.FromImage(bitmap).DrawString(sPoBarcode, fontBarcode, Brushes.Black, 10f, 10f);
        //bitmap.Save(Ini.Temp_FileName("Barcode_", "", ".png"), ImageFormat.Png);
        //ppea.Graphics.DrawImage(bitmap, xyLoc);
        //bitmap.Dispose(); bitmap = null;


        public const string EP_ALIGN = "ALIGN";
        public const string EP_VISIBLE = "VISIBLE";
        public const string EP_WIDTH = "WIDTH";
        public const string EP_MARGIN_LEFT = "MARGIN_LEFT";
        public const string EP_MARGIN_TOP = "MARGIN_TOP";
        public const string EP_MARGIN_RIGHT = "MARGIN_RIGHT";
        public const string EP_MARGIN_BOTTOM = "MARGIN_BOTTOM";
        public const string EP_FONT_STYLE = "FONT_STYLE";

        public static void ReportXPropertiesAdd(ref DataTable rxTbl, bool bDefVisible, float fDefCellWidth, string sDefAlign,
                                                float fDefMarginLeft, float fDefMarginTop, float fDefMarginRight, float fDefMarginBottom,
                                                string sDefFontStyle)
        {
            // Add Extended Properties for Printing Control
            for (int cc = 0; cc < rxTbl.Columns.Count; cc++)
            {
                rxTbl.Columns[cc].ExtendedProperties.Add(EP_ALIGN, sDefAlign);
                rxTbl.Columns[cc].ExtendedProperties.Add(EP_VISIBLE, bDefVisible);
                rxTbl.Columns[cc].ExtendedProperties.Add(EP_WIDTH, fDefCellWidth);
                rxTbl.Columns[cc].ExtendedProperties.Add(EP_MARGIN_LEFT, fDefMarginLeft);
                rxTbl.Columns[cc].ExtendedProperties.Add(EP_MARGIN_TOP, fDefMarginTop);
                rxTbl.Columns[cc].ExtendedProperties.Add(EP_MARGIN_RIGHT, fDefMarginRight);
                rxTbl.Columns[cc].ExtendedProperties.Add(EP_MARGIN_BOTTOM, fDefMarginBottom);
                rxTbl.Columns[cc].ExtendedProperties.Add(EP_FONT_STYLE, sDefFontStyle);
            }
        }


        public static SizeF Data_MaxCellSizeF(Graphics g, ref SizeF szfRow, float fTblScale, Font cellFont, DataView xDV, Int32 iRowCursor, ref string sMaxFieldName)
        {
            SizeF szfMax = new SizeF(0.0f, 0.0f);
            SizeF szfData = new SizeF(0.0f, 0.0f);
            szfRow = new SizeF(0.0f, 0.0f);

            for (int cc = 0; cc < xDV.Table.Columns.Count; cc++)
            {
                if (!Convert.ToBoolean(xDV.Table.Columns[cc].ExtendedProperties[EP_VISIBLE]))
                {
                    // skip invisible columns
                    continue;
                }
                szfData = g.MeasureString(xDV[iRowCursor][cc].ToString(), cellFont,
                                          Convert.ToInt32((Convert.ToSingle(xDV.Table.Columns[cc].ExtendedProperties[EP_WIDTH]) * fTblScale)));
                szfData.Height += Convert.ToInt32(xDV.Table.Columns[cc].ExtendedProperties[EP_MARGIN_TOP])
                                  + Convert.ToInt32(xDV.Table.Columns[cc].ExtendedProperties[EP_MARGIN_BOTTOM]);
                if (szfData.Height > szfMax.Height)
                {
                    szfMax.Height = szfData.Height;
                    sMaxFieldName = xDV.Table.Columns[cc].ColumnName;
                }
                if (szfData.Width > szfMax.Width)
                {
                    szfMax.Width = szfData.Width;
                }
                szfRow.Width += Convert.ToSingle(xDV.Table.Columns[cc].ExtendedProperties[EP_WIDTH]) * fTblScale;
            }
            szfRow.Height = szfMax.Height;
            return szfMax;
        }


        public static SizeF Header_MaxCellSizeF(Graphics g, ref SizeF szfRow, float fTblScale, Font cellFont, DataTable xTbl, ref string sMaxFieldName)
        { 
            SizeF szfMax = new SizeF(0.0f, 0.0f);
            SizeF szfData = new SizeF(0.0f, 0.0f);
            szfRow = new SizeF(0.0f, 0.0f);

            for (int cc = 0; cc < xTbl.Columns.Count; cc++)
            {
                if (!Convert.ToBoolean(xTbl.Columns[cc].ExtendedProperties[EP_VISIBLE])) { continue; }
                szfData = g.MeasureString(xTbl.Columns[cc].Caption, cellFont,
                                            Convert.ToInt32((Convert.ToSingle(xTbl.Columns[cc].ExtendedProperties[EP_WIDTH]) * fTblScale) - 4.0f));
                if (szfData.Height > szfMax.Height)
                {
                    szfMax.Height = szfData.Height;
                    sMaxFieldName = xTbl.Columns[cc].ColumnName;
                }
                if (szfData.Width > szfMax.Width) { szfMax.Width = szfData.Width; }
                szfRow.Width += Convert.ToInt32(Convert.ToSingle(xTbl.Columns[cc].ExtendedProperties[EP_WIDTH]) * fTblScale);
            }
            szfRow.Height = szfMax.Height;
            return szfMax;
        }


        public static float Scale_Factor(DataTable xTbl, float fBoundsWidth)
        {
            float fScale = 1.0f;
            // Calculate the total of all DataGridView Column widths and the Scale Factor to get them to fit on the page
            float fTblWidth = 0.0f;
            for (int cc = 0; cc < xTbl.Columns.Count; cc++)
            {
                if (!Convert.ToBoolean(xTbl.Columns[cc].ExtendedProperties[EP_VISIBLE])) { continue; }
                fTblWidth += Convert.ToSingle(xTbl.Columns[cc].ExtendedProperties[EP_WIDTH]);
            }
            if (fTblWidth == 0) { fTblWidth = 1; }
            else { fScale = fBoundsWidth / fTblWidth; }
            return fScale;
        }


        public static SizeF Print_Table_Header(Graphics g, float fScale, float fPageX, float fPageY,
                                               Font fontHeader, Brush brushFont, Brush brushFill, Pen penGrid,
                                               DataTable xTbl)
        {
            SizeF szfRow = new SizeF(0.0f, 0.0f);
            string sMaxFieldName = "";
            float fCellRight = 0.0f;
            try
            {
                // Measure the Maximum Height of the Header Cells' Text and Calculate the Y coordinate of the cell bottom
                SizeF szfMaxCell = Header_MaxCellSizeF(g, ref szfRow, fScale, fontHeader, xTbl, ref sMaxFieldName);
                szfRow.Height += 2.0f;
                float fCellBottom = fPageY + szfRow.Height;

                // Print the Table Header shading
                g.FillRectangle(brushFill, fPageX + 0.2F, fPageY, szfRow.Width - 0.2F, szfRow.Height);

                // Print the Header labels
                for (int cc = 0; cc < xTbl.Columns.Count; cc++)
                {
                    if (!Convert.ToBoolean(xTbl.Columns[cc].ExtendedProperties[EP_VISIBLE]))
                    {
                        // ignore invisible columns
                        continue;
                    }

                    string sText = xTbl.Columns[cc].Caption;
                    SizeF szfHeader = g.MeasureString(sText, fontHeader, Convert.ToInt32((Convert.ToSingle(xTbl.Columns[cc].ExtendedProperties[EP_WIDTH]) * fScale) - 4.0f));
                    RectangleF rectfCell;
                    if (xTbl.Columns[cc].ExtendedProperties[EP_ALIGN].ToString() == "R")
                    {
                        // Right Align
                        rectfCell = new RectangleF(fPageX + ((fCellRight + Convert.ToSingle(xTbl.Columns[cc].ExtendedProperties[EP_WIDTH])) * fScale) - szfHeader.Width - 4.0f, fPageY + 2.0f,
                                                   (Convert.ToSingle(xTbl.Columns[cc].ExtendedProperties[EP_WIDTH]) * fScale) - 4.0f, szfMaxCell.Height);
                    }
                    else if (xTbl.Columns[cc].ExtendedProperties[EP_ALIGN].ToString() == "C")
                    {
                        // Center Align
                        rectfCell = new RectangleF(fPageX + (fCellRight * fScale) + (Convert.ToSingle(xTbl.Columns[cc].ExtendedProperties[EP_WIDTH]) * fScale / 2.0f) - (szfHeader.Width / 2.0f),
                                                   fPageY + 2.0f, szfHeader.Width, szfMaxCell.Height);
                    }
                    else
                    {
                        // Left Align
                        rectfCell = new RectangleF(fPageX + (fCellRight * fScale) + 4.0f, fPageY + 2.0f,
                                                   (Convert.ToSingle(xTbl.Columns[cc].ExtendedProperties[EP_WIDTH]) * fScale) - 4.0f, szfMaxCell.Height);
                    }
                    g.DrawString(sText, fontHeader, brushFont, rectfCell);

                    fCellRight += Convert.ToSingle(xTbl.Columns[cc].ExtendedProperties[EP_WIDTH]);  // Unscaled Right side of completed Cell
                    if (cc < xTbl.Columns.Count - 1)
                    {
                        // Print the Vertical Grid Line.  Don't print the last vertical line
                        g.DrawLine(penGrid, fPageX + (fCellRight * fScale), fPageY, fPageX + (fCellRight * fScale), fCellBottom);
                    }
                }

                // Print the Border Lines
                g.DrawLine(penGrid, fPageX, fPageY, fPageX, fCellBottom);  // Left
                g.DrawLine(penGrid, fPageX + szfRow.Width, fPageY, fPageX + szfRow.Width, fCellBottom);  // Right
                g.DrawLine(penGrid, fPageX, fPageY, fPageX + szfRow.Width, fPageY);  // Top
                g.DrawLine(penGrid, fPageX, fCellBottom, fPageX + szfRow.Width, fCellBottom);  // Botom
            }
            catch (Exception ex)
            {
                //Log.Err(ex, THIS_NAME, "Print_Table_Header", Log.LogDevices.CON_LOG_DLG);
            }
            return szfRow;
        }


        public static SizeF Print_TableRow(Graphics g, float fScale, float fPageX, float fPageY, float fRowHeight,
                                            Font fontRow, Brush brushFont, Brush brushFill, Pen penGrid,
                                            DataView xDV, Int32 iRowCursor)
        {
            SizeF szfGridRow = new SizeF(0.0f, 0.0f);
            string sMaxFieldName = "";

            // Measure the Maximum Height of the Row Cells' Text and Calculate the Y coordinate of the cell bottom
            SizeF szfMaxCell = Data_MaxCellSizeF(g, ref szfGridRow, fScale, fontRow, xDV, iRowCursor, ref sMaxFieldName);
            if (fRowHeight > szfMaxCell.Height)
            {
                szfMaxCell.Height = fRowHeight;
                szfGridRow.Height = fRowHeight;
            }
            else
            {
                fRowHeight = szfMaxCell.Height;
            }

            float fCellRight = 0.0f;
            float fCellBottom = fPageY + fRowHeight;
            // Print the Table shading
            g.FillRectangle(brushFill, fPageX + 0.2F, fPageY, szfGridRow.Width - 0.2F, fRowHeight);

            for (int cc = 0; cc < xDV.Table.Columns.Count; cc++)
            {
                if (!Convert.ToBoolean(xDV.Table.Columns[cc].ExtendedProperties[EP_VISIBLE]))
                {
                    // skip invisible columns
                    continue;
                }

                Font fontCol = (Font)fontRow.Clone();
                if (xDV.Table.Columns[cc].ExtendedProperties[EP_FONT_STYLE].ToString() == "B")
                {
                    fontCol = new Font(fontRow, FontStyle.Bold);
                }
                else if (xDV.Table.Columns[cc].ExtendedProperties[EP_FONT_STYLE].ToString() == "R")
                {
                    fontCol = new Font(fontRow, FontStyle.Regular);
                }
                else if (xDV.Table.Columns[cc].ExtendedProperties[EP_FONT_STYLE].ToString() == "I")
                {
                    fontCol = new Font(fontRow, FontStyle.Italic);
                }
                else if (xDV.Table.Columns[cc].ExtendedProperties[EP_FONT_STYLE].ToString() == "S")
                {
                    fontCol = new Font(fontRow, FontStyle.Strikeout);
                }
                else if (xDV.Table.Columns[cc].ExtendedProperties[EP_FONT_STYLE].ToString() == "U")
                {
                    fontCol = new Font(fontRow, FontStyle.Underline);
                }
                else
                {
                    fontCol = new Font(fontRow, FontStyle.Regular);
                }

                string sText = xDV[iRowCursor][cc].ToString();
                SizeF szfText = new SizeF(0.0f, 0.0f); // g.MeasureString(sText, fontCol, Convert.ToInt32((Convert.ToSingle(xDV.Table.Columns[cc].ExtendedProperties[EP_WIDTH]) * fScale) - 4.0f));
                RectangleF rectfCell;
                float epWidth = Convert.ToSingle(xDV.Table.Columns[cc].ExtendedProperties[EP_WIDTH]);
                float epRightMargin = Convert.ToSingle(xDV.Table.Columns[cc].ExtendedProperties[EP_MARGIN_RIGHT]);
                float epLeftMargin = Convert.ToSingle(xDV.Table.Columns[cc].ExtendedProperties[EP_MARGIN_LEFT]);
                float epTopMargin = Convert.ToSingle(xDV.Table.Columns[cc].ExtendedProperties[EP_MARGIN_TOP]);
                float epBottomMargin = Convert.ToSingle(xDV.Table.Columns[cc].ExtendedProperties[EP_MARGIN_BOTTOM]);

                rectfCell = new RectangleF(fPageX + (fCellRight * fScale),
                                           fPageY + epTopMargin,
                                           (epWidth * fScale) - epLeftMargin - epRightMargin,
                                           fRowHeight - epTopMargin - epBottomMargin);

                if (xDV.Table.Columns[cc].ExtendedProperties[EP_ALIGN].ToString().IndexOf("R") == 0)
                {
                    // Right Align
                    szfText = DrawText_AlignRight(g, sText, fontCol, brushFont, 
                                                  new SizeF(rectfCell.Width, rectfCell.Height), rectfCell.X, rectfCell.Y);
                }
                else if (xDV.Table.Columns[cc].ExtendedProperties[EP_ALIGN].ToString().IndexOf("C") == 0)
                {
                    // Center Align
                    szfText = DrawText_AlignCenter(g, sText.Trim(), fontCol, brushFont, new SizeF(rectfCell.Width, rectfCell.Height), rectfCell.X, rectfCell.Y);
                }
                else if (xDV.Table.Columns[cc].ExtendedProperties[EP_ALIGN].ToString().IndexOf("LM") == 0)
                {
                    // Left Middle Align
                    DrawText_AlignLeft(g, sText.Trim(), fontCol, brushFont, new SizeF(rectfCell.Width, rectfCell.Height), rectfCell.X, rectfCell.Y);
                }
                else
                {
                    // Left Align
                    DrawText_AlignLeft(g, sText.Trim(), fontCol, brushFont, new SizeF(rectfCell.Width, rectfCell.Height), rectfCell.X, rectfCell.Y);
                }

                fCellRight += Convert.ToSingle(xDV.Table.Columns[cc].ExtendedProperties[EP_WIDTH]);
                if (penGrid.Color != Color.Transparent)
                {

                    if (cc < xDV.Table.Columns.Count - 1)
                    {
                        // Print the Vertical Grid Line.  Don't print the last vertical line
                        g.DrawLine(penGrid, fPageX + (fCellRight * fScale), fPageY, fPageX + (fCellRight * fScale), fCellBottom);
                    }
                    fontCol.Dispose();

                    // Print the Rows Bottom Line
                    g.DrawLine(penGrid, fPageX, fPageY, fPageX, fCellBottom);
                    g.DrawLine(penGrid, fPageX + szfGridRow.Width, fPageY, fPageX + szfGridRow.Width, fCellBottom);
                    g.DrawLine(penGrid, fPageX, fPageY, fPageX + szfGridRow.Width, fPageY);
                    g.DrawLine(penGrid, fPageX, fCellBottom, fPageX + szfGridRow.Width, fCellBottom);
                }
            }
            return szfGridRow;
        }



        public static Image Report_Logo = null;

        public static void Print_PageHeader(PrintPageEventArgs ppea, string[] sTitle, Font[] fontTitle,
                                            float fOriginLeft, float fOriginTop, float fHeadHeight)
        {
            try
            {
                if (Report_Logo == null)
                {
                    if (System.IO.File.Exists(Application.StartupPath + @"\Report_Logo.jpg"))
                    {
                        Report_Logo = Bitmap.FromFile(Application.StartupPath + @"\Report_Logo.jpg");
                    }
                }
            }
            catch (Exception ex)
            {
                //Log.Err(ex, THIS_NAME, "Load Report Logo", Log.LogDevices.CON_LOG_DLG);
            }

            Pen penHeader1 = new Pen(Color.DarkGray, 0);
            try
            {
                //// Diagnostic
                // e.Graphics.DrawRectangle(penHeader1, g_OriginLeft, g_OriginTop, e.MarginBounds.Width, e.MarginBounds.Height);
                ////
                // Print the Page Header, Company Logo, and Report Title
                if (Report_Logo != null)
                {
                    ppea.Graphics.DrawImage(Report_Logo, fOriginLeft + 2.0F, fOriginTop + 4.0F, fHeadHeight - 6.0f, fHeadHeight - 6.0f);
                    ppea.Graphics.DrawRectangle(penHeader1, fOriginLeft, fOriginTop, fHeadHeight - 4.0f, fHeadHeight);
                }
                ppea.Graphics.DrawRectangle(penHeader1, fOriginLeft, fOriginTop, ppea.MarginBounds.Width, fHeadHeight);

                float yPos = fOriginTop + 4.0F;
                for (int tt = 0; tt < sTitle.Length; tt++)
                {
                    SizeF szfTitle = ppea.Graphics.MeasureString(sTitle[tt], fontTitle[tt]);
                    ppea.Graphics.DrawString(sTitle[tt], fontTitle[tt], Brushes.Black, fOriginLeft + (ppea.MarginBounds.Width / 2.0F) - (szfTitle.Width / 2.0F), yPos);
                    yPos += szfTitle.Height;
                }
            }
            catch (Exception ex)
            {
                //Log.Err(ex, THIS_NAME, "Print_PageHeader", Log.LogDevices.CON_LOG_DLG);
            }
            penHeader1.Dispose(); 
        }


        #region  ////  Report Text Routines ////

        public static SizeF DrawText_AlignLeft(Graphics g, string sText, Font fontText, Brush brushColor, SizeF szfArea, float xPos, float yPos)
        {
            SizeF szfText = new SizeF(0.0f, 0.0f);
            StringFormat _sfmt = new StringFormat();
            _sfmt.Alignment = StringAlignment.Near;
            szfText = g.MeasureString(sText, fontText, szfArea, _sfmt);
            g.DrawString(sText, fontText, brushColor, new RectangleF(xPos, yPos, szfArea.Width, szfArea.Height), _sfmt);
            return szfText;
        }


        public static SizeF DrawTextBox_AlignLeft(Graphics g, string sText, Font fontText, Brush brushColor, SizeF szfArea, float xPos, float yPos,
                                                  float fBorderWidth, Color cBorderColor, Color cFillColor, float fLeftMargin, float fTopMargin)
        {
            SizeF szfText = new SizeF(0.0f, 0.0f);
            StringFormat _sfmt = new StringFormat();
            _sfmt.Alignment = StringAlignment.Near;
            szfText = g.MeasureString(sText, fontText, szfArea, _sfmt);
            RectangleF rect = new RectangleF(xPos, yPos, szfArea.Width, szfArea.Height);
            if (cFillColor != Color.Transparent)
            {
                g.FillRectangle(new SolidBrush(cFillColor), rect);
            }
            if (fBorderWidth > 0.0f)
            {
                g.DrawRectangle(new Pen(cBorderColor, fBorderWidth), rect.X, rect.Y, rect.Width, rect.Height);
            }
            g.DrawString(sText, fontText, brushColor, new RectangleF(xPos + fLeftMargin, yPos + fTopMargin, szfArea.Width, szfArea.Height), _sfmt);
            return szfText;
        }


        public static SizeF DrawText_AlignRight(Graphics g, string sText, Font fontText, Brush brushColor, SizeF szfArea, float xPos, float yPos)
        {
            SizeF szfText = new SizeF(0.0f, 0.0f);
            StringFormat sF = new StringFormat();
            sF.Alignment = StringAlignment.Far;
            szfText = g.MeasureString(sText, fontText, szfArea, sF);

            g.DrawString(sText, fontText, brushColor, new RectangleF(xPos, yPos, szfArea.Width, szfArea.Height), sF);
            sF.Dispose();
            return szfText;
        }


        public static SizeF DrawTextBox_AlignRight(Graphics g, string sText, Font fontText, Brush brushColor, SizeF szfArea, float xPos, float yPos,
                                                   float fBorderWidth, Color cBorderColor, Color cFillColor, float fRightMargin, float fTopMargin)
        {
            SizeF szfText = new SizeF(0.0f, 0.0f);
            StringFormat sF = new StringFormat();
            sF.Alignment = StringAlignment.Far;
            szfText = g.MeasureString(sText, fontText, szfArea, sF);
            RectangleF rect = new RectangleF(xPos, yPos, szfArea.Width, szfArea.Height);
            if (cFillColor != Color.Transparent)
            {
                g.FillRectangle(new SolidBrush(cFillColor), rect);
            }
            if (fBorderWidth > 0.0f)
            {
                g.DrawRectangle(new Pen(cBorderColor, fBorderWidth), rect.X, rect.Y, rect.Width, rect.Height);
            }
            g.DrawString(sText, fontText, brushColor, new RectangleF(xPos - fRightMargin, yPos + fTopMargin, szfArea.Width, szfArea.Height), sF);
            sF.Dispose();
            return szfText;
        }


        public static SizeF DrawText_AlignCenter(Graphics g, string sText, Font fontText, Brush brushColor, SizeF szfArea, float xPos, float yPos)
        {
            SizeF szfText = new SizeF(0.0f, 0.0f);
            StringFormat sF = new StringFormat();
            sF.Alignment = StringAlignment.Center;
            szfText = g.MeasureString(sText, fontText, szfArea, sF);
            g.DrawString(sText, fontText, brushColor, new RectangleF(xPos, yPos + ((szfArea.Height / 2.0f) - (szfText.Height / 2)), szfArea.Width, szfArea.Height), sF);
            sF.Dispose();
            return szfText;
        }


        public static SizeF DrawTextBox_AlignCenter(Graphics g, string sText, Font fontText, Brush brushColor, SizeF szfArea,
                                                    float xPos, float yPos, float fBorderWidth, Color cBorderColor, Color cFillColor)
        {
            SizeF szfText = new SizeF(0.0f, 0.0f);
            StringFormat sF = new StringFormat();
            sF.Alignment = StringAlignment.Center;
            szfText = g.MeasureString(sText, fontText, szfArea, sF);
            RectangleF rect = new RectangleF(xPos, yPos, szfArea.Width, szfArea.Height);
            if (cFillColor != Color.Transparent)
            {
                g.FillRectangle(new SolidBrush(cFillColor), rect);
            }
            if (fBorderWidth > 0.0f)
            {
                g.DrawRectangle(new Pen(cBorderColor, fBorderWidth), rect.X, rect.Y, rect.Width, rect.Height);
            }
            g.DrawString(sText, fontText, brushColor, new RectangleF(xPos, yPos + ((szfArea.Height / 2.0f) - (szfText.Height / 2)), szfArea.Width, szfArea.Height), sF);
            sF.Dispose();
            return szfText;
        }

        #endregion

    }
}
