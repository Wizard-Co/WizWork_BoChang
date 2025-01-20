using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WizCommon;
using WizWork.Properties;
using System.Diagnostics;
using System.IO;

namespace WizWork
{
    public partial class Frm_tprc_Result : Form
    {
        INI_GS gs = Frm_tprc_Main.gs;
        private DataSet ds = null;
        WizWorkLib Lib = new WizWorkLib();
        LogData LogData = new LogData(); //2022-06-21 log 남기는 함수
        int z = 0; //수평 스크롤바 이동용 변수

        // 체크용
        private string ChkNextWorkData_Msg = "";

        /// <summary>
        /// 생성
        /// </summary>
        public Frm_tprc_Result()
        {
            InitializeComponent();
        }


        private void btnLookup_Click(object sender, EventArgs e)
        {
            LogData.LogSave(this.GetType().Name, "R"); //2022-06-22 조회
            btnLookup.Enabled = false;

            Lib.Delay(3000); //2021-11-10 버튼을 여러번 클릭해도 한번만 클릭되게 딜레이 추가
            procQuery();

            btnLookup.Enabled = true;
        }

        /// <summary>
        /// 메인화면 버튼 클릭 - 폼 닫기
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMoveMain_Click(object sender, EventArgs e)
        {
            Close();
        }

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
        }

        string m_ResablyID = "";

        private void btnDelete_Click_1(object sender, EventArgs e)                            
        {
            int checkCount = 0;//체크된 카운트
            int deleteCount = 0; //작업일이 현재일자와 같지 않아서 삭제할 수 없는 행의 수
            int c = 0; //작업일이 현재일자와 같지 않아서 삭제할 수 없는 행의 수

            //
            string ErrorMsg = "";
            // 
            

            if (grdData.RowCount == 0)
            {
                WizCommon.Popup.MyMessageBox.ShowBox("조회 후 삭제 버튼을 눌러주십시오.", "[조회자료 없음]", 0, 1);
            }
            else
            {
                foreach (DataGridViewRow dgvr in grdData.Rows)
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
                    //List<string> list_Confirm = new List<string>();//프로시저 수행 성공여부 값 저장/success/failure
                    double i = 0;//삭제대상 JobID 임시 저장 변수
                    int SplitSeq = 0;


                    if (WizCommon.Popup.MyMessageBox.ShowBox("선택항목에 대해서 삭제처리하시겠습니까?", "[삭제]", 0, 0) == DialogResult.OK)
                    {
                        // 둘리
                        // 이 라벨로 다음 공정을 진행을 했다면??
                        ChkNextWorkData_Msg = "";

                        foreach (DataGridViewRow dgvr in grdData.Rows)
                        {
                            if (dgvr.Cells["Check"].Value.ToString().ToUpper() == "TRUE")
                            {
                                
                                i = 0;
                                double.TryParse(dgvr.Cells["JobID"].Value.ToString(), out i);
                                int.TryParse(dgvr.Cells["SplitSeq"].Value.ToString(), out SplitSeq);

                                string now = "";
                                // 8시 이전이면 전날짜도 삭제 되도록
                                if (ConvertDouble(DateTime.Now.ToString("HH")) < 8)
                                {
                                    now = DateTime.Now.AddDays(-1).ToString("yyyyMMdd");
                                }
                                else
                                {
                                    now = DateTime.Now.ToString("yyyyMMdd");
                                }

                                if (dgvr.Cells["WorkDate"].Value.ToString().Replace("-", "") == DateTime.Now.ToString("yyyyMMdd")
                                    || dgvr.Cells["WorkDate"].Value.ToString().Replace("-", "") == now)
                                {
                                    //후공정 이력이 없으면!!
                                    if (CheckIsNextWorkData(i) == true)
                                    {
                                        Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                                        sqlParameter.Add(Work_sResultWithMachineComProcess.JOBID, i);// grdData.Rows[rowIndex].Cells["JobID"].Value.ToString());
                                        sqlParameter.Add("SplitSeq", SplitSeq);// grdData.Rows[rowIndex].Cells["JobID"].Value.ToString());
                                        sqlParameter.Add(Work_sResultWithMachineComProcess.CREATEUSERID, Frm_tprc_Main.g_tBase.PersonID);
                                        sqlParameter.Add(Work_sResultWithMachineComProcess.SRTNMSG, "");

                                        //string[] sConfirm = new string[2];
                                        //sConfirm = DataStore.Instance.ExecuteProcedure("xp_prdWork_dWkResult", sqlParameter, true); //삭제
                                        //list_Confirm.Add(sConfirm[0]);
                                        //if (sConfirm[0].ToUpper() == "SUCCESS")
                                        //{ deleteCount++; }

                                        Dictionary<string, int> outputParam = new Dictionary<string, int>();
                                        outputParam.Add("sRtnMsg", 500);

                                        Dictionary<string, string> dicResult = DataStore.Instance.ExecuteProcedureOutputNoTran("xp_prdWork_dWkResult", sqlParameter, outputParam, true);
                                        string result = dicResult["sRtnMsg"];
                                        if ((result != string.Empty || result != "9999")
                                            && result.Equals(""))
                                        {
                                            deleteCount++;
                                        }
                                        else
                                        {
                                            ChkNextWorkData_Msg += (ChkNextWorkData_Msg.Length > 0 ? "\r\n" : "") + result;
                                        }
                                    }
                                }
                                else
                                {
                                    c++;
                                }
                            }
                        }
                        if (deleteCount > 0)//삭제결과 리스트
                        {
                            LogData.LogSave(this.GetType().Name, "D"); //2022-06-22 삭제
                            procQuery();
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

                            DataStore.Instance.CloseConnection(); //2021-10-07 DB 커넥트 연결 해제
                        }
                        else//삭제 결과리스트가 없음 > 삭제를 안했음
                        {
                            if (c > 0)
                            {
                                ErrorMsg += c.ToString() + "개 작업건수는 현재 날짜와 동일하지 않아 삭제할 수 없습니다.";
                            } 
                        }

                        // 후공정 이력 메세지가 존재한다면,
                        if (ChkNextWorkData_Msg.Length > 0)
                        {
                            ChkNextWorkData_Msg = "후공정, 검사, 출고 中 하나 이상의 이력이 있는경우 삭제가 불가능합니다." + ChkNextWorkData_Msg;

                            if (ErrorMsg.Length > 0)
                            {
                                ErrorMsg += "\r\n" + ChkNextWorkData_Msg;
                            }
                            else
                            {
                                ErrorMsg += ChkNextWorkData_Msg;
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
                            //MessageBox.Show(ErrorMsg);
                            WizCommon.Popup.MyMessageBox.ShowBox(ErrorMsg, "[삭제 불가]", 0, 1, 2);
                        }
                    }
                }
            }
        }

