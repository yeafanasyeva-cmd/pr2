using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace пр2
{
    internal class FileReader
    {
        public static List<Shape> ReadShapes(string filePath)
        {
            List<Shape> shapes = new List<Shape>();

            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                string trimmed = line.Trim();
                if (string.IsNullOrEmpty(trimmed))
                    continue;

                Shape shape = ShapeParser.Parse(trimmed);
                if (shape != null)
                    shapes.Add(shape);
            }

            return shapes;
        }
    }
}
