using MaterialDesignThemes.Wpf;

namespace ReportCompiler.WPF.Models
{
    internal class DirectoryItem
    {
        public string? Name { get; set; }
        public bool IsDirectory { get; set; }
        public PackIconKind PackIconKind => IsDirectory ? PackIconKind.Folder : PackIconKind.MicrosoftExcel;
        
    }
}
