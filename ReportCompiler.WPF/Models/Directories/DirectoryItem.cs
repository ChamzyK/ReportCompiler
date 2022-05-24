namespace ReportCompiler.WPF.Models
{
    internal class DirectoryItem
    {
        public string Name { get; init; }
        public string Path { get; init; }
        public DirectoryItemType Type { get; init; }

        public DirectoryItem(string name, string path, DirectoryItemType type)
        {
            Name = name;
            Path = path;
            Type = type;
        }
    }
}
