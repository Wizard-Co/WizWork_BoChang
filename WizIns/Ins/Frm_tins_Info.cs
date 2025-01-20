using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Resources;
using System.Reflection;
using System.Data.SqlClient;
using WizCommon;

namespace WizIns
{
    public partial class Frm_tins_Info : Form
    {
        Frm_tins_Main Ftm = new Frm_tins_Main();

        public Frm_tins_Info()
        {
           InitializeComponent();
        }

        public Frm_tins_Info(Form form)
        {
            InitializeComponent();
            MdiParent = form;
        }

        private void Frm_tlpTest_Info_Load(object sender, EventArgs e)
        {
            Ftm.LogSave(this.GetType().Name, "S");
            SetScreen();
            NoticeSet();
        }
        //TableLayoutPanel 세팅
        private void SetScreen()
        {
            //WizardLogo이미지 Common 프로젝트에서 가져오기
            Assembly _assembly;
            Stream _imageStream;

            _assembly = Assembly.Load("WizCommon");
            _imageStream = _assembly.GetManifestResourceStream("WizCommon.Resources.WizardLogo.png");

            var img = Bitmap.FromStream(_imageStream);
            //PictureBox컨트롤 셋팅
            PictureBox pbLogo = new PictureBox();
            pbLogo.Dock = DockStyle.Fill;
            pbLogo.SizeMode = PictureBoxSizeMode.StretchImage;
            pbLogo.Image = img;
            //패널 배치 및 조정
            tlp_Info.Controls.Add(pbLogo, 0, 3);
            tlp_Info.SetColumnSpan(lblName, 6);
            tlp_Info.SetColumnSpan(p_lbl_Notice, 6);
            tlp_Info.SetColumnSpan(p_txt_Notice, 6);
            tlp_Info.SetRowSpan(pbLogo, 3);   
        }

        //공지사항 값 불러오기, 당일 날짜
        private void NoticeSet()
        {
            p_txt_Notice.Text = "오늘은 " + string.Format(@"{0:yyyy년 MM월 dd일}", DateTime.Now) + " 입니다 \r\n\r\n";
            SqlParameter[] param = {
                                       new SqlParameter("SCompanyID",""),
                                       new SqlParameter("SDATE", DateTime.Now.ToString("yyyyMMdd")),
                                       new SqlParameter("EDATE", DateTime.Now.ToString("yyyyMMdd"))
                                    };
            DataSet ds = DataStore.Instance.ExecuteDataSet("xp_Info_sInfoByDate", param, false);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                p_txt_Notice.Text = p_txt_Notice.Text + dr["Info"].ToString() + "\r\n\r\n";
            }
            lblComTel.Select();
            lblComTel.Focus();
            Ftm.LogSave(this.GetType().Name, "R"); //2022-06-23 R

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MdiParent == null)
            {
                MessageBox.Show("null");
            }
            else
            {
                MessageBox.Show("no null");
            }
        }

        private void Frm_Info_Activated(object sender, EventArgs e)
        {
            ((Frm_tins_Main)(MdiParent)).LoadRegistry();
        }
    }
}
