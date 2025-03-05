using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WizCommon;

namespace WizInOut
{
    public partial class Frm_tinout_OutWareScan_Q : Form
    {
        WizWorkLib Lib = new WizWorkLib();
        int RowsCount = 0; //조회한 그리드 수
        int CheckRowsCount = 0; //체크표시한 그리드 수
        bool blOpen = false;
        string OutWareID = ""; //수정시 사용할 OutwareID
        private DataSet ds = null;
        //int OutSeq = 0;        //수정시 사용할 OutSeq

        LogData LogData = new LogData(); //2022-10-24 log 남기는 함수

        public Frm_tinout_OutWareScan_Q()
        {
            InitializeComponent();
        }

        private void Frm_tinout_OutWareScan_Q_Load(object sender, EventArgs e)
        {
            LogData.LogSave(this.GetType().Name, "S"); //log 남기기(로드 S) 2022-10-24

            //출고일자 Checked
            chkODate.Checked = true;
            //출고일자 From
            mtb_From.Text = DateTime.Today.AddDays(-1).ToString("yyyyMMdd");
            //출고일자 To
            mtb_To.Text = DateTime.Today.ToString("yyyy-MM-dd");

            InitGrid();

            SetComboBox();
        }

        #region 그리드 초기화

        private void InitGrid()
        {
            dgvOutware.Columns.Clear();
            dgvOutware.ColumnCount = 17;

            int i = 0;

            //순번
            dgvOutware.Columns[i].Name = "No";
            dgvOutware.Columns[i].HeaderText = "No";
            dgvOutware.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvOutware.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            //관리번호
            dgvOutware.Columns[++i].Name = "OrderID";
            dgvOutware.Columns[i].HeaderText = "관리번호";
            dgvOutware.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvOutware.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvOutware.Columns[i].Visible = false;
            //출고일자
            dgvOutware.Columns[++i].Name = "OutDate";
            dgvOutware.Columns[i].HeaderText = "출고일자";
            dgvOutware.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvOutware.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dgvOutware.Columns[i].Visible = true;
            //납품거래처
            dgvOutware.Columns[++i].Name = "Custom";
            dgvOutware.Columns[i].HeaderText = "납품거래처";
            dgvOutware.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvOutware.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dgvOutware.Columns[i].Visible = true;
            //품명
            dgvOutware.Columns[++i].Name = "Article";
            dgvOutware.Columns[i].HeaderText = "품명";
            dgvOutware.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvOutware.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dgvOutware.Columns[i].Visible = true;
            //품번
            dgvOutware.Columns[++i].Name = "BuyerArticleNo";
            dgvOutware.Columns[i].HeaderText = "품번";
            dgvOutware.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvOutware.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dgvOutware.Columns[i].Visible = true;
            //출고량
            dgvOutware.Columns[++i].Name = "Qty";
            dgvOutware.Columns[i].HeaderText = "출고량";
            dgvOutware.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvOutware.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dgvOutware.Columns[i].Visible = true;
            //출고박스
            dgvOutware.Columns[++i].Name = "BoxQty";
            dgvOutware.Columns[i].HeaderText = "출고박스";
            dgvOutware.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvOutware.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dgvOutware.Columns[i].Visible = true;
            //출고단위
            dgvOutware.Columns[++i].Name = "UnitClss";
            dgvOutware.Columns[i].HeaderText = "단위";
            dgvOutware.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvOutware.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvOutware.Columns[i].Visible = true;
            //출고처
            dgvOutware.Columns[++i].Name = "OutCustom";
            dgvOutware.Columns[i].HeaderText = "출고처명";
            dgvOutware.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvOutware.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dgvOutware.Columns[i].Visible = true;
            //출고구분
            dgvOutware.Columns[++i].Name = "OutGbn";
            dgvOutware.Columns[i].HeaderText = "출고구분";
            dgvOutware.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvOutware.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dgvOutware.Columns[i].Visible = true;
            //수주거래처
            dgvOutware.Columns[++i].Name = "OrderCustom";
            dgvOutware.Columns[i].HeaderText = "수주거래처";
            dgvOutware.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvOutware.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvOutware.Columns[i].Visible = false;
            //차종
            dgvOutware.Columns[++i].Name = "Model";
            dgvOutware.Columns[i].HeaderText = "차종";
            dgvOutware.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvOutware.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvOutware.Columns[i].Visible = false;
            //비고
            dgvOutware.Columns[++i].Name = "Comments";
            dgvOutware.Columns[i].HeaderText = "비고";
            dgvOutware.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvOutware.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvOutware.Columns[i].Visible = false;
            //단가
            dgvOutware.Columns[++i].Name = "Price";
            dgvOutware.Columns[i].HeaderText = "단가";
            dgvOutware.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvOutware.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvOutware.Columns[i].Visible = false;
            //라벨
            dgvOutware.Columns[++i].Name = "LabelID";
            dgvOutware.Columns[i].HeaderText = "라벨";
            dgvOutware.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvOutware.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvOutware.Columns[i].Visible = false;
            //OutwareID
            dgvOutware.Columns[++i].Name = "OutwareID";
            dgvOutware.Columns[i].HeaderText = "OutwareID";
            dgvOutware.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvOutware.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvOutware.Columns[i].Visible = false;
            ////OutSeq
            //dgvOutware.Columns[++i].Name = "OutSeq";
            //dgvOutware.Columns[i].HeaderText = "OutSeq";
            //dgvOutware.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            //dgvOutware.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            //dgvOutware.Columns[i].Visible = false;


            DataGridViewCheckBoxColumn chkCol = new DataGridViewCheckBoxColumn();
            {
                chkCol.HeaderText = "";
                chkCol.Name = "Check";
                chkCol.Width = 110;
                chkCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                chkCol.FlatStyle = FlatStyle.Standard;
                chkCol.ThreeState = true;
                chkCol.CellTemplate = new DataGridViewCheckBoxCell();
                chkCol.CellTemplate.Style.BackColor = Color.Beige;
                chkCol.Visible = true;
            }
            dgvOutware.Columns.Insert(0, chkCol);

            dgvOutware.ReadOnly = true;
            dgvOutware.Font = new Font("맑은 고딕", 10, FontStyle.Bold);
            dgvOutware.RowTemplate.Height = 50;
            dgvOutware.ColumnHeadersHeight = 35;
            dgvOutware.ScrollBars = ScrollBars.Both;
            dgvOutware.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvOutware.MultiSelect = false;
            dgvOutware.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvOutware.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(234, 234, 234);
            dgvOutware.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            foreach (DataGridViewColumn col in dgvOutware.Columns)
            {
                if (col.Index > 0)
                {
                    col.DataPropertyName = col.Name;
                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    col.SortMode = DataGridViewColumnSortMode.NotSortable;
                    //col.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                }

            }

        }

        #endregion

        #region 조회, 수정, 삭제 함수

        private void FillGrid()
        {
            dgvOutware.Rows.Clear();
            RowsCount = 0;

            try
            {
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Clear();

                //일자
                sqlParameter.Add("nChkDate", chkODate.Checked == true ? 1 : 0);
                sqlParameter.Add("sSDate", mtb_From.Text.Replace("-", ""));
                sqlParameter.Add("sEDate", mtb_To.Text.Replace("-", ""));
                //납품거래처
                sqlParameter.Add("nChkCustom", chkCustom.Checked == true ? 1 : 0);
                sqlParameter.Add("sCustom", txtCustomTag.Text.ToString());
                //품명
                sqlParameter.Add("nChkArticleID", chkArticle.Checked == true ? 1 : 0);
                sqlParameter.Add("sArticleID", txtArticle.Text.ToString());
                //출고구분
                sqlParameter.Add("nChkOutClss", chkOutClss.Checked == true ? 1 : 0);
                sqlParameter.Add("sOutClss", cboOutClss.SelectedValue.ToString()); //txtSGbnTag.Text.ToString()
                //관리번호
                //sqlParameter.Add("nChkOrderID", chkOrderID.Checked == true ? 1 : 0);
                //sqlParameter.Add("sOrderID", txtOrderID.Text.ToString());

                DataSet ds = DataStore.Instance.ProcedureToDataSet_NewLog("xp_WizWork_sOutware", sqlParameter, true, "R", Frm_tinout_Main.g_tBase.PersonID);

                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        int i = 0;
                        DataRowCollection drc = dt.Rows;

                        foreach (DataRow dr in drc)
                        {
                            dgvOutware.Rows.Add(false,                    //체크박스
                                                 ++i,                     //순번
                                                 dr["OrderID"].ToString(),//관리번호
                                                 dr["OutDate"].ToString(),//출고일자
                                                 dr["DvlyCustom"].ToString(),//납품거래처  
                                                 dr["Article"].ToString(), //품명
                                                 dr["BuyerArticleNo"].ToString(),//품번
                                                 string.Format("{0:#,###}", Lib.ConvertDouble(dr["OutQty"].ToString())),//출고량
                                                 dr["BoxQty"].ToString(),//출고박스
                                                 dr["UnitClssName"].ToString(), //단위
                                                 dr["OutCustom"].ToString(),//출고처
                                                 dr["OutClss"].ToString(),//출고구분
                                                 dr["KCustom"].ToString(),//수주거래처
                                                 dr["BuyerModel"].ToString(),//차종
                                                 dr["Remark"].ToString(),//비고
                                                 dr["UnitPrice"].ToString(),//단가
                                                 dr["LabelID"].ToString(),//바코드
                                                 dr["OutwareID"].ToString()//OutwareID                                            
                                );

                            RowsCount++;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                DataStore.Instance.CloseConnection();
            }
        }


        private void UpdateSave()
        {
            Form form = null;//폼 초기화

            Frm_tinout_OutWareScan_U child1 = new Frm_tinout_OutWareScan_U(OutWareID);
            form = child1;

            if (form != null)
            {
                foreach (Form openForm in Application.OpenForms)//중복실행방지
                {
                    if (openForm.Name == form.Name)
                    {
                        openForm.Close();   //2023-12-29
                        blOpen = true;
                        //openForm.BringToFront();
                        //openForm.Activate();
                        //return;
                        break;              //2023-12-29
                    }
                }
                form.MdiParent = this.ParentForm;   //<< 핵심.
                form.TopLevel = false;
                form.Dock = DockStyle.Fill;
                form.Show();

                if (!blOpen)
                {
                    form.BringToFront();
                    form.Show();
                }
            }
        }

        //삭제
        private void Delete(string OutwareID)
        {
            //List<string> list_Confirm = new List<string>();         //프로시저 수행 성공여부 값 저장/success/failure
            Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
            sqlParameter.Add("OutwareID", OutwareID);
            string[] sConfirm = new string[2];
            sConfirm = DataStore.Instance.ExecuteProcedure_NewLog("xp_WizWork_dOutware", sqlParameter, true, "D", Frm_tinout_Main.g_tBase.PersonID);
            //list_Confirm.Add(sConfirm[0]);
            //if (sConfirm[0].ToUpper() == "SUCCESS")
            //{ 

            //}
            //else
            //{

            //}
        }

        #endregion

        #region 검색조건 이벤트

        //일자
        private void mtb_From_Click(object sender, EventArgs e)
        {
            WizCommon.Popup.Frm_TLP_Calendar calendar = new WizCommon.Popup.Frm_TLP_Calendar(mtb_From.Text.Replace("-", ""), mtb_From.Name, mtb_To.Text.Replace("-", ""));
            calendar.WriteDateTextEvent += new WizCommon.Popup.Frm_TLP_Calendar.TextEventHandler(GetDate);
            calendar.Owner = this;
            calendar.ShowDialog();
        }

        //일자
        private void mtb_To_Click(object sender, EventArgs e)
        {
            WizCommon.Popup.Frm_TLP_Calendar calendar = new WizCommon.Popup.Frm_TLP_Calendar(mtb_To.Text.Replace("-", ""), mtb_To.Name, mtb_From.Text.Replace("-", ""));
            calendar.WriteDateTextEvent += new WizCommon.Popup.Frm_TLP_Calendar.TextEventHandler(GetDate);
            calendar.Owner = this;
            calendar.ShowDialog();
        }

        //일자 이벤트 함수
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

        //거래처
        private void chkCustom_Click(object sender, EventArgs e)
        {
            if (chkCustom.Checked)
            {
                Frm_PopUpSel_Common FPSC = new Frm_PopUpSel_Common("Custom", txtCustom.Text, "", "O");
                FPSC.StartPosition = FormStartPosition.CenterScreen;
                FPSC.BringToFront();
                FPSC.TopMost = false;
                FPSC.OWriteTextEvent += PopUp_WriteTextEvent;
                FPSC.ShowDialog();

                void PopUp_WriteTextEvent(string Custom, string InCustom, string OrderID, string OrderSeq, string BuyerArticleNo, string OrderQty, string OUnitClss, string Model, string Article, string DvlyDate, string Work, string ArticleGrp, string CustomID, string ArticleID, string OUnitClssID, string BuyerModelID, string WorkID, string InCustomID, string ArticleGrpID, string OK)
                {
                    if (OK == "Cancel")
                    {
                        return;
                    }
                    else
                    {
                        txtCustomTag.Text = OrderID;
                        txtCustom.Text = Custom;
                    }
                }
            }
            else
            {
                txtCustomTag.Text = "";
                txtCustom.Text = "";
            }
        }

        //거래처
        private void txtCustom_Click(object sender, EventArgs e)
        {
            Frm_PopUpSel_Common FPSC = new Frm_PopUpSel_Common("Custom", txtCustom.Text, "", "O");
            FPSC.StartPosition = FormStartPosition.CenterScreen;
            FPSC.BringToFront();
            FPSC.TopMost = false;
            FPSC.OWriteTextEvent += PopUp_WriteTextEvent;
            FPSC.ShowDialog();

            void PopUp_WriteTextEvent(string Custom, string InCustom, string OrderID, string OrderSeq, string BuyerArticleNo, string OrderQty, string OUnitClss, string Model, string Article, string DvlyDate, string Work, string ArticleGrp, string CustomID, string ArticleID, string OUnitClssID, string BuyerModelID, string WorkID, string InCustomID, string ArticleGrpID, string OK)
            {
                if (OK == "Cancel")
                {
                    return;
                }
                else
                {
                    chkCustom.Checked = true;
                    txtCustomTag.Text = OrderID;
                    txtCustom.Text = Custom;
                }
            }
        }

        //품번
        private void chkBuyerArticleNo_Click(object sender, EventArgs e)
        {
            Frm_PopUpSel_Common FPSC = new Frm_PopUpSel_Common("MA", txtBuyerArticleNo.Text, "", "O");
            FPSC.StartPosition = FormStartPosition.CenterScreen;
            FPSC.BringToFront();
            FPSC.TopMost = false;
            FPSC.OWriteTextEvent += PopUp_WriteTextEvent;
            FPSC.ShowDialog();

            void PopUp_WriteTextEvent(string Custom, string InCustom, string OrderID, string OrderSeq, string BuyerArticleNo, string OrderQty, string OUnitClss, string Model, string Article, string DvlyDate, string Work, string ArticleGrp, string CustomID, string ArticleID, string OUnitClssID, string BuyerModelID, string WorkID, string InCustomID, string ArticleGrpID, string OK)
            {
                if (OK == "Cancel")
                {
                    return;
                }
                else
                {
                    chkBuyerArticleNo.Checked = true;
                    txtArticleTag.Text = ArticleID;
                    txtBuyerArticleNo.Text = BuyerArticleNo;
                }
            }
        }

        //품번
        private void txtBuyerArticleNo_Click(object sender, EventArgs e)
        {
            Frm_PopUpSel_Common FPSC = new Frm_PopUpSel_Common("MA", txtBuyerArticleNo.Text, "", "O");
            FPSC.StartPosition = FormStartPosition.CenterScreen;
            FPSC.BringToFront();
            FPSC.TopMost = false;
            FPSC.OWriteTextEvent += PopUp_WriteTextEvent;
            FPSC.ShowDialog();

            void PopUp_WriteTextEvent(string Custom, string InCustom, string OrderID, string OrderSeq, string BuyerArticleNo, string OrderQty, string OUnitClss, string Model, string Article, string DvlyDate, string Work, string ArticleGrp, string CustomID, string ArticleID, string OUnitClssID, string BuyerModelID, string WorkID, string InCustomID, string ArticleGrpID, string OK)
            {
                if (OK == "Cancel")
                {
                    return;
                }
                else
                {
                    chkBuyerArticleNo.Checked = true;
                    txtArticleTag.Text = ArticleID;
                    txtBuyerArticleNo.Text = BuyerArticleNo;
                }
            }
        }

        //관리번호
        private void chkOrderID_Click(object sender, EventArgs e)
        {
            Frm_PopUpSel_Common FPSC = new Frm_PopUpSel_Common("OrderID", txtOrderID.Text, "", "O");
            FPSC.StartPosition = FormStartPosition.CenterScreen;
            FPSC.BringToFront();
            FPSC.TopMost = false;
            FPSC.OWriteTextEvent += PopUp_WriteTextEvent;
            FPSC.ShowDialog();

            void PopUp_WriteTextEvent(string Custom, string InCustom, string OrderID, string OrderSeq, string BuyerArticleNo, string OrderQty, string OUnitClss, string Model, string Article, string DvlyDate, string Work, string ArticleGrp, string CustomID, string ArticleID, string OUnitClssID, string BuyerModelID, string WorkID, string InCustomID, string ArticleGrpID, string OK)
            {
                if (OK == "Cancel")
                {
                    return;
                }
                else
                {
                    chkOrderID.Checked = true;
                    txtOrderID.Text = OrderID;
                }
            }
        }

        //관리번호
        private void txtOrderID_Click(object sender, EventArgs e)
        {
            Frm_PopUpSel_Common FPSC = new Frm_PopUpSel_Common("OrderID", txtOrderID.Text, "", "O");
            FPSC.StartPosition = FormStartPosition.CenterScreen;
            FPSC.BringToFront();
            FPSC.TopMost = false;
            FPSC.OWriteTextEvent += PopUp_WriteTextEvent;
            FPSC.ShowDialog();

            void PopUp_WriteTextEvent(string Custom, string InCustom, string OrderID, string OrderSeq, string BuyerArticleNo, string OrderQty, string OUnitClss, string Model, string Article, string DvlyDate, string Work, string ArticleGrp, string CustomID, string ArticleID, string OUnitClssID, string BuyerModelID, string WorkID, string InCustomID, string ArticleGrpID, string OK)
            {
                if (OK == "Cancel")
                {
                    return;
                }
                else
                {
                    chkOrderID.Checked = true;
                    txtOrderID.Text = OrderID;
                }
            }
        }

        //품명
        private void chkArticle_Click(object sender, EventArgs e)
        {
            if (chkArticle.Checked)
            {
                Frm_PopUpSel_Common FPSC = new Frm_PopUpSel_Common("MA", txtArticle.Text, "", "O");
                FPSC.StartPosition = FormStartPosition.CenterScreen;
                FPSC.BringToFront();
                FPSC.TopMost = false;
                FPSC.OWriteTextEvent += PopUp_WriteTextEvent;
                FPSC.ShowDialog();

                void PopUp_WriteTextEvent(string Custom, string InCustom, string OrderID, string OrderSeq, string BuyerArticleNo, string OrderQty, string OUnitClss, string Model, string Article, string DvlyDate, string Work, string ArticleGrp, string CustomID, string ArticleID, string OUnitClssID, string BuyerModelID, string WorkID, string InCustomID, string ArticleGrpID, string OK)
                {
                    if (OK == "Cancel")
                    {
                        return;
                    }
                    else
                    {
                        txtArticleTag.Text = ArticleID;
                        txtArticle.Text = Article;
                    }
                }
            }
            else
            {
                txtArticleTag.Text = "";
                txtArticle.Text = "";
            }
        }

        //품명
        private void txtArticle_Click(object sender, EventArgs e)
        {
            Frm_PopUpSel_Common FPSC = new Frm_PopUpSel_Common("MA", txtArticle.Text, "", "O");
            FPSC.StartPosition = FormStartPosition.CenterScreen;
            FPSC.BringToFront();
            FPSC.TopMost = false;
            FPSC.OWriteTextEvent += PopUp_WriteTextEvent;
            FPSC.ShowDialog();

            void PopUp_WriteTextEvent(string Custom, string InCustom, string OrderID, string OrderSeq, string BuyerArticleNo, string OrderQty, string OUnitClss, string Model, string Article, string DvlyDate, string Work, string ArticleGrp, string CustomID, string ArticleID, string OUnitClssID, string BuyerModelID, string WorkID, string InCustomID, string ArticleGrpID, string OK)
            {
                if (OK == "Cancel")
                {
                    return;
                }
                else
                {
                    chkArticle.Checked = true;
                    txtArticleTag.Text = ArticleID;
                    txtArticle.Text = Article;
                }
            }
        }

        #endregion

        #region 조회, 닫기, 수정, 삭제 이벤트

        private void btnFillGrid_Click(object sender, EventArgs e)
        {
            FillGrid();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            LogData.LogSave(this.GetType().Name, "S"); //log 남기기(로드 S) 2022-10-24
            this.Close();
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            if (dgvOutware.Rows.Count > 0)
            {
                //전체선택, 전체해제 조건문
                if (btnAll.Text == "전체선택")
                {
                    for (int i = 0; i < dgvOutware.Rows.Count; i++)
                    {
                        dgvOutware.Rows[i].Cells["Check"].Value = true;
                    }

                    btnAll.Text = "전체해제";
                }
                else
                {
                    for (int i = 0; i < dgvOutware.Rows.Count; i++)
                    {
                        dgvOutware.Rows[i].Cells["Check"].Value = false;
                    }

                    btnAll.Text = "전체선택";
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int CheckCount = 0;

            if (dgvOutware.Rows.Count > 0)
            {
                foreach (DataGridViewRow dgvsr in dgvOutware.Rows)
                {
                    DataGridViewCell Cell = dgvsr.Cells["Check"];
                    bool isChecked = (bool)Cell.EditedFormattedValue;
                    if (isChecked)
                    {
                        CheckCount++;
                        if (CheckCount > 1)
                        {
                            WizCommon.Popup.MyMessageBox.ShowBox("수정은 하나만 가능합니다.\r\n체크박스 유무를 확인해주세요.", "[수정 전 확인]", 0, 1);
                            return;
                        }
                        else
                        {
                            OutWareID = dgvsr.Cells["OutWareID"].Value.ToString();
                            //OutSeq = Lib.ConvertInt(dgvsr.Cells["OutSeq"].Value.ToString());
                        }
                    }
                }
                UpdateSave();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int checkCount = 0;//체크된 카운트
            int deleteCount = 0;    //삭제된 데이터 카운트
          //int NotdeleteCount = 0; //삭제 되지 않은 데이터 카운트

            if (dgvOutware.RowCount == 0)
            {
                WizCommon.Popup.MyMessageBox.ShowBox("조회 후 삭제 버튼을 눌러주십시오.", "[조회자료 없음]", 0, 1);
            }
            else
            {
                foreach (DataGridViewRow dgvr in dgvOutware.Rows)
                {
                    if (dgvr.Cells["Check"].Value.ToString().ToUpper() == "TRUE")
                    {
                        checkCount++;
                    }
                }

                if (checkCount == 0)
                {
                    WizCommon.Popup.MyMessageBox.ShowBox("삭제 대상을 선택 후 '삭제'버튼을 클릭해주세요.", "[삭제 대상 클릭]", 0, 0);
                }
                else
                {
                    //삭제
                    if (WizCommon.Popup.MyMessageBox.ShowBox("선택한 출고이력을 삭제처리하시겠습니까?", "[삭제]", 0, 0) == DialogResult.OK)
                    {
                        foreach (DataGridViewRow dgvr in dgvOutware.Rows)
                        {
                            if (dgvr.Cells["Check"].Value.ToString().ToUpper() == "TRUE")
                            {
                                //사용이력 찾아서 메세지로 확인
                                //if (CheckUseLotID(dgvr.Cells["LotID"].Value.ToString()))
                                //{
                                    Delete(dgvr.Cells["OutwareID"].Value.ToString());
                                    deleteCount++;
                                    //WizCommon.Popup.MyMessageBox.ShowBox("삭제 되었습니다.", "[삭제]", 0, 0);
                                //}
                                //else
                                //{
                                //    NotdeleteCount++;
                                //}
                            }
                        }

                        //삭제된 경우
                        if (deleteCount > 0)
                        {
                            WizCommon.Popup.MyMessageBox.ShowBox(deleteCount.ToString() + "건 삭제되었습니다.", "[삭제 완료]", 0, 1);
                            //if (NotdeleteCount > 0)
                            //{
                            //    WizCommon.Popup.MyMessageBox.ShowBox(deleteCount.ToString() + "건은 사용이력이 있어 삭제가 되지 않았습니다.", "[삭제 완료]", 0, 1);
                            //}
                        }
                        else //삭제가 안 된 경우
                        {
                            //if (NotdeleteCount > 0)
                            //{
                            //    WizCommon.Popup.MyMessageBox.ShowBox(deleteCount.ToString() + "건은 사용이력이 있어 삭제가 되지 않았습니다.", "[삭제 완료]", 0, 1);
                            //}
                        }

                        FillGrid();
                    }
                }
            }
        }

        private void btnLabelList_Click(object sender, EventArgs e)
        {
            //체크표시 확인, 하나했는지 두개이상했는지 확인 필요
            int checkcount = 0;
            string OutwareID = "";

            for (int i = 0; i < dgvOutware.Rows.Count; i++)
            {
                if (dgvOutware.Rows[i].Cells["Check"].Value.ToString() == "True")
                {
                    checkcount++;
                    OutwareID = dgvOutware.Rows[i].Cells["OutwareID"].Value.ToString();
                }
            }

            //상세라벨리스트는 하나의 출고정보만 볼수있음
            if (checkcount == 1)
            {
                //수입검사이력이 있으면 보여주기
                Frm_PopUpSel_sOutwareSub FPUSOS = new Frm_PopUpSel_sOutwareSub(OutwareID);
                FPUSOS.StartPosition = FormStartPosition.CenterScreen;
                FPUSOS.BringToFront();
                FPUSOS.TopMost = false;
                FPUSOS.ShowDialog();
            }
            else
            {
                WizCommon.Popup.MyMessageBox.ShowBox("선택된 출고이력이 없거나, \r\n출고이력이 여러개 선택되었습니다.\r\n하나의 출고이력만 선택해주세요.", "[라벨리스트확인 전 확인]", 0, 1);
                return;
            }
        }

        #endregion

        #region 그리드 클릭 이벤트

        private void dgvOutware_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                CheckRowsCount = 0;

                if (dgvOutware.Rows[e.RowIndex].Cells["Check"].Value.ToString().ToUpper() == "FALSE")
                {
                    dgvOutware.Rows[e.RowIndex].Cells["Check"].Value = true;

                    for (int i = 0; i < dgvOutware.Rows.Count; i++)
                    {
                        if (dgvOutware.Rows[i].Cells["Check"].Value.ToString().ToUpper() == "TRUE")
                        {
                            CheckRowsCount++;
                        }
                    }

                    if (RowsCount == CheckRowsCount)
                    {
                        btnAll.Text = "전체해제";
                    }
                    else
                    {
                        btnAll.Text = "전체선택";
                    }

                }
                else if (dgvOutware.Rows[e.RowIndex].Cells["Check"].Value.ToString().ToUpper() == "TRUE")
                {
                    dgvOutware.Rows[e.RowIndex].Cells["Check"].Value = false;

                    for (int i = 0; i < dgvOutware.Rows.Count; i++)
                    {
                        if (dgvOutware.Rows[i].Cells["Check"].Value.ToString().ToUpper() == "TRUE")
                        {
                            CheckRowsCount++;
                        }
                    }

                    if (RowsCount == CheckRowsCount)
                    {
                        btnAll.Text = "전체해제";
                    }
                    else
                    {
                        btnAll.Text = "전체선택";
                    }
                }
            }
        }






        #endregion

        #region 콤보박스 설정

        private void SetComboBox()
        {
            SetcboOutClss();
        }

        //출고구분
        private void SetcboOutClss()
        {
            cboOutClss.Items.Clear();
            //출고구분
            ds = DataStore.Instance.ProcedureToDataSet("[xp_WizWork_SOutClss]", null, false);
            DataRow newRow = ds.Tables[0].NewRow();
            newRow["CodeID"] = "*";
            newRow["CodeName"] = "전체";
            ds.Tables[0].Rows.InsertAt(newRow, 0);
            cboOutClss.DataSource = ds.Tables[0];
            cboOutClss.ValueMember = "CodeID";
            cboOutClss.DisplayMember = "CodeName";
        }



        #endregion


    }
}
