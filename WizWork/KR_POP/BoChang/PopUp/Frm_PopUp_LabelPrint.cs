using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WizCommon;

namespace WizWork
{
    public partial class Frm_PopUp_LabelPrint : Form
    {
        DataSet ds = null;

        WizWorkLib Lib = new WizWorkLib();
        List<Sub_TWkLabelPrint> list_TWkLabelPrint = null;
        POPUP.Frm_CMNumericKeypad keypad = null;
        public TTag Sub_m_tTag = new TTag();
        public TTagSub Sub_m_tItem = new TTagSub();
        public List<TTagSub> list_m_tItem = new List<TTagSub>();
        string[] Message = new string[2];

        string Article = string.Empty;           //품명
        string ArticleID = string.Empty;         //품명ID
        string BuyerArticleNo = string.Empty;    //품번
        string OutQtyPerBox = string.Empty;      //출하용박스수량
        string IsTagID = string.Empty;
        string LabelID = string.Empty;

        /// <summary>
        /// 폼간 데이터전달을 위한 소스
        /// </summary>
        /// <param name="text"></param> 
        ///  



        public Frm_PopUp_LabelPrint()
        {
            InitializeComponent();
        }

        private void SetScreen()
        {
            tlpMain.Dock = DockStyle.Fill;
            foreach (Control control in tlpMain.Controls)
            {
                control.Dock = DockStyle.Fill;
                control.Margin = new Padding(0, 0, 0, 0);
                //foreach (Control contro in control.Controls)
                //{
                //    contro.Dock = DockStyle.Fill;
                //    contro.Margin = new Padding(0, 0, 0, 0);
                //    foreach (Control contr in contro.Controls)
                //    {
                //        contr.Dock = DockStyle.Fill;
                //        contr.Margin = new Padding(0, 0, 0, 0);

                //    }
                //}
            }
        }

        private void Frm_PopUp_LabelPrint_Load(object sender, EventArgs e)
        {
            SetScreen();
            btnBuyerArticleNO.BackColor = Color.White;
            btnArticleEnabled.BackColor = Color.White;
            btnArticle.BackColor = Color.White;
            InitGrid();
            timer1.Start();
        }


        #region 선택, 닫기, 발행 이벤트

