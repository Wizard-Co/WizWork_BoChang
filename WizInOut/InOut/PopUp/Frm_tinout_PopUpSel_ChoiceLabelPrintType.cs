using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Drawing.Printing;

//*******************************************************************************
//프로그램명    Frm_tinout_PopUpSel_ChoiceLabelPrintType.cs
//메뉴ID        
//설명          Frm_tinout_PopUpSel_ChoiceLabelPrintType 메인소스입니다.
//              출고시 엑셀로 거래명세서 인쇄할 경우 엑셀이 없는 경우 인쇄 못하게
//              막기 위해 선택 팝업창 생성
//작성일        2025.01.14
//개발자        KDH
//*******************************************************************************
// 변경일자     변경자      요청자      요구사항ID          요청 및 작업내용
//*******************************************************************************

//*******************************************************************************

namespace WizInOut
{
    public partial class Frm_tinout_PopUpSel_ChoiceLabelPrintType : Form
    {
        public delegate void TextEventHandler(string Message);    // string을 반환값으로 갖는 대리자를 선언합니다.
        public event TextEventHandler WriteTextEvent;          // 대리자 타입의 이벤트 처리기를 설정합니다.
        public static RegistryKey excelKey = null; //2024-02-07 엑셀 설치 여부 확인

        public Frm_tinout_PopUpSel_ChoiceLabelPrintType()
        {
            InitializeComponent();
            SetScreen();  //TLP 사이즈 조정
        }

        #region 테이블 레이아웃 패널 사이즈 조정
        private void SetScreen()
        {
            tlpMain.Dock = DockStyle.Fill;
            foreach (Control control in tlpMain.Controls)
            {
                control.Dock = DockStyle.Fill;
                control.Margin = new Padding(1, 1, 1, 1);
                foreach (Control contro in control.Controls)
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
                                }
                            }
                        }
                    }
                }
            }
        }

        #endregion

        // 로드 이벤트.
        private void Frm_tinout_PopUpSel_ChoiceLabelPrintType_Load(object sender, EventArgs e)
        {
            excelKey = Registry.ClassesRoot.OpenSubKey(@"Excel.Application");
            string defaultPrinter = new PrinterSettings().PrinterName;

            btnSave.Text = "저장";

            //null이 아니라면 엑셀 설치 됨
            if(excelKey != null)
            {
                if (!string.IsNullOrEmpty(defaultPrinter))
                {
                    btnExcel.Text = $"발행 \r\n기본 프린터 : {defaultPrinter} ";
                }
                else
                {
                    btnExcel.Text = "발행 \r\n기본 프린터 : 설정 안 됨";
                }
            }
            else
            {
                btnExcel.Text = "엑셀 설치가 안 되어 발행 불가";
            }
        }

        //excel
        private void btnExcel_Click(object sender, EventArgs e)
        {
            WriteTextEvent("Excel");
            this.Close();
        }

        //저장
        private void btnSave_Click(object sender, EventArgs e)
        {
            WriteTextEvent("Save");
            this.Close();
        }

        // 취소버튼 클릭.
        private void btnCancel_Click(object sender, EventArgs e)
        {
            WriteTextEvent("Cancel");
            this.Close();
        }

    }
}
