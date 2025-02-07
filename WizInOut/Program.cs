using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using WizCommon;

namespace WizInOut
{
    static class Program
    {
        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // 컴파일 배포시 단이 exe 로 배포하고 , 참조하는 dll은 리소스 파일에 추가하여 (참조리소스)
            // 단일 실행파일로 만들어 배포함.
            // 프로그램은 실행시 참조 dll 파일을 생성하고 리소스 파일에 있는 원 파일을 읽어서 대체함.

            //뮤텍스명 지정
            string mtxName = "WizInOut";
            Mutex mtx = new Mutex(true, mtxName);
            //1초 동안 뮤텍스를 획득하려 대기
            TimeSpan tsWait = new TimeSpan(0, 0, 1);
            bool success = mtx.WaitOne(tsWait);
            //실패하면 프로그램 종료
            if (!success)
            {
                return;
            }

            ApplicationStart();

        }

        private static void ApplicationStart()
        {
            try
            {
                string POPServerIPAddress = Frm_tinout_Main.gs.GetValue("SQLServer", "server", "wizis.iptime.org,20220");
                string DBCatalog_MES = ";Initial Catalog= " + Frm_tinout_Main.gs.GetValue("SQLServer", "Database", "WizMRP") + ";UID=";

                DataStore.Instance.SetConnectionString(POPServerIPAddress, ConnectionInfo.POPServerLoginID, ConnectionInfo.POPServerPassword, DBCatalog_MES);
                DataStore.Log_Instance.L_SetConnectionString(POPServerIPAddress, ConnectionInfo.POPServerLoginID, ConnectionInfo.POPServerPassword, ConnectionInfo.DBCatalog_LOG);

                Application.EnableVisualStyles();

                Application.SetCompatibleTextRenderingDefault(false);

                Application.Run(new Frm_tinout_Main());
            }
            catch (Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", ex.Message), "[오류]", 0, 1);
            }
        }
    }
}
