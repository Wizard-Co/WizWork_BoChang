using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WizWork.Properties;
using Microsoft.Win32;
using Microsoft.VisualBasic;
using WizCommon;
namespace WizWork
{
    public partial class frm_tprc_MoldInspect_U : Form
    {
        POPUP.Frm_CMKeypad keypad = new POPUP.Frm_CMKeypad();
        string g_ProcessID = string.Empty;
        string g_MachineList = string.Empty;
        INI_GS gs = new INI_GS();
        List<string> DeleteRowInspectID = new List<string>();
        DataGridView UpdateList = new DataGridView();

        WizWorkLib Lib = new WizWorkLib();
        LogData LogData = new LogData(); //2022-06-21 log 남기는 함수
        string ItemCode = string.Empty;

        string CheckList = string.Empty;
        string No = string.Empty;
        string InsContents = string.Empty;
        string McInsCheck = string.Empty;
        string Path = string.Empty;
        string File = string.Empty;
        string ArticleID = string.Empty;
        string MoldID = string.Empty;

        string MoldInspectBasisID = string.Empty;
        string MoldRInspectDate = string.Empty;

        bool blLoad = false;
        Frm_PopUp_ImgIns2 II2 = null;
        Frm_PopUp_ImgIns II = null;


        /// <summary>
        /// 생성
        /// </summary>
        public frm_tprc_MoldInspect_U()
        {
            InitializeComponent();
        }

