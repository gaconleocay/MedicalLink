using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalLink.Base
{
    public class Logging
    {
        private static ILog logFile = LogManager.GetLogger(typeof(Logging));

       public static bool IsDebugEnabled()
        {
            return logFile.IsDebugEnabled;
        }

        public static bool IsInfoEnabled()
        {
            return logFile.IsInfoEnabled;
        }

        public static void Debug(string message)
        {
            Debug(message, null);
        }

        public static void Debug(Exception ex)
        {
            logFile.Debug(null, ex);
        }

        public static void Debug(string message, Exception ex)
        {
            logFile.Debug(message, ex);
        }

        public static void Info(string message)
        {
            logFile.Info(message);
        }

        public static void Warn(string message)
        {
            Warn(message, null);
        }

        public static void Warn(Exception ex)
        {
            Warn(null, ex);
        }

        public static void Warn(string message, Exception ex)
        {
            logFile.Warn(message, ex);
        }

        public static void Error(string message)
        {
            Error(message, null);
        }

        public static void Error(Exception ex)
        {
            Error(null, ex);
        }

        public static void Error(string message, Exception ex)
        {
            logFile.Error(message, ex);
        }

        public static void Fatal(string message)
        {
            Fatal(message, null);
        }

        public static void Fatal(Exception ex)
        {
            Fatal(null, ex);
        }

        public static void Fatal(string message, Exception ex)
        {
            logFile.Fatal(message, ex);
        }
    }
}
