using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WizCommon;
//using WizWork.Tkb.PopUp;

namespace WizWork
{
    public partial class frm_tprc_WorkCall_Q : Form
    {
        string[] Message = new string[2];
        INI_GS gs = Frm_tprc_Main.gs;
        WizWorkLib Lib = Frm_tprc_Main.Lib;
        DataTable dt = null; 
        private DataSet ds = null; //2022-10-21
        int x = 0; //grdlist 그리드 좌우 이동용 변수
        List<string> WorkCallIDList = new List<string>(); //여러개를 한꺼번에 처리할 경우를 위해 리스트 생성 2022-10-21

        LogData LogData = new LogData(); //2022-10-24 log 남기는 함수

        public frm_tprc_WorkCall_Q()
        {
            InitializeComponent();
        }

        //화면 닫기
        private void cmdClose_Click(object sender, EventArgs e)
        {
            LogData.LogSave(this.GetType().Name, "S"); //log 남기기(로드 S) 2022-10-24
            this.Close();
        }

        private void frm_tins_InspectAutoResult_Q_Load(object sender, EventArgs e)
        {
            LogData.LogSave(this.GetType().Name, "S"); //log 남기기(로드 S) 2022-10-24

            SetScreen();
            InitGrid();
            
            setComboBox();

            mtb_From.Text = DateTime.Today.ToString("yyyy-MM-dd");
            mtb_To.Text = DateTime.Today.AddDays(7).ToString("yyyyMMdd");

            FillGridList();

        }

        #region 콤보박스 세팅

        private void setComboBox()
        {
            SetComboBoxProcess();
            SetComboBoxMachineID("");
            setStateComboBox();
        }

        private void setStateComboBox()
        {
            var state = new BindingList<KeyValuePair<string, string>>();

            state.Add(new KeyValuePair<string, string>("*", "전체"));
            state.Add(new KeyValuePair<string, string>("N", "미처리"));
            state.Add(new KeyValuePair<string, string>("Y", "완료"));

            cboState.DataSource = state;
            cboState.ValueMember = "Key";
            cboState.DisplayMember = "Value";
            cboState.SelectedIndex = 0;
        }   

        #endregion

        #region 레이아웃에 채우기

