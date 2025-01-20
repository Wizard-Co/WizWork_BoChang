using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;
using WizCommon;
using WizWork;
using WizWork.Properties;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Threading;


namespace WizIns
{
    public partial class Frm_tprc_tins_xgt_AutoGathering_U : Form
    {
        INI_GS gs = Frm_tprc_Main.gs;
        WizWorkLib Lib = new WizWorkLib();
        Frm_tins_Main Ftm = new Frm_tins_Main(); //2022-06-23                                                
        private string PortNum = string.Empty;
        private bool ConnectIgnore = false;
        string ReceviedData = string.Empty;

        private CheckBox allCheck = new CheckBox();
        CheckBox chkbx = new CheckBox();
        CheckBox chkbx2 = new CheckBox();


        //2022-11-02
        string MachineID = string.Empty;
        string ChanelNum = string.Empty;
        string InspectR1value = string.Empty;
        string InspectR2value = string.Empty;
        string InspectR3value = string.Empty;
        string InspectRvalue = string.Empty;
        string InspectI1value = string.Empty;
        string InspectI2value = string.Empty;
        string InspectI3value = string.Empty;
        string InspectIvalue = string.Empty;
        string Resultvalue = string.Empty;
        string WorkLogComment = string.Empty;
        string WorkLogLOTID = string.Empty;

        string[] Message = new string[2];

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        /// <summary>
        /// 생성
        /// </summary>
        public Frm_tprc_tins_xgt_AutoGathering_U()
        {
            InitializeComponent();
        }

        #region Dock = Fill : SetScreen()
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
        #endregion

        private void Frm_tprc_tins_xgt_AutoGathering_U_Load(object sender, EventArgs e)
        {
            Ftm.LogSave(this.GetType().Name, "S"); //2022-06-23 사용시간(로드, 닫기)
            SetScreen();
            // 데이터 그리드 초기 설정(헤더)
            InitGrid();
            // 데이터 그리드 초기 설정(데이터)
            InitDgvInspect();
            AddHeaderCheckBox();
            AddHeaderCheckBox2();
            openSerialPort();
        }

        #region  그리드 헤더 설정

