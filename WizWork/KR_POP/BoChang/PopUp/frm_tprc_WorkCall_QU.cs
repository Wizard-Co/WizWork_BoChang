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
    public partial class frm_tprc_WorkCall_QU : Form
    {
        WizWorkLib Lib = new WizWorkLib();

        private DataSet ds = null;
        private DataTable dt = null; //2022-10-20
        string[] Message = new string[2];
        public bool blClose = false;
        List<string> WorkCallList = new List<string>();

        LogData LogData = new LogData(); //2022-10-24 log 남기는 함수

        //public string SendValue { get; set; } // setProcess_CallMgr 에서 전송된 문자열 받기위해

        public frm_tprc_WorkCall_QU(List<string> WorkCallList)
        {
            InitializeComponent();
            this.WorkCallList = WorkCallList;
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
            SetComboBoxPerson();            //처리자
            //WorkCallPerson();             //이전의 호출자 불러오기
            SetComboBoxPRDRespond();     //조치내용
        }

        private void SetComboBoxPerson()
        {
            try
            {
                ds = DataStore.Instance.ProcedureToDataSet("xp_WizWork_sCallMgrPersonPsr", null, false);

                DataRow newRow = ds.Tables[0].NewRow();
                newRow[Person_sPersonByProcess.PERSONID] = "*";
                newRow[Person_sPersonByProcess.NAME] = "전체";

                if (ds != null && ds.Tables[0].Rows.Count > 0)
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

        private void SetComboBoxPRDRespond()
        {
            try
            {
                ds = DataStore.Instance.ProcedureToDataSet("xp_WizWork_sCallMgrCodeRespond", null, false);

                DataRow newRow = ds.Tables[0].NewRow();
                newRow[WizWork_sNoWorkCodeReason.CODE_ID] = "*";
                newRow[WizWork_sNoWorkCodeReason.CODE_NAME] = "전체";

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    ds.Tables[0].Rows.InsertAt(newRow, 0);
                    cboRepond.DataSource = ds.Tables[0];
                }

                cboRepond.ValueMember = WizWork_sNoWorkCodeReason.CODE_ID;
                cboRepond.DisplayMember = WizWork_sNoWorkCodeReason.CODE_NAME;
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
            //tableLayoutPanel1.SetRowSpan(panel1, 2);
        }

        #endregion


        private bool CheckData(string SaveYN)
        {
            try
            {
                if(cboPerson.SelectedIndex.ToString() == "0")
                {
                    Message[0] = "[처리자]";
                    Message[1] = "처리자를 선택하지 않았습니다.";
                    throw new Exception();
                }
                if (SaveYN == "Y")
                {
                    if (cboRepond.SelectedIndex.ToString() == "0")
                    {
                        Message[0] = "[조치내용]";
                        Message[1] = "조치내용을 선택하지 않았습니다.";
                        throw new Exception();
                    }
                }
                else
                {
                    if (cboRepond.SelectedIndex.ToString() != "0")
                    {
                        Message[0] = "[조치내용]";
                        Message[1] = "처리불가인 경우 처리사유를 전체로 수정해주세요.";
                        throw new Exception();
                    }
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

        #region 저장 버튼 모음 (처리완료,처리 불가) 

        private void cmdSaveN_Click(object sender, EventArgs e)
        {
            string SaveYN = "N";

            if (CheckData(SaveYN))
            {
                try
                {
                    //Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                    List<Procedure> Prolist = new List<Procedure>();
                    List<Dictionary<string, object>> ListParameter = new List<Dictionary<string, object>>();

                    for (int i = 0; i < WorkCallList.Count; i++) 
                    {
                        Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                        sqlParameter.Add("sWorkCallID", WorkCallList[i].ToString());
                        sqlParameter.Add("sCallRepondCode", cboRepond.SelectedValue.ToString());
                        sqlParameter.Add("sRespondAbleYN", SaveYN);
                        sqlParameter.Add("sUpdateUserID", cboPerson.SelectedValue.ToString());

                        Procedure pro1 = new Procedure();
                        pro1.Name = "xp_WizWork_uWkWorkCall";
                        pro1.OutputUseYN = "N";
                        pro1.OutputName = "WorkCallID";
                        pro1.OutputLength = "20";

                        Prolist.Add(pro1);
                        ListParameter.Add(sqlParameter);
                    }

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
                    DialogResult = DialogResult.OK;
                    Close();
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                    return;
                }
            }
        }

        private void cmdSaveY_Click(object sender, EventArgs e)
        {
            string SaveYN = "Y";

            if (CheckData(SaveYN))
            {
                try
                {
                    //Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                    List<Procedure> Prolist = new List<Procedure>();
                    List<Dictionary<string, object>> ListParameter = new List<Dictionary<string, object>>();

                    for (int i = 0; i < WorkCallList.Count; i++)
                    {
                        Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                        sqlParameter.Add("sWorkCallID", WorkCallList[i].ToString());
                        sqlParameter.Add("sCallRepondCode", cboRepond.SelectedValue.ToString());
                        sqlParameter.Add("sRespondAbleYN", SaveYN);
                        sqlParameter.Add("sUpdateUserID", cboPerson.SelectedValue.ToString());

                        Procedure pro1 = new Procedure();
                        pro1.Name = "xp_WizWork_uWkWorkCall";
                        pro1.OutputUseYN = "N";
                        pro1.OutputName = "WorkCallID";
                        pro1.OutputLength = "20";

                        Prolist.Add(pro1);
                        ListParameter.Add(sqlParameter);
                    }

                    List<KeyValue> list_Result = new List<KeyValue>();
                    list_Result = DataStore.Instance.ExecuteAllProcedureOutputGetCS(Prolist, ListParameter);

                    if (list_Result[0].key.ToLower() == "success")
                    {
                        LogData.LogSave(this.GetType().Name, "C"); //2022-10-24 추가
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
                    DialogResult = DialogResult.OK;
                    Close();
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                    return;
                }
            }
        }

        #endregion


        //닫기 버튼
        private void cmdClose_Click(object sender, EventArgs e)
        {
            LogData.LogSave(this.GetType().Name, "S"); //log 남기기(로드 S) 2022-10-24
            this.Close();
        }
    }
}
