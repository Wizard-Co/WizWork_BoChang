using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows.Forms;
using WizCommon;
using System.Diagnostics;

namespace WizInOut
{
    public partial class Frm_tinout_PopUpSel_Excel : Form
    {
        WizWorkLib Lib = new WizWorkLib();
        string[] Message = new string[2];
        string OutwareID = "";
        string OrderID = "";

        //거래명세서 엑셀 추가
        Excel.Application excelApp = new Excel.Application();
        Excel.Workbook workbook;
        Excel.Worksheet worksheet;
        Excel.Range range;

        public Frm_tinout_PopUpSel_Excel(string outwareID, string orderID)
        {
            InitializeComponent();
            this.OutwareID = outwareID;
            this.OrderID = orderID;
        }

        private void Frm_tinout_PopUpSel_Excel_Load(object sender, EventArgs e)
        {
            //엑셀 인쇄 하고 닫기

            ExcelData();

            this.Close();
        }


        #region 프린터 함수(Excel)

        private void ExcelData()
        {
            string g_sPrinterName = "";
            g_sPrinterName = Lib.GetDefaultPrinter();
            List<string> Printlist_Data = null;     //상단
            List<string> Printlist_Data_s = null;   //하단

            int i = 0; //구분

            try
            {
                //상단
                Printlist_Data = new List<string>();
                Printlist_Data_s = new List<string>();

                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();

                sqlParameter.Add("OrderID", OrderID);          //orderID
                sqlParameter.Add("OutwareID", OutwareID);                       //OutwareID

                DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_WizWork_sPrint_Excel", sqlParameter, false);

                foreach (DataRow dr in dt.Rows)
                {
                    //공급받는자
                    Printlist_Data.Add(Lib.CheckNull(dr["OutDate"].ToString())); //거래일자
                    Printlist_Data.Add(Lib.CheckNull(dr["KCustom"].ToString()));//상호(거래처)
                    Printlist_Data.Add(Lib.CheckNull(dr["Address1"].ToString())); //사업장 주소
                    Printlist_Data.Add(Lib.CheckNull(dr["Chief"].ToString()));//성명
                    Printlist_Data.Add("");//합계금액

                    //공급자
                    Printlist_Data.Add(Lib.CheckNull(dr["CompanyNo"].ToString()));//등록번호
                    Printlist_Data.Add(Lib.CheckNull(dr["KCompany"].ToString()));//상호
                    Printlist_Data.Add(Lib.CheckNull(dr["PChief"].ToString()));//대표자
                    Printlist_Data.Add(Lib.CheckNull(dr["PAddress"].ToString()));//사업장주소
                    Printlist_Data.Add(Lib.CheckNull(dr["Phone1"].ToString()));//전화
                    Printlist_Data.Add(Lib.CheckNull(dr["FaxNo"].ToString()));//팩스
                }

                DataStore.Instance.CloseConnection();

                //하단
                Dictionary<string, object> sqlParameter1 = new Dictionary<string, object>();

                sqlParameter1.Add("OrderID", OrderID);          //orderID
                sqlParameter1.Add("OutwareID", OutwareID);                       //OutwareID

                DataTable dt2 = DataStore.Instance.ProcedureToDataTable("xp_WizWork_sPrint_Detail_Excel", sqlParameter1, false);
                if (dt2.Rows.Count > 0)
                {
                    foreach (DataRow dr2 in dt2.Rows)
                    {
                        i++;
                        Printlist_Data_s.Add(i.ToString());               //구분
                        Printlist_Data_s.Add(Lib.CheckNull(dr2["BuyerArticleNo"].ToString()));       //품번
                        Printlist_Data_s.Add(Lib.CheckNull(dr2["UnitClssName"].ToString()));         //단위
                        Printlist_Data_s.Add(Lib.CheckNull(dr2["OutQty"].ToString()));               //납품수량
                        Printlist_Data_s.Add(Lib.CheckNull(dr2["LabelID"].ToString()));              //LOTNO
                    }
                }

                DataStore.Instance.CloseConnection();

                ExcelPrint(Printlist_Data, Printlist_Data_s, i);
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

        private void ExcelPrint(List<string> Printlist_Data, List<string> Printlist_Data_s, int Num)
        {
            try
            {
                excelApp = null;
                excelApp = new Excel.Application();

                string excelopen_path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\wizwork_출고거래명세표.xlsx";

                workbook = excelApp.Workbooks.Open(excelopen_path, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                                Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                                Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                                Type.Missing, Type.Missing);

                worksheet = (Excel.Worksheet)workbook.Sheets["Form"];

                //공급받는자
                //거래일자
                range = worksheet.get_Range("C4", "H4");
                range.Value2 = Printlist_Data[0].Substring(0, 4) + "." + Printlist_Data[0].Substring(4, 2) + "." + Printlist_Data[0].Substring(6, 2);
                range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                //상호
                range = worksheet.get_Range("G5", "P6");
                range.Value2 = Printlist_Data[1];
                range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                //사업장주소
                range = worksheet.get_Range("G7", "R8");
                range.Value2 = Printlist_Data[2];
                range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                //성명
                range = worksheet.get_Range("G9", "R10");
                range.Value2 = Printlist_Data[3];
                range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                //공급자
                //등록번호
                range = worksheet.get_Range("W5", "AK6");
                range.Value2 = Printlist_Data[5];
                range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                //상호
                range = worksheet.get_Range("W7", "AC8");
                range.Value2 = Printlist_Data[6];
                range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                //성명
                range = worksheet.get_Range("AF7", "AK8");
                range.Value2 = Printlist_Data[7];
                range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                //사업장주소
                range = worksheet.get_Range("W9", "AK10");
                range.Value2 = Printlist_Data[8];
                range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                //전화
                range = worksheet.get_Range("W11", "AC12");
                range.Value2 = Printlist_Data[9];
                range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                //팩스
                range = worksheet.get_Range("AF11", "AK12");
                range.Value2 = Printlist_Data[10];
                range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                //합계주소

                //품번이 다를 경우 다음줄에 입력
                //Num이 1이 아니면 다음줄이 있음
                //하단
                //구분
                for (int Count = 0; Count < Num; Count++)
                {
                    //range = worksheet.get_Range("D" + (14 + Num).ToString() , "F" + (14 + Num).ToString());
                    //range.Value2 = Printlist_Data_s[0];
                    //range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                    //품번
                    range = worksheet.get_Range("G" + (14 + Num).ToString(), "M" + (14 + Num).ToString());
                    range.Value2 = Printlist_Data_s[1];
                    range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                    //단위
                    range = worksheet.get_Range("N" + (14 + Num).ToString(), "O" + (14 + Num).ToString());
                    range.Value2 = Printlist_Data_s[2];
                    range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                    //납품수량
                    range = worksheet.get_Range("P" + (14 + Num).ToString(), "Q" + (14 + Num).ToString());
                    range.Value2 = Printlist_Data_s[3];
                    range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                    //LOTNO
                    range = worksheet.get_Range("W" + (14 + Num).ToString(), "AC" + (14 + Num).ToString());
                    range.Value2 = Printlist_Data_s[4];
                    range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                }

                worksheet.PrintOutEx(1, 1, 1);

                workbook.Close(false);
                excelApp.Quit();

                //excel process를 전부 닫음(실행중인 다른 excel도 전부 닫힘)
                Process[] process = Process.GetProcessesByName("EXCEL");
                foreach (Process p in process)
                {
                    if (!string.IsNullOrEmpty(p.ProcessName))
                    {
                        try
                        {
                            p.Kill();
                        }
                        catch
                        {

                        }
                    }
                }



            }
            catch (Exception e)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", e.Message), "[오류]", 0, 1);
            }

        }

        #endregion
    }
}