        private void InitGrid()
        {
            dgvInspect.Columns.Clear();
            dgvInspect.ColumnCount = 13;
            int i = 0;
            
            dgvInspect.Columns[i].Name = "Num";
            dgvInspect.Columns[i].HeaderText = "순";
            dgvInspect.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvInspect.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvInspect.Columns[i].ReadOnly = true;
            dgvInspect.Columns[i++].Visible = true;

            dgvInspect.Columns[i].Name = "LOTID";
            dgvInspect.Columns[i].HeaderText = "공정이동전표";
            dgvInspect.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvInspect.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvInspect.Columns[i].ReadOnly = true;
            dgvInspect.Columns[i++].Visible = true;

            dgvInspect.Columns[i].Name = "InspectR1";
            dgvInspect.Columns[i].HeaderText = "저항1";
            dgvInspect.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvInspect.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvInspect.Columns[i].ReadOnly = true;
            dgvInspect.Columns[i++].Visible = true;

            dgvInspect.Columns[i].Name = "InspectR2";
            dgvInspect.Columns[i].HeaderText = "저항2";
            dgvInspect.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvInspect.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvInspect.Columns[i].ReadOnly = true;
            dgvInspect.Columns[i++].Visible = true;

            dgvInspect.Columns[i].Name = "InspectR3";
            dgvInspect.Columns[i].HeaderText = "저항3";
            dgvInspect.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvInspect.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvInspect.Columns[i].ReadOnly = true;
            dgvInspect.Columns[i++].Visible = true;

            dgvInspect.Columns[i].Name = "InspectR";
            dgvInspect.Columns[i].HeaderText = "저항(Tol%)";
            dgvInspect.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvInspect.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvInspect.Columns[i].ReadOnly = true;
            dgvInspect.Columns[i++].Visible = true;

            dgvInspect.Columns[i].Name = "InspectI1";
            dgvInspect.Columns[i].HeaderText = "인덕턴스1";
            dgvInspect.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvInspect.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvInspect.Columns[i].ReadOnly = true;
            dgvInspect.Columns[i++].Visible = true;

            dgvInspect.Columns[i].Name = "InspectI2";
            dgvInspect.Columns[i].HeaderText = "인덕턴스2";
            dgvInspect.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvInspect.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvInspect.Columns[i].ReadOnly = true;
            dgvInspect.Columns[i++].Visible = true;

            dgvInspect.Columns[i].Name = "InspectI3";
            dgvInspect.Columns[i].HeaderText = "인덕턴스3";
            dgvInspect.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvInspect.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvInspect.Columns[i].ReadOnly = true;
            dgvInspect.Columns[i++].Visible = true;

            dgvInspect.Columns[i].Name = "InspectI";
            dgvInspect.Columns[i].HeaderText = "인덕턴스(Tol%)";
            dgvInspect.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvInspect.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvInspect.Columns[i].ReadOnly = true;
            dgvInspect.Columns[i++].Visible = true;

            dgvInspect.Columns[i].Name = "Puncture";
            dgvInspect.Columns[i].HeaderText = "Puncture";
            dgvInspect.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvInspect.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvInspect.Columns[i].ReadOnly = true;
            dgvInspect.Columns[i++].Visible = true;

            dgvInspect.Columns[i].Name = "Result";
            dgvInspect.Columns[i].HeaderText = "결과";
            dgvInspect.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvInspect.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvInspect.Columns[i].ReadOnly = true;
            dgvInspect.Columns[i++].Visible = true;

            dgvInspect.Columns[i].Name = "MachineID";
            dgvInspect.Columns[i].HeaderText = "호기";
            dgvInspect.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvInspect.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvInspect.Columns[i].ReadOnly = true;
            dgvInspect.Columns[i++].Visible = false;

            DataGridViewCheckBoxColumn chkCol = new DataGridViewCheckBoxColumn();
            {
                chkCol.HeaderText = "자주";
                chkCol.Name = "Check";
                chkCol.Width = 110;
                //chkCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                chkCol.FlatStyle = FlatStyle.Standard;
                chkCol.ThreeState = true;
                chkCol.CellTemplate = new DataGridViewCheckBoxCell();
                chkCol.CellTemplate.Style.BackColor = Color.Beige;
                chkCol.Visible = true;
            }
            dgvInspect.Columns.Insert(13, chkCol); //2021-06-22 check는 따로 인서트해서 순서를 여기 수정

            DataGridViewCheckBoxColumn chkCol2 = new DataGridViewCheckBoxColumn();
            {
                chkCol2.HeaderText = "출하";
                chkCol2.Name = "Check2";
                chkCol2.Width = 110;
                //chkCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                chkCol2.FlatStyle = FlatStyle.Standard;
                chkCol2.ThreeState = true;
                chkCol2.CellTemplate = new DataGridViewCheckBoxCell();
                chkCol2.CellTemplate.Style.BackColor = Color.Beige;
                chkCol2.Visible = true;
            }
            dgvInspect.Columns.Insert(14, chkCol2); //2021-06-22 check는 따로 인서트해서 순서를 여기 수정

            DataGridViewButtonColumn btncol = new DataGridViewButtonColumn();
            {
                btncol.UseColumnTextForButtonValue = true;
                btncol.Text = "삭제";
                btncol.Name = "삭제";              
            }
            dgvInspect.Columns.Insert(15, btncol); //2021-06-22 check는 따로 인서트해서 순서를 여기 수정

            dgvInspect.Font = new Font("맑은 고딕", 7, FontStyle.Bold);
            dgvInspect.RowTemplate.Height = 37;
            dgvInspect.ColumnHeadersHeight = 35;
            dgvInspect.ScrollBars = ScrollBars.Both;
            //dgvInspect.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvInspect.ReadOnly = true;
            dgvInspect.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(234, 234, 234);
            dgvInspect.ScrollBars = ScrollBars.Both;
            dgvInspect.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvInspect.MultiSelect = false;


            //allCheck.Name = "allCheck";
            //allCheck.CheckedChanged += new EventHandler(AllCheckClick);
            //allCheck.Size = new Size(13, 13);
            //allCheck.Location = new Point(dgvInspect.RowHeadersWidth + dgvInspect.Columns[13].Width - 20, (dgvInspect.ColumnHeadersHeight / 2) - (allCheck.Height / 2));
            //dgvInspect.Controls.Add(allCheck); // DataGridView에 CheckBox 추가( 헤더용 )
   
            foreach (DataGridViewColumn col in dgvInspect.Columns)
            {
                if (col.Name == "Check" || col.Name == "Check2") 
                {
                    col.DataPropertyName = col.Name;
                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    col.SortMode = DataGridViewColumnSortMode.NotSortable;
                    //col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
                else
                {
                    col.DataPropertyName = col.Name;
                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    col.SortMode = DataGridViewColumnSortMode.NotSortable;
                    //col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            return;
        }

        #endregion

        //private void AllCheckClick(object sender, EventArgs e)
        //{
        //        if (allCheck.Checked)
        //            for (int i = 0; i < dgvInspect.Rows.Count; i++)
        //                dgvInspect.Rows[i].Cells[14].Value = true;
        //        else
        //            for (int i = 0; i < dgvInspect.Rows.Count; i++)
        //                dgvInspect.Rows[i].Cells[14].Value = false;
        //        dgvInspect.EndEdit(DataGridViewDataErrorContexts.Commit); // << 이거 안할경우 선택된 Cell이 CheckBoxCell일 경우 변화가 없는것처럼 보임
            
        //}



        #region 그리드 초기 설정
        private void InitDgvInspect()
        {
            for (int i = 0; i < 10; i++) //최대 10개로 고정
            {
                dgvInspect.Rows.Add((i + 1).ToString() // 순번
                                           , ""
                                           , ""
                                           , ""
                                           , ""
                                           , ""
                                           , ""
                                           , ""
                                           , ""
                                           , ""
                                           , ""
                                           , ""
                                           , ""
                                           ,false
                                           ,false);
            }
        }
        #endregion

        #region 저장, 닫기 이벤트

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckData()) 
            {
                if (SaveData())
                {
                    for (int i = 0; i < dgvInspect.Rows.Count; i++)
                    {
                        dgvInspect.Rows[i].SetValues((i + 1).ToString()
                                                    , ""
                                                    , ""
                                                    , ""
                                                    , ""
                                                    , ""
                                                    , ""
                                                    , ""
                                                    , ""
                                                    , ""
                                                    , ""
                                                    , ""
                                                    , ""                                                  
                                                    ,false
                                                    ,false);
                    }
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Ftm.LogSave(this.GetType().Name, "S"); //2022-06-23 사용시간(로드, 닫기)
            serialPort1.Close();
            Close();
        }

        #endregion

        #region 저장시 저장함수, 체크함수

        private bool SaveData()
        {
            List<string> ProcedureInfo = null;
            List<List<string>> ListProcedureName = new List<List<string>>();
            List<Dictionary<string, object>> ListParameter = new List<Dictionary<string, object>>();

            bool blResult = false;

            try 
            {
                for (int i = 0; i < dgvInspect.Rows.Count; i++) 
                {
                    if (dgvInspect.Rows[i].Cells["LOTID"].Value.ToString() != "" && dgvInspect.Rows[i].Cells["Result"].Value.ToString() != "")
                    {
                        Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                        sqlParameter.Add("INSPECTID", "");                                                      //pk
                        sqlParameter.Add("sLOTID", dgvInspect.Rows[i].Cells["LOTID"].Value.ToString());         //LOTID
                        sqlParameter.Add("sProcessID", "7171");                                                 //ProcessID
                        sqlParameter.Add("sMachineID", dgvInspect.Rows[i].Cells["MachineID"].Value.ToString()); //MachineID

                        //자주, 출하 체크로 저장
                        if (dgvInspect.Rows[i].Cells["Check"].Value.ToString().ToUpper() == "TRUE") 
                        {
                            sqlParameter.Add("sInspectPoint", "9");
                        }
                        else
                        {
                            sqlParameter.Add("sInspectPoint", "5");
                        }

                        sqlParameter.Add("Result", dgvInspect.Rows[i].Cells["Result"].Value.ToString());        //Result GOOD, N.G
                        sqlParameter.Add("sPerson", Frm_tins_Main.g_tBase.PersonID);                            //PersonID

                        //ProcedureInfo List
                        //0번 : 프로시저 이름, 1번 output yn, 2번 output 이름, 3번 output size(output y일때만)
                        ProcedureInfo = new List<string>();
                        ProcedureInfo.Add("xp_WizIns_iAutoInspectG");
                        ProcedureInfo.Add("Y");
                        ProcedureInfo.Add("INSPECTID");
                        ProcedureInfo.Add("12");

                        ListProcedureName.Add(ProcedureInfo);
                        ListParameter.Add(sqlParameter);
                        ProcedureInfo = null;
                        for (int n = 2; n < 12; n++) //측정값만 가져오기 위해 순, 공정이동전표 빼고 나머지 값 입력
                        {
                            Dictionary<string, object> sqlParameter2 = new Dictionary<string, object>();
                            sqlParameter2.Add("INSPECTID", "");
                            sqlParameter2.Add("SEQ", 0);
                            sqlParameter2.Add("Column", dgvInspect.Columns[n].Name);
                            if (n == 10 || n == 11) //마지막 Result는 string으로 처리 되어 조건 추가
                            {
                                sqlParameter2.Add("InspectValue", 0);
                            }
                            else
                            {
                                sqlParameter2.Add("InspectValue", dgvInspect[n, i].Value.ToString());
                            }
                            sqlParameter2.Add("Result", dgvInspect.Rows[i].Cells["Result"].Value.ToString());
                            sqlParameter2.Add("sPerson", Frm_tins_Main.g_tBase.PersonID);
                            //ProcedureInfo List
                            //0번 : 프로시저 이름, 1번 output yn, 2번 output 이름, 3번 output size(output y일때만)
                            ProcedureInfo = new List<string>();
                            ProcedureInfo.Add("xp_WizIns_iAutoInspectSubG");
                            ProcedureInfo.Add("N");
                            ProcedureInfo.Add("INSPECTID");
                            ProcedureInfo.Add("12");

                            ListProcedureName.Add(ProcedureInfo);
                            ListParameter.Add(sqlParameter2);
                            ProcedureInfo = null;
                        }
                    }
                }

                string[] Confirm = new string[2];
                Confirm = DataStore.Instance.ExecuteAllProcedureOutput(ListProcedureName, ListParameter);
                if (Confirm[0].ToLower() == "success")
                {
                    Ftm.LogSave(this.GetType().Name, "C"); //2022-06-23 저장
                    blResult = true;
                }
                else
                {
                    Message[0] = "[저장실패]";
                    Message[1] = Confirm[1].ToString();
                    WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                    return blResult;
                }
            }
            catch (Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", ex.Message), "[오류]", 0, 1);
            }
            DataStore.Instance.CloseConnection(); //2021-10-07 DB 커넥트 연결 해제
            return blResult;
        }

        private bool CheckData()
        {
            int LOTIDCount = 0;
            int ResultCount = 0;

            if (dgvInspect.Rows[0].Cells["LOTID"].Value.ToString() != "")
            {
                for (int i = 0; i < dgvInspect.Rows.Count; i++)
                {

                    if (dgvInspect.Rows[i].Cells["LOTID"].Value.ToString() == "")
                    {
                        LOTIDCount++;
                    }

                    if (dgvInspect.Rows[i].Cells["Result"].Value.ToString() == "")
                    {
                        ResultCount++;
                    }
                }

                if (LOTIDCount != ResultCount)
                {
                    WizCommon.Popup.MyMessageBox.ShowBox("데이터를 입력해 주세요.", "[오류]", 0, 1);
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                WizCommon.Popup.MyMessageBox.ShowBox("데이터를 입력해 주세요.", "[오류]", 0, 1);
                return false;
            }

            for (int i = 0; i < dgvInspect.Rows.Count; i++)
            {
                if (dgvInspect.Rows[i].Cells["LOTID"].Value.ToString() != ""  && dgvInspect.Rows[i].Cells[dgvInspect.Columns["Check"].Index].Value.ToString().ToUpper() == "FALSE" 
                    && dgvInspect.Rows[i].Cells[dgvInspect.Columns["Check2"].Index].Value.ToString().ToUpper() == "FALSE")
                {
                    WizCommon.Popup.MyMessageBox.ShowBox((i + 1) + " 번 자주, 출하를 선택해 주세요", "[오류]", 0, 1);
                    return false;
                }
            }

            return true;
        }

        #endregion

        #region Port설정 및 데이터 받기

        private string SetPortNUM()
        {
            StringBuilder port = new StringBuilder();
            GetPrivateProfileString("COMPort", "Inspect", "(NONE)", port, 10, ConnectionInfo.filePath);
            return port.ToString();
        }

        public bool openSerialPort()
        {
            bool flagYN = false;

            try
            {
                if (!serialPort1.IsOpen)
                {
                    PortNum = SetPortNUM();

                    if (PortNum != null && PortNum != "")
                    {

                        serialPort1.PortName = PortNum;
                        serialPort1.BaudRate = 115200;
                        serialPort1.DataBits = 8;
                        serialPort1.Parity = Parity.None;
                        serialPort1.StopBits = StopBits.One;
                        //serialPort1.DataReceived += new SerialDataReceivedEventHandler(serialPort1_DataReceived);
                        serialPort1.Open();

                        System.Diagnostics.Debug.WriteLine("포트 연결됨");
                        flagYN = true;

                    }
                    else
                    {
                        string txt = "포트가 빠져 있습니다. 포트 꼽은 후 환경설정에서 설정해주세요";
                        WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", txt), "[오류]", 0, 1);

                        System.Diagnostics.Debug.WriteLine("포트 연결 실패");
                    }

                }
                else
                {

                    string txt = "이미 연결 되어 있거나 혹은 포트가 빠져 있습니다. 다시 확인해주세요";
                    WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", txt), "[오류]", 0, 1);

                }


            }
            catch (Exception ex)
            {
                if (ConnectIgnore == false)
                {
                    WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", ex.Message), "[오류]", 0, 1);
                }
            }
            finally
            {
                System.Diagnostics.Debug.WriteLine("포트 연결 종료");
            }

            return flagYN;
        }

        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                if (serialPort1.IsOpen)
                {
                    //통신값이 받아 지면 출력 하는 메소드
                    if (serialPort1.BytesToRead > 0)
                    {
                        ReceviedData = serialPort1.ReadLine();

                        CheckForIllegalCrossThreadCalls = false;

                        GetDataByString(ReceviedData);
                    }
                }
            }
            catch (TimeoutException) { }
            //2022-04-01 스레드 예외처리(스레드 종료 또는 응용 프로그램 요청 때문에 I/O 작업이 취소되었습니다)
            catch (System.IO.IOException)
            {
                return;
            }
            catch (System.ComponentModel.Win32Exception)
            {
                return;
            }
            catch (System.InvalidOperationException)
            {
                return;
            }
            catch (System.NullReferenceException)
            {
                return;
            }
        }

        private void GetDataByString(string str)
        {
            
            MachineID = string.Empty;
            InspectR1value = string.Empty;
            InspectR2value = string.Empty;
            InspectR3value = string.Empty;
            InspectRvalue = string.Empty;
            InspectI1value = string.Empty;
            InspectI2value = string.Empty;
            InspectI3value = string.Empty;
            InspectIvalue = string.Empty;
            Resultvalue = string.Empty;
            WorkLogComment = string.Empty;
            WorkLogLOTID = string.Empty;

            if (str.Length > 3)
            {
                //WorkLog에 넣을 Comment
                WorkLogComment = str;

                //띄워쓰기 3번째까지 자르기 3번째 다음부터 데이터 값
                for (int i = 0; i < 3; i++) 
                {
                    if (i == 0)
                    {
                        //MachineID 자르기
                        MachineID = str.Substring(0, str.IndexOf("\t"));
                        MachineID = MachineID.Replace("\u0002","");                    
                        str = str.Substring(str.IndexOf("\t") + 1);
                    }
                    else if(i == 2)
                    {
                        //ChanelNum 자르기
                        ChanelNum = str.Substring(0, str.IndexOf("\t")).Trim();
                        str = str.Substring(str.IndexOf("\t") + 1);
                    }
                    else
                    {
                        str = str.Substring(str.IndexOf("\t") + 1);
                    }
                }

                for(int i = 0; i < 9; i++)
                {
                    if (i == 0)
                    {
                        InspectR1value = str.Substring(0, str.IndexOf("\t"));
                        str = str.Substring(str.IndexOf("\t") + 1);
                    }
                    else if(i == 1)
                    {
                        InspectR2value = str.Substring(0, str.IndexOf("\t"));
                        str = str.Substring(str.IndexOf("\t") + 1);
                    }
                    else if (i == 2)
                    {
                        InspectR3value = str.Substring(0, str.IndexOf("\t"));
                        str = str.Substring(str.IndexOf("\t") + 1);
                    }
                    else if (i == 3)
                    {
                        InspectRvalue = str.Substring(0, str.IndexOf("\t"));
                        str = str.Substring(str.IndexOf("\t") + 1);
                    }
                    else if (i == 4)
                    {
                        InspectI1value = str.Substring(0, str.IndexOf("\t"));
                        str = str.Substring(str.IndexOf("\t") + 1);
                    }
                    else if (i == 5)
                    {
                        InspectI2value = str.Substring(0, str.IndexOf("\t"));
                        str = str.Substring(str.IndexOf("\t") + 1);
                    }
                    else if (i == 6)
                    {
                        InspectI3value = str.Substring(0, str.IndexOf("\t"));
                        str = str.Substring(str.IndexOf("\t") + 1);
                    }
                    else if (i == 7)
                    {
                        InspectIvalue = str.Substring(0, str.IndexOf("\t"));
                        str = str.Substring(str.IndexOf("\t") + 1);
                    }
                    else
                    {
                        if (str.Trim().Substring(0,1) == "G")
                        {
                            Resultvalue = str.Trim().Substring(0, 4);
                        }
                        else if (str.Trim().Substring(0, 1) == "N")
                        {
                            Resultvalue = str.Trim().Substring(0, 3);
                        }
                    }
                }

                for (int i = 0; i < dgvInspect.Rows.Count; i++)
                {
                    //ChanelNum과 순번이 같고 LOTID가 빈 값이 아니면 입력되게
                    if (dgvInspect.Rows[i].Cells["Num"].Value.ToString() == ChanelNum && dgvInspect.Rows[i].Cells["LOTID"].Value.ToString() != "") //&& dgvInspect.Rows[i].Cells["LOTID"].Value.ToString() != ""
                    {
                        dgvInspect.Rows[i].Cells["InspectR1"].Value = InspectR1value;
                        dgvInspect.Rows[i].Cells["InspectR2"].Value = InspectR2value;
                        dgvInspect.Rows[i].Cells["InspectR3"].Value = InspectR3value;
                        dgvInspect.Rows[i].Cells["InspectR"].Value = InspectRvalue;
                        dgvInspect.Rows[i].Cells["InspectI1"].Value = InspectI1value;
                        dgvInspect.Rows[i].Cells["InspectI2"].Value = InspectI2value;
                        dgvInspect.Rows[i].Cells["InspectI3"].Value = InspectI3value;
                        dgvInspect.Rows[i].Cells["InspectI"].Value = InspectIvalue;
                        dgvInspect.Rows[i].Cells["Result"].Value = Resultvalue;
                        dgvInspect.Rows[i].Cells["MachineID"].Value = MachineID;

                        WorkLogLOTID = dgvInspect.Rows[i].Cells["LOTID"].Value.ToString();

                    }
                }


            }          
            
        }


        #endregion

        #region 바코드 스캔 이벤트(버튼 클릭, 텍스트박스 엔터)

        private void btnScan_Click(object sender, EventArgs e)
        {
            loadKeyboard(txtBarcode, "라벨");

            if (txtBarcode.Text.Trim().Length > 0)
            {
                BarcodeScan(txtBarcode.Text.Trim().ToUpper());
                txtBarcode.Text = "";
                txtBarcode.Focus();
            }
        }

        private void txtBarcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (txtBarcode.Text.Trim().Length > 0)
                {
                    BarcodeScan(txtBarcode.Text.Trim().ToUpper());
                    txtBarcode.Text = "";
                    txtBarcode.Focus();
                }
            }
        }

