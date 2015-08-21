using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WebTu.Logger
{
    public class FileLogger
    {
        public void LogException(Exception e)
        {
            var errinfo = new string[]{
                     "Message:" + e.Message,
                     "StackTrace:" + e.StackTrace
                 };

            File.WriteAllLines("E://Errors//" + DateTime.Now.ToString("yyyy-MM-dd HH mm ss") + ".txt",errinfo                 
                );
        }
    }
}