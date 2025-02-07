using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using WizCommon;
using System.Windows.Forms;


namespace WizWork.BoChang.PopUp
{
    public partial class Frm_PopUp_Work_Jaturi_Save_U : Form
    {
        public string LOTID = "";          //원자재 LOTID
        public string JaturiLOTID = "";    //생성할 자투리 LOTID
        public string CuttingName = "";    //절단명
        public string CuttingWeight = "";  //중량
        public string JaturiLOC = "";      //자투리위치

        private DataSet ds = null;
        string[] Message = new string[2]; //메세지

        public Frm_PopUp_Work_Jaturi_Save_U(string LOTID)
        {
            InitializeComponent();
            this.LOTID = LOTID;

        }

        #region 로드 이벤트

        private void Frm_PopUp_Work_Jaturi_Save_U_Load(object sender, EventArgs e)
        {
            txtLotID.Text = LOTID;
            //자투리 콤보박스 생성
            SetComboBox();
            SetJaturiLotID();
            SetCuttingName();

        }

        #endregion

        //자투리위치 콤보박스 생성
        private void SetComboBox()
        {
            cboJaturiLoc.Items.Clear();
            //자투리 위치 콤보박스
            ds = DataStore.Instance.ProcedureToDataSet("[xp_WizWork_SLOCClss]", null, false);
            DataRow newRow = ds.Tables[0].NewRow();
            newRow["CodeID"] = "*";
            newRow["CodeName"] = "전체";
            ds.Tables[0].Rows.InsertAt(newRow, 0);
            cboJaturiLoc.DataSource = ds.Tables[0];
            cboJaturiLoc.ValueMember = "CodeID";
            cboJaturiLoc.DisplayMember = "CodeName";
        }


        //자투리로트번호 생성 함수
        private void SetJaturiLotID()
        {
            try
            {
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Clear();

                sqlParameter.Add("LabelID", LOTID);

                DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_WizWork_SetJaturiLabelID_By_Cutting", sqlParameter, false);

                if (dt != null
                    && dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    JaturiLOTID = dr["JaturiLabelID"].ToString();
                    txtJaturiLotID.Text = JaturiLOTID;
                }
            }
            catch (Exception ex)
            {
                JaturiLOTID = "";
                WizCommon.Popup.MyMessageBox.ShowBox("자투리 라벨 생성 오류 [ SetJaturiLotID ] + \r\n" + ex.Message, "생성 오류", 0, 1);
            }
            DataStore.Instance.CloseConnection(); //2021-09-23 DB 커넥트 연결 해제

        }

        //절단명 생성 함수

        private void SetCuttingName()
        {
            try
            {
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Clear();

                sqlParameter.Add("LabelID", LOTID);

                DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_WizWork_SetCuttingName", sqlParameter, false);

                if (dt != null
                    && dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    CuttingName = dr["CuttingName"].ToString();
                    txtCutting.Text = CuttingName;
                }
            }
            catch (Exception ex)
            {
                CuttingName = "";
                WizCommon.Popup.MyMessageBox.ShowBox("자투리 라벨 생성 오류 [ SetJaturiLotID ] + \r\n" + ex.Message, "생성 오류", 0, 1);
            }
            DataStore.Instance.CloseConnection(); //2021-09-23 DB 커넥트 연결 해제

        }

        //저장전 체크 함수

        private bool checkSave()
        {
            bool flag = true;

            if (txtLotID.Text == "")
            {
                Message[0] = "[저장 전 체크]";
                Message[1] = "원로트번호가 없습니다. 관리자에게 문의 해주세요.";
                WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 3, 1);
                flag = false;
                return flag;
            }

            if (txtJaturiLotID.Text == "")
            {
                Message[0] = "[저장 전 체크]";
                Message[1] = "자투리로트번호가 없습니다. 관리자에게 문의 해주세요.";
                WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 3, 1);
                flag = false;
                return flag;
            }

            if (txtCutting.Text == "")
            {
                Message[0] = "[저장 전 체크]";
                Message[1] = "절단명이 없습니다. 입력하거나 관리자에게 문의 해주세요.";
                WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 3, 1);
                flag = false;
                return flag;
            }

            if (txtWeight.Text == "" || txtWeight.Text == "0")
            {
                Message[0] = "[저장 전 체크]";
                Message[1] = "중량이 없습니다. 중량을 입력해주세요.";
                WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 3, 1);
                flag = false;
                return flag;
            }

            if (cboJaturiLoc.Text == "전체" || cboJaturiLoc.Text == "")
            {
                Message[0] = "[저장 전 체크]";
                Message[1] = "자투리위치를 선택해주세요.";
                WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 3, 1);
                flag = false;
                return flag;
            }

            return flag;

        }


        #region 저장, 취소 버튼 이벤트

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (checkSave())
            {
                JaturiLOTID = txtJaturiLotID.Text;
                CuttingName = txtCutting.Text;
                CuttingWeight = txtWeight.Text;
                JaturiLOC = cboJaturiLoc.SelectedValue.ToString();

                this.DialogResult = DialogResult.OK;
            }
           
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }



        #endregion

        #region 절단명, 중량 클릭 이벤트

        private void txtCutting_Click(object sender, EventArgs e)
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

                    txtCutting.Focus();

                }
            }
            catch (Exception ex)
            {
                useMasicKeyboard(txtCutting);
            }
        }

        private void txtWeight_Click(object sender, EventArgs e)
        {
            POPUP.Frm_CMNumericKeypad keypad = new POPUP.Frm_CMNumericKeypad("중량입력", "중량");

            keypad.Owner = this;
            if (keypad.ShowDialog() == DialogResult.OK)
            {
                txtWeight.Text = keypad.tbInputText.Text;
            }
        }


        #endregion

        //키보드 함수
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


    }
}
