using System;
using System.Collections.Generic;
using System.IO;
using пр2;

namespace пр2
{
    internal static class Program
    {
        private static int Main(string[] args)
        {
            string filePath = null;
            string operation = null;

            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i])
                {
                    case "-f":
                    case "--file":
                        if (i + 1 >= args.Length)
                        {
                            const string msg = "Не указан путь к файлу после -f/--file";
                            Console.Error.WriteLine(msg);
                            Logger.LogError(msg);
                            return 1;
                        }
                        filePath = args[++i];
                        break;

                    case "-o":
                    case "--oper":
                        if (i + 1 >= args.Length)
                        {
                            const string msg = "Не указана операция после -o/--oper";
                            Console.Error.WriteLine(msg);
                            Logger.LogError(msg);
                            return 1;
                        }
                        operation = args[++i];
                        break;

                    default:
                        string unknown = $"Неизвестный аргумент: {args[i]}";
                        Console.Error.WriteLine(unknown);
                        Logger.LogError(unknown);
                        return 1;
                }
            }

            if (filePath == null || operation == null)
            {
                string msg = "Не указаны обязательные аргументы (-f и -o)";
                Console.WriteLine("Использование: program -f <файл> -o <операция>");
                Console.WriteLine("Операции: print, count");
                Logger.LogError(msg);
                return 1;
            }

            if (!File.Exists(filePath))
            {
                string msg = $"Файл не найден: {filePath}";
                Console.Error.WriteLine(msg);
                Logger.LogError(msg);
                return 1;
            }

            List<Shape> shapes;
            try
            {
                shapes = FileReader.ReadShapes(filePath);
            }
            catch (Exception ex)
            {
                string msg = $"Ошибка при чтении файла: {ex.Message}";
                Console.Error.WriteLine(msg);
                Logger.LogError(ex, "Чтение файла");
                return 1;
            }

            switch (operation.ToLowerInvariant())
            {
                case "print":
                    foreach (Shape shape in shapes)
                        Console.WriteLine(shape);
                    break;

                case "count":
                    Console.WriteLine(shapes.Count);
                    break;

                default:
                    string msg = $"Неизвестная операция: {operation}";
                    Console.Error.WriteLine(msg);
                    Logger.LogError(msg);
                    return 1;
            }

            Logger.Log($"Обработка завершена. Операция: {operation}, фигур: {shapes.Count}");
            return 0;
        }
    }
}