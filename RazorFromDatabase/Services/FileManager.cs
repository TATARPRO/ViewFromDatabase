namespace RazorFromDatabase.Services
{
    public class FileManager
    {
        public FileManager()
        {
        }

        public static string GetUniqueFileName(string existingName)
        {
            //Regex fileNameExpression = new Regex("*(+d/)");
            //if(fileNameExpression.IsMatch(existingName))
            //{

            //return string.Empty;
            //}
            //else
            //{
            //    return existingName;
            //}
            return existingName;
        }
    }
}
