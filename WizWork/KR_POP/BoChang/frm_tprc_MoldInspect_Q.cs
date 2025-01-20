using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WizWork.Properties;
using Microsoft.Win32;
using Microsoft.VisualBasic;
using WizCommon;
namespace WizWork
{
    public partial class frm_tprc_MoldInspect_Q : Form
    {
        POPUP.Frm_CMKeypad keypad = new POPUP.Frm_CMKeypad();
        private DataSet ds = null;
        private DataSet ds1 = null;
        string[] Message = new string[2];
        string g_ProcessID = string.Empty;
        string g_MachineList = string.Empty;
        string MoldID = string.Empty;
        INI_GS gs = new INI_GS();
        List<string> DeleteRowInspectID = new List<string>();
        DataGridView UpdateList = new DataGridView();

        WizWorkLib Lib = new WizWorkLib();
        LogData LogData = new LogData(); //2022-06-21 log 남기는 함수

        public frm_tprc_MoldInspect_Q()
        {
            InitializeComponent();
        }

        private void frm_tprc_MoldInspect_Q_Load(object sender, EventArgs e)
        {
            LogData.LogSave(this.GetType().Name, "S"); //2022-06-22 사용시간(로드, 닫기)
            SetDateTimePicker();
            InitGrid();
            InitGrid2();
        }

        #region 그리드 초기 설정

        private void InitGrid()
        {
            dgvMold.Columns.Clear();
            dgvMold.ColumnCount = 7;

            int n = 0;
            // Set the Colums Hearder Names

            dgvMold.Columns[n].Name = "RowSeq";
            dgvMold.Columns[n].HeaderText = "순";
            dgvMold.Columns[n].Width = 50;
            dgvMold.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvMold.Columns[n++].Visible = true;

            dgvMold.Columns[n].Name = "MoldRInspectDate";
            dgvMold.Columns[n].HeaderText = "점검일자";
            dgvMold.Columns[n].Width = 130;
            dgvMold.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvMold.Columns[n++].Visible = true;

            dgvMold.Columns[n].Name = "MoldNo";
            dgvMold.Columns[n].HeaderText = "금형로트번호";
            dgvMold.Columns[n].Width = 160;
            dgvMold.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvMold.Columns[n++].Visible = true;

            dgvMold.Columns[n].Name = "BuyerArticleNo";
            dgvMold.Columns[n].HeaderText = "품번";
            dgvMold.Columns[n].Width = 90;
            dgvMold.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvMold.Columns[n++].Visible = true;

            dgvMold.Columns[n].Name = "ArticleID";
            dgvMold.Columns[n].HeaderText = "품명";
            dgvMold.Columns[n].Width = 90;
            dgvMold.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvMold.Columns[n++].Visible = true;

            dgvMold.Columns[n].Name = "MoldID";
            dgvMold.Columns[n].HeaderText = "금형명";
            dgvMold.Columns[n].Width = 90;
            dgvMold.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvMold.Columns[n++].Visible = true;

            dgvMold.Columns[n].Name = "MoldRInspectID";
            dgvMold.Columns[n].HeaderText = "MoldRInspectID";
            dgvMold.Columns[n].Width = 200;
            dgvMold.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dgvMold.Columns[n++].Visible = false;

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
                chkCol.Visible = true;
            }
            dgvMold.Columns.Insert(1, chkCol);

            dgvMold.Font = new Font("맑은 고딕", 12);
            dgvMold.RowTemplate.Height = 30;
            dgvMold.ColumnHeadersHeight = 35;
            dgvMold.ScrollBars = ScrollBars.Both;
            dgvMold.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMold.ReadOnly = true;

            foreach (DataGridViewColumn col in dgvMold.Columns)
            {
                col.DataPropertyName = col.Name;
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            return;
        }

