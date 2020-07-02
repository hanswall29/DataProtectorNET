using System;
using System.IO;

namespace DataProtector.Classes
{
    public class Logging
    {
        public static string CreateLog()
        {
            string newlogfile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + $@"\DataProtector\Logs\Log{DateTime.Today.Date:dd/M/yyyy}.txt");
            if (File.Exists(newlogfile) == false)
                File.Create(newlogfile);
            return newlogfile;
        }

        public static void WriteLog(string newlogfile,string type, DateTime dateTime, string sourcefilestring, string filestring, string watermarkfilestring)
        {
            string LogText = $"{dateTime}: {type}, {sourcefilestring}, {filestring}/ Watermark: {watermarkfilestring}";
            File.AppendAllText(newlogfile, LogText + Environment.NewLine);
        }
    }
}
