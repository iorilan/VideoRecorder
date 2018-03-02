using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoRecorder
{
    public static class Config
    {
        public static string RtspURL
        {
            get
            {
                var rtsp = ConfigurationManager.AppSettings["rtsp"];
                if (string.IsNullOrEmpty(rtsp))
                {
                    throw new Exception("[Configuration] rtsp path is missing");
                }

                return rtsp;
            }
        }
        public static string FFmpegPath
        {
            get
            {
                var ffmpeg = ConfigurationManager.AppSettings["ffmpegPath"];
                if (string.IsNullOrEmpty(ffmpeg))
                {
                    throw new Exception("[Configuration] ffmpeg path is missing");
                }

                return ffmpeg;
            }
        }

        public static string VideoOutputPath
        {
            get
            {
                var outputPath = ConfigurationManager.AppSettings["videoOutputPath"];
                if (string.IsNullOrEmpty(outputPath))
                {
                    throw new Exception("[Configuration] videoOutputPath is missing");
                }

                return outputPath;
            }
        }

        public static string VideoTimeInSeconds
        {
            get
            {
                var videoTime = ConfigurationManager.AppSettings["videoTimeInSecond"];
                if (string.IsNullOrEmpty(videoTime))
                {
                    throw new Exception("[Configuration] videoTimeInSecond is missing");
                }

                return videoTime;
            }
        }

        public static string NoOfFiles
        {
            get
            {
                var noOfFiles = ConfigurationManager.AppSettings["NoOfFiles"];
                if (string.IsNullOrEmpty(noOfFiles))
                {
                    throw new Exception("[Configuration] noOfFiles is missing");
                }

                return noOfFiles;
                
            }
        }

        public static string SnapshotPath
        {
            get
            {
                var snapshotPath = ConfigurationManager.AppSettings["snapshotPath"];
                if (string.IsNullOrEmpty(snapshotPath))
                {
                    throw new Exception("[Configuration] snapshotPath is missing");
                }

                return snapshotPath;
            }
        }
    }
}
