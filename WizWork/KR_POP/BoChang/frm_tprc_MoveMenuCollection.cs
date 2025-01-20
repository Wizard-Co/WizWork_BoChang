using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WizWork
{
    public partial class frm_tprc_MoveMenuCollection : Form
    {

        bool blOpen = false;

        public frm_tprc_MoveMenuCollection()
        {
            InitializeComponent();
        }

        private void frm_tprc_MoveMenuCollection_Load(object sender, EventArgs e)
        {

        }

        private void btnControl_Click(object sender, EventArgs e)
        {
            int i = 0;
            Form form = null;//폼 초기화

            try
            {
                Button btn = sender as Button;
                string x = btn.Name.Substring(btn.Name.Length - 1, 1);
                int.TryParse(x, out i);

                switch (i)
                {
                    case 1:     //원자재입고
                        frm_mtr_OCStuffin_U child1 = new frm_mtr_OCStuffin_U();
                        form = child1;
                        break;
                    case 2:     //제품출고
                        frm_tprc_OutWareScan_U child2 = new frm_tprc_OutWareScan_U();
                        form = child2;
                        break;
                    case 3:     //금형입고
                        frm_mtr_RemainMove_Q child3 = new frm_mtr_RemainMove_Q();
                        form = child3;
                        break;
                    case 4:     //잔량이동처리
                        frm_mtr_RemainQtyMoveByLotID_U child4 = new frm_mtr_RemainQtyMoveByLotID_U();
                        form = child4;
                        break;
                }
                if (form != null)
                {
                    foreach (Form openForm in Application.OpenForms)//중복실행방지
                    {
                        if (openForm.Name == form.Name)
                        {
                            blOpen = true;
                            openForm.BringToFront();
                            openForm.Activate();
                            return;
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
            catch (Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", ex.Message), "[오류]", 0, 1);
            }
        }



        private void btnClose_Click(object sender, EventArgs e)
        {
            Frm_tprc_Main.g_tBase.ResablyID = "";
            this.Close();
        }


    }
}
