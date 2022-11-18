namespace JamesWebApp
{
    public class ErrorLog
    {
        //created a logger that writes to a txt file
        public void ErrorLogger(Exception Error)
        {
            var file = System.IO.File.AppendText("Error.txt");
            file.WriteLine("-----------------------------ERROR---------------------------------");
            file.WriteLine(Error.ToString());
            file.Close();
        }
    }
}
