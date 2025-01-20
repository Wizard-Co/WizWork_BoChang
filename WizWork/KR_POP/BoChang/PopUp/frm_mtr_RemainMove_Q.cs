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
    public partial class frm_mtr_RemainMove_Q : Form
    {
        string[] Message = new string[2];
        INI_GS gs = Frm_tprc_Main.gs;
        WizWorkLib Lib = Frm_tprc_Main.Lib;
        LogData LogData = new LogData(); //2022-06-21 log 남기는 함수
        DataTable dt = null;
        DataTable tab1_dt = null;
        DataTable tab2_dt = null;
        DataTable del_dt = null;
        int x = 0;//grdList 가로 이동용 변수
        int z = 0;//grdData 가로 이동용 변수
        /// <summary>
        /// 생성
        /// </summary>
        public frm_mtr_RemainMove_Q()
        {
            InitializeComponent();

        }
        private void frm_mtr_RemainMove_Q_Load(object sender, EventArgs e)
        {
            LogData.LogSave(this.GetType().Name, "S"); //2022-06-22 사용시간(로드, 닫기)
            SetScreen();
            SetDateTime();

            InitGrid();
            InitGrid2();
            ProcQuery();
            LogData.LogSave(this.GetType().Name, "R"); //2022-06-22 조회
        }
        private void SetDateTime()
        {
            ////ini 날짜 불러와서 기간 설정하기
            chkInsDate.Checked = true;
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
            
        }

        #region Default Grid Setting

        private void InitGrid()
        {
            grdList.Columns.Clear();
            grdList.ColumnCount = 14;

            int n = 0;
            // Set the Colums Hearder Names

            grdList.Columns[n].Name = "No";
            grdList.Columns[n].HeaderText = "";
            grdList.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            grdList.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            grdList.Columns[n].ReadOnly = true;
            grdList.Columns[n++].Visible = true;

            grdList.Columns[n].Name = "MoveID";
            grdList.Columns[n].HeaderText = "MoveID";
            grdList.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdList.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            grdList.Columns[n].ReadOnly = true;
            grdList.Columns[n++].Visible = false;

            grdList.Columns[n].Name = "MoveJobID";
            grdList.Columns[n].HeaderText = "이동\r\n번호";
            grdList.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdList.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            grdList.Columns[n].ReadOnly = true;
            grdList.Columns[n++].Visible = true;

            grdList.Columns[n].Name = "StuffDate";
            grdList.Columns[n].HeaderText = "이동일자";
            grdList.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdList.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdList.Columns[n].ReadOnly = true;
            grdList.Columns[n++].Visible = true;

            grdList.Columns[n].Name = "StuffinID";
            grdList.Columns[n].HeaderText = "StuffinID";
            grdList.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdList.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdList.Columns[n].ReadOnly = true;
            grdList.Columns[n++].Visible = false;

            grdList.Columns[n].Name = "StuffClss";
            grdList.Columns[n].HeaderText = "StuffClss";
            grdList.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdList.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdList.Columns[n].ReadOnly = true;
            grdList.Columns[n++].Visible = false;

            grdList.Columns[n].Name = "Article";
            grdList.Columns[n].HeaderText = "품명";
            grdList.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdList.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            grdList.Columns[n].ReadOnly = true;
            grdList.Columns[n++].Visible = true;

            grdList.Columns[n].Name = "ArticleID";
            grdList.Columns[n].HeaderText = "ArticleID";
            grdList.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdList.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdList.Columns[n].ReadOnly = true;
            grdList.Columns[n++].Visible = false;

            grdList.Columns[n].Name = "BuyerArticleNo";
            grdList.Columns[n].HeaderText = "품번";
            grdList.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdList.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdList.Columns[n].ReadOnly = true;
            grdList.Columns[n++].Visible = false;

            grdList.Columns[n].Name = "LotID";
            grdList.Columns[n].HeaderText = "LotID";
            grdList.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdList.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            grdList.Columns[n].ReadOnly = true;
            grdList.Columns[n++].Visible = true;

            grdList.Columns[n].Name = "MoveQty";
            grdList.Columns[n].HeaderText = "이동잔량";
            grdList.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdList.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            grdList.Columns[n].ReadOnly = true;
            grdList.Columns[n++].Visible = true;

            grdList.Columns[n].Name = "SumQty";
            grdList.Columns[n].HeaderText = "합계잔량";
            grdList.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grdList.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            grdList.Columns[n].ReadOnly = true;
            grdList.Columns[n++].Visible = true;

            grdList.Columns[n].Name = "UnitClssName";
            grdList.Columns[n].HeaderText = "단위";
            grdList.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdList.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            grdList.Columns[n].ReadOnly = true;
            grdList.Columns[n++].Visible = true;

            grdList.Columns[n].Name = "LotCount";
            grdList.Columns[n].HeaderText = "B갯수";
            grdList.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdList.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            grdList.Columns[n].ReadOnly = true;
            grdList.Columns[n++].Visible = true;

            grdList.Font = new Font("맑은 고딕", 10, FontStyle.Bold);
            grdList.RowTemplate.Height = 30;
            grdList.ColumnHeadersHeight = 45;
            grdList.ScrollBars = ScrollBars.Both;
            grdList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grdList.ReadOnly = true;
            grdList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grdList.MultiSelect = false;

            foreach (DataGridViewColumn col in grdList.Columns)
            {
                col.DataPropertyName = col.Name;
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            }
            return;
        }

        #endregion
        #region Default Grid2 Setting

        private void InitGrid2()
        {
            grdData.Columns.Clear();
            grdData.ColumnCount = 11;

            int n = 0;
            // Set the Colums Hearder Names

            grdData.Columns[n].Name = "Seq";
            grdData.Columns[n].HeaderText = "";
            grdData.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            grdData.Columns[n].ReadOnly = true;
            grdData.Columns[n++].Visible = true;

            grdData.Columns[n].Name = "MoveJobID";
            grdData.Columns[n].HeaderText = "이동\r\n번호";
            grdData.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            grdData.Columns[n].ReadOnly = true;
            grdData.Columns[n++].Visible = true;

            grdData.Columns[n].Name = "Article";
            grdData.Columns[n].HeaderText = "품명";
            grdData.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[n].ReadOnly = true;
            grdData.Columns[n++].Visible = true;

            grdData.Columns[n].Name = "ArticleID";
            grdData.Columns[n].HeaderText = "ArticleID";
            grdData.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[n].ReadOnly = true;
            grdData.Columns[n++].Visible = false;

            grdData.Columns[n].Name = "BuyerArticleNo";
            grdData.Columns[n].HeaderText = "품번";
            grdData.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[n].ReadOnly = true;
            grdData.Columns[n++].Visible = false;

            grdData.Columns[n].Name = "OutwareID";
            grdData.Columns[n].HeaderText = "OutwareID";
            grdData.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            grdData.Columns[n].ReadOnly = true;
            grdData.Columns[n++].Visible = false;

            grdData.Columns[n].Name = "OutDate";
            grdData.Columns[n].HeaderText = "이동일자";
            grdData.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[n].ReadOnly = true;
            grdData.Columns[n++].Visible = true;

            grdData.Columns[n].Name = "StuffLotID";
            grdData.Columns[n].HeaderText = "A_LotID";
            grdData.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            grdData.Columns[n].ReadOnly = true;
            grdData.Columns[n++].Visible = true;

            grdData.Columns[n].Name = "OutLotID";
            grdData.Columns[n].HeaderText = "B_LotID";
            grdData.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            grdData.Columns[n].ReadOnly = true;
            grdData.Columns[n++].Visible = true;

            grdData.Columns[n].Name = "OutQty";
            grdData.Columns[n].HeaderText = "이동잔량";
            grdData.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grdData.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            grdData.Columns[n].ReadOnly = true;
            grdData.Columns[n++].Visible = true;

            grdData.Columns[n].Name = "UnitClssName";
            grdData.Columns[n].HeaderText = "단위";
            grdData.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            grdData.Columns[n].ReadOnly = true;
            grdData.Columns[n++].Visible = true;

            grdData.Font = new Font("맑은 고딕", 10,FontStyle.Bold);
            grdData.RowTemplate.Height = 30;
            grdData.ColumnHeadersHeight = 45;
            grdData.ScrollBars = ScrollBars.Both;
            grdData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grdData.ReadOnly = true;
            grdData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grdData.MultiSelect = false;

            foreach (DataGridViewColumn col in grdData.Columns)
            {
                col.DataPropertyName = col.Name;
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            }
            return;
        }

        #endregion

        private void btnLookup_Click(object sender, EventArgs e)
        {
            LogData.LogSave(this.GetType().Name, "R"); //2022-06-22 조회
            btnLookup.Enabled = false;
            Lib.Delay(3000); //2021-11-10 버튼을 여러번 클릭해도 한번만 클릭되게 딜레이 추가
            ProcQuery();
            btnLookup.Enabled = true;          
        }

        private void ProcQuery()
        {
            grdList.Rows.Clear();
            grdData.Rows.Clear();
            double douMJID = 0;
            try
            {
                if (txtArticle.Tag is null)
                {
                    txtArticle.Tag = "";
                }
                double.TryParse(txtMoveJobID.Text.Trim(), out douMJID);
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                if (chkInsDate.Checked)
                {
                    sqlParameter.Add("@chkDate", "1");    
                    sqlParameter.Add("@SDate", mtb_From.Text.Replace("-", ""));
                    sqlParameter.Add("@EDate", mtb_To.Text.Replace("-", ""));
                }
                else
                {
                    sqlParameter.Add("@chkDate", "0");
                    sqlParameter.Add("@SDate", mtb_From.Text.Replace("-", ""));
                    sqlParameter.Add("@EDate", mtb_To.Text.Replace("-", ""));
                }
                
                if (chkMoveJobID.Checked)
                {
                    sqlParameter.Add("@ChkMoveJobID", "1");
                    sqlParameter.Add("@MoveJobID", douMJID);
                }
                else
                {
                    sqlParameter.Add("@ChkMoveJobID", "0");
                    sqlParameter.Add("@MoveJobID", 0);
                }
                if (chkLotID.Checked)
                {
                    sqlParameter.Add("@ChkLotID", "1");
                    sqlParameter.Add("@LotID", txtLotID.Text.Trim());
                }
                else
                {
                    sqlParameter.Add("@ChkLotID", "0");
                    sqlParameter.Add("@LotID", txtLotID.Text.Trim());
                }
                if (chkBuyerArticleNo.Checked)
                {
                    sqlParameter.Add("@ChkBuyerArticleNo", "1");
                    sqlParameter.Add("@BuyerArticleNo", txtBuyerArticleNo.Text.Trim());
                }
                else
                {
                    sqlParameter.Add("@ChkBuyerArticleNo", "0");
                    sqlParameter.Add("@BuyerArticleNo", txtBuyerArticleNo.Text.Trim());
                }
                if (chkArticle.Checked)
                {
                    if (txtArticle.Tag.ToString() != "")
                    {
                        sqlParameter.Add("@chkArticle", "0");
                        sqlParameter.Add("@Article", txtArticle.Text.ToString().Trim());
                        sqlParameter.Add("@chkArticleID", "1");
                        sqlParameter.Add("@ArticleID", txtArticle.Tag.ToString().Trim());
                    }
                    else
                    {
                        sqlParameter.Add("@chkArticle", "1");
                        sqlParameter.Add("@Article", txtArticle.Text.ToString().Trim());
                        sqlParameter.Add("@chkArticleID", "0");
                        sqlParameter.Add("@ArticleID", txtArticle.Tag.ToString().Trim());
                    }
                }
                else
                {
                    sqlParameter.Add("@chkArticle", "0");
                    sqlParameter.Add("@Article", txtArticle.Text.ToString().Trim());
                    sqlParameter.Add("@chkArticleID", "0");
                    sqlParameter.Add("@ArticleID", txtArticle.Tag.ToString().Trim());
                }

                dt = DataStore.Instance.ProcedureToDataTable("xp_WizWork_sRemainMove", sqlParameter, false);
                tab1_dt = null;
                tab1_dt = dt.Clone();
                tab1_dt = dt.Copy();
                tab2_dt = null;
                tab2_dt = dt.Clone();
                tab2_dt = dt.Copy();
                if (dt.Rows.Count > 0)
                {
                    tab1_dt = tab1_dt.AsEnumerable()
               .GroupBy(r => new {
                   MoveID = r["MoveID"],
                   MoveJobID = r["MoveJobID"],
                   Article = r["Article"],
                   ArticleID = r["ArticleID"],
                   BuyerArticleNo = r["BuyerArticleNo"],
                   StuffINID = r["StuffINID"],
                   StuffLotID = r["StuffLotID"],
                   StuffDate = r["StuffDate"],
                   StuffClss = r["StuffClss"],
                   MoveQty = r["MoveQty"],
                   SumQty = r["SumQty"],
                   BLotCount = r["BLotCount"]
               })
               .Select(g => g.OrderBy(r => r["MoveID"]).First())
               .CopyToDataTable();

                    int i = 0; //idx용 변수
                    double dMQty = 0;
                    double dSQty = 0;
                    //double dOQty = 0;
                    foreach (DataRow dr in tab1_dt.Rows)
                    {
                        i++;
                        double.TryParse(dr["MoveQty"].ToString(), out dMQty);
                        double.TryParse(dr["SumQty"].ToString(), out dSQty);
                        grdList.Rows.Add(
                                        i.ToString(),
                                        dr["MoveID"].ToString(),
                                        dr["MoveJobID"].ToString(),
                                        Frm_tprc_Main.Lib.MakeDateTime("yyyymmdd", dr["StuffDate"].ToString()),
                                        dr["StuffinID"].ToString(),
                                        dr["StuffClss"].ToString(),
                                        dr["Article"].ToString(),
                                        dr["ArticleID"].ToString().Trim(),
                                        dr["BuyerArticleNo"].ToString(),
                                        dr["StuffLotID"].ToString(),
                                        string.Format("{0:n5}", dMQty),
                                        string.Format("{0:n5}", dSQty),
                                        dr["UnitClssName"].ToString(),
                                        dr["BLotCount"].ToString()
                                        );
                    }
                    //foreach (DataRow dr in tab2_dt.Rows)
                    //{
                    //    double.TryParse(dr["OutQty"].ToString(), out dOQty);
                    //    i++;
                    //    grdData.Rows.Add(
                    //                    dr["Seq"].ToString(),
                    //                    dr["MoveJobID"].ToString(),
                    //                    dr["Sabun"].ToString(),
                    //                    dr["Article"].ToString(),
                    //                    dr["ArticleID"].ToString(),
                    //                    dr["OutwareID"].ToString(),
                    //                    Frm_tprc_Main.Lib.MakeDateTime("yyyymmdd", dr["OutDate"].ToString()),
                    //                    dr["StuffLotID"].ToString(),
                    //                    dr["OutLotID"].ToString(),
                    //                    string.Format("{0:n0}", dOQty),
                    //                    dr["UnitClssName"].ToString()
                    //                    );
                    //}
                    if (grdList.RowCount > 0)
                    {
                        grdList[0, 0].Selected = true;
                    }
                    Frm_tprc_Main.gv.queryCount = string.Format("{0:n0}", grdList.RowCount);
                    Frm_tprc_Main.gv.SetStbInfo();
                }
            }
            catch (Exception excpt)
            {
                Message[0] = "[오류]";
                Message[1] = string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message);
                WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
            }
        }
        
        public void procDelete()
        {
            if (WizCommon.Popup.MyMessageBox.ShowBox("선택항목에 대해서 삭제처리하시겠습니까?", "[삭제]", 0, 0) == DialogResult.OK)
            {
                try
                {
                    //LotID 체크 상위품이 없는지 Check
                    Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                    sqlParameter.Add("@chkDate", "0");
                    sqlParameter.Add("@SDate", mtb_From.Text.Replace("-", ""));
                    sqlParameter.Add("@EDate", mtb_To.Text.Replace("-", ""));
                    sqlParameter.Add("@ChkMoveJobID", "0");
                    sqlParameter.Add("@MoveJobID", 0);
                    sqlParameter.Add("@ChkLotID", "1");
                    sqlParameter.Add("@LotID", grdList.SelectedRows[0].Cells["LotID"].Value.ToString());
                    del_dt = DataStore.Instance.ProcedureToDataTable("xp_WizWork_sRemainMove", sqlParameter, false);

                    if (del_dt.Rows.Count > 0)
                    {
                        if (grdList.SelectedRows[0].Cells["LotID"].Value.ToString() == del_dt.Rows[0]["OutLotID"].ToString())
                        {
                            Message[0] = "[오류]";
                            Message[1] = del_dt.Rows[0]["OutLotID"].ToString() + " LotID는 \r\n" + del_dt.Rows[0]["StuffLotID"].ToString() + "에 잔량이동처리 되었습니다.\r\n" +
                                "삭제하시려면 잔량이동번호 " + del_dt.Rows[0]["MoveJobID"].ToString() + "을 먼저 삭제해야합니다.";
                            chkInsDate.Checked = false;
                            chkLotID.Checked = false;
                            chkBuyerArticleNo.Checked = false;
                            chkArticle.Checked = false;
                            chkMoveJobID.Checked = true;
                            txtMoveJobID.Text = del_dt.Rows[0]["MoveJobID"].ToString();
                            WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                            return;
                        }
                    }

                    // 2020.09.04 해당 입고된 라벨의 잔량이 없을경우!!!!!!
                    // 라벨 취소를 하면 없는 잔량에서 출고시켜야 되는데, 이건 말이 안됨.
                    // → 잔량이동처리 한 라벨을 사용했을 경우
                    //     , 잔량이동처리 후 사용된 라벨 건은 삭제가 불가능 합니다. 라는 메시지를 띄워야 함.
                    if (CheckUsedThisLabel(grdList.SelectedRows[0].Cells["MoveJobID"].Value.ToString()))
                    {
                        //Check 결과 상위품이 없을 경우 삭제 프로시저 실행
                        sqlParameter = new Dictionary<string, object>();
                        sqlParameter.Add("@MoveJobID", grdList.SelectedRows[0].Cells["MoveJobID"].Value.ToString());
                        DataStore.Instance.ProcedureToDataSet("[xp_WizWork_dRemainMove]", sqlParameter, true);
                        ProcQuery();
                        LogData.LogSave(this.GetType().Name, "R"); //2022-06-22 조회
                    }
                }

                catch (Exception excpt)
                {
                    Message[0] = "[오류]";
                    Message[1] = string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message);
                    WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                }
            }
            else
            {
                return;
            }
        }

        #region 이동 수량 500, 해당 입고라벨의 잔량 0 개일 경우, 돌아갈 갯수가 없기 때문에 삭제가 안되도록 막아야 됨

        private bool CheckUsedThisLabel(string MoveID)
        {
            bool flag = false;

            try
            {
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();

                sqlParameter.Add("MoveJobID", ConvertDouble(MoveID));

                DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_prdWork_CheckUsedThisLabel", sqlParameter, false);

                if (dt != null
                    && dt.Rows.Count > 0
                    && dt.Columns.Count == 1)
                {
                    string Msg = dt.Rows[0]["Result"].ToString().Trim();

                    if (Msg.ToUpper().Equals("PASS"))
                    {
                        flag = true;
                    }
                    else
                    {
                        WizCommon.Popup.MyMessageBox.ShowBox(dt.Rows[0]["Result"].ToString().Trim().Replace("|", "\r\n"), "[삭제 실패]", 0, 1);
                        flag = false;
                    }
                }

                return flag;
            }
            catch (Exception excpt)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message), "[오류]", 0, 1);
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

        /// <summary>
        /// 메인화면 버튼 클릭 - 폼 닫기
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMoveMain_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// 제품명 검색
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbItemName_ClickButton(object sender, EventArgs e)
        {
            //Frm_Qlt_SelectItem item = new Frm_Qlt_SelectItem();
            //item.ShowDialog();
        }

        /// <summary>
        /// 제품명 검색
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbItemName_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //Frm_Qlt_SelectItem item = new Frm_Qlt_SelectItem();
            //item.ShowDialog();
        }

        private void cmdRowUp_Click(object sender, EventArgs e)
        {
            if (tabControl.SelectedIndex == 0)
            {
                Lib.btnRowUp(grdList, x);
            }
            else if (tabControl.SelectedIndex == 1)
            {
                Lib.btnRowUp(grdData, z);
            }

        }

        private void cmdRowDown_Click(object sender, EventArgs e)
        {
            if (tabControl.SelectedIndex == 0)
            {
                Lib.btnRowDown(grdList, x);
            }
            else if (tabControl.SelectedIndex == 1)
            {
                Lib.btnRowDown(grdData, z);
            }
        }

       
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (grdList.RowCount == 0 || grdList.SelectedRows.Count == 0)
            {
                Message[0] = "[조회내용 없음]";
                Message[1] = "조회 및 선택 후 삭제 버튼을 눌러주십시오.";
                WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
            }
            else if (grdList.SelectedRows.Count > 0)
            {
                if (grdList.SelectedRows[0].Cells["StuffDate"].Value.ToString().Replace("-", "") == DateTime.Now.ToString("yyyyMMdd"))
                {
                    procDelete();
                    LogData.LogSave(this.GetType().Name, "D"); //2022-06-22 삭제

                }
                else
                {
                    WizCommon.Popup.MyMessageBox.ShowBox("현재 날짜와 동일한 이동일자만 삭제할 수 있습니다.", "[삭제 불가]", 0, 1);
                }
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedIndex == 0)
            {
                btnDelete.Visible = true;
            }
            else if(tabControl.SelectedIndex == 1)
            {
                btnDelete.Visible = false;
                if (grdList.RowCount > 0)
                {
                    if (grdList.SelectedRows.Count == 1)
                    {
                        double dOQty = 0;
                        int i = 0;
                        string Where = string.Empty;
                        Where = "MoveID = " + grdList.SelectedRows[0].Cells["MoveID"].Value.ToString();
                        tab2_dt = dt.Select(Where).CopyToDataTable();
                        grdData.Rows.Clear();
                        if (tab2_dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr in tab2_dt.Rows)
                            {
                                double.TryParse(dr["OutQty"].ToString(), out dOQty);
                                i++;
                                grdData.Rows.Add(
                                                dr["Seq"].ToString(),
                                                dr["MoveJobID"].ToString(),
                                                dr["Article"].ToString(),
                                                dr["ArticleID"].ToString().Trim(),
                                                dr["BuyerArticleNo"].ToString(),
                                                dr["OutwareID"].ToString(),
                                                Frm_tprc_Main.Lib.MakeDateTime("yyyymmdd", dr["OutDate"].ToString()),
                                                dr["StuffLotID"].ToString(),
                                                dr["OutLotID"].ToString(),
                                                string.Format("{0:n5}", dOQty),
                                                dr["UnitClssName"].ToString()
                                                );
                            }
                            Frm_tprc_Main.gv.queryCount = string.Format("{0:n0}", grdData.RowCount);
                            Frm_tprc_Main.gv.SetStbInfo();
                        }
                    }
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            LogData.LogSave(this.GetType().Name, "S"); //2022-06-22 사용시간(로드, 닫기)
            Close();
        }

        private void chkMoveJobID_Click(object sender, EventArgs e)
        {
            if (chkMoveJobID.Checked)
            {
                txtMoveJobID.Text = "";
                WizCommon.Popup.Frm_CMNumericKeypad keypad = new WizCommon.Popup.Frm_CMNumericKeypad(txtMoveJobID.Text.Trim(), "잔량이동번호");
                keypad.Owner = this;
                if (keypad.ShowDialog() == DialogResult.OK)
                {
                    txtMoveJobID.Text = keypad.tbInputText.Text;
                }
            }
            else
            {
                txtMoveJobID.Text = "";
            }
        }

        private void chkLotID_Click(object sender, EventArgs e)
        {
            if (chkLotID.Checked)
            {
                txtLotID.Text = "";
                POPUP.Frm_CMKeypad keypad = new POPUP.Frm_CMKeypad("LOTID입력", "LOTID");

                keypad.Owner = this;
                if (keypad.ShowDialog() == DialogResult.OK)
                {
                    txtLotID.Text = keypad.tbInputText.Text;
                }
            }
            else
            {
                txtLotID.Text = "";
            }
        }

        private void chkArticle_Click(object sender, EventArgs e)
        {
            if (chkArticle.Checked)
            {
                Frm_PopUpSel_sArticle_O fps = new Frm_PopUpSel_sArticle_O(txtArticle.Text);
                fps.WriteTextEvent += new Frm_PopUpSel_sArticle_O.TextEventHandler(GetData);
                fps.Owner = this;
                fps.Show();

                void GetData(string sArticleID, string sArticle, string sBuyerArticleNo)
                {
                    txtArticle.Text = sArticle;
                    txtArticle.Tag = sArticleID;
                    txtBuyerArticleNo.Text = sBuyerArticleNo;
                }
            }
            else
            {
                txtArticle.Text = "";
            }
        }

        private void txtArticle_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtArticle.Tag = "";
        }

        private void chkBuyerArticleNo_Click(object sender, EventArgs e)
        {
            if (chkBuyerArticleNo.Checked)
            {
                Frm_PopUpSel_sArticle_O fps = new Frm_PopUpSel_sArticle_O();
                fps.WriteTextEvent += new Frm_PopUpSel_sArticle_O.TextEventHandler(GetData);
                fps.Owner = this;
                fps.Show();

                void GetData(string sArticleID, string sArticle, string sBuyerArticleNo)
                {
                    txtArticle.Text = sArticle;
                    txtArticle.Tag = sArticleID;
                    txtBuyerArticleNo.Text = sBuyerArticleNo;
                }
            }
            else
            {
                txtBuyerArticleNo.Text = "";
            }
        }

        private void btnColLeft_Click(object sender, EventArgs e)
        {
            switch (tabControl.SelectedIndex)
            {
                case 0:
                    x = Frm_tprc_Main.Lib.btnColLeft(grdList, x);
                    break;
                case 1:
                    z = Frm_tprc_Main.Lib.btnColLeft(grdData, z);
                    break;
            }
        }

        private void btnColRight_Click(object sender, EventArgs e)
        {
            switch (tabControl.SelectedIndex)
            {
                case 0:
                    x = Frm_tprc_Main.Lib.btnColRight(grdList, x);
                    break;
                case 1:
                    z = Frm_tprc_Main.Lib.btnColRight(grdData, z);
                    break;
            }
        }

        private void frm_mtr_RemainMove_Q_Activated(object sender, EventArgs e)
        {
            ((Frm_tprc_Main)(MdiParent)).LoadRegistry();
        }
    }
}
