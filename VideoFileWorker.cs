using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using log4net;

namespace VideoRecorder
{
    public class VideoFileWorker
    {
        private const string FileSuffix = "seconds_.mp4";
        private ILog log = LogManager.GetLogger(typeof(VideoFileWorker));

        public static VideoFileWorker Do { get { return new VideoFileWorker();} }

        public void RunAsync()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    Thread.Sleep(1000);
                    try
                    {
                        MainatainFilesCount(Config.VideoOutputPath);
                    }
                    catch (Exception ex)
                    {
                        log.Error(ex);
                    }
                }
            });

        }

        private void MainatainFilesCount(string videopath)
        {
            var maxFiles = int.Parse(Config.NoOfFiles);
            var files = Directory.GetFiles(videopath).Where(x => x.Contains(FileSuffix)).ToList();
            if (files.Count > maxFiles)
            {
                var first = files.First();
                File.Delete(first);
            }
        }
    }
}
