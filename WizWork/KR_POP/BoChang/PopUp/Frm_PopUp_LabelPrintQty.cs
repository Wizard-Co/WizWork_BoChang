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
    public partial class Frm_PopUp_LabelPrintQty : Form
    {
        DataSet ds = null;

        WizWorkLib Lib = new WizWorkLib();
        List<Sub_TWkLabelPrint> list_TWkLabelPrint = null;
        POPUP.Frm_CMNumericKeypad keypad = null;
        public TTag Sub_m_tTag = new TTag();
        public TTagSub Sub_m_tItem = new TTagSub();
        public List<TTagSub> list_m_tItem = new List<TTagSub>();
        string[] Message = new string[2];

        string OutQtyPerBox = string.Empty;
        string ArticleID = string.Empty;
        string IsTagID = string.Empty;
        string LabelID = string.Empty;

        int Time = 3;

        /// <summary>
        /// 폼간 데이터전달을 위한 소스
        /// </summary>
        /// <param name="text"></param> 
        ///  

        public Frm_PopUp_LabelPrintQty()
        {
            InitializeComponent();
        }

        public Frm_PopUp_LabelPrintQty(string OutQtyPerBox, string ArticleID)
        {
            InitializeComponent();
            this.OutQtyPerBox = OutQtyPerBox;
            this.ArticleID = ArticleID;
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

        private void Frm_PopUp_LabelPrintQty_Load(object sender, EventArgs e)
        {
            SetScreen();
            txtQty.Text = OutQtyPerBox;
            timer1.Start();
        }

        #region 수량 이벤트
        private void btnQty_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            timer1.Dispose();

            keypad = new POPUP.Frm_CMNumericKeypad("비밀번호", "");
            if (keypad.ShowDialog() == DialogResult.OK)
            {
                if (keypad.tbInputText.Text.Trim() == "0000")
                {
                    POPUP.Frm_CMNumericKeypad keypad = new POPUP.Frm_CMNumericKeypad("수량입력", "수량");

                    keypad.Owner = this;
                    if (keypad.ShowDialog() == DialogResult.OK)
                    {
                        txtQty.Text = keypad.tbInputText.Text;
                        if (txtQty.Text == "" || Convert.ToInt32(txtQty.Text) == 0)
                        {
                            txtQty.Text = "0";
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

        private void txtQty_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            timer1.Dispose();

            keypad = new POPUP.Frm_CMNumericKeypad("비밀번호", "");
            if (keypad.ShowDialog() == DialogResult.OK)
            {
                if (keypad.tbInputText.Text.Trim() == "0000")
                {
                    POPUP.Frm_CMNumericKeypad keypad = new POPUP.Frm_CMNumericKeypad("수량입력", "수량");

                    keypad.Owner = this;
                    if (keypad.ShowDialog() == DialogResult.OK)
                    {
                        txtQty.Text = keypad.tbInputText.Text;
                        if (txtQty.Text == "" || Convert.ToInt32(txtQty.Text) == 0)
                        {
                            txtQty.Text = "0";
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

        #region 발행, 닫기 이벤트

        private void btnPrint_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            timer1.Dispose();

            if (SaveData())
            {
                Print();
                DialogResult = DialogResult.OK;
                this.Close();
            }          
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            timer1.Dispose();
            this.Close();
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
            sqlParameter.Add("ArticleID", ArticleID.Trim());
            sqlParameter.Add("PrintDate", DateTime.Now.ToString("yyyyMMdd"));

            sqlParameter.Add("ReprintDate", "");
            sqlParameter.Add("ReprintQty",  0);
            sqlParameter.Add("InstID", "");
            sqlParameter.Add("InstDetSeq", 0);
            sqlParameter.Add("OrderID", "");

            sqlParameter.Add("PrintQty", 1);
            sqlParameter.Add("LabelPrintQty", 1);
            sqlParameter.Add("nQtyPerBox", Convert.ToDouble(txtQty.Text.ToString()));
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

            sqlParameter1.Add("ArticleID", ArticleID.Trim());
            sqlParameter1.Add("WorkQty", Convert.ToDouble(txtQty.Text.ToString()));
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
            pro2.OutputUseYN = "N";
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

        #region 프린트 함수

        private void Print()
        {
            string g_sPrinterName = Lib.GetDefaultPrinter();
            try
            {
                int R = 0;      // Rotation R.
                IsTagID = "013";
                int WorkQty = Convert.ToInt32(txtQty.Text.ToString());
                List<string> list_Data = null;
                //Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                //sqlParameter.Add("InstID", list_TWkLabelPrint[0].sInstID);
                //DataTable dt = DataStore.Instance.ProcedureToDataTable("[xp_WorkCard_sWorkCard]", sqlParameter, false);
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

                        list_Data.Add(Lib.CheckNull(dr["BuyerArticleNo"].ToString()).Substring(0, 8));  //품명

                        list_Data.Add(Lib.CheckNull(dr["Article"].ToString()).Substring(0, 3));   //품번

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

        private void timer1_Tick(object sender, EventArgs e)
        {
            Time--;

            if (Time >= 0)
            {
                this.Text = "수량 입력 " + Time.ToString();
            }
            else
            {
                timer1.Stop();
                timer1.Dispose();
                btnPrint_Click(null,null);
            }
        }
    }
}