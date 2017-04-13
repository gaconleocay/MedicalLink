using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.UserSkins;
using DevExpress.Skins;
using DevExpress.LookAndFeel;
using log4net;
using MedicalLink.FormCommon;
using System.Threading;

namespace MedicalLink
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
            AppDomain.CurrentDomain.AppendPrivatePath(AppDomain.CurrentDomain.BaseDirectory + @"\Library");
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("vi");
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("vi");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            BonusSkins.Register();
            SkinManager.EnableFormSkins();
            UserLookAndFeel.Default.SetSkinStyle("DevExpress Style");
            MedicalLink.Base.Logging.Info("Application_Start. Time=" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff"));
            Application.Run(new frmLogin());
            //Application.Run(new MedicalLink.Utilities.PrintPreview.PrintPreviewDev());
        }
    }
}
