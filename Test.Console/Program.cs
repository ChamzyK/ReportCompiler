using OfficeOpenXml;
using System.Text.RegularExpressions;
using Test.Console;

public static class Program
{
    public static void Main(string[] args)
    {
        // Допустим в исходной строке нужно найти все числа,
        // соответствующие стоимости продукта
        string input = "количество проведенных проверок соблюдения работниками государственного учреждения службы занятости населения административного регламента предоставления государственной услуги (текущий контроль). За  январь-45, февраль-60, март-0, апрель-0, май-0, июнь-0, июль-0, август -0, сентябрь-0, октябрь-0, ноябрь-0, декабрь-3563";

        string pattern = @"(\d*)";
        var regex = new Regex(pattern);

        // Достигаем того же результата что и в предыдущем примере, 
        // используя метод Regex.Matches() возвращающий MatchCollection
        foreach (Match match in regex.Matches(input.Replace(" ", "")))
        {
            if (int.TryParse(match.Groups[1].Value, out int temp))
                Console.WriteLine(temp);
        }
    }

}
