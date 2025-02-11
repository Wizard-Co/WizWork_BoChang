using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using WizCommon;
//using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;

namespace WizInOut
{
    public partial class Frm_tinout_OCStuffin_U : Form
    {
        string m_InspectBasisID = ""; //검사기준 ID
        public string m_InspectText;

        DataGridView dgv = null; //탭페이지 데이터그리드뷰, 불량 수량 용도
        DataGridView dgvtabInspect = null; //탭페이지 데이터그리드뷰 저장 용도

        WizWorkLib Lib = new WizWorkLib();

        Frm_tinout_PopUpSel_InspectText m_Popform_1 = null;
        PopUp.Frm_CMNumericKeypad m_Popform_2 = null;

        public TTag Sub_m_tTag = new TTag();
        public TTagSub Sub_m_tItem = new TTagSub();
        public List<TTagSub> list_m_tItem = new List<TTagSub>();

        private string IsTagID = "";
        List<string> lData = null;
        string[] Message = new string[2];

        string m_StuffinID = "";
        string m_LabelID = "";

        string u_StuffinID = ""; //StuffinID
        string u_LotID = "";     //LotID
        string u_ArticleID = ""; //ArticleID
        string u_InspectID = ""; //InspectID

        //int u_Seq = 0;  //검사실적 수정시 seq 순서

        int index = 0; //콤보박스 데이터 찾기용 변수

        private DataSet ds = null;
        //string SetComboBoxChanged = "0";  //로드시 changed 이벤트 처리 안 되게 하기위해 처음에 0으로 들어오면, 이벤트 실행 안 하고 0이 아닐 경우 진행되게 추가

        LogData LogData = new LogData(); //2022-10-24 log 남기는 함수

        public Frm_tinout_OCStuffin_U()
        {
            InitializeComponent();
        }

        public Frm_tinout_OCStuffin_U(string StuffinID, string LotID, string ArticleID, string InspectID)
        {
            InitializeComponent();
            u_StuffinID = StuffinID;
            u_LotID = LotID;
            u_ArticleID = ArticleID;
            u_InspectID = InspectID;
        }

        private void Frm_tinout_OCStuffin_U_Load(object sender, EventArgs e)
        {
            LogData.LogSave(this.GetType().Name, "S"); //log 남기기(로드 S) 2022-10-24

            //창고

            //입고구분

            InitgrdInsItemGrid();

            InitTabPage();

            SetComboBox();

            //수정일 경우 데이터 보여주기
            if (u_StuffinID != "" && u_LotID != "")
            {
                UFillGrid();
            }
            else //입력인 경우
            {
                //입고일자
                mtb_SDate.Text = DateTime.Today.ToString("yyyy-MM-dd");

                //부가세 별도
                txtVAT.Text = "Y";

                //검사필요여부
                txtInspectYN.Text = "Y";
                //btnInspectPerson.Text = "입고처\r\n검사자";
                //btnInspectDate.Text = "입고처\r\n검사일자";

                //검수일자
                mtb_IDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
            }
        }

        #region 그리드, 탭페이지 초기화

