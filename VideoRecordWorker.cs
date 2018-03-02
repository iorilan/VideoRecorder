using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using log4net;

namespace VideoRecorder
{
    public class VideoRecordWorker
    {
        public static VideoRecordWorker Do { get { return new VideoRecordWorker();} }
        private ILog log = LogManager.GetLogger(typeof(VideoRecordWorker));


        private DateTime LastScheduledAt = DateTime.Now;

        private string FileName(int seconds)
        {
            return string.Format(DateTime.Now.ToString("yyyyMMddHHmmss") + "_{0}seconds_.mp4", seconds);
        }
        
        

        public void RunAsync()
        {
            int totalSeconds = int.Parse(Config.VideoTimeInSeconds);

            while (true)
            {
                try
                {
                    if (DateTime.Now > LastScheduledAt.AddSeconds(totalSeconds))
                    {
                        Task.Run(() =>
                        {
                            LastScheduledAt = DateTime.Now;
                            RunFFmpegProcess(Config.VideoOutputPath, totalSeconds, Config.FFmpegPath);
                        });
                        Thread.Sleep(1000);
                    }
                }
                catch (Exception ex)
                {
                    log.Error(ex);
                }
            }
        }

        private void RunFFmpegProcess(string outputPath, int totalSeconds, string ffmpeg)
        {
            try
            {
                var fileName = Path.Combine(outputPath, FileName(totalSeconds));

                var rtsp = ConfigurationManager.AppSettings["rtsp"];
                if (string.IsNullOrEmpty(rtsp))
                {
                    throw new Exception("[Configuration] rtsp is missing");
                }

                string arguments = string.Format("-i \"{0}\" -ss {1} -t {1} -r 24 {2}", rtsp, totalSeconds,
                    fileName);
                var process = new Process
                {
                    StartInfo =
                    {
                        Arguments = arguments,
                        FileName = ffmpeg,
                        WindowStyle = ProcessWindowStyle.Hidden
                    }
                };

                process.Exited += (sender, eventArgs) => { System("ffmpeg Existed."); };
                process.ErrorDataReceived += (sender, eventArgs) => { System("ffmpeg Error"); };

                process.Start();
                process.WaitForExit();


                //var p = Process.Start(ffmpeg, arguments);
                System(string.Format("video recording done...file name : {0}", fileName));

            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        public void System(string message)
        {
            Console.WriteLine("[System] :" + message);
        }
    }
}
