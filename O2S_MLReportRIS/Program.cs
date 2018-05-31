using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace O2S_MLReportRIS
{
    static class Program
    {
        private static readonly ILog logFile = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            O2S_Common.Logging.LogSystem.Info("Application_Start. Time=" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff"));
            Application.Run(new frmMain());
        }
    }
}
