using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WizCommon;
//using WizWork.Tkb.PopUp;

namespace WizWork
{
    public partial class frm_tprc_MoldInOutware_Q : Form
    {
        string[] Message = new string[2];
        INI_GS gs = Frm_tprc_Main.gs;
        WizWorkLib Lib = Frm_tprc_Main.Lib;
        DataTable dt = null; 
        private DataSet ds = null; //2022-10-21
        int x = 0; //grdlist 그리드 좌우 이동용 변수
        List<string> WorkCallIDList = new List<string>(); //여러개를 한꺼번에 처리할 경우를 위해 리스트 생성 2022-10-21

        LogData LogData = new LogData(); //2022-10-24 log 남기는 함수

        public frm_tprc_MoldInOutware_Q()
        {
            InitializeComponent();
        }

        private void InitGrid()
        {
            dgvMoldLoT.Columns.Clear();
            dgvMoldLoT.ColumnCount = 8;

            int n = 0;
            // Set the Colums Hearder Names

            dgvMoldLoT.Columns[n].Name = "RowSeq";
            dgvMoldLoT.Columns[n].HeaderText = "순";
            dgvMoldLoT.Columns[n].Width = 50;
            dgvMoldLoT.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvMoldLoT.Columns[n++].Visible = true;

            dgvMoldLoT.Columns[n].Name = "InOutGbn";
            dgvMoldLoT.Columns[n].HeaderText = "구분";
            dgvMoldLoT.Columns[n].Width = 130;
            dgvMoldLoT.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvMoldLoT.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvMoldLoT.Columns[n++].Visible = true;

            dgvMoldLoT.Columns[n].Name = "InOutDate";
            dgvMoldLoT.Columns[n].HeaderText = "일자";
            dgvMoldLoT.Columns[n].Width = 130;
            dgvMoldLoT.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvMoldLoT.Columns[n++].Visible = true;

            dgvMoldLoT.Columns[n].Name = "MoldNo";
            dgvMoldLoT.Columns[n].HeaderText = "금형로트번호";
            dgvMoldLoT.Columns[n].Width = 160;
            dgvMoldLoT.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvMoldLoT.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvMoldLoT.Columns[n++].Visible = true;

            dgvMoldLoT.Columns[n].Name = "BuyerArticleNo";
            dgvMoldLoT.Columns[n].HeaderText = "품번";
            dgvMoldLoT.Columns[n].Width = 90;
            dgvMoldLoT.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvMoldLoT.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvMoldLoT.Columns[n++].Visible = true;

            dgvMoldLoT.Columns[n].Name = "ArticleID";
            dgvMoldLoT.Columns[n].HeaderText = "품명";
            dgvMoldLoT.Columns[n].Width = 90;
            dgvMoldLoT.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvMoldLoT.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvMoldLoT.Columns[n++].Visible = true;

            dgvMoldLoT.Columns[n].Name = "MoldID";
            dgvMoldLoT.Columns[n].HeaderText = "금형명";
            dgvMoldLoT.Columns[n].Width = 90;
            dgvMoldLoT.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvMoldLoT.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvMoldLoT.Columns[n++].Visible = true;

            dgvMoldLoT.Columns[n].Name = "InOutID";
            dgvMoldLoT.Columns[n].HeaderText = "InOutID";
            dgvMoldLoT.Columns[n].Width = 200;
            dgvMoldLoT.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dgvMoldLoT.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvMoldLoT.Columns[n++].Visible = false;

            DataGridViewCheckBoxColumn chkCol = new DataGridViewCheckBoxColumn();
            {
                chkCol.HeaderText = "선택";
                chkCol.Name = "Check";
                chkCol.Width = 110;
                //chkCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                chkCol.FlatStyle = FlatStyle.Standard;
                chkCol.ThreeState = true;
                chkCol.CellTemplate = new DataGridViewCheckBoxCell();
                chkCol.CellTemplate.Style.BackColor = Color.Beige;
                chkCol.Visible = true;
            }
            dgvMoldLoT.Columns.Insert(1, chkCol);

            dgvMoldLoT.Font = new Font("맑은 고딕", 12);
            dgvMoldLoT.RowTemplate.Height = 30;
            dgvMoldLoT.ColumnHeadersHeight = 35;
            dgvMoldLoT.ScrollBars = ScrollBars.Both;
            dgvMoldLoT.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMoldLoT.ReadOnly = true;

            foreach (DataGridViewColumn col in dgvMoldLoT.Columns)
            {
                col.DataPropertyName = col.Name;
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
                //col.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            }
            return;
        }

        private void frm_tprc_MoldInOutware_Q_Load(object sender, EventArgs e)
        {
            LogData.LogSave(this.GetType().Name, "S"); //log 남기기(로드 S) 2022-10-24
            chkDate.Checked = true;
            chkGbn.Checked = true;
            SetDateTimePicker();
            SetProcessComboBox();
            cboGbn.SelectedIndex = 0;
            InitGrid();
            LogData.LogSave(this.GetType().Name, "R"); //2022-06-22 조회
            FillGrid();
        }

        #region 조회 함수

        private void FillGrid()
        {
            dgvMoldLoT.Rows.Clear();
            try
            {
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                DateTime dateTime = DateTime.Now;
                if (!chkDate.Checked)
                {
                    sqlParameter.Add("nChkDate", "0");    // 최종검사
                    sqlParameter.Add("SDate", "");
                    sqlParameter.Add("EDate", "");
                }
                else
                {
                    sqlParameter.Add("nChkDate", "1");    // 최종검사
                    sqlParameter.Add("SDate", mtb_From.Text.Replace("-", ""));
                    sqlParameter.Add("EDate", mtb_To.Text.Replace("-", ""));
                }

                sqlParameter.Add("MoldLotNO", txtMoldLoT.Text);
                sqlParameter.Add("nchkInOutGbn", chkGbn.Checked == true ? 1 : 0);
                sqlParameter.Add("InOutGbn", cboGbn.SelectedValue.ToString());

                ds = DataStore.Instance.ProcedureToDataSet("xp_WizWork_sMoldInOutware", sqlParameter, false);

                if (ds.Tables[0].Rows.Count > 0)
                {

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DataRow dr = ds.Tables[0].Rows[i];
                        dateTime = DateTime.ParseExact(dr["InOutDate"].ToString(), "yyyyMMdd", null);
                        dgvMoldLoT.Rows.Add(
                            i + 1,                                          //'0)Index
                            false,                                          //'1)check
                            dr["InOutGbn"].ToString(),
                            dateTime.ToString("yyyy-MM-dd"),                //'2)점검일자
                            dr["MoldNo"].ToString(),                        //'3)금형로트번호
                            dr["BuyerArticleNo"].ToString(),                //'5)품번
                            dr["Article"].ToString(),                       //'6)품명
                            dr["MoldID"].ToString(),                        //금형명
                            dr["InOutID"].ToString()                        //'7)InOutID
                            );
                        dgvMoldLoT.Rows[i].Height = 30;
                    }
                    dgvMoldLoT.ClearSelection();
                    dgvMoldLoT[0, 0].Selected = true;
                    dgvMoldLoT.Columns["RowSeq"].Width = 60;
                }
                Frm_tprc_Main.gv.queryCount = string.Format("{0:n0}", dgvMoldLoT.RowCount);
                Frm_tprc_Main.gv.SetStbInfo();
            }
            catch (Exception excpt)
            {
                Message[0] = "[오류]";
                Message[1] = string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message);
                WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
            }

        }