        private void btnArticleSelect_Click(object sender, EventArgs e)
        {
            this.Article = "";
            this.ArticleID = "";
            this.BuyerArticleNo = "";
            this.OutQtyPerBox = "";

            this.txtBuyerArticleNO.Text = "";
            this.txtArticle.Text = "";
            this.txtWorkQty.Text = "";

            Frm_PopUp_LabelPrintSelect FPLPS = new Frm_PopUp_LabelPrintSelect();
            FPLPS.Owner = this;

            if (FPLPS.ShowDialog() == DialogResult.OK)
            {
                this.Article = FPLPS.Article;
                this.ArticleID = FPLPS.ArticleID;
                this.BuyerArticleNo = FPLPS.BuyerArticleNo;
                this.OutQtyPerBox = FPLPS.OutQtyPerBox;

                this.txtBuyerArticleNO.Text = BuyerArticleNo;
                this.txtArticle.Text = Article;
                this.txtWorkQty.Text = OutQtyPerBox;

                FillGrid(ArticleID);

            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {
                Print(LabelID);
                DataClear();
                //this.Close();
            }

        }

        #endregion

        #region 바코드 스캔 이벤트

        private void btnScan_Click(object sender, EventArgs e)
        {
            try
            {
                POPUP.Frm_CMKeypad.g_Name = "바코드 스캔";
                POPUP.Frm_CMKeypad FK = new POPUP.Frm_CMKeypad();
                POPUP.Frm_CMKeypad.KeypadStr = txtScan.Text.Trim();

                FK.Owner = this;
                if (FK.ShowDialog() == DialogResult.OK)
                {
                    txtScan.Text = FK.tbInputText.Text;

                    KeyPressEventArgs key = new KeyPressEventArgs((char)13);
                    txtScan_KeyPress(null, key);

                }
                else
                {
                    txtScan.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox("관리자에게 문의해주세요.\r\n(Info : " + ex.Message.ToString() + ")", "[오류 - 스캔버튼 클릭 부분]", 0, 1);
            }
        }

        //바코드 스캔
        private void txtScan_Click(object sender, EventArgs e)
        {
            try
            {
                POPUP.Frm_CMKeypad.g_Name = "바코드 스캔";
                POPUP.Frm_CMKeypad FK = new POPUP.Frm_CMKeypad();
                POPUP.Frm_CMKeypad.KeypadStr = txtScan.Text.Trim();

                FK.Owner = this;
                if (FK.ShowDialog() == DialogResult.OK)
                {
                    txtScan.Text = FK.tbInputText.Text;

                    KeyPressEventArgs key = new KeyPressEventArgs((char)13);
                    txtScan_KeyPress(null, key);

                }
                else
                {
                    txtScan.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox("관리자에게 문의해주세요.\r\n(Info : " + ex.Message.ToString() + ")", "[오류 - 스캔버튼 클릭 부분]", 0, 1);
            }
        }

        //바코드 스캔
        private void txtScan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (BarcodeEnter(txtScan.Text.Trim().ToString()))
                {
                    FillGrid(ArticleID);
                    Frm_PopUp_LabelPrintQty FPLPQ = new Frm_PopUp_LabelPrintQty(txtWorkQty.Text.ToString(), ArticleID);
                    FPLPQ.Owner = this;

                    if (FPLPQ.ShowDialog() == DialogResult.OK)
                    {
                        DataClear();
                    };
                }

                txtScan.Text = "";
            }
        }

        private bool BarcodeEnter(string Barcode)
        {
            //string BarcodeScan = string.Empty;


            //if (Barcode.Length > 7) 
            //{
            //    BarcodeScan = Barcode.Substring(0, 8);
            //}

            Dictionary<string, object> sqlParameter = new Dictionary<string, object>();

            sqlParameter.Add("sArticleID", Barcode);

            DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_prdWork_sArticleCardLabelPrintByArticleID", sqlParameter, false);

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    txtBuyerArticleNO.Text = dr["BuyerArticleNo"].ToString();    //품번
                    txtArticle.Text = dr["Article"].ToString();                  //품명
                    txtWorkQty.Text = dr["OutQtyPerBox"].ToString();     //출하용박스당 수량
                    ArticleID = dr["ArticleID"].ToString();              //품명ID                                
                }

                return true;
            }
            else
            {
                WizCommon.Popup.MyMessageBox.ShowBox("제품이 아니거나 품명코드에 등록이 되지 않았습니다.", "[확인]", 0, 1);
                return false;
            }

        }

        #endregion

        #region 수량 이벤트

        private void btnWorkQty_Click(object sender, EventArgs e)
        {
            keypad = new POPUP.Frm_CMNumericKeypad("비밀번호", "");
            if (keypad.ShowDialog() == DialogResult.OK)
            {
                if (keypad.tbInputText.Text.Trim() == "0000")
                {
                    POPUP.Frm_CMNumericKeypad keypad = new POPUP.Frm_CMNumericKeypad("수량입력", "수량");

                    keypad.Owner = this;
                    if (keypad.ShowDialog() == DialogResult.OK)
                    {
                        txtWorkQty.Text = keypad.tbInputText.Text;
                        if (txtWorkQty.Text == "" || Convert.ToInt32(txtWorkQty.Text) == 0)
                        {
                            txtWorkQty.Text = "0";
                        }
                    }
                }
                else
                {
                    WizCommon.Popup.MyMessageBox.ShowBox("비밀번호가 일치하지 않습니다", "[잘못된 비밀번호]", 3, 1);
                }
            }
            keypad = null;
        }

        private void txtWorkQty_Click(object sender, EventArgs e)
        {
            keypad = new POPUP.Frm_CMNumericKeypad("비밀번호", "");
            if (keypad.ShowDialog() == DialogResult.OK)
            {
                if (keypad.tbInputText.Text.Trim() == "0000")
                {
                    POPUP.Frm_CMNumericKeypad keypad = new POPUP.Frm_CMNumericKeypad("수량입력", "수량");

                    keypad.Owner = this;
                    if (keypad.ShowDialog() == DialogResult.OK)
                    {
                        txtWorkQty.Text = keypad.tbInputText.Text;
                        if (txtWorkQty.Text == "" || Convert.ToInt32(txtWorkQty.Text) == 0)
                        {
                            txtWorkQty.Text = "0";
                        }
                    }
                }
                else
                {
                    WizCommon.Popup.MyMessageBox.ShowBox("비밀번호가 일치하지 않습니다", "[잘못된 비밀번호]", 3, 1);
                }
            }
            keypad = null;
        }

