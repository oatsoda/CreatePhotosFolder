namespace CreatePhotosFolder
{
    public class ParentFolder
    {
        public string Name { get; }
        public string Path { get; }

        public ParentFolder(string name, string path)
        {
            Name = name;
            Path = path;
        }
    }
}