        #endregion

        #region 키패드 함수

        private void loadKeyboard(TextBox txtSender, string title)
        {
            txtSender.Text = "";
            WizWork.POPUP.Frm_CMKeypad keypad = new WizWork.POPUP.Frm_CMKeypad(title + "입력", title);

            keypad.Owner = this;
            if (keypad.ShowDialog() == DialogResult.OK)
            {
                txtSender.Text = keypad.tbInputText.Text;
            }
        }

        #endregion

        #region 바코드 스캔 함수
        private void BarcodeScan(string LabelID)
        {
            try
            {
                if (CheckScanData(LabelID) == false) { return; }

                // 해당 라벨이 이미 등록 되었다면!

                //앞공정의 실적을 체크한다. 없을 시 close한다. ex)2차가류에 데이터가 없는데, 
                Dictionary<string, object> sqlParameters = new Dictionary<string, object>();
                sqlParameters.Add("LabelID", LabelID);
                DataTable dt = DataStore.Instance.ProcedureToDataTable("[xp_prdIns_swkResult]", sqlParameters, false);

                if (dt != null
                    && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dgvInspect.Rows.Count; i++ ) 
                    {
                        if (dgvInspect.Rows[i].Cells["LOTID"].Value.ToString() == "") 
                        {
                            dgvInspect.Rows[i].SetValues((i + 1).ToString()
                                                        , LabelID
                                                        , ""
                                                        , ""
                                                        , ""
                                                        , ""
                                                        , ""
                                                        , ""
                                                        , ""
                                                        , ""
                                                        , ""
                                                        , ""
                                                        , "");
                            break;
                        }
                    }

                }
                else
                {
                    //앞공정의 실적을 체크한다. 없을 시 close한다. ex)2차가류에 데이터가 없는데, 
                    sqlParameters = new Dictionary<string, object>();
                    sqlParameters.Add("LabelID", LabelID);
                    dt = DataStore.Instance.ProcedureToDataTable("[xp_prdIns_sCheckLabelID]", sqlParameters, false);

                    if (dt != null
                        && dt.Rows.Count > 0
                        && dt.Columns.Count == 2)
                    {
                        DataRow dr = dt.Rows[0];

                        string Title = dr["Title"].ToString();
                        string Msg = dr["Msg"].ToString().Replace("|", "\r\n");

                        WizCommon.Popup.MyMessageBox.ShowBox(Msg, Title, 0, 1);
                    }
                    else
                    {
                        WizCommon.Popup.MyMessageBox.ShowBox("해당 라벨 정보를 찾을 수 없습니다.", "[오류]", 0, 1);
                    }
                }

            }
            catch (Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", ex.Message), "[오류]", 0, 1);
            }
            finally
            {
                DataStore.Instance.CloseConnection(); //2021-10-07 DB 커넥트 연결 해제
            }
        }

