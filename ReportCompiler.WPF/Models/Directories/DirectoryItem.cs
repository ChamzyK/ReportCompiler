namespace ReportCompiler.WPF.Models
{
    internal class DirectoryItem
    {
        public string Name { get; }
        public string Path { get; }
        public DirectoryItemType Type { get; }

        public DirectoryItem(string name, string path, DirectoryItemType type)
        {
            Name = name;
            Path = path;
            Type = type;
        }
    }
}
