namespace crudAuthApp.Helper
{
    public class AdpParser
    {
        public async Task Parse()
        {
            string fileName = @"C:\Users\HP\Downloads\MDUNP25P.A01";

            using (StreamReader streamReader = System.IO.File.OpenText(fileName))
            {
                string text = streamReader.ReadToEnd();
                string[] lines = text.Split(Environment.NewLine);

                foreach (string line in lines)
                {
                    Console.WriteLine(line);
                }
            }
        }
    }
}