        //바코드 체크
        private bool CheckScanData(string LabelID)
        {
            // 바코드 길이
            if (LabelID.Length != 11)
            {
                WizCommon.Popup.MyMessageBox.ShowBox("바코드 문자 길이를 확인해주세요.\r\n[ex) C2009160001 : 11자리]", "[바코드 오류]", 0, 1);
                return false;
            }

            if (!LabelID.Contains("C"))
            {
                WizCommon.Popup.MyMessageBox.ShowBox("C라벨을 스캔 해주세요\r\n[ex) C2009160001 : 11자리]", "[바코드 오류]", 0, 1);
                return false;
            }

            return true;
        }

        #endregion

        #region 데이터 통신 시 WorkLog에 입력하는 함수

        private void iWorkLog()
        {
            Dictionary<string, object> sqlParameter = new Dictionary<string, object>();

            List<Procedure> Prolist = new List<Procedure>();
            List<Dictionary<string, object>> ListParameter = new List<Dictionary<string, object>>();


            sqlParameter = new Dictionary<string, object>();
            sqlParameter.Clear();
            sqlParameter.Add("sProcessID", "7171");                             // 2022-11-02 ProcessID            
            sqlParameter.Add("sMachineID", MachineID);                               // 2022-11-02 MachineID 
            sqlParameter.Add("sLoTID", WorkLogLOTID);                           // 2022-11-02 LOTID
            sqlParameter.Add("sComment", WorkLogComment);                       // 2022-11-02 Comment 데이터 값 전부 입력
            sqlParameter.Add("sPerson", Frm_tins_Main.g_tBase.PersonID);        // 2022-11-02 작업자

            Procedure pro1 = new Procedure();
            pro1.Name = "xp_iWorkLogWinForm_Inspect";

            Prolist.Add(pro1);
            ListParameter.Add(sqlParameter);

            List<KeyValue> list_Result = new List<KeyValue>();
            list_Result = DataStore.Instance.ExecuteAllProcedureOutputGetCS(Prolist, ListParameter);

            if (list_Result[0].key.ToLower() == "success")
            {
                DataStore.Instance.CloseConnection(); //2021-09-23 DB 커넥트 연결 해제
                return;
            }
            else
            {
                DataStore.Instance.CloseConnection(); //2021-09-23 DB 커넥트 연결 해제
                return;
            }
        }

