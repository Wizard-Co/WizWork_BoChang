using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using WizCommon;
using System.IO;


namespace WizInOut
{
    public partial class Frm_PopUpSel_Common : Form
    {
        public delegate void STextEventHandler(string CodeID, string CodeName, string UnitClss, string UnitClssID, string PriceClss, string PriceClssID, string ArticleGrp, string Ok);  //입고 이벤트, string을 반환값으로 갖는 대리자를 선언합니다.       
        public delegate void OTextEventHandler(string Custom, string InCustom, string OrderID, string OrderSeq, string BuyerArticleNo, string OrderQty, string UnitClss, string Model, string Article, string DvlyDate, string Work, string ArticleGrp, string CustomID, string ArticleID, string UnitClssID, string BuyerModelID, string WorkID, string InCustomID, string ArticleGrpID, string OK); //출고 이벤트
                                                    //거래처, 관리번호, 품목명, 수주량, 단위, 차종, 품번, 납기일자, 가공구분, 품명그룹, CustomID, ArticleID, UnitClss, BuyerModelID, WorkID, ArticleGrpID


        public event STextEventHandler SWriteTextEvent;  //입고 이벤트, 대리자 타입의 이벤트 처리기를 설정합니다.
        public event OTextEventHandler OWriteTextEvent;  //출고 이벤트

        string GBN = "";
        string Condi = "";
        string CondiID = "";
        string SOGbn = "";

        public Frm_PopUpSel_Common()
        {
            InitializeComponent();
        }

        public Frm_PopUpSel_Common(string GBN, string TextBox, string CondiID, string SOGbn)
        {
            InitializeComponent();

            this.GBN = GBN; //구분
            this.Condi = TextBox; //텍스트박스
            this.CondiID = CondiID; //조건(거래처)
            this.SOGbn = SOGbn; //(입고에서 호출(S)/출고에서 호출 구분(O))
            this.TextBox.Text = Condi;

        }

