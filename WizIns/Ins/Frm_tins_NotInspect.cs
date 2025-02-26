using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WizCommon;
using WizWork;
using WizWork.Properties;
using System.Diagnostics;
using System.IO;

namespace WizIns
{
    public partial class Frm_tins_NotInspect : Form
    {
        INI_GS gs = Frm_tprc_Main.gs;
        WizWorkLib Lib = new WizWorkLib();
        Frm_tins_Main Ftm = new Frm_tins_Main(); //2022-06-23

        // 메인 데이터 그리드 뷰 관리 객체
        List<Frm_tins_Order_Q_CodeView> lstMain = new List<Frm_tins_Order_Q_CodeView>();

        // 검사할 리스트
        List<Frm_tins_Order_Q_CodeView> lstIns = new List<Frm_tins_Order_Q_CodeView>();

        private int SumWorkQty2 = 0;
        private int SumNoInsQty2 = 0;

        /// <summary>
        /// 생성
        /// </summary>
        public Frm_tins_NotInspect()
        {
            InitializeComponent();
        }

        #region Dock = Fill : SetScreen()
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
        #endregion

        private void Frm_tins_NotInspect_Load(object sender, EventArgs e)
        {
            SetScreen();
            
            // 오늘 날짜 세팅
            mtb_From.Text = DateTime.Today.AddDays(1 - DateTime.Now.Day).ToString("yyyyMMdd"); //mtb_From.Text = DateTime.Today.ToString("yyyyMMdd"); 2021-05-19 매월1일이 나오도록 수정
            mtb_To.Text = DateTime.Today.ToString("yyyyMMdd");
            //txtpersonview.Text = Frm_tins_Main.g_tBase.Name;
            //FillGrid(); 2021-07-06 바로 조회되면 속도가 느려 진입한 뒤 조회 버튼 누르면 조회 되도록 변경
            Ftm.LogSave(this.GetType().Name, "S"); //2022-06-23 사용시간(로드, 닫기

        }



        private void Frm_tins_NotInspect_Activated(object sender, EventArgs e)
        {
            txtpersonview.Text = Frm_tins_Main.g_tBase.Name;
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

        #region Header - 검색조건 : 품번 검색

        private void chkBuyerArticleNo_Click(object sender, EventArgs e)
        {
            if (chkArticle.Checked)
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

                        txtArticle.Focus();

                    }
                }
                catch (Exception ex) 
                {
                    //txtBuyerArticleNo.Text = "";
                    useMasicKeyboard(txtArticle);
                }
                //txtBuyerArticleNo.Text = "";
                //WizWork.POPUP.Frm_CMKeypad keypad = new WizWork.POPUP.Frm_CMKeypad("품번입력", "품번");

                //keypad.Owner = this;
                //if (keypad.ShowDialog() == DialogResult.OK)
                //{
                //    txtBuyerArticleNo.Text = keypad.tbInputText.Text;
                //    FillGrid();
                //}
            }
            else
            {
                try
                {                   
                    //2021-07-20
                    var path64 = System.IO.Path.Combine(Directory.GetDirectories(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "winsxs"), "amd64_microsoft-windows-osk_*")[0], "osk.exe");
                    var path32 = @"C:\windows\system32\osk.exe";
                    var path = (Environment.Is64BitOperatingSystem) ? path64 : path32;
                    if (File.Exists(path) && !Frm_tprc_Main.Lib.ReturnKillRunningProcess("osk"))
                    {
                        //System.Diagnostics.Process.Start(path);

                        txtArticle.Focus();

                    }

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


                    useMasicKeyboard(txtArticle);
                }
                //txtBuyerArticleNo.Text = "";
                //useMasicKeyboard(txtBuyerArticleNo);
            }
        }

        private void txtBuyerArticleNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnSearch.Enabled = false;
                chkArticle.Checked = true;
                FillGrid();
                btnSearch.Enabled = true;
                Ftm.LogSave(this.GetType().Name, "R"); //2022-06-23 조회
            }
        }

        #endregion

        // 조회 버튼 클릭 이벤트
        private void btnSearch_Click(object sender, EventArgs e)
        {
            //2021-09-10
            btnSearch.Enabled = false;
            Lib.Delay(3000); //2021-11-10 버튼을 여러번 클릭해도 한번만 클릭되게 딜레이 추가
            FillGrid();
            btnSearch.Enabled = true;
            Ftm.LogSave(this.GetType().Name, "R"); //2022-06-23 조회
        }

        // 닫기 버튼 클릭 이벤트
        private void btnClose_Click(object sender, EventArgs e)
        {
            Ftm.LogSave(this.GetType().Name, "S"); //2022-06-23 사용시간(로드, 닫기)
            Close();
        }

        #region Right - 위 아래 버튼 클릭 이벤트 모음
        private void btnUp_Click(object sender, EventArgs e)
        {
            DataGridSelRow_UpDown(-1);
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            DataGridSelRow_UpDown(1);
        }
        private void DataGridSelRow_UpDown(int upDown)
        {
            if (dgdMain.Rows.Count > 0
                && dgdMain.SelectedRows[0] != null)
            {
                int moveIndex = dgdMain.SelectedRows[0].Index + upDown;
                int maxIndex = dgdMain.Rows.Count;

                if (moveIndex >= 0
                    && moveIndex < maxIndex)
                {
                    dgdMain[0, moveIndex].Selected = true;
                }
            }
        }
        #endregion

        private void dgdMain_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //2025-02-25 하나만 선택 되도록 추가
            for (int x = 0; x < dgdMain.RowCount; x++)
            {
                if (x != e.RowIndex)
                {
                    dgdMain.Rows[x].Cells["Check"].Value = false;
                }
            }

            int y = 0;
            SumWorkQty2 = 0;
            SumNoInsQty2 = 0;
            if (e.RowIndex > -1)
            {
                if (dgdMain.Rows[e.RowIndex].Cells["Check"].Value.ToString().ToUpper() == "FALSE")
                {
                    dgdMain.Rows[e.RowIndex].Cells["Check"].Value = true;
                }
                else if (dgdMain.Rows[e.RowIndex].Cells["Check"].Value.ToString().ToUpper() == "TRUE")
                {
                    dgdMain.Rows[e.RowIndex].Cells["Check"].Value = false;
                }

                //dgdMain.Refresh();
            }

            for (int i = 0; i < dgdMain.RowCount; i++)
            {
                if (dgdMain.Rows[i].Cells["Check"].Value.ToString().ToUpper() == "TRUE")
                {
                    SumWorkQty2 += (int)Lib.ConvertDouble(dgdMain.Rows[i].Cells["WorkQty"].Value.ToString());
                    SumNoInsQty2 += (int)Lib.ConvertDouble(dgdMain.Rows[i].Cells["NoInspectQty"].Value.ToString());
                    y++;
                }
            }
            setSumDgv(y, SumWorkQty2, SumNoInsQty2);
        }

        #region 검사 조회 : FillGrid()

        private void FillGrid()
        {
            try
            {
                dgdMain.AutoGenerateColumns = false; //2021-07-21
                txtpersonview2.Text = Frm_tins_Main.g_tBase.Name; //2021-06-23
                // 총 작업수량
                int SumWorkQty = 0;
                int SumNoInsQty = 0;

                lstMain.Clear();
                
                Dictionary<string, object> sqlParameters = new Dictionary<string, object>();
                sqlParameters.Add("ChkWorkDate", chkDate.Checked == true ? 1 : 0);
                sqlParameters.Add("SDate", mtb_From.Text.Replace("-", ""));
                sqlParameters.Add("EDate", mtb_To.Text.Replace("-", ""));
                sqlParameters.Add("ChkArticle", chkArticle.Checked == true ? 1 : 0);
                sqlParameters.Add("Article", txtArticle.Text.Trim());
                DataTable dt = DataStore.Instance.ProcedureToDataTable("[xp_prdIns_sNoInspect]", sqlParameters, false); //xp_prdIns_sNoInspect_20210614

                if (dt != null
                    && dt.Rows.Count > 0)
                {
                    int i = 0;
                    foreach(DataRow dr in dt.Rows)
                    {
                        i++;
                        SumWorkQty += (int)Lib.ConvertDouble(dr["WorkQty"].ToString());
                        SumNoInsQty += (int)Lib.ConvertDouble(dr["NoInspectQty"].ToString());

                        var Scan = new Frm_tins_Order_Q_CodeView()
                        {
                            Num = i,
                            OrderID = dr["OrderID"].ToString(),
                            LabelID = dr["LabelID"].ToString().Trim(),
                            LabelGubun = dr["LabelGubun"].ToString(),
                            ProcessID = dr["ProcessID"].ToString(),
                            ArticleID = dr["ArticleID"].ToString().Trim(),
                            CustomID = dr["CustomID"].ToString(),
                            KCustom = dr["KCustom"].ToString(),
                            Article = dr["Article"].ToString(),
                            BuyerArticleNo = dr["BuyerArticleNo"].ToString(),
                            Model = dr["Model"].ToString(),
                            OrderQty = Lib.stringFormatN0(dr["OrderQty"]),
                            WorkQty = Lib.stringFormatN0(dr["WorkQty"]),
                            NoInspectQty = Lib.stringFormatN0(dr["NoInspectQty"]),
                            WorkDate = Lib.DatePickerFormat(dr["WorkEndDate"].ToString()),
                            //JobID = dr["JobID"].ToString()
                        };

                        lstMain.Add(Scan);
                    }

                    setSumDgv(i, SumWorkQty, SumNoInsQty);                   
                    var bs = new BindingSource();   //2021-07-21 BindingSource -> var 로 변경 
                    bs.DataSource = lstMain;
                    dgdMain.DataSource = bs;
                }
                else if (dt.Rows.Count == 0)
                {
                    WizCommon.Popup.MyMessageBox.ShowBox("조회 정보를 찾을 수 없습니다.", "[조회]", 0, 1);
                    //txtBuyerArticleNo.Text = "";
                    txtArticle.Focus();
                    setSumDgv(0, SumWorkQty, SumNoInsQty);                   
                    var bs = new BindingSource();           //2021-07-21 BindingSource -> var 로 변경
                    bs.DataSource = lstMain;
                    dgdMain.DataSource = bs;
                    //dgdMain.ClearSelection();


                    return;
                }


                dgdMain.ClearSelection();
                //dgdMain[0, 0].Selected = true;

                
            }
            catch (Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", ex.Message), "[오류]", 0, 1);

            }
            finally
            {
                DataStore.Instance.CloseConnection(); //2021-10-07 DB 커넥트 연결 해제
            }
        }

        private void setSumDgv(int count, int SumWorkQty, int SumNoInsQty)
        {
            dgdSum.Rows.Clear();

            dgdSum.Rows.Add("작업량 / 미검사량 합계"
                                        , Lib.stringFormatN0(count) + " 건"
                                        , Lib.stringFormatN0(SumWorkQty)
                                        , Lib.stringFormatN0(SumNoInsQty)
                                        );

            dgdSum.CurrentCell.Selected = false;
        }

        #endregion

        #region 검사 등록 전 체크 + 검사할 리스트 세팅 To lstIns : CheckBeforeStart()
        private bool CheckBeforeStart()
        {
            lstIns = new List<Frm_tins_Order_Q_CodeView>();

            // 품명이 같은 것만 검사 및 포장 가능  2020.11.23. 거래처가 다를 경우 검사 막기 추가 KGH
            string ArticleID = "";
            string KCustom = "";           
            for (int i = 0; i < dgdMain.Rows.Count; i++)
            {
                var Main = dgdMain.Rows[i].DataBoundItem as Frm_tins_Order_Q_CodeView;
                if (Main != null
                    && Main.Check == true)
                {
                    if (ArticleID.Equals("") && KCustom.Equals(""))
                    {
                        ArticleID = Main.ArticleID;
                        KCustom = Main.KCustom;
                    }
                    else
                    {
                        // 거래처가 다를 경우 막기  2020.11.23. 추가 KGH
                        if (!KCustom.Equals(Main.KCustom))
                        {
                            //WizCommon.Popup.MyMessageBox.ShowBox("거래처를 확인해주세요.", "저장 전", 0, 1);
                            //2021-06-14 검사문구 수정
                            WizCommon.Popup.MyMessageBox.ShowBox("2가지 이상의 다른 거래처가 선택되었습니다. 검사는 한번에 하나의 거래처만 검사가 가능합니다.", "저장 전", 0, 1); 
                            return false;
                        }
                        // 품명이 다를 경우 막기
                        else if (!ArticleID.Equals(Main.ArticleID))
                        {
                            WizCommon.Popup.MyMessageBox.ShowBox("품명이 같은것만 선택해주세요.", "[검사 등록 전]", 0, 1);
                            return false;
                        }
                    }
                    lstIns.Add(Main);
                }
            }

            if (lstIns.Count <= 0)
            {
                WizCommon.Popup.MyMessageBox.ShowBox("검사 등록할 라벨을 선택(체크)해주세요.", "[검사 등록 전]", 0, 1);
                return false;
            }
            //2021-05-10 작업자를 선택 안 했을 경우 메세지 표시 
            if(Frm_tins_Main.g_tBase.PersonID == null || Frm_tins_Main.g_tBase.PersonID == "")
            {
                WizCommon.Popup.MyMessageBox.ShowBox("작업자를 선택해주세요.", "[검사 등록 전]", 0, 1);
                return false;
            }
            return true;
        }
        #endregion


        #region 최종검사를 하지 않으면 저장되지 않게

        private bool CheckInspect()
        {
            for (int i = 0; i < dgdMain.Rows.Count; i++)
            {
                var Main = dgdMain.Rows[i].DataBoundItem as Frm_tins_Order_Q_CodeView;
                if (Main != null
                    && Main.Check == true)
                {

                    Dictionary<string, object> sqlParameters = new Dictionary<string, object>();
                    sqlParameters.Add("LabelID", Main.LabelID);
                    DataTable dt = DataStore.Instance.ProcedureToDataTable("[xp_prdIns_sNoInspectAuto]", sqlParameters, false);

                    if(dt != null
                    && dt.Rows.Count > 0)
                    {

                    }
                    else
                    {
                        WizCommon.Popup.MyMessageBox.ShowBox("최종검사이력이 없습니다.", "[검사 등록 전]", 0, 1);
                        return false;
                    }
                }
            }

            return true;
        }

        #endregion

        // 검사 등록 버튼 클릭 이벤트
        private void btnStart_Click(object sender, EventArgs e)
        {
            //if (CheckInspect() == true)
            //{
                if (CheckBeforeStart() == false) { return; }

                #region 포장 라벨이 새롭게 나오는 경우
                Frm_PopUp_Packing pack = new Frm_PopUp_Packing(lstIns);
                pack.ShowDialog();

                if (pack.DialogResult == DialogResult.OK)
                {
                    btnSearch.Enabled = false;
                    FillGrid();
                    btnSearch.Enabled = true;
                    Ftm.LogSave(this.GetType().Name, "R"); //2022-06-23 조회
                }
                #endregion

                #region 생산라벨 그대로 포장 하는 경우 (서강정밀)
                //Frm_PopUp_PackingAndOutBox pack = new Frm_PopUp_PackingAndOutBox(lstIns);
                //pack.ShowDialog();

                //if (pack.DialogResult == DialogResult.OK)
                //{
                //    FillGrid();
                //}
                #endregion
            //}
        }

        // 전체 선택 체크 이벤트
        private void btnAllCheck_Click(object sender, EventArgs e)
        {
            int y = 0;
            SumWorkQty2 = 0;
            SumNoInsQty2 = 0;

            Button btnSender = sender as Button;
            if (btnSender.Text.Equals("전체해제"))
            {
                for (int i = 0; i < lstMain.Count; i++)
                {
                    lstMain[i].Check = false;
                }
                btnSender.Text = "전체선택";
            }
            else
            {
                for (int i = 0; i < lstMain.Count; i++)
                {
                    lstMain[i].Check = true;
                }
                btnSender.Text = "전체해제";
            }

            var bs = new BindingSource();
            bs.DataSource = lstMain;
            dgdMain.DataSource = bs;

            for (int i = 0; i < dgdMain.RowCount; i++)
            {
                if (dgdMain.Rows[i].Cells["Check"].Value.ToString().ToUpper() == "TRUE")
                {
                    SumWorkQty2 += (int)Lib.ConvertDouble(dgdMain.Rows[i].Cells["WorkQty"].Value.ToString());
                    SumNoInsQty2 += (int)Lib.ConvertDouble(dgdMain.Rows[i].Cells["NoInspectQty"].Value.ToString());
                    y++;
                }
            }
            setSumDgv(y, SumWorkQty2, SumNoInsQty2);


        }

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

        private void dgdMain_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
            //if (e.Exception != null && e.Context == DataGridViewDataErrorContexts.Commit) 
            //{ 
            //    MessageBox.Show("닫기를 누른 뒤 재조회를 해주세요"); 
            //}

        }
    }
}
