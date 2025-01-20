using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WizCommon;
using WizWork;

//*******************************************************************************
//프로그램명    Frm_PopUp_Packing.cs
//메뉴ID        
//설명          Frm_PopUp_Packing 메인소스입니다.
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
    public partial class Frm_PopUp_Packing : Form
    {
        string[] Message = new string[2];  // 메시지박스 처리용도.

        WizWorkLib Lib = new WizWorkLib();

        // 메인 데이터 그리드 뷰 관리 객체
        List<Frm_PopUp_Packing_CodeView> lstMain = new List<Frm_PopUp_Packing_CodeView>();
        // 넘어온 총 생산량
        int TotalQty = 0;

        string strOrderID = string.Empty;
        string strCustomID = string.Empty;

        public WizWork.TTag Sub_m_tTag = new WizWork.TTag(); //2021-05-18
        public List<WizWork.TTagSub> list_m_tItem = new List<WizWork.TTagSub>(); //2021-05-18
        private string m_ProcessID = ""; //2021-05-18

        // 불량 리스트
        Dictionary<string, frm_tprc_Work_Defect_U_CodeView> dicDefect = new Dictionary<string, frm_tprc_Work_Defect_U_CodeView>();

        public delegate void DataPassEventHandler(int txtinspectqty, int txtpassqty, int txtdefectqty, int txtRemainQty, int txtqtyperbox, int txtBoxQty); //2021-11-22 포장수량을 Work_U로 넘기기
                                                   //검사수량          합격수량        불량수량            잔량            박스당수량        박스수  
        public event DataPassEventHandler DataPassEvent;        //2021-11-22 포장수량을 Work_U로 넘기기

        public Frm_PopUp_Packing()
        {
            InitializeComponent();
            SetScreen();  //TLP 사이즈 조정
        }

        public Frm_PopUp_Packing(string WorkQty, string NextArticleID)
        {
            InitializeComponent();
            SetScreen();  //TLP 사이즈 조정
            SetCombox(NextArticleID);
            setInitial(WorkQty);
            //SetCombox(NextArticleID);


        }

        private void SetCombox(string NextArticleID)
        {
            try
            {
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                //sqlParameter.Add("LabelID", ""); //2021-05-10 , 2021-08-19 LabelID로 수주의 ArticleID를 찾아서 수주제품으로 찾을 수있게 수정
                //sqlParameter.Add("KCustom", ""); //2021-05-10
                sqlParameter.Add("ArticleID", NextArticleID);
                DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_Packing_Custom", sqlParameter, false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        cboCustom.DataSource = dt;

                        cboCustom.ValueMember = dt.Columns["CustomID"].ToString();

                        cboCustom.DisplayMember = dt.Columns["DvlyPlace"].ToString();

                        cboCustom.SelectedItem = dt.Rows[i].ItemArray[1].ToString();

                        txtQtyPerBox.Text = dt.Rows[i].ItemArray[3].ToString();
                    }

                }
                else
                {
                    WizCommon.Popup.MyMessageBox.ShowBox("조회 대상이 없습니다. \n거래처별등록품목에 등록이 되었는지 확인해주세요.", "[조회 실패]", 3, 1);
                    //WizCommon.Popup.MyMessageBox.ShowBox("조회 대상이 없습니다. \n사무실 프로그램에서 수주등록이 제대로 이루어줬는지 확인해주세요.", "[조회 실패]", 3, 1); //2021-11-08 문구 수정
                }
            }
            catch
            {
                WizCommon.Popup.MyMessageBox.ShowBox("데이터를 불러오지를 못합니다 \n관리자에게 문의해주세요.", "[조회 실패]", 3, 1);
                return;
            }
            finally
            {
                DataStore.Instance.CloseConnection(); //2021-10-07 DB 커넥트 연결 해제
            }
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
        private void Frm_PopUp_Packing_Load(object sender, EventArgs e)
        {
            this.Size = new Size(450, 330);

            //mtb_Date.Text = DateTime.Today.ToString("yyyy-MM-dd");
            //dtInspectTime.Value = DateTime.Now;
        }

        #region 첫 세팅 : setInitial()
        private void setInitial(string WorkQty)
        {
            //lstMain.Clear();
            TotalQty = 0;

            //for (int i = 0; i < lstIns.Count; i++)
            //{
            //    lstMain.Add(new Frm_PopUp_Packing_CodeView(lstIns[i]));
            //    lstMain[i].Num = lstMain.Count;

            //    strOrderID = lstIns[i].OrderID;

            //    TotalQty += Lib.ConvertInt(lstIns[i].NoInspectQty);
            //}

            //BindingSource bs = new BindingSource();
            //bs.DataSource = lstMain;
            //dgdMain.DataSource = bs;
            TotalQty = Lib.ConvertInt(WorkQty); //2021-11-22
            txtInspectQty.Text = Lib.stringFormatN0(WorkQty);
            txtDefectQty.Text = "0";            //2021-11-22
            CalQty();
        }
        #endregion

        #region 검사일자, 검사시간 클릭 이벤트

        // 검사 일자
        private void mtb_Date_Click(object sender, EventArgs e)
        {
            //WizCommon.Popup.Frm_TLP_Calendar calendar = new WizCommon.Popup.Frm_TLP_Calendar(mtb_Date.Text.Replace("-", ""), mtb_Date.Name);
            //calendar.WriteDateTextEvent += new WizCommon.Popup.Frm_TLP_Calendar.TextEventHandler(GetDate);
            //calendar.Owner = this;
            //calendar.ShowDialog();
        }

        //  Calendar.Value -> mtbBox.Text 달력창으로부터 텍스트로 값을 옮겨주는 메소드
        private void GetDate(string strDate, string btnName)
        {
            DateTime dateTime = new DateTime();
            dateTime = DateTime.ParseExact(strDate, "yyyyMMdd", null);
           // mtb_Date.Text = dateTime.ToString("yyyy-MM-dd");
        }

        // 검사 시간
        private void btnInspectTime_Click(object sender, EventArgs e)
        {
            //TimeCheck(dtInspectTime, "검사시간");
        }

        private void TimeCheck(DateTimePicker dtpSender, string Title)
        {
            try
            {
                WizWork.POPUP.Frm_CMNumericKeypad FK = new WizWork.POPUP.Frm_CMNumericKeypad(Title);
                FK.Owner = this;
                string sTime = "";
                DateTime dt = DateTime.Now;
                if (FK.ShowDialog() == DialogResult.OK)
                {
                    sTime = FK.InputTextValue;
                    if (sTime != "")
                    {
                        sTime = sTime.PadRight(6, '0').Substring(0, 6);

                        // 24시간 넘어가면, 0시로 변환
                        if (Lib.ConvertInt(sTime) >= 240000) { sTime = "000000"; }

                        // 시간으로 변환
                        if (DateTime.TryParseExact(sTime.Substring(0, 6), "HHmmss", null, System.Globalization.DateTimeStyles.None, out dt) == false)
                        {
                            dt = DateTime.Now;
                        }
                    }
                    dtpSender.Value = dt;
                }
            }
            catch (Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(ex.Message, "[검사시간 등록 실패]", 0, 1);
                return;
            }
        }

        #endregion

        #region 확인, 취소 버튼 클릭 이벤트
        private void btnOK_Click(object sender, EventArgs e)
        {
            //if (SaveData())
            //{
            //    this.DialogResult = DialogResult.OK;
            //    this.Close();
            //}
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region 저장 구문 SaveData(), CheckData()

        //private bool SaveData()
        //{
        //    bool flag = false;

        //    List<WizCommon.Procedure> Prolist = new List<WizCommon.Procedure>();
        //    List<List<string>> ListProcedureName = new List<List<string>>();
        //    List<Dictionary<string, object>> ListParameter = new List<Dictionary<string, object>>();

        //    Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
        //    Dictionary<string, object> sqlParameter2 = new Dictionary<string, object>();
        //    try
        //    {
        //        if (CheckData() == false) { return false; }

        //        // 저장구문
        //        // 저장시, 검사수량이 0 초과인 건만 하기.
        //        int QtyPerBox = Lib.ConvertInt(txtQtyPerBox.Text); // 박스당 수량
        //        int totInspectQty = Lib.ConvertInt(txtInspectQty.Text); // 검사수량
        //        int DefectQty = Lib.ConvertInt(txtDefectQty.Text); // 불량 수량
        //        int totPassQty = totInspectQty - DefectQty;

        //        // 결과적으로 wk_Packing 에 박스당 수량으로 나눠서 등록이 되어야 됨
        //        // → 줄기처럼 데이터 하나에 엮어서 들어가는게 아니라. 텍스트박스의 수량을 믿고 등록하는 수밖에 없는데...
        //        int BoxQty = Lib.ConvertInt(txtBoxQty.Text);
        //        int PackQty = 0; // 실제 포장 수량

        //        int index = 0;
        //        // wk_Packing 등록
        //        //2021-06-22 전량 불량인 경우가 있어 조건 추가
        //        if (BoxQty == 0)
        //        {
        //            for (int i = 0; i <= BoxQty; i++)
        //            {
        //                //// 마지막은 잔량만
        //                //if (totPassQty > 0
        //                //    && i == BoxQty - 1)
        //                //{
        //                //    PackQty = QtyPerBox == 0 ? totPassQty : (totPassQty % QtyPerBox == 0 ? QtyPerBox : totPassQty % QtyPerBox);
        //                //}
        //                //else
        //                //{
        //                //    PackQty = QtyPerBox;
        //                //}

        //                sqlParameter = new Dictionary<string, object>();
        //                sqlParameter.Add("PackID", "");
        //                sqlParameter.Add("nseq", ++index);
        //                sqlParameter.Add("ArticleID", dgdMain.Rows[0].Cells["ArticleID"].Value.ToString().Trim());
        //                sqlParameter.Add("OrderID", dgdMain.Rows[0].Cells["OrderID"].Value.ToString()); //2021-06-09 포장품명을 새롭게 생성하여 마지막 공정의 후의 ArticleID가 필요해서 추가
        //                sqlParameter.Add("sPackDate", mtb_Date.Text.Replace("-", ""));
        //                sqlParameter.Add("nPackqty", PackQty);
        //                sqlParameter.Add("PackCustomID", cboCustom.SelectedValue.ToString());
        //                sqlParameter.Add("sPackPersonID", Frm_tins_Main.g_tBase.PersonID);


        //                WizCommon.Procedure pro1 = new WizCommon.Procedure();
        //                pro1.Name = "[xp_prdIns_iWkPacking]";
        //                pro1.OutputUseYN = (i == 0 ? "Y" : "N");
        //                pro1.OutputName = "PackID";
        //                pro1.OutputLength = "10";

        //                Prolist.Add(pro1);
        //                ListParameter.Add(sqlParameter);
        //            }
        //            index = 0;
        //            // wk_PackingCardList 에는 투입 라벨만 들어가면 되고.
        //            // wk_Inspect 에는 각 라벨이 들어가야 됨.
        //            for (int i = 0; i < dgdMain.Rows.Count; i++)
        //            {
        //                int PassQty = Lib.ConvertInt(dgdMain.Rows[i].Cells["PassQty"].Value.ToString());

        //                // 합격수량으로만 포장을 하니, 합격수량이 0 초과인 것들만 포장에 넣기.
        //                if (PassQty >= 0)
        //                {
        //                    // wk_PackingCardList
        //                    sqlParameter = new Dictionary<string, object>();

        //                    sqlParameter.Add("PackID", "");
        //                    sqlParameter.Add("nCardSeq", ++index);
        //                    sqlParameter.Add("sCardID", dgdMain.Rows[i].Cells["LabelID"].Value.ToString());
        //                    sqlParameter.Add("nProdQty", PassQty);
        //                    sqlParameter.Add("sCreateUserID", Frm_tins_Main.g_tBase.PersonID);
        //                    //sqlParameter.Add("OrderID", dgdMain.Rows[i].Cells["OrderID"].Value.ToString()); //2021-05-20
        //                    //sqlParameter.Add("UnitClss", ""); //2021-05-20
        //                    //sqlParameter.Add("RollSeq", 0);  //2021-05-20
        //                    //sqlParameter.Add("ExamDate", mtb_Date.Text.Replace("-", ""));       //2021-05-20
        //                    //sqlParameter.Add("ExamTime", dtInspectTime.Value.ToString("HHmmss"));   //2021-05-20
        //                    //sqlParameter.Add("DefectQty", txtDefectQty.Text);   //2021-05-20
        //                    WizCommon.Procedure pro2 = new WizCommon.Procedure();
        //                    pro2.Name = "[xp_prdIns_iWkPackingCardList]";
        //                    pro2.OutputUseYN = "N";
        //                    pro2.OutputName = "PackID";
        //                    pro2.OutputLength = "20";

        //                    Prolist.Add(pro2);
        //                    ListParameter.Add(sqlParameter);
        //                }

        //                // Inspect 넣기
        //                int InspectQty = Lib.ConvertInt(dgdMain.Rows[i].Cells["InspectQty"].Value.ToString());

        //                //index = 0;

        //                // 검사수량이 0 이상인 건들만 넣기
        //                if (InspectQty >= 0)
        //                {
        //                    sqlParameter = new Dictionary<string, object>();

        //                    sqlParameter.Add("OrderID", dgdMain.Rows[i].Cells["OrderID"].Value.ToString());
        //                    sqlParameter.Add("RollSeq", 0);
        //                    sqlParameter.Add("OrderSeq", 1);
        //                    sqlParameter.Add("RollNo", 0);
        //                    sqlParameter.Add("ExamNO", "00");
        //                    sqlParameter.Add("ExamDate", mtb_Date.Text.Replace("-", ""));

        //                    sqlParameter.Add("ExamTime", dtInspectTime.Value.ToString("HHmmss"));
        //                    sqlParameter.Add("TeamID", "");
        //                    sqlParameter.Add("PersonID", Frm_tins_Main.g_tBase.PersonID);
        //                    sqlParameter.Add("RealQty", Lib.ConvertInt(dgdMain.Rows[i].Cells["InspectQty"].Value.ToString()));
        //                    sqlParameter.Add("CtrlQty", Lib.ConvertInt(dgdMain.Rows[i].Cells["PassQty"].Value.ToString()));

        //                    sqlParameter.Add("UnitClss", "");
        //                    sqlParameter.Add("GradeID", "1");
        //                    sqlParameter.Add("LotNo", "");
        //                    sqlParameter.Add("BoxID", dgdMain.Rows[i].Cells["LabelID"].Value.ToString());
        //                    sqlParameter.Add("DefectQty", Lib.ConvertInt(dgdMain.Rows[i].Cells["DefectQty"].Value.ToString()));

        //                    sqlParameter.Add("DefectPoint", 0);
        //                    sqlParameter.Add("DefectID", "");
        //                    sqlParameter.Add("DefectClss", "");
        //                    sqlParameter.Add("InstID", "");
        //                    sqlParameter.Add("CardIDList", "");

        //                    sqlParameter.Add("CreateUserID", Frm_tins_Main.g_tBase.PersonID);
        //                    sqlParameter.Add("PackID", "");

        //                    WizCommon.Procedure pro3 = new WizCommon.Procedure();
        //                    pro3.Name = "[xp_prdIns_iInspectFinal]";
        //                    pro3.OutputUseYN = "Y";
        //                    pro3.OutputName = "RollSeq";
        //                    pro3.OutputLength = "20";

        //                    Prolist.Add(pro3);
        //                    ListParameter.Add(sqlParameter);

        //                    var lstDefect = dgdMain.Rows[i].Cells["lstDefect"].Value as Dictionary<string, frm_tprc_Work_Defect_U_CodeView>;
        //                    if (lstDefect != null)
        //                    {
        //                        int k = 0;
        //                        foreach (string Key in lstDefect.Keys)
        //                        {
        //                            var Defect = lstDefect[Key] as frm_tprc_Work_Defect_U_CodeView;
        //                            if (Defect != null
        //                                && Lib.ConvertInt(Defect.DefectQty) > 0)
        //                            {
        //                                sqlParameter = new Dictionary<string, object>();

        //                                sqlParameter.Add("OrderID", dgdMain.Rows[i].Cells["OrderID"].Value.ToString());
        //                                sqlParameter.Add("RollSeq", 0);
        //                                sqlParameter.Add("DefectSeq", ++k);
        //                                sqlParameter.Add("DefectID", Key);
        //                                sqlParameter.Add("DefectQty", Lib.ConvertInt(Defect.DefectQty));

        //                                sqlParameter.Add("PersonID", Frm_tins_Main.g_tBase.PersonID);
        //                                sqlParameter.Add("PackID", "");

        //                                WizCommon.Procedure pro4 = new WizCommon.Procedure();
        //                                pro4.Name = "[xp_prdIns_iInspectSub]";
        //                                pro4.OutputUseYN = "N";
        //                                pro4.OutputName = "OrderID";
        //                                pro4.OutputLength = "20";

        //                                Prolist.Add(pro4);
        //                                ListParameter.Add(sqlParameter);
        //                            }
        //                        }
        //                    }

        //                    // 예외출고 구문
        //                    //sqlParameter = new Dictionary<string, object>();

        //                    //sqlParameter.Add("OrderID", dgdMain.Rows[i].Cells["OrderID"].Value.ToString());
        //                    //sqlParameter.Add("RollSeq", 0);

        //                    //WizCommon.Procedure pro5 = new WizCommon.Procedure();
        //                    //pro5.Name = "[xp_prdIns_iInspect_ExcptOut]";
        //                    //pro5.OutputUseYN = "N";
        //                    //pro5.OutputName = "JobID";
        //                    //pro5.OutputLength = "20";

        //                    //Prolist.Add(pro5);
        //                    //ListParameter.Add(sqlParameter);

        //                }
        //            }
        //        }
        //        else
        //        {
        //            for (int i = 0; i < BoxQty; i++)
        //            {
        //                // 마지막은 잔량만
        //                if (totPassQty > 0
        //                    && i == BoxQty - 1)
        //                {
        //                    PackQty = QtyPerBox == 0 ? totPassQty : (totPassQty % QtyPerBox == 0 ? QtyPerBox : totPassQty % QtyPerBox);
        //                }
        //                else
        //                {
        //                    PackQty = QtyPerBox;
        //                }

        //                sqlParameter = new Dictionary<string, object>();
        //                sqlParameter.Add("PackID", "");
        //                sqlParameter.Add("nseq", ++index);
        //                sqlParameter.Add("ArticleID", dgdMain.Rows[0].Cells["ArticleID"].Value.ToString().Trim());
        //                sqlParameter.Add("OrderID", dgdMain.Rows[0].Cells["OrderID"].Value.ToString()); //2021-06-09 포장품명을 새롭게 생성하여 마지막 공정의 후의 ArticleID가 필요해서 추가
        //                sqlParameter.Add("sPackDate", mtb_Date.Text.Replace("-", ""));
        //                sqlParameter.Add("nPackqty", PackQty);
        //                sqlParameter.Add("PackCustomID", cboCustom.SelectedValue.ToString());
        //                sqlParameter.Add("sPackPersonID", Frm_tins_Main.g_tBase.PersonID);


        //                WizCommon.Procedure pro1 = new WizCommon.Procedure();
        //                pro1.Name = "[xp_prdIns_iWkPacking]";
        //                pro1.OutputUseYN = (i == 0 ? "Y" : "N");
        //                pro1.OutputName = "PackID";
        //                pro1.OutputLength = "10";

        //                Prolist.Add(pro1);
        //                ListParameter.Add(sqlParameter);
        //            }
        //            index = 0;
        //            // wk_PackingCardList 에는 투입 라벨만 들어가면 되고.
        //            // wk_Inspect 에는 각 라벨이 들어가야 됨.
        //            for (int i = 0; i < dgdMain.Rows.Count; i++)
        //            {
        //                int PassQty = Lib.ConvertInt(dgdMain.Rows[i].Cells["PassQty"].Value.ToString());

        //                // 합격수량으로만 포장을 하니, 합격수량이 0 초과인 것들만 포장에 넣기.
        //                if (PassQty > 0)
        //                {
        //                    // wk_PackingCardList
        //                    sqlParameter = new Dictionary<string, object>();

        //                    sqlParameter.Add("PackID", "");
        //                    sqlParameter.Add("nCardSeq", ++index);
        //                    sqlParameter.Add("sCardID", dgdMain.Rows[i].Cells["LabelID"].Value.ToString());
        //                    sqlParameter.Add("nProdQty", PassQty);
        //                    sqlParameter.Add("sCreateUserID", Frm_tins_Main.g_tBase.PersonID);
        //                    //sqlParameter.Add("OrderID", dgdMain.Rows[i].Cells["OrderID"].Value.ToString()); //2021-05-20
        //                    //sqlParameter.Add("UnitClss", ""); //2021-05-20
        //                    //sqlParameter.Add("RollSeq", 0);  //2021-05-20
        //                    //sqlParameter.Add("ExamDate", mtb_Date.Text.Replace("-", ""));       //2021-05-20
        //                    //sqlParameter.Add("ExamTime", dtInspectTime.Value.ToString("HHmmss"));   //2021-05-20
        //                    //sqlParameter.Add("DefectQty", txtDefectQty.Text);   //2021-05-20
        //                    WizCommon.Procedure pro2 = new WizCommon.Procedure();
        //                    pro2.Name = "[xp_prdIns_iWkPackingCardList]";
        //                    pro2.OutputUseYN = "N";
        //                    pro2.OutputName = "PackID";
        //                    pro2.OutputLength = "20";

        //                    Prolist.Add(pro2);
        //                    ListParameter.Add(sqlParameter);
        //                }

        //                // Inspect 넣기
        //                int InspectQty = Lib.ConvertInt(dgdMain.Rows[i].Cells["InspectQty"].Value.ToString());

        //                //index = 0;

        //                // 검사수량이 0 이상인 건들만 넣기
        //                if (InspectQty > 0)
        //                {
        //                    sqlParameter = new Dictionary<string, object>();

        //                    sqlParameter.Add("OrderID", dgdMain.Rows[i].Cells["OrderID"].Value.ToString());
        //                    sqlParameter.Add("RollSeq", 0);
        //                    sqlParameter.Add("OrderSeq", 1);
        //                    sqlParameter.Add("RollNo", 0);
        //                    sqlParameter.Add("ExamNO", "00");
        //                    sqlParameter.Add("ExamDate", mtb_Date.Text.Replace("-", ""));

        //                    sqlParameter.Add("ExamTime", dtInspectTime.Value.ToString("HHmmss"));
        //                    sqlParameter.Add("TeamID", "");
        //                    sqlParameter.Add("PersonID", Frm_tins_Main.g_tBase.PersonID);
        //                    sqlParameter.Add("RealQty", Lib.ConvertInt(dgdMain.Rows[i].Cells["InspectQty"].Value.ToString()));
        //                    sqlParameter.Add("CtrlQty", Lib.ConvertInt(dgdMain.Rows[i].Cells["PassQty"].Value.ToString()));

        //                    sqlParameter.Add("UnitClss", "");
        //                    sqlParameter.Add("GradeID", "1");
        //                    sqlParameter.Add("LotNo", "");
        //                    sqlParameter.Add("BoxID", dgdMain.Rows[i].Cells["LabelID"].Value.ToString());
        //                    sqlParameter.Add("DefectQty", Lib.ConvertInt(dgdMain.Rows[i].Cells["DefectQty"].Value.ToString()));

        //                    sqlParameter.Add("DefectPoint", 0);
        //                    sqlParameter.Add("DefectID", "");
        //                    sqlParameter.Add("DefectClss", "");
        //                    sqlParameter.Add("InstID", "");
        //                    sqlParameter.Add("CardIDList", "");

        //                    sqlParameter.Add("CreateUserID", Frm_tins_Main.g_tBase.PersonID);
        //                    sqlParameter.Add("PackID", "");

        //                    WizCommon.Procedure pro3 = new WizCommon.Procedure();
        //                    pro3.Name = "[xp_prdIns_iInspectFinal]";
        //                    pro3.OutputUseYN = "Y";
        //                    pro3.OutputName = "RollSeq";
        //                    pro3.OutputLength = "20";

        //                    Prolist.Add(pro3);
        //                    ListParameter.Add(sqlParameter);

        //                    var lstDefect = dgdMain.Rows[i].Cells["lstDefect"].Value as Dictionary<string, frm_tprc_Work_Defect_U_CodeView>;
        //                    if (lstDefect != null)
        //                    {
        //                        int k = 0;
        //                        foreach (string Key in lstDefect.Keys)
        //                        {
        //                            var Defect = lstDefect[Key] as frm_tprc_Work_Defect_U_CodeView;
        //                            if (Defect != null
        //                                && Lib.ConvertInt(Defect.DefectQty) > 0)
        //                            {
        //                                sqlParameter = new Dictionary<string, object>();

        //                                sqlParameter.Add("OrderID", dgdMain.Rows[i].Cells["OrderID"].Value.ToString());
        //                                sqlParameter.Add("RollSeq", 0);
        //                                sqlParameter.Add("DefectSeq", ++k);
        //                                sqlParameter.Add("DefectID", Key);
        //                                sqlParameter.Add("DefectQty", Lib.ConvertInt(Defect.DefectQty));

        //                                sqlParameter.Add("PersonID", Frm_tins_Main.g_tBase.PersonID);
        //                                sqlParameter.Add("PackID", "");

        //                                WizCommon.Procedure pro4 = new WizCommon.Procedure();
        //                                pro4.Name = "[xp_prdIns_iInspectSub]";
        //                                pro4.OutputUseYN = "N";
        //                                pro4.OutputName = "OrderID";
        //                                pro4.OutputLength = "20";

        //                                Prolist.Add(pro4);
        //                                ListParameter.Add(sqlParameter);
        //                            }
        //                        }
        //                    }

        //                    // 예외출고 구문
        //                    //sqlParameter = new Dictionary<string, object>();

        //                    //sqlParameter.Add("OrderID", dgdMain.Rows[i].Cells["OrderID"].Value.ToString());
        //                    //sqlParameter.Add("RollSeq", 0);

        //                    //WizCommon.Procedure pro5 = new WizCommon.Procedure();
        //                    //pro5.Name = "[xp_prdIns_iInspect_ExcptOut]";
        //                    //pro5.OutputUseYN = "N";
        //                    //pro5.OutputName = "JobID";
        //                    //pro5.OutputLength = "20";

        //                    //Prolist.Add(pro5);
        //                    //ListParameter.Add(sqlParameter);

        //                }
        //            }
        //        }
               
        //        //2021-05-22 검사포장 재고 생성
        //        sqlParameter2 = new Dictionary<string, object>();

        //        sqlParameter2.Add("PackID", "");
        //        sqlParameter2.Add("ArticleID", dgdMain.Rows[0].Cells["ArticleID"].Value.ToString().Trim()); //2021-06-08 포장재고를 생성하고 하위품을 재고 소진 하기 위해 추가
        //        sqlParameter2.Add("CreateUserID", Frm_tins_Main.g_tBase.PersonID);
        //        //sqlParameter.Add("OrderID", dgdMain.Rows[i].Cells["OrderID"].Value.ToString()); //2021-05-20
        //        //sqlParameter.Add("UnitClss", ""); //2021-05-20
        //        //sqlParameter.Add("RollSeq", 0);  //2021-05-20
        //        //sqlParameter.Add("ExamDate", mtb_Date.Text.Replace("-", ""));       //2021-05-20
        //        //sqlParameter.Add("ExamTime", dtInspectTime.Value.ToString("HHmmss"));   //2021-05-20
        //        //sqlParameter.Add("DefectQty", txtDefectQty.Text);   //2021-05-20
        //        WizCommon.Procedure pro5 = new WizCommon.Procedure();
        //        pro5.Name = "[xp_prdIns_iStuffinOutware]";
        //        pro5.OutputUseYN = "N";
        //        pro5.OutputName = "PackID";
        //        pro5.OutputLength = "20";

        //        Prolist.Add(pro5);
        //        ListParameter.Add(sqlParameter2);

        //        List<KeyValue> list_Result = new List<KeyValue>();
        //        list_Result = DataStore.Instance.ExecuteAllProcedureOutputToCS(Prolist, ListParameter);

        //        if (list_Result[0].key.ToLower() == "success")
        //        {
        //            WizCommon.Popup.MyMessageBox.ShowBox("검사 등록이 성공적으로 완료되었습니다.", "[저장 완료]", 3, 1);


        //            foreach (KeyValue kv in list_Result)
        //            {
        //                if (kv.key.ToLower() == "packid")
        //                {
        //                    // 라벨 발행
        //                    LabelPrint(kv.value.ToString().Trim());
        //                }
        //            }
                    
        //            return true;
        //        }
        //        else
        //        {
        //            foreach (KeyValue kv in list_Result)
        //            {
        //                if (kv.key.ToLower() == "failure")
        //                {
        //                    throw new Exception(kv.value.ToString());
        //                }
        //            }
        //            return false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        WizCommon.Popup.MyMessageBox.ShowBox(ex.Message, "[저장 실패 : SaveData부분]", 3, 1);
        //    }

        //    return flag;
        //}


        private bool LabelPrint(string PackID)
        {
            bool flag = true;
            
            List<string> list_Data = null;
            try
            {
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Add("PackID", PackID);//상위품ID

                DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_prdIns_sBLabelInfo_ByPackID", sqlParameter, false);

                foreach (DataRow dr in dt.Rows)
                {
                    string g_sPrinterName = Lib.GetDefaultPrinter();

                    list_Data = new List<string>();

                    string TagID = "010"; //2021-05-10 GSL(mt_tag에서 확인)

                    list_Data.Add(Lib.CheckNull(dr["InBoxID"].ToString()));// 공정라벨
                    list_Data.Add(Lib.CheckNull(dr["KCustom"].ToString()));// 업체명
                    //list_Data.Add(Lib.CheckNull(dr["Article"].ToString()));// 품명
                    list_Data.Add(Lib.CheckNull(dr["BuyerArticleNo"].ToString()));// 품번
                    list_Data.Add(Lib.stringFormatN0(Lib.ConvertDouble(Lib.CheckNull(dr["PackQty"].ToString()))) + " EA");// 수량
                    list_Data.Add(Lib.CheckNull(dr["Name"].ToString()));// 작업자
                    list_Data.Add(Lib.DatePickerFormat(Lib.CheckNull(dr["PackDate"].ToString())));// 일자
                    //list_Data.Add(Lib.stringFormatN0(Lib.ConvertDouble(Lib.CheckNull(dr["PackQty"].ToString()))));// 수량
                    //list_Data.Add(Lib.CheckNull(dr["Name"].ToString()));// 작업자
                    list_Data.Add(Lib.stringFormatN0(Lib.ConvertDouble(Lib.CheckNull(dr["DefectQty"].ToString()))));// 불량수량 YS 제거요청으로 인한 주석처리 2020.11.21.KGH

                    //frm_tprc_Work_U ftWU = new frm_tprc_Work_U(); 2021-05-19 프린트 함수 여기에 따라 추가해서 프린터 되게 만듬

                    WizWork.TSCLIB_DLL.openport(g_sPrinterName);
                    if (SendWindowDllCommand(list_Data, TagID, 1, 0))
                    {
                        Message[0] = "[라벨발행 중]";
                        Message[1] = "라벨 발행중입니다. 잠시만 기다려주세요.";
                        WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 2, 2);
                    }

                    else
                    {
                        Message[0] = "[라벨발행 실패]";
                        Message[1] = "라벨 발행에 실패했습니다. 관리자에게 문의하여주세요.\r\n<SendWindowDllCommand>";
                        WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 2, 2);
                    }
                    WizWork.TSCLIB_DLL.closeport();
                }
            }
            catch (Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", ex.Message), "[오류]", 0, 1);
            }
            finally
            {
                DataStore.Instance.CloseConnection(); //2021-10-07 DB 커넥트 연결 해제
            }

            return flag;
        }

        // 저장 전 체크
        private bool CheckData()
        {
            //if (dgdMain.Rows.Count == 0)
            //{
            //    WizCommon.Popup.MyMessageBox.ShowBox("검사할 수 있는 라벨이 없습니다.", "저장 전", 0, 1);
            //    return false;
            //}

            // 검사수량이 0 이면 막기.
            if (Lib.ConvertInt(txtInspectQty.Text) == 0)
            {
                WizCommon.Popup.MyMessageBox.ShowBox("검사 수량이 0 입니다.", "저장 전", 0, 1);
                return false;
            }

            // 합격수량이 0 미만이면 저장 못하게 막기!
            if (Lib.ConvertInt(txtPassQty.Text) < 0)
            {
                WizCommon.Popup.MyMessageBox.ShowBox("합격 수량이 0 미만 일 수 없습니다.\r\n(검사수량, 불량수량을 확인해주세요.)", "저장 전", 0, 1);
                return false;
            }

            // 불량수량 텍스트박스가 "0"이 아닌 "" → 불량 검사를 안했다는 것이니, 메시지 띄우기
            if (txtDefectQty.Text.Equals(""))
            {
                WizCommon.Popup.MyMessageBox.ShowBox("불량 검사를 진행해주세요.", "저장 전", 0, 1);
                return false;
            }

            if (cboCustom.SelectedIndex.Equals(-1) || cboCustom.SelectedValue.Equals(""))
            {
                WizCommon.Popup.MyMessageBox.ShowBox("고객사를 선택해주세요.", "저장 전", 0, 1);
                return false;
            }

            if (CheckQty() == false) //2021-08-21 재고 체크
            {
                return false;
            }

            return true;
        }

        #endregion

        #region 재고 확인
        private bool CheckQty() //2021-08-21
        {
            //try
            //{
            //    float StockQty = 0;

            //    for (int i = 0; i < dgdMain.Rows.Count; i++)
            //    {
            //        Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
            //        sqlParameter.Add("ArticleID", dgdMain.Rows[i].Cells["ArticleID"].Value.ToString().Trim());//현재품ID
            //        sqlParameter.Add("LabelID", dgdMain.Rows[i].Cells["LabelID"].Value.ToString().Trim());//현재품LabelID
            //        DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_prdIns_mtr_sLotStockLoc", sqlParameter, false);

            //        if (dt != null
            //            && dt.Rows.Count > 0)
            //        {
            //            foreach (DataRow dr in dt.Rows)
            //            {
            //                StockQty = float.Parse(dr["StockQty"].ToString());

            //                if (0 >= StockQty)
            //                {
            //                    WizCommon.Popup.MyMessageBox.ShowBox("해당" + dr["LoTID"].ToString() + "라벨의 재고가 0 입니다. 라벨을 다시 스캔 해주세요", "[등록 오류]", 0, 1);
            //                    return false;
            //                }
            //            }
            //        }
            //    }
            //}
            //catch(Exception e)
            //{
            //    WizCommon.Popup.MyMessageBox.ShowBox("오류", "[등록 오류]", 0, 1);
            //    return false;
            //}
            //finally
            //{
            //    DataStore.Instance.CloseConnection(); //2021-10-07 DB 커넥트 연결 해제
            //}

            return true;
        }
        #endregion

        private bool SaveNotPrintData()
        {
            bool flag = true;

            //List<WizCommon.Procedure> Prolist = new List<WizCommon.Procedure>();
            //List<List<string>> ListProcedureName = new List<List<string>>();
            //List<Dictionary<string, object>> ListParameter = new List<Dictionary<string, object>>();

            //Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
            //Dictionary<string, object> sqlParameter2 = new Dictionary<string, object>();
            try
            {
                if (CheckData() == false) { return false; }

                // 저장구문
                // 저장시, 검사수량이 0 초과인 건만 하기.
                int QtyPerBox = Lib.ConvertInt(txtQtyPerBox.Text); // 박스당 수량
                int totInspectQty = Lib.ConvertInt(txtInspectQty.Text); // 검사수량
                int DefectQty = Lib.ConvertInt(txtDefectQty.Text); // 불량 수량
                int totPassQty = totInspectQty - DefectQty;

                // 결과적으로 wk_Packing 에 박스당 수량으로 나눠서 등록이 되어야 됨
                // → 줄기처럼 데이터 하나에 엮어서 들어가는게 아니라. 텍스트박스의 수량을 믿고 등록하는 수밖에 없는데...
                int BoxQty = Lib.ConvertInt(txtBoxQty.Text);
                int PackQty = 0; // 실제 포장 수량

                DataPassEvent(totInspectQty, totPassQty, DefectQty, Lib.ConvertInt(txtRemainQty.Text), QtyPerBox, BoxQty);
                //int index = 0;

                #region 2021-11-22 프린터 구문 주석
                // wk_Packing 등록
                //2021-06-22 전량 불량인 경우가 있어 조건 추가
                //if (BoxQty == 0)
                //{
                //    for (int i = 0; i <= BoxQty; i++)
                //    {
                //        //// 마지막은 잔량만
                //        //if (totPassQty > 0
                //        //    && i == BoxQty - 1)
                //        //{
                //        //    PackQty = QtyPerBox == 0 ? totPassQty : (totPassQty % QtyPerBox == 0 ? QtyPerBox : totPassQty % QtyPerBox);
                //        //}
                //        //else
                //        //{
                //        //    PackQty = QtyPerBox;
                //        //}

                //        sqlParameter = new Dictionary<string, object>();
                //        sqlParameter.Add("PackID", "");
                //        sqlParameter.Add("nseq", ++index);
                //        sqlParameter.Add("ArticleID", dgdMain.Rows[0].Cells["ArticleID"].Value.ToString().Trim());
                //        sqlParameter.Add("OrderID", dgdMain.Rows[0].Cells["OrderID"].Value.ToString()); //2021-06-09 포장품명을 새롭게 생성하여 마지막 공정의 후의 ArticleID가 필요해서 추가
                //        sqlParameter.Add("sPackDate", mtb_Date.Text.Replace("-", ""));
                //        sqlParameter.Add("nPackqty", PackQty);
                //        sqlParameter.Add("PackCustomID", cboCustom.SelectedValue.ToString());
                //        sqlParameter.Add("sPackPersonID", Frm_tins_Main.g_tBase.PersonID);


                //        WizCommon.Procedure pro1 = new WizCommon.Procedure();
                //        pro1.Name = "[xp_prdIns_iWkPacking]";
                //        pro1.OutputUseYN = (i == 0 ? "Y" : "N");
                //        pro1.OutputName = "PackID";
                //        pro1.OutputLength = "10";

                //        Prolist.Add(pro1);
                //        ListParameter.Add(sqlParameter);
                //    }
                //    index = 0;
                //    // wk_PackingCardList 에는 투입 라벨만 들어가면 되고.
                //    // wk_Inspect 에는 각 라벨이 들어가야 됨.
                //    for (int i = 0; i < dgdMain.Rows.Count; i++)
                //    {
                //        int PassQty = Lib.ConvertInt(dgdMain.Rows[i].Cells["PassQty"].Value.ToString());

                //        // 합격수량으로만 포장을 하니, 합격수량이 0 초과인 것들만 포장에 넣기.
                //        if (PassQty >= 0)
                //        {
                //            // wk_PackingCardList
                //            sqlParameter = new Dictionary<string, object>();

                //            sqlParameter.Add("PackID", "");
                //            sqlParameter.Add("nCardSeq", ++index);
                //            sqlParameter.Add("sCardID", dgdMain.Rows[i].Cells["LabelID"].Value.ToString());
                //            sqlParameter.Add("nProdQty", PassQty);
                //            sqlParameter.Add("sCreateUserID", Frm_tins_Main.g_tBase.PersonID);
                //            //sqlParameter.Add("OrderID", dgdMain.Rows[i].Cells["OrderID"].Value.ToString()); //2021-05-20
                //            //sqlParameter.Add("UnitClss", ""); //2021-05-20
                //            //sqlParameter.Add("RollSeq", 0);  //2021-05-20
                //            //sqlParameter.Add("ExamDate", mtb_Date.Text.Replace("-", ""));       //2021-05-20
                //            //sqlParameter.Add("ExamTime", dtInspectTime.Value.ToString("HHmmss"));   //2021-05-20
                //            //sqlParameter.Add("DefectQty", txtDefectQty.Text);   //2021-05-20
                //            WizCommon.Procedure pro2 = new WizCommon.Procedure();
                //            pro2.Name = "[xp_prdIns_iWkPackingCardList]";
                //            pro2.OutputUseYN = "N";
                //            pro2.OutputName = "PackID";
                //            pro2.OutputLength = "20";

                //            Prolist.Add(pro2);
                //            ListParameter.Add(sqlParameter);
                //        }

                //        // Inspect 넣기
                //        int InspectQty = Lib.ConvertInt(dgdMain.Rows[i].Cells["InspectQty"].Value.ToString());

                //        //index = 0;

                //        // 검사수량이 0 이상인 건들만 넣기
                //        if (InspectQty >= 0)
                //        {
                //            sqlParameter = new Dictionary<string, object>();

                //            sqlParameter.Add("OrderID", dgdMain.Rows[i].Cells["OrderID"].Value.ToString());
                //            sqlParameter.Add("RollSeq", 0);
                //            sqlParameter.Add("OrderSeq", 1);
                //            sqlParameter.Add("RollNo", 0);
                //            sqlParameter.Add("ExamNO", "00");
                //            sqlParameter.Add("ExamDate", mtb_Date.Text.Replace("-", ""));

                //            sqlParameter.Add("ExamTime", dtInspectTime.Value.ToString("HHmmss"));
                //            sqlParameter.Add("TeamID", "");
                //            sqlParameter.Add("PersonID", Frm_tins_Main.g_tBase.PersonID);
                //            sqlParameter.Add("RealQty", Lib.ConvertInt(dgdMain.Rows[i].Cells["InspectQty"].Value.ToString()));
                //            sqlParameter.Add("CtrlQty", Lib.ConvertInt(dgdMain.Rows[i].Cells["PassQty"].Value.ToString()));

                //            sqlParameter.Add("UnitClss", "");
                //            sqlParameter.Add("GradeID", "1");
                //            sqlParameter.Add("LotNo", "");
                //            sqlParameter.Add("BoxID", dgdMain.Rows[i].Cells["LabelID"].Value.ToString());
                //            sqlParameter.Add("DefectQty", Lib.ConvertInt(dgdMain.Rows[i].Cells["DefectQty"].Value.ToString()));

                //            sqlParameter.Add("DefectPoint", 0);
                //            sqlParameter.Add("DefectID", "");
                //            sqlParameter.Add("DefectClss", "");
                //            sqlParameter.Add("InstID", "");
                //            sqlParameter.Add("CardIDList", "");

                //            sqlParameter.Add("CreateUserID", Frm_tins_Main.g_tBase.PersonID);
                //            sqlParameter.Add("PackID", "");

                //            WizCommon.Procedure pro3 = new WizCommon.Procedure();
                //            pro3.Name = "[xp_prdIns_iInspectFinal]";
                //            pro3.OutputUseYN = "Y";
                //            pro3.OutputName = "RollSeq";
                //            pro3.OutputLength = "20";

                //            Prolist.Add(pro3);
                //            ListParameter.Add(sqlParameter);

                //            var lstDefect = dgdMain.Rows[i].Cells["lstDefect"].Value as Dictionary<string, frm_tprc_Work_Defect_U_CodeView>;
                //            if (lstDefect != null)
                //            {
                //                int k = 0;
                //                foreach (string Key in lstDefect.Keys)
                //                {
                //                    var Defect = lstDefect[Key] as frm_tprc_Work_Defect_U_CodeView;
                //                    if (Defect != null
                //                        && Lib.ConvertInt(Defect.DefectQty) > 0)
                //                    {
                //                        sqlParameter = new Dictionary<string, object>();

                //                        sqlParameter.Add("OrderID", dgdMain.Rows[i].Cells["OrderID"].Value.ToString());
                //                        sqlParameter.Add("RollSeq", 0);
                //                        sqlParameter.Add("DefectSeq", ++k);
                //                        sqlParameter.Add("DefectID", Key);
                //                        sqlParameter.Add("DefectQty", Lib.ConvertInt(Defect.DefectQty));

                //                        sqlParameter.Add("PersonID", Frm_tins_Main.g_tBase.PersonID);
                //                        sqlParameter.Add("PackID", "");

                //                        WizCommon.Procedure pro4 = new WizCommon.Procedure();
                //                        pro4.Name = "[xp_prdIns_iInspectSub]";
                //                        pro4.OutputUseYN = "N";
                //                        pro4.OutputName = "OrderID";
                //                        pro4.OutputLength = "20";

                //                        Prolist.Add(pro4);
                //                        ListParameter.Add(sqlParameter);
                //                    }
                //                }
                //            }

                //            // 예외출고 구문
                //            //sqlParameter = new Dictionary<string, object>();

                //            //sqlParameter.Add("OrderID", dgdMain.Rows[i].Cells["OrderID"].Value.ToString());
                //            //sqlParameter.Add("RollSeq", 0);

                //            //WizCommon.Procedure pro5 = new WizCommon.Procedure();
                //            //pro5.Name = "[xp_prdIns_iInspect_ExcptOut]";
                //            //pro5.OutputUseYN = "N";
                //            //pro5.OutputName = "JobID";
                //            //pro5.OutputLength = "20";

                //            //Prolist.Add(pro5);
                //            //ListParameter.Add(sqlParameter);

                //        }
                //    }
                //}
                //else
                //{
                //    for (int i = 0; i < BoxQty; i++)
                //    {
                //        // 마지막은 잔량만
                //        if (totPassQty > 0
                //            && i == BoxQty - 1)
                //        {
                //            PackQty = QtyPerBox == 0 ? totPassQty : (totPassQty % QtyPerBox == 0 ? QtyPerBox : totPassQty % QtyPerBox);
                //        }
                //        else
                //        {
                //            PackQty = QtyPerBox;
                //        }

                //        sqlParameter = new Dictionary<string, object>();
                //        sqlParameter.Add("PackID", "");
                //        sqlParameter.Add("nseq", ++index);
                //        sqlParameter.Add("ArticleID", dgdMain.Rows[0].Cells["ArticleID"].Value.ToString().Trim());
                //        sqlParameter.Add("OrderID", dgdMain.Rows[0].Cells["OrderID"].Value.ToString()); //2021-06-09 포장품명을 새롭게 생성하여 마지막 공정의 후의 ArticleID가 필요해서 추가
                //        sqlParameter.Add("sPackDate", mtb_Date.Text.Replace("-", ""));
                //        sqlParameter.Add("nPackqty", PackQty);
                //        sqlParameter.Add("PackCustomID", cboCustom.SelectedValue.ToString());
                //        sqlParameter.Add("sPackPersonID", Frm_tins_Main.g_tBase.PersonID);


                //        WizCommon.Procedure pro1 = new WizCommon.Procedure();
                //        pro1.Name = "[xp_prdIns_iWkPacking]";
                //        pro1.OutputUseYN = (i == 0 ? "Y" : "N");
                //        pro1.OutputName = "PackID";
                //        pro1.OutputLength = "10";

                //        Prolist.Add(pro1);
                //        ListParameter.Add(sqlParameter);
                //    }
                //    index = 0;
                //    // wk_PackingCardList 에는 투입 라벨만 들어가면 되고.
                //    // wk_Inspect 에는 각 라벨이 들어가야 됨.
                //    for (int i = 0; i < dgdMain.Rows.Count; i++)
                //    {
                //        int PassQty = Lib.ConvertInt(dgdMain.Rows[i].Cells["PassQty"].Value.ToString());

                //        // 합격수량으로만 포장을 하니, 합격수량이 0 초과인 것들만 포장에 넣기.
                //        if (PassQty > 0)
                //        {
                //            // wk_PackingCardList
                //            sqlParameter = new Dictionary<string, object>();

                //            sqlParameter.Add("PackID", "");
                //            sqlParameter.Add("nCardSeq", ++index);
                //            sqlParameter.Add("sCardID", dgdMain.Rows[i].Cells["LabelID"].Value.ToString());
                //            sqlParameter.Add("nProdQty", PassQty);
                //            sqlParameter.Add("sCreateUserID", Frm_tins_Main.g_tBase.PersonID);
                //            //sqlParameter.Add("OrderID", dgdMain.Rows[i].Cells["OrderID"].Value.ToString()); //2021-05-20
                //            //sqlParameter.Add("UnitClss", ""); //2021-05-20
                //            //sqlParameter.Add("RollSeq", 0);  //2021-05-20
                //            //sqlParameter.Add("ExamDate", mtb_Date.Text.Replace("-", ""));       //2021-05-20
                //            //sqlParameter.Add("ExamTime", dtInspectTime.Value.ToString("HHmmss"));   //2021-05-20
                //            //sqlParameter.Add("DefectQty", txtDefectQty.Text);   //2021-05-20
                //            WizCommon.Procedure pro2 = new WizCommon.Procedure();
                //            pro2.Name = "[xp_prdIns_iWkPackingCardList]";
                //            pro2.OutputUseYN = "N";
                //            pro2.OutputName = "PackID";
                //            pro2.OutputLength = "20";

                //            Prolist.Add(pro2);
                //            ListParameter.Add(sqlParameter);
                //        }

                //        // Inspect 넣기
                //        int InspectQty = Lib.ConvertInt(dgdMain.Rows[i].Cells["InspectQty"].Value.ToString());

                //        //index = 0;

                //        // 검사수량이 0 이상인 건들만 넣기
                //        if (InspectQty > 0)
                //        {
                //            sqlParameter = new Dictionary<string, object>();

                //            sqlParameter.Add("OrderID", dgdMain.Rows[i].Cells["OrderID"].Value.ToString());
                //            sqlParameter.Add("RollSeq", 0);
                //            sqlParameter.Add("OrderSeq", 1);
                //            sqlParameter.Add("RollNo", 0);
                //            sqlParameter.Add("ExamNO", "00");
                //            sqlParameter.Add("ExamDate", mtb_Date.Text.Replace("-", ""));

                //            sqlParameter.Add("ExamTime", dtInspectTime.Value.ToString("HHmmss"));
                //            sqlParameter.Add("TeamID", "");
                //            sqlParameter.Add("PersonID", Frm_tins_Main.g_tBase.PersonID);
                //            sqlParameter.Add("RealQty", Lib.ConvertInt(dgdMain.Rows[i].Cells["InspectQty"].Value.ToString()));
                //            sqlParameter.Add("CtrlQty", Lib.ConvertInt(dgdMain.Rows[i].Cells["PassQty"].Value.ToString()));

                //            sqlParameter.Add("UnitClss", "");
                //            sqlParameter.Add("GradeID", "1");
                //            sqlParameter.Add("LotNo", "");
                //            sqlParameter.Add("BoxID", dgdMain.Rows[i].Cells["LabelID"].Value.ToString());
                //            sqlParameter.Add("DefectQty", Lib.ConvertInt(dgdMain.Rows[i].Cells["DefectQty"].Value.ToString()));

                //            sqlParameter.Add("DefectPoint", 0);
                //            sqlParameter.Add("DefectID", "");
                //            sqlParameter.Add("DefectClss", "");
                //            sqlParameter.Add("InstID", "");
                //            sqlParameter.Add("CardIDList", "");

                //            sqlParameter.Add("CreateUserID", Frm_tins_Main.g_tBase.PersonID);
                //            sqlParameter.Add("PackID", "");

                //            WizCommon.Procedure pro3 = new WizCommon.Procedure();
                //            pro3.Name = "[xp_prdIns_iInspectFinal]";
                //            pro3.OutputUseYN = "Y";
                //            pro3.OutputName = "RollSeq";
                //            pro3.OutputLength = "20";

                //            Prolist.Add(pro3);
                //            ListParameter.Add(sqlParameter);

                //            var lstDefect = dgdMain.Rows[i].Cells["lstDefect"].Value as Dictionary<string, frm_tprc_Work_Defect_U_CodeView>;
                //            if (lstDefect != null)
                //            {
                //                int k = 0;
                //                foreach (string Key in lstDefect.Keys)
                //                {
                //                    var Defect = lstDefect[Key] as frm_tprc_Work_Defect_U_CodeView;
                //                    if (Defect != null
                //                        && Lib.ConvertInt(Defect.DefectQty) > 0)
                //                    {
                //                        sqlParameter = new Dictionary<string, object>();

                //                        sqlParameter.Add("OrderID", dgdMain.Rows[i].Cells["OrderID"].Value.ToString());
                //                        sqlParameter.Add("RollSeq", 0);
                //                        sqlParameter.Add("DefectSeq", ++k);
                //                        sqlParameter.Add("DefectID", Key);
                //                        sqlParameter.Add("DefectQty", Lib.ConvertInt(Defect.DefectQty));

                //                        sqlParameter.Add("PersonID", Frm_tins_Main.g_tBase.PersonID);
                //                        sqlParameter.Add("PackID", "");

                //                        WizCommon.Procedure pro4 = new WizCommon.Procedure();
                //                        pro4.Name = "[xp_prdIns_iInspectSub]";
                //                        pro4.OutputUseYN = "N";
                //                        pro4.OutputName = "OrderID";
                //                        pro4.OutputLength = "20";

                //                        Prolist.Add(pro4);
                //                        ListParameter.Add(sqlParameter);
                //                    }
                //                }
                //            }

                //            // 예외출고 구문
                //            //sqlParameter = new Dictionary<string, object>();

                //            //sqlParameter.Add("OrderID", dgdMain.Rows[i].Cells["OrderID"].Value.ToString());
                //            //sqlParameter.Add("RollSeq", 0);

                //            //WizCommon.Procedure pro5 = new WizCommon.Procedure();
                //            //pro5.Name = "[xp_prdIns_iInspect_ExcptOut]";
                //            //pro5.OutputUseYN = "N";
                //            //pro5.OutputName = "JobID";
                //            //pro5.OutputLength = "20";

                //            //Prolist.Add(pro5);
                //            //ListParameter.Add(sqlParameter);

                //        }
                //    }
                //}
                ////2021-05-22 검사포장 재고 생성
                //sqlParameter2 = new Dictionary<string, object>();

                //sqlParameter2.Add("PackID", "");
                //sqlParameter2.Add("ArticleID", dgdMain.Rows[0].Cells["ArticleID"].Value.ToString().Trim()); //2021-06-08 포장재고를 생성하고 하위품을 재고 소진 하기 위해 추가
                //sqlParameter2.Add("CreateUserID", Frm_tins_Main.g_tBase.PersonID);
                ////sqlParameter.Add("OrderID", dgdMain.Rows[i].Cells["OrderID"].Value.ToString()); //2021-05-20
                ////sqlParameter.Add("UnitClss", ""); //2021-05-20
                ////sqlParameter.Add("RollSeq", 0);  //2021-05-20
                ////sqlParameter.Add("ExamDate", mtb_Date.Text.Replace("-", ""));       //2021-05-20
                ////sqlParameter.Add("ExamTime", dtInspectTime.Value.ToString("HHmmss"));   //2021-05-20
                ////sqlParameter.Add("DefectQty", txtDefectQty.Text);   //2021-05-20
                //WizCommon.Procedure pro5 = new WizCommon.Procedure();
                //pro5.Name = "[xp_prdIns_iStuffinOutware]";
                //pro5.OutputUseYN = "N";
                //pro5.OutputName = "PackID";
                //pro5.OutputLength = "20";

                //Prolist.Add(pro5);
                //ListParameter.Add(sqlParameter2);

                //List<KeyValue> list_Result = new List<KeyValue>();
                //list_Result = DataStore.Instance.ExecuteAllProcedureOutputToCS(Prolist, ListParameter);

                //if (list_Result[0].key.ToLower() == "success")
                //{
                //    WizCommon.Popup.MyMessageBox.ShowBox("검사 등록이 성공적으로 완료되었습니다.", "[저장 완료]", 3, 1);

                //    DataStore.Instance.CloseConnection(); //2021-10-07 DB 커넥트 연결 해제
                //    return true;
                //}
                //else
                //{
                //    foreach (KeyValue kv in list_Result)
                //    {
                //        if (kv.key.ToLower() == "failure")
                //        {
                //            throw new Exception(kv.value.ToString());
                //        }
                //    }
                //    return false;
                //}
                #endregion
            }
            catch (Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(ex.Message, "[저장 실패 : SaveData부분]", 3, 1);
            }

            return flag;
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

        #region 수량 / 숫자 키패드 팝업 이벤트

        // 검사수량 등록 시 → 작업수량 합계를 초과한 경우에는 막아야됨
        private void chkInspectQty_Click(object sender, EventArgs e)
        {
            loadNumberKeyboard(txtInspectQty);
        }

        private void chkQtyPerBox_Click(object sender, EventArgs e)
        {
            loadNumberKeyboard(txtQtyPerBox);
        }

        // 작업수량 기입 팝업창 생성
        private void txtNumber_Click(object sender, EventArgs e)
        {
            TextBox txtSender = sender as TextBox;
            if (txtSender != null)
            {
                loadNumberKeyboard(txtSender);
            }
        }

        private void loadNumberKeyboard(TextBox txtSender)
        {
            string Title = txtSender.Tag == null ? "" : txtSender.Tag.ToString();

            //string bakup = txtSender.Text;

            //txtSender.Text = "";
            WizWork.POPUP.Frm_CMNumericKeypad keypad = new WizWork.POPUP.Frm_CMNumericKeypad(Title + "입력", Title);

            keypad.Owner = this;
            if (keypad.ShowDialog() == DialogResult.OK)
            {
                int chgValue = Lib.ConvertInt(keypad.tbInputText.Text);

                // 검사 수량 변경시 체크
                if (CheckChangeInspectQty(txtSender, chgValue) == false) { return; }

                txtSender.Text = Lib.stringFormatN0(chgValue);
            }

            CalQty();
        }

        // 수량 변경하는 건이 검사수량 일때.
        // 1. 투입 라벨의 총 작업수량을 넘길 수 없음.
        // 2. 불량수량보다 적을 수 없음.
        private bool CheckChangeInspectQty(TextBox txtSender, int value)
        {
            if (txtSender == null) { return false; }

            // 검사수량 변경 일시
            if (txtSender.Name.Equals("txtInspectQty") == true)
            {
                if (TotalQty < value)
                {
                    string MsgTotalQty = Lib.stringFormatN0(TotalQty);

                    WizCommon.Popup.MyMessageBox.ShowBox("검사 수량이 총 작업수량 : " + MsgTotalQty + "개를 넘을 수 없습니다.", "검사수량 변경 전", 0, 1);
                    return false;
                }

                int DefectQty = Lib.ConvertInt(txtDefectQty.Text);

                if (value < DefectQty)
                {
                    string MsgTotalQty = Lib.stringFormatN0(TotalQty);

                    WizCommon.Popup.MyMessageBox.ShowBox("검사 수량이 불량수량 : " + txtDefectQty.Text + "개 보다 작을 수 없습니다.", "검사수량 변경 전", 0, 1);
                    return false;
                }
            }

            return true;
        }

        #endregion

        #region 수량 입력 및 변경시, 계산 메서드 : CalQty()

        private void CalQty()
        {
            //Dictionary<string, frm_tprc_Work_Defect_U_CodeView> cloneDicDefect = new Dictionary<string, frm_tprc_Work_Defect_U_CodeView>(dicDefect);
           // Dictionary<string, frm_tprc_Work_Defect_U_CodeView> cloneDicDefect = dicDefect.ToDictionary(entry => entry.Key, entry => entry.Value.Copy());
            Dictionary<string, frm_tprc_Work_Defect_U_CodeView> InsDefect = new Dictionary<string, frm_tprc_Work_Defect_U_CodeView>();

            int InspectQty = Lib.ConvertInt(txtInspectQty.Text);
            int DefectQty = Lib.ConvertInt(txtDefectQty.Text);

            // 합격수량 = 검사수량 - 불량수량
            int PassQty = InspectQty - DefectQty;
            txtPassQty.Text = Lib.stringFormatN0(PassQty);

            // 잔량수량 = 총수량 - 검사수량
            int RemainQty = TotalQty - InspectQty;
            txtRemainQty.Text = Lib.stringFormatN0(RemainQty);

            // 
            int QtyPerBox = Lib.ConvertInt(txtQtyPerBox.Text);

            if (PassQty <= 0) // 합격 수량이 0 이면 라벨 발행할 필요가 없으므로 0처리
            {
                txtBoxQty.Text = "0";
            }
            else if (QtyPerBox == 0) // 박스당 수량이 없다면 1장으로 뽑기
            {
                txtBoxQty.Text = "1";
            }
            else // 나머지는 나누기 + 올림처리
            {
                txtBoxQty.Text = Lib.stringFormatN0(Math.Ceiling(1.0 * PassQty / QtyPerBox));
            }
            #region 주석
            // 투입 라벨 리스트 수량 수정 처리
            //for (int i = 0; i < dgdMain.Rows.Count; i++)
            //{
            //    //int WorkQty = Lib.ConvertInt(dgdMain.Rows[i].Cells["WorkQty"].Value.ToString());

            //    var Main = dgdMain.Rows[i].DataBoundItem as Frm_PopUp_Packing_CodeView;
            //    if (Main != null)
            //    {
            //        // 검사수량이 있어야됨!
            //        if (InspectQty > 0)
            //        {
            //            // 검사수량 조정
            //            if (Lib.ConvertInt(Main.WorkQty) >= InspectQty)
            //            {
            //                Main.InspectQty = Lib.stringFormatN0(InspectQty);
            //                InspectQty = 0;
            //            }
            //            else
            //            {
            //                Main.InspectQty = Lib.stringFormatN0(Main.WorkQty);
            //                InspectQty -= Lib.ConvertInt(Main.WorkQty);
            //            }

            //            // 불량 수량이 있으면.
            //            if (DefectQty > 0)
            //            {
            //                // 불량 수량 조정
            //                if (Lib.ConvertInt(Main.InspectQty) >= DefectQty)
            //                {
            //                    Main.DefectQty = Lib.stringFormatN0(DefectQty);
            //                    DefectQty = 0;
            //                }
            //                else
            //                {
            //                    Main.DefectQty = Lib.stringFormatN0(Main.InspectQty);
            //                    DefectQty -= Lib.ConvertInt(Main.InspectQty);
            //                }
            //            }

            //            Main.PassQty = Lib.stringFormatN0(Lib.ConvertInt(Main.InspectQty) - Lib.ConvertInt(Main.DefectQty));
            //            Main.RemainQty = Lib.stringFormatN0(Lib.ConvertInt(Main.WorkQty) - Lib.ConvertInt(Main.InspectQty));
            //        }
            //        else
            //        {
            //            Main.InspectQty = Lib.stringFormatN0(0);
            //            Main.PassQty = Lib.stringFormatN0(0);
            //            Main.DefectQty = Lib.stringFormatN0(0);
            //            Main.RemainQty = Lib.stringFormatN0(Lib.ConvertInt(Main.WorkQty));
            //        }

            //        // 등록할 해당 라벨 불량 리스트 초기화
            //        InsDefect = new Dictionary<string, frm_tprc_Work_Defect_U_CodeView>();

            //        // 해당 라벨에 불량 수량이 존재한다면.
            //        int InsDefectQty = Lib.ConvertInt(Main.DefectQty);
            //        //if (InsDefectQty > 0)
            //        //{
            //        //    foreach (string Key in cloneDicDefect.Keys)
            //        //    {
            //        //        // 불량 갯수 모두 등록 했으면 종료
            //        //        if (InsDefectQty <= 0) { break; }

            //        //        // 불량 유형 하나 들고와서
            //        //        var Defect = (cloneDicDefect[Key] as frm_tprc_Work_Defect_U_CodeView).Copy();
            //        //        if (Defect != null
            //        //            && Lib.ConvertInt(Defect.DefectQty) > 0)
            //        //        {
            //        //            // 해당 라벨 불량 수량 보다 작으면, 그대로 넣고 Key로 삭제.
            //        //            if (Lib.ConvertInt(Defect.DefectQty) <= InsDefectQty)
            //        //            {
            //        //                InsDefect.Add(Key, Defect);
            //        //                //cloneDicDefect.Remove(Key);
            //        //                cloneDicDefect[Key].DefectQty = "0";

            //        //                InsDefectQty -= Lib.ConvertInt(Defect.DefectQty);
            //        //            }
            //        //            else // 크면, 라벨 불량 갯수만큼만 넣고, 전체 불량 리스트 수정.
            //        //            {
            //        //                cloneDicDefect[Key].DefectQty = Lib.stringFormatN0(Lib.ConvertInt(Defect.DefectQty) - InsDefectQty);

            //        //                Defect.DefectQty = Lib.stringFormatN0(InsDefectQty);
            //        //                InsDefect.Add(Key, Defect);

            //        //                InsDefectQty = 0;
            //        //            }
            //        //        }
            //        //    }
            //        //}

            //        dgdMain.Rows[i].Cells["InspectQty"].Value = Main.InspectQty;
            //        dgdMain.Rows[i].Cells["DefectQty"].Value = Main.DefectQty;
            //        dgdMain.Rows[i].Cells["PassQty"].Value = Main.PassQty;
            //        dgdMain.Rows[i].Cells["RemainQty"].Value = Main.RemainQty;
            //        dgdMain.Rows[i].Cells["lstDefect"].Value = InsDefect;
            //    }
            //}

            //dgdMain.Refresh();
            #endregion
        }

        #endregion

        #region 불량 수량 클릭 이벤트

        private void chkDefectQty_Click(object sender, EventArgs e)
        {

        }

        private void txtDefectQty_Click(object sender, EventArgs e)
        {

        }

        private void loadDefectList()
        {
            double InsQty = Lib.ConvertDouble(txtInspectQty.Text);
            if (InsQty == 0)
            {
                WizCommon.Popup.MyMessageBox.ShowBox("검사 수량을 먼저 입력해주세요.\r\n(불량 수량은 검사수량 이하로 입력 가능합니다.)", "불량 등록 전", 0, 1);
                return;
            }

            //frm_tprc_Work_Defect_U defect = new frm_tprc_Work_Defect_U("", dicDefect, Lib.ConvertDouble(txtInspectQty.Text));
            //defect.Owner = this;
            //defect.ShowDialog();
            //if (defect.DialogResult == DialogResult.OK)
            //{
            //    this.dicDefect = defect.dicDefect;
            //    this.txtDefectQty.Text = defect.returnTotalQty;
            //}

            CalQty();
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
                WizWork.TSCLIB_DLL.setup(stringFormatN1(strWidth), stringFormatN1(strHeight), "4", "15", "1", "4", "0"); // GLS Black Mark Setting
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
                                    int inty = list_m_tItem[i].y + 46;
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
        //2021-06-10 프린터 없는 곳에서 실적 저장을 위해 추가
        private void btnsave_Click(object sender, EventArgs e)
        {
            if (SaveNotPrintData())
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
    #endregion


    #region 코드뷰 : Frm_PopUp_Packing_CodeView
    public class Frm_PopUp_Packing_CodeView
    {
        public int Num { get; set; }

        public string LabelID { get; set; }
        public string LabelGubun { get; set; }
        public string ArticleID { get; set; }
        public string Article { get; set; }
        public string BuyerArticleNo { get; set; }
        public string WorkQty { get; set; }
        public string InspectQty { get; set; }
        public string PassQty { get; set; }
        public string DefectQty { get; set; }
        public string RemainQty { get; set; }
        public string KCompany { get; set; }
        public string CustomID { get; set; }
        public string OrderID { get; set; }

        public Dictionary<string, frm_tprc_Work_Defect_U_CodeView> lstDefect { get; set; }

        //public Frm_PopUp_Packing_CodeView(Frm_tins_Order_Q_CodeView Ins)
        //{
        //    this.LabelID = Ins.LabelID;
        //    this.LabelGubun = Ins.LabelGubun;
        //    this.ArticleID = Ins.ArticleID;
        //    this.Article = Ins.Article;
        //    this.BuyerArticleNo = Ins.BuyerArticleNo;
        //    this.WorkQty = Ins.NoInspectQty;
        //    this.InspectQty = Ins.WorkQty;
        //    this.PassQty = Ins.WorkQty;
        //    this.DefectQty = "0";
        //    this.RemainQty = "0";
        //    this.KCompany = Ins.KCustom;
        //    this.CustomID = CustomID;
        //    this.OrderID = Ins.OrderID;

        //    this.lstDefect = new Dictionary<string, frm_tprc_Work_Defect_U_CodeView>();
        //}
    }
    #endregion
}
