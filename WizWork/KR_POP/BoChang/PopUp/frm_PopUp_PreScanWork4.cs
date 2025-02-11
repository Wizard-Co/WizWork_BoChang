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
//프로그램명    frm_PopUp_PreScanWork4.cs
//메뉴ID        
//설명          frm_PopUp_PreScanWork4 메인소스입니다.
//작성일        2019.07.29
//개발자        허윤구
//*******************************************************************************
// 변경일자     변경자      요청자      요구사항ID          요청 및 작업내용
//*******************************************************************************
// 2025.01.23   KDH         보창프레스                       절단인 경우 pl_input이 아니라 pl_PlateDesign을 사용하여 조건 추가
//*******************************************************************************

namespace WizWork
{
    public partial class frm_PopUp_PreScanWork4 : Form
    {

        private string m_ProcessID = "";        //공정id
        private string m_MachineID = "";        //머신id  
        private string m_ArticleID = "";        //품명id 
        private string m_LabelGubun = "";       //라벨구분  
        private string m_MoldID = "";
        private string m_MtrExceptYN = "";      // 예외처리 체크용도
        private string m_ChildArticleID = "";   //2021-05-12
        private string m_ChildArticle = "";     //2021-05-12
        private string m_ChildUnitClss = "";    //2021-05-12
        private string Wh_Ar_InstID = "";
        private string Wh_Ar_InstID_Seq = "";

        private double m_LocRemainQty = 0;      //    '자품목 현 재고량  (스캔 후 초기화)

        List<string> ArticleIDList = new List<string>(); //2022-05-20 하위품 수를 알기 위한 리스트
        List<string> ChildArticleIDList = new List<string>(); //2023-10-06 하위품 갯수 리스트

        int ArticleIDCount = 0; //2022-05-20 하위품 수 판단하는 변수
        private string m_PCMtrExceptYN = "";      // 2022-06-08 공정별 예외처리 체크용도

        string[] Message = new string[2];  // 메시지박스 처리용도.

        WizWorkLib Lib = new WizWorkLib();

