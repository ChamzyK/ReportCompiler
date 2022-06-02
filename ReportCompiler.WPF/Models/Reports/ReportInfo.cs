namespace ReportCompiler.WPF.Models.Reports
{
    internal class ReportInfo
    {
        public bool IsCorrect { get; set; }
        public string Path { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }

        public ReportInfo(string name, string path, bool isCorrect, string comment = "")
        {
            Name = name;
            Path = path;
            IsCorrect = isCorrect;
            Comment = comment;
        }
    }
}
