namespace laba
{
    public class FileWorker
    {
        public List<string> FileContent { get; set; }

        public string FilePath { get; set; }

        public FileWorker(string filePath)
        {
            var charsToRemove = new string[] { "[", "]", ",", "-", "\r" };
            var fileString = File.ReadAllText(filePath);

            FileContent = Clear(charsToRemove, fileString).Split("\n").ToList();
            FilePath = filePath;
        }
        public string Clear(string[] charsToRemove, string str)
        {
            foreach (var c in charsToRemove)
            {
                str = str.Replace(c, string.Empty);
            }

            return str;
        }

    }
}