        private void InitGrid2()
        {
            dgvMoldSub.Columns.Clear();
            dgvMoldSub.ColumnCount = 4;

            int n = 0;
            // Set the Colums Hearder Names

            dgvMoldSub.Columns[n].Name = "RowSeq";
            dgvMoldSub.Columns[n].HeaderText = "No";
            dgvMoldSub.Columns[n].Width = 40;
            dgvMoldSub.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvMoldSub.Columns[n++].Visible = true;

            dgvMoldSub.Columns[n].Name = "MoldInspectItemName";
            dgvMoldSub.Columns[n].HeaderText = "검사항목";
            dgvMoldSub.Columns[n].Width = 100;
            dgvMoldSub.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dgvMoldSub.Columns[n++].Visible = true;

            dgvMoldSub.Columns[n].Name = "MoldInspectContent";
            dgvMoldSub.Columns[n].HeaderText = "검사기준";
            dgvMoldSub.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvMoldSub.Columns[n].ReadOnly = true;
            dgvMoldSub.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dgvMoldSub.Columns[n++].Visible = true;

            dgvMoldSub.Columns[n].Name = "MldRInspectLegend";
            dgvMoldSub.Columns[n].HeaderText = "검사결과";
            dgvMoldSub.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvMoldSub.Columns[n].ReadOnly = true;
            dgvMoldSub.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvMoldSub.Columns[n++].Visible = true;

            dgvMoldSub.Font = new Font("맑은 고딕", 12);
            dgvMoldSub.RowTemplate.Height = 30;
            dgvMoldSub.ColumnHeadersHeight = 35;
            dgvMoldSub.ScrollBars = ScrollBars.Both;
            dgvMoldSub.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMoldSub.ReadOnly = true;

            foreach (DataGridViewColumn col in dgvMoldSub.Columns)
            {
                col.DataPropertyName = col.Name;
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            return;
        }

        #endregion

        #region 날짜 초기화

        private void SetDateTimePicker()
        {
            mtb_From.Text = DateTime.Today.ToString("yyyyMMdd");
            mtb_To.Text = DateTime.Today.AddDays(7).ToString("yyyyMMdd");
        }

        #endregion

        #region 금형버튼 이벤트

        private void btnMoldLoT_Click(object sender, EventArgs e)
        {
            frm_tprc_Mold_ByArticleID Ftmba = new frm_tprc_Mold_ByArticleID();
            Ftmba.StartPosition = FormStartPosition.CenterScreen;
            if (Ftmba.ShowDialog() == DialogResult.OK)
            {
                txtMoldLoT.Text = Ftmba.MoldNo;
                //MoldID = Ftmba.MoldID;
                FillGrid();
            }
        }

        private void txtMoldLoT_Click(object sender, EventArgs e)
        {
            frm_tprc_Mold_ByArticleID Ftmba = new frm_tprc_Mold_ByArticleID();
            Ftmba.StartPosition = FormStartPosition.CenterScreen;
            if (Ftmba.ShowDialog() == DialogResult.OK)
            {
                txtMoldLoT.Text = Ftmba.MoldNo;
                //MoldID = Ftmba.MoldID;
                FillGrid();
            }
        }

        #endregion

        #region 달력 이벤트

        private void mtStartDate_Click(object sender, EventArgs e)
        {
            WizCommon.Popup.Frm_TLP_Calendar calendar = new WizCommon.Popup.Frm_TLP_Calendar(mtb_From.Text.Replace("-", ""), mtb_From.Name, mtb_To.Text.Replace("-", ""));
            calendar.WriteDateTextEvent += new WizCommon.Popup.Frm_TLP_Calendar.TextEventHandler(GetDate);
            calendar.Owner = this;
            calendar.ShowDialog();
        }

        private void mtEndDate_Click(object sender, EventArgs e)
        {
            WizCommon.Popup.Frm_TLP_Calendar calendar = new WizCommon.Popup.Frm_TLP_Calendar(mtb_To.Text.Replace("-", ""), mtb_To.Name, mtb_From.Text.Replace("-", ""));
            calendar.WriteDateTextEvent += new WizCommon.Popup.Frm_TLP_Calendar.TextEventHandler(GetDate);
            calendar.Owner = this;
            calendar.ShowDialog();
        }

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

        #region 검색, 삭제, 닫기 이벤트

        private void btnFillGrid_Click(object sender, EventArgs e)
        {
            FillGrid();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            LogData.LogSave(this.GetType().Name, "S"); //2022-06-22 사용시간(로드, 닫기)
            this.Close();
        }

        #endregion

        #region 탭 변경 이벤트

        private void tabc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabc.SelectedIndex == 0)
            {
                btnDelete.Visible = true;
                btnFillGrid.Visible = true;
                if (dgvMold.Rows.Count > 0 && dgvMold.SelectedRows.Count > 0)
                {
                    Frm_tprc_Main.gv.queryCount = string.Format("{0:n0}", dgvMold.RowCount);
                    Frm_tprc_Main.gv.SetStbInfo();
                    FillGrid();
                }
            }
            else if (tabc.SelectedIndex == 1)
            {
                btnDelete.Visible = false;
                btnFillGrid.Visible = false;
                FillGridSub();
            }
        }

        #endregion