        #endregion

       

        #region 데이터 초기화

        private void DataClear()
        {
            this.Article = string.Empty;           //품명
            this.ArticleID = string.Empty;         //품명ID
            this.BuyerArticleNo = string.Empty;    //품번
            this.OutQtyPerBox = string.Empty;      //출하용박스수량

            this.txtBuyerArticleNO.Text = string.Empty;
            this.txtArticle.Text = string.Empty;
            this.txtWorkQty.Text = string.Empty;

            this.dgvWorking.Rows.Clear();

        }

        #endregion

    
        #region 저장 함수

        private bool SaveData()
        {
            List<WizCommon.Procedure> Prolist = new List<WizCommon.Procedure>();
            List<List<string>> ListProcedureName = new List<List<string>>();
            List<Dictionary<string, object>> ListParameter = new List<Dictionary<string, object>>();

            Dictionary<string, object> sqlParameter = new Dictionary<string, object>();

            sqlParameter.Add("LabelID", "");
            sqlParameter.Add("LabelGubun", "7");
            sqlParameter.Add("ProcessID", "");
            sqlParameter.Add("ArticleID", ArticleID.ToString().Trim());
            sqlParameter.Add("PrintDate", DateTime.Now.ToString("yyyyMMdd"));

            sqlParameter.Add("ReprintDate", "");
            sqlParameter.Add("ReprintQty", 0);
            sqlParameter.Add("InstID", "");
            sqlParameter.Add("InstDetSeq", 0);
            sqlParameter.Add("OrderID", "");

            sqlParameter.Add("PrintQty", 1);
            sqlParameter.Add("LabelPrintQty", 1);
            sqlParameter.Add("nQtyPerBox", Convert.ToDouble(txtWorkQty.Text.ToString()));
            sqlParameter.Add("CreateUserID", Frm_tprc_Main.g_tBase.PersonID);

            WizCommon.Procedure pro1 = new WizCommon.Procedure();
            pro1.Name = "[xp_WizWork_iwkLabelPrint_C]";
            pro1.OutputUseYN = "Y";
            pro1.OutputName = "LabelID";
            pro1.OutputLength = "20";

            Prolist.Add(pro1);
            ListParameter.Add(sqlParameter);


            Dictionary<string, object> sqlParameter1 = new Dictionary<string, object>();
            WizCommon.Procedure pro2 = new WizCommon.Procedure();

            sqlParameter1.Add("JobID", 0);
            sqlParameter1.Add("InstID", "");
            sqlParameter1.Add("InstDetSeq", 0);
            sqlParameter1.Add("LabelID", "");
            sqlParameter1.Add("StartSaveLabelID", "");

            sqlParameter1.Add("LabelGubun", "7");
            sqlParameter1.Add("ProcessID", "");
            sqlParameter1.Add("MachineID", "");
            sqlParameter1.Add("ScanDate", DateTime.Now.ToString("yyyyMMdd"));
            sqlParameter1.Add("ScanTime", DateTime.Now.ToString("HHmmss"));

            sqlParameter1.Add("ArticleID", ArticleID.ToString().Trim());
            sqlParameter1.Add("WorkQty", Convert.ToDouble(txtWorkQty.Text.ToString()));
            sqlParameter1.Add("Comments", "현장 제품 라벨 발행");
            sqlParameter1.Add("ReworkOldYN", "");
            sqlParameter1.Add("ReworkLinkProdID", "");

            sqlParameter1.Add("WorkStartDate", DateTime.Now.ToString("yyyyMMdd"));
            sqlParameter1.Add("WorkStartTime", DateTime.Now.ToString("HHmmss"));
            sqlParameter1.Add("WorkEndDate", DateTime.Now.ToString("yyyyMMdd"));
            sqlParameter1.Add("WorkEndTime", DateTime.Now.ToString("HHmmss"));
            sqlParameter1.Add("JobGbn", "1");

            sqlParameter1.Add("NoReworkCode", "");
            sqlParameter1.Add("WDNO", "");
            sqlParameter1.Add("WDID", "");
            sqlParameter1.Add("WDQty", 0);
            sqlParameter1.Add("LogID", 0);

            sqlParameter1.Add("s4MID", "");

            if (Convert.ToInt32(DateTime.Now.ToString("HHmmss")) >= 200000 || (Convert.ToInt32(DateTime.Now.ToString("HHmmss"))) <= 080000)
            {
                sqlParameter1.Add("DayOrNightID", "02");
            }
            else
            {
                sqlParameter1.Add("DayOrNightID", "01");
            }

            sqlParameter1.Add("SplitYNGBN", "");
            sqlParameter1.Add("CycleTime", 0);
            sqlParameter1.Add("CreateUserID", Frm_tprc_Main.g_tBase.PersonID);

            pro2.Name = "xp_wkResult_iWkResult_LabelPrint";
            pro2.OutputUseYN = "Y";
            pro2.OutputName = "JobID";
            pro2.OutputLength = "20";

            Prolist.Add(pro2);
            ListParameter.Add(sqlParameter1);

            List<KeyValue> list_Result = new List<KeyValue>();
            list_Result = DataStore.Instance.ExecuteAllProcedureOutputToCS(Prolist, ListParameter);

            if (list_Result[0].key.ToLower() == "success")
            {
                list_Result.RemoveAt(0);
                int a = 0;
                for (int i = 0; i < list_Result.Count; i++)
                {
                    KeyValue kv = list_Result[i];
                    if (kv.key == "LabelID")
                    {
                        LabelID = kv.value;
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

        #endregion

        #region 조회 함수

        private void InitGrid()
        {
            dgvWorking.Columns.Clear();
            dgvWorking.ColumnCount = 6;
            // Set the Colums Hearder Names
            int i = 0;

            dgvWorking.Columns[i].Name = "WorkDate";
            dgvWorking.Columns[i].HeaderText = "일자";
            dgvWorking.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvWorking.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvWorking.Columns[i].ReadOnly = true;
            dgvWorking.Columns[i].Visible = true;

            dgvWorking.Columns[++i].Name = "WorkTime";
            dgvWorking.Columns[i].HeaderText = "시간";
            dgvWorking.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvWorking.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvWorking.Columns[i].ReadOnly = true;
            dgvWorking.Columns[i].Visible = true;

            dgvWorking.Columns[++i].Name = "BuyerArticleNo";
            dgvWorking.Columns[i].HeaderText = "품번";
            dgvWorking.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvWorking.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvWorking.Columns[i].ReadOnly = true;
            dgvWorking.Columns[i].Visible = true;

            dgvWorking.Columns[++i].Name = "Article";
            dgvWorking.Columns[i].HeaderText = "품명";
            dgvWorking.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvWorking.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvWorking.Columns[i].ReadOnly = true;
            dgvWorking.Columns[i].Visible = true;

            dgvWorking.Columns[++i].Name = "OutQtyPerBox";
            dgvWorking.Columns[i].HeaderText = "수량";
            dgvWorking.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dgvWorking.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvWorking.Columns[i].ReadOnly = true;
            dgvWorking.Columns[i].Visible = true;

            dgvWorking.Columns[++i].Name = "Person";
            dgvWorking.Columns[i].HeaderText = "작업자";
            dgvWorking.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dgvWorking.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvWorking.Columns[i].ReadOnly = true;
            dgvWorking.Columns[i].Visible = true;

            //dgvWorking.Columns[++i].Name = "ArticleID";
            //dgvWorking.Columns[i].HeaderText = "품명ID";
            //dgvWorking.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            //dgvWorking.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //dgvWorking.Columns[i].ReadOnly = true;
            //dgvWorking.Columns[i].Visible = false;

            dgvWorking.Font = new Font("맑은 고딕", 13);
            dgvWorking.RowTemplate.DefaultCellStyle.Font = new Font("맑은 고딕", 14, FontStyle.Bold);
            dgvWorking.RowTemplate.Height = 35;
            dgvWorking.ColumnHeadersHeight = 35;
            dgvWorking.ScrollBars = ScrollBars.Both;
            dgvWorking.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvWorking.MultiSelect = false;
            dgvWorking.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //grdData.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(234, 234, 234);
            //dgvWorking.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvWorking.ReadOnly = true;

            //dgvArticle.EnableHeadersVisualStyles = false;  // 헤더 셀 스타일 적용 용도.

            foreach (DataGridViewColumn col in dgvWorking.Columns)
            {
                col.DataPropertyName = col.Name;
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            return;
        }

        private void FillGrid(string ArticleID)
        {

            Dictionary<string, object> sqlParameter = new Dictionary<string, object>();

            sqlParameter.Add("sArticleID", ArticleID);

            DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_prdWork_sArticleWkresult", sqlParameter, false);

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    dgvWorking.Rows.Add(
                                        dr["WorkDate"].ToString(),          //일자
                                        dr["WorkEndTime"].ToString(),       //시간
                                        dr["BuyerArticleNo"].ToString(),    //품번
                                        dr["Article"].ToString(),           //품명
                                        dr["WorkQty"].ToString(),      //수량
                                        dr["Person"].ToString()             //작업자
                                         );
                }
            }

        }

        #endregion

        #region 프린트 함수

        private void Print(string LabelID)
        {
            string g_sPrinterName = Lib.GetDefaultPrinter();
            try
            {
                int R = 0;      // Rotation R.
                IsTagID = "013";
                int WorkQty = Convert.ToInt32(txtWorkQty.Text.ToString());
                List<string> list_Data = null;
                g_sPrinterName = Lib.GetDefaultPrinter();
                TSCLIB_DLL.openport(g_sPrinterName);

                for (int i = 0; i < WorkQty; i++)
                {
                    list_Data = new List<string>();
                    Dictionary<string, object> sqlParameter = new Dictionary<string, object>();

                    sqlParameter.Add("LabelID", LabelID);

                    DataTable dt2 = DataStore.Instance.ProcedureToDataTable("[xp_prdWork_sArticleWkresultbyArticleID]", sqlParameter, false);
                    foreach (DataRow dr in dt2.Rows)
                    {
                        list_Data.Add(Lib.CheckNull(dr["BuyerArticleNo"].ToString()));        //바코드(품명)
                        
                        list_Data.Add(Lib.CheckNull(dr["Spec"].ToString()));          //세부내역(추후 차종으로 수정)

                        list_Data.Add(Lib.CheckNull(dr["BuyerArticleNo"].ToString()).Substring(0,8));  //품명

                        list_Data.Add(Lib.CheckNull(dr["Article"].ToString()).Substring(0,3));   //품번

                        list_Data.Add(Lib.CheckNull(dr["Article"].ToString()).Substring(Lib.CheckNull(dr["Article"].ToString()).Length - 2));   //품번 뒤에 2자리

                    }

                    g_sPrinterName = Lib.GetDefaultPrinter();
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
                        //'QR CODE
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
                        //        int intx = list_m_tItem[i].x - 470;
                        //        int inty = list_m_tItem[i].y + 70;
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
                                    sBarType[1] = "4";
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

                                //if (ReadAble.Equals("0"))
                                //{
                                //    // 바코드 글자 세팅
                                //    int intx = list_m_tItem[i].x + 90;
                                //    int inty = list_m_tItem[i].y + 70;
                                //    int fontheight = 50;
                                //    int rotation = 0;
                                //    int fontstyle = 0;
                                //    int fontunderline = 0;
                                //    string FaceName = "맑은 고딕";
                                //    string content = Lib.CheckNull(list_m_tItem[i].sText).Trim();

                                //    TSCLIB_DLL.windowsfont(intx, inty, fontheight, rotation, fontstyle, fontunderline, FaceName, content);
                                //}
                            }
                        }


                        //    }
                        //}




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


        #endregion

        #region 기타 함수

        private string stringFormatN1(object obj)
        {
            return string.Format("{0:N0}", obj);
        }


        #endregion

        #region 타이머

        private void timer1_Tick(object sender, EventArgs e)
        {
            txtScan.Focus();
        }

        #endregion

    }
}