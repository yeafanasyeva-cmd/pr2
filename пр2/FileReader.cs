using System;
using System.Collections.Generic;
using System.IO;

namespace пр2
{
    internal static class FileReader
    {
        public static List<Shape> ReadShapes(string filePath)
        {
            var shapes = new List<Shape>();
            int lineNumber = 0;

            foreach (string line in File.ReadLines(filePath))
            {
                lineNumber++;

                string trimmed = line.Trim();
                if (trimmed.Length == 0)
                    continue;

                Shape shape = ShapeParser.Parse(trimmed);

                if (shape != null)
                {
                    shapes.Add(shape);
                }
                else
                {
                    Logger.LogError(
                        $"Некорректная строка {lineNumber} в файле \"{filePath}\": {trimmed}");
                }
            }

            return shapes;
        }
    }
}