        #endregion

        #region SerialPort 재연결 이벤트

        private void btnSerialPort_Click(object sender, EventArgs e)
        {
            openSerialPort();
        }

        #endregion

        private void dgvInspect_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int checkcount = 0;

            if (e.RowIndex > -1)
            {
                if (e.ColumnIndex == 13)
                {
                    if (dgvInspect.Rows[e.RowIndex].Cells[dgvInspect.Columns["Check"].Index].Value.ToString().ToUpper() == "FALSE")
                    {
                        dgvInspect.Rows[e.RowIndex].Cells[dgvInspect.Columns["Check"].Index].Value = true;
                        dgvInspect.Rows[e.RowIndex].Cells[dgvInspect.Columns["Check2"].Index].Value = false;
                    }
                    else if (dgvInspect.Rows[e.RowIndex].Cells[dgvInspect.Columns["Check"].Index].Value.ToString().ToUpper() == "TRUE")
                    {
                        dgvInspect.Rows[e.RowIndex].Cells[dgvInspect.Columns["Check"].Index].Value = false;
                    }

                    for (int i = 0; i < dgvInspect.Rows.Count; i++)
                    {
                        if(dgvInspect.Rows[i].Cells[dgvInspect.Columns["Check"].Index].Value.ToString().ToUpper() == "FALSE")
                        {
                            chkbx.Checked = false;
                            chkbx2.Checked = false;
                            checkcount = 1;
                        }
                    }

                    if(checkcount == 0)
                    {
                        chkbx.Checked = true;
                    }

                }
                else if (e.ColumnIndex == 14) 
                {
                    if (dgvInspect.Rows[e.RowIndex].Cells[dgvInspect.Columns["Check2"].Index].Value.ToString().ToUpper() == "FALSE")
                    {
                        dgvInspect.Rows[e.RowIndex].Cells[dgvInspect.Columns["Check2"].Index].Value = true;
                        dgvInspect.Rows[e.RowIndex].Cells[dgvInspect.Columns["Check"].Index].Value = false;
                    }
                    else if (dgvInspect.Rows[e.RowIndex].Cells[dgvInspect.Columns["Check2"].Index].Value.ToString().ToUpper() == "TRUE")
                    {
                        dgvInspect.Rows[e.RowIndex].Cells[dgvInspect.Columns["Check2"].Index].Value = false;
                    }


                    for (int i = 0; i < dgvInspect.Rows.Count; i++)
                    {
                        if (dgvInspect.Rows[i].Cells[dgvInspect.Columns["Check2"].Index].Value.ToString().ToUpper() == "FALSE")
                        {
                            chkbx.Checked = false;
                            chkbx2.Checked = false;
                            checkcount = 1;
                        }

                    }

                    if (checkcount == 0)
                    {
                        chkbx2.Checked = true;
                    }


                }
                else if(e.ColumnIndex == 15)
                {
                    dgvInspect.Rows[e.RowIndex].SetValues((e.RowIndex + 1).ToString()
                                                   , ""
                                                   , ""
                                                   , ""
                                                   , ""
                                                   , ""
                                                   , ""
                                                   , ""
                                                   , ""
                                                   , ""
                                                   , ""
                                                   , ""
                                                   , ""
                                                   , false
                                                   , false);
                }
            }
            
        }


        #region 헤더에 체크박스 생성 및 이벤트

        private void AddHeaderCheckBox()
        {
            chkbx = new CheckBox();
            chkbx.Size = new Size(15, 15);
            this.dgvInspect.Controls.Add(chkbx);

            //set position of check box

            Rectangle oRectangle = this.dgvInspect.GetCellDisplayRectangle(0, -1, true);

            Point oPoint = new Point();
            //Set position of checkbox according to requirment where we want to display        the checkbox
            oPoint.X = 830;
            oPoint.Y = 4;
            //Change the location of the CheckBox to make it stay on the header
            chkbx.Location = oPoint;
            chkbx.MouseClick += new MouseEventHandler(chkbx_MouseClick);
        }

        private void AddHeaderCheckBox2()
        {
            chkbx2 = new CheckBox();
            chkbx2.Size = new Size(15, 15);
            this.dgvInspect.Controls.Add(chkbx2);

            //set position of check box

            Rectangle oRectangle = this.dgvInspect.GetCellDisplayRectangle(0, -1, true);

            Point oPoint = new Point();
            //Set position of checkbox according to requirment where we want to display        the checkbox
            oPoint.X = 890;
            oPoint.Y = 4;
            //Change the location of the CheckBox to make it stay on the header
            chkbx2.Location = oPoint;
            chkbx2.MouseClick += new MouseEventHandler(chkbx2_MouseClick);
        }

        private void chkbx_MouseClick(object sender, MouseEventArgs e)
        {
            HeaderCheckBoxClick((CheckBox)sender);
        }

        private void chkbx2_MouseClick(object sender, MouseEventArgs e)
        {
            HeaderCheckBox2Click((CheckBox)sender);
        }

        private void HeaderCheckBoxClick(CheckBox HCheckBox)
        {
            foreach (DataGridViewRow Row in dgvInspect.Rows)
            {
                ((DataGridViewCheckBoxCell)Row.Cells["Check"]).Value = HCheckBox.Checked;
                ((DataGridViewCheckBoxCell)Row.Cells["Check2"]).Value = false;
            }

            chkbx2.Checked = false;
            dgvInspect.RefreshEdit();
        }

        private void HeaderCheckBox2Click(CheckBox HCheckBox)
        {
            foreach (DataGridViewRow Row in dgvInspect.Rows)
            {
                ((DataGridViewCheckBoxCell)Row.Cells["Check2"]).Value = HCheckBox.Checked;
                ((DataGridViewCheckBoxCell)Row.Cells["Check"]).Value = false;               
            }

            chkbx.Checked = false;
            dgvInspect.RefreshEdit();
        }

        #endregion

    }

}
