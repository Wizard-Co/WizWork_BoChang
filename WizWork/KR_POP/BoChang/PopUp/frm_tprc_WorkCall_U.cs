using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;
using WizWork.Tools;
using WizWork.Properties;
using WizCommon;
using System.Diagnostics;
using System.IO;


namespace WizWork
{
    public partial class frm_tprc_WorkCall_U : Form
    {
        WizWorkLib Lib = new WizWorkLib();

        private DataSet ds = null;
        private DataTable dt = null; //2022-10-20
        string[] Message = new string[2];
        public bool blClose = false;

        LogData LogData = new LogData(); //2022-10-24 log 남기는 함수

        //public string SendValue { get; set; } // setProcess_CallMgr 에서 전송된 문자열 받기위해

        public frm_tprc_WorkCall_U()
        {
            InitializeComponent();

            this.mtb_Date.Text = DateTime.Today.ToString("yyyyMMdd"); // 호출일자
            this.dtCallTime.CustomFormat = "HH:mm:ss"; // 호출시간

            //if (!blClose)
            //{
            //    SetComboBox();               // 콤보박스 설정
            //}
        }

        private void frm_tprc_CallMgr_U_Load(object sender, EventArgs e)
        {
            LogData.LogSave(this.GetType().Name, "S"); //log 남기기(로드 S) 2022-10-24
            SetScreen();
            SetComboBox();
        }

