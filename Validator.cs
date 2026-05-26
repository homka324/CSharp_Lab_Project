using System;
using System.IO;

public class Logger
{
    private static readonly string filePath = "log.txt";
    private static readonly object lockObject = new object();

    // 1. Метод проверки строки перед логированием
    public static bool IsLogValid(string message, out string errorMessage)
    {
        errorMessage = string.Empty;

        if (string.IsNullOrWhiteSpace(message))
        {
            errorMessage = "Сообщение не может быть пустым.";
            return false;
        }

        if (message.Length > 1000) // Ограничение длины для защиты от переполнения
        {
            errorMessage = "Сообщение слишком длинное (максимум 1000 символов).";
            return false;
        }

        return true;
    }

    // 2. Метод записи лога с автоматической валидацией
    public static void Log(string message, string level = "INFO")
    {
        // Проверяем строку. Если она пустая — игнорируем запись
        if (!IsLogValid(message, out _))
        {
            return;
        }

        lock (lockObject)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    string logLine = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{level}] {message.Trim()}";
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