        #region 조회 함수

        private void FillGrid()
        {
            dgvMold.Rows.Clear();
            try
            {
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                DateTime dateTime = DateTime.Now;
                if (!chkDate.Checked)
                {
                    sqlParameter.Add("nChkDate", "0");    // 최종검사
                    sqlParameter.Add("SDate", "");
                    sqlParameter.Add("EDate", "");
                }
                else
                {
                    sqlParameter.Add("nChkDate", "1");    // 최종검사
                    sqlParameter.Add("SDate", mtb_From.Text.Replace("-", ""));
                    sqlParameter.Add("EDate", mtb_To.Text.Replace("-", ""));
                }

                sqlParameter.Add("MoldLotNO", txtMoldLoT.Text);
                       
                ds = DataStore.Instance.ProcedureToDataSet("xp_WizWork_sRegularInspect", sqlParameter, false);

                if (ds.Tables[0].Rows.Count > 0)
                {

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DataRow dr = ds.Tables[0].Rows[i];
                        dateTime = DateTime.ParseExact(dr["MoldInspectDate"].ToString(), "yyyyMMdd", null);
                        dgvMold.Rows.Add(
                            i + 1,                                          //'0)Index
                            false,                                          //'1)check
                            dateTime.ToString("yyyy-MM-dd"),                //'2)점검일자
                            dr["MoldNo"].ToString(),                        //'3)금형로트번호
                            dr["BuyerArticleNo"].ToString(),                //'5)품번
                            dr["Article"].ToString(),                       //'6)품명
                            dr["MoldID"].ToString(),                        //금형명
                            dr["MoldInspectID"].ToString()                 //'7)MoldRInspectID
                            );
                        dgvMold.Rows[i].Height = 30;
                    }
                    dgvMold.ClearSelection();
                    dgvMold[0, 0].Selected = true;
                    dgvMold.Columns["RowSeq"].Width = 60;
                }
                Frm_tprc_Main.gv.queryCount = string.Format("{0:n0}", dgvMold.RowCount);
                Frm_tprc_Main.gv.SetStbInfo();
                LogData.LogSave(this.GetType().Name, "R"); //2022-06-22 조회
            }
            catch (Exception excpt)
            {
                Message[0] = "[오류]";
                Message[1] = string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message);
                WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
            }
        }


        private void FillGridSub()
        {
            try
            {
                dgvMoldSub.Rows.Clear();
                if (dgvMold.SelectedRows.Count == 0)
                {
                    return;
                }
                Dictionary<string, object> sqlParameter1 = new Dictionary<string, object>();

                sqlParameter1.Add("MoldRInspectID", dgvMold.SelectedRows[0].Cells["MoldRInspectID"].Value.ToString());    // 최종검사
                sqlParameter1.Add("MoldID", dgvMold.SelectedRows[0].Cells["MoldID"].Value.ToString());

                ds1 = DataStore.Instance.ProcedureToDataSet("xp_WizWork_sRegularInspectSub", sqlParameter1, false);

                if (ds1.Tables[0].Rows.Count > 0)
                {
                    string Value = "";
                    double douValue = 0;
                    for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                    {
                        DataRow dr = ds1.Tables[0].Rows[i];

                        if (dr["MoldInspectRecordGbn"].ToString() == "01")
                        {
                                dgvMoldSub.Rows.Add(
                                    i + 1,                                              //NO
                                    dr["MoldInspectItemName"],                          //점검항목
                                    dr["MoldInspectContent"],                           //점검내용
                                    dr["LEGEND"]);                                      //점검결과
                        }
                        else if (dr["MoldInspectRecordGbn"].ToString() == "02")
                        {
                                double.TryParse(dr["MldValue"].ToString(), out douValue);
                                dgvMoldSub.Rows.Add(
                                   i + 1,                                               //NO
                                   dr["MoldInspectItemName"],                          //점검항목
                                   dr["MoldInspectContent"],                           //점검내용
                                   string.Format("{0:n1}", douValue));
                        }
                        dgvMoldSub.Rows[i].Height = 30;
                    }
                    dgvMoldSub.AutoResizeColumns();
                    Frm_tprc_Main.gv.queryCount = string.Format("{0:n0}", dgvMoldSub.RowCount);
                    Frm_tprc_Main.gv.SetStbInfo();
                }
            }
            catch (Exception excpt)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message), "[오류]", 0, 1);
            }
            finally
            {
                DataStore.Instance.CloseConnection(); //2021-10-07 DB 커넥트 연결 해제
            }
        }


        #endregion

        #region 삭제 함수

        private void DeleteData()
        {
            int checkCount = 0;//체크된 카운트
            int c = 0; //작업일이 현재일자와 같지 않아서 삭제할 수 없는 행의 수
            int deleteCount = 0; //작업일이 현재일자와 같지 않아서 삭제할 수 없는 행의 수
            if (tabc.SelectedIndex == 0)
            {
                if (dgvMold.RowCount == 0)
                {
                    WizCommon.Popup.MyMessageBox.ShowBox("조회 후 삭제 버튼을 눌러주십시오.", "[조회 클릭]", 0, 0);
                }
                else
                {
                    foreach (DataGridViewRow dgvr in dgvMold.Rows)
                    {
                        if (dgvr.Cells["Check"].Value.ToString().ToUpper() == "TRUE")
                        {
                            checkCount++;
                        }
                    }
                    if (checkCount == 0)
                    {
                        WizCommon.Popup.MyMessageBox.ShowBox("삭제 대상을 선택 후 '삭제'버튼을 클릭해주세요.", "[삭제 대상 클릭]", 0, 0);
                    }
                    else
                    {
                        if (WizCommon.Popup.MyMessageBox.ShowBox("선택항목에 대해서 삭제처리하시겠습니까?", "[삭제]", 0, 0) == DialogResult.OK)
                        {
                            List<string> list_Confirm = new List<string>();//프로시저 수행 성공여부 값 저장/success/failure
                            foreach (DataGridViewRow dgvr in dgvMold.Rows)
                            {
                                if (dgvr.Cells["Check"].Value.ToString().ToUpper() == "TRUE")
                                {
                                    if (dgvr.Cells["MoldRInspectDate"].Value.ToString().Replace("-", "") == DateTime.Now.ToString("yyyyMMdd"))
                                    {
                                        Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                                        sqlParameter.Add("MoldRInspectID", dgvr.Cells["MoldRInspectID"].Value.ToString());
                                        string[] sConfirm = new string[2];
                                        sConfirm = DataStore.Instance.ExecuteProcedure("xp_WizWork_dRegularInspect", sqlParameter, true);
                                        list_Confirm.Add(sConfirm[0]);
                                        if (sConfirm[0].ToUpper() == "SUCCESS")
                                        { deleteCount++; }
                                    }
                                    else
                                    {
                                        c++;
                                    }
                                }
                            }
                            if (list_Confirm.Count > 0)//삭제결과 리스트
                            {
                                LogData.LogSave(this.GetType().Name, "D"); //2022-06-22 삭제
                                FillGrid();
                                LogData.LogSave(this.GetType().Name, "R"); //2022-06-22 조회
                                if (c > 0)
                                {
                                    WizCommon.Popup.MyMessageBox.ShowBox("현재 날짜와 동일한 작업일자" + deleteCount.ToString() + "건 삭제완료됬습니다." +
                                    "\r\n" + c.ToString() + "개 작업건수는 현재 날짜와 동일하지 않아 삭제할 수 없습니다.", "[삭제 완료]", 0, 1);
                                }
                                else
                                {
                                    WizCommon.Popup.MyMessageBox.ShowBox(deleteCount.ToString() + "건 삭제완료됬습니다.", "[삭제 완료]", 0, 1);
                                }
                            }
                            else//삭제 결과리스트가 없음 > 삭제를 안했음
                            {
                                if (c > 0)
                                {
                                    WizCommon.Popup.MyMessageBox.ShowBox(c.ToString() + "개 작업건수는 현재 날짜와 동일하지 않아 삭제할 수 없습니다.", "[삭제 실패]", 0, 1);
                                }
                                else
                                {
                                    WizCommon.Popup.MyMessageBox.ShowBox("삭제실패! 관리자에게 문의하세요", "[삭제 실패]", 0, 1);
                                }
                            }

                            DataStore.Instance.CloseConnection(); //2021-10-07 DB 커넥트 연결 해제
                        }
                    }
                }
            }
        }

        #endregion

        private void dgvMold_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (dgvMold.Rows[e.RowIndex].Cells["Check"].Value.ToString().ToUpper() == "FALSE")
                {
                    dgvMold.Rows[e.RowIndex].Cells["Check"].Value = true;
                }
                else if (dgvMold.Rows[e.RowIndex].Cells["Check"].Value.ToString().ToUpper() == "TRUE")
                {
                    dgvMold.Rows[e.RowIndex].Cells["Check"].Value = false;
                }
            }
        }
    }
}
