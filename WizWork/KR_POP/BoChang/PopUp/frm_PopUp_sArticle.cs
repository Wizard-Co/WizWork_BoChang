using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WizWork.Properties;
using WizWork.Tools;
using WizCommon;

namespace WizWork
{

    public partial class Frm_PopUp_sArticle : Form
    {
        DataTable dt = null;
        WizWorkLib Lib = Frm_tprc_Main.Lib;
        string[] Message = new string[2];
        public string Article = "";
        public string BuyerArticleNo = "";
        public string ArticleID = "";

        public string m_sFormName = "";
        bool CheckOk = false;

        public Frm_PopUp_sArticle()
        {
            InitializeComponent();

        }

        private void Frm_PopUp_sArticle_Load(object sender, EventArgs e)
        {
            try
            {
                CheckOk = true;
                SetScreen();
                InitGrid();
                //InitGrid(); // Grid 초기화
                //SetFormClearData();
                btnClose.Text = "취소";
                btnSave.Enabled = true;
                FillGrid();
            }
            catch (Exception ex)
            {
                btnClose.Text = "닫기";
            }
        }

        #region TableLayoutPanel 하위 컨트롤들의 DockStyle.Fill 세팅
        private void SetScreen()
        {
            tlpForm.Dock = DockStyle.Fill;
            tlpForm.Margin = new Padding(0, 0, 0, 0);
            foreach (Control control in tlpForm.Controls)//con = tlp 상위에서 2번째
            {
                control.Dock = DockStyle.Fill;
                control.Margin = new Padding(0, 0, 0, 0);
                foreach (Control contro in control.Controls)//tlp 상위에서 3번째
                {
                    contro.Dock = DockStyle.Fill;
                    contro.Margin = new Padding(0, 0, 0, 0);
                    foreach (Control contr in contro.Controls)
                    {
                        contr.Dock = DockStyle.Fill;
                        contr.Margin = new Padding(0, 0, 0, 0);
                        foreach (Control cont in contr.Controls)
                        {
                            cont.Dock = DockStyle.Fill;
                            cont.Margin = new Padding(0, 0, 0, 0);
                            foreach (Control con in cont.Controls)
                            {
                                con.Dock = DockStyle.Fill;
                                con.Margin = new Padding(0, 0, 0, 0);
                                foreach (Control co in con.Controls)
                                {
                                    co.Dock = DockStyle.Fill;
                                    co.Margin = new Padding(0, 0, 0, 0);
                                }
                            }
                        }
                    }
                }
            }
        }
        #endregion

