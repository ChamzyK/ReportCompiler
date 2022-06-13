namespace ReportCompiler.WPF.Models.Reports
{
    internal class ReportInfo
    {
        public bool IsCorrect { get; set; }
        public string? Path { get; set; }
        public string? Name { get; set; }
        public string? Comment { get; set; }
        public TemplateType? Template { get; set; }
        public (int, int) StartCellAddress { get; set; }
        public (int, int)? InspectionsAddress { get; set; }
    }
}
