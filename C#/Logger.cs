using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TatuelAPI.Utilities
{
    [Obsolete]
    public class Logger
    {

       const int LEVEL_DEBUG = 0;
       const int LEVEL_INFO = 1;
       const int LEVEL_ERROR = 2;

       private static Logger instance = new Logger();
       private NLog.Logger nlogger;

       private Logger()
       {
           nlogger = NLog.LogManager.GetCurrentClassLogger();
       }

       public static Logger GetInstance() { return instance; }

       public void LogDebug(string message, Exception e)
       {
           message = "[DEBUG]" + message;
           if (e != null)
               message = message + " E:" + e + " ST:" + String.Join("/", e.StackTrace);
           System.Diagnostics.Debug.WriteLine(message);
           nlogger.Error(message);
       }

       /// <summary>
       /// [AS]
       /// </summary>
       /// <param name="e"></param>
       public void LogDebug(Exception e)
       {
           LogDebug("", e);
       }

       public void LogDebug(string message)
       {
           this.LogDebug(message, null);
       }

       public void LogInfo(string message, Exception e)
       {
           message = "[INFO]" + message;
           if (e != null)
               message = message + " E:" + e + " ST:" + String.Join("/", e.StackTrace);
           System.Diagnostics.Debug.WriteLine(message);
           nlogger.Error(message);
       }

       public void LogInfo(string message)
       {
           this.LogInfo(message, null);
       }

       /// <summary>
       /// [AS]
       /// </summary>
       /// <param name="e"></param>
       public void LogError(Exception e)
       {
           LogError("", e);
       }

       public void LogError(string message, Exception e)
       {
           message = "[ERROR]" + message;
           if (e != null)
               message = message + " E:" + e + " ST:" + String.Join("/", e.StackTrace);
           System.Diagnostics.Debug.WriteLine(message);
           nlogger.Error(message);
       }

       public void LogError(string message)
       {
           this.LogError(message, null);
       }
    }
}