        #region 특정 문자 갯수 구하기

        public int WordCheck(string String, string Word)
        {
            string[] StringArray = String.Split(new string[] { Word }, StringSplitOptions.None);
            return (StringArray.Length - 1);
        }

        #endregion

        #region 해당 라벨의 후공정이 있는지 체크 하기, 있으면 삭제가 안되도록!

        private bool CheckIsNextWorkData(double JobID)
        {
            bool flag = false;

            try
            {
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();

                sqlParameter.Add("JobID", JobID);

                DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_prdWork_CheckIsNextWorkData", sqlParameter, false);

                if (dt != null 
                    && dt.Rows.Count > 0
                    && dt.Columns.Count == 1)
                {
                    string Msg = dt.Rows[0]["Result"].ToString().Trim();
                    
                    if (Msg.ToUpper().Equals("PASS"))
                    {
                        flag = true;
                    }
                    else
                    {
                        ChkNextWorkData_Msg += "\r\n" + Msg;
                        flag = false;
                    }
                }
                DataStore.Instance.CloseConnection(); //2021-10-07 DB 커넥트 연결 해제
                return flag;
            }
            catch (Exception excpt)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message), "[오류]", 0, 1);
                return false;
            }
        }

        #endregion

        #region wk_result StartSaveLabelID 로 삭제 시 이 라벨로 시작한게 있다면 그건 삭제가 불가능하도록

        // 설정된 설비와 툴 아이디로 작업수량, 설정수명
        // 조회 메서드
        private void CheckStartByLabel(string LabelID)
        {
            //try
            //{
            //    Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
            //    sqlParameter.Clear();

            //    sqlParameter.Add("LabelID", LabelID);

            //    DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_WizWork_sMCPartInfo", sqlParameter, false);


            //    if (dt != null && dt.Rows.Count > 0)
            //    {
            //        DataRow dr = dt.Rows[0];

            //        txtWorkQty.Text = stringFormatN0(dr["WorkQty"]).Equals("") ? "0" : stringFormatN0(dr["WorkQty"]);
            //        txtSetProdQty.Text = stringFormatN0(dr["setProdQty"]).Equals("") ? "0" : stringFormatN0(dr["setProdQty"]);
            //    }
            //}
            //catch (Exception excpt)
            //{
            //    WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message), "[오류]", 0, 1);
            //}
        }


        #endregion

        public void procQuery()
        {
            int nRecordCount = 0;
            grdData.Rows.Clear();
            Dictionary<string, object> sqlParameter = new Dictionary<string, object>();

            if (!chkResultDate.Checked)
            {
                sqlParameter.Add(Work_sResultWithMachineComProcess.CHKDATE, "0");
            }
            else
            {
                sqlParameter.Add(Work_sResultWithMachineComProcess.CHKDATE, "1");
            }

            sqlParameter.Add(Work_sResultWithMachineComProcess.SDATE, mtb_From.Text.Replace("-", ""));
            sqlParameter.Add(Work_sResultWithMachineComProcess.EDATE, mtb_To.Text.Replace("-", ""));

            if (cboProcess.SelectedIndex == 0)
            {
                sqlParameter.Add(Work_sResultWithMachineComProcess.CHKPROCESSID, "0");
                sqlParameter.Add(Work_sResultWithMachineComProcess.PROCESSID, this.cboProcess.SelectedValue.ToString());
                sqlParameter.Add(Work_sResultWithMachineComProcess.CHKMACHINEID, "0");
                sqlParameter.Add(Work_sResultWithMachineComProcess.MACHINEID, "0");
            }
            else
            {
                sqlParameter.Add(Work_sResultWithMachineComProcess.CHKPROCESSID, "1");
                sqlParameter.Add(Work_sResultWithMachineComProcess.PROCESSID, this.cboProcess.SelectedValue.ToString());
                if (cboMachine.SelectedIndex == 0)
                {
                    sqlParameter.Add(Work_sResultWithMachineComProcess.CHKMACHINEID, "0");
                    sqlParameter.Add(Work_sResultWithMachineComProcess.MACHINEID, "0");
                }
                else
                {
                    sqlParameter.Add(Work_sResultWithMachineComProcess.CHKMACHINEID, "1");
                    sqlParameter.Add(Work_sResultWithMachineComProcess.MACHINEID, cboMachine.SelectedValue.ToString());
                }
            }
            if (cboTeam.SelectedIndex == 0)
            {
                sqlParameter.Add(Work_sResultWithMachineComProcess.NCHKTEAMID, "0");
                sqlParameter.Add(Work_sResultWithMachineComProcess.STEAMID, cboTeam.SelectedValue.ToString());
            }
            else
            {
                sqlParameter.Add(Work_sResultWithMachineComProcess.NCHKTEAMID, "1");
                sqlParameter.Add(Work_sResultWithMachineComProcess.STEAMID, cboTeam.SelectedValue.ToString());
            }

            if (chkPLotID.Checked) //
            {
                sqlParameter.Add("nChkPLotID", "0"); //2021-03-30 DESKTOP-2RCHN60(컴퓨터 이름) 에선 1일 경우 조회를 못불러와서 0으로 변경해야됨, 서버에 올릴땐 1로 변경
                sqlParameter.Add("sPLotID", txtPLotID.Text.Trim().ToString());
            }
             
            if (cboJobGbn.Text == "전체")//'':전체,1:정상,2:무작업,3:재작업
            {
                sqlParameter.Add(Work_sResultWithMachineComProcess.SJOBGBN, "");
            }
            else if (cboJobGbn.Text == "정상")
            {
                sqlParameter.Add(Work_sResultWithMachineComProcess.SJOBGBN, "1");
            }
            else if (cboJobGbn.Text == "무작업")
            {
                sqlParameter.Add(Work_sResultWithMachineComProcess.SJOBGBN, "2");
            }
            else if (cboJobGbn.Text == "재작업")
            {
                sqlParameter.Add(Work_sResultWithMachineComProcess.SJOBGBN, "3");
            }

            if (Frm_tprc_Main.g_tBase.ResablyID == null
                || Frm_tprc_Main.g_tBase.PersonID == null)
            {
                Frm_tprc_Main.g_tBase.ResablyID = "";
            }

            sqlParameter.Add("ResablyID", Frm_tprc_Main.g_tBase.ResablyID.ToString());
            sqlParameter.Add("PersonID", Frm_tprc_Main.g_tBase.PersonID.ToString());

            sqlParameter.Add("nBuyerArticleNo", chkBuyerArticleNo.Checked == true ? 1: 0);
            sqlParameter.Add("BuyerArticleNo", chkBuyerArticleNo.Checked == true && txtBuyerArticleNo.Text.Trim().Length > 0 ? txtBuyerArticleNo.Text : "");

            ds = DataStore.Instance.ProcedureToDataSet("xp_prdWork_sWkResult", sqlParameter, false);
            IFormatProvider KR_Format = new System.Globalization.CultureInfo("ko-KR", true);
            if (ds.Tables[0].Rows.Count > 0)
            {
                double douWorkQty = 0;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = ds.Tables[0].Rows[i];
                    string sDate = string.Empty;
                    string sStartTime = string.Empty;
                    string sEndTime = string.Empty;
                    double.TryParse(dr[Work_sResultWithMachineComProcess.WORKQTY].ToString(), out douWorkQty);
                    if (dr["WorkDate"].ToString().Length == 8)
                    {
                        sDate = dr["WorkDate"].ToString();
                        sDate = string.Format(sDate.Substring(0, 4) + "-" + sDate.Substring(4, 2) + "-" + sDate.Substring(6), "yyyy-MM-dd");
                    }
                    if (dr["WorkStartTime"].ToString().Length == 6)
                    {
                        sStartTime = dr["WorkStartTime"].ToString();
                        sStartTime = string.Format(sStartTime.Substring(0, 2) + ":" + sStartTime.Substring(2, 2), "HHmm");
                    }
                    if (dr["WorkEndTime"].ToString().Length == 6)
                    {
                        sEndTime = dr["WorkEndTime"].ToString();
                        sEndTime = string.Format(sEndTime.Substring(0, 2) + ":" + sEndTime.Substring(2, 2), "HHmm");
                    } 
                    
                    grdData.Rows.Add(
                        false,
                        i+1,
                        dr[Work_sResultWithMachineComProcess.JOBGBNNAME],           //작업구분
                        dr["DayNight"].ToString(),
                        sDate,
                        sStartTime,
                        sEndTime,

                        dr[Work_sResultWithMachineComProcess.PROCESS],          //공정
                        dr[Work_sResultWithMachineComProcess.MACHINENO],        //호기
                        dr[Work_sResultWithMachineComProcess.BOXID],            //박스번호
                        dr[Work_sResultWithMachineComProcess.LOTID],            //지시번호
                        
                       
                        dr["OrderArticleID"].ToString().Trim(),
                        dr[Work_sResultWithMachineComProcess.BUYERARTICLENO],   //품번
                        dr[Work_sResultWithMachineComProcess.ARTICLE],          //품명
                        dr[Work_sResultWithMachineComProcess.KCUSTOM],          //거래처
                        dr["BuyerModel"].ToString(),                            //차종
                        dr[Work_sResultWithMachineComProcess.ORDERNO],          //오더번호
                       
                        string.Format("{0:n0}", douWorkQty),                    //작업량
                        dr["UnitClss"].ToString(),                              //단위
                        dr[Work_sResultWithMachineComProcess.ORDERID],          //관리번호
                        dr[Work_sResultWithMachineComProcess.TEAM],             //작업조
                        dr[Work_sResultWithMachineComProcess.WORKMANNAME],      //작업자


                        dr[Work_sResultWithMachineComProcess.NOREWORKCODENAME], //무작업사유 

                        dr[Work_sResultWithMachineComProcess.JOBID], //JobID 
                        dr["SplitSeq"].ToString());

                    string hoit = dr["JobID"].ToString();

                    if (grdData.Rows[i].Cells["JobGbnName"].Value.ToString() == "무작업")
                    {
                        grdData.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(238, 108, 128);
                    }
                }
                grdData.ClearSelection();

                grdData[0, 0].Selected = true; //0번째 행 선택
                //grdData.AutoResizeColumns();
                //grdData.Columns["RowSeq"].Width = 100;
                double Total = 0;
                for (int i = 0; i < grdData.Rows.Count; i++)
                {
                    Total += Convert.ToDouble(grdData.Rows[i].Cells["WorkQty"].Value);
                }
                nRecordCount = grdData.RowCount; //현재 행의 갯수
                Frm_tprc_Main.gv.queryCount = string.Format("{0:n0}", nRecordCount);
                Frm_tprc_Main.gv.SetStbInfo();
            }

            FillGrdSum();
            DataStore.Instance.CloseConnection(); //2021-10-07 DB 커넥트 연결 해제

        }

        public void procDelete(double i)
        {
            if(grdData.Rows.Count > 0 && grdData.SelectedRows.Count > 0)
            {
                    try
                    {
                        Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                        sqlParameter.Add(Work_sResultWithMachineComProcess.JOBID, i);// grdData.Rows[rowIndex].Cells["JobID"].Value.ToString());
                        sqlParameter.Add(Work_sResultWithMachineComProcess.CREATEUSERID, Frm_tprc_Main.g_tBase.PersonID);
                        sqlParameter.Add(Work_sResultWithMachineComProcess.SRTNMSG, "");
                        DataStore.Instance.ExecuteProcedure("xp_wkResult_dWkResult", sqlParameter, true); //삭제
                    }
                    catch (Exception ex)
                    {
                        //WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", ex.Message), "[오류]", 0, 1);
                    }
            }
        }

        //콤보박스 데이터 바인딩
        private void SetComboBox()
        {
            //cboJobGbn.SelectedIndex = 0;
            string strProcessID = "";

            strProcessID =Frm_tprc_Main.gs.GetValue("Work", "ProcessID", "ProcessID");
            string[] gubunProcess = strProcessID.Split(new char[] { '|' });

            strProcessID = string.Empty;

            for (int i = 0; i < gubunProcess.Length; i++)
            {
                if (strProcessID.Equals(string.Empty))
                {
                    strProcessID = gubunProcess[i];
                }
                else
                {
                    strProcessID = strProcessID + "|" + gubunProcess[i];
                }
            }

            //공정 가져오기
            Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
            sqlParameter.Add(Work_sProcess.NCHKPROC, "1");//cboProcess.Text 
            sqlParameter.Add(Work_sProcess.PROCESSID, strProcessID);//cboProcess.Text
            ds = DataStore.Instance.ProcedureToDataSet("[xp_Work_sProcess]", sqlParameter, false);
            DataRow newRow = ds.Tables[0].NewRow();
            newRow[Work_sProcess.PROCESSID] = "*";
            newRow[Work_sProcess.PROCESS] = "전체";
            ds.Tables[0].Rows.InsertAt(newRow, 0);
            cboProcess.DataSource = ds.Tables[0];
            cboProcess.ValueMember = Work_sProcess.PROCESSID;
            cboProcess.DisplayMember = Work_sProcess.PROCESS;

            //작업조 가져오기
            ds = DataStore.Instance.ProcedureToDataSet("xp_Code_sTeam", null, false);
            DataRow newRow1 = ds.Tables[0].NewRow();
            newRow1[Code_sTeam.TEAMID] = "*";
            newRow1[Code_sTeam.TEAM] = "전체";
            ds.Tables[0].Rows.InsertAt(newRow1, 0);
            cboTeam.DataSource = ds.Tables[0];
            cboTeam.ValueMember = Code_sTeam.TEAMID;
            cboTeam.DisplayMember = Code_sTeam.TEAM;
            //GetInsTypeDataSource(cboTeam);

            //Dictionary<string, object> sqlParameter = new Dictionary<string, object>();



            //sqlParameter.Add(Work_sProcess.PROCESSID, this.cboProcess.SelectedValue.ToString());
            //ds = DataStore.Instance.ProcedureToDataSet("xp_Work_sMachinebyProcess", sqlParameter, false);
            //DataStore.Instance.CloseConnection();

            //this.cboMachine.DataSource = null;

            //this.cboMachine.DataSource = ds.Tables[0];

            ////CHKPROCESSID
            //this.cboMachine.ValueMember = Work_sMachineByProcess.MACHINEID;
            //this.cboMachine.DisplayMember = Work_sMachineByProcess.MACHINE;



            //ds = DataStore.Instance.ExecuteDataSet("xp_Code_sTeam", null, false);
            //DataStore.Instance.CloseConnection();

            //this.cboTeam.DataSource = null;

            //this.cboTeam.DataSource = ds.Tables[0];
            //this.cboTeam.ValueMember = Code_sTeam.TEAMID;
            //this.cboTeam.DisplayMember = Code_sTeam.TEAM;



            //ds = DataStore.Instance.ExecuteDataSet("xp_Code_sTeam", null, false);
            //DataStore.Instance.CloseConnection();

            //this.cboJobGbn.DataSource = null;

            //this.cboJobGbn.DataSource = ds.Tables[0];
            //this.cboJobGbn.ValueMember = Code_sTeam.TEAMID;
            //this.cboJobGbn.DisplayMember = Code_sTeam.TEAM;


            ////콤보박스 Y, N 값 추가
            //DataTable dtUseYN = new DataTable();
            //dtUseYN.Columns.Add(Work_sProcess.PROCESSID);
            //dtUseYN.Columns.Add(Work_sProcess.PROCESS);
            //dtUseYN.Rows.Add("Y", "Y");
            //dtUseYN.Rows.Add("N", "N");

            //DataGridViewComboBoxColumn colUseYN = dgvMenuList.Columns[com_Menu.USEYN] as DataGridViewComboBoxColumn;
            //colUseYN.DataSource = dtUseYN;
            //colUseYN.DisplayMember = com_Code.CODENAME;
            //colUseYN.ValueMember = com_Code.SCODE;

            ////콤보박스 디자인
            //colUseYN.FlatStyle = FlatStyle.Flat;

            DataStore.Instance.CloseConnection(); //2021-10-07 DB 커넥트 연결 해제

        }

        //공정에 따른 기계이름 가져오기
        private void cboProcess_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strProcess = cboProcess.SelectedValue.ToString();
            Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
            sqlParameter.Add(Work_sProcess.PROCESSID, strProcess);//cboProcess.Text 

            DataTable dt  = DataStore.Instance.ProcedureToDataTable("xp_Work_sMachinebyProcess", sqlParameter, false);
            DataTable dt2 = dt.Clone();

            string[] sMachineID = null;
            sMachineID =Frm_tprc_Main.gs.GetValue("Work", "Machine", "Machine").Split('|');//배열에 설비아이디 넣기
            List<string> sMachine = new List<string>();
            foreach (string str in sMachineID)
            {
                sMachine.Add(str);
            }
            sMachineID = null;
            bool chkOK = false;
            //ini값과 같으면 저장
            foreach (DataRow dr in dt.Rows)
            {
                chkOK = false;
                foreach (string Mac in sMachine)
                {
                    if (Mac.Length > 4)
                    {
                        if (Mac.Substring(0, 4) == strProcess)
                        {
                            if (Mac.Substring(4, 2) == dr["MachineID"].ToString())
                            {
                                chkOK = true;
                                dt2.Rows.Add(dr.ItemArray);
                                break;
                            }
                        }
                    }
                }
                if (!chkOK)
                {
                    sMachine.Remove(strProcess + dr["MachineID"].ToString());
                }
            }
            DataRow newRow = dt2.NewRow();
            newRow[Work_sMachineByProcess.MACHINEID] = "*";
            newRow[Work_sMachineByProcess.MACHINENO] = "전체";
            dt2.Rows.InsertAt(newRow, 0);
            cboMachine.DataSource = dt2;
            cboMachine.ValueMember = Work_sMachineByProcess.MACHINEID;
            cboMachine.DisplayMember = Work_sMachineByProcess.MACHINENO;
            if (dt2.Rows.Count > 1)
            {
                cboMachine.SelectedIndex = 0;
            }
            dt = null;
            DataStore.Instance.CloseConnection(); //2021-10-07 DB 커넥트 연결 해제
        }

       
        //위 버튼, 그리드뷰 선택된 셀에서 위로 이동
        private void cmdRowUp_Click(object sender, EventArgs e)
        {
            Frm_tprc_Main.Lib.btnRowUp(grdData, z);
        }
        //아래 버튼, 그리드뷰 선택된 셀에서 아래로 이동
        private void cmdRowDown_Click(object sender, EventArgs e)
        {
            Frm_tprc_Main.Lib.btnRowDown(grdData, z);      
        }

        private void SetDateTime()
        {
            ////ini 날짜 불러와서 기간 설정하기
            chkResultDate.Checked = true;
            int Days = 0;
            string[] sInstDate =Frm_tprc_Main.gs.GetValue("Work", "Screen", "Screen").Split('|');
            foreach (string str in sInstDate)
            {
                string[] Value = str.Split('/');
                if (this.Name.ToUpper().Contains(Value[0].ToUpper()))
                {
                    int.TryParse(Value[1], out Days);
                    break;
                }
            }
            mtb_From.Text = DateTime.Today.AddDays(-Days).ToString("yyyyMMdd");
            mtb_To.Text = DateTime.Today.ToString("yyyyMMdd");
            
        }


        private void Frm_tprc_Result_Load(object sender, EventArgs e)
        {
            LogData.LogSave(this.GetType().Name, "S"); //2022-06-22 사용시간(로드, 닫기)
            SetScreen();
            SetDateTime();
            SetComboBox();
            cboJobGbn.SelectedIndex = 0;
            InitGrid();

            txtPLotID.Text = gs.GetValue("Work", "SetLOTID", "");
            if (txtPLotID.Text != string.Empty)
            {
                //chkPLotID.Checked = true;
                procQuery();
            }
            else
            {
                //chkPLotID.Checked = false;
                procQuery();
            }
            LogData.LogSave(this.GetType().Name, "R"); //2022-06-22 조회
        }

        #region Default Grid Setting

        private void InitGrid()
        {
            grdData.Columns.Clear();
            grdData.ColumnCount = 24;

            int n = 0;

            grdData.Columns[n].Name = "RowSeq";
            grdData.Columns[n].HeaderText = "No";
            grdData.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            grdData.Columns[n++].Visible = true;

            grdData.Columns[n].Name = "JobGbnName";
            grdData.Columns[n].HeaderText = "구분";
            grdData.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            grdData.Columns[n++].Visible = false;

            grdData.Columns[n].Name = "DayNight";
            grdData.Columns[n].HeaderText = "주간" + "\r\n" + "야간";
            grdData.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[n++].Visible = true;

            grdData.Columns[n].Name = "WorkDate";
            grdData.Columns[n].HeaderText = "작업집계일";
            grdData.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[n++].Visible = true;

            grdData.Columns[n].Name = "WorkStartTime";
            grdData.Columns[n].HeaderText = "작업"+"\r\n" +"시작";
            grdData.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[n++].Visible = true;

            grdData.Columns[n].Name = "WorkEndTime";
            grdData.Columns[n].HeaderText = "작업" + "\r\n" + "종료";
            grdData.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[n++].Visible = true;

            grdData.Columns[n].Name = "Process";
            grdData.Columns[n].HeaderText = "공정";
            grdData.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            grdData.Columns[n++].Visible = true;

            grdData.Columns[n].Name = "MachineNo";
            grdData.Columns[n].HeaderText = "호기";
            grdData.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[n++].Visible = true;

            grdData.Columns[n].Name = "BoxID";
            grdData.Columns[n].HeaderText = "이동전표";
            grdData.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[n++].Visible = true;

            grdData.Columns[n].Name = "LotID";
            grdData.Columns[n].HeaderText = "지시LOTID";
            grdData.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[n++].Visible = false;

            grdData.Columns[n].Name = "OrderArticleID";
            grdData.Columns[n].HeaderText = "품목코드";
            grdData.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[n++].Visible = false;

            grdData.Columns[n].Name = "BuyerArticleNo";
            grdData.Columns[n].HeaderText = "품번";
            grdData.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            grdData.Columns[n++].Visible = true;

            grdData.Columns[n].Name = "Article";
            grdData.Columns[n].HeaderText = "품명";
            grdData.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            grdData.Columns[n++].Visible = true;

            grdData.Columns[n].Name = "KCUSTOM";
            grdData.Columns[n].HeaderText = "거래처";
            grdData.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            grdData.Columns[n++].Visible = true;

            grdData.Columns[n].Name = "BuyerModel";
            grdData.Columns[n].HeaderText = "차종";
            grdData.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            grdData.Columns[n++].Visible = true;

            grdData.Columns[n].Name = "OrderNO";
            grdData.Columns[n].HeaderText = "OrderNo";
            grdData.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[n++].Visible = false;            

            grdData.Columns[n].Name = "WorkQty";
            grdData.Columns[n].HeaderText = "작업량";
            grdData.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            grdData.Columns[n++].Visible = true;

            grdData.Columns[n].Name = "UnitClss";
            grdData.Columns[n].HeaderText = "단위";
            grdData.Columns[n++].Visible = false;
            
            grdData.Columns[n].Name = "OrderID";
            grdData.Columns[n].HeaderText = "접수번호";
            grdData.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[n++].Visible = false;

            grdData.Columns[n].Name = "Team";
            grdData.Columns[n].HeaderText = "작업조";
            grdData.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[n++].Visible = false;

            grdData.Columns[n].Name = "WorkManName";
            grdData.Columns[n].HeaderText = "작업자";
            grdData.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[n++].Visible = true;

            grdData.Columns[n].Name = "NoReworkCodeName";
            grdData.Columns[n].HeaderText = "무작업사유";
            grdData.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[n++].Visible = true;

            grdData.Columns[n].Name = "JobID";
            grdData.Columns[n].HeaderText = "JobID";
            grdData.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            grdData.Columns[n++].Visible = false;

            grdData.Columns[n].Name = "SplitSeq";
            grdData.Columns[n].HeaderText = "SplitSeq";
            grdData.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            grdData.Columns[n++].Visible = false;

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
            grdData.Columns.Insert(0, chkCol);

            grdData.Font = new Font("맑은 고딕", 12, FontStyle.Bold);
            grdData.RowTemplate.Height = 37;
            grdData.ColumnHeadersHeight = 35;
            grdData.ScrollBars = ScrollBars.Both;
            grdData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grdData.ReadOnly = true;
            grdData.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(234, 234, 234);
            grdData.ScrollBars = ScrollBars.Both;
            grdData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grdData.MultiSelect = false;

            foreach (DataGridViewColumn col in grdData.Columns)
            {
                col.DataPropertyName = col.Name;
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            }
            return;
        }

        private void FillGrdSum()
        {
            double douMissingWorkSum = 0;
            int WorkCount = 0;
            double douWorkQty = 0;
            WorkCount = grdData.Rows.Count;

            for (int i = 0; i < WorkCount; i++)
            {
                douMissingWorkSum = douMissingWorkSum + double.Parse(grdData.Rows[i].Cells["WorkQty"].Value.ToString());
            }
            
            DataTable dt = new DataTable();
            dt.Columns.Add("WorkSumText".ToString());
            dt.Columns.Add("WorkCount".ToString());
            dt.Columns.Add("WorkSum".ToString());
            //dt.Columns.Add("Space".ToString());
            DataRow dr = dt.NewRow();
            dr["WorkSumText"] = "작업 합계";
            dr["WorkCount"] = string.Format("{0:n0}", WorkCount) + " 건";
            dr["WorkSum"] = string.Format("{0:n0}", douMissingWorkSum);   // 소수점 없애달래.ㅇㅇ...         
            //dr["Space"] = "";
            dt.Rows.Add(dr);

            grdSum.DataSource = dt;
            grdSum.Columns[0].DefaultCellStyle.BackColor = Color.FromArgb(234, 234, 234);

            grdSum.Font = new Font("맑은 고딕", 12, FontStyle.Bold);
            grdSum.RowTemplate.Height = 40;
            grdSum.ColumnHeadersHeight = 35;
            grdSum.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            grdSum.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grdSum.ReadOnly = true;
            for (int i = 0; i < grdSum.SelectedCells.Count; i++)
            {
                grdSum.SelectedCells[i].Selected = false;
            }
        }

        #endregion

        private void cboProcess_Click(object sender, EventArgs e)
        {
            SetComboBox();
        }


        private void txtPLotID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (txtPLotID.Text.ToUpper().Contains("PL") && (txtPLotID.Text.Trim().Length == 15 || txtPLotID.Text.Trim().Length == 16))
                {
                    chkPLotID.Checked = true;
                    procQuery();
                    LogData.LogSave(this.GetType().Name, "R"); //2022-06-22 조회
                }
                else
                {
                    WizCommon.Popup.MyMessageBox.ShowBox("작업지시목록 LotID가 아닙니다. 작업지시목록에 있는 바코드를 스캔해주세요!", "[바코드 오류]", 2, 1);
                    return;
                }
            }
        }

        private void chkPLotID_Click(object sender, EventArgs e)
        {
            if (chkPLotID.Checked)
            {
                txtPLotID.Text = "";
                POPUP.Frm_CMKeypad keypad = new POPUP.Frm_CMKeypad("LOTID입력", "LOTID");

                keypad.Owner = this;
                if (keypad.ShowDialog() == DialogResult.OK)
                {
                    txtPLotID.Text = keypad.tbInputText.Text;
                }
            }
            else
            {
                txtPLotID.Text = "";
            }
        }

        private void chkBuyerArticleNo_Click(object sender, EventArgs e)
        {
            if (chkBuyerArticleNo.Checked)
            {
                try
                {
                    //2021-07-20
                    var path64 = System.IO.Path.Combine(Directory.GetDirectories(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "winsxs"), "amd64_microsoft-windows-osk_*")[0], "osk.exe");
                    var path32 = @"C:\windows\system32\osk.exe";
                    var path = (Environment.Is64BitOperatingSystem) ? path64 : path32;
                    if (File.Exists(path) && !Frm_tprc_Main.Lib.ReturnKillRunningProcess("osk"))
                    {
                        System.Diagnostics.Process.Start(path);

                        txtBuyerArticleNo.Focus();

                    }
                }
                catch (Exception ex)
                {                   
                    useMasicKeyboard(txtBuyerArticleNo);
                }
                //useMasicKeyboard(txtBuyerArticleNo);
            }
            else
            {
                txtBuyerArticleNo.Text = "";
            }
        }

        private void txtBuyerArticleNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                chkBuyerArticleNo.Checked = true;
                procQuery();
                LogData.LogSave(this.GetType().Name, "R"); //2022-06-22 조회
            }
        }

        #region GLS 2020.09.14 특수문자도 입력할 수 있도록 요청

        private void useMasicKeyboard(TextBox txtBox)
        {
            try
            {
                if (txtBox == null) { return; }
                txtBox.Text = "";

                //실행중인 프로세스가 없을때 
                if (!Frm_tprc_Main.Lib.ReturnKillRunningProcess("osk"))
                {
                    System.Diagnostics.Process ps = new System.Diagnostics.Process();
                    ps.StartInfo.FileName = "osk.exe";
                    ps.Start();

                    //string progFiles = @"C:\Program Files\Common Files\Microsoft Shared\ink";
                    //string keyboardPath = Path.Combine(progFiles, "TabTip.exe");

                    //Process.Start(keyboardPath);
                }
            }
            catch (Exception ex)
            {
                try
                {
                    Console.Write(ex.Message);
                    //WizCommon.Popup.MyMessageBox.ShowBox("관리자에게 문의해주세요.", "[매직 키보드 실행 오류]", 2, 1);
                    System.Diagnostics.Process.Start(@"C:\Windows\winsxs\x86_microsoft-windows-osk_31bf3856ad364e35_6.1.7601.18512_none_acc225fbb832b17f\osk.exe");
                }
                catch (Exception ex2)
                {
                    Console.Write(ex2.Message);
                }
            }
            txtBox.Select();
            txtBox.Focus();
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            LogData.LogSave(this.GetType().Name, "S"); //2022-06-22 사용시간(로드, 닫기)
            Close();
        }

        private void btnColRight_Click(object sender, EventArgs e)
        {
            z = Frm_tprc_Main.Lib.btnColRight(grdData, z);
        }

        private void btnColLeft_Click(object sender, EventArgs e)
        {
            z = Frm_tprc_Main.Lib.btnColLeft(grdData, z);
        }

        private void Frm_tprc_Result_Activated(object sender, EventArgs e)
        {
            ((Frm_tprc_Main)(MdiParent)).LoadRegistry();
            txtPLotID.Text = gs.GetValue("Work", "SetLOTID", "");
            if (txtPLotID.Text != string.Empty)
            {
                chkPLotID.Checked = true;
                procQuery();
            }
            else
            {
                chkPLotID.Checked = false;
                procQuery();
            }
        }

        private void grdData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void grdData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (grdData.Rows[e.RowIndex].Cells["Check"].Value.ToString().ToUpper() == "FALSE")
                {
                    grdData.Rows[e.RowIndex].Cells["Check"].Value = true;
                }
                else if (grdData.Rows[e.RowIndex].Cells["Check"].Value.ToString().ToUpper() == "TRUE")
                {
                    grdData.Rows[e.RowIndex].Cells["Check"].Value = false;
                }
            }
            
        }

        #region 기타 메서드 모음

        // 천마리 콤마, 소수점 버리기
        private string stringFormatN0(object obj)
        {
            return string.Format("{0:N0}", obj);
        }

        // 천마리 콤마, 소수점 버리기
        private string stringFormatN1(object obj)
        {
            return string.Format("{0:N0}", obj);
        }

        // 천마리 콤마, 소수점 두자리
        private string stringFormatN2(object obj)
        {
            return string.Format("{0:N2}", obj);
        }

        // 천마리 콤마, 소수점 두자리
        private string stringFormatNDigit(object obj, int digit)
        {
            return string.Format("{0:N" + digit + "}", obj);
        }

        // 데이터피커 포맷으로 변경
        private string DatePickerFormat(string str)
        {
            string result = "";

            if (str.Length == 8)
            {
                if (!str.Trim().Equals(""))
                {
                    result = str.Substring(0, 4) + "-" + str.Substring(4, 2) + "-" + str.Substring(6, 2);
                }
            }

            return result;
        }

        // 시간 형식 6글자라면! 11:11:11
        private string DateTimeFormat(string str)
        {
            str = str.Replace(":", "").Trim();

            if (str.Length == 6)
            {
                string Hour = str.Substring(0, 2);
                string Min = str.Substring(2, 2);
                string Sec = str.Substring(4, 2);

                str = Hour + ":" + Min + ":" + Sec;
            }

            return str;
        }

        // 시간 분 → 11:12 형식으로 변환
        private string DateTimeMinToTime(string str)
        {
            str = str.Replace(":", "").Trim();

            int num = 0;
            if (int.TryParse(str, out num) == true)
            {
                string hour = (num / 60).ToString();
                string min = (num % 60).ToString();

                if (min.Length == 1)
                {
                    min = "0" + min;
                }

                str = hour + ":" + min;
            }

            return str;
        }

        // Int로 변환
        private int ConvertInt(string str)
        {
            int result = 0;
            int chkInt = 0;

            if (!str.Trim().Equals(""))
            {
                str = str.Replace(",", "");

                if (Int32.TryParse(str, out chkInt) == true)
                {
                    result = Int32.Parse(str);
                }
            }

            return result;
        }

        // 소수로 변환 가능한지 체크 이벤트
        private bool CheckConvertDouble(string str)
        {
            bool flag = false;
            double chkDouble = 0;

            if (!str.Trim().Equals(""))
            {
                if (Double.TryParse(str, out chkDouble) == true)
                {
                    flag = true;
                }
            }

            return flag;
        }

        // 숫자로 변환 가능한지 체크 이벤트
        private bool CheckConvertInt(string str)
        {
            bool flag = false;
            int chkInt = 0;

            if (!str.Trim().Equals(""))
            {
                str = str.Trim().Replace(",", "");

                if (Int32.TryParse(str, out chkInt) == true)
                {
                    flag = true;
                }
            }

            return flag;
        }

        // 소수로 변환
        private double ConvertDouble(string str)
        {
            double result = 0;
            double chkDouble = 0;

            if (!str.Trim().Equals(""))
            {
                str = str.Replace(",", "");

                if (Double.TryParse(str, out chkDouble) == true)
                {
                    result = Double.Parse(str);
                }
            }

            return result;
        }


        #endregion

        
    }
}
