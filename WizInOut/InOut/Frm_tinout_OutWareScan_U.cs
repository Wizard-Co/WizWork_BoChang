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

namespace WizInOut
{
    public partial class Frm_tinout_OutWareScan_U : Form
    {
        WizWorkLib Lib = new WizWorkLib();
        string[] Message = new string[2];
        string u_OutwareID = "";
        private DataSet ds = null;
        LogData LogData = new LogData(); //2022-10-24 log 남기는 함수
        //int u_OutSeq = 0;

        int index = 0; //콤보박스 데이터 찾기용 변수

        public Frm_tinout_OutWareScan_U()
        {
            InitializeComponent();
        }

        public Frm_tinout_OutWareScan_U(string OutwareID)
        {
            InitializeComponent();
            this.u_OutwareID = OutwareID;
            //this.u_OutSeq = OutSeq;
        }

        private void frm_tprc_OutWareScan_U_Load(object sender, EventArgs e)
        {
            LogData.LogSave(this.GetType().Name, "S"); //log 남기기(로드 S) 2022-10-24
            //데이터 그리드 컬럼 초기화
            InitgrdInsItemGrid();

            SetComboBox();

            //출고구분
            //제품출고

            //창고
            //사내창고


            if (u_OutwareID != "")
            {
                //수정시 데이터 가져오기
                UFillGrid();

            }
            else
            {
                //출고일자
                mtb_ODate.Text = DateTime.Today.ToString("yyyy-MM-dd");
            }

        }

        #region 그리드 초기화

        //그리드 초기화
        private void InitgrdInsItemGrid()
        {
            dgvOutware.Columns.Clear();
            dgvOutware.ColumnCount = 12;

            int i = 0;

            // Set the Colums Hearder Names
            dgvOutware.Columns[i].Name = "No";
            dgvOutware.Columns[i].HeaderText = "";
            dgvOutware.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            dgvOutware.Columns[++i].Name = "LOTID";
            dgvOutware.Columns[i].HeaderText = "라벨번호";
            dgvOutware.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvOutware.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dgvOutware.Columns[i].Visible = true;

            dgvOutware.Columns[++i].Name = "ArticleGroup";
            dgvOutware.Columns[i].HeaderText = "품명그룹";
            dgvOutware.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvOutware.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvOutware.Columns[i].Visible = false;

            dgvOutware.Columns[++i].Name = "BuyerArticleNo";
            dgvOutware.Columns[i].HeaderText = "품명";
            dgvOutware.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvOutware.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvOutware.Columns[i].Visible = false;

            dgvOutware.Columns[++i].Name = "Article";
            dgvOutware.Columns[i].HeaderText = "품번";
            dgvOutware.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvOutware.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvOutware.Columns[i].Visible = false;

            dgvOutware.Columns[++i].Name = "Qty";
            dgvOutware.Columns[i].HeaderText = "수량";
            dgvOutware.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvOutware.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dgvOutware.Columns[i].Visible = true;

            dgvOutware.Columns[++i].Name = "UnitClssName";
            dgvOutware.Columns[i].HeaderText = "단위";
            dgvOutware.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvOutware.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvOutware.Columns[i].Visible = true;

            //dgvOutware.Columns[++i].Name = "Loc";
            //dgvOutware.Columns[i].HeaderText = "창고";
            //dgvOutware.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //dgvOutware.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            //dgvOutware.Columns[i].Visible = true;

            //dgvOutware.Columns[++i].Name = "LocID";
            //dgvOutware.Columns[i].HeaderText = "LocID";
            //dgvOutware.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            //dgvOutware.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            //dgvOutware.Columns[i].Visible = false;

            dgvOutware.Columns[++i].Name = "ArticleGroupID";
            dgvOutware.Columns[i].HeaderText = "ArticleGroupID";
            dgvOutware.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvOutware.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvOutware.Columns[i].Visible = false;

            dgvOutware.Columns[++i].Name = "ArticleID";
            dgvOutware.Columns[i].HeaderText = "ArticleID";
            dgvOutware.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvOutware.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvOutware.Columns[i].Visible = false;

            dgvOutware.Columns[++i].Name = "UnitClss";
            dgvOutware.Columns[i].HeaderText = "UnitClss";
            dgvOutware.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvOutware.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvOutware.Columns[i].Visible = false;

            dgvOutware.Columns[++i].Name = "OrderID";
            dgvOutware.Columns[i].HeaderText = "OrderID";
            dgvOutware.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvOutware.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvOutware.Columns[i].Visible = false;

            dgvOutware.Columns[++i].Name = "OrderSeq";
            dgvOutware.Columns[i].HeaderText = "OrderSeq";
            dgvOutware.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvOutware.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvOutware.Columns[i].Visible = false;

            dgvOutware.Rows.Clear();
            dgvOutware.RowTemplate.Height = 33;

            DataGridViewButtonColumn btncol = new DataGridViewButtonColumn();
            {
                btncol.UseColumnTextForButtonValue = true;
                btncol.Text = "삭제";
                btncol.Name = "삭제";
            }
            dgvOutware.Columns.Insert(7, btncol); //2021-06-22 check는 따로 인서트해서 순서를 여기 수정

            dgvOutware.ReadOnly = true;
            dgvOutware.Font = new Font("맑은 고딕", 10, FontStyle.Bold);
            dgvOutware.RowTemplate.Height = 30;
            dgvOutware.ColumnHeadersHeight = 35;
            dgvOutware.ScrollBars = ScrollBars.Both;
            dgvOutware.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvOutware.MultiSelect = false;
            dgvOutware.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvOutware.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(234, 234, 234);
            dgvOutware.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            foreach (DataGridViewColumn col in dgvOutware.Columns)
            {
                col.DataPropertyName = col.Name;
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

        }

        #endregion

        #region 바코드 입력 이벤트
        //자판
        private void btnBarcode_Click(object sender, EventArgs e)
        {
            //키패드 후 엔터
            var path64 = System.IO.Path.Combine(Directory.GetDirectories(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "winsxs"), "amd64_microsoft-windows-osk_*")[0], "osk.exe");
            var path32 = @"C:\windows\system32\osk.exe";
            var path = (Environment.Is64BitOperatingSystem) ? path64 : path32;
            if (File.Exists(path) && !Frm_tinout_Main.Lib.ReturnKillRunningProcess("osk"))
            {
                System.Diagnostics.Process.Start(path);

                txtBarcode.Focus();

            }
        }
        //하위 그리드 입력
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                //ID기준, 수량 기준 나누기
                //하나 입력시 체크 수정 안 되게
                //그리드에 한줄도 없으면 체크 수정 가능

                int i = dgvOutware.Rows.Count;

