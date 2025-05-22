namespace MiniPay_Backend
{
    public static class FileStorage
    {        
        public static void WriteFile(string path, string fileContent)
        {
            File.WriteAllText(path, fileContent);
        }

        public static string ReadFile(string path)
        {
            string jsonContent = string.Empty;

            if (!File.Exists(path))
            {
                File.WriteAllText(path, string.Empty);
            }
            else
            {
                jsonContent = File.ReadAllText(path);
            }

            return jsonContent;
        }
    }
}
