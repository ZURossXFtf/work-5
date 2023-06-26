using System;


class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Введите комплексное число a в формате (a+bi):");
        Complex a = ParseComplex(Console.ReadLine());

        Console.WriteLine("Введите комплексное число b в формате (a+bi):");
        Complex b = ParseComplex(Console.ReadLine());

        Console.WriteLine($"a = {a}");
        Console.WriteLine($"b = {b}");

        Complex sum = a.Add(b);
        Console.WriteLine($"Сумма: {sum}");

        Complex difference = a.Subtract(b);
        Console.WriteLine($"Разность: {difference}");

        Complex product = a.Multiply(b);
        Console.WriteLine($"Произведение: {product}");

        Complex quotient = a.Divide(b);
        Console.WriteLine($"Частное: {quotient}");
    }

    static Complex ParseComplex(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            throw new ArgumentException("Строка не может быть пустой или содержать только пробелы");
        }

        // Удаляем пробелы и скобки
        input = input.Replace(" ", "").Replace("(", "").Replace(")", "");

        // Разделяем вещественную и мнимую части
        string[] parts = input.Split('+', '-');
        if (parts.Length != 2)
        {
            throw new FormatException("Строка должна содержать ровно один знак плюс или минус");
        }

        double real = double.Parse(parts[0]);
        double imaginary = double.Parse(parts[1].Replace("i", ""));
        if (input.Contains("-"))
        {
            imaginary = -imaginary;
        }

        return new Complex(real, imaginary);
    }
}