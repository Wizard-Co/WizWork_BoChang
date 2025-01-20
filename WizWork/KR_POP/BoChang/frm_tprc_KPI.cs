using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WizWork.Properties;
using WizCommon;

namespace WizWork
{
    public partial class frm_tprc_KPI : Form
    {
        

        public frm_tprc_KPI()
        {
            InitializeComponent();
        }

        private void frm_tprc_KPI_Load(object sender, EventArgs e)
        {
            initGrid();
        }

        #region datagridview

        private void initGrid()
        {
            dgvKPI.Columns.Clear();
            dgvKPI.ColumnCount = 17;

            int i = 0;

            dgvKPI.Columns[i].Name = "InstID";
            dgvKPI.Columns[i].HeaderText = "지시번호";
            dgvKPI.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvKPI.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvKPI.Columns[i].ReadOnly = true;
            dgvKPI.Columns[i].Visible = true;

            dgvKPI.Columns[++i].Name = "InstDetSeq";
            dgvKPI.Columns[i].HeaderText = "지시번호순서";
            dgvKPI.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvKPI.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvKPI.Columns[i].ReadOnly = true;
            dgvKPI.Columns[i].Visible = true;

            dgvKPI.Columns[++i].Name = "Process";
            dgvKPI.Columns[i].HeaderText = "공정";
            dgvKPI.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvKPI.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvKPI.Columns[i].ReadOnly = true;
            dgvKPI.Columns[i].Visible = true;

            dgvKPI.Columns[++i].Name = "ProcessID";
            dgvKPI.Columns[i].HeaderText = "ProcessID";
            dgvKPI.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvKPI.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvKPI.Columns[i].ReadOnly = true;
            dgvKPI.Columns[i].Visible = true;

            dgvKPI.Columns[++i].Name = "StartDate";
            dgvKPI.Columns[i].HeaderText = "시작일자";
            dgvKPI.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvKPI.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvKPI.Columns[i].ReadOnly = true;
            dgvKPI.Columns[i].Visible = true;

            dgvKPI.Columns[++i].Name = "StartTime";
            dgvKPI.Columns[i].HeaderText = "시작시간";
            dgvKPI.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvKPI.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvKPI.Columns[i].ReadOnly = true;
            dgvKPI.Columns[i].Visible = true;

            dgvKPI.Columns[++i].Name = "EndDate";
            dgvKPI.Columns[i].HeaderText = "종료일자";
            dgvKPI.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvKPI.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvKPI.Columns[i].ReadOnly = true;
            dgvKPI.Columns[i].Visible = true;

            dgvKPI.Columns[++i].Name = "EndTime";
            dgvKPI.Columns[i].HeaderText = "종료시간";
            dgvKPI.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvKPI.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvKPI.Columns[i].ReadOnly = true;
            dgvKPI.Columns[i].Visible = true;

            dgvKPI.Columns[++i].Name = "WorkQty";
            dgvKPI.Columns[i].HeaderText = "작업수량";
            dgvKPI.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvKPI.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvKPI.Columns[i].ReadOnly = true;
            dgvKPI.Columns[i].Visible = true;

            dgvKPI.Columns[++i].Name = "DefectQty";
            dgvKPI.Columns[i].HeaderText = "불량수량";
            dgvKPI.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvKPI.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvKPI.Columns[i].ReadOnly = true;
            dgvKPI.Columns[i].Visible = true;

            dgvKPI.Columns[++i].Name = "BoxQty";
            dgvKPI.Columns[i].HeaderText = "박스수량";
            dgvKPI.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvKPI.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvKPI.Columns[i].ReadOnly = true;
            dgvKPI.Columns[i].Visible = true;

            dgvKPI.Columns[++i].Name = "Person";
            dgvKPI.Columns[i].HeaderText = "작업자";
            dgvKPI.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvKPI.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvKPI.Columns[i].ReadOnly = true;
            dgvKPI.Columns[i].Visible = true;

            dgvKPI.Columns[++i].Name = "PersonID";
            dgvKPI.Columns[i].HeaderText = "작업자ID";
            dgvKPI.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvKPI.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvKPI.Columns[i].ReadOnly = true;
            dgvKPI.Columns[i].Visible = true;

            dgvKPI.Columns[++i].Name = "DayOrNightGbn";
            dgvKPI.Columns[i].HeaderText = "주/야";
            dgvKPI.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvKPI.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvKPI.Columns[i].ReadOnly = true;
            dgvKPI.Columns[i].Visible = true;

            dgvKPI.Columns[++i].Name = "DayOrNightGbnID";
            dgvKPI.Columns[i].HeaderText = "주/야ID";
            dgvKPI.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvKPI.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvKPI.Columns[i].ReadOnly = true;
            dgvKPI.Columns[i].Visible = true;

            dgvKPI.Columns[++i].Name = "JobGbn";
            dgvKPI.Columns[i].HeaderText = "작업구분";
            dgvKPI.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvKPI.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvKPI.Columns[i].ReadOnly = true;
            dgvKPI.Columns[i].Visible = true;

            dgvKPI.Columns[++i].Name = "JobGbnID";
            dgvKPI.Columns[i].HeaderText = "작업구분ID";
            dgvKPI.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvKPI.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvKPI.Columns[i].ReadOnly = true;
            dgvKPI.Columns[i].Visible = true;

            dgvKPI.Font = new Font("맑은 고딕", 10, FontStyle.Bold);
            dgvKPI.RowsDefaultCellStyle.Font = new Font("맑은 고딕", 10, FontStyle.Bold);
            dgvKPI.AlternatingRowsDefaultCellStyle.Font = new Font("맑은 고딕", 10, FontStyle.Bold);
            dgvKPI.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvKPI.MultiSelect = false;
            dgvKPI.ScrollBars = ScrollBars.Both;
            dgvKPI.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvKPI.EnableHeadersVisualStyles = false;  // 헤더 셀 스타일 적용 용도.

            foreach (DataGridViewColumn col in dgvKPI.Columns)
            {
                col.DataPropertyName = col.Name;
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

        }

        #endregion


        #region 추가, 저장, 닫기 버튼

        //닫기
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //저장
        private void button2_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {

            }
        }

