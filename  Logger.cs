using System;
using System.IO;

public class Logger
{
    private static readonly string filePath = "log.txt";
    private static readonly object lockObject = new object();

    public static void Log(string message, string level = "INFO")
    {
        lock (lockObject)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    string logLine = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{level}] {message}";
                    writer.WriteLine(logLine);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка записи в лог: {ex.Message}");
            }
        }
    }
}