        private void InitgrdInsItemGrid()
        {
            dgvInspect.Columns.Clear();
            dgvInspect.ColumnCount = 13;

            int i = 0;

            // Set the Colums Hearder Names
            dgvInspect.Columns[i].Name = "No";
            dgvInspect.Columns[i].HeaderText = "";
            dgvInspect.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            dgvInspect.Columns[++i].Name = "insItemName";
            dgvInspect.Columns[i].HeaderText = "항목";
            dgvInspect.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvInspect.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;

            dgvInspect.Columns[++i].Name = "InsSpec";
            dgvInspect.Columns[i].HeaderText = "스펙";
            dgvInspect.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvInspect.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;

            dgvInspect.Columns[++i].Name = "InsTPSpecMax";
            dgvInspect.Columns[i].HeaderText = "정성적 Max";
            dgvInspect.Columns[i].Width = 0;
            dgvInspect.Columns[i].Visible = false;

            dgvInspect.Columns[++i].Name = "InsTPSpecMin";
            dgvInspect.Columns[i].HeaderText = "정성적 Min";
            dgvInspect.Columns[i].Width = 0;
            dgvInspect.Columns[i].Visible = false;

            dgvInspect.Columns[++i].Name = "InsRASpecMax";
            dgvInspect.Columns[i].HeaderText = "정량적 Max";
            dgvInspect.Columns[i].Width = 0;
            dgvInspect.Columns[i].Visible = false;

            dgvInspect.Columns[++i].Name = "InsRASpecMin";
            dgvInspect.Columns[i].HeaderText = "정량적 Min";
            dgvInspect.Columns[i].Width = 0;
            dgvInspect.Columns[i].Visible = false;

            dgvInspect.Columns[++i].Name = "InsSampleQty";
            dgvInspect.Columns[i].HeaderText = "샘플수량";
            dgvInspect.Columns[i].Width = 0;
            dgvInspect.Columns[i].Visible = false;

            dgvInspect.Columns[++i].Name = "InsType";
            dgvInspect.Columns[i].HeaderText = "정량/정성 타입";
            dgvInspect.Columns[i].Width = 0;
            dgvInspect.Columns[i].Visible = false;

            dgvInspect.Columns[++i].Name = "SubSeq";
            dgvInspect.Columns[i].HeaderText = "InspectBasisSub";
            dgvInspect.Columns[i].Width = 0;
            dgvInspect.Columns[i].Visible = false;

            dgvInspect.Columns[++i].Name = "MinValue";
            dgvInspect.Columns[i].HeaderText = "최소값";
            dgvInspect.Columns[i].Width = 80;
            dgvInspect.Columns[i].Visible = false;

            dgvInspect.Columns[++i].Name = "MaxValue";
            dgvInspect.Columns[i].HeaderText = "최대값";
            dgvInspect.Columns[i].Width = 80;
            dgvInspect.Columns[i].Visible = false;

            dgvInspect.Columns[++i].Name = "DefectYN";
            dgvInspect.Columns[i].HeaderText = "합/불";
            dgvInspect.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvInspect.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvInspect.Columns[i].Visible = true;

            dgvInspect.Rows.Clear();
            dgvInspect.RowTemplate.Height = 33;

            dgvInspect.ReadOnly = true;
            dgvInspect.Font = new Font("맑은 고딕", 10, FontStyle.Bold);
            dgvInspect.RowTemplate.Height = 30;
            dgvInspect.ColumnHeadersHeight = 35;
            dgvInspect.ScrollBars = ScrollBars.Both;
            dgvInspect.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvInspect.MultiSelect = false;
            dgvInspect.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvInspect.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(234, 234, 234);
            dgvInspect.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            foreach (DataGridViewColumn col in dgvInspect.Columns)
            {
                col.DataPropertyName = col.Name;
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

        }

        private void InitTabPage(string SampleQty)
        {
            int intRowCnt = 0;

            intRowCnt = Int32.Parse(SampleQty);
            string title = (tabInspect.TabCount + 1).ToString();

            tabInspect.DrawItem += tabInspect_DrawItem;

            tabInspect.Font = new Font("맑은 고딕", 12, FontStyle.Bold);
            tabInspect.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabInspect.SizeMode = TabSizeMode.Fixed;

            dgv = new DataGridView();
            dgv.Dock = DockStyle.Fill;
            dgv.Margin = new Padding(1, 1, 1, 1);

            InitGrid(dgv);

            for (int i = 0; i < intRowCnt; i++)
            {
                dgv.Rows.Add(i + 1, "");
            }

            SetTabPage(dgv, title);
        }

        private void InitTabPage()
        {
            tabInspect.TabPages.Clear();

            tabInspect.DrawItem += tabInspect_DrawItem;

            tabInspect.Font = new Font("맑은 고딕", 12, FontStyle.Bold);
            tabInspect.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabInspect.SizeMode = TabSizeMode.Fixed;
            //tabControl1.Alignment = System.Windows.Forms.TabAlignment.MiddleCenter;
            dgv = new DataGridView();
            dgv.Dock = DockStyle.Fill;
            dgv.Margin = new Padding(1, 1, 1, 1);
            InitGrid(dgv);
            SetTabPage(dgv, "1");
        }

        private void tabInspect_DrawItem(object sender, DrawItemEventArgs e)
        {
            Graphics gr = e.Graphics;
            Font font = new Font("맑은 고딕", 14, FontStyle.Bold);

            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;

            for (int i = 0; i < tabInspect.TabPages.Count; i++)
            {
                string str = "";
                str = tabInspect.TabPages[i].Text;
                gr.DrawString(str, font, Brushes.Black, tabInspect.GetTabRect(i), sf);
            }
        }

        private void InitGrid(DataGridView _dgv)
        {
            _dgv.AllowUserToAddRows = false;
            _dgv.Columns.Clear();
            _dgv.ColumnCount = 2;           

            int n = 0;
            // Set the Colums Hearder Names
            _dgv.Columns[n].Name = "No";
            _dgv.Columns[n].HeaderText = "No";
            _dgv.Columns[n].Width = 65;
            _dgv.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            _dgv.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            _dgv.Columns[n++].Visible = true;

            _dgv.Columns[n].Name = "InspectValueText";
            _dgv.Columns[n].HeaderText = "측정값";
            _dgv.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            _dgv.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            _dgv.Columns[n].Visible = true;

            _dgv.MultiSelect = false;
            _dgv.ReadOnly = true;
            _dgv.Size = new System.Drawing.Size(252, 351);
            _dgv.ColumnHeadersHeight = 35;
            _dgv.RowHeadersVisible = false;
            _dgv.Font = new Font("맑은 고딕", 12, FontStyle.Bold);
            _dgv.ColumnHeadersDefaultCellStyle.Font = new Font("맑은 고딕", 12F, FontStyle.Bold);
            _dgv.RowsDefaultCellStyle.Font = new Font("굴림", 14F);
            _dgv.RowTemplate.Height = 33;
        }

        private void SetTabPage(DataGridView _dgvInspect, string _strtitle)
        {
            TabPage TabPage = new TabPage(_strtitle);
            TabPage.Font = new Font("맑은 고딕", 12, FontStyle.Bold);
            TabPage.Controls.Add(dgv);
            _dgvInspect.Location = new Point(2, 2);
            tabInspect.TabPages.Add(TabPage);
            //2023-04-28 헤더 사이즈 조절
            tabInspect.ItemSize = new Size(30, 30);
        }

        private void FillGridData(string SampleQty)
        {
            InitTabPage(SampleQty);
        }

        #endregion

        #region 수정시 데이터 가져오는 함수

        private void UFillGrid()
        {
            //위 텍스트 박스 정보
            clearAllValue();
            clearDgvTab();

            try
            {
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Clear();
              
                sqlParameter.Add("StuffinID", u_StuffinID); //StuffinID
                sqlParameter.Add("LotID", u_LotID);         //LotID
                sqlParameter.Add("ArticleID", u_ArticleID);         //LotID

                DataSet ds = DataStore.Instance.ProcedureToDataSet("xp_WizWork_sStuffIN_U", sqlParameter, true);

                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        DataRowCollection drc = dt.Rows;

                        foreach (DataRow dr in drc)
                        {
                            mtb_SDate.Text = dr["StuffDate"].ToString();//입고일자                
                            
                            //txtSGbn.Text = dr["StuffClssName"].ToString();//입고구분
                            //txtSGbnTag.Text = dr["StuffClss"].ToString();//입고구분ID

                            //입고구분
                            index = cboSGbn.FindString(dr["StuffClssName"].ToString());

                            if (index != -1)
                            {
                                cboSGbn.SelectedIndex = index;
                            }
                            else
                            {
                                cboSGbn.SelectedIndex = 0;
                            }

                            txtCustom.Text = dr["CustomName"].ToString();//거래처
                            txtCustomTag.Text = dr["CustomID"].ToString();//거래처ID
                            txtOrderNum.Text = dr["REQ_ID"].ToString();//발주번호
                            txtArticle.Text = dr["Article"].ToString();//품명
                            txtArticleTag.Text = dr["ArticleID"].ToString();//ArticleID
                            txtSCustom.Text = dr["Custom"].ToString();//입고처명
                            txtSCustomTag.Text = dr["BuyCustomID"].ToString();//입고처ID

                            //txtArticleGbn.Text = dr["ArticleGrp"].ToString();//품명그룹

                            //품명그룹
                            index = cboArticleGbn.FindString(dr["ArticleGrp"].ToString());

                            if (index != -1)
                            {
                                cboArticleGbn.SelectedIndex = index;
                            }
                            else
                            {
                                cboArticleGbn.SelectedIndex = 0;
                            }

                            //txtVAT.Text = dr["Vat_Ind_YN"].ToString();//부가세별도

                            //부가세별도
                            index = cboVAT.FindString(dr["Vat_Ind_YN"].ToString());

                            if (index != -1)
                            {
                                cboVAT.SelectedIndex = index;
                            }
                            else
                            {
                                cboVAT.SelectedIndex = 0;
                            }

                            //txtUnitClss.Text = dr["UnitClssName"].ToString();//입고단위
                            //txtUnitClssTag.Text = dr["UnitClss"].ToString();//입고단위ID

                            //입고단위
                            index = cboUnitClss.FindString(dr["UnitClssName"].ToString());

                            if (index != -1)
                            {
                                cboUnitClss.SelectedIndex = index;
                            }
                            else
                            {
                                cboUnitClss.SelectedIndex = 0;
                            }

                            //txtMoneyClss.Text = dr["PriceClssName"].ToString(); ;//화폐단위
                            //txtMoneyClssTag.Text = dr["PriceClss"].ToString(); ;//화폐단위ID

                            //화폐단위
                            index = cboMoneyClss.FindString(dr["PriceClssName"].ToString());

                            if (index != -1)
                            {
                                cboMoneyClss.SelectedIndex = index;
                            }
                            else
                            {
                                cboMoneyClss.SelectedIndex = 0;
                            }

                            //txtLoc.Text = dr["ToLocName"].ToString();//후창고
                            //txtLocTag.Text = dr["ToLocID"].ToString();//후창고ID

                            //후창고
                            index = cboLoc.FindString(dr["ToLocName"].ToString());

                            if (index != -1)
                            {
                                cboLoc.SelectedIndex = index;
                            }
                            else
                            {
                                cboLoc.SelectedIndex = 0;
                            }

                            txtSQty.Text = dr["StuffQty"].ToString();//입고수량
                            txtmtrWeightPerBonsu.Text = dr["mtrWeightPerBonsu"].ToString();//본딩중량
                            txtmtrWeight.Text = dr["mtrWeight"].ToString(); //중량
                            txtLotNo.Text = dr["LOTID"].ToString();     //로트NO
                            txtSLotNo.Text = dr["mtrCustomLotno"].ToString();    //입고처로트번호
                            txtSPerson.Text = dr["mtrCustomInspectPerson"].ToString(); //입고처 검수자

                            //입고자
                            index = cboStuffinPerson.FindString(dr["StuffinPerson"].ToString());
                            if (index != -1)
                            {
                                cboStuffinPerson.SelectedIndex = index;
                            }
                            else
                            {
                                cboStuffinPerson.SelectedIndex = 0;
                            }


                            //txtInspectYN.Text = dr["Vat_Ind_YN"].ToString(); //검사필요여부

                            //txtInspectPerson.Text = dr["Inspector1"].ToString();//검수자
                            //txtInspectPersonTag.Text = dr["CreateUserID"].ToString();//검수자ID

                            //검수자
                            index = cboInspectPerson.FindString(dr["Inspector1"].ToString());

                            if (index != -1)
                            {
                                cboInspectPerson.SelectedIndex = index;
                            }
                            else
                            {
                                cboInspectPerson.SelectedIndex = 0;
                            }

                            mtb_IDate.Text = dr["InspectDate"].ToString();        //검수일자
                        }

                    }
                }

                //검사기준
                if (ArticleInspectBasis(u_ArticleID))
                {
                    ArticleInspectBasisSub(m_InspectBasisID);
                    //검사한 이력 있으면 보여주기
                    ArticleInspect();
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

        //검사 이력이 있는 경우 보여주기
        private void ArticleInspect()
        {
            Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
            sqlParameter.Clear();

            sqlParameter.Add("LotID", u_LotID);         //LotID
            sqlParameter.Add("InspectID", u_InspectID); //검사ID

            DataSet ds = DataStore.Instance.ProcedureToDataSet("xp_WizWork_sInspectAutoMtr", sqlParameter, true);
            DataTable dt = ds.Tables[0];

            if (dt.Rows.Count > 0)
            {
                DataRowCollection drc = dt.Rows;

                foreach (DataRow dr in drc)
                {
                    for(int i = 0; i < dgvInspect.Rows.Count; i++)
                    {
                        //검사기준과 맞는 검사 결과를 찾고, 탭에 순서대로 입력, 합불 추가
                        if(dgvInspect.Rows[i].Cells["SubSeq"].Value.ToString() == dr["SubSeq"].ToString() && dgvInspect.Rows[i].Cells["insType"].Value.ToString() == dr["insType"].ToString())
                        {
                            for (int x = 0; x < tabInspect.TabPages.Count; x++)
                            {
                                if(tabInspect.TabPages[x].Text.ToString() == (i + 1).ToString())
                                {
                                    //탭 안에 그리드 찾기
                                    DataGridView UDataGridVeiw = tabInspect.TabPages[i].Controls[0] as DataGridView;

                                    for (int y = 0; y < UDataGridVeiw.Rows.Count; y++)
                                    {
                                        if(UDataGridVeiw.Rows[y].Cells["InspectValueText"].Value.ToString() == "")
                                        {
                                            if (dr["insType"].ToString().Trim() == "1")
                                            {
                                                UDataGridVeiw.Rows[y].Cells["InspectValueText"].Value = dr["InspectText"].ToString();
                                                break;
                                            }
                                            else
                                            {
                                                UDataGridVeiw.Rows[y].Cells["InspectValueText"].Value = dr["InspectValue"].ToString();
                                                break;
                                            }                                                                                  
                                        }


                                    }                                                             
                                }

                                //하나라도 불량이면 불량
                                if (dr["DefectYN"].ToString() == "Y" && (dgvInspect.Rows[i].Cells["DefectYN"].Value.ToString() == "합" || dgvInspect.Rows[i].Cells["DefectYN"].Value.ToString() == ""))
                                {
                                    dgvInspect.Rows[i].Cells["DefectYN"].Value = "불";
                                }
                                else if (dr["DefectYN"].ToString() == "N" && dgvInspect.Rows[i].Cells["DefectYN"].Value.ToString() == "")
                                {
                                    dgvInspect.Rows[i].Cells["DefectYN"].Value = "합";
                                }
                            }
                        }
                    }
                }
            }
        }

        #endregion

        #region 콤보박스 설정

        private void SetComboBox()
        {
            SetcboStuffinPerson();
            SetcboInspectPerson();
            SetcboLoc();
            SetcboSGbn();
            SetcboArticleGbn();
            SetcboUnitClss();
            SetcboVAT();
            SetcboMoneyClss();
            SetcboInspectYN();
        }

        //입고자
        private void SetcboStuffinPerson()
        {
            cboStuffinPerson.Items.Clear();
            //입고자
            ds = DataStore.Instance.ProcedureToDataSet("[xp_WizWork_SStuffinPerson]", null, false);
            DataRow newRow = ds.Tables[0].NewRow();
            newRow["PersonID"] = "*";
            newRow["Name"] = "전체";
            ds.Tables[0].Rows.InsertAt(newRow, 0);
            cboStuffinPerson.DataSource = ds.Tables[0];
            cboStuffinPerson.ValueMember = "PersonID";
            cboStuffinPerson.DisplayMember = "Name";

        }

        //검수자
        private void SetcboInspectPerson()
        {
            cboInspectPerson.Items.Clear();
            //검수자
            ds = DataStore.Instance.ProcedureToDataSet("[xp_WizWork_SInspectPerson]", null, false);
            DataRow newRow = ds.Tables[0].NewRow();
            newRow["PersonID"] = "*";
            newRow["Name"] = "전체";
            ds.Tables[0].Rows.InsertAt(newRow, 0);
            cboInspectPerson.DataSource = ds.Tables[0];
            cboInspectPerson.ValueMember = "PersonID";
            cboInspectPerson.DisplayMember = "Name";

        }

        //후창고
        private void SetcboLoc()
        {
            cboLoc.Items.Clear();
            //후창고
            ds = DataStore.Instance.ProcedureToDataSet("[xp_WizWork_SLoc]", null, false);
            DataRow newRow = ds.Tables[0].NewRow();
            newRow["CodeID"] = "*";
            newRow["CodeName"] = "전체";
            ds.Tables[0].Rows.InsertAt(newRow, 0);
            cboLoc.DataSource = ds.Tables[0];
            cboLoc.ValueMember = "CodeID";
            cboLoc.DisplayMember = "CodeName";

        }

        //입고구분
        private void SetcboSGbn()
        {
            cboSGbn.Items.Clear();
            //입고구분
            ds = DataStore.Instance.ProcedureToDataSet("[xp_WizWork_SSGbn]", null, false);
            DataRow newRow = ds.Tables[0].NewRow();
            newRow["CodeID"] = "*";
            newRow["CodeName"] = "전체";
            ds.Tables[0].Rows.InsertAt(newRow, 0);
            cboSGbn.DataSource = ds.Tables[0];
            cboSGbn.ValueMember = "CodeID";
            cboSGbn.DisplayMember = "CodeName";

        }

        //품명그룹
        private void SetcboArticleGbn()
        {
            cboArticleGbn.Items.Clear();
            //품명그룹
            ds = DataStore.Instance.ProcedureToDataSet("[xp_WizWork_SArticleGbn]", null, false);
            DataRow newRow = ds.Tables[0].NewRow();
            newRow["ArticleGrpID"] = "*";
            newRow["ArticleGrp"] = "전체";
            ds.Tables[0].Rows.InsertAt(newRow, 0);
            cboArticleGbn.DataSource = ds.Tables[0];
            cboArticleGbn.ValueMember = "ArticleGrpID";
            cboArticleGbn.DisplayMember = "ArticleGrp";

        }

        //입고단위
        private void SetcboUnitClss()
        {
            cboUnitClss.Items.Clear();
            //입고단위
            ds = DataStore.Instance.ProcedureToDataSet("[xp_WizWork_SUnitClss]", null, false);
            DataRow newRow = ds.Tables[0].NewRow();
            newRow["CodeID"] = "*";
            newRow["CodeName"] = "전체";
            ds.Tables[0].Rows.InsertAt(newRow, 0);
            cboUnitClss.DataSource = ds.Tables[0];
            cboUnitClss.ValueMember = "CodeID";
            cboUnitClss.DisplayMember = "CodeName";

        }

        //부가세별도
        private void SetcboVAT()
        {
            cboVAT.Items.Clear();
            //부가세별도
            ds = DataStore.Instance.ProcedureToDataSet("[xp_WizWork_SVAT]", null, false);
            DataRow newRow = ds.Tables[0].NewRow();
            newRow["CodeID"] = "*";
            newRow["CodeName"] = "전체";
            ds.Tables[0].Rows.InsertAt(newRow, 0);
            cboVAT.DataSource = ds.Tables[0];
            cboVAT.ValueMember = "CodeID";
            cboVAT.DisplayMember = "CodeName";

        }

        //화폐단위
        private void SetcboMoneyClss()
        {
            cboMoneyClss.Items.Clear();
            //화폐단위
            ds = DataStore.Instance.ProcedureToDataSet("[xp_WizWork_SMoneyClss]", null, false);
            DataRow newRow = ds.Tables[0].NewRow();
            newRow["CodeID"] = "*";
            newRow["CodeName"] = "전체";
            ds.Tables[0].Rows.InsertAt(newRow, 0);
            cboMoneyClss.DataSource = ds.Tables[0];
            cboMoneyClss.ValueMember = "CodeID";
            cboMoneyClss.DisplayMember = "CodeName";

        }

        //검사필요여부
        private void SetcboInspectYN()
        {
            cboInspectYN.Items.Clear();
            //검사필요여부
            ds = DataStore.Instance.ProcedureToDataSet("[xp_WizWork_SInspectYN]", null, false);
            DataRow newRow = ds.Tables[0].NewRow();
            newRow["CodeID"] = "*";
            newRow["CodeName"] = "전체";
            ds.Tables[0].Rows.InsertAt(newRow, 0);
            cboInspectYN.DataSource = ds.Tables[0];
            cboInspectYN.ValueMember = "CodeID";
            cboInspectYN.DisplayMember = "CodeName";

            //SetComboBoxChanged = "1"; //2024-01-29
        }




        #endregion

        #region 클릭이벤트(팝업창)

        //입고일 
        private void btnSDate_Click(object sender, EventArgs e)
        {
            WizCommon.Popup.Frm_TLP_Calendar calendar = new WizCommon.Popup.Frm_TLP_Calendar(mtb_SDate.Text.Replace("-", ""), mtb_SDate.Name);
            calendar.WriteDateTextEvent += new WizCommon.Popup.Frm_TLP_Calendar.TextEventHandler(GetDate);
            calendar.Owner = this;
            calendar.ShowDialog();
        }

        private void mtb_SDate_Click(object sender, EventArgs e)
        {
            WizCommon.Popup.Frm_TLP_Calendar calendar = new WizCommon.Popup.Frm_TLP_Calendar(mtb_SDate.Text.Replace("-", ""), mtb_SDate.Name);
            calendar.WriteDateTextEvent += new WizCommon.Popup.Frm_TLP_Calendar.TextEventHandler(GetDate);
            calendar.Owner = this;
            calendar.ShowDialog();
        }

        private void btnCal_SDate_Click(object sender, EventArgs e)
        {
            WizCommon.Popup.Frm_TLP_Calendar calendar = new WizCommon.Popup.Frm_TLP_Calendar(mtb_SDate.Text.Replace("-", ""), mtb_SDate.Name);
            calendar.WriteDateTextEvent += new WizCommon.Popup.Frm_TLP_Calendar.TextEventHandler(GetDate);
            calendar.Owner = this;
            calendar.ShowDialog();
        }

        //  Calendar.Value -> mtbBox.Text 달력창으로부터 텍스트로 값을 옮겨주는 메소드
        private void GetDate(string strDate, string btnName)
        {
            DateTime dateTime = new DateTime();
            dateTime = DateTime.ParseExact(strDate, "yyyyMMdd", null);
            mtb_SDate.Text = dateTime.ToString("yyyy-MM-dd");
        }

        private void txtSGbn_Click(object sender, EventArgs e)
        {
            Frm_PopUpSel_Common FPSC = new Frm_PopUpSel_Common("ICD", txtSGbn.Text, "", "S");
            FPSC.StartPosition = FormStartPosition.CenterScreen;
            FPSC.BringToFront();
            FPSC.TopMost = false;
            FPSC.SWriteTextEvent += PopUp_WriteTextEvent;
            FPSC.ShowDialog();

            void PopUp_WriteTextEvent(string CodeID, string CodeName, string UnitClss, string UnitClssID, string PriceClss, string PriceClssID, string ArticleGrp, string OK)
            {
                if (OK == "Cancel")
                { return; }
                else
                {
                    txtSGbnTag.Text = CodeID;
                    txtSGbn.Text = CodeName;
                }
            }
        }

        //거래처
        private void btnCustom_Click(object sender, EventArgs e)
        {
            
            Frm_PopUpSel_Common FPSC = new Frm_PopUpSel_Common("MC", txtCustom.Text,"", "S");
            FPSC.StartPosition = FormStartPosition.CenterScreen;
            FPSC.BringToFront();
            FPSC.TopMost = false;
            FPSC.SWriteTextEvent += PopUp_WriteTextEvent;
            FPSC.ShowDialog();

            void PopUp_WriteTextEvent(string CodeID, string CodeName, string UnitClss, string UnitClssID, string PriceClss, string PriceClssID, string ArticleGrp, string OK)
            {
                if (OK == "Cancel")
                { return; }
                else
                {
                    txtCustomTag.Text = CodeID;
                    txtCustom.Text = CodeName;
                    txtSCustomTag.Text = CodeID;
                    txtSCustom.Text = CodeName;
                }
            }
        }

        private void txtCustom_Click(object sender, EventArgs e)
        {
            Frm_PopUpSel_Common FPSC = new Frm_PopUpSel_Common("MC", txtCustom.Text, "", "S");
            FPSC.StartPosition = FormStartPosition.CenterScreen;
            FPSC.BringToFront();
            FPSC.TopMost = false;
            FPSC.SWriteTextEvent += PopUp_WriteTextEvent;
            FPSC.ShowDialog();

            void PopUp_WriteTextEvent(string CodeID, string CodeName, string UnitClss, string UnitClssID, string PriceClss, string PriceClssID, string ArticleGrp, string OK)
            {
                if (OK == "Cancel")
                { return; }
                else
                {
                    txtCustomTag.Text = CodeID;
                    txtCustom.Text = CodeName;
                    txtSCustomTag.Text = CodeID;
                    txtSCustom.Text = CodeName;
                }
            }
        }

        //품명
        private void btnArticle_Click(object sender, EventArgs e)
        {
            Frm_PopUpSel_Common FPSC = new Frm_PopUpSel_Common("MA", txtArticle.Text, txtCustomTag.Text, "S");
            FPSC.StartPosition = FormStartPosition.CenterScreen;
            FPSC.BringToFront();
            FPSC.TopMost = false;
            FPSC.SWriteTextEvent += PopUp_WriteTextEvent;
            FPSC.ShowDialog();

            void PopUp_WriteTextEvent(string CodeID, string CodeName, string UnitClss, string UnitClssID, string PriceClss, string PriceClssID, string ArticleGrp, string OK)
            {
                if (OK == "Cancel")
                {
                    return;
                }
                else
                {
                    //int index = 0;
                    txtArticle.Text = CodeName;
                    txtArticleTag.Text = CodeID;

                    //품명그룹
                    index = cboArticleGbn.FindString(ArticleGrp);

                    if (index != -1)
                    {
                        cboArticleGbn.SelectedIndex = index;
                    }
                    else
                    {
                        cboArticleGbn.SelectedIndex = 0;
                    }

                    //단위
                    index = cboUnitClss.FindString(UnitClss);

                    if (index != -1)
                    {
                        cboUnitClss.SelectedIndex = index;
                    }
                    else
                    {
                        cboUnitClss.SelectedIndex = 0;
                    }

                    //화폐단위
                    index = cboMoneyClss.FindString(PriceClss);

                    if (index != -1)
                    {
                        cboMoneyClss.SelectedIndex = index;
                    }
                    else
                    {
                        cboMoneyClss.SelectedIndex = 0;
                    }


                    //후창고      
                    index = cboLoc.FindString("사내창고");

                    if (index != -1)
                    {
                        cboLoc.SelectedIndex = index;
                    }
                    else
                    {
                        cboLoc.SelectedIndex = 0;
                    }

                    //입고구분    
                    index = cboSGbn.FindString("자재입고");

                    if (index != -1)
                    {
                        cboSGbn.SelectedIndex = index;
                    }
                    else
                    {
                        cboSGbn.SelectedIndex = 0;
                    }

                    //부가세 별도
                    index = cboVAT.FindString("Y");

                    if (index != -1)
                    {
                        cboVAT.SelectedIndex = index;
                    }
                    else
                    {
                        cboVAT.SelectedIndex = 0;
                    }

                    //txtArticleGbn.Text = ArticleGrp;

                    //txtUnitClss.Text = UnitClss;

                    //txtUnitClssTag.Text = UnitClssID;

                    //txtMoneyClss.Text = PriceClss;

                    //txtMoneyClssTag.Text = PriceClssID;

                    //검사기준
                    if (ArticleInspectBasis(txtArticleTag.Text.ToString()))
                    {
                        ArticleInspectBasisSub(m_InspectBasisID);
                    }
                }
            }
        }

        private void txtArticle_Click(object sender, EventArgs e)
        {
            Frm_PopUpSel_Common FPSC = new Frm_PopUpSel_Common("MA", txtArticle.Text, txtCustomTag.Text, "S");
            FPSC.StartPosition = FormStartPosition.CenterScreen;
            FPSC.BringToFront();
            FPSC.TopMost = false;
            FPSC.SWriteTextEvent += PopUp_WriteTextEvent;
            FPSC.ShowDialog();

            void PopUp_WriteTextEvent(string CodeID, string CodeName, string UnitClss, string UnitClssID, string PriceClss, string PriceClssID, string ArticleGrp, string OK)
            {
                if (OK == "Cancel")
                {
                    return;
                }
                else
                {
                    //int index = 0;
                    txtArticle.Text = CodeName;
                    txtArticleTag.Text = CodeID;

                    //품명그룹
                    index = cboArticleGbn.FindString(ArticleGrp);

                    if (index != -1)
                    {
                        cboArticleGbn.SelectedIndex = index;
                    }
                    else
                    {
                        cboArticleGbn.SelectedIndex = 0;
                    }

                    //단위
                    index = cboUnitClss.FindString(UnitClss);

                    if (index != -1)
                    {
                        cboUnitClss.SelectedIndex = index;
                    }
                    else
                    {
                        cboUnitClss.SelectedIndex = 0;
                    }

                    //화폐단위
                    index = cboMoneyClss.FindString(PriceClss);

                    if (index != -1)
                    {
                        cboMoneyClss.SelectedIndex = index;
                    }
                    else
                    {
                        cboMoneyClss.SelectedIndex = 0;
                    }


                    //후창고      
                    index = cboLoc.FindString("사내창고");

                    if (index != -1)
                    {
                        cboLoc.SelectedIndex = index;
                    }
                    else
                    {
                        cboLoc.SelectedIndex = 0;
                    }

                    //입고구분    
                    index = cboSGbn.FindString("자재입고");

                    if (index != -1)
                    {
                        cboSGbn.SelectedIndex = index;
                    }
                    else
                    {
                        cboSGbn.SelectedIndex = 0;
                    }

                    //부가세 별도
                    index = cboVAT.FindString("Y");

                    if (index != -1)
                    {
                        cboVAT.SelectedIndex = index;
                    }
                    else
                    {
                        cboVAT.SelectedIndex = 0;
                    }

                    //txtArticleGbn.Text = ArticleGrp;
                    //txtUnitClss.Text = UnitClss;
                    //txtUnitClssTag.Text = UnitClssID;
                    //txtMoneyClss.Text = PriceClss;
                    //txtMoneyClssTag.Text = PriceClssID;

                    //검사기준
                    if (ArticleInspectBasis(txtArticleTag.Text.ToString()))
                    {
                        ArticleInspectBasisSub(m_InspectBasisID);
                    }
                }
            }
        }

        //발주번호
        private void btnOrderNum_Click(object sender, EventArgs e)
        {
            Frm_PopUpSel_Common FPSC = new Frm_PopUpSel_Common("REQ", txtOrderNum.Text, "", "S");
            FPSC.StartPosition = FormStartPosition.CenterScreen;
            FPSC.BringToFront();
            FPSC.TopMost = false;
            FPSC.SWriteTextEvent += PopUp_WriteTextEvent;
            FPSC.ShowDialog();

            void PopUp_WriteTextEvent(string CodeID, string CodeName, string UnitClss, string UnitClssID, string PriceClss, string PriceClssID, string ArticleGrp, string OK)
            {
                if (OK == "Cancel")
                {
                    return;
                }
                else
                {
                    txtOrderNum.Text = CodeName;
                }
            }
        }

        private void txtOrderNum_Click(object sender, EventArgs e)
        {
            Frm_PopUpSel_Common FPSC = new Frm_PopUpSel_Common("REQ", txtOrderNum.Text, "", "S");
            FPSC.StartPosition = FormStartPosition.CenterScreen;
            FPSC.BringToFront();
            FPSC.TopMost = false;
            FPSC.SWriteTextEvent += PopUp_WriteTextEvent;
            FPSC.ShowDialog();

            void PopUp_WriteTextEvent(string CodeID, string CodeName, string UnitClss, string UnitClssID, string PriceClss, string PriceClssID, string ArticleGrp, string OK)
            {
                if (OK == "Cancel")
                { 
                    return; 
                }
                else
                {
                    txtOrderNum.Text = CodeName;
                }
            }
        }

        //입고처명
        private void btnSCustom_Click(object sender, EventArgs e)
        {
            Frm_PopUpSel_Common FPSC = new Frm_PopUpSel_Common("MC", txtSCustom.Text, "", "S");
            FPSC.StartPosition = FormStartPosition.CenterScreen;
            FPSC.BringToFront();
            FPSC.TopMost = false;
            FPSC.SWriteTextEvent += PopUp_WriteTextEvent;
            FPSC.ShowDialog();

            void PopUp_WriteTextEvent(string CodeID, string CodeName, string UnitClss, string UnitClssID, string PriceClss, string PriceClssID, string ArticleGrp, string OK)
            {
                if (OK == "Cancel")
                { return; }
                else
                {
                    txtSCustomTag.Text = CodeID;
                    txtSCustom.Text = CodeName;
                }
            }
        }

        private void txtSCustom_Click(object sender, EventArgs e)
        {
            Frm_PopUpSel_Common FPSC = new Frm_PopUpSel_Common("MC", txtSCustom.Text, "", "S");
            FPSC.StartPosition = FormStartPosition.CenterScreen;
            FPSC.BringToFront();
            FPSC.TopMost = false;
            FPSC.SWriteTextEvent += PopUp_WriteTextEvent;
            FPSC.ShowDialog();

            void PopUp_WriteTextEvent(string CodeID, string CodeName, string UnitClss, string UnitClssID, string PriceClss, string PriceClssID, string ArticleGrp, string OK)
            {
                if (OK == "Cancel")
                { return; }
                else
                {
                    txtSCustomTag.Text = CodeID;
                    txtSCustom.Text = CodeName;
                }
            }
        }

        //부가세
        private void txtVAT_Click(object sender, EventArgs e)
        {
            Frm_PopUpSel_Common FPSC = new Frm_PopUpSel_Common("VAT", "", "", "S");
            FPSC.StartPosition = FormStartPosition.CenterScreen;
            FPSC.BringToFront();
            FPSC.TopMost = false;
            FPSC.SWriteTextEvent += PopUp_WriteTextEvent;
            FPSC.ShowDialog();

            void PopUp_WriteTextEvent(string CodeID, string CodeName, string UnitClss, string UnitClssID, string PriceClss, string PriceClssID, string ArticleGrp, string OK)
            {
                if (OK == "Cancel")
                {
                    return;
                }
                else
                {
                    txtVAT.Text = CodeName;
                }
            }
        }

        //후창고
        private void txtLoc_Click(object sender, EventArgs e)
        {
            Frm_PopUpSel_Common FPSC = new Frm_PopUpSel_Common("LOC", "", "", "S");
            FPSC.StartPosition = FormStartPosition.CenterScreen;
            FPSC.BringToFront();
            FPSC.TopMost = false;
            FPSC.SWriteTextEvent += PopUp_WriteTextEvent;
            FPSC.ShowDialog();

            void PopUp_WriteTextEvent(string CodeID, string CodeName, string UnitClss, string UnitClssID, string PriceClss, string PriceClssID, string ArticleGrp, string OK)
            {
                if (OK == "Cancel")
                { return; }
                else
                {
                    txtLocTag.Text = CodeID;
                    txtLoc.Text = CodeName;
                }
            }
        }

        //입고수량
        private void btnSQty_Click(object sender, EventArgs e)
        {
            PopUp.Frm_CMNumericKeypad keypad = new PopUp.Frm_CMNumericKeypad("수량입력", "수량");

            keypad.Owner = this;
            if (keypad.ShowDialog() == DialogResult.OK)
            {
                txtSQty.Text = keypad.tbInputText.Text;
                if (txtSQty.Text == "" || Convert.ToDouble(txtSQty.Text) == 0)
                {
                    txtSQty.Text = "0";
                }
            }
        }

        private void txtSQty_Click(object sender, EventArgs e)
        {
            PopUp.Frm_CMNumericKeypad keypad = new PopUp.Frm_CMNumericKeypad("수량입력", "수량");

            keypad.Owner = this;
            if (keypad.ShowDialog() == DialogResult.OK)
            {
                txtSQty.Text = keypad.tbInputText.Text;
                if (txtSQty.Text == "" || Convert.ToDouble(txtSQty.Text) == 0)
                {
                    txtSQty.Text = "0";
                }
            }
        }

        //본딩중량
        private void btnmtrWeightPerBonsu_Click(object sender, EventArgs e)
        {
            PopUp.Frm_CMNumericKeypad keypad = new PopUp.Frm_CMNumericKeypad("중량입력", "중량");

            keypad.Owner = this;
            if (keypad.ShowDialog() == DialogResult.OK)
            {
                txtmtrWeightPerBonsu.Text = keypad.tbInputText.Text;
                if (txtmtrWeightPerBonsu.Text == "" || Convert.ToDouble(txtmtrWeightPerBonsu.Text) == 0)
                {
                    txtmtrWeightPerBonsu.Text = "0";
                }
            }
        }

        private void txtmtrWeightPerBonsu_Click(object sender, EventArgs e)
        {
            PopUp.Frm_CMNumericKeypad keypad = new PopUp.Frm_CMNumericKeypad("중량입력", "중량");

            keypad.Owner = this;
            if (keypad.ShowDialog() == DialogResult.OK)
            {
                txtmtrWeightPerBonsu.Text = keypad.tbInputText.Text;
                if (txtmtrWeightPerBonsu.Text == "" || Convert.ToDouble(txtmtrWeightPerBonsu.Text) == 0)
                {
                    txtmtrWeightPerBonsu.Text = "0";
                }
            }
        }

        //중량
        private void btnmtrWeight_Click(object sender, EventArgs e)
        {
            PopUp.Frm_CMNumericKeypad keypad = new PopUp.Frm_CMNumericKeypad("중량입력", "중량");

            keypad.Owner = this;
            if (keypad.ShowDialog() == DialogResult.OK)
            {
                txtmtrWeight.Text = keypad.tbInputText.Text;
                if (txtmtrWeight.Text == "" || Convert.ToDouble(txtmtrWeight.Text) == 0)
                {
                    txtmtrWeight.Text = "0";
                }
            }
        }

        private void txtmtrWeight_Click(object sender, EventArgs e)
        {
            PopUp.Frm_CMNumericKeypad keypad = new PopUp.Frm_CMNumericKeypad("중량입력", "중량");

            keypad.Owner = this;
            if (keypad.ShowDialog() == DialogResult.OK)
            {
                txtmtrWeight.Text = keypad.tbInputText.Text;
                if (txtmtrWeight.Text == "" || Convert.ToDouble(txtmtrWeight.Text) == 0)
                {
                    txtmtrWeight.Text = "0";
                }
            }
        }

        //입고처로트번호
        private void btnSLotNo_Click(object sender, EventArgs e)
        {
            var path64 = System.IO.Path.Combine(Directory.GetDirectories(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "winsxs"), "amd64_microsoft-windows-osk_*")[0], "osk.exe");
            var path32 = @"C:\windows\system32\osk.exe";
            var path = (Environment.Is64BitOperatingSystem) ? path64 : path32;
            if (File.Exists(path) && !Frm_tinout_Main.Lib.ReturnKillRunningProcess("osk"))
            {
                System.Diagnostics.Process.Start(path);

                txtSLotNo.Focus();

            }
        }

        private void txtSLotNo_Click(object sender, EventArgs e)
        {
            var path64 = System.IO.Path.Combine(Directory.GetDirectories(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "winsxs"), "amd64_microsoft-windows-osk_*")[0], "osk.exe");
            var path32 = @"C:\windows\system32\osk.exe";
            var path = (Environment.Is64BitOperatingSystem) ? path64 : path32;
            if (File.Exists(path) && !Frm_tinout_Main.Lib.ReturnKillRunningProcess("osk"))
            {
                System.Diagnostics.Process.Start(path);

                txtSLotNo.Focus();

            }
        }

