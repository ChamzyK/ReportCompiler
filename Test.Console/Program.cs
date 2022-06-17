using Spire.Xls;

public static class Program
{
    public static void Main(string[] args)
    {
        var workbook = new Workbook();
        workbook.LoadFromFile("старый.xls");
        workbook.SaveToFile("новый.xlsx", ExcelVersion.Version2013);
    }
}
