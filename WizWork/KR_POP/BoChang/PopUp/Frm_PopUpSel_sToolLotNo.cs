using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using WizCommon;

namespace WizWork
{
    public partial class Frm_PopUpSel_sToolLotNo : Form
    {
        string sMCID = string.Empty;
        public string m_sToolWorkQty = string.Empty;                     //2022-02-28
        public string m_sToolSetProdQty = string.Empty;                  //2022-02-28
        public string m_ToolLotID = string.Empty;
        public delegate void TextEventHandler();                // string을 반환값으로 갖는 대리자를 선언합니다.
        public event TextEventHandler WriteTextEvent;           // 대리자 타입의 이벤트 처리기를 설정합니다.
        //WizWorkLib Lib = new WizWorkLib();
        List<CB_IDNAME> list_cbx = new List<CB_IDNAME>();

        public Frm_PopUpSel_sToolLotNo()
        {
            InitializeComponent();
        }

        public Frm_PopUpSel_sToolLotNo(string strMCID)
        {
            InitializeComponent();
            this.sMCID = strMCID;
        }

        private void Frm_PopUpSel_sMachine_Load(object sender, EventArgs e)
        {
            this.Text = "톱날LotNo 선택";
            this.Size = new Size(457, 499);

            m_sToolSetProdQty = string.Empty;
            m_sToolWorkQty = string.Empty;
            m_ToolLotID = string.Empty;


            InitGrid();
            SetScreen();
            FillGrid();
        }
        private void SetScreen()
        {
            tlpMain.Dock = DockStyle.Fill;
            foreach (Control control in tlpMain.Controls)
            {
                control.Dock = DockStyle.Fill;
                control.Margin = new Padding(0, 0, 0, 0);
                foreach (Control contro in control.Controls)
                {
                    contro.Dock = DockStyle.Fill;
                    contro.Margin = new Padding(0, 0, 0, 0);
                    foreach (Control contr in contro.Controls)
                    {
                        contr.Dock = DockStyle.Fill;
                        contr.Margin = new Padding(0, 0, 0, 0);

                    }
                }
            }
        }


        //그리드 컬럼 셋팅
        private void InitGrid()
        {
            grdData.Columns.Clear(); //체크박스나 콤보박스 사용시 필요하다.
            grdData.ColumnCount = 4;

            int i = 0;

            grdData.Columns[i].Name = "MCPartName";
            grdData.Columns[i].HeaderText = "품명";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "ToolLotID";
            grdData.Columns[i].HeaderText = "ToolLotID";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "WorkQty";
            grdData.Columns[i].HeaderText = "누적사용량";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "SetProdQty";
            grdData.Columns[i].HeaderText = "설정수명";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Font = new Font("맑은 고딕", 15, FontStyle.Bold);
            grdData.RowsDefaultCellStyle.Font = new Font("맑은 고딕", 15, FontStyle.Bold);
            grdData.AlternatingRowsDefaultCellStyle.Font = new Font("맑은 고딕", 15, FontStyle.Bold);
            grdData.RowTemplate.Height = 30;
            grdData.ColumnHeadersHeight = 35;
            grdData.ScrollBars = ScrollBars.Both;
            grdData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grdData.MultiSelect = false;
            grdData.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            foreach (DataGridViewColumn col in grdData.Columns)
            {
                col.DataPropertyName = col.Name;
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        //그리드에 콤보박스 인덱스(부서)에 따른 사용자 세팅
        private void FillGrid()
        {
            try
            {
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Add("sMCID", sMCID);             
                DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_MCID_sMCPart", sqlParameter, false);


                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    grdData.Rows.Add(dr["MCPartName"].ToString(),
                                    dr["ToolLotID"].ToString(),
                                    dr["WorkQty"].ToString(),
                                    dr["SetProdQty"].ToString());
                }
            }
            catch (Exception e)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", e.Message), "[오류]", 0, 1);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            int iSelRow = 0;
            for (int i = 0; i < grdData.SelectedCells.Count; i++)
            {
                iSelRow = grdData.SelectedCells[i].RowIndex;
                if (iSelRow == 0) return;
                grdData[0, iSelRow - 1].Selected = true;
                break;
            }
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            int iSelRow = 0;
            for (int i = 0; i < grdData.SelectedCells.Count; i++)
            {
                iSelRow = grdData.SelectedCells[i].RowIndex;
                if (iSelRow == grdData.Rows.Count - 1) return;
                grdData[0, iSelRow + 1].Selected = true;
                break;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (grdData.Rows.Count == 0 || grdData.CurrentRow is null)
            {
                WizCommon.Popup.MyMessageBox.ShowBox("선택된 Tool이 없습니다.. Tool을 선택해주세요.", "[오류]", 0, 1);
                return;
            }
            m_sToolWorkQty = grdData.SelectedRows[0].Cells["WorkQty"].Value.ToString();
            m_sToolSetProdQty = grdData.SelectedRows[0].Cells["SetProdQty"].Value.ToString();
            m_ToolLotID = grdData.SelectedRows[0].Cells["ToolLotID"].Value.ToString();
            //전역변수에 사용자 저장
            //Frm_tprc_Main.g_tBase.MachineID = grdData.SelectedRows[0].Cells["MachineID"].Value.ToString();
            //Frm_tprc_Main.g_tBase.Machine = grdData.SelectedRows[0].Cells["Machine"].Value.ToString();
            //WriteTextEvent();
            DialogResult = DialogResult.OK;
            this.Dispose();
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

    }
}
