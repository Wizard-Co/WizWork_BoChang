using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WizWork.BoChang.PopUp
{
    public partial class Frm_PopUp_Work_Save_U : Form
    {
        string Article = "";    //가져온 품명
        string WorkQty = "";    //가져온 생산수량
        string DefectQty = "";  //가져온 불량수량
        string PreWorkQty = ""; //가져온 이전 생산 수량
        Dictionary<string, frm_tprc_Work_Defect_U_CodeView> dicDefect = new Dictionary<string, frm_tprc_Work_Defect_U_CodeView>(); //가져온 불량내역
        public string PopUpWorkQty = ""; //생산실적화면 하단 그리드에서 보여줄 생산수량
        public string PopUpDefectQty = ""; //생산실적화면에서 저장시 사용할 불량수량
        public Dictionary<string, frm_tprc_Work_Defect_U_CodeView> PopUpdicDefect = new Dictionary<string, frm_tprc_Work_Defect_U_CodeView>(); //생산실적화면에서 저장시 사용할 불량내역
        string[] Message = new string[2]; //메세지

        public Frm_PopUp_Work_Save_U(string Article, string WorkQty, string DefectQty, string PreWorkQty, Dictionary<string, frm_tprc_Work_Defect_U_CodeView> dicDefect)
        {
            InitializeComponent();
            this.Article = Article;
            this.WorkQty = WorkQty;
            this.DefectQty = DefectQty;
            this.PreWorkQty = PreWorkQty;
            this.dicDefect = dicDefect;

        }

        #region 로드 이벤트

        private void Frm_PopUp_Work_Save_U_Load(object sender, EventArgs e)
        {
            txtArticle.Text = Article; //품명

            if(WorkQty == "") 
            {
                txtWorkQty.Text = "0";
            }
            else
            {
                txtWorkQty.Text = WorkQty; //생산수량
            }
            
            if (dicDefect.Count > 0)
            {
                txtDefectQty.Text = DefectQty;
            }
            else
            {
                txtDefectQty.Text = "0"; //불량수량
            }

            if(PreWorkQty == "")
            {
                txtPreWorkQty.Text = "0";
            }
            else
            {
                txtPreWorkQty.Text = PreWorkQty; //이전 생산수량
            }
        }

        #endregion

        #region 저장, 취소 버튼 이벤트

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtWorkQty.Text.ToString()) < Convert.ToInt32(txtDefectQty.Text.ToString()))
            {
                Message[0] = "[수량 체크]";
                Message[1] = "작업수량보다 불량수량이 많습니다.";
                WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 3, 1);
                return;

            }
            else 
            {
                PopUpWorkQty = txtWorkQty.Text.ToString();
                PopUpDefectQty = txtDefectQty.Text.ToString();
                PopUpdicDefect = dicDefect;
                this.DialogResult = DialogResult.OK;
            }
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }


        #endregion 

        #region 수량, 불량 클릭 이벤트

        private void txtWorkQty_Click(object sender, EventArgs e)
        {
            POPUP.Frm_CMNumericKeypad keypad = new POPUP.Frm_CMNumericKeypad("수량입력", "수량");

            keypad.Owner = this;
            if (keypad.ShowDialog() == DialogResult.OK)
            {
                txtWorkQty.Text = keypad.tbInputText.Text;
            }
        }

        private void txtDefectQty_Click(object sender, EventArgs e)
        {
            frm_tprc_Work_Defect_U defect = new frm_tprc_Work_Defect_U(dicDefect);
            defect.Owner = this;
            defect.ShowDialog();
            if (defect.DialogResult == DialogResult.OK)
            {
                this.dicDefect = defect.dicDefect;
                this.txtDefectQty.Text = defect.returnTotalQty;
            }
            return;
        }



        #endregion
    }
}
