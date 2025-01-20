using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WizWork.Tools;
using WizWork.Properties;
using WizCommon;
using System.Diagnostics;

namespace WizWork
{
    public partial class Frm_tprc_NoWork_U : Form
    {
        WizWorkLib Lib = new WizWorkLib();
        LogData LogData = new LogData(); //2022-06-21 log 남기는 함수

        private DataSet ds = null;
        string[] Message = new string[2];
        string sProcessID = "";
        string sMachineID = "";
        string sPersonID = "";
        string sTeam = "";
        string sUserID = "";

        public bool blClose = false;
        public Frm_tprc_NoWork_U()
        {
            InitializeComponent();
        }

        private void Frm_tprc_NoWork_U_Load(object sender, EventArgs e)
        {
            LogData.LogSave(this.GetType().Name, "S"); //2022-06-22 사용시간(로드, 닫기)
            cmdsetProcess.Text = "공정\r\n작업자\r\n변경";
            SetInfo();                      // 작업자설정 팝업 로드
            SetScreen();
            SetDateTimePicker();
            //SetFormDataClear();
            if (!blClose)
            {
                SetComboBox();               // 콤보박스 설정
                SetFormDataClear();
                if (sUserID != "")
                {
                    sProcessID = Frm_tprc_Main.g_tBase.ProcessID;
                    sMachineID = Frm_tprc_Main.g_tBase.MachineID;
                    sUserID = Frm_tprc_Main.g_tBase.PersonID;

                    // 2020.11.20, GDU → 공정 작업자 버튼을 통해서 변경해도 변경이 안된다고 하여, 확인용으로 추가.
                    txtProcessID.Text = Frm_tprc_Main.g_tBase.ProcessID;
                    txtProcess.Text = Frm_tprc_Main.g_tBase.Process;
                    txtMachineID.Text = Frm_tprc_Main.g_tBase.MachineID;
                    txtMachine.Text = Frm_tprc_Main.g_tBase.Machine;
                    txtPersonID.Text = Frm_tprc_Main.g_tBase.PersonID;
                    txtPerson.Text = Frm_tprc_Main.g_tBase.Person;
                    txtDayOrNightID.Text = Frm_tprc_Main.g_tBase.DayOrNightID;
                    txtDayOrNight.Text = Frm_tprc_Main.g_tBase.DayOrNight;
                }
            }

            //setComponentVisible();

            txtBarCodeScan.Select();
            txtBarCodeScan.Focus();

            // 작업자용으로 ProcessID, MachineID, PersonID 숨김
            txtProcessID.Visible = false;
            txtMachineID.Visible = false;
            txtPersonID.Visible = false;
            txtDayOrNightID.Visible = false;
        }

        private void setComponentVisible()
        {
            // 지시LotID ~ 차종까지 안보이도록
            tableLayoutPanel2.RowStyles[0].Height = 25;
            tableLayoutPanel2.RowStyles[1].Height = 0;
            tableLayoutPanel2.RowStyles[2].Height = 0;
            tableLayoutPanel2.RowStyles[3].Height = 0;
            tableLayoutPanel2.RowStyles[4].Height = 0;
            tableLayoutPanel2.RowStyles[5].Height = 0;
            tableLayoutPanel2.RowStyles[6].Height = 25;

            //label1.BackColor = Color.LightGray;
            //label1.ForeColor = Color.Black;

            chkPLotID.Visible = false;
            txtBarCodeScan.Visible = false;
            label5.Visible = false;
            lblPLotID.Visible = false;
            label23.Visible = false;
            lblCustom.Visible = false;
            label23.Visible = false;
            lblCustom.Visible = false;
            label22.Visible = false;
            lblArticle.Visible = false;
            label7.Visible = false;
            lblBuyerArticleNo.Visible = false;
            label2.Visible = false;
            lblBuyerModel.Visible = false;
        }

