using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;
using WizCommon;
using WizWork;
using WizWork.Properties;
using System.Diagnostics;
using System.IO;
//using Excel = Microsoft.Office.Interop.Excel;



namespace WizIns
{
    public partial class Frm_tins_Result_Q : Form
    {
        INI_GS gs = Frm_tprc_Main.gs;
        WizWorkLib Lib = new WizWorkLib();
        Frm_tins_Main Ftm = new Frm_tins_Main(); //2022-06-23
        // 메인 데이터 그리드 뷰 관리 객체
        List<Frm_tins_Result_Q_CodeView> lstMain = new List<Frm_tins_Result_Q_CodeView>();

        // 서브 데이터 그리드 뷰 관리 객체
        List<Frm_tins_InsDefectList> lstSub = new List<Frm_tins_InsDefectList>();

        // 삭제 및 라벨 재발행건들을 위한 객체
        List<Frm_tins_Result_Q_CodeView> lstIns = new List<Frm_tins_Result_Q_CodeView>();

        // 삭제는 PackID로 일괄 삭제 -> 합격수량이 0 일때, 포장 데이터가 생성되지 않아서, 해당 건은 삭제가 불가능하게 됨. 2020-11-10 GDU
        List<string> lstPackID = new List<string>();
        List<string> lstSeq = new List<string>();

        // 삭제할 ID와 아닌 ID를 각자 저장 하기 위해 생성 2021-07-20
        List<string> dlstPackID = new List<string>();
        List<string> ndlstPackID = new List<string>();

        // lstPackID 위의 문제로, InspectID 만 삭제하는 용도 리스트 생성. 2020-11-10 GDU
        List<string> lstInspectID = new List<string>();

        public WizWork.TTag Sub_m_tTag = new WizWork.TTag(); //2021-05-18
        public List<WizWork.TTagSub> list_m_tItem = new List<WizWork.TTagSub>(); //2021-05-18
        private string m_ProcessID = ""; //2021-05-18

        List<WizWork.Sub_TWkLabelPrint> list_TWkLabelPrint = null;
        string[] Message = new string[2];

        string OrderArticleID = string.Empty; //수주 제품이 슬라이드인지 리니어모터인지 구분하기 위해 2023-01-20

        string ArticleTagID = string.Empty;   //리니어모터(1), 슬라이드(2) 라벨 양식이 다르게 처리 하기 위해 TagID 구분자

        //Excel.Application excelApp = new Excel.Application();
        //Excel.Workbook workbook;
        //Excel.Worksheet worksheet;
        //Excel.Range range;

        /// <summary>
        /// 생성
        /// </summary>
        public Frm_tins_Result_Q()
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

        private void Frm_tins_Result_Q_Load(object sender, EventArgs e)
        {
            Ftm.LogSave(this.GetType().Name, "S"); //2022-06-23 사용시간(로드, 닫기)
            SetScreen();

            // 데이터 그리드 초기 설정
            //initDgv();

            // 오늘 날짜 세팅
            mtb_From.Text = DateTime.Today.ToString("yyyyMMdd"); //DateTime.Today.AddDays(1 - DateTime.Now.Day).ToString("yyyyMMdd");   
            mtb_To.Text = DateTime.Today.ToString("yyyyMMdd");

            InitGrid();

            FillGrid();
            Ftm.LogSave(this.GetType().Name, "R"); //2022-06-23 조회
        }

        private void Frm_tins_Result_Q_Activated(object sender, EventArgs e)
        {
            
        }

        private void InitGrid()
        {
            dgdMain.Columns.Clear();
            dgdMain.ColumnCount = 19;
            int i = 0;
            //2021-06-22 첫번째 Check가 있을 경우 조회할때 오류 발생하는 것으로 보여 처음엔 No로 시작하게 수정
            dgdMain.Columns[i].Name = "Num";
            dgdMain.Columns[i].HeaderText = "No";
            dgdMain.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgdMain.Columns[i].ReadOnly = true;
            dgdMain.Columns[i++].Visible = true;

            dgdMain.Columns[i].Name = "PackID";
            dgdMain.Columns[i].HeaderText = "포장번호";
            dgdMain.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgdMain.Columns[i].ReadOnly = true;
            dgdMain.Columns[i++].Visible = false;

            dgdMain.Columns[i].Name = "ExamDate";
            dgdMain.Columns[i].HeaderText = "검사일자";
            dgdMain.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgdMain.Columns[i].ReadOnly = true;
            dgdMain.Columns[i++].Visible = true;

            dgdMain.Columns[i].Name = "ExamTime";
            dgdMain.Columns[i].HeaderText = "검사시간";
            dgdMain.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgdMain.Columns[i].ReadOnly = true;
            dgdMain.Columns[i++].Visible = true;

            dgdMain.Columns[i].Name = "CustomID";
            dgdMain.Columns[i].HeaderText = "거래처";
            dgdMain.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgdMain.Columns[i].ReadOnly = true;
            dgdMain.Columns[i++].Visible = false;

            dgdMain.Columns[i].Name = "KCustom";
            dgdMain.Columns[i].HeaderText = "거래처";
            dgdMain.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgdMain.Columns[i].ReadOnly = true;
            dgdMain.Columns[i++].Visible = true;

            dgdMain.Columns[i].Name = "ArticleID";
            dgdMain.Columns[i].HeaderText = "품명코드";
            dgdMain.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dgdMain.Columns[i].ReadOnly = true;
            dgdMain.Columns[i++].Visible = false;

            dgdMain.Columns[i].Name = "BuyerArticleNo";
            dgdMain.Columns[i].HeaderText = "품  번";
            dgdMain.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgdMain.Columns[i].ReadOnly = true;
            dgdMain.Columns[i++].Visible = false;

            dgdMain.Columns[i].Name = "Article";
            dgdMain.Columns[i].HeaderText = "품  명";
            dgdMain.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgdMain.Columns[i].ReadOnly = true;
            dgdMain.Columns[i++].Visible = true;

            dgdMain.Columns[i].Name = "RealQty";
            dgdMain.Columns[i].HeaderText = "검사수량";
            dgdMain.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dgdMain.Columns[i].ReadOnly = true;
            dgdMain.Columns[i++].Visible = true;

            dgdMain.Columns[i].Name = "CtrlQty";
            dgdMain.Columns[i].HeaderText = "합격수량";
            dgdMain.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dgdMain.Columns[i].ReadOnly = true;
            dgdMain.Columns[i++].Visible = true;

            dgdMain.Columns[i].Name = "DefectQty";
            dgdMain.Columns[i].HeaderText = "불량수량";
            dgdMain.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dgdMain.Columns[i].ReadOnly = true;
            dgdMain.Columns[i++].Visible = true;

            dgdMain.Columns[i].Name = "PersonID";
            dgdMain.Columns[i].HeaderText = "작업자번호";
            dgdMain.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dgdMain.Columns[i].ReadOnly = true;
            dgdMain.Columns[i++].Visible = false;

            dgdMain.Columns[i].Name = "Name";
            dgdMain.Columns[i].HeaderText = "작업자";
            dgdMain.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgdMain.Columns[i].ReadOnly = true;
            dgdMain.Columns[i++].Visible = true;

            dgdMain.Columns[i].Name = "BoxID";
            dgdMain.Columns[i].HeaderText = "공정이동전표";
            dgdMain.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dgdMain.Columns[i].ReadOnly = true;
            dgdMain.Columns[i++].Visible = true;

            dgdMain.Columns[i].Name = "PackBoxID";
            dgdMain.Columns[i].HeaderText = "포장라벨전표";
            dgdMain.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dgdMain.Columns[i].ReadOnly = true;
            dgdMain.Columns[i++].Visible = true;

            dgdMain.Columns[i].Name = "OutDate";
            dgdMain.Columns[i].HeaderText = "출고일";
            dgdMain.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dgdMain.Columns[i].ReadOnly = true;
            dgdMain.Columns[i++].Visible = false;

            dgdMain.Columns[i].Name = "OrderID";
            dgdMain.Columns[i].HeaderText = "수주번호";
            dgdMain.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dgdMain.Columns[i].ReadOnly = true;
            dgdMain.Columns[i++].Visible = false;

            dgdMain.Columns[i].Name = "RollSeq";
            dgdMain.Columns[i].HeaderText = "순서";
            dgdMain.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dgdMain.Columns[i].ReadOnly = true;
            dgdMain.Columns[i++].Visible = false;

            
            DataGridViewCheckBoxColumn chkCol = new DataGridViewCheckBoxColumn();
            {
                chkCol.HeaderText = "C";
                chkCol.Name = "Check";
                chkCol.Width = 110;
                //chkCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                chkCol.FlatStyle = FlatStyle.Standard;
                chkCol.ThreeState = true;
                chkCol.CellTemplate = new DataGridViewCheckBoxCell();
                chkCol.CellTemplate.Style.BackColor = Color.Beige;
                chkCol.Visible = true;
            }
            dgdMain.Columns.Insert(1, chkCol); //2021-06-22 check는 따로 인서트해서 순서를 여기 수정

            dgdMain.Font = new Font("맑은 고딕", 12, FontStyle.Bold);
            dgdMain.RowTemplate.Height = 37;
            dgdMain.ColumnHeadersHeight = 35;
            dgdMain.ScrollBars = ScrollBars.Both;
            dgdMain.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgdMain.ReadOnly = true;
            dgdMain.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(234, 234, 234);
            dgdMain.ScrollBars = ScrollBars.Both;
            dgdMain.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgdMain.MultiSelect = false;
            dgdMain.AutoGenerateColumns = false; //2021-07-21

            foreach (DataGridViewColumn col in dgdMain.Columns)
            {
                col.DataPropertyName = col.Name;
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            }
            return;
        }


        #region 데이터 그리드 초기 설정 : wrap 설정
        private void initDgv()
        {
            // Header wrap 속성 false
            dgdMain.Columns["ExamDate"].HeaderCell.Style.WrapMode = DataGridViewTriState.False;
            dgdMain.Columns["ExamTime"].HeaderCell.Style.WrapMode = DataGridViewTriState.False;
            dgdMain.Columns["BuyerArticleNo"].HeaderCell.Style.WrapMode = DataGridViewTriState.False;
            dgdMain.Columns["Article"].HeaderCell.Style.WrapMode = DataGridViewTriState.False;
            dgdMain.Columns["Worker"].HeaderCell.Style.WrapMode = DataGridViewTriState.False;
            dgdMain.Columns["BoxID"].HeaderCell.Style.WrapMode = DataGridViewTriState.False;
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
                //useMasicKeyboard(txtBuyerArticleNo);

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
                        //System.Diagnostics.Process.Start(path); 2021-09-09 체크표시를 풀면 키보드가 나오지 않게 주석

                        txtArticle.Focus();

                    }
                }
                catch (Exception ex)
                {
                    //txtBuyerArticleNo.Text = "";
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
                chkArticle.Checked = true;
                FillGrid();
                Ftm.LogSave(this.GetType().Name, "R"); //2022-06-23 조회
            }
        }

        #endregion

        // 조회 버튼 클릭 이벤트
        private void btnSearch_Click(object sender, EventArgs e)
        {
            Ftm.LogSave(this.GetType().Name, "R"); //2022-06-23 조회
            btnSearch.Enabled = false;
            Lib.Delay(3000); //2021-11-10 버튼을 여러번 클릭해도 한번만 클릭되게 딜레이 추가
            FillGrid();
            btnSearch.Enabled = true;
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

        #region Right - 삭제 버튼, 구문 모음

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //if (CheckBeforeDelete() == false) { return; } 2021-07-15 프로시저로 확인 하도록 변경
 
            if (CheckBeforeDeletePro(lstPackID) == false) { return; } //2021-07-15 삭제 되는 것과 안 되는 것을 같이 선택했을 경우를 생각해야 됨

            if (dlstPackID.Count > 0)
            {

                if (DeleteData(dlstPackID))
                {
                    Ftm.LogSave(this.GetType().Name, "D"); //2022-06-23 삭제
                    lstMain.Clear();
                    dlstPackID.Clear();
                    dgdMain.Rows.Clear();
                    dgdSum.Rows.Clear();
                    FillGrid();
                    Ftm.LogSave(this.GetType().Name, "R"); //2022-06-23 조회
                }
            }

            //if (DeleteData(lstPackID))
            //{
            //    lstMain.Clear();
            //    dgdMain.Rows.Clear();
            //    dgdSum.Rows.Clear();
            //    FillGrid();
            //}
        }

        #region 삭제 구문 : DeleteData() ← 수정 필요 
        private bool DeleteData(List<string> lstPackID)
        {
            bool flag = false;
            try
            {
                int deleteCount = 0;

                #region Inspect 의 저장 된 데이터 삭제 구문 사용 할 필요 없음 (프로시져를 다른거 타고 삭제함)
                //for (int i = 0; i < lstInspectID.Count; i++)
                //{
                //    string orderID = lstInspectID[i].Split('|')[0];
                //    int rollSeq = Lib.ConvertInt(lstInspectID[i].Split('|')[1]);

                //    Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                //    sqlParameter.Add("OrderID", orderID);
                //    sqlParameter.Add("RollSeq", rollSeq);

                //    string[] sConfirm = new string[2];
                //    sConfirm = DataStore.Instance.ExecuteProcedure("[xp_prdIns_dInspect]", sqlParameter, true); //삭제
                //    if (sConfirm[0].ToUpper() == "SUCCESS")
                //    { deleteCount++; }
                //}
                #endregion

                for (int j = 0; j < lstPackID.Count; j++)
                {
                    
                    Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                    sqlParameter.Add("PackID", lstPackID[j]);
                    string[] sConfirm = new string[2];
                    sConfirm = DataStore.Instance.ExecuteProcedure("[xp_prdIns_dInspectAndPacking]", sqlParameter, true); //삭제
                    if (sConfirm[0].ToUpper() == "SUCCESS")
                    { deleteCount++; }

                }

                if (deleteCount > 0)
                {
                    WizCommon.Popup.MyMessageBox.ShowBox("총 " + deleteCount + "건이 삭제 완료 되었습니다.", "[삭제 완료]", 0, 1);
                    DataStore.Instance.CloseConnection(); //2021-10-07 DB 커넥트 연결 해제
                    return true;
                }
                
            }
            catch(Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", ex.Message), "[오류]", 0, 1);
                return false;
            }

            return flag;
        }
        #endregion

        // 삭제할 리스트 확인 및 세팅
        private bool CheckBeforeDelete()
        {
            bool flag = false;

            if (dgdMain.Rows.Count <= 0)
            {
                return false;
            }

            lstPackID.Clear();
            lstSeq.Clear();
            lstInspectID.Clear();


            int OutCnt = 0;
            for (int i = 0;  i < dgdMain.Rows.Count; i++)
            {
                var Main = dgdMain.Rows[i].DataBoundItem as Frm_tins_Result_Q_CodeView;
                if (Main != null && Main.Check == true)
                {
                    if (Main.OutDate.Trim().Equals("") == false)
                    {
                        OutCnt++;
                    }
                    else if (Main.PackID.Trim().Equals("") == false)
                    {
                        if (lstPackID.Contains(Main.PackID) == false)
                        {
                            lstPackID.Add(Main.PackID);
                        }


                    }
                    else if (string.IsNullOrEmpty(Main.PackID.Trim()) // 2020.11.10 GDU, 합격 수량이 0 일 경우, 검사 테이블만 삭제할 수 있도록
                        && string.IsNullOrEmpty(Main.OrderID) == false)
                    {
                        lstInspectID.Add(Main.OrderID + "|" + Main.PackID);
                    }
                }
            }

            if (lstPackID.Count + lstInspectID.Count <= 0)
            {
                // 출고되었을 경우 팝업창 출력 2020.11.10. KGH
                if (dgdMain.SelectedRows[0].Cells["OutDate"].Value.ToString() != null)
                {
                    for (int i = 0; i < dgdMain.Rows.Count; i++)
                    {
                        if (dgdMain.Rows[i].Cells["Check"].Value.ToString().ToUpper() == "TRUE")
                        {
                            //MessageBox.Show("text");
                            WizCommon.Popup.MyMessageBox.ShowBox("선택한 데이터는 모두 출고된 건으로\n 삭제가 불가능합니다.", "[삭제 전]", 0, 1);
                            return true;
                        }
                    }
                }
                // 선택된 라벨이 없을 경우 해당 팝업창 출력
                WizCommon.Popup.MyMessageBox.ShowBox("삭제할 라벨을 선택(체크)해주세요.", "[삭제 전]", 0, 1);
                return false;
            }

            if (OutCnt > 0)
            {
                //if (WizCommon.Popup.MyMessageBox.ShowBox("포장되어 출고된 건 " + OutCnt + "건 을 제외한 " + lstIns.Count + " 건을 모두 삭제하시겠습니까?", "[삭제 전]", 0, 1) == DialogResult.OK)
                if (WizCommon.Popup.MyMessageBox.ShowBox("총 " + lstIns.Count + "건이 삭제 완료 되었습니다.", "[삭제 완료]", 0, 1) == DialogResult.OK)
                {
                    flag = true;
                }
            }
            else
            {
                flag = true;
            }

            return flag;
        }

        #endregion

        private bool CheckBeforeDeletePro(List<string> lstPackID)
        {
            bool flag = false;

            if (dgdMain.Rows.Count <= 0)
            {
                return false;
            }

            lstPackID.Clear();
            lstSeq.Clear();
            lstInspectID.Clear();

            for (int i = 0; i < dgdMain.Rows.Count; i++)
            {
                var Main = dgdMain.Rows[i].DataBoundItem as Frm_tins_Result_Q_CodeView;
                if (Main != null && Main.Check == true)
                {
                    if (Main.PackID.Trim().Equals("") == false)
                    {
                        if (lstPackID.Contains(Main.PackID) == false)
                        {
                            lstPackID.Add(Main.PackID);
                        }

                    }

                }
            }


            int deleteCount = 0;
            int notdeleteCount = 0;
            for (int j = 0; j < lstPackID.Count; j++)
            {
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Add("PackID", lstPackID[j]);
                DataTable dt = DataStore.Instance.ProcedureToDataTable("[xp_prdIns_dcInspectAndPacking]", sqlParameter, true); //삭제

                if (dt != null
                   && dt.Rows.Count > 0)
                {                  
                    foreach (DataRow dr in dt.Rows)
                    {

                        string outclss = dr["outclss"].ToString().Trim();
                        string packdate = dr["packdate"].ToString().Trim();
                        string today = DateTime.Today.ToString("yyyyMMdd");

                  
                        //if (outclss == "*" || packdate != today)
                        //{
                        //    notdeleteCount++;
                        //    ndlstPackID.Add(lstPackID[j]);
                        //}
                        //else
                        //{
                            deleteCount++;
                            dlstPackID.Add(lstPackID[j]);
                        //}
                    }
                }
                else
                {
                    flag = true;
                    return flag;
                }
                DataStore.Instance.CloseConnection(); //2021-10-07 DB 커넥트 연결 해제

            }


            if (notdeleteCount > 0 && deleteCount == 0)
            {
                WizCommon.Popup.MyMessageBox.ShowBox("선택한 데이터는 모두 출고된 건이거나 오늘 검사하지 않은 품목이므로 \n 삭제가 불가능합니다.", "[삭제 전]", 0, 1);
                //WizCommon.Popup.MyMessageBox.ShowBox("선택한 데이터는 모두 출고된 건으로\n 삭제가 불가능합니다.", "[삭제 전]", 0, 1);
                flag = false;
                return flag;
            }
            else
            {
                flag = true;
                return flag;
            }
            #region 주석
            //int OutCnt = 0;
            //for (int i = 0; i < dgdMain.Rows.Count; i++)
            //{
            //    var Main = dgdMain.Rows[i].DataBoundItem as Frm_tins_Result_Q_CodeView;
            //    if (Main != null && Main.Check == true)
            //    {
            //        if (Main.OutDate.Trim().Equals("") == false)
            //        {
            //            OutCnt++;
            //        }
            //        else if (Main.PackID.Trim().Equals("") == false)
            //        {
            //            if (lstPackID.Contains(Main.PackID) == false)
            //            {
            //                lstPackID.Add(Main.PackID);
            //            }


            //        }
            //        else if (string.IsNullOrEmpty(Main.PackID.Trim()) // 2020.11.10 GDU, 합격 수량이 0 일 경우, 검사 테이블만 삭제할 수 있도록
            //            && string.IsNullOrEmpty(Main.OrderID) == false)
            //        {
            //            lstInspectID.Add(Main.OrderID + "|" + Main.PackID);
            //        }
            //    }
            //}

            //if (lstPackID.Count + lstInspectID.Count <= 0)
            //{
            //    // 출고되었을 경우 팝업창 출력 2020.11.10. KGH
            //    if (dgdMain.SelectedRows[0].Cells["OutDate"].Value.ToString() != null)
            //    {
            //        for (int i = 0; i < dgdMain.Rows.Count; i++)
            //        {
            //            if (dgdMain.Rows[i].Cells["Check"].Value.ToString().ToUpper() == "TRUE")
            //            {
            //                //MessageBox.Show("text");
            //                WizCommon.Popup.MyMessageBox.ShowBox("선택한 데이터는 모두 출고된 건으로\n 삭제가 불가능합니다.", "[삭제 전]", 0, 1);
            //                return true;
            //            }
            //        }
            //    }
            //    // 선택된 라벨이 없을 경우 해당 팝업창 출력
            //    WizCommon.Popup.MyMessageBox.ShowBox("삭제할 라벨을 선택(체크)해주세요.", "[삭제 전]", 0, 1);
            //    return false;
            //}

            //if (OutCnt > 0)
            //{
            //    //if (WizCommon.Popup.MyMessageBox.ShowBox("포장되어 출고된 건 " + OutCnt + "건 을 제외한 " + lstIns.Count + " 건을 모두 삭제하시겠습니까?", "[삭제 전]", 0, 1) == DialogResult.OK)
            //    if (WizCommon.Popup.MyMessageBox.ShowBox("총 " + lstIns.Count + "건이 삭제 완료 되었습니다.", "[삭제 완료]", 0, 1) == DialogResult.OK)
            //    {
            //        flag = true;
            //    }
            //}
            //else
            //{
            //    flag = true;
            //}

            //return flag;
            #endregion
        }



        private List<DataGridViewRow> lstReprint = new List<DataGridViewRow>(); // 재발행 라벨 리스트

        private bool CheckData()
        {
            bool IsOk = true;
            if (dgdMain.Rows.Count > 0)
            {
                //bool hasCheck = false;
                //foreach (DataGridViewRow dr in dgdMain.Rows)
                //{
                //    string strReprintDate = dr.Cells["BoxID"].Value.ToString().Trim();
                //    bool isChecked = (bool)dr.Cells["Check"].EditedFormattedValue;
                //    if (isChecked)//체크된
                //    {
                //        if (strReprintDate.Equals(""))
                //        {
                //            Message[0] = "[이동전표 확인]";
                //            Message[1] = "이동전표가 없을 경우 재발행되지 않습니다";
                //            WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                //            IsOk = false;
                //        }
                //        else
                //        {
                //            hasCheck = true;
                //        }
                //    }
                //}

                //if (!hasCheck)
                //{
                //    Message[0] = "[재발행라벨 선택]";
                //    Message[1] = "재발행할 라벨을 선택해야 합니다";
                //    WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                //    IsOk = false;
                //}

                lstReprint.Clear();

                foreach (DataGridViewRow dr in dgdMain.Rows)
                {
                    if ((bool)dr.Cells["Check"].EditedFormattedValue == true)
                    {
                        lstReprint.Add(dr);
                    }
                }

                if (lstReprint.Count == 0)
                {
                    Message[0] = "[재발행라벨 선택]";
                    Message[1] = "재발행할 라벨을 선택해야 합니다";
                    WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                }
            }
            else
            {
                Message[0] = "[조회]";
                Message[1] = "조회된 내용이 없습니다. 조회 후 클릭하여 주십시오.";
                WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                IsOk = false;
            }
            return IsOk;
        }

        private bool SaveData()
        {
            bool flag = true;

            List<string> list_Data = null;

            string sTagID = "011";
            string LabelArticleID = "";

            for (int i = 0; i < lstReprint.Count; i++)
            {
                try
                {
                    string[] bLabel = lstReprint[i].Cells["PackBoxID"].Value.ToString().Split(',');
                    //string g_sPrinterName = Lib.GetDefaultPrinter();
                    //WizWork.TSCLIB_DLL.openport(g_sPrinterName);
                    for (int k = 0; k < bLabel.Length; k++)
                    {
                        Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                        sqlParameter.Add("LabelID", bLabel[k].Trim());//상위품ID
                        
                        DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_prdIns_sBLabelInfo", sqlParameter, false);

                        DataRow dr = dt.Rows[0];

                        LabelArticleID = dr["ArticleID"].ToString();
                        string g_sPrinterName = Lib.GetDefaultPrinter();

                        list_Data = new List<string>();

                        list_Data.Add(Lib.CheckNull(dr["InBoxID"].ToString()));// 공정라벨
                        list_Data.Add(Lib.CheckNull(dr["Article"].ToString()));// 품명 1
                        list_Data.Add(Lib.stringFormatN0(Lib.ConvertDouble(Lib.CheckNull(dr["PackQty"].ToString()))) + " EA");// 수량 2
                        list_Data.Add(Lib.CheckNull(Lib.MakeDate(WizWorkLib.DateTimeClss.DF_FULL, dr["PackDate"].ToString())));// 일자 3
                        list_Data.Add(Lib.CheckNull(dr["Name"].ToString()));// 작업자 4

                        WizWork.TSCLIB_DLL.openport(g_sPrinterName);
                        if (SendWindowDllCommand(list_Data, sTagID, 1,0))
                        {
                            //if (k == 0) //2021-11-29 라벨발행을 한꺼번에 처리하여 한번나오게 조건 추가
                            //{
                                Message[0] = "[라벨발행 중]";
                                Message[1] = "라벨 발행중입니다. 잠시만 기다려주세요.";
                                WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 2, 2);
                            //}
                        }
                        else
                        {
                            Message[0] = "[라벨발행 실패]";
                            Message[1] = "라벨 발행에 실패했습니다. 관리자에게 문의하여주세요.\r\n<SendWindowDllCommand>";
                            WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 2, 2);
                        }
                        WizWork.TSCLIB_DLL.closeport();
                    }
                    //WizWork.TSCLIB_DLL.closeport();
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

            return flag;

            #region 버림
            //bool IsOK = true;
            //int Count = 0;
            ////if(grdData.Rows.Count == grdData.CurrentRow.Index)
            ////{
            ////    //MessageBox.Show(LoadResString(231));
            ////}

            //list_TWkLabelPrint = new List<WizWork.Sub_TWkLabelPrint>(); // 초기화

            //foreach (DataGridViewRow dr in dgdMain.Rows)
            //{
            //    DataGridViewCell Cell = dr.Cells["Check"];
            //    bool isChecked = (bool)Cell.EditedFormattedValue;
            //    if (isChecked)
            //    {
            //        list_TWkLabelPrint.Add(new WizWork.Sub_TWkLabelPrint());
            //        list_TWkLabelPrint[Count].sLabelID = dr.Cells["LabelID"].Value.ToString();
            //        list_TWkLabelPrint[Count].sReprintDate = dr.Cells["ReprintDate"].Value.ToString().Replace("-", "");
            //        list_TWkLabelPrint[Count].nQtyPerBox = Int32.Parse(Lib.CheckNum(dr.Cells["QtyPerBox"].Value.ToString()).Replace(",", ""));
            //        list_TWkLabelPrint[Count].sCreateuserID = Frm_tprc_Main.g_tBase.PersonID;
            //        list_TWkLabelPrint[Count].sLastProdArticleID = Lib.CheckNull(dr.Cells["LastProdArticleID"].Value.ToString());
            //        list_TWkLabelPrint[Count].sInstID = dr.Cells["InstID"].Value.ToString();
            //        Count++;
            //    }
            //}

            //UpdateWorkCardPrint(Count);

            //return IsOK;
            #endregion
        }

        #region Right - 라벨 재발행 (미구현)
        // Winform btnRePrint button 속성 Visible : false (서강정밀) 추후 타업체 재발행 활성화 필요. 2020.10.30.KGH
        private void btnRePrint_Click(object sender, EventArgs e)
        {
            if (CheckData() == true)
            {           
                if (SaveData() == true)
                {
                    Ftm.LogSave(this.GetType().Name, "P"); //2022-06-23 인쇄, 재발행
                    FillGrid();
                    Ftm.LogSave(this.GetType().Name, "R"); //2022-06-23 조회
                }             
            }
        }
        #endregion

        private void dgdMain_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                //var PackID = dgdMain.Rows[e.RowIndex].Cells["PackID"].Value.ToString();

                if (dgdMain.Rows[e.RowIndex].Cells["Check"].Value.ToString().ToUpper() == "FALSE")
                {
                    dgdMain.Rows[e.RowIndex].Cells["Check"].Value = true;
                    //for (int i = 0; i < dgdMain.Rows.Count; i++)
                    //{
                    //    if (PackID == dgdMain.Rows[i].Cells["PackID"].Value.ToString())
                    //    {
                    //        dgdMain.Rows[i].Cells["Check"].Value = true;
                    //    }
                    //}
                    if (!dgdMain.Rows[e.RowIndex].Cells["DefectQty"].Value.Equals("0"))
                    {
                        setDgdSub(e.RowIndex);
                    }
                }
                else if (dgdMain.Rows[e.RowIndex].Cells["Check"].Value.ToString().ToUpper() == "TRUE")
                {
                    dgdMain.Rows[e.RowIndex].Cells["Check"].Value = false;
                    dgdSub.Visible = false;
                }

               // setDgdSub(e.RowIndex);

            }
        }

        #region 불량 내역 조회 : FillGridSub()

        private void FillGridSub(string OrderID, string PackID)
        {
            try
            {
                lstSub.Clear();

                Dictionary<string, object> sqlParameters = new Dictionary<string, object>();
                sqlParameters.Add("OrderID", OrderID);
                sqlParameters.Add("PackID", PackID);
               // sqlParameters.Add("RollSeq", RollSeq);
                DataTable dt = DataStore.Instance.ProcedureToDataTable("[xp_prdIns_sInspectSub]", sqlParameters, false);

                if (dt != null
                    && dt.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        i++;

                        var Ins = new Frm_tins_InsDefectList()
                        {
                            Num = i,
                            KDefect = dr["KDefect"].ToString().Trim(),
                            DefectQty = Lib.stringFormatN0(dr["DefectQty"])
                        };

                        lstSub.Add(Ins);
                    }
                }

                BindingSource bs = new BindingSource();
                bs.DataSource = lstSub;
                dgdSub.DataSource = bs;
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

        #endregion

        #region 서브 그리드  : 불량 내역이 있으면 활성화

        private void setDgdSub(int index)
        {
            // 불량이 있으면 불량 데이터 그리드 활성화
            double DefectQty = Lib.ConvertDouble(dgdMain.Rows[index].Cells["DefectQty"].Value.ToString());
            if (DefectQty > 0)
            {
                string OrderID = dgdMain.Rows[index].Cells["OrderID"].Value.ToString();
                string PackID = dgdMain.Rows[index].Cells["PackID"].Value.ToString();
                //int RollSeq = Lib.ConvertInt(dgdMain.Rows[index].Cells["RollSeq"].Value.ToString());

                /* 불량 내역 조회 */
                FillGridSub(OrderID, PackID);

                /* 서브 그리드 세팅 높이 세팅 */
                int rowH = dgdSub.Rows.GetRowsHeight(DataGridViewElementStates.Visible) + 1;
                int headerH = dgdSub.ColumnHeadersHeight;

                dgdSub.Height = rowH + headerH;

                /* dgdSub 좌표 구하기 */

                #region 셀 바로 밑에 보이게 하는 버전
                //// 최대 Y 값.
                //int maxY = dgdMain.Location.Y + dgdMain.Height;

                //// 선택 셀 Bottom 좌표
                //int selCellY = dgdMain.GetCellDisplayRectangle(1, index, false).Bottom + 45; // 왜 45라는 오차가 발생하는지 모르겠음

                //// dgdMain 을 벗어나지 않는다면
                //if (selCellY + dgdSub.Height < maxY)
                //{
                //    dgdSub.Location = new Point(dgdSub.Location.X, selCellY);
                //    //dgdSub.Location = new Point(dgdSub.Location.X, 285);
                //}
                //else // 벗어나면 위로
                //{
                //    selCellY = dgdMain.GetCellDisplayRectangle(1, index, false).Top - dgdSub.Height + 45;

                //    dgdSub.Location = new Point(dgdSub.Location.X, selCellY);
                //}
                #endregion
                #region 선택 셀이 dgdMain 의 절반을 넘어서면 위로 보이도록

                int halfY = (dgdMain.Location.Y + dgdMain.Height) / 2;

                int selCellY = dgdMain.GetCellDisplayRectangle(1, index, false).Bottom + 25; 

                if (selCellY <= halfY)
                {
                    dgdSub.Location = new Point(dgdSub.Location.X, halfY + dgdSub.RowTemplate.Height + 13);
                }
                else
                {
                    dgdSub.Location = new Point(dgdSub.Location.X, halfY - dgdSub.MaximumSize.Height);
                }

                #endregion

                dgdSub.Visible = true;
            }
            else
            {
                dgdSub.Visible = false;
            }
        }

        #endregion

        #region 검사 조회 : FillGrid()

        private void FillGrid()
        {

            try
            {

                // 불량 그리드 숨기기
                dgdSub.Visible = false;
                
                // 총 검사수량
                int SumInsQty = 0;
                lstMain.Clear();


                Dictionary<string, object> sqlParameters = new Dictionary<string, object>();
                sqlParameters.Add("ChkExamDate", chkDate.Checked == true ? 1 : 0);
                sqlParameters.Add("SDate", mtb_From.Text.Replace("-", ""));
                sqlParameters.Add("EDate", mtb_To.Text.Replace("-", ""));
                sqlParameters.Add("ChkArticle", chkArticle.Checked == true ? 1 : 0);
                sqlParameters.Add("Article", txtArticle.Text.Trim());
                sqlParameters.Add("PersonCheck", personcheck.Checked == true ? 1 : 0); //2021-06-09 검색 조건 작업자 추가
                sqlParameters.Add("Person", txtperson.Text.Trim()); //2021-06-09 검색 조건 작업자 추가
                DataTable dt = DataStore.Instance.ProcedureToDataTable("[xp_prdIns_sInspect]", sqlParameters, false);

                if (dt != null
                    && dt.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        i++;
                        SumInsQty += (int)Lib.ConvertDouble(dr["RealQty"].ToString());

                        var Ins = new Frm_tins_Result_Q_CodeView()
                        {
                            Num = i,
                            ExamDate = Lib.DatePickerFormat(dr["ExamDate"].ToString()),
                            ExamTime = Lib.DateTimeFormat(dr["ExamTime"].ToString()),
                            KCustom = dr["KCustom"].ToString(),
                            OrderID = dr["OrderID"].ToString(),
                            ArticleID = dr["ArticleID"].ToString().Trim(),
                            BuyerArticleNo = dr["BuyerArticleNo"].ToString(),

                            Article = dr["Article"].ToString(),
                            PersonID = dr["PersonID"].ToString(),
                            Name = dr["Name"].ToString(),
                            PackID = dr["PackID"].ToString(),
                            RealQty = Lib.stringFormatN0(dr["RealQty"]),

                            CtrlQty = Lib.stringFormatN0(dr["CtrlQty"]),
                            DefectQty = Lib.stringFormatN0(dr["DefectQty"]),
                            CustomID = dr["CustomID"].ToString(),

                            BoxID = dr["BoxID"].ToString().Trim(),

                            PackBoxID = dr["PackBoxID"].ToString().Trim(),
                            OutDate = dr["OutDate"].ToString().Trim(),
                            RollSeq = dr["RollSeq"].ToString().Trim(),

                            Check = false 
                        };

                        lstMain.Add(Ins);

                    }

                    setSumDgv(i, SumInsQty);

                    var bs = new BindingSource();    //2021-07-21 BindingSource -> var 로 변경               
                    bs.DataSource = lstMain;
                    dgdMain.DataSource = bs;
                }
                //2021-06-05 검색 조회 없을때 추가
                else if (dt.Rows.Count == 0)
                {
                    WizCommon.Popup.MyMessageBox.ShowBox("조회 정보를 찾을 수 없습니다.", "[조회]", 0, 1);
                    txtArticle.Text = "";
                    txtperson.Text = "";
                    setSumDgv(0, SumInsQty);
                    var bs = new BindingSource();   //2021-07-21 BindingSource -> var 로 변경 
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

        private void setSumDgv(int count, int SumQty)
        {
            dgdSum.Rows.Clear();

            dgdSum.Rows.Add("검사 합계"
                                        , Lib.stringFormatN0(count) + " 건"
                                        , Lib.stringFormatN0(SumQty)
                                        );

            dgdSum.CurrentCell.Selected = false;
        }

        #endregion
        public bool SendWindowDllCommand(List<string> vData, string sTagID, int nPrintCount, int nDefectCnt)
        {
            try
            {
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Add("TagID", sTagID);
                DataTable dt = DataStore.Instance.ProcedureToDataTable("[xp_WizWork_sMtTag]", sqlParameter, false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    Sub_m_tTag.sTagID = Lib.CheckNull(dr["TagID"].ToString());
                    Sub_m_tTag.sTag = Lib.CheckNull(dr["Tag"].ToString());
                    Sub_m_tTag.nWidth = int.Parse(dr["Width"].ToString());
                    Sub_m_tTag.nHeight = int.Parse(dr["Height"].ToString());
                    //Sub_m_tTag.sUse_YN = dr["clss"].ToString();

                    Sub_m_tTag.nDefHeight = int.Parse(dr["DefHeight"].ToString());
                    Sub_m_tTag.nDefBaseY = int.Parse(dr["DefBaseY"].ToString());
                    Sub_m_tTag.nDefBaseX1 = int.Parse(dr["DefBaseX1"].ToString());
                    Sub_m_tTag.nDefBaseX2 = int.Parse(dr["DefBaseX2"].ToString());
                    Sub_m_tTag.nDefBaseX3 = int.Parse(dr["DefBaseX3"].ToString());

                    Sub_m_tTag.nDefGapY = int.Parse(dr["DefGapY"].ToString());
                    Sub_m_tTag.nDefGapX1 = int.Parse(dr["DefGapX1"].ToString());
                    Sub_m_tTag.nDefGapX2 = int.Parse(dr["DefGapX2"].ToString());
                    Sub_m_tTag.nDefLength = int.Parse(dr["DefLength"].ToString());
                    Sub_m_tTag.nDefHCount = int.Parse(dr["DefHCount"].ToString());

                    Sub_m_tTag.nDefBarClss = int.Parse(dr["DefBarClss"].ToString());
                    Sub_m_tTag.nGap = int.Parse(dr["Gap"].ToString());
                    Sub_m_tTag.sDirect = dr["Direct"].ToString();
                }

                dt = null;
                Dictionary<string, object> sqlParameter2 = new Dictionary<string, object>();
                sqlParameter2.Add("TagID", sTagID);
                dt = DataStore.Instance.ProcedureToDataTable("[xp_WizWork_sMtTagSub]", sqlParameter, false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dr = dt.Rows[i];

                        list_m_tItem.Add(new WizWork.TTagSub());

                        //list_m_tItem[i]' .sTag_ID = int.Parse(dr["TagID"].ToString());
                        //list_m_tItem[i]' .sTag_Seq = 	int.Parse(dr["TagSeq"].ToString());
                        list_m_tItem[i].sName = dr["Name"].ToString();
                        list_m_tItem[i].nType = int.Parse(dr["Type"].ToString());
                        list_m_tItem[i].nAlign = int.Parse(dr["Align"].ToString());
                        list_m_tItem[i].x = int.Parse(dr["x"].ToString());
                        list_m_tItem[i].y = int.Parse(dr["y"].ToString());
                        list_m_tItem[i].nFont = int.Parse(dr["Font"].ToString());
                        list_m_tItem[i].nLength = int.Parse(dr["Length"].ToString());
                        list_m_tItem[i].nHMulti = int.Parse(dr["HMulti"].ToString());
                        list_m_tItem[i].nVMulti = int.Parse(dr["VMulti"].ToString());
                        list_m_tItem[i].nRelation = int.Parse(dr["Relation"].ToString());
                        list_m_tItem[i].nRotation = int.Parse(dr["Rotation"].ToString());
                        list_m_tItem[i].nSpace = int.Parse(dr["Space"].ToString());

                        list_m_tItem[i].nPrevItem = int.Parse(dr["PrevItem"].ToString());
                        list_m_tItem[i].nBarType = int.Parse(dr["BarType"].ToString());
                        list_m_tItem[i].nBarHeight = int.Parse(dr["BarHeight"].ToString());
                        list_m_tItem[i].nFigureWidth = int.Parse(dr["FigureWidth"].ToString());
                        list_m_tItem[i].nFigureHeight = int.Parse(dr["FigureHeight"].ToString());
                        list_m_tItem[i].nThickness = int.Parse(dr["Thickness"].ToString());
                        list_m_tItem[i].sImageFile = dr["ImageFile"].ToString();
                        list_m_tItem[i].nWidth = int.Parse(dr["Width"].ToString());
                        list_m_tItem[i].nHeight = int.Parse(dr["Height"].ToString());
                        list_m_tItem[i].nVisible = int.Parse(dr["Visible"].ToString());

                        list_m_tItem[i].sFontName = dr["FontName"].ToString();
                        list_m_tItem[i].sFontStyle = dr["FontStyle"].ToString();
                        list_m_tItem[i].sFontUnderLine = dr["FontUnderLine"].ToString();


                        //int a = 0;
                        //foreach (string str in lData)
                        //{
                        //    Console.WriteLine(a++.ToString() + "/////" + str + "///////");
                        //}

                        //20171011 김종영 수정 type 변경
                        //if (list_m_tItem[i].nType == 1 && list_m_tItem[i].sName.Substring(0, 1).ToUpper() == "D")

                        if (list_m_tItem[i].nType < 2 && list_m_tItem[i].sName.Substring(0, 1).ToUpper() == "D")
                        {
                            if (list_m_tItem[i].nRelation == 0 && list_m_tItem[i].nType == 1)//바코드
                            {
                                list_m_tItem[i].sText = vData[0];
                            }

                            else if (list_m_tItem[i].nRelation > 0 && list_m_tItem[i].nType == 0)
                            {
                                if (vData.Count > list_m_tItem[i].nRelation)
                                {
                                    list_m_tItem[i].sText = vData[list_m_tItem[i].nRelation];
                                }
                                else
                                {
                                    list_m_tItem[i].sText = "";
                                }
                            }
                        }
                        else
                        {
                            list_m_tItem[i].sText = Lib.CheckNull(dr["Text"].ToString());
                        }

                        //if (list_m_tItem[i].sName.Substring(0, 1).ToUpper() == "D")
                        //{
                        //    if (list_m_tItem[i].nRelation == 0 && list_m_tItem[i].nType == 8)//QR 바코드
                        //    {
                        //        list_m_tItem[i].sText = vData[0];
                        //    }

                        //    else if (list_m_tItem[i].nRelation > 0 && list_m_tItem[i].nType == 0)
                        //    {
                        //        if (vData.Count > list_m_tItem[i].nRelation)
                        //        {
                        //            list_m_tItem[i].sText = vData[list_m_tItem[i].nRelation];
                        //        }
                        //        else
                        //        {
                        //            list_m_tItem[i].sText = "";
                        //        }
                        //    }
                        //}
                        //else
                        //{
                        //    list_m_tItem[i].sText = Lib.CheckNull(dr["Text"].ToString());
                        //}


                    }
                }

                double strWidth = 0;
                double strHeight = 0;
                try
                {
                    if (Lib.CheckNum(Sub_m_tTag.nWidth.ToString()) != "0")
                    {
                        strWidth = (Sub_m_tTag.nWidth / 10F);
                    }
                    if (Lib.CheckNum(Sub_m_tTag.nHeight.ToString()) != "0")
                    {
                        strHeight = (Sub_m_tTag.nHeight / 10F);
                    }
                }
                catch
                {
                    strWidth = 0;
                    strHeight = 0;
                }


                // setup

                //TSCLIB_DLL.setup(stringFormatN1(strWidth), stringFormatN1(strHeight), "8", "15", "0", "3", "0");//기존소스
                //WizWork.TSCLIB_DLL.setup(stringFormatN1(strWidth), stringFormatN1(strHeight), "4", "15", "1", "4", "0"); // GLS Black Mark Setting
                WizWork.TSCLIB_DLL.setup(stringFormatN1(strWidth), stringFormatN1(strHeight), "4", "15", "0", "3", "0"); // GLS Black Mark Setting, 2021-11-17 이걸로 수정
                //TSCLIB_DLL.setup(stringFormatN1(strWidth), stringFormatN1(strHeight), "8", "15", "0", "0", "0");//감열지 테스트용
                WizWork.TSCLIB_DLL.sendcommand("DIRECTION " + Sub_m_tTag.sDirect);

                WizWork.TSCLIB_DLL.clearbuffer();
                string sText = "";
                string[] sBarType = new string[2];

                for (int i = 0; i < list_m_tItem.Count; i++)
                {
                    if (list_m_tItem[i].nVisible > 0)//출력여부
                    {
                        //'바코드
                        if (list_m_tItem[i].nType == WizWork.EnumItem.IO_BARCODE)
                        {
                            if (list_m_tItem[i].nPrevItem == 0)
                            {
                                if (list_m_tItem[i].nBarType == 0)// 1:1 Code
                                {
                                    sBarType[0] = "1";
                                    sBarType[1] = "1";
                                }
                                else                            // 2:5 Code
                                {
                                    sBarType[0] = "2";
                                    sBarType[1] = "5";
                                }

                                string ReadAble = "0"; // 1 : 자동 바코드 출력 / 0 : 안보임

                                WizWork.TSCLIB_DLL.barcode(list_m_tItem[i].x.ToString(), // x
                                                   list_m_tItem[i].y.ToString(), // y
                                                   "39", // type
                                                   list_m_tItem[i].nBarHeight.ToString(), // height
                                                   ReadAble, // ReadAble
                                                   list_m_tItem[i].nRotation.ToString(), // Rotation
                                                   sBarType[0], // Narrow
                                                   sBarType[1], // Wide
                                                   list_m_tItem[0].sText
                                                   );

                                if (ReadAble.Equals("0"))
                                {
                                    // 바코드 글자 세팅
                                    int intx = list_m_tItem[i].x;
                                    int inty = list_m_tItem[i].y + 60;
                                    int fontheight = 50;
                                    int rotation = 0;
                                    int fontstyle = 0;
                                    int fontunderline = 0;
                                    string FaceName = "맑은 고딕";
                                    string content = Lib.CheckNull(list_m_tItem[i].sText).Trim();

                                    WizWork.TSCLIB_DLL.windowsfont(intx, inty, fontheight, rotation, fontstyle, fontunderline, FaceName, content);
                                }
                            }
                        }

                        ////'QR CODE
                        //if (list_m_tItem[i].nType == WizWork.EnumItem.IO_QRcode)
                        //{
                        //    //QRCODE x, y, ECC Level,cell width, mode, rotation,[model, mask,]"content"

                        //    string qr_command = "QRCODE " + list_m_tItem[i].x.ToString() + "," +
                        //                list_m_tItem[i].y.ToString() + "," +
                        //                "M" + "," +     // ECC Level (L,M,Q,H)
                        //                list_m_tItem[i].nFigureWidth.ToString() + "," +
                        //                "M" + "," +         // MODE (A,M)
                        //                "0" + "," +
                        //                "M2" + "," +
                        //                "S1" + "," +
                        //                //list_m_tItem[i].sText;
                        //                "\"A" + list_m_tItem[0].sText + "\"";


                        //    //string qr_command = "QRCODE 100,80,L,7,M,0,M2,S1," + "\"A" + list_m_tItem[0].sText + "\"";

                        //    WizWork.TSCLIB_DLL.sendcommand(qr_command);
                        //}


                        //데이터 OR 문자
                        else if (list_m_tItem[i].nType == WizWork.EnumItem.IO_DATA || list_m_tItem[i].nType == WizWork.EnumItem.IO_TEXT)
                        {
                            sText = Lib.CheckNull(list_m_tItem[i].sText);
                            int intx = list_m_tItem[i].x;
                            int inty = list_m_tItem[i].y;
                            int fontheight = int.Parse((list_m_tItem[i].nFont).ToString());
                            int rotation = list_m_tItem[i].nRotation;
                            int fontstyle = int.Parse(Lib.CheckNum(list_m_tItem[i].sFontStyle));
                            int fontunderline = int.Parse(Lib.CheckNum(list_m_tItem[i].sFontUnderLine));
                            string szFaceName = list_m_tItem[i].sFontName;
                            string content = sText.Trim();

                            WizWork.TSCLIB_DLL.windowsfont(intx, inty, fontheight, rotation, fontstyle, fontunderline, szFaceName, content);
                        }
                        //'선(Line)-5이하
                        else if (list_m_tItem[i].nType == WizWork.EnumItem.IO_LINE)// && (list_m_tItem[i].nFigureHeight <= 5 || list_m_tItem[i].nFigureWidth <= 5))
                        {
                            int x1 = 0;
                            int x2 = 0;
                            int y1 = 0;
                            int y2 = 0;
                            int.TryParse(list_m_tItem[i].x.ToString(), out x1);
                            int.TryParse(list_m_tItem[i].y.ToString(), out y1);
                            int.TryParse(list_m_tItem[i].nFigureWidth.ToString(), out x2);
                            int.TryParse(list_m_tItem[i].nFigureHeight.ToString(), out y2);

                            string IsDllStr = "BAR " + x1.ToString() + ", " + y1.ToString() + ", " + x2.ToString() + ", " + y2.ToString();

                            WizWork.TSCLIB_DLL.sendcommand(IsDllStr);
                        }
                        else if (list_m_tItem[i].nType == WizWork.EnumItem.IO_BOX)
                        {
                            int x1 = 0;
                            int x2 = 0;
                            int y1 = 0;
                            int y2 = 0;
                            int nTh = 0;
                            int.TryParse(list_m_tItem[i].x.ToString(), out x1);
                            int.TryParse(list_m_tItem[i].y.ToString(), out y1);
                            int.TryParse(list_m_tItem[i].nFigureWidth.ToString(), out x2);
                            int.TryParse(list_m_tItem[i].nFigureHeight.ToString(), out y2);
                            int.TryParse(list_m_tItem[i].nThickness.ToString(), out nTh);

                            string IsDllStr = "BOX " + x1.ToString() + ", " + y1.ToString() + ", " + x2.ToString() + ", " + y2.ToString() + ", " + nTh.ToString();

                            WizWork.TSCLIB_DLL.sendcommand(IsDllStr);
                        }

                    }
                }
                if (m_ProcessID == "0405")
                {
                    nPrintCount = 2;
                }

                WizWork.TSCLIB_DLL.printlabel("1", nPrintCount.ToString());

                list_m_tItem = new List<WizWork.TTagSub>();
                vData = new List<string>();
                return true;
            }
            catch (Exception excpt)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의<SendWindowDllCommand>\r\n{0}", excpt.Message), "[오류]", 0, 1);
                return false;
            }
        }
        private string stringFormatN1(object obj)
        {
            return string.Format("{0:N0}", obj);
        }
        //2021-05-29 특수문자 있는 키패드
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
        //2021-06-09 검색조건 작업자 추가
        private void personcheck_Click(object sender, EventArgs e)
        {
            if (personcheck.Checked)
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

                        txtperson.Focus();

                    }
                }
                catch (Exception ex)
                {
                    //txtBuyerArticleNo.Text = "";
                    useMasicKeyboard(txtperson);
                }

                //txtperson.Text = "";
                //useMasicKeyboard(txtperson);

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
                        //System.Diagnostics.Process.Start(path); 2021-09-09 체크표시를 풀면 키보드가 나오지 않게 주석

                        txtperson.Focus();

                    }
                }
                catch (Exception ex)
                {
                    //txtBuyerArticleNo.Text = "";
                    useMasicKeyboard(txtperson);
                }

                //txtperson.Text = "";
                //useMasicKeyboard(txtperson);
            }
        }
        //2021-06-09 검색조건 작업자 추가
        private void txtperson_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                personcheck.Checked = true;
                FillGrid();
                Ftm.LogSave(this.GetType().Name, "R"); //2022-06-23 조회
            }
        }

        private void dgdMain_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
            //if (e.Exception != null && e.Context == DataGridViewDataErrorContexts.Commit)
            //{
            //    MessageBox.Show("닫기를 누른 뒤 재조회를 해주세요");
            //}
        }

        //Serial 번호 인쇄
        private void btnSerialPrint_Click(object sender, EventArgs e)
        {
            List<string> A = new List<string>();

            string g_sPrinterName = Lib.GetDefaultPrinter();
            WizWork.TSCLIB_DLL.openport(g_sPrinterName);
            if (SendWindowDllCommand(A, "015", 1, 0))
            {
                //if (k == 0) //2021-11-29 라벨발행을 한꺼번에 처리하여 한번나오게 조건 추가
                //{
                Message[0] = "[라벨발행 중]";
                Message[1] = "라벨 발행중입니다. 잠시만 기다려주세요.";
                WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 2, 2);
                //}
            }
            else
            {
                Message[0] = "[라벨발행 실패]";
                Message[1] = "라벨 발행에 실패했습니다. 관리자에게 문의하여주세요.\r\n<SendWindowDllCommand>";
                WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 2, 2);
            }
            WizWork.TSCLIB_DLL.closeport();
        }

        //public bool Send_ExcelPrintCommand(List<string> vData, string sTagID, string LabelArticleID)
        //{
        //    Dictionary<string, object> sqlParameter2 = new Dictionary<string, object>();
        //    sqlParameter2.Add("TagID", sTagID);
        //    DataTable dt = DataStore.Instance.ProcedureToDataTable("[xp_WizWork_sMtTag]", sqlParameter2, false);
        //    if (dt != null && dt.Rows.Count > 0)
        //    {
        //        DataRow dr = dt.Rows[0];
        //        Sub_m_tTag.sTagID = Lib.CheckNull(dr["TagID"].ToString());
        //        Sub_m_tTag.sTag = Lib.CheckNull(dr["Tag"].ToString());
        //        Sub_m_tTag.nWidth = int.Parse(dr["Width"].ToString());
        //        Sub_m_tTag.nHeight = int.Parse(dr["Height"].ToString());
        //    }

        //    string strWidth = "";
        //    string strHeight = "";
        //    try
        //    {
        //        if (Lib.CheckNum(Sub_m_tTag.nWidth.ToString()) != "0")
        //        {
        //            strWidth = (Sub_m_tTag.nWidth / 10F).ToString();
        //        }
        //        if (Lib.CheckNum(Sub_m_tTag.nHeight.ToString()) != "0")
        //        {
        //            strHeight = (Sub_m_tTag.nHeight / 10F).ToString();
        //        }
        //    }
        //    catch
        //    {
        //        strWidth = "0";
        //        strHeight = "0";
        //    }

        //    WizWork.TSCLIB_DLL.setup(strWidth, strHeight, "4", "15", "1", "3", "0"); // GLS Black Mark Setting

        //    excelApp = null;
        //    excelApp = new Excel.Application();
        //    string excelopen_path = "";

        //    //excelopen_path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\Excel\\ANTRH.xlsx";

        //    if (LabelArticleID.Substring(4, 1) == "1") //5번째가 1이면 LH
        //    {
        //        excelopen_path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\RePort\\ANTLH.xls";
        //    }
        //    else if (LabelArticleID.Substring(4, 1) == "2") //5번째가 2이면 RH
        //    {
        //        excelopen_path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\RePort\\ANTRH.xls";
        //    }
        //    else
        //    {
        //        excelopen_path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\RePort\\ANT공용.xls";
        //    }

        //    workbook = excelApp.Workbooks.Open(excelopen_path, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
        //                                    Type.Missing, Type.Missing, Type.Missing, Type.Missing,
        //                                    Type.Missing, Type.Missing, Type.Missing, Type.Missing,
        //                                    Type.Missing, Type.Missing);
        //    worksheet = (Excel.Worksheet)workbook.Sheets["sheet1"];

        //    //바코드
        //    range = worksheet.get_Range("B6", "E7");
        //    range.Value2 = "*" + vData[0] + "*";
        //    range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

        //    ////바코드 번호
        //    range = worksheet.get_Range("B7", "E7");
        //    range.Value2 = vData[0];
        //    range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

        //    //차종
        //    range = worksheet.get_Range("C2", "E2");
        //    range.Value2 = vData[1];
        //    range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

        //    //품번
        //    range = worksheet.get_Range("C3", "E3");
        //    range.Value2 = vData[2];
        //    range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

        //    //품명
        //    range = worksheet.get_Range("C4", "G4");
        //    range.Value2 = vData[3];
        //    range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

        //    //LOT
        //    range = worksheet.get_Range("C5", "D5");
        //    range.Value2 = vData[4] + "-" + vData[8];
        //    range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

        //    //수량
        //    range = worksheet.get_Range("F5", "G5");
        //    range.Value2 = vData[5];
        //    range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

        //    //작업조
        //    range = worksheet.get_Range("F6", "G6");
        //    range.Value2 = vData[6];
        //    range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

        //    //작업자
        //    range = worksheet.get_Range("F7", "G7");
        //    range.Value2 = vData[7];
        //    range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

        //    worksheet.PageSetup.PaperSize = (Microsoft.Office.Interop.Excel.XlPaperSize)int.Parse("261");
        //    worksheet.PrintOutEx(1, 1, 1);


        //    workbook.Close(false);
        //    excelApp.Quit();

        //    //excel process를 전부 닫음(실행중인 다른 excel도 전부 닫힘)
        //    Process[] process = Process.GetProcessesByName("EXCEL");
        //    foreach (Process p in process)
        //    {
        //        if (!string.IsNullOrEmpty(p.ProcessName))
        //        {
        //            try
        //            {
        //                p.Kill();
        //            }
        //            catch
        //            {

        //            }
        //        }
        //    }


        //    return true;
        //}

    }

    //2021-06-10 컬럼 위치 수정이 필요한 경우 여기서 순서 바꾸면 수정이 되는 것으로 보임
    #region 검사실적 코드뷰 : Frm_tins_Result_Q_CodeView
    public class Frm_tins_Result_Q_CodeView
    {
        public int Num { get; set; }
        public string PackID { get; set; }
        public string ExamDate { get; set; }
        public string ExamTime { get; set; }
        public string KCustom { get; set; }
        public string ArticleID { get; set; }
        public string BuyerArticleNo { get; set; }
        public string Article { get; set; }
        public string RealQty { get; set; }
        public string CtrlQty { get; set; }
        public string DefectQty { get; set; }
        public string PersonID { get; set; }
        public string Name { get; set; }
        public string BoxID { get; set; }
        public string PackBoxID { get; set; }
        public string OutDate { get; set; }

        public string OrderID { get; set; }
        public string CustomID { get; set; }
        public string RollSeq { get; set; }
        public bool Check { get; set; }

    }
    #endregion

    #region 불량내역 코드뷰 : Frm_tins_InsDefectList

    class Frm_tins_InsDefectList
    {
        public int Num { get; set; }
        public string KDefect { get; set; }
        public string DefectQty { get; set; }
    }

    class Frm_dd
    {
        public string outclss { get; set; }
    }

    #endregion
}