        private void SetScreen()
        {
            tlpForm.Dock = DockStyle.Fill;
            tlpForm.Margin = new Padding(1, 1, 1, 1);
            foreach (Control control in tlpForm.Controls)//con = tlp 상위에서 2번째
            {
                control.Dock = DockStyle.Fill;
                control.Margin = new Padding(1, 1, 1, 1);
                foreach (Control contro in control.Controls)//tlp 상위에서 3번째
                {
                    contro.Dock = DockStyle.Fill;
                    contro.Margin = new Padding(1, 1, 1, 1);
                    foreach (Control contr in contro.Controls)
                    {
                        contr.Dock = DockStyle.Fill;
                        contr.Margin = new Padding(1, 1, 1, 1);
                        foreach (Control cont in contr.Controls)
                        {
                            cont.Dock = DockStyle.Fill;
                            cont.Margin = new Padding(1, 1, 1, 1);
                            foreach (Control con in cont.Controls)
                            {
                                con.Dock = DockStyle.Fill;
                                con.Margin = new Padding(1, 1, 1, 1);
                                foreach (Control co in con.Controls)
                                {
                                    co.Dock = DockStyle.Fill;
                                    co.Margin = new Padding(1, 1, 1, 1);
                                    foreach (Control c in co.Controls)
                                    {
                                        c.Dock = DockStyle.Fill;
                                        c.Margin = new Padding(1, 1, 1, 1);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            //tlp_Search_Date.SetRowSpan(chkDate, 2);
        }

        #endregion

        #region Default Grid Setting

        private void InitGrid()
        {
            grdList.Columns.Clear();
            grdList.ColumnCount = 14;
            
            int n = 0;
            // Set the Colums Hearder Names

            grdList.Columns[n].Name = "RowSeq";
            grdList.Columns[n].HeaderText = "No";
            grdList.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdList.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            grdList.Columns[n++].Visible = true;

            grdList.Columns[n].Name = "Process";
            grdList.Columns[n].HeaderText = "공정";
            grdList.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdList.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdList.Columns[n++].Visible = true;

            grdList.Columns[n].Name = "MachineNo";
            grdList.Columns[n].HeaderText = "호기";
            grdList.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdList.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdList.Columns[n++].Visible = true;

            grdList.Columns[n].Name = "CallPersonName";
            grdList.Columns[n].HeaderText = "호출자";
            grdList.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdList.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            grdList.Columns[n++].Visible = true;

            grdList.Columns[n].Name = "CallReasonName";
            grdList.Columns[n].HeaderText = "호출사유";
            grdList.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdList.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdList.Columns[n++].Visible = true;

            grdList.Columns[n].Name = "RepondDate";
            grdList.Columns[n].HeaderText = "처리일";
            grdList.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdList.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            grdList.Columns[n++].Visible = true;

            grdList.Columns[n].Name = "RepondPersonName";
            grdList.Columns[n].HeaderText = "처리자";
            grdList.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdList.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdList.Columns[n++].Visible = true;

            grdList.Columns[n].Name = "RepondName";
            grdList.Columns[n].HeaderText = "조치내용";
            grdList.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdList.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdList.Columns[n++].Visible = true;

            grdList.Columns[n].Name = "WorkCallID";
            grdList.Columns[n].HeaderText = "WorkCallID";
            grdList.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdList.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdList.Columns[n++].Visible = false;

            grdList.Columns[n].Name = "CallReasonCode";
            grdList.Columns[n].HeaderText = "호출사유ID";
            grdList.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdList.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdList.Columns[n++].Visible = false;

            grdList.Columns[n].Name = "RepondCode";
            grdList.Columns[n].HeaderText = "처리사유ID";
            grdList.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdList.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdList.Columns[n++].Visible = false;

            grdList.Columns[n].Name = "RespondAbleYN";
            grdList.Columns[n].HeaderText = "처리가능여부";
            grdList.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdList.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdList.Columns[n++].Visible = false;

            grdList.Columns[n].Name = "ProcessID";
            grdList.Columns[n].HeaderText = "ProcessID";
            grdList.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdList.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdList.Columns[n++].Visible = false;

            grdList.Columns[n].Name = "MachineID";
            grdList.Columns[n].HeaderText = "MachineID";
            grdList.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdList.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdList.Columns[n++].Visible = false;

            DataGridViewCheckBoxColumn chkCol = new DataGridViewCheckBoxColumn();
            {
                chkCol.HeaderText = "";
                chkCol.Name = "Check";
                chkCol.Width = 110;
                //chkCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                chkCol.FlatStyle = FlatStyle.Standard;
                chkCol.ThreeState = true;
                chkCol.CellTemplate = new DataGridViewCheckBoxCell();
                chkCol.CellTemplate.Style.BackColor = Color.Beige;
                chkCol.Visible = true;
            }
            grdList.Columns.Insert(0, chkCol);

            grdList.Font = new Font("맑은 고딕", 12, FontStyle.Bold);
            grdList.RowTemplate.Height = 30;
            grdList.ColumnHeadersHeight = 45;
            grdList.ScrollBars = ScrollBars.Both;
            grdList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grdList.ReadOnly = true;
            grdList.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(234, 234, 234);
            grdList.ScrollBars = ScrollBars.Both;
            grdList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grdList.MultiSelect = false;

            foreach (DataGridViewColumn col in grdList.Columns)
            {
                col.DataPropertyName = col.Name;
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            }
            return;
        }

        #endregion

        #region 검색버튼 클릭 → 조회 모음

        private void cmdSearch_Click(object sender, EventArgs e)
        {
            FillGridList();
        }

        private void FillGridList()
        {
            try
            {
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Clear();

                sqlParameter.Add("nchkFallDate", chkDate.Checked  == true ? 1 : 0);
                sqlParameter.Add("sCallFromDate", mtb_From.Text.Replace("-", ""));
                sqlParameter.Add("sCallToDate", mtb_To.Text.Replace("-", ""));

                sqlParameter.Add("nchkProcessID", chkprocess.Checked == true ? 1 : 0);
                sqlParameter.Add("sProcessID", cboProcess.SelectedValue.ToString());

                sqlParameter.Add("nchkMachineID", chkMachine.Checked == true ? 1 : 0);
                sqlParameter.Add("sMachineID", cboMachineID.SelectedValue.ToString());

                sqlParameter.Add("nchkCallState", chkState.Checked == true ? 1 : 0);
                sqlParameter.Add("sCallState", cboState.SelectedValue.ToString());

                dt = DataStore.Instance.ProcedureToDataTable("xp_WizWork_sWkWorkCall", sqlParameter, false);

                grdList.Rows.Clear();

                int index = 0;

                foreach (DataRow dr in dt.Rows)
                {
                    index++;
                   
                    grdList.Rows.Add(
                                    false,                                                      //check
                                    index.ToString(),                                           //순서
                                    dr["Process"].ToString().Trim(),                            //공정명
                                    dr["MachineNo"].ToString().Trim(),                          //호기                 
                                    dr["CallPersonName"].ToString().Trim(),                     //호출자
                                    dr["CallReasonName"].ToString().Trim(),                     //호출사유
                                    Lib.DatePickerFormat(dr["RepondDate"].ToString().Trim()),   //처리일자
                                    dr["RepondPersonName"].ToString().Trim(),                   //처리자
                                    dr["RepondCodeName"].ToString().Trim(),                     //처리사유
                                    dr["WorkCallID"].ToString().Trim(),                         //WorkCallID
                                    dr["CallReasonCode"].ToString().Trim(),                     //호출사유코드
                                    dr["RepondCode"].ToString().Trim(),                         //처리사유코드
                                    dr["RespondAbleYN"].ToString().Trim(),                      //처리가능여부
                                    dr["processID"].ToString().Trim(),                          //공정ID
                                    dr["MachineID"].ToString().Trim()                           //호기ID
                                    );
                }
                Frm_tprc_Main.gv.queryCount = string.Format("{0:n0}", grdList.RowCount);
                Frm_tprc_Main.gv.SetStbInfo();


                if (cboState.SelectedValue.ToString() == "Y" && chkState.Checked == true) //처리완료
                {
                    CallSumYN.Text = "처리건수";
                    txtCallSumAll.Text = string.Format("{0:n0}", index);
                    txtCallSumYN.Text = string.Format("{0:n0}", index);
                }
                else if(cboState.SelectedValue.ToString() == "N" && chkState.Checked == true) //처리불가
                {
                    CallSumYN.Text = "미처리건수";
                    txtCallSumAll.Text = string.Format("{0:n0}", index);
                    txtCallSumYN.Text = string.Format("{0:n0}", index);
                }
                else
                {
                    CallSumYN.Text = "처리건수";
                    txtCallSumAll.Text = string.Format("{0:n0}", index);
                    int CallSumYNCount = 0;
                    for(int i = 0; i < grdList.Rows.Count; i++)
                    {
                        if(grdList.Rows[i].Cells["RespondAbleYN"].Value.ToString() != "")
                        {
                            CallSumYNCount++;
                        }
                    }
                    txtCallSumYN.Text = string.Format("{0:n0}", CallSumYNCount);
                }
                LogData.LogSave(this.GetType().Name, "R"); //log 남기기(조회 R) 2022-10-24

            }
            catch (Exception excpt)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message), "[오류]", 0, 1);
            }
        }

       
        #endregion

        /// <summary>
        /// Grid 위로 버튼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdUp_Click(object sender, EventArgs e)
        {
            Lib.btnRowUp(grdList, x);
        }

        private void cmdDown_Click(object sender, EventArgs e)
        {
            Lib.btnRowDown(grdList, x);
        }

        #region 삭제 모음

        private void cmdDelete_Click(object sender, EventArgs e)
        {

            try
            {
                //조치한 호출은 조치처리한 날짜에만 삭제가능, 조치하지 않은 호출은 호출일자에만 삭제 가능, 삭제시 미조치, 조치 전부 삭제 됨
                int CheckCount = 0;   //체크한 행
                int DeleteYCount = 0; //삭제가능한 행
                int DeleteNCount = 0; //삭제불가능한 행

                string ErrorMsg = "";

                if (grdList.RowCount == 0)
                {
                    WizCommon.Popup.MyMessageBox.ShowBox("조회 후 삭제 버튼을 눌러주십시오.", "[조회자료 없음]", 0, 1);
                    return;
                }
                else
                {
                    foreach (DataGridViewRow dgvr in grdList.Rows)
                    {
                        if (dgvr.Cells["Check"].Value.ToString().ToUpper() == "TRUE")
                        {
                            CheckCount++;
                        }
                    }

                    if (CheckCount == 0)
                    {
                        WizCommon.Popup.MyMessageBox.ShowBox("처리할 호출을 선택해주세요.", "[삭제 처리]", 0, 1);
                        return;
                    }
                    else
                    {
                        string DeleteWorkCallID = string.Empty;

                        if (WizCommon.Popup.MyMessageBox.ShowBox("선택항목에 대해서 삭제처리하시겠습니까?", "[삭제]", 0, 0) == DialogResult.OK)
                        {
                            foreach (DataGridViewRow dgvr in grdList.Rows)
                            {
                                if (dgvr.Cells["Check"].Value.ToString().ToUpper() == "TRUE")
                                {
                                    if (dgvr.Cells["RepondDate"].Value.ToString().Replace("-", "") == "" || 
                                        dgvr.Cells["RepondDate"].Value.ToString().Replace("-", "") == DateTime.Now.ToString("yyyyMMdd")) 
                                    {
                                        DeleteWorkCallID = "";
                                        DeleteWorkCallID = dgvr.Cells["WorkCallID"].Value.ToString();

                                        Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                                        sqlParameter.Add("sWorkCallID", DeleteWorkCallID);// grdData.Rows[rowIndex].Cells["JobID"].Value.ToString());
                                        sqlParameter.Add("sRtnMsg", "");

                                        Dictionary<string, int> outputParam = new Dictionary<string, int>();
                                        outputParam.Add("sRtnMsg", 500);

                                        Dictionary<string, string> dicResult = DataStore.Instance.ExecuteProcedureOutputNoTran("xp_WizWork_dWkWorkCall", sqlParameter, outputParam, true);
                                        string result = dicResult["sRtnMsg"];
                                        if ((result != string.Empty || result != "9999")
                                            && result.Equals(""))
                                        {
                                            DeleteYCount++;
                                        }
                                    }
                                    else
                                    {
                                        DeleteNCount++;
                                    }
                                }
                            }
                            if (DeleteYCount > 0)//삭제결과 리스트
                            {
                                LogData.LogSave(this.GetType().Name, "D"); //2022-10-24 삭제
                                FillGridList();
                                if (DeleteNCount > 0)
                                {
                                    WizCommon.Popup.MyMessageBox.ShowBox("현재 날짜와 동일한 조치일자 " + DeleteYCount.ToString() + "건 삭제완료됬습니다." +
                                    "\r\n" + DeleteNCount.ToString() + "개 호출건은 현재 날짜와 동일하지 않아 삭제할 수 없습니다.", "[삭제 완료]", 0, 1);
                                }
                                else
                                {
                                    WizCommon.Popup.MyMessageBox.ShowBox(DeleteYCount.ToString() + "건 삭제완료됬습니다.", "[삭제 완료]", 0, 1);
                                }

                                DataStore.Instance.CloseConnection(); //2021-10-07 DB 커넥트 연결 해제
                            }
                            else//삭제 결과리스트가 없음 > 삭제를 안했음
                            {
                                if (DeleteNCount > 0)
                                {
                                    ErrorMsg += DeleteNCount.ToString() + "개 호출건은 현재 날짜와 동일하지 않아 삭제할 수 없습니다.";
                                }
                            }

                            if (ErrorMsg.Length > 0)
                            {
                                int maxLine = 4;
                                // 줄넘김이 5개 이상이면 자르고, 외 몇건으로 표기하기.
                                if (WordCheck(ErrorMsg, "\r\n") > maxLine)
                                {
                                    string[] msg = ErrorMsg.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                                    ErrorMsg = "";
                                    int AfterCnt = 0;

                                    for (int k = 0; k < msg.Length; k++)
                                    {
                                        if (k < maxLine)
                                        {
                                            if (k == 0)
                                            {
                                                ErrorMsg += msg[k];
                                            }
                                            else
                                            {
                                                ErrorMsg += "\r\n" + msg[k];
                                            }

                                        }
                                        else
                                        {
                                            AfterCnt++;
                                        }
                                    }

                                    if (AfterCnt > 0) { ErrorMsg += "\r\n 외 " + AfterCnt + "건"; }
                                }
                                WizCommon.Popup.MyMessageBox.ShowBox(ErrorMsg, "[삭제 불가]", 0, 1, 2);
                            }
                        }

                    }

                }

                //if (grdList.SelectedRows.Count == 0) { WizCommon.Popup.MyMessageBox.ShowBox("삭제할 항목을 선택해주세요.", "[선택 항목 없음]", 0, 1); return; }

                //// 이미 미조치 처리 상태면 리턴
                //if (grdList.SelectedRows[0].Cells["MgrProcessDate"].Value.ToString().Trim().Equals("") == false) { WizCommon.Popup.MyMessageBox.ShowBox("삭제는 미조치 항목만 가능합니다.", "[삭제 불가능]", 0, 1); return; }

                //string CallMgrID = grdList.SelectedRows[0].Cells["CallMgrID"].Value.ToString().Trim();

                //if (WizCommon.Popup.MyMessageBox.ShowBox("선택항목에 대해서 삭제처리하시겠습니까?", "[삭제]", 0, 0) == DialogResult.OK)
                //{
                //    if (DeleteData(CallMgrID))
                //    {
                //        FillGridList();
                //    }
                //}
            }
            catch (Exception excpt)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message), "[오류]", 0, 1);
            }
        }

        #region 특정 문자 갯수 구하기

        public int WordCheck(string String, string Word)
        {
            string[] StringArray = String.Split(new string[] { Word }, StringSplitOptions.None);
            return (StringArray.Length - 1);
        }

        #endregion


        private bool DeleteData(string strID)
        {
            bool flag = false;

            try
            {
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Clear();

                sqlParameter.Add("CallMgrID", strID);

                DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_prdWork_dCallManager", sqlParameter, false);

                WizCommon.Popup.MyMessageBox.ShowBox("삭제가 완료되었습니다.", "[삭제]", 0, 1);

                if (dt is null || dt.Rows.Count == 0 || dt.Rows.Count > 1)
                {
                    return true;
                }

            }
            catch (Exception excpt)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message), "[오류]", 0, 1);
            }

