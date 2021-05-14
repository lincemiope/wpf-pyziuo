using System;
using System.IO;

namespace PyziUO.Tools
{
    public static class Logger
    {
        public enum LogType
        {
            Info,
            Warning,
            Exception
        }
        public enum LogSector
        {
            Internal,
            Script
        }
        public static void Log(string func, string text, LogSector sector, LogType type = LogType.Info)
        {
            var logpath = Path.Combine(App.AssemblyDirectory, $"{DateTime.Now.ToString("yyyyMMdd")}{sector}.txt");
            if (!File.Exists(logpath))
            {
                try
                {
                    File.Create(logpath).Close();
                } catch (Exception) { }
            }
            AddText(logpath, $"[{DateTime.Now.ToString("HH:mm:ss")}] [{type.ToString()}] [{func}] {text}");
        }
        private static void AddText(string path, string text)
        {
            try
            {
                File.AppendAllText(path, text);
            } catch (Exception) { }
        }
    }
}
