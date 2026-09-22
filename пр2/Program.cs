using пр2;

static void Main(string[] args)
{
    string filePath = null;
    string operation = null;

    for (int i = 0; i < args.Length; i++)
    {
        if (args[i] == "-f" || args[i] == "--file")
        {
            if (i + 1 < args.Length)
            {
                filePath = args[i + 1];
                i++;
            }
        }
        else if (args[i] == "-o" || args[i] == "--oper")
        {
            if (i + 1 < args.Length)
            {
                operation = args[i + 1];
                i++;
            }
        }
    }

    if (filePath == null || operation == null)
    {
        Console.WriteLine("Использование: program -f <файл> -o <операция>");
        Console.WriteLine("Операции: print, count");
        return;
    }

    if (!File.Exists(filePath))
    {
        Console.WriteLine($"Файл не найден: {filePath}");
        return;
    }

    List<Shape> shapes;
    try
    {
        shapes = FileReader.ReadShapes(filePath);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Ошибка при чтении файла: {ex.Message}");
        return;
    }

    switch (operation.ToLower())
    {
        case "print":
            foreach (Shape shape in shapes)
            {
                Console.WriteLine(shape);
            }
            break;

        case "count":
            Console.WriteLine(shapes.Count);
            break;

        default:
            Console.WriteLine($"Неизвестная операция: {operation}");
            break;
    }
}