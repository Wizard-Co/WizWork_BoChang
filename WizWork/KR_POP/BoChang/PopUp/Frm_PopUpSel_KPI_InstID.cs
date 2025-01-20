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
    public partial class Frm_PopUpSel_KPI_InstID : Form
    {

        WizWorkLib Lib = new WizWorkLib();

        public string InstID = "";
        public string InstDetSeq = "";
        public string Process = "";
        public string ProcessID = "";
        public string StartDate = "";
        public string StartTime = "";
        public string EndDate = "";
        public string EndTime = "";

        public string Person = "";
        public string PersonID = "";

        public string DayOrNight = "";
        public string DayOrNightID = "";

        public string JobGbn = "";
        public string JobGbnID = "";



        string GBN = "";

        public Frm_PopUpSel_KPI_InstID()
        {
            InitializeComponent();
        }

        public Frm_PopUpSel_KPI_InstID(string GBN)
        {
            InitializeComponent();
            this.GBN = GBN;
        }

        //그리드 컬럼 셋팅
        private void InitGrid()
        {
            dgvInst.Columns.Clear(); //체크박스나 콤보박스 사용시 필요하다.
            dgvInst.ColumnCount = 8;

            int i = 0;

            dgvInst.Columns[i].Name = "InstID";
            dgvInst.Columns[i].HeaderText = "지시번호";
            dgvInst.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvInst.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvInst.Columns[i].ReadOnly = true;
            dgvInst.Columns[i].Visible = true;

            dgvInst.Columns[++i].Name = "InstDetSeq";
            dgvInst.Columns[i].HeaderText = "지시번호순서";
            dgvInst.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvInst.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvInst.Columns[i].ReadOnly = true;
            dgvInst.Columns[i].Visible = true;

            dgvInst.Columns[++i].Name = "Process";
            dgvInst.Columns[i].HeaderText = "공정";
            dgvInst.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvInst.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvInst.Columns[i].ReadOnly = true;
            dgvInst.Columns[i].Visible = true;

            dgvInst.Columns[++i].Name = "ProcessID";
            dgvInst.Columns[i].HeaderText = "ProcessID";
            dgvInst.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvInst.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvInst.Columns[i].ReadOnly = true;
            dgvInst.Columns[i].Visible = true;

            dgvInst.Columns[++i].Name = "StartDate";
            dgvInst.Columns[i].HeaderText = "시작일자";
            dgvInst.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvInst.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvInst.Columns[i].ReadOnly = true;
            dgvInst.Columns[i].Visible = true;

            dgvInst.Columns[++i].Name = "StartTime";
            dgvInst.Columns[i].HeaderText = "시작시간";
            dgvInst.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvInst.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvInst.Columns[i].ReadOnly = true;
            dgvInst.Columns[i].Visible = true;

            dgvInst.Columns[++i].Name = "EndDate";
            dgvInst.Columns[i].HeaderText = "종료일자";
            dgvInst.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvInst.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvInst.Columns[i].ReadOnly = true;
            dgvInst.Columns[i].Visible = true;

            dgvInst.Columns[++i].Name = "EndTime";
            dgvInst.Columns[i].HeaderText = "종료일자";
            dgvInst.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvInst.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvInst.Columns[i].ReadOnly = true;
            dgvInst.Columns[i].Visible = true;

            dgvInst.Font = new Font("맑은 고딕", 10, FontStyle.Bold);
            dgvInst.RowsDefaultCellStyle.Font = new Font("맑은 고딕", 10, FontStyle.Bold);
            dgvInst.AlternatingRowsDefaultCellStyle.Font = new Font("맑은 고딕", 10, FontStyle.Bold);
            dgvInst.ScrollBars = ScrollBars.Both;
            dgvInst.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvInst.MultiSelect = false;
            dgvInst.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvInst.EnableHeadersVisualStyles = false;  // 헤더 셀 스타일 적용 용도.

            foreach (DataGridViewColumn col in dgvInst.Columns)
            {
                col.DataPropertyName = col.Name;
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void InitGridP()
        {
            dgvInst.Columns.Clear(); //체크박스나 콤보박스 사용시 필요하다.
            dgvInst.ColumnCount = 2;

            int i = 0;

            dgvInst.Columns[i].Name = "Person";
            dgvInst.Columns[i].HeaderText = "작업자";
            dgvInst.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvInst.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvInst.Columns[i].ReadOnly = true;
            dgvInst.Columns[i].Visible = true;

            dgvInst.Columns[++i].Name = "PersonID";
            dgvInst.Columns[i].HeaderText = "작업자 번호";
            dgvInst.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvInst.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvInst.Columns[i].ReadOnly = true;
            dgvInst.Columns[i].Visible = true;

            dgvInst.Font = new Font("맑은 고딕", 10, FontStyle.Bold);
            dgvInst.RowsDefaultCellStyle.Font = new Font("맑은 고딕", 10, FontStyle.Bold);
            dgvInst.AlternatingRowsDefaultCellStyle.Font = new Font("맑은 고딕", 10, FontStyle.Bold);
            dgvInst.ScrollBars = ScrollBars.Both;
            dgvInst.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvInst.MultiSelect = false;
            dgvInst.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvInst.EnableHeadersVisualStyles = false;  // 헤더 셀 스타일 적용 용도.

            foreach (DataGridViewColumn col in dgvInst.Columns)
            {
                col.DataPropertyName = col.Name;
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void InitGridDN()
        {
            dgvInst.Columns.Clear(); //체크박스나 콤보박스 사용시 필요하다.
            dgvInst.ColumnCount = 2;

            int i = 0;

            dgvInst.Columns[i].Name = "DayOrNightGbn";
            dgvInst.Columns[i].HeaderText = "주/야";
            dgvInst.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvInst.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvInst.Columns[i].ReadOnly = true;
            dgvInst.Columns[i].Visible = true;

            dgvInst.Columns[++i].Name = "DayOrNightGbnID";
            dgvInst.Columns[i].HeaderText = "주/야ID";
            dgvInst.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvInst.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvInst.Columns[i].ReadOnly = true;
            dgvInst.Columns[i].Visible = true;

            dgvInst.Font = new Font("맑은 고딕", 10, FontStyle.Bold);
            dgvInst.RowsDefaultCellStyle.Font = new Font("맑은 고딕", 10, FontStyle.Bold);
            dgvInst.AlternatingRowsDefaultCellStyle.Font = new Font("맑은 고딕", 10, FontStyle.Bold);
            dgvInst.ScrollBars = ScrollBars.Both;
            dgvInst.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvInst.MultiSelect = false;
            dgvInst.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvInst.EnableHeadersVisualStyles = false;  // 헤더 셀 스타일 적용 용도.

            foreach (DataGridViewColumn col in dgvInst.Columns)
            {
                col.DataPropertyName = col.Name;
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void InitGridJG()
        {
            dgvInst.Columns.Clear(); //체크박스나 콤보박스 사용시 필요하다.
            dgvInst.ColumnCount = 2;

            int i = 0;

            dgvInst.Columns[i].Name = "JobGbn";
            dgvInst.Columns[i].HeaderText = "작업구분";
            dgvInst.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvInst.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvInst.Columns[i].ReadOnly = true;
            dgvInst.Columns[i].Visible = true;

            dgvInst.Columns[++i].Name = "JobGbnID";
            dgvInst.Columns[i].HeaderText = "작업구분ID";
            dgvInst.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvInst.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvInst.Columns[i].ReadOnly = true;
            dgvInst.Columns[i].Visible = true;

            dgvInst.Font = new Font("맑은 고딕", 10, FontStyle.Bold);
            dgvInst.RowsDefaultCellStyle.Font = new Font("맑은 고딕", 10, FontStyle.Bold);
            dgvInst.AlternatingRowsDefaultCellStyle.Font = new Font("맑은 고딕", 10, FontStyle.Bold);
            dgvInst.ScrollBars = ScrollBars.Both;
            dgvInst.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvInst.MultiSelect = false;
            dgvInst.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvInst.EnableHeadersVisualStyles = false;  // 헤더 셀 스타일 적용 용도.

            foreach (DataGridViewColumn col in dgvInst.Columns)
            {
                col.DataPropertyName = col.Name;
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void FillGrid()
        {
            try
            {
                dgvInst.Rows.Clear();

                int intnchkInstDate = 0;

                string strStartDate = "";
                string strEndDate = "";

                // 검색일자 체크
                if (chkInsDate.Checked)
                {
                    intnchkInstDate = 1;
                    strStartDate = mtb_From.Text.Replace("-", "");
                    strEndDate = mtb_To.Text.Replace("-", "");
                }

                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();

                sqlParameter.Add("nchkInstDate", intnchkInstDate);
                sqlParameter.Add("FromDate", strStartDate);
                sqlParameter.Add("ToDate", strEndDate);
                
                DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_prdWork_sInst_KPI", sqlParameter, false);

                if (dt != null && dt.Rows.Count > 0)
                {

                    foreach (DataRow dr in dt.Rows)
                    {
                        dgvInst.Rows.Add( dr["InstID"].ToString(),          //작업지시번호
                                            dr["InstDetSeq"].ToString(),    //작업지시순서
                                            dr["Process"].ToString(),       //공정
                                            dr["ProcessID"].ToString(),     //공정ID
                                            dr["InstDate"].ToString(),      //시작일자
                                            "083000",                       //시작시간
                                            dr["InstDate"].ToString(),      //종료일자
                                            "180000"                        //종료시간
                        );                                          
                    }
                }
                else
                {                    
                    dgvInst.Rows.Clear();
                }

                DataStore.Instance.CloseConnection(); //2021-10-07 DB 커넥트 연결 해제
            }
            catch (Exception excpt)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message), "[오류]", 0, 1);
            }
        }

        //작업자
        private void FillGridP()
        {
            try
            {
                dgvInst.Rows.Clear();

                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();

                DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_prdWork_sPerson_KPI", sqlParameter, false);

                if (dt != null && dt.Rows.Count > 0)
                {

                    foreach (DataRow dr in dt.Rows)
                    {
                        dgvInst.Rows.Add(dr["Name"].ToString(),       //작업자
                                         dr["PersonID"].ToString()    //작업자ID
                        );
                    }
                }
                else
                {
                    dgvInst.Rows.Clear();
                }

                DataStore.Instance.CloseConnection(); //2021-10-07 DB 커넥트 연결 해제
            }
            catch (Exception excpt)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message), "[오류]", 0, 1);
            }
        }

        //주/야
        private void FillGridDN()
        {
            try
            {
                dgvInst.Rows.Clear();

              
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();

                sqlParameter.Add("CodeGbn", "DayOrNight");

                DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_prdWork_sDayorNight_KPI", sqlParameter, false);

                if (dt != null && dt.Rows.Count > 0)
                {

                    foreach (DataRow dr in dt.Rows)
                    {
                        dgvInst.Rows.Add(dr["Code_Name"].ToString(),    //주/야
                                         dr["Code_ID"].ToString()       //주/야ID
                        );
                    }
                }
                else
                {
                    dgvInst.Rows.Clear();
                }

                DataStore.Instance.CloseConnection(); //2021-10-07 DB 커넥트 연결 해제
            }
            catch (Exception excpt)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message), "[오류]", 0, 1);
            }
        }

        //작업구분
        private void FillGridJG()
        {
            try
            {
                dgvInst.Rows.Clear();

                
                dgvInst.Rows.Add("정상",       //작업구분
                                  "1"          //작업구분ID
                );

                dgvInst.Rows.Add("비가동",       //작업구분
                                  "2"            //작업구분ID
                );

            }
            catch (Exception excpt)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message), "[오류]", 0, 1);
            }
        }

        private void Frm_PopUpSel_Load(object sender, EventArgs e)
        {

            chkInsDate.Checked = true;

            mtb_From.Text = DateTime.Now.AddDays(1 - DateTime.Now.Day).ToString("yyyyMMdd");
            mtb_To.Text = DateTime.Today.ToString("yyyyMMdd");


            if(GBN == "P")
            {
                InitGridP();
                FillGridP();
            }
            else if (GBN == "DN")
            {
                InitGridDN();
                FillGridDN();
            }
            else if (GBN == "WG")
            {
                InitGridJG();
                FillGridJG();
            }
            else
            {
                InitGrid();
                FillGrid();
            }
           
        }

        private void btnFillGrid_Click(object sender, EventArgs e)
        {
            FillGrid();
        }


        private void btnOK_Click(object sender, EventArgs e)
        {
            if (GBN == "P")
            {
                PersonID = dgvInst.SelectedRows[0].Cells["PersonID"].Value.ToString();
                Person = dgvInst.SelectedRows[0].Cells["Person"].Value.ToString();
            }
            else if (GBN == "DN")
            {
                DayOrNightID = dgvInst.SelectedRows[0].Cells["DayOrNightGbnID"].Value.ToString();
                DayOrNight = dgvInst.SelectedRows[0].Cells["DayOrNightGbn"].Value.ToString();
            }
            else if (GBN == "WG")
            {
                JobGbnID = dgvInst.SelectedRows[0].Cells["JobGbnID"].Value.ToString();
                JobGbn = dgvInst.SelectedRows[0].Cells["JobGbn"].Value.ToString();
            }
            else if (GBN == "")
            {
                InstID = dgvInst.SelectedRows[0].Cells["InstID"].Value.ToString();
                InstDetSeq = dgvInst.SelectedRows[0].Cells["InstDetSeq"].Value.ToString();
                Process = dgvInst.SelectedRows[0].Cells["Process"].Value.ToString();
                ProcessID = dgvInst.SelectedRows[0].Cells["ProcessID"].Value.ToString();
                StartDate = dgvInst.SelectedRows[0].Cells["StartDate"].Value.ToString();
                StartTime = dgvInst.SelectedRows[0].Cells["StartTime"].Value.ToString();
                EndDate = dgvInst.SelectedRows[0].Cells["EndDate"].Value.ToString();
                EndTime = dgvInst.SelectedRows[0].Cells["EndTime"].Value.ToString();
            }



            DialogResult = DialogResult.OK;

            this.Dispose();
            this.Close();       
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }



        #region 달력 From값 입력 // 달력 창 띄우기
        private void mtb_From_Click(object sender, EventArgs e)
        {
            WizCommon.Popup.Frm_TLP_Calendar calendar = new WizCommon.Popup.Frm_TLP_Calendar(mtb_From.Text.Replace("-", ""), mtb_From.Name, mtb_To.Text.Replace("-", ""));
            calendar.WriteDateTextEvent += new WizCommon.Popup.Frm_TLP_Calendar.TextEventHandler(GetDate);
            calendar.Owner = this;
            calendar.ShowDialog();
        }
        #endregion
        #region 달력 To값 입력 // 달력 창 띄우기
        private void mtb_To_Click(object sender, EventArgs e)
        {
            WizCommon.Popup.Frm_TLP_Calendar calendar = new WizCommon.Popup.Frm_TLP_Calendar(mtb_To.Text.Replace("-", ""), mtb_To.Name, mtb_From.Text.Replace("-", ""));
            calendar.WriteDateTextEvent += new WizCommon.Popup.Frm_TLP_Calendar.TextEventHandler(GetDate);
            calendar.Owner = this;
            calendar.ShowDialog();
        }
        #endregion
        #region Calendar.Value -> mtbBox.Text 달력창으로부터 텍스트로 값을 옮겨주는 메소드
        private void GetDate(string strDate, string btnName)
        {
            DateTime dateTime = new DateTime();
            dateTime = DateTime.ParseExact(strDate, "yyyyMMdd", null);
            if (btnName == mtb_From.Name)
            {
                mtb_From.Text = dateTime.ToString("yyyy-MM-dd");
            }
            else if (btnName == mtb_To.Name)
            {
                mtb_To.Text = dateTime.ToString("yyyy-MM-dd");
            }

        }

        #endregion
    }
}