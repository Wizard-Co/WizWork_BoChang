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
    public partial class Frm_tinout_OCStuffin_Q : Form
    {
        private DataSet ds = null;
        private string IsTagID = "";
        WizWorkLib Lib = new WizWorkLib();
        List<string> lData = null;
        string[] Message = new string[2];

        bool blOpen = false;

        string StuffinID = ""; //수정시 사용할 StuffinID
        string LotID = "";     //수정시 사용할 LotID
        string ArticleID = ""; //수정시 사용할 ArticleID
        string InspectID = ""; //수정시 사용할 InspectID(입고검사ID)

        int RowsCount = 0; //조회한 그리드 수
        int CheckRowsCount = 0; //체크표시한 그리드 수
        double SumQty = 0; //하단 합계 입고량
        int SumMarkingCount = 0; //하단 합계 입고수 

        LogData LogData = new LogData(); //2022-10-24 log 남기는 함수

        public Frm_tinout_OCStuffin_Q()
        {
            InitializeComponent();
        }

        private void Frm_tinout_OCStuffin_Q_Load(object sender, EventArgs e)
        {
            LogData.LogSave(this.GetType().Name, "S"); //log 남기기(로드 S) 2022-10-24

            //입고일자 Checked
            chkSDate.Checked = true;
            //입고일자 From
            mtb_From.Text = DateTime.Today.AddDays(-1).ToString("yyyyMMdd");
            //입고일자 To
            mtb_To.Text = DateTime.Today.ToString("yyyy-MM-dd");

            InitGrid();

            SetcboSGbn();
        }

        #region 그리드 초기화

        private void InitGrid()
        {
            dgvStuffin.Columns.Clear();
            dgvStuffin.ColumnCount = 29;

            int i = 0;

            //순번
            dgvStuffin.Columns[i].Name = "No";
            dgvStuffin.Columns[i].HeaderText = "No";
            dgvStuffin.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvStuffin.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            //발주비고
            dgvStuffin.Columns[++i].Name = "ReqName";
            dgvStuffin.Columns[i].HeaderText = "발주비고";
            dgvStuffin.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvStuffin.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvStuffin.Columns[i].Visible = false;
            //입고일자
            dgvStuffin.Columns[++i].Name = "StuffDate";
            dgvStuffin.Columns[i].HeaderText = "입고일자";
            dgvStuffin.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvStuffin.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dgvStuffin.Columns[i].Visible = true;
            //거래처
            dgvStuffin.Columns[++i].Name = "Custom";
            dgvStuffin.Columns[i].HeaderText = "거래처";
            dgvStuffin.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvStuffin.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dgvStuffin.Columns[i].Visible = true;
            //품명
            dgvStuffin.Columns[++i].Name = "Article";
            dgvStuffin.Columns[i].HeaderText = "품명";
            dgvStuffin.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvStuffin.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dgvStuffin.Columns[i].Visible = true;
            //품번
            dgvStuffin.Columns[++i].Name = "BuyerArticleNo";
            dgvStuffin.Columns[i].HeaderText = "품번";
            dgvStuffin.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvStuffin.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dgvStuffin.Columns[i].Visible = true;
            //입고수량
            dgvStuffin.Columns[++i].Name = "SQty";
            dgvStuffin.Columns[i].HeaderText = "입고수량";
            dgvStuffin.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvStuffin.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dgvStuffin.Columns[i].Visible = true;
            //입고단위
            dgvStuffin.Columns[++i].Name = "UnitClss";
            dgvStuffin.Columns[i].HeaderText = "입고단위";
            dgvStuffin.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvStuffin.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvStuffin.Columns[i].Visible = true;
            //입고처명
            dgvStuffin.Columns[++i].Name = "SCustom";
            dgvStuffin.Columns[i].HeaderText = "입고처명";
            dgvStuffin.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvStuffin.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dgvStuffin.Columns[i].Visible = true;
            //발주번호
            dgvStuffin.Columns[++i].Name = "ReqID";
            dgvStuffin.Columns[i].HeaderText = "발주번호";
            dgvStuffin.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvStuffin.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dgvStuffin.Columns[i].Visible = true;
            //입고구분
            dgvStuffin.Columns[++i].Name = "StuffClss";
            dgvStuffin.Columns[i].HeaderText = "입고구분";
            dgvStuffin.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvStuffin.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dgvStuffin.Columns[i].Visible = true;
            //입고후창고
            dgvStuffin.Columns[++i].Name = "ToLoc";
            dgvStuffin.Columns[i].HeaderText = "입고후창고";
            dgvStuffin.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvStuffin.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvStuffin.Columns[i].Visible = false;
            //화폐단위
            dgvStuffin.Columns[++i].Name = "PriceClss";
            dgvStuffin.Columns[i].HeaderText = "화폐단위";
            dgvStuffin.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvStuffin.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvStuffin.Columns[i].Visible = false;
            //부가세
            dgvStuffin.Columns[++i].Name = "VatIndYN";
            dgvStuffin.Columns[i].HeaderText = "부가세별도";
            dgvStuffin.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvStuffin.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvStuffin.Columns[i].Visible = true;
            //비고
            dgvStuffin.Columns[++i].Name = "Remark";
            dgvStuffin.Columns[i].HeaderText = "비고";
            dgvStuffin.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvStuffin.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvStuffin.Columns[i].Visible = false;
            //검사필요여부
            dgvStuffin.Columns[++i].Name = "InspectYN";
            dgvStuffin.Columns[i].HeaderText = "검사필요여부";
            dgvStuffin.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvStuffin.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvStuffin.Columns[i].Visible = true;
            //LOTID
            dgvStuffin.Columns[++i].Name = "LotID";
            dgvStuffin.Columns[i].HeaderText = "라벨번호";
            dgvStuffin.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvStuffin.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvStuffin.Columns[i].Visible = true;
            //입고번호
            dgvStuffin.Columns[++i].Name = "StuffInID";
            dgvStuffin.Columns[i].HeaderText = "입고번호";
            dgvStuffin.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvStuffin.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvStuffin.Columns[i].Visible = false;
            //재질
            dgvStuffin.Columns[++i].Name = "InDiameter";
            dgvStuffin.Columns[i].HeaderText = "재질";
            dgvStuffin.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvStuffin.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvStuffin.Columns[i].Visible = false;
            //두께
            dgvStuffin.Columns[++i].Name = "Thickness";
            dgvStuffin.Columns[i].HeaderText = "두께";
            dgvStuffin.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvStuffin.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvStuffin.Columns[i].Visible = false;
            //폭
            dgvStuffin.Columns[++i].Name = "Width";
            dgvStuffin.Columns[i].HeaderText = "폭";
            dgvStuffin.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvStuffin.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvStuffin.Columns[i].Visible = false;
            //Spec
            dgvStuffin.Columns[++i].Name = "Spec";
            dgvStuffin.Columns[i].HeaderText = "Spec";
            dgvStuffin.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvStuffin.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvStuffin.Columns[i].Visible = false;
            //검수자
            dgvStuffin.Columns[++i].Name = "Inspector";
            dgvStuffin.Columns[i].HeaderText = "검수자";
            dgvStuffin.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvStuffin.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvStuffin.Columns[i].Visible = false;
            //입고단위ID
            dgvStuffin.Columns[++i].Name = "UnitClssID";
            dgvStuffin.Columns[i].HeaderText = "단위ID";
            dgvStuffin.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvStuffin.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvStuffin.Columns[i].Visible = false;
            //검사자
            dgvStuffin.Columns[++i].Name = "Inspector1";
            dgvStuffin.Columns[i].HeaderText = "검사자";
            dgvStuffin.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvStuffin.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvStuffin.Columns[i].Visible = false;
            //금액은 false
            dgvStuffin.Columns[++i].Name = "Amount";
            dgvStuffin.Columns[i].HeaderText = "금액";
            dgvStuffin.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvStuffin.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvStuffin.Columns[i].Visible = false;
            //잔량
            dgvStuffin.Columns[++i].Name = "ScrapQty";
            dgvStuffin.Columns[i].HeaderText = "잔량";
            dgvStuffin.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvStuffin.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvStuffin.Columns[i].Visible = false;
            //ArticleID
            dgvStuffin.Columns[++i].Name = "ArticleID";
            dgvStuffin.Columns[i].HeaderText = "ArticleID";
            dgvStuffin.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvStuffin.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvStuffin.Columns[i].Visible = false;
            //InspectID
            dgvStuffin.Columns[++i].Name = "InspectID";
            dgvStuffin.Columns[i].HeaderText = "InspectID";
            dgvStuffin.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvStuffin.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvStuffin.Columns[i].Visible = false;

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
            dgvStuffin.Columns.Insert(0, chkCol);

            dgvStuffin.ReadOnly = true;
            dgvStuffin.Font = new Font("맑은 고딕", 10, FontStyle.Bold);
            dgvStuffin.RowTemplate.Height = 50;
            dgvStuffin.ColumnHeadersHeight = 35;
            dgvStuffin.ScrollBars = ScrollBars.Both;
            dgvStuffin.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvStuffin.MultiSelect = false;
            dgvStuffin.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvStuffin.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(234, 234, 234);
            dgvStuffin.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            foreach (DataGridViewColumn col in dgvStuffin.Columns)
            {
                if (col.Index > 0)
                {
                    col.DataPropertyName = col.Name;
                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    col.SortMode = DataGridViewColumnSortMode.NotSortable;
                    col.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                }
              
            }

        }

        #endregion

        #region 재발행, 조회, 수정, 삭제, 닫기, 전체 선택 이벤트
        private void btnReprint_Click(object sender, EventArgs e)
        {
            //재발행
            //여러장
            //조회된 데이터가 없는 경우
            if (dgvStuffin.Rows.Count > 0) 
            {
                foreach (DataGridViewRow dgvsr in dgvStuffin.Rows)
                {
                    DataGridViewCell Cell = dgvsr.Cells["Check"];
                    bool isChecked = (bool)Cell.EditedFormattedValue;
                    if (isChecked)
                    {
                        Reprint(dgvsr);    
                    }
                }

                FillGrid();
            }
            else
            {
                WizCommon.Popup.MyMessageBox.ShowBox("조회된 데이터가 없습니다.\r\n조회를 먼저 해주세요.", "[재발행 전 확인]", 0, 1);
                return;
            }
        }

        private void btnFillGrid_Click(object sender, EventArgs e)
        {
            //조회
            FillGrid();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            LogData.LogSave(this.GetType().Name, "S"); //log 남기기(로드 S) 2022-10-24
            //닫기
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int CheckCount = 0;

            if (dgvStuffin.Rows.Count > 0) 
            {
                foreach (DataGridViewRow dgvsr in dgvStuffin.Rows)
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
                            StuffinID = dgvsr.Cells["StuffinID"].Value.ToString();
                            LotID = dgvsr.Cells["LotID"].Value.ToString();
                            ArticleID = dgvsr.Cells["ArticleID"].Value.ToString();
                            InspectID = dgvsr.Cells["InspectID"].Value.ToString();
                        }
                    }
                }

                //수정
                //원자재 입고 화면 열기
                UpdateSave();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int checkCount = 0;//체크된 카운트
            int deleteCount = 0;    //삭제된 데이터 카운트
            int NotdeleteCount = 0; //삭제 되지 않은 데이터 카운트

            if (dgvStuffin.RowCount == 0)
            {
                WizCommon.Popup.MyMessageBox.ShowBox("조회 후 삭제 버튼을 눌러주십시오.", "[조회자료 없음]", 0, 1);
            }
            else
            {
                foreach (DataGridViewRow dgvr in dgvStuffin.Rows)
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
                    if (WizCommon.Popup.MyMessageBox.ShowBox("선택한 입고이력을 삭제처리하시겠습니까?", "[삭제]", 0, 0) == DialogResult.OK)
                    {
                        foreach (DataGridViewRow dgvr in dgvStuffin.Rows)
                        {
                            if (dgvr.Cells["Check"].Value.ToString().ToUpper() == "TRUE")
                            {
                                //사용이력 찾아서 메세지로 확인
                                if (CheckUseLotID(dgvr.Cells["LotID"].Value.ToString()))
                                {
                                    Delete(dgvr.Cells["StuffinID"].Value.ToString());
                                    deleteCount++;
                                    //WizCommon.Popup.MyMessageBox.ShowBox("삭제 되었습니다.", "[삭제]", 0, 0);
                                }
                                else
                                {
                                    NotdeleteCount++;
                                }
                            }
                        }

                        //삭제된 경우
                        if(deleteCount > 0)
                        {
                            WizCommon.Popup.MyMessageBox.ShowBox(deleteCount.ToString() + "건 삭제되었습니다.", "[삭제 완료]", 0, 1);
                            if (NotdeleteCount > 0)
                            {
                                WizCommon.Popup.MyMessageBox.ShowBox(deleteCount.ToString() + "건은 사용이력이 있어 삭제가 되지 않았습니다.", "[삭제 완료]", 0, 1);
                            }
                        }
                        else //삭제가 안 된 경우
                        {
                            if (NotdeleteCount > 0)
                            {
                                WizCommon.Popup.MyMessageBox.ShowBox(deleteCount.ToString() + "건은 사용이력이 있어 삭제가 되지 않았습니다.", "[삭제 완료]", 0, 1);
                            }
                        }

                        FillGrid();
                    }
                }
            }          
        }

        private void chkAll_Click(object sender, EventArgs e)
        {
            if (dgvStuffin.Rows.Count > 0)
            {
                //전체선택, 전체해제 조건문
                if (btnAll.Text == "전체선택")
                {
                    for (int i = 0; i < dgvStuffin.Rows.Count; i++)
                    {
                        dgvStuffin.Rows[i].Cells["Check"].Value = true;
                    }

                    btnAll.Text = "전체해제";
                }
                else
                {
                    for (int i = 0; i < dgvStuffin.Rows.Count; i++)
                    {
                        dgvStuffin.Rows[i].Cells["Check"].Value = false;
                    }

                    btnAll.Text = "전체선택";
                }
            }
        }

        private void btnInspectSub_Click(object sender, EventArgs e)
        {
            //체크표시 확인, 하나했는지 두개이상했는지 확인 필요
            int checkcount = 0;
            string InspectID = "";

            for (int i = 0; i < dgvStuffin.Rows.Count; i++)
            {
                if(dgvStuffin.Rows[i].Cells["Check"].Value.ToString() == "True")
                {
                    checkcount++;
                    InspectID = dgvStuffin.Rows[i].Cells["InspectID"].Value.ToString();
                }
            }

            //검사이력 확인시 하나의 이력만 볼수 있음
            if (checkcount == 1) 
            {
                //수입검사이력이 있으면 보여주기
                Frm_PopUpSel_sInspectSub FPUSIS = new Frm_PopUpSel_sInspectSub(InspectID);
                FPUSIS.StartPosition = FormStartPosition.CenterScreen;
                FPUSIS.BringToFront();
                FPUSIS.TopMost = false;
                FPUSIS.ShowDialog();
            }
            else
            {
                WizCommon.Popup.MyMessageBox.ShowBox("선택된 입고이력이 없거나, \r\n입고이력이 여러개 선택되었습니다.\r\n하나의 입고이력만 선택해주세요.", "[수입검사확인 전 확인]", 0, 1);
                return;
            }
        }

        #endregion

        #region 검색조건 이벤트

        //일자 클릭 이벤트
        private void mtb_From_Click(object sender, EventArgs e)
        {
            WizCommon.Popup.Frm_TLP_Calendar calendar = new WizCommon.Popup.Frm_TLP_Calendar(mtb_From.Text.Replace("-", ""), mtb_From.Name, mtb_To.Text.Replace("-", ""));
            calendar.WriteDateTextEvent += new WizCommon.Popup.Frm_TLP_Calendar.TextEventHandler(GetDate);
            calendar.Owner = this;
            calendar.ShowDialog();
        }

        //일자 클릭 이벤트
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

        //품번
        private void chkBuyerArticleNo_Click(object sender, EventArgs e)
        {
            if (chkBuyerArticleNo.Checked)
            {
                Frm_PopUpSel_Common FPSC = new Frm_PopUpSel_Common("MA", txtBuyerArticleNo.Text, "", "S");
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
                        txtArticleTag.Text = CodeID;
                        txtBuyerArticleNo.Text = CodeName;
                    }
                }
            }
            else
            {
                txtArticleTag.Text = "";
                txtBuyerArticleNo.Text = "";
            }
        }

        //품번
        private void txtBuyerArticleNo_Click(object sender, EventArgs e)
        {
            Frm_PopUpSel_Common FPSC = new Frm_PopUpSel_Common("MA", txtBuyerArticleNo.Text, "", "S");
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
                    chkBuyerArticleNo.Checked = true;
                    txtArticleTag.Text = CodeID;
                    txtBuyerArticleNo.Text = CodeName;
                }
            }
        }

        //품명그룹
        private void chkArticleGbn_Click(object sender, EventArgs e)
        {
            //품명그룹 안 보이게 수정, 사용시 팝업창, 팝업창 조회 프로시저 수정
            //if (chkArticleGbn.Checked)
            //{
            //    Frm_PopUpSel_Common FPSC = new Frm_PopUpSel_Common("", txtArticleGbn.Text, "", "S");
            //    FPSC.StartPosition = FormStartPosition.CenterScreen;
            //    FPSC.BringToFront();
            //    FPSC.TopMost = false;
            //    FPSC.SWriteTextEvent += PopUp_WriteTextEvent;
            //    FPSC.ShowDialog();

            //    void PopUp_WriteTextEvent(string CodeID, string CodeName, string UnitClss, string UnitClssID, string PriceClss, string PriceClssID, string ArticleGrp, string OK)
            //    {
            //        if (OK == "Cancel")
            //        {
            //            return;
            //        }
            //        else
            //        {
            //            txtArticleGbnTag.Text = CodeID;
            //            txtArticleGbn.Text = CodeName;
            //        }
            //    }
            //}
            //else
            //{
            //    txtArticleGbnTag.Text = "";
            //    txtArticleGbn.Text = "";
            //}
        }

        //품명그룹
        private void txtArticleGbn_Click(object sender, EventArgs e)
        {
            //품명그룹 안 보이게 수정, 사용시 팝업창, 팝업창 조회 프로시저 수정
            //Frm_PopUpSel_Common FPSC = new Frm_PopUpSel_Common("ICD", txtArticleGbn.Text, "", "S");
            //FPSC.StartPosition = FormStartPosition.CenterScreen;
            //FPSC.BringToFront();
            //FPSC.TopMost = false;
            //FPSC.SWriteTextEvent += PopUp_WriteTextEvent;
            //FPSC.ShowDialog();

            //void PopUp_WriteTextEvent(string CodeID, string CodeName, string UnitClss, string UnitClssID, string PriceClss, string PriceClssID, string ArticleGrp, string OK)
            //{
            //    if (OK == "Cancel")
            //    {
            //        return;
            //    }
            //    else
            //    {
            //        chkArticleGbn.Checked = true;
            //        txtArticleGbnTag.Text = CodeID;
            //        txtArticleGbn.Text = CodeName;
            //    }
            //}
        }

        //거래처
        private void chkCustom_Click(object sender, EventArgs e)
        {
            if (chkCustom.Checked)
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
                    {
                        return;
                    }
                    else
                    {
                        txtCustomTag.Text = CodeID;
                        txtCustom.Text = CodeName;
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
            Frm_PopUpSel_Common FPSC = new Frm_PopUpSel_Common("MC", txtCustom.Text, "", "S");
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
                    chkCustom.Checked = true;
                    txtCustomTag.Text = CodeID;
                    txtCustom.Text = CodeName;
                }
            }
        }

        //입고구분
        private void chkSGbn_Click(object sender, EventArgs e)
        {
            if (chkSGbn.Checked)
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
                    {
                        return;
                    }
                    else
                    {
                        txtSGbnTag.Text = CodeID;
                        txtSGbn.Text = CodeName;
                    }
                }
            }
            else
            {
                txtSGbnTag.Text = "";
                txtSGbn.Text = "";
            }
        }

        //입고구분
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
                {
                    return;
                }
                else
                {
                    chkSGbn.Checked = true;
                    txtSGbnTag.Text = CodeID;
                    txtSGbn.Text = CodeName;
                }
            }
        }

        //품명
        private void chkArticle_Click(object sender, EventArgs e)
        {
            if (chkArticle.Checked)
            {
                Frm_PopUpSel_Common FPSC = new Frm_PopUpSel_Common("MA", txtArticle.Text, "", "S");
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
                        txtArticleTag.Text = CodeID;
                        txtArticle.Text = CodeName;
                    }
                }
            }
            else
            {
                txtArticleTag.Text = "";
                txtArticle.Text = "";
            }
        }

        private void txtArticle_Click(object sender, EventArgs e)
        {
            Frm_PopUpSel_Common FPSC = new Frm_PopUpSel_Common("MA", txtArticle.Text, "", "S");
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
                    chkArticle.Checked = true;
                    txtArticleTag.Text = CodeID;
                    txtArticle.Text = CodeName;
                }
            }
        }

        #endregion

        #region 조회, 재발행, 수정, 삭제 함수

        //조회
        private void FillGrid()
        {
            dgvStuffin.Rows.Clear();
            RowsCount = 0;
            SumQty = 0;
            SumMarkingCount = 0;

            try
            {
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Clear();

                //일자
                sqlParameter.Add("nChkDate", chkSDate.Checked == true ? 1 : 0);
                sqlParameter.Add("sSDate", mtb_From.Text.Replace("-", ""));
                sqlParameter.Add("sEDate", mtb_To.Text.Replace("-", ""));
                //거래처
                sqlParameter.Add("nChkCustom", chkCustom.Checked == true ? 1 : 0);
                sqlParameter.Add("sCustom", txtCustomTag.Text.ToString());
                //품명
                sqlParameter.Add("nChkArticleID", chkArticle.Checked == true ? 1 : 0);
                sqlParameter.Add("sArticleID", txtArticle.Text.ToString());
                //입고구분
                sqlParameter.Add("nChkStuffClss", chkSGbn.Checked == true ? 1 : 0);
                sqlParameter.Add("sStuffClss", cboSGbn.SelectedValue.ToString()); //txtSGbnTag.Text.ToString()
                ////품명그룹
                //sqlParameter.Add("nChkArticleGrp", chkArticleGbn.Checked == true ? 1 : 0);
                //sqlParameter.Add("ArticleGrpID", txtArticleGbnTag.Text.ToString());

                DataSet ds = DataStore.Instance.ProcedureToDataSet_NewLog("xp_WizWork_sStuffIN", sqlParameter, true, "R", Frm_tinout_Main.g_tBase.PersonID);

                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        int i = 0;
                        DataRowCollection drc = dt.Rows;

                        foreach (DataRow dr in drc)
                        {
                            dgvStuffin.Rows.Add( false,         //체크박스
                                                 ++i,           //순번
                                                 dr["ReqName"].ToString(),//발주비고
                                                 dr["StuffDate"].ToString(),//입고일자
                                                 dr["CustomName"].ToString(),//거래처
                                                 dr["BuyerArticleNo"].ToString(),//품명
                                                 dr["Article"].ToString(),//품번
                                                 string.Format("{0:#,###}", Lib.ConvertDouble(dr["StuffQty"].ToString())),//입고수량
                                                 dr["UnitClssName"].ToString(),//입고단위
                                                 dr["Custom"].ToString(),//입고처명
                                                 dr["REQ_ID"].ToString(),//발주번호
                                                 dr["StuffClssName"].ToString(),//입고구분
                                                 dr["ToLocName"].ToString(),//입고후창고
                                                 dr["PriceClssName"].ToString(),//화폐단위
                                                 dr["Vat_Ind_YN"].ToString(),//부가세
                                                 dr["Remark"].ToString(),//비고                           
                                                 dr["InpectYN"].ToString(),//검사필요여부
                                                 dr["Lotid"].ToString(),//LotID
                                                 dr["StuffInID"].ToString(),//입고번호
                                                 dr["InDiameter"].ToString(),//재질
                                                 "",//두께
                                                 dr["Width"].ToString(),//폭
                                                 dr["Spec"].ToString(),//Spec
                                                 dr["Inspector1"].ToString(),//검수자
                                                 dr["UnitClss"].ToString(),//단위ID
                                                 dr["Inspector"].ToString(),//검사자
                                                 dr["Amount"].ToString(),//금액
                                                 dr["ScrapQty"].ToString(), //잔량
                                                 dr["ArticleID"].ToString(), //ArticleID
                                                 dr["InspectID"].ToString()
                                );

                            RowsCount++;

                            SumQty += Lib.ConvertDouble(dr["StuffQty"].ToString());
                            SumMarkingCount++;
                        }

                    }
                }

                txtSumSQty.Text = Lib.stringFormatN2(SumQty);
                txtSumQty.Text = Lib.stringFormatN0(SumMarkingCount);


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

        //재발행
        private void Reprint(DataGridViewRow dgvsr)
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
                sqlParameter2.Add("StuffinID", dgvsr.Cells["StuffInID"].Value.ToString());
                sqlParameter2.Add("LabelID", dgvsr.Cells["LotID"].Value.ToString());

                DataTable dt2 = DataStore.Instance.ProcedureToDataTable("xp_WizWork_sPrintCard_i", sqlParameter2, false);
                lData = new List<string>();

                double douworkqty = 0;

                foreach (DataRow dr in dt2.Rows)
                {
                    double.TryParse(dr["Qty"].ToString(), out douworkqty);

                    list_Data.Add(Lib.CheckNull(dr["LotID"].ToString())); //라벨번호(공정전표) 바코드 0
                    list_Data.Add(Lib.CheckNull(dr["KCustom"].ToString()));//거래처 1
                    list_Data.Add(Lib.CheckNull(dr["BuyerArticleNo"].ToString())); //품명 2
                    list_Data.Add(Lib.CheckNull(dr["Spec"].ToString()));//Spec 3
                    list_Data.Add(Lib.CheckNull(dr["Texture"].ToString()));//재질 4
                    list_Data.Add((string.Format("{0:n0}", (int)douworkqty)) + dr["UnitClss"].ToString());//입고수량 5
                    list_Data.Add(Lib.MakeDate(WizWorkLib.DateTimeClss.DF_FULL, Lib.CheckNull(dr["StuffDate"].ToString())));//입고일자 6
                }

                g_sPrinterName = Lib.GetDefaultPrinter();

                Frm_tinout_OCStuffin_U fmocs = new Frm_tinout_OCStuffin_U();

                if (fmocs.SendWindowDllCommand(list_Data, IsTagID, 1, 0))
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

        //수정
        private void UpdateSave()
        {
            Form form = null;//폼 초기화

            Frm_tinout_OCStuffin_U child1 = new Frm_tinout_OCStuffin_U(StuffinID, LotID, ArticleID, InspectID);
            form = child1;

            if (form != null)
            {
                foreach (Form openForm in Application.OpenForms)//중복실행방지
                {
                    if (openForm.Name == form.Name)
                    {
                        openForm.Close();       //2023-12-29
                        blOpen = true;
                        //openForm.BringToFront();
                        //openForm.Activate();
                        //return;
                        break;                  //2023-12-29
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
        private void Delete(string StuffinID)
        {
            //List<string> list_Confirm = new List<string>();         //프로시저 수행 성공여부 값 저장/success/failure
            Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
            sqlParameter.Add("StuffinID", StuffinID);
            string[] sConfirm = new string[2];
            sConfirm = DataStore.Instance.ExecuteProcedure_NewLog("xp_WizWork_dStuffin", sqlParameter, true, "D", Frm_tinout_Main.g_tBase.PersonID);
            //list_Confirm.Add(sConfirm[0]);
            //if (sConfirm[0].ToUpper() == "SUCCESS")
            //{ 

            //}
            //else
            //{

            //}
        }

        //삭제 전 사용이력 확인
        private bool CheckUseLotID(string LotID)
        {
            //stuffin 입고이외의 데이터, outware에 1줄이라도 있으면 사용함
            try
            {
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Clear();

                sqlParameter.Add("LotID", LotID);

                DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_WizWork_CheckDelete", sqlParameter, false);

                if (dt != null
                    && dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];

                    if (Convert.ToDouble(dr["StuffinCount"].ToString()) > 1 || Convert.ToDouble(dr["OutwareCount"].ToString()) > 0)
                    {
                        return false;
                    }                                  
                }
            }
            catch (Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox("중복 체크 구문 오류 [ CheckAlreadyWorkIn ] + \r\n" + ex.Message, "저장 전 체크 오류", 0, 1);
                return false;
            }

            DataStore.Instance.CloseConnection(); //2021-09-23 DB 커넥트 연결 해제
            return true;
        }

        #endregion

        #region 그리드 클릭 이벤트

        private void dgvStuffin_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                CheckRowsCount = 0;

                if (dgvStuffin.Rows[e.RowIndex].Cells["Check"].Value.ToString().ToUpper() == "FALSE")
                {
                    dgvStuffin.Rows[e.RowIndex].Cells["Check"].Value = true;

                    for (int i = 0; i < dgvStuffin.Rows.Count; i++)
                    {
                        if(dgvStuffin.Rows[i].Cells["Check"].Value.ToString().ToUpper() == "TRUE")
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
                else if (dgvStuffin.Rows[e.RowIndex].Cells["Check"].Value.ToString().ToUpper() == "TRUE")
                {
                    dgvStuffin.Rows[e.RowIndex].Cells["Check"].Value = false;

                    for (int i = 0; i < dgvStuffin.Rows.Count; i++)
                    {
                        if (dgvStuffin.Rows[i].Cells["Check"].Value.ToString().ToUpper() == "TRUE")
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

        #endregion


    }
}
