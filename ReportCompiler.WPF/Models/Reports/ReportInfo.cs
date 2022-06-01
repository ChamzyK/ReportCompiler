namespace ReportCompiler.WPF.Models.Reports
{
    internal class ReportInfo
    {
        public bool IsCorrect { get; set; }
        public string Path { get; set; }
        public string Comment { get; set; }

        public ReportInfo(string path, bool isCorrect, string comment = "")
        {
            Path = path;
            IsCorrect = isCorrect;
            Comment = comment;
        }
    }
}
