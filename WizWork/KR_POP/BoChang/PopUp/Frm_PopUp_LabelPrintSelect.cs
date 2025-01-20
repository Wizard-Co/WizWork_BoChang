using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WizCommon;

namespace WizWork
{
    public partial class Frm_PopUp_LabelPrintSelect : Form
    {
        DataSet ds = null;

        WizWorkLib Lib = new WizWorkLib();
        string[] Message = new string[2];

        public string Article = string.Empty;           //품명
        public string ArticleID = string.Empty;         //품명ID
        public string BuyerArticleNo = string.Empty;    //품번
        public string OutQtyPerBox = string.Empty;      //출하용박스수량

        /// <summary>
        /// 폼간 데이터전달을 위한 소스
        /// </summary>
        /// <param name="text"></param> 
        ///  

        public Frm_PopUp_LabelPrintSelect()
        {
            InitializeComponent();
        }

        private void SetScreen()
        {
            tlpMain.Dock = DockStyle.Fill;
            foreach (Control control in tlpMain.Controls)
            {
                control.Dock = DockStyle.Fill;
                control.Margin = new Padding(0, 0, 0, 0);
                //foreach (Control contro in control.Controls)
                //{
                //    contro.Dock = DockStyle.Fill;
                //    contro.Margin = new Padding(0, 0, 0, 0);
                //    foreach (Control contr in contro.Controls)
                //    {
                //        contr.Dock = DockStyle.Fill;
                //        contr.Margin = new Padding(0, 0, 0, 0);

                //    }
                //}
            }
        }

        private void Frm_PopUp_LabelPrintSelect_Load(object sender, EventArgs e)
        {
            SetScreen();
            InitGrid();
            FillGrid();
        }

        #region 조회 함수

        private void InitGrid()
        {
            dgvArticle.Columns.Clear();
            dgvArticle.ColumnCount = 4;
            // Set the Colums Hearder Names
            int i = 0;

            dgvArticle.Columns[i].Name = "BuyerArticleNo";
            dgvArticle.Columns[i].HeaderText = "품번";
            dgvArticle.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvArticle.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvArticle.Columns[i].ReadOnly = true;
            dgvArticle.Columns[i].Visible = true;

            dgvArticle.Columns[++i].Name = "Article";
            dgvArticle.Columns[i].HeaderText = "품명";
            dgvArticle.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvArticle.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvArticle.Columns[i].ReadOnly = true;
            dgvArticle.Columns[i].Visible = true;

            dgvArticle.Columns[++i].Name = "OutQtyPerBox";
            dgvArticle.Columns[i].HeaderText = "포장수량";
            dgvArticle.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dgvArticle.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvArticle.Columns[i].ReadOnly = true;
            dgvArticle.Columns[i].Visible = true;

            dgvArticle.Columns[++i].Name = "ArticleID";
            dgvArticle.Columns[i].HeaderText = "품명ID";
            dgvArticle.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dgvArticle.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvArticle.Columns[i].ReadOnly = true;
            dgvArticle.Columns[i].Visible = false;

            DataGridViewCheckBoxColumn curCol = new DataGridViewCheckBoxColumn();
            curCol.HeaderText = "선택";
            curCol.Name = "Check";
            curCol.Width = 50;
            curCol.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvArticle.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvArticle.Columns.Insert(0, curCol);

            dgvArticle.Font = new Font("맑은 고딕", 13);
            dgvArticle.RowTemplate.DefaultCellStyle.Font = new Font("맑은 고딕", 14, FontStyle.Bold);
            dgvArticle.RowTemplate.Height = 35;
            dgvArticle.ColumnHeadersHeight = 35;
            dgvArticle.ScrollBars = ScrollBars.Both;
            dgvArticle.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvArticle.MultiSelect = false;
            dgvArticle.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //grdData.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(234, 234, 234);
            dgvArticle.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvArticle.ReadOnly = true;

            //dgvArticle.EnableHeadersVisualStyles = false;  // 헤더 셀 스타일 적용 용도.

            foreach (DataGridViewColumn col in dgvArticle.Columns)
            {
                col.DataPropertyName = col.Name;
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            return;
        }

        private void FillGrid()
        {
            DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_prdWork_sArticleCardLabelPrint", null, false);

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    dgvArticle.Rows.Add(false,                              //선택
                                        dr["BuyerArticleNo"].ToString(),    //품번
                                        dr["Article"].ToString(),           //품명
                                        dr["OutQtyPerBox"].ToString(),      //출하용박스당 수량
                                        dr["ArticleID"].ToString()          //품명ID
                                         );
                }
            }

        }

        #endregion

        #region 선택, 닫기 이벤트

        private void btnOk_Click(object sender, EventArgs e)
        {
           
            for (int i = 0; i < dgvArticle.Rows.Count; i++)
            {
               if(dgvArticle.Rows[i].Cells["Check"].Value.ToString().ToUpper() == "TRUE")
               {
                    Article = dgvArticle.Rows[i].Cells["Article"].Value.ToString();
                    ArticleID = dgvArticle.Rows[i].Cells["ArticleID"].Value.ToString();
                    BuyerArticleNo = dgvArticle.Rows[i].Cells["BuyerArticleNo"].Value.ToString();
                    OutQtyPerBox = dgvArticle.Rows[i].Cells["OutQtyPerBox"].Value.ToString();

                    DialogResult = DialogResult.OK;
                    this.Close();
                    return;
               }
            }

            if (DialogResult != DialogResult.OK)
            {
                WizCommon.Popup.MyMessageBox.ShowBox("라벨발행할 품번을 선택해주세요.", "[선택 오류]", 0, 1);
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region 체크 표시 이벤트

        private void dgvArticle_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                for(int i = 0; i < dgvArticle.Rows.Count; i++)
                {
                    dgvArticle.Rows[i].Cells["Check"].Value = false;
                }

                bool flag = (bool)dgvArticle.Rows[e.RowIndex].Cells["Check"].Value;
                dgvArticle.Rows[e.RowIndex].Cells["Check"].Value = !flag;
            }
        }

        #endregion
    }
}