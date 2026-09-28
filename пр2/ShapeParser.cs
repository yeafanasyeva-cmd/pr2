using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace пр2
{
    internal class ShapeParser
    {
        public static Shape Parse(string line)
        {
            if (string.IsNullOrWhiteSpace(line))
                return null;

            line = line.Trim();

            try
            {
                if (line.StartsWith("Point("))
                    return ParsePoint(line);
                if (line.StartsWith("Line("))
                    return ParseLine(line);
                if (line.StartsWith("Circle("))
                    return ParseCircle(line);
            }
            catch
            {
                return null;
            }

            return null;
        }

        private static Point ParsePoint(string line)
        {
            string inner = ExtractInner(line, "Point");
            var parts = SplitTopLevel(inner);
            if (parts.Count != 2)
                throw new FormatException();

            double x = ParseDouble(parts[0]);
            double y = ParseDouble(parts[1]);
            return new Point(x, y);
        }

        private static Line ParseLine(string line)
        {
            string inner = ExtractInner(line, "Line");
            var parts = SplitTopLevel(inner);
            if (parts.Count != 2)
                throw new FormatException();

            var start = ParsePoint(parts[0].Trim());
            var end = ParsePoint(parts[1].Trim());
            return new Line(start, end);
        }

        private static Circle ParseCircle(string line)
        {
            string inner = ExtractInner(line, "Circle");
            var parts = SplitTopLevel(inner);
            if (parts.Count != 2)
                throw new FormatException();

            var center = ParsePoint(parts[0].Trim());
            double radius = ParseDouble(parts[1]);
            return new Circle(center, radius);
        }

        private static string ExtractInner(string line, string name)
        {
            int open = line.IndexOf('(');
            int close = line.LastIndexOf(')');
            if (open < 0 || close < 0 || close <= open)
                throw new FormatException();

            string prefix = line.Substring(0, open).Trim();
            if (prefix != name)
                throw new FormatException();

            if (line.Substring(close + 1).Trim().Length != 0)
                throw new FormatException();

            return line.Substring(open + 1, close - open - 1);
        }

        private static List<string> SplitTopLevel(string input)
        {
            var result = new List<string>();
            int depth = 0;
            int start = 0;

            for (int i = 0; i < input.Length; i++)
            {
                char c = input[i];
                if (c == '(') depth++;
                else if (c == ')') depth--;
                else if (c == ',' && depth == 0)
                {
                    result.Add(input.Substring(start, i - start));
                    start = i + 1;
                }
            }

            result.Add(input.Substring(start));
            return result;
        }

        private static double ParseDouble(string s)
        {
            return double.Parse(
                s.Trim(),
                NumberStyles.Float,
                CultureInfo.InvariantCulture);
        }
    }
}