        #endregion

        #region 날짜 초기화

        private void SetDateTimePicker()
        {
            mtb_From.Text = DateTime.Today.ToString("yyyyMMdd");
            mtb_To.Text = DateTime.Today.AddDays(7).ToString("yyyyMMdd");
        }

        #endregion

        #region 달력 이벤트

        private void mtb_From_Click(object sender, EventArgs e)
        {
            WizCommon.Popup.Frm_TLP_Calendar calendar = new WizCommon.Popup.Frm_TLP_Calendar(mtb_From.Text.Replace("-", ""), mtb_From.Name, mtb_To.Text.Replace("-", ""));
            calendar.WriteDateTextEvent += new WizCommon.Popup.Frm_TLP_Calendar.TextEventHandler(GetDate);
            calendar.Owner = this;
            calendar.ShowDialog();
        }

        private void mtb_To_Click(object sender, EventArgs e)
        {
            WizCommon.Popup.Frm_TLP_Calendar calendar = new WizCommon.Popup.Frm_TLP_Calendar(mtb_To.Text.Replace("-", ""), mtb_To.Name, mtb_From.Text.Replace("-", ""));
            calendar.WriteDateTextEvent += new WizCommon.Popup.Frm_TLP_Calendar.TextEventHandler(GetDate);
            calendar.Owner = this;
            calendar.ShowDialog();
        }

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

        #region 금형로트번호 이벤트

        private void btnMoldLoT_Click(object sender, EventArgs e)
        {
            frm_tprc_Mold_ByArticleID Ftmba = new frm_tprc_Mold_ByArticleID();
            Ftmba.StartPosition = FormStartPosition.CenterScreen;
            if (Ftmba.ShowDialog() == DialogResult.OK)
            {
                txtMoldLoT.Text = Ftmba.MoldNo;
                //MoldID = Ftmba.MoldID;
                //FillGrid();
            }
        }

        private void txtMoldLoT_Click(object sender, EventArgs e)
        {
            frm_tprc_Mold_ByArticleID Ftmba = new frm_tprc_Mold_ByArticleID();
            Ftmba.StartPosition = FormStartPosition.CenterScreen;
            if (Ftmba.ShowDialog() == DialogResult.OK)
            {
                txtMoldLoT.Text = Ftmba.MoldNo;
                //MoldID = Ftmba.MoldID;
                //FillGrid();
            }
        }

        #endregion

        #region 콤보박스 이벤트

