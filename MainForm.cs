/* Created by SharpDevelop.
 * User: AJ
 * Date: 11/2/2015
 * Time: 7:15 PM
 */
using System;
using System.Data;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace KoWordSearch
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			m_VocabTbl.Columns.Add(new DataColumn("KoWord", typeof(string)));
			m_VocabTbl.Columns.Add(new DataColumn("KoWordLen", typeof(int)));
			m_VocabTbl.Columns.Add(new DataColumn("EnWord", typeof(string)));
			m_VocabTbl.Columns.Add(new DataColumn("EnWordLen", typeof(int)));
			m_VocabTbl.Columns.Add(new DataColumn("Orient", typeof(bool)));
			m_VocabTbl.Columns.Add(new DataColumn("X", typeof(int)));
			m_VocabTbl.Columns["X"].DefaultValue = 0;
			m_VocabTbl.Columns.Add(new DataColumn("Y", typeof(int)));
			m_VocabTbl.Columns["Y"].DefaultValue = 0;
		}


		BindingSource m_MatrixBs = new BindingSource();
		DataTable m_MatrixTbl = new DataTable("MatrixTbl");
		
		BindingSource m_VocabBs = new BindingSource();
		DataTable m_VocabTbl = new DataTable("VocabTbl");
		
		void QuitMnuClick(object sender, EventArgs e)
		{
			this.Close();
		}
		
		
		void ColumnsNudValueChanged(object sender, EventArgs e)
		{
			ColumnsBuild();
		}
		
		
		void ColumnsBuild()
		{
			m_MatrixBs.DataSource = null;
			m_MatrixTbl.Columns.Clear();
			this.MatrixDgv.Columns.Clear();
			for (int cc = 0; cc < this.ColumnsNud.Value; cc++)
			{
				string colName = "X" + cc.ToString("00");
				m_MatrixTbl.Columns.Add(new DataColumn(colName, typeof(string)));
				this.MatrixDgv.Columns.Add(colName, colName);
				this.MatrixDgv.Columns[cc].DataPropertyName = colName;
				this.MatrixDgv.Columns[cc].Width = 60;
			}
			m_MatrixTbl.AcceptChanges();
			m_MatrixBs.DataSource = m_MatrixTbl.DefaultView;
			this.MatrixDgv.DataSource = m_MatrixBs;
		}
		
		
		void RowsBuild()
		{
			m_MatrixTbl.Rows.Clear();
			m_MatrixTbl.AcceptChanges();
			for (int rr = 0; rr < this.RowsNud.Value; rr++)
			{
				DataRow dr = m_MatrixTbl.NewRow();
				m_MatrixTbl.Rows.Add(dr);
			}
			m_MatrixTbl.AcceptChanges();			
		}

		
		void RowsNudValueChanged(object sender, EventArgs e)
		{
			RowsBuild();
		}
		
		
		void SaveMnuClick(object sender, EventArgs e)
		{
	
		}
		
		
		Random ran = new Random(0);
		
		string HangulRandom()
		{
			return ((char)ran.Next(0xAC00, 0xD7A4)).ToString();
		}
	
		
		void VocabLoad()
		{
			m_VocabTbl.Rows.Clear();
			
			string[] vocablines = File.ReadAllLines(Application.StartupPath + @"\Vocab.txt");			
			foreach (string vocab in vocablines)
			{
				if (vocab.Trim().Length == 0)
				{
					break;
				}
				
				string[] voWord = vocab.Split(",".ToCharArray());
				
				DataRow dr = m_VocabTbl.NewRow();
				dr["KoWord"] = voWord[0].Trim();
				dr["KoWordLen"] = voWord[0].Trim().Length;
				
				dr["EnWord"] = voWord[1].Trim();
				dr["EnWordLen"] = voWord[1].Trim().Length;
				dr["X"] = 0;
				dr["Y"] = 0;
				if (ran.Next(0, 2) == 0)
				{
					dr["Orient"] = true;
				}
				else
				{
					dr["Orient"] = false;
				}
				m_VocabTbl.Rows.Add(dr);
			}
			m_VocabTbl.AcceptChanges();
			m_VocabBs.DataSource = m_VocabTbl.DefaultView;
			this.VocabDgv.DataSource = m_VocabBs;
		}

		
		void VocabLayout()
		{
			ColumnsBuild();
			RowsBuild();			
			for (int ii = 0; ii < m_VocabTbl.DefaultView.Count; ii++)
			{
				m_VocabTbl.DefaultView[ii].BeginEdit();
				if (Convert.ToBoolean(m_VocabTbl.DefaultView[ii]["Orient"]) == true)
				{
					// Horizontal
					bool placed = false;
					while (!placed)
					{
						// select a random row
						int rowRan = ran.Next(0, Convert.ToInt32(this.RowsNud.Value));
						// select a random column that allows the word to fit.
						int colRan = ran.Next(0, Convert.ToInt32(Convert.ToInt32(this.ColumnsNud.Value) - Convert.ToInt32(m_VocabTbl.DefaultView[ii]["KoWordLen"])));
						// Test for collision
						for (int xx = 0; xx < Convert.ToInt32(m_VocabTbl.DefaultView[ii]["KoWordLen"]); xx++)
						{
							if ((m_MatrixTbl.DefaultView[rowRan][colRan + xx].ToString().Length == 0)
							    || (m_MatrixTbl.DefaultView[rowRan][colRan + xx].ToString() == m_VocabTbl.DefaultView[ii]["KoWord"].ToString()[xx].ToString()))
							{
								// cell is either empty or contains the same letter that we want to put there
								// no collision
								if (xx == (Convert.ToInt32(m_VocabTbl.DefaultView[ii]["KoWordLen"]) - 1))
								{
									placed = true;
								}
							}
							else
							{
								// we have a collision
								break;
							}
						}
						
						if (placed)
						{
							for (int xx = 0; xx < Convert.ToInt32(m_VocabTbl.DefaultView[ii]["KoWordLen"]); xx++)
							{
     							m_VocabTbl.DefaultView[ii]["X"] = colRan;
								m_VocabTbl.DefaultView[ii]["Y"] = rowRan;
								m_MatrixTbl.DefaultView[rowRan].BeginEdit();
								m_MatrixTbl.DefaultView[rowRan][colRan + xx] = m_VocabTbl.DefaultView[ii]["KoWord"].ToString()[xx];
								m_MatrixTbl.DefaultView[rowRan].EndEdit();
							}
							m_MatrixTbl.AcceptChanges();
						}
					}
				}
				else
				{
					// Vertical
					bool placed = false;
					while (!placed)
					{
						// select a random row that will allow the word to fit vertically
						int rowRan = ran.Next(0, Convert.ToInt32(this.RowsNud.Value) - Convert.ToInt32(m_VocabTbl.DefaultView[ii]["KoWordLen"]));
						// select a random column
						int colRan = ran.Next(0, Convert.ToInt32(this.ColumnsNud.Value));
						// Test for collision
						for (int yy = 0; yy < Convert.ToInt32(m_VocabTbl.DefaultView[ii]["KoWordLen"]); yy++)
						{
							if ((m_MatrixTbl.DefaultView[rowRan + yy][colRan].ToString().Length == 0)
							    || (m_MatrixTbl.DefaultView[rowRan + yy][colRan].ToString() == m_VocabTbl.DefaultView[ii]["KoWord"].ToString()[yy].ToString()))
							{
								// cell is either empty or contains the same letter that we want to put there
								// no collision
								if (yy == (Convert.ToInt32(m_VocabTbl.DefaultView[ii]["KoWordLen"]) - 1))
								{
									placed = true;
								}
							}
							else
							{
								// we have a collision
								break;
							}
						}
						
						if (placed)
						{
							for (int yy = 0; yy < Convert.ToInt32(m_VocabTbl.DefaultView[ii]["KoWordLen"]); yy++)
							{
								m_VocabTbl.DefaultView[ii]["X"] = colRan;
								m_VocabTbl.DefaultView[ii]["Y"] = rowRan;
								m_MatrixTbl.DefaultView[rowRan].BeginEdit();
								m_MatrixTbl.DefaultView[rowRan + yy][colRan] = m_VocabTbl.DefaultView[ii]["KoWord"].ToString()[yy];
								m_MatrixTbl.DefaultView[rowRan].EndEdit();
							}
							m_MatrixTbl.AcceptChanges();
						}
					}
				}
				m_VocabTbl.DefaultView[ii].EndEdit();
			}
		}
		
		
		void RunTBtnClick(object sender, EventArgs e)
		{
			//VocabLoad();
			//VocabLayout();
			
			foreach (DataRowView drv in m_MatrixTbl.DefaultView)
			{
				drv.BeginEdit();
				foreach (DataColumn dc in m_MatrixTbl.Columns)
				{
					if (drv[dc.ColumnName].ToString().Trim().Length == 0)
					{
						drv[dc.ColumnName] = HangulRandom();
					}
				}
				drv.EndEdit();
			}
			m_MatrixTbl.AcceptChanges();
		}
		
		
		void PreviewTBtnClick(object sender, EventArgs e)
		{
			VocabLoad();
			VocabLayout();
		}
		
		
		
		
		
	}
}