                if (chkID.Checked) //ID기준 입력
                {
                    if (!(txtBarcode.Text.ToString().ToUpper().Contains("I")))
                    {
                        //똑같은 입력시 입력 안 되게
                        //추가 선택 후 그리드에 데이터 넣기
                        Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                        sqlParameter.Add("LabelID", txtBarcode.Text.ToString()); //검사기준ID
                        DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_WizWork_Outware_LoTIDInfo", sqlParameter, false);
                 
                        if (dt.Rows.Count > 0)
                        {
                            DataRow Textdr = dt.Rows[0];

                            if (dgvOutware.Rows.Count == 0)
                            {
                                //바코드로 찾은 데이터 넣기
                                //하나의 라벨에 여러품명이 있을 경우 첫번째 수주의 거래처가 보이게
                                if (Lib.ConvertDouble(Textdr["Qty"].ToString()) > 0)
                                {
                                    //출고지시번호인 경우와 수주번호인경우 나눔
                                    //버튼 텍스트에 따라 구분하도록 함
                                    if (btnOutwareReqID.Text.Contains("출고")) 
                                    {
                                        //출고지시가 빈칸일 경우(없거나 여러개인경우)
                                        if (Textdr["OutWareReqID"].ToString() == "" && txtOutwareReqID.Text == "")
                                        {
                                            WizCommon.Popup.MyMessageBox.ShowBox("해당 라벨의 제품의 출고지시번호를 \r\n 먼저 선택해주세요. \r\n 라벨번호 : " + txtBarcode.Text.ToString(), "[확인]", 0, 1);
                                            txtBarcode.Text = "";
                                            throw new Exception();
                                        }


                                        if (txtOutwareReqID.Text == "")  //지시번호가 있는 경우 데이터 입력 다시 안 해도 됨
                                        {
                                            txtOutwareReqID.Text = Textdr["OutWareReqID"].ToString();       //출고지시번호
                                            txtOrderID.Text = Textdr["OrderID"].ToString();       //관리번호
                                            txtOrderSeq.Text = Textdr["OrderSeq"].ToString();     //OrderSeq
                                            txtOrderCustom.Text = Textdr["KCustom"].ToString();   //수주거래처
                                            txtOutCustom.Text = Textdr["KCustom"].ToString();     //납품거래처
                                            txtInCustom.Text = Textdr["InCustom"].ToString();      //출고처
                                            txtOrderCustomTag.Text = Textdr["CustomID"].ToString(); //수주거래처ID
                                            txtOutCustomTag.Text = Textdr["CustomID"].ToString();   //납품거래처ID
                                            txtInCustomTag.Text = Textdr["InCustomID"].ToString();    //출고처ID
                                            txtArticle.Text = Textdr["Article"].ToString();         //품명
                                            txtBuyerArticle.Text = Textdr["BuyerArticleNo"].ToString(); //품번
                                            txtArticleTag.Text = Textdr["ArticleID"].ToString(); //ArticleID
                                        }

                                        //출고지시선택 후 바코드 스캔할 경우 라벨과 출고지시 제품이 다른 경우 메세지창
                                        if (txtArticleTag.Text.ToString().Trim() != Textdr["ArticleID"].ToString().Trim())
                                        {
                                            WizCommon.Popup.MyMessageBox.ShowBox("해당 라벨의 제품과 출고지시번호 제품이 다릅니다. \r\n 라벨번호 : " + txtBarcode.Text.ToString(), "[확인]", 0, 1);
                                            txtBarcode.Text = "";
                                            throw new Exception();
                                        }
                                    }
                                    else
                                    {
                                        ////출고지시가 빈칸일 경우(없거나 여러개인경우)
                                        //if (Textdr["OutWareReqID"].ToString() == "" && txtOutwareReqID.Text == "")
                                        //{
                                        //    WizCommon.Popup.MyMessageBox.ShowBox("해당 라벨의 제품의 출고지시번호를 \r\n 먼저 선택해주세요. \r\n 라벨번호 : " + txtBarcode.Text.ToString(), "[확인]", 0, 1);
                                        //    txtBarcode.Text = "";
                                        //    throw new Exception();
                                        //}


                                        if (txtOutwareReqID.Text == "")  //지시번호가 있는 경우 데이터 입력 다시 안 해도 됨
                                        {
                                            txtOutwareReqID.Text = Textdr["OrderID"].ToString();       //관리번호
                                            txtOrderID.Text = Textdr["OrderID"].ToString();       //관리번호
                                            txtOrderSeq.Text = Textdr["OrderSeq"].ToString();     //OrderSeq
                                            txtOrderCustom.Text = Textdr["KCustom"].ToString();   //수주거래처
                                            txtOutCustom.Text = Textdr["KCustom"].ToString();     //납품거래처
                                            txtInCustom.Text = Textdr["InCustom"].ToString();      //출고처
                                            txtOrderCustomTag.Text = Textdr["CustomID"].ToString(); //수주거래처ID
                                            txtOutCustomTag.Text = Textdr["CustomID"].ToString();   //납품거래처ID
                                            txtInCustomTag.Text = Textdr["InCustomID"].ToString();    //출고처ID
                                            txtArticle.Text = Textdr["Article"].ToString();         //품명
                                            txtBuyerArticle.Text = Textdr["BuyerArticleNo"].ToString(); //품번
                                            txtArticleTag.Text = Textdr["ArticleID"].ToString(); //ArticleID
                                        }

                                        //출고지시선택 후 바코드 스캔할 경우 라벨과 출고지시 제품이 다른 경우 메세지창
                                        if (txtArticleTag.Text.ToString().Trim() != Textdr["ArticleID"].ToString().Trim())
                                        {
                                            WizCommon.Popup.MyMessageBox.ShowBox("해당 라벨의 제품과 수주관리번호 제품이 다릅니다. \r\n 라벨번호 : " + txtBarcode.Text.ToString(), "[확인]", 0, 1);
                                            txtBarcode.Text = "";
                                            throw new Exception();
                                        }
                                    }



                                    //2024-01-31 창고입력 안 해서 여기서 입력
                                    txtLoc.Text = Textdr["Loc"].ToString();             //창고
                                    txtLocTag.Text = Textdr["LocID"].ToString();        //창고ID

                                    //if (dgvOutware.Rows.Count > 0)
                                    //{
                                    //    txtBoxQty.Text = (Lib.ConvertDouble(txtBoxQty.Text) + Lib.ConvertDouble(Textdr["BoxQty"].ToString())).ToString();  //라벨갯수
                                    //    txtQty.Text = (Lib.ConvertDouble(txtQty.Text) + Lib.ConvertDouble(Textdr["Qty"].ToString())).ToString(); //수량
                                    //}
                                    //else
                                    //{
                                    txtBoxQty.Text = Textdr["BoxQty"].ToString();                         //라벨갯수
                                    txtQty.Text = Lib.ConvertDouble(Textdr["Qty"].ToString()).ToString(); //수량
                                    //}
                                    //txtUnitClssTag.Text = Textdr["UnitClss"].ToString(); //단위

                                    foreach (DataRow dr in dt.Rows)
                                    {
                                        dgvOutware.Rows.Add(++i,
                                                            dr["LOTID"],         //라벨번호
                                                            dr["ArticleGrp"],    //품명그룹
                                                            dr["BuyerArticleNo"],    //품명
                                                            dr["Article"],           //품번
                                                            Lib.ConvertDouble(dr["Qty"].ToString()).ToString(), //수량
                                                            dr["UnitClssName"],//단위
                                                            //dr["LOC"], //창고
                                                            "", //삭제
                                                            //dr["LOCID"], //창고ID
                                                            dr["ArticleGrpID"], //품명그룹ID
                                                            dr["ArticleID"],    //ArticleID
                                                            dr["UnitClss"],//단위ID
                                                            dr["OrderID"], //관리번호
                                                            dr["OrderSeq"]  //OrderSeq
                                                            );
                                    }

                                    btnchange.Enabled = false;
                                }
                                else
                                {
                                    WizCommon.Popup.MyMessageBox.ShowBox("해당 라벨의 재고가 0 입니다. \r\n 라벨번호 : " + txtBarcode.Text.ToString(), "[확인]", 0, 1);
                                    txtBarcode.Text = "";
                                    throw new Exception();
                                }
                                txtBarcode.Text = "";
                            }
                            else
                            {
                                //원자재인 경우 상관없이 전부 입력 되게(Outware에 ArticleID, CustomID가 입력되어 거래처와 품명이 같은 것만 되게 함, OutwareSub에 ArticleID가 입력 될 경우 품명이 다른 라벨은 진행할수있게)
                                //라벨에 여러제품 있는 경우 무조건 마지막 하나만 입력 되어야 함

                                //먼저 입력한 데이터가 있는 경우 품목, 거래처가 같은 경우만 라벨 등록되게
                                //수주거래처ID
                                if (txtOrderCustomTag.Text != Textdr["CustomID"].ToString())
                                {
                                    WizCommon.Popup.MyMessageBox.ShowBox("거래처가 다릅니다. 같은 거래처로 출고되는 라벨을 스캔해주세요. \r\n 라벨번호 : " + txtBarcode.Text.ToString(), "[확인]", 0, 1);
                                    txtBarcode.Text = "";
                                    return;
                                }
                                //납품거래처ID
                                if (txtOutCustomTag.Text != Textdr["CustomID"].ToString())
                                {
                                    WizCommon.Popup.MyMessageBox.ShowBox("거래처가 다릅니다. 같은 거래처로 출고되는 라벨을 스캔해주세요. \r\n 라벨번호 : " + txtBarcode.Text.ToString(), "[확인]", 0, 1);
                                    txtBarcode.Text = "";
                                    return;
                                }
                                //출고처ID
                                if (txtInCustomTag.Text != Textdr["CustomID"].ToString())
                                {
                                    WizCommon.Popup.MyMessageBox.ShowBox("거래처가 다릅니다. 같은 거래처로 출고되는 라벨을 스캔해주세요. \r\n 라벨번호 : " + txtBarcode.Text.ToString(), "[확인]", 0, 1);
                                    txtBarcode.Text = "";
                                    return;
                                }
                                //품목
                                if (txtArticleTag.Text != Textdr["ArticleID"].ToString())
                                {
                                    WizCommon.Popup.MyMessageBox.ShowBox("품번이 다릅니다. 품번이 같은 라벨을 스캔해주세요. \r\n 라벨번호 : " + txtBarcode.Text.ToString(), "[확인]", 0, 1);
                                    txtBarcode.Text = "";
                                    return;
                                }

                                for (int x = 0; x < dgvOutware.Rows.Count; x++) 
                                {
                                    //바코드 있으면 다시 입력되지 않게 조건 추가
                                    if (txtBarcode.Text.ToString().ToUpper() == dgvOutware.Rows[x].Cells["LOTID"].Value.ToString().ToUpper())
                                    {
                                        WizCommon.Popup.MyMessageBox.ShowBox("이미 라벨을 스캔했습니다. \r\n 라벨을 확인해주세요. \r\n 라벨번호 : " + txtBarcode.Text.ToString(), "[확인]", 0, 1);
                                        txtBarcode.Text = "";
                                        return;
                                    }
                                }
                                if (Lib.ConvertDouble(Textdr["Qty"].ToString()) > 0)
                                {
                                    txtBoxQty.Text = (Lib.ConvertDouble(txtBoxQty.Text) + Lib.ConvertDouble(Textdr["BoxQty"].ToString())).ToString();                         //라벨갯수
                                    txtQty.Text = (Lib.ConvertDouble(txtQty.Text) + Lib.ConvertDouble(Textdr["Qty"].ToString())).ToString(); //수량
                                                                                                                                         //txtUnitClssTag.Text = Textdr["UnitClss"].ToString(); //단위
                                    foreach (DataRow dr in dt.Rows)
                                    {
                                        dgvOutware.Rows.Add(++i,
                                                            dr["LOTID"],         //라벨번호
                                                            dr["ArticleGrp"],    //품명그룹
                                                            dr["BuyerArticleNo"],    //품명
                                                            dr["Article"],           //품번
                                                            Lib.ConvertDouble(dr["Qty"].ToString()).ToString(), //수량
                                                            dr["UnitClssName"],//단위
                                                            //dr["LOC"], //창고
                                                            "", //삭제
                                                            //dr["LOCID"], //창고ID
                                                            dr["ArticleGrpID"], //품명그룹ID
                                                            dr["ArticleID"],     //ArticleID
                                                            dr["UnitClss"], //단위ID
                                                            dr["OrderID"], //관리번호
                                                            dr["OrderSeq"]  //OrderSeq
                                                            );
                                    }
                                }
                                else
                                {
                                    WizCommon.Popup.MyMessageBox.ShowBox("해당 라벨의 재고가 0 입니다. \r\n 라벨번호 : " + txtBarcode.Text.ToString(), "[확인]", 0, 1);
                                    txtBarcode.Text = "";
                                    throw new Exception();
                                }

                                txtBarcode.Text = "";
                            }

                            //ID기준이나 수량기준으로 입력이 하나라도 되면 입력한 기준으로만 입력할 수 있도록 체크박스 막기
                            chkID.Enabled = false;
                            chkQty.Enabled = false;

                        }
                        else
                        {
                            WizCommon.Popup.MyMessageBox.ShowBox("라벨번호를 확인해주세요. \r\n 라벨번호 : " + txtBarcode.Text.ToString(), "[확인]", 0, 1);
                            txtBarcode.Text = "";
                            throw new Exception();
                        }
                    }
                    else
                    {
                        WizCommon.Popup.MyMessageBox.ShowBox("제품라벨만 출고 가능합니다. \r\n 라벨번호 : " + txtBarcode.Text.ToString(), "[확인]", 0, 1);
                        txtBarcode.Text = "";
                        throw new Exception();
                    }
                }
                else //수량기준 입력
                {

                    //ID기준이나 수량기준으로 입력이 하나라도 되면 입력한 기준으로만 입력할 수 있도록 체크박스 막기
                    //txtBarcode.Text에 수량만 입력되게 
                    //기본 데이터 입력 후 수량 입력 되도록 확인 함수 추가

                    if (CheckData())
                    {
                        dgvOutware.Rows.Add(++i,
                                            "",                                           //라벨번호
                                            txtArticleGrp.Text.ToString(),                //품명그룹
                                            txtBuyerArticle.Text.ToString(),              //품명
                                            txtArticle.Text.ToString(),                   //품번
                                            Lib.ConvertDouble(txtBarcode.Text.ToString()), //수량
                                            txtUnitClss.Text.ToString(),                  //단위
                                            "", //삭제
                                            txtArticleGrpTag.Text.ToString(),             //품명그룹ID
                                            txtArticleTag.Text.ToString(),                //ArticleID
                                            txtUnitClssTag.Text.ToString(),               //단위ID
                                            txtOrderID.Text.ToString(),                   //관리번호
                                            txtOrderSeq.Text.ToString()                   //OrderSeq
                                            );


                        txtBarcode.Text = "";

                        chkID.Enabled = false;
                        chkQty.Enabled = false;
                    }

                    //박스수량, 총수량 계산
                    double dsumQty = 0;

                    for (int GridRowCount = 0; GridRowCount < dgvOutware.Rows.Count; GridRowCount++)
                    {
                        dsumQty += Convert.ToDouble(dgvOutware.Rows[GridRowCount].Cells["Qty"].Value);
                    }

                    txtBoxQty.Text = (dgvOutware.Rows.Count).ToString();
                    txtQty.Text = dsumQty.ToString();

                    //2024-01-31 창고입력 안 해서 여기서 입력
                    txtLoc.Text = "사내창고";
                    txtLocTag.Text = "A0001";

                }
            }
            catch(Exception Exception)
            {
                //뒤에 메세지 안 보이게 주석 처리
                //WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", Exception.Message), "[오류]", 0, 1);
            }
            finally
            {
                DataStore.Instance.CloseConnection(); //2021-10-07 DB 커넥트 연결 해제
            }
        }

        //엔터치면 추가 누르기
        private void txtBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    btnAdd_Click(null, null);
                }
            }
            catch(Exception except)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", except.Message), "[오류]", 0, 1);
            }
        }

        //수량기준일 경우 숫자만 입력되게 하는 이벤트
        private void txtBarcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (chkQty.Checked)
            {
                if (!char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && !char.IsControl(e.KeyChar))
                {
                    e.Handled = true;
                }

                if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
                {
                    e.Handled = true;
                }
            }
        }

        #endregion

        #region 하위그리드 클릭 이벤트

        //삭제 선택하는 경우와 사내창고 외주창고 선택하는 경우
        private void dgvOutware_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {

                //수량
                if(e.ColumnIndex == 5)
                {
                    //숫자입력판
                    PopUp.Frm_CMNumericKeypad keypad = new PopUp.Frm_CMNumericKeypad("수량입력", "수량");

                    keypad.Owner = this;
                    if (keypad.ShowDialog() == DialogResult.OK)
                    {
                        //창고가 수정될 경우 각 창고에 있는 재고 보이기
                        Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                        sqlParameter.Add("LabelID", dgvOutware.Rows[e.RowIndex].Cells["LOTID"].Value.ToString()); //검사기준ID
                        sqlParameter.Add("ArticleID", dgvOutware.Rows[e.RowIndex].Cells["ArticleID"].Value.ToString());
                        sqlParameter.Add("LOCID", txtLocTag.Text.ToString());
                        DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_WizWork_Outware_LoTIDStock", sqlParameter, false);

                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                if(Lib.ConvertDouble(dr["Qty"].ToString()) < Lib.ConvertDouble(keypad.tbInputText.Text))
                                {
                                    WizCommon.Popup.MyMessageBox.ShowBox("라벨의 재고보다 많은 수량을 입력했습니다.\r\n 수량을 다시 입력해주세요.", "[수량 수정 전 확인]", 0, 1);
                                    dgvOutware.Rows[e.RowIndex].Cells["Qty"].Value = Lib.ConvertDouble(dr["Qty"].ToString());
                                }
                                else
                                {
                                    dgvOutware.Rows[e.RowIndex].Cells["Qty"].Value = keypad.tbInputText.Text;
                                    if (dgvOutware.Rows[e.RowIndex].Cells["Qty"].Value.ToString() == "" || Convert.ToDouble(dgvOutware.Rows[e.RowIndex].Cells["Qty"].Value) == 0)
                                    {
                                        dgvOutware.Rows[e.RowIndex].Cells["Qty"].Value = "0";
                                    }
                                }
                            }
                        }
                     
                        //박스수량, 총수량 계산
                        double dsumQty = 0;

                        for (int i = 0; i < dgvOutware.Rows.Count; i++)
                        {
                            dsumQty += Convert.ToDouble(dgvOutware.Rows[i].Cells["Qty"].Value);
                        }

                        txtBoxQty.Text = (dgvOutware.Rows.Count).ToString();
                        txtQty.Text = dsumQty.ToString();


                    }   
                    

                }
                //창고
                //else if(e.ColumnIndex == 7)
                //{
                //    //팝업창
                //    Frm_PopUpSel_Common FPSC = new Frm_PopUpSel_Common("LOC", "", "", "O");
                //    FPSC.StartPosition = FormStartPosition.CenterScreen;
                //    FPSC.BringToFront();
                //    FPSC.TopMost = false;
                //    FPSC.OWriteTextEvent += PopUp_WriteTextEvent;
                //    FPSC.ShowDialog();

                //    void PopUp_WriteTextEvent(string Custom, string InCustom, string OrderID, string BuyerArticleNo, string OrderQty, string OUnitClss, string Model, string Article, string DvlyDate, string Work, string CustomID, string ArticleID, string OUnitClssID, string BuyerModelID, string WorkID, string InCustomID, string OK)
                //    {
                //        if (OK == "Cancel")
                //        { return; }
                //        else
                //        {
                //            dgvOutware.Rows[e.RowIndex].Cells["LOC"].Value = Custom;
                //            dgvOutware.Rows[e.RowIndex].Cells["LOCID"].Value = InCustom;

                //            //창고가 수정될 경우 각 창고에 있는 재고 보이기

                //            Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                //            sqlParameter.Add("LabelID", dgvOutware.Rows[e.RowIndex].Cells["LOTID"].Value.ToString()); //검사기준ID
                //            sqlParameter.Add("ArticleID", dgvOutware.Rows[e.RowIndex].Cells["ArticleID"].Value.ToString());
                //            sqlParameter.Add("LOCID", dgvOutware.Rows[e.RowIndex].Cells["LOCID"].Value.ToString());
                //            DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_WizWork_Outware_LoTIDStock", sqlParameter, false);
                            
                //            if (dt.Rows.Count > 0)
                //            {
                //                foreach (DataRow dr in dt.Rows)
                //                {
                //                    dgvOutware.Rows[e.RowIndex].Cells["Qty"].Value = dr["Qty"].ToString();
                //                }
                //            }
                //        }
                //    }
                //}
                //삭제
                else if (e.ColumnIndex == 7)
                {
                    //삭제 후 재 정렬
                    dgvOutware.Rows.Remove(dgvOutware.Rows[e.RowIndex]);

                    for (int i = 0; i < dgvOutware.Rows.Count; i++)
                    {
                        dgvOutware.Rows[i].SetValues((i + 1).ToString());
                    }
                  
                    //하위 그리드에 데이터 없으면 정보 초기화
                    if (dgvOutware.Rows.Count == 0)
                    {
                        chkID.Enabled = true;
                        chkQty.Enabled = true;

                        Clear();
                    }
                    else
                    {
                        //박스수량, 총수량 계산
                        double dsumQty = 0;

                        for (int i = 0; i < dgvOutware.Rows.Count; i++)
                        {
                            dsumQty += Convert.ToDouble(dgvOutware.Rows[i].Cells["Qty"].Value);
                        }

                        txtBoxQty.Text = (dgvOutware.Rows.Count).ToString();
                        txtQty.Text = dsumQty.ToString();

                        //품번, 품명 여러개인 경우 품번, 품명 변경 되게, 관리번호 변경
                        //txtArticle.Text = dgvOutware.Rows[0].Cells["Article"].Value.ToString();
                        //txtBuyerArticle.Text = dgvOutware.Rows[0].Cells["BuyerArticleNo"].Value.ToString();
                        //txtArticleTag.Text = dgvOutware.Rows[0].Cells["ArticleID"].Value.ToString();
                        //txtOrderID.Text = dgvOutware.Rows[0].Cells["OrderID"].Value.ToString();

                    }
                }
            }
        }

        #endregion

        #region ID기준 출고와 수량 기준 출고 체크박스 이벤트

        private void chkID_Click(object sender, EventArgs e)
        {
            if (chkID.Checked)
            {
                chkQty.Checked = false;
            }
            else
            {
                chkQty.Checked = true;
            }
        }

        private void chkQty_Click(object sender, EventArgs e)
        {
            if (chkQty.Checked)
            {
                chkID.Checked = false;
            }
            else
            {
                chkID.Checked = true;
            }
        }


        #endregion

        #region 저장, 초기화, 닫기 이벤트

        private void cmdsave_Click(object sender, EventArgs e)
        {
            if (CheckData()) 
            {
                if (SaveData())
                {
                    chkID.Enabled = true;
                    chkQty.Enabled = true;

                    Clear();
                    DataGridClear();
                    WizCommon.Popup.MyMessageBox.ShowBox("저장이 완료되었습니다.", "[확인]", 0, 1);
                    LogData.LogSave(this.GetType().Name, "C"); //log 남기기(로드 S) 2022-10-24
                    return;
                }
            }
        }

        private void cmdclear_Click(object sender, EventArgs e)
        {
            Clear();
            DataGridClear();
        }

        private void cmdclose_Click(object sender, EventArgs e)
        {
            LogData.LogSave(this.GetType().Name, "S"); //log 남기기(로드 S) 2022-10-24
            Clear();
            DataGridClear();
            this.Close();
        }

        #endregion

        #region 초기화 함수

        private void Clear()
        {
            txtOutwareReqID.Text = "";               //관리번호
            txtOrderID.Text = "";
            txtOrderSeq.Text = "";              //OrderSeq

            //txtOutClss.Text = "";               //출고구분
            //txtOutClssTag.Text = "";            //출고구분ID

            txtOrderCustom.Text = "";           //수주거래처
            txtOutCustom.Text = "";             //납품거래처
            txtInCustom.Text = "";              //출고처
            txtOrderCustomTag.Text = "";        //수주거래처ID
            txtOutCustomTag.Text = "";          //납품거래처ID
            txtInCustomTag.Text = "";           //출고처ID
            txtArticle.Text = "";               //품명
            txtBuyerArticle.Text = "";          //품번
            txtArticleTag.Text = "";            //ArticleID
            txtBoxQty.Text = "";                //라벨갯수
            txtQty.Text = "";                   //수량
            txtUnitClssTag.Text = "";           //단위ID
            txtUnitClss.Text = "";              //단위
            txtOutClssTag.Text = "";            //출고구분

            //txtPerson.Text = "";                //출고자
            //txtPersonTag.Text = "";             //출고자ID

            txtLoc.Text = "";                   //창고
            txtLocTag.Text = "";                //창고ID
            txtArticleGrp.Text = "";            //품명그룹
            txtArticleGrpTag.Text = "";         //품명그룹ID

            cboOutClss.SelectedIndex = 0; //출고구분
            cboPerson.SelectedIndex = 0;  //출고자

            btnchange.Enabled = true; //수주관리번호, 출고지시번호 버튼 클릭
        }

        private void DataGridClear()
        {
            dgvOutware.Rows.Clear();
        }



        #endregion

        #region 클릭이벤트 (팝업창)

        //출하지시번호(OutwareReqID)
        private void btnOutwareReqID_Click(object sender, EventArgs e)
        {
            //수주관리번호일 경우와 출고지시번호일 경우

            if (btnOutwareReqID.Text.Contains("출고")) 
            {
                Frm_PopUpSel_Common FPSC = new Frm_PopUpSel_Common("OutwareReqID", txtOutwareReqID.Text, "", "O");
                FPSC.StartPosition = FormStartPosition.CenterScreen;
                FPSC.BringToFront();
                FPSC.TopMost = false;
                FPSC.OWriteTextEvent += PopUp_WriteTextEvent;
                FPSC.ShowDialog();
            }
            else
            {
                Frm_PopUpSel_Common FPSC = new Frm_PopUpSel_Common("OrderID", txtOutwareReqID.Text, "", "O");
                FPSC.StartPosition = FormStartPosition.CenterScreen;
                FPSC.BringToFront();
                FPSC.TopMost = false;
                FPSC.OWriteTextEvent += PopUp_WriteTextEvent;
                FPSC.ShowDialog();
            }

            void PopUp_WriteTextEvent(string Custom, string InCustom, string OrderID, string OrderSeq, string BuyerArticleNo, string OrderQty, string OUnitClss, string Model, string Article, string DvlyDate, string Work, string ArticleGrp, string CustomID, string ArticleID, string OUnitClssID, string BuyerModelID, string WorkID, string InCustomID, string ArticleGrpID, string OK)
            {
                if (OK == "Cancel")
                {                      
                    return; 
                }
                else
                {
                    btnchange.Enabled = false;

                    Dictionary<string, object> sqlParameter = new Dictionary<string, object>();

                    //출고지시번호와 수주관리번호 구분
                    if (btnOutwareReqID.Text.Contains("출고")) 
                    {
                        txtOutwareReqID.Text = InCustom; //출하지시
                        sqlParameter.Add("OrderID", Custom); //OrderID
                    }
                    else
                    {
                        txtOutwareReqID.Text = OrderID; //OrderID
                        sqlParameter.Add("OrderID", OrderID); //OrderID
                    }
                  
                    //프로시저 태워서 데이터 가져오기
                    DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_WizWork_OutwareReqByOrderID", sqlParameter, false);
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];

                        txtOrderID.Text = dr["OrderID"].ToString();
                        txtOrderSeq.Text = dr["OrderSeq"].ToString();
                        txtOrderCustom.Text = dr["KCustom"].ToString();
                        txtInCustom.Text = dr["InCustom"].ToString(); //매출거래처
                        txtOutCustom.Text = dr["KCustom"].ToString();
                        txtOrderCustomTag.Text = dr["CustomID"].ToString();
                        txtInCustomTag.Text = dr["InCustomID"].ToString(); //매출거래처ID
                        txtOutCustomTag.Text = dr["CustomID"].ToString();
                        txtBuyerArticle.Text = dr["BuyerArticleNo"].ToString();
                        txtArticle.Text = dr["Article"].ToString();
                        txtArticleTag.Text = dr["ArticleID"].ToString();
                        txtArticleGrp.Text = dr["ArticleGrp"].ToString();//품명그룹
                        txtArticleGrpTag.Text = dr["ArticleGrpID"].ToString();//품명그룹ID
                        txtUnitClss.Text = dr["CodeName"].ToString();//단위
                        txtUnitClssTag.Text = dr["UnitClss"].ToString();//단위ID

                    }
                }
            }
        }

        private void txtOutwareReqID_Click(object sender, EventArgs e)
        {
            //수주관리번호일 경우와 출고지시번호일 경우
            if (btnOutwareReqID.Text.Contains("출고"))
            {
                Frm_PopUpSel_Common FPSC = new Frm_PopUpSel_Common("OutwareReqID", txtOutwareReqID.Text, "", "O");
                FPSC.StartPosition = FormStartPosition.CenterScreen;
                FPSC.BringToFront();
                FPSC.TopMost = false;
                FPSC.OWriteTextEvent += PopUp_WriteTextEvent;
                FPSC.ShowDialog();
            }
            else
            {
                Frm_PopUpSel_Common FPSC = new Frm_PopUpSel_Common("OrderID", txtOutwareReqID.Text, "", "O");
                FPSC.StartPosition = FormStartPosition.CenterScreen;
                FPSC.BringToFront();
                FPSC.TopMost = false;
                FPSC.OWriteTextEvent += PopUp_WriteTextEvent;
                FPSC.ShowDialog();
            }

            void PopUp_WriteTextEvent(string Custom, string InCustom, string OrderID, string OrderSeq, string BuyerArticleNo, string OrderQty, string OUnitClss, string Model, string Article, string DvlyDate, string Work, string ArticleGrp, string CustomID, string ArticleID, string OUnitClssID, string BuyerModelID, string WorkID, string InCustomID, string ArticleGrpID, string OK)
            {
                if (OK == "Cancel")
                { return; }
                else
                {
                    btnchange.Enabled = false;

                    Dictionary<string, object> sqlParameter = new Dictionary<string, object>();

                    //출고지시번호와 수주관리번호 구분
                    if (btnOutwareReqID.Text.Contains("출고"))
                    {
                        txtOutwareReqID.Text = InCustom; //출하지시
                        sqlParameter.Add("OrderID", Custom); //OrderID
                    }
                    else
                    {
                        txtOutwareReqID.Text = OrderID; //OrderID
                        sqlParameter.Add("OrderID", OrderID); //OrderID
                    }

                    //프로시저 태워서 데이터 가져오기                
                    DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_WizWork_OutwareReqByOrderID", sqlParameter, false);
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];

                        txtOrderID.Text = dr["OrderID"].ToString();
                        txtOrderSeq.Text = dr["OrderSeq"].ToString();
                        txtOrderCustom.Text = dr["KCustom"].ToString();
                        txtInCustom.Text = dr["InCustom"].ToString(); //매출거래처
                        txtOutCustom.Text = dr["KCustom"].ToString();
                        txtOrderCustomTag.Text = dr["CustomID"].ToString();
                        txtInCustomTag.Text = dr["InCustomID"].ToString(); //매출거래처ID
                        txtOutCustomTag.Text = dr["CustomID"].ToString();
                        txtBuyerArticle.Text = dr["BuyerArticleNo"].ToString();
                        txtArticle.Text = dr["Article"].ToString();
                        txtArticleTag.Text = dr["ArticleID"].ToString();
                        txtArticleGrp.Text = dr["ArticleGrp"].ToString();//품명그룹
                        txtArticleGrpTag.Text = dr["ArticleGrpID"].ToString();//품명그룹ID
                        txtUnitClss.Text = dr["CodeName"].ToString();//단위
                        txtUnitClssTag.Text = dr["UnitClss"].ToString();//단위ID

                    }
                }
            }
        }

        //출고구분
        private void btnOutClss_Click(object sender, EventArgs e)
        {
            Frm_PopUpSel_Common FPSC = new Frm_PopUpSel_Common("OCD", txtOutClss.Text, "", "O");
            FPSC.StartPosition = FormStartPosition.CenterScreen;
            FPSC.BringToFront();
            FPSC.TopMost = false;
            FPSC.OWriteTextEvent += PopUp_WriteTextEvent;
            FPSC.ShowDialog();

            void PopUp_WriteTextEvent(string Custom, string InCustom, string OrderID, string OrderSeq, string BuyerArticleNo, string OrderQty, string OUnitClss, string Model, string Article, string DvlyDate, string Work, string ArticleGrp, string CustomID, string ArticleID, string OUnitClssID, string BuyerModelID, string WorkID, string InCustomID, string ArticleGrpID, string OK)
            {
                if (OK == "Cancel")
                { return; }
                else
                {
                    txtOutClss.Text = Custom;
                    txtOutClssTag.Text = OrderID;
               
                }
            }
        }

        private void txtOutClss_Click(object sender, EventArgs e)
        {
            Frm_PopUpSel_Common FPSC = new Frm_PopUpSel_Common("OCD", txtOutClss.Text, "", "O");
            FPSC.StartPosition = FormStartPosition.CenterScreen;
            FPSC.BringToFront();
            FPSC.TopMost = false;
            FPSC.OWriteTextEvent += PopUp_WriteTextEvent;
            FPSC.ShowDialog();

            void PopUp_WriteTextEvent(string Custom, string InCustom, string OrderID, string OrderSeq, string BuyerArticleNo, string OrderQty, string OUnitClss, string Model, string Article, string DvlyDate, string Work, string ArticleGrp, string CustomID, string ArticleID, string OUnitClssID, string BuyerModelID, string WorkID, string InCustomID, string ArticleGrpID, string OK)
            {
                if (OK == "Cancel")
                { return; }
                else
                {
                    txtOutClss.Text = Custom;
                    txtOutClssTag.Text = OrderID;

                }
            }
        }

        //출고일자
        private void btnOutDate_Click(object sender, EventArgs e)
        {
            WizCommon.Popup.Frm_TLP_Calendar calendar = new WizCommon.Popup.Frm_TLP_Calendar(mtb_ODate.Text.Replace("-", ""), mtb_ODate.Name);
            calendar.WriteDateTextEvent += new WizCommon.Popup.Frm_TLP_Calendar.TextEventHandler(GetDate);
            calendar.Owner = this;
            calendar.ShowDialog();
        }

        private void mtb_ODate_Click(object sender, EventArgs e)
        {
            WizCommon.Popup.Frm_TLP_Calendar calendar = new WizCommon.Popup.Frm_TLP_Calendar(mtb_ODate.Text.Replace("-", ""), mtb_ODate.Name);
            calendar.WriteDateTextEvent += new WizCommon.Popup.Frm_TLP_Calendar.TextEventHandler(GetDate);
            calendar.Owner = this;
            calendar.ShowDialog();
        }

        //수주거래처
        private void btnOrderCustom_Click(object sender, EventArgs e)
        {
            Frm_PopUpSel_Common FPSC = new Frm_PopUpSel_Common("Custom", txtOrderCustom.Text, "", "O");
            FPSC.StartPosition = FormStartPosition.CenterScreen;
            FPSC.BringToFront();
            FPSC.TopMost = false;
            FPSC.OWriteTextEvent += PopUp_WriteTextEvent;
            FPSC.ShowDialog();

            void PopUp_WriteTextEvent(string Custom, string InCustom, string OrderID, string OrderSeq, string BuyerArticleNo, string OrderQty, string OUnitClss, string Model, string Article, string DvlyDate, string Work, string ArticleGrp, string CustomID, string ArticleID, string OUnitClssID, string BuyerModelID, string WorkID, string InCustomID, string ArticleGrpID, string OK)
            {
                if (OK == "Cancel")
                { return; }
                else
                {
                    txtOrderCustom.Text = Custom;
                    txtOrderCustomTag.Text = OrderID;
                }
            }
        }

        private void txtOrderCustom_Click(object sender, EventArgs e)
        {
            Frm_PopUpSel_Common FPSC = new Frm_PopUpSel_Common("Custom", txtOrderCustom.Text, "", "O");
            FPSC.StartPosition = FormStartPosition.CenterScreen;
            FPSC.BringToFront();
            FPSC.TopMost = false;
            FPSC.OWriteTextEvent += PopUp_WriteTextEvent;
            FPSC.ShowDialog();
            void PopUp_WriteTextEvent(string Custom, string InCustom, string OrderID, string OrderSeq, string BuyerArticleNo, string OrderQty, string OUnitClss, string Model, string Article, string DvlyDate, string Work, string ArticleGrp, string CustomID, string ArticleID, string OUnitClssID, string BuyerModelID, string WorkID, string InCustomID, string ArticleGrpID, string OK)
            {
                if (OK == "Cancel")
                { return; }
                else
                {
                    txtOrderCustom.Text = Custom;
                    txtOrderCustomTag.Text = OrderID;
                }
            }
        }

        //납품거래처
        private void btnOutCustom_Click(object sender, EventArgs e)
        {
            Frm_PopUpSel_Common FPSC = new Frm_PopUpSel_Common("Custom", txtOutCustom.Text, "", "O");
            FPSC.StartPosition = FormStartPosition.CenterScreen;
            FPSC.BringToFront();
            FPSC.TopMost = false;
            FPSC.OWriteTextEvent += PopUp_WriteTextEvent;
            FPSC.ShowDialog();

            void PopUp_WriteTextEvent(string Custom, string InCustom, string OrderID, string OrderSeq, string BuyerArticleNo, string OrderQty, string OUnitClss, string Model, string Article, string DvlyDate, string Work, string ArticleGrp, string CustomID, string ArticleID, string OUnitClssID, string BuyerModelID, string WorkID, string InCustomID, string ArticleGrpID, string OK)
            {
                if (OK == "Cancel")
                { return; }
                else
                {
                    txtOutCustom.Text = Custom;
                    txtOutCustomTag.Text = OrderID;
                }
            }
        }

        private void txtOutCustom_Click(object sender, EventArgs e)
        {
            Frm_PopUpSel_Common FPSC = new Frm_PopUpSel_Common("Custom", txtOutCustom.Text, "", "O");
            FPSC.StartPosition = FormStartPosition.CenterScreen;
            FPSC.BringToFront();
            FPSC.TopMost = false;
            FPSC.OWriteTextEvent += PopUp_WriteTextEvent;
            FPSC.ShowDialog();

            void PopUp_WriteTextEvent(string Custom, string InCustom, string OrderID, string OrderSeq, string BuyerArticleNo, string OrderQty, string OUnitClss, string Model, string Article, string DvlyDate, string Work, string ArticleGrp, string CustomID, string ArticleID, string OUnitClssID, string BuyerModelID, string WorkID, string InCustomID, string ArticleGrpID, string OK)
            {
                if (OK == "Cancel")
                { return; }
                else
                {
                    txtOutCustom.Text = Custom;
                    txtOutCustomTag.Text = OrderID;
                }
            }
        }

        //출고처
        private void btnInCustom_Click(object sender, EventArgs e)
        {
            Frm_PopUpSel_Common FPSC = new Frm_PopUpSel_Common("Custom", txtInCustom.Text, "", "O");
            FPSC.StartPosition = FormStartPosition.CenterScreen;
            FPSC.BringToFront();
            FPSC.TopMost = false;
            FPSC.OWriteTextEvent += PopUp_WriteTextEvent;
            FPSC.ShowDialog();

            void PopUp_WriteTextEvent(string Custom, string InCustom, string OrderID, string OrderSeq, string BuyerArticleNo, string OrderQty, string OUnitClss, string Model, string Article, string DvlyDate, string Work, string ArticleGrp, string CustomID, string ArticleID, string OUnitClssID, string BuyerModelID, string WorkID, string InCustomID, string ArticleGrpID, string OK)
            {
                if (OK == "Cancel")
                { return; }
                else
                {
                    txtInCustom.Text = Custom;
                    txtInCustomTag.Text = OrderID;
                }
            }
        }

        private void txtInCustom_Click(object sender, EventArgs e)
        {
            Frm_PopUpSel_Common FPSC = new Frm_PopUpSel_Common("Custom", txtInCustom.Text, "", "O");
            FPSC.StartPosition = FormStartPosition.CenterScreen;
            FPSC.BringToFront();
            FPSC.TopMost = false;
            FPSC.OWriteTextEvent += PopUp_WriteTextEvent;
            FPSC.ShowDialog();

            void PopUp_WriteTextEvent(string Custom, string InCustom, string OrderID, string OrderSeq, string BuyerArticleNo, string OrderQty, string OUnitClss, string Model, string Article, string DvlyDate, string Work, string ArticleGrp, string CustomID, string ArticleID, string OUnitClssID, string BuyerModelID, string WorkID, string InCustomID, string ArticleGrpID, string OK)
            {
                if (OK == "Cancel")
                { return; }
                else
                {
                    txtInCustom.Text = Custom;
                    txtInCustomTag.Text = OrderID;
                }
            }
        }
        
        //품명
        private void btnArticle_Click(object sender, EventArgs e)
        {
            //수주입력 후 품명을 수정 시 조회가 안 될 수도 있어 주석처리
            //Frm_PopUpSel_Common FPSC = new Frm_PopUpSel_Common("MA", txtArticle.Text, "", "O");
            //FPSC.StartPosition = FormStartPosition.CenterScreen;
            //FPSC.BringToFront();
            //FPSC.TopMost = false;
            //FPSC.OWriteTextEvent += PopUp_WriteTextEvent;
            //FPSC.ShowDialog();

            //void PopUp_WriteTextEvent(string Custom, string InCustom, string OrderID, string BuyerArticleNo, string OrderQty, string OUnitClss, string Model, string Article, string DvlyDate, string Work, string CustomID, string ArticleID, string OUnitClssID, string BuyerModelID, string WorkID, string InCustomID, string OK)
            //{
            //    if (OK == "Cancel")
            //    { return; }
            //    else
            //    {
            //        txtArticle.Text = Article;
            //        txtArticleTag.Text = ArticleID;
            //        txtBuyerArticle.Text = BuyerArticleNo;
            //        txtUnitClssTag.Text = OUnitClssID;
            //    }
            //}
        }

        private void txtArticle_Click(object sender, EventArgs e)
        {
            //수주입력 후 품명을 수정 시 조회가 안 될 수도 있어 주석처리
            //Frm_PopUpSel_Common FPSC = new Frm_PopUpSel_Common("MA", txtArticle.Text, "", "O");
            //FPSC.StartPosition = FormStartPosition.CenterScreen;
            //FPSC.BringToFront();
            //FPSC.TopMost = false;
            //FPSC.OWriteTextEvent += PopUp_WriteTextEvent;
            //FPSC.ShowDialog();

            //void PopUp_WriteTextEvent(string Custom, string InCustom, string OrderID, string BuyerArticleNo, string OrderQty, string OUnitClss, string Model, string Article, string DvlyDate, string Work, string CustomID, string ArticleID, string OUnitClssID, string BuyerModelID, string WorkID, string InCustomID, string OK)
            //{
            //    if (OK == "Cancel")
            //    { return; }
            //    else
            //    {
            //        txtArticle.Text = Article;
            //        txtArticleTag.Text = ArticleID;
            //        txtBuyerArticle.Text = BuyerArticleNo;
            //        txtUnitClssTag.Text = OUnitClssID;
            //    }
            //}
        }

        //품번
        private void btnBuyerArticle_Click(object sender, EventArgs e)
        {
            //수주입력 후 품명을 수정 시 조회가 안 될 수도 있어 주석처리
            //Frm_PopUpSel_Common FPSC = new Frm_PopUpSel_Common("MA", txtBuyerArticle.Text, "", "O");
            //FPSC.StartPosition = FormStartPosition.CenterScreen;
            //FPSC.BringToFront();
            //FPSC.TopMost = false;
            //FPSC.OWriteTextEvent += PopUp_WriteTextEvent;
            //FPSC.ShowDialog();

            //void PopUp_WriteTextEvent(string Custom, string InCustom, string OrderID, string BuyerArticleNo, string OrderQty, string OUnitClss, string Model, string Article, string DvlyDate, string Work, string CustomID, string ArticleID, string OUnitClssID, string BuyerModelID, string WorkID, string InCustomID, string OK)
            //{
            //    if (OK == "Cancel")
            //    { return; }
            //    else
            //    {
            //        txtArticle.Text = Article;
            //        txtArticleTag.Text = ArticleID;
            //        txtBuyerArticle.Text = BuyerArticleNo;
            //        txtUnitClssTag.Text = OUnitClssID;
            //    }
            //}
        }

        private void txtBuyerArticle_Click(object sender, EventArgs e)
        {
            //수주입력 후 품명을 수정 시 조회가 안 될 수도 있어 주석처리
            //Frm_PopUpSel_Common FPSC = new Frm_PopUpSel_Common("MA", txtBuyerArticle.Text, "", "O");
            //FPSC.StartPosition = FormStartPosition.CenterScreen;
            //FPSC.BringToFront();
            //FPSC.TopMost = false;
            //FPSC.OWriteTextEvent += PopUp_WriteTextEvent;
            //FPSC.ShowDialog();

            //void PopUp_WriteTextEvent(string Custom, string InCustom, string OrderID, string BuyerArticleNo, string OrderQty, string OUnitClss, string Model, string Article, string DvlyDate, string Work, string CustomID, string ArticleID, string OUnitClssID, string BuyerModelID, string WorkID, string InCustomID, string OK)
            //{
            //    if (OK == "Cancel")
            //    { return; }
            //    else
            //    {
            //        txtArticle.Text = Article;
            //        txtArticleTag.Text = ArticleID;
            //        txtBuyerArticle.Text = BuyerArticleNo;
            //        txtUnitClssTag.Text = OUnitClssID;
            //    }
            //}
        }

        //창고
        private void btnLoc_Click(object sender, EventArgs e)
        {
            Frm_PopUpSel_Common FPSC = new Frm_PopUpSel_Common("LOC", "", "", "O");
            FPSC.StartPosition = FormStartPosition.CenterScreen;
            FPSC.BringToFront();
            FPSC.TopMost = false;
            FPSC.OWriteTextEvent += PopUp_WriteTextEvent;
            FPSC.ShowDialog();

            void PopUp_WriteTextEvent(string Custom, string InCustom, string OrderID, string OrderSeq, string BuyerArticleNo, string OrderQty, string OUnitClss, string Model, string Article, string DvlyDate, string Work, string ArticleGrp, string CustomID, string ArticleID, string OUnitClssID, string BuyerModelID, string WorkID, string InCustomID, string ArticleGrpID, string OK)
            {
                if (OK == "Cancel")
                { return; }
                else
                {            
                    txtLoc.Text = Custom;
                    txtLocTag.Text = OrderID;

                    //수정 되면 그리드에 있는 재고 수정
                    if (dgvOutware.Rows.Count > 0)
                    {
                        for (int i = 0; i < dgvOutware.Rows.Count; i++) 
                        {
                            //창고가 수정될 경우 각 창고에 있는 재고 보이기
                            Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                            sqlParameter.Add("LabelID", dgvOutware.Rows[i].Cells["LOTID"].Value.ToString()); //검사기준ID
                            sqlParameter.Add("ArticleID", dgvOutware.Rows[i].Cells["ArticleID"].Value.ToString());
                            sqlParameter.Add("LOCID", txtLocTag.Text.ToString());
                            DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_WizWork_Outware_LoTIDStock", sqlParameter, false);

                            if (dt.Rows.Count > 0)
                            {
                                foreach (DataRow dr in dt.Rows)
                                {
                                    dgvOutware.Rows[i].Cells["Qty"].Value = dr["Qty"].ToString();
                                }
                            }
                        }
                    }
                }
            }
        }

        private void txtLoc_Click(object sender, EventArgs e)
        {
            Frm_PopUpSel_Common FPSC = new Frm_PopUpSel_Common("LOC", "", "", "O");
            FPSC.StartPosition = FormStartPosition.CenterScreen;
            FPSC.BringToFront();
            FPSC.TopMost = false;
            FPSC.OWriteTextEvent += PopUp_WriteTextEvent;
            FPSC.ShowDialog();

            void PopUp_WriteTextEvent(string Custom, string InCustom, string OrderID, string OrderSeq, string BuyerArticleNo, string OrderQty, string OUnitClss, string Model, string Article, string DvlyDate, string Work, string ArticleGrp, string CustomID, string ArticleID, string OUnitClssID, string BuyerModelID, string WorkID, string InCustomID, string ArticleGrpID, string OK)
            {
                if (OK == "Cancel")
                { return; }
                else
                {                   
                    txtLoc.Text = Custom;
                    txtLocTag.Text = OrderID;

                    //수정 되면 그리드에 있는 재고 수정
                    if (dgvOutware.Rows.Count > 0)
                    {
                        for (int i = 0; i < dgvOutware.Rows.Count; i++)
                        {
                            //창고가 수정될 경우 각 창고에 있는 재고 보이기
                            Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                            sqlParameter.Add("LabelID", dgvOutware.Rows[i].Cells["LOTID"].Value.ToString()); //검사기준ID
                            sqlParameter.Add("ArticleID", dgvOutware.Rows[i].Cells["ArticleID"].Value.ToString());
                            sqlParameter.Add("LOCID", txtLocTag.Text.ToString());
                            DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_WizWork_Outware_LoTIDStock", sqlParameter, false);

                            if (dt.Rows.Count > 0)
                            {
                                foreach (DataRow dr in dt.Rows)
                                {
                                    dgvOutware.Rows[i].Cells["Qty"].Value = dr["Qty"].ToString();
                                }
                            }
                        }
                    }
                }
            }
        }

        //출고자
        private void btnPerson_Click(object sender, EventArgs e)
        {
            Frm_PopUpSel_Common FPSC = new Frm_PopUpSel_Common("InP", "", "", "O");
            FPSC.StartPosition = FormStartPosition.CenterScreen;
            FPSC.BringToFront();
            FPSC.TopMost = false;
            FPSC.OWriteTextEvent += PopUp_WriteTextEvent;
            FPSC.ShowDialog();

            void PopUp_WriteTextEvent(string Custom, string InCustom, string OrderID, string OrderSeq, string BuyerArticleNo, string OrderQty, string OUnitClss, string Model, string Article, string DvlyDate, string Work, string ArticleGrp, string CustomID, string ArticleID, string OUnitClssID, string BuyerModelID, string WorkID, string InCustomID, string ArticleGrpID, string OK)
            {
                if (OK == "Cancel")
                { return; }
                else
                {                  
                    txtPerson.Text = Custom;
                    txtPersonTag.Text = OrderID;
                }
            }
        }

        private void txtPerson_Click(object sender, EventArgs e)
        {
            Frm_PopUpSel_Common FPSC = new Frm_PopUpSel_Common("InP", "", "", "O");
            FPSC.StartPosition = FormStartPosition.CenterScreen;
            FPSC.BringToFront();
            FPSC.TopMost = false;
            FPSC.OWriteTextEvent += PopUp_WriteTextEvent;
            FPSC.ShowDialog();

            void PopUp_WriteTextEvent(string Custom, string InCustom, string OrderID, string OrderSeq, string BuyerArticleNo, string OrderQty, string OUnitClss, string Model, string Article, string DvlyDate, string Work, string ArticleGrp, string CustomID, string ArticleID, string OUnitClssID, string BuyerModelID, string WorkID, string InCustomID, string ArticleGrpID, string OK)
            {
                if (OK == "Cancel")
                { return; }
                else
                {                  
                    txtPerson.Text = Custom;
                    txtPersonTag.Text = OrderID;
                }
            }
        }

        #endregion

        #region 일자 함수

        //  Calendar.Value -> mtbBox.Text 달력창으로부터 텍스트로 값을 옮겨주는 메소드
        private void GetDate(string strDate, string btnName)
        {
            DateTime dateTime = new DateTime();
            dateTime = DateTime.ParseExact(strDate, "yyyyMMdd", null);
            mtb_ODate.Text = dateTime.ToString("yyyy-MM-dd");
        }



        #endregion

        #region 저장 함수

        private bool SaveData()
        {
            try
            {
                List<WizCommon.Procedure> Prolist = new List<WizCommon.Procedure>();
                List<List<string>> ListProcedureName = new List<List<string>>();
                List<Dictionary<string, object>> ListParameter = new List<Dictionary<string, object>>();

                //u_OutwareID가 빈 값이면 수정이 아니고 입력
                if (u_OutwareID == "")
                {
                    //outware
                    Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                    sqlParameter.Add("OrderID", txtOrderID.Text);               //관리번호, 출하지시의 관리번호가 입력됨
                    sqlParameter.Add("CompanyID", "0001");                      //본인회사
                    sqlParameter.Add("OutSeq", "");
                    sqlParameter.Add("OutwareID", "");
                    sqlParameter.Add("OutClss", cboOutClss.SelectedValue.ToString()); //출고구분 txtOutClssTag.Text.ToString()
                    sqlParameter.Add("CustomID", txtOrderCustomTag.Text.ToString()); ; //수주거래처
                    sqlParameter.Add("BuyerDirectYN", "Y");
                    sqlParameter.Add("WorkID", "0001");                         //가공구분
                    sqlParameter.Add("ExchRate", 0);
                    sqlParameter.Add("UnitPriceClss", "0");                     //단가 단위
                    sqlParameter.Add("InsStuffInYN", "N");                      //동시입고여부
                    sqlParameter.Add("OutcustomID", txtInCustomTag.Text.ToString());     //출고처
                    sqlParameter.Add("Outcustom", txtInCustom.Text);            //출고처
                    sqlParameter.Add("LossRate", 0);
                    sqlParameter.Add("LossQty", 0);
                    sqlParameter.Add("OutRoll", txtBoxQty.Text.Equals("") == true ? 0 : Convert.ToInt32(txtBoxQty.Text.Replace(",", "")));   //박스 수
                    sqlParameter.Add("OutQty", txtQty.Text.Equals("") == true ? 0 : Convert.ToDouble(txtQty.Text.Replace(",", "")));         //출고 수량
                    sqlParameter.Add("OutRealQty", Convert.ToDouble(txtQty.Text.Replace(",", "")));                                          //출고 수량
                    sqlParameter.Add("OutDate", mtb_ODate.Text.ToString().Substring(0, 10).Replace("-", ""));
                    //sqlParameter.Add("OutTemQty", 0); //외주이동 때문에 추가
                    sqlParameter.Add("ResultDate", mtb_ODate.Text.ToString().Substring(0, 10).Replace("-", ""));
                    sqlParameter.Add("Remark", "현장프로그램에서 출고");
                    sqlParameter.Add("OutType", "3");                 //스캔출고형태가 3번
                    sqlParameter.Add("OutSubType", "");               //안쓰니까 일단 빈값??
                    sqlParameter.Add("Amount", 0);                    //안쓰니까 일단 빈값??
                    sqlParameter.Add("VatAmount", 0);                 //안쓰니까 일단 빈값??
                    sqlParameter.Add("VatINDYN", "Y");                //안쓰니까 일단 빈값??
                    sqlParameter.Add("FromLocID", txtLocTag.Text.ToString());        //창고
                    sqlParameter.Add("ToLocID", txtLocTag.Text.ToString());          //창고
                    sqlParameter.Add("UnitClss", 0);
                    sqlParameter.Add("ArticleID", txtArticleTag.Text.ToString());    //ArticleID
                    sqlParameter.Add("DvlyCustomID", txtOutCustomTag.Text.ToString()); //납품거래처

                    //수주관리번호일 경우에 출하지시번호 빈칸처리
                    if (btnOutwareReqID.Text.Contains("출고")) 
                    {
                        sqlParameter.Add("OutwareReqID", txtOutwareReqID.Text.ToString()); //출고지시번호
                    }
                    else
                    {
                        sqlParameter.Add("OutwareReqID", ""); //출고지시번호
                    }

                    sqlParameter.Add("UserID", cboPerson.SelectedValue.ToString());        //출고자 txtPersonTag.Text.ToString()

                    WizCommon.Procedure pro1 = new WizCommon.Procedure();
                    pro1.Name = "[xp_WizWork_iOutwareStock]";
                    pro1.OutputUseYN = "Y";
                    pro1.OutputName = "OutwareID";
                    pro1.OutputLength = "20";

                    Prolist.Add(pro1);
                    ListParameter.Add(sqlParameter);

                    //outwaresub
                    for (int i = 0; i < dgvOutware.Rows.Count; i++)
                    {
                        sqlParameter = new Dictionary<string, object>();

                        sqlParameter.Add("OutwareID", "");              //OutwareID
                        sqlParameter.Add("OrderID", txtOrderID.Text);   //관리번호, 출하지시의 관리번호가 입력됨
                        sqlParameter.Add("OutSeq", "");
                        sqlParameter.Add("OutSubSeq", i + 1);
                        sqlParameter.Add("OrderSeq", Lib.ConvertInt(txtOrderSeq.Text.ToString()));  // 하나의 수주에 여러개의 제품이 있을 경우 Lib.ConvertInt(dgvOutware.Rows[i].Cells["OrderSeq"].Value.ToString())
                        sqlParameter.Add("LineSeq", 0);
                        sqlParameter.Add("LineSubSeq", 0);
                        sqlParameter.Add("RollSeq", i);
                        sqlParameter.Add("LabelID", dgvOutware.Rows[i].Cells["LOTID"].Value.ToString());    //라벨ID
                        sqlParameter.Add("LabelGubun", "2");        //박스라벨출고는 2번
                        sqlParameter.Add("LotNo", "0");
                        sqlParameter.Add("Gubun", "");              //용도를 몰라서 빈값
                        sqlParameter.Add("StuffQty", 0);
                        sqlParameter.Add("OutQty", dgvOutware.Rows[i].Cells["Qty"].Value.ToString().Replace(",", ""));
                        sqlParameter.Add("OutRoll", 1); // 하나당 박스 1개로 처리 하니, 1로 저장한다고 함
                        sqlParameter.Add("DefectQty", 0);
                        sqlParameter.Add("UnitPrice", 0); //수주단가
                        sqlParameter.Add("CustomBoxID", "");
                        sqlParameter.Add("DefectID", "");           //결함사유라는데.. 빈값으로 
                        sqlParameter.Add("BoxID", dgvOutware.Rows[i].Cells["LOTID"].Value.ToString());
                        sqlParameter.Add("ArticleID", txtArticleTag.Text.ToString());    //ArticleID
                        sqlParameter.Add("UserID", cboPerson.SelectedValue.ToString()); //txtPersonTag.Text.ToString()

                        WizCommon.Procedure pro2 = new WizCommon.Procedure();
                        pro2.Name = "[xp_WizWork_iOutwareSubStock]";
                        pro2.OutputUseYN = "N";
                        pro2.OutputName = "OutwareID";
                        pro2.OutputLength = "20";

                        Prolist.Add(pro2);
                        ListParameter.Add(sqlParameter);
                    }

                    List<KeyValue> list_Result = new List<KeyValue>();
                    list_Result = DataStore.Instance.ExecuteAllProcedureOutputToCS(Prolist, ListParameter);

                    if (list_Result[0].key.ToLower() == "success")
                    {
                        list_Result.RemoveAt(0);

                        //for (int i = 0; i < list_Result.Count; i++)
                        //{
                        //    KeyValue kv = list_Result[i];
                        //    if (kv.key == "OutwareID")
                        //    {
                        //        m_StuffinID = kv.value.ToString();
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
                else
                {
                    
                    Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                    sqlParameter.Add("OrderID", txtOrderID.Text);               //관리번호, 출하지시의 관리번호가 입력됨
                    sqlParameter.Add("CompanyID", "0001");                      //본인회사
                    sqlParameter.Add("OutwareID", u_OutwareID);
                    sqlParameter.Add("OutClss", cboOutClss.SelectedValue.ToString()); //출고구분 txtOutClssTag.Text.ToString()
                    sqlParameter.Add("CustomID", txtOrderCustomTag.Text.ToString()); ; //수주거래처
                    sqlParameter.Add("BuyerDirectYN", "Y");
                    sqlParameter.Add("WorkID", "0001");                         //가공구분
                    sqlParameter.Add("ExchRate", 0);
                    sqlParameter.Add("UnitPriceClss", "0");                     //단가 단위
                    sqlParameter.Add("InsStuffInYN", "N");                      //동시입고여부
                    sqlParameter.Add("OutcustomID", txtInCustomTag.Text.ToString());     //출고처
                    sqlParameter.Add("Outcustom", txtInCustom.Text);            //출고처
                    sqlParameter.Add("LossRate", 0);
                    sqlParameter.Add("LossQty", 0);
                    sqlParameter.Add("OutRoll", txtBoxQty.Text.Equals("") == true ? 0 : Convert.ToInt32(txtBoxQty.Text.Replace(",", "")));   //박스 수
                    sqlParameter.Add("OutQty", txtQty.Text.Equals("") == true ? 0 : Convert.ToDouble(txtQty.Text.Replace(",", "")));         //출고 수량
                    sqlParameter.Add("OutRealQty", Convert.ToDouble(txtQty.Text.Replace(",", "")));                                          //출고 수량
                    sqlParameter.Add("OutDate", mtb_ODate.Text.ToString().Substring(0, 10).Replace("-", ""));
                    //sqlParameter.Add("OutTemQty", 0); //외주이동 때문에 추가
                    sqlParameter.Add("ResultDate", mtb_ODate.Text.ToString().Substring(0, 10).Replace("-", ""));
                    sqlParameter.Add("Remark", "현장프로그램에서 출고");
                    sqlParameter.Add("OutType", "3");                 //스캔출고형태가 3번
                    sqlParameter.Add("OutSubType", "");               //안쓰니까 일단 빈값??
                    sqlParameter.Add("Amount", 0);                    //안쓰니까 일단 빈값??
                    sqlParameter.Add("VatAmount", 0);                 //안쓰니까 일단 빈값??
                    sqlParameter.Add("VatINDYN", "Y");                //안쓰니까 일단 빈값??
                    sqlParameter.Add("FromLocID", txtLocTag.Text.ToString());        //창고
                    sqlParameter.Add("ToLocID", txtLocTag.Text.ToString());          //창고
                    sqlParameter.Add("UnitClss", 0);
                    sqlParameter.Add("ArticleID", txtArticleTag.Text.ToString());    //ArticleID
                    sqlParameter.Add("DvlyCustomID", txtOutCustomTag.Text.ToString()); //납품거래처

                    //수주관리번호일 경우에 출하지시번호 빈칸처리
                    if (btnOutwareReqID.Text.Contains("출고"))
                    {
                        sqlParameter.Add("OutwareReqID", txtOutwareReqID.Text.ToString()); //출고지시번호
                    }
                    else
                    {
                        sqlParameter.Add("OutwareReqID", ""); //출고지시번호
                    }

                    sqlParameter.Add("UserID", cboPerson.SelectedValue.ToString());        //출고자  txtPersonTag.Text.ToString()

                    WizCommon.Procedure pro1 = new WizCommon.Procedure();
                    pro1.Name = "[xp_WizWork_uOutwareStock]";
                    pro1.OutputUseYN = "N";
                    pro1.OutputName = "OutwareID";
                    pro1.OutputLength = "20";

                    Prolist.Add(pro1);
                    ListParameter.Add(sqlParameter);

                    //outwaresub
                    for (int i = 0; i < dgvOutware.Rows.Count; i++)
                    {
                        sqlParameter = new Dictionary<string, object>();

                        sqlParameter.Add("OutwareID", u_OutwareID);              //OutwareID
                        sqlParameter.Add("OrderID", txtOrderID.Text);            //관리번호, 출하지시의 관리번호가 입력됨
                        sqlParameter.Add("OutSeq", "");
                        sqlParameter.Add("OutSubSeq", i + 1);
                        sqlParameter.Add("OrderSeq", 1);  // 하나의 수주에 여러개의 제품이 있을 경우
                        sqlParameter.Add("LineSeq", 0);
                        sqlParameter.Add("LineSubSeq", 0);
                        sqlParameter.Add("RollSeq", i);
                        sqlParameter.Add("LabelID", dgvOutware.Rows[i].Cells["LOTID"].Value.ToString());    //라벨ID
                        sqlParameter.Add("LabelGubun", "2");        //박스라벨출고는 2번
                        sqlParameter.Add("LotNo", "0");
                        sqlParameter.Add("Gubun", "");              //용도를 몰라서 빈값
                        sqlParameter.Add("StuffQty", 0);
                        sqlParameter.Add("OutQty", dgvOutware.Rows[i].Cells["Qty"].Value.ToString().Replace(",", ""));
                        sqlParameter.Add("OutRoll", 1); // 하나당 박스 1개로 처리 하니, 1로 저장한다고 함
                        sqlParameter.Add("DefectQty", 0);
                        sqlParameter.Add("UnitPrice", 0); //수주단가
                        sqlParameter.Add("CustomBoxID", "");
                        sqlParameter.Add("DefectID", "");           //결함사유라는데.. 빈값으로 
                        sqlParameter.Add("BoxID", dgvOutware.Rows[i].Cells["LOTID"].Value.ToString());
                        sqlParameter.Add("ArticleID", txtArticleTag.Text.ToString());    //ArticleID
                        sqlParameter.Add("UserID", cboPerson.SelectedValue.ToString()); //txtPersonTag.Text.ToString()

                        WizCommon.Procedure pro2 = new WizCommon.Procedure();
                        pro2.Name = "[xp_WizWork_uOutwareSubStock]";
                        pro2.OutputUseYN = "N";
                        pro2.OutputName = "OutwareID";
                        pro2.OutputLength = "20";

                        Prolist.Add(pro2);
                        ListParameter.Add(sqlParameter);
                    }

                    List<KeyValue> list_Result = new List<KeyValue>();
                    list_Result = DataStore.Instance.ExecuteAllProcedureOutputToCS(Prolist, ListParameter);

                    if (list_Result[0].key.ToLower() == "success")
                    {
                        list_Result.RemoveAt(0);

                        //for (int i = 0; i < list_Result.Count; i++)
                        //{
                        //    KeyValue kv = list_Result[i];
                        //    if (kv.key == "OutwareID")
                        //    {
                        //        m_StuffinID = kv.value.ToString();
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
            catch (Exception e)
            {
                Message[0] = "[오류]";
                Message[1] = string.Format("오류!관리자에게 문의\r\n{0}", e.Message);
                WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                return false;
            }
        }

        private bool CheckData()
        {
            if (btnOutwareReqID.Text.Contains("출고")) 
            {
                //출하지시번호
                if (txtOutwareReqID.Text == "")
                {
                    WizCommon.Popup.MyMessageBox.ShowBox("출고지시번호를 입력해주세요.", "[확인]", 0, 1);
                    return false;
                }
            }
            else
            {
                //수주관리번호
                if (txtOutwareReqID.Text == "")
                {
                    WizCommon.Popup.MyMessageBox.ShowBox("수주관리번호를 입력해주세요.", "[확인]", 0, 1);
                    return false;
                }
            }

            //출고구분
            if (cboOutClss.Text == "전체" || cboOutClss.Text =="") //txtOutClss.Text == ""
            {
                WizCommon.Popup.MyMessageBox.ShowBox("출고구분을 입력해주세요.", "[확인]", 0, 1);
                return false;
            }

            //출고일자
            if (mtb_ODate.Text == "")
            {
                WizCommon.Popup.MyMessageBox.ShowBox("출고일자를 입력해주세요.", "[확인]", 0, 1);
                return false;
            }
            //수주거래처
            if (txtOrderCustom.Text == "")
            {
                WizCommon.Popup.MyMessageBox.ShowBox("수주거래처를 입력해주세요.", "[확인]", 0, 1);
                return false;
            }
            //납품거래처
            if (txtOutCustom.Text == "")
            {
                WizCommon.Popup.MyMessageBox.ShowBox("납품거래처를 입력해주세요.", "[확인]", 0, 1);
                return false;
            }
            //출고처
            if (txtInCustom.Text == "")
            {
                WizCommon.Popup.MyMessageBox.ShowBox("출고처를 입력해주세요.", "[확인]", 0, 1);
                return false;
            }
            //품번
            if (txtArticle.Text == "")
            {
                WizCommon.Popup.MyMessageBox.ShowBox("품번을 입력해주세요.", "[확인]", 0, 1);
                return false;
            }
            //품명
            if (txtBuyerArticle.Text == "")
            {
                WizCommon.Popup.MyMessageBox.ShowBox("품명을 입력해주세요.", "[확인]", 0, 1);
                return false;
            }

            ////박스
            //if (txtBoxQty.Text == "")
            //{
            //    WizCommon.Popup.MyMessageBox.ShowBox("박스수를 입력해주세요.", "[확인]", 0, 1);
            //    return false;
            //}
            ////수량
            //if (txtQty.Text == "")
            //{
            //    WizCommon.Popup.MyMessageBox.ShowBox("수량을 입력해주세요.", "[확인]", 0, 1);
            //    return false;
            //}

            //창고
            if (txtLoc.Text == "")
            {
                WizCommon.Popup.MyMessageBox.ShowBox("창고를 입력해주세요.", "[확인]", 0, 1);
                return false;
            }

            //출고자
            if (cboPerson.Text == "전체" || cboPerson.Text == "") //txtPerson.Text == ""
            {
                WizCommon.Popup.MyMessageBox.ShowBox("출고자를 입력해주세요.", "[확인]", 0, 1);
                return false;
            }

            return true;
        }




        #endregion

        #region 수정시 데이터 가져오는 함수

        private void UFillGrid()
        {
            Clear();
            DataGridClear();

            try
            {
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Clear();

                sqlParameter.Add("OutwareID", u_OutwareID); //OutwareID

                DataSet ds = DataStore.Instance.ProcedureToDataSet("xp_WizWork_sOutware_U", sqlParameter, true);

                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        DataRowCollection drc = dt.Rows;

                        foreach (DataRow dr in drc)
                        {
                            if (dr["OutwareReqID"].ToString() != "") 
                            {
                                txtOutwareReqID.Text = dr["OutwareReqID"].ToString();//출고지시번호
                                txtOrderID.Text = dr["OrderID"].ToString();//관리번호
                                //txtOutClss.Text = dr["OutClssName"].ToString();//출고구분
                                //txtOutClssTag.Text = dr["OutClss"].ToString();//출고구분ID
                                btnchange.Text = "수주관리번호";
                                btnOutwareReqID.Text = "출고지시\r\n번호";
                                btnchange.Enabled = false;
                            }
                            else
                            {
                                txtOutwareReqID.Text = dr["OrderID"].ToString();//관리번호
                                txtOrderID.Text = dr["OrderID"].ToString();//관리번호
                                btnchange.Text = "출고지시번호";
                                btnOutwareReqID.Text = "수주관리\r\n번호";
                                btnchange.Enabled = false;
                            }

                            //출고구분
                            index = cboOutClss.FindString(dr["OutClssName"].ToString());

                            if (index != -1)
                            {
                                cboOutClss.SelectedIndex = index;
                            }
                            else
                            {
                                cboOutClss.SelectedIndex = 0;
                            }

                            mtb_ODate.Text = dr["OutDate"].ToString();//출고일자
                            txtOrderCustom.Text = dr["CustomName"].ToString();//수주거래처
                            txtOrderCustomTag.Text = dr["CustomID"].ToString();//수주거래처ID
                            txtOutCustom.Text = dr["DvlyCustom"].ToString();//납품거래처
                            txtOutCustomTag.Text = dr["DvlyCustomID"].ToString();//납품거래처ID
                            txtInCustom.Text = dr["Outcustom"].ToString();//출고처
                            txtInCustomTag.Text = dr["OutcustomID"].ToString();//출고처ID
                            txtArticle.Text = dr["BuyerArticleNo"].ToString();//품번
                            txtBuyerArticle.Text = dr["Article"].ToString();//품명
                            txtArticleTag.Text = dr["ArticleID"].ToString();//ArticleID
                            txtBoxQty.Text = dr["OutRoll"].ToString();//박스
                            txtQty.Text = dr["OutQty"].ToString();//수량
                            txtLoc.Text = dr["ToLocName"].ToString();//창고
                            txtLocTag.Text = dr["ToLocID"].ToString();//창고ID

                            //txtPerson.Text = dr["CreateUser"].ToString();//출고자
                            //txtPersonTag.Text = dr["CreateUserID"].ToString();//출고자ID

                            //출고자
                            index = cboPerson.FindString(dr["CreateUser"].ToString());

                            if (index != -1)
                            {
                                cboPerson.SelectedIndex = index;
                            }
                            else
                            {
                                cboPerson.SelectedIndex = 0;
                            }

                        }

                    }
                }

                //LabelID 그리드
                OutwareSub();
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

        //하위 그리드 보여주기
        private void OutwareSub()
        {
            try
            {
                int i = dgvOutware.Rows.Count;

                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Clear();

                sqlParameter.Add("OutwareID", u_OutwareID); //OutwareID

                DataSet ds = DataStore.Instance.ProcedureToDataSet("xp_WizWork_sOutwareSub_U", sqlParameter, true);

                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        DataRowCollection drc = dt.Rows;

                        foreach (DataRow dr in drc)
                        {
                            dgvOutware.Rows.Add(++i,
                                                dr["LOTID"],         //라벨번호
                                                dr["ArticleGrp"],    //품명그룹
                                                dr["BuyerArticleNo"],    //품명
                                                dr["Article"],           //품번
                                                Lib.ConvertDouble(dr["Qty"].ToString()).ToString(), //수량
                                                dr["UnitClssName"],//단위
                                                                    //dr["LOC"], //창고
                                                "", //삭제
                                                    //dr["LOCID"], //창고ID
                                                dr["ArticleGrpID"], //품명그룹ID
                                                dr["ArticleID"],    //ArticleID
                                                dr["UnitClss"],//단위ID
                                                dr["OrderID"] //관리번호
                                                );
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

        #endregion

        #region 콤보박스 설정

        private void SetComboBox()
        {
            SetcboOutClss();
            SetcboPerson();
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

        //출고자
        private void SetcboPerson()
        {
            cboPerson.Items.Clear();
            //출고자
            ds = DataStore.Instance.ProcedureToDataSet("[xp_WizWork_SPerson]", null, false);
            DataRow newRow = ds.Tables[0].NewRow();
            newRow["PersonID"] = "*";
            newRow["Name"] = "전체";
            ds.Tables[0].Rows.InsertAt(newRow, 0);
            cboPerson.DataSource = ds.Tables[0];
            cboPerson.ValueMember = "PersonID";
            cboPerson.DisplayMember = "Name";
        }



        #endregion

        //출고지시 없이 입력할 경우를 위해 수주번호로도 입력할수 있게 수주번호로 바꾸는 이벤트 추가
        private void btnchange_Click(object sender, EventArgs e)
        {
            if (btnchange.Text == "수주관리번호")
            {
                btnchange.Text = "출고지시번호";
                btnOutwareReqID.Text = "수주관리\r\n번호";
            }
            else
            {
                btnchange.Text = "수주관리번호";
                btnOutwareReqID.Text = "출고지시\r\n번호";
            }
        }
    }
}