        #region TableLayoutPanel 하위 컨트롤들의 DockStyle.Fill 세팅
        private void SetScreen()
        {
            pnlForm.Dock = DockStyle.Fill;
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
                                    foreach (Control c in co.Controls)
                                    {
                                        c.Dock = DockStyle.Fill;
                                        c.Margin = new Padding(0, 0, 0, 0);
                                        foreach (Control ctrl in c.Controls)
                                        {
                                            ctrl.Dock = DockStyle.Fill;
                                            ctrl.Margin = new Padding(0, 0, 0, 0);
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

        private void frm_tprc_DailyMoldCheck_Load(object sender, EventArgs e)
        {
            LogData.LogSave(this.GetType().Name, "S"); //2022-06-22 사용시간(로드, 닫기)
            SetScreen();
            InitGrid();
            InitGrid2();
            ClearData();
            blLoad = true;

        }

        #region Default Grid Setting

        private void InitGrid()
        {
            GridData1.Columns.Clear();
            GridData1.ColumnCount = 14;
            int n = 0;
            // Set the Colums Hearder Names
            GridData1.Columns[n].Name = "No";
            GridData1.Columns[n].HeaderText = "No";
            GridData1.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            GridData1.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            GridData1.Columns[n++].Visible = true;

            GridData1.Columns[n].Name = "CheckList";
            GridData1.Columns[n].HeaderText = "점검항목";
            GridData1.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            GridData1.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            GridData1.Columns[n++].Visible = true;

            GridData1.Columns[n].Name = "InsContents";
            GridData1.Columns[n].HeaderText = "점검내용";
            GridData1.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            GridData1.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            GridData1.Columns[n++].Visible = true;

            GridData1.Columns[n].Name = "MoldInsCheck";//MoldNo
            GridData1.Columns[n].HeaderText = "확인";
            GridData1.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            GridData1.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            GridData1.Columns[n].MinimumWidth = 110;
            GridData1.Columns[n++].Visible = true;

            GridData1.Columns[n].Name = "CycleGbnName";
            GridData1.Columns[n].HeaderText = "주기";
            GridData1.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            GridData1.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            GridData1.Columns[n].MinimumWidth = 110;
            GridData1.Columns[n++].Visible = true;

            GridData1.Columns[n].Name = "InspectionLegendName";
            GridData1.Columns[n].HeaderText = "검사";
            GridData1.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            GridData1.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            GridData1.Columns[n].MinimumWidth = 110;
            GridData1.Columns[n++].Visible = true;

            GridData1.Columns[n].Name = "MoldInspectBasisID";
            GridData1.Columns[n].HeaderText = "MoldInspectBasisID";
            GridData1.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            GridData1.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            GridData1.Columns[n++].Visible = false;

            GridData1.Columns[n].Name = "MoldInspectSeq";
            GridData1.Columns[n].HeaderText = "MoldInspectSeq";
            GridData1.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            GridData1.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            GridData1.Columns[n++].Visible = false;

            GridData1.Columns[n].Name = "MoldInspectCycleGbn";
            GridData1.Columns[n].HeaderText = "MoldInspectCycleGbn";
            GridData1.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            GridData1.Columns[n++].Visible = false;

            GridData1.Columns[n].Name = "MoldInspectRecordGbn";
            GridData1.Columns[n].HeaderText = "MoldInspectRecordGbn";
            GridData1.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            GridData1.Columns[n++].Visible = false;

            GridData1.Columns[n].Name = "MoldInspectImagePath";
            GridData1.Columns[n].HeaderText = "MoldInspectImagePath";
            GridData1.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            GridData1.Columns[n++].Visible = false;

            GridData1.Columns[n].Name = "MoldInspectImageFile";
            GridData1.Columns[n].HeaderText = "MoldInspectImageFile";
            GridData1.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            GridData1.Columns[n++].Visible = false;

            GridData1.Columns[n].Name = "MoldInsSeq";
            GridData1.Columns[n].HeaderText = "MoldInsSeq";
            GridData1.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            GridData1.Columns[n++].Visible = false;

            GridData1.Columns[n].Name = "MldInspectLegend";
            GridData1.Columns[n].HeaderText = "MldInspectLegend";
            GridData1.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            GridData1.Columns[n++].Visible = false;

            GridData1.Font = new Font("맑은 고딕", 12, FontStyle.Bold);
            GridData1.RowTemplate.Height = 30;
            GridData1.ColumnHeadersHeight = 45;
            GridData1.ScrollBars = ScrollBars.Both;
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

        #endregion

        #region Default Grid2 Setting

        private void InitGrid2()
        {
            GridData2.Columns.Clear();
            GridData2.ColumnCount = 12;

            int n = 0;
            // Set the Colums Hearder Names
            GridData2.Columns[n].Name = "No";
            GridData2.Columns[n].HeaderText = "No";
            GridData2.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            GridData2.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            GridData2.Columns[n++].Visible = true;

            GridData2.Columns[n].Name = "CheckList";
            GridData2.Columns[n].HeaderText = "점검항목";
            GridData2.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            GridData2.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            GridData2.Columns[n++].Visible = true;

            GridData2.Columns[n].Name = "InsContents";
            GridData2.Columns[n].HeaderText = "점검내용";
            GridData2.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            GridData2.Columns[n].ReadOnly = true;
            GridData2.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            GridData2.Columns[n++].Visible = true;

            GridData2.Columns[n].Name = "MoldInsCheck";
            GridData2.Columns[n].HeaderText = "확인";
            GridData2.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            GridData2.Columns[n].ReadOnly = true;
            GridData2.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            GridData1.Columns[n].MinimumWidth = 110;
            GridData2.Columns[n++].Visible = true;

            GridData2.Columns[n].Name = "CycleGbnName";
            GridData2.Columns[n].HeaderText = "주기";
            GridData2.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            GridData2.Columns[n].ReadOnly = true;
            GridData2.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            GridData1.Columns[n].MinimumWidth = 110;
            GridData2.Columns[n++].Visible = true;

            GridData2.Columns[n].Name = "InspectionFigure";
            GridData2.Columns[n].HeaderText = "검사";
            GridData2.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            GridData2.Columns[n].ReadOnly = true;
            GridData2.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            GridData1.Columns[n].MinimumWidth = 110;
            GridData2.Columns[n++].Visible = true;

            GridData2.Columns[n].Name = "MoldInspectBasisID";
            GridData2.Columns[n].HeaderText = "MoldInspectBasisID";
            GridData2.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            GridData2.Columns[n].ReadOnly = true;
            GridData2.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            GridData2.Columns[n++].Visible = false;

            GridData2.Columns[n].Name = "MoldInspectSeq";
            GridData2.Columns[n].HeaderText = "MoldInspectSeq";
            GridData2.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            GridData2.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            GridData2.Columns[n++].Visible = false;

            GridData2.Columns[n].Name = "MoldInspectCycleGbn";
            GridData2.Columns[n].HeaderText = "MoldInspectCycleGbn";
            GridData2.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            GridData2.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            GridData2.Columns[n++].Visible = false;

            GridData2.Columns[n].Name = "MoldInspectRecordGbn";
            GridData2.Columns[n].HeaderText = "MoldInspectRecordGbn";
            GridData2.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            GridData2.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            GridData2.Columns[n++].Visible = false;

            GridData2.Columns[n].Name = "MoldInspectImagePath";
            GridData2.Columns[n].HeaderText = "MoldInspectImagePath";
            GridData2.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            GridData2.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            GridData2.Columns[n++].Visible = false;

            GridData2.Columns[n].Name = "MoldInspectImageFile";
            GridData2.Columns[n].HeaderText = "MoldInspectImageFile";
            GridData2.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            GridData2.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            GridData2.Columns[n++].Visible = false;

            GridData2.Font = new Font("맑은 고딕", 12, FontStyle.Bold);
            GridData2.RowTemplate.Height = 30;
            GridData2.ColumnHeadersHeight = 45;
            GridData2.ScrollBars = ScrollBars.Both;
            GridData2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            GridData2.ReadOnly = true;
            foreach (DataGridViewColumn col in GridData2.Columns)
            {
                col.DataPropertyName = col.Name;
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            return;
        }

        #endregion

        #region 데이터 초기화

        private void ClearData()
        {
            GridData1.Rows.Clear();
            GridData2.Rows.Clear();

            //m_InsepctBasisID = "";
            GridCellClear();
            mtDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
            dtTime.CustomFormat = "HH:mm:ss";

        }

        private void GridCellClear()
        {
            LegendClear();
            FigureClear();
        }

        private void LegendClear()
        {
            for (int i = 0; i < GridData1.Rows.Count; i++)
            {
                GridData1.Rows[i].Cells["InspectionLegendName"].Value = "";
                GridData1.Rows[i].Cells["MldInspectLegend"].Value = "";
            }
        }
        private void FigureClear()
        {
            for (int i = 0; i < GridData2.Rows.Count; i++)
            {
                GridData2.Rows[i].Cells["InspectionFigure"].Value = "";
            }
        }

        #endregion

        #region 일자, 시간 이벤트

        private void btnDate_Click(object sender, EventArgs e)
        {
            LoadCalendar();
        }

        private void mtDate_Click(object sender, EventArgs e)
        {
            LoadCalendar();
        }

        private void LoadCalendar()
        {
            WizCommon.Popup.Frm_TLP_Calendar calendar = new WizCommon.Popup.Frm_TLP_Calendar(mtDate.Text.Replace("-", ""), mtDate.Name);
            calendar.WriteDateTextEvent += new WizCommon.Popup.Frm_TLP_Calendar.TextEventHandler(GetDate);
            calendar.Owner = this;
            calendar.ShowDialog();
            //Calendar.Value -> mtbBox.Text 달력창으로부터 텍스트로 값을 옮겨주는 메소드
            void GetDate(string strDate, string btnName)
            {
                DateTime dateTime = new DateTime();
                dateTime = DateTime.ParseExact(strDate, "yyyyMMdd", null);
                mtDate.Text = dateTime.ToString("yyyy-MM-dd");
            }
        }

        private void btnTime_Click(object sender, EventArgs e)
        {
            TimeCheck("점검시간");
        }

        // 점검시간 키패드 입력
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
            if (strTime == "점검시간") { dtTime.Value = dt; }
        }




        #endregion

        #region 품명, 품번, 금형 이벤트
        //품번
        private void btnBuyerArticleNo_Click(object sender, EventArgs e)
        {
            Frm_PopUp_sArticle FPA = new Frm_PopUp_sArticle();
            FPA.StartPosition = FormStartPosition.CenterScreen;
            if (FPA.ShowDialog() == DialogResult.OK)
            {
                txtArticle.Text = FPA.Article;
                txtBuyerArticleNo.Text = FPA.BuyerArticleNo;
                ArticleID = FPA.ArticleID;
            }
        }

        private void txtBuyerArticleNo_Click(object sender, EventArgs e)
        {
            Frm_PopUp_sArticle FPA = new Frm_PopUp_sArticle();
            FPA.StartPosition = FormStartPosition.CenterScreen;
            if (FPA.ShowDialog() == DialogResult.OK)
            {
                txtArticle.Text = FPA.Article;
                txtBuyerArticleNo.Text = FPA.BuyerArticleNo;
                ArticleID = FPA.ArticleID;
            }
        }

        //품명
        private void btnArticle_Click(object sender, EventArgs e)
        {
            Frm_PopUp_sArticle FPA = new Frm_PopUp_sArticle();
            FPA.StartPosition = FormStartPosition.CenterScreen;
            if (FPA.ShowDialog() == DialogResult.OK)
            {
                txtArticle.Text = FPA.Article;
                txtBuyerArticleNo.Text = FPA.BuyerArticleNo;
                ArticleID = FPA.ArticleID;
            }
        }

        private void txtArticle_Click(object sender, EventArgs e)
        {
            Frm_PopUp_sArticle FPA = new Frm_PopUp_sArticle();
            FPA.StartPosition = FormStartPosition.CenterScreen;
            if (FPA.ShowDialog() == DialogResult.OK)
            {
                txtArticle.Text = FPA.Article;
                txtBuyerArticleNo.Text = FPA.BuyerArticleNo;
                ArticleID = FPA.ArticleID;
            }
        }

        //금형
        private void btnMoldNo_Click(object sender, EventArgs e)
        {
            frm_tprc_Mold_ByArticleID Ftmba = new frm_tprc_Mold_ByArticleID(ArticleID);
            Ftmba.StartPosition = FormStartPosition.CenterScreen;
            if (Ftmba.ShowDialog() == DialogResult.OK)
            {
                txtMoldNo.Text = Ftmba.MoldNo;
                MoldID = Ftmba.MoldID;
                txtArticle.Text = Ftmba.Article;
                txtBuyerArticleNo.Text = Ftmba.BuyerArticleNo;
                ProcQuery();
            }
        }

        private void txtMoldNo_Click(object sender, EventArgs e)
        {
            frm_tprc_Mold_ByArticleID Ftmba = new frm_tprc_Mold_ByArticleID(ArticleID);
            Ftmba.StartPosition = FormStartPosition.CenterScreen;
            if (Ftmba.ShowDialog() == DialogResult.OK)
            {
                txtMoldNo.Text = Ftmba.MoldNo;
                MoldID = Ftmba.MoldID;
                txtArticle.Text = Ftmba.Article;
                txtBuyerArticleNo.Text = Ftmba.BuyerArticleNo;
                ProcQuery();
            }
        }


        #endregion


        #region 조회 함수

        private void ProcQuery()
        {
            GridData1.Rows.Clear();
            GridData2.Rows.Clear();
            try
            {
                if (txtMoldNo.Text != "" && MoldID != "")
                {
                    Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                    sqlParameter.Add("MoldID", MoldID);

                    DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_WizWork_sMoldRegularInspectBasisSubByMold", sqlParameter, false);

                    if (dt is null)
                    {
                        return;
                    }

                    if (dt.Rows.Count > 0)
                    {
                        int j = 0; int k = 0;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            DataRow dr = dt.Rows[i];

                            MoldInspectBasisID = dr["MoldInspectBasisID"].ToString();
                            MoldRInspectDate = dr["MoldInspectBasisDate"].ToString();


                            if (dr["MoldInspectRecordGbn"].ToString() == "01")
                            {
                                k = k + 1;
                                GridData1.Rows.Add(
                                    k,                                          //NO
                                    dr["MoldInspectItemName"],                  //점검항목
                                    dr["MoldInspectContent"],                   //점검내용
                                    dr["MoldInspectCheck"],                     //확인
                                    dr["MoldInspectCycle"],                     //주기
                                    "",                                         //검사
                                    dr["MoldInspectBasisID"],                   //mcseq
                                    dr["MoldInspectSeq"],                       //McSeq
                                    dr["MoldInspectCycleGbn"],                  //mcinsrecordgbn
                                    dr["MoldInspectRecordGbn"],
                                    "/ImageData/MoldBasis/" + dr["MoldInspectBasisID"],    //FTP이미지경로
                                    dr["MoldInspectImageFile"]                             //FTP이미지파일명
                                    );
                            }
                            else if (dr["MoldInspectRecordGbn"].ToString() == "02")
                            {
                                j = j + 1;
                                GridData2.Rows.Add(
                                    j,                             //NO
                                    dr["MoldInspectItemName"],     //점검항목
                                    dr["MoldInspectContent"],      //점검내용
                                    dr["MoldInspectCheck"],        //확인
                                    dr["MoldInspectCycle"],        //주기
                                    "",                            //검사
                                    dr["MoldInspectBasisID"],      //mcseq
                                    dr["MoldInspectSeq"],          //McSeq
                                    dr["MoldInspectCycleGbn"],     //mcinsrecordgbn
                                    dr["MoldInspectRecordGbn"],
                                    "/ImageData/MoldBasis/" + dr["MoldInspectBasisID"],    //FTP이미지경로
                                    dr["MoldInspectImageFile"]               //FTP이미지파일명
                                );
                            }
                            //foreach (DataGridViewRow dgvr in GridData1.Rows)
                            //{
                            //    dgvr.Visible = false;
                            //}
                            //foreach (DataGridViewRow dgvr in GridData2.Rows)
                            //{
                            //    dgvr.Visible = false;
                            //}
                        }
                        //GridData1.ClearSelection();
                        //GridData2.ClearSelection();
                        //FillGridByMcInsCycleGbn();
                        DataStore.Instance.CloseConnection(); //2021-10-07 DB 커넥트 연결 해제
                    }
                }
            }
            catch (Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", ex.Message), "[오류]", 0, 1);
            }
        }

        #endregion

        #region 작업자, 초기화, 저장, 닫기 이벤트

        private void btnPerson_Click(object sender, EventArgs e)
        {
            string Send_ProcessID = string.Empty;
            string Send_MachineID = string.Empty;

            Send_ProcessID = Frm_tprc_Main.g_tBase.ProcessID;
            Send_MachineID = Frm_tprc_Main.g_tBase.MachineID;

            frm_tprc_setProcess FTSP = new frm_tprc_setProcess(Send_ProcessID, Send_MachineID, true);
            FTSP.Owner = this.ParentForm;
            if (FTSP.ShowDialog() == DialogResult.OK)
            {
            };
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            LegendClear();
            FigureClear();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (GridData1.RowCount + GridData2.RowCount == 0)
            {
                WizCommon.Popup.MyMessageBox.ShowBox("[금형]을 선택하시고, 입력버튼 클릭 후 값을 입력하여 주십시오.", "[확인]", 3, 1);
                return;
            }
            else
            {
                if (GridData1.Rows.Count > 0)
                {

                    for (int i = 0; i < GridData1.Rows.Count; i++)
                    {
                        if (GridData1.Rows[i].Cells["MldInspectLegend"].Value.ToString() == string.Empty)
                        {
                            WizCommon.Popup.MyMessageBox.ShowBox("검사자료-범례 '입력'을 클릭하시고, 값을 입력해주십시오.", "[입력 확인]", 3, 1);
                            return;
                        }
                    }
                }
                if (GridData2.Rows.Count > 0)
                {
                    for (int i = 0; i < GridData2.Rows.Count; i++)
                    {
                        if (GridData2.Rows[i].Cells["InspectionFigure"].Value.ToString() == string.Empty)
                        {
                            WizCommon.Popup.MyMessageBox.ShowBox("검사자료-수치 '입력'을 클릭하시고, 값을 입력해주십시오.", "[입력 확인]", 3, 1);
                            return;
                        }
                    }
                }

                ProcSave();
                LogData.LogSave(this.GetType().Name, "C"); //2022-06-22 저장

            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            LogData.LogSave(this.GetType().Name, "S"); //2022-06-22 사용시간(로드, 닫기)
            Close();
        }

        #endregion


        #region 저장 함수

        private void ProcSave()
        {
            //xp_McRegularInspect_iMcRegularInspect    
            //첫번째 프로시저 데이터값 셋팅
            //List<string> ProcedureInfo = new List<string>();
            //List<List<string>> ListProcedureName = new List<List<string>>();
            //List<Dictionary<string, object>> ListParameter = new List<Dictionary<string, object>>();
            try
            {
                List<WizCommon.Procedure> Prolist = new List<WizCommon.Procedure>();
                List<Dictionary<string, object>> ListParameter = new List<Dictionary<string, object>>();

                string strMcrInstpectID = "";
                string strMcrInstpectUserID = "";
                string strMcrInspectDate = "";
                string strMcrInspectTime = "";
                string strMcInstpectBasisID = "";
                string strMcInstpectBasisDate = "";
                string strComments = "";
                string strCreateUserID = "";

                string strMcInspectBasisSeq = "";
                string strMcrInspectLegend = "";
                string strMcrInspectValue = "";

                strMcrInstpectID = "";
                strMcrInstpectUserID = Frm_tprc_Main.g_tBase.PersonID;

                strMcrInspectDate = mtDate.Text.Replace("-", "").Replace("/", "");
                strMcrInspectTime = dtTime.Value.ToString("HHmmss");
                strMcInstpectBasisID = MoldInspectBasisID;
                strMcInstpectBasisDate = MoldRInspectDate;

                strComments = "";
                strCreateUserID = Frm_tprc_Main.g_tBase.PersonID;

                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Add("MoldRInspectID", "");                                 //검사ID
                sqlParameter.Add("MoldRInspectUserID", strMcrInstpectUserID);           //검사자ID //메인에 저장되있는 전역변수 UserID //만들어야됨
                sqlParameter.Add("MoldRInspectDate", strMcrInspectDate);                //검사일자
                sqlParameter.Add("MoldRInspectTime", strMcrInspectTime);                //검사시간
                sqlParameter.Add("MoldInspectBasisID", strMcInstpectBasisID);           //검사기준ID
                sqlParameter.Add("MoldRInspectBasisDate", strMcInstpectBasisDate);      //개정일자
                sqlParameter.Add("Comments", strComments);                              //비고
                sqlParameter.Add("CreateUserID", strCreateUserID);                      //생성자   //메인에 저장되있는 전역변수 UserID // 만들어야함

                WizCommon.Procedure pro1 = new WizCommon.Procedure();
                pro1.list_OutputName = new List<string>();
                pro1.list_OutputLength = new List<string>();

                pro1.Name = "xp_WizWork_dvlMoldIns_iRegularInspect";
                pro1.OutputUseYN = "Y";
                pro1.list_OutputName.Add("MoldRInspectID");
                pro1.list_OutputLength.Add("20");

                Prolist.Add(pro1);
                ListParameter.Add(sqlParameter);

                if (GridData1.Rows.Count > 0)
                {
                    for (int i = 0; i < GridData1.Rows.Count; i++)
                    {
                        Dictionary<string, object> sqlParameter2 = new Dictionary<string, object>();

                        strMcInspectBasisSeq = GridData1.Rows[i].Cells["MoldInspectSeq"].Value.ToString();
                        strMcrInspectLegend = GridData1.Rows[i].Cells["MldInspectLegend"].Value.ToString();
                        strMcrInspectValue = "0";

                        sqlParameter2.Add("MoldRInspectID", strMcrInstpectID);            //iWkResult 프로시저로 만들어진 JoibID
                        sqlParameter2.Add("MoldInspectBasisID", strMcInstpectBasisID);    //
                        sqlParameter2.Add("MoldInspectSeq", strMcInspectBasisSeq);        //
                        sqlParameter2.Add("MoldInsSeq", strMcInspectBasisSeq);
                        sqlParameter2.Add("MldRInspectLegend", strMcrInspectLegend);         //
                        sqlParameter2.Add("MldRValue", strMcrInspectValue);               //

                        sqlParameter2.Add("CreateUserID", strCreateUserID);             //전역변수, 메인폼에 있는 전역변수값 가져오기

                        WizCommon.Procedure pro2 = new WizCommon.Procedure();
                        pro2.list_OutputName = new List<string>();
                        pro2.list_OutputLength = new List<string>();

                        pro2.Name = "xp_WizWork_dvlMoldIns_iRegularInspectSub";
                        pro2.OutputUseYN = "N";
                        pro2.list_OutputName.Add("MoldRInspectID");
                        pro2.list_OutputLength.Add("20");

                        Prolist.Add(pro2);
                        ListParameter.Add(sqlParameter2);

                    }
                }
                if (GridData2.Rows.Count > 0)
                {
                    for (int i = 0; i < GridData2.Rows.Count; i++)
                    {
                        Dictionary<string, object> sqlParameter3 = new Dictionary<string, object>();

                        strMcInspectBasisSeq = GridData2.Rows[i].Cells["MoldInspectSeq"].Value.ToString();
                        strMcrInspectLegend = "";
                        strMcrInspectValue = GridData2.Rows[i].Cells["InspectionFigure"].Value.ToString();

                        sqlParameter3.Add("MoldRInspectID", strMcrInstpectID);//iWkResult 프로시저로 만들어진 JoibID
                        sqlParameter3.Add("MoldInspectBasisID", strMcInstpectBasisID); //GP LOT번호 &&바코드로 받아옴
                        sqlParameter3.Add("MoldInspectSeq", strMcInspectBasisSeq);//MCSEQ
                        sqlParameter3.Add("MoldInsSeq", strMcInspectBasisSeq);
                        sqlParameter3.Add("MldRInspectLegend", strMcrInspectLegend);//
                        sqlParameter3.Add("MldRValue", strMcrInspectValue);//GP LOT별 수량

                        sqlParameter3.Add(Work_iMcRegularInspectSub.CREATEUSERID, Frm_tprc_Main.g_tBase.PersonID);//전역변수, 메인폼에 있는 전역변수값 가져오기

                        WizCommon.Procedure pro3 = new WizCommon.Procedure();
                        pro3.list_OutputName = new List<string>();
                        pro3.list_OutputLength = new List<string>();

                        pro3.Name = "xp_WizWork_dvlMoldIns_iRegularInspectSub";
                        pro3.OutputUseYN = "N";
                        pro3.list_OutputName.Add("MoldRInspectID");
                        pro3.list_OutputLength.Add("20");

                        Prolist.Add(pro3);
                        ListParameter.Add(sqlParameter3);
                    }
                }

                List<KeyValue> list_Result = new List<KeyValue>();
                list_Result = DataStore.Instance.ExecuteAllProcedureOutputListGetCS(Prolist, ListParameter);

                if (list_Result[0].key.ToLower() == "success")
                {
                    WizCommon.Popup.MyMessageBox.ShowBox("금형 점검 등록을 완료하였습니다", "[금형점검등록완료]", 3, 1);
                    ClearData();
                }
                else
                {
                    WizCommon.Popup.MyMessageBox.ShowBox("[저장실패]\r\n" + list_Result[0].value.ToString(), "[오류]", 0, 1);
                    return;
                }
                DataStore.Instance.CloseConnection(); //2021-10-07 DB 커넥트 연결 해제
                GridCellClear();
            }
            catch (Exception excpt)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message), "[오류]", 0, 1);
            }
        }

        #endregion


        #region 그리드 버튼(초기화, 수정, 입력)


        //초기화(범례)
        private void cmdLegend_Clear_Click(object sender, EventArgs e)
        {
            LegendClear();
        }

        //초기화(수치)
        private void cmdFigure_Clear_Click(object sender, EventArgs e)
        {
            FigureClear();
        }

        //수정(범례)
        private void cmdLegend_mod_Click(object sender, EventArgs e)
        {
            if (GridData1.Rows.Count > 0 && GridData1.SelectedRows.Count > 0)
            {
                int i = GridData1.SelectedRows[0].Index;
                No = GridData1.SelectedRows[0].Cells["No"].Value.ToString().Trim();
                CheckList = GridData1.SelectedRows[0].Cells["CheckList"].Value.ToString().Trim();
                InsContents = GridData1.SelectedRows[0].Cells["InsContents"].Value.ToString().Trim();
                McInsCheck = GridData1.SelectedRows[0].Cells["MoldInsCheck"].Value.ToString().Trim();
                Path = GridData1.SelectedRows[0].Cells["MoldInspectImagePath"].Value.ToString().Trim();
                File = GridData1.SelectedRows[0].Cells["MoldInspectImageFile"].Value.ToString().Trim();

                II = new Frm_PopUp_ImgIns(GridData1.Rows.Count, i, No, CheckList, InsContents, McInsCheck, Path, File);
                II.blMod = true;
                II.WriteTextEvent += new Frm_PopUp_ImgIns.TextEventHandler(GetData);
                II.Show();
            }
        }

        //수정(수치)
        private void cmdFigure_mod_Click(object sender, EventArgs e)
        {
            if (GridData2.Rows.Count > 0 && GridData2.SelectedRows.Count > 0)
            {
                int i = GridData2.SelectedRows[0].Index;
                No = GridData2.SelectedRows[0].Cells["No"].Value.ToString().Trim();
                CheckList = GridData2.SelectedRows[0].Cells["CheckList"].Value.ToString().Trim();
                InsContents = GridData2.SelectedRows[0].Cells["InsContents"].Value.ToString().Trim();
                McInsCheck = GridData2.SelectedRows[0].Cells["MoldInsCheck"].Value.ToString().Trim();
                Path = GridData2.SelectedRows[0].Cells["MoldInspectImagePath"].Value.ToString().Trim();
                File = GridData2.SelectedRows[0].Cells["MoldInspectImageFile"].Value.ToString().Trim();

                II2 = new Frm_PopUp_ImgIns2(GridData2.Rows.Count, i, No, CheckList, InsContents, McInsCheck, Path, File);
                II2.WriteTextEvent += new Frm_PopUp_ImgIns2.TextEventHandler(GetData2);
                II2.blMod = true;
                II2.Show();
            }
        }

        //입력(범례)
        private void cmdLegend_Click(object sender, EventArgs e)
        {
            if (GridData1.Rows.Count > 0)//검색해서 행이 채워졌을때
            {
                for (int i = 0; i < GridData1.Rows.Count; i++)
                {
                    if (GridData1.Rows[i].Cells["InspectionLegendName"].Value.ToString() == string.Empty)
                    {
                        //if ((GridData1.Rows[i].Cells["McImagePath"].Value != null && GridData1.Rows[i].Cells["McImageFile"].Value != null) 
                        //    || (GridData1.Rows[i].Cells["McImagePath"].Value.ToString() != "" && GridData1.Rows[i].Cells["McImageFile"].Value.ToString() != ""))
                        if (GridData1.Rows[i].Cells["MoldInspectImageFile"].Value != null && GridData1.Rows[i].Cells["MoldInspectImageFile"].Value.ToString().Trim() != "")
                        {
                            No = GridData1.Rows[i].Cells["No"].Value.ToString().Trim();
                            CheckList = GridData1.Rows[i].Cells["CheckList"].Value.ToString().Trim();
                            InsContents = GridData1.Rows[i].Cells["InsContents"].Value.ToString().Trim();
                            McInsCheck = GridData1.Rows[i].Cells["MoldInsCheck"].Value.ToString().Trim();
                            Path = GridData1.Rows[i].Cells["MoldInspectImagePath"].Value.ToString().Trim();
                            File = GridData1.Rows[i].Cells["MoldInspectImageFile"].Value.ToString().Trim();
                        }
                        else
                        {
                            No = GridData1.Rows[i].Cells["No"].Value.ToString().Trim();
                            CheckList = GridData1.Rows[i].Cells["CheckList"].Value.ToString().Trim();
                            InsContents = GridData1.Rows[i].Cells["InsContents"].Value.ToString().Trim();
                            McInsCheck = GridData1.Rows[i].Cells["MoldInsCheck"].Value.ToString().Trim();
                            Path = string.Empty;
                            File = string.Empty;
                        }
                        //행의 전체 크기, 행의 입력시작 행위치를 보내서 행위치부터 마지막까지의 입력값만 받아온다.
                        GridData1.Rows[i].Selected = true;
                        int cnt = 0;
                        foreach (DataGridViewRow dgvr in GridData1.Rows)
                        {
                            if (dgvr.Visible)
                            { ++cnt; }
                        }

                        II = new Frm_PopUp_ImgIns(cnt, i, No, CheckList, InsContents, McInsCheck, Path, File, "");
                        II.blMod = false;
                        II.WriteTextEvent += new Frm_PopUp_ImgIns.TextEventHandler(GetData);
                        II.Show();
                        break;
                    }
                }
            }
        }

        //입력(수치)
        private void cmdFigure_Click(object sender, EventArgs e)
        {
            if (GridData2.Rows.Count > 0)
            {
                for (int i = 0; i < GridData2.Rows.Count; i++)
                {
                    GridData2.Rows[i].Selected = true;
                    if (GridData2.Rows[i].Cells["InspectionFigure"].Value.ToString() == string.Empty)
                    {
                        if (GridData2.Rows[i].Cells["MoldInspectImageFile"].Value != null && GridData2.Rows[i].Cells["MoldInspectImageFile"].Value != null)
                        {
                            No = GridData2.Rows[i].Cells["No"].Value.ToString().Trim();
                            CheckList = GridData2.Rows[i].Cells["CheckList"].Value.ToString().Trim();
                            InsContents = GridData2.Rows[i].Cells["InsContents"].Value.ToString().Trim();
                            McInsCheck = GridData2.Rows[i].Cells["MoldInsCheck"].Value.ToString().Trim();
                            Path = GridData2.Rows[i].Cells["MoldInspectImagePath"].Value.ToString().Trim();
                            File = GridData2.Rows[i].Cells["MoldInspectImageFile"].Value.ToString().Trim();
                        }
                        else
                        {
                            No = GridData2.Rows[i].Cells["No"].Value.ToString().Trim();
                            CheckList = GridData2.Rows[i].Cells["CheckList"].Value.ToString().Trim();
                            InsContents = GridData2.Rows[i].Cells["InsContents"].Value.ToString().Trim();
                            McInsCheck = GridData2.Rows[i].Cells["MoldInsCheck"].Value.ToString().Trim();
                            Path = string.Empty;
                            File = string.Empty;
                        }
                        //행의 전체 크기, 행의 입력시작 행위치를 보내서 행위치부터 마지막까지의 입력값만 받아온다.
                        GridData2.Rows[i].Selected = true;
                        int cnt = 0;
                        foreach (DataGridViewRow dgvr in GridData2.Rows)
                        {
                            if (dgvr.Visible)
                            { ++cnt; }
                        }
                        II2 = new Frm_PopUp_ImgIns2(cnt, i, No, CheckList, InsContents, McInsCheck, Path, File, "");
                        II2.WriteTextEvent += new Frm_PopUp_ImgIns2.TextEventHandler(GetData2);
                        II2.blMod = false;
                        II2.Show();
                        break;
                    }
                }
            }
        }



        #endregion

        #region 데이터 처리

        void GetData(int a, string InspectionLegendName, string InspectionLegendID, Frm_PopUp_ImgIns Pop_II)
        {
            GridData1.Rows[a].Cells["InspectionLegendName"].Value = InspectionLegendName;
            GridData1.Rows[a].Cells["MldInspectLegend"].Value = InspectionLegendID;

            for (int i = a; GridData1.Rows.Count - 1 >= a + 1; a++)
            {
                if (GridData1.Rows.Count - 1 >= a + 1)
                {
                    if (GridData1.Rows[a + 1].Cells["InspectionLegendName"].Value.ToString() == "")
                    {
                        Pop_II.sNo = GridData1.Rows[a + 1].Cells["No"].Value.ToString().Trim();
                        Pop_II.sCheckList = GridData1.Rows[a + 1].Cells["CheckList"].Value.ToString().Trim();
                        Pop_II.sInsContents = GridData1.Rows[a + 1].Cells["InsContents"].Value.ToString().Trim();
                        Pop_II.sMcInsCheck = GridData1.Rows[a + 1].Cells["MoldInsCheck"].Value.ToString().Trim();
                        Pop_II.sPath = GridData1.Rows[a + 1].Cells["MoldInspectImagePath"].Value.ToString().Trim();
                        Pop_II.sFile = GridData1.Rows[a + 1].Cells["MoldInspectImageFile"].Value.ToString().Trim();
                        Pop_II.sCurrentRow = a;
                        GridData1.Rows[a + 1].Selected = true;
                        break;
                    }
                    else
                    {
                        Pop_II.sNo = "";
                        Pop_II.sCheckList = "";
                        Pop_II.sInsContents = "";
                        Pop_II.sMcInsCheck = "";
                        Pop_II.sPath = "";
                        Pop_II.sFile = "";
                    }
                }
            }

        }


        void GetData2(int a, string InspectionLegendFigure, Frm_PopUp_ImgIns2 Pop_II)
        {
            GridData2.Rows[a].Cells["InspectionFigure"].Value = InspectionLegendFigure;

            for (int i = a; GridData2.Rows.Count - 1 >= a + 1; a++)
            {
                if (GridData2.Rows.Count - 1 >= a + 1)
                {
                    if (GridData2.Rows[a + 1].Cells["InspectionFigure"].Value.ToString() == "")
                    {
                        Pop_II.sNo = GridData2.Rows[a + 1].Cells["No"].Value.ToString().Trim();
                        Pop_II.sCheckList = GridData2.Rows[a + 1].Cells["CheckList"].Value.ToString().Trim();
                        Pop_II.sInsContents = GridData2.Rows[a + 1].Cells["InsContents"].Value.ToString().Trim();
                        Pop_II.sMcInsCheck = GridData2.Rows[a + 1].Cells["MoldInsCheck"].Value.ToString().Trim();
                        Pop_II.sPath = GridData2.Rows[a + 1].Cells["MoldInspectImagePath"].Value.ToString().Trim();
                        Pop_II.sFile = GridData2.Rows[a + 1].Cells["MoldInspectImageFile"].Value.ToString().Trim();
                        Pop_II.sCurrentRow = a;
                        GridData2.Rows[a + 1].Selected = true;
                        break;
                    }
                    else
                    {
                        Pop_II.sNo = "";
                        Pop_II.sCheckList = "";
                        Pop_II.sInsContents = "";
                        Pop_II.sMcInsCheck = "";
                        Pop_II.sPath = "";
                        Pop_II.sFile = "";
                    }
                }
            }
        }

        #endregion

    }
}