        private void SetInfo()
        {
            sProcessID = Frm_tprc_Main.g_tBase.ProcessID;//setProcess폼에서 선택한 ProcessID
            sMachineID = Frm_tprc_Main.g_tBase.MachineID;//setProcess폼에서 선택한 MachineID
            sPersonID = Frm_tprc_Main.g_tBase.PersonID;//setProcess폼에서 선택한 PersonID
            sTeam = Frm_tprc_Main.g_tBase.TeamID;//setProcess폼에서 선택한 TeamID
            sUserID = Frm_tprc_Main.g_tBase.PersonID;//setProcess폼에서 선택한 UserID
        }

        private void SetFormDataClear()
        {
            txtBarCodeScan.Text = "";   
            lblCustom.Text = "";
            lblCustom.Tag = "";
            lblArticle.Text = "";
            lblArticle.Tag = "";
            lblPLotID.Text = "";
            lblPLotID.Tag = "";
            lblBuyerArticleNo.Text = "";
            lblBuyerModel.Text = "";
            cboNoWorkCode.SelectedIndex = 0;           
            txtComments.Text = "";
            txtBarCodeScan.Focus();
        }

        private void SetComboBox()
        {
            try
            {
                ds = DataStore.Instance.ProcedureToDataSet("xp_WizWork_sNoWorkCodeReason", null, false);
                cboNoWorkCode.DataSource = ds.Tables[0];
                DataRow newRow = ds.Tables[0].NewRow();
                newRow[WizWork_sNoWorkCodeReason.CODE_ID] = "";
                newRow[WizWork_sNoWorkCodeReason.CODE_NAME] = "";// Resources.CMB_VALUE_OPTION_ALL;
                ds.Tables[0].Rows.InsertAt(newRow, 0);

                cboNoWorkCode.ValueMember = WizWork_sNoWorkCodeReason.CODE_ID;
                cboNoWorkCode.DisplayMember = WizWork_sNoWorkCodeReason.CODE_NAME;
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
        private void cmdBarCodeScan_Click(object sender, EventArgs e)
        {
            WizCommon.Popup.Frm_CMKeypad FK = new WizCommon.Popup.Frm_CMKeypad(txtBarCodeScan.Text.Trim(), "생산LOT ID");
            FK.Owner = this;
            if (FK.ShowDialog() == DialogResult.OK)
            {
                if ((txtBarCodeScan.Text.Length == 15 || txtBarCodeScan.Text.Length == 16) && txtBarCodeScan.Text.ToUpper().Contains("PL"))
                {
                    txtBarCodeScan.Text = FK.tbInputText.Text.Trim();
                }
                else
                {
                    txtBarCodeScan.Text = "";
                }
            }

            if (this.txtBarCodeScan.Text != "")
            {
                LF_GetBarcodeData();
            }
        }


        public void LF_GetBarcodeData()
        {
            try
            {
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Add("LotID", txtBarCodeScan.Text.Trim());

                ds = DataStore.Instance.ProcedureToDataSet("xp_wkNoWork_sLotID", sqlParameter, false);

                if (ds.Tables[0].Rows.Count == 1)
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    lblPLotID.Text = txtBarCodeScan.Text.Trim().ToUpper();
                    lblArticle.Tag = dr["ArticleID"].ToString().Trim();
                    lblArticle.Text = dr["Article"].ToString().Trim();
                    lblCustom.Text = dr["KCustom"].ToString().Trim();
                    lblPLotID.Tag = dr["InstID"].ToString().Trim();
                    lblCustom.Tag = dr["InstDetSeq"].ToString().Trim();
                    lblBuyerArticleNo.Text = dr["BuyerArticleNo"].ToString().Trim();
                    lblBuyerModel.Text = dr["BuyerModel"].ToString().Trim();
                    txtBarCodeScan.Text = "";
                }
                else
                {
                    Message[0] = "[지시LOTID 입력오류]";
                    Message[1] = "존재하지 않는 지시LotID입니다.\r\n확인하여주시기 바랍니다.";
                    txtBarCodeScan.Text = "";
                    throw new Exception();
                }
            }
            catch (Exception excpt)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
            }
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
        private void cmdExit_Click(object sender, EventArgs e)
        {
            LogData.LogSave(this.GetType().Name, "S"); //2022-06-22 사용시간(로드, 닫기)
            Close();
        }

        private bool CheckData()
        {
            try
            {
                double StartTime = 0; //2021-07-21 자리수로 인해 float를 double로 변경
                double EndTime = 0;   //2021-07-21 자리수로 인해 float를 double로 변경
                //if (lblPLotID.Text == "")
                //{
                //    Message[0] = "[지시LOTID 입력오류]";
                //    Message[1] = "지시LOTID를 입력해주십시오.";
                //    txtBarCodeScan.Focus();
                //    throw new Exception();
                //}
                if (cboNoWorkCode.SelectedValue.ToString().Trim() == "")
                {
                    Message[0] = "[무작업 사유]";
                    Message[1] = "무작업 사유가 선택되지 못하였습니다.";
                    cboNoWorkCode.Select();
                    cboNoWorkCode.Focus();
                    throw new Exception();
                }
                StartTime = double.Parse(mtb_From.Text.Replace("-", "") + dtStartTime.Value.ToString("HHmmss").Replace(":","")); //2021-07-21 자리수로 인해 float를 double로 변경
                EndTime = double.Parse(mtb_To.Text.Replace("-", "") + dtEndTime.Value.ToString("HHmmss").Replace(":", ""));      //2021-07-21 자리수로 인해 float를 double로 변경
                if (StartTime > EndTime)
                {
                    Message[0] = "[무작업시간 오류]";
                    Message[1] = "시작시간이 종료시간보다 더 큽니다.";
                    throw new Exception();
                }
                

                // 2020.07.27 추가
                // 만약에 같은 시간, 공정, 호기로 작업한게 있다면.. 막기
                if (!CheckIsSameWorkTime())
                {
                    return false; 
                }

                return true;
            }
            catch (Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                return false;
            }

        }

        #region 만약에 같은 시간, 공정, 호기로 작업한게 있다면.. 막기

        private bool CheckIsSameWorkTime()
        {
            bool flag = false;

            try
            {
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Clear();

                sqlParameter.Add("ProcessID", Frm_tprc_Main.g_tBase.ProcessID);
                sqlParameter.Add("MachineID", Frm_tprc_Main.g_tBase.MachineID);
                sqlParameter.Add("WorkStartDate", mtb_From.Text.Replace("-", ""));
                sqlParameter.Add("WorkEndDate", mtb_To.Text.Replace("-", ""));
                sqlParameter.Add("WorkStartTime", dtStartTime.Value.ToString("HHmmss"));
                sqlParameter.Add("WorkEndTime", dtEndTime.Value.ToString("HHmmss"));
                
                //DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_prdWork_CheckIsSameWorkTime", sqlParameter, false);
                DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_prdWork_CheckIsSameNoWorkTime", sqlParameter, false);

                if (dt != null
                    && dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];

                    if (dt.Columns.Count == 1
                        && dr["Result"].ToString().ToUpper().Equals("SUCCESS"))
                    {

                        flag = true;
                    }
                    else if (dt.Columns.Count > 1)
                    {
                        // 예시 : 
                        // CNC 공정 1호기에 이미 같은 일자 및 시간으로 무작업이 등록되어 있습니다.

                        Message[0] = "[무작업 중복 등록 오류]";
                        Message[1] = dr["Process"].ToString() + " 공정 " + dr["MachineNo"].ToString() + "에 같은 일자 및 시간으로\r\n이미 무작업이 등록되어 있습니다.";
                        Message[1] += "\r\n[" + dr["Process"].ToString() + " / " + dr["MachineNo"].ToString() + " / " + dr["Name"].ToString() + " / " + dr["NoReworkCodeName"].ToString() + "]";
                        WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                    }
                }
            }
            catch(Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox("중복 체크 구문 오류 [ CheckIsSameWorkTime ] + \r\n" + ex.Message, "중복 체크 오류", 0, 1);
                return false;
            }

            return flag;
        }

