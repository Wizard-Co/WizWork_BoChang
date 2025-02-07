using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WizWork.Properties;
using WizCommon;
using System.ComponentModel;
using WizWork.GLS.PopUp;
using System.Diagnostics;
using System.IO;



//*******************************************************************************
//프로그램명    Frm_tprc_PlanInputMolded_Q.cs
//메뉴ID        
//설명          Frm_tprc_PlanInputMolded_Q 메인소스입니다.
//작성일        2019.08.01
//개발자        허윤구
//*******************************************************************************
// 변경일자     변경자      요청자      요구사항ID          요청 및 작업내용
//*******************************************************************************

//*******************************************************************************

namespace WizWork
{
    public partial class Frm_tprc_PlanInputMolded_Q : Form
    {
        DataGridViewRow dgvr = null;
        string[] Message = new string[2];
        //string sFileName = ConnectionInfo.filePath;             //Wizard.ini 파일위치
        INI_GS gs = new INI_GS();
        WizWorkLib Lib = new WizWorkLib();
        LogData LogData = new LogData(); //2022-06-21 log 남기는 함수
        public static string strLotID = "";
        public static string strMachineID = "";
        public static string strProcessID = "";
        public static string strInstID = "";
        public static string strInstDetSeq = "";

        public Frm_tprc_PlanInputMolded_Q()
        {
            InitializeComponent();
        }

        private void Frm_tprc_PlanInput_Q_Load(object sender, EventArgs e)
        {

            LogData.LogSave(this.GetType().Name, "S"); //2022-06-22 사용시간(로드, 닫기)
            txtPLotID.Text = "";
            txtBuyerArticle.Text = "";

            SetDateTime();

            //InitPanel();

            InitGrid(); // Grid Setting  
            SetProcessComboBox();  // 공정 콤보 셋팅
            chkProcess.Checked = true;

            if (cboProcess.Items.Count == 3)
            {
                // 아템갯수 3 = 전체 + 부분전체 + 환경설정 상 선택한거 한개일경우,
                cboProcess.SelectedIndex = 2;   // 환경설정 항목으로 고정
            }
            else if (cboProcess.Items.Count == 0)
            {
                //cboProcess.SelectedIndex = -1;
                WizCommon.Popup.MyMessageBox.ShowBox("환경 설정에서 공정을 선택해주세요.", "[공정 설정 필요]", 0, 1);
                //this.Close(); // 로드 = CreateHandler() 중에는 종료가 안된다고 함.
                this.BeginInvoke(new MethodInvoker(this.Close));
                //this.Activated += null; // 이렇게 지정을 해도, 디자인화면에서 설정한 activated 메서드를 실행시킴
                return;
            }
            else
            {
                cboProcess.SelectedIndex = 1;
            }

            //SetBuyerArticleComboBox(); //2022-08-25 품번 콤보박스 초기값 설정

            // ☆★ Activated 시 조회 및 작업 공정 버튼 세팅 ← 2020.10.19 위의 공정세팅이 되어있지 않을시에는, 화면 종료를 위해서. (로드 이벤트 후 바로 엑티브 이벤트 발생함)
            // 디자인단에서 설정한 메서드를 소스에서 설정함.
            this.Activated += Frm_tprc_PlanInput_Q_Activated;

            //2022-09-15 금월버튼 클릭 주석처리
            //btnThisMonth_Click(null, null);
            chkComplete.Checked = true;

            procQuery();
            LogData.LogSave(this.GetType().Name, "R"); //2022-06-22 조회

            WorkingMachine_btnSetting(); //2023-03-28

            chkInsDate.Checked = true;
            chkPLotID.Checked = false;
            txtPLotID.Select();
            txtPLotID.Focus();
        }

        // 금월버튼. (사번별 작지 월단위 일괄등록에 따라 필요해진 버튼.)
        private void btnThisMonth_Click(object sender, EventArgs e)
        {
            var now = DateTime.Now;
            var startOfMonth = new DateTime(now.Year, now.Month, 1);

            //mtb_From.Text = "2020-07-10";
            mtb_From.Text = startOfMonth.ToString("yyyyMMdd");
            mtb_To.Text = DateTime.Today.ToString("yyyyMMdd");
        }
        private void SetDateTime()
        {
            ////ini 날짜 불러와서 기간 설정하기
            chkInsDate.Checked = true;
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

            //mtb_From.Text = DateTime.Today.AddDays(-Days).ToString("yyyyMMdd");
            //mtb_To.Text = DateTime.Today.ToString("yyyyMMdd");

            //2022-09-15 오늘을 기준으로 1주일 기간이 보이도록 수정
            mtb_From.Text = DateTime.Today.ToString("yyyyMMdd");
            mtb_To.Text = DateTime.Today.AddDays(7).ToString("yyyyMMdd");
        }
        private void InitPanel()
        {
            //tlpForm.Dock = DockStyle.Fill;
            //foreach (Control control in tlpForm.Controls)
            //{
            //    control.Dock = DockStyle.Fill;
            //    control.Margin = new Padding(1, 1, 1, 1);
            //    foreach (Control ctl in control.Controls)//tlp 상위에서 3번째
            //    {
            //        ctl.Dock = DockStyle.Fill;
            //        ctl.Margin = new Padding(1, 1, 1, 1);
            //        foreach (Control con in ctl.Controls)
            //        {
            //            con.Dock = DockStyle.Fill;
            //            con.Margin = new Padding(1, 1, 1, 1);
            //            foreach (Control co in con.Controls)
            //            {
            //                co.Dock = DockStyle.Fill;
            //                co.Margin = new Padding(1, 1, 1, 1);
            //                foreach (Control c in co.Controls)
            //                {
            //                    c.Dock = DockStyle.Fill;
            //                    c.Margin = new Padding(1, 1, 1, 1);
            //                    foreach (Control contro in c.Controls)
            //                    {
            //                        contro.Dock = DockStyle.Fill;
            //                        contro.Margin = new Padding(1, 1, 1, 1);
            //                        foreach (Control contr in contro.Controls)
            //                        {
            //                            contr.Dock = DockStyle.Fill;
            //                            contr.Margin = new Padding(1, 1, 1, 1);
            //                            foreach (Control cont in contr.Controls)
            //                            {
            //                                cont.Dock = DockStyle.Fill;
            //                                cont.Margin = new Padding(1, 1, 1, 1);
            //                            }
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}

        }

        #region Default Grid Setting

        private void InitGrid()
        {
            grdData.Columns.Clear();
            grdData.ColumnCount = 28;
            // Set the Colums Hearder Names
            int i = 0;

            grdData.Columns[i].Name = "RowSeq";
            grdData.Columns[i].HeaderText = "";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "StartDate";
            grdData.Columns[i].HeaderText = "지시일자";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "CustomID";
            grdData.Columns[i].HeaderText = "거래처코드";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;

            grdData.Columns[++i].Name = "KCustom";
            grdData.Columns[i].HeaderText = "거래처";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;

            grdData.Columns[++i].Name = "BuyerModelID";
            grdData.Columns[i].HeaderText = "차종코드";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;

            grdData.Columns[++i].Name = "Model";
            grdData.Columns[i].HeaderText = "차종명";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;

            grdData.Columns[++i].Name = "ArticleID";
            grdData.Columns[i].HeaderText = "품목코드";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;        
            
            grdData.Columns[++i].Name = "BuyerArticleNo";
            grdData.Columns[i].HeaderText = "품번";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "InstID";
            grdData.Columns[i].HeaderText = "지시번호";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;

            grdData.Columns[++i].Name = "OrderID";
            grdData.Columns[i].HeaderText = "관리번호";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;

            grdData.Columns[++i].Name = "InstQty";
            grdData.Columns[i].HeaderText = "지시수량";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "Process";
            grdData.Columns[i].HeaderText = "공정";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "MachineID";
            grdData.Columns[i].HeaderText = "호기";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;

            grdData.Columns[++i].Name = "Article";
            grdData.Columns[i].HeaderText = "품명";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;


            grdData.Columns[++i].Name = "ToTalWorkQty";
            grdData.Columns[i].HeaderText = "작업완료수량";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "WorkQty";
            grdData.Columns[i].HeaderText = "합격수량";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "DefectQty";
            grdData.Columns[i].HeaderText = "불량수량";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;


            grdData.Columns[++i].Name = "LotID";
            grdData.Columns[i].HeaderText = "생산LOTID";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            //grdData.Columns[++i].Name = "InstQty";
            //grdData.Columns[i].HeaderText = "지시수량";
            //grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            //grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            //grdData.Columns[i].ReadOnly = true;
            //grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "LabelPrintQty";
            grdData.Columns[i].HeaderText = "발행수량";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;

            grdData.Columns[++i].Name = "OrderQty";
            grdData.Columns[i].HeaderText = "오더수량";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;

            grdData.Columns[++i].Name = "QtyPerBox";
            grdData.Columns[i].HeaderText = "박스당수량";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;

            grdData.Columns[++i].Name = "InstDetSeq";
            grdData.Columns[i].HeaderText = "InstDetSeq";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;

            grdData.Columns[++i].Name = "LabelPrintYN";
            grdData.Columns[i].HeaderText = "LabelPrintYN";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;

            grdData.Columns[++i].Name = "PATTERNID";
            grdData.Columns[i].HeaderText = "PATTERNID";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;

            grdData.Columns[++i].Name = "ProcessID";
            grdData.Columns[i].HeaderText = "ProcessID";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;

            grdData.Columns[++i].Name = "MachineID";
            grdData.Columns[i].HeaderText = "MachineID";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;

            grdData.Columns[++i].Name = "OverYN";
            grdData.Columns[i].HeaderText = "OverYN";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;         

            grdData.Columns[++i].Name = "PrevProcessCompletYN";
            grdData.Columns[i].HeaderText = "이전공정";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;


            grdData.Font = new Font("맑은 고딕", 13);
            grdData.RowTemplate.DefaultCellStyle.Font = new Font("맑은 고딕", 14, FontStyle.Bold);
            grdData.RowTemplate.Height = 32;
            grdData.ColumnHeadersHeight = 35;
            grdData.ScrollBars = ScrollBars.Both;
            grdData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grdData.MultiSelect = false;
            grdData.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //grdData.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(234, 234, 234);
            //grdData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grdData.ReadOnly = true;

            grdData.EnableHeadersVisualStyles = false;  // 헤더 셀 스타일 적용 용도.

            foreach (DataGridViewColumn col in grdData.Columns)
            {              
                col.DataPropertyName = col.Name;
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            return;


        }

        #endregion

        //콤보박스 데이터 바인딩
        private void SetProcessComboBox()
        {
            int intnChkProc = 1;
            string strProcessID = "";

            DataSet ds = null;

            strProcessID =Frm_tprc_Main.gs.GetValue("Work", "ProcessID", "ProcessID");
            string[] gubunProcess = strProcessID.Split(new char[] { '|' });

            //공정 가져오기
            try
            {
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

                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Add(Work_sProcess.NCHKPROC, intnChkProc);//cboProcess.Text 
                sqlParameter.Add(Work_sProcess.PROCESSID, strProcessID);//cboProcess.Text

                ds = DataStore.Instance.ProcedureToDataSet("[xp_Work_sProcess]", sqlParameter, false);

                DataRow newRow = ds.Tables[0].NewRow();
                newRow[Work_sProcess.PROCESSID] = "*";
                newRow[Work_sProcess.PROCESS] = "전체";

                DataRow newRow2 = ds.Tables[0].NewRow();
                newRow2[Work_sProcess.PROCESSID] = strProcessID;
                newRow2[Work_sProcess.PROCESS] = "부분전체";

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    ds.Tables[0].Rows.InsertAt(newRow, 0);
                    ds.Tables[0].Rows.InsertAt(newRow2, 1);
                    cboProcess.DataSource = ds.Tables[0];
                }

                cboProcess.ValueMember = Work_sProcess.PROCESSID;
                cboProcess.DisplayMember = Work_sProcess.PROCESS;

                DataStore.Instance.CloseConnection(); //2021-10-07 DB 커넥트 연결 해제
            }
            catch (Exception excpt)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message), "[오류]", 0, 1);
            }
            return;
        }



        public void procQuery()
        {
            string strErr = "";

            grdData.Rows.Clear();

            int intnchkInstDate = 0;

            string strStartDate = "";
            string strEndDate = "";
            int intnChkProcessID = 0;
            string strProcessID = "";

            int intnChkBuyerArticle = 0;
            string strBuyerArticle = "";
            int intnChkLotID = 0;
            string strLotID = "";


            double InstQty = 0;
            int intnChkCompleteYN = 0;

            //2023-05-08
            string strMachineID = "";

            DataSet ds = null;

            strMachineID = Frm_tprc_Main.gs.GetValue("Work", "Machine", "Machine");
            string[] gubunMachineID = strMachineID.Split(new char[] { '|' });

            strMachineID = string.Empty;

            for (int i = 0; i < gubunMachineID.Length; i++)
            {
                if (strMachineID.Equals(string.Empty))
                {
                    strMachineID = gubunMachineID[i].Substring(4,2);
                }
                else
                {
                    strMachineID = strMachineID + "|" + gubunMachineID[i].Substring(4, 2);
                }
            }

            // 검색일자 체크
            if (chkInsDate.Checked)
            {
                intnchkInstDate = 1;
                strStartDate = mtb_From.Text.Replace("-", "");
                strEndDate = mtb_To.Text.Replace("-", "");
                if ((int.Parse(strStartDate) - int.Parse(strEndDate)) > 0)
                {
                    string Message = "[지시일] 시작일이 종료일보다 늦을 수 없습니다.";
                    WizCommon.Popup.MyMessageBox.ShowBox(Message, "[검색조건]", 0, 1);
                    return;
                }
            }

            // 완료일자 포함여부 체크.
            if (chkComplete.Checked)
            {
                intnChkCompleteYN = 1;
            }

            // 작지번호 기입여부 체크
            if (chkPLotID.Checked)
            {
                if (this.txtPLotID.Text == "" || txtPLotID.Text == "전체")
                {
                    Message[0] = "[검색조건]";
                    Message[1] = "LOTID 를 입력하시기 바랍니다.!!";
                    WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                    this.txtPLotID.Focus();

                    return;
                }
                //strInstID = txtPSabun.Text.Trim().Substring(2, 12);//지시번호
                intnChkLotID = 1;
                strLotID = txtPLotID.Text.Trim();
            }

            // 품번 기입여부 체크
            if (chkBuyerArticle.Checked)
            {
                if (this.txtBuyerArticle.Text == "" || txtBuyerArticle.Text == string.Empty)
                {
                    Message[0] = "[검색조건]";
                    Message[1] = "품번 을 입력하시기 바랍니다.!!";
                    WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                    this.txtBuyerArticle.Focus();

                    return;
                }

                //if (chkBuyerArticle.Checked == false || (chkBuyerArticle.Checked == true && cboBuyerArticle.SelectedIndex == 0)) //공정체크 : N
                //{
                //    intnChkBuyerArticle = 0;
                //    strBuyerArticle = "";
                //}
                //else
                //{
                    intnChkBuyerArticle = 1;
                    strBuyerArticle = txtBuyerArticle.Text.Trim();
                //}
            }



            // 공정 값체크
            if (chkProcess.Checked == false || (chkProcess.Checked == true && cboProcess.SelectedIndex == 0)) //공정체크 : N
            {
                intnChkProcessID = 0;
                strProcessID = "";
            }
            else if (chkProcess.Checked == true && cboProcess.SelectedIndex > 0) // 공정체크 : O
            {
                intnChkProcessID = 1;
                try
                {
                    strProcessID = cboProcess.SelectedValue.ToString();
                }
                catch (Exception e1)
                {
                    strErr = e1.Message.ToString();

                    strProcessID = "";
                }
            }

            if (chkInsDate.Checked == false && chkProcess.Checked == false && chkBuyerArticle.Checked == false && intnchkInstDate == 0 && intnChkProcessID == 0 &&  intnChkLotID == 0)
            {
                WizCommon.Popup.MyMessageBox.ShowBox("최소한 하나의 검색조건을 선택하세요.\n ※검색조건 선택은 버튼을 눌러 오목하게 들어간 모양으로 만들어주세요!!", "[검색조건]", 0, 1);
                return;
            }
            try
            {
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();

                sqlParameter.Add("nchkInstDate", intnchkInstDate);
                sqlParameter.Add("FromDate", strStartDate);
                sqlParameter.Add("ToDate", strEndDate);
                sqlParameter.Add("nChkProcessID", intnChkProcessID);
                sqlParameter.Add("ProcessID", strProcessID);

                sqlParameter.Add("nChkBuyerArticle", intnChkBuyerArticle);
                sqlParameter.Add("BuyerArticle", strBuyerArticle);
                sqlParameter.Add("nChkLotID", intnChkLotID);
                sqlParameter.Add("LotID", strLotID);
                sqlParameter.Add("nChkCompleteYN", intnChkCompleteYN);

                sqlParameter.Add("nChkMachineID", 1);
                sqlParameter.Add("MachineID", strMachineID);

                DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_WizWork_splInputDet", sqlParameter, false);


                if (dt != null && dt.Rows.Count > 0)
                {
                    int i = 0;
                    int OrderQty = 0;
                    int ProdQtyPerBox = 0;
                    int LabelPrintQty = 0;

                    double TotalWorkQty = 0;
                    double WorkQty = 0;
                    double DefectQty = 0;

                    DataGridViewRow dgvr = null;

                    foreach (DataRow dr in dt.Rows)
                    {
                        string PrevProcessCompleYN = "X";                                                
                        
                        double.TryParse(dr["InstQty"].ToString(), out InstQty);
                        int.TryParse(dr["ColorQty"].ToString(), out OrderQty);       //2022-02-22   OrderQty -> ColorQty 수정              
                        int.TryParse(dr["ProdQtyPerBox"].ToString(), out ProdQtyPerBox);
                        int.TryParse(dr["LabelPrintQty"].ToString(), out LabelPrintQty);

                        double.TryParse(dr["Workqty"].ToString(), out WorkQty);
                        double.TryParse(dr["DefectQty"].ToString(), out DefectQty);
                        double.TryParse(dr["ToTalWorkQty"].ToString(), out TotalWorkQty);

                        grdData.Rows.Add(   ++i,                            
                                            Lib.MakeDate(WizWorkLib.DateTimeClss.DF_MID, dr["StartDate"].ToString()),                                                
                                                Lib.CheckNull(dr["CustomID"].ToString()),
                                                Lib.CheckNull(dr["KCustom"].ToString()),
                                                Lib.CheckNull(dr["BuyerModelID"].ToString()),
                                                Lib.CheckNull(dr["Model"].ToString()),
                                                Lib.CheckNull(dr["ArticleID"].ToString().Trim()),                                              
                                                Lib.CheckNull(dr["BuyerArticleNo"].ToString()),
                                                Lib.CheckNull(dr["InstID"].ToString()),
                                                Lib.CheckNull(dr["OrderID"].ToString()),
                                                string.Format("{0:n0}", InstQty),          
                                                Lib.CheckNull(dr["Process"].ToString()),
                                                Lib.CheckNull(dr["MachineID"].ToString()),
                                                Lib.CheckNull(dr["Article"].ToString()),
                                                string.Format("{0:n0}", (int)TotalWorkQty),    // 작업완료수량
                                                string.Format("{0:n0}", (int)WorkQty),         // 합격수량
                                                string.Format("{0:n0}", (int)DefectQty),       // 불량수량
                                                Lib.CheckNull(dr["LotID"].ToString()),                                                 
                                                string.Format("{0:n0}", LabelPrintQty),
                                                string.Format("{0:n0}", OrderQty),
                                                string.Format("{0:n0}", ProdQtyPerBox),
                                                Lib.CheckNull(dr["InstDetSeq"].ToString()),
                                                Lib.CheckNull(dr["LabelPrintYN"].ToString()),
                                                Lib.CheckNull(dr["PATTERNID"].ToString()),
                                                Lib.CheckNull(dr["ProcessID"].ToString()),
                                                Lib.CheckNull(dr["MachineID"].ToString()),
                                                Lib.CheckNull(dr["OverYN"].ToString()),                                                 
                                                PrevProcessCompleYN //
                        );                        
                        dgvr = grdData.Rows[i - 1];
                        dgvr.Cells["DefectQty"].Style.ForeColor = Color.Red;
                        //작업을 했네.                                          
                    }
                }
                else
                {
                    //((WizWork.Frm_tprc_Main)(this.MdiParent)).SetstbLookUp("0개의 자료가 검색되었습니다.");
                    grdData.Rows.Clear();
                }
                Frm_tprc_Main.gv.queryCount = string.Format("{0:n0}", grdData.RowCount);
                Frm_tprc_Main.gv.SetStbInfo();
                DataStore.Instance.CloseConnection(); //2021-10-07 DB 커넥트 연결 해제
            }
            catch (Exception excpt)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message), "[오류]", 0, 1);
            }
        }


        /// <summary>
        /// 조회버튼 클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLookup_Click(object sender, EventArgs e)
        {
            btnLookup.Enabled = false;
            Lib.Delay(3000); //2021-11-10 버튼을 여러번 클릭해도 한번만 클릭되게 딜레이 추가
            LogData.LogSave(this.GetType().Name, "R"); //2022-06-22 조회
            procQuery();
            WorkingMachine_btnSetting();

            btnLookup.Enabled = true;

        }
  
        private void cmdRowUp_Click(object sender, EventArgs e)
        {
            Lib.btnRowUp(grdData);
        }

        private void cmdRowDown_Click(object sender, EventArgs e)
        {
            Lib.btnRowDown(grdData);
        }

        public void SetsetProcessFormLoad()
        {
            frm_tprc_setProcess Set_ps = new frm_tprc_setProcess(strLotID, strMachineID, strProcessID, strInstID);
            Set_ps.Owner = this;
            if (Set_ps.ShowDialog() == DialogResult.OK)
            {

            };
        }
        public void Set_stbInfo()
        {
            if (MdiParent == null)
            {
                MdiParent = new Frm_tprc_Main();
            }
            ((WizWork.Frm_tprc_Main)(MdiParent)).Set_stsInfo();
        }

        private void cboProcess_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string strProcess = cboProcess.SelectedValue.ToString();
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Add(Work_sProcess.PROCESSID, strProcess);//cboProcess.Text 

                DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_Work_sMachinebyProcess", sqlParameter, false);
                //DataRow newRow = dt.NewRow();
                //newRow[Work_sMachineByProcess.MACHINEID] = "*";
                //newRow[Work_sMachineByProcess.MACHINENO] = "전체";
                //dt.Rows.InsertAt(newRow, 0);

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

                if (strProcess == "2101") // 성형작업시. 머신이 너무 길어. 
                {
                    newRow[Work_sMachineByProcess.MACHINEID] = "*";
                    newRow[Work_sMachineByProcess.MACHINENO] = "전체";
                }
                else
                {
                    newRow[Work_sMachineByProcess.MACHINEID] = "*";
                    newRow[Work_sMachineByProcess.MACHINE] = "전체";
                }
                //newRow[Work_sMachineByProcess.MACHINEID] = "*";
                //newRow[Work_sMachineByProcess.MACHINE] = "전체";
                dt2.Rows.InsertAt(newRow, 0);
                DataStore.Instance.CloseConnection(); //2021-10-07 DB 커넥트 연결 해제

            }
            catch (Exception excpt)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message), "[오류]", 0, 1);
            }
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            if (WizCommon.Popup.MyMessageBox.ShowBox("공정작업 화면을 종료하시겠습니까?", "[공정작업 종료]", 0, 0) == DialogResult.OK)
            {
                LogData.LogSave(this.GetType().Name, "S"); //2022-06-22 사용시간(로드, 닫기)
                Dispose();
                Close();
            }
            return;
        }

        #region 안쓰는 함수들
        public bool SetJobIDBy0405()
        {
            return true;
        }

        

        public void SetWork_UFormLoad()
        {
            //Frm_tprc_Work_U WkU_NO = new Frm_tprc_Work_U();
            //WkU_NO.Owner = this;
            //WkU_NO.ShowDialog();

        }

        

        

        private string GetJobIDBy0405()
        {
            string JobID_0401 = "";
            Frm_PopUp_sJobIDBy0405 fpjib = new Frm_PopUp_sJobIDBy0405();
            fpjib.WriteTextEvent += new Frm_PopUp_sJobIDBy0405.TextEventHandler(GetData);

            void GetData(string JobID)
            {
                JobID_0401 = JobID;
            }
            fpjib.Show();
            return JobID_0401;
            if (fpjib.ShowDialog() == DialogResult.No)
            {
                return "";
            }
            else
            {
                return "";
            }
            //else
            //{

            //}

        }


        

        


        private void dgvLookupResult_CurrentCellChanged(object sender, EventArgs e)
        {
            //e.RowIndex

        }

        private void cboProcess_Click(object sender, EventArgs e)
        {
            SetProcessComboBox();
        }

        #endregion

        private void chkBuyerArticle_Click(object sender, EventArgs e)
        {
            if (this.chkBuyerArticle.Checked)
            {
                try
                {
                    chkBuyerArticle.Checked = true;
                    //2021-07-20
                    //var path64 = System.IO.Path.Combine(Directory.GetDirectories(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "winsxs"), "amd64_microsoft-windows-osk_*")[0], "osk.exe");
                    //var path32 = @"C:\windows\system32\osk.exe";
                    //var path = (Environment.Is64BitOperatingSystem) ? path64 : path32;
                    //if (File.Exists(path) && !Frm_tprc_Main.Lib.ReturnKillRunningProcess("osk"))
                    //{

                    //    System.Diagnostics.Process.Start(path);
                    //    txtBuyerArticle.Focus();

                    //}

                }
                catch (Exception ex)
                {
                    
                    //txtBuyerArticle.Text = "";
                    //POPUP.Frm_CMKeypad keypad = new POPUP.Frm_CMKeypad("품번입력", "품번");

                    //keypad.Owner = this;
                    //if (keypad.ShowDialog() == DialogResult.OK)
                    //{
                    //    txtBuyerArticle.Text = keypad.tbInputText.Text;
                    //    procQuery();
                    //}


                    //useMasicKeyboard(txtBuyerArticle);
                }
            }
            else
            {
                //this.txtBuyerArticle.Text = "";
                chkBuyerArticle.Checked = false;
            }
        }
        private void txtBuyerArticle_Click(object sender, EventArgs e)
        {
            if (this.chkBuyerArticle.Checked)
            {
                try
                {
                    //chkBuyerArticle.Checked = false;
                    ////2021-07-20
                    var path64 = System.IO.Path.Combine(Directory.GetDirectories(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "winsxs"), "amd64_microsoft-windows-osk_*")[0], "osk.exe");
                    var path32 = @"C:\windows\system32\osk.exe";
                    var path = (Environment.Is64BitOperatingSystem) ? path64 : path32;
                    if (File.Exists(path) && !Frm_tprc_Main.Lib.ReturnKillRunningProcess("osk"))
                    {
                        System.Diagnostics.Process.Start(path);

                        txtBuyerArticle.Focus();

                    }
                }
                catch (Exception ex) {
                    txtBuyerArticle.Text = "";
                    POPUP.Frm_CMKeypad keypad = new POPUP.Frm_CMKeypad("품번입력", "품번");

                    keypad.Owner = this;
                    if (keypad.ShowDialog() == DialogResult.OK)
                    {
                        txtBuyerArticle.Text = keypad.tbInputText.Text;
                        procQuery();
                    }

                    useMasicKeyboard(txtBuyerArticle);
                }
            }
            else
            {
                try
                {
                    chkBuyerArticle.Checked = true;

                    //2021-07-20
                    var path64 = System.IO.Path.Combine(Directory.GetDirectories(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "winsxs"), "amd64_microsoft-windows-osk_*")[0], "osk.exe");
                    var path32 = @"C:\windows\system32\osk.exe";
                    var path = (Environment.Is64BitOperatingSystem) ? path64 : path32;
                    if (File.Exists(path) && !Frm_tprc_Main.Lib.ReturnKillRunningProcess("osk"))
                    {
                        System.Diagnostics.Process.Start(path);

                        txtBuyerArticle.Focus();

                    }
                }
                catch (Exception ex)
                {
                    txtBuyerArticle.Text = "";
                    POPUP.Frm_CMKeypad keypad = new POPUP.Frm_CMKeypad("품번입력", "품번");

                    keypad.Owner = this;
                    if (keypad.ShowDialog() == DialogResult.OK)
                    {
                        txtBuyerArticle.Text = keypad.tbInputText.Text;
                        procQuery();
                    }

                    useMasicKeyboard(txtBuyerArticle);
                }
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
                catch(Exception ex2)
                {
                    System.Diagnostics.Process.Start(@"C:\windows\SysWOW64\osk.exe"); 
                    Console.Write(ex2.Message);
                }
               
            }
            txtBox.Select();
            txtBox.Focus();
        }

        #endregion

        // 품번 Enter → 검색
        private void txtBuyerArticleNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                procQuery();
                LogData.LogSave(this.GetType().Name, "R"); //2022-06-22 조회
            }
        }

        private void chkPLotID_Click(object sender, EventArgs e)
        {
            if (this.chkPLotID.Checked)
            {
                txtPLotID.Text = "";
                POPUP.Frm_CMKeypad keypad = new POPUP.Frm_CMKeypad("작지번호 입력", "번호");

                keypad.Owner = this;
                if (keypad.ShowDialog() == DialogResult.OK)
                {
                    txtPLotID.Text = keypad.tbInputText.Text;
                    procQuery();
                    LogData.LogSave(this.GetType().Name, "R"); //2022-06-22 조회
                }
            }
            else
            {
                this.txtPLotID.Text = "";
            }
        }

        private void txtPLotID_Click(object sender, EventArgs e)
        {
            if (this.chkPLotID.Checked)
            {
                txtPLotID.Text = "";
                POPUP.Frm_CMKeypad keypad = new POPUP.Frm_CMKeypad("작지번호 입력", "번호");

                keypad.Owner = this;
                if (keypad.ShowDialog() == DialogResult.OK)
                {
                    txtPLotID.Text = keypad.tbInputText.Text;
                    procQuery();
                    LogData.LogSave(this.GetType().Name, "R"); //2022-06-22 조회
                }
            }
            else
            {
                chkPLotID.Checked = true;
                txtPLotID.Text = "";
                POPUP.Frm_CMKeypad keypad = new POPUP.Frm_CMKeypad("작지번호 입력", "번호");

                keypad.Owner = this;
                if (keypad.ShowDialog() == DialogResult.OK)
                {
                    txtPLotID.Text = keypad.tbInputText.Text;
                    procQuery();
                    LogData.LogSave(this.GetType().Name, "R"); //2022-06-22 조회
                }
            }
        }



        private void Frm_tprc_PlanInput_Q_Activated(object sender, EventArgs e)
        {
            procQuery();

            WorkingMachine_btnSetting();
        }

        private void chkInsDate_Click(object sender, EventArgs e)
        {
            if (chkInsDate.Checked)
            {
                mtb_From.Enabled = true;
                mtb_To.Enabled = true;
                btnCal_From.Enabled = true;
                btnCal_To.Enabled = true;
            }
            else
            {
                mtb_From.Enabled = false;
                mtb_To.Enabled = false;
                btnCal_From.Enabled = false;
                btnCal_To.Enabled = false;
            }
        }

        private void tlpForm_Paint(object sender, PaintEventArgs e)
        {

        }

        private void grdData_CurrentCellChanged(object sender, EventArgs e)
        {
            if (((DataGridView)(sender)).SelectedRows.Count > 0)
            {
                int i = ((DataGridView)(sender)).SelectedCells[0].RowIndex;
                if (((DataGridView)(sender)).Rows[i].Cells["OrderID"].Value.ToString().Trim().Length > 0)// != "")
                {
                    Frm_tprc_Main.g_tBase.OrderID = ((DataGridView)(sender)).Rows[i].Cells["OrderID"].Value.ToString();
                }
                Frm_tprc_Main.g_tBase.sLotID = ((DataGridView)(sender)).Rows[i].Cells["LotID"].Value.ToString();
                Frm_tprc_Main.g_tBase.sInstID = ((DataGridView)(sender)).Rows[i].Cells["InstID"].Value.ToString();
            }
        }

        

        // 설비점검 및 자주검사 체크로직. >> set process 화면 생략에 따른 조치.
        public bool LF_ChkMachineCheck()
        {
            // '***************************************************************
            // '0:공정작업입력 시 설비 점검(하루1회이상) 및 자주검사 수행(작업지시별 1회이상) check
            // '***************************************************************
            bool blResult = false;
            bool bFirst = false;

            string strMachine = "";
            string[] MachineTemp = null;

            DataSet ds = null;
            string strMessage = "";
            string strMessageInspect = "";

            int intResult = 0;
            int intNoWorkTime = 0;
            int inAutoInspect = 0;

            Tools.INI_GS gs = new Tools.INI_GS();

            strMachine = Frm_tprc_Main.gs.GetValue("Work", "Machine", "");

            if (strMachine != "")
            {
                MachineTemp = strMachine.Split('|');//머신
                foreach (string str in MachineTemp)
                {
                    if (str == strProcessID + Frm_tprc_Main.g_tBase.MachineID)
                    {
                        Dictionary<string, object> sqlParameter = new Dictionary<string, object>();

                        sqlParameter.Add("ProcessID", Frm_tprc_Main.g_tBase.ProcessID);
                        sqlParameter.Add("MachineID", Frm_tprc_Main.g_tBase.MachineID);
                        sqlParameter.Add("PLotID", Frm_tprc_Main.g_tBase.sLotID);

                        DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_WizWork_sToDayMcRegularInspectAutoYN", sqlParameter, false);
                        if (dt != null && dt.Rows.Count == 1)
                        {
                            DataRow dr = null;
                            dr = dt.Rows[0];

                            int.TryParse(dr["result"].ToString(), out intResult);

                            if (intResult < 0)
                            {
                                MessageBox.Show(string.Format("처리할 공정 및 호기를 설정하세요.", "공정및호기 설정오류"));
                                return blResult;
                            }
                            int.TryParse(dr["NoWorkTime"].ToString(), out intNoWorkTime);//'계획정지시간 이 없는 건만 Check
                            if (intNoWorkTime == 0)
                            {
                                if (intResult == 0)
                                {
                                    if (bFirst)
                                    {
                                        strMessage = dr["McName"].ToString().Trim();
                                    }
                                    else
                                    {
                                        if (strMessage == "")
                                        {
                                            strMessage = dr["McName"].ToString().Trim();
                                        }
                                        else
                                        {
                                            strMessage = strMessage + ",  " + dr["McName"].ToString().Trim();
                                        }
                                    }
                                    if (strMessage != "")
                                    {
                                        Message[0] = "[설비점검 오류]";
                                        Message[1] = strMessage + "의 설비점검을 하셔야합니다.";
                                        WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                                        //timer1.Stop();
                                        return false;
                                    }
                                }
                                if (Frm_tprc_Main.g_tBase.ProcessID == "0405")
                                {
                                    if (intResult < 4)
                                    {
                                        Message[0] = "[설비점검 오류]";
                                        Message[1] = "CMB/혼련 공정에서는 총 4기기의 설비점검을 모두 하셔야합니다.";
                                        WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                                        //timer1.Stop();
                                        return false;
                                    }
                                }
                                int.TryParse(dr["AutoInspectByInstID"].ToString(), out inAutoInspect);
                                if (inAutoInspect == 0 && Frm_tprc_Main.g_tBase.ProcessID != "0405")
                                {
                                    if (bFirst == true)
                                    {
                                        strMessageInspect = dr["McName"].ToString().Trim();

                                    }
                                    else
                                    {
                                        if (strMessageInspect == "")
                                        {
                                            strMessageInspect = dr["McName"].ToString().Trim();
                                        }
                                        else
                                        {
                                            strMessageInspect = strMessageInspect + ",  " + dr["McName"].ToString().Trim();
                                        }
                                    }


                                    if (strMessageInspect != "")
                                    {
                                        //Message[0] = "[자주검사 오류]";
                                        //Message[1] = strMessageInspect + "의 자주검사를 하셔야합니다.";
                                        //WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                                        //timer1.Stop();
                                        // 2019.0305 허윤구 >> 2019.0320 허윤구, 자주검사 체크 안한다 했음.
                                    }
                                }
                            }
                            DataStore.Instance.CloseConnection(); //2021-10-07 DB 커넥트 연결 해제

                        }
                        else
                        {
                            Message[0] = "[공정및호기 설정오류]";
                            Message[1] = "처리할 공정 및 호기를 설정하세요.";
                            WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                            return false;
                        }
                    }
                }
            }

            return true;
        }  

        #region(통신페이지로 넘어가도 좋은지 체크) btnPLC_Transfer_AccessCheck_YN
        private bool btnPLC_Transfer_AccessCheck_YN()
        {
            // 1. 통신을 볼려면.. 성형이어야 겠지?
            if (grdData.SelectedRows.Count > 0 && grdData.SelectedRows.Count == 1)
            {
                string ChkProcess = grdData.SelectedRows[0].Cells["ProcessID"].Value.ToString();
                if (ChkProcess == "1101")
                {
                    // 재단은 필요없자나..
                    WizCommon.Popup.MyMessageBox.ShowBox("선택한 작지는 재단 작업지시입니다. \r\n 금형변경 버튼을 선택할 수 없습니다.", "[공정오류]", 0, 1);
                    return false;
                }
                return true;
            }
            else
            { return false; }
        }

        #endregion


        // 그리드 헤더 컬럼 셀렉션 ASC, DESC 이벤트 먹이기. 2019.05.13 허윤구
        private void grdData_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int point = e.ColumnIndex;
            if (grdData.SortOrder.ToString() == "Ascending") // Check if sorting is Ascending
            {
                grdData.Sort(grdData.Columns[point], ListSortDirection.Descending);
            }
            else
            {
                grdData.Sort(grdData.Columns[point], ListSortDirection.Ascending);
            }
        }


        // 2019.03.20 허윤구. 작업중인 아이들 버튼화로 별도분리.
        private void WorkingMachine_btnSetting()
        {
            //2023-03-22
            //PersonNameList.Clear();
            //LotidList.Clear();
            //Machine_NameList.Clear();
            //Process_NameList.Clear();

            try
            {
                ////
                ///0. 버튼 초기화.
                ////
                btnMoldW1.Enabled = true;
                btnMoldW1.BackColor = SystemColors.Control;
                btnMoldW1.Text = "작업1";

                btnMoldW2.Enabled = true;
                btnMoldW2.BackColor = SystemColors.Control;
                btnMoldW2.Text = "작업2";

                btnMoldW3.Enabled = true;
                btnMoldW3.BackColor = SystemColors.Control;
                btnMoldW3.Text = "작업3";

                btnMoldW4.Enabled = true;
                btnMoldW4.BackColor = SystemColors.Control;
                btnMoldW4.Text = "작업4";

                btnMoldW5.Enabled = true;
                btnMoldW5.BackColor = SystemColors.Control;
                btnMoldW5.Text = "작업5";

                btnMoldW6.Enabled = true;
                btnMoldW6.BackColor = SystemColors.Control;
                btnMoldW6.Text = "작업6";

                btnMoldW7.Enabled = true;
                btnMoldW7.BackColor = SystemColors.Control;
                btnMoldW7.Text = "작업7";

                btnMoldW8.Enabled = true;
                btnMoldW8.BackColor = SystemColors.Control;
                btnMoldW8.Text = "작업8";

                btnMoldW9.Enabled = true;
                btnMoldW9.BackColor = SystemColors.Control;
                btnMoldW9.Text = "작업9";

                btnMoldW10.Enabled = true;
                btnMoldW10.BackColor = SystemColors.Control;
                btnMoldW10.Text = "작업10";

                //11 ~ 20
                btnMoldW11.Enabled = true;
                btnMoldW11.BackColor = SystemColors.Control;
                btnMoldW11.Text = "작업11";

                btnMoldW12.Enabled = true;
                btnMoldW12.BackColor = SystemColors.Control;
                btnMoldW12.Text = "작업12";

                btnMoldW13.Enabled = true;
                btnMoldW13.BackColor = SystemColors.Control;
                btnMoldW13.Text = "작업13";

                btnMoldW14.Enabled = true;
                btnMoldW14.BackColor = SystemColors.Control;
                btnMoldW14.Text = "작업14";

                btnMoldW15.Enabled = true;
                btnMoldW15.BackColor = SystemColors.Control;
                btnMoldW15.Text = "작업15";

                btnMoldW16.Enabled = true;
                btnMoldW16.BackColor = SystemColors.Control;
                btnMoldW16.Text = "작업16";

                btnMoldW17.Enabled = true;
                btnMoldW17.BackColor = SystemColors.Control;
                btnMoldW17.Text = "작업17";

                btnMoldW18.Enabled = true;
                btnMoldW18.BackColor = SystemColors.Control;
                btnMoldW18.Text = "작업18";

                btnMoldW19.Enabled = true;
                btnMoldW19.BackColor = SystemColors.Control;
                btnMoldW19.Text = "작업19";

                btnMoldW20.Enabled = true;
                btnMoldW20.BackColor = SystemColors.Control;
                btnMoldW20.Text = "작업20";


                //21 ~ 30
                btnMoldW21.Enabled = true;
                btnMoldW21.BackColor = SystemColors.Control;
                btnMoldW21.Text = "작업21";

                btnMoldW22.Enabled = true;
                btnMoldW22.BackColor = SystemColors.Control;
                btnMoldW22.Text = "작업22";

                btnMoldW23.Enabled = true;
                btnMoldW23.BackColor = SystemColors.Control;
                btnMoldW23.Text = "작업23";

                btnMoldW24.Enabled = true;
                btnMoldW24.BackColor = SystemColors.Control;
                btnMoldW24.Text = "작업24";

                btnMoldW25.Enabled = true;
                btnMoldW25.BackColor = SystemColors.Control;
                btnMoldW25.Text = "작업25";

                btnMoldW26.Enabled = true;
                btnMoldW26.BackColor = SystemColors.Control;
                btnMoldW26.Text = "작업26";

                btnMoldW27.Enabled = true;
                btnMoldW27.BackColor = SystemColors.Control;
                btnMoldW27.Text = "작업27";

                btnMoldW28.Enabled = true;
                btnMoldW28.BackColor = SystemColors.Control;
                btnMoldW28.Text = "작업28";

                btnMoldW29.Enabled = true;
                btnMoldW29.BackColor = SystemColors.Control;
                btnMoldW29.Text = "작업29";

                btnMoldW30.Enabled = true;
                btnMoldW30.BackColor = SystemColors.Control;
                btnMoldW30.Text = "작업30";

                //31 ~ 40
                btnMoldW31.Enabled = true;
                btnMoldW31.BackColor = SystemColors.Control;
                btnMoldW31.Text = "작업31";

                btnMoldW32.Enabled = true;
                btnMoldW32.BackColor = SystemColors.Control;
                btnMoldW32.Text = "작업32";

                btnMoldW33.Enabled = true;
                btnMoldW33.BackColor = SystemColors.Control;
                btnMoldW33.Text = "작업33";

                btnMoldW34.Enabled = true;
                btnMoldW34.BackColor = SystemColors.Control;
                btnMoldW34.Text = "작업34";

                btnMoldW35.Enabled = true;
                btnMoldW35.BackColor = SystemColors.Control;
                btnMoldW35.Text = "작업35";

                btnMoldW36.Enabled = true;
                btnMoldW36.BackColor = SystemColors.Control;
                btnMoldW36.Text = "작업36";

                btnMoldW37.Enabled = true;
                btnMoldW37.BackColor = SystemColors.Control;
                btnMoldW37.Text = "작업37";

                btnMoldW38.Enabled = true;
                btnMoldW38.BackColor = SystemColors.Control;
                btnMoldW38.Text = "작업38";

                btnMoldW39.Enabled = true;
                btnMoldW39.BackColor = SystemColors.Control;
                btnMoldW39.Text = "작업39";

                btnMoldW40.Enabled = true;
                btnMoldW40.BackColor = SystemColors.Control;
                btnMoldW40.Text = "작업40";

                ////
                ///1. 각 버튼별 호기번호 세팅. (환경설정에 맞추어) > 재단/성형 같이 쓸수 있게끔.
                ////              

                string strProcess = cboProcess.SelectedValue.ToString();

                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Add(Work_sMachineByProcess.SPROCESSID, strProcess);  // 성형.

                DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_WizWork_sMachine_InProcess", sqlParameter, false);
                DataTable dt2 = dt.Clone();

                string[] sMachineID = null;
                sMachineID = Frm_tprc_Main.gs.GetValue("Work", "Machine", "Machine").Split('|');//배열에 설비아이디 넣기                               
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
                            // 길이가 4칸이다 > 즉 콕 집어놓은 한녀석에 대해서만 머신체크 한다.
                            if (strProcess.Length == 4)
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
                            // strProcess가 중복이라면 프로세스 스플릿.
                            else
                            {
                                string[] sMachine_Mac = null;
                                sMachine_Mac = strProcess.Split('|');
                                List<string> Machine_Mac = new List<string>();
                                foreach (string str in sMachine_Mac)
                                {
                                    Machine_Mac.Add(str);
                                }

                                // 스플릿 끊은애랑 Mac이랑 같고, 그게 성형이거나 재단일때. > 같을때 dr2 인서트.
                                foreach (string Checker in Machine_Mac)
                                {
                                    if (Mac.Substring(0, 4) == Checker)
                                    {
                                        if (Checker == dr["ProcessID"].ToString())
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
                            }                            
                        }
                    }
                    //if (!chkOK)
                    //{
                    //    sMachine.Remove("2101" + dr["MachineID"].ToString());
                    //}
                }
                if (dt2.Rows.Count > 0)
                {
                    List<string> MachineName = new List<string>();
                    foreach (DataRow dr in dt2.Rows)
                    {
                        MachineName.Add(dr["Machine"].ToString().Trim() + "\r\n" +
                            dr["Process"].ToString().Trim());
                    }
                    for (int w = 1; w <= MachineName.Count; w++)
                    {
                        if (w < 41)
                        {
                            if (w == 1) { btnMoldW1.Text = MachineName[0]; }
                            if (w == 2) { btnMoldW2.Text = MachineName[1]; }
                            if (w == 3) { btnMoldW3.Text = MachineName[2]; }
                            if (w == 4) { btnMoldW4.Text = MachineName[3]; }
                            if (w == 5) { btnMoldW5.Text = MachineName[4]; }
                            if (w == 6) { btnMoldW6.Text = MachineName[5]; }
                            if (w == 7) { btnMoldW7.Text = MachineName[6]; }
                            if (w == 8) { btnMoldW8.Text = MachineName[7]; }
                            if (w == 9) { btnMoldW9.Text = MachineName[8]; }
                            if (w == 10) { btnMoldW10.Text = MachineName[9]; }
                            //11 ~ 20
                            if (w == 11) { btnMoldW11.Text = MachineName[10]; }
                            if (w == 12) { btnMoldW12.Text = MachineName[11]; }
                            if (w == 13) { btnMoldW13.Text = MachineName[12]; }
                            if (w == 14) { btnMoldW14.Text = MachineName[13]; }
                            if (w == 15) { btnMoldW15.Text = MachineName[14]; }
                            if (w == 16) { btnMoldW16.Text = MachineName[15]; }
                            if (w == 17) { btnMoldW17.Text = MachineName[16]; }
                            if (w == 18) { btnMoldW18.Text = MachineName[17]; }
                            if (w == 19) { btnMoldW19.Text = MachineName[18]; }
                            if (w == 20) { btnMoldW20.Text = MachineName[19]; }
                            //21 ~ 30
                            if (w == 21) { btnMoldW21.Text = MachineName[20]; }
                            if (w == 22) { btnMoldW22.Text = MachineName[21]; }
                            if (w == 23) { btnMoldW23.Text = MachineName[22]; }
                            if (w == 24) { btnMoldW24.Text = MachineName[23]; }
                            if (w == 25) { btnMoldW25.Text = MachineName[24]; }
                            if (w == 26) { btnMoldW26.Text = MachineName[25]; }
                            if (w == 27) { btnMoldW27.Text = MachineName[26]; }
                            if (w == 28) { btnMoldW28.Text = MachineName[27]; }
                            if (w == 29) { btnMoldW29.Text = MachineName[28]; }
                            if (w == 30) { btnMoldW30.Text = MachineName[29]; }
                            //31 ~ 40
                            if (w == 31) { btnMoldW31.Text = MachineName[30]; }
                            if (w == 32) { btnMoldW32.Text = MachineName[31]; }
                            if (w == 33) { btnMoldW33.Text = MachineName[32]; }
                            if (w == 34) { btnMoldW34.Text = MachineName[33]; }
                            if (w == 35) { btnMoldW35.Text = MachineName[34]; }
                            if (w == 36) { btnMoldW36.Text = MachineName[35]; }
                            if (w == 37) { btnMoldW37.Text = MachineName[36]; }
                            if (w == 38) { btnMoldW38.Text = MachineName[37]; }
                            if (w == 39) { btnMoldW39.Text = MachineName[38]; }
                            if (w == 40) { btnMoldW40.Text = MachineName[39]; }
                        }
                    }
                }               
                dt = null;

                ////
                ///2. 호기번호 없는 아이들 > btn disable 처리.
                //// 
                if (btnMoldW1.Text == "작업1") { btnMoldW1.Enabled = false; }
                if (btnMoldW2.Text == "작업2") { btnMoldW2.Enabled = false; }
                if (btnMoldW3.Text == "작업3") { btnMoldW3.Enabled = false; }
                if (btnMoldW4.Text == "작업4") { btnMoldW4.Enabled = false; }
                if (btnMoldW5.Text == "작업5") { btnMoldW5.Enabled = false; }
                if (btnMoldW6.Text == "작업6") { btnMoldW6.Enabled = false; }
                if (btnMoldW7.Text == "작업7") { btnMoldW7.Enabled = false; }
                if (btnMoldW8.Text == "작업8") { btnMoldW8.Enabled = false; }
                if (btnMoldW9.Text == "작업9") { btnMoldW9.Enabled = false; }
                if (btnMoldW10.Text == "작업10") { btnMoldW10.Enabled = false; }
                //11 ~ 20
                if (btnMoldW11.Text == "작업11") { btnMoldW11.Enabled = false; }
                if (btnMoldW12.Text == "작업12") { btnMoldW12.Enabled = false; }
                if (btnMoldW13.Text == "작업13") { btnMoldW13.Enabled = false; }
                if (btnMoldW14.Text == "작업14") { btnMoldW14.Enabled = false; }
                if (btnMoldW15.Text == "작업15") { btnMoldW15.Enabled = false; }
                if (btnMoldW16.Text == "작업16") { btnMoldW16.Enabled = false; }
                if (btnMoldW17.Text == "작업17") { btnMoldW17.Enabled = false; }
                if (btnMoldW18.Text == "작업18") { btnMoldW18.Enabled = false; }
                if (btnMoldW19.Text == "작업19") { btnMoldW19.Enabled = false; }
                if (btnMoldW20.Text == "작업20") { btnMoldW20.Enabled = false; }
                //21 ~ 30
                if (btnMoldW21.Text == "작업21") { btnMoldW21.Enabled = false; }
                if (btnMoldW22.Text == "작업22") { btnMoldW22.Enabled = false; }
                if (btnMoldW23.Text == "작업23") { btnMoldW23.Enabled = false; }
                if (btnMoldW24.Text == "작업24") { btnMoldW24.Enabled = false; }
                if (btnMoldW25.Text == "작업25") { btnMoldW25.Enabled = false; }
                if (btnMoldW26.Text == "작업26") { btnMoldW26.Enabled = false; }
                if (btnMoldW27.Text == "작업27") { btnMoldW27.Enabled = false; }
                if (btnMoldW28.Text == "작업28") { btnMoldW28.Enabled = false; }
                if (btnMoldW29.Text == "작업29") { btnMoldW29.Enabled = false; }
                if (btnMoldW30.Text == "작업30") { btnMoldW30.Enabled = false; }
                //31 ~ 40
                if (btnMoldW31.Text == "작업31") { btnMoldW31.Enabled = false; }
                if (btnMoldW32.Text == "작업32") { btnMoldW32.Enabled = false; }
                if (btnMoldW33.Text == "작업33") { btnMoldW33.Enabled = false; }
                if (btnMoldW34.Text == "작업34") { btnMoldW34.Enabled = false; }
                if (btnMoldW35.Text == "작업35") { btnMoldW35.Enabled = false; }
                if (btnMoldW36.Text == "작업36") { btnMoldW36.Enabled = false; }
                if (btnMoldW37.Text == "작업37") { btnMoldW37.Enabled = false; }
                if (btnMoldW38.Text == "작업38") { btnMoldW38.Enabled = false; }
                if (btnMoldW39.Text == "작업39") { btnMoldW39.Enabled = false; }
                if (btnMoldW40.Text == "작업40") { btnMoldW40.Enabled = false; }

                DataStore.Instance.CloseConnection(); //2021-10-07 DB 커넥트 연결 해제
                ////
                ///3. 지금 working중인 > 작업 진행중인 아이와 연동.
                ////


                string Lotid = string.Empty;
                string PersonName = string.Empty;
                string Machine_Name = string.Empty;
                string Process_Name = string.Empty;


                DataTable dt3 = DataStore.Instance.ProcedureToDataTable("xp_WizWork_sNowWorking", null, false);

                foreach (DataRow dr in dt3.Rows)
                {

                    PersonName = dr["Name"].ToString().Trim();
                    Lotid = dr["LabelID"].ToString().Trim();
                    Machine_Name = dr["MachineNo"].ToString().Trim();
                    Process_Name = dr["Process"].ToString().Trim();

                    // 작업 진행중 이라고 판단한 이 아이의 현재 MACHINE_NO가
                    // 내가 btnMold_X_W.Text 에 담아놓은 머신명과 일치하는가? 
                    // 1 ~ 9까지 체크반복, 맞으면 글자 업데이트.


                    if (Machine_Name + "\r\n" + Process_Name == btnMoldW1.Text)
                    {
                        btnMoldW1.Text = Machine_Name + "\r\n" + Process_Name + "\r\n" + PersonName + "\r\n" + "\r\n" +
                            "작업중";      //Lotid + "\r\n" + "\r\n" +
                        btnMoldW1.Tag = Lotid;
                    }

                    if (Machine_Name + "\r\n" + Process_Name == btnMoldW2.Text)
                    {
                        btnMoldW2.Text = Machine_Name + "\r\n" + Process_Name + "\r\n" + PersonName + "\r\n" + "\r\n" +
                            "작업중";      //Lotid + "\r\n" + "\r\n" +
                        btnMoldW2.Tag = Lotid;
                    }

                    if (Machine_Name + "\r\n" + Process_Name == btnMoldW3.Text)
                    {
                        btnMoldW3.Text = Machine_Name + "\r\n" + Process_Name + "\r\n" + PersonName + "\r\n" + "\r\n" +
                            "작업중";      //Lotid + "\r\n" + "\r\n" +
                        btnMoldW3.Tag = Lotid;
                    }

                    if (Machine_Name + "\r\n" + Process_Name == btnMoldW4.Text)
                    {
                        btnMoldW4.Text = Machine_Name + "\r\n" + Process_Name + "\r\n" + PersonName + "\r\n" + "\r\n" +
                            "작업중";      //Lotid + "\r\n" + "\r\n" +
                        btnMoldW4.Tag = Lotid;
                    }

                    if (Machine_Name + "\r\n" + Process_Name == btnMoldW5.Text)
                    {
                        btnMoldW5.Text = Machine_Name + "\r\n" + Process_Name + "\r\n" + PersonName + "\r\n" + "\r\n" +
                            "작업중";      //Lotid + "\r\n" + "\r\n" +
                        btnMoldW5.Tag = Lotid;
                    }

                    if (Machine_Name + "\r\n" + Process_Name == btnMoldW6.Text)
                    {
                        btnMoldW6.Text = Machine_Name + "\r\n" + Process_Name + "\r\n" + PersonName + "\r\n" + "\r\n" +
                            "작업중";      //Lotid + "\r\n" + "\r\n" +
                        btnMoldW6.Tag = Lotid;
                    }

                    if (Machine_Name + "\r\n" + Process_Name == btnMoldW7.Text)
                    {
                        btnMoldW7.Text = Machine_Name + "\r\n" + Process_Name + "\r\n" + PersonName + "\r\n" + "\r\n" +
                            "작업중";      //Lotid + "\r\n" + "\r\n" +
                        btnMoldW7.Tag = Lotid;
                    }

                    if (Machine_Name + "\r\n" + Process_Name == btnMoldW8.Text)
                    {
                        btnMoldW8.Text = Machine_Name + "\r\n" + Process_Name + "\r\n" + PersonName + "\r\n" + "\r\n" +
                            "작업중";      //Lotid + "\r\n" + "\r\n" +
                        btnMoldW8.Tag = Lotid;
                    }

                    if (Machine_Name + "\r\n" + Process_Name == btnMoldW9.Text)
                    {
                        btnMoldW9.Text = Machine_Name + "\r\n" + Process_Name + "\r\n" + PersonName + "\r\n" + "\r\n" +
                            "작업중";      //Lotid + "\r\n" + "\r\n" +
                        btnMoldW9.Tag = Lotid;
                    }

                    if (Machine_Name + "\r\n" + Process_Name == btnMoldW10.Text)
                    {
                        btnMoldW10.Text = Machine_Name + "\r\n" + Process_Name + "\r\n" + PersonName + "\r\n" + "\r\n" +
                            "작업중";      //Lotid + "\r\n" + "\r\n" +
                        btnMoldW10.Tag = Lotid;
                    }

                    //11 ~ 20
                    if (Machine_Name + "\r\n" + Process_Name == btnMoldW11.Text)
                    {
                        btnMoldW11.Text = Machine_Name + "\r\n" + Process_Name + "\r\n" + PersonName + "\r\n" + "\r\n" +
                            "작업중";      //Lotid + "\r\n" + "\r\n" +
                        btnMoldW11.Tag = Lotid;
                    }

                    if (Machine_Name + "\r\n" + Process_Name == btnMoldW12.Text)
                    {
                        btnMoldW12.Text = Machine_Name + "\r\n" + Process_Name + "\r\n" + PersonName + "\r\n" + "\r\n" +
                            "작업중";      //Lotid + "\r\n" + "\r\n" +
                        btnMoldW12.Tag = Lotid;
                    }

                    if (Machine_Name + "\r\n" + Process_Name == btnMoldW13.Text)
                    {
                        btnMoldW13.Text = Machine_Name + "\r\n" + Process_Name + "\r\n" + PersonName + "\r\n" + "\r\n" +
                            "작업중";      //Lotid + "\r\n" + "\r\n" +
                        btnMoldW13.Tag = Lotid;
                    }

                    if (Machine_Name + "\r\n" + Process_Name == btnMoldW14.Text)
                    {
                        btnMoldW14.Text = Machine_Name + "\r\n" + Process_Name + "\r\n" + PersonName + "\r\n" + "\r\n" +
                            "작업중";      //Lotid + "\r\n" + "\r\n" +
                        btnMoldW14.Tag = Lotid;
                    }

                    if (Machine_Name + "\r\n" + Process_Name == btnMoldW15.Text)
                    {
                        btnMoldW15.Text = Machine_Name + "\r\n" + Process_Name + "\r\n" + PersonName + "\r\n" + "\r\n" +
                            "작업중";      //Lotid + "\r\n" + "\r\n" +
                        btnMoldW15.Tag = Lotid;
                    }

                    if (Machine_Name + "\r\n" + Process_Name == btnMoldW16.Text)
                    {
                        btnMoldW16.Text = Machine_Name + "\r\n" + Process_Name + "\r\n" + PersonName + "\r\n" + "\r\n" +
                            "작업중";      //Lotid + "\r\n" + "\r\n" +
                        btnMoldW16.Tag = Lotid;
                    }

                    if (Machine_Name + "\r\n" + Process_Name == btnMoldW17.Text)
                    {
                        btnMoldW17.Text = Machine_Name + "\r\n" + Process_Name + "\r\n" + PersonName + "\r\n" + "\r\n" +
                            "작업중";      //Lotid + "\r\n" + "\r\n" +
                        btnMoldW17.Tag = Lotid;
                    }

                    if (Machine_Name + "\r\n" + Process_Name == btnMoldW18.Text)
                    {
                        btnMoldW18.Text = Machine_Name + "\r\n" + Process_Name + "\r\n" + PersonName + "\r\n" + "\r\n" +
                            "작업중";      //Lotid + "\r\n" + "\r\n" +
                        btnMoldW18.Tag = Lotid;
                    }

                    if (Machine_Name + "\r\n" + Process_Name == btnMoldW19.Text)
                    {
                        btnMoldW19.Text = Machine_Name + "\r\n" + Process_Name + "\r\n" + PersonName + "\r\n" + "\r\n" +
                            "작업중";      //Lotid + "\r\n" + "\r\n" +
                        btnMoldW19.Tag = Lotid;
                    }

                    if (Machine_Name + "\r\n" + Process_Name == btnMoldW20.Text)
                    {
                        btnMoldW20.Text = Machine_Name + "\r\n" + Process_Name + "\r\n" + PersonName + "\r\n" + "\r\n" +
                            "작업중";      //Lotid + "\r\n" + "\r\n" +
                        btnMoldW20.Tag = Lotid;
                    }

                    //21 ~ 30
                    if (Machine_Name + "\r\n" + Process_Name == btnMoldW21.Text)
                    {
                        btnMoldW21.Text = Machine_Name + "\r\n" + Process_Name + "\r\n" + PersonName + "\r\n" + "\r\n" +
                            "작업중";      //Lotid + "\r\n" + "\r\n" +
                        btnMoldW21.Tag = Lotid;
                    }

                    if (Machine_Name + "\r\n" + Process_Name == btnMoldW22.Text)
                    {
                        btnMoldW22.Text = Machine_Name + "\r\n" + Process_Name + "\r\n" + PersonName + "\r\n" + "\r\n" +
                            "작업중";      //Lotid + "\r\n" + "\r\n" +
                        btnMoldW22.Tag = Lotid;
                    }

                    if (Machine_Name + "\r\n" + Process_Name == btnMoldW23.Text)
                    {
                        btnMoldW23.Text = Machine_Name + "\r\n" + Process_Name + "\r\n" + PersonName + "\r\n" + "\r\n" +
                            "작업중";      //Lotid + "\r\n" + "\r\n" +
                        btnMoldW23.Tag = Lotid;
                    }

                    if (Machine_Name + "\r\n" + Process_Name == btnMoldW24.Text)
                    {
                        btnMoldW24.Text = Machine_Name + "\r\n" + Process_Name + "\r\n" + PersonName + "\r\n" + "\r\n" +
                            "작업중";      //Lotid + "\r\n" + "\r\n" +
                        btnMoldW24.Tag = Lotid;
                    }

                    if (Machine_Name + "\r\n" + Process_Name == btnMoldW25.Text)
                    {
                        btnMoldW25.Text = Machine_Name + "\r\n" + Process_Name + "\r\n" + PersonName + "\r\n" + "\r\n" +
                            "작업중";      //Lotid + "\r\n" + "\r\n" +
                        btnMoldW25.Tag = Lotid;
                    }

                    if (Machine_Name + "\r\n" + Process_Name == btnMoldW26.Text)
                    {
                        btnMoldW26.Text = Machine_Name + "\r\n" + Process_Name + "\r\n" + PersonName + "\r\n" + "\r\n" +
                            "작업중";      //Lotid + "\r\n" + "\r\n" +
                        btnMoldW26.Tag = Lotid;
                    }

                    if (Machine_Name + "\r\n" + Process_Name == btnMoldW27.Text)
                    {
                        btnMoldW27.Text = Machine_Name + "\r\n" + Process_Name + "\r\n" + PersonName + "\r\n" + "\r\n" +
                            "작업중";      //Lotid + "\r\n" + "\r\n" +
                        btnMoldW27.Tag = Lotid;
                    }

                    if (Machine_Name + "\r\n" + Process_Name == btnMoldW28.Text)
                    {
                        btnMoldW28.Text = Machine_Name + "\r\n" + Process_Name + "\r\n" + PersonName + "\r\n" + "\r\n" +
                            "작업중";      //Lotid + "\r\n" + "\r\n" +
                        btnMoldW28.Tag = Lotid;
                    }

                    if (Machine_Name + "\r\n" + Process_Name == btnMoldW29.Text)
                    {
                        btnMoldW29.Text = Machine_Name + "\r\n" + Process_Name + "\r\n" + PersonName + "\r\n" + "\r\n" +
                            "작업중";      //Lotid + "\r\n" + "\r\n" +
                        btnMoldW29.Tag = Lotid;
                    }

                    if (Machine_Name + "\r\n" + Process_Name == btnMoldW30.Text)
                    {
                        btnMoldW30.Text = Machine_Name + "\r\n" + Process_Name + "\r\n" + PersonName + "\r\n" + "\r\n" +
                            "작업중";      //Lotid + "\r\n" + "\r\n" +
                        btnMoldW30.Tag = Lotid;
                    }

                    //31 ~ 40
                    if (Machine_Name + "\r\n" + Process_Name == btnMoldW31.Text)
                    {
                        btnMoldW31.Text = Machine_Name + "\r\n" + Process_Name + "\r\n" + PersonName + "\r\n" + "\r\n" +
                            "작업중";      //Lotid + "\r\n" + "\r\n" +
                        btnMoldW31.Tag = Lotid;
                    }

                    if (Machine_Name + "\r\n" + Process_Name == btnMoldW32.Text)
                    {
                        btnMoldW32.Text = Machine_Name + "\r\n" + Process_Name + "\r\n" + PersonName + "\r\n" + "\r\n" +
                            "작업중";      //Lotid + "\r\n" + "\r\n" +
                        btnMoldW32.Tag = Lotid;
                    }

                    if (Machine_Name + "\r\n" + Process_Name == btnMoldW33.Text)
                    {
                        btnMoldW33.Text = Machine_Name + "\r\n" + Process_Name + "\r\n" + PersonName + "\r\n" + "\r\n" +
                            "작업중";      //Lotid + "\r\n" + "\r\n" +
                        btnMoldW33.Tag = Lotid;
                    }

                    if (Machine_Name + "\r\n" + Process_Name == btnMoldW34.Text)
                    {
                        btnMoldW34.Text = Machine_Name + "\r\n" + Process_Name + "\r\n" + PersonName + "\r\n" + "\r\n" +
                            "작업중";      //Lotid + "\r\n" + "\r\n" +
                        btnMoldW34.Tag = Lotid;
                    }

                    if (Machine_Name + "\r\n" + Process_Name == btnMoldW35.Text)
                    {
                        btnMoldW35.Text = Machine_Name + "\r\n" + Process_Name + "\r\n" + PersonName + "\r\n" + "\r\n" +
                            "작업중";      //Lotid + "\r\n" + "\r\n" +
                        btnMoldW35.Tag = Lotid;
                    }

                    if (Machine_Name + "\r\n" + Process_Name == btnMoldW36.Text)
                    {
                        btnMoldW36.Text = Machine_Name + "\r\n" + Process_Name + "\r\n" + PersonName + "\r\n" + "\r\n" +
                            "작업중";      //Lotid + "\r\n" + "\r\n" +
                        btnMoldW36.Tag = Lotid;
                    }

                    if (Machine_Name + "\r\n" + Process_Name == btnMoldW37.Text)
                    {
                        btnMoldW37.Text = Machine_Name + "\r\n" + Process_Name + "\r\n" + PersonName + "\r\n" + "\r\n" +
                            "작업중";      //Lotid + "\r\n" + "\r\n" +
                        btnMoldW37.Tag = Lotid;
                    }

                    if (Machine_Name + "\r\n" + Process_Name == btnMoldW38.Text)
                    {
                        btnMoldW38.Text = Machine_Name + "\r\n" + Process_Name + "\r\n" + PersonName + "\r\n" + "\r\n" +
                            "작업중";      //Lotid + "\r\n" + "\r\n" +
                        btnMoldW38.Tag = Lotid;
                    }

                    if (Machine_Name + "\r\n" + Process_Name == btnMoldW39.Text)
                    {
                        btnMoldW39.Text = Machine_Name + "\r\n" + Process_Name + "\r\n" + PersonName + "\r\n" + "\r\n" +
                            "작업중";      //Lotid + "\r\n" + "\r\n" +
                        btnMoldW39.Tag = Lotid;
                    }

                    if (Machine_Name + "\r\n" + Process_Name == btnMoldW40.Text)
                    {
                        btnMoldW40.Text = Machine_Name + "\r\n" + Process_Name + "\r\n" + PersonName + "\r\n" + "\r\n" +
                            "작업중";      //Lotid + "\r\n" + "\r\n" +
                        btnMoldW40.Tag = Lotid;
                    }
                }
                ////
                ///4. 연동이 완료된 아이는 노란색 색칠.
                ///  그렇지 못한 아니는 btn disable 처리.
                ////
                if (btnMoldW1.Text.EndsWith("중") == false) { btnMoldW1.Enabled = false; }
                if (btnMoldW2.Text.EndsWith("중") == false) { btnMoldW2.Enabled = false; }
                if (btnMoldW3.Text.EndsWith("중") == false) { btnMoldW3.Enabled = false; }
                if (btnMoldW4.Text.EndsWith("중") == false) { btnMoldW4.Enabled = false; }
                if (btnMoldW5.Text.EndsWith("중") == false) { btnMoldW5.Enabled = false; }
                if (btnMoldW6.Text.EndsWith("중") == false) { btnMoldW6.Enabled = false; }
                if (btnMoldW7.Text.EndsWith("중") == false) { btnMoldW7.Enabled = false; }
                if (btnMoldW8.Text.EndsWith("중") == false) { btnMoldW8.Enabled = false; }
                if (btnMoldW9.Text.EndsWith("중") == false) { btnMoldW9.Enabled = false; }
                if (btnMoldW10.Text.EndsWith("중") == false) { btnMoldW10.Enabled = false; }
                //11 ~ 20
                if (btnMoldW11.Text.EndsWith("중") == false) { btnMoldW11.Enabled = false; }
                if (btnMoldW12.Text.EndsWith("중") == false) { btnMoldW12.Enabled = false; }
                if (btnMoldW13.Text.EndsWith("중") == false) { btnMoldW13.Enabled = false; }
                if (btnMoldW14.Text.EndsWith("중") == false) { btnMoldW14.Enabled = false; }
                if (btnMoldW15.Text.EndsWith("중") == false) { btnMoldW15.Enabled = false; }
                if (btnMoldW16.Text.EndsWith("중") == false) { btnMoldW16.Enabled = false; }
                if (btnMoldW17.Text.EndsWith("중") == false) { btnMoldW17.Enabled = false; }
                if (btnMoldW18.Text.EndsWith("중") == false) { btnMoldW18.Enabled = false; }
                if (btnMoldW19.Text.EndsWith("중") == false) { btnMoldW19.Enabled = false; }
                if (btnMoldW20.Text.EndsWith("중") == false) { btnMoldW20.Enabled = false; }
                //21 ~ 30
                if (btnMoldW21.Text.EndsWith("중") == false) { btnMoldW21.Enabled = false; }
                if (btnMoldW22.Text.EndsWith("중") == false) { btnMoldW22.Enabled = false; }
                if (btnMoldW23.Text.EndsWith("중") == false) { btnMoldW23.Enabled = false; }
                if (btnMoldW24.Text.EndsWith("중") == false) { btnMoldW24.Enabled = false; }
                if (btnMoldW25.Text.EndsWith("중") == false) { btnMoldW25.Enabled = false; }
                if (btnMoldW26.Text.EndsWith("중") == false) { btnMoldW26.Enabled = false; }
                if (btnMoldW27.Text.EndsWith("중") == false) { btnMoldW27.Enabled = false; }
                if (btnMoldW28.Text.EndsWith("중") == false) { btnMoldW28.Enabled = false; }
                if (btnMoldW29.Text.EndsWith("중") == false) { btnMoldW29.Enabled = false; }
                if (btnMoldW30.Text.EndsWith("중") == false) { btnMoldW30.Enabled = false; }
                //31 ~ 40
                if (btnMoldW31.Text.EndsWith("중") == false) { btnMoldW31.Enabled = false; }
                if (btnMoldW32.Text.EndsWith("중") == false) { btnMoldW32.Enabled = false; }
                if (btnMoldW33.Text.EndsWith("중") == false) { btnMoldW33.Enabled = false; }
                if (btnMoldW34.Text.EndsWith("중") == false) { btnMoldW34.Enabled = false; }
                if (btnMoldW35.Text.EndsWith("중") == false) { btnMoldW35.Enabled = false; }
                if (btnMoldW36.Text.EndsWith("중") == false) { btnMoldW36.Enabled = false; }
                if (btnMoldW37.Text.EndsWith("중") == false) { btnMoldW37.Enabled = false; }
                if (btnMoldW38.Text.EndsWith("중") == false) { btnMoldW38.Enabled = false; }
                if (btnMoldW39.Text.EndsWith("중") == false) { btnMoldW39.Enabled = false; }
                if (btnMoldW40.Text.EndsWith("중") == false) { btnMoldW40.Enabled = false; }


                if (btnMoldW1.Enabled == true) { btnMoldW1.BackColor = Color.Yellow; }
                if (btnMoldW2.Enabled == true) { btnMoldW2.BackColor = Color.Yellow; }
                if (btnMoldW3.Enabled == true) { btnMoldW3.BackColor = Color.Yellow; }
                if (btnMoldW4.Enabled == true) { btnMoldW4.BackColor = Color.Yellow; }
                if (btnMoldW5.Enabled == true) { btnMoldW5.BackColor = Color.Yellow; }
                if (btnMoldW6.Enabled == true) { btnMoldW6.BackColor = Color.Yellow; }
                if (btnMoldW7.Enabled == true) { btnMoldW7.BackColor = Color.Yellow; }
                if (btnMoldW8.Enabled == true) { btnMoldW8.BackColor = Color.Yellow; }
                if (btnMoldW9.Enabled == true) { btnMoldW9.BackColor = Color.Yellow; }
                if (btnMoldW10.Enabled == true) { btnMoldW10.BackColor = Color.Yellow; }
                //11 ~ 20
                if (btnMoldW11.Enabled == true) { btnMoldW11.BackColor = Color.Yellow; }
                if (btnMoldW12.Enabled == true) { btnMoldW12.BackColor = Color.Yellow; }
                if (btnMoldW13.Enabled == true) { btnMoldW13.BackColor = Color.Yellow; }
                if (btnMoldW14.Enabled == true) { btnMoldW14.BackColor = Color.Yellow; }
                if (btnMoldW15.Enabled == true) { btnMoldW15.BackColor = Color.Yellow; }
                if (btnMoldW16.Enabled == true) { btnMoldW16.BackColor = Color.Yellow; }
                if (btnMoldW17.Enabled == true) { btnMoldW17.BackColor = Color.Yellow; }
                if (btnMoldW18.Enabled == true) { btnMoldW18.BackColor = Color.Yellow; }
                if (btnMoldW19.Enabled == true) { btnMoldW19.BackColor = Color.Yellow; }
                if (btnMoldW20.Enabled == true) { btnMoldW20.BackColor = Color.Yellow; }
                //21 ~ 30
                if (btnMoldW21.Enabled == true) { btnMoldW21.BackColor = Color.Yellow; }
                if (btnMoldW22.Enabled == true) { btnMoldW22.BackColor = Color.Yellow; }
                if (btnMoldW23.Enabled == true) { btnMoldW23.BackColor = Color.Yellow; }
                if (btnMoldW24.Enabled == true) { btnMoldW24.BackColor = Color.Yellow; }
                if (btnMoldW25.Enabled == true) { btnMoldW25.BackColor = Color.Yellow; }
                if (btnMoldW26.Enabled == true) { btnMoldW26.BackColor = Color.Yellow; }
                if (btnMoldW27.Enabled == true) { btnMoldW27.BackColor = Color.Yellow; }
                if (btnMoldW28.Enabled == true) { btnMoldW28.BackColor = Color.Yellow; }
                if (btnMoldW29.Enabled == true) { btnMoldW29.BackColor = Color.Yellow; }
                if (btnMoldW30.Enabled == true) { btnMoldW30.BackColor = Color.Yellow; }
                //31 ~ 40
                if (btnMoldW31.Enabled == true) { btnMoldW31.BackColor = Color.Yellow; }
                if (btnMoldW32.Enabled == true) { btnMoldW32.BackColor = Color.Yellow; }
                if (btnMoldW33.Enabled == true) { btnMoldW33.BackColor = Color.Yellow; }
                if (btnMoldW34.Enabled == true) { btnMoldW34.BackColor = Color.Yellow; }
                if (btnMoldW35.Enabled == true) { btnMoldW35.BackColor = Color.Yellow; }
                if (btnMoldW36.Enabled == true) { btnMoldW36.BackColor = Color.Yellow; }
                if (btnMoldW37.Enabled == true) { btnMoldW37.BackColor = Color.Yellow; }
                if (btnMoldW38.Enabled == true) { btnMoldW38.BackColor = Color.Yellow; }
                if (btnMoldW39.Enabled == true) { btnMoldW39.BackColor = Color.Yellow; }
                if (btnMoldW40.Enabled == true) { btnMoldW40.BackColor = Color.Yellow; }

                DataStore.Instance.CloseConnection(); //2021-10-07 DB 커넥트 연결 해제
            }

            catch (Exception excpt)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message), "[오류]", 0, 1);
            }
        }


        // 노란색으로 색칠된 작업 진행중인 아이를 클릭했어요.
        // 중간과정을 모두 생략하고(처음 작업 시작할때 했으니까)
        // 바로 (work_U) 로 보내버려요.
        private void btnWorkingMold_Click(object sender, EventArgs e)
        {
            string ClickText = ((Button)sender).Tag.ToString();
            // 2020.02.24 둘리
            string MachineNo = ((Button)sender).Text.Split('\r')[0].ToString();

            Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
            sqlParameter.Add("LikeLot", ClickText);  // 유사 로트번호
            sqlParameter.Add("MachineNo", MachineNo);  // 2020.02.24 둘리
            DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_WizWork_sWorkingEndTarget", sqlParameter, false);

            if (dt.Rows.Count == 0)
            {
                WizCommon.Popup.MyMessageBox.ShowBox("작업 진행중인 공정 LotID가 아닙니다. {" + ClickText + "} 관리자에게 문의해 주세요!", "[Start 데이터 서치오류]", 2, 1);
                return;
            }

            string strInstID = dt.Rows[0]["InstID"].ToString();
            string prodlotid = dt.Rows[0]["LabelID"].ToString();
            string strMachineID = dt.Rows[0]["MachineID"].ToString();
            string strMachine = dt.Rows[0]["MachineNo"].ToString();
            string strprocessid = dt.Rows[0]["ProcessID"].ToString();
            string strProcess = dt.Rows[0]["Process"].ToString();
            string strInstDetSeq = dt.Rows[0]["InstDetSeq"].ToString();
            string strPersonID = dt.Rows[0]["WorkPersonID"].ToString();
            string strPerson = dt.Rows[0]["Name"].ToString();
            string strTeamID = dt.Rows[0]["TeamID"].ToString();
            string strTeam = dt.Rows[0]["Team"].ToString();
            string strCT = dt.Rows[0]["CT"].ToString(); // 2020.03.03 둘리 추가
            string strDayOrNightID = dt.Rows[0]["DayOrNightID"].ToString();

            // 미리 선스캔 했던 진짜 라벨번호.
            string StartSaveLabelID = dt.Rows[0]["StartSaveLabelID"].ToString();
            string WorkStartDate = dt.Rows[0]["WorkStartDate"].ToString();
            string WorkStartTime = dt.Rows[0]["WorkStartTime"].ToString();
            string JobID = dt.Rows[0]["JobID"].ToString();

            //2021-11-30 하위품 라벨 리스트 추가
            List<string> listChildLabelID = new List<string>();
            //2021-11-30 하위품 Article 리스트 추가
            List<string> listChildArticle = new List<string>();
            //2022-05-18 하위품 ArticleID 리스트 추가
            List<string> listChildArticleID = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                listChildLabelID.Add(dt.Rows[i]["ChildLabelID"].ToString());
                listChildArticle.Add(dt.Rows[i]["Article"].ToString());
                listChildArticleID.Add(dt.Rows[i]["ChildArticleID"].ToString().Trim());
            }

            // 불나방도 최소한의 대비는 하고 가야지. _ g_tBase 값 Update.
            Frm_tprc_Main.g_tBase.sInstID = strInstID;
            Frm_tprc_Main.g_tBase.sLotID = prodlotid;
            Frm_tprc_Main.g_tBase.MachineID = strMachineID;
            Frm_tprc_Main.g_tBase.Machine = strMachine;
            Frm_tprc_Main.g_tBase.ProcessID = strprocessid;
            Frm_tprc_Main.g_tBase.Process = strProcess;
            Frm_tprc_Main.g_tBase.sInstDetSeq = strInstDetSeq;
            Frm_tprc_Main.g_tBase.PersonID = strPersonID;
            Frm_tprc_Main.g_tBase.Person = strPerson;
            Frm_tprc_Main.g_tBase.TeamID = strTeamID;
            Frm_tprc_Main.g_tBase.Team = strTeam;
            Frm_tprc_Main.g_tBase.DayOrNightID = strDayOrNightID;

            Set_stbInfo();

            //DataStore.Instance.CloseConnection(); //2021-10-07 DB 커넥트 연결 해제

            // 2020.02.24 둘리 : 해당 공정 선택했을시 하단의 텍스트 수정 되도록!!!!! 일단 공정과 설비만

            Form form = null;
            frm_tprc_Work_U child8 = new frm_tprc_Work_U(JobID, strprocessid, StartSaveLabelID, WorkStartDate, WorkStartTime, strDayOrNightID, listChildLabelID, listChildArticle, listChildArticleID); //2021-11-30 하위품 라벨, 하위품 품번 리스트 추가
            child8.m_CycleTime = ConvertDouble(strCT);
            form = child8;

            if (form != null)
            {
                foreach (Form openForm in Application.OpenForms)//중복실행방지
                {
                    if (openForm.Name == form.Name)
                    {
                        openForm.BringToFront();
                        openForm.Activate();
                        return;
                    }
                }
                form.MdiParent = this.ParentForm;
                form.TopLevel = false;
                form.Dock = DockStyle.Fill;

                form.BringToFront();
                form.Show();                
            }
        }
        //2021-09-28 외주 생산 함수 생성
        private void MoveWorking(List<string> listChildLabelID, List<string> listChildArticle, List<string> listChildArticleID)
        {

            string ClickText = Frm_tprc_Main.g_tBase.sLotID.ToString();

            // 2020.02.24 둘리
            string MachineNo = Frm_tprc_Main.g_tBase.MachineID.ToString();

            Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
            sqlParameter.Add("LotID", ClickText);  // 유사 로트번호
            sqlParameter.Add("MachineID", MachineNo);  // 2020.02.24 둘리
            DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_prdWork_sForInsertWorkData", sqlParameter, false);
            //DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_WizWork_sWorkingEndTarget", sqlParameter, false);
            
            if (dt.Rows.Count == 0)
            {
                WizCommon.Popup.MyMessageBox.ShowBox("작업 진행중인 공정 LotID가 아닙니다. {" + ClickText + "} 관리자에게 문의해 주세요!", "[Start 데이터 서치오류]", 2, 1);
                return;
            }

            string strInstID = dt.Rows[0]["InstID"].ToString();
            string prodlotid = dt.Rows[0]["LabelID"].ToString();
            string strMachineID = dt.Rows[0]["MachineID"].ToString();
            string strMachine = dt.Rows[0]["MachineNo"].ToString();
            string strprocessid = dt.Rows[0]["ProcessID"].ToString();
            string strProcess = dt.Rows[0]["Process"].ToString();
            string strInstDetSeq = dt.Rows[0]["InstDetSeq"].ToString();
            //string strPersonID = dt.Rows[0]["WorkPersonID"].ToString();
            //string strPerson = dt.Rows[0]["Name"].ToString();
            //string strTeamID = dt.Rows[0]["TeamID"].ToString();
            //string strTeam = dt.Rows[0]["Team"].ToString();
            string strCT = dt.Rows[0]["CT"].ToString(); // 2020.03.03 둘리 추가
            //string strDayOrNightID = dt.Rows[0]["DayOrNightID"].ToString();

            // 미리 선스캔 했던 진짜 라벨번호.
            //string StartSaveLabelID = dt.Rows[0]["StartSaveLabelID"].ToString();
            //string WorkStartDate = dt.Rows[0]["WorkStartDate"].ToString();
            //string WorkStartTime = dt.Rows[0]["WorkStartTime"].ToString();
            //string JobID = dt.Rows[0]["JobID"].ToString();


            // 불나방도 최소한의 대비는 하고 가야지. _ g_tBase 값 Update.

            string strDayOrNightID = Frm_tprc_Main.g_tBase.DayOrNightID; //2021-10-20 외주이동생산 처리 시 임시 인서트를 하지 않아 주/야간 값을 업데이트 못해서 처음 선택한 값 가지고 처리

            Frm_tprc_Main.g_tBase.sInstID = strInstID;
            Frm_tprc_Main.g_tBase.sLotID = prodlotid;
            Frm_tprc_Main.g_tBase.MachineID = strMachineID;
            Frm_tprc_Main.g_tBase.Machine = strMachine;
            Frm_tprc_Main.g_tBase.ProcessID = strprocessid;
            Frm_tprc_Main.g_tBase.Process = strProcess;
            Frm_tprc_Main.g_tBase.sInstDetSeq = strInstDetSeq;           
            //Frm_tprc_Main.g_tBase.PersonID = strPersonID;
            //Frm_tprc_Main.g_tBase.Person = strPerson;
            //Frm_tprc_Main.g_tBase.TeamID = strTeamID;
            //Frm_tprc_Main.g_tBase.Team = strTeam;
            //Frm_tprc_Main.g_tBase.DayOrNightID = strDayOrNightID;

            Set_stbInfo();

            DataStore.Instance.CloseConnection(); //2021-10-07 DB 커넥트 연결 해제
            // 2020.02.24 둘리 : 해당 공정 선택했을시 하단의 텍스트 수정 되도록!!!!! 일단 공정과 설비만

            Form form = null;
            frm_tprc_Work_U_Move_Prd child8 = new frm_tprc_Work_U_Move_Prd(strprocessid, strDayOrNightID, listChildLabelID, listChildArticle, listChildArticleID);
            //frm_tprc_Work_U_Move_Prd child8 = new frm_tprc_Work_U_Move_Prd(JobID, strprocessid, StartSaveLabelID, WorkStartDate, WorkStartTime, strDayOrNightID);
            child8.m_CycleTime = ConvertDouble(strCT);
            form = child8;

            if (form != null)
            {
                foreach (Form openForm in Application.OpenForms)//중복실행방지
                {
                    if (openForm.Name == form.Name)
                    {
                        openForm.BringToFront();
                        openForm.Activate();
                        return;
                    }
                }
                form.MdiParent = this.ParentForm;
                form.TopLevel = false;
                form.Dock = DockStyle.Fill;

                form.BringToFront();
                form.Show();
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

        private void grdData_SelectionChanged(object sender, EventArgs e)
        {
            //if (grdData.SelectedRows.Count > 0 && grdData.SelectedRows.Count == 1)
            //{
            //    dgvr = grdData.SelectedRows[0];
            //    if (dgvr.Cells["ProcessID"].Value.ToString() == "2101" && dgvr.Cells["SHWorkingYN"].Value.ToString() == "Y")
            //    {
            //        lblSHWorkingYN.Visible = true;
            //    }
            //    else
            //    {
            //        lblSHWorkingYN.Visible = false;
            //    }
            //}
        }

        private void cmdStart_Click(object sender, EventArgs e)
        {
            if (grdData.SelectedRows.Count > 0 && grdData.SelectedRows.Count == 1)
            {
                dgvr = null;
                dgvr = grdData.SelectedRows[0];

                strLotID = "";
                strMachineID = "";
                strProcessID = "";
                strInstID = "";
                strInstDetSeq = "";

                strLotID = grdData.SelectedRows[0].Cells["LotID"].Value.ToString();
                strMachineID = grdData.SelectedRows[0].Cells["MachineID"].Value.ToString().Trim();
                strProcessID = grdData.SelectedRows[0].Cells["ProcessID"].Value.ToString().Trim();
                strInstID = grdData.SelectedRows[0].Cells["InstID"].Value.ToString();
                strInstDetSeq = grdData.SelectedRows[0].Cells["InstDetSeq"].Value.ToString();

                Frm_tprc_Main.g_tBase.sInstID = strInstID;
                Frm_tprc_Main.g_tBase.sLotID = strLotID;
                Frm_tprc_Main.g_tBase.MachineID = strMachineID;
                Frm_tprc_Main.g_tBase.ProcessID = strProcessID;
                Frm_tprc_Main.g_tBase.sInstDetSeq = strInstDetSeq;

                Frm_tprc_Main.list_tMold = new List<TMold>();

                SetsetProcessFormLoad();

                return;
            }
            else
            {
                WizCommon.Popup.MyMessageBox.ShowBox("선택된 작업지시가 없습니다.", "[오류]", 0, 1);
            }

        }


        public void SetPreScanPopUpLoad(string processid, string machindid, string moldid)
        {
            frm_PopUp_PreScanWork4 FPPSW = new frm_PopUp_PreScanWork4(processid, machindid, moldid);
            FPPSW.StartPosition = FormStartPosition.CenterScreen;
            FPPSW.BringToFront();
            FPPSW.TopMost = true;

            if (FPPSW.ShowDialog() == DialogResult.OK)
            {
                // ok라는건, 새로운 시작처리가 하나 있다는 것.
                // re_search.
                procQuery();
                WorkingMachine_btnSetting();
                LogData.LogSave(this.GetType().Name, "R"); //2022-06-22 조회
            }
        }




        // 툴 교환 등록 버튼 클릭 이벤트 → 툴 교환 팝업 띄우기
        private void btnToolChange_Click(object sender, EventArgs e)
        {
            frm_tprc_setProcess Set_ps = new frm_tprc_setProcess(true);//NoWork == true라는 bool값
            Frm_tprc_Main main = new Frm_tprc_Main();
            Set_ps.Owner = main;
            if (Set_ps.ShowDialog() == DialogResult.OK)
            {
                // Tool 교환 등록 띄우기
                SetToolPopUpLoad();
            };

            //frm_tprc_setProcess Set_ps = new frm_tprc_setProcess(strLotID, strMachineID, strProcessID, strInstID);
            //Set_ps.Owner = this;
            //if (Set_ps.ShowDialog() == DialogResult.OK)
            //{
            //    // Tool 교환 등록 띄우기
            //    SetToolPopUpLoad();
            //};
        }

        // 작업지도서 확인 버튼 클릭 이벤트 > 작업지도서 확인이 가능한 그림파일 띄어주기.
        private void btnWorkOrderJPG_Click(object sender, EventArgs e)
        {
            //if (grdData.SelectedRows.Count > 0 && grdData.SelectedRows.Count == 1)
            //{
            //    string File_1 = string.Empty;
            //    string Path_1 = string.Empty;
            //    string File_2 = string.Empty;
            //    string Path_2 = string.Empty;

            //    string ArticleID = grdData.SelectedRows[0].Cells["ArticleID"].Value.ToString();

            //    Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
            //    sqlParameter.Add("ArticleID", ArticleID);//cboProcess.Text 

            //    DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_WizWork_sWorkOrderRoute", sqlParameter, false);

            //    if (dt != null && dt.Rows.Count > 0)
            //    {
            //        File_1 = dt.Rows[0]["Sketch1File"].ToString();
            //        Path_1 = dt.Rows[0]["Sketch1Path"].ToString();
            //        File_2 = dt.Rows[0]["Sketch2File"].ToString();
            //        Path_2 = dt.Rows[0]["Sketch2Path"].ToString();
            //    }

            //    if (File_2.Trim().Equals(""))
            //    {
            //        WizCommon.Popup.MyMessageBox.ShowBox("[FTP] " + "해당 품명의 작업지도 이미지가 등록되어 있지 않습니다.", "[작업지도 이미지 없음]", 0, 1);
            //        return;
            //    }

            //    // 작업지도서 올리기. ( 스케치 경로 2번 )
            //    Frm_PopUp_ImgWorkOrder IWO = null;
            //    IWO = new Frm_PopUp_ImgWorkOrder(Path_2, File_2);
            //    IWO.StartPosition = FormStartPosition.CenterParent;
            //    IWO.Show();

            //}

            try
            {
                if (grdData.SelectedRows.Count > 0 && grdData.SelectedRows.Count == 1)
                {

                    string ArticleID = grdData.SelectedRows[0].Cells["ArticleID"].Value.ToString().Trim();

                    Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                    sqlParameter.Add("ArticleID", ArticleID);//cboProcess.Text 

                    DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_WizWork_sWorkOrderRoute", sqlParameter, false);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];

                        Frm_PopUp_ImgArticleInfo_CodeView ArticleImg = new Frm_PopUp_ImgArticleInfo_CodeView()
                        {
                            Sketch1File = dr["Sketch1File"].ToString(),
                            Sketch1Path = dr["Sketch1Path"].ToString(),
                            Sketch2File = dr["Sketch2File"].ToString(),
                            Sketch2Path = dr["Sketch2Path"].ToString(),
                            Sketch3File = dr["Sketch3File"].ToString(),
                            Sketch3Path = dr["Sketch3Path"].ToString(),
                            Sketch4File = dr["Sketch4File"].ToString(),
                            Sketch4Path = dr["Sketch4Path"].ToString(),
                            Sketch5File = dr["Sketch5File"].ToString(),
                            Sketch5Path = dr["Sketch5Path"].ToString(),
                            Sketch6File = dr["Sketch6File"].ToString(),
                            Sketch6Path = dr["Sketch6Path"].ToString(),
                        };
                        DataStore.Instance.CloseConnection(); //2021-10-07 DB 커넥트 연결 해제
                        // Frm_PopUp_ImgArticleInfo
                        Frm_PopUp_ImgArticleInfo IWO = new Frm_PopUp_ImgArticleInfo(ArticleImg);
                        IWO.StartPosition = FormStartPosition.CenterParent;
                        IWO.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox("관리자에게 문의해주세요.\r\n" + ex.Message, "[품명 이미지 조회 오류]", 2, 1);
                return;
            }
        }

        private void btnFunctionPrepare(object sender, EventArgs e)
        {
            WizCommon.Popup.MyMessageBox.ShowBox("기능이 준비중에 있습니다.", "[준비중]", 2, 1);
            return;
        }



        private void SetToolPopUpLoad()
        {
            frm_tprc_UseTool_U Tool = new frm_tprc_UseTool_U();
            Tool.StartPosition = FormStartPosition.CenterScreen;
            Tool.BringToFront();
            //Tool.TopMost = true;

            if (Tool.ShowDialog() == DialogResult.OK)
            {

            }
        }

        #region 기타 메서드 모음

        // 천마리 콤마, 소수점 버리기
        private string stringFormatN0(object obj)
        {
            return string.Format("{0:N0}", obj);
        }

        // 천마리 콤마, 소수점 두자리
        private string stringFormatN2(object obj)
        {
            return string.Format("{0:N2}", obj);
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

        #region 공정작업에서 자주검사 이동 모음 - btnInspectAuto_Click(), CheckIsInspectAuto()

        // 한민테크 7월 요청사항 → 자주검사를 작업지시서 라벨 스캔없이 할 수 있도록
        // → 공정작업에서 해당 작업지시를 선택하여 자주검사를 시행하면?
        private void btnInspectAuto_Click(object sender, EventArgs e)
        {
            try
            {
                string LotID = grdData.SelectedRows[0].Cells["LOTID"].Value.ToString();

                if (CheckIsInspectAuto(LotID))
                {
                    Frm_tprc_Main.OpenInspectAuto(LotID);
                }

            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }

        // 자주검사 등록이 되어 있는지 체크
        private bool CheckIsInspectAuto(string LotID)
        {
            bool flag = false;

            try
            {
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Add("LotID", LotID);

                DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_prdWork_sCheckInspectAuto", sqlParameter, false);

                if (dt != null
                    && dt.Rows.Count > 0
                    && dt.Columns.Count == 1)
                {
                    if (dt.Rows[0]["Msg"].ToString().Equals("PASS"))
                    {
                        flag = true;
                    }
                    else
                    {
                        WizCommon.Popup.MyMessageBox.ShowBox(dt.Rows[0]["Msg"].ToString().Replace("|", "\r\n"), "[자주검사 이동 오류 - btnInspectAuto_Click]", 0, 1);
                    }
                }
                DataStore.Instance.CloseConnection(); //2021-10-07 DB 커넥트 연결 해제
            }
            catch (Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", ex.Message), "[오류]", 0, 1);
            }

            return flag;
        }

        #endregion

        #region 검색조건 품번 콤보박스 함수

        private void SetBuyerArticleComboBox() //2022-08-25 콤보박스 설정
        {
            string strErr = "";

            int intnChkProc = 1;
            string strProcessID = "";

            int intnchkInstDate = 0;
            string strStartDate = "";
            string strEndDate = "";

            DataSet ds = null;

            strProcessID = Frm_tprc_Main.gs.GetValue("Work", "ProcessID", "ProcessID");
            string[] gubunProcess = strProcessID.Split(new char[] { '|' });

            //공정 가져오기
            try
            {

                if (chkInsDate.Checked)
                {
                    intnchkInstDate = 1;
                    strStartDate = mtb_From.Text.Replace("-", "");
                    strEndDate = mtb_To.Text.Replace("-", "");
                    if ((int.Parse(strStartDate) - int.Parse(strEndDate)) > 0)
                    {
                        string Message = "[지시일] 시작일이 종료일보다 늦을 수 없습니다.";
                        WizCommon.Popup.MyMessageBox.ShowBox(Message, "[검색조건]", 0, 1);
                        return;
                    }
                }

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

                // 공정 값체크
                if (chkProcess.Checked == false || (chkProcess.Checked == true && cboProcess.SelectedIndex == 0)) //공정체크 : N
                {
                    intnChkProc = 0;
                    strProcessID = "";
                }
                else if (chkProcess.Checked == true && cboProcess.SelectedIndex > 0) // 공정체크 : O
                {
                    intnChkProc = 1;
                    try
                    {
                        strProcessID = cboProcess.SelectedValue.ToString();
                    }
                    catch (Exception e1)
                    {
                        strErr = e1.Message.ToString();

                        strProcessID = "";
                    }
                }

                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();

                sqlParameter.Add("nchkInstDate", intnchkInstDate);
                sqlParameter.Add("FromDate", strStartDate);
                sqlParameter.Add("ToDate", strEndDate);
                sqlParameter.Add(Work_sProcess.NCHKPROC, intnChkProc);//cboProcess.Text 
                sqlParameter.Add(Work_sProcess.PROCESSID, strProcessID);//cboProcess.Text

                ds = DataStore.Instance.ProcedureToDataSet("[xp_Work_sBuyerArticle]", sqlParameter, false);

                DataRow newRow = ds.Tables[0].NewRow();
                newRow["ArticleID"] = "*";
                newRow["BuyerArticleNo"] = "전체";

                //DataRow newRow2 = ds.Tables[0].NewRow();
                //newRow2[Work_sProcess.PROCESSID] = strProcessID;
                //newRow2[Work_sProcess.PROCESS] = "부분전체";

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    ds.Tables[0].Rows.InsertAt(newRow, 0);
                    //ds.Tables[0].Rows.InsertAt(newRow2, 1);
                    cboBuyerArticle.DataSource = ds.Tables[0];
                }

                cboBuyerArticle.ValueMember = "ArticleID";
                cboBuyerArticle.DisplayMember = "BuyerArticleNo";

                DataStore.Instance.CloseConnection(); //2021-10-07 DB 커넥트 연결 해제
            }
            catch (Exception excpt)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message), "[오류]", 0, 1);
            }
            return;



        }

        private void cboBuyerArticle_DropDown(object sender, EventArgs e) //2022-08-25 콤보박스를 볼때마다 일자, 공정조건에 따라 다르게 나타나야 되어 볼때마다 다시 값 설정
        {
            //SetBuyerArticleComboBox();
        }

        private void cboBuyerArticle_SelectedIndexChanged(object sender, EventArgs e) //2022-08-25 전체를 추가하여 무조건 체크처리함
        {
            //chkBuyerArticle.Checked = true;
        }
        #endregion

        // 작업지시 여러개 선택시 원자재가 같은 것들만 선택되게 2023-03-21, 공정만 같으면 되게 수정 2023-04-06
        private bool Checkmaterials(List<string> LotID)
        {
            bool flag = false;
            int ChildArticleNum = 0;
            int ChildProcessNum = 0;
            List<string> ChildArticleID = new List<string>();
            List<string> ChildProcessID = new List<string>();
            //List<string> ChildArticleID2 = new List<string>();
            //List<string> ChildProcessID2 = new List<string>();

            try
            {
                if (LotID.Count > 1)
                {
                    for (int i = 0; i < LotID.Count; i++)
                    {
                        Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                        sqlParameter.Add("LotID", LotID[i]);

                        DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_prdWork_sCheckmaterials", sqlParameter, false);

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            for (int x = 0; x < dt.Rows.Count; x++)
                            {
                                ChildArticleID.Add(dt.Rows[x]["ChildArticleID"].ToString());
                                ChildArticleNum = Convert.ToInt32(dt.Rows[x]["Childnum"].ToString());
                                ChildProcessID.Add(dt.Rows[x]["ProcessID"].ToString());
                                ChildProcessNum = Convert.ToInt32(dt.Rows[x]["ChildProcessNum"].ToString());
                            }
                        }
                        else
                        {
                            WizCommon.Popup.MyMessageBox.ShowBox("등록된 하위품이 없습니다. \r\n 작업지시를 확인해주세요.", "[진행 전 확인]", 0, 1);
                            return false;
                        }

                        DataStore.Instance.CloseConnection(); //2021-10-07 DB 커넥트 연결 해제
                    }

                    if (ChildProcessID.Distinct().ToList().Count > 1)                               //(ChildArticleID2.Distinct().ToList().Count != ChildArticleID.Distinct().ToList().Count || ChildProcessID2.Distinct().ToList().Count != ChildProcessID.Distinct().ToList().Count)
                    {
                        //WizCommon.Popup.MyMessageBox.ShowBox("서로 다른 하위품으로 생산되는 작업지시를 선택하였습니다. \r\n 같은 원자재로 생산되는 작업지시를 선택해주세요.", "[진행 전 확인]", 0, 1);
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                    sqlParameter.Add("LotID", LotID[0]);

                    DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_prdWork_sCheckmaterials", sqlParameter, false);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        for (int x = 0; x < dt.Rows.Count; x++)
                        {
                            ChildArticleID.Add(dt.Rows[x]["ChildArticleID"].ToString());
                            ChildArticleNum = Convert.ToInt32(dt.Rows[x]["Childnum"].ToString());
                            ChildProcessID.Add(dt.Rows[x]["ProcessID"].ToString());
                            ChildProcessNum = Convert.ToInt32(dt.Rows[x]["ChildProcessNum"].ToString());
                        }
                    }
                    else
                    {
                        WizCommon.Popup.MyMessageBox.ShowBox("등록된 하위품이 없습니다. \r\n 작업지시를 확인해주세요.", "[진행 전 확인]", 0, 1);
                        return false;
                    }

                    DataStore.Instance.CloseConnection(); //2021-10-07 DB 커넥트 연결 해제
                

                    //if (ChildArticleNum != ChildArticleID.Distinct().ToList().Count || ChildProcessNum != ChildProcessID.Distinct().ToList().Count)
                    //{
                    //    //WizCommon.Popup.MyMessageBox.ShowBox("서로 다른 하위품으로 생산되는 작업지시를 선택하였습니다. \r\n 같은 원자재로 생산되는 작업지시를 선택해주세요.", "[진행 전 확인]", 0, 1);
                    //    return false;
                    //}
                    //else
                    //{
                        return true;
                    //}
                }
            }
            catch (Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", ex.Message), "[오류]", 0, 1);
            }

            return flag;
        }

        private void btnKPI_Click(object sender, EventArgs e)
        {
            frm_tprc_KPI Set_KPI = new frm_tprc_KPI();
            Set_KPI.Owner = this;
            if (Set_KPI.ShowDialog() == DialogResult.OK)
            {

            };
        }

        private void txtPLotID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                procQuery();
                LogData.LogSave(this.GetType().Name, "R"); //2022-06-22 조회
            }
        }
    }
}
