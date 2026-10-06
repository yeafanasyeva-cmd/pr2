using System;
using System.IO;

namespace пр2
{
    internal static class Logger
    {
        private static readonly object _lock = new object();
        private static readonly string LogDirectory;
        private static readonly string LogFilePath;

        static Logger()
        {
            string appData = Environment.GetFolderPath(
                Environment.SpecialFolder.ApplicationData);

            LogDirectory = Path.Combine(appData, "lab2");
            LogFilePath = Path.Combine(LogDirectory, "log.txt");

            try
            {
                Directory.CreateDirectory(LogDirectory);
            }
            catch
            {
                //aaaaa
            }
        }

        public static string FilePath => LogFilePath;

        public static void Log(string message)
        {
            Write($"[INFO] {message}");
        }

        public static void LogError(string message)
        {
            Write($"[ERROR] {message}");
        }

        public static void LogError(Exception ex, string context = null)
        {
            string text = context == null
                ? $"[ERROR] {ex.GetType().Name}: {ex.Message}"
                : $"[ERROR] {context} -> {ex.GetType().Name}: {ex.Message}";
            Write(text);
        }

        private static void Write(string text)
        {
            string line = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} {text}";

            lock (_lock)
            {
                try
                {
                    File.AppendAllText(LogFilePath, line + Environment.NewLine);
                }
                catch
                {
                    //aaaaaa
                }
            }
        }
    }
}