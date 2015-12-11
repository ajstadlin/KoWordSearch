/// <summary>
/// Korean Word Search Application
/// 11/2/2015  Created by //AJ
/// </summary>
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
		#region  [ Members ]
		/// <summary>
		/// Matrix Data Table
		/// </summary>
		private BindingSource m_MatrixBs = new BindingSource();
		private DataTable m_MatrixTbl = new DataTable("MatrixTbl");
		
		/// <summary>
		/// Vocabulary Data Table
		/// </summary>		
		private BindingSource m_VocabBs = new BindingSource();
		private DataTable m_VocabTbl = new DataTable("VocabTbl");
		private string m_VocabFile = Application.StartupPath + @"\Vocabulary\Vocab-0001.txt";
		private const string VOC_KOWORD = "KoWord";
		private const string VOC_KOWORDLEN = "KoWordLen";
		private const string VOC_ENWORD = "EnWord";
		private const string VOC_ENWORDLEN = "EnWordLen";
		private const string VOC_ORIENT = "Orient";
		private const string VOC_X = "X";
		private const string VOC_Y = "Y";
        private const string VOC_ISPICKED = "IsPicked";
				
		private const string CRLF = "\x000d\x000a";
		private Random m_Ran = new Random(0);
		private int m_TriesMaximum = 4000;
			
		/// <summary>
		/// Configuration Parameters
		/// </summary>
		private string CONFIG_FILE = Application.StartupPath + @"\" + "config.ini";
		private const string CFG_VOCABULARY_FILE = "Vocabulary_File";
		private const string CFG_MATRIX_ROWS = "Matrix_Rows";
		private const string CFG_MATRIX_COLS = "Matrix_Cols";
		private const string CFG_TRIES_MAXIMUM = "Tries_Maximum";
        private const string CFG_LIST_LANGUAGE = "List_Language";
		#endregion


		#region [ Constructor and Configuration ]

		public MainForm()
		{
			// The InitializeComponent() call is required for Windows Forms designer support.
			InitializeComponent();

			m_VocabTbl.Columns.Add(new DataColumn(VOC_KOWORD, typeof(string)));
			m_VocabTbl.Columns.Add(new DataColumn(VOC_KOWORDLEN, typeof(int)));
			m_VocabTbl.Columns.Add(new DataColumn(VOC_ENWORD, typeof(string)));
			m_VocabTbl.Columns.Add(new DataColumn(VOC_ENWORDLEN, typeof(int)));
			m_VocabTbl.Columns.Add(new DataColumn(VOC_ORIENT, typeof(bool)));
			m_VocabTbl.Columns.Add(new DataColumn(VOC_X, typeof(int)));
			m_VocabTbl.Columns[VOC_X].DefaultValue = 0;
			m_VocabTbl.Columns.Add(new DataColumn(VOC_Y, typeof(int)));
			m_VocabTbl.Columns[VOC_Y].DefaultValue = 0;
            m_VocabTbl.Columns.Add(new DataColumn(VOC_ISPICKED, typeof(bool)));
            m_VocabTbl.Columns[VOC_ISPICKED].DefaultValue = false;
			ConfigLoad();
		}
		

		/// <summary>
		/// Saves the current configuration for the User
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SaveMnuClick(object sender, EventArgs e)
		{
			ConfigSave();
		}


        /// <summary>
        /// Saves current configuration when the User quits and closes the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ConfigSave();
		}
		
		
		/// <summary>
		/// Saves the configuration
		/// </summary>
		private void ConfigSave()
		{
            try
            {
                StringBuilder sb = new StringBuilder("[Vocabulary]" + CRLF);
                sb.Append(CFG_VOCABULARY_FILE + "=" + Application.StartupPath + @"\Vocabulary\Vocab-0001.txt" + CRLF);
                sb.Append("[Matrix]" + CRLF);
                sb.Append(CFG_MATRIX_COLS + "=" + this.MatrixColsNud.Value.ToString() + CRLF);
                sb.Append(CFG_MATRIX_ROWS + "=" + this.MatrixRowsNud.Value.ToString() + CRLF);
                sb.Append(CFG_TRIES_MAXIMUM + "=" + m_TriesMaximum.ToString() + CRLF);
                sb.Append(CFG_LIST_LANGUAGE + "=" + this.EnglishRadio.Checked.ToString() + CRLF);
                if (File.Exists(CONFIG_FILE))
                {
                    File.Delete(CONFIG_FILE);
                }
                File.WriteAllText(CONFIG_FILE, sb.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Save Configuration File" + CRLF + ex.Message, "Save Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
		}
		
		
		/// <summary>
		/// Loads the Configuration Settings
		/// </summary>
		private void ConfigLoad()
		{
			bool isIniLoaded = false;
			if (File.Exists(CONFIG_FILE))
			{
				try
				{
					string[] inis = File.ReadAllLines(CONFIG_FILE);
					foreach (string ini in inis)
					{
						string[] lrs = ini.Split("=".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
						if (lrs.Length > 1)
						{
							if (lrs[0].Trim() == CFG_VOCABULARY_FILE)
							{
								m_VocabFile = lrs[1].Trim();
								if (m_VocabFile.Length > 0)
								{
									VocabLoad();
								}
							}
							else if (lrs[0].Trim() == CFG_MATRIX_COLS)
							{
								try
								{
								  int cols = Convert.ToInt32(lrs[1].Trim());
								  this.MatrixColsNud.Value = cols;
								}
								catch 
								{
									// default per design mode
								}
							}
							else if (lrs[0].Trim() == CFG_MATRIX_ROWS)
							{
								try
								{
									int rows = Convert.ToInt32(lrs[1].Trim());
									this.MatrixRowsNud.Value = rows;
								}
								catch
								{
									// default per design mode
								}
							}
							else if (lrs[0].Trim() == CFG_TRIES_MAXIMUM)
							{
								try
								{
									int tries = Convert.ToInt32(lrs[1].Trim());
									m_TriesMaximum = tries;
								}
								catch
								{
									// default per design mode
								}
							}
                            else if (lrs[0].Trim() == CFG_LIST_LANGUAGE)
                            {
                                try
                                {
                                    bool test = Convert.ToBoolean(lrs[1].Trim());
                                    this.EnglishRadio.Checked = test;
                                }
                                catch
                                {
                                    // default per design mode
                                }
                            }
                        }
                    }
					isIniLoaded = true;
				}
				catch (Exception ex)
				{
					MessageBox.Show("Error Loading Configuration File" + CRLF + ex.Message, "Load Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			
			if (!isIniLoaded)
			{
				// Initialize a new Configuration file
				ConfigSave();
			}
		}
		
		
		/// <summary>
		/// Rebuilds the Matrix when the user changes the Columns count
		/// </summary>
		private void ColumnsNudValueChanged(object sender, EventArgs e)
		{
			ColumnsBuild();
		}
		
		
		/// <summary>
		/// Rebuilds the Matrix Columns
		/// </summary>
		private void ColumnsBuild()
		{
			m_MatrixBs.DataSource = null;
			m_MatrixTbl.Columns.Clear();
			this.MatrixDgv.Columns.Clear();
			for (int cc = 0; cc < this.MatrixColsNud.Value; cc++)
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
		

		/// <summary>
		/// Rebuilds the Matrix when the User changes the Rows count
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RowsNudValueChanged(object sender, EventArgs e)
		{
			RowsBuild();
		}

		
		/// <summary>
		/// Rebuilds the Matrix Rows
		/// </summary>
		private void RowsBuild()
		{
			m_MatrixTbl.Rows.Clear();
			m_MatrixTbl.AcceptChanges();
			for (int rr = 0; rr < this.MatrixRowsNud.Value; rr++)
			{
				DataRow dr = m_MatrixTbl.NewRow();
				m_MatrixTbl.Rows.Add(dr);
			}
			m_MatrixTbl.AcceptChanges();			
		}
		
		
		/// <summary>
		/// Selects a Random Hangul Sylable from the Unicode Characters
		/// </summary>
		/// <returns></returns>
		private string HangulRandom()
		{
			return ((char)m_Ran.Next(0xAC00, 0xD7A4)).ToString();
		}
	
		
		/// <summary>
		/// Loads the Vacabulary data from the selected vocabulary file
		/// </summary>
		private void VocabLoad()
		{
			m_VocabTbl.Rows.Clear();
			if (!File.Exists(m_VocabFile))
			{
				return;
			}
			
			string[] vocablines = File.ReadAllLines(m_VocabFile);			
			foreach (string vocab in vocablines)
			{
				if (vocab.Trim().Length == 0)
				{
					break;
				}
				
				string[] voWord = vocab.Split(",".ToCharArray());
				
				DataRow dr = m_VocabTbl.NewRow();
				dr[VOC_KOWORD] = voWord[0].Trim();
				dr[VOC_KOWORDLEN] = voWord[0].Trim().Length;
				
				dr[VOC_ENWORD] = voWord[1].Trim();
				dr[VOC_ENWORDLEN] = voWord[1].Trim().Length;
				dr[VOC_X] = 0;
				dr[VOC_Y] = 0;
				if (m_Ran.Next(0, 2) == 0)
				{
					dr[VOC_ORIENT] = true;
				}
				else
				{
					dr[VOC_ORIENT] = false;
				}
				m_VocabTbl.Rows.Add(dr);
			}
			m_VocabTbl.AcceptChanges();
			m_VocabBs.DataSource = m_VocabTbl.DefaultView;
			this.VocabDgv.DataSource = m_VocabBs;
            VocabListBox_SetLanguage();
            int iMatrixMinimum = MatrixMinimum();
			this.StatusLb.Text = "Vocabulary Loaded.  Minimum Matrix Size = " + iMatrixMinimum.ToString() + " x " + iMatrixMinimum.ToString();
		}

		
		/// <summary>
		/// Determines the minimum size for the matrix based on the vocabulary
		/// The minimum size is the longest vocabulary word's length
		/// </summary>
		/// <returns></returns>
		private int MatrixMinimum()
		{
			int iMaxWordLength = 0;
			foreach (DataRowView drv in m_VocabTbl.DefaultView)
			{
				if (Convert.ToInt32(drv[VOC_KOWORDLEN]) > iMaxWordLength)
				{
					iMaxWordLength = Convert.ToInt32(drv[VOC_KOWORDLEN]);
				}
			}
			return iMaxWordLength;
		}
		
		
		/// <summary>
		/// Places the vocabulary words into the Matrix
		/// </summary>
		private void VocabLayout()
		{
			ColumnsBuild();
			RowsBuild();	

     		int iTries = 0;
     		int iGaveUp = 0;
     		string sPrefix = "";
     		
			for (int ii = 0; ii < m_VocabTbl.DefaultView.Count; ii++)
			{
				sPrefix = m_VocabTbl.DefaultView[ii][VOC_KOWORD].ToString() + " -> ";
				m_VocabTbl.DefaultView[ii].BeginEdit();
				iTries = 0;
				if (Convert.ToBoolean(m_VocabTbl.DefaultView[ii][VOC_ORIENT]) == true)
				{
					// Horizontal
					bool placed = false;
					while (!placed)
					{
						iTries++;
						if (iTries > m_TriesMaximum)
						{
							iGaveUp++;
							break;
						}
						this.StatusLb.Text = sPrefix + iTries.ToString();
						this.statusStrip1.Refresh();
						
						// select a random row
						int rowRan = m_Ran.Next(0, Convert.ToInt32(this.MatrixRowsNud.Value));
						
						// select a random column that allows the word to fit.
						int colRan = m_Ran.Next(0, Convert.ToInt32(Convert.ToInt32(this.MatrixColsNud.Value) - Convert.ToInt32(m_VocabTbl.DefaultView[ii][VOC_KOWORDLEN])) + 1);
						
						// Test for collision
						for (int xx = 0; xx < Convert.ToInt32(m_VocabTbl.DefaultView[ii][VOC_KOWORDLEN]); xx++)
						{
							if ((m_MatrixTbl.DefaultView[rowRan][colRan + xx].ToString().Length == 0)
							    || (m_MatrixTbl.DefaultView[rowRan][colRan + xx].ToString() == m_VocabTbl.DefaultView[ii][VOC_KOWORD].ToString()[xx].ToString()))
							{
								// cell is either empty or contains the same letter that we want to put there
								// no collision
								if (xx == (Convert.ToInt32(m_VocabTbl.DefaultView[ii][VOC_KOWORDLEN]) - 1))
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
							for (int xx = 0; xx < Convert.ToInt32(m_VocabTbl.DefaultView[ii][VOC_KOWORDLEN]); xx++)
							{
     							m_VocabTbl.DefaultView[ii][VOC_X] = colRan;
								m_VocabTbl.DefaultView[ii][VOC_Y] = rowRan;
								m_MatrixTbl.DefaultView[rowRan].BeginEdit();
								m_MatrixTbl.DefaultView[rowRan][colRan + xx] = m_VocabTbl.DefaultView[ii][VOC_KOWORD].ToString()[xx];
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
						iTries++;
						if (iTries > m_TriesMaximum)
						{
							iGaveUp++;
							break;
						}
						this.StatusLb.Text = sPrefix + iTries.ToString();
						this.statusStrip1.Refresh();

						// select a random row that will allow the word to fit vertically
						int rowRan = m_Ran.Next(0, Convert.ToInt32(this.MatrixRowsNud.Value) - Convert.ToInt32(m_VocabTbl.DefaultView[ii][VOC_KOWORDLEN]));
						
						// select a random column
						int colRan = m_Ran.Next(0, Convert.ToInt32(this.MatrixColsNud.Value));
						
						// Test for collision
						for (int yy = 0; yy < Convert.ToInt32(m_VocabTbl.DefaultView[ii][VOC_KOWORDLEN]); yy++)
						{
							if ((m_MatrixTbl.DefaultView[rowRan + yy][colRan].ToString().Length == 0)
							    || (m_MatrixTbl.DefaultView[rowRan + yy][colRan].ToString() == m_VocabTbl.DefaultView[ii][VOC_KOWORD].ToString()[yy].ToString()))
							{
								// cell is either empty or contains the same letter that we want to put there
								// no collision
								if (yy == (Convert.ToInt32(m_VocabTbl.DefaultView[ii][VOC_KOWORDLEN]) - 1))
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
							for (int yy = 0; yy < Convert.ToInt32(m_VocabTbl.DefaultView[ii][VOC_KOWORDLEN]); yy++)
							{
								m_VocabTbl.DefaultView[ii][VOC_X] = colRan;
								m_VocabTbl.DefaultView[ii][VOC_Y] = rowRan;
								m_MatrixTbl.DefaultView[rowRan].BeginEdit();
								m_MatrixTbl.DefaultView[rowRan + yy][colRan] = m_VocabTbl.DefaultView[ii][VOC_KOWORD].ToString()[yy];
								m_MatrixTbl.DefaultView[rowRan].EndEdit();
							}
							m_MatrixTbl.AcceptChanges();
						}
					}
				}
				m_VocabTbl.DefaultView[ii].EndEdit();
			}
			if (iGaveUp > 0)
			{
				this.StatusLb.Text = sPrefix + "I gave up trying to place " + iGaveUp.ToString() + " words!";
			}
			else
			{
				this.StatusLb.Text = "Done = " + iTries.ToString() + " tries";
				this.statusStrip1.Refresh();			
			}
		}
		
		
		/// <summary>
		/// Generate the Final Word Search
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RunTBtnClick(object sender, EventArgs e)
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
		
		
		/// <summary>
		/// Loads the Vocabulary List and Places the words in the Matrix
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void PreviewTBtnClick(object sender, EventArgs e)
		{
			VocabLoad();
			VocabLayout();
		}
		
		
		/// <summary>
		/// Displays the File Open Dialog for the User to select a Vocabulary file.
		/// If selected, then loads the file into the vocabulary table
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LoadVocabFileMnuClick(object sender, EventArgs e)
		{
			OpenFileDialog ofDlg = new OpenFileDialog();
			ofDlg.DefaultExt = "txt";
			ofDlg.InitialDirectory = Path.GetDirectoryName(m_VocabFile);
			try
			{
				if (ofDlg.ShowDialog() == DialogResult.OK)
				{
					m_VocabFile = ofDlg.FileName;
					VocabLoad();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error Loading Vocabulary File" + CRLF + ex.Message, "Load Vocabulary Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			ofDlg.Dispose();			
		}

        #endregion


        /// <summary>
        /// Changes the Vocabulary List to the Language the User selects
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LanguageRadio_CheckedChanged(object sender, EventArgs e)
        {
            VocabListBox_SetLanguage();
        }


        /// <summary>
        /// Sets the Vocabulary List Language to the current selected Language
        /// </summary>
        private void VocabListBox_SetLanguage()
        {
            this.VocabListBox.DataBindings.Clear();
            ((ListBox)this.VocabListBox).DataSource = m_VocabTbl.DefaultView;
            ((ListBox)this.VocabListBox).ValueMember = VOC_ISPICKED;
            if (this.EnglishRadio.Checked)
            {
                ((ListBox)this.VocabListBox).DisplayMember = VOC_ENWORD;
            }
            else
            {
                ((ListBox)this.VocabListBox).DisplayMember = VOC_KOWORD;
            }
        }


    }
}