            return flag;
        }

        #endregion


        #region 달력 From값 입력 // 달력 창 띄우기
        private void mtb_From_Click(object sender, EventArgs e)
        {
            WizCommon.Popup.Frm_TLP_Calendar calendar = new WizCommon.Popup.Frm_TLP_Calendar(mtb_From.Text.Replace("-", ""), mtb_From.Name, mtb_To.Text.Replace("-", ""));
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
        #region 달력 To값 입력 // 달력 창 띄우기
        private void mtb_To_Click(object sender, EventArgs e)
        {
            WizCommon.Popup.Frm_TLP_Calendar calendar = new WizCommon.Popup.Frm_TLP_Calendar(mtb_To.Text.Replace("-", ""), mtb_To.Name, mtb_From.Text.Replace("-", ""));
            calendar.WriteDateTextEvent += new WizCommon.Popup.Frm_TLP_Calendar.TextEventHandler(GetDate);
            calendar.Owner = this;
            calendar.ShowDialog();
        }
        #endregion

        #region 저장 관련 모음

        // 미 조치 등록
        private void btnSave_No_Click(object sender, EventArgs e)
        {
            if (grdList.SelectedRows.Count == 0) { WizCommon.Popup.MyMessageBox.ShowBox("미조치 처리할 호출을 선택해주세요.", "[미조치 처리]", 0, 1); return; }

            // 이미 미조치 처리 상태면 리턴
            if (grdList.SelectedRows[0].Cells["MgrProcessDate"].Value.ToString().Trim().Equals("")) { return; }

            string CallMgrID = grdList.SelectedRows[0].Cells["CallMgrID"].Value.ToString().Trim();

            if (WizCommon.Popup.MyMessageBox.ShowBox("선택항목에 대해서 미조치 처리하시겠습니까?", "[미조치 처리]", 0, 0) == DialogResult.OK)
            {
                if (SaveData_No(CallMgrID))
                {
                    FillGridList();
                }
            }
        }

        // 조치 등록
        private void btnSave_OK_Click(object sender, EventArgs e)
        {
            int checkCount = 0;
            int checkCountYN = 0;
            WorkCallIDList.Clear();

            foreach (DataGridViewRow dgvr in grdList.Rows)
            {
                if (dgvr.Cells["Check"].Value.ToString().ToUpper() == "TRUE")
                {
                    checkCount++;
                }
            }

            if (checkCount == 0) 
            { 
                WizCommon.Popup.MyMessageBox.ShowBox("처리할 호출을 선택해주세요.", "[미조치 처리]", 0, 1); 
                return; 
            }

            foreach (DataGridViewRow dgvr in grdList.Rows)
            {
                if (dgvr.Cells["Check"].Value.ToString().ToUpper() == "TRUE" 
                    && dgvr.Cells["RespondAbleYN"].Value.ToString().Trim().Equals("") == false )
                {
                    checkCountYN++;
                }
            }

            // 이미 조치 처리 상태면 리턴
            if (checkCountYN > 0) 
            {
                Message[0] = "[호출 처리 등록]";
                Message[1] = "이미 처리된 호출이 포함되어 있습니다.\r\n 그래도 진행하실려면 확인을 눌러 진행해주세요.";
                if(WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 0) == DialogResult.OK)
                {
                    foreach (DataGridViewRow dgvr in grdList.Rows)
                    {
                        if (dgvr.Cells["Check"].Value.ToString().ToUpper() == "TRUE")
                        {
                            WorkCallIDList.Add(dgvr.Cells["WorkCallID"].Value.ToString());
                        }
                    }

                    frm_tprc_WorkCall_QU call = new frm_tprc_WorkCall_QU(WorkCallIDList);
                    call.StartPosition = FormStartPosition.CenterScreen;
                    if (call.ShowDialog() == DialogResult.OK)
                    {
                        LogData.LogSave(this.GetType().Name, "C"); //2022-10-24 추가
                        FillGridList();
                    }

                }
                else 
                {
                    return;
                }

            }
            else
            {
                foreach (DataGridViewRow dgvr in grdList.Rows)
                {
                    if (dgvr.Cells["Check"].Value.ToString().ToUpper() == "TRUE")
                    {
                        WorkCallIDList.Add(dgvr.Cells["WorkCallID"].Value.ToString());
                    }
                }

                frm_tprc_WorkCall_QU call = new frm_tprc_WorkCall_QU(WorkCallIDList);
                call.StartPosition = FormStartPosition.CenterScreen;
                if (call.ShowDialog() == DialogResult.OK)
                {
                    LogData.LogSave(this.GetType().Name, "C"); //2022-10-24 추가
                    FillGridList();
                }

            }



        }

        private bool SaveData_No(string CallMgrID)
        {
            bool flag = false;

            try
            {
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();

                //1
                List<Procedure> Prolist = new List<Procedure>();
                List<Dictionary<string, object>> ListParameter = new List<Dictionary<string, object>>();

                sqlParameter.Add("CallMgrID", CallMgrID); // 0401
                sqlParameter.Add("sMgrPersonID", "");
                sqlParameter.Add("sMgrProcessDate", "");
                sqlParameter.Add("sMgrProcessTime", "");
                sqlParameter.Add("sMgrProcessID", "");

                sqlParameter.Add("sComments", "");
                sqlParameter.Add("sUpdateUserID", Frm_tprc_Main.g_tBase.PersonID);

                Procedure pro1 = new Procedure();
                pro1.Name = "xp_prdWork_uCallManagerProcess";
                pro1.OutputUseYN = "N";
                pro1.OutputName = "JobID";
                pro1.OutputLength = "19";

                Prolist.Add(pro1);
                ListParameter.Add(sqlParameter);

                List<KeyValue> list_Result = new List<KeyValue>();
                list_Result = DataStore.Instance.ExecuteAllProcedureOutputGetCS(Prolist, ListParameter);

                if (list_Result[0].key.ToLower() == "success")
                {
                    flag = true;
                    Message[0] = "[저장 성공]";
                    Message[1] = "정상적으로 등록이 되었습니다.";
                    WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                }
                else
                {
                    Message[0] = "[저장 실패]";
                    Message[1] = "오류! 관리자에게 문의";
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                return false;
            }

            return flag;
        }

        #endregion


        #region 콤보박스 관련 함수

        private void cboMachine_DropDown(object sender, EventArgs e)
        {
            SetComboBoxMachineID(cboProcess.SelectedValue.ToString());
        }

        private void SetComboBoxProcess()
        {
            try
            {
                ds = DataStore.Instance.ProcedureToDataSet("[xp_WizWork_sCallMgrProcess]", null, false);

                DataRow newRow = ds.Tables[0].NewRow();
                newRow[Work_sProcess.PROCESSID] = "*";
                newRow[Work_sProcess.PROCESS] = "전체";

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    ds.Tables[0].Rows.InsertAt(newRow, 0);
                    cboProcess.DataSource = ds.Tables[0];
                }

                cboProcess.ValueMember = Work_sProcess.PROCESSID;
                cboProcess.DisplayMember = Work_sProcess.PROCESS;

            }
            catch (Exception excpt)
            {
                MessageBox.Show(string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message));
            }
            finally
            {
                DataStore.Instance.CloseConnection();
            }
        }

        private void SetComboBoxMachineID(string ProcessID)
        {
            try
            {
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Add("SPROCESSID", ProcessID);
                ds = DataStore.Instance.ProcedureToDataSet("xp_WizWork_sCallMgrMachineID", sqlParameter, false);

                DataRow newRow = ds.Tables[0].NewRow();
                newRow[Work_sMachineByProcess.MACHINEID] = "*";
                newRow[Work_sMachineByProcess.MACHINE] = "전체";

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    ds.Tables[0].Rows.InsertAt(newRow, 0);
                    cboMachineID.DataSource = ds.Tables[0];
                }
                else
                {
                    ds.Tables[0].Rows.InsertAt(newRow, 0);
                    cboMachineID.DataSource = ds.Tables[0];

                    Message[0] = "[호기 설정]";
                    Message[1] = "해당 공정의 설비가 등록되어 있지 않습니다.";
                    WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                }

                cboMachineID.ValueMember = Work_sMachineByProcess.MACHINEID;
                cboMachineID.DisplayMember = Work_sMachineByProcess.MACHINE;
            }
            catch (Exception excpt)
            {
                MessageBox.Show(string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message));
            }
            finally
            {
                DataStore.Instance.CloseConnection();
            }
        }

        #endregion

        private void grdList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (grdList.Rows[e.RowIndex].Cells["Check"].Value.ToString().ToUpper() == "FALSE")
                {
                    grdList.Rows[e.RowIndex].Cells["Check"].Value = true;
                }
                else if (grdList.Rows[e.RowIndex].Cells["Check"].Value.ToString().ToUpper() == "TRUE")
                {
                    grdList.Rows[e.RowIndex].Cells["Check"].Value = false;
                }
            }
        }

        #region 콤보박스 인덱스 변경시 이벤트 모음
        private void cboProcess_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboProcess.SelectedIndex.ToString() != "0")
            {
                cboMachineID.Enabled = true;
                cboMachineID.SelectedIndex = 0;
                chkprocess.Checked = true;
            }
            else
            {
                if (cboMachineID.Items.Count > 0) //처음에 콤보박스설정전에 해당 함수를 실행하여 조건 추가함
                {
                    cboMachineID.SelectedIndex = 0;
                    chkMachine.Checked = false;
                    cboMachineID.Enabled = false;
                    chkprocess.Checked = false;
                }
                else
                {
                    chkMachine.Checked = false;
                    cboMachineID.Enabled = false;
                    chkprocess.Checked = false;
                }
            }
        }


        private void cboMachineID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMachineID.SelectedIndex.ToString() != "0")
            {
                chkMachine.Checked = true;
            }
            else
            {
                chkMachine.Checked = false;
            }
        }

        private void cboState_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboState.SelectedIndex.ToString() != "0")
            {
                chkState.Checked = true;
            }
            else
            {
                chkState.Checked = false;
            }
        }
        #endregion

    }
}