        #region 콤보박스 설정 및 콤보박스 설정 모음(공정, 호기, 호출자, 호출사유)
        // 콤보박스 설정
        private void SetComboBox()
        {
            SetComboBoxProcess();       //공정
            SetComboBoxMachineID("");   //호기
            SetComboBoxPerson();        //호출자
            WorkCallPerson();           //이전의 호출자 불러오기
            SetComboBoxPRDCallReason(); //호출사유
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
                else
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

        private void SetComboBoxPerson()
        {
            try
            {
                ds = DataStore.Instance.ProcedureToDataSet("xp_WizWork_sCallMgrPerson", null, false);

                DataRow newRow = ds.Tables[0].NewRow();
                newRow[Person_sPersonByProcess.PERSONID] = "*";
                newRow[Person_sPersonByProcess.NAME] = "전체";

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    ds.Tables[0].Rows.InsertAt(newRow, 0);
                    cboPerson.DataSource = ds.Tables[0];
                }
                else
                {
                    ds.Tables[0].Rows.InsertAt(newRow, 0);
                    cboPerson.DataSource = ds.Tables[0];

                }

                cboPerson.ValueMember = Person_sPersonByProcess.PERSONID;
                cboPerson.DisplayMember = Person_sPersonByProcess.NAME;
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

        private void SetComboBoxPRDCallReason()
        {
            try
            {
                ds = DataStore.Instance.ProcedureToDataSet("xp_WizWork_sCallMgrCodeReason", null, false);

                DataRow newRow = ds.Tables[0].NewRow();
                newRow[WizWork_sNoWorkCodeReason.CODE_ID] = "*";
                newRow[WizWork_sNoWorkCodeReason.CODE_NAME] = "전체";

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    ds.Tables[0].Rows.InsertAt(newRow, 0);
                    cboCallReason.DataSource = ds.Tables[0];
                }
                else
                {
                    ds.Tables[0].Rows.InsertAt(newRow, 0);
                    cboCallReason.DataSource = ds.Tables[0];

                }

                cboCallReason.ValueMember = WizWork_sNoWorkCodeReason.CODE_ID;
                cboCallReason.DisplayMember = WizWork_sNoWorkCodeReason.CODE_NAME;
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

        //이전의 호출자 불러오기
        private void WorkCallPerson()
        {
            try
            {
                dt = DataStore.Instance.ProcedureToDataTable("[xp_WizWork_sCallMgrPersonWorkCall]", null, false);

                if (dt != null
                    && dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    cboPerson.SelectedValue = dr["PersonID"].ToString();     //콤박스 값 찾기           
                }
                else
                {
                    cboPerson.SelectedIndex = 0;
                }

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


        #region 레이아웃에 채우기

        private void SetScreen()
        {
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Margin = new Padding(1, 1, 1, 1);
            foreach (Control control in tableLayoutPanel1.Controls)//con = tlp 상위에서 2번째
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
            tableLayoutPanel1.SetRowSpan(panel1, 2);
        }

        #endregion


        private bool CheckData()
        {
            try
            {
                if(cboProcess.SelectedIndex.ToString() == "0")
                {
                    Message[0] = "[공정]";
                    Message[1] = "공정을 선택하지 않았습니다.";
                    throw new Exception();
                }
                if(cboMachineID.SelectedIndex.ToString() == "0")
                {
                    Message[0] = "[호기]";
                    Message[1] = "호기를 선택하지 않았습니다.";
                    throw new Exception();
                }
                if (cboPerson.SelectedIndex.ToString() == "0")
                {
                    Message[0] = "[작업자]";
                    Message[1] = "작업자를 선택하지 않았습니다.";
                    throw new Exception();
                }
                if (Convert.ToInt32(mtb_Date.Text.Replace("-", "")) > Int64.Parse(DateTime.Now.ToString("yyyyMMdd")))
                {
                    Message[0] = "[호출 일자]";
                    Message[1] = "호출 일자가 오늘 일자 보다 큽니다.";
                    throw new Exception();
                }
                if (Convert.ToInt32(dtCallTime.Value.ToString("HHmmss")) > Int64.Parse(DateTime.Now.ToString("HHmmss")))
                {
                    Message[0] = "[호출 시간]";
                    Message[1] = "호출 시간이 현재 시간보다 큽니다.";
                    throw new Exception();
                }
                if (cboCallReason.SelectedIndex.ToString() == "0")
                {
                    Message[0] = "[호출 사유]";
                    Message[1] = "관리자 호출 사유를 선택하지 않았습니다.";
                    throw new Exception();
                }
                
                return true;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                return false;
            }

        }


        // 닫기 버튼
        private void cmdExit_Click(object sender, EventArgs e)
        {
            LogData.LogSave(this.GetType().Name, "S"); //log 남기기(로드 S) 2022-10-24
            this.Close();
        }

        // 초기화 버튼
        private void cmdClear_Click(object sender, EventArgs e)
        {
            SetFormDataClear();
        }

        // 초기화 버튼
        private void SetFormDataClear()
        {
            mtb_Date.Text = DateTime.Today.ToString("yyyyMMdd"); // 호출일자
            dtCallTime.Text = string.Empty; // 호출시간
            cboCallReason.SelectedIndex = 0; // 호출사유
        }

        // 저장 버튼
        private void cmdSave_Click(object sender, EventArgs e)
        {
            if (CheckData())
            {
                try
                {
                    Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                    List<Procedure> Prolist = new List<Procedure>();
                    List<Dictionary<string, object>> ListParameter = new List<Dictionary<string, object>>();

                    sqlParameter.Add("sProcessID", cboProcess.SelectedValue.ToString()); // 0401
                    sqlParameter.Add("sMachineID", cboMachineID.SelectedValue.ToString());
                    sqlParameter.Add("sCallPersonID", cboPerson.SelectedValue.ToString());
                    sqlParameter.Add("sCallDate", mtb_Date.Text.Replace("-", ""));
                    sqlParameter.Add("sCallTime", dtCallTime.Value.ToString("HHmmss"));
    
                    sqlParameter.Add("sCallReasonCode", cboCallReason.SelectedValue.ToString());
                    sqlParameter.Add("sCreateUserID", cboPerson.SelectedValue.ToString());

                    Procedure pro1 = new Procedure();
                    pro1.Name = "xp_WizWork_iWkWorkCall";
                    pro1.OutputUseYN = "N";
                    pro1.OutputName = "WorkCallID";
                    pro1.OutputLength = "20";

                    Prolist.Add(pro1);
                    ListParameter.Add(sqlParameter);


                    List<KeyValue> list_Result = new List<KeyValue>();
                    list_Result = DataStore.Instance.ExecuteAllProcedureOutputGetCS(Prolist, ListParameter);

                    if (list_Result[0].key.ToLower() == "success")
                    {
                        LogData.LogSave(this.GetType().Name, "C"); //log 남기기(추가 C) 2022-10-24
                        Message[0] = "[저장 성공]";
                        Message[1] = "정상적으로 등록이 되었습니다.";
                        WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                        cmdExit_Click(null,null);
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
                    return;
                }
            }
        }

        // 달력 이미지 버튼 눌렀을 경우 달력 출력
        private void btnCal_Date_Click(object sender, EventArgs e)
        {
            LoadCalendar();
        }
        
        // 호출일자 버튼 눌렀을 경우 달력 출력
        private void btnCallDay_Click(object sender, EventArgs e)
        {
            LoadCalendar();
        }

        // 달력 Form값 띄우기
        private void LoadCalendar()
        {
            WizCommon.Popup.Frm_TLP_Calendar calendar = new WizCommon.Popup.Frm_TLP_Calendar(mtb_Date.Text.Replace("-", ""), mtb_Date.Name);
            calendar.WriteDateTextEvent += new WizCommon.Popup.Frm_TLP_Calendar.TextEventHandler(GetDate);
            calendar.Owner = this;
            calendar.ShowDialog();
            //Calendar.Value -> mtbBox.Text 달력창으로부터 텍스트로 값을 옮겨주는 메소드
            void GetDate(string strDate, string btnName)
            {
                DateTime dateTime = new DateTime();
                dateTime = DateTime.ParseExact(strDate, "yyyyMMdd", null);
                mtb_Date.Text = dateTime.ToString("yyyy-MM-dd");
            }
        }

        // 호출시간 키패드 입력
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
            if (strTime == "호출시간") { dtCallTime.Value = dt; }
        }

        private void btnCallTime_Click(object sender, EventArgs e)
        {
            TimeCheck("호출시간");
        }

        //2022-10-20 공정 선택시 호기 선택가능하게 추가
        private void cboProcess_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboProcess.SelectedIndex.ToString() != "0")
            {
                cboMachineID.Enabled = true;
                cboMachineID.SelectedIndex = 0;
            }
            else
            {
                cboMachineID.Enabled = false;
            }

        }

        //날짜는 3가지중 어딜 눌러도 선택가능하게 추가 2022-10-20
        private void CallDay_From_Click(object sender, EventArgs e)
        {
            LoadCalendar();
        }

        //콤보박스 선택하면 공정가지고 다시 설정 2022-10-20
        private void cboMachineID_DropDown(object sender, EventArgs e)
        {
            SetComboBoxMachineID(cboProcess.SelectedValue.ToString());
        }
    }
}
