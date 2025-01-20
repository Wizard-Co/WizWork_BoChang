using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WizCommon;

//*******************************************************************************
//프로그램명    frm_PopUp_PreScanWork.cs
//메뉴ID        
//설명          frm_PopUp_PreScanWork 메인소스입니다.
//작성일        2019.07.29
//개발자        허윤구
//*******************************************************************************
// 변경일자     변경자      요청자      요구사항ID          요청 및 작업내용
//*******************************************************************************
//  19_0729     허윤구  * 성형 하나에 재단 2개가 필요한 케이스에 따라 수정보완.
//                          (InsertX)
//  2019.08.01 > 허윤구    FMB가 어떤 이유로 이미 재단창고에 가 있을 케이스에 대한 로직추가.
//*******************************************************************************

namespace WizWork
{
    public partial class frm_PopUp_Login : Form
    {

        string m_ResablyID = "";

        WizWorkLib Lib = new WizWorkLib();


        public frm_PopUp_Login()
        {
            InitializeComponent();
            SetScreen();  //TLP 사이즈 조정
        }
        public frm_PopUp_Login(string strProcessID, string strMachineID, string strMoldID)
        {
            InitializeComponent();
            SetScreen();  //TLP 사이즈 조정
        }


        #region 테이블 레이아웃 패널 사이즈 조정
        private void SetScreen()
        {
            tlpMain.Dock = DockStyle.Fill;
            foreach (Control control in tlpMain.Controls)
            {
                control.Dock = DockStyle.Fill;
                control.Margin = new Padding(1, 1, 1, 1);
                foreach (Control contro in control.Controls)
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
                                }
                            }
                        }
                    }
                }
            }
        }

        #endregion


        //로드.
        private void frm_PopUp_PreScanWork_Load(object sender, EventArgs e)
        {

        }

        // After Load
        private void frm_PopUp_PreScanWork_Shown(object sender, EventArgs e)
        {

        }

      


        #region 아이디, 비밀번호 입력

        // 아이디 버튼 클릭
        private void cmdID_Click(object sender, EventArgs e)
        {
            //실행중인 프로세스가 없을때 
            if (!Frm_tprc_Main.Lib.ReturnProcessRunStop("osk"))
            {
                System.Diagnostics.Process ps = new System.Diagnostics.Process();
                ps.StartInfo.FileName = "osk.exe";
                ps.Start();
            }
            txtID.Select();
            txtID.Focus();

            //POPUP.Frm_CMKeypad.g_Name = "아이디 입력";
            //POPUP.Frm_CMKeypad FK = new POPUP.Frm_CMKeypad();
            //POPUP.Frm_CMKeypad.KeypadStr = txtID.Text.Trim();
            //if (FK.ShowDialog() == DialogResult.OK)
            //{
            //    txtID.Text = FK.tbInputText.Text;
            //}
            //else
            //{
            //    txtID.Text = string.Empty;
            //}
        }

        // 비밀번호 버튼 클릭
        private void cmdPW_Click(object sender, EventArgs e)
        {
            //실행중인 프로세스가 없을때 
            if (!Frm_tprc_Main.Lib.ReturnProcessRunStop("osk"))
            {
                System.Diagnostics.Process ps = new System.Diagnostics.Process();
                ps.StartInfo.FileName = "osk.exe";
                ps.Start();
            }
            txtPW.Select();
            txtPW.Focus();

            //POPUP.Frm_CMKeypad.g_Name = "비밀번호 입력";
            //POPUP.Frm_CMKeypad FK = new POPUP.Frm_CMKeypad();
            //POPUP.Frm_CMKeypad.KeypadStr = txtPW.Text.Trim();
            //if (FK.ShowDialog() == DialogResult.OK)
            //{
            //    txtPW.Text = FK.tbInputText.Text;
            //}
            //else
            //{
            //    txtPW.Text = string.Empty;
            //}
        }

        #endregion

        #region 로그인, 취소 버튼선택.

        // 로그인 버튼 선택시.
        private void btnOK_Click(object sender, EventArgs e)
        {
            // DB 임시 인서트.
            if (Login() == true)
            {
                DialogResult = DialogResult.OK;
                btnCancel_Click(null, null);
            }
            else
            {
                //WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n"), "[오류]", 0, 1);
            }
        }

        // 취소 버튼 선택시.
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        #region DB 임시인서트 펑션

        // DB 임시데이터 인서트 작업.
        private bool Login()
        {
            bool flag = false;

            try
            {
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Add("ID", txtID.Text);
                sqlParameter.Add("PW", txtPW.Text);

                DataSet ds = DataStore.Instance.ProcedureToDataSet("xp_prdWork_Login", sqlParameter, false);

                DataTable dt = ds.Tables[0];

                if (dt.Rows.Count > 0)
                {
                    if (dt.Columns.Count == 1)
                    {
                        WizCommon.Popup.MyMessageBox.ShowBox(dt.Rows[0]["Msg"].ToString(), "[로그인 오류]", 0, 1);
                    }
                    else
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            Frm_tprc_Main.g_tBase.PersonID = dr["PersonID"].ToString();
                            Frm_tprc_Main.g_tBase.Person = dr["Name"].ToString();
                            Frm_tprc_Main.g_tBase.ResablyID = dr["ResablyID"].ToString();

                            if (MdiParent == null)
                            {
                                MdiParent = new Frm_tprc_Main();
                            }
                            ((WizWork.Frm_tprc_Main)(MdiParent)).Set_stsInfo();

                            flag = true;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(ex.Message, "[오류]", 0, 1);
            }

            return flag;
        }




        #endregion

        #endregion

        
    }
}