        //입고처검사자
        private void btnSPerson_Click(object sender, EventArgs e)
        {
            var path64 = System.IO.Path.Combine(Directory.GetDirectories(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "winsxs"), "amd64_microsoft-windows-osk_*")[0], "osk.exe");
            var path32 = @"C:\windows\system32\osk.exe";
            var path = (Environment.Is64BitOperatingSystem) ? path64 : path32;
            if (File.Exists(path) && !Frm_tinout_Main.Lib.ReturnKillRunningProcess("osk"))
            {
                System.Diagnostics.Process.Start(path);

                txtSPerson.Focus();

            }
        }

        private void txtSPerson_Click(object sender, EventArgs e)
        {
            var path64 = System.IO.Path.Combine(Directory.GetDirectories(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "winsxs"), "amd64_microsoft-windows-osk_*")[0], "osk.exe");
            var path32 = @"C:\windows\system32\osk.exe";
            var path = (Environment.Is64BitOperatingSystem) ? path64 : path32;
            if (File.Exists(path) && !Frm_tinout_Main.Lib.ReturnKillRunningProcess("osk"))
            {
                System.Diagnostics.Process.Start(path);

                txtSPerson.Focus();

            }
        }

        //검사 여부
        private void txtInspectYN_Click(object sender, EventArgs e)
        {
            Frm_PopUpSel_Common FPSC = new Frm_PopUpSel_Common("IYN", "", "", "S");
            FPSC.StartPosition = FormStartPosition.CenterScreen;
            FPSC.BringToFront();
            FPSC.TopMost = false;
            FPSC.SWriteTextEvent += PopUp_WriteTextEvent;
            FPSC.ShowDialog();

            void PopUp_WriteTextEvent(string CodeID, string CodeName, string UnitClss, string UnitClssID, string PriceClss, string PriceClssID, string ArticleGrp, string OK)
            {
                if (OK == "Cancel")
                {
                    return;
                }
                else
                {
                    txtInspectYN.Text = CodeName;

                    //if (txtInspectYN.Text.ToString() == "Y")
                    //{
                    //    btnInspectPerson.Text = "입고처\r\n검사자";
                    //    btnInspectDate.Text = "입고처\r\n검사일자";
                    //}
                    //else
                    //{
                    //    btnInspectPerson.Text = "검수자";
                    //    btnInspectDate.Text = "검수일자";
                    //}
                }
            }
        }

        //검수자
        private void txtInspectPerson_Click(object sender, EventArgs e)
        {
            Frm_PopUpSel_Common FPSC = new Frm_PopUpSel_Common("InP", "", "", "S");
            FPSC.StartPosition = FormStartPosition.CenterScreen;
            FPSC.BringToFront();
            FPSC.TopMost = false;
            FPSC.SWriteTextEvent += PopUp_WriteTextEvent;
            FPSC.ShowDialog();

            void PopUp_WriteTextEvent(string CodeID, string CodeName, string UnitClss, string UnitClssID, string PriceClss, string PriceClssID, string ArticleGrp, string OK)
            {
                if (OK == "Cancel")
                { return; }
                else
                {
                    txtInspectPersonTag.Text = CodeID;
                    txtInspectPerson.Text = CodeName;
                }
            }
        }

        //검수일자
        private void btnInspectDate_Click(object sender, EventArgs e)
        {
            WizCommon.Popup.Frm_TLP_Calendar calendar = new WizCommon.Popup.Frm_TLP_Calendar(mtb_IDate.Text.Replace("-", ""), mtb_IDate.Name);
            calendar.WriteDateTextEvent += new WizCommon.Popup.Frm_TLP_Calendar.TextEventHandler(GetIDate);
            calendar.Owner = this;
            calendar.ShowDialog();
        }

        private void mtb_IDate_Click(object sender, EventArgs e)
        {
            WizCommon.Popup.Frm_TLP_Calendar calendar = new WizCommon.Popup.Frm_TLP_Calendar(mtb_IDate.Text.Replace("-", ""), mtb_IDate.Name);
            calendar.WriteDateTextEvent += new WizCommon.Popup.Frm_TLP_Calendar.TextEventHandler(GetIDate);
            calendar.Owner = this;
            calendar.ShowDialog();
        }

        private void btnCal_IDate_Click(object sender, EventArgs e)
        {
            WizCommon.Popup.Frm_TLP_Calendar calendar = new WizCommon.Popup.Frm_TLP_Calendar(mtb_IDate.Text.Replace("-", ""), mtb_IDate.Name);
            calendar.WriteDateTextEvent += new WizCommon.Popup.Frm_TLP_Calendar.TextEventHandler(GetIDate);
            calendar.Owner = this;
            calendar.ShowDialog();
        }

        //  Calendar.Value -> mtbBox.Text 달력창으로부터 텍스트로 값을 옮겨주는 메소드
        private void GetIDate(string strDate, string btnName)
        {
            DateTime dateTime = new DateTime();
            dateTime = DateTime.ParseExact(strDate, "yyyyMMdd", null);
            mtb_IDate.Text = dateTime.ToString("yyyy-MM-dd");
        }

        #endregion

        #region 품명 입력시 검사 기준이 보여주는 함수

        //기준 조회
        private bool ArticleInspectBasis(string ArticleID)
        {
            //int index = 0;
            //검사기준 유무 확인
            Dictionary<string, object> sqlParameter = new Dictionary<string, object>();

            sqlParameter.Add("ArticleID", ArticleID); //ArticleID
            sqlParameter.Add("InspectPoint", "1");    //원자재 검사는 수입검사(1)로 처리
            DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_WizWork_sInspectAutoBasisMTR", sqlParameter, false);

            int i = 0;

            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];

                m_InspectBasisID = dr["InspectBasisID"].ToString();
                //기준이 있으면 Y 없으면 N

                index = cboInspectYN.FindString("Y");

                if (index != -1)
                {
                    cboInspectYN.SelectedIndex = index;
                }
                else
                {
                    cboInspectYN.SelectedIndex = 0;
                }

                //txtInspectYN.Text = "Y";
                return true;
            }
            else
            {
                tabInspect.TabPages.Clear();
                dgvInspect.Rows.Clear();

                InitTabPage();

                WizCommon.Popup.MyMessageBox.ShowBox("해당 품명의 검사기준이 없습니다. \r\n " +
                         "검사기준등록화면에서 해당 품목의 검사기준데이터를 확인해 주세요.", "[확인]", 0, 1);
                //기준이 있으면 Y 없으면 N

                index = cboInspectYN.FindString("N");

                if (index != -1)
                {
                    cboInspectYN.SelectedIndex = index;
                }
                else
                {
                    cboInspectYN.SelectedIndex = 0;
                }

                //txtInspectYN.Text = "N";

                return false;
            }

        }

        private void ArticleInspectBasisSub(string InspectBasisID)
        {
            tabInspect.TabPages.Clear();
            dgvInspect.Rows.Clear();

            //검사기준 유무 확인
            Dictionary<string, object> sqlParameter = new Dictionary<string, object>();

            sqlParameter.Add("InspectBasisID", InspectBasisID); //검사기준ID
            DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_WizWork_sInspectAutoBasisSubMTR", sqlParameter, false);

            int i = 1;

            foreach (DataRow dr in dt.Rows)
            {
                if (dr["insType"].ToString().Trim() == "1")
                {
                    dgvInspect.Rows.Add(i++,
                                        dr["insItemName"],
                                        dr["InsTPSpec"],
                                        dr["InsTPSpecMax"],
                                        dr["InsTPSpecMin"],
                                        dr["InsRASpecMax"],
                                        dr["InsRASpecMin"],
                                        dr["InsSampleQty"],
                                        dr["insType"],
                                        dr["SubSeq"],
                                        dr["InsTPSpecMin"],
                                        dr["InsTPSpecMax"],
                                        ""
                                        );
                }
                else
                {
                    dgvInspect.Rows.Add(i++,
                                        dr["insItemName"],
                                        dr["InsRASpec"],
                                        dr["InsTPSpecMax"],
                                        dr["InsTPSpecMin"],
                                        dr["InsRASpecMax"],
                                        dr["InsRASpecMin"],
                                        dr["InsSampleQty"],
                                        dr["insType"],
                                        dr["SubSeq"],
                                        dr["InsRASpecMin"],
                                        dr["InsRASpecMax"],
                                        ""
                                       );

                }

                FillGridData(dr["InsSampleQty"].ToString());

            }
        }

        #endregion

        #region 오른쪽 버튼 이벤트(저장, 수정, 닫기, 입력)

        //라벨 발행
        private void cmdprint_Click(object sender, EventArgs e)
        {
            //프린터 여부
            if (CheckData())
            {
                if (SaveData())
                {
                    LogData.LogSave(this.GetType().Name, "C"); //log 남기기 2022-10-24
                    PrintWorkCard();
                    clearAllValue();
                    clearDgvTab();
                    LogData.LogSave(this.GetType().Name, "P"); //log 남기기 2022-10-24

                    if (u_LotID != "") 
                    {
                        cmdclose_Click(null, null);
                    }

                    return;           
                }
            }
        }

        //저장
        private void cmdsave_Click(object sender, EventArgs e)
        {
            //프린터 여부
            if (CheckData())
            {
                if (SaveData())
                {
                    clearAllValue();
                    clearDgvTab();
                    WizCommon.Popup.MyMessageBox.ShowBox("저장이 완료되었습니다.", "[확인]", 0, 1);

                    LogData.LogSave(this.GetType().Name, "C"); //log 남기기(로드 S) 2022-10-24

                    if (u_LotID != "")
                    {
                        cmdclose_Click(null, null);
                    }

                    return;                    
                }
            }        
        }

        //수입검사 수정
        private void cmdInspectU_Click(object sender, EventArgs e)
        {
            DataGridView dgvtabInspectSelected = null;
            int i = 0;
            dgvtabInspectSelected = (DataGridView)tabInspect.SelectedTab.Controls[0];
            i = tabInspect.SelectedIndex + 1;
            if (dgvtabInspectSelected.Rows.Count > 0)
            {
                foreach (DataGridViewRow dgvr in dgvInspect.Rows)
                {
                    if (dgvr.Cells["No"].Value.ToString() == i.ToString())
                    {
                        dgvr.Cells["DefectYN"].Value = "";
                    }
                }
                foreach (DataGridViewRow row in dgvtabInspectSelected.Rows)
                {
                    row.Cells["InspectValueText"].Value = "";
                }
            }
        }

        //수입검사 초기화
        private void btnInspectR_Click(object sender, EventArgs e)
        {
            clearAllInsertedValue();
        }

        //전체 초기화
        private void btnAllR_Click(object sender, EventArgs e)
        {
            clearAllValue();
            clearAllInsertedValue();
        }

        //닫기
        private void cmdclose_Click(object sender, EventArgs e)
        {
            clearAllValue();
            clearAllInsertedValue();
            LogData.LogSave(this.GetType().Name, "S"); //log 남기기(로드 S) 2022-10-24
            this.Close();
        }

        //검사 입력
        private void btnInput_Click(object sender, EventArgs e)
        {
            if (dgvInspect.Rows.Count > 0) 
            {
                string strInsType = "";
                double duMinVal = 0;
                double duMaxVal = 0;
                string strSpec = "";

                int nCount = tabInspect.TabPages.Count;

                if (nCount > 0)
                {
                    for (int k = dgvInspect.SelectedRows[0].Index; k < dgvInspect.Rows.Count; k++)//TabPage 하나의 이벤트
                    {
                        tabInspect.TabPages[k].Select();                               //TabPage선택
                        tabInspect.SelectedIndex = k;
                        dgvInspect.Rows[k].Selected = true;

                        //2022-08-23 항목 8개이상이면 자동 스크롤 
                        if (k > 6)
                        {
                            dgvInspect.FirstDisplayedScrollingRowIndex = k; //2022-08-23 자동 스크롤
                        }

                        double douValue = 0;
                        DataGridView dgvValue = tabInspect.TabPages[k].Controls[0] as DataGridView;

                        if (dgvValue.Rows.Count < 1)
                        {
                            continue;
                        }

                        DataGridViewRow dgvr = dgvInspect.SelectedRows[0];

                        strInsType = dgvr.Cells["InsType"].Value.ToString().Trim();
                        double.TryParse(dgvr.Cells["InsRASpecMin"].Value.ToString(), out duMinVal);
                        double.TryParse(dgvr.Cells["InsRASpecMax"].Value.ToString(), out duMaxVal);

                        strSpec = dgvr.Cells["InsSpec"].Value.ToString();

                        for (int i = 0; i < dgvValue.Rows.Count; i++)
                        {
                            if (strInsType == "1")  // 정성적 검사값 입력
                            {
                                if (Lib.CheckNull(dgvValue["InspectValueText", i].Value.ToString()) == "")
                                {
                                    dgvValue.Rows[i].Selected = true;
                                    m_Popform_1 = new Frm_tinout_PopUpSel_InspectText();
                                    m_Popform_1.Owner = this;
                                    m_InspectText = "";
                                    m_Popform_1.ShowDialog();

                                    if (m_InspectText == "")
                                    {
                                        return;
                                    }
                                    else
                                    {
                                        dgvValue["InspectValueText", i].Value = m_InspectText;
                                        dgvValue.Columns["InspectValueText"].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
                                    }

                                    //if ((k + 1) < grdInsItem.Rows.Count)
                                    //{
                                    //    grdInsItem.Rows[k + 1].Selected = true;
                                    //}
                                    m_Popform_1 = null;

                                    if ((i + 1) >= dgvValue.Rows.Count)
                                    {
                                        string str = dgvr.Cells["insItemName"].Value.ToString();
                                        WizCommon.Popup.MyMessageBox.ShowBox(str + " 측정값을 다 입력하셨습니다", str, 1, 1);
                                        dgvr.Cells["DefectYN"].Value = LF_JudgeDefect();
                                        if (dgvr.Cells["DefectYN"].Value.ToString() == "불")
                                        {
                                            DataGridViewCellStyle style = new DataGridViewCellStyle();
                                            style.ForeColor = Color.Red;
                                            dgvr.Cells["DefectYN"].Style = style;
                                        }

                                    }
                                }

                            }

                            else   // 정량적 입력값
                            {
                                if (Lib.CheckNull(dgvValue["InspectValueText", i].Value.ToString()) == "")
                                {
                                    dgvValue.Rows[i].Selected = true;
                                    m_Popform_2 = new PopUp.Frm_CMNumericKeypad("검사값 입력", "측정값");
                                    m_Popform_2.Owner = this;
                                    m_Popform_2.StartPosition = FormStartPosition.Manual;
                                    m_Popform_2.Left = 600;
                                    m_Popform_2.Top = 180;
                                    m_Popform_2.ShowDialog();

                                    // 키패드 팝업창을 x키로 종료하는 경우에 "양호"로 진행되는 경우가 있어서 추가 2020.10.22
                                    if (m_Popform_2.DialogResult != DialogResult.OK)
                                    {
                                        m_InspectText = "";
                                    }

                                    if (m_InspectText == "")
                                    {
                                        return;
                                    }
                                    else
                                    {
                                        try
                                        {
                                            douValue = 0;
                                            double.TryParse(m_InspectText, out douValue);
                                            //기준값 미만 또는 초과라서 불량일 경우 메세지 띄워줌
                                            if (duMinVal > douValue)
                                            {
                                                WizCommon.Popup.MyMessageBox.ShowBox("불량 " + "\r\n" + strSpec + "  " + "미만", "[불량]", 1, 1);
                                            }
                                            else if (duMaxVal < douValue)
                                            {
                                                WizCommon.Popup.MyMessageBox.ShowBox("불량 " + "\r\n" + strSpec + "  " + "초과", "[불량]", 1, 1);
                                            }
                                            dgvValue["InspectValueText", i].Value = m_InspectText;
                                            dgvValue.Columns["InspectValueText"].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.BottomRight;
                                        }
                                        catch (Exception e1)
                                        {
                                            // MessageBox.Show("측정값 입력 오류입니다.");
                                            i = i - 1;
                                            return;
                                        }
                                    }
                                    m_Popform_2 = null;

                                    if ((k + 1) < dgvInspect.Rows.Count)
                                    {
                                        //grdInsItem.Rows[k + 1].Selected = true;
                                        dgvInspect.Rows[k].Selected = true;
                                    }

                                    if ((i + 1) >= dgvValue.Rows.Count)
                                    {
                                        string str = dgvr.Cells["insItemName"].Value.ToString();
                                        WizCommon.Popup.MyMessageBox.ShowBox(str + " 측정값을 다 입력하셨습니다", str, 1, 1);
                                        dgvr.Cells["DefectYN"].Value = LF_JudgeDefect();
                                        if (dgvr.Cells["DefectYN"].Value.ToString() == "불")
                                        {
                                            DataGridViewCellStyle style = new DataGridViewCellStyle();
                                            style.ForeColor = Color.Red;
                                            dgvr.Cells["DefectYN"].Style = style;
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

        #region 합/불 표시

        private string LF_JudgeDefect()
        {

            string strResult = "";

            string strInsType = "";

            double duMRAinVal = 0;
            double duRAMaxVal = 0;
            string strSpec = "";

            Boolean bDefect = false;

            //DataGridViewRow dr = grdInsItem.SelectedRows[0];
            if (dgvInspect.SelectedRows is null)
            {
                return "";
            }
            DataGridViewRow dr = dgvInspect.SelectedRows[0];
            DataGridView dgvTab = tabInspect.SelectedTab.Controls[0] as DataGridView;

            strInsType = dr.Cells["InsType"].Value.ToString().Trim();

            // 정량일때만, 최대값 과 최소값을 구해야 한다.  > 2020.02.12 허윤구 수정.
            if (strInsType == "2")
            {
                duMRAinVal = double.Parse(dr.Cells["InsRASpecMin"].Value.ToString().Trim());
                duRAMaxVal = double.Parse(dr.Cells["InsRASpecMax"].Value.ToString().Trim());
                strSpec = dr.Cells["InsSpec"].Value.ToString();
            }

            for (int i = 0; i < dgvTab.Rows.Count; i++)
            {
                string strValue = dgvTab["InspectValueText", i].Value.ToString().Trim();
                if (strInsType == "1")
                {
                    if (dgvTab["InspectValueText", i].Value.ToString() != "")
                    {
                        if ((strValue != "양호") && (strValue != "정상"))
                        {
                            bDefect = true;
                            break;
                        }
                    }
                }
                else
                {
                    if ((double.Parse(strValue) < duMRAinVal) || (double.Parse(strValue) > duRAMaxVal))
                    {
                        bDefect = true;
                        break;
                    }
                }
            }

            if (bDefect == true)
            {
                strResult = "불";
            }
            else
            {
                strResult = "합";
            }
            return strResult;
        }

        public void SetCheckValue(string strChkValue)
        {
            m_InspectText = strChkValue;
            return;
        }

        public void SetCheckValueCancel(string strChkValue)
        {
            m_InspectText = strChkValue;
            return;
        }

        #endregion

        #region 저장 함수

        private bool CheckData()
        {
            DataGridView dgvSave = null;
            int intRowChkCount = 0;
            int intValue = 0; //빈칸 갯수
            int intSampleQty = 0;
            string sInsType = "";
            int intInspectBasisSubSeq = 0;

            //입고일자
            if (mtb_SDate.Text == "")
            {
                WizCommon.Popup.MyMessageBox.ShowBox("입고일자를 입력해주세요.", "[확인]", 0, 1);
                return false;
            }
            //입고구분
            if (cboSGbn.Text == "전체" || cboSGbn.Text == "")
            {
                WizCommon.Popup.MyMessageBox.ShowBox("입고구분를 입력해주세요.", "[확인]", 0, 1);
                return false;
            }
            //거래처
            if (txtCustom.Text == "")
            {
                WizCommon.Popup.MyMessageBox.ShowBox("거래처를 입력해주세요.", "[확인]", 0, 1);
                return false;
            }
            //품번
            if (txtArticle.Text == "")
            {
                WizCommon.Popup.MyMessageBox.ShowBox("품번을 입력해주세요.", "[확인]", 0, 1);
                return false;
            }
            //품명그룹
            if (cboArticleGbn.Text == "전체" || cboArticleGbn.Text == "")
            {
                WizCommon.Popup.MyMessageBox.ShowBox("품명그룹을 입력해주세요.", "[확인]", 0, 1);
                return false;
            }
            //부가세별도
            if (cboVAT.Text == "전체" || cboVAT.Text == "")
            {
                WizCommon.Popup.MyMessageBox.ShowBox("부가세별도을 입력해주세요.", "[확인]", 0, 1);
                return false;
            }
            //입고단위
            if (cboUnitClss.Text == "전체" || cboUnitClss.Text == "")
            {
                WizCommon.Popup.MyMessageBox.ShowBox("입고단위을 입력해주세요.", "[확인]", 0, 1);
                return false;
            }
            //화폐단위
            if (cboMoneyClss.Text == "전체" || cboMoneyClss.Text == "")
            {
                WizCommon.Popup.MyMessageBox.ShowBox("화폐단위을 입력해주세요.", "[확인]", 0, 1);
                return false;
            }
            //후창고
            if (cboLoc.Text == "전체" || cboLoc.Text == "")
            {
                WizCommon.Popup.MyMessageBox.ShowBox("후창고을 입력해주세요.", "[확인]", 0, 1);
                return false;
            }
            //입고수량
            if (txtSQty.Text == "")
            {
                WizCommon.Popup.MyMessageBox.ShowBox("입고수량을 입력해주세요.", "[확인]", 0, 1);
                return false;
            }

            //입고자
            if (cboStuffinPerson.Text == "전체"  || cboStuffinPerson.Text == "")
            {
                WizCommon.Popup.MyMessageBox.ShowBox("입고자를 선택해주세요.", "[확인]", 0, 1);
                return false;
            }

            ////본딩중량
            //if (txtmtrWeightPerBonsu.Text == "")
            //{
            //    WizCommon.Popup.MyMessageBox.ShowBox("본딩중량을 입력해주세요.", "[확인]", 0, 1);
            //    return false;
            //}
            ////중량
            //if (txtmtrWeight.Text == "")
            //{
            //    WizCommon.Popup.MyMessageBox.ShowBox("중량을 입력해주세요.", "[확인]", 0, 1);
            //    return false;
            //}

            //검사필요여부
            if (cboInspectYN.Text == "전체" || cboInspectYN.Text == "")
            {
                WizCommon.Popup.MyMessageBox.ShowBox("검사필요여부을 입력해주세요.", "[확인]", 0, 1);
                return false;
            }

            if (cboInspectYN.Text == "Y")
            {
                //검수자
                if (cboInspectPerson.Text == "전체" || cboInspectPerson.Text == "")
                {
                    WizCommon.Popup.MyMessageBox.ShowBox("검수자을 입력해주세요.", "[확인]", 0, 1);
                    return false;
                }

                //검수일자
                if (mtb_IDate.Text == "")
                {
                    WizCommon.Popup.MyMessageBox.ShowBox("검수일자을 입력해주세요.", "[확인]", 0, 1);
                    return false;
                }

                intRowChkCount = 0;
                intValue = 0;

                for (int j = 0; j < dgvInspect.Rows.Count; j++)
                {
                    DataGridViewRow dr2 = dgvInspect.Rows[j];
                    dgvSave = tabInspect.TabPages[j].Controls[0] as DataGridView;  
                    intInspectBasisSubSeq = int.Parse(dr2.Cells["SubSeq"].Value.ToString().Trim());
                    intSampleQty = int.Parse(dr2.Cells["InsSampleQty"].Value.ToString().Trim());
                    sInsType = dr2.Cells["InsType"].Value.ToString().Trim();
                    if (intSampleQty == 0)
                    {
                        continue;
                    }

                    for (int i = 0; i < dgvSave.Rows.Count; i++)
                    {
                        if (Lib.CheckNull(dgvSave["InspectValueText", i].Value.ToString()) == "")
                        {
                            intValue++;
                            //if(WizCommon.Popup.MyMessageBox.ShowBox("비어있는 측정값으로 인한 오류입니다. \r\n" + "이대로 등록하시겠습니까 ?", "[측정값 오류]", 0, 0) == DialogResult.No) //+ (j + 1).ToString() + "번째 탭의 측정값을 모두 입력해주십시오.
                            //{
                            //    return false;
                            //}
                        }
                        else
                        {
                            intRowChkCount = intRowChkCount + 1;
                        }
                    }
                }

                if (intRowChkCount < dgvSave.Rows.Count)
                {
                    if (WizCommon.Popup.MyMessageBox.ShowBox("측정값의 갯수가 등록해야하는 갯수보다 작습니다. 이대로 등록하시겠습니까 ?", "확인", 0, 0) == DialogResult.No)//NO
                    {
                        return false;
                    }
                }

                if (intValue > 0)
                {
                    if (WizCommon.Popup.MyMessageBox.ShowBox("비어있는 측정값으로 인한 오류입니다. \r\n" + "이대로 등록하시겠습니까 ?", "[측정값 오류]", 0, 0) == DialogResult.No) //+ (j + 1).ToString() + "번째 탭의 측정값을 모두 입력해주십시오.
                    {
                        return false;
                    }
                }
            }
            else
            {
                ////검수자
                //if (txtInspectPerson.Text == "전체")
                //{
                //    WizCommon.Popup.MyMessageBox.ShowBox("검사자을 입력해주세요.", "[확인]", 0, 1);
                //    return false;
                //}

                ////검수일자
                //if (mtb_IDate.Text == "")
                //{
                //    WizCommon.Popup.MyMessageBox.ShowBox("검사일자을 입력해주세요.", "[확인]", 0, 1);
                //    return false;
                //}
            }

            return true;
        }


        private bool SaveData()
        {
            try
            {
                bool blDefectYN = false;
                string strsDefectYN = ""; //불량여부
                double defectQty = 0; //불량수량
                m_StuffinID = "";
                m_LabelID = "";

                //검사 기준이 있으면서 검사 데이터 입력, 기준이 없으면 빈칸으로 처리
                if (dgvInspect.Rows.Count > 0 && txtInspectYN.Text.ToString() == "Y")
                {
                    foreach (DataGridViewRow dgvr in dgvInspect.Rows)
                    {
                        if (dgvr.Cells["DefectYN"].Value.ToString() == "불")
                        {
                            blDefectYN = true;
                            strsDefectYN = "Y";
                            break;
                        }
                    }

                    if (!blDefectYN)
                    {
                        strsDefectYN = "N";
                    }
                }

                //grdInspect에서 min,max,type 가지고 와야 됨
                //수치불량이면 min,max,type 가지고 판단
                //외관불량이면 입력 데이터로 판단

                //불량수량 더하기
                //합격/불량, 수치 불량 나눠서 확인 후 더 해야 됨
                for (int j = 0; j < dgvInspect.Rows.Count; j++)
                {
                    DataGridViewRow dgvIns = dgvInspect.Rows[j];

                    //하위 그리드
                    dgv = tabInspect.TabPages[j].Controls[0] as DataGridView;
                    for (int i = 0; i < dgv.Rows.Count; i++)
                    {
                        //외관불량
                        if (dgvIns.Cells["InsType"].Value.ToString().Trim() == "1")
                        {
                            if (dgv["InspectValueText", i].Value.ToString() == "불량")
                            {
                                defectQty++;
                            }
                        }
                        //수치불량
                        else
                        {
                            //최소값보다 작으면 불량
                            if (Lib.ConvertDouble(dgv["InspectValueText", i].Value.ToString()) < Lib.ConvertDouble(dgvIns.Cells["InsRASpecMin"].Value.ToString()))
                            {
                                defectQty++;
                            }
                            //최대값보다 크면 불량
                            else if (Lib.ConvertDouble(dgv["InspectValueText", i].Value.ToString()) > Lib.ConvertDouble(dgvIns.Cells["InsRASpecMax"].Value.ToString()))
                            {
                                defectQty++;
                            }
                        }
                    }
                }

                List<WizCommon.Procedure> Prolist = new List<WizCommon.Procedure>();
                List<List<string>> ListProcedureName = new List<List<string>>();
                List<Dictionary<string, object>> ListParameter = new List<Dictionary<string, object>>();

                //수정과 입력 구분
                //u_LotID이 빈칸이면 입력 아니면 수정
                if (u_LotID == "")
                {
                    //stuffin
                    Dictionary<string, object> sqlParameter = new Dictionary<string, object>();

                    sqlParameter.Add("StuffinID", "");//stuffinid output
                    sqlParameter.Add("StuffDate", mtb_SDate.Text.Replace("-", ""));//입고일자
                    sqlParameter.Add("StuffClss", cboSGbn.SelectedValue.ToString());//입고구분 txtSGbnTag.Text.ToString()
                    sqlParameter.Add("CustomID", txtCustomTag.Text.ToString());//거래처
                    sqlParameter.Add("ReqID", txtOrderNum.Text.ToString());//발주번호
                    sqlParameter.Add("ArticleID", txtArticleTag.Text);//품명
                    sqlParameter.Add("BuyCustomID", txtSCustomTag.Text.ToString());//입고처명
                    sqlParameter.Add("VatIndYN", cboVAT.SelectedValue.ToString());//부가세별도 txtVAT.Text.ToString()
                    sqlParameter.Add("UnitClss", cboUnitClss.SelectedValue.ToString());//입고단위 txtUnitClssTag.Text.ToString()
                    sqlParameter.Add("PriceClss", cboMoneyClss.SelectedValue.ToString());//화폐단위 txtMoneyClssTag.Text.ToString()
                    sqlParameter.Add("TOLocID", cboLoc.SelectedValue.ToString());//후창고 txtLocTag.Text.ToString()
                    sqlParameter.Add("TotQty", Lib.ConvertDouble(txtSQty.Text.ToString()));//입고수량
                    sqlParameter.Add("MtrWeightPerBonsu", Lib.ConvertDouble(txtmtrWeightPerBonsu.Text.ToString()));//본딩중량
                    sqlParameter.Add("MtrWeight", Lib.ConvertDouble(txtmtrWeight.Text.ToString()));//중량
                    sqlParameter.Add("CreateUserID", cboStuffinPerson.SelectedValue.ToString()); //입고자 txtInspectPersonTag.Text.ToString()
                    
                    WizCommon.Procedure pro1 = new WizCommon.Procedure();
                    pro1.Name = "[xp_WizWork_iStuffinStock]";
                    pro1.OutputUseYN = "Y";
                    pro1.OutputName = "StuffinID";
                    pro1.OutputLength = "20";

                    Prolist.Add(pro1);
                    ListParameter.Add(sqlParameter);

                    //stuffinsub
                    sqlParameter = new Dictionary<string, object>();

                    sqlParameter.Add("StuffinID", "");      //stuffinid
                    sqlParameter.Add("StuffinSubseq", 1);   //stuffinsubseq, 하나씩 밖에 입력이 되지 않아 1로 하드코딩
                    sqlParameter.Add("Qty", Lib.ConvertDouble(txtSQty.Text.ToString()));    //수량
                    
                    sqlParameter.Add("CustomInspectDate", mtb_IDate.Text.Replace("-", "").ToString());//검사일자
                    sqlParameter.Add("InspectYN", cboInspectYN.SelectedValue.ToString()); //검사 여부 txtInspectYN.Text.ToString()

                    //검사하는 경우(Y)
                    if (cboInspectYN.SelectedValue.ToString() == "Y")
                    {
                        if (strsDefectYN == "Y")
                        {
                            sqlParameter.Add("PassYN", "N");                   //합격 여부, 합격 (Y), 불합격 (N)
                        }
                        else
                        {
                            sqlParameter.Add("PassYN", "Y");                   //합격 여부, 합격 (Y), 불합격 (N)
                        }

                        sqlParameter.Add("CustomInspector", cboInspectPerson.Text.ToString()); //검사자 txtInspectPerson.Text.ToString()

                    }
                    else
                    {
                        //검사 안 하는 경우(N)
                        sqlParameter.Add("PassYN", "Y");                            //검사 안 해도 되면 자동으로 검사 되게 함
                        sqlParameter.Add("CustomInspector", cboStuffinPerson.Text.ToString()); //검사자 txtInspectPerson.Text.ToString()
                    }

                    sqlParameter.Add("LabelID", "");                                         //labelID output
                    sqlParameter.Add("mtrCustomLotno", txtSLotNo.Text.ToString());          //입고처 lotno           
                    sqlParameter.Add("mtrCustomInspectPerson", txtSPerson.Text.ToString());//입고처 검수자
                    sqlParameter.Add("CreateUserID", cboStuffinPerson.SelectedValue.ToString());  //입고자 txtInspectPersonTag.Text.ToString()

                    WizCommon.Procedure pro2 = new WizCommon.Procedure();
                    pro2.Name = "[xp_WizWork_iStuffinSubStock]";
                    pro2.OutputUseYN = "Y";
                    pro2.OutputName = "LabelID";
                    pro2.OutputLength = "20";

                    Prolist.Add(pro2);
                    ListParameter.Add(sqlParameter);

                    if (cboInspectYN.SelectedValue.ToString() == "Y" && dgvInspect.Rows.Count > 0)
                    {

                        //ins_inspectauto
                        sqlParameter = new Dictionary<string, object>();
                        sqlParameter.Add("InspectID", "");      //inspectid
                        sqlParameter.Add("ArticleID", txtArticleTag.Text.ToString());  //품명               
                        sqlParameter.Add("LabelID", "");        //labelid
                        sqlParameter.Add("InspectGubun", "2");  //InspectGubun, 검사구분(전수검사:1, 샘플검사:2, 일반검사:3) 
                        sqlParameter.Add("InspectDate", mtb_IDate.Text.Replace("-", "").ToString());//검사일자
                                                                                                    //검사수량 sample수량 더하기
                        sqlParameter.Add("InspectUserID", cboInspectPerson.SelectedValue.ToString());//검사자  txtInspectPersonTag.Text.ToString()
                        sqlParameter.Add("InspectBasisID", m_InspectBasisID);//InspectBasisID
                                                                             //InspectBasisIDSeq            
                        sqlParameter.Add("DefectYN", strsDefectYN); //합(N)/불(Y)                
                        sqlParameter.Add("InspectPoint", "1");  //InspectPoint, //검사구분(수입검사:1, 자주검사, 출하검사)
                        sqlParameter.Add("InpCustomID", txtSCustomTag.Text.ToString());  //입고거래처
                        sqlParameter.Add("InpDate", mtb_SDate.Text.Replace("-", "").ToString());  //입고일
                        sqlParameter.Add("FMLGubun", "1");  //초:1/중:2/종:3
                        sqlParameter.Add("TotalDefectQty", defectQty);  //불량 수량 각 그리드 별로 불량 더하기
                                                                        //총 검사수량 sample수량 더하기
                        sqlParameter.Add("SumDefectQty", defectQty);    //총 불량 수량 각 그리드 별로 불량 더하기
                        sqlParameter.Add("CreateUserID", cboInspectPerson.SelectedValue.ToString());//검사자

                        WizCommon.Procedure pro3 = new WizCommon.Procedure();
                        pro3.Name = "[xp_WizWork_iInspectAuto]";
                        pro3.OutputUseYN = "Y";
                        pro3.OutputName = "InspectID";
                        pro3.OutputLength = "20";

                        Prolist.Add(pro3);
                        ListParameter.Add(sqlParameter);


                        //ins_inspectautosub
                        //grdInspect반복
                        for (int grdInspectCount = 0; grdInspectCount < dgvInspect.Rows.Count; grdInspectCount++)
                        {
                            //tab 반복
                            dgvtabInspect = tabInspect.TabPages[grdInspectCount].Controls[0] as DataGridView;
                            if (dgvtabInspect.Rows.Count > 0)
                            {
                                for (int dgvtabInspectCount = 0; dgvtabInspectCount < dgvtabInspect.Rows.Count; dgvtabInspectCount++)
                                {

                                    sqlParameter = new Dictionary<string, object>();

                                    sqlParameter.Add("InspectID", ""); //inspectid
                                    sqlParameter.Add("InspectBasisID", m_InspectBasisID);//InspectBasisID
                                    sqlParameter.Add("InspectBasisSeq", "1");//InspectBasisSeq

                                    if (dgvInspect.Rows[grdInspectCount].Cells["InsType"].Value.ToString().Trim() == "1")
                                    {
                                        sqlParameter.Add("InspectBasisSubSeq", dgvInspect.Rows[grdInspectCount].Cells["SubSeq"].Value.ToString().Trim());//InspectBasisSubSeq(외관, 수치 구분 용도) 1 텍스트
                                        sqlParameter.Add("InspectText", dgvtabInspect["InspectValueText", dgvtabInspectCount].Value.ToString());//InspectText(합격/불량)
                                        sqlParameter.Add("InspectValue", 0);//InspectValue(수치)
                                    }
                                    else
                                    {
                                        sqlParameter.Add("InspectBasisSubSeq", dgvInspect.Rows[grdInspectCount].Cells["SubSeq"].Value.ToString().Trim());//InspectBasisSubSeq(외관, 수치 구분 용도) 2 수치
                                        sqlParameter.Add("InspectText", "");//InspectText(합격/불량)
                                        sqlParameter.Add("InspectValue", Lib.ConvertDouble(dgvtabInspect["InspectValueText", dgvtabInspectCount].Value.ToString()));//InspectValue(수치)
                                    }

                                    sqlParameter.Add("Seq", 0);
                                    sqlParameter.Add("CreateUserID", cboInspectPerson.SelectedValue.ToString());//검사자

                                    WizCommon.Procedure pro4 = new WizCommon.Procedure();
                                    pro4.Name = "[xp_WizWork_iInspectAutoSub]";
                                    pro4.OutputUseYN = "N";
                                    pro4.OutputName = "InspectID";
                                    pro4.OutputLength = "20";

                                    Prolist.Add(pro4);
                                    ListParameter.Add(sqlParameter);
                                }
                            }
                        }
                    }

                    List<KeyValue> list_Result = new List<KeyValue>();
                    list_Result = DataStore.Instance.ExecuteAllProcedureOutputToCS(Prolist, ListParameter);

                    if (list_Result[0].key.ToLower() == "success")
                    {
                        list_Result.RemoveAt(0);

                        for (int i = 0; i < list_Result.Count; i++)
                        {
                            KeyValue kv = list_Result[i];
                            if (kv.key == "StuffinID")
                            {
                                m_StuffinID = kv.value.ToString();
                            }
                            else if (kv.key == "InspectID")
                            {
                                kv.value.ToString();
                            }
                            else if (kv.key == "LabelID")
                            {
                                m_LabelID = kv.value.ToString();
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
                else //수정
                {
                    //u_Seq = 1;

                    //stuffin
                    Dictionary<string, object> sqlParameter = new Dictionary<string, object>();

                    sqlParameter.Add("StuffinID", u_StuffinID);//stuffinid output
                    sqlParameter.Add("StuffDate", mtb_SDate.Text.Replace("-", ""));//입고일자
                    sqlParameter.Add("StuffClss", cboSGbn.SelectedValue.ToString());//입고구분
                    sqlParameter.Add("CustomID", txtCustomTag.Text.ToString());//거래처
                    sqlParameter.Add("ReqID", txtOrderNum.Text.ToString());//발주번호
                    sqlParameter.Add("ArticleID", txtArticleTag.Text);//품명
                    sqlParameter.Add("BuyCustomID", txtSCustomTag.Text.ToString());//입고처명
                    sqlParameter.Add("VatIndYN", cboVAT.SelectedValue.ToString());//부가세별도
                    sqlParameter.Add("UnitClss", cboUnitClss.SelectedValue.ToString());//입고단위
                    sqlParameter.Add("PriceClss", cboMoneyClss.SelectedValue.ToString());//화폐단위
                    sqlParameter.Add("TOLocID", cboLoc.SelectedValue.ToString());//후창고
                    sqlParameter.Add("TotQty", Lib.ConvertDouble(txtSQty.Text.ToString()));//입고수량
                    sqlParameter.Add("MtrWeightPerBonsu", Lib.ConvertDouble(txtmtrWeightPerBonsu.Text.ToString()));//본딩중량
                    sqlParameter.Add("MtrWeight", Lib.ConvertDouble(txtmtrWeight.Text.ToString()));//중량
                    sqlParameter.Add("CreateUserID", cboStuffinPerson.SelectedValue.ToString()); //작업자

                    WizCommon.Procedure pro1 = new WizCommon.Procedure();
                    pro1.Name = "[xp_WizWork_uStuffinStock]";
                    pro1.OutputUseYN = "N";
                    pro1.OutputName = "StuffinID";
                    pro1.OutputLength = "20";

                    Prolist.Add(pro1);
                    ListParameter.Add(sqlParameter);

                    //stuffinsub
                    sqlParameter = new Dictionary<string, object>();

                    sqlParameter.Add("StuffinID", u_StuffinID);      //stuffinid
                    sqlParameter.Add("StuffinSubseq", 1);   //stuffinsubseq, 하나씩 밖에 입력이 되지 않아 1로 하드코딩
                    sqlParameter.Add("Qty", Lib.ConvertDouble(txtSQty.Text.ToString()));    //수량
                    
                    sqlParameter.Add("CustomInspectDate", mtb_IDate.Text.Replace("-", "").ToString());//검사일자
                    sqlParameter.Add("InspectYN", cboInspectYN.SelectedValue.ToString()); //검사 여부
                    sqlParameter.Add("dInspectID", u_InspectID); //삭제할 이전 검사ID

                    //검사하는 경우(Y)
                    if (cboInspectYN.SelectedValue.ToString() == "Y")
                    {
                        if (strsDefectYN == "Y")
                        {
                            sqlParameter.Add("PassYN", "N");                   //합격 여부, 합격 (Y), 불합격 (N)
                        }
                        else
                        {
                            sqlParameter.Add("PassYN", "Y");                   //합격 여부, 합격 (Y), 불합격 (N)
                        }

                        sqlParameter.Add("CustomInspector", cboInspectPerson.Text.ToString()); //검사자

                    }
                    else
                    {
                        //검사 안 하는 경우(N)
                        sqlParameter.Add("PassYN", "Y");                            //검사 안 해도 되면 자동으로 검사 되게 함
                        sqlParameter.Add("CustomInspector", cboStuffinPerson.Text.ToString()); //검사자
                    }

                    sqlParameter.Add("mtrCustomLotno", txtSLotNo.Text.ToString());          //입고처 lotno
                    sqlParameter.Add("mtrCustomInspectPerson", txtSPerson.Text.ToString());//입고처 검수자
                    sqlParameter.Add("CreateUserID", cboStuffinPerson.SelectedValue.ToString());  //작업자

                    WizCommon.Procedure pro2 = new WizCommon.Procedure();
                    pro2.Name = "[xp_WizWork_uStuffinSubStock]";
                    pro2.OutputUseYN = "N";
                    pro2.OutputName = "LabelID";
                    pro2.OutputLength = "20";

                    Prolist.Add(pro2);
                    ListParameter.Add(sqlParameter);

                    if (cboInspectYN.SelectedValue.ToString() == "Y" && dgvInspect.Rows.Count > 0)
                    {

                        //ins_inspectauto
                        sqlParameter = new Dictionary<string, object>();
                        sqlParameter.Add("InspectID", "");      //inspectid
                        sqlParameter.Add("ArticleID", txtArticleTag.Text.ToString());  //품명               
                        sqlParameter.Add("LabelID", u_LotID);        //labelid
                        sqlParameter.Add("InspectGubun", "2");  //InspectGubun, 검사구분(전수검사:1, 샘플검사:2, 일반검사:3) 
                        sqlParameter.Add("InspectDate", mtb_IDate.Text.Replace("-", "").ToString());//검사일자
                                                                                                    //검사수량 sample수량 더하기
                        sqlParameter.Add("InspectUserID", cboInspectPerson.SelectedValue.ToString());//검사자
                        sqlParameter.Add("InspectBasisID", m_InspectBasisID);//InspectBasisID
                                                                             //InspectBasisIDSeq            
                        sqlParameter.Add("DefectYN", strsDefectYN); //합(N)/불(Y)                
                        sqlParameter.Add("InspectPoint", "1");  //InspectPoint, //검사구분(수입검사:1, 자주검사, 출하검사)
                        sqlParameter.Add("InpCustomID", txtSCustomTag.Text.ToString());  //입고거래처
                        sqlParameter.Add("InpDate", mtb_SDate.Text.Replace("-", "").ToString());  //입고일
                        sqlParameter.Add("FMLGubun", "1");  //초:1/중:2/종:3
                        sqlParameter.Add("TotalDefectQty", defectQty);  //불량 수량 각 그리드 별로 불량 더하기
                                                                        //총 검사수량 sample수량 더하기
                        sqlParameter.Add("SumDefectQty", defectQty);    //총 불량 수량 각 그리드 별로 불량 더하기
                        sqlParameter.Add("CreateUserID", cboInspectPerson.SelectedValue.ToString());//검사자


                        WizCommon.Procedure pro3 = new WizCommon.Procedure();
                        pro3.Name = "[xp_WizWork_iInspectAuto]";
                        pro3.OutputUseYN = "Y";
                        pro3.OutputName = "InspectID";
                        pro3.OutputLength = "20";

                        Prolist.Add(pro3);
                        ListParameter.Add(sqlParameter);


                        //ins_inspectautosub
                        //grdInspect반복
                        for (int grdInspectCount = 0; grdInspectCount < dgvInspect.Rows.Count; grdInspectCount++)
                        {
                            //tab 반복
                            dgvtabInspect = tabInspect.TabPages[grdInspectCount].Controls[0] as DataGridView;
                            if (dgvtabInspect.Rows.Count > 0)
                            {
                                for (int dgvtabInspectCount = 0; dgvtabInspectCount < dgvtabInspect.Rows.Count; dgvtabInspectCount++)
                                {

                                    sqlParameter = new Dictionary<string, object>();

                                    sqlParameter.Add("InspectID", ""); //inspectid
                                    sqlParameter.Add("InspectBasisID", m_InspectBasisID);//InspectBasisID
                                    sqlParameter.Add("InspectBasisSeq", "1");//InspectBasisSeq

                                    if (dgvInspect.Rows[grdInspectCount].Cells["InsType"].Value.ToString().Trim() == "1")
                                    {
                                        sqlParameter.Add("InspectBasisSubSeq", dgvInspect.Rows[grdInspectCount].Cells["SubSeq"].Value.ToString().Trim());//InspectBasisSubSeq(외관, 수치 구분 용도) 1 텍스트
                                        sqlParameter.Add("InspectText", dgvtabInspect["InspectValueText", dgvtabInspectCount].Value.ToString());//InspectText(합격/불량)
                                        sqlParameter.Add("InspectValue", 0);//InspectValue(수치)
                                    }
                                    else
                                    {
                                        sqlParameter.Add("InspectBasisSubSeq", dgvInspect.Rows[grdInspectCount].Cells["SubSeq"].Value.ToString().Trim());//InspectBasisSubSeq(외관, 수치 구분 용도) 2 수치
                                        sqlParameter.Add("InspectText", "");//InspectText(합격/불량)
                                        sqlParameter.Add("InspectValue", Lib.ConvertDouble(dgvtabInspect["InspectValueText", dgvtabInspectCount].Value.ToString()));//InspectValue(수치)
                                    }

                                    sqlParameter.Add("Seq", 0); //2023-11-15 삭제 후 다시 입력

                                    sqlParameter.Add("CreateUserID", cboInspectPerson.SelectedValue.ToString());//검사자

                                    WizCommon.Procedure pro4 = new WizCommon.Procedure();
                                    pro4.Name = "[xp_WizWork_iInspectAutoSub]";
                                    pro4.OutputUseYN = "N";
                                    pro4.OutputName = "InspectID";
                                    pro4.OutputLength = "20";

                                    Prolist.Add(pro4);
                                    ListParameter.Add(sqlParameter);
                                }
                            }
                        }
                    }

                    List<KeyValue> list_Result = new List<KeyValue>();
                    list_Result = DataStore.Instance.ExecuteAllProcedureOutputToCS(Prolist, ListParameter);

                    if (list_Result[0].key.ToLower() == "success")
                    {
                        list_Result.RemoveAt(0);

                        m_StuffinID = u_StuffinID;
                        m_LabelID = u_LotID;

                        //for (int i = 0; i < list_Result.Count; i++)
                        //{
                        //    KeyValue kv = list_Result[i];
                        //    if (kv.key == "StuffinID")
                        //    {
                        //        m_StuffinID = kv.value.ToString();
                        //    }
                        //    else if (kv.key == "InspectID")
                        //    {
                        //        kv.value.ToString();
                        //    }
                        //    else if (kv.key == "LabelID")
                        //    {
                        //        m_LabelID = kv.value.ToString();
                        //    }
                        //}

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
            }
            catch(Exception e)
            {
                Message[0] = "[오류]";
                Message[1] = string.Format("오류!관리자에게 문의\r\n{0}", e.Message);
                WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                return false;
            }
        }



        #endregion

        #region 초기화 함수

        //수입검사 초기화
        private void clearAllInsertedValue()
        {
            if (dgvInspect.Rows.Count > 0)
            {
                for (int k = 0; k < tabInspect.TabCount; k++)
                {
                    dgvInspect.Rows[k].Cells["DefectYN"].Value = string.Empty;

                    // 측정값 초기화.
                    DataGridView dgv = tabInspect.TabPages[k].Controls[0] as DataGridView;

                    if (dgv == null)
                    {
                        continue;
                    }

                    if (dgv != null)
                    {
                        for (int i = 0; i < dgv.Rows.Count; i++)
                        {
                            dgv.Rows[i].Cells["InspectValueText"].Value = string.Empty;
                        }
                    }
                }

                // 초기화 후, 첫번째 행 선택하기
                if (dgvInspect.Rows.Count > 0)
                {
                    dgvInspect.Rows[0].Selected = true;
                }
            }
        }

        //전체 초기화
        private void clearAllValue()
        {
            mtb_SDate.Text = DateTime.Today.ToString("yyyy-MM-dd"); //입고일자         
            //txtVAT.Text = "Y"; //부가세 별도        
            //txtInspectYN.Text = "Y"; //검사필요여부       
            mtb_IDate.Text = DateTime.Today.ToString("yyyy-MM-dd"); //검수일자
            //txtSGbn.Text = "";      //입고구분
            //txtSGbnTag.Text = "";
            txtCustom.Text = "";//거래처
            txtCustomTag.Text = "";
            txtOrderNum.Text = "";//발주번호
            txtArticle.Text = "";//품명
            txtArticleTag.Text = "";
            txtSCustom.Text = "";//입고처명
            txtSCustomTag.Text = "";
            //txtArticleGbn.Text = "";//품명그룹
            //txtUnitClss.Text = "";//입고단위
            //txtUnitClssTag.Text = "";
            //txtMoneyClss.Text = "";//화폐단위
            //txtMoneyClssTag.Text = "";
            //txtLoc.Text = "";//후창고
            //txtLocTag.Text = "";
            txtSQty.Text = "";//입고수량
            txtmtrWeightPerBonsu.Text = "";//본딩중량
            txtmtrWeight.Text = "";//중량
            txtSLotNo.Text = "";//입고처로트번호
            txtSPerson.Text = ""; //입고처검수자
            //txtInspectPerson.Text = "";//검수자
            //txtInspectPersonTag.Text = "";

            //입고자
            cboStuffinPerson.SelectedIndex = 0;
            //검수자
            cboInspectPerson.SelectedIndex = 0;
            //후창고
            cboLoc.SelectedIndex = 0;
            //입고구분
            cboSGbn.SelectedIndex = 0;
            //품명그룹
            cboArticleGbn.SelectedIndex = 0;
            //입고단위
            cboUnitClss.SelectedIndex = 0;
            //부가세별도
            cboVAT.SelectedIndex = 0;
            //화폐단위
            cboMoneyClss.SelectedIndex = 0;
            //검사필요여부
            cboInspectYN.SelectedIndex = 0;


        }

        //검사기준 그리드, 탭 초기화
        private void clearDgvTab()
        {
            if(dgvInspect.Rows.Count > 0)
            {
                dgvInspect.Rows.Clear();
                InitTabPage();
            }      
        }



        #endregion

        #region 셀 선택시 탭 변경

        private void dgvInspect_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dgvInspect.SelectedRows.Count > 0)
            {
                int i = 0;
                int.TryParse(dgvInspect.SelectedRows[0].Cells["No"].Value.ToString(), out i);
                foreach (TabPage tp in tabInspect.TabPages)
                {
                    if (tp.Text == i.ToString())
                    {
                        tp.Select();
                        tabInspect.SelectedIndex = (i - 1);
                        foreach (DataGridView dgv in tp.Controls)
                        {
                            foreach (DataGridViewRow dgvr in dgv.Rows)
                            {
                                if (dgvr.Cells["InspectValueText"].Value.ToString() == "")
                                {
                                    break;
                                }
                            }
                        }
                    }

                }
            }
        }

        #endregion

        #region 프린터 함수(Tag)

        private void PrintWorkCard()
        {
            string g_sPrinterName = Lib.GetDefaultPrinter();
            try
            {
                int R = 0;      // Rotation R.
                IsTagID = "013";
                List<string> list_Data = null;
                g_sPrinterName = Lib.GetDefaultPrinter();
                TSCLIB_DLL.openport(g_sPrinterName);
                
                list_Data = new List<string>();
                Dictionary<string, object> sqlParameter2 = new Dictionary<string, object>();
                sqlParameter2.Add("StuffinID", m_StuffinID);
                sqlParameter2.Add("LabelID", m_LabelID);
   
                DataTable dt2 = DataStore.Instance.ProcedureToDataTable("xp_WizWork_sPrintCard_i", sqlParameter2, false);
                lData = new List<string>();
               
                double douworkqty = 0;

                foreach (DataRow dr in dt2.Rows)
                {
                    double.TryParse(dr["Qty"].ToString(), out douworkqty);

                    list_Data.Add(Lib.CheckNull(m_LabelID)); //라벨번호(공정전표) 바코드 0
                    list_Data.Add(Lib.CheckNull(dr["Article"].ToString()));//품명 1
                    list_Data.Add(Lib.MakeDate(WizWorkLib.DateTimeClss.DF_FULL, Lib.CheckNull(dr["StuffDate"].ToString())));//입고일 2
                    list_Data.Add(Lib.CheckNull(dr["KCustom"].ToString()));//거래처 3
                    list_Data.Add((string.Format("{0:n0}", (int)douworkqty)) + dr["UnitClss"].ToString());//수량 4
                    list_Data.Add(m_LabelID);//로트번호 5

                }

                g_sPrinterName = Lib.GetDefaultPrinter();

                if (SendWindowDllCommand(list_Data, IsTagID, 1, 0))
                {
                    Message[0] = "[라벨발행중]";
                    Message[1] = "라벨 발행중입니다. 잠시만 기다려주세요.";
                    WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 2, 2);                   
                }
                else
                {
                    Message[0] = "[라벨발행 실패]";
                    Message[1] = "라벨 발행에 실패했습니다. 관리자에게 문의하여주세요.\r\n<SendWindowDllCommand>";
                    WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 2, 2);
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

        //tag 함수
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
                TSCLIB_DLL.setup(stringFormatN1(strWidth), stringFormatN1(strHeight), "4", "15", "1", "4", "0"); // GLS Black Mark Setting
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
                                    sBarType[1] = "5";
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

                                if (ReadAble.Equals("0"))
                                {
                                    // 바코드 글자 세팅
                                    int intx = list_m_tItem[i].x;
                                    int inty = list_m_tItem[i].y + 60;
                                    int fontheight = 60;
                                    int rotation = 0;
                                    int fontstyle = 0;
                                    int fontunderline = 0;
                                    string FaceName = "맑은 고딕";
                                    string content = Lib.CheckNull(list_m_tItem[i].sText).Trim();

                                    TSCLIB_DLL.windowsfont(intx, inty, fontheight, rotation, fontstyle, fontunderline, FaceName, content);
                                }
                            }
                        }
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

            //기타 함수
            private string stringFormatN1(object obj)
        {
            return string.Format("{0:N0}", obj);
        }



        #endregion


    }
}