        #endregion

        //저장
        private void SaveData()
        {
            try
            {
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                string MoldID = string.Empty;
                string MCID = string.Empty;
                string managerid = string.Empty;
                string customid = string.Empty;
                string buycustomid = string.Empty;
                string personid = string.Empty;
                string personname = string.Empty;

                #region 금형수리와 관련된 무작업을 사용하는 업체에만 해당.
                ////0 - 0. // 
                //if (cboNoWorkCode.SelectedValue.ToString() == "6")      //코드 금형수리
                //{
                //    // 0 - 0 - 1. 일단 article로 mold 먼저 찾고 나서 보자.
                //    string[] CheckMold = new string[2];
                //    string Query = "select  TOP 1  MoldID from mt_ArticleMold where ArticleID = '" + lblArticle.Tag.ToString() + "'";
                //    CheckMold = DataStore.Instance.ExecuteQuery(Query, false);
                //    MoldID = CheckMold[1];

                //    if (MoldID == "NullReferenceException")   // null 
                //    {
                //        Message[0] = "[금형코드 문제]";
                //        Message[1] = "오류! 지시LOTID는 금형정보가 없습니다. \r\n 무작업사유로 금형수리를 선택할 수 없습니다.";
                //        throw new Exception();
                //    }

                //    // 0 - 0 - 2. 금형수리 시간측정을 위해선, 작업기간 설정을 유도해야 한다.
                //    string fromdate = mtb_From.Text.Replace("-", "");
                //    string todate = mtb_To.Text.Replace("-", "");
                //    string fromtime = dtStartTime.Value.ToString("HHmmss");
                //    string totime = dtEndTime.Value.ToString("HHmmss");

                //    if (fromdate == todate && fromtime == totime)
                //    {
                //        Message[0] = "[금형수리 소요시간 문제]";
                //        Message[1] = "금형수리 소요시간 계산이 필요합니다. \r\n 작업기간을 이용, 소요시간을 입력해 주세요.";
                //        throw new Exception();
                //    }

                //}


                // 설비문제와 관련된 무작업을 사용하는 업체에만 해당.
                //0 - 1.
                //if (cboNoWorkCode.Text.Contains("설비")) //설비문제를 선택한 경우.
                //{
                //    sqlParameter = new Dictionary<string, object>();
                //    sqlParameter.Clear();
                //    sqlParameter.Add("ProcessID", sProcessID);  // (setProcess에서 선택한)
                //    sqlParameter.Add("MachineID", sMachineID);  // (setProcess에서 선택한)

                //    ds = DataStore.Instance.ProcedureToDataSet("xp_WizWork_sMcCodeByProcMach", sqlParameter, false);

                //    if (ds != null)
                //    {
                //        DataTable dt = ds.Tables[0];
                //        if (dt.Rows.Count == 0)
                //        {
                //            Message[0] = "[설비코드 문제]";
                //            Message[1] = "선택한 공정,호기와 일치하는 설비정보가 없습니다. \r\n 무작업사유로 설비문제를 선택할 수 없습니다.";
                //            throw new Exception();
                //        }
                //        else
                //        {
                //            MCID = dt.Rows[0]["MCID"].ToString();
                //            managerid = dt.Rows[0]["managerid"].ToString();
                //            customid = dt.Rows[0]["customid"].ToString();
                //            buycustomid = dt.Rows[0]["buycustomid"].ToString();
                //            personid = dt.Rows[0]["personid"].ToString();
                //            personname = dt.Rows[0]["personname"].ToString();
                //        }
                //    }

                //    // 0 - 1 - 2. 설비문제 시간측정을 위해선, 작업기간 설정을 유도해야 한다.
                //    string fromdate = mtb_From.Text.Replace("-", "");
                //    string todate = mtb_To.Text.Replace("-", "");
                //    string fromtime = dtStartTime.Value.ToString("HHmmss");
                //    string totime = dtEndTime.Value.ToString("HHmmss");

                //    if (fromdate == todate && fromtime == totime)
                //    {
                //        Message[0] = "[설비문제 소요시간 문제]";
                //        Message[1] = "설비문제 소요시간 계산이 필요합니다. \r\n 작업기간을 이용, 소요시간을 입력해 주세요.";
                //        throw new Exception();
                //    }
                //}
                #endregion

                //1
                List<Procedure> Prolist = new List<Procedure>();
                List<Dictionary<string, object>> ListParameter = new List<Dictionary<string, object>>();

                string strstrLabelGubun = "0";
                string strComments = txtComments.Text.Trim();//"무작업";
                string srReworkOldYN = "";
                string strReworkLinkProdID = "";
                string strJobGbn = "2";

                sqlParameter = new Dictionary<string, object>();
                sqlParameter.Clear();
                sqlParameter.Add("JobID", 0);              //JobID 입력안함. 지금 프로시저 수행 후 만들어짐~~
                sqlParameter.Add("InstID", lblPLotID.Tag.ToString());
                sqlParameter.Add("InstDetSeq", lblCustom.Tag.ToString());
                sqlParameter.Add("LabelID", lblPLotID.Text.ToUpper());  //TWkLabelPrint(i).sLabelID)
                sqlParameter.Add("StartSaveLabelID", lblPLotID.Text.ToUpper());  //TWkLabelPrint(i).sLabelID)

                sqlParameter.Add("LabelGubun", strstrLabelGubun);       //WkLabelPrint(i).sLabelGubun
                sqlParameter.Add("ProcessID", Frm_tprc_Main.g_tBase.ProcessID); //선택되있는 sProcessID(setProcess에서 선택한)
                sqlParameter.Add("MachineID", Frm_tprc_Main.g_tBase.MachineID); //선택되있는 sMachineID(setProcess에서 선택한)
                sqlParameter.Add("ScanDate", DateTime.Now.ToString("yyyyMMdd")); //년월일
                sqlParameter.Add("ScanTime", DateTime.Now.ToString("HHmmss")); //시분초

                sqlParameter.Add("ArticleID", lblArticle.Tag.ToString().Trim()); //품명코드=재종코드
                sqlParameter.Add("WorkQty", 0); //생산수량
                sqlParameter.Add("Comments", strComments);//지시커멘트
                sqlParameter.Add("ReworkOldYN", srReworkOldYN); //재작업여부 NO
                sqlParameter.Add("ReworkLinkProdID", strReworkLinkProdID);//????????????????????????

                sqlParameter.Add("WorkStartDate", mtb_From.Text.Replace("-", ""));      //작업시작날짜
                sqlParameter.Add("WorkStartTime", dtStartTime.Value.ToString("HHmmss"));//작업시작시간
                sqlParameter.Add("WorkEndDate",   mtb_To.Text.Replace("-", ""));        //작업종료날짜
                sqlParameter.Add("WorkEndTime", dtEndTime.Value.ToString("HHmmss"));    //작업종료시간

                sqlParameter.Add("JobGbn", strJobGbn);//작업구분 1:정상,2:무작업,3:재작업 NO_Work_U폼에서는 2번 무작업으로 처리
                sqlParameter.Add("NoReworkCode", cboNoWorkCode.SelectedValue.ToString());//무작업코드_

                sqlParameter.Add("WDNO", "");
                sqlParameter.Add("WDID", "");
                sqlParameter.Add("WDQty", 0);
                sqlParameter.Add("LogID", 0);
                sqlParameter.Add("s4MID", "");
                //sqlParameter.Add("YLabelID", "");
                //sqlParameter.Add("UpBurnPlateTemper1", 0);
                //sqlParameter.Add("UpBurnPlateTemper2", 0);
                //sqlParameter.Add("SetUpBurnPlateTemper", 0);
                //sqlParameter.Add("DownBurnPlateTemper1", 0);
                //sqlParameter.Add("DownBurnPlateTemper2", 0);
                //sqlParameter.Add("SetDownBurnPlateTemper", 0);
                //sqlParameter.Add("FormaTime", 0);
                //sqlParameter.Add("SetFormaTime", 0);
                //sqlParameter.Add("FormaOpenTime", 0);
                //sqlParameter.Add("SetFormaOpenTime", 0);
                sqlParameter.Add("DayOrNightID", Frm_tprc_Main.g_tBase.DayOrNightID);

                sqlParameter.Add("CreateUserID", Frm_tprc_Main.g_tBase.PersonID);// 작업자


                Procedure pro1 = new Procedure();
                pro1.Name = "xp_wkResult_iWkResult"; 
                pro1.OutputUseYN = "Y";
                pro1.OutputName = "JobID";
                pro1.OutputLength = "19";

                Prolist.Add(pro1);
                ListParameter.Add(sqlParameter);


                //if (cboNoWorkCode.SelectedValue.ToString() == "6")  // 코드id 6번 = 금형수리.
                //{
                //    string fromdate = mtb_From.Text.Replace("-", "");
                //    string todate = mtb_To.Text.Replace("-", "");
                //    string fromtime = dtStartTime.Value.ToString("HHmmss");
                //    string totime = dtEndTime.Value.ToString("HHmmss");

                //    string from = fromdate + fromtime;
                //    string to = todate + totime;
                //    IFormatProvider KR_Format = new System.Globalization.CultureInfo("ko-KR", true);
                //    DateTime DT_From = DateTime.ParseExact(from, "yyyyMMddHHmmss", KR_Format);
                //    DateTime DT_To = DateTime.ParseExact(to, "yyyyMMddHHmmss", KR_Format);

                //    TimeSpan timeDiff = DT_To - DT_From;
                //    double diffTotalMiniute = timeDiff.TotalMinutes;

                //    if (diffTotalMiniute.ToString().Substring(0, 1) == "-")
                //    {
                //        Message[0] = "[금형수리 시간 문제]";
                //        Message[1] = "시작일자가 종료시간보다 늦을 수 없습니다. \r\n 작업기간을 이용, 소요시간을 입력해 주세요.";
                //        throw new Exception();
                //    }               

                //    // 무작업 사유가 금형수리인 경우, 금형수리등록 자동 insert.
                //    // AFT > 대표님 요청사항 >> 2019.04.30 허윤구.
                //    sqlParameter = new Dictionary<string, object>();
                //    sqlParameter.Clear();
                //    sqlParameter.Add("RepairID", "");
                //    sqlParameter.Add("repairdate", DateTime.Now.ToString("yyyyMMdd"));
                //    sqlParameter.Add("repairTime", diffTotalMiniute.ToString() + "분"); 
                //    sqlParameter.Add("RepairGubun", "1");   // 금형수리 >> 수리는 교체구분 1번.
                //    sqlParameter.Add("MoldID", MoldID);
                //    sqlParameter.Add("RepairCustom", "");  // 수리업체 등록인데.. 어딘지 알수가 있나...
                //    sqlParameter.Add("repairremark", "현장 비가동등록 무작업사유에 금형수리 선택에 따른 자동저장");
                //    sqlParameter.Add("CreateUserID", sUserID);

                //    Procedure pro2 = new Procedure();
                //    pro2.Name = "xp_dvlMold_iMoldRepair";
                //    pro2.OutputUseYN = "Y";
                //    pro2.OutputName = "RepairID";
                //    pro2.OutputLength = "10";

                //    Prolist.Add(pro2);
                //    ListParameter.Add(sqlParameter);
                //}

                //if (cboNoWorkCode.SelectedValue.ToString() == "1")   // 코드id 1번 = 설비문제.
                //{

                //    string fromdate = mtb_From.Text.Replace("-", "");
                //    string todate = mtb_To.Text.Replace("-", "");
                //    string fromtime = dtStartTime.Value.ToString("HHmmss");
                //    string totime = dtEndTime.Value.ToString("HHmmss");

                //    string from = fromdate + fromtime;
                //    string to = todate + totime;
                //    IFormatProvider KR_Format = new System.Globalization.CultureInfo("ko-KR", true);
                //    DateTime DT_From = DateTime.ParseExact(from, "yyyyMMddHHmmss", KR_Format);
                //    DateTime DT_To = DateTime.ParseExact(to, "yyyyMMddHHmmss", KR_Format);

                //    TimeSpan timeDiff = DT_To - DT_From;
                //    double diffTotalMiniute = timeDiff.TotalMinutes;

                //    if (diffTotalMiniute.ToString().Substring(0, 1) == "-")
                //    {
                //        Message[0] = "[설비문제 시간 문제]";
                //        Message[1] = "시작일자가 종료시간보다 늦을 수 없습니다. \r\n 작업기간을 이용, 소요시간을 입력해 주세요.";
                //        throw new Exception();
                //    }



                //    sqlParameter = new Dictionary<string, object>();
                //    sqlParameter.Clear();
                //    sqlParameter.Add("RepairID", "");
                //    sqlParameter.Add("repairdate", DateTime.Now.ToString("yyyyMMdd"));
                //    sqlParameter.Add("RepairTime", diffTotalMiniute.ToString() + "분");
                //    sqlParameter.Add("RepairGubun", /*이게 수리야 교체야,... 수리는 1이고 교체는 2인데...*/ " ");
                //    sqlParameter.Add("mcid", MCID);   // 기계명 id.  MT_MC.
                //    sqlParameter.Add("managerid", managerid);     //MT_MC
                //    sqlParameter.Add("customid", customid);       //MT_MC
                //    sqlParameter.Add("buycustomid", buycustomid); //MT_MC
                //    sqlParameter.Add("personid", personid);       //MT_MC
                //    sqlParameter.Add("personname", personname);   //MT_MC
                //    sqlParameter.Add("repairremark", "현장 비가동등록 무작업사유에 설비문제 선택에 따른 자동저장");
                //    sqlParameter.Add("CreateUserID", sUserID);


                //    Procedure pro3 = new Procedure();
                //    pro3.Name = "xp_mcRepair_iRepair";
                //    pro3.OutputUseYN = "Y";
                //    pro3.OutputName = "RepairID";
                //    pro3.OutputLength = "10";

                //    Prolist.Add(pro3);
                //    ListParameter.Add(sqlParameter);
                //}

                List<KeyValue> list_Result = new List<KeyValue>();
                list_Result = DataStore.Instance.ExecuteAllProcedureOutputGetCS(Prolist, ListParameter);

                if (list_Result[0].key.ToLower() == "success")                
                {
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
                SetFormDataClear();
            }
            catch (Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                return;
            }
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            if (CheckData())
            {
                SaveData();
                LogData.LogSave(this.GetType().Name, "C"); //2022-06-22 저장
            }
        }

        private void cmdsetProcess_Click(object sender, EventArgs e)
        {
            frm_tprc_setProcess form = new frm_tprc_setProcess(true);
            form.Owner = ((Frm_tprc_Main)(MdiParent));
            if (form.ShowDialog() == DialogResult.OK)
            {
                //Set_stbInfo(Frm_tprc_Main.g_tBase.TeamID, Frm_tprc_Main.g_tBase.Team, 
                //    Frm_tprc_Main.g_tBase.PersonID, Frm_tprc_Main.g_tBase.Person, 
                //    Frm_tprc_Main.g_tBase.MachineID, Frm_tprc_Main.g_tBase.Machine, 
                //    Frm_tprc_Main.g_tBase.sMoldID, Frm_tprc_Main.g_tBase.sMold, 
                //    Frm_tprc_Main.g_tBase.ProcessID, Frm_tprc_Main.g_tBase.Process, 
                //    Frm_tprc_Main.g_tBase.sInstID);
                SetInfo();
                ((Frm_tprc_Main)(MdiParent)).Set_stsInfo();

                // 2020.11.20, GDU → 공정 작업자 버튼을 통해서 변경해도 변경이 안된다고 하여, 확인용으로 추가.
                txtProcessID.Text = Frm_tprc_Main.g_tBase.ProcessID;
                txtProcess.Text = Frm_tprc_Main.g_tBase.Process;
                txtMachineID.Text = Frm_tprc_Main.g_tBase.MachineID;
                txtMachine.Text = Frm_tprc_Main.g_tBase.Machine;
                txtPersonID.Text = Frm_tprc_Main.g_tBase.PersonID;
                txtPerson.Text = Frm_tprc_Main.g_tBase.Person;
                txtDayOrNightID.Text = Frm_tprc_Main.g_tBase.DayOrNightID;
                txtDayOrNight.Text = Frm_tprc_Main.g_tBase.DayOrNight;
            };
        }

        //전체 초기화
        private void cmdClear_Click(object sender, EventArgs e)
        {
            SetFormDataClear();
        }

        public void Set_stbInfo(string g_TeamID, string g_TeamName, string sPersonID, string sPersonName, string sMachineID, string sMachineName,
                             string MoldID, string MoldName, string sProcessID, string sProcessName, string strInstID)
        {
            ((Frm_tprc_Main)(MdiParent)).Set_stsInfo();
        }

        private void txtBarCodeScan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (txtBarCodeScan.Text.Length == 15 && txtBarCodeScan.Text.ToUpper().Contains("PL"))
                {
                    LF_GetBarcodeData();
                }
                else
                {
                    txtBarCodeScan.Text = "";
                    return;
                }
            }
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
        private void TimeCheck(string strTime)
        {
            POPUP.Frm_CMNumericKeypad FK = new POPUP.Frm_CMNumericKeypad(strTime);
            FK.Owner = this;
            string sTime = "";
            DateTime dt = DateTime.Now;
            if (FK.ShowDialog() == DialogResult.OK)
            {

                sTime = FK.InputTextValue;
                if (sTime != "")
                {
                    dt = DateTime.ParseExact(sTime, "HHmmss", null);
                }
            }

            if (strTime == "시작시간") { dtStartTime.Value = dt; }
            else if (strTime == "종료시간") { dtEndTime.Value = dt; }
        }
        private void SetDateTimePicker()
        {
            mtb_From.Text = DateTime.Today.ToString("yyyyMMdd");
            mtb_To.Text = DateTime.Today.ToString("yyyyMMdd");
            dtStartTime.Format = DateTimePickerFormat.Custom;
            dtStartTime.CustomFormat = "HH:mm:ss";
            dtEndTime.Format = DateTimePickerFormat.Custom;
            dtEndTime.CustomFormat = "HH:mm:ss";
        }
        private void btnStartTime_Click(object sender, EventArgs e)
        {
            TimeCheck("시작시간");
        }

        private void btnEndTime_Click(object sender, EventArgs e)
        {
            TimeCheck("종료시간");
        }

        private void lblComments_Click(object sender, EventArgs e)
        {
            try
            {
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
                Console.Write(ex.Message);
                MessageBox.Show("실패");
                Process.Start(@"C:\Windows\winsxs\x86_microsoft-windows-osk_31bf3856ad364e35_6.1.7601.18512_none_acc225fbb832b17f\osk.exe");
            }
            txtComments.Select();
            txtComments.Focus();
        }

        private void Frm_tprc_NoWork_U_Activated(object sender, EventArgs e)
        {
            ((Frm_tprc_Main)(MdiParent)).LoadRegistry();
        }
    }
}
