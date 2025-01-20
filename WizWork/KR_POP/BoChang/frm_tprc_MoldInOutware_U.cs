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
    public partial class frm_tprc_MoldInOutware_U : Form
    {
        string[] Message = new string[2];
        INI_GS gs = Frm_tprc_Main.gs;
        WizWorkLib Lib = Frm_tprc_Main.Lib;
        DataTable dt = null; 
        private DataSet ds = null; //2022-10-21
        int x = 0; //grdlist 그리드 좌우 이동용 변수
        List<string> WorkCallIDList = new List<string>(); //여러개를 한꺼번에 처리할 경우를 위해 리스트 생성 2022-10-21

        LogData LogData = new LogData(); //2022-10-24 log 남기는 함수

        string ArticleID = string.Empty;
        string MoldID = string.Empty;

        public frm_tprc_MoldInOutware_U()
        {
            InitializeComponent();
        }

        //화면 닫기
        private void btnClose_Click(object sender, EventArgs e)
        {
            LogData.LogSave(this.GetType().Name, "S"); //log 남기기(로드 S) 2022-10-24
            this.Close();
        }

        private void frm_tprc_MoldInOutware_U_Load(object sender, EventArgs e)
        {
            LogData.LogSave(this.GetType().Name, "S"); //log 남기기(로드 S) 2022-10-24

            SetScreen();
            InitGrid();
            DataClear();

            mtDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
            dtTime.CustomFormat = "HH:mm:ss";

        }



        #region 레이아웃에 채우기

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
            //tlp_Search_Date.SetRowSpan(chkDate, 2);
        }

        #endregion

        #region Default Grid Setting

        private void InitGrid()
        {
            dgvMold.Columns.Clear();
            dgvMold.ColumnCount = 4;

            int n = 0;
            // Set the Colums Hearder Names

            dgvMold.Columns[n].Name = "MoldNo";
            dgvMold.Columns[n].HeaderText = "금형로트번호";
            dgvMold.Columns[n].Width = 160;
            dgvMold.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvMold.Columns[n++].Visible = true;

            dgvMold.Columns[n].Name = "BuyerArticleNo";
            dgvMold.Columns[n].HeaderText = "품번";
            dgvMold.Columns[n].Width = 90;
            dgvMold.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvMold.Columns[n++].Visible = true;

            dgvMold.Columns[n].Name = "ArticleID";
            dgvMold.Columns[n].HeaderText = "품명";
            dgvMold.Columns[n].Width = 90;
            dgvMold.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvMold.Columns[n++].Visible = true;

            dgvMold.Columns[n].Name = "MoldID";
            dgvMold.Columns[n].HeaderText = "금형명";
            dgvMold.Columns[n].Width = 90;
            dgvMold.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvMold.Columns[n++].Visible = true;

            dgvMold.Font = new Font("맑은 고딕", 12);
            dgvMold.RowTemplate.Height = 30;
            dgvMold.ColumnHeadersHeight = 35;
            dgvMold.ScrollBars = ScrollBars.Both;
            dgvMold.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMold.ReadOnly = true;

            foreach (DataGridViewColumn col in dgvMold.Columns)
            {
                col.DataPropertyName = col.Name;
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            return;


        }

        #endregion

        #region 저장 관련 모음

        private bool SaveData()
        {
            bool flag = false;

            List<Procedure> Prolist = new List<Procedure>();
            List<Dictionary<string, object>> ListParameter = new List<Dictionary<string, object>>();

            try
            {
                for (int i = 0; i < dgvMold.Rows.Count; i++)
                {
                    if (rbSW.Checked == true)
                    {
                        Dictionary<string, object> sqlParameter = new Dictionary<string, object>();

                        sqlParameter.Add("InOutID", ""); // 0401
                        sqlParameter.Add("MoldID", dgvMold.Rows[i].Cells["MoldID"].Value.ToString());
                        sqlParameter.Add("InOutGbn", "3");
                        sqlParameter.Add("InOutQty", 1);
                        sqlParameter.Add("InOutDate", mtDate.Text.Replace("-", "").Replace("/", ""));
                        sqlParameter.Add("InOutPlace", "1");
                        sqlParameter.Add("sComments", "");
                        sqlParameter.Add("InOutPerson", Frm_tprc_Main.g_tBase.PersonID);
                        sqlParameter.Add("CreateUserID", Frm_tprc_Main.g_tBase.PersonID);

                        Procedure pro1 = new Procedure();
                        pro1.Name = "xp_WizWork_dvlMold_iMoldInOut";
                        pro1.OutputUseYN = "N";
                        pro1.OutputName = "InOutID";
                        pro1.OutputLength = "19";

                        Prolist.Add(pro1);
                        ListParameter.Add(sqlParameter);

                        Dictionary<string, object> sqlParameter2 = new Dictionary<string, object>();

                        sqlParameter2.Add("InOutID", ""); // 0401
                        sqlParameter2.Add("MoldID", dgvMold.Rows[i].Cells["MoldID"].Value.ToString());
                        sqlParameter2.Add("InOutGbn", "2");
                        sqlParameter2.Add("InOutQty", 1);
                        sqlParameter2.Add("InOutDate", mtDate.Text.Replace("-", "").Replace("/", ""));
                        sqlParameter2.Add("InOutPlace", "1");
                        sqlParameter2.Add("sComments", "");
                        sqlParameter2.Add("InOutPerson", Frm_tprc_Main.g_tBase.PersonID);
                        sqlParameter2.Add("CreateUserID", Frm_tprc_Main.g_tBase.PersonID);

                        Procedure pro2 = new Procedure();
                        pro2.Name = "xp_WizWork_dvlMold_iMoldInOut";
                        pro2.OutputUseYN = "N";
                        pro2.OutputName = "InOutID";
                        pro2.OutputLength = "19";

                        Prolist.Add(pro2);
                        ListParameter.Add(sqlParameter2);
                    }
                    else
                    {
                        Dictionary<string, object> sqlParameter = new Dictionary<string, object>();

                        sqlParameter.Add("InOutID", ""); // 0401
                        sqlParameter.Add("MoldID", dgvMold.Rows[i].Cells["MoldID"].Value.ToString());

                        if (rbS.Checked == true) //입고
                        {
                            sqlParameter.Add("InOutGbn", "3");
                        }
                        else if (rbW.Checked == true) //세척
                        {
                            sqlParameter.Add("InOutGbn", "2");
                        }
                        else if (rbOut.Checked == true) //출고
                        {
                            sqlParameter.Add("InOutGbn", "1");
                        }

                        sqlParameter.Add("InOutQty", 1);
                        sqlParameter.Add("InOutDate", mtDate.Text.Replace("-", "").Replace("/", ""));
                        sqlParameter.Add("InOutPlace", "1");
                        sqlParameter.Add("sComments", "");
                        sqlParameter.Add("InOutPerson", Frm_tprc_Main.g_tBase.PersonID);
                        sqlParameter.Add("CreateUserID", Frm_tprc_Main.g_tBase.PersonID);

                        Procedure pro1 = new Procedure();
                        pro1.Name = "xp_WizWork_dvlMold_iMoldInOut";
                        pro1.OutputUseYN = "N";
                        pro1.OutputName = "InOutID";
                        pro1.OutputLength = "19";

                        Prolist.Add(pro1);
                        ListParameter.Add(sqlParameter);
                    }
                }
                List<KeyValue> list_Result = new List<KeyValue>();
                list_Result = DataStore.Instance.ExecuteAllProcedureOutputGetCS(Prolist, ListParameter);

                if (list_Result[0].key.ToLower() == "success")
                {
                    LogData.LogSave(this.GetType().Name, "C"); //2022-06-22 저장
                    flag = true;
                    Message[0] = "[저장 성공]";
                    Message[1] = "정상적으로 등록이 되었습니다.";
                    WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                }
                else
                {
                    Message[0] = "[저장 실패]";
                    Message[1] = "오류! 관리자에게 문의";
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                return false;
            }

            return flag;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (rbS.Checked == false && rbW.Checked == false && rbSW.Checked == false && rbOut.Checked == false)
            {
                Message[0] = "[저장 전 확인]";
                Message[1] = "입고, 세척, 입고시 세척, 출고 중 하나를 선택해주세요. ";
                WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                return;
            }

            if (dgvMold.Rows.Count > 0) 
            {
                if (SaveData())
                {
                    dgvMold.Rows.Clear();
                    DataClear();
                }
            }
            else
            {
                Message[0] = "[저장 전 확인]";
                Message[1] = "금형을 입력해주세요.";
                WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                return;
            }
        }


        #endregion

        #region 클릭 이벤트

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

                if (dgvMold.Rows.Count > 0) 
                {
                    for (int i = 0; i < dgvMold.Rows.Count; i++) 
                    {
                        if (dgvMold.Rows[i].Cells["MoldID"].Value.ToString() == MoldID) 
                        {
                            Message[0] = "[확인]";
                            Message[1] = string.Format("이미 입력한 금형 입니다.");
                            WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                            DataClear();
                            return;
                        }
                    }

                    AddGridData(MoldID, ArticleID);
                    DataClear();
                }
                else
                {
                    AddGridData(MoldID, ArticleID);
                    DataClear();
                }
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

                if (dgvMold.Rows.Count > 0)
                {
                    for (int i = 0; i < dgvMold.Rows.Count; i++)
                    {
                        if (dgvMold.Rows[i].Cells["MoldID"].Value.ToString() == MoldID)
                        {
                            Message[0] = "[확인]";
                            Message[1] = string.Format("이미 입력한 금형 입니다.");
                            WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                            DataClear();
                            return;
                        }
                    }

                    AddGridData(MoldID, ArticleID);
                    DataClear();
                }
                else
                {
                    AddGridData(MoldID, ArticleID);
                    DataClear();
                }
            }
        }

        private void btnDate_Click(object sender, EventArgs e)
        {
            LoadCalendar();
        }

        private void btnTime_Click(object sender, EventArgs e)
        {
            TimeCheck("시작시간");
        }

        private void mtDate_Click(object sender, EventArgs e)
        {
            LoadCalendar();
        }

        #endregion

        #region 달력, 시간 입력 함수

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
            if (strTime == "시작시간") { dtTime.Value = dt; }
        }

        #endregion

        #region 조회 함수
        
        private void AddGridData(string MoldID, string ArticleID)
        {
            try
            {
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();

                sqlParameter.Add("MoldID", MoldID);
                sqlParameter.Add("ArticleID", ArticleID);

                ds = DataStore.Instance.ProcedureToDataSet("xp_WizWork_sMoldID", sqlParameter, false);

                if (ds.Tables[0].Rows.Count > 0)
                {

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DataRow dr = ds.Tables[0].Rows[i];
                        dgvMold.Rows.Add(
                            dr["MoldNo"].ToString(),                        //'3)금형로트번호
                            dr["BuyerArticleNo"].ToString(),                //'5)품번
                            dr["Article"].ToString(),                       //'6)품명
                            dr["MoldID"].ToString()                         //금형명
                            );
                        dgvMold.Rows[i].Height = 30;
                    }
                    dgvMold.ClearSelection();
                    //dgvMold[0, 0].Selected = true;
                    //dgvMold.Columns["RowSeq"].Width = 60;
                }
                //Frm_tprc_Main.gv.queryCount = string.Format("{0:n0}", dgvMold.RowCount);
                //Frm_tprc_Main.gv.SetStbInfo();
            }
            catch (Exception excpt)
            {
                Message[0] = "[오류]";
                Message[1] = string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message);
                WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
            }

        }

        #endregion

        #region 데이터 초기화

        private void DataClear()
        {
            txtMoldNo.Text = "";
            MoldID = "";
            txtArticle.Text = "";
            txtBuyerArticleNo.Text = "";
            ArticleID = "";
        }

        #endregion


    }
}
