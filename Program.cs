using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using log4net;

namespace VideoRecorder
{
    class Program
    {
        private static ILog log = LogManager.GetLogger(typeof(Program));

        static void Main(string[] args)
        {
            try
            {
                VideoFileWorker.Do.RunAsync();
                VideoRecordWorker.Do.RunAsync();
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }
        

        
        
        
    }
}
