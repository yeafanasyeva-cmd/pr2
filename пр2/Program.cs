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
                            Console.Error.WriteLine("Не указан путь к файлу после -f/--file");
                            return 1;
                        }
                        filePath = args[++i];
                        break;

                    case "-o":
                    case "--oper":
                        if (i + 1 >= args.Length)
                        {
                            Console.Error.WriteLine("Не указана операция после -o/--oper");
                            return 1;
                        }
                        operation = args[++i];
                        break;

                    default:
                        Console.Error.WriteLine($"Неизвестный аргумент: {args[i]}");
                        return 1;
                }
            }

            if (filePath == null || operation == null)
            {
                Console.WriteLine("Использование: program -f <файл> -o <операция>");
                Console.WriteLine("Операции: print, count");
                return 1;
            }

            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"Файл не найден: {filePath}");
                return 1;
            }

            List<Shape> shapes;
            try
            {
                shapes = FileReader.ReadShapes(filePath);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Ошибка при чтении файла: {ex.Message}");
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
                    Console.Error.WriteLine($"Неизвестная операция: {operation}");
                    return 1;
            }

            return 0;
        }
    }
}