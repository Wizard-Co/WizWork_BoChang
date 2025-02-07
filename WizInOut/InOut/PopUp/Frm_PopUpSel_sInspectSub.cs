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
    public partial class Frm_PopUpSel_sInspectSub : Form
    {
        string InspectID = "";
        string InspectBasisID = "";

        DataGridView dgv = null; //탭페이지 데이터그리드뷰, 불량 수량 용도

        public Frm_PopUpSel_sInspectSub()
        {
            InitializeComponent();
        }

        public Frm_PopUpSel_sInspectSub(string strInspectID)
        {
            InitializeComponent();
            InspectID = strInspectID;
        }

        private void Frm_PopUpSel_sInspectSub_Load(object sender, EventArgs e)
        {
            InitgrdInsItemGrid();
            InitTabPage();

            //조회
            //InspectID로 검사이력 찾고, 기준 찾아서 조회하기

            //검사기준 찾기
            FillGridInspectBasisID();

            //검사이력 데이터 조회
            FillGridInspectID();

        }

        #region 닫기 이벤트

        private void cmdclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region 데이터그리드, TabPage, 검사기준, 검사이력 함수

        //검사기준 그리드
        private void InitgrdInsItemGrid()
        {
            dgvInspectSub.Columns.Clear();
            dgvInspectSub.ColumnCount = 13;

            int i = 0;

            // Set the Colums Hearder Names
            dgvInspectSub.Columns[i].Name = "No";
            dgvInspectSub.Columns[i].HeaderText = "";
            dgvInspectSub.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvInspectSub.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;

            dgvInspectSub.Columns[++i].Name = "insItemName";
            dgvInspectSub.Columns[i].HeaderText = "항목";
            dgvInspectSub.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvInspectSub.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;

            dgvInspectSub.Columns[++i].Name = "InsSpec";
            dgvInspectSub.Columns[i].HeaderText = "스펙";
            dgvInspectSub.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvInspectSub.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;

            dgvInspectSub.Columns[++i].Name = "InsTPSpecMax";
            dgvInspectSub.Columns[i].HeaderText = "정성적 Max";
            dgvInspectSub.Columns[i].Width = 0;
            dgvInspectSub.Columns[i].Visible = false;

            dgvInspectSub.Columns[++i].Name = "InsTPSpecMin";
            dgvInspectSub.Columns[i].HeaderText = "정성적 Min";
            dgvInspectSub.Columns[i].Width = 0;
            dgvInspectSub.Columns[i].Visible = false;

            dgvInspectSub.Columns[++i].Name = "InsRASpecMax";
            dgvInspectSub.Columns[i].HeaderText = "정량적 Max";
            dgvInspectSub.Columns[i].Width = 0;
            dgvInspectSub.Columns[i].Visible = false;

            dgvInspectSub.Columns[++i].Name = "InsRASpecMin";
            dgvInspectSub.Columns[i].HeaderText = "정량적 Min";
            dgvInspectSub.Columns[i].Width = 0;
            dgvInspectSub.Columns[i].Visible = false;

            dgvInspectSub.Columns[++i].Name = "InsSampleQty";
            dgvInspectSub.Columns[i].HeaderText = "샘플수량";
            dgvInspectSub.Columns[i].Width = 0;
            dgvInspectSub.Columns[i].Visible = false;

            dgvInspectSub.Columns[++i].Name = "InsType";
            dgvInspectSub.Columns[i].HeaderText = "정량/정성 타입";
            dgvInspectSub.Columns[i].Width = 0;
            dgvInspectSub.Columns[i].Visible = false;

            dgvInspectSub.Columns[++i].Name = "SubSeq";
            dgvInspectSub.Columns[i].HeaderText = "InspectBasisSub";
            dgvInspectSub.Columns[i].Width = 0;
            dgvInspectSub.Columns[i].Visible = false;

            dgvInspectSub.Columns[++i].Name = "MinValue";
            dgvInspectSub.Columns[i].HeaderText = "최소값";
            dgvInspectSub.Columns[i].Width = 80;
            dgvInspectSub.Columns[i].Visible = false;

            dgvInspectSub.Columns[++i].Name = "MaxValue";
            dgvInspectSub.Columns[i].HeaderText = "최대값";
            dgvInspectSub.Columns[i].Width = 80;
            dgvInspectSub.Columns[i].Visible = false;

            dgvInspectSub.Columns[++i].Name = "DefectYN";
            dgvInspectSub.Columns[i].HeaderText = "합/불";
            dgvInspectSub.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvInspectSub.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvInspectSub.Columns[i].Visible = true;

            dgvInspectSub.Rows.Clear();
            dgvInspectSub.RowTemplate.Height = 33;

            dgvInspectSub.ReadOnly = true;
            dgvInspectSub.Font = new Font("맑은 고딕", 10, FontStyle.Bold);
            dgvInspectSub.RowTemplate.Height = 30;
            dgvInspectSub.ColumnHeadersHeight = 35;
            dgvInspectSub.ScrollBars = ScrollBars.Both;
            dgvInspectSub.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvInspectSub.MultiSelect = false;
            dgvInspectSub.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvInspectSub.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(234, 234, 234);
            dgvInspectSub.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            foreach (DataGridViewColumn col in dgvInspectSub.Columns)
            {
                col.DataPropertyName = col.Name;
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

        }

        //검사 데이터 그리드
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
            _dgv.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
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

        //TabPage 
        private void InitTabPage()
        {
            tabInspectSub.TabPages.Clear();

            tabInspectSub.DrawItem += tabInspectSub_DrawItem;

            tabInspectSub.Font = new Font("맑은 고딕", 12, FontStyle.Bold);
            tabInspectSub.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabInspectSub.SizeMode = TabSizeMode.Fixed;
            //tabControl1.Alignment = System.Windows.Forms.TabAlignment.MiddleCenter;
            dgv = new DataGridView();
            dgv.Dock = DockStyle.Fill;
            dgv.Margin = new Padding(1, 1, 1, 1);
            InitGrid(dgv);
            SetTabPage(dgv, "1");
        }

        //TabPage 샘플수량만큼 추가
        private void InitTabPage(string SampleQty)
        {
            int intRowCnt = 0;

            intRowCnt = Int32.Parse(SampleQty);
            string title = (tabInspectSub.TabCount + 1).ToString();

            tabInspectSub.DrawItem += tabInspectSub_DrawItem;

            tabInspectSub.Font = new Font("맑은 고딕", 12, FontStyle.Bold);
            tabInspectSub.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabInspectSub.SizeMode = TabSizeMode.Fixed;

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

        //TabPage 설정
        private void SetTabPage(DataGridView _dgvInspect, string _strtitle)
        {
            TabPage TabPage = new TabPage(_strtitle);
            TabPage.Font = new Font("맑은 고딕", 12, FontStyle.Bold);
            TabPage.Controls.Add(dgv);
            _dgvInspect.Location = new Point(2, 2);
            tabInspectSub.TabPages.Add(TabPage);
            //2023-04-28 헤더 사이즈 조절
            tabInspectSub.ItemSize = new Size(30, 30);
        }

        private void tabInspectSub_DrawItem(object sender, DrawItemEventArgs e)
        {
            Graphics gr = e.Graphics;
            Font font = new Font("맑은 고딕", 14, FontStyle.Bold);

            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;

            for (int i = 0; i < tabInspectSub.TabPages.Count; i++)
            {
                string str = "";
                str = tabInspectSub.TabPages[i].Text;
                gr.DrawString(str, font, Brushes.Black, tabInspectSub.GetTabRect(i), sf);
            }
        }

        private void FillGridInspectBasisID()
        {
            //기준 찾기
            Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
            sqlParameter.Add("InspectID", InspectID); 
            DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_WizWork_sInspectAutoBasisByInspectID", sqlParameter, false);

            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];

                InspectBasisID = dr["InspectBasisID"].ToString();
            }

            DataStore.Instance.CloseConnection(); //2021-10-07 DB 커넥트 연결 해제

            if(InspectBasisID != "")
            {
                tabInspectSub.TabPages.Clear();
                dgvInspectSub.Rows.Clear();

                //검사기준 유무 확인
                sqlParameter = new Dictionary<string, object>();

                sqlParameter.Add("InspectBasisID", InspectBasisID); //검사기준ID
                dt = DataStore.Instance.ProcedureToDataTable("xp_WizWork_sInspectAutoBasisSubMTR", sqlParameter, false);

                int i = 1;

                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["insType"].ToString().Trim() == "1")
                    {
                        dgvInspectSub.Rows.Add(i++,
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
                        dgvInspectSub.Rows.Add(i++,
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

                DataStore.Instance.CloseConnection(); //2021-10-07 DB 커넥트 연결 해제
            }
            else
            {
                tabInspectSub.TabPages.Clear();
                dgvInspectSub.Rows.Clear();

                InitTabPage();

                WizCommon.Popup.MyMessageBox.ShowBox("해당 품명의 검사기준이 없습니다. \r\n " +
                         "검사기준등록화면에서 해당 품목의 검사기준데이터를 확인해 주세요.", "[확인]", 0, 1);
            }
        }

        private void FillGridInspectID()
        {
            //데이터 조회
            Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
            sqlParameter.Clear();

            sqlParameter.Add("InspectID", InspectID); //검사ID

            DataSet ds = DataStore.Instance.ProcedureToDataSet("xp_WizWork_sInspectAutoMtrByInspectID", sqlParameter, true);
            DataTable dt = ds.Tables[0];

            if (dt.Rows.Count > 0)
            {
                DataRowCollection drc = dt.Rows;

                foreach (DataRow dr in drc)
                {
                    for (int i = 0; i < dgvInspectSub.Rows.Count; i++)
                    {
                        //검사기준과 맞는 검사 결과를 찾고, 탭에 순서대로 입력, 합불 추가
                        if (dgvInspectSub.Rows[i].Cells["SubSeq"].Value.ToString() == dr["SubSeq"].ToString() && dgvInspectSub.Rows[i].Cells["insType"].Value.ToString() == dr["insType"].ToString())
                        {
                            for (int x = 0; x < tabInspectSub.TabPages.Count; x++)
                            {
                                if (tabInspectSub.TabPages[x].Text.ToString() == (i + 1).ToString())
                                {
                                    //탭 안에 그리드 찾기
                                    DataGridView UDataGridVeiw = tabInspectSub.TabPages[i].Controls[0] as DataGridView;

                                    for (int y = 0; y < UDataGridVeiw.Rows.Count; y++)
                                    {
                                        if (UDataGridVeiw.Rows[y].Cells["InspectValueText"].Value.ToString() == "")
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
                                if (dr["DefectYN"].ToString() == "Y" && (dgvInspectSub.Rows[i].Cells["DefectYN"].Value.ToString() == "합" || dgvInspectSub.Rows[i].Cells["DefectYN"].Value.ToString() == ""))
                                {
                                    dgvInspectSub.Rows[i].Cells["DefectYN"].Value = "불";
                                }
                                else if (dr["DefectYN"].ToString() == "N" && dgvInspectSub.Rows[i].Cells["DefectYN"].Value.ToString() == "")
                                {
                                    dgvInspectSub.Rows[i].Cells["DefectYN"].Value = "합";
                                }
                            }
                        }
                    }
                }
            }

        }

        private void FillGridData(string SampleQty)
        {
            InitTabPage(SampleQty);
        }

        #endregion

        #region 셀 선택시 탭 변경

        private void dgvInspectSub_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dgvInspectSub.SelectedRows.Count > 0)
            {
                int i = 0;
                int.TryParse(dgvInspectSub.SelectedRows[0].Cells["No"].Value.ToString(), out i);
                foreach (TabPage tp in tabInspectSub.TabPages)
                {
                    if (tp.Text == i.ToString())
                    {
                        tp.Select();
                        tabInspectSub.SelectedIndex = (i - 1);
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
    }
}