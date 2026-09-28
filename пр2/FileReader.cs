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
            var shapes = new List<Shape>();

            foreach (string line in File.ReadLines(filePath))
            {
                Shape shape = ShapeParser.Parse(line);
                if (shape != null)
                    shapes.Add(shape);
            }

            return shapes;
        }
    }
}
