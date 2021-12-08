using System.IO;
using System;
namespace powerplant_coding_challenge.Logging
{
    public class Error : LogBase
    {
        public string CurrentDirectory { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public Error()
        {
            this.CurrentDirectory = Directory.GetCurrentDirectory();
            this.FileName = "ErrorLog.txt";
            this.FilePath = this.CurrentDirectory + "/" + this.FileName;
        }
        public override void Log(string message)
        {
            using (System.IO.StreamWriter w = System.IO.File.AppendText(this.FilePath))
            {
                w.Write("\r\nLog Entry : ");
                w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
                w.WriteLine("  {0}", message);
                w.WriteLine("-----------------------------------------------");
            }
        }
    }
}