        private void InitGrid()
        {
            dgvArticleList.Columns.Clear();
            dgvArticleList.ColumnCount = 5;

            int i = 0;

            DataGridViewCheckBoxColumn chkCol = new DataGridViewCheckBoxColumn();
            {
                chkCol.HeaderText = "선택";
                chkCol.Name = "Check";
                chkCol.Width = 110;
                //chkCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                chkCol.FlatStyle = FlatStyle.Standard;
                chkCol.ThreeState = true;
                chkCol.CellTemplate = new DataGridViewCheckBoxCell();
                chkCol.CellTemplate.Style.BackColor = Color.Beige;
                chkCol.Visible = CheckOk;
            }
            dgvArticleList.Columns.Insert(0, chkCol);

            dgvArticleList.Columns[++i].Name = "ArticleID";
            dgvArticleList.Columns[i].HeaderText = "ArticleID";
            dgvArticleList.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvArticleList.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvArticleList.Columns[i].ReadOnly = true;
            dgvArticleList.Columns[i].Visible = false;

            dgvArticleList.Columns[++i].Name = "Article";
            dgvArticleList.Columns[i].HeaderText = "품명";
            dgvArticleList.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvArticleList.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvArticleList.Columns[i].ReadOnly = true;
            dgvArticleList.Columns[i].Visible = true;

            dgvArticleList.Columns[++i].Name = "BuyerArticleNo";
            dgvArticleList.Columns[i].HeaderText = "품번";
            dgvArticleList.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvArticleList.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvArticleList.Columns[i].ReadOnly = true;
            dgvArticleList.Columns[i].Visible = true;

            dgvArticleList.Columns[++i].Name = "LotNO";
            dgvArticleList.Columns[i].HeaderText = "금형LotNO";
            dgvArticleList.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvArticleList.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvArticleList.Columns[i].ReadOnly = true;
            dgvArticleList.Columns[i].Visible = false;

            dgvArticleList.Columns[++i].Name = "MoldID";
            dgvArticleList.Columns[i].HeaderText = "MoldID";
            dgvArticleList.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvArticleList.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvArticleList.Columns[i].ReadOnly = true;
            dgvArticleList.Columns[i].Visible = false;



            dgvArticleList.Font = new Font("맑은 고딕", 15, FontStyle.Bold);
            dgvArticleList.RowTemplate.Height = 30;
            dgvArticleList.ColumnHeadersHeight = 35;
            dgvArticleList.ScrollBars = ScrollBars.Both;
            dgvArticleList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvArticleList.MultiSelect = false;
            dgvArticleList.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvArticleList.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(234, 234, 234);

            foreach (DataGridViewColumn col in dgvArticleList.Columns)
            {
                col.DataPropertyName = col.Name;
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            return;
        }

        private void FillGrid()
        {
            dgvArticleList.Rows.Clear();
            try
            {
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();

                dt = DataStore.Instance.ProcedureToDataTable("xp_WizWork_sMoldArticleID", null, false);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {

                        dgvArticleList.Rows.Add(false,
                                              Lib.CheckNull(dr["ArticleID"].ToString()),        //ArticleiD                          
                                              Lib.CheckNull(dr["Article"].ToString()),          //품명
                                              Lib.CheckNull(dr["BuyerArticleNo"].ToString()),   //품번
                                              Lib.CheckNull(dr["MoldNo"].ToString()),           //금형LotNO
                                              Lib.CheckNull(dr["MoldID"].ToString())            //금형명
                                          );
                    }
                    if (dgvArticleList.Rows.Count > 0)
                    {
                        dgvArticleList.Rows[0].Selected = true;
                    }
                    else
                    {
                        Message[0] = "[검색결과 없음]";
                        Message[1] = "금형이 등록된 품명이 없습니다.";
                        WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 2, 1);
                        //btnUseMold.Enabled = false;
                    }
                }
                else
                {
                    Message[0] = "[검색결과 없음]";
                    Message[1] = "금형이 등록된 품명이 없습니다.";
                    WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 2, 1);
                    //btnUseMold.Enabled = false;
                }
            }
            catch (Exception excpt)
            {
                Message[0] = "[오류]";
                Message[1] = string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message);
                WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 2, 1);
                btnSave.Enabled = false;
            }
            finally
            {
                DataStore.Instance.CloseConnection();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            for(int i = 0; i < dgvArticleList.Rows.Count; i++)
            {
                if (dgvArticleList.Rows[i].Cells["Check"].Value.ToString().ToUpper() == "TRUE")
                {
                    Article = dgvArticleList.Rows[i].Cells["Article"].Value.ToString();
                    BuyerArticleNo = dgvArticleList.Rows[i].Cells["BuyerArticleNo"].Value.ToString();
                    ArticleID = dgvArticleList.Rows[i].Cells["ArticleID"].Value.ToString();
                    DialogResult = DialogResult.OK;
                    this.Dispose();
                    this.Close();
                    return;
                }
            }

            Message[0] = "[품명, 품번 선택]";
            Message[1] = "선택된 품명 또는 선택된 품번이 없습니다.";
            WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 2, 1);

        }

        private void btnClose_Click(object sender, EventArgs e)
        {

            DialogResult = DialogResult.No;
            this.Dispose();
            this.Close();

        }

        private void dgvArticleList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow dr = dgvArticleList.SelectedRows[0];

            for (int i = 0; i < dgvArticleList.Rows.Count; i++)
            {
                dgvArticleList.Rows[i].Cells["Check"].Value = false;
            }

            if (dr.Cells[0].Value.ToString().ToUpper() == "false".ToUpper())
            {
                dr.Cells[0].Value = true;
            }
            else
            {
                dr.Cells[0].Value = false;
            }
        }
    }
}