        private void SetProcessComboBox()
        {
            try
            {

                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Add("CodeGbn", "DVLGBN");

                DataSet ds = DataStore.Instance.ProcedureToDataSet("xp_WizWork_DVLGBN", sqlParameter, false);

                DataRow newRow = ds.Tables[0].NewRow();
                newRow["CodeID"] = "*";
                newRow["CodeName"] = "전체";

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    ds.Tables[0].Rows.InsertAt(newRow, 0);
                    cboGbn.DataSource = ds.Tables[0];
                }

                cboGbn.ValueMember = "CodeID";
                cboGbn.DisplayMember = "CodeName";

                DataStore.Instance.CloseConnection(); //2021-10-07 DB 커넥트 연결 해제
            }
            catch (Exception excpt)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message), "[오류]", 0, 1);
            }
            return;
        }

        #endregion

        #region 검색, 삭제, 닫기 이벤트

        private void btnFillGrid_Click(object sender, EventArgs e)
        {
            LogData.LogSave(this.GetType().Name, "R"); //2022-06-22 조회
            FillGrid();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            LogData.LogSave(this.GetType().Name, "S"); //log 남기기(로드 S) 2022-10-24
            this.Close();
        }

        #endregion

        #region 체크표시 이벤트

        private void dgvMoldLoT_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (dgvMoldLoT.Rows[e.RowIndex].Cells["Check"].Value.ToString().ToUpper() == "FALSE")
                {
                    dgvMoldLoT.Rows[e.RowIndex].Cells["Check"].Value = true;
                }
                else if (dgvMoldLoT.Rows[e.RowIndex].Cells["Check"].Value.ToString().ToUpper() == "TRUE")
                {
                    dgvMoldLoT.Rows[e.RowIndex].Cells["Check"].Value = false;
                }
            }
        }

        #endregion

        #region 삭제 함수

        private void DeleteData()
        {
            int checkCount = 0;//체크된 카운트
            int c = 0; //작업일이 현재일자와 같지 않아서 삭제할 수 없는 행의 수
            int deleteCount = 0; //작업일이 현재일자와 같지 않아서 삭제할 수 없는 행의 수
    
            if (dgvMoldLoT.RowCount == 0)
            {
                WizCommon.Popup.MyMessageBox.ShowBox("조회 후 삭제 버튼을 눌러주십시오.", "[조회 클릭]", 0, 0);
            }
            else
            {
                foreach (DataGridViewRow dgvr in dgvMoldLoT.Rows)
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
                    if (WizCommon.Popup.MyMessageBox.ShowBox("선택항목에 대해서 삭제처리하시겠습니까?", "[삭제]", 0, 0) == DialogResult.OK)
                    {
                        List<string> list_Confirm = new List<string>();//프로시저 수행 성공여부 값 저장/success/failure
                        foreach (DataGridViewRow dgvr in dgvMoldLoT.Rows)
                        {
                            if (dgvr.Cells["Check"].Value.ToString().ToUpper() == "TRUE")
                            {
                                if (dgvr.Cells["InOutDate"].Value.ToString().Replace("-", "") == DateTime.Now.ToString("yyyyMMdd"))
                                {
                                    Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                                    sqlParameter.Add("InOutID", dgvr.Cells["InOutID"].Value.ToString());
                                    string[] sConfirm = new string[2];
                                    sConfirm = DataStore.Instance.ExecuteProcedure("xp_WizWork_dMoldInOut", sqlParameter, true);
                                    list_Confirm.Add(sConfirm[0]);
                                    if (sConfirm[0].ToUpper() == "SUCCESS")
                                    { deleteCount++; }
                                }
                                else
                                {
                                    c++;
                                }
                            }
                        }
                        if (list_Confirm.Count > 0)//삭제결과 리스트
                        {
                            LogData.LogSave(this.GetType().Name, "D"); //2022-06-22 삭제
                            FillGrid();
                            LogData.LogSave(this.GetType().Name, "R"); //2022-06-22 조회
                            if (c > 0)
                            {
                                WizCommon.Popup.MyMessageBox.ShowBox("현재 날짜와 동일한 작업일자" + deleteCount.ToString() + "건 삭제완료됬습니다." +
                                "\r\n" + c.ToString() + "개 작업건수는 현재 날짜와 동일하지 않아 삭제할 수 없습니다.", "[삭제 완료]", 0, 1);
                            }
                            else
                            {
                                WizCommon.Popup.MyMessageBox.ShowBox(deleteCount.ToString() + "건 삭제완료됬습니다.", "[삭제 완료]", 0, 1);
                            }
                        }
                        else//삭제 결과리스트가 없음 > 삭제를 안했음
                        {
                            if (c > 0)
                            {
                                WizCommon.Popup.MyMessageBox.ShowBox(c.ToString() + "개 작업건수는 현재 날짜와 동일하지 않아 삭제할 수 없습니다.", "[삭제 실패]", 0, 1);
                            }
                            else
                            {
                                WizCommon.Popup.MyMessageBox.ShowBox("삭제실패! 관리자에게 문의하세요", "[삭제 실패]", 0, 1);
                            }
                        }

                        DataStore.Instance.CloseConnection(); //2021-10-07 DB 커넥트 연결 해제
                    }
                }
            }

        }

        #endregion


    }
}
