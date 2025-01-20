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
    public partial class Frm_PopUpSel_sOutwareSub : Form
    {
        string OutwareID = "";
        WizWorkLib Lib = new WizWorkLib();

        public Frm_PopUpSel_sOutwareSub()
        {
            InitializeComponent();
        }

        public Frm_PopUpSel_sOutwareSub(string strOutwareID)
        {
            InitializeComponent();
            OutwareID = strOutwareID;
        }
        private void Frm_PopUpSel_sOutwareSub_Load(object sender, EventArgs e)
        {
            initGrid();
            FillGrid();
        }


        #region 닫기 이벤트

        private void cmdclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region 데이터그리드, 조회 함수
        //그리드 초기화
        private void initGrid()
        {
            dgvOutwareSub.Columns.Clear();
            dgvOutwareSub.ColumnCount = 3;

            int i = 0;

            // Set the Colums Hearder Names
            dgvOutwareSub.Columns[i].Name = "No";
            dgvOutwareSub.Columns[i].HeaderText = "";
            dgvOutwareSub.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvOutwareSub.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;

            dgvOutwareSub.Columns[++i].Name = "LOTID";
            dgvOutwareSub.Columns[i].HeaderText = "라벨ID";
            dgvOutwareSub.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvOutwareSub.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;

            dgvOutwareSub.Columns[++i].Name = "Qty";
            dgvOutwareSub.Columns[i].HeaderText = "수량";
            dgvOutwareSub.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvOutwareSub.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;

            dgvOutwareSub.Rows.Clear();
            dgvOutwareSub.RowTemplate.Height = 33;

            dgvOutwareSub.ReadOnly = true;
            dgvOutwareSub.Font = new Font("맑은 고딕", 10, FontStyle.Bold);
            dgvOutwareSub.RowTemplate.Height = 50;
            dgvOutwareSub.ColumnHeadersHeight = 35;
            dgvOutwareSub.ScrollBars = ScrollBars.Both;
            dgvOutwareSub.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvOutwareSub.MultiSelect = false;
            dgvOutwareSub.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvOutwareSub.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(234, 234, 234);
            dgvOutwareSub.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            foreach (DataGridViewColumn col in dgvOutwareSub.Columns)
            {
                col.DataPropertyName = col.Name;
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

        }

        //조회
        private void FillGrid()
        {
            Dictionary<string, object> sqlParameter = new Dictionary<string, object>();

            sqlParameter.Add("OutwareID", OutwareID); //OutwareID
            DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_WizWork_sOutwareSubLabelList", sqlParameter, false);

            int i = 1;

            foreach (DataRow dr in dt.Rows)
            {
                dgvOutwareSub.Rows.Add(i++,
                                    dr["LOTID"],
                                    string.Format("{0:#,###}", Lib.ConvertDouble(dr["Qty"].ToString()))//출고수량
                                    );
            }
        }

        #endregion


    }
}