        private void Frm_PopUpSel_Common_Load(object sender, EventArgs e)
        {
            SetScreen();

            //입고에서 호출
            if (SOGbn == "S")
            {

                InitGrid();
                if (GBN != "VAT" && GBN != "IYN")
                {
                    FillGrid();

                    if (Condi != "")
                    {
                        chkArticle.Checked = true;
                        if (GBN != "MA")
                        {
                            chkBuyerArticle.Visible = false;
                            tableLayoutPanel1.SetColumnSpan(chkArticle, 2);

                            if (GBN == "ICD")
                            {
                                chkArticle.Text = "입고구분";
                                grdData.Columns[1].HeaderText = chkArticle.Text;
                                grdData.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                                //발주관련 열
                                grdData.Columns[2].Visible = false;
                                grdData.Columns[3].Visible = false;
                                grdData.Columns[4].Visible = false;
                                grdData.Columns[5].Visible = false;
                                grdData.Columns[6].Visible = false;
                            }
                            else if (GBN == "MC")
                            {
                                chkArticle.Text = "거래처";
                                grdData.Columns[1].HeaderText = chkArticle.Text;
                                grdData.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                                //발주관련 열
                                grdData.Columns[2].Visible = false;
                                grdData.Columns[3].Visible = false;
                                grdData.Columns[4].Visible = false;
                                grdData.Columns[5].Visible = false;
                                grdData.Columns[6].Visible = false;
                            }
                            else if (GBN == "REQ")
                            {
                                chkArticle.Text = "발주번호";
                                grdData.Columns[1].HeaderText = chkArticle.Text;
                            }
                            else if (GBN == "LOC")
                            {
                                chkArticle.Text = "후창고";
                                grdData.Columns[1].HeaderText = chkArticle.Text;
                                grdData.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                                //발주관련 열
                                grdData.Columns[2].Visible = false;
                                grdData.Columns[3].Visible = false;
                                grdData.Columns[4].Visible = false;
                                grdData.Columns[5].Visible = false;
                                grdData.Columns[6].Visible = false;
                            }
                            else if (GBN == "InP")
                            {
                                chkArticle.Text = "검사자";
                                grdData.Columns[1].HeaderText = chkArticle.Text;
                                grdData.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                                //발주관련 열
                                grdData.Columns[2].Visible = false;
                                grdData.Columns[3].Visible = false;
                                grdData.Columns[4].Visible = false;
                                grdData.Columns[5].Visible = false;
                                grdData.Columns[6].Visible = false;
                            }
                        }
                        else
                        {
                            //grdData.Columns[2].HeaderText = "품번";
                            grdData.Columns[1].Visible = false;
                            grdData.Columns[2].Visible = false;
                            grdData.Columns[3].Visible = false;
                            grdData.Columns[6].Visible = false;
                        }
                    }
                    else
                    {
                        if (GBN != "MA")
                        {
                            chkBuyerArticle.Visible = false;
                            tableLayoutPanel1.SetColumnSpan(chkArticle, 2);

                            if (GBN == "ICD")
                            {
                                chkArticle.Text = "입고구분";
                                grdData.Columns[1].HeaderText = chkArticle.Text;
                                grdData.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                                //발주관련 열
                                grdData.Columns[2].Visible = false;
                                grdData.Columns[3].Visible = false;
                                grdData.Columns[4].Visible = false;
                                grdData.Columns[5].Visible = false;
                                grdData.Columns[6].Visible = false;
                            }
                            else if (GBN == "MC")
                            {
                                chkArticle.Text = "거래처";
                                grdData.Columns[1].HeaderText = chkArticle.Text;
                                grdData.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                                //발주관련 열
                                grdData.Columns[2].Visible = false;
                                grdData.Columns[3].Visible = false;
                                grdData.Columns[4].Visible = false;
                                grdData.Columns[5].Visible = false;
                                grdData.Columns[6].Visible = false;
                            }
                            else if (GBN == "REQ")
                            {
                                chkArticle.Text = "발주번호";
                                grdData.Columns[1].HeaderText = chkArticle.Text;
                            }
                            else if (GBN == "LOC")
                            {
                                chkArticle.Text = "후창고";
                                grdData.Columns[1].HeaderText = chkArticle.Text;
                                grdData.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                                //발주관련 열
                                grdData.Columns[2].Visible = false;
                                grdData.Columns[3].Visible = false;
                                grdData.Columns[4].Visible = false;
                                grdData.Columns[5].Visible = false;
                                grdData.Columns[6].Visible = false;
                            }
                            else if (GBN == "InP")
                            {
                                chkArticle.Text = "검수자";
                                grdData.Columns[1].HeaderText = chkArticle.Text;
                                grdData.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                                grdData.Columns[2].HeaderText = "검수자ID";
                                grdData.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                                //발주관련 열
                                grdData.Columns[3].Visible = false;
                                grdData.Columns[4].Visible = false;
                                grdData.Columns[5].Visible = false;
                                grdData.Columns[6].Visible = false;
                            }

                        }
                        else
                        {
                            //grdData.Columns[2].HeaderText = "품번";
                            grdData.Columns[1].Visible = false;
                            grdData.Columns[2].Visible = false;
                            grdData.Columns[3].Visible = false;
                            grdData.Columns[6].Visible = false;
                        }
                    }
                }
                else
                {
                    chkBuyerArticle.Visible = false;
                    tableLayoutPanel1.SetColumnSpan(chkArticle, 2);

                    List<string> VAT = new List<string>();

                    VAT.Add("Y");
                    VAT.Add("N");
                    if (GBN == "VAT")
                    {
                        VAT.Add("0");
                    }

                    //부가세는 Y,N,O 3가지 밖에 없어서 3으로 하드 코딩 2023-08-24
                    for (int i = 0; i < VAT.Count; i++)
                    {
                        grdData.Rows.Add(i + 1,                       //num
                                       VAT[i].ToString(),       //코드네임(품명)
                                       "",                          //발주순번
                                       "",                          //발주일
                                       "",                          //품명
                                       "",                          //품번
                                       "",                          //잔량
                                       "",                          //입고단위                                   
                                       "",                          //화폐단위
                                       "",                          //원자재 그룹
                                       VAT[i].ToString());
                    }

                    if (GBN == "VAT")
                    {
                        chkArticle.Text = "부가세 별도";
                        grdData.Columns[1].HeaderText = chkArticle.Text;
                        grdData.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        //발주관련 열
                        grdData.Columns[2].Visible = false;
                        grdData.Columns[3].Visible = false;
                        grdData.Columns[4].Visible = false;
                        grdData.Columns[5].Visible = false;
                        grdData.Columns[6].Visible = false;
                    }
                    else
                    {
                        chkArticle.Text = "검사필요여부";
                        grdData.Columns[1].HeaderText = chkArticle.Text;
                        grdData.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        //발주관련 열
                        grdData.Columns[2].Visible = false;
                        grdData.Columns[3].Visible = false;
                        grdData.Columns[4].Visible = false;
                        grdData.Columns[5].Visible = false;
                        grdData.Columns[6].Visible = false;
                    }

                }
            }
            else //출고에서 호출
            {
                InitGridO();

                FillGrid();

                //품명, 품번에서 호출한 경우 아닌 경우
                if (GBN != "MA" && GBN != "OutwareReqID" && GBN !="OrderID")
                {
                    chkBuyerArticle.Visible = false;
                    tableLayoutPanel1.SetColumnSpan(chkArticle, 2);
                }

                //각 항목마다 조건 다르게 표시
                //품명, 품번
                if (GBN == "MA")
                {
                    //grdData.Columns[1].HeaderText = "품목명";
                    grdData.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    grdData.Columns[1].Visible = false;
                    grdData.Columns[2].Visible = false;
                    grdData.Columns[3].Visible = false;                  
                    grdData.Columns[4].Visible = false;
                    grdData.Columns[6].Visible = false;
                    grdData.Columns[7].Visible = false;
                    grdData.Columns[9].Visible = false;
                    grdData.Columns[10].Visible = false;
                    grdData.Columns[11].Visible = false;
                    grdData.Columns[12].Visible = false;
                }
                //거래처
                else if(GBN == "Custom")
                {
                    chkArticle.Text = "거래처";
                    grdData.Columns[1].HeaderText = chkArticle.Text;
                    grdData.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    grdData.Columns[2].Visible = false;
                    grdData.Columns[3].Visible = false;
                    grdData.Columns[4].Visible = false;
                    grdData.Columns[5].Visible = false;
                    grdData.Columns[6].Visible = false;
                    grdData.Columns[7].Visible = false;
                    grdData.Columns[8].Visible = false;
                    grdData.Columns[9].Visible = false;
                    grdData.Columns[10].Visible = false;
                    grdData.Columns[11].Visible = false;
                    grdData.Columns[12].Visible = false;
                }
                //출고구분
                else if(GBN == "OCD")
                {
                    chkArticle.Text = "출고구분";
                    grdData.Columns[1].HeaderText = chkArticle.Text;
                    grdData.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    grdData.Columns[2].Visible = false;
                    grdData.Columns[3].Visible = false;
                    grdData.Columns[4].Visible = false;
                    grdData.Columns[5].Visible = false;
                    grdData.Columns[6].Visible = false;
                    grdData.Columns[7].Visible = false;
                    grdData.Columns[8].Visible = false;
                    grdData.Columns[9].Visible = false;
                    grdData.Columns[10].Visible = false;
                    grdData.Columns[11].Visible = false;
                    grdData.Columns[12].Visible = false;
                }
                //창고
                else if (GBN == "LOC")
                {
                    chkArticle.Text = "후창고";
                    grdData.Columns[1].HeaderText = chkArticle.Text;
                    grdData.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    grdData.Columns[2].Visible = false;
                    grdData.Columns[3].Visible = false;
                    grdData.Columns[4].Visible = false;
                    grdData.Columns[5].Visible = false;
                    grdData.Columns[6].Visible = false;
                    grdData.Columns[7].Visible = false;
                    grdData.Columns[8].Visible = false;
                    grdData.Columns[9].Visible = false;
                    grdData.Columns[10].Visible = false;
                    grdData.Columns[11].Visible = false;
                    grdData.Columns[12].Visible = false;

                }
                //출고자
                else if (GBN == "InP")
                {
                    chkArticle.Text = "출고자";
                    grdData.Columns[1].HeaderText = chkArticle.Text;
                    grdData.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    grdData.Columns[2].Visible = false;
                    grdData.Columns[3].HeaderText = "출고자ID";
                    grdData.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    grdData.Columns[3].Visible = true;
                    grdData.Columns[4].Visible = false;
                    grdData.Columns[5].Visible = false;
                    grdData.Columns[6].Visible = false;
                    grdData.Columns[7].Visible = false;
                    grdData.Columns[8].Visible = false;
                    grdData.Columns[9].Visible = false;
                    grdData.Columns[10].Visible = false;
                    grdData.Columns[11].Visible = false;
                    grdData.Columns[12].Visible = false;
                }
                //출고지시번호
                else if(GBN == "OutwareReqID")
                {
                    chkBuyerArticle.Text = "출고지시번호";
                    grdData.Columns[1].HeaderText = "관리번호"; //관리번호
                    //grdData.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    grdData.Columns[2].HeaderText = "출고지시번호"; //출고지시번호
                    grdData.Columns[2].Visible = true;
                    grdData.Columns[3].HeaderText = "고객사"; //고객사
                    grdData.Columns[3].Visible = true;
                    grdData.Columns[4].HeaderText = "최종고객사"; 
                    grdData.Columns[4].Visible = true; //최종고객사
                    grdData.Columns[5].HeaderText = "품명"; 
                    grdData.Columns[5].Visible = true; //품명
                    grdData.Columns[6].HeaderText = "지시일자";
                    grdData.Columns[6].Visible = true; //지시일자
                    grdData.Columns[7].HeaderText = "지시수량";
                    grdData.Columns[7].Visible = true; //지시수량
                    grdData.Columns[8].HeaderText = "출고수량";
                    grdData.Columns[8].Visible = true; //출고수량
                    grdData.Columns[9].HeaderText = "출고잔량";
                    grdData.Columns[9].Visible = true; //출고잔량
                    grdData.Columns[10].Visible = false;
                    grdData.Columns[11].Visible = false;
                    grdData.Columns[12].Visible = false;
                }
                //수주관리번호
                else
                {
                    chkBuyerArticle.Text = "수주관리번호";
                }

            }
        }