        public frm_PopUp_PreScanWork4()
        {
            InitializeComponent();
            SetScreen();  //TLP 사이즈 조정
        }
        public frm_PopUp_PreScanWork4(string strProcessID, string strMachineID, string strMoldID)
        {
            InitializeComponent();
            m_ProcessID = strProcessID;
            m_MachineID = strMachineID;
            m_MoldID = strMoldID;
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
        private void frm_PopUp_PreScanWork4_Load(object sender, EventArgs e)
        {
            // 전체 클리어.
            SetFormDataClear();

            //절단이 아닌 경우만 
            if (m_ProcessID != "0401") 
            {
                //2022-06-08 mtrExceptYN 빈값인지 아닌지 판단
                ProcessMtrExceptYN();
            }

            // 시작작업 처리
            Form_Activate();

            //절단이 아닌 경우만 
            if (m_ProcessID != "0401") 
            {
                if ((m_MtrExceptYN.Equals("N") && m_PCMtrExceptYN.Equals("")) || m_PCMtrExceptYN.Equals("N"))
                {
                    ChildArticleIDCount();
                    setDataGrid__ChildArticle();
                }
            }
            else
            {
                setDataGridChildArticleByCutting();
            }
        }

        #region 하위품 그리드 세팅 setDataGrid__ChildArticle() → 첫 공정일 때만

        private void setDataGrid__ChildArticle()
        {
            dgdMain.Rows.Clear();
            ArticleIDList.Clear(); //2022-05-20
            ArticleIDCount = 0;    //2022-05-20
            try
            {
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();

                sqlParameter.Add("InstID", Frm_tprc_Main.g_tBase.sInstID);
                sqlParameter.Add("InstDetSeq", ConvertInt(Frm_tprc_Main.g_tBase.sInstDetSeq));
                DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_prdWork_sChildArticleForScan_GLS", sqlParameter, false); //xp_prdWork_sChildArticleForScan_GLS_20210412

                if (dt != null && dt.Rows.Count > 0)
                {

                    foreach (DataRow dr2 in dt.Rows)
                    {
                        ArticleIDList.Add(dr2["ArticleID"].ToString().Trim());
                    }

                    if (ArticleIDList.Distinct().ToList().Count() > 1)
                    {
                        ArticleIDCount = 2;
                    }

                    ArticleIDList = ArticleIDList.Distinct().ToList();

                    if (ArticleIDList.Count != ChildArticleIDList.Count)
                    {
                        dgdMain.Rows.Clear();
                        WizCommon.Popup.MyMessageBox.ShowBox("투입 가능한 입고라벨이 없습니다.", "[입고내역 조회]", 0, 1);
                        btnOK.Enabled = false; //2022-07-18 입고라벨 없으면 시작처리 안되게 false
                    }
                    else
                    {
                        DataGridViewRow dgvr = null;

                        int i = 0;
                        double x = 0;
                        string y = "";
                        foreach (DataRow dr in dt.Rows)
                        {

                            dgdMain.Rows.Add(++i,
                                                     Lib.CheckNull(dr["ArticleID"].ToString()),
                                                     Lib.CheckNull(dr["BuyerArticleNo"].ToString().Trim()),
                                                     Lib.CheckNull(dr["Article"].ToString().Trim()),
                                                     Lib.CheckNull(DatePickerFormat(dr["StuffDate"].ToString().Trim())),
                                                     Lib.CheckNull(dr["StuffClssName"].ToString().Trim()), //2021-04-12 어떻게 입고가 되었는지 표시하기 위해 추가
                                                     Lib.CheckNull(stringFormatN5(dr["BomQty"])), // 소요량 NowLoc
                                                     Lib.CheckNull(stringFormatN5(dr["NowLoc"])), // 현재고량
                                                     Lib.CheckNull(dr["LotID"].ToString().Trim()), // 라벨
                                                     Lib.CheckNull(dr["LabelPrintYN"].ToString().Trim()),
                                                     //Lib.CheckNull("X") // 투입여부
                                                     x += double.Parse((Lib.CheckNull(stringFormatN5(dr["NowLoc"])))) //2021-04-06 현재고량의 합계를 보기 위해 추가
                            );
                            dgvr = dgdMain.Rows[i - 1];
                            //dgvr.Cells["IsIN"].Style.ForeColor = Color.Red;
                        }
                        y = string.Format("{0:#,###.#####}", x); //2021-04-12 자리수마다 콤마 표시
                        dgdMain.Rows.Add(++i, "", "", "", "", "", "합계", y, ""); //2021-04-06 마지막 그리드에 현재고량 합계 출력
                        btnOK.Enabled = true; //2022-07-18
                    }
                }
                else
                {
                    //((WizWork.Frm_tprc_Main)(this.MdiParent)).SetstbLookUp("0개의 자료가 검색되었습니다.");
                    dgdMain.Rows.Clear();
                    WizCommon.Popup.MyMessageBox.ShowBox("투입 가능한 입고라벨이 없습니다.", "[입고내역 조회]", 0, 1);
                    btnOK.Enabled = false; //2022-07-18 입고라벨 없으면 시작처리 안되게 false
                }
                Frm_tprc_Main.gv.queryCount = string.Format("{0:n0}", dgdMain.RowCount);
                Frm_tprc_Main.gv.SetStbInfo();
            }
            catch (Exception excpt)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message), "[오류]", 0, 1);
            }
        }


        //절단인 경우
        private void setDataGridChildArticleByCutting()
        {
            dgdMain.Rows.Clear();

            try
            {
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();

                sqlParameter.Add("PlateInstID", Frm_tprc_Main.g_tBase.sInstID);
                DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_prdWork_sChildArticleForScan_By_Cutting", sqlParameter, false); //xp_prdWork_sChildArticleForScan_GLS_20210412

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataGridViewRow dgvr = null;

                    int i = 0;
                    double x = 0;
                    string y = "";
                    foreach (DataRow dr in dt.Rows)
                    {

                        dgdMain.Rows.Add(++i,
                                        Lib.CheckNull(dr["ArticleID"].ToString()),
                                        Lib.CheckNull(dr["BuyerArticleNo"].ToString().Trim()),
                                        Lib.CheckNull(dr["Article"].ToString().Trim()),
                                        Lib.CheckNull(DatePickerFormat(dr["StuffDate"].ToString().Trim())),
                                        Lib.CheckNull(dr["StuffClssName"].ToString().Trim()), //2021-04-12 어떻게 입고가 되었는지 표시하기 위해 추가
                                        Lib.CheckNull(stringFormatN0(dr["BomQty"])), // 소요량 NowLoc
                                        Lib.CheckNull(stringFormatN0(dr["NowLoc"])), // 현재고량
                                        Lib.CheckNull(dr["LotID"].ToString().Trim()), // 라벨
                                        Lib.CheckNull(dr["LabelPrintYN"].ToString().Trim()),
                                        //Lib.CheckNull("X") // 투입여부
                                        x += double.Parse((Lib.CheckNull(stringFormatN5(dr["NowLoc"])))) //2021-04-06 현재고량의 합계를 보기 위해 추가
                        );
                        dgvr = dgdMain.Rows[i - 1];
                        //dgvr.Cells["IsIN"].Style.ForeColor = Color.Red;
                    }
                    y = string.Format("{0:#,###.#####}", x); //2021-04-12 자리수마다 콤마 표시
                    dgdMain.Rows.Add(++i, "", "", "", "", "", "합계", y, ""); //2021-04-06 마지막 그리드에 현재고량 합계 출력
                    btnOK.Enabled = true; //2022-07-18                    
                }
                else
                {
                    //((WizWork.Frm_tprc_Main)(this.MdiParent)).SetstbLookUp("0개의 자료가 검색되었습니다.");
                    dgdMain.Rows.Clear();
                    WizCommon.Popup.MyMessageBox.ShowBox("투입 가능한 입고라벨이 없습니다.", "[입고내역 조회]", 0, 1);
                    btnOK.Enabled = false; //2022-07-18 입고라벨 없으면 시작처리 안되게 false
                }
                Frm_tprc_Main.gv.queryCount = string.Format("{0:n0}", dgdMain.RowCount);
                Frm_tprc_Main.gv.SetStbInfo();
            }
            catch (Exception excpt)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message), "[오류]", 0, 1);
            }
        }
        #endregion

        #region 하위 그리드 취소 버튼 클릭 > 해당 하위품 취소
        private void dgdMain_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (dgdMain.SelectedRows[0].Cells["IsIN"].Value.ToString().Trim().Equals("투입완료")
            //   && dgdMain.Columns[e.ColumnIndex].Name == "Cancel")
            //{
            //    if (MessageBox.Show("해당 하위품을 투입 취소하시겠습니까?", "취소 전 확인", MessageBoxButtons.YesNo) == DialogResult.Yes)
            //    {
            //        dgdMain.SelectedRows[0].Cells["IsIN"].Value = "X";
            //        dgdMain.SelectedRows[0].Cells["Label"].Value = "";
            //        dgdMain.SelectedRows[0].DefaultCellStyle.ForeColor = Color.Black;

            //        btnOK.Tag = "X";
            //    }
            //}
        }

        #endregion

        // After Load
        private void frm_PopUp_PreScanWork4_Shown(object sender, EventArgs e)
        {
            // 포커스
            txtBarCodePreScan.Focus();
            txtBarCodePreScan.Select(0, 0);
            txtBarCodePreScan.Focus();
        }

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

        #region 전체 클리어
        private void SetFormDataClear()
        {
            this.txtBarCodePreScan.Text = string.Empty;
            //this.txtArticle.Text = string.Empty;
            //this.txtBuyerArticleNo.Text = string.Empty;

            // 스캔체크에 통과할때까지 '시작처리' 버튼은 사용불가. 
            //btnOK.Enabled = false;
        }

        #endregion

        #region Form_Activate 묶음

        #region 시작작업
        private void Form_Activate()
        {
            try
            {
                if (m_ProcessID != "0401") 
                {
                    Wh_Ar_InstID = CheckLabelID(Frm_tprc_Main.g_tBase.sLotID);
                }
                else
                {
                    Wh_Ar_InstID = CheckLabelIDByCutting(Frm_tprc_Main.g_tBase.sLotID);
                }
            }
            catch(Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", ex.Message), "[오류]", 0, 1);
            }
            
        }

        #endregion

        #region  시작용 체크 + Textbox 채우기.
        private string CheckLabelID(string strBarCode)
        {
            string strInstID = "";
            string strInstDetSeq = "";

            try
            {
                string sMoldID = "";

                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Add("PLotID", strBarCode);
                sqlParameter.Add("ProcessID", m_ProcessID); //SearchProcessID());                
                sqlParameter.Add("MoldID", sMoldID);

                DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_WizWork_Chkworklotid", sqlParameter, false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    m_MtrExceptYN = Lib.CheckNull(dr["MtrExceptYN"].ToString());//PLotID가 라벨일때 pl_input의 MtrExceptYN                    
                    strInstID = Lib.CheckNull(dr["InstID"].ToString());//PLotID가 라벨일때 pl_input의 InstID                                        
                    strInstDetSeq = Lib.CheckNull(dr["InstDetSeq"].ToString());
                    Wh_Ar_InstID_Seq = strInstDetSeq;

                    double InstQty = 0;
                    double InstWorkQty = 0;
                    double InstRemainQty = 0;

                    double.TryParse(dr["InstQty"].ToString(), out InstQty);
                    double.TryParse(dr["InstWorkQty"].ToString(), out InstWorkQty);
                    InstRemainQty = InstQty - InstWorkQty;                    

                    Frm_tprc_Main.g_tBase.sArticleID = Lib.CheckNull(dr["ArticleID"].ToString().Trim());//mt_article
                    Frm_tprc_Main.g_tBase.Article = Lib.CheckNull(dr["pldArticle"].ToString());//mt_article
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
                    Frm_tprc_Main.g_tBase.Basis = "";
                    Frm_tprc_Main.g_tBase.BasisID = 0;

                    if ((m_MtrExceptYN.Equals("Y") && m_PCMtrExceptYN.Equals("")) || m_PCMtrExceptYN.Equals("Y"))
                    {
                        setPreScanLabel(); //2021-05-07 Grid 생성 없이 바로 넘어가면 checkdata 에서 오류 생겨서 Y도 Grid생성 되게 추가
                        btnOK_Click(null, null);
                    }

                }

            }
            catch (Exception excpt)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message), "[오류]", 0, 1);
                return "";
            }

            return strInstID;
        }

        //절단인 경우
        private string CheckLabelIDByCutting(string strBarCode)
        {
            string strInstID = "";
            string strInstDetSeq = "";

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
                    strInstDetSeq = Lib.CheckNull(dr["InstDetSeq"].ToString());
                    Wh_Ar_InstID_Seq = strInstDetSeq;

                    Frm_tprc_Main.g_tBase.sArticleID = Lib.CheckNull(dr["ArticleID"].ToString().Trim());//반제품, 제품 ArticleID

                    //if ((m_MtrExceptYN.Equals("Y") && m_PCMtrExceptYN.Equals("")) || m_PCMtrExceptYN.Equals("Y"))
                    //{
                    //    setPreScanLabel(); //2021-05-07 Grid 생성 없이 바로 넘어가면 checkdata 에서 오류 생겨서 Y도 Grid생성 되게 추가
                    //    btnOK_Click(null, null);
                    //}

                }

            }
            catch (Exception excpt)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message), "[오류]", 0, 1);
                return "";
            }

            return strInstID;
        }

        #endregion

        #endregion

        #region 시작, 취소 버튼선택.

        // 시작처리 버튼 선택시.
        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                // DB 임시 인서트.
                if (StartHandleWorking_Click() == true)
                {
                    DialogResult = DialogResult.OK;
                    btnCancel_Click(null, null);
                }
                else
                {
                    Message[0] = "[오류]";
                    Message[1] = string.Format("오류! 관리자에게 문의\r\n");
                    WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                }
            }
            catch(Exception ex)
            {
                Message[0] = "[오류]";
                Message[1] = string.Format("오류! 관리자에게 문의\r\n");
                WizCommon.Popup.MyMessageBox.ShowBox(Message[1] + ex.Message, Message[0], 0, 1);
            }
        }

        // 취소 버튼 선택시.
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region DB 임시인서트 펑션

        // DB 임시데이터 인서트 작업.
        private bool StartHandleWorking_Click()
        {
            List<WizCommon.Procedure> Prolist = new List<WizCommon.Procedure>();
            List<Dictionary<string, object>> ListParameter = new List<Dictionary<string, object>>();

            try
            {
                Dictionary<string, object> sqlParameter1 = new Dictionary<string, object>();
                double WorkQty = 0;

                sqlParameter1.Add("JobID", 0);
                sqlParameter1.Add("InstID", Frm_tprc_Main.g_tBase.sInstID);
                sqlParameter1.Add("InstDetSeq", Frm_tprc_Main.g_tBase.sInstDetSeq);

                //절단이 아닌 경우만
                if (m_ProcessID != "0401") 
                {
                    sqlParameter1.Add("LabelID", Frm_tprc_Main.g_tBase.sLotID);
                }
                else
                {
                    sqlParameter1.Add("LabelID", "PL" + Frm_tprc_Main.g_tBase.sLotID + "1"); //절단일 경우 PlateInstID가 입력 됨으로 임시 인서트시 저장되는 양식으로 저장, 공정으로 구분해야됨
                }

                sqlParameter1.Add("StartSaveLabelID", dgdMain.SelectedRows[0].Cells["Label"].Value.ToString()); //2021-05-29 old : dgdMain.SelectedRows[0].Cells["Label"].Value.ToString()                
                sqlParameter1.Add("LabelGubun", m_LabelGubun);
                sqlParameter1.Add("ProcessID", m_ProcessID);
                sqlParameter1.Add("MachineID", m_MachineID);

                // 새벽 작업자 scan date 일자 -1 작업(새벽반의 물량도 전날물량으로 쳐야 하니까.)
                // 2019.09.05 허윤구.
                string Midnight_Worker = DateTime.Now.ToString("HHmmss");
                if ((Convert.ToInt32(Midnight_Worker) >= 000000) && (Convert.ToInt32(Midnight_Worker) <= 073000))
                {
                    sqlParameter1.Add("ScanDate", DateTime.Now.AddDays(-1).ToString("yyyyMMdd"));
                }
                else
                {
                    sqlParameter1.Add("ScanDate", DateTime.Now.ToString("yyyyMMdd"));
                }
                sqlParameter1.Add("ScanTime", DateTime.Now.ToString("HHmmss"));

                sqlParameter1.Add("ArticleID", Frm_tprc_Main.g_tBase.sArticleID);
                sqlParameter1.Add("WorkQty", WorkQty);
                sqlParameter1.Add("Comments", m_ProcessID + " " + "공정 시작처리에 의한 기록작업");
                sqlParameter1.Add("ReworkOldYN", "");
                sqlParameter1.Add("ReworkLinkProdID", "");

                sqlParameter1.Add("WorkStartDate", DateTime.Now.ToString("yyyyMMdd"));
                sqlParameter1.Add("WorkStartTime", DateTime.Now.ToString("HHmmss"));
                sqlParameter1.Add("WorkEndDate", "");
                sqlParameter1.Add("WorkEndTime", "");
                sqlParameter1.Add("JobGbn", "1");

                sqlParameter1.Add("NoReworkCode", "");
                sqlParameter1.Add("WDNO", "");
                sqlParameter1.Add("WDID", "");
                sqlParameter1.Add("WDQty", 0);
                sqlParameter1.Add("LogID", 0);

                sqlParameter1.Add("s4MID", "");
                sqlParameter1.Add("DayOrNightID", Frm_tprc_Main.g_tBase.DayOrNightID);
                sqlParameter1.Add("CreateUserID", Frm_tprc_Main.g_tBase.PersonID);
                

                WizCommon.Procedure pro1 = new WizCommon.Procedure();
                pro1.Name = "xp_wkResult_iWkResult";
                pro1.OutputUseYN = "Y";
                pro1.OutputName = "JobID";
                pro1.OutputLength = "20";

                Prolist.Add(pro1);
                ListParameter.Add(sqlParameter1);


                //절단이 아닌 경우만
                if (m_ProcessID != "0401")
                {
                    if ((m_MtrExceptYN.Equals("N") && m_PCMtrExceptYN.Equals("")) || m_PCMtrExceptYN.Equals("N"))
                    {
                        if (ArticleIDCount != 2) //2022-05-20 하위품 하나와 2개이상일 경우 다르게 처리함
                        {

                            Dictionary<string, object> sqlParameter2 = new Dictionary<string, object>();
                            sqlParameter2.Add("JobID", "");
                            sqlParameter2.Add("ChildLabelID", dgdMain.SelectedRows[0].Cells["Label"].Value.ToString());//2021-05-29 old : dgdMain.SelectedRows[0].Cells["Label"].Value.ToString()
                            sqlParameter2.Add("ChildArticleID", dgdMain.SelectedRows[0].Cells["ArticleID"].Value.ToString().Trim()); //2021-05-29 old : dgdMain.SelectedRows[0].Cells["ArticleID"].Value.ToString()                   
                            sqlParameter2.Add("ChildLabelGubun", ""); // 더미 데이터니까, 일단 이건 뺌
                            sqlParameter2.Add("ReworkOldYN", "");
                            sqlParameter2.Add("ReworkLinkChildProdID", "");
                            sqlParameter2.Add("ChildUseQty", 0);
                            sqlParameter2.Add("CreateUserID", Frm_tprc_Main.g_tBase.PersonID);

                            WizCommon.Procedure pro2 = new WizCommon.Procedure();
                            pro2.Name = "xp_wkResult_iWkResultArticleChild";
                            pro2.OutputUseYN = "N";
                            pro2.OutputName = "JobID";
                            pro2.OutputLength = "20";

                            Prolist.Add(pro2);
                            ListParameter.Add(sqlParameter2);

                        }
                        else
                        {
                            for (int i = 0; i < dgdMain.Rows.Count - 1; i++)
                            {

                                Dictionary<string, object> sqlParameter2 = new Dictionary<string, object>();
                                sqlParameter2.Add("JobID", "");
                                sqlParameter2.Add("ChildLabelID", dgdMain.Rows[i].Cells["Label"].Value.ToString());//
                                sqlParameter2.Add("ChildLabelGubun", ""); // 더미 데이터니까, 일단 이건 뺌
                                sqlParameter2.Add("ChildArticleID", dgdMain.Rows[i].Cells["ArticleID"].Value.ToString().Trim());
                                sqlParameter2.Add("ReworkOldYN", "");
                                sqlParameter2.Add("ReworkLinkChildProdID", "");
                                sqlParameter2.Add("CreateUserID", Frm_tprc_Main.g_tBase.PersonID);
                                sqlParameter2.Add("ChildUseQty", 0);

                                WizCommon.Procedure pro2 = new WizCommon.Procedure();
                                pro2.Name = "xp_wkResult_iWkResultArticleChild";
                                pro2.OutputUseYN = "N";
                                pro2.OutputName = "JobID";
                                pro2.OutputLength = "20";

                                Prolist.Add(pro2);
                                ListParameter.Add(sqlParameter2);
                            }
                        }
                    }
                    else
                    {
                        if (ArticleIDCount != 2) //2022-05-20 하위품 하나와 2개이상일 경우 다르게 처리함
                        {

                            Dictionary<string, object> sqlParameter2 = new Dictionary<string, object>();
                            sqlParameter2.Add("JobID", "");
                            sqlParameter2.Add("ChildLabelID", dgdMain.SelectedRows[0].Cells["Label"].Value.ToString());//2021-05-29 old : dgdMain.SelectedRows[0].Cells["Label"].Value.ToString()
                            sqlParameter2.Add("ChildArticleID", dgdMain.SelectedRows[0].Cells["ArticleID"].Value.ToString().Trim()); //2021-05-29 old : dgdMain.SelectedRows[0].Cells["ArticleID"].Value.ToString()                   
                            sqlParameter2.Add("ChildLabelGubun", ""); // 더미 데이터니까, 일단 이건 뺌
                            sqlParameter2.Add("ReworkOldYN", "");
                            sqlParameter2.Add("ReworkLinkChildProdID", "");
                            sqlParameter2.Add("ChildUseQty", 0);
                            sqlParameter2.Add("CreateUserID", Frm_tprc_Main.g_tBase.PersonID);

                            WizCommon.Procedure pro2 = new WizCommon.Procedure();
                            pro2.Name = "xp_wkResult_iWkResultArticleChild";
                            pro2.OutputUseYN = "N";
                            pro2.OutputName = "JobID";
                            pro2.OutputLength = "20";

                            Prolist.Add(pro2);
                            ListParameter.Add(sqlParameter2);

                        }
                        else
                        {
                            for (int i = 0; i < dgdMain.Rows.Count; i++)
                            {

                                Dictionary<string, object> sqlParameter2 = new Dictionary<string, object>();
                                sqlParameter2.Add("JobID", "");
                                sqlParameter2.Add("ChildLabelID", dgdMain.Rows[i].Cells["Label"].Value.ToString());//
                                sqlParameter2.Add("ChildLabelGubun", ""); // 더미 데이터니까, 일단 이건 뺌
                                sqlParameter2.Add("ChildArticleID", dgdMain.Rows[i].Cells["ArticleID"].Value.ToString().Trim());
                                sqlParameter2.Add("ReworkOldYN", "");
                                sqlParameter2.Add("ReworkLinkChildProdID", "");
                                sqlParameter2.Add("CreateUserID", Frm_tprc_Main.g_tBase.PersonID);
                                sqlParameter2.Add("ChildUseQty", 0);

                                WizCommon.Procedure pro2 = new WizCommon.Procedure();
                                pro2.Name = "xp_wkResult_iWkResultArticleChild";
                                pro2.OutputUseYN = "N";
                                pro2.OutputName = "JobID";
                                pro2.OutputLength = "20";

                                Prolist.Add(pro2);
                                ListParameter.Add(sqlParameter2);
                            }
                        }
                    }
                }
                else
                {
                    Dictionary<string, object> sqlParameter2 = new Dictionary<string, object>();
                    sqlParameter2.Add("JobID", "");
                    sqlParameter2.Add("ChildLabelID", dgdMain.SelectedRows[0].Cells["Label"].Value.ToString());//2021-05-29 old : dgdMain.SelectedRows[0].Cells["Label"].Value.ToString()
                    sqlParameter2.Add("ChildArticleID", dgdMain.SelectedRows[0].Cells["ArticleID"].Value.ToString().Trim()); //2021-05-29 old : dgdMain.SelectedRows[0].Cells["ArticleID"].Value.ToString()                   
                    sqlParameter2.Add("ChildLabelGubun", ""); // 더미 데이터니까, 일단 이건 뺌
                    sqlParameter2.Add("ReworkOldYN", "");
                    sqlParameter2.Add("ReworkLinkChildProdID", "");
                    sqlParameter2.Add("ChildUseQty", 0);
                    sqlParameter2.Add("CreateUserID", Frm_tprc_Main.g_tBase.PersonID);

                    WizCommon.Procedure pro2 = new WizCommon.Procedure();
                    pro2.Name = "xp_wkResult_iWkResultArticleChild";
                    pro2.OutputUseYN = "N";
                    pro2.OutputName = "JobID";
                    pro2.OutputLength = "20";

                    Prolist.Add(pro2);
                    ListParameter.Add(sqlParameter2);
                }
              
                List<KeyValue> list_Result = new List<KeyValue>();
                list_Result = DataStore.Instance.ExecuteAllProcedureOutputToCS(Prolist, ListParameter);

                if (list_Result[0].key.ToLower() == "success")
                {
                    list_Result.RemoveAt(0);
                    m_LabelGubun = "";
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

        // 천마리 콤마, 소수점 네자리
        private string stringFormatN5(object obj)
        {
            return string.Format("{0:N5}", obj);
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

        private void setPreScanLabel()
        {
            try
            {
                ArticleIDList.Clear(); //2022-05-20
                ArticleIDCount = 0;    //2022-05-20
                //2021-05-12
                int index = 1;
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Add("PLotID", Frm_tprc_Main.g_tBase.sLotID);

                DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_PlanInput_sPlanInputDetArticle_Child", sqlParameter, false);

                foreach (DataRow dr2 in dt.Rows)
                {
                    ArticleIDList.Add(dr2["ChildArticleID"].ToString().Trim());
                }

                if (ArticleIDList.Distinct().ToList().Count() > 1)
                {
                    ArticleIDCount = 2;
                }

                foreach (DataRow dr in dt.Rows)
                {
                    m_ChildArticle = dr["BuyerArticleNo"].ToString();
                    m_ChildArticleID = dr["ChildArticleID"].ToString().Trim();
                    m_ChildUnitClss = dr["UnitClss"].ToString();


                    dgdMain.Rows.Add(index.ToString() // 순번
                                               , m_ChildArticleID
                                               , m_ChildArticle
                                               , ""
                                               , "선스캔라벨"
                                               , ""
                                               , ""
                                               , m_ChildUnitClss
                                               , "" //2022-02-14
                                              );
                    index++;
                    //InsertX++; //2021-05-12 articlechild 에 임시 임서트 하기 위해 추가
                }

            }
            catch (Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", ex.Message), "[오류 - setPreScanLabel()]", 0, 1);
            }
        }


        //하위품 갯수 함수
        private void ChildArticleIDCount()
        {
            try
            {
                ChildArticleIDList.Clear(); //2022-05-20
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Add("PLotID", Frm_tprc_Main.g_tBase.sLotID);

                DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_PlanInput_sPlanInputDetArticle_Child", sqlParameter, false);

                foreach (DataRow dr2 in dt.Rows)
                {
                    ChildArticleIDList.Add(dr2["ChildArticleID"].ToString().Trim());
                }

                ChildArticleIDList = ChildArticleIDList.Distinct().ToList();

            }
            catch (Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", ex.Message), "[오류 - setPreScanLabel()]", 0, 1);
            }
        }

        //스캔 클릭 이벤트
        private void cmdBarCodePreScan_Click(object sender, EventArgs e)
        {
            //절단이 아닌 경우만
            if (m_ProcessID != "0401")
            {
                POPUP.Frm_CMKeypad.g_Name = "바코드 스캔";
                POPUP.Frm_CMKeypad FK = new POPUP.Frm_CMKeypad();
                POPUP.Frm_CMKeypad.KeypadStr = txtBarCodePreScan.Text.Trim();
                if (FK.ShowDialog() == DialogResult.OK)
                {
                    txtBarCodePreScan.Text = FK.tbInputText.Text;
                    if (BarcodeEnter())
                    {

                    }
                }
                else
                {
                    txtBarCodePreScan.Text = string.Empty;
                }
            }
        }

        //텍스트박스 keypress 이벤트
        private void txtBarCodePreScan_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                //절단이 아닌 경우만
                if (m_ProcessID != "0401")
                {
                    if (e.KeyChar == (char)13)
                    {
                        txtBarCodePreScan.Text = txtBarCodePreScan.Text.Trim().ToUpper();
                        if (BarcodeEnter())
                        {

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(ex.Message.ToString(), "[오류]", 0, 1);
            }
        }

        #region 스캔 바코드 기반 데이터 가져오기

        // 스캔 바코드 기반 조건체크 및 데이터 가져오기.
        private bool BarcodeEnter()
        {
            string Barcode = txtBarCodePreScan.Text.Trim();
            double SumQty = 0;

            try
            {
                //마지막은 합계라 마지막 포함 안 함
                for (int i = 0; i < dgdMain.Rows.Count - 1; i++) 
                {
                    BarCodeCheck(Barcode, dgdMain.Rows[i].Cells["ArticleID"].Value.ToString());
                }

                if(m_ArticleID == "")
                {
                    Message[0] = "[스캔오류]";
                    Message[1] = "하위품이 아니거나 잘못된 바코드입니다.";
                    throw new Exception();
                }

                if ((m_LocRemainQty == 0) || (m_LocRemainQty < 0))
                {
                    Message[0] = "[라벨오류]";
                    Message[1] = "입력된 롯트는 현재 재고량이 0이하입니다.\r\n" +
                                   "재고를 모두 소진하였습니다.";
                    throw new Exception();
                }

                for (int i = 0; i < dgdMain.Rows.Count; i++)
                {
                    if (dgdMain.Rows[i].Cells["ArticleID"].Value.ToString() == m_ArticleID)
                    {
                        dgdMain.Rows[i].Cells["Label"].Value = txtBarCodePreScan.Text.ToString();
                        dgdMain.Rows[i].Cells["NowLoc"].Value = Lib.CheckNull(stringFormatN0(m_LocRemainQty)); // 현재고량                        
                        txtBarCodePreScan.Text = "";
                    }

                    if (i < (dgdMain.Rows.Count - 1))
                    {
                        SumQty += Convert.ToDouble(dgdMain.Rows[i].Cells["NowLoc"].Value);
                    }

                }

                dgdMain.Rows[dgdMain.Rows.Count - 1].Cells["NowLoc"].Value = Lib.CheckNull(stringFormatN0(SumQty));

                return true;
            }

            catch (Exception)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                return false;
            }
        }

        #endregion

        #region BarCodeCheck 펑션

        /// <summary>
        /// LotID에 해당하는 ArticleID 가져오기
        /// 하위품 스캔 체크
        /// </summary>
        /// <param name="strBarCode"></param>
        private void BarCodeCheck(string strBarCode, string childArticleID)
        {
            DataRow dr = null;
            try
            {
                // 메시지 초기화
                Message[0] = "";
                Message[1] = "";

                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Add("LotID", strBarCode);
                sqlParameter.Add("ProcessID", m_ProcessID);
                sqlParameter.Add("MachineID", m_MachineID);
                sqlParameter.Add("InstID", Frm_tprc_Main.g_tBase.sInstID);
                sqlParameter.Add("InstDetSeq", Frm_tprc_Main.g_tBase.sInstDetSeq);
                sqlParameter.Add("sArticleID", childArticleID);
                DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_WizWork_sLotInfoByLotID", sqlParameter, false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    dr = dt.Rows[0];

                    m_ArticleID = dr["ArticleID"].ToString().Trim();
                    m_LabelGubun = dr["LabelGubun"].ToString().Trim();                    //라벨구분 1은 원자재 
                    double.TryParse(dr["LocRemainQty"].ToString(), out m_LocRemainQty);   //해당창고재고량

                }
                //else
                //{
                //    // dt.rows count가 0 이야.
                //    // 근데, 여기 들어오는 케이스가 현재 발견된게 지금 3건.
                //    //  1. 입고승인/ 2. stuffinsub 의 outwareyn = y 인 케이스.
                //    //  3. 하위품 사라진 경우(DB삭제)
                //    //  이것들을 가려내야 한다.   (20_0330_허윤구)

                //    //stuffinsub outware y or n
                //    string[] CheckYN = new string[2];
                //    string Query = "select outwareyn from stuffinsub where lotid = '" + strBarCode + "'";
                //    CheckYN = DataStore.Instance.ExecuteQuery(Query, false);
                //    string OutCheckYN = CheckYN[1];

                //    string[] StuffCount = new string[2];
                //    string sql_1 = "select cnt = COUNT(*) from StuffinSub where LotID = '" + strBarCode + "'";
                //    StuffCount = DataStore.Instance.ExecuteQuery(sql_1, false);

                //    string[] DeleteBarcodeYN = new string[2];
                //    string sql_2 = "select cnt = COUNT(*) from wk_result where labelid = '" + strBarCode + "'";
                //    DeleteBarcodeYN = DataStore.Instance.ExecuteQuery(sql_2, false);


                //    if (OutCheckYN == "Y")
                //    {
                //        Message[0] = "[재고소진]";
                //        Message[1] = "입력된 자재는 현재 재고량이 0이하입니다.\r\n" +
                //                       "재고를 모두 소진하였습니다.";
                //        throw new Exception();
                //    }
                //    else if (StuffCount[1] == "0")
                //    {
                //        Message[0] = "[입고 내역 없음]";
                //        Message[1] = "해당 라벨로 입고된 내역이 없습니다.";
                //        throw new Exception();
                //    }
                //    else if (DeleteBarcodeYN[1] == "0")
                //    {
                //        Message[0] = "[하위품 소실]";
                //        Message[1] = "해당 하위품( " + strBarCode + " )은 승인되지 않은 품목이거나 삭제처리된 Lot입니다.";
                //        throw new Exception();
                //    }
                //    else
                //    {
                //        Message[0] = "[입고승인]";
                //        Message[1] = "해당 품목은 승인되지 않은 품목이거나 입고내역이 없는 품목이므로 사용할 수 없습니다.";
                //        throw new Exception();
                //    }
                //}
            }
            catch (Exception)
            {
                m_ArticleID = "";
                m_LabelGubun = "";
                txtBarCodePreScan.Text = "";
                //WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
            }
        }

        #endregion
    }
}
