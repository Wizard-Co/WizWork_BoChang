using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WizControl
{
    public partial class uc_Calendar : UserControl
    {
        string paramDate = "";
        string paramName = "";
        IFormatProvider culture = new System.Globalization.CultureInfo("ko-KR", true);
        public delegate void TextEventHandler(string strDate, string btnName);// string을 반환값으로 갖는 대리자를 선언합니다.
        public event TextEventHandler WriteDateTextEvent;          // 대리자 타입의 이벤트 처리기를 설정합니다.

        public uc_Calendar()
        {
            InitializeComponent();
        }

        public uc_Calendar(string strDate, string strName)
        {
            InitializeComponent();
            this.paramDate = strDate;
            this.paramName = strName;
        }
        private void btnPrev_Click(object sender, EventArgs e)
        {
            DateTime dt = Convert.ToDateTime(excelCalendar.Value.ToString());
            excelCalendar.Value = dt.AddMonths(-1).ToString();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            DateTime dt = Convert.ToDateTime(excelCalendar.Value.ToString());
            excelCalendar.Value = dt.AddMonths(+1).ToString();
        }
        private void uc_Calendar_Load(object sender, EventArgs e)
        {
            if (paramDate != "" && paramDate.Length == 8)
            {
                DateTime dt = DateTime.ParseExact(paramDate, "yyyyMMdd", culture);
                excelCalendar.Value = dt;
            }
            else if (paramDate != "" && paramDate.Length == 6)
            {
                DateTime dt = DateTime.ParseExact(paramDate, "yyMMdd", culture);
                excelCalendar.Value = dt;
            }

        }

        private void excelCalendar_ClickEvent(object sender, EventArgs e)
        {
            DateTime dt = Convert.ToDateTime(excelCalendar.Value.ToString(), culture);
            string date = dt.ToString("yyyyMMdd");// + DateTime.Now.ToString("HHmmss");
            WriteDateTextEvent(date, paramName);
            this.Dispose();
        }

    }
}
