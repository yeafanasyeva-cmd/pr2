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
            if (line.StartsWith("Point("))
            {
                var coords = ExtractNumbers(line);
                if (coords.Count == 2)
                    return new Point(coords[0], coords[1]);
            }
            else if (line.StartsWith("Line("))
            {
                int firstOpen = line.IndexOf('(');
                int firstClose = FindMatchingParen(line, firstOpen);
                int secondOpen = line.IndexOf('(', firstClose);
                int secondClose = FindMatchingParen(line, secondOpen);

                string point1Str = line.Substring(firstOpen, firstClose - firstOpen + 1);
                string point2Str = line.Substring(secondOpen, secondClose - secondOpen + 1);

                var coords1 = ExtractNumbers(point1Str);
                var coords2 = ExtractNumbers(point2Str);

                if (coords1.Count == 2 && coords2.Count == 2)
                {
                    Point p1 = new Point(coords1[0], coords1[1]);
                    Point p2 = new Point(coords2[0], coords2[1]);
                    return new Line(p1, p2);
                }
            }
            else if (line.StartsWith("Circle("))
            {
                int open = line.IndexOf('(');
                int close = FindMatchingParen(line, open);
                string pointStr = line.Substring(open, close - open + 1);

                var pointCoords = ExtractNumbers(pointStr);

                int afterPoint = close + 1;
                string rest = line.Substring(afterPoint).TrimStart(',', ' ', ')');
                rest = rest.TrimEnd(')');
                var allNumbers = ExtractNumbers(rest);

                if (pointCoords.Count == 2 && allNumbers.Count >= 1)
                {
                    Point center = new Point(pointCoords[0], pointCoords[1]);
                    return new Circle(center, allNumbers[0]);
                }
            }

            return null;
        }

        private static int FindMatchingParen(string s, int openIndex)
        {
            int depth = 0;
            for (int i = openIndex; i < s.Length; i++)
            {
                if (s[i] == '(') depth++;
                else if (s[i] == ')')
                {
                    depth--;
                    if (depth == 0) return i;
                }
            }
            return -1;
        }

        private static List<double> ExtractNumbers(string s)
        {
            List<double> numbers = new List<double>();
            string current = "";
            foreach (char c in s)
            {
                if (char.IsDigit(c) || c == '.' || c == '-' || c == '+' || c == ',')
                {
                    if (c == ',' && !current.Contains('.'))
                    {
                        current += '.';
                    }
                    else if (c == ',')
                    {
                        if (!string.IsNullOrEmpty(current))
                        {
                            double val;
                            if (double.TryParse(current, NumberStyles.Any, CultureInfo.InvariantCulture, out val))
                                numbers.Add(val);
                            current = "";
                        }
                    }
                    else
                    {
                        current += c;
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(current))
                    {
                        double val;
                        if (double.TryParse(current, NumberStyles.Any, CultureInfo.InvariantCulture, out val))
                            numbers.Add(val);
                        current = "";
                    }
                }
            }
            if (!string.IsNullOrEmpty(current))
            {
                double val;
                if (double.TryParse(current, NumberStyles.Any, CultureInfo.InvariantCulture, out val))
                    numbers.Add(val);
            }
            return numbers;
        }
    }
}