        //추가
        private void button3_Click(object sender, EventArgs e)
        {
            dgvKPI.Rows.Add("", "", "", "", "", "", "", "", "", "", "");
        }


        #endregion

        private void dgvKPI_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (e.ColumnIndex == 0 || e.ColumnIndex == 1 || e.ColumnIndex == 2 || e.ColumnIndex == 3)
                {

                    Frm_PopUpSel_KPI_InstID FPKI = new Frm_PopUpSel_KPI_InstID();
                    FPKI.StartPosition = FormStartPosition.CenterScreen;
                    FPKI.BringToFront();
                    //FPKI.TopMost = true;

                    if (FPKI.ShowDialog() == DialogResult.OK)
                    {
                        dgvKPI.Rows[e.RowIndex].Cells["InstID"].Value = FPKI.InstID;
                        dgvKPI.Rows[e.RowIndex].Cells["InstDetSeq"].Value = FPKI.InstDetSeq;
                        dgvKPI.Rows[e.RowIndex].Cells["Process"].Value = FPKI.Process;
                        dgvKPI.Rows[e.RowIndex].Cells["ProcessID"].Value = FPKI.ProcessID;
                        dgvKPI.Rows[e.RowIndex].Cells["StartDate"].Value = FPKI.StartDate;
                        dgvKPI.Rows[e.RowIndex].Cells["StartTime"].Value = FPKI.StartTime;
                        dgvKPI.Rows[e.RowIndex].Cells["EndDate"].Value = FPKI.EndDate;
                        dgvKPI.Rows[e.RowIndex].Cells["EndTime"].Value = FPKI.EndTime;
                    }
                }
                else if (e.ColumnIndex > 3 && e.ColumnIndex < 11)
                {

                    POPUP.Frm_CMNumericKeypad keypad = new POPUP.Frm_CMNumericKeypad("입력", "수량");

                    keypad.Owner = this;
                    if (keypad.ShowDialog() == DialogResult.OK)
                    {
                        if (e.ColumnIndex == 4)
                        {
                            dgvKPI.Rows[e.RowIndex].Cells["StartDate"].Value = keypad.tbInputText.Text;
                        }
                        else if (e.ColumnIndex == 5)
                        {
                            dgvKPI.Rows[e.RowIndex].Cells["StartTime"].Value = keypad.tbInputText.Text;
                        }
                        else if (e.ColumnIndex == 6)
                        {
                            dgvKPI.Rows[e.RowIndex].Cells["EndDate"].Value = keypad.tbInputText.Text;
                        }
                        else if (e.ColumnIndex == 7)
                        {
                            dgvKPI.Rows[e.RowIndex].Cells["EndTime"].Value = keypad.tbInputText.Text;
                        }
                        else if (e.ColumnIndex == 8)
                        {
                            dgvKPI.Rows[e.RowIndex].Cells["WorkQty"].Value = keypad.tbInputText.Text;
                        }
                        else if (e.ColumnIndex == 9)
                        {
                            dgvKPI.Rows[e.RowIndex].Cells["DefectQty"].Value = keypad.tbInputText.Text;
                        }
                        else if (e.ColumnIndex == 10)
                        {
                            dgvKPI.Rows[e.RowIndex].Cells["BoxQty"].Value = keypad.tbInputText.Text;
                        }

                    }
                    
                }
                else if (e.ColumnIndex == 11 || e.ColumnIndex == 12) //작업자
                {
                    Frm_PopUpSel_KPI_InstID FPKI = new Frm_PopUpSel_KPI_InstID("P");
                    FPKI.StartPosition = FormStartPosition.CenterScreen;
                    FPKI.BringToFront();
                    //FPKI.TopMost = true;

                    if (FPKI.ShowDialog() == DialogResult.OK)
                    {
                        dgvKPI.Rows[e.RowIndex].Cells["PersonID"].Value = FPKI.PersonID;
                        dgvKPI.Rows[e.RowIndex].Cells["Person"].Value = FPKI.Person;
                    }
                }
                else if (e.ColumnIndex == 13 || e.ColumnIndex == 14) //주/야간
                {
                    Frm_PopUpSel_KPI_InstID FPKI = new Frm_PopUpSel_KPI_InstID("DN");
                    FPKI.StartPosition = FormStartPosition.CenterScreen;
                    FPKI.BringToFront();
                    //FPKI.TopMost = true;

                    if (FPKI.ShowDialog() == DialogResult.OK)
                    {
                        dgvKPI.Rows[e.RowIndex].Cells["DayOrNightGbnID"].Value = FPKI.DayOrNightID;
                        dgvKPI.Rows[e.RowIndex].Cells["DayOrNightGbn"].Value = FPKI.DayOrNight;
                    }
                }
                else if (e.ColumnIndex == 15 || e.ColumnIndex == 16) //작업구분
                {
                    Frm_PopUpSel_KPI_InstID FPKI = new Frm_PopUpSel_KPI_InstID("WG");
                    FPKI.StartPosition = FormStartPosition.CenterScreen;
                    FPKI.BringToFront();
                    //FPKI.TopMost = true;

                    if (FPKI.ShowDialog() == DialogResult.OK)
                    {
                        dgvKPI.Rows[e.RowIndex].Cells["JobGbnID"].Value = FPKI.JobGbnID;
                        dgvKPI.Rows[e.RowIndex].Cells["JobGbn"].Value = FPKI.JobGbn;
                    }
                }
            }

        }


        private bool SaveData()
        {
            try
            {
                List<WizCommon.Procedure> Prolist = new List<WizCommon.Procedure>();
                List<List<string>> ListProcedureName = new List<List<string>>();
                List<Dictionary<string, object>> ListParameter = new List<Dictionary<string, object>>();

                for (int i = 0; i < dgvKPI.Rows.Count; i++)
                {             
                    Dictionary<string, object> sqlParameter = new Dictionary<string, object>();

                    sqlParameter.Add("InstID", dgvKPI.Rows[i].Cells["InstID"].Value.ToString());
                    sqlParameter.Add("InstDetSeq", Convert.ToInt32(dgvKPI.Rows[i].Cells["InstDetSeq"].Value.ToString()));
                    sqlParameter.Add("ProcessID", dgvKPI.Rows[i].Cells["ProcessID"].Value.ToString());
                    sqlParameter.Add("StartDate", dgvKPI.Rows[i].Cells["StartDate"].Value.ToString());
                    sqlParameter.Add("StartTime", dgvKPI.Rows[i].Cells["StartTime"].Value.ToString());
                    sqlParameter.Add("EndDate", dgvKPI.Rows[i].Cells["EndDate"].Value.ToString());
                    sqlParameter.Add("EndTime", dgvKPI.Rows[i].Cells["EndTime"].Value.ToString());
                    sqlParameter.Add("WorkQty", Convert.ToInt32(dgvKPI.Rows[i].Cells["WorkQty"].Value.ToString()));
                    sqlParameter.Add("DefectQty", Convert.ToInt32(dgvKPI.Rows[i].Cells["DefectQty"].Value.ToString()));
                    sqlParameter.Add("BoxQty", Convert.ToInt32(dgvKPI.Rows[i].Cells["BoxQty"].Value.ToString()));
                    sqlParameter.Add("CreateUserID", dgvKPI.Rows[i].Cells["PersonID"].Value.ToString());
                    sqlParameter.Add("DayOrNightID", dgvKPI.Rows[i].Cells["DayOrNightGbnID"].Value.ToString());
                    sqlParameter.Add("JobGbn", dgvKPI.Rows[i].Cells["JobGbnID"].Value.ToString());

                    WizCommon.Procedure pro1 = new WizCommon.Procedure();
                    pro1.Name = "[xp_WizWork_KPI_i]";
                    pro1.OutputUseYN = "N";
                    pro1.OutputName = "InstID";
                    pro1.OutputLength = "20";

                    Prolist.Add(pro1);
                    ListParameter.Add(sqlParameter);


                    //Dictionary<string, object> sqlParameter1 = new Dictionary<string, object>();
                    //WizCommon.Procedure pro2 = new WizCommon.Procedure();

                    //sqlParameter1.Add("JobID", 0);
                    //sqlParameter1.Add("InstID", list_TWkResult[i].InstID);
                    //sqlParameter1.Add("InstDetSeq", list_TWkResult[i].InstDetSeq);
                    //if (list_TWkLabelPrint.Count > i)
                    //{
                    //    sqlParameter1.Add("LabelID", list_TWkLabelPrint[i].sLabelID);
                    //}
                    //else
                    //{
                    //    sqlParameter1.Add("LabelID", list_TWkResult[i].LabelID);
                    //}

                    //#region 주석 2021-12-08 일괄 스캔시에도 선라벨이 들어가게 수정함
                    //// 일괄 스캔 → 스타트 라벨 세팅을 어떻게 하지?	
                    //if (i > 0 && lstLabelList.Count > 0)
                    //{
                    //    sqlParameter1.Add("StartSaveLabelID", lstLabelList[i - 1]);
                    //    //sqlParameter1.Add("StartSaveLabelID", lstLabelList[0]); //2021-12-08 일괄스캔 시 오류가 나타나 선라벨이 들어가게 수정
                    //}
                    //else
                    //{
                    //    sqlParameter1.Add("StartSaveLabelID", txtPreInsertLabelBarCode.Text);
                    //}
                    //#endregion

                    ////sqlParameter1.Add("StartSaveLabelID", txtPreInsertLabelBarCode.Text); //2021-12-08 수정

                    //sqlParameter1.Add("LabelGubun", list_TWkResult[i].LabelGubun);
                    //sqlParameter1.Add("ProcessID", list_TWkResult[i].ProcessID);
                    //sqlParameter1.Add("MachineID", list_TWkResult[i].MachineID);
                    //sqlParameter1.Add("ScanDate", list_TWkResult[i].ScanDate);
                    //sqlParameter1.Add("ScanTime", list_TWkResult[i].ScanTime);

                    //sqlParameter1.Add("ArticleID", list_TWkResult[i].ArticleID);
                    //sqlParameter1.Add("WorkQty", list_TWkResult[i].WorkQty);
                    //sqlParameter1.Add("Comments", list_TWkResult[i].Comments);
                    //sqlParameter1.Add("ReworkOldYN", list_TWkResult[i].ReworkOldYN);
                    //sqlParameter1.Add("ReworkLinkProdID", list_TWkResult[i].ReworkLinkProdID);

                    //sqlParameter1.Add("WorkStartDate", list_TWkResult[i].WorkStartDate);
                    //sqlParameter1.Add("WorkStartTime", list_TWkResult[i].WorkStartTime);
                    //sqlParameter1.Add("WorkEndDate", list_TWkResult[i].WorkEndDate);
                    //sqlParameter1.Add("WorkEndTime", list_TWkResult[i].WorkEndTime);
                    //sqlParameter1.Add("JobGbn", list_TWkResult[i].JobGbn);

                    //sqlParameter1.Add("NoReworkCode", list_TWkResult[i].sNowReworkCode);
                    //sqlParameter1.Add("WDNO", list_TWkResult[i].WDNO);
                    //sqlParameter1.Add("WDID", list_TWkResult[i].WDID);
                    //sqlParameter1.Add("WDQty", list_TWkResult[i].WDQty);
                    //sqlParameter1.Add("LogID", list_TWkResult[i].sLogID);

                    //sqlParameter1.Add("s4MID", list_TWkResult[i].s4MID);
                    //sqlParameter1.Add("DayOrNightID", list_TWkResult[i].DayOrNightID);
                    //sqlParameter1.Add("SplitYNGBN", list_TWkResult[i].SplitYNGBN);
                    //sqlParameter1.Add("CycleTime", list_TWkResult[i].CycleTime);
                    //sqlParameter1.Add("CreateUserID", list_TWkResult[i].CreateUserID);

                    //pro2.Name = "xp_wkResult_iWkResult";
                    //pro2.OutputUseYN = "Y";
                    //pro2.OutputName = "JobID";
                    //pro2.OutputLength = "20";


                    //Dictionary<string, object> sqlParameter2 = new Dictionary<string, object>();

                    //sqlParameter2.Add("ChildLabelID", list_TWkResultArticleChild[k].ChildLabelID);//	                                   
                    //                                                                              //sqlParameter2.Add("ChildLabelID", list_TWkResultArticleChild[k].ChildLabelID); //2021-12-06 수정
                    //sqlParameter2.Add("ChildLabelGubun", list_TWkResultArticleChild[k].ChildLabelGubun);
                    //sqlParameter2.Add("ChildArticleID", list_TWkResultArticleChild[k].ChildArticleID);
                    //sqlParameter2.Add("ReworkOldYN", list_TWkResultArticleChild[k].ReworkOldYN);
                    //sqlParameter2.Add("ReworkLinkChildProdID", list_TWkResultArticleChild[k].ReworkLinkChildProdID);
                    //sqlParameter2.Add("CreateUserID", list_TWkResultArticleChild[k].CreateUserID);

                    //if (i > 0 && lstLabelList.Count > 0)
                    //{
                    //    sqlParameter2.Add("ChildUseQty", list_TWkResult[i].WorkQty * list_TWkResultArticleChild[k].ReqQty);
                    //    //sqlParameter1.Add("StartSaveLabelID", lstLabelList[0]); //2021-12-08 일괄스캔 시 오류가 나타나 선라벨이 들어가게 수정
                    //}
                    //else
                    //{
                    //    sqlParameter2.Add("ChildUseQty", LabelPaper_Qty * list_TWkResultArticleChild[k].ReqQty);
                    //}

                    //WizCommon.Procedure pro3 = new WizCommon.Procedure();
                    //pro3.Name = "xp_wkResult_iWkResultArticleChild";
                    //pro3.OutputUseYN = "N";
                    //pro3.OutputName = "JobID";
                    //pro3.OutputLength = "20";

                    //Prolist.Add(pro3);
                    //ListParameter.Add(sqlParameter2);


                    //Dictionary<string, object> sqlParameter4 = new Dictionary<string, object>();

                    //sqlParameter4.Add("DefectID", Key.Trim());
                    //sqlParameter4.Add("DefectQty", ConvertDouble(Defect.DefectQty));
                    //sqlParameter4.Add("XPos", ConvertInt(Defect.XPos));
                    //sqlParameter4.Add("YPos", ConvertInt(Defect.YPos));
                    //sqlParameter4.Add("JobID", list_TWkResult[0].JobID);
                    //sqlParameter4.Add("CreateUserID", list_TWkResult[0].CreateUserID);

                    //WizCommon.Procedure pro4 = new WizCommon.Procedure();
                    //pro4.Name = "xp_prdWork_iWorkDefect";
                    //pro4.OutputUseYN = "N";
                    //pro4.OutputName = "JobID";
                    //pro4.OutputLength = "20";

                    //Prolist.Add(pro4);
                    //ListParameter.Add(sqlParameter4);


                    //Dictionary<string, object> sqlParameter6 = new Dictionary<string, object>();

                    //if (i == 0)
                    //{
                    //    sqlParameter6.Add("JobID", list_TWkResult[i].JobID);
                    //}
                    //else
                    //{
                    //    sqlParameter6.Add("JobID", 0);
                    //}

                    //sqlParameter6.Add("MoldID", MoldList[x].ToString());
                    //sqlParameter6.Add("RealCavity", 0);
                    //sqlParameter6.Add("HitCount", list_TWkResult[i].WorkQty);
                    //sqlParameter6.Add("CreateUserID", list_TWkResult[0].CreateUserID);

                    //WizCommon.Procedure pro7 = new WizCommon.Procedure();
                    //pro7.Name = "xp_wkResult_iWkResultMold";
                    //pro7.OutputUseYN = "N";
                    //pro7.OutputName = "JobID";
                    //pro7.OutputLength = "20";

                    //Prolist.Add(pro7);
                    //ListParameter.Add(sqlParameter6);
                }


                List<KeyValue> list_Result = new List<KeyValue>();
                list_Result = DataStore.Instance.ExecuteAllProcedureOutputToCS(Prolist, ListParameter);

                if (list_Result[0].key.ToLower() == "success")
                {
                    list_Result.RemoveAt(0);

                    int a = 0;
                  
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

            return true;
        }
    }
}
