using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using WizCommon;

namespace WizInOut
{
    public partial class Frm_tinout_Info : Form
    {
        LogData LogData = new LogData(); //2025-01-13 log 남기는 함수

        public Frm_tinout_Info()
        {
            InitializeComponent();
        }

        private void Frm_tinout_Info_Load(object sender, EventArgs e)
        {
            SetScreen();
            NoticeSet();
            LogData.LogSave(this.GetType().Name, "S"); //2025-01-13 사용시간(로드, 닫기)
        }

        private void Frm_tinout_Info_Activated(object sender, EventArgs e)
        {
            ((Frm_tinout_Main)(MdiParent)).LoadRegistry();
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
            LogData.LogSave(this.GetType().Name, "R"); //log 남기기(조회 R) 2022-06-22
        }
    }
}
