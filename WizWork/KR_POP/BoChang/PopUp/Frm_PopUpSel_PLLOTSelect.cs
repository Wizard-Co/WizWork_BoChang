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
    public partial class Frm_PopUpSel_PLLOTSelect : Form
    {
        DataSet ds = null;

        string MachineNo = ""; //호기

        List<string> PLLOTLIST = new List<string>(); //작업지시 리스트

        public string PLLOT = ""; //선택한 작업지시

        /// <summary>
        /// 폼간 데이터전달을 위한 소스
        /// </summary>
        /// <param name="text"></param> 
        ///  

        public delegate void TextEventHandler();// string을 반환값으로 갖는 대리자를 선언합니다.
        public event TextEventHandler WriteTextEvent;          // 대리자 타입의 이벤트 처리기를 설정합니다.


        //WizINs용
        WizWorkLib Lib = new WizWorkLib();
        List<CB_IDNAME> list_cbx = new List<CB_IDNAME>();
        public Frm_PopUpSel_PLLOTSelect()
        {
            InitializeComponent();
        }

        public Frm_PopUpSel_PLLOTSelect(List<string> PLLOTLIST, string MachineNo)
        {
            InitializeComponent();
            this.PLLOTLIST = PLLOTLIST;
            this.MachineNo = MachineNo;
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

            grdData.Columns[i].Name = "RowSeq";
            grdData.Columns[i].HeaderText = "";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "PLLOTID";
            grdData.Columns[i].HeaderText = "작업지시";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "BuyerArticleNo";
            grdData.Columns[i].HeaderText = "품번";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "Person";
            grdData.Columns[i].HeaderText = "작업자";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
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

        private void FillGrid()
        {
            for (int i = 0; i < PLLOTLIST.Count(); i++) 
            {
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Add("LikeLot", PLLOTLIST[i]);
                sqlParameter.Add("MachineNo", MachineNo);
                DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_WizWork_sWorkingEndTarget_Select", sqlParameter, false);

                if (dt.Rows.Count > 0)
                {

                    grdData.Rows.Add((i + 1).ToString(),
                                     dt.Rows[0]["LabelID"].ToString(),
                                     dt.Rows[0]["BuyerArticleNo"].ToString(),
                                     dt.Rows[0]["Name"].ToString()
                                    );
                }
                else
                {
                    WizCommon.Popup.MyMessageBox.ShowBox("작업 진행중인 공정 LotID가 아닙니다. {" + PLLOTLIST[i] + "} 관리자에게 문의해 주세요!", "[Start 데이터 서치오류]", 2, 1);
                    return;                 
                }

            }
        }

        private void Frm_PopUpSel_Load(object sender, EventArgs e)
        {
            SetScreen();
            InitGrid();
            FillGrid();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (grdData.Rows.Count == 0 || grdData.CurrentRow is null)
            {
                WizCommon.Popup.MyMessageBox.ShowBox("선택된 작업지시가 없습니다. 작업지시를 선택해주세요.", "[확인]", 0, 1);
                return;
            }

            if(grdData.SelectedRows.Count > 0)
            {
                PLLOT = grdData.SelectedRows[0].Cells["PLLOTID"].Value.ToString();
                DialogResult = DialogResult.OK;
                this.Dispose();
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

    }
}