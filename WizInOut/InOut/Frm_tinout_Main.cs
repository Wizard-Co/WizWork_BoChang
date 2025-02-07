using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using WizCommon;

namespace WizInOut
{
    public partial class Frm_tinout_Main : Form
    {
        int i = 0;//상단버튼 switch문용 정수
        Form form = null; //화면 열때 사용하는 form 변수
        Button btn = null;//상단 조작부 버튼 클릭 시 사용할 변수
        bool blOpen = false; //form이 이미 열려있는지 확인하는 변수
        string[] Message = new string[2]; //메세지창
        public static INI_GS gs = new INI_GS(); //ini 정보 사용
        public static TBaseSpec g_tBase = new TBaseSpec(); //기본 정보
        public static WizWorkLib Lib = new WizWorkLib(); //공통 lib
        LogData LogData = new LogData(); //2022-06-21 log 남기는 함수

        public Frm_tinout_Main()
        {
            InitializeComponent();
        }

        private void Frm_tinout_Main_Load(object sender, EventArgs e)
        {
            MyIP();      //현재 컴퓨터 IP
            LogData.LogSave(this.GetType().Name, ""); //log 남기기(종료 빈값) 2022-06-21 만약에 EndDate, EndTime에 빈 값이 존재하면 현재 날짜로 update(정전, 윈도우 강제종료시 로그가 formclosing 안 되는거 같아 여기서 처리함)
            SetScreen(); //헤더 버튼 설정
            LoadRegistry();

            btnControl_Click(btnInfo, null);
        }

        private void Frm_tinout_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveRegistry();

            if (!AccessibilityObject.Name.Contains("종료"))
            {
                LogData.LogSave(this.GetType().Name, ""); //log 남기기(종료 빈값) 2022-06-21
            }

            Message[0] = "[WizInOut - 입고/출고 관리 프로그램 종료]";
            Message[1] = "입고/출고 관리 프로그램을 종료합니다. \r\n계속하시겠습니까?";

            if (WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 0) == DialogResult.OK)//NO
            {
                LogData.LogSave(this.GetType().Name, ""); //log 남기기(종료 빈값) 2022-06-21
                Dispose();
                Close();
            }
            else
            {
                e.Cancel = true;
                return;
            }
        }

        private void Frm_tinout_Main_Activated(object sender, EventArgs e)
        {
            LoadRegistry();
        }

        //상단 버튼 클릭 이벤트
        private void btnControl_Click(object sender, EventArgs e)
        {
            try
            {
                i = 0;//case문에 사용할 정수 초기화
                form = null;//폼 초기화
                blOpen = false;//폼 활성화 여부
                btn = sender as Button;

                int.TryParse(btn.Tag.ToString(), out i);
                switch (i)
                {
                    case 0://공지사항
                        btnimage(0);
                        Frm_tinout_Info child0 = new Frm_tinout_Info();
                        form = child0;
                        break;
                    case 1://원자재 입고
                        btnimage(1);
                        Frm_tinout_OCStuffin_U child1 = new Frm_tinout_OCStuffin_U();
                        form = child1;
                        break;

                    case 2://제품 출고
                        btnimage(2);
                        Frm_tinout_OutWareScan_U child2 = new Frm_tinout_OutWareScan_U();
                        form = child2;
                        break;

                    case 3://원자재 입고 조회
                        btnimage(3);
                        Frm_tinout_OCStuffin_Q child3 = new Frm_tinout_OCStuffin_Q();
                        form = child3;
                        break;

                    case 4://제품 출고 조회
                        btnimage(4);
                        Frm_tinout_OutWareScan_Q child4 = new Frm_tinout_OutWareScan_Q();
                        form = child4;
                        break;
                    case 6://종료
                        btnimage(9);
                        Close();
                        SaveRegistry();
                        break;
                }

                if (form != null)
                {
                    foreach (Form openForm in Application.OpenForms)//중복실행방지
                    {
                        if (openForm.Name == form.Name)
                        {
                            blOpen = true;
                            openForm.BringToFront();
                            openForm.Activate();
                            return;
                        }
                    }
                    form.MdiParent = this;
                    form.TopLevel = false;
                    form.Dock = DockStyle.Fill;

                    if (!blOpen)
                    {
                        form.BringToFront();
                        form.Show();
                    }
                }
            }
            catch (Exception excpt)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message), "[오류]", 0, 1);
            }

        }

        private void MyIP()
        {
            var host = Dns.GetHostEntry(System.Environment.MachineName);

            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    g_tBase.MyIP = ip.ToString();
                }
            }

        }

        //헤더 버튼 설정
        private void SetScreen()
        {
            //버튼.Text 
            btnInfo.Text = "공지\r\n사항";
            btnStuffin.Text = "원자재\r\n입고";
            btnOutware.Text = "제품\r\n출고";
            btnStuffinSearch.Text = "원자재입고\r\n조회";
            btnOutwareSearch.Text = "제품출고\r\n조회";

            btnChoiceWorker.Text = "작업자\r\n선택";
            btnExit.Text = "작업\r\n종료";

            //버튼.Tag = 폼명
            btnInfo.Tag = "0";
            btnStuffin.Tag = "1";
            btnOutware.Tag = "2";
            btnStuffinSearch.Tag = "3";
            btnOutwareSearch.Tag = "4";

            btnChoiceWorker.Tag = "5";
            btnExit.Tag = "6";

            tlptop.Dock = DockStyle.Top;
            foreach (Control control in tlptop.Controls)
            {
                control.Dock = DockStyle.Fill;
                control.Margin = new Padding(0, 0, 0, 0);
                foreach (Control contro in control.Controls)//tlp 상위에서 3번째
                {
                    contro.Dock = DockStyle.Fill;
                    contro.Margin = new Padding(0, 0, 0, 0);
                    foreach (Control contr in contro.Controls)
                    {
                        contr.Dock = DockStyle.Fill;
                        contr.Margin = new Padding(0, 0, 0, 0);
                    }
                }
            }
        }

        public void LoadRegistry()
        {
            try
            {
                g_tBase.PersonID = gs.GetValue("Work", "SetPersonID", "");      //' 작업자 코드
                g_tBase.Person = gs.GetValue("Work", "SetPerson", "");          //' 작업자명
            }
            catch (Exception excpt)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message), "[오류]", 0, 1);
            }
        }

        private void SaveRegistry()
        {
            //레지스트리 대신에 ini에 값들을 저장하고 불러온다.
            gs.SetValue("Work", "SetPersonID", g_tBase.PersonID, ConnectionInfo.filePath);      //' 작업자 코드
            gs.SetValue("Work", "SetPerson", g_tBase.Person, ConnectionInfo.filePath);          //' 작업자명
        }

        private void btnimage(int casenum)
        {
            if (casenum == 0)
            {
                btnInfo.BackgroundImage = Properties.Resources.correct_mark__1_;
                btnStuffin.BackgroundImage = null;
                btnOutware.BackgroundImage = null;
                btnStuffinSearch.BackgroundImage = null;
                btnOutwareSearch.BackgroundImage = null;
                btnExit.BackgroundImage = null;
            }
            else if (casenum == 1)
            {
                btnStuffin.BackgroundImage = Properties.Resources.correct_mark__1_;
                btnInfo.BackgroundImage = null;
                btnOutware.BackgroundImage = null;
                btnStuffinSearch.BackgroundImage = null;
                btnOutwareSearch.BackgroundImage = null;
                btnExit.BackgroundImage = null;
            }
            else if (casenum == 2)
            {
                btnOutware.BackgroundImage = Properties.Resources.correct_mark__1_;
                btnInfo.BackgroundImage = null;
                btnStuffin.BackgroundImage = null;
                btnStuffinSearch.BackgroundImage = null;
                btnOutwareSearch.BackgroundImage = null;
                btnExit.BackgroundImage = null;
            }
            else if (casenum == 3)
            {
                btnStuffinSearch.BackgroundImage = Properties.Resources.correct_mark__1_;
                btnInfo.BackgroundImage = null;
                btnStuffin.BackgroundImage = null;
                btnOutware.BackgroundImage = null;
                btnOutwareSearch.BackgroundImage = null;
                btnExit.BackgroundImage = null;
            }
            else if (casenum == 4)
            {
                btnOutwareSearch.BackgroundImage = Properties.Resources.correct_mark__1_;
                btnInfo.BackgroundImage = null;
                btnStuffin.BackgroundImage = null;
                btnStuffinSearch.BackgroundImage = null;
                btnOutware.BackgroundImage = null;
                btnExit.BackgroundImage = null;
            }
            else if (casenum == 9)
            {
                btnExit.BackgroundImage = Properties.Resources.correct_mark__1_;
                btnInfo.BackgroundImage = null;
                btnStuffin.BackgroundImage = null;
                btnStuffinSearch.BackgroundImage = null;
                btnOutware.BackgroundImage = null;
                btnOutwareSearch.BackgroundImage = null;
            }
        }

    }
}
