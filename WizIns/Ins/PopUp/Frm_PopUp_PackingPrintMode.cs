using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WizCommon;
using WizWork;

//*******************************************************************************
//프로그램명    Frm_PopUp_Packing.cs
//메뉴ID        
//설명          Frm_PopUp_Packing 메인소스입니다.
//작성일        2019.07.29
//개발자        허윤구
//*******************************************************************************
// 변경일자     변경자      요청자      요구사항ID          요청 및 작업내용
//*******************************************************************************
//  19_0729     허윤구  * 성형 하나에 재단 2개가 필요한 케이스에 따라 수정보완.
//                          (InsertX)
//  2019.08.01 > 허윤구    FMB가 어떤 이유로 이미 재단창고에 가 있을 케이스에 대한 로직추가.
//*******************************************************************************

namespace WizIns
{
    public partial class Frm_PopUp_PackingPrintMode : Form
    {
        string[] Message = new string[2];  // 메시지박스 처리용도.

        WizWorkLib Lib = new WizWorkLib();
        Frm_tins_Main Ftm = new Frm_tins_Main(); //2022-06-23

        public string TagID = "";

        public Frm_PopUp_PackingPrintMode()
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

        private void Frm_PopUp_PackingPrintMode_Load(object sender, EventArgs e)
        {
            this.Size = new Size(650, 320);
            //Ftm.LogSave(this.GetType().Name, "S"); //2022-06-23 사용시간(로드, 닫기)
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            this.DialogResult = DialogResult.No;
        }

        private void btnTagID_Click(object sender, EventArgs e)
        {
            TagID = "013";
            this.DialogResult = DialogResult.OK;

        }

        private void btnTagID2_Click(object sender, EventArgs e)
        {
            TagID = "014";
            this.DialogResult = DialogResult.OK;
        }
    }
}