        //tableLayoutPanel 세팅
        private void SetScreen()
        {
            tlpMain.Dock = DockStyle.Fill;
            tlpMain.Margin = new Padding(0, 0, 0, 0);
            foreach (Control control in tlpMain.Controls)
            {
                control.Dock = DockStyle.Fill;
                control.Margin = new Padding(0, 0, 0, 0);
                foreach (Control contro in control.Controls)
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
                        }
                    }
                }
            }
        }

        //그리드 컬럼 셋팅
        private void InitGrid()
        {
            grdData.Columns.Clear(); //체크박스나 콤보박스 사용시 필요하다.
            grdData.ColumnCount = 11;

            int i = 0;

            grdData.Columns[i].Name = "NUM";
            grdData.Columns[i].HeaderText = "";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "CodeName";
            grdData.Columns[i].HeaderText = "품명";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "Req";
            grdData.Columns[i].HeaderText = "순번";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "ReqDate";
            grdData.Columns[i].HeaderText = "발주일";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "Article";
            grdData.Columns[i].HeaderText = "품명";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "BuyerArticleNo";
            grdData.Columns[i].HeaderText = "품번";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "Qty";
            grdData.Columns[i].HeaderText = "잔량";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "UnitClss";
            grdData.Columns[i].HeaderText = "UnitClss";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;

            grdData.Columns[++i].Name = "PriceClss";
            grdData.Columns[i].HeaderText = "PriceClss";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;

            grdData.Columns[++i].Name = "ArticleGrp";
            grdData.Columns[i].HeaderText = "ArticleGrp";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;

            grdData.Columns[++i].Name = "CodeID";
            grdData.Columns[i].HeaderText = "ID";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;

            grdData.Font = new Font("맑은 고딕", 15, FontStyle.Bold);
            grdData.RowTemplate.Height = 30;
            grdData.ColumnHeadersHeight = 35;
            grdData.ScrollBars = ScrollBars.Both;
            grdData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grdData.MultiSelect = false;
            grdData.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            foreach (DataGridViewColumn col in grdData.Columns)
            {
                col.DataPropertyName = col.Name;
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void InitGridO()
        {
            grdData.Columns.Clear(); 
            grdData.ColumnCount = 20;

            int i = 0;

            grdData.Columns[i].Name = "NUM";
            grdData.Columns[i].HeaderText = "";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            //거래처
            grdData.Columns[++i].Name = "Custom";
            grdData.Columns[i].HeaderText = "거래처";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            //매출거래처
            grdData.Columns[++i].Name = "InCustom";
            grdData.Columns[i].HeaderText = "매출거래처";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;

            //관리번호
            grdData.Columns[++i].Name = "OrderID";
            grdData.Columns[i].HeaderText = "수주관리번호";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            //OrderSeq
            grdData.Columns[++i].Name = "OrderSeq";
            grdData.Columns[i].HeaderText = "OrderSeq";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            //품목명
            grdData.Columns[++i].Name = "BuyerArticleNo";
            grdData.Columns[i].HeaderText = "품목명";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            //수주량
            grdData.Columns[++i].Name = "OrderQty";
            grdData.Columns[i].HeaderText = "수주량";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            //단위
            grdData.Columns[++i].Name = "UnitClss";
            grdData.Columns[i].HeaderText = "단위";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            //차종
            grdData.Columns[++i].Name = "BuyerModel";
            grdData.Columns[i].HeaderText = "차종";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            //품번
            grdData.Columns[++i].Name = "Article";
            grdData.Columns[i].HeaderText = "품번";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            //납기일자
            grdData.Columns[++i].Name = "DvlyDate";
            grdData.Columns[i].HeaderText = "납기일자";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            //가공구분
            grdData.Columns[++i].Name = "Work";
            grdData.Columns[i].HeaderText = "가공구분";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            //품명그룹
            grdData.Columns[++i].Name = "ArticleGrp";
            grdData.Columns[i].HeaderText = "품명그룹";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            //CustomID
            grdData.Columns[++i].Name = "CustomID";
            grdData.Columns[i].HeaderText = "CustomID";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;

            //ArticleID
            grdData.Columns[++i].Name = "ArticleID";
            grdData.Columns[i].HeaderText = "ArticleID";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;

            //UnitClssID
            grdData.Columns[++i].Name = "UnitClssID";
            grdData.Columns[i].HeaderText = "UnitClssID";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;

            //BuyerModelID
            grdData.Columns[++i].Name = "BuyerModelID";
            grdData.Columns[i].HeaderText = "BuyerModelID";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;

            //WorkID
            grdData.Columns[++i].Name = "WorkID";
            grdData.Columns[i].HeaderText = "WorkID";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;

            //매출거래처
            grdData.Columns[++i].Name = "InCustomID";
            grdData.Columns[i].HeaderText = "InCustomID";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;

            //품명그룹ID
            grdData.Columns[++i].Name = "ArticleGrpID";
            grdData.Columns[i].HeaderText = "ArticleGrpID";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;

            grdData.Font = new Font("맑은 고딕", 15, FontStyle.Bold);
            grdData.RowTemplate.Height = 30;
            grdData.ColumnHeadersHeight = 35;
            grdData.ScrollBars = ScrollBars.Both;
            grdData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grdData.MultiSelect = false;
            grdData.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            foreach (DataGridViewColumn col in grdData.Columns)
            {
                col.DataPropertyName = col.Name;
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        //그리드에 콤보박스 인덱스(부서)에 따른 사용자 세팅
        private void FillGrid()
        {
            try
            {
                grdData.Rows.Clear();
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();

                if (chkBuyerArticle.Visible == false)
                {
                    //조건이 하나만 있는 겨우
                    sqlParameter.Add("chkYN", 1); //체크박스 여부 1이면 체크
                    sqlParameter.Add("chkYN2", 0); //체크박스 여부
                }
                else
                {
                    //품명, 품번과 같이 조건이 2개가 있는 경우
                    if (chkBuyerArticle.Checked) 
                    {                    
                        sqlParameter.Add("chkYN", 0); //체크박스 여부  chkArticle
                        sqlParameter.Add("chkYN2", 1); //체크박스 여부 chkBuyerArticle
                    }
                    else if(chkArticle.Checked)
                    {
                        sqlParameter.Add("chkYN", 1); //체크박스 여부  chkArticle
                        sqlParameter.Add("chkYN2", 0); //체크박스 여부 chkBuyerArticle
                    }
                    else
                    {
                        sqlParameter.Add("chkYN", 0); //체크박스 여부  chkArticle
                        sqlParameter.Add("chkYN2", 0); //체크박스 여부 chkBuyerArticle
                    }
                }

                sqlParameter.Add("TextBox", TextBox.Text.Trim()); //내용(텍스트박스 like)
                sqlParameter.Add("GBN", GBN); //공통
                sqlParameter.Add("CondiID", CondiID); //조건(거래처 입력후 거래처 있는 경우)
                sqlParameter.Add("SOGBN", SOGbn); //입고에서 호출, 출고에서 호출 구분
                DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_WizWork_ComPopUp", sqlParameter, false);
                if (SOGbn == "S") 
                {
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        grdData.Rows.Add(++i,                       //num
                                        dr["CodeName"].ToString(),  //코드네임(품명)
                                        dr["CodeID"].ToString(),    //발주순번
                                        dr["ReqDate"].ToString(),    //발주일
                                        dr["Article"].ToString(),    //품명
                                        dr["BuyerArticleNo"].ToString(),    //품번
                                        dr["Qty"].ToString(),       //잔량
                                        dr["Qty2"].ToString(),      //입고단위                                   
                                        dr["ReqDate2"].ToString(),  //화폐단위
                                        dr["CodeNameB"].ToString(), //원자재 그룹
                                        dr["CodeID"].ToString());
                    }
                }
                else
                {
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        grdData.Rows.Add(++i,                                   //num
                                        dr["KCustom"].ToString(),               //거래처
                                        dr["InCustom"].ToString(),              //매출거래처
                                        dr["OrderID"].ToString(),               //관리번호
                                        dr["OrderSeq"].ToString(),              //OrderSeq
                                        dr["BuyerArticleNo"].ToString(),        //품목명
                                        dr["OrderQty"].ToString(),              //수주량
                                        dr["CodeName"].ToString(),              //단위
                                        dr["Model"].ToString(),                 //차종
                                        dr["Article"].ToString(),               //품번                                   
                                        dr["DvlyDate"].ToString(),              //납기일자
                                        dr["WorkName"].ToString(),              //가공구분
                                        dr["ArticleGrp"].ToString(),            //품명그룹
                                        dr["CustomID"].ToString(),              //CustomID
                                        dr["ArticleID"].ToString(),             //ArticleID
                                        dr["UnitClss"].ToString(),              //UnitClss
                                        dr["BuyerModelID"].ToString(),          //BuyerModelID
                                        dr["WorkID"].ToString(),                //WorkID
                                        dr["InCustomID"].ToString(),            //InCustomID
                                        dr["ArticleGrpID"].ToString()           //ArticleGrpID
                                        );   

                    }
                }
            }
            catch (Exception e)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", e.Message), "[오류]", 0, 1);
            }
            finally
            {
                DataStore.Instance.CloseConnection(); //2021-09-23 DB 커넥트 연결 해제
            }

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string Ok = "";

            if (grdData.Rows.Count == 0 || grdData.SelectedRows.Count == 0)
            {
                WizCommon.Popup.MyMessageBox.ShowBox("선택된 품명이 없습니다. 품명을 선택해주세요.", "[오류]", 0, 1);
                return;
            }

            if (SOGbn == "S")
            {
                //전역변수에 사용자 저장
                string CodeID = "";
                string CodeName = "";

                string UnitClss = "";
                string UnitClssID = "";
                string PriceClss = "";
                string PriceClssID = "";
                string ArticleIDGrp = "";
            
                //전역변수에 사용자 저장
                CodeID = grdData.SelectedRows[0].Cells["CodeID"].Value.ToString();
                CodeName = grdData.SelectedRows[0].Cells["CodeName"].Value.ToString();

                //입고단위
                UnitClss = grdData.SelectedRows[0].Cells["UnitClss"].Value.ToString();

                //입고단위ID
                UnitClssID = grdData.SelectedRows[0].Cells["Qty"].Value.ToString();

                //화폐단위
                PriceClss = grdData.SelectedRows[0].Cells["PriceClss"].Value.ToString();

                //화폐단위ID
                PriceClssID = grdData.SelectedRows[0].Cells["ReqDate"].Value.ToString();

                //원자재 그룹
                ArticleIDGrp = grdData.SelectedRows[0].Cells["ArticleGrp"].Value.ToString();

                Ok = "OK";

                //Frm_tprc_Main.g_tBase.sArticleID = grdData.SelectedRows[0].Cells["ArticleID"].Value.ToString();
                //Frm_tprc_Main.g_tBase.sArticle = grdData.SelectedRows[0].Cells["Article"].Value.ToString();

                SWriteTextEvent(CodeID, CodeName, UnitClss, UnitClssID, PriceClss, PriceClssID, ArticleIDGrp, Ok);
            }
            else
            {
                //거래처
                string Custom = "";
                //매출거래처
                string InCustom = "";
                //관리번호        
                string OrderID = "";
                //OrderSeq
                string OrderSeq = "";
                //품목명
                string BuyerArticleNo = "";
                //수주량        
                string OrderQty = "";
                //단위         
                string OUnitClss = "";
                //차종         
                string Model = "";
                //품번
                string Article = "";
                //납기일자          
                string DvlyDate = "";
                //가공구분
                string Work = "";
                //품명그룹
                string ArticleGrp = "";
                //CustomID           
                string CustomID = "";
                //ArticleID           
                string ArticleID = "";
                //UnitClssID         
                string OUnitClssID = "";
                //BuyerModelID      
                string BuyerModelID = "";
                //WorkID
                string WorkID = "";
                //InCustomID
                string InCustomID = "";
                //ArticleGrpID
                string ArticleGrpID = "";

                //거래처
                Custom = grdData.SelectedRows[0].Cells["Custom"].Value.ToString();
                //매출거래처
                InCustom = grdData.SelectedRows[0].Cells["InCustom"].Value.ToString();
                //관리번호
                OrderID = grdData.SelectedRows[0].Cells["OrderID"].Value.ToString();
                //OrderSeq
                OrderSeq = grdData.SelectedRows[0].Cells["OrderSeq"].Value.ToString();
                //품목명
                BuyerArticleNo = grdData.SelectedRows[0].Cells["BuyerArticleNo"].Value.ToString();
                //수주량
                OrderQty = grdData.SelectedRows[0].Cells["OrderQty"].Value.ToString();
                //단위
                OUnitClss = grdData.SelectedRows[0].Cells["UnitClss"].Value.ToString();
                //차종
                Model = grdData.SelectedRows[0].Cells["BuyerModel"].Value.ToString();
                //품번
                Article = grdData.SelectedRows[0].Cells["Article"].Value.ToString();
                //납기일자
                DvlyDate = grdData.SelectedRows[0].Cells["DvlyDate"].Value.ToString();
                //가공구분
                Work = grdData.SelectedRows[0].Cells["Work"].Value.ToString();
                //품명그룹
                ArticleGrp = grdData.SelectedRows[0].Cells["ArticleGrp"].Value.ToString();
                //CustomID
                CustomID = grdData.SelectedRows[0].Cells["CustomID"].Value.ToString();
                //ArticleID
                ArticleID = grdData.SelectedRows[0].Cells["ArticleID"].Value.ToString();
                //UnitClssID
                OUnitClssID = grdData.SelectedRows[0].Cells["UnitClssID"].Value.ToString();
                //BuyerModelID
                BuyerModelID = grdData.SelectedRows[0].Cells["BuyerModelID"].Value.ToString();
                //WorkID
                WorkID = grdData.SelectedRows[0].Cells["WorkID"].Value.ToString();
                //InCustomID
                InCustomID = grdData.SelectedRows[0].Cells["InCustomID"].Value.ToString();
                //ArticleGrpID
                ArticleGrpID = grdData.SelectedRows[0].Cells["ArticleGrpID"].Value.ToString();

                Ok = "OK";

                OWriteTextEvent(Custom,InCustom, OrderID, OrderSeq, BuyerArticleNo, OrderQty, OUnitClss, Model, Article, DvlyDate, Work, ArticleGrp, CustomID, ArticleID, OUnitClssID, BuyerModelID, WorkID, InCustomID, ArticleGrpID, Ok);
            }

            this.Dispose();
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (SOGbn == "S") 
            {
                SWriteTextEvent("", "", "", "", "", "", "", "Cancel");
            }
            else
            {
                OWriteTextEvent("","","","","","","","","","","","","", "", "", "", "", "", "", "Cancel");
            }

            this.Dispose();
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (GBN != "VAT" && GBN != "IYN") 
            {
                FillGrid();
            }
        }

        private void chkArticle_Click(object sender, EventArgs e)
        {

            if (chkArticle.Checked) 
            {
                chkBuyerArticle.Checked = false;
                //2021-07-20
                var path64 = System.IO.Path.Combine(Directory.GetDirectories(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "winsxs"), "amd64_microsoft-windows-osk_*")[0], "osk.exe");
                var path32 = @"C:\windows\system32\osk.exe";
                var path = (Environment.Is64BitOperatingSystem) ? path64 : path32;
                if (File.Exists(path) && !Frm_tinout_Main.Lib.ReturnKillRunningProcess("osk"))
                {
                    System.Diagnostics.Process.Start(path);

                    TextBox.Focus();

                }
            }

        }

        private void chkBuyerArticle_Click(object sender, EventArgs e)
        {
            if (chkBuyerArticle.Checked) 
            {
                chkArticle.Checked = false;
                //2021-07-20
                var path64 = System.IO.Path.Combine(Directory.GetDirectories(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "winsxs"), "amd64_microsoft-windows-osk_*")[0], "osk.exe");
                var path32 = @"C:\windows\system32\osk.exe";
                var path = (Environment.Is64BitOperatingSystem) ? path64 : path32;
                if (File.Exists(path) && !Frm_tinout_Main.Lib.ReturnKillRunningProcess("osk"))
                {
                    System.Diagnostics.Process.Start(path);

                    TextBox.Focus();

                }
            }
        }

        private void TextBox_Click(object sender, EventArgs e)
        {
            //2021-07-20
            var path64 = System.IO.Path.Combine(Directory.GetDirectories(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "winsxs"), "amd64_microsoft-windows-osk_*")[0], "osk.exe");
            var path32 = @"C:\windows\system32\osk.exe";
            var path = (Environment.Is64BitOperatingSystem) ? path64 : path32;
            if (File.Exists(path) && !Frm_tinout_Main.Lib.ReturnKillRunningProcess("osk"))
            {
                System.Diagnostics.Process.Start(path);

                TextBox.Focus();

            }
        }


    }
}
