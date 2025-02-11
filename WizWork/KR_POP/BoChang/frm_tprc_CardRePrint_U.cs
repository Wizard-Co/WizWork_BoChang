using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using WizCommon;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace WizWork
{
    public partial class frm_tprc_CardRePrint_U : Form
    {
        INI_GS gs = new INI_GS();
        WizWorkLib Lib = new WizWorkLib();
        LogData LogData = new LogData(); //2022-06-21 log 남기는 함수
        List<Sub_TWkLabelPrint> list_TWkLabelPrint = null;
        string[] Message = new string[2];

        static AutoResetEvent autoEvent = new AutoResetEvent(false); //2023-12-06

        public frm_tprc_CardRePrint_U()
        {
            InitializeComponent();
        }

        #region Default Grid Setting

        private void InitGrid()
        {
            grdData.Columns.Clear(); //체크박스나 콤보박스 사용시 필요하다.
            grdData.ColumnCount = 20;

            int i = 0;

            grdData.Columns[i].Name = "RowSeq";
            grdData.Columns[i].HeaderText = "No";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;


            grdData.Columns[++i].Name = "LabelID";
            grdData.Columns[i].HeaderText = "생산라벨번호";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "PrintDate";
            grdData.Columns[i].HeaderText = "발행일자";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "PrintTime";
            grdData.Columns[i].HeaderText = "발행시각";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "ReprintDate";
            grdData.Columns[i].HeaderText = "재발행일자";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "ReprintQty";
            grdData.Columns[i].HeaderText = "재발행수량";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;


            grdData.Columns[++i].Name = "Worker";
            grdData.Columns[i].HeaderText = "작업자";
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
            grdData.Columns[i].HeaderText = "오더번호";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;

            grdData.Columns[++i].Name = "QtyPerBox";
            grdData.Columns[i].HeaderText = "수량";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;

            grdData.Columns[++i].Name = "BuyerArticleNo";
            grdData.Columns[i].HeaderText = "품번";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;

            grdData.Columns[++i].Name = "OrderArticleID";
            grdData.Columns[i].HeaderText = "품목코드";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;

            grdData.Columns[++i].Name = "Article";
            grdData.Columns[i].HeaderText = "품명";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "KCustom";
            grdData.Columns[i].HeaderText = "거래처";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;           

            grdData.Columns[++i].Name = "Model";
            grdData.Columns[i].HeaderText = "차종";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;

            grdData.Columns[++i].Name = "LastProdArticleID";
            grdData.Columns[i].HeaderText = "최종품품명코드ID";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;
           
            grdData.Columns[++i].Name = "WorkDate";
            grdData.Columns[i].HeaderText = "작업일자";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;

            grdData.Columns[++i].Name = "WorkQty";
            grdData.Columns[i].HeaderText = "작업수량";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "ArticleID";
            grdData.Columns[i].HeaderText = "ArticleID";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = false;
            grdData.Columns[i].Visible = false;

            grdData.Columns[++i].Name = "ProcessID";
            grdData.Columns[i].HeaderText = "ProcessID";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = false;
            grdData.Columns[i].Visible = false;

            DataGridViewCheckBoxColumn curCol = new DataGridViewCheckBoxColumn();
            curCol.HeaderText = "선택";
            curCol.Name = "Check";
            curCol.Width = 50;
            curCol.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns.Insert(1, curCol);

            grdData.Font = new Font("맑은 고딕", 12, FontStyle.Bold);
            grdData.RowTemplate.Height = 30;
            grdData.ColumnHeadersHeight = 35;
            grdData.ScrollBars = ScrollBars.Both;


            foreach (DataGridViewColumn col in grdData.Columns)
            {
                col.DataPropertyName = col.Name;
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        #endregion

        private void btnNew_Click(object sender, EventArgs e)
        {
            LogData.LogSave(this.GetType().Name, "R"); //2022-06-22 조회
            btnNew.Enabled = false;
            Lib.Delay(3000); //2021-11-10 버튼을 여러번 클릭해도 한번만 클릭되게 딜레이 추가

            FillGridData();
            btnNew.Enabled = true;
        }

        private void FillGridData()
        {
            try
            {
                grdData.Rows.Clear();
                string sLabelGubun = "";
                string sInstID = "";
                int nChkDate = chkInstDate.Checked == true ? 1 : 0;
                string sSDate = mtb_From.Text.Replace("-", "");
                string sEDate = mtb_To.Text.Replace("-", "");

                
                foreach (Control rbn in tlpSearch_LL.Controls)
                {
                    if (rbn is RadioButton)
                    {
                        RadioButton chkrbn = rbn as RadioButton;
                        if (chkrbn.Checked)
                        {
                            sLabelGubun = rbn.Tag.ToString();
                            break;
                        }
                    }
                }

                // 2020.04.13 둘리 품번으로 검색, 재 발행인데 라벨 번호로 검색을 하는건 거의 불가능 하지 않슴니까
                int nChkBuyerArticleNo = chkBuyerArticleNo.Checked == true ? 1 : 0;
                if (txtBuyerArticleNo.Text.ToUpper().Contains("PL") && (txtBuyerArticleNo.Text.Trim().Length == 15 || txtBuyerArticleNo.Text.Trim().Length == 16))
                {
                    sInstID = txtBuyerArticleNo.Text.Trim().Substring(2, 12);
                }

                int nChkLabelID = chkLabelID.Checked == true ? 1 : 0;


                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();

                sqlParameter.Add("nChkDate", nChkDate);//상위품ID
                sqlParameter.Add("FromDate", sSDate);
                sqlParameter.Add("ToDate", sEDate);
                sqlParameter.Add("LabelGubun", sLabelGubun);
                sqlParameter.Add("nChkInstID", nChkLabelID);// 공정이동 전표 라벨 스캔 체크
                sqlParameter.Add("InstID", txtInstID.Text.Trim());//상위품ID
                sqlParameter.Add("nBuyerArticleNo", nChkBuyerArticleNo);//상위품ID
                sqlParameter.Add("BuyerArticleNo", txtBuyerArticleNo.Text.Trim());//상위품ID

                
                DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_prdWork_sCardLabelPrint", sqlParameter, false);
                DataRow dr = null;
                int QtyPerBox = 0;
                int ReprintQty = 0;
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dr = dt.Rows[i];
                        int.TryParse(Lib.CheckNum(dr["ReprintQty"].ToString()), out ReprintQty);
                        int.TryParse(Lib.CheckNum(dr["QtyPerBox"].ToString()), out QtyPerBox);
                        grdData.Rows.Add((i + 1).ToString(),//Index
                                         false,                     //선택
                                         dr["LabelID"].ToString(),  //라벨ID
                                         Lib.MakeDateTime("yyyymmdd", dr["PrintDate"].ToString().Trim()),
                                         Lib.MakeDateTime("HHmmSS", dr["WorkEndTime"].ToString().Trim()),

                                         Lib.MakeDateTime("yyyymmdd", dr["ReprintDate"].ToString().Trim()),   //'재발행일자
                                         string.Format("{0:n0}", ReprintQty),       // 재발행'수량

                                         dr["Worker"].ToString(),          // '작업자  
                                         dr["InstID"].ToString(),       // 'InstID       
                                         dr["OrderID"].ToString(),           // 'OrderID  
                                         string.Format("{0:n0}", QtyPerBox),   //'수량
                                         dr["BuyerArticleNo"].ToString(),    //'품번  
                                         dr["OrderArticleID"].ToString().Trim(),         //'품명
                                         dr["Article"].ToString(),         //'품명    
                                         dr["KCUSTOM"].ToString(),             //  '거래처                                         
                                         dr["Model"].ToString(),            //'차종   
                                         dr["LastProdArticleID"].ToString().Trim(),   //'마지막제품 품명코드ID                                           
                                         "",//dr["WorkDate"].ToString(),          //'작업일자                 
                                         string.Format("{0:n0}", dr["WorkQty"]),         // 마지막 작업 수량
                                         dr["ArticleID"].ToString(), //ArticleID
                                         dr["ProcessID"].ToString()  //ProcessID
                        );
                    }
                    grdData.ClearSelection();
                    if (grdData.Rows.Count > 0) { grdData[0, 0].Selected = true; } //0번째 행 선택 
                    Frm_tprc_Main.gv.queryCount = string.Format("{0:n0}", grdData.RowCount);
                    Frm_tprc_Main.gv.SetStbInfo();
                }
                else
                {
                    WizCommon.Popup.MyMessageBox.ShowBox("0개의 자료가 검색되었습니다.", "[조회]", 0, 1); //2021-11-25 메세지창 추가
                    ((WizWork.Frm_tprc_Main)(this.MdiParent)).stsInfo_Msg.Text = "0개의 자료가 검색되었습니다.";
                    grdData.Rows.Clear();
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
        }

        private void cmdRowUp_Click(object sender, EventArgs e)
        {
            Lib.btnRowUp(grdData);
        }

        private void cmdRowDown_Click(object sender, EventArgs e)
        {
            Lib.btnRowDown(grdData);
        }

        private void cmdBarcode_Click(object sender, EventArgs e)
        {
            if (CheckData() == true)
            {
                if (SaveData() == true)
                {
                    LogData.LogSave(this.GetType().Name, "P"); //2022-06-22 인쇄, 재발행
                    FillGridData();
                    LogData.LogSave(this.GetType().Name, "R"); //2022-06-22 조회
                }

            }
        }
        private bool CheckData()
        {
            bool IsOk = true;
            if (grdData.Rows.Count > 0)
            {
                bool hasCheck = false;
                foreach (DataGridViewRow dr in grdData.Rows)
                {
                    string strReprintDate = dr.Cells["ReprintDate"].Value.ToString().Trim();
                    bool isChecked = (bool)dr.Cells["Check"].EditedFormattedValue;
                    if (isChecked)//체크된
                    {
                        if (strReprintDate.Equals(""))
                        {
                            Message[0] = "[재발행일 확인]";
                            Message[1] = "재발행일자가 없을 경우 재발행되지 않습니다";
                            WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                            IsOk = false;
                        }
                        else
                        {
                            hasCheck = true;
                        }
                    }
                }
                
                if (!hasCheck)
                {
                    Message[0] = "[재발행라벨 선택]";
                    Message[1] = "재발행할 라벨을 선택해야 합니다";
                    WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                    IsOk = false;
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
            
            bool IsOK = true;
            int Count = 0;
            //if(grdData.Rows.Count == grdData.CurrentRow.Index)
            //{
            //    //MessageBox.Show(LoadResString(231));
            //}

            list_TWkLabelPrint = new List<Sub_TWkLabelPrint>(); // 초기화

            foreach (DataGridViewRow dr in grdData.Rows)
            {
                DataGridViewCell Cell = dr.Cells["Check"];
                bool isChecked = (bool)Cell.EditedFormattedValue;
                if (isChecked)
                {
                    list_TWkLabelPrint.Add(new Sub_TWkLabelPrint());
                    list_TWkLabelPrint[Count].sLabelID = dr.Cells["LabelID"].Value.ToString();
                    list_TWkLabelPrint[Count].sReprintDate = dr.Cells["ReprintDate"].Value.ToString().Replace("-","");
                    list_TWkLabelPrint[Count].nQtyPerBox = Int32.Parse(Lib.CheckNum(dr.Cells["QtyPerBox"].Value.ToString()).Replace(",",""));
                    list_TWkLabelPrint[Count].sCreateuserID = Frm_tprc_Main.g_tBase.PersonID;
                    list_TWkLabelPrint[Count].sLastProdArticleID = Lib.CheckNull(dr.Cells["LastProdArticleID"].Value.ToString().Trim());
                    list_TWkLabelPrint[Count].sInstID = dr.Cells["InstID"].Value.ToString();
                    list_TWkLabelPrint[Count].sArticleID = dr.Cells["ArticleID"].Value.ToString();
                    list_TWkLabelPrint[Count].sProcessID = dr.Cells["ProcessID"].Value.ToString();
                    Count++;
                }
            }
            
            UpdateWorkCardPrint(Count);

            return IsOK;
        }
        public void UpdateWorkCardPrint(int nCount)
        {
            if (list_TWkLabelPrint.Count > 0)
            {
                string g_sPrinterName = Lib.GetDefaultPrinter();

                //TSCLIB_DLL.openport(g_sPrinterName);//2021-11-29 한번만 열기
                
                List<string> list_Data = null;
                for (int i = 0; i < nCount; i++)
                {
                    TSCLIB_DLL.openport(g_sPrinterName);//한번만 열고 닫을 경우 여러장 할 경우 인쇄가 1장씩 안 되고 2~3장 인쇄되고 시간이 흐른 뒤에 나머지 장이 한꺼번에 나와 1장씩 인쇄되게 인쇄할때마다 열고 닫게 수정 2023-12-06

                    try
                    {
                        Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                        sqlParameter.Add("LabelID", list_TWkLabelPrint[i].sLabelID);//상위품ID
                        sqlParameter.Add("ReprintDate", list_TWkLabelPrint[i].sReprintDate);//상위품ID
                        sqlParameter.Add("UpdateUserID", list_TWkLabelPrint[i].sCreateuserID);//상위품ID

                        DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_WizWork_uCardLabelPrint", sqlParameter, false);


                        //string g_sPrinterName = Lib.GetDefaultPrinter();

                        string TagID = "";
                        string strProcessID = "";
                        list_Data = new List<string>();
                        //라벨선택
                        if (rbnProcessLabel.Checked) //<-- 검색 조건을 사용 할 수 있게 해달라고 요청 하여서 사용(2021.02.15 Khs)
                        //if(checkBox1.Checked)
                        {
                            //2024-06-04
                            TagID = "012";
                            //TagID = "014";
                            //nowProcessID = "0401";
                            list_TWkLabelPrint[i].sLabelGubun = "7";
                        }
                        //데이터셋팅
                        
                        int a = 0;
                        int count = 0;
                        double douworkqty = 0;
                        double doudefectqty = 0;
                        string effectdate = "";
                        string LabelArticleID = "";
                        foreach (DataRow dr in dt.Rows)
                        {
                            strProcessID = dr["ProcessID"].ToString();
                            LabelArticleID = dr["ArticleID"].ToString();
                            //if(dr["ProcSeq"].ToString() == "1")//&& strProcessID == nowProcessID) // 첫 공정일 때,
                            //2023-01-16
                            if (dr["ArticleID"].ToString().Trim() == list_TWkLabelPrint[i].sArticleID.ToString().Trim() && dr["ProcessID"].ToString().Trim() == list_TWkLabelPrint[i].sProcessID.ToString().Trim())
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

                        //지연 함수 2023-12-06
                        //ThreadPool.QueueUserWorkItem(new WaitCallback(WorkMethod), autoEvent);
                        // autoEvent가 Set이 될 때까지 기다림
                        //autoEvent.WaitOne();

                        //인쇄DLL 

                        frm_tprc_Work_U ftWU = new frm_tprc_Work_U();

                        //LabelArticleID = LabelArticleID.Substring(5, 1);

                        if (ftWU.SendWindowDllCommand(list_Data, TagID, 1,0)) 
                        {
                            //if (i == 0) //2021-11-29 라벨발행을 한꺼번에 처리하여 한번나오게 조건 추가
                            //{
                                Message[0] = "[라벨발행중]";
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

                        TSCLIB_DLL.closeport(); //한번만 열고 닫을 경우 여러장 할 경우 인쇄가 1장씩 안 되고 2~3장 인쇄되고 시간이 흐른 뒤에 나머지 장이 한꺼번에 나와 1장씩 인쇄되게 인쇄할때마다 열고 닫게 수정 2023-12-06                    
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
                //TSCLIB_DLL.closeport(); //2021-11-29 처리 다 되면 닫기  
            } 
        }

        //지연 함수 2023-12-06
        static void WorkMethod(object stateInfo)
        {
            //랜덤한 시간 대기
            //Thread.Sleep(new Random().Next(100, 2000));
            Thread.Sleep(1000);

            //대기 후 autoEvent를 Set 합니다
            ((AutoResetEvent)stateInfo).Set();
        }


        private void frm_tprc_CardRePrint_U_Load(object sender, EventArgs e)
        {
            LogData.LogSave(this.GetType().Name, "S"); //2022-06-22 사용시간(로드, 닫기)
            SetScreen();
            InitGrid();
            SetDateTime();

            rbnProcessLabel.Checked = true; //<-- 이 부분은 자동으로 체크 해놓아서 일단은 풀어서 사용함 (21.02.15 Khs)            
            FillGridData();
            LogData.LogSave(this.GetType().Name, "R"); //2022-06-22 조회
        }

        private void SetDateTime()
        {
            ////ini 날짜 불러와서 기간 설정하기
            chkInstDate.Checked = true;
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
            mtb_From.Text = DateTime.Today.AddDays(-Days).ToString("yyyyMMdd");
            mtb_To.Text = DateTime.Today.ToString("yyyyMMdd");
            //
        }

        #region TableLayoutPanel 하위 컨트롤들의 DockStyle.Fill 세팅
        private void SetScreen()
        {
            tlpForm.Dock = DockStyle.Fill;
            tlpForm.Margin = new Padding(0, 0, 0, 0);
            foreach (Control control in tlpForm.Controls)//con = tlp 상위에서 2번째
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
                        foreach (Control cont in contr.Controls)
                        {
                            cont.Dock = DockStyle.Fill;
                            cont.Margin = new Padding(0, 0, 0, 0);
                            foreach (Control con in cont.Controls)
                            {
                                con.Dock = DockStyle.Fill;
                                con.Margin = new Padding(0, 0, 0, 0);
                                foreach (Control co in con.Controls)
                                {
                                    co.Dock = DockStyle.Fill;
                                    co.Margin = new Padding(0, 0, 0, 0);
                                }
                            }
                        }
                    }
                }
            }

            //Main tlp 세팅
            tlp_Search_Date.SetRowSpan(chkInstDate, 2);
            tlpSearch_LL.SetColumnSpan(txtInstID, 2);
            tlpSearch_LL.SetColumnSpan(txtBuyerArticleNo, 2);

        }
        #endregion
        private void cmdClose_Click(object sender, EventArgs e)
        {
            LogData.LogSave(this.GetType().Name, "S"); //2022-06-22 사용시간(로드, 닫기)
            this.Dispose();
            this.Close();
        }

        private void grdData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                bool flag = (bool)grdData.Rows[e.RowIndex].Cells["Check"].Value;
                grdData.Rows[e.RowIndex].Cells["Check"].Value = !flag;
                if (!flag)
                {
                    if(grdData.Rows[e.RowIndex].Cells["ReprintDate"].Value.ToString().Equals(""))
                    {
                        grdData.Rows[e.RowIndex].Cells["ReprintDate"].Value = Lib.MakeDateTime("yyyymmdd", DateTime.Now.ToString("yyyyMMdd"));
                    }
                }
                else
                {
                    grdData.Rows[e.RowIndex].Cells["ReprintDate"].Value = "";
                }
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

        private void rbn_Click(object sender, EventArgs e)
        {
            FillGridData();
        }

        #region 둘리 - LotID 검색 → 품번 검색으로 변경

        private void chkBuyerArticleNo_Click(object sender, EventArgs e)
        {
            if (this.chkBuyerArticleNo.Checked)
            {
                txtBuyerArticleNo.Text = "";
                try
                {
                    chkBuyerArticleNo.Checked = true;

                    //2021-07-20
                    var path64 = System.IO.Path.Combine(Directory.GetDirectories(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "winsxs"), "amd64_microsoft-windows-osk_*")[0], "osk.exe");
                    var path32 = @"C:\windows\system32\osk.exe";
                    var path = (Environment.Is64BitOperatingSystem) ? path64 : path32;
                    if (File.Exists(path) && !Frm_tprc_Main.Lib.ReturnKillRunningProcess("osk"))
                    {
                        System.Diagnostics.Process.Start(path);

                        txtBuyerArticleNo.Focus();

                    }
                }
                catch (Exception ex)
                {
                    useMasicKeyboard(txtBuyerArticleNo);
                }
                //POPUP.Frm_CMKeypad keypad = new POPUP.Frm_CMKeypad("품번입력", "품번");

                //keypad.Owner = this;
                //if (keypad.ShowDialog() == DialogResult.OK)
                //{
                //    txtBuyerArticleNo.Text = keypad.tbInputText.Text;
                //}
            }
            else
            {
                this.txtBuyerArticleNo.Text = "";
            }
        }
        private void txtBuyerArticleNo_Click(object sender, EventArgs e)
        {
            if (this.chkBuyerArticleNo.Checked)
            {
               
                try
                {
                    chkBuyerArticleNo.Checked = true;
                    txtBuyerArticleNo.Text = "";
                    //2021-07-20
                    var path64 = System.IO.Path.Combine(Directory.GetDirectories(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "winsxs"), "amd64_microsoft-windows-osk_*")[0], "osk.exe");
                    var path32 = @"C:\windows\system32\osk.exe";
                    var path = (Environment.Is64BitOperatingSystem) ? path64 : path32;
                    if (File.Exists(path) && !Frm_tprc_Main.Lib.ReturnKillRunningProcess("osk"))
                    {
                        System.Diagnostics.Process.Start(path);

                        txtBuyerArticleNo.Focus();

                    }
                }
                catch (Exception ex)
                {
                    useMasicKeyboard(txtBuyerArticleNo);
                }
                //POPUP.Frm_CMKeypad keypad = new POPUP.Frm_CMKeypad("품번입력", "품번");

                //keypad.Owner = this;
                //if (keypad.ShowDialog() == DialogResult.OK)
                //{
                //    txtBuyerArticleNo.Text = keypad.tbInputText.Text;
                //}
            }
            else
            {
                //chkBuyerArticleNo.Checked = true;
                //txtBuyerArticleNo.Text = "";

                try
                {
                    chkBuyerArticleNo.Checked = true;
                    txtBuyerArticleNo.Text = "";
                    //2021-07-20
                    var path64 = System.IO.Path.Combine(Directory.GetDirectories(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "winsxs"), "amd64_microsoft-windows-osk_*")[0], "osk.exe");
                    var path32 = @"C:\windows\system32\osk.exe";
                    var path = (Environment.Is64BitOperatingSystem) ? path64 : path32;
                    if (File.Exists(path) && !Frm_tprc_Main.Lib.ReturnKillRunningProcess("osk"))
                    {
                        System.Diagnostics.Process.Start(path);

                        txtBuyerArticleNo.Focus();

                    }
                }
                catch (Exception ex)
                {
                    useMasicKeyboard(txtBuyerArticleNo);
                }
                //POPUP.Frm_CMKeypad keypad = new POPUP.Frm_CMKeypad("품번입력", "품번");

                //keypad.Owner = this;
                //if (keypad.ShowDialog() == DialogResult.OK)
                //{
                //    txtBuyerArticleNo.Text = keypad.tbInputText.Text;
                //}
            }
        }

        #endregion

        private void chkLabelID_Click(object sender, EventArgs e)
        {
            if (this.chkLabelID.Checked)
            {
                txtInstID.Text = "";

                POPUP.Frm_CMKeypad keypad = new POPUP.Frm_CMKeypad("공정전표", "전표");

                keypad.Owner = this;
                if (keypad.ShowDialog() == DialogResult.OK)
                {
                    txtInstID.Text = keypad.tbInputText.Text;
                }
            }
            else
            {
                this.txtInstID.Text = "";
            }
        }

        private void txtInstID_Click(object sender, EventArgs e)
        {
            if (this.rbnProcessLabel.Checked)
            {
                
                //chkLabelID.Checked = true;
                txtInstID.Text = "";

                POPUP.Frm_CMKeypad keypad = new POPUP.Frm_CMKeypad("공정전표", "전표");

                keypad.Owner = this;
                if (keypad.ShowDialog() == DialogResult.OK)
                {
                    txtInstID.Text = keypad.tbInputText.Text;
                }
            }
            else
            {
                rbnProcessLabel.Checked = true;
                txtInstID.Text = "";

                POPUP.Frm_CMKeypad keypad = new POPUP.Frm_CMKeypad("공정전표", "전표");

                keypad.Owner = this;
                if (keypad.ShowDialog() == DialogResult.OK)
                {
                    txtInstID.Text = keypad.tbInputText.Text;
                }
            }
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
                    System.Diagnostics.Process.Start(@"C:\windows\SysWOW64\osk.exe");
                    Console.Write(ex2.Message);
                }

            }
            txtBox.Select();
            txtBox.Focus();
        }

    }
}
