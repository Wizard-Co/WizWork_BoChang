using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using Microsoft.VisualBasic;
using System.Management;
using System.Printing;
using System.Data.SqlClient;
using WizCommon;
using System.Runtime.InteropServices;

namespace WizWork
{
    public partial class frm_tprc_Work_U : Form
    {
        private string m_LogID = "";

        private string updateJobID = "";        // 물고 들어간 Job ID
        private string m_ProcessID = "";
        private string m_LabelID = "";          // Start Save Label ID
        private string ProdQty = "";

        private string m_WorkStartDate = "";
        private string m_WorkStartTime = "";

        /////////////////////////////
        ///
        private string m_MachineName = "";
        private string m_MachineID = "";
        private string m_MtrExceptYN = "";
        private string m_OutwareExceptYN = "";

        private string m_LastArticleYN = "";
        private string m_OrderID = "";
        private int m_OrderSeq = 0;
        private string m_OrderNO = "";
        private string m_UnitClss = "";
        private string m_UnitClssName = "";

        private string m_EffectDate = "";
        private string m_ProdAutoInspectYN = "";
        private string m_OrderArticleID = "";
        private string m_ArticleID = "";
        private string m_LabelGubun = "";

        private string m_Inspector = "";
        private double m_RemainQty = 0;   //      입고수량
        private double m_LocRemainQty = 0;  //    '자품목 현 재고량
        private double m_douReqQty = 0; //현재품목 소요량
        private double m_douProdCapa = 0;
        private double m_MindouProdCapa = 0; //하위품이 여러개일 경우 가장 작은 생산가능량을 사용하기 위해 추가

        public double m_CycleTime = 0;

        // 앞선 1번의 결과물을 알아야 뒤 라벨에 이 값을 그대로 박아 넣을 수 있으니까 가져와야 해.
        private string Wh_Ar_DayOrNightID = ""; // 주간이냐 / 야간이냐. 

        // PL_InputDet_SEQ 가 1이더라도, 라벨프린트 발행여부 에 따라, 뽑을지 말지 결정해야 한다.
        private string Wh_Ar_LabelPrintYN = "";

        private double m_LabelSumQty = 0; //2021-12-01 로트별 총 수량을 위해 변수 추가

        private string m_PCMtrExceptYN = "";      // 2022-06-08 공정별 예외처리 체크용도
        private string m_ScanExceptYN = "";  //스캔 여부 판단하는 용도

        string sFirst_ArticleID = "";   //하위품중에 윗칸에 있는 품명ID 2023.04.20 생산가능량일때 아티클 값 비교

        INI_GS gs = new INI_GS();
        //public Sub_TMold[] Sub_TMold = null;
        public Sub_TWkResult Sub_TWkResult = new Sub_TWkResult();
        public Sub_TWkResultArticleChild Sub_TWkResultArticleChild = new Sub_TWkResultArticleChild();
        public Sub_TWkLabelPrint Sub_TWkLabelPrint = new Sub_TWkLabelPrint();
        public Sub_TtdChange Sub_Ttd = new Sub_TtdChange();
        public TTag Sub_m_tTag = new TTag();
        public TTagSub Sub_m_tItem = new TTagSub();
        public List<TTagSub> list_m_tItem = new List<TTagSub>();
        public List<Sub_TWkResultArticleChild> list_TWkResultArticleChild = new List<Sub_TWkResultArticleChild>();
        public List<Sub_TWkResult> list_TWkResult = new List<Sub_TWkResult>();
        public List<Sub_TWkLabelPrint> list_TWkLabelPrint = new List<Sub_TWkLabelPrint>();

        public List<Sub_TMold> list_TMold = null;

        public Sub_TWkResult_By_Cutting Sub_TWkResult_By_Cutting = new Sub_TWkResult_By_Cutting();
        public Sub_TWkResultArticleChild_By_Cutting Sub_TWkResultArticleChild_By_Cutting = new Sub_TWkResultArticleChild_By_Cutting();
        public Sub_TWkLabelPrint_By_Cutting Sub_TWkLabelPrint_By_Cutting = new Sub_TWkLabelPrint_By_Cutting();
        public List<Sub_TWkResultArticleChild_By_Cutting> list_TWkResultArticleChild_By_Cutting = new List<Sub_TWkResultArticleChild_By_Cutting>();
        public List<Sub_TWkResult_By_Cutting> list_TWkResult_By_Cutting = new List<Sub_TWkResult_By_Cutting>();
        public List<Sub_TWkLabelPrint_By_Cutting> list_TWkLabelPrint_By_Cutting = new List<Sub_TWkLabelPrint_By_Cutting>();

        List<string> lData = null;

        List<string> ListChildLabelID = null; //2021-11-30 하위품 라벨 리스트

        List<string> ListChildArticle = null; //2021-11-30 품번 라벨 리스트

        List<string> ListChildArticleID = null; //2022-05-18 ArticleID 리스트

        private string IsTagID = "";
        string[] m_sData = null;
        WizWorkLib Lib = new WizWorkLib();
        LogData LogData = new LogData(); //2022-06-21 log 남기는 함수

        
        private string LabelPrintYN = "N";      // 공정 간 이동전표를 뽑아햐 합니까? 그냥 저장만 하면 됩니까?
        string[] Message = new string[2];
        
        public bool blPRReuslt = true;  //해당 작업지시의 평량결과가 있는지 없는지
        public bool blpldClose = false; //해당 작업지시에서 현재공정보다 앞공정의 작업실적이 있는지 없는지
        public bool blSHExit = false;     //성형 작업종료 [uwkResult프로시저와 나머지 프로시저 사용]
        public string JobID0401 = "";
        public bool blClose = false;

        WizCommon.Popup.Frm_CMNumericKeypad FK = null;
        WizCommon.Popup.Frm_CMKeypad FCK = null;

        private List<string> lstLabelList = new List<string>();
        private List<float> lstQty = new List<float>();
        private List<string> lstLabelQtyList = new List<string>(); //2022-06-02
        private List<string> lstLOTIDQtyList = new List<string>(); //2022-06-02
        private List<string> lstArticleIDList = new List<string>(); //2022-12-02
        private List<string> lstArticleList = new List<string>();   //2023-01-13 일괄스캔한 Article

        private List<string> lstLabelListSum = new List<string>();
        private List<float> lstQtySum = new List<float>();
        private List<string> lstArticleListSum = new List<string>();   //2023-01-13 일괄스캔한 Article

        private int DeleteGridData2Count = 0; //2021-12-01 일괄스캔을 다시하는 경우 기존꺼 삭제하고 다시 Insert하기 위해 추가

        int InspectQty = 0; //2021-12-02 포장 팝업 저장 시 데이터 
        int QtyperBox = 0;  //2021-12-02 포장 팝업 저장 시 데이터
        double LastQty = 0;    //2022-02-24 라벨이 마지막인지 아닌지 확인하는 변수
        double UnderSumQty = 0;     //2021-12-03 하위품의 사용량을 알기 위해 추가
        double UnderRealSumQty = 0; //2021-12-03 실제로 사용되는 하위품의 수량의 합
        double UnderRealUseSumQty = 0; //2021-12-03

        private string JaturiLOTID = "";  //자투리로트번호 2025-02-04 KDH
        private string CuttingName = "";  //절단명 2025-02-04 KDH
        private string CuttingWeight = "";  //중량    2025-02-04 KDH
        private string JaturiLOC = "";  //자투리 위치 2025-02-04 KDH

        // 불량 리스트
        Dictionary<string, frm_tprc_Work_Defect_U_CodeView> dicDefect = new Dictionary<string, frm_tprc_Work_Defect_U_CodeView>();
        // 불량 리스트를 행 순서에 맞게 모은 dic
        Dictionary<int, Dictionary<string, frm_tprc_Work_Defect_U_CodeView>> outerdicDefect = new Dictionary<int, Dictionary<string, frm_tprc_Work_Defect_U_CodeView>>();

        public frm_tprc_Work_U()
        {
            InitializeComponent();
        }

        public frm_tprc_Work_U(string JobID, string strProcessID, string StartSaveLabelID, string WorkStartDate, string WorkStartTime, string DayOrNightID, List<string> listChildLabelID, List<string> listChildArticle, List<string> listChildArticleID) //2021-11-30 하위품 라벨 리스트 추가
        {
            InitializeComponent();
            updateJobID = JobID;
            m_ProcessID = strProcessID;
            m_LabelID = StartSaveLabelID;
            m_WorkStartDate = WorkStartDate;
            m_WorkStartTime = WorkStartTime;
            Wh_Ar_DayOrNightID = DayOrNightID;

            ListChildLabelID = listChildLabelID; //2021-11-30 하위품 라벨 리스트 추가
            ListChildArticle = listChildArticle; //2021-11-30 하위품 품번 리스트 추가
            ListChildArticleID = listChildArticleID; //2022-05-18 하위품 ArticleID 리스트 추가(원자재 예외출고에서 사용하기 위해)
 
        }
        
        private void FormLoading()
        {
            m_MachineName = Frm_tprc_Main.g_tBase.Machine;
            m_MachineID = Frm_tprc_Main.g_tBase.MachineID;

            FormInit();

            txtMachine.Text = m_MachineName;
            txtCycleTime.Text = stringFormatN0(m_CycleTime);
        }

        private void frm_tprc_Work_U_Load(object sender, EventArgs e)
        {
            FormLoading();
        }

        private void FormInit()
        {
            InitPanel();
            InitGridData1();

            //절단이 아닌 경우만
            if (m_ProcessID != "0401") 
            {
                InitGridData2();
            }
            else
            {
                InitGridData2ByCutting();
            }


            SetFormDataClear();

            Form_Activate();

            LogData.LogSave(this.GetType().Name, "S"); //2022-06-22 사용시간(로드, 닫기)

        }

        #region InitPanel : Dock → Fill

        private void InitPanel()
        {
            
            tlpForm.Dock = DockStyle.Fill;
            tlpForm.Margin = new Padding(1, 1, 1, 1);

            foreach (Control control in tlpForm.Controls)
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
                                        foreach (Control ctl in c.Controls)
                                        {
                                            ctl.Dock = DockStyle.Fill;
                                            ctl.Margin = new Padding(1, 1, 1, 1);
                                            foreach (Control ct in ctl.Controls)
                                            {
                                                ct.Dock = DockStyle.Fill;
                                                ct.Margin = new Padding(1, 1, 1, 1);
                                                foreach (Control cl in ct.Controls)
                                                {
                                                    cl.Dock = DockStyle.Fill;
                                                    cl.Margin = new Padding(1, 1, 1, 1);

                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

        }

        #endregion

        private void cmdExit_Click(object sender, EventArgs e)
        {
            LogData.LogSave(this.GetType().Name, "S"); //2022-06-22 사용시간(로드, 닫기)
            Frm_tprc_Main.list_g_tsplit.Clear();
            this.Close();
        }

        private void InitGridData1()
        {
            int i = 0;
            GridData1.Columns.Clear();
            GridData1.ColumnCount = 2;
            // Set the Colums Hearder Names
            GridData1.Columns[i].Name = "RowSeq";
            GridData1.Columns[i].HeaderText = "No";
            //GridData1.Columns[i].Width = 30;
            GridData1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            GridData1.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            GridData1.Columns[i].ReadOnly = true;
            GridData1.Columns[i++].Visible = true;

            GridData1.Columns[i].Name = "LotID";
            GridData1.Columns[i].HeaderText = "LotID";
            //GridData1.Columns[i].Width = 140;
            GridData1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            GridData1.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            GridData1.Columns[i].ReadOnly = true;
            GridData1.Columns[i++].Visible = true;

            GridData1.Font = new Font("맑은 고딕", 15);//, FontStyle.Bold);
            GridData1.RowTemplate.Height = 30;
            GridData1.ColumnHeadersHeight = 37;
            GridData1.ScrollBars = ScrollBars.Both;
            GridData1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            GridData1.MultiSelect = false;
            GridData1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            GridData1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(234, 234, 234);
            GridData1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            GridData1.ReadOnly = true;

            foreach (DataGridViewColumn col in GridData1.Columns)
            {
                col.DataPropertyName = col.Name;
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            return;
        }

        private void InitGridData2()
        {
            int i = 0;
            GridData2.Columns.Clear();
            GridData2.ColumnCount = 22;

            // Set the Colums Hearder Names
            GridData2.Columns[i].Name = "RowSeq";
            GridData2.Columns[i].HeaderText = "No";
            //GridData2.Columns[i].Width = 30;
            GridData2.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            GridData2.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            GridData2.Columns[i].ReadOnly = true;
            GridData2.Columns[i++].Visible = true;

            GridData2.Columns[i].Name = "InstID";
            GridData2.Columns[i].HeaderText = "InstID";
            GridData2.Columns[i].Width = 80;
            GridData2.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            GridData2.Columns[i].ReadOnly = true;
            GridData2.Columns[i++].Visible = false;

            GridData2.Columns[i].Name = "DetSeq";
            GridData2.Columns[i].HeaderText = "DetSeq";
            GridData2.Columns[i].Width = 80;
            GridData2.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            GridData2.Columns[i].ReadOnly = true;
            GridData2.Columns[i++].Visible = false;

            GridData2.Columns[i].Name = "ChildSeq";
            GridData2.Columns[i].HeaderText = "ChildSeq";
            GridData2.Columns[i].Width = 80;
            GridData2.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            GridData2.Columns[i].ReadOnly = true;
            GridData2.Columns[i++].Visible = false;

            GridData2.Columns[i].Name = "ChildArticleID";
            GridData2.Columns[i].HeaderText = "ChildArticleID";
            GridData2.Columns[i].Width = 80;
            GridData2.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            GridData2.Columns[i].ReadOnly = true;
            GridData2.Columns[i++].Visible = false;

            GridData2.Columns[i].Name = "Article";
            GridData2.Columns[i].HeaderText = "품명";
            //GridData2.Columns[i].Width = 120;
            GridData2.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            GridData2.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            GridData2.Columns[i].ReadOnly = true;
            GridData2.Columns[i++].Visible = false;

            GridData2.Columns[i].Name = "BuyerArticle";
            GridData2.Columns[i].HeaderText = "품번";
            //GridData2.Columns[i].Width = 120;
            GridData2.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            GridData2.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            GridData2.Columns[i].ReadOnly = true;
            GridData2.Columns[i++].Visible = true;

            GridData2.Columns[i].Name = "BarCode";
            GridData2.Columns[i].HeaderText = "바코드";
            //GridData2.Columns[i].Width = 120;
            GridData2.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            GridData2.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            GridData2.Columns[i].ReadOnly = true;
            GridData2.Columns[i++].Visible = true;

            GridData2.Columns[i].Name = "ScanExceptYN";
            GridData2.Columns[i].HeaderText = "체크";
            //GridData2.Columns[i].Width = 80;
            GridData2.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            GridData2.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            GridData2.Columns[i].ReadOnly = true;
            GridData2.Columns[i++].Visible = false;

            GridData2.Columns[i].Name = "LabelGubun";
            GridData2.Columns[i].HeaderText = "LabelGubun";
            GridData2.Columns[i].Width = 80;
            GridData2.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            GridData2.Columns[i].ReadOnly = true;
            GridData2.Columns[i++].Visible = false;

            GridData2.Columns[i].Name = "Flag";
            GridData2.Columns[i].HeaderText = "Flag";
            GridData2.Columns[i].Width = 80;
            GridData2.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            GridData2.Columns[i].ReadOnly = true;
            GridData2.Columns[i++].Visible = false;

            GridData2.Columns[i].Name = "ScanExceptYN1";
            GridData2.Columns[i].HeaderText = "예외";
            GridData2.Columns[i].Width = 80;
            GridData2.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            GridData2.Columns[i].ReadOnly = true;
            GridData2.Columns[i++].Visible = false;

            GridData2.Columns[i].Name = "RemainQty";
            GridData2.Columns[i].HeaderText = "전체 재고량";
            GridData2.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            //GridData2.Columns[i].Width = 100;
            GridData2.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            GridData2.Columns[i].ReadOnly = true;
            GridData2.Columns[i++].Visible = ConvertDouble(Frm_tprc_Main.g_tBase.sInstDetSeq) == 1 ? true : false;

            GridData2.Columns[i].Name = "LocRemainQty";
            GridData2.Columns[i].HeaderText = "라벨 재고량";
            //GridData2.Columns[i].Width = 80;
            GridData2.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            GridData2.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            GridData2.Columns[i].ReadOnly = true;
            GridData2.Columns[i++].Visible = true;

            GridData2.Columns[i].Name = "UnitClss";
            GridData2.Columns[i].HeaderText = "하위품단위";
            //GridData2.Columns[i].Width = 80;
            GridData2.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            GridData2.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            GridData2.Columns[i].ReadOnly = true;
            GridData2.Columns[i++].Visible = false;

            GridData2.Columns[i].Name = "UnitClssName";
            GridData2.Columns[i].HeaderText = "재고단위";
            //GridData2.Columns[i].Width = 80;
            GridData2.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            GridData2.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            GridData2.Columns[i].ReadOnly = true;
            GridData2.Columns[i++].Visible = true;

            GridData2.Columns[i].Name = "ReqQty";
            GridData2.Columns[i].HeaderText = "소모량";
            //GridData2.Columns[i].Width = 40;
            GridData2.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            GridData2.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            GridData2.Columns[i].ReadOnly = true;
            GridData2.Columns[i++].Visible = true;

            GridData2.Columns[i].Name = "ProdCapa";
            GridData2.Columns[i].HeaderText = "생산가능량";
            //GridData2.Columns[i].Width = 80;
            GridData2.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            GridData2.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            GridData2.Columns[i].ReadOnly = true;
            GridData2.Columns[i++].Visible = true;

            GridData2.Columns[i].Name = "ProdUnitClss";
            GridData2.Columns[i].HeaderText = "생산단위";
            //GridData2.Columns[i].Width = 80;
            GridData2.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            GridData2.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            GridData2.Columns[i].ReadOnly = true;
            GridData2.Columns[i++].Visible = false;

            GridData2.Columns[i].Name = "ProdUnitClssName";
            GridData2.Columns[i].HeaderText = "생산단위";
            //GridData2.Columns[i].Width = 80;
            GridData2.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            GridData2.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            GridData2.Columns[i].ReadOnly = true;
            GridData2.Columns[i++].Visible = true;

            GridData2.Columns[i].Name = "EffectDate";
            GridData2.Columns[i].HeaderText = "유효기간";
            //GridData2.Columns[i].Width = 80;
            GridData2.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            GridData2.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            GridData2.Columns[i].ReadOnly = true;
            GridData2.Columns[i++].Visible = false;

            GridData2.Columns[i].Name = "ChildUseQty";
            GridData2.Columns[i].HeaderText = "사용량";
            //GridData2.Columns[i].Width = 80;
            GridData2.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            GridData2.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            GridData2.Columns[i].ReadOnly = true;
            GridData2.Columns[i++].Visible = false;

            GridData2.Font = new Font("맑은 고딕", 15);//, FontStyle.Bold);
            GridData2.ColumnHeadersDefaultCellStyle.Font = new Font("맑은 고딕", 12);//, FontStyle.Bold);
            GridData2.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            GridData2.RowTemplate.Height = 30;
            GridData2.ColumnHeadersHeight = 35;
            GridData2.ScrollBars = ScrollBars.Both;
            GridData2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            GridData2.MultiSelect = false;
            GridData2.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            GridData2.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(234, 234, 234);
            GridData2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            GridData2.ReadOnly = true;

            foreach (DataGridViewColumn col in GridData2.Columns)
            {
                col.DataPropertyName = col.Name;
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            return;

        }

        private void InitGridData2ByCutting()
        {
            int i = 0;
            GridData2.Columns.Clear();
            GridData2.ColumnCount = 10;

            // Set the Colums Hearder Names
            GridData2.Columns[i].Name = "RowSeq";
            GridData2.Columns[i].HeaderText = "No";
            GridData2.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            GridData2.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            GridData2.Columns[i].ReadOnly = true;
            GridData2.Columns[i++].Visible = true;

            GridData2.Columns[i].Name = "Article";
            GridData2.Columns[i].HeaderText = "품명";
            GridData2.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            GridData2.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            GridData2.Columns[i].ReadOnly = true;
            GridData2.Columns[i++].Visible = true;

            GridData2.Columns[i].Name = "InstQty";
            GridData2.Columns[i].HeaderText = "지시수량";
            GridData2.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            GridData2.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            GridData2.Columns[i].ReadOnly = true;
            GridData2.Columns[i++].Visible = true;

            GridData2.Columns[i].Name = "WorkQty";
            GridData2.Columns[i].HeaderText = "생산수량";
            GridData2.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            GridData2.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            GridData2.Columns[i].ReadOnly = true;
            GridData2.Columns[i++].Visible = true;

            GridData2.Columns[i].Name = "DefectQty";
            GridData2.Columns[i].HeaderText = "불량수량";
            GridData2.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            GridData2.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            GridData2.Columns[i].ReadOnly = true;
            GridData2.Columns[i++].Visible = true;

            GridData2.Columns[i].Name = "PreWorkQty";
            GridData2.Columns[i].HeaderText = "이전작업수량";
            GridData2.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            GridData2.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            GridData2.Columns[i].ReadOnly = true;
            GridData2.Columns[i++].Visible = false;

            GridData2.Columns[i].Name = "UnitClssName";
            GridData2.Columns[i].HeaderText = "단위";
            GridData2.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            GridData2.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            GridData2.Columns[i].ReadOnly = true;
            GridData2.Columns[i++].Visible = true;

            GridData2.Columns[i].Name = "ArticleID";
            GridData2.Columns[i].HeaderText = "ArticleID";
            GridData2.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            GridData2.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            GridData2.Columns[i].ReadOnly = true;
            GridData2.Columns[i++].Visible = false;

            GridData2.Columns[i].Name = "UnitClss";
            GridData2.Columns[i].HeaderText = "UnitClssID";
            GridData2.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            GridData2.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            GridData2.Columns[i].ReadOnly = true;
            GridData2.Columns[i++].Visible = false;

            GridData2.Columns[i].Name = "PLPDSInstSeq";
            GridData2.Columns[i].HeaderText = "PLPDSInstSeq";
            GridData2.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            GridData2.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            GridData2.Columns[i].ReadOnly = true;
            GridData2.Columns[i++].Visible = false;

            GridData2.Font = new Font("맑은 고딕", 15);//, FontStyle.Bold);
            GridData2.ColumnHeadersDefaultCellStyle.Font = new Font("맑은 고딕", 12);//, FontStyle.Bold);
            GridData2.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            GridData2.RowTemplate.Height = 30;
            GridData2.ColumnHeadersHeight = 35;
            GridData2.ScrollBars = ScrollBars.Both;
            GridData2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            GridData2.MultiSelect = false;
            GridData2.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            GridData2.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(234, 234, 234);
            GridData2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            GridData2.ReadOnly = true;

            foreach (DataGridViewColumn col in GridData2.Columns)
            {
                col.DataPropertyName = col.Name;
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            return;

        }

        #region 해당 작업 건이 이미 등록된 건이 아닌지 체크 하기.

        private bool CheckAlreadyWorkIn()
        {
            bool flag = false;

            try
            {
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Clear();

                sqlParameter.Add("JobID", ConvertDouble(updateJobID));

                DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_prdWork_CheckAlreadyWorkIn", sqlParameter, false);

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
                        Message[0] = "[작업 중복 등록 오류]";
                        Message[1] = "해당 작업 건은 " + DatePickerFormat(dr["WorkEndDate"].ToString()) + " " + DateTimeFormat(dr["WorkEndTime"].ToString()) + "에 이미 실적이 등록 되었습니다.\r\n작업실적을 종료합니다.";
                        WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                    }
                }
            }
            catch (Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox("중복 체크 구문 오류 [ CheckAlreadyWorkIn ] + \r\n" + ex.Message, "저장 전 체크 오류", 0, 1);
                return false;
            }
            DataStore.Instance.CloseConnection(); //2021-09-23 DB 커넥트 연결 해제
            return flag;
        }

        #endregion

        #region 해당 작업 건을 취소를 했는 지 안했는 지 체크하기
        private bool CheckAlreadyWorkOut()
        {
            bool flag = false;

            try
            {
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Clear();

                sqlParameter.Add("JobID", ConvertDouble(updateJobID));

                DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_prdWork_CheckAlreadyWorkOut", sqlParameter, false);

                if (dt != null
                    && dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];

                    if (dt.Columns.Count == 1
                        && dr["Result"].ToString().ToUpper().Equals("SUCCESS"))
                    {
                        flag = true;
                    }
                    else if (dr["Result"].ToString().ToUpper().Equals("FAIL")) //old : dt.Columns.Count > 1 2021-08-20
                    {                       
                        Message[0] = "[작업 등록 오류]";
                        Message[1] = "해당 작업 건은 취소 되었습니다. 확인 후 다시 진행해주세요.";
                        WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                    }
                }
            }
            catch (Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox("중복 체크 구문 오류 [ CheckAlreadyWorkOut ] + \r\n" + ex.Message, "저장 전 체크 오류", 0, 1);
                return false;
            }
            DataStore.Instance.CloseConnection(); //2021-09-23 DB 커넥트 연결 해제
            return flag;
        }
        #endregion

        #region 하나의 라벨로 동시에 여러 작업을 등록했을 경우 해당 라벨의 마이너스 재고 막기

        private bool CheckLOTIDQty(string LOTID, string Qty, string ArticleID)
        {
            bool flag = false;

            try
            {

                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Clear();

                sqlParameter.Add("LOTID", LOTID);
                sqlParameter.Add("ArticleID", ArticleID); //2022-12-01 하나의 라벨로 계속 진행할 경우를 위해 추가
                sqlParameter.Add("Qty", Qty);

                DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_prdWork_CheckLOTIDQty", sqlParameter, false);

                if (dt != null
                    && dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];

                    if (dt.Columns.Count == 1
                        && dr["Result"].ToString().ToUpper().Equals("SUCCESS"))
                    {
                        flag = true;
                    }
                    else if (dr["Result"].ToString().ToUpper().Equals("FAIL")) //old : dt.Columns.Count > 1 2021-08-20
                    {
                        Message[0] = "[작업 등록 오류]";
                        Message[1] = "작업중인 라벨의 재고 변동이 있습니다. \r\n 화면을 닫은 후 다시 진행해주세요.";
                        WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                    }
                }

            }
            catch (Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox("수량 체크 구문 오류 [ CheckLOTIDQty ] + \r\n" + ex.Message, "저장 전 체크 오류", 0, 1);
                return false;
            }
            DataStore.Instance.CloseConnection(); //2021-09-23 DB 커넥트 연결 해제
            return flag;
        }

        //절단인 경우 원자재라벨 재고가 변경되었을 경우 확인하는 함수 2025-02-03
        private bool CheckLOTIDQty_By_Cutting()
        {
            bool flag = false;

            try
            {

                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Clear();

                sqlParameter.Add("LOTID", txtPreInsertLabelBarCode.Text.ToString());
                sqlParameter.Add("ArticleID", ListChildArticleID[0].ToString()); //2022-12-01 하나의 라벨로 계속 진행할 경우를 위해 추가

                DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_prdWork_CheckLOTIDQty_By_Cutting", sqlParameter, false);

                if (dt != null
                    && dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];

                    if (dt.Columns.Count == 1
                        && dr["Result"].ToString().ToUpper().Equals("SUCCESS"))
                    {
                        flag = true;
                    }
                    else if (dr["Result"].ToString().ToUpper().Equals("FAIL")) //old : dt.Columns.Count > 1 2021-08-20
                    {
                        Message[0] = "[작업 등록 오류]";
                        Message[1] = "작업중인 라벨의 재고 변동이 있습니다. \r\n 해당 화면을 닫고 원자재 라벨 재고를 확인해 주세요.";
                        WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                    }
                }

            }
            catch (Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox("수량 체크 구문 오류 [ CheckLOTIDQty ] + \r\n" + ex.Message, "저장 전 체크 오류", 0, 1);
                return false;
            }
            DataStore.Instance.CloseConnection(); //2021-09-23 DB 커넥트 연결 해제
            return flag;
        }

        #endregion

        #region 시간 체크 하기(같은 시간이면 저장이 되지 않게 막기)
        private bool CheckTime()
        {
            bool flag = false;

            try
            {
                if(dtStartTime.Value.ToString("HHmmss") == dtEndTime.Value.ToString("HHmmss"))
                {
                    Message[0] = "[작업 등록 오류]";
                    Message[1] = "해당 작업의 시작시간과 종료시간이 같습니다.";
                    WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                }
                else
                {
                    flag = true;
                }
            }
            catch (Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox("중복 체크 구문 오류 [ CheckToday ] + \r\n" + ex.Message, "저장 전 체크 오류", 0, 1);
                return false;
            }

            return flag;
        }
        #endregion

        #region 공정별로 하위품 투입 예외 관리가 가능하여 공정별로 다를 경우 처리하기 위해 생성
        private void ProcessMtrExceptYN()
        {
            try
            {
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();

                sqlParameter.Add("InstID", Frm_tprc_Main.g_tBase.sInstID);
                sqlParameter.Add("InstDetSeq", ConvertInt(Frm_tprc_Main.g_tBase.sInstDetSeq));

                DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_PlanInput_sPlanInputDetArticle_ChildMtrExceptYN", sqlParameter, false); //xp_prdWork_sChildArticleForScan_GLS_20210412

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    m_PCMtrExceptYN = dr["MtrExceptYN"].ToString();
                }
            }
            catch (Exception excpt)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message), "[오류]", 0, 1);
            }
        }
        #endregion

        //중간저장
        private void btnMiddleSave_Click(object sender, EventArgs e)
        {
            // 해당 작업 건이 이미 등록된 건이 아닌지 체크하기
            if (CheckAlreadyWorkIn() == false)
            {
                Frm_tprc_Main.list_g_tsplit.Clear();
                this.Close();
                return;
            }

            //해당 작업 건을 취소를 했는 지 안했는 지 체크하기 2021-08-20
            if (CheckAlreadyWorkOut() == false)
            {
                Frm_tprc_Main.list_g_tsplit.Clear();
                this.Close();
                return;
            }

            //시작 시간과 종료 시간을 비교하여 같으면 저장 안되게 여기서 체크 2021-08-20 
            if (CheckTime() == false)
            {
                return;
            }

            if (CheckLOTIDQty_By_Cutting())
            {
                string PopUpArticle = "";
                string PopUpWorkQty = "";
                string PopUpDefectQty = "";
                string PopUpPreWorkQty = "";
                Dictionary<string, frm_tprc_Work_Defect_U_CodeView> PopUpDicDefect = new Dictionary<string, frm_tprc_Work_Defect_U_CodeView>();

                //생산 품명 리스트에 입력
                for (int i = 0; i < GridData2.Rows.Count; i++)
                {
                    PopUpArticle = GridData2.Rows[i].Cells["Article"].Value.ToString();
                    PopUpWorkQty = GridData2.Rows[i].Cells["WorkQty"].Value.ToString();
                    PopUpDefectQty = GridData2.Rows[i].Cells["DefectQty"].Value.ToString();
                    PopUpPreWorkQty = GridData2.Rows[i].Cells["PreWorkQty"].Value.ToString();

                    //값이 있는지 없는지 확인
                    if (outerdicDefect.ContainsKey(i))
                    {
                        PopUpDicDefect = outerdicDefect[i];
                    }
                    else
                    {
                        PopUpDicDefect = dicDefect;
                    }

                    //절단인 경우 2025-01-31
                    BoChang.PopUp.Frm_PopUp_Work_Save_U FPWSU = new BoChang.PopUp.Frm_PopUp_Work_Save_U(PopUpArticle, PopUpWorkQty, PopUpDefectQty, PopUpPreWorkQty, PopUpDicDefect);
                    DialogResult FPWSUResult = FPWSU.ShowDialog();
                    //취소인 경우 빠져 나옴
                    if (FPWSUResult == DialogResult.Cancel)
                    {
                        return;
                    }

                    //저장인 경우
                    GridData2.Rows[i].Cells["WorkQty"].Value = FPWSU.PopUpWorkQty;
                    GridData2.Rows[i].Cells["DefectQty"].Value = FPWSU.PopUpDefectQty;
                    txtDefectQty.Text = (Lib.ConvertInt(txtDefectQty.Text) + Lib.ConvertInt(FPWSU.PopUpDefectQty)).ToString();

                    if (outerdicDefect.ContainsKey(i))
                    {
                        outerdicDefect[i] = FPWSU.PopUpdicDefect;
                    }
                    else
                    {
                        outerdicDefect.Add(i, FPWSU.PopUpdicDefect);
                    }
                }

                //마지막까지 저장한 경우 저장 함수
                SaveDataByCutting("N");
            }
        }


        private void cmdSave_Click(object sender, EventArgs e)
        {
            // 해당 작업 건이 이미 등록된 건이 아닌지 체크하기
            if (CheckAlreadyWorkIn() == false)
            {
                Frm_tprc_Main.list_g_tsplit.Clear();
                this.Close();
                return;
            }

            //해당 작업 건을 취소를 했는 지 안했는 지 체크하기 2021-08-20
            if (CheckAlreadyWorkOut() == false)
            {
                Frm_tprc_Main.list_g_tsplit.Clear();
                this.Close();
                return;
            }

            //시작 시간과 종료 시간을 비교하여 같으면 저장 안되게 여기서 체크 2021-08-20 
            if (CheckTime() == false)
            {
                return;
            }

            if (m_ProcessID != "0401") 
            {
                //2022-06-02 재고 확인용
                lstLabelQtyList.Clear();
                lstLOTIDQtyList.Clear();
                lstArticleIDList.Clear();
                for (int i = 0; i < GridData2.Rows.Count; i++)
                {
                    lstLabelQtyList.Add(GridData2.Rows[i].Cells["BarCode"].Value.ToString());
                    lstLOTIDQtyList.Add(GridData2.Rows[i].Cells["LocRemainQty"].Value.ToString());
                    lstArticleIDList.Add(GridData2.Rows[i].Cells["ChildArticleID"].Value.ToString());
                }

                //2022-06-09
                ProcessMtrExceptYN();

                //2022-12-29
                if ((m_MtrExceptYN.Equals("N") && m_PCMtrExceptYN.Equals("")) || m_PCMtrExceptYN.Equals("N"))
                {
                    //2022-06-02 하나의 라벨로 동시에 여러 작업을 할 경우에 라벨 재고 확인
                    for (int i = 0; i < lstLabelQtyList.Count; i++)
                    {
                        if (lstLabelQtyList[i].ToString() != "")
                        {
                            if (CheckLOTIDQty(lstLabelQtyList[i], lstLOTIDQtyList[i], lstArticleIDList[i]) == false)
                            {
                                lstLOTIDQtyList.Clear();
                                lstLabelQtyList.Clear();
                                lstArticleIDList.Clear();
                                Frm_tprc_Main.list_g_tsplit.Clear();
                                this.Close();
                                return;
                            }
                        }
                    }
                }

                // 2021-05-06 퇴사하기전에 남겨놓은 작업이 있는 경우를 때문에 작업자가 퇴사,근속 확인 후에 넘어가도록 추가, 
                // 똑같은 사람이 여러번 입사와 퇴사를 할수 있어 PersonID로 구분
                string Person = "";
                string[] PersonResign = new string[2];
                string sql = "select EndDate from mt_Person where Name = '" + txtNowWorker.Text + "' and PersonID = '" + Frm_tprc_Main.g_tBase.PersonID + "'";
                PersonResign = DataStore.Instance.ExecuteQuery(sql, false);
                Person = PersonResign[1];

                if (Person != null && Person != "")
                {
                    WizCommon.Popup.MyMessageBox.ShowBox("작업자 : " + txtNowWorker.Text + ", 퇴사자 입니다. 작업취소 후 작업자를 다시 선택해 주세요", "[저장 전 오류]", 0, 1);
                    return;
                }
                m_MindouProdCapa = double.Parse(String.Format("{0:n0}", m_MindouProdCapa)); //2021-04-09 소수점 버림으로써 생산가능량에서 소수점버린 것과 같이 비교하기 위해 여기서도 소수점 버린것으로 비교
                                                                                            //2021-12-01 생산가능량을 하위품이 여러개인 경우 최소값을 가져가게 수정함 m_douProdCapa -> m_MindouProdCapa
                if (m_MindouProdCapa != 0 || m_MindouProdCapa == 0)   // 생산가능량 : m_douProdCapa 
                {
                    //2025-03-12 무조건 1개 되도록 수정
                    if ((m_MtrExceptYN.Equals("N") && m_PCMtrExceptYN.Equals("")) || m_PCMtrExceptYN.Equals("N"))
                    {
                        if (txtMindouProdCapa.Text != "")
                        {
                            if(Lib.GetDouble(txtWorkQty.Text) > 1) //(Lib.GetDouble(txtWorkQty.Text) > Lib.GetDouble(txtMindouProdCapa.Text))
                            {
                                // 내 순수 작업물량이 생산가능량 보다 크다면, 막아야 한다.
                                Message[0] = "[작업수량]";
                                Message[1] = "작업수량이 1보다 더 큽니다." +
                                                "생산실적 저장을 중단합니다.";
                                WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 3, 1);
                                return;
                            }
                        }
                    }
                }

                bool SaveStart_OK = true;
                double LabelPaper_Qty = 0;              // 라벨에 찍혀나올 qty 값.
                double LabelPaper_Count = 0;            // 라벨 발행 부수.
                double LabelPaper_OneMoreQty = 0;       // 한장 더 이 수만큼 뽑거나  / or / 이 수만큼 split에 담아두거나.
                string Split_GBN = string.Empty;

                SaveStart_OK = false;


                string AllQty = txtWorkQty.Text.ToString();
                string StandardQty = txtWorkQty.Text.ToString(); // 2025-02-05 무조건 라벨 하나로 처리하기 위해 작업수량과 같은 값이 입력되게 수정 txtLotProdQty.Text.ToString();
                string BringRemainQty = txtRemainAdd.Text;
                string MyQty = txtWorkQty.Text.ToString();  

                double d_AllQty = 0;
                double d_StandardQty = 0;
                double d_BringRemainQty = 0;
                double d_MyQty = 0;



                Double.TryParse(AllQty, out d_AllQty);
                Double.TryParse(StandardQty, out d_StandardQty);
                Double.TryParse(BringRemainQty, out d_BringRemainQty);
                Double.TryParse(MyQty, out d_MyQty);

                double d_DefectQty = ConvertDouble(txtDefectQty.Text); //생산불량 
                                                                       //double i_DefectQty = ConvertDouble(DefectQty.ToString()); //검사포장불량
                                                                       // d

                if (d_AllQty == 0)
                {
                    Message[0] = "[작업수량]";
                    Message[1] = "작업수량이 입력되지 않았습니다. \r\n" +
                                    "라벨발행을 중단합니다.";
                    WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 3, 1);
                    return;
                }
                if (d_AllQty < d_DefectQty)
                {
                    Message[0] = "[작업수량]";
                    Message[1] = "작업수량(" + d_AllQty + ")이 불량수량(" + d_DefectQty + ")보다 작습니다. \r\n" +
                                    "라벨발행을 중단합니다.";
                    WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 3, 1);
                    return;
                }
                if (d_AllQty - d_DefectQty == 0)
                {
                    Message[0] = "[작업수량]";
                    Message[1] = "불량(" + d_DefectQty + ")을 제외한 작업 수량(" + d_AllQty + ")이 0개 입니다. \r\n" +
                                    "라벨발행을 중단합니다.";
                    WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 3, 1);
                    return;
                }
                else if (d_StandardQty == 0)
                {
                    Message[0] = "[기준수량]";
                    Message[1] = "생산박스 당 수량이 입력되지 않았습니다. \r\n" +
                                "라벨발행을 중단합니다.";
                    WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 3, 1);
                    return;
                }

                bool PopUp_HeaderMessage = false; // true 라벨 발행 안 함 2022-09-14

                // 첫 공정으로서 > 전표발행을 담당하는 공정이라면,
                // 라벨발행 유형 타입을 설정합니다.
                frm_PopUp_ChoiceLabelPrintType ChoiceType = new frm_PopUp_ChoiceLabelPrintType(d_AllQty - d_DefectQty, d_StandardQty, PopUp_HeaderMessage, txtInUnitClss.Text);
                ChoiceType.Owner = this;
                ChoiceType.WriteTextEvent += ChoiceType_WriteTextEvent;
                ChoiceType.ShowDialog();

                void ChoiceType_WriteTextEvent(string Message, double Qty, double PrintCount, double RemainOneMoreQty)
                {
                    if (Message == "Cancel")
                    {
                        return;
                    }
                    else
                    {
                        Split_GBN = Message;
                        LabelPaper_Qty = Qty;
                        LabelPaper_Count = PrintCount;
                        LabelPaper_OneMoreQty = RemainOneMoreQty;
                        SaveStart_OK = true;
                    }
                }

                InspectQty = Lib.ConvertInt(txtWorkQty.Text);// - Lib.ConvertInt(txtBoxQty.Text); //2022-01-24 작업수량(불량 포함 안함)

                if (SaveStart_OK == true)
                {
                    Save_Function(Split_GBN, LabelPaper_Qty, LabelPaper_Count, LabelPaper_OneMoreQty, QtyperBox);
                }
                else
                {
                    return;
                }
            }
            else
            {

                if (CheckLOTIDQty_By_Cutting())
                {
                    string PopUpArticle = "";
                    string PopUpWorkQty = "";
                    string PopUpDefectQty = "";
                    string PopUpPreWorkQty = "";
                    Dictionary<string, frm_tprc_Work_Defect_U_CodeView> PopUpDicDefect = new Dictionary<string, frm_tprc_Work_Defect_U_CodeView>();

                    //생산 품명 리스트에 입력
                    for (int i = 0; i < GridData2.Rows.Count; i++)
                    {
                        PopUpArticle = GridData2.Rows[i].Cells["Article"].Value.ToString();
                        PopUpWorkQty = GridData2.Rows[i].Cells["WorkQty"].Value.ToString();
                        PopUpDefectQty = GridData2.Rows[i].Cells["DefectQty"].Value.ToString();
                        PopUpPreWorkQty = GridData2.Rows[i].Cells["PreWorkQty"].Value.ToString();

                        //값이 있는지 없는지 확인
                        if (outerdicDefect.ContainsKey(i))
                        {
                            PopUpDicDefect = outerdicDefect[i];
                        }
                        else
                        {
                            PopUpDicDefect = dicDefect;
                        }

                        //절단인 경우 2025-01-31
                        BoChang.PopUp.Frm_PopUp_Work_Save_U FPWSU = new BoChang.PopUp.Frm_PopUp_Work_Save_U(PopUpArticle, PopUpWorkQty, PopUpDefectQty, PopUpPreWorkQty, PopUpDicDefect);
                        DialogResult FPWSUResult = FPWSU.ShowDialog();
                        //취소인 경우 빠져 나옴
                        if (FPWSUResult == DialogResult.Cancel)
                        {
                            return;
                        }

                        //저장인 경우
                        GridData2.Rows[i].Cells["WorkQty"].Value = FPWSU.PopUpWorkQty;
                        GridData2.Rows[i].Cells["DefectQty"].Value = FPWSU.PopUpDefectQty;
                        txtDefectQty.Text = (Lib.ConvertInt(txtDefectQty.Text) + Lib.ConvertInt(FPWSU.PopUpDefectQty)).ToString();

                        if (outerdicDefect.ContainsKey(i))
                        {
                            outerdicDefect[i] = FPWSU.PopUpdicDefect;
                        }
                        else
                        {
                            outerdicDefect.Add(i, FPWSU.PopUpdicDefect);
                        }
                    }

                    //마지막까지 저장한 경우 저장 함수
                    SaveDataByCutting("Y");
                }
            }

        }

        //자투리 생성
        private void btnJaturi_Click(object sender, EventArgs e)
        {

            //절단인 경우 2025-01-31
            BoChang.PopUp.Frm_PopUp_Work_Jaturi_Save_U FPWJSU = new BoChang.PopUp.Frm_PopUp_Work_Jaturi_Save_U(txtPreInsertLabelBarCode.Text);
            DialogResult FPWJSUResult = FPWJSU.ShowDialog();
            //취소인 경우 빠져 나옴
            if (FPWJSUResult == DialogResult.Cancel)
            {
                return;
            }

            JaturiLOTID = FPWJSU.JaturiLOTID;       //자투리 로트번호     
            CuttingName = FPWJSU.CuttingName;       //절단명
            CuttingWeight = FPWJSU.CuttingWeight;   //중량
            JaturiLOC = FPWJSU.JaturiLOC;           //자투리 위치

            btnJaturi.Text = "자투리\r\n생성(O)";

        }

        #region 만약에 같은 시간, 공정, 호기로 작업한게 있다면.. 막기

        private bool CheckIsSameWorkTime()
        {
            bool flag = false;

            try
            {
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Clear();

                sqlParameter.Add("sProcessID", Frm_tprc_Main.g_tBase.ProcessID);
                sqlParameter.Add("sMachineID", Frm_tprc_Main.g_tBase.MachineID);
                sqlParameter.Add("WorkStartDate", mtb_From.Text.Replace("-", ""));
                sqlParameter.Add("WorkEndDate", mtb_To.Text.Replace("-", ""));
                sqlParameter.Add("WorkStartTime", dtStartTime.Value.ToString("HHmmss"));
                sqlParameter.Add("WorkEndTime", dtEndTime.Value.ToString("HHmmss"));

                //DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_prdWork_CheckIsSameWorkTime", sqlParameter, false); //2021-05-18
                DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_WizWork_CheckWorkTime", sqlParameter, false); 
                if (dt != null
                    && dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];

                    if (dt.Columns.Count == 1
                        && dr["Msg"].ToString().ToUpper().Equals(""))
                    {

                        flag = true;
                    }
                    else if (dr["Msg"].ToString().ToUpper().Equals("이미 동일 시간대에 생산실적이 있습니다."))  //else if (dt.Columns.Count > 1) 2021-05-18 시간대가 겹치면 안들어가도록 수정
                    {
                        // 예시 : 
                        // CNC 공정 1호기에 이미 같은 일자 및 시간으로 무작업이 등록되어 있습니다.

                        Message[0] = "[작업 중복 등록 오류]";
                        Message[1] = dr["Msg"].ToString();
                        //Message[1] = dr["Process"].ToString() + " 공정 " + dr["MachineNo"].ToString() + "에 같은 일자 및 시간으로\r\n이미 작업이 등록되어 있습니다.";
                        //Message[1] += "\r\n[" + dr["Process"].ToString() + " / " + dr["MachineNo"].ToString() + " / " + dr["Name"].ToString() + "]";
                        WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                    }
                }
            }
            catch (Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox("중복 체크 구문 오류 [ CheckIsSameWorkTime ] + \r\n" + ex.Message, "중복 체크 오류", 0, 1);
                return false;
            }
            DataStore.Instance.CloseConnection(); //2021-09-23 DB 커넥트 연결 해제
            return flag;
        }

        #endregion


        #region 실제 주요 저장 구문. (Save_Function)
        // 저장 _ 실제 주요 로직 구문.
        private void Save_Function(string Split_GBN, double LabelPaper_Qty, 
                                   double LabelPaper_Count, double LabelPaper_OneMoreQty, double QtyperBox)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                string sWDNO = "";
                string sWDID = "";
                float sWDQty = 0;
                int iCnt = 0;
                int nColorRow = 0;

                float StartTime = 0;
                float EndTime = 0;
                bool Success = false;
                float sLogID = 0;

                string FacilityCollectQty = "";
                string LotTotalQty = "";
                string CycleTime = "";

                double SumQty = double.Parse(InspectQty.ToString()); //+ double.Parse(Lib.GetDouble(txtDefectQty.Text).ToString()); //2021-11-23 불량 + 생산수량

                var UseChildQty = new Dictionary<string, double>();     //하위품이 몇개 인지 판단하기 위한 Dic
                var UseRealChildQty = new Dictionary<string, double>(); //하위품의 사용량을 판단하기 위한 Dic

                //2021-12-03 하위품 사용량 계산하기
                if (GridData2.RowCount > 0)
                {               
                    double PackQty3;
                    PackQty3 = double.Parse(InspectQty.ToString()); //+ double.Parse(Lib.GetDouble(txtDefectQty.Text).ToString()); //2021-02-18 불량 수량도 빠지게 하기 위해 더함

                    UnderSumQty = 0; //2021-12-03 하위품의 사용량을 알기 위해 추가
                    UnderRealSumQty = 0; //2021-12-03 실제로 사용되는 하위품의 수량의 합
                    UnderRealUseSumQty = 0;

                    //2022-06-09
                    //m_MtrExceptYN.Equals("Y") && m_PCMtrExceptYN.Equals("")) || m_PCMtrExceptYN.Equals("Y")
                    //if (m_MtrExceptYN == "Y")
                    if((m_MtrExceptYN.Equals("Y") && m_PCMtrExceptYN.Equals("")) || m_PCMtrExceptYN.Equals("Y"))
                    {
                        for (int i = 0; i < GridData2.Rows.Count; i++)
                        {
                            if (UseChildQty.ContainsKey(GridData2.Rows[i].Cells["ChildArticleID"].Value.ToString().Trim()))
                            {
                                UseChildQty[GridData2.Rows[i].Cells["ChildArticleID"].Value.ToString().Trim()] += Lib.ConvertDouble(txtWorkQty.Text.ToString()) * Lib.ConvertDouble(GridData2.Rows[i].Cells["ReqQty"].Value.ToString());
                                UseRealChildQty[GridData2.Rows[i].Cells["ChildArticleID"].Value.ToString().Trim()] += Lib.ConvertDouble(txtWorkQty.Text.ToString()) * Lib.ConvertDouble(GridData2.Rows[i].Cells["ReqQty"].Value.ToString());

                                if (UseChildQty[GridData2.Rows[i].Cells["ChildArticleID"].Value.ToString().Trim()] > PackQty3 * Lib.ConvertDouble(GridData2.Rows[i].Cells["ReqQty"].Value.ToString())) //2021-12-07 사용할 만큼의 하위품만 넣기 위해 조건 추가
                                {
                                    UseChildQty[GridData2.Rows[i].Cells["ChildArticleID"].Value.ToString().Trim()] = PackQty3 * Lib.ConvertDouble(GridData2.Rows[i].Cells["ReqQty"].Value.ToString());
                                    UseRealChildQty[GridData2.Rows[i].Cells["ChildArticleID"].Value.ToString().Trim()] = PackQty3 * Lib.ConvertDouble(GridData2.Rows[i].Cells["ReqQty"].Value.ToString());
                                }
                            }
                            else
                            {
                                UseChildQty.Add(GridData2.Rows[i].Cells["ChildArticleID"].Value.ToString().Trim(), Lib.ConvertDouble(txtWorkQty.Text.ToString()) * Lib.ConvertDouble(GridData2.Rows[i].Cells["ReqQty"].Value.ToString()));
                                UseRealChildQty.Add(GridData2.Rows[i].Cells["ChildArticleID"].Value.ToString().Trim(), Lib.ConvertDouble(txtWorkQty.Text.ToString()) * Lib.ConvertDouble(GridData2.Rows[i].Cells["ReqQty"].Value.ToString()));
                                
                                if (UseChildQty[GridData2.Rows[i].Cells["ChildArticleID"].Value.ToString().Trim()] > PackQty3 * Lib.ConvertDouble(GridData2.Rows[i].Cells["ReqQty"].Value.ToString())) //2021-12-07 사용할 만큼의 하위품만 넣기 위해 조건 추가
                                {
                                    UseChildQty[GridData2.Rows[i].Cells["ChildArticleID"].Value.ToString().Trim()] = PackQty3 * Lib.ConvertDouble(GridData2.Rows[i].Cells["ReqQty"].Value.ToString());
                                    UseRealChildQty[GridData2.Rows[i].Cells["ChildArticleID"].Value.ToString().Trim()] = PackQty3 * Lib.ConvertDouble(GridData2.Rows[i].Cells["ReqQty"].Value.ToString());
                                }
                            }
                        }


                        //2021-12-03 실제로 필요한 하위품 수량의 합을 구함
                        for (int i = 0; i < UseChildQty.Count; i++)
                        {
                            UnderRealSumQty += PackQty3 * Lib.ConvertDouble(GridData2.Rows[i].Cells["ReqQty"].Value.ToString());
                        }

                        //2021-12-03 실제로 필요한 하위품 수량을 가지고 라벨당 소모량을 계산하도록 만듬
                        for (int i = 0; i < GridData2.Rows.Count; i++)
                        {
                            //하위품이 맞는 지 확인
                            if (UseChildQty.ContainsKey(GridData2.Rows[i].Cells["ChildArticleID"].Value.ToString().Trim()))
                            {
                                if (UnderRealSumQty != UnderSumQty && UseChildQty[GridData2.Rows[i].Cells["ChildArticleID"].Value.ToString().Trim()] != 0)
                                {
                                    if (UseChildQty[GridData2.Rows[i].Cells["ChildArticleID"].Value.ToString().Trim()] > Lib.ConvertDouble(txtWorkQty.Text.ToString()) * Lib.ConvertDouble(GridData2.Rows[i].Cells["ReqQty"].Value.ToString())) //2021-12-07 dic에 넣어둔 실제로 사용할 하위품 수량이 현재 라벨의 사용 가능량보다 크다면 실제로 사용할 만큼만 넣기
                                    {
                                        GridData2.Rows[i].Cells["ChildUseQty"].Value = PackQty3 * Lib.ConvertDouble(GridData2.Rows[i].Cells["ReqQty"].Value.ToString()); //UseChildQty[GridData2.Rows[i].Cells["ChildArticleID"].Value.ToString().Trim()] * Lib.ConvertDouble(GridData2.Rows[i].Cells["ReqQty"].Value.ToString());
                                        UnderSumQty += Lib.ConvertDouble(GridData2.Rows[i].Cells["ChildUseQty"].Value.ToString()); //2021-12-03 해당 하위품의 수량이 생산 수량만큼 들어갔는 지 확인 하기 위해 추가 
                                        if (UnderRealSumQty < UnderSumQty) //2021-12-06 많을 경우 필요한 수량만큼 넣기
                                        {
                                            GridData2.Rows[i].Cells["ChildUseQty"].Value = (UnderRealSumQty - UnderRealUseSumQty);
                                        }
                                        UnderRealUseSumQty += Lib.ConvertDouble(GridData2.Rows[i].Cells["ChildUseQty"].Value.ToString()); //2021-12-06 필요한 수량을 찾기 위해 추가
                                        UseChildQty[GridData2.Rows[i].Cells["ChildArticleID"].Value.ToString().Trim()] -= Lib.ConvertDouble(GridData2.Rows[i].Cells["ChildUseQty"].Value.ToString());
                                    }
                                    else
                                    {
                                        GridData2.Rows[i].Cells["ChildUseQty"].Value = PackQty3 * Lib.ConvertDouble(GridData2.Rows[i].Cells["ReqQty"].Value.ToString());
                                        UnderSumQty += Lib.ConvertDouble(GridData2.Rows[i].Cells["ChildUseQty"].Value.ToString()); //2021-12-03 해당 하위품의 수량이 생산 수량만큼 들어갔는 지 확인 하기 위해 추가 
                                        if (UnderRealSumQty < UnderSumQty) //2021-12-06 많을 경우 필요한 수량만큼 넣기
                                        {
                                            GridData2.Rows[i].Cells["ChildUseQty"].Value = (UnderRealSumQty - UnderRealUseSumQty);
                                        }
                                        UnderRealUseSumQty += Lib.ConvertDouble(GridData2.Rows[i].Cells["ChildUseQty"].Value.ToString()); //2021-12-06 필요한 수량을 찾기 위해 추가
                                        UseChildQty[GridData2.Rows[i].Cells["ChildArticleID"].Value.ToString().Trim()] -= Lib.ConvertDouble(GridData2.Rows[i].Cells["ChildUseQty"].Value.ToString());
                                    }
                                }
                            }
                        }
                    }
                    else 
                    {  
                        for (int i = 0; i < GridData2.Rows.Count; i++)
                        {
                            if (UseChildQty.ContainsKey(GridData2.Rows[i].Cells["ChildArticleID"].Value.ToString().Trim()))
                            {
                                if (GridData2.Rows[i].Cells["ScanExceptYN"].Value.ToString() == "N")
                                {
                                    UseChildQty[GridData2.Rows[i].Cells["ChildArticleID"].Value.ToString().Trim()] += Lib.ConvertDouble(GridData2.Rows[i].Cells["ProdCapa"].Value.ToString()) * Lib.ConvertDouble(GridData2.Rows[i].Cells["ReqQty"].Value.ToString());
                                    UseRealChildQty[GridData2.Rows[i].Cells["ChildArticleID"].Value.ToString().Trim()] += Lib.ConvertDouble(GridData2.Rows[i].Cells["ProdCapa"].Value.ToString()) * Lib.ConvertDouble(GridData2.Rows[i].Cells["ReqQty"].Value.ToString());

                                    if (UseChildQty[GridData2.Rows[i].Cells["ChildArticleID"].Value.ToString().Trim()] > PackQty3 * Lib.ConvertDouble(GridData2.Rows[i].Cells["ReqQty"].Value.ToString())) //2021-12-07 사용할 만큼의 하위품만 넣기 위해 조건 추가
                                    {
                                        UseChildQty[GridData2.Rows[i].Cells["ChildArticleID"].Value.ToString().Trim()] = PackQty3 * Lib.ConvertDouble(GridData2.Rows[i].Cells["ReqQty"].Value.ToString());
                                        UseRealChildQty[GridData2.Rows[i].Cells["ChildArticleID"].Value.ToString().Trim()] = PackQty3 * Lib.ConvertDouble(GridData2.Rows[i].Cells["ReqQty"].Value.ToString());
                                    }
                                }
                                else
                                {
                                    UseChildQty[GridData2.Rows[i].Cells["ChildArticleID"].Value.ToString().Trim()] += Lib.ConvertDouble(txtWorkQty.Text.ToString()) * Lib.ConvertDouble(GridData2.Rows[i].Cells["ReqQty"].Value.ToString());
                                    UseRealChildQty[GridData2.Rows[i].Cells["ChildArticleID"].Value.ToString().Trim()] += Lib.ConvertDouble(txtWorkQty.Text.ToString()) * Lib.ConvertDouble(GridData2.Rows[i].Cells["ReqQty"].Value.ToString());

                                    if (UseChildQty[GridData2.Rows[i].Cells["ChildArticleID"].Value.ToString().Trim()] > PackQty3 * Lib.ConvertDouble(GridData2.Rows[i].Cells["ReqQty"].Value.ToString())) //2021-12-07 사용할 만큼의 하위품만 넣기 위해 조건 추가
                                    {
                                        UseChildQty[GridData2.Rows[i].Cells["ChildArticleID"].Value.ToString().Trim()] = PackQty3 * Lib.ConvertDouble(GridData2.Rows[i].Cells["ReqQty"].Value.ToString());
                                        UseRealChildQty[GridData2.Rows[i].Cells["ChildArticleID"].Value.ToString().Trim()] = PackQty3 * Lib.ConvertDouble(GridData2.Rows[i].Cells["ReqQty"].Value.ToString());
                                    }
                                }
                            }
                            else
                            {
                                if (GridData2.Rows[i].Cells["ScanExceptYN"].Value.ToString() == "N")
                                {
                                    UseChildQty.Add(GridData2.Rows[i].Cells["ChildArticleID"].Value.ToString().Trim(), Lib.ConvertDouble(GridData2.Rows[i].Cells["ProdCapa"].Value.ToString()) * Lib.ConvertDouble(GridData2.Rows[i].Cells["ReqQty"].Value.ToString()));
                                    UseRealChildQty.Add(GridData2.Rows[i].Cells["ChildArticleID"].Value.ToString().Trim(), Lib.ConvertDouble(GridData2.Rows[i].Cells["ProdCapa"].Value.ToString()) * Lib.ConvertDouble(GridData2.Rows[i].Cells["ReqQty"].Value.ToString()));
                                    if (UseChildQty[GridData2.Rows[i].Cells["ChildArticleID"].Value.ToString().Trim()] > PackQty3 * Lib.ConvertDouble(GridData2.Rows[i].Cells["ReqQty"].Value.ToString())) //2021-12-07 사용할 만큼의 하위품만 넣기 위해 조건 추가
                                    {
                                        UseChildQty[GridData2.Rows[i].Cells["ChildArticleID"].Value.ToString().Trim()] = PackQty3 * Lib.ConvertDouble(GridData2.Rows[i].Cells["ReqQty"].Value.ToString());
                                        UseRealChildQty[GridData2.Rows[i].Cells["ChildArticleID"].Value.ToString().Trim()] = PackQty3 * Lib.ConvertDouble(GridData2.Rows[i].Cells["ReqQty"].Value.ToString());
                                    }
                                }
                                else
                                {
                                    UseChildQty.Add(GridData2.Rows[i].Cells["ChildArticleID"].Value.ToString().Trim(), Lib.ConvertDouble(txtWorkQty.Text.ToString()) * Lib.ConvertDouble(GridData2.Rows[i].Cells["ReqQty"].Value.ToString()));
                                    UseRealChildQty.Add(GridData2.Rows[i].Cells["ChildArticleID"].Value.ToString().Trim(), Lib.ConvertDouble(txtWorkQty.Text.ToString()) * Lib.ConvertDouble(GridData2.Rows[i].Cells["ReqQty"].Value.ToString()));
                                    if (UseChildQty[GridData2.Rows[i].Cells["ChildArticleID"].Value.ToString().Trim()] > PackQty3 * Lib.ConvertDouble(GridData2.Rows[i].Cells["ReqQty"].Value.ToString())) //2021-12-07 사용할 만큼의 하위품만 넣기 위해 조건 추가
                                    {
                                        UseChildQty[GridData2.Rows[i].Cells["ChildArticleID"].Value.ToString().Trim()] = PackQty3 * Lib.ConvertDouble(GridData2.Rows[i].Cells["ReqQty"].Value.ToString());
                                        UseRealChildQty[GridData2.Rows[i].Cells["ChildArticleID"].Value.ToString().Trim()] = PackQty3 * Lib.ConvertDouble(GridData2.Rows[i].Cells["ReqQty"].Value.ToString());
                                    }
                                }
                            }
                        }

                    
                        //2021-12-03 실제로 필요한 하위품 수량의 합을 구함
                        for (int i = 0; i < UseChildQty.Count; i++)
                        {
                            UnderRealSumQty += PackQty3 * Lib.ConvertDouble(GridData2.Rows[i].Cells["ReqQty"].Value.ToString());
                        }

                        //2021-12-03 실제로 필요한 하위품 수량을 가지고 라벨당 소모량을 계산하도록 만듬
                        for (int i = 0; i < GridData2.Rows.Count; i++)
                        {
                            //하위품이 맞는 지 확인
                            if (UseChildQty.ContainsKey(GridData2.Rows[i].Cells["ChildArticleID"].Value.ToString().Trim()))
                            {
                                if (GridData2.Rows[i].Cells["ScanExceptYN"].Value.ToString() == "N") 
                                {
                                    if (UnderRealSumQty != UnderSumQty && UseChildQty[GridData2.Rows[i].Cells["ChildArticleID"].Value.ToString().Trim()] != 0)
                                    {
                                        if (UseChildQty[GridData2.Rows[i].Cells["ChildArticleID"].Value.ToString()] > Lib.ConvertDouble(GridData2.Rows[i].Cells["ProdCapa"].Value.ToString()) * Lib.ConvertDouble(GridData2.Rows[i].Cells["ReqQty"].Value.ToString())) //2021-12-07 dic에 넣어둔 실제로 사용할 하위품 수량이 현재 라벨의 사용 가능량보다 크다면 실제로 사용할 만큼만 넣기
                                        {
                                            GridData2.Rows[i].Cells["ChildUseQty"].Value = Lib.ConvertDouble(GridData2.Rows[i].Cells["ProdCapa"].Value.ToString()) * Lib.ConvertDouble(GridData2.Rows[i].Cells["ReqQty"].Value.ToString());  //PackQty3 * Lib.ConvertDouble(GridData2.Rows[i].Cells["ReqQty"].Value.ToString());
                                            UnderSumQty += Lib.ConvertDouble(GridData2.Rows[i].Cells["ChildUseQty"].Value.ToString()); //2021-12-03 해당 하위품의 수량이 생산 수량만큼 들어갔는 지 확인 하기 위해 추가 
                                            if (UnderRealSumQty < UnderSumQty) //2021-12-06 많을 경우 필요한 수량만큼 넣기
                                            {
                                                GridData2.Rows[i].Cells["ChildUseQty"].Value = (UnderRealSumQty - UnderRealUseSumQty);
                                            }
                                            UnderRealUseSumQty += Lib.ConvertDouble(GridData2.Rows[i].Cells["ChildUseQty"].Value.ToString()); //2021-12-06 필요한 수량을 찾기 위해 추가
                                            UseChildQty[GridData2.Rows[i].Cells["ChildArticleID"].Value.ToString()] -= Lib.ConvertDouble(GridData2.Rows[i].Cells["ChildUseQty"].Value.ToString());
                                        }
                                        else
                                        {
                                            GridData2.Rows[i].Cells["ChildUseQty"].Value = UseChildQty[GridData2.Rows[i].Cells["ChildArticleID"].Value.ToString().Trim()]; //PackQty3 * Lib.ConvertDouble(GridData2.Rows[i].Cells["ReqQty"].Value.ToString());
                                            UnderSumQty += Lib.ConvertDouble(GridData2.Rows[i].Cells["ChildUseQty"].Value.ToString()); //2021-12-03 해당 하위품의 수량이 생산 수량만큼 들어갔는 지 확인 하기 위해 추가 
                                            if (UnderRealSumQty < UnderSumQty) //2021-12-06 많을 경우 필요한 수량만큼 넣기
                                            {
                                                GridData2.Rows[i].Cells["ChildUseQty"].Value = (UnderRealSumQty - UnderRealUseSumQty);
                                            }
                                            UnderRealUseSumQty += Lib.ConvertDouble(GridData2.Rows[i].Cells["ChildUseQty"].Value.ToString()); //2021-12-06 필요한 수량을 찾기 위해 추가
                                            UseChildQty[GridData2.Rows[i].Cells["ChildArticleID"].Value.ToString()] -= Lib.ConvertDouble(GridData2.Rows[i].Cells["ChildUseQty"].Value.ToString());
                                        }
                                    }
                                    else
                                    {
                                        GridData2.Rows[i].Cells["ChildUseQty"].Value = 0;
                                    }
                                }
                                else
                                {
                                    if (UnderRealSumQty != UnderSumQty && UseChildQty[GridData2.Rows[i].Cells["ChildArticleID"].Value.ToString().Trim()] != 0)
                                    {
                                        if (UseChildQty[GridData2.Rows[i].Cells["ChildArticleID"].Value.ToString()] > Lib.ConvertDouble(txtWorkQty.Text.ToString()) * Lib.ConvertDouble(GridData2.Rows[i].Cells["ReqQty"].Value.ToString())) //2021-12-07 dic에 넣어둔 실제로 사용할 하위품 수량이 현재 라벨의 사용 가능량보다 크다면 실제로 사용할 만큼만 넣기
                                        {
                                            GridData2.Rows[i].Cells["ChildUseQty"].Value = Lib.ConvertDouble(txtWorkQty.Text.ToString()) * Lib.ConvertDouble(GridData2.Rows[i].Cells["ReqQty"].Value.ToString());  //PackQty3 * Lib.ConvertDouble(GridData2.Rows[i].Cells["ReqQty"].Value.ToString());
                                            UnderSumQty += Lib.ConvertDouble(GridData2.Rows[i].Cells["ChildUseQty"].Value.ToString()); //2021-12-03 해당 하위품의 수량이 생산 수량만큼 들어갔는 지 확인 하기 위해 추가 
                                            if (UnderRealSumQty < UnderSumQty) //2021-12-06 많을 경우 필요한 수량만큼 넣기
                                            {
                                                GridData2.Rows[i].Cells["ChildUseQty"].Value = (UnderRealSumQty - UnderRealUseSumQty);
                                            }
                                            UnderRealUseSumQty += Lib.ConvertDouble(GridData2.Rows[i].Cells["ChildUseQty"].Value.ToString()); //2021-12-06 필요한 수량을 찾기 위해 추가
                                            UseChildQty[GridData2.Rows[i].Cells["ChildArticleID"].Value.ToString()] -= Lib.ConvertDouble(GridData2.Rows[i].Cells["ChildUseQty"].Value.ToString());
                                        }
                                        else
                                        {
                                            GridData2.Rows[i].Cells["ChildUseQty"].Value = UseChildQty[GridData2.Rows[i].Cells["ChildArticleID"].Value.ToString().Trim()]; //PackQty3 * Lib.ConvertDouble(GridData2.Rows[i].Cells["ReqQty"].Value.ToString());
                                            UnderSumQty += Lib.ConvertDouble(GridData2.Rows[i].Cells["ChildUseQty"].Value.ToString()); //2021-12-03 해당 하위품의 수량이 생산 수량만큼 들어갔는 지 확인 하기 위해 추가 
                                            if (UnderRealSumQty < UnderSumQty) //2021-12-06 많을 경우 필요한 수량만큼 넣기
                                            {
                                                GridData2.Rows[i].Cells["ChildUseQty"].Value = (UnderRealSumQty - UnderRealUseSumQty);
                                            }
                                            UnderRealUseSumQty += Lib.ConvertDouble(GridData2.Rows[i].Cells["ChildUseQty"].Value.ToString()); //2021-12-06 필요한 수량을 찾기 위해 추가
                                            UseChildQty[GridData2.Rows[i].Cells["ChildArticleID"].Value.ToString()] -= Lib.ConvertDouble(GridData2.Rows[i].Cells["ChildUseQty"].Value.ToString());
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                LabelPrintYN = "Y"; //2021-11-22 라벨발행은 안되고 Print에 저장만 함(영승공업, 덕우테크, HK스틸)

                ProdQty = (Lib.GetDouble(txtWorkQty.Text) - Lib.GetDouble(txtDefectQty.Text)).ToString(); //2022-02-24 생산수량 - 불량수량 // old : Lib.GetDouble(txtWorkQty.Text).ToString();    // 순수 내 작업물량.
                //StartTime = float.Parse(mtb_From.Text.Replace("-", "") + dtStartTime.Value.ToString("HHmmss"));
                //EndTime = float.Parse(mtb_To.Text.Replace("-", "") + dtEndTime.Value.ToString("HHmmss"));
                DateTime S_DT = DateTime.ParseExact(mtb_From.Text.Replace("-", "") + dtStartTime.Value.ToString("HHmmss"), "yyyyMMddHHmmss", null);
                DateTime E_DT = DateTime.ParseExact(mtb_To.Text.Replace("-", "") + dtEndTime.Value.ToString("HHmmss"), "yyyyMMddHHmmss", null);

                FacilityCollectQty = Lib.GetDouble(txtFacilityCollectQty.Text).ToString();      // 설비수집 수량
                LotTotalQty = Lib.GetDouble(txtTotalLabelQty.Text).ToString();                    // 로트별 총 수량
                CycleTime = Lib.GetDouble(txtCycleTime.Text).ToString();                      // CycleTime


                if (S_DT > E_DT)
                {
                    throw new Exception("시작시간이 종료시간보다 더 큽니다.");
                }
                if (Frm_tprc_Main.g_tBase.ProcessID == "" || Frm_tprc_Main.g_tBase.MachineID == "")
                {
                    throw new Exception("공정 또는 호기가 선택되지 않았습니다.");
                }
                if (Frm_tprc_Main.g_tBase.PersonID == "")//수정필요
                {
                    throw new Exception("작업자가 선택되지 않았습니다.");
                }
                if (ProdQty == "0")
                {
                    throw new Exception("작업수량이 입력되지 않았습니다.");
                }

                //'-------------------------------------------------------------------------------
                //'생산실적 잔량 초과 실적 등록 불가
                //'-------------------------------------------------------------------------------
                if (txtInUnitClss.Text == "EA") // 생산제품의 단위가 갯수(EA)로 세리고 있는 공정이라면
                {
                    if ((int)Lib.GetDouble(ProdQty) > (int)Lib.GetDouble(txtInstRemainQty.Text))
                    {
                        Message[0] = "[확인]";
                        Message[1] = "생산잔량: " + txtInstRemainQty.Text + "입니다. 초과된 생산 실적을 등록하시겠습니까?";
                        // 등록 불가 합니다. \r\n 계속 진행 하시겠습니까?";
                        if (WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 0) == DialogResult.No)
                        {
                            Cursor = Cursors.Default;
                            return;
                        }
                    }
                }
                else if (txtInUnitClss.Text == "M")
                {
                    if ((int)Lib.GetDouble(ProdQty) > (int)Lib.GetDouble(txtInstRemainQty.Text))
                    {
                        Message[0] = "[확인]";
                        Message[1] = "생산잔량: " + txtInstRemainQty.Text + "M입니다. 초과된 생산 실적을 등록하시겠습니까?";
                        // 등록 불가 합니다. \r\n 계속 진행 하시겠습니까?";
                        if (WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 0) == DialogResult.No)
                        {
                            Cursor = Cursors.Default;
                            return;
                        }
                    }
                }
                else                 // Gram이라 가정한다면.
                {
                    double douProdQty = 0;
                    double douInstRemainQty = 0;
                    douProdQty = Lib.GetDouble(ProdQty) / 1000;//kg단위로 변환
                    douInstRemainQty = Lib.GetDouble(txtInstRemainQty.Text);//kg단위
                    if (douProdQty > douInstRemainQty)
                    {
                        Message[0] = "[확인]";
                        Message[1] = "생산잔량: " + txtInstRemainQty.Text + "kg입니다. 초과된 생산 실적을 등록하시겠습니까?";
                        if (WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 0) == DialogResult.No)
                        {
                            Cursor = Cursors.Default;
                            return;
                        }
                    }
                }
                //18.06.18 주석 
                //txtInstRemainQty.Text = string.Format("{0:#,###}", float.Parse(Lib.CheckNum(InstQty.ToString())) - float.Parse(Lib.CheckNum(WorkQty.ToString())) - float.Parse(Lib.CheckNum(ProdQty)));
                //18.06.18 주석 


                //'-----------------------------------------------------------------------------------------
                //'탭/다이스 교환
                //'-----------------------------------------------------------------------------------------

                Sub_Ttd.TdChangeYN = "N";
                sWDNO = "";
                sWDID = "";

                int TWkRCon = 1;

                // 첫공정인지 아닌지 이걸로 판단할거야.
                // 가장 중요한 데이터 중 하나가 된듯..
                int InstDetSeq = 0;
                int.TryParse(Lib.GetDouble(txtInstDetSeq.Text).ToString(), out InstDetSeq);

                // 첫 공정이다.      // 총 도는 횟수는 라벨 페이퍼 카운트에 맞춰야 하고,
                TWkRCon = (int)LabelPaper_Count;

                if (TWkRCon == 0)
                {
                    TWkRCon = 1;
                }
                if ((Split_GBN == "YO" || Split_GBN == "YC") && (LabelPaper_OneMoreQty > 0))
                {
                    TWkRCon = TWkRCon + 1;
                }

                //'-------------------------------------------------------------------------------
                //'상위품 설정
                //'-------------------------------------------------------------------------------

                list_TWkResult = new List<Sub_TWkResult>();
                
                float WorkQty = 0;
                //float Lot_ProdQty = 0;

                float nCycleTime = 0;
                for (int i = 0; i < TWkRCon; i++)
                {
                    list_TWkResult.Add(new Sub_TWkResult());

                    list_TWkResult[i].JobID = float.Parse(updateJobID);
                    list_TWkResult[i].InstID = txtInstID.Text;                    
                    list_TWkResult[i].InstDetSeq = InstDetSeq;
                    list_TWkResult[i].LabelID = txtPreInsertLabelBarCode.Text;
                    list_TWkResult[i].LabelGubun = this.txtLabelGubun.Text;

                    list_TWkResult[i].ProcessID = Frm_tprc_Main.g_tBase.ProcessID;
                    list_TWkResult[i].MachineID = Frm_tprc_Main.g_tBase.MachineID;
                    list_TWkResult[i].ScanDate = mtb_From.Text.Replace("-", "");
                    list_TWkResult[i].ScanTime = dtStartTime.Value.ToString("HHmmss");

                    list_TWkResult[i].ArticleID = txtArticleID.Text;


                    // 작업수량 결정 구문 (wk_result 작업수량은 내 순수 작업수량에 맞춰야 한다.)
                    ////////////////////////////////
                    ///

                    // 첫 공정
                    if (TWkRCon == 1)
                    {
                        // Update값 물고난 뒤에 한번만 발행하면 되는 케이스.
                        if ((Split_GBN == "YO" || Split_GBN == "YC") && (LabelPaper_OneMoreQty > 0))
                        {
                            float.TryParse(LabelPaper_OneMoreQty.ToString(), out WorkQty);
                            list_TWkResult[i].SplitYNGBN = Split_GBN;
                        }
                        else
                        {
                            float.TryParse(LabelPaper_Qty.ToString(), out WorkQty);
                            list_TWkResult[i].SplitYNGBN = "NC";
                        }
                    }
                    else
                    {
                        if ((i + 1 == TWkRCon) && (Split_GBN == "YO" || Split_GBN == "YC") && (LabelPaper_OneMoreQty > 0))
                        {
                            float.TryParse(LabelPaper_OneMoreQty.ToString(), out WorkQty);
                            list_TWkResult[i].SplitYNGBN = Split_GBN;
                        }
                        else
                        {
                            float.TryParse(LabelPaper_Qty.ToString(), out WorkQty);
                            list_TWkResult[i].SplitYNGBN = "NC";
                        }                            
                    }

                    list_TWkResult[i].WorkQty = WorkQty;

                    // 작업수량 결정 구문 끝.
                    ////////////////////////////////
                    ///


                    float.TryParse(CycleTime, out nCycleTime);
                    list_TWkResult[i].CycleTime = nCycleTime;


                    // 2020.06.09 둘리
                    // 불량 → 그냥 실적 저장일때, 불량 갯수를 제외해줘야 한다
                    if (LabelPrintYN == "N"
                        && i == 0)
                    {
                        float DefectQty = 0;
                        float.TryParse(txtDefectQty.Text, out DefectQty);
                        list_TWkResult[i].WorkQty = WorkQty - DefectQty;
                        txtDefectQty.Text = string.Empty;
                    }

                    list_TWkResult[i].Comments = Frm_tprc_Main.g_tBase.ProcessID + "작업종료에 따른 데이터 저장(Insert)";
                    list_TWkResult[i].ReworkOldYN = "";
                    list_TWkResult[i].ReworkLinkProdID = "";

                    list_TWkResult[i].WorkStartDate = mtb_From.Text.Replace("-", "");
                    list_TWkResult[i].WorkStartTime = dtStartTime.Value.ToString("HHmmss");
                    list_TWkResult[i].WorkEndDate = mtb_To.Text.Replace("-", "");
                    list_TWkResult[i].WorkEndTime = dtEndTime.Value.ToString("HHmmss");
                    list_TWkResult[i].JobGbn = "1";

                    list_TWkResult[i].sNowReworkCode = "";
                    list_TWkResult[i].WDNO = sWDNO;
                    list_TWkResult[i].WDID = sWDID;
                    list_TWkResult[i].WDQty = sWDQty;
                    float.TryParse(m_LogID, out sLogID);
                    list_TWkResult[i].sLogID = sLogID;

                    list_TWkResult[i].s4MID = "";
                    list_TWkResult[i].DayOrNightID = Wh_Ar_DayOrNightID;
                    list_TWkResult[i].CreateUserID = Frm_tprc_Main.g_tBase.PersonID;
                    

                    //'------------------------------------------------------------------------------------------
                    list_TWkResult[i].sLastArticleYN = m_LastArticleYN;
                    list_TWkResult[i].ProdAutoInspectYN = m_ProdAutoInspectYN;
                    list_TWkResult[i].sOrderID = m_OrderID;
                    list_TWkResult[i].nOrderSeq = m_OrderSeq;                    
                                                           
                }

                iCnt = 0;

                //'-------------------------------------------------------------------------------
                //'하위품 설정
                //'-------------------------------------------------------------------------------

                nColorRow = GridData2.Rows.Count;
                if (nColorRow > 0)
                {
                    list_TWkResultArticleChild = new List<Sub_TWkResultArticleChild>();

                    for (int i = 0; i < nColorRow; i++)
                    {
                        DataGridViewRow row = GridData2.Rows[i];

                        list_TWkResultArticleChild.Add(new Sub_TWkResultArticleChild());

                        list_TWkResultArticleChild[i].JobID = 0;
                        list_TWkResultArticleChild[i].ChildLabelID = Lib.CheckNull(row.Cells["BarCode"].Value.ToString());
                        list_TWkResultArticleChild[i].ChildLabelGubun = Lib.CheckNull(row.Cells["LabelGubun"].Value.ToString());
                        list_TWkResultArticleChild[i].ChildArticleID = Lib.CheckNull(row.Cells["ChildArticleID"].Value.ToString().Trim());
                        list_TWkResultArticleChild[i].ReworkOldYN = "";
                        list_TWkResultArticleChild[i].ReworkLinkChildProdID = "";
                        list_TWkResultArticleChild[i].OutDate = DateTime.Now.ToString("yyyyMMdd");
                        list_TWkResultArticleChild[i].OutTime = DateTime.Today.ToString("HHmmss");
                        list_TWkResultArticleChild[i].Flag = Lib.CheckNull(row.Cells["Flag"].Value.ToString());
                        list_TWkResultArticleChild[i].CreateUserID = Frm_tprc_Main.g_tBase.PersonID;
                        list_TWkResultArticleChild[i].ChildUseQty = Lib.ConvertDouble(row.Cells["ChildUseQty"].Value.ToString());
                        list_TWkResultArticleChild[i].ReqQty = Lib.ConvertDouble(row.Cells["ReqQty"].Value.ToString()); //2021-12-07 소모량 추가

                        iCnt++;
                    }
                }

                //'-------------------------------------------------------------------------------
                //'불량입력화면에서 가져온 불량 수량 만큼의 정보에 데이타 설정
                //'-------------------------------------------------------------------------------

                if (Frm_tprc_Main.g_tBase.DefectCnt > 0)
                {
                    for (int i = 0; i < Frm_tprc_Main.g_tBase.DefectCnt; i++)
                    {
                        Frm_tprc_Main.list_g_tInsSub[i].BoxID = txtBoxID.Text;
                        Frm_tprc_Main.list_g_tInsSub[i].OrderID = m_OrderID;
                        Frm_tprc_Main.list_g_tInsSub[i].OrderSeq = m_OrderSeq;
                    }
                }

                //'--------------------------------------------------------------------------------
                //'   현재 진행하는 건이 첫 공정 이라면 공동이동전표 발행 
                //'--------------------------------------------------------------------------------
                // 첫 공정이다.

                int mInstDetSeq = 0;
                long nQtyPerBox = 0;
                list_TWkLabelPrint = new List<Sub_TWkLabelPrint>();

                //잔량 불러오기 라벨 건이 있다면, 그 라벨도 인쇄 될 수 있도록.
                //하위라벨이 C로 시작하는 경우 라벨 발행 안 함
                //첫공정인 경우(재관/용접)인 경우 라벨 발행 2025-03-10 KDH
                //!(txtPreInsertLabelBarCode.Text.ToString().Contains("C")) || Frm_tprc_Main.g_tBase.ProcessID == "2010"
                if (InstDetSeq == 1) 
                {
                    for (int i = 0; i < TWkRCon; i++)
                    {

                        list_TWkLabelPrint.Add(new Sub_TWkLabelPrint());

                        list_TWkLabelPrint[i].sLabelID = "";
                        list_TWkLabelPrint[i].sProcessID = Frm_tprc_Main.g_tBase.ProcessID;

                        list_TWkLabelPrint[i].sArticleID = txtArticleID.Text;
                        list_TWkLabelPrint[i].sLabelGubun = "7";


                        list_TWkLabelPrint[i].sPrintDate = mtb_From.Text.Replace("-", "");

                        list_TWkLabelPrint[i].sReprintDate = "";
                        list_TWkLabelPrint[i].nReprintQty = 0;
                        list_TWkLabelPrint[i].sInstID = txtInstID.Text;

                        int.TryParse(Lib.GetDouble(txtInstDetSeq.Text).ToString(), out mInstDetSeq);
                        list_TWkLabelPrint[i].nInstDetSeq = mInstDetSeq;
                        list_TWkLabelPrint[i].sOrderID = m_OrderID;

                        list_TWkLabelPrint[i].nPrintQty = 1;

                        if (TWkRCon == 1)
                        {
                            // Update값 물고난 뒤에 한번만 발행하면 되는 케이스.
                            if ((Split_GBN == "YO" || Split_GBN == "YC") && (LabelPaper_OneMoreQty > 0))
                            {
                                long.TryParse(LabelPaper_OneMoreQty.ToString(), out nQtyPerBox);
                                list_TWkLabelPrint[i].nQtyPerBox = nQtyPerBox;
                            }
                            else
                            {
                                long.TryParse(LabelPaper_Qty.ToString(), out nQtyPerBox);
                                list_TWkLabelPrint[i].nQtyPerBox = nQtyPerBox;
                            }
                        }
                        else
                        {
                            if ((i + 1 == TWkRCon) && (Split_GBN == "YC" || Split_GBN == "YO") && (LabelPaper_OneMoreQty > 0))
                            {
                                long.TryParse(LabelPaper_OneMoreQty.ToString(), out nQtyPerBox);
                                list_TWkLabelPrint[i].nQtyPerBox = nQtyPerBox;
                            }
                            else
                            {
                                long.TryParse(LabelPaper_Qty.ToString(), out nQtyPerBox);
                                list_TWkLabelPrint[i].nQtyPerBox = nQtyPerBox;
                            }
                        }

                        list_TWkLabelPrint[i].sCreateuserID = Frm_tprc_Main.g_tBase.PersonID;
                        list_TWkLabelPrint[i].sLastUpdateUserID = Frm_tprc_Main.g_tBase.PersonID;

                    }
                }

                //'-------------------------------------------------------------------------------
                //'생산실적  저장
                //'-------------------------------------------------------------------------------
                                                                 // 불량 + 생산수량 라벨 수   라벨 당 수량(나머지 포함 될 수 있음)   라벨 당 수량
                Success = AddNewWorkResult(iCnt, Frm_tprc_Main.g_tBase.DefectCnt, SumQty, LabelPaper_Count, LabelPaper_Qty, QtyperBox, UnderRealSumQty, UseChildQty.Count, UseRealChildQty); //
                if (Success)
                {
                    //첫 공정(DETSEQ가 1)   && MT_ARTICLE  LABELPRINTYN = Y && 1개 이상의 박스당 생산수 2022-09-14 라벨 밣행 선택화면을 수정하면서 조건 추가 Split_GBN == "YC"
                    if (((LabelPrintYN == "Y") && (Wh_Ar_LabelPrintYN == "Y") && (LabelPaper_Count > 0) && (Split_GBN == "YC"))) //첫번째면 무조건 라벨 발행 2023-06-21 || InstDetSeq == 1
                    {
                        if ((Split_GBN == "YC" && LabelPaper_OneMoreQty > 0))
                        {
                            PrintWorkCard((int)LabelPaper_Count + 1);
                            //Message[0] = "[저장 완료]";
                            //Message[1] = "저장이 완료되었습니다.";
                            //WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 3, 1);
                        }
                        else
                        {
                            PrintWorkCard((int)LabelPaper_Count);
                            //Message[0] = "[저장 완료]";
                            //Message[1] = "저장이 완료되었습니다.";
                            //WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 3, 1);
                        }
                    }
                    else
                    {
                        Message[0] = "[저장 완료]";
                        Message[1] = "저장이 완료되었습니다.";
                        WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 3, 1);
                    }

                    Frm_tprc_Main.list_g_tsplit.Clear();
                    SetFormDataClear();
                    cmdExit_Click(null, null);  // 나가.
                }
                else
                {
                    throw new Exception();
                }
                //'    '-----------------------------------------------------------------------------------------
                //     '저장된 결과 재 조회
                //'    '-----------------------------------------------------------------------------------------
                //FillGridData1();
                Cursor = Cursors.Default;
            }

            catch (Exception excpt)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의<cmdSave>\r\n{0}", excpt.Message), "[오류]", 0, 1);
                Cursor = Cursors.Default;
            }
        }

        #endregion


        //'생산 등록, 생산 하위품 등록, 생산 불량 등록한다
        private bool AddNewWorkResult(int nCnt, int nDefectCnt, double SumQty, double LabelPaper_Count, double LabelPaper_Qty, double QtyperBox, double UnderRealSumQty, double UseChildQtyCount, Dictionary<String, Double> UseRealChildQty)
        {
            List<WizCommon.Procedure> Prolist = new List<WizCommon.Procedure>();
            List<List<string>> ListProcedureName = new List<List<string>>();
            List<Dictionary<string, object>> ListParameter = new List<Dictionary<string, object>>();
            try
            {
                for (int i = 0; i < list_TWkResult.Count; i++)
                {
                    LastQty = i;//2021-12-07

                    //'*****************************************************************************************************
                    //'                  공정이동전표 등록
                    //'*****************************************************************************************************

                    if (list_TWkLabelPrint.Count > i)
                    {
                        if (list_TWkLabelPrint[i].sProcessID != "")
                        {
                            Dictionary<string, object> sqlParameter = new Dictionary<string, object>();

                            sqlParameter.Add("LabelID", list_TWkLabelPrint[i].sLabelID);
                            sqlParameter.Add("LabelGubun", list_TWkLabelPrint[i].sLabelGubun);
                            sqlParameter.Add("ProcessID", list_TWkLabelPrint[i].sProcessID);
                            sqlParameter.Add("ArticleID", list_TWkLabelPrint[i].sArticleID);
                            sqlParameter.Add("PrintDate", list_TWkLabelPrint[i].sPrintDate);

                            sqlParameter.Add("ReprintDate", list_TWkLabelPrint[i].sReprintDate);
                            sqlParameter.Add("ReprintQty", list_TWkLabelPrint[i].nReprintQty);
                            sqlParameter.Add("InstID", list_TWkLabelPrint[i].sInstID);
                            sqlParameter.Add("InstDetSeq", list_TWkLabelPrint[i].nInstDetSeq);
                            sqlParameter.Add("OrderID", list_TWkLabelPrint[i].sOrderID);

                            sqlParameter.Add("PrintQty", list_TWkLabelPrint[i].nPrintQty);
                            sqlParameter.Add("LabelPrintQty", 1);
                            sqlParameter.Add("nQtyPerBox", list_TWkLabelPrint[i].nQtyPerBox);
                            sqlParameter.Add("CreateUserID", list_TWkLabelPrint[i].sCreateuserID);

                            WizCommon.Procedure pro1 = new WizCommon.Procedure();
                            pro1.Name = "[xp_WizWork_iwkLabelPrint_C]";
                            pro1.OutputUseYN = "Y";
                            pro1.OutputName = "LabelID";
                            pro1.OutputLength = "20";

                            Prolist.Add(pro1);
                            ListParameter.Add(sqlParameter);
                        }
                    }

                    //'************************************************************************************************
                    //'                               상위품 생산 //xp_wkResult_iWkResult
                    //'************************************************************************************************
                    Dictionary<string, object> sqlParameter1 = new Dictionary<string, object>();
                    WizCommon.Procedure pro2 = new WizCommon.Procedure();

                    if (i == 0)
                    {
                        sqlParameter1.Add("JobID", list_TWkResult[i].JobID);

                        if (list_TWkLabelPrint.Count > i)
                        {
                            sqlParameter1.Add("LabelID", list_TWkLabelPrint[i].sLabelID);
                        }
                        else
                        {
                            sqlParameter1.Add("LabelID", list_TWkResult[i].LabelID);
                        }
                        sqlParameter1.Add("LabelGubun", list_TWkResult[i].LabelGubun);
                        sqlParameter1.Add("ScanDate", list_TWkResult[i].ScanDate);
                        sqlParameter1.Add("ScanTime", list_TWkResult[i].ScanTime);

                        sqlParameter1.Add("WorkStartDate", list_TWkResult[i].WorkStartDate);
                        sqlParameter1.Add("WorkStartTime", list_TWkResult[i].WorkStartTime);
                        sqlParameter1.Add("WorkEndDate", list_TWkResult[i].WorkEndDate);
                        sqlParameter1.Add("WorkEndTime", list_TWkResult[i].WorkEndTime);

                        sqlParameter1.Add("WorkQty", list_TWkResult[i].WorkQty);
                        sqlParameter1.Add("CycleTime", list_TWkResult[i].CycleTime);
                        sqlParameter1.Add("ProcessID", list_TWkResult[i].ProcessID);
                        sqlParameter1.Add("MachineID", list_TWkResult[i].MachineID);
                        sqlParameter1.Add("Comments", list_TWkResult[i].ProcessID + "작업종료에 따른 저장구문");

                        sqlParameter1.Add("SplitYNGBN", list_TWkResult[i].SplitYNGBN);
                        sqlParameter1.Add("UpdateUserID", list_TWkResult[i].CreateUserID);

                        //pro2.Name = "xp_wkResult_uWkResultOne";
                        pro2.Name = "xp_WizWork_uWkResultOne";//하이테크만 이렇게 사용 // 2020.02.21 둘리 : CycleTime 추가되도록 수정
                        pro2.OutputUseYN = "N";
                        pro2.OutputName = "JobID";
                        pro2.OutputLength = "20";
                    }
                    else
                    {
                        sqlParameter1.Add("JobID", 0);
                        sqlParameter1.Add("InstID", list_TWkResult[i].InstID);
                        sqlParameter1.Add("InstDetSeq", list_TWkResult[i].InstDetSeq);
                        if (list_TWkLabelPrint.Count > i)
                        {
                            sqlParameter1.Add("LabelID", list_TWkLabelPrint[i].sLabelID);
                        }
                        else
                        {
                            sqlParameter1.Add("LabelID", list_TWkResult[i].LabelID);
                        }

                        //sqlParameter1.Add("StartSaveLabelID", txtPreInsertLabelBarCode.Text); //2021-12-08 수정

                        sqlParameter1.Add("LabelGubun", list_TWkResult[i].LabelGubun);
                        sqlParameter1.Add("ProcessID", list_TWkResult[i].ProcessID);
                        sqlParameter1.Add("MachineID", list_TWkResult[i].MachineID);
                        sqlParameter1.Add("ScanDate", list_TWkResult[i].ScanDate);
                        sqlParameter1.Add("ScanTime", list_TWkResult[i].ScanTime);

                        sqlParameter1.Add("ArticleID", list_TWkResult[i].ArticleID);
                        sqlParameter1.Add("WorkQty", list_TWkResult[i].WorkQty);
                        sqlParameter1.Add("Comments", list_TWkResult[i].Comments);
                        sqlParameter1.Add("ReworkOldYN", list_TWkResult[i].ReworkOldYN);
                        sqlParameter1.Add("ReworkLinkProdID", list_TWkResult[i].ReworkLinkProdID);

                        sqlParameter1.Add("WorkStartDate", list_TWkResult[i].WorkStartDate);
                        sqlParameter1.Add("WorkStartTime", list_TWkResult[i].WorkStartTime);
                        sqlParameter1.Add("WorkEndDate", list_TWkResult[i].WorkEndDate);
                        sqlParameter1.Add("WorkEndTime", list_TWkResult[i].WorkEndTime);
                        sqlParameter1.Add("JobGbn", list_TWkResult[i].JobGbn);

                        sqlParameter1.Add("NoReworkCode", list_TWkResult[i].sNowReworkCode);
                        sqlParameter1.Add("WDNO", list_TWkResult[i].WDNO);
                        sqlParameter1.Add("WDID", list_TWkResult[i].WDID);
                        sqlParameter1.Add("WDQty", list_TWkResult[i].WDQty);
                        sqlParameter1.Add("LogID", list_TWkResult[i].sLogID);

                        sqlParameter1.Add("s4MID", list_TWkResult[i].s4MID);                        
                        sqlParameter1.Add("DayOrNightID", list_TWkResult[i].DayOrNightID);
                        sqlParameter1.Add("SplitYNGBN", list_TWkResult[i].SplitYNGBN);
                        sqlParameter1.Add("CycleTime", list_TWkResult[i].CycleTime);
                        sqlParameter1.Add("CreateUserID", list_TWkResult[i].CreateUserID);

                        pro2.Name = "xp_wkResult_iWkResult";
                        pro2.OutputUseYN = "Y";
                        pro2.OutputName = "JobID";
                        pro2.OutputLength = "20";
                    }

                    Prolist.Add(pro2);
                    ListParameter.Add(sqlParameter1);

                    //'****************************************************************************************************
                    //'정상작업의 경우    Sub_TWkResult.JobGbn = "1"
                    //'****************************************************************************************************
                    if (list_TWkResult[0].JobGbn == "1")
                    {
                        //'************************************************************************************************
                        //'                               하위품 스켄이력 등록
                        //'************************************************************************************************
                        
                        if (nCnt > 0)
                        {
                            for (int x = 0; x < UseRealChildQty.Count; x++) 
                            {
                                UseRealChildQty[GridData2.Rows[x].Cells["ChildArticleID"].Value.ToString().Trim()] = 0;
                            }

                            //double PackQty = 0;
                            //PackQty = Math.Truncate(UnderRealSumQty / list_TWkResult.Count); //2021-12-06 박스 당 실제로 들어가야 할 하위품 수량

                            for (int k = 0; k < nCnt; k++)
                            {

                                Dictionary<string, object> sqlParameter2 = new Dictionary<string, object>();

                                if (i == 0)
                                {
                                    sqlParameter2.Add("JobID", list_TWkResult[i].JobID);
                                }
                                else
                                {
                                    sqlParameter2.Add("JobID", 0);
                                }

                                //스캔 해야 되는 Article과 안 해도 되는 ArticleID 구분
                                if (list_TWkResultArticleChild[k].ChildLabelGubun != "") 
                                {
                                    sqlParameter2.Add("ChildLabelID", list_TWkResultArticleChild[k].ChildLabelID); //2021-12-06 수정
                                    sqlParameter2.Add("ChildLabelGubun", list_TWkResultArticleChild[k].ChildLabelGubun);
                                    sqlParameter2.Add("ChildArticleID", list_TWkResultArticleChild[k].ChildArticleID);
                                    sqlParameter2.Add("ReworkOldYN", list_TWkResultArticleChild[k].ReworkOldYN);
                                    sqlParameter2.Add("ReworkLinkChildProdID", list_TWkResultArticleChild[k].ReworkLinkChildProdID);
                                    sqlParameter2.Add("CreateUserID", list_TWkResultArticleChild[k].CreateUserID);

                                    if (i > 0 && lstLabelList.Count > 0)
                                    {
                                        sqlParameter2.Add("ChildUseQty", list_TWkResult[i].WorkQty);
                                        //sqlParameter1.Add("StartSaveLabelID", lstLabelList[0]); //2021-12-08 일괄스캔 시 오류가 나타나 선라벨이 들어가게 수정
                                    }
                                    else
                                    {
                                        if (UseRealChildQty.ContainsKey(GridData2.Rows[k].Cells["ChildArticleID"].Value.ToString().Trim()))
                                        {
                                            //2022-02-24 마지막에 불량수량도 포함해서 재고를 마이너스 하기 위해 조건 추가
                                            if (LastQty == list_TWkResult.Count - 1)
                                            {
                                                //2021-11-26 하위품 라벨에서 하나의 라벨이 생성될때 사용량을 추가하기 위해
                                                if (UseRealChildQty[GridData2.Rows[k].Cells["ChildArticleID"].Value.ToString().Trim()] != (LabelPaper_Qty + double.Parse(Lib.GetDouble(txtDefectQty.Text).ToString())) * list_TWkResultArticleChild[k].ReqQty) //생산량만큼 넣었는 지 확인
                                                {
                                                    if (list_TWkResultArticleChild[k].ChildUseQty >= (LabelPaper_Qty + double.Parse(Lib.GetDouble(txtDefectQty.Text).ToString())) * list_TWkResultArticleChild[k].ReqQty) //박스 당 수량 보다 많을 경우
                                                    {
                                                        //먼저 들어간 라벨수량이 있을 수 있으니 그 수량은 빼고 필요한 만큼 넣기
                                                        sqlParameter2.Add("ChildUseQty", (((LabelPaper_Qty + double.Parse(Lib.GetDouble(txtDefectQty.Text).ToString())) - UseRealChildQty[GridData2.Rows[k].Cells["ChildArticleID"].Value.ToString().Trim()]) * list_TWkResultArticleChild[k].ReqQty));
                                                        //원래 자기 수량에서 사용량을 빼서 재고를 다시 저장하기
                                                        list_TWkResultArticleChild[k].ChildUseQty = list_TWkResultArticleChild[k].ChildUseQty - ((((LabelPaper_Qty + double.Parse(Lib.GetDouble(txtDefectQty.Text).ToString())) * list_TWkResultArticleChild[k].ReqQty) - (UseRealChildQty[GridData2.Rows[k].Cells["ChildArticleID"].Value.ToString().Trim()])));
                                                        //다음에 필요한 수량 구하기 위해 사용한 라벨을 Dic에 넣기
                                                        UseRealChildQty[GridData2.Rows[k].Cells["ChildArticleID"].Value.ToString().Trim()] += ((((LabelPaper_Qty + double.Parse(Lib.GetDouble(txtDefectQty.Text).ToString())) * list_TWkResultArticleChild[k].ReqQty) - (UseRealChildQty[GridData2.Rows[k].Cells["ChildArticleID"].Value.ToString().Trim()])));
                                                    }
                                                    else
                                                    {   //박스 당 수량 보다 작은 경우, 필요한 수량보다 해당 라벨의 재고가 작은 경우
                                                        if (((((LabelPaper_Qty + double.Parse(Lib.GetDouble(txtDefectQty.Text).ToString())) * list_TWkResultArticleChild[k].ReqQty) - UseRealChildQty[GridData2.Rows[k].Cells["ChildArticleID"].Value.ToString().Trim()])) > list_TWkResultArticleChild[k].ChildUseQty)
                                                        {
                                                            //전부 소모하고 0으로 함
                                                            sqlParameter2.Add("ChildUseQty", (list_TWkResultArticleChild[k].ChildUseQty)); //* list_TWkResultArticleChild[k].ReqQty
                                                            UseRealChildQty[GridData2.Rows[k].Cells["ChildArticleID"].Value.ToString().Trim()] += list_TWkResultArticleChild[k].ChildUseQty;
                                                            list_TWkResultArticleChild[k].ChildUseQty = 0;
                                                        }
                                                        else
                                                        {
                                                            //필요한 수량보다 해당 라벨의 재고가 많은 경우, 라벨 당 수량에서 Dic에 있는 수량을 빼 필요한 수량만큼 넣기
                                                            sqlParameter2.Add("ChildUseQty", ((((LabelPaper_Qty + double.Parse(Lib.GetDouble(txtDefectQty.Text).ToString())) * list_TWkResultArticleChild[k].ReqQty) - UseRealChildQty[GridData2.Rows[k].Cells["ChildArticleID"].Value.ToString().Trim()]))); //* list_TWkResultArticleChild[k].ReqQty
                                                            list_TWkResultArticleChild[k].ChildUseQty = list_TWkResultArticleChild[k].ChildUseQty - ((((LabelPaper_Qty + double.Parse(Lib.GetDouble(txtDefectQty.Text).ToString())) * list_TWkResultArticleChild[k].ReqQty) - UseRealChildQty[GridData2.Rows[k].Cells["ChildArticleID"].Value.ToString().Trim()])); //* list_TWkResultArticleChild[k].ReqQty
                                                            UseRealChildQty[GridData2.Rows[k].Cells["ChildArticleID"].Value.ToString().Trim()] += ((((LabelPaper_Qty + double.Parse(Lib.GetDouble(txtDefectQty.Text).ToString())) * list_TWkResultArticleChild[k].ReqQty) - UseRealChildQty[GridData2.Rows[k].Cells["ChildArticleID"].Value.ToString().Trim()])); //* list_TWkResultArticleChild[k].ReqQty
                                                        }
                                                    }

                                                }
                                                else
                                                {
                                                    sqlParameter2.Add("ChildUseQty", 0);
                                                }
                                            }
                                            else
                                            {
                                                //2021-11-26 하위품 라벨에서 하나의 라벨이 생성될때 사용량을 추가하기 위해
                                                if (UseRealChildQty[GridData2.Rows[k].Cells["ChildArticleID"].Value.ToString().Trim()] != LabelPaper_Qty * list_TWkResultArticleChild[k].ReqQty) //생산량만큼 넣었는 지 확인
                                                {
                                                    if (list_TWkResultArticleChild[k].ChildUseQty >= LabelPaper_Qty * list_TWkResultArticleChild[k].ReqQty) //박스 당 수량 보다 많을 경우
                                                    {
                                                        //먼저 들어간 라벨수량이 있을 수 있으니 그 수량은 빼고 필요한 만큼 넣기
                                                        sqlParameter2.Add("ChildUseQty", ((LabelPaper_Qty - UseRealChildQty[GridData2.Rows[k].Cells["ChildArticleID"].Value.ToString().Trim()]) * list_TWkResultArticleChild[k].ReqQty));
                                                        //원래 자기 수량에서 사용량을 빼서 재고를 다시 저장하기
                                                        list_TWkResultArticleChild[k].ChildUseQty = list_TWkResultArticleChild[k].ChildUseQty - (((LabelPaper_Qty * list_TWkResultArticleChild[k].ReqQty) - (UseRealChildQty[GridData2.Rows[k].Cells["ChildArticleID"].Value.ToString().Trim()])));
                                                        //다음에 필요한 수량 구하기 위해 사용한 라벨을 Dic에 넣기
                                                        UseRealChildQty[GridData2.Rows[k].Cells["ChildArticleID"].Value.ToString().Trim()] += (((LabelPaper_Qty * list_TWkResultArticleChild[k].ReqQty) - (UseRealChildQty[GridData2.Rows[k].Cells["ChildArticleID"].Value.ToString().Trim()])));
                                                    }
                                                    else
                                                    {   //박스 당 수량 보다 작은 경우, 필요한 수량보다 해당 라벨의 재고가 작은 경우
                                                        if ((((LabelPaper_Qty * list_TWkResultArticleChild[k].ReqQty) - UseRealChildQty[GridData2.Rows[k].Cells["ChildArticleID"].Value.ToString().Trim()])) > list_TWkResultArticleChild[k].ChildUseQty)
                                                        {
                                                            //전부 소모하고 0으로 함
                                                            sqlParameter2.Add("ChildUseQty", (list_TWkResultArticleChild[k].ChildUseQty)); //* list_TWkResultArticleChild[k].ReqQty
                                                            UseRealChildQty[GridData2.Rows[k].Cells["ChildArticleID"].Value.ToString().Trim()] += list_TWkResultArticleChild[k].ChildUseQty;
                                                            list_TWkResultArticleChild[k].ChildUseQty = 0;
                                                        }
                                                        else
                                                        {
                                                            //필요한 수량보다 해당 라벨의 재고가 많은 경우, 라벨 당 수량에서 Dic에 있는 수량을 빼 필요한 수량만큼 넣기
                                                            sqlParameter2.Add("ChildUseQty", (((LabelPaper_Qty * list_TWkResultArticleChild[k].ReqQty) - UseRealChildQty[GridData2.Rows[k].Cells["ChildArticleID"].Value.ToString().Trim()]))); //* list_TWkResultArticleChild[k].ReqQty
                                                            list_TWkResultArticleChild[k].ChildUseQty = list_TWkResultArticleChild[k].ChildUseQty - (((LabelPaper_Qty * list_TWkResultArticleChild[k].ReqQty) - UseRealChildQty[GridData2.Rows[k].Cells["ChildArticleID"].Value.ToString().Trim()])); //* list_TWkResultArticleChild[k].ReqQty
                                                            UseRealChildQty[GridData2.Rows[k].Cells["ChildArticleID"].Value.ToString().Trim()] += (((LabelPaper_Qty * list_TWkResultArticleChild[k].ReqQty) - UseRealChildQty[GridData2.Rows[k].Cells["ChildArticleID"].Value.ToString().Trim()])); //* list_TWkResultArticleChild[k].ReqQty
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    sqlParameter2.Add("ChildUseQty", 0);
                                                }
                                            }

                                        }
                                    }

                                }
                                else
                                {
                                     
                                    sqlParameter2.Add("ChildLabelID", list_TWkResultArticleChild[k].ChildLabelID); //2021-12-06 수정
                                    sqlParameter2.Add("ChildLabelGubun", list_TWkResultArticleChild[k].ChildLabelGubun);
                                    sqlParameter2.Add("ChildArticleID", list_TWkResultArticleChild[k].ChildArticleID);
                                    sqlParameter2.Add("ReworkOldYN", list_TWkResultArticleChild[k].ReworkOldYN);
                                    sqlParameter2.Add("ReworkLinkChildProdID", list_TWkResultArticleChild[k].ReworkLinkChildProdID);
                                    sqlParameter2.Add("CreateUserID", list_TWkResultArticleChild[k].CreateUserID);

                                    if (i > 0 && lstLabelList.Count > 0)
                                    {
                                        sqlParameter2.Add("ChildUseQty", list_TWkResult[i].WorkQty * list_TWkResultArticleChild[k].ReqQty);
                                        //sqlParameter1.Add("StartSaveLabelID", lstLabelList[0]); //2021-12-08 일괄스캔 시 오류가 나타나 선라벨이 들어가게 수정
                                    }
                                    else
                                    {
                                        sqlParameter2.Add("ChildUseQty", LabelPaper_Qty * list_TWkResultArticleChild[k].ReqQty);
                                    }
                                                           
                                }

                                WizCommon.Procedure pro3 = new WizCommon.Procedure();
                                pro3.Name = "xp_wkResult_iWkResultArticleChild";
                                pro3.OutputUseYN = "N";
                                pro3.OutputName = "JobID";
                                pro3.OutputLength = "20";

                                Prolist.Add(pro3);
                                ListParameter.Add(sqlParameter2);

                            }
                        }

                        // '************************************************************************************************
                        //'                              불량 등록 시   //xp_wkResult_iInspect
                        //'************************************************************************************************
                        //2021-11-26 불량라벨은 생성 되지 않고 마지막 라벨에서 전부 불량으로 처리 됨
                        if (i + 1 == list_TWkResult.Count)
                        {
                            //2021-09-23
                            foreach (string Key in dicDefect.Keys)
                            {
                               
                                var Defect = dicDefect[Key] as frm_tprc_Work_Defect_U_CodeView;
                                if (Defect != null)
                                {
                                    Dictionary<string, object> sqlParameter4 = new Dictionary<string, object>();

                                    sqlParameter4.Add("DefectID", Key.Trim());
                                    sqlParameter4.Add("DefectQty", ConvertDouble(Defect.DefectQty));
                                    sqlParameter4.Add("XPos", ConvertInt(Defect.XPos));
                                    sqlParameter4.Add("YPos", ConvertInt(Defect.YPos));
                                    sqlParameter4.Add("JobID", list_TWkResult[0].JobID);
                                    sqlParameter4.Add("CreateUserID", list_TWkResult[0].CreateUserID);

                                    WizCommon.Procedure pro4 = new WizCommon.Procedure();
                                    pro4.Name = "xp_prdWork_iWorkDefect";
                                    pro4.OutputUseYN = "N";
                                    pro4.OutputName = "JobID";
                                    pro4.OutputLength = "20";

                                    Prolist.Add(pro4);
                                    ListParameter.Add(sqlParameter4);
                                  
                                }
                            }
                        }
                                                

                        //'************************************************************************************************
                        //'                            생산제품 재고 생성 및 하품 자재 출고 처리  //xp_wkResult_iWkResultStuffInOut
                        //'************************************************************************************************

                        Dictionary<string, object> sqlParameter5 = new Dictionary<string, object>();

                        if (i == 0)
                        {
                            sqlParameter5.Add("JobID", list_TWkResult[i].JobID);
                        }
                        else
                        {
                            sqlParameter5.Add("JobID", 0);
                        }

                        sqlParameter5.Add("CreateUserID", list_TWkResult[i].CreateUserID);
                        sqlParameter5.Add("sRtnMsg", "");
                        

                        WizCommon.Procedure pro6 = new WizCommon.Procedure();
                        pro6.Name = "xp_wkResult_iWkResultStuffInOut";    
                        pro6.OutputUseYN = "N";
                        pro6.OutputName = "JobID";
                        pro6.OutputLength = "20";

                        Prolist.Add(pro6);
                        ListParameter.Add(sqlParameter5);
                    }
                }

                List<KeyValue> list_Result = new List<KeyValue>();
                list_Result = DataStore.Instance.ExecuteAllProcedureOutputToCS_NewLog(Prolist, ListParameter, "C", Frm_tprc_Main.g_tBase.PersonID);

                if (list_Result[0].key.ToLower() == "success")
                {
                    list_Result.RemoveAt(0);

                    //int rsCount = list_Result.Count / 2;//2 = Output갯수(JobID, LabelID)
                    int a = 0;
                    for (int i = 0; i < list_Result.Count; i++)
                    {
                        KeyValue kv = list_Result[i];
                        if (kv.key == "LabelID")
                        {
                            list_TWkLabelPrint[a++].sLabelID = kv.value;
                            //list_TWkLabelPrint[i].sLabelID = kv.value;
                        }
                        //else if (kv.key == "JobID")
                        //{
                        //    //list_TWkResult[i / 2].JobID = float.Parse(kv.value);
                        //    list_TWkResult[i].JobID = float.Parse(kv.value);
                        //}
                    }
                    DataStore.Instance.CloseConnection(); //2021-09-23 DB 커넥트 연결 해제
                    return true;
                }
                else
                {
                    foreach (KeyValue kv in list_Result)
                    {
                        if (kv.key.ToLower() == "failure")
                        {
                            throw new Exception(kv.value.ToString());
                        }
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", ex.Message), "[오류]", 0, 1);                
                return false;
            }

        }

        #region 절단 저장 구문

        private void SaveDataByCutting(string CompletionYN)
        {
            list_TWkResult_By_Cutting = new List<Sub_TWkResult_By_Cutting>();
            list_TWkResultArticleChild_By_Cutting = new List<Sub_TWkResultArticleChild_By_Cutting>();
            list_TWkLabelPrint_By_Cutting = new List<Sub_TWkLabelPrint_By_Cutting>();

            for (int i = 0; i < GridData2.Rows.Count; i++) 
            {
                list_TWkResult_By_Cutting.Add(new Sub_TWkResult_By_Cutting());
                list_TWkResultArticleChild_By_Cutting.Add(new Sub_TWkResultArticleChild_By_Cutting());
                list_TWkLabelPrint_By_Cutting.Add(new Sub_TWkLabelPrint_By_Cutting());

                //wk_result 저장
                list_TWkResult_By_Cutting[i].JobID = float.Parse(updateJobID);
                list_TWkResult_By_Cutting[i].ArticleID = GridData2.Rows[i].Cells["ArticleID"].Value.ToString();
                list_TWkResult_By_Cutting[i].LabelID = "";
                list_TWkResult_By_Cutting[i].LabelGubun = "";
                list_TWkResult_By_Cutting[i].StartLabelID = txtPreInsertLabelBarCode.Text;
                list_TWkResult_By_Cutting[i].PLPDInstID = Frm_tprc_Main.g_tBase.sInstID;
                list_TWkResult_By_Cutting[i].PLPDSInstSeq = Lib.ConvertInt(GridData2.Rows[i].Cells["PLPDSInstSeq"].Value.ToString());
                list_TWkResult_By_Cutting[i].ProcessID = m_ProcessID;
                list_TWkResult_By_Cutting[i].MachineID = m_MachineID;
                list_TWkResult_By_Cutting[i].WorkQty = Lib.ConvertInt(GridData2.Rows[i].Cells["WorkQty"].Value.ToString()) - Lib.ConvertInt(GridData2.Rows[i].Cells["DefectQty"].Value.ToString());
                list_TWkResult_By_Cutting[i].ScanDate = mtb_From.Text.Replace("-", "");
                list_TWkResult_By_Cutting[i].ScanTime = dtStartTime.Value.ToString("HHmmss");
                list_TWkResult_By_Cutting[i].WorkStartDate = mtb_From.Text.Replace("-", "");
                list_TWkResult_By_Cutting[i].WorkStartTime = dtStartTime.Value.ToString("HHmmss");
                list_TWkResult_By_Cutting[i].WorkEndDate = mtb_To.Text.Replace("-", "");
                list_TWkResult_By_Cutting[i].WorkEndTime = dtEndTime.Value.ToString("HHmmss");
                list_TWkResult_By_Cutting[i].Comments = m_ProcessID + "작업종료에 따른 데이터 저장(Insert)";
                list_TWkResult_By_Cutting[i].SplitYNGBN = "YO";
                list_TWkResult_By_Cutting[i].CompletionYN = CompletionYN;
                list_TWkResult_By_Cutting[i].CreateUserID = Frm_tprc_Main.g_tBase.PersonID;

                //wk_Result_ArticleChild 저장
                list_TWkResultArticleChild_By_Cutting[i].ChildArticleID = ListChildArticleID[0].ToString();
                list_TWkResultArticleChild_By_Cutting[i].ChildLabelID = txtPreInsertLabelBarCode.Text;

                if (CompletionYN == "Y")
                {
                    list_TWkResultArticleChild_By_Cutting[i].ChildUseQty = m_LocRemainQty; //LOTID의 현 재고량 전부 소모
                }
                else
                {
                    list_TWkResultArticleChild_By_Cutting[i].ChildUseQty = 0; //LOTID의 현 재고량 전부 소모
                }

                list_TWkResultArticleChild_By_Cutting[i].CreateUserID = Frm_tprc_Main.g_tBase.PersonID;

                //라벨 발행
                list_TWkLabelPrint_By_Cutting[i].LabelID = "";
                list_TWkLabelPrint_By_Cutting[i].LabelGubun = "7";
                list_TWkLabelPrint_By_Cutting[i].ProcessID = m_ProcessID;
                list_TWkLabelPrint_By_Cutting[i].ArticleID = GridData2.Rows[i].Cells["ArticleID"].Value.ToString();
                list_TWkLabelPrint_By_Cutting[i].PrintDate = mtb_From.Text.Replace("-", "");
                list_TWkLabelPrint_By_Cutting[i].ReprintDate = "";
                list_TWkLabelPrint_By_Cutting[i].ReprintQty = 0;
                list_TWkLabelPrint_By_Cutting[i].PLPDInstID = Frm_tprc_Main.g_tBase.sInstID;
                list_TWkLabelPrint_By_Cutting[i].PLPDSInstSeq = Lib.ConvertInt(GridData2.Rows[i].Cells["PLPDSInstSeq"].Value.ToString());
                list_TWkLabelPrint_By_Cutting[i].OrderID = "";
                list_TWkLabelPrint_By_Cutting[i].PrintQty = 1;
                list_TWkLabelPrint_By_Cutting[i].QtyPerBox = 1;
                list_TWkLabelPrint_By_Cutting[i].CreateUserID = Frm_tprc_Main.g_tBase.PersonID;
                         
            }

            if (AddWorkResultByCutting())
            {
                Message[0] = "[저장 완료]";
                Message[1] = "저장이 완료되었습니다.";
                WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 3, 1);

                //수량 0은 저장 안 하는데 PL로 입력된 데이터 수량이 0이라서 저장이 안 될 경우 PL로 임시로 입력된 데이터 삭제
                DeleteByCutting();
            }
        }

        private bool AddWorkResultByCutting()
        {
            try
            {
                int AddCount = 0; //사용량처리하기 위해 추가
                int JaturiCount = 0;//자투리 처리를 하기 위해 추가
                List<Procedure> Prolist = new List<Procedure>();
                List<List<string>> ListProcedureName = new List<List<string>>();
                List<Dictionary<string, object>> ListParameter = new List<Dictionary<string, object>>();

                for (int i = 0; i < list_TWkResult_By_Cutting.Count; i++)
                {
                    //생산량이 0보다 클 경우만 저장 되게
                    if (list_TWkResult_By_Cutting[i].WorkQty > 0)
                    {

                        //'*****************************************************************************************************
                        //'                  공정이동전표 등록
                        //'*****************************************************************************************************
                        Dictionary<string, object> sqlParameter = new Dictionary<string, object>();

                        sqlParameter.Add("LabelID", list_TWkLabelPrint_By_Cutting[i].LabelID);
                        sqlParameter.Add("LabelGubun", list_TWkLabelPrint_By_Cutting[i].LabelGubun);
                        sqlParameter.Add("ProcessID", list_TWkLabelPrint_By_Cutting[i].ProcessID);
                        sqlParameter.Add("ArticleID", list_TWkLabelPrint_By_Cutting[i].ArticleID);
                        sqlParameter.Add("PrintDate", list_TWkLabelPrint_By_Cutting[i].PrintDate);

                        sqlParameter.Add("ReprintDate", list_TWkLabelPrint_By_Cutting[i].ReprintDate);
                        sqlParameter.Add("ReprintQty", list_TWkLabelPrint_By_Cutting[i].ReprintQty);
                        sqlParameter.Add("InstID", list_TWkLabelPrint_By_Cutting[i].PLPDInstID);
                        sqlParameter.Add("InstDetSeq", list_TWkLabelPrint_By_Cutting[i].PLPDSInstSeq);
                        sqlParameter.Add("OrderID", list_TWkLabelPrint_By_Cutting[i].OrderID);

                        sqlParameter.Add("PrintQty", list_TWkLabelPrint_By_Cutting[i].PrintQty);
                        sqlParameter.Add("LabelPrintQty", 1);
                        sqlParameter.Add("nQtyPerBox", list_TWkLabelPrint_By_Cutting[i].QtyPerBox);
                        sqlParameter.Add("CreateUserID", list_TWkLabelPrint_By_Cutting[i].CreateUserID);

                        WizCommon.Procedure pro1 = new WizCommon.Procedure();
                        pro1.Name = "[xp_WizWork_iwkLabelPrint_C]";
                        pro1.OutputUseYN = "Y";
                        pro1.OutputName = "LabelID";
                        pro1.OutputLength = "20";

                        Prolist.Add(pro1);
                        ListParameter.Add(sqlParameter);


                        //'************************************************************************************************
                        //'                               상위품 생산 //xp_wkResult_iWkResult
                        //'************************************************************************************************
                        Dictionary<string, object> sqlParameter1 = new Dictionary<string, object>();
                        Procedure pro2 = new Procedure();

                        if (i == 0)
                        {
                            sqlParameter1.Add("JobID", list_TWkResult_By_Cutting[i].JobID);
                            sqlParameter1.Add("LabelID", list_TWkLabelPrint_By_Cutting[i].LabelID);

                            sqlParameter1.Add("LabelGubun", list_TWkResult_By_Cutting[i].LabelGubun);
                            sqlParameter1.Add("ScanDate", list_TWkResult_By_Cutting[i].ScanDate);
                            sqlParameter1.Add("ScanTime", list_TWkResult_By_Cutting[i].ScanTime);

                            sqlParameter1.Add("WorkStartDate", list_TWkResult_By_Cutting[i].WorkStartDate);
                            sqlParameter1.Add("WorkStartTime", list_TWkResult_By_Cutting[i].WorkStartTime);
                            sqlParameter1.Add("WorkEndDate", list_TWkResult_By_Cutting[i].WorkEndDate);
                            sqlParameter1.Add("WorkEndTime", list_TWkResult_By_Cutting[i].WorkEndTime);

                            sqlParameter1.Add("WorkQty", list_TWkResult_By_Cutting[i].WorkQty);
                            sqlParameter1.Add("CycleTime", list_TWkResult_By_Cutting[i].CycleTime);
                            sqlParameter1.Add("ProcessID", list_TWkResult_By_Cutting[i].ProcessID);
                            sqlParameter1.Add("MachineID", list_TWkResult_By_Cutting[i].MachineID);
                            sqlParameter1.Add("Comments", list_TWkResult_By_Cutting[i].ProcessID + "작업종료에 따른 저장구문");
                            sqlParameter1.Add("CompletionYN", list_TWkResult_By_Cutting[i].CompletionYN);

                            sqlParameter1.Add("SplitYNGBN", list_TWkResult_By_Cutting[i].SplitYNGBN);
                            sqlParameter1.Add("UpdateUserID", list_TWkResult_By_Cutting[i].CreateUserID);

                            //자투리 여부 확인 2025-02-04
                            if(JaturiLOTID != "")
                            {
                                sqlParameter1.Add("WorkJobName", CuttingName); //절단명
                            }
                            else
                            {
                                sqlParameter1.Add("WorkJobName", "");
                            }

                            pro2.Name = "xp_WizWork_uWkResultOne";
                            pro2.OutputUseYN = "N";
                            pro2.OutputName = "JobID";
                            pro2.OutputLength = "20";
                        }
                        else
                        {
                            sqlParameter1.Add("JobID", 0);
                            sqlParameter1.Add("InstID", list_TWkResult_By_Cutting[i].PLPDInstID);
                            sqlParameter1.Add("InstDetSeq", list_TWkResult_By_Cutting[i].PLPDSInstSeq);
                            sqlParameter1.Add("LabelID", list_TWkLabelPrint_By_Cutting[i].LabelID);
                            sqlParameter1.Add("StartSaveLabelID", txtPreInsertLabelBarCode.Text);
                            sqlParameter1.Add("LabelGubun", list_TWkResult_By_Cutting[i].LabelGubun);
                            sqlParameter1.Add("ProcessID", list_TWkResult_By_Cutting[i].ProcessID);
                            sqlParameter1.Add("MachineID", list_TWkResult_By_Cutting[i].MachineID);
                            sqlParameter1.Add("ScanDate", list_TWkResult_By_Cutting[i].ScanDate);
                            sqlParameter1.Add("ScanTime", list_TWkResult_By_Cutting[i].ScanTime);

                            sqlParameter1.Add("ArticleID", list_TWkResult_By_Cutting[i].ArticleID);
                            sqlParameter1.Add("WorkQty", list_TWkResult_By_Cutting[i].WorkQty);
                            sqlParameter1.Add("Comments", list_TWkResult_By_Cutting[i].ProcessID + "작업종료에 따른 저장구문");
                            sqlParameter1.Add("ReworkOldYN", "");
                            sqlParameter1.Add("ReworkLinkProdID", "");

                            sqlParameter1.Add("WorkStartDate", list_TWkResult_By_Cutting[i].WorkStartDate);
                            sqlParameter1.Add("WorkStartTime", list_TWkResult_By_Cutting[i].WorkStartTime);
                            sqlParameter1.Add("WorkEndDate", list_TWkResult_By_Cutting[i].WorkEndDate);
                            sqlParameter1.Add("WorkEndTime", list_TWkResult_By_Cutting[i].WorkEndTime);
                            sqlParameter1.Add("JobGbn", "1");

                            sqlParameter1.Add("NoReworkCode", "");
                            sqlParameter1.Add("WDNO", "");
                            sqlParameter1.Add("WDID", "");
                            sqlParameter1.Add("WDQty", 0);
                            sqlParameter1.Add("LogID", 0);

                            sqlParameter1.Add("s4MID", "");
                            sqlParameter1.Add("DayOrNightID", Wh_Ar_DayOrNightID);
                            sqlParameter1.Add("SplitYNGBN", list_TWkResult_By_Cutting[i].SplitYNGBN);
                            sqlParameter1.Add("CycleTime", list_TWkResult_By_Cutting[i].CycleTime);
                            sqlParameter1.Add("CompletionYN", list_TWkResult_By_Cutting[i].CompletionYN);
                            sqlParameter1.Add("CreateUserID", list_TWkResult_By_Cutting[i].CreateUserID);

                            //자투리 여부 확인 2025-02-04
                            if (JaturiLOTID != "")
                            {
                                sqlParameter1.Add("WorkJobName", CuttingName); //절단명
                            }
                            else
                            {
                                sqlParameter1.Add("WorkJobName", "");
                            }

                            pro2.Name = "xp_wkResult_iWkResult";
                            pro2.OutputUseYN = "Y";
                            pro2.OutputName = "JobID";
                            pro2.OutputLength = "20";
                        }

                        Prolist.Add(pro2);
                        ListParameter.Add(sqlParameter1);


                        Dictionary<string, object> sqlParameter2 = new Dictionary<string, object>();

                        if (i == 0)
                        {
                            sqlParameter2.Add("JobID", list_TWkResult_By_Cutting[i].JobID);
                        }
                        else
                        {
                            sqlParameter2.Add("JobID", 0);
                        }

                        if (list_TWkResult_By_Cutting[i].CompletionYN == "Y" && AddCount == 0)
                        {
                            sqlParameter2.Add("ChildUseQty", list_TWkResultArticleChild_By_Cutting[0].ChildUseQty); //하위품 입력은 되는데 재고는 처음 작업에서만 전부 사용량으로 처리
                            AddCount++;
                        }
                        else
                        {
                            sqlParameter2.Add("ChildUseQty", 0);    //하위품 입력은 되는데 재고는 처음 작업에서만 전부 사용량으로 처리
                        }

                        sqlParameter2.Add("ChildLabelID", list_TWkResultArticleChild_By_Cutting[0].ChildLabelID);//	                                   
                        sqlParameter2.Add("ChildLabelGubun", "");
                        sqlParameter2.Add("ChildArticleID", list_TWkResultArticleChild_By_Cutting[0].ChildArticleID);
                        sqlParameter2.Add("ReworkOldYN", "");
                        sqlParameter2.Add("ReworkLinkChildProdID", "");
                        sqlParameter2.Add("CreateUserID", list_TWkResultArticleChild_By_Cutting[0].CreateUserID);

                        Procedure pro3 = new Procedure();
                        pro3.Name = "xp_wkResult_iWkResultArticleChild";
                        pro3.OutputUseYN = "N";
                        pro3.OutputName = "JobID";
                        pro3.OutputLength = "20";

                        Prolist.Add(pro3);
                        ListParameter.Add(sqlParameter2);


                        //'************************************************************************************************
                        //'                            불량 처리
                        //'************************************************************************************************

                        //2025-02-03 각 지시별로 불량이 있는 경우 확인해서 있을 경우에만 들어오도록해야 됨
                        if (outerdicDefect.ContainsKey(i))
                        {
                            Dictionary<string, frm_tprc_Work_Defect_U_CodeView> innerDict = outerdicDefect[i];

                            foreach (string Key in innerDict.Keys)
                            {

                                var Defect = innerDict[Key] as frm_tprc_Work_Defect_U_CodeView;
                                if (Defect != null)
                                {
                                    Dictionary<string, object> sqlParameter3 = new Dictionary<string, object>();

                                    sqlParameter3.Add("DefectID", Key.Trim());
                                    sqlParameter3.Add("DefectQty", ConvertDouble(Defect.DefectQty));
                                    sqlParameter3.Add("XPos", ConvertInt(Defect.XPos));
                                    sqlParameter3.Add("YPos", ConvertInt(Defect.YPos));
                                    sqlParameter3.Add("JobID", list_TWkResult_By_Cutting[i].JobID);
                                    sqlParameter3.Add("CreateUserID", list_TWkResult_By_Cutting[i].CreateUserID);

                                    Procedure pro4 = new Procedure();
                                    pro4.Name = "xp_prdWork_iWorkDefect";
                                    pro4.OutputUseYN = "N";
                                    pro4.OutputName = "JobID";
                                    pro4.OutputLength = "20";

                                    Prolist.Add(pro4);
                                    ListParameter.Add(sqlParameter3);

                                }
                            }
                        }

                        //'************************************************************************************************
                        //'                            생산제품 재고 생성 및 하품 자재 출고 처리  //xp_wkResult_iWkResultStuffInOut
                        //'************************************************************************************************


                        Dictionary<string, object> sqlParameter4 = new Dictionary<string, object>();

                        if (i == 0)
                        {
                            sqlParameter4.Add("JobID", list_TWkResult_By_Cutting[i].JobID);
                        }
                        else
                        {
                            sqlParameter4.Add("JobID", 0);
                        }

                        sqlParameter4.Add("CreateUserID", list_TWkResult_By_Cutting[i].CreateUserID);
                        sqlParameter4.Add("sRtnMsg", "");

                        //자투리 여부 확인 2025-02-04
                        if (JaturiLOTID != "")
                        {
                            sqlParameter4.Add("OutwareYN", "N"); //자투리 발생
                        }
                        else
                        {
                            sqlParameter4.Add("OutwareYN", "Y"); //자투리 발생 안 됨
                        }

                        Procedure pro5 = new Procedure();
                        pro5.Name = "xp_wkResult_iWkResultStuffInOut_By_Cutting";
                        pro5.OutputUseYN = "N";
                        pro5.OutputName = "JobID";
                        pro5.OutputLength = "20";

                        Prolist.Add(pro5);
                        ListParameter.Add(sqlParameter4);


                        //자투리 생성시 저장 구문, 여기서 StuffinSUb에 OutwareYN 업데이트 예정
                        //OutwareYN stuffinsub
                        //JaturiYN stuffinsub
                        //OrgLotID stuffinsub
                        //WorkJobName wk_result

                        if (JaturiLOTID != "" && JaturiCount == 0)
                        {
                            Dictionary<string, object> sqlParameter5 = new Dictionary<string, object>();

                            if (i == 0)
                            {
                                sqlParameter5.Add("JobID", list_TWkResult_By_Cutting[i].JobID); //JobID
                            }
                            else
                            {
                                sqlParameter5.Add("JobID", 0);//JobID
                            }

                            sqlParameter5.Add("JaturiLOTID", JaturiLOTID); //자투리로트번호
                            sqlParameter5.Add("CuttingWeight", CuttingWeight); //중량
                            sqlParameter5.Add("JaturiYN", "Y"); //자투리 여부
                            sqlParameter5.Add("OutwareYN", "N"); //자투리 발생 여부
                            sqlParameter5.Add("JaturiLOC", JaturiLOC); //자투리 창고
                            sqlParameter5.Add("OrgLotID", txtPreInsertLabelBarCode.Text.ToString());

                            sqlParameter5.Add("CreateUserID", list_TWkResult_By_Cutting[i].CreateUserID);


                            Procedure pro6 = new Procedure();
                            pro6.Name = "xp_WizWork_iCutting_By_Jaturi";
                            pro6.OutputUseYN = "N";
                            pro6.OutputName = "JobID";
                            pro6.OutputLength = "20";

                            Prolist.Add(pro6);
                            ListParameter.Add(sqlParameter5);

                            JaturiCount++;
                        }
                    }
                }


                List<KeyValue> list_Result = new List<KeyValue>();
                list_Result = DataStore.Instance.ExecuteAllProcedureOutputToCS(Prolist, ListParameter);

                if (list_Result[0].key.ToLower() == "success")
                {
                    list_Result.RemoveAt(0);

                    int Cnt = 0; //저장후 사용할 라벨ID를 저장하기 위해 사용하는 변수

                    for (int i = 0; i < list_Result.Count; i++)
                    {
                        KeyValue kv = list_Result[i];
                        if (kv.key == "LabelID")
                        {
                            list_TWkLabelPrint_By_Cutting[Cnt++].LabelID = kv.value;
                        }
                    }
                    DataStore.Instance.CloseConnection(); //2021-09-23 DB 커넥트 연결 해제
                    return true;
                }
                else
                {
                    foreach (KeyValue kv in list_Result)
                    {
                        if (kv.key.ToLower() == "failure")
                        {
                            throw new Exception(kv.value.ToString());
                        }
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", ex.Message), "[오류]", 0, 1);                
                return false;
            }

        }


        #endregion

        //절단 저장시 PL로 입력된 데이터가 저장인 안 될 경우 삭제하는 함수 2025-02-04
        private void DeleteByCutting()
        {
            //1. 현재아이 정보.
            float NowJobID = float.Parse(updateJobID);

            try
            {
                //2. 삭제 프로시저.
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Add("JobID", NowJobID);
                DataStore.Instance.ProcedureToDataSet_NewLog("[xp_WizWork_dWkResult_YellowIng]", sqlParameter, true, "D", Frm_tprc_Main.g_tBase.PersonID);
                DataStore.Instance.CloseConnection(); 

                cmdExit_Click(null, null);
            }
            catch (Exception EX)
            {
                return;
            }
        }

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

                        list_m_tItem.Add(new TTagSub());

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
                TSCLIB_DLL.setup(stringFormatN1(strWidth), stringFormatN1(strHeight), "4", "15", "0", "3", "0"); // GLS Black Mark Setting
                //TSCLIB_DLL.setup(stringFormatN1(strWidth), stringFormatN1(strHeight), "3", "15", "1", "3", "3"); // GLS Black Mark Setting, 2021-11-17 이걸로 수정
                //TSCLIB_DLL.setup(stringFormatN1(strWidth), stringFormatN1(strHeight), "8", "15", "0", "0", "0");//감열지 테스트용

                TSCLIB_DLL.clearbuffer();

                TSCLIB_DLL.sendcommand("DIRECTION " + Sub_m_tTag.sDirect);
                
                string sText = "";
                string[] sBarType = new string[2];

                for (int i = 0; i < list_m_tItem.Count; i++)
                {
                    if (list_m_tItem[i].nVisible > 0)//출력여부
                    {
                        ////'QR CODE
                        //if (list_m_tItem[i].nType == EnumItem.IO_QRcode)
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

                        //    TSCLIB_DLL.sendcommand(qr_command);

                        //    string ReadAble = "0";

                        //    if (ReadAble.Equals("0"))
                        //    {
                        //        // 바코드 글자 세팅
                        //        int intx = list_m_tItem[i].x - 150;
                        //        int inty = list_m_tItem[i].y - 10;
                        //        int fontheight = 30;
                        //        int rotation = 0;
                        //        int fontstyle = 0;
                        //        int fontunderline = 0;
                        //        string FaceName = "맑은 고딕";
                        //        string content = Lib.CheckNull(list_m_tItem[i].sText).Trim();

                        //        TSCLIB_DLL.windowsfont(intx, inty, fontheight, rotation, fontstyle, fontunderline, FaceName, content);
                        //    }
                        //}






                        //'바코드
                        if (list_m_tItem[i].nType == EnumItem.IO_BARCODE)
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

                                TSCLIB_DLL.barcode(list_m_tItem[i].x.ToString(), // x
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
                                    int fontheight = 50; //30 2024-06-04
                                    int rotation = 0;
                                    int fontstyle = 0;
                                    int fontunderline = 0;
                                    string FaceName = "맑은 고딕";
                                    string content = Lib.CheckNull(list_m_tItem[i].sText).Trim();

                                    TSCLIB_DLL.windowsfont(intx, inty, fontheight, rotation, fontstyle, fontunderline, FaceName, content);
                                }
                            }
                        }




                        //데이터 OR 문자
                        else if (list_m_tItem[i].nType == EnumItem.IO_DATA || list_m_tItem[i].nType == EnumItem.IO_TEXT)
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

                            TSCLIB_DLL.windowsfont(intx, inty, fontheight, rotation, fontstyle, fontunderline, szFaceName, content);
                        }
                        //'선(Line)-5이하
                        else if (list_m_tItem[i].nType == EnumItem.IO_LINE)// && (list_m_tItem[i].nFigureHeight <= 5 || list_m_tItem[i].nFigureWidth <= 5))
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

                            TSCLIB_DLL.sendcommand(IsDllStr);
                        }
                        else if (list_m_tItem[i].nType == EnumItem.IO_BOX)
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

                            TSCLIB_DLL.sendcommand(IsDllStr);
                        }

                    }
                }

                

                if (m_ProcessID == "0405")
                {
                    nPrintCount = 2;
                }

                TSCLIB_DLL.printlabel("1", nPrintCount.ToString());

                list_m_tItem = new List<TTagSub>();
                vData = new List<string>();
                DataStore.Instance.CloseConnection(); //2021-09-23 DB 커넥트 연결 해제
                return true;
            }
            catch (Exception excpt)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의<SendWindowDllCommand>\r\n{0}", excpt.Message), "[오류]", 0, 1);
                return false;
            }
        }

        private void PrintWorkCard(int intPrintCount)
        {
            string g_sPrinterName = "";

            try
            {
                int R = 0;      

                IsTagID = "012";

                List<string> list_Data = null;

                g_sPrinterName = Lib.GetDefaultPrinter();

                TSCLIB_DLL.openport(g_sPrinterName);
                for (int i = 0; i < intPrintCount; i++)
                {                    
                    list_Data = new List<string>();
                    Dictionary<string, object> sqlParameter2 = new Dictionary<string, object>();

                    if (Frm_tprc_Main.list_g_tsplit.Count > i)
                    {
                        sqlParameter2.Add("InstID", Frm_tprc_Main.list_g_tsplit[i].InstID);
                        sqlParameter2.Add("CardID", Frm_tprc_Main.list_g_tsplit[i].LabelID);
                        R++;
                    }
                    else
                    {
                        sqlParameter2.Add("InstID", Frm_tprc_Main.g_tBase.sInstID);
                        //if (LabelPrintYN == "Y")
                        //{
                        //    sqlParameter2.Add("CardID", list_TWkResult[i - R].sLabelID);
                        //}
                        //else
                        //{
                            sqlParameter2.Add("CardID", list_TWkResult[i - R].LabelID);
                        //}
                    }

                    DataTable dt2 = DataStore.Instance.ProcedureToDataTable_NewLog("[xp_WorkCard_sWorkCardPrint]", sqlParameter2, false, "P", Frm_tprc_Main.g_tBase.PersonID);
                    lData = new List<string>();
                    string strProcessID = "";
                    int a = 0;
                    int count = 0;
                    double douworkqty = 0;
                    double doudefectqty = 0;
                    string effectdate = "";
                    foreach (DataRow dr in dt2.Rows)
                    {
                        strProcessID = dr["ProcessID"].ToString();
                        //if (dr["ProcSeq"].ToString() == "1") // 첫 공정일 때,
                        //2023-01-16
                        if (dr["ArticleID"].ToString().Trim() == txtArticleID.Text.ToString().Trim() && Frm_tprc_Main.g_tBase.ProcessID == strProcessID)
                        {
                            double.TryParse(dr["WorkQty"].ToString(), out douworkqty);
                            double.TryParse(dr["wk_defectQty"].ToString(), out doudefectqty);

                            list_Data.Add(Lib.CheckNull(dr["wk_CardID"].ToString())); //라벨번호(공정전표)
                            list_Data.Add(Lib.CheckNull(dr["Article"].ToString()));//품명 1
                            list_Data.Add((string.Format("{0:n0}", (int)douworkqty)) + Lib.CheckNull(dr["UnitClssName"].ToString()));//수량 2
                            list_Data.Add(Lib.CheckNull(Lib.MakeDate(WizWorkLib.DateTimeClss.DF_FULL, dr["wk_ResultDate"].ToString())));//생산일 3
                            list_Data.Add(Lib.CheckNull(dr["wk_Name"].ToString()));//생산자 4

                        }
                    }

                    //g_sPrinterName = Lib.GetDefaultPrinter();
                    //TSCLIB_DLL.openport(g_sPrinterName);
                    if (SendWindowDllCommand(list_Data, IsTagID, 1, 0))
                    {
                        if (i == 0) //2021-11-29 라벨발행을 한꺼번에 처리하여 한번나오게 조건 추가
                        {
                            Message[0] = "[라벨발행중]";
                            Message[1] = "라벨 발행중입니다. 잠시만 기다려주세요.";
                            WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 2, 2);
                        }
                    }
                    else
                    {
                        Message[0] = "[라벨발행 실패]";
                        Message[1] = "라벨 발행에 실패했습니다. 관리자에게 문의하여주세요.\r\n<SendWindowDllCommand>";
                        WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 2, 2);
                    }

                    //TSCLIB_DLL.clearbuffer();
                    //TSCLIB_DLL.closeport();
                }
                TSCLIB_DLL.closeport();
            }
            catch (Exception excpt)
            {
                Message[0] = "[오류]";
                Message[1] = string.Format("오류!관리자에게 문의\r\n{0}", excpt.Message);
                WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
            }
            finally
            {
                DataStore.Instance.CloseConnection(); //2021-09-23 DB 커넥트 연결 해제
            }
        }

        private void cmdWorkDefect_Click(object sender, EventArgs e)
        {
            frm_tprc_Work_Defect_U defect = new frm_tprc_Work_Defect_U(updateJobID, dicDefect);
            defect.Owner = this;
            defect.ShowDialog();
            if (defect.DialogResult == DialogResult.OK)
            {
                this.dicDefect = defect.dicDefect;
                this.txtDefectQty.Text = defect.returnTotalQty;
            }
            return;
        }

        private void SetGrid1RowClear()
        {
            while (GridData1.Rows.Count > 0)
            {
                GridData1.Rows.RemoveAt(0);
            }
        }
        private void SetGrid12owClear()
        {
            while (GridData2.Rows.Count > 0)
            {
                GridData2.Rows.RemoveAt(0);
            }
        }

        private void SetFormDataClear()
        {
            this.txtArticle.Text = "";
            txtBuyerArticleNo.Text = "";            
            this.txtSpec.Text = "";

            SetGrid1RowClear();
            SetGrid12owClear();

            this.txtRemark.Text = "";
            this.txtDailyInstWorkQty.Text = "";
            this.txtDefectQty.Text = "";
            this.txtOrderQty.Text = "";
            this.txtOrderWorkQty.Text = "";
            this.txtOrderRemainQty.Text = "";
            this.txtlInstQty.Text = "";
            this.txtInstRemainQty.Text = "";
            txtInRmUnitClss.Text = "";
            txtInUnitClss.Text = "";

            txtProdQty.Text = "";
            txtInstWorkQty.Text = "";
            

            SetDateTimePicker();

            txtBoxID.Text = "";

            lstLabelQtyList.Clear();//2022-06-02
            lstLOTIDQtyList.Clear();//2022-06-02
            lstArticleIDList.Clear();//2022-12-01
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

        private void Form_Activate()
        {
            try
            {
                txtWorkQty.Text = "0";                  // (내가 한) 작업수량
                txtRemainAdd.Text = "0";                // (남이 한) 잔량수량
                txtLotProdQty.Text = "0";               // 생산박스 당 수량
                txtTotalLabelQty.Text = "0";            // 총 수량( 라벨발행의 기준)
                txtCycleTime.Text = "0";                // Cycle Time
                txtBoxQty.Text = "0";                   // 박스 수량
                txtDefectQty.Text = "0";                // 불량 수량
                txtNowCycleTime.Text = "0";             // 현재 Cycle Time

                //절단이 아닌 경우만
                if (m_ProcessID != "0401") 
                {
                    cmdSave.Text = "저 장";
                    btnMiddleSave.Visible = false;
                    btnJaturi.Visible = false;
                    CheckLabelID(Frm_tprc_Main.g_tBase.sLotID);//pl_inputdet의 LotID를 
                    BarcodeEnter(ListChildLabelID, ListChildArticleID);  // 여기서 이제 선 기입한 바코드 값 자동기입.

                    //2022-06-02 재고 확인용
                    lstLabelQtyList.Clear();
                    lstLOTIDQtyList.Clear();
                    lstArticleIDList.Clear();
                    for (int i = 0; i < GridData2.Rows.Count; i++)
                    {
                        lstLabelQtyList.Add(GridData2.Rows[i].Cells["BarCode"].Value.ToString());
                        lstLOTIDQtyList.Add(GridData2.Rows[i].Cells["LocRemainQty"].Value.ToString());
                        lstArticleIDList.Add(GridData2.Rows[i].Cells["ChildArticleID"].Value.ToString());
                    }

                    //재관/용접 이후는 무조건 1로 처리해야 되어 1 미리 입력함 2025-03-12 KDH
                    txtWorkQty.Text = "1";

                }
                else
                {
                    //절단인 경우 클릭 안되게
                    chkWorkQty.Text = "합계 생산량";
                    chkWorkQty.BackColor = Color.LightSkyBlue;
                    chkWorkQty.Enabled = false;
                    txtWorkQty.Enabled = false;
                    cmdWorkDefect.Visible = false;
                    CheckLabelIDByCutting(Frm_tprc_Main.g_tBase.sInstID);//pl_PlateDesign의 InstID를 
                    FillGridData2ByCutting(Frm_tprc_Main.g_tBase.sInstID); //생산할 품목 하단 그리드에 조회
                    SetLabelQty_By_Cutting();//재고 가져오기

                    //DB 저장시 최대 길이 20이라 19자리부터 자투리 못 만들게 조건 추가 2025-02-10 KDH
                    if(m_LabelID.Length > 18)
                    {
                        btnJaturi.Visible = false;
                    }

                }

                FillGridData1();//당일 해당공정의 생산실적 조회           
                Frm_tprc_Main.g_tBase.OrderID = m_OrderID;//생산으로 넘어올 시 글로벌 오더ID 변경

                txtPreInsertLabelBarCode.Text = m_LabelID;

                //BarcodeEnter(ListChildLabelID, ListChildArticleID);  // 여기서 이제 선 기입한 바코드 값 자동기입.
 
                txtTotalLabelQty.Text = string.Format("{0:n0}", m_LabelSumQty); //2021-12-01 스캔한 라벨의 총 수량

                // 시작일자와 시작시간을 scandate로 맞출 것.
                DateTime dateTime = new DateTime();
                dateTime = DateTime.ParseExact(m_WorkStartDate, "yyyyMMdd", null);
                mtb_From.Text = dateTime.ToString("yyyy-MM-dd");

                DateTime dt = DateTime.Now;
                dt = DateTime.ParseExact(m_WorkStartTime, "HHmmss", null);
                dtStartTime.Value = dt;

                // 현재 작업자 표시.
                txtNowWorker.Text = Frm_tprc_Main.g_tBase.Person;

                //2021-12-01 삭제하기 위해 처음의 그리드 수를 알아야 되서 추가
                DeleteGridData2Count = GridData2.Rows.Count;

                // WorkLog에 데이터 수집을 하는 공정이라면, 
                // 수집데이터를 가져와서 자동으로 뿌려주는 작업을 진행해야 겠지. ㅇㅇ.
                Find_Collect_WorkLogData();

                LabelPrintYN = "Y";

                // 수량 / CycleTime 값 0 세팅.
                if (txtFacilityCollectQty.Text == string.Empty)
                {
                    txtFacilityCollectQty.Text = "0";       // 설비수집 수량
                }
            }
            catch (Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", ex.Message), "[오류]", 0, 1);
            }

        }

        //pl_inputdet의 LotID를 
        private string CheckLabelID(string strBarCode)
        {
            string strInstID = "";
            try
            {
                string sMoldID = "";
                if (Frm_tprc_Main.list_tMold.Count > 0)
                {
                    sMoldID = Frm_tprc_Main.list_tMold[0].sMoldID;
                }

                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Add("PLotID", strBarCode);
                sqlParameter.Add("ProcessID", m_ProcessID); //SearchProcessID());                
                sqlParameter.Add("MoldID", sMoldID);

                DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_WizWork_Chkworklotid", sqlParameter, false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];

                    m_MtrExceptYN = Lib.CheckNull(dr["MtrExceptYN"].ToString());//PLotID가 라벨일때 pl_input의 MtrExceptYN
                    m_OutwareExceptYN = Lib.CheckNull(dr["OutwareExceptYN"].ToString());//PLotID가 라벨일때 pl_input의 OutwareExceptYN
                    //m_NewProductYN = Lib.CheckNull(dr["NewProductYN"].ToString()); //2022-02-15 신제품, 재연마 구분
                    strInstID = Lib.CheckNull(dr["InstID"].ToString());//PLotID가 라벨일때 pl_input의 InstID
                    this.txtInstID.Text = Lib.CheckNull(dr["InstID"].ToString());//PLotID가 라벨일때 pl_input의 InstID
                    this.txtInstDetSeq.Text = Lib.CheckNull(dr["InstDetSeq"].ToString());//PLotID가 라벨일때 pl_inputdet의 Instdetseq
                    this.txtLabelGubun.Text = Lib.CheckNull(dr["LabelGubun"].ToString());//PLotID가 라벨일때 LabelGubun = 0                    
                    this.txtArticleID.Text = Lib.CheckNull(dr["ArticleID"].ToString().Trim());//PLotID가 라벨일때 pl_inputdet의 ArticleID
                    this.txtArticle.Text = Lib.CheckNull(dr["pldArticle"].ToString());
                    this.txtBuyerArticleNo.Text = Lib.CheckNull(dr["pldBuyerArticle"].ToString());
                    this.txtSpec.Text = Lib.CheckNull(dr["Spec"].ToString()); //2022-11-07 Spec 추가
                    //Lib.CheckNull(dr["BuyerArticleNo"].ToString());

                    //txtArticle.Text = Lib.CheckNull(dr["pldBuyerArticle"].ToString());
                    //txtBuyerArticleNo.Text = Lib.CheckNull(dr["pldArticle"].ToString());


                    // 라벨발행여부 YN (전역변수 기입)
                    Wh_Ar_LabelPrintYN = Lib.CheckNull(dr["LabelPrintYN"].ToString());

                    double InstQty = 0;
                    double ProdQtyPerBox = 0;
                    double InstWorkQty = 0;
                    double InstRemainQty = 0;
                    double.TryParse(dr["InstQty"].ToString(), out InstQty);
                    //pIdProdQtyPerBox 임시 18.06.18 계속확인할것
                    double.TryParse(dr["ProdQtyPerBox"].ToString(), out ProdQtyPerBox);

                    //pIdProdQtyPerBox 임시 18.06.18 계속확인할것
                    double.TryParse(dr["InstWorkQty"].ToString(), out InstWorkQty);
                    InstRemainQty = InstQty - InstWorkQty;

                    txtlInstQty.Text = string.Format("{0:n2}", InstQty);//지시량
                    txtInstRemainQty.Text = string.Format("{0:n2}", InstRemainQty);//남은지시수량 = 지시수량 - 지시누계량

                    txtInUnitClss.Text = Lib.CheckNull(dr["UnitClssName"].ToString());
                    txtInRmUnitClss.Text = Lib.CheckNull(dr["UnitClssName"].ToString());

                    // 
                    if (!strBarCode.ToUpper().Contains("PL"))
                    {
                        txtProdQty.Text = string.Format("{0:n0}", (int)ProdQtyPerBox);//mt_article의 qtyperbox박스당수량
                    }
                    txtInstWorkQty.Text = string.Format("{0:n2}", InstWorkQty);//지시누계량 = wk_result 생산수량의 합 
                                                                               
                    txtRemark.Text = Lib.CheckNull(dr["Remark"].ToString());//pl_inputdet Remark
                    Frm_tprc_Main.g_tBase.Article = Lib.CheckNull(dr["Article"].ToString());//mt_article
                    Frm_tprc_Main.g_tBase.OrderID = Lib.CheckNull(dr["OrderID"].ToString());//pl_input
                    Frm_tprc_Main.g_tBase.OrderNO = Lib.CheckNull(dr["OrderNO"].ToString());//order

                    ///////////////
                    int WorkQty = 0;
                    int OrderSeq = 0;
                    int.TryParse(dr["ProdQtyPerBox"].ToString(), out WorkQty);
                    int.TryParse(dr["OrderSeq"].ToString(), out OrderSeq);
                    Frm_tprc_Main.g_tBase.WorkQty = WorkQty;//wk_labelprint의 수량
                    //전역변수 WorkQty(생산량)에 박스당 수량을 집어넣는다? 왜?? 수정이 필요해보임
                    Frm_tprc_Main.g_tBase.OrderUnit = Lib.CheckNull(dr["UnitClss"].ToString());//order
                    Frm_tprc_Main.g_tBase.OrderSeq = OrderSeq;
                    //////////
                    m_OrderID = Lib.CheckNull(dr["OrderID"].ToString());//pl_input의 OrderID
                    m_ProdAutoInspectYN = Lib.CheckNull(dr["ProductAutoInspectYN"].ToString());//Order의 ProductAutoInspectYN
                    m_OrderSeq = OrderSeq;
                    /////////////
                    Frm_tprc_Main.g_tBase.Basis = "";
                    Frm_tprc_Main.g_tBase.BasisID = 0;

                    m_OrderNO = Lib.CheckNull(dr["OrderNO"].ToString());//수주번호
                    m_UnitClss = Lib.CheckNull(dr["UnitClss"].ToString());//pl_inputdet articleid의 UnitClss

                    double OrderQty = 0;
                    double OrderWorkQty = 0;
                    double OrderRemainQty = 0;
                    double DefectQty = 0;
                    double DailyInstWorkQty = 0;
                    double.TryParse(dr["OrderQty"].ToString(), out OrderQty);
                    double.TryParse(dr["OrderWorkQty"].ToString(), out OrderWorkQty);
                    double.TryParse(dr["DefectQty"].ToString(), out DefectQty);
                    double.TryParse(dr["DailyInstWorkQty"].ToString(), out DailyInstWorkQty);
                    OrderRemainQty = OrderQty - OrderWorkQty;
                    txtOrderQty.Text = string.Format("{0:n0}", OrderQty);//오더전체수주량//전체오더량
                    txtOrderWorkQty.Text = string.Format("{0:n0}", OrderWorkQty);//오더생산량
                    txtOrderRemainQty.Text = string.Format("{0:n0}", OrderRemainQty);//오더잔량 = 전체오더량 - 오더에 따른 생산량(wk_result)
                    //txtDefectQty.Text = string.Format("{0:n0}", DefectQty); // 오더번호 기준 기존 불량수량
                    txtDailyInstWorkQty.Text = string.Format("{0:n0}", DailyInstWorkQty);//당일 지시 누계량
                    
                    //2018.06.17 추가
                    m_OrderArticleID = dr["OrderArticleID"].ToString().Trim();//오더ArticleID

                    //전역변수 LotID Frm_tprc_Main.g_tBase.sLotID = strBarCode
                    //strbarcode가 라벨id가 아니라 pl_inputdet의 lotid 일때
                    //하위품 그리드를  채워넣는다.
                    if (strBarCode.Contains("PL"))
                    {
                        FillGridData2(strInstID, Frm_tprc_Main.g_tBase.ProcessID);
                    }
                }
            }
            catch (Exception excpt)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message), "[오류]", 0, 1);
                return "";
            }
            finally
            {
                DataStore.Instance.CloseConnection(); //2021-09-23 DB 커넥트 연결 해제
            }
            return strInstID;

        }

        //절단인 경우
        private string CheckLabelIDByCutting(string strBarCode)
        {
            string strInstID = "";
            try
            {

                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Add("PlateInstID", strBarCode);
                sqlParameter.Add("ProcessID", m_ProcessID);         

                DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_WizWork_Chkworklotid_By_Cutting", sqlParameter, false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];

                    strInstID = Lib.CheckNull(dr["InstID"].ToString());//PLotID가 라벨일때 pl_input의 InstID
                    this.txtInstID.Text = Lib.CheckNull(dr["InstID"].ToString());//PLotID가 라벨일때 pl_input의 InstID
                    this.txtInstDetSeq.Text = Lib.CheckNull(dr["InstDetSeq"].ToString());//PLotID가 라벨일때 pl_inputdet의 Instdetseq
                    this.txtLabelGubun.Text = Lib.CheckNull(dr["LabelGubun"].ToString());//PLotID가 라벨일때 LabelGubun = 0                    
                    this.txtArticleID.Text = Lib.CheckNull(dr["ArticleID"].ToString().Trim());//PLotID가 라벨일때 pl_inputdet의 ArticleID
                    this.txtArticle.Text = Lib.CheckNull(dr["Article"].ToString());
                    this.txtBuyerArticleNo.Text = Lib.CheckNull(dr["BuyerArticleNo"].ToString());
                    this.txtSpec.Text = Lib.CheckNull(dr["Spec"].ToString()); //2022-11-07 Spec 추가
                                                                       
                    // 라벨발행여부 YN (전역변수 기입)
                    Wh_Ar_LabelPrintYN = Lib.CheckNull(dr["LabelPrintYN"].ToString());

                    double ProdQtyPerBox = 0;
 
                    double.TryParse(dr["ProdQtyPerBox"].ToString(), out ProdQtyPerBox);


                    txtInUnitClss.Text = Lib.CheckNull(dr["UnitClssName"].ToString());
                    txtInRmUnitClss.Text = Lib.CheckNull(dr["UnitClssName"].ToString());

                    if (!strBarCode.ToUpper().Contains("PL"))
                    {
                        txtProdQty.Text = string.Format("{0:n0}", (int)ProdQtyPerBox);//mt_article의 qtyperbox박스당수량
                    }
                                                                            
                    txtRemark.Text = Lib.CheckNull(dr["Remark"].ToString());//pl_inputdet Remark
                    Frm_tprc_Main.g_tBase.Article = Lib.CheckNull(dr["Article"].ToString());//mt_article

                    m_UnitClss = Lib.CheckNull(dr["UnitClss"].ToString());//pl_inputdet articleid의 UnitClss

                    double DailyInstWorkQty = 0;

                    double.TryParse(dr["InstWorkQty"].ToString(), out DailyInstWorkQty);

                    txtDailyInstWorkQty.Text = string.Format("{0:n0}", DailyInstWorkQty);//당일 지시 누계량
                }
            }
            catch (Exception excpt)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message), "[오류]", 0, 1);
                return "";
            }
            finally
            {
                DataStore.Instance.CloseConnection(); //2021-09-23 DB 커넥트 연결 해제
            }
            return strInstID;

        }

        private void SetGridData2RowClear()
        {
            while (GridData2.Rows.Count > 0)
            {
                GridData2.Rows.RemoveAt(0);
            }
        }

        /// <summary>
        /// LotID에 해당하는 ArticleID 가져오기
        /// 하위품 스캔 체크
        /// </summary>
        /// <param name="strBarCode"></param>
        private bool BarCodeCheck(string strBarCode, string strArticleID)
        {
            DataRow dr = null;
            try
            {
                if (strBarCode == "") //2022-03-07 원자재 예외출고 인 경우 빈 값으로 처리되어 조건 추가
                {
                    Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                    sqlParameter.Add("LotID", strBarCode);
                    sqlParameter.Add("ArticleID", strArticleID);
                    sqlParameter.Add("InstID", Frm_tprc_Main.g_tBase.sInstID);
                    sqlParameter.Add("InstDetSeq", Frm_tprc_Main.g_tBase.sInstDetSeq);
                    DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_prdWork_sGetSubItemInfo", sqlParameter, false);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        dr = dt.Rows[0];

                        if (dr["ScanExceptYN"].ToString() == "Y") 
                        {

                            m_ArticleID = dr["ArticleID"].ToString().Trim();
                            m_LabelGubun = dr["LabelGubun"].ToString().Trim();                    //라벨구분 1은 원자재 
                            this.txtLabelGubun.Text = m_LabelGubun;
                            m_Inspector = "";                      //검사자
                            double.TryParse(dr["RemainQty"].ToString(), out m_RemainQty);         //전체재고량
                            double.TryParse(dr["LocRemainQty"].ToString(), out m_LocRemainQty);   //해당창고재고량
                            m_UnitClss = dr["UnitClss"].ToString();
                            m_UnitClssName = dr["UnitClssName"].ToString();                         //투입되는 원자재의 재고단위
                            m_EffectDate = "";
                            m_ScanExceptYN = "Y"; //스캔 여부

                        }
                        else
                        {
                            m_ArticleID = dr["ArticleID"].ToString().Trim();
                            m_LabelGubun = dr["LabelGubun"].ToString().Trim();                    //라벨구분 1은 원자재 
                            this.txtLabelGubun.Text = m_LabelGubun;
                            m_Inspector = "";                      //검사자
                            double.TryParse("0", out m_RemainQty);         //전체재고량
                            double.TryParse("0", out m_LocRemainQty);   //해당창고재고량
                            m_UnitClss = dr["UnitClss"].ToString();
                            m_UnitClssName = dr["UnitClssName"].ToString();                         //투입되는 원자재의 재고단위
                            m_EffectDate = "";
                            m_ScanExceptYN = "Y"; //스캔 여부

                        }



                        DataStore.Instance.CloseConnection(); //2021-09-23 DB 커넥트 연결 해제
                        return true;
                    }
                    else
                    {
                        //2020.04.03 허윤구
                        // DT가 NULL이 될 수 있는 케이스.
                        // 지속적으로 찾아서 개별 케이스별로 메시지 UPDATE 해야 합니다.

                        //1. 하위품이 사라진경우. > 즉, 알수없는 (여러) 이유로 삭제된 케이스.                    
                    
                        string[] DeleteBarcodeYN = new string[2];
                        string sql_2 = "select cnt = COUNT(*) from wk_result where labelid = '" + strBarCode + "'";
                        DeleteBarcodeYN = DataStore.Instance.ExecuteQuery(sql_2, false);
                        if (DeleteBarcodeYN[1] == "0")
                        {
                            Message[0] = "[하위품 소실]";
                            Message[1] = "해당 하위품( " + strBarCode + " )은 승인되지 않은 품목이거나 삭제처리된 Lot입니다\r\n(작업 취소 후 재시작 해주세요.).";
                            throw new Exception();
                        }
                        else
                        {
                            Message[0] = "[입고승인]";
                            Message[1] = "해당 품목은 승인되지 않은 품목이거나 입고내역이 없는 품목이므로 사용할 수 없습니다.\r\n(작업 취소 후 재시작 해주세요.)";
                            throw new Exception();
                        }
                    }
                }
                else
                {
                    Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                    sqlParameter.Add("LotID", strBarCode);
                    sqlParameter.Add("ProcessID", m_ProcessID);
                    sqlParameter.Add("MachineID", m_MachineID);
                    sqlParameter.Add("InstID", Frm_tprc_Main.g_tBase.sInstID);
                    sqlParameter.Add("InstDetSeq", Frm_tprc_Main.g_tBase.sInstDetSeq);
                    sqlParameter.Add("sArticleID", strArticleID);
                    DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_WizWork_sLotInfoByLotID", sqlParameter, false);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        dr = dt.Rows[0];

                        m_ArticleID = dr["ArticleID"].ToString().Trim();
                        m_LabelGubun = dr["LabelGubun"].ToString().Trim();                    //라벨구분 1은 원자재 
                        this.txtLabelGubun.Text = m_LabelGubun;
                        m_Inspector = dr["Inspector"].ToString().Trim();                      //검사자
                        double.TryParse(dr["Stuffin01_Qty"].ToString(), out m_RemainQty);         //전체재고량
                        double.TryParse(dr["LocRemainQty"].ToString(), out m_LocRemainQty);   //해당창고재고량
                        m_UnitClss = dr["UnitClss"].ToString();
                        m_UnitClssName = dr["UnitClssName"].ToString();                         //투입되는 원자재의 재고단위
                        m_EffectDate = Lib.MakeDateTime("yyyyMMdd", dr["EffectDate"].ToString());
                        m_ScanExceptYN = "N"; //라벨이 있으면 스캔을 했다고 생각하여 무조건 N

                        DataStore.Instance.CloseConnection(); //2021-09-23 DB 커넥트 연결 해제
                        return true;
                    }
                    else
                    {
                        //2020.04.03 허윤구
                        // DT가 NULL이 될 수 있는 케이스.
                        // 지속적으로 찾아서 개별 케이스별로 메시지 UPDATE 해야 합니다.

                        //1. 하위품이 사라진경우. > 즉, 알수없는 (여러) 이유로 삭제된 케이스.                    

                        string[] DeleteBarcodeYN = new string[2];
                        string sql_2 = "select cnt = COUNT(*) from wk_result where labelid = '" + strBarCode + "'";
                        DeleteBarcodeYN = DataStore.Instance.ExecuteQuery(sql_2, false);
                        if (DeleteBarcodeYN[1] == "0")
                        {
                            Message[0] = "[하위품 소실]";
                            Message[1] = "해당 하위품( " + strBarCode + " )은 승인되지 않은 품목이거나 삭제처리된 Lot입니다\r\n(작업 취소 후 재시작 해주세요.).";
                            throw new Exception();
                        }
                        else
                        {
                            Message[0] = "[입고승인]";
                            Message[1] = "해당 품목은 승인되지 않은 품목이거나 입고내역이 없는 품목이므로 사용할 수 없습니다.\r\n(작업 취소 후 재시작 해주세요.)";
                            throw new Exception();
                        }
                    }
                }
            }
            catch (Exception excpt)
            {
                m_ArticleID = "";
                m_LabelGubun = "";
                m_Inspector = "";
                m_RemainQty = 0;
                m_LocRemainQty = 0;
                m_UnitClss = "";
                m_UnitClssName = "";
                m_EffectDate = "";

                // 2020.09.23 아이고 세상에...
                // 오류 발생시 저장이 안되도록 해야 됨!!!!!
                cmdSave.Enabled = false;
                btnBringSplitData.Enabled = false;
                cmdWorkDefect.Enabled = false;
                return false;
            }
        }

        private void FillGridData2(string strInstID, string strProcessID)
        {
            double dulReqQty = 0;
            SetGridData2RowClear();

            try
            {
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();

                sqlParameter.Add("InstID", strInstID);
                sqlParameter.Add("ProcessID", strProcessID);
                sqlParameter.Add("InstDetSeq", Frm_tprc_Main.g_tBase.sInstDetSeq); //2022-11-11 같은 공정을 2개를 같이 작업지시 내려서 InstDetSeq 추가

                DataTable dt = null;
                dt = DataStore.Instance.ProcedureToDataTable("xp_wklabelprint_sGetworkchild", sqlParameter, false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    int a = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        double.TryParse(dr["ReqQty"].ToString(), out dulReqQty);
                        GridData2.Rows.Add(++a
                                           , dr["InstID"].ToString().Trim()
                                           , dr["DetSeq"].ToString().Trim()
                                           , dr["ChildSeq"].ToString().Trim()
                                           , dr["ChildArticleID"].ToString().Trim()
                                           , dr["Article"].ToString().Trim()
                                           , dr["BuyerArticleNo"].ToString().Trim()
                                           , ""
                                           , dr["ScanExceptYN"].ToString().Trim()
                                           , ""
                                           , dr["Flag"].ToString().Trim()
                                           , dr["ScanExceptYN"].ToString().Trim()
                                           , ""
                                           , ""
                                           , dr["UnitClss"].ToString().Trim()
                                           , ""
                                           , dulReqQty.ToString()
                                           , ""
                                           , m_UnitClss//""
                                           , "" 
                                           , ""
                                          );
                        if (dr["ScanExceptYN"].ToString() == "Y")
                        {
                            GridData2.Rows[a - 1].DefaultCellStyle.BackColor = Color.FromArgb(238, 108, 128);
                        }

                        //LastArticle = dr["LastArticle"].ToString(); //2022-11-11 마지막공정이 조립인지 아닌지 판단하기 위한 변수
                    }
                }
            }
            catch (Exception excpt)
            {
                Message[0] = "[오류]";
                Message[1] = string.Format("오류!관리자에게 문의\r\n{0}", excpt.Message);
                WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
            }
            finally
            {
                DataStore.Instance.CloseConnection(); //2021-09-23 DB 커넥트 연결 해제
            }
        }

        private void FillGridData2ByCutting(string strInstID)
        {
            SetGridData2RowClear();

            try
            {
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();

                sqlParameter.Add("PlateInstID", strInstID);

                DataTable dt = null;
                dt = DataStore.Instance.ProcedureToDataTable("xp_wklabelprint_sGetworkchild_By_Cutting", sqlParameter, false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    int a = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        GridData2.Rows.Add(++a
                                           , dr["Article"].ToString().Trim()
                                           , dr["InstQty"].ToString().Trim()
                                           , ""
                                           , ""
                                           , dr["PreWorkQty"].ToString().Trim()
                                           , dr["UnitClssName"].ToString().Trim()
                                           , dr["ArticleID"].ToString().Trim()
                                           , dr["UnitClss"].ToString().Trim()
                                           , dr["PLPDSInstSeq"].ToString()
                                          );
                    }
                }
            }
            catch (Exception excpt)
            {
                Message[0] = "[오류]";
                Message[1] = string.Format("오류!관리자에게 문의\r\n{0}", excpt.Message);
                WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
            }
            finally
            {
                DataStore.Instance.CloseConnection(); //2021-09-23 DB 커넥트 연결 해제
            }
        }

        private void SetGridData1RowClear()
        {
            while (GridData1.Rows.Count > 0)
            {
                GridData1.Rows.RemoveAt(0);
            }
        }


        //당일 해당공정의 생산실적 조회
        private void FillGridData1()
        {
            DataRow dr = null;
            DataGridViewRow row = null;

            SetGridData1RowClear();
            try
            {
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Add("sDate", DateTime.Now.ToString("yyyyMMdd"));
                sqlParameter.Add("ProcessID", Frm_tprc_Main.g_tBase.ProcessID);
                sqlParameter.Add("MachineID", Frm_tprc_Main.g_tBase.MachineID);
                DataSet ds = null;
                ds = DataStore.Instance.ProcedureToDataSet("xp_Work_sResultListbyProcessID", sqlParameter, false);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        dr = ds.Tables[0].Rows[i];
                        GridData1.Rows.Add(i + 1
                                           , dr["LabelID"].ToString().Trim()
                                          );
                        row = GridData1.Rows[i];
                        row.Height = 30;
                    }
                    GridData1.ClearSelection();
                    txtGrd1Count.Text = ds.Tables[0].Rows.Count.ToString() + "건";
                }
                else
                {

                }
            }
            catch (Exception excpt)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의<FillGridData1>\r\n{0}", excpt.Message), "[오류]", 0, 1);
            }
            finally
            {
                DataStore.Instance.CloseConnection(); //2021-09-23 DB 커넥트 연결 해제
            }
        }

        /// <summary>
        /// ////////////////////
        /// </summary>
        // 바코드 (자동) 스캔.
        private void BarcodeEnter(List<string> ListChildLabelID, List<string> ListChildArticleID)
        {

            string sInstID = "";
            string sGridArticleID = "";

            //string Barcode = txtPreInsertLabelBarCode.Text.Trim(); 
            
            try
            {
                for (int listcount = 0; listcount < ListChildLabelID.Count; listcount++) //2021-11-30 하위품 라벨 리스트에 있는 라벨들 전부 그리드에 보여주기 위해 반복문 추가
                {
                    // '바코드에 해당하는 Article, LabelGubun을 전역변수에 저장
                    if (!BarCodeCheck(ListChildLabelID[listcount], ListChildArticleID[listcount]))
                    {
                        throw new Exception();
                    }

                    if (GridData2.RowCount > 0)
                    {
                        for (int i = 0; i < GridData2.RowCount; i++)
                        {
                            if (listcount == 0 || GridData2.Rows[i].Cells["Barcode"].Value.ToString() == "") //|| ListChildArticleID.Distinct().Count() > 1
                            {
                                sGridArticleID = GridData2.Rows[i].Cells["ChildArticleID"].Value.ToString().Trim();

                                if (sFirst_ArticleID.Equals(""))
                                {
                                    sFirst_ArticleID = GridData2.Rows[i].Cells["ChildArticleID"].Value.ToString().Trim();
                                }

                                if (m_ArticleID == sGridArticleID)
                                {
                                    //Grid의 단위와 불러온 입고품의 단위를 Grid의 단위에 맞게 맞추기
                                    if (GridData2.Rows[i].Cells["UnitClss"].Value.ToString() != m_UnitClss)
                                    {
                                        string GridUnitClss = GridData2.Rows[i].Cells["UnitClss"].Value.ToString();
                                        if (GridUnitClss == "1" && m_UnitClss == "2")//g
                                        {
                                            m_LocRemainQty = m_LocRemainQty * 1000;
                                        }
                                        else if (GridUnitClss == "2" && m_UnitClss == "1")//kg
                                        {
                                            m_LocRemainQty = m_LocRemainQty / 1000;
                                        }
                                    }

                                    GridData2.Rows[i].Cells["ScanExceptYN"].Value = m_ScanExceptYN;
                                    GridData2.Rows[i].Cells["BarCode"].Value = ListChildLabelID[listcount]; //2021-11-30 old : this.txtPreInsertLabelBarCode.Text.Trim();
                                    GridData2.Rows[i].Cells["LabelGubun"].Value = m_LabelGubun;
                                    GridData2["BuyerArticle", i].Selected = true;
                                    GridData2.Rows[i].Cells["RemainQty"].Value = string.Format("{0:n0}", m_RemainQty);//전체잔량
                                    GridData2.Rows[i].Cells["LocRemainQty"].Value = string.Format("{0:n0}", m_LocRemainQty);//창고잔량
                                    double.TryParse(GridData2.Rows[i].Cells["ReqQty"].Value.ToString(), out m_douReqQty);
                                    GridData2.Rows[i].Cells["EffectDate"].Value = m_EffectDate;
                                    GridData2.Rows[i].Cells["UnitClssName"].Value = m_UnitClssName;         // 하위품의 재고단위
                                    GridData2.Rows[i].Cells["ProdUnitClssName"].Value = txtInUnitClss.Text; // 생산되는 생산품의 재고단위


                                    m_douProdCapa = m_LocRemainQty / m_douReqQty;
                                    if (m_douProdCapa.ToString().Contains("."))
                                    {
                                        string[] sProdCapa = m_douProdCapa.ToString().Split('.');
                                        double.TryParse(sProdCapa[0].ToString(), out m_douProdCapa);//소수점 버림
                                    }
                                    GridData2.Rows[i].Cells["ProdCapa"].Value = string.Format("{0:n0}", m_douProdCapa);

                                    m_LabelSumQty += m_LocRemainQty; //2021-12-01 로트별 합계를 위해 추가

                                    //2022-12-29
                                    if (m_douProdCapa > 0)
                                    {
                                        //2021-12-01 라벨스캔할때 재고 0은 스캔이 되지 않아 처음만 0이라서 처음엔 무조건 생산가능량을 넣기
                                        if (m_MindouProdCapa == 0)
                                        {
                                            m_MindouProdCapa = m_douProdCapa;
                                            txtMindouProdCapa.Text = m_MindouProdCapa.ToString();
                                        }
                                        //2021-12-01 작은 값 가져가기
                                        else if (m_MindouProdCapa > m_douProdCapa && sFirst_ArticleID != sGridArticleID)
                                        {
                                            m_MindouProdCapa = m_douProdCapa;
                                            txtMindouProdCapa.Text = m_MindouProdCapa.ToString();
                                        }
                                        else if (sFirst_ArticleID == sGridArticleID)
                                        {
                                            m_MindouProdCapa += m_douProdCapa;
                                            txtMindouProdCapa.Text = string.Format("{0:n0}", m_MindouProdCapa); //m_MindouProdCapa.ToString();
                                        }
                                    }
                                    //}

                                    m_ArticleID = "";
                                    m_LabelGubun = "";
                                    m_LocRemainQty = 0;
                                    m_RemainQty = 0;
                                    m_EffectDate = "";
                                    m_UnitClssName = "";
                                }
                            }
                            else
                            {
                                sGridArticleID = GridData2.Rows[i].Cells["ChildArticleID"].Value.ToString().Trim();


                                if (m_ArticleID == sGridArticleID)
                                {
                                    //Grid의 단위와 불러온 입고품의 단위를 Grid의 단위에 맞게 맞추기
                                    if (GridData2.Rows[i].Cells["UnitClss"].Value.ToString() != m_UnitClss)
                                    {
                                        string GridUnitClss = GridData2.Rows[i].Cells["UnitClss"].Value.ToString();
                                        if (GridUnitClss == "1" && m_UnitClss == "2")//g
                                        {
                                            m_LocRemainQty = m_LocRemainQty * 1000;
                                        }
                                        else if (GridUnitClss == "2" && m_UnitClss == "1")//kg
                                        {
                                            m_LocRemainQty = m_LocRemainQty / 1000;
                                        }
                                    }

                                    double.TryParse(GridData2.Rows[i].Cells["ReqQty"].Value.ToString(), out m_douReqQty);

                                    m_douProdCapa = m_LocRemainQty / m_douReqQty;
                                    if (m_douProdCapa.ToString().Contains("."))
                                    {
                                        string[] sProdCapa = m_douProdCapa.ToString().Split('.');
                                        double.TryParse(sProdCapa[0].ToString(), out m_douProdCapa);//소수점 버림
                                    }

                                    GridData2.Rows.Add(GridData2.Rows.Count + 1
                                                        , GridData2.Rows[i].Cells["InstID"].Value
                                                        , GridData2.Rows[i].Cells["DetSeq"].Value
                                                        , GridData2.Rows[i].Cells["ChildSeq"].Value
                                                        , GridData2.Rows[i].Cells["ChildArticleID"].Value
                                                        , GridData2.Rows[i].Cells["Article"].Value
                                                        , GridData2.Rows[i].Cells["BuyerArticle"].Value
                                                        , ListChildLabelID[listcount].ToString()
                                                        , m_ScanExceptYN
                                                        , m_LabelGubun
                                                        , GridData2.Rows[i].Cells["Flag"].Value
                                                        , m_ScanExceptYN
                                                        , string.Format("{0:n0}", m_RemainQty)      //m_RemainQty.ToString()
                                                        , string.Format("{0:n0}", m_LocRemainQty)   //m_LocRemainQty.ToString()
                                                        , GridData2.Rows[i].Cells["UnitClss"].Value
                                                        , m_UnitClssName
                                                        , m_douReqQty.ToString()
                                                        , string.Format("{0:n0}", m_douProdCapa) //m_douProdCapa.ToString()
                                                        , m_UnitClss
                                                        , txtInUnitClss.Text
                                                        , m_EffectDate
                                        );

                                    m_LabelSumQty += m_LocRemainQty; //2021-12-01 로트별 합계를 위해 추가

                                    //2022-12-29
                                    if (m_douProdCapa > 0)
                                    {
                                        //2021-12-01 라벨스캔할때 재고 0은 스캔이 되지 않아 처음만 0이라서 처음엔 무조건 생산가능량을 넣기
                                        if (m_MindouProdCapa == 0)
                                        {
                                            m_MindouProdCapa = m_douProdCapa;
                                            txtMindouProdCapa.Text = m_MindouProdCapa.ToString();
                                        }
                                        //2021-12-01 작은 값 가져가기
                                        else if (m_MindouProdCapa > m_douProdCapa && sFirst_ArticleID != sGridArticleID)
                                        {
                                            m_MindouProdCapa = m_douProdCapa;
                                            txtMindouProdCapa.Text = m_MindouProdCapa.ToString();
                                        }
                                        else if (sFirst_ArticleID == sGridArticleID)
                                        {
                                            m_MindouProdCapa += m_douProdCapa;
                                            txtMindouProdCapa.Text = string.Format("{0:n0}", m_MindouProdCapa); //m_MindouProdCapa.ToString();
                                        }
                                    }
                                    //}

                                    m_ArticleID = "";
                                    m_LabelGubun = "";
                                    m_LocRemainQty = 0;
                                    m_RemainQty = 0;
                                    m_EffectDate = "";
                                    m_UnitClssName = "";
                                }


                            }

                        }
                    }

                    Get_TotalQty();
                }
            }
            catch (Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(Message[1].Length == 0 ? ex.Message : Message[1], Message[0], 0, 1);
                return;
            }

        }

        // workLog 가져올거 있으면 가져오고 뿌리고 해야 함.
        private void Find_Collect_WorkLogData()
        {
            try
            {
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_WorkLog_sCollectWorkLogData", sqlParameter, false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        // 공정과 호기정보가 일치한다면,
                        if (m_ProcessID == dr["ProcessID"].ToString() &&
                            Frm_tprc_Main.g_tBase.MachineID == dr["MachineID"].ToString())
                        {
                            txtFacilityCollectQty.Text = Lib.CheckNull(dr["WorkQty"].ToString().Trim());
                            txtWorkQty.Text = Lib.CheckNull(dr["WorkQty"].ToString().Trim());
                            break;
                        }
                    }
                }
            }
            catch (Exception)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                return;
            }
            finally
            {
                DataStore.Instance.CloseConnection(); //2021-09-23 DB 커넥트 연결 해제
            }

        }

        //재고 가져오기 2025-02-03
        private void SetLabelQty_By_Cutting()
        {
            try
            {
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Clear();

                sqlParameter.Add("LabelID", m_LabelID);
                sqlParameter.Add("ArticleID", ListChildArticleID[0].ToString()); //2022-12-01 하나의 라벨로 계속 진행할 경우를 위해 추가

                DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_WizWork_SetLabelIDQty_By_Cutting", sqlParameter, false);

                if (dt != null
                    && dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    m_LocRemainQty = Lib.ConvertDouble(dr["Qty"].ToString());
                }
            }
            catch (Exception ex)
            {
                m_LocRemainQty = 0;
                WizCommon.Popup.MyMessageBox.ShowBox("재고 체크 구문 오류 [ SetLabelQty_By_Cutting ] + \r\n" + ex.Message, "저장 전 체크 오류", 0, 1);
            }
            DataStore.Instance.CloseConnection(); //2021-09-23 DB 커넥트 연결 해제
        }

        private void btnStartTime_Click(object sender, EventArgs e)
        {
            TimeCheck("시작시간");
        }

        private void btnEndTime_Click(object sender, EventArgs e)
        {
            TimeCheck("종료시간");
        }

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

            if (strTime == "시작시간")
            {
                dtStartTime.Value = dt;
            }
            else if (strTime == "종료시간")
            {
                dtEndTime.Value = dt;
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

        #region 수량 / CycleTime 기입용 숫자 키패드 팝업창 모음.

        // 작업수량 기입 팝업창 생성
        private void chkWorkQty_Click(object sender, EventArgs e)
        {
            double DOU_WorkQty = 0;
            double DOU_LotProdQty = 0; //박스 당 수량

            txtWorkQty.Text = "";
            POPUP.Frm_CMNumericKeypad keypad = new POPUP.Frm_CMNumericKeypad("수량입력", "수량");

            keypad.Owner = this;
            if (keypad.ShowDialog() == DialogResult.OK)
            {
                txtWorkQty.Text = keypad.tbInputText.Text;
                //txtLotProdQty.Text = keypad.tbInputText.Text; //무조건 1박스로 처리하여 수량와 같이 들어가게 추가 
                if (txtWorkQty.Text == "" || Convert.ToInt32(txtWorkQty.Text) == 0)
                {
                    txtWorkQty.Text = "0";
                }
                Double.TryParse(txtLotProdQty.Text, out DOU_LotProdQty);
                Double.TryParse(txtWorkQty.Text, out DOU_WorkQty);
                txtWorkQty.Text = string.Format("{0:n0}", (int)DOU_WorkQty);
                //txtLotProdQty.Text = string.Format("{0:n0}", (int)DOU_WorkQty); //무조건 1박스로 처리하여 수량와 같이 들어가게 추가
            }

            //Get_TotalQty();

            txtBoxQty.Text = string.Format("{0:n0}", Math.Ceiling(DOU_WorkQty / DOU_LotProdQty));


            if (Double.IsNaN(Math.Ceiling(DOU_WorkQty / DOU_LotProdQty))
                || Double.IsPositiveInfinity(Math.Ceiling(DOU_WorkQty / DOU_LotProdQty))
                || Double.IsNegativeInfinity(Math.Ceiling(DOU_WorkQty / DOU_LotProdQty)))
            {
                txtBoxQty.Text = string.Format("{0:n0}", 0);
            }
            else
            {
                txtBoxQty.Text = string.Format("{0:n0}", Math.Ceiling(DOU_WorkQty / DOU_LotProdQty));
            }

        }

        // 작업수량 기입 팝업창 생성
        private void txtWorkQty_Click(object sender, EventArgs e)
        {
            double DOU_WorkQty = 0;
            double DOU_LotProdQty = 0; //박스 당 수량

            //txtWorkQty.Text = "";
            POPUP.Frm_CMNumericKeypad keypad = new POPUP.Frm_CMNumericKeypad("수량입력", "수량");

            keypad.Owner = this;
            if (keypad.ShowDialog() == DialogResult.OK)
            {
                txtWorkQty.Text = keypad.tbInputText.Text;
                //txtLotProdQty.Text = keypad.tbInputText.Text; //무조건 1박스로 처리하여 수량와 같이 들어가게 추가
                if (txtWorkQty.Text == "" || Convert.ToInt32(txtWorkQty.Text) == 0)
                {
                    txtWorkQty.Text = "0";
                }
                Double.TryParse(txtLotProdQty.Text, out DOU_LotProdQty);
                Double.TryParse(txtWorkQty.Text, out DOU_WorkQty);
                txtWorkQty.Text = string.Format("{0:n0}", (int)DOU_WorkQty);
                //txtLotProdQty.Text = string.Format("{0:n0}", (int)DOU_WorkQty); //무조건 1박스로 처리하여 수량와 같이 들어가게 추가
            }

            //Get_TotalQty();

            txtBoxQty.Text = string.Format("{0:n0}", Math.Ceiling(DOU_WorkQty / DOU_LotProdQty));


            if (Double.IsNaN(Math.Ceiling(DOU_WorkQty / DOU_LotProdQty))
                || Double.IsPositiveInfinity(Math.Ceiling(DOU_WorkQty / DOU_LotProdQty))
                || Double.IsNegativeInfinity(Math.Ceiling(DOU_WorkQty / DOU_LotProdQty)))
            {
                txtBoxQty.Text = string.Format("{0:n0}", 0);
            }
            else
            {
                txtBoxQty.Text = string.Format("{0:n0}", Math.Ceiling(DOU_WorkQty / DOU_LotProdQty));
            }

        }

        // 생산박스 당 수량 기입 팝업창 생성
        private void chkLotProdQty_Click(object sender, EventArgs e)
        {
            double DOU_LotProdQty = 0;
            double DOU_WorkQty = 0;

            txtLotProdQty.Text = "";
            POPUP.Frm_CMNumericKeypad keypad = new POPUP.Frm_CMNumericKeypad("생산수량", "생산수량");

            keypad.Owner = this;
            if (keypad.ShowDialog() == DialogResult.OK)
            {
                txtLotProdQty.Text = keypad.tbInputText.Text;
                if (txtLotProdQty.Text == "" || Convert.ToInt32(txtLotProdQty.Text) == 0)
                {
                    txtLotProdQty.Text = "0";
                }
                Double.TryParse(txtWorkQty.Text, out DOU_WorkQty);
                Double.TryParse(txtLotProdQty.Text, out DOU_LotProdQty);
                txtLotProdQty.Text = string.Format("{0:n0}", (int)DOU_LotProdQty);
            }

            txtBoxQty.Text = string.Format("{0:n0}", Math.Ceiling(DOU_WorkQty / DOU_LotProdQty));


            if (Double.IsNaN(Math.Ceiling(DOU_WorkQty / DOU_LotProdQty))
                || Double.IsPositiveInfinity(Math.Ceiling(DOU_WorkQty / DOU_LotProdQty))
                || Double.IsNegativeInfinity(Math.Ceiling(DOU_WorkQty / DOU_LotProdQty)))
            {
                txtBoxQty.Text = string.Format("{0:n0}", 0);
            }
            else
            {
                txtBoxQty.Text = string.Format("{0:n0}", Math.Ceiling(DOU_WorkQty / DOU_LotProdQty));
            }

        }
        // 생산박스 당 수량 기입 팝업창 생성
        private void txtLotProdQty_Click(object sender, EventArgs e)
        {
            double DOU_LotProdQty = 0;
            double DOU_WorkQty = 0;

            txtLotProdQty.Text = "";
            POPUP.Frm_CMNumericKeypad keypad = new POPUP.Frm_CMNumericKeypad("생산수량", "생산수량");

            keypad.Owner = this;
            if (keypad.ShowDialog() == DialogResult.OK)
            {
                txtLotProdQty.Text = keypad.tbInputText.Text;
                if (txtLotProdQty.Text == "" || Convert.ToInt32(txtLotProdQty.Text) == 0)
                {
                    txtLotProdQty.Text = "0";
                }
                Double.TryParse(txtWorkQty.Text, out DOU_WorkQty);
                Double.TryParse(txtLotProdQty.Text, out DOU_LotProdQty);
                txtLotProdQty.Text = string.Format("{0:n0}", (int)DOU_LotProdQty);
            }

            txtBoxQty.Text = string.Format("{0:n0}", Math.Ceiling(DOU_WorkQty / DOU_LotProdQty));


            if (Double.IsNaN(Math.Ceiling(DOU_WorkQty / DOU_LotProdQty))
                || Double.IsPositiveInfinity(Math.Ceiling(DOU_WorkQty / DOU_LotProdQty))
                || Double.IsNegativeInfinity(Math.Ceiling(DOU_WorkQty / DOU_LotProdQty)))
            {
                txtBoxQty.Text = string.Format("{0:n0}", 0);
            }
            else
            {
                txtBoxQty.Text = string.Format("{0:n0}", Math.Ceiling(DOU_WorkQty / DOU_LotProdQty));
            }
        }


        // CycleTime 기입 팝업창 생성
        private void chkCycleTime_Click(object sender, EventArgs e)
        {
            double DOU_CycleTime = 0;

            txtCycleTime.Text = "";
            POPUP.Frm_CMNumericKeypad keypad = new POPUP.Frm_CMNumericKeypad("C-T", "C-T");

            keypad.Owner = this;
            if (keypad.ShowDialog() == DialogResult.OK)
            {
                txtCycleTime.Text = keypad.tbInputText.Text;
                if (txtCycleTime.Text == "" || Convert.ToInt32(txtCycleTime.Text) == 0)
                {
                    txtCycleTime.Text = "0";
                }
                Double.TryParse(txtCycleTime.Text, out DOU_CycleTime);
                txtCycleTime.Text = string.Format("{0:n0}", (int)DOU_CycleTime);
            }
        }
        // CycleTime 기입 팝업창 생성
        private void txtCycleTime_Click(object sender, EventArgs e)
        {
            double DOU_CycleTime = 0;

            txtCycleTime.Text = "";
            POPUP.Frm_CMNumericKeypad keypad = new POPUP.Frm_CMNumericKeypad("C-T", "C-T");

            keypad.Owner = this;
            if (keypad.ShowDialog() == DialogResult.OK)
            {
                txtCycleTime.Text = keypad.tbInputText.Text;
                if (txtCycleTime.Text == "" || Convert.ToInt32(txtCycleTime.Text) == 0)
                {
                    txtCycleTime.Text = "0";
                }
                Double.TryParse(txtCycleTime.Text, out DOU_CycleTime);
                txtCycleTime.Text = string.Format("{0:n0}", (int)DOU_CycleTime);
            }
        }

        #endregion

        // 합계수량 구해내기.
        private void Get_TotalQty()
        {
            double My_WorkQty = 0;
            double Your_RemainQty = 0;
            double LotSumQty = 0; //2021-12-01 추가
            double TotalQty = 0; 

            double.TryParse(txtWorkQty.Text, out My_WorkQty);
            double.TryParse(txtRemainAdd.Text, out Your_RemainQty);
            double.TryParse(txtTotalLabelQty.Text, out LotSumQty); //2021-12-01 추가

            TotalQty = LotSumQty + Your_RemainQty; //2021-12-01 수정
            txtTotalLabelQty.Text = string.Format("{0:n0}", TotalQty); //2021-12-01 수정
        }

        // 오른쪽 작업(취소)(삭제)(파기)??? 버튼 신규생성.
        private void btnWorkingDestory_Click(object sender, EventArgs e)
        {
            Message[0] = "[작업삭제]";
            Message[1] = "생산작업을 진행중이였습니다. 작업을 삭제하시겠습니까?";
            if (WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 0) == DialogResult.OK)
            {
                //1. 현재아이 정보.
                float NowJobID = float.Parse(updateJobID);
                string YLabelID = m_LabelID;

                try
                {
                    //2. 삭제 프로시저.
                    Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                    sqlParameter.Add("JobID", NowJobID);
                    //sqlParameter.Add("YLabelID", YLabelID);
                    DataStore.Instance.ProcedureToDataSet_NewLog("[xp_WizWork_dWkResult_YellowIng]", sqlParameter, true, "D", Frm_tprc_Main.g_tBase.PersonID);
                    DataStore.Instance.CloseConnection(); //2021-09-23 DB 커넥트 연결 해제
                    //3. 다시 목록으로 복귀.
                    cmdExit_Click(null, null);
                }
                catch (Exception EX)
                {
                    Message[0] = "[작업삭제]";
                    Message[1] = "작업삭제에 실패하였습니다. JobID: " + NowJobID + ",라벨: " + YLabelID + "\r\n" +
                        EX.Message.ToString();
                    WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                    return;
                }
            }
        }

        // (실제는 체크, 모양은 버튼) 체크형 버튼들 > 체크하더라도 본 색깔 유형 그대로 유지하도록.
        private void checkBox_CheckedPrevent(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == true)
            {
                ((CheckBox)sender).Checked = false;
            }
            else
            {
                ((CheckBox)sender).Checked = false;
            }
        }

        #region 기타 메서드 모음

        // 천마리 콤마, 소수점 버리기
        private string stringFormatN0(object obj)
        {
            return string.Format("{0:N0}", obj);
        }

        private string stringFormatN1(object obj)
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
            else if (str.Length == 4)
            {
                string Hour = str.Substring(0, 2);
                string Min = str.Substring(2, 2);

                str = Hour + ":" + Min;
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

        //박스수 TextBox
        private void txtBoxQty_Click(object sender, EventArgs e)
        {
            double DOU_BoxQty = 0; //박스 수
            double DOU_WorkQty = 0;
           
            POPUP.Frm_CMNumericKeypad keypad = new POPUP.Frm_CMNumericKeypad("수량입력", "수량");

            keypad.Owner = this;
            if (keypad.ShowDialog() == DialogResult.OK)
            {
                txtBoxQty.Text = keypad.tbInputText.Text;
                if (txtBoxQty.Text == "" || Convert.ToInt32(txtBoxQty.Text) == 0)
                {
                    txtBoxQty.Text = "0";
                }
                Double.TryParse(txtBoxQty.Text, out DOU_BoxQty);
                Double.TryParse(txtWorkQty.Text, out DOU_WorkQty);
                txtBoxQty.Text = string.Format("{0:n0}", (int)DOU_BoxQty);
            }

            if (Double.IsNaN(Math.Ceiling(DOU_WorkQty / DOU_BoxQty))
                || Double.IsPositiveInfinity(Math.Ceiling(DOU_WorkQty / DOU_BoxQty))
                || Double.IsNegativeInfinity(Math.Ceiling(DOU_WorkQty / DOU_BoxQty)))
            {
                txtLotProdQty.Text = string.Format("{0:n0}", 0);
            }
            else
            {
                txtLotProdQty.Text = string.Format("{0:n0}", Math.Ceiling(DOU_WorkQty / DOU_BoxQty));
            }
        }

        //박스수 
        private void chkBoxQty_Click(object sender, EventArgs e)
        {
            double DOU_BoxQty = 0; //박스 수
            double DOU_WorkQty = 0;

            POPUP.Frm_CMNumericKeypad keypad = new POPUP.Frm_CMNumericKeypad("수량입력", "수량");

            keypad.Owner = this;
            if (keypad.ShowDialog() == DialogResult.OK)
            {
                txtBoxQty.Text = keypad.tbInputText.Text;
                if (txtBoxQty.Text == "" || Convert.ToInt32(txtBoxQty.Text) == 0)
                {
                    txtBoxQty.Text = "0";
                }
                Double.TryParse(txtBoxQty.Text, out DOU_BoxQty);
                Double.TryParse(txtWorkQty.Text, out DOU_WorkQty);
                txtBoxQty.Text = string.Format("{0:n0}", (int)DOU_BoxQty);
            }

            if (Double.IsNaN(Math.Ceiling(DOU_WorkQty / DOU_BoxQty))
                || Double.IsPositiveInfinity(Math.Ceiling(DOU_WorkQty / DOU_BoxQty))
                || Double.IsNegativeInfinity(Math.Ceiling(DOU_WorkQty / DOU_BoxQty)))
            {
                txtLotProdQty.Text = string.Format("{0:n0}", 0);
            }
            else
            {
                txtLotProdQty.Text = string.Format("{0:n0}", Math.Ceiling(DOU_WorkQty / DOU_BoxQty));
            }
        }


    }
}