namespace CreatePhotosFolder.App.Settings
{
    public class RootFolder
    {
        public string Name { get; }
        public string Path { get; }

        public RootFolder(string name, string path)
        {
            Name = name;
            Path = path;
        }
    }
}
