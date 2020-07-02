using Spire.Xls;
using System.Drawing;
using System.Windows;

namespace DataProtector.Classes
{
    public class ExcelManage
    {
        public static string ManageXLS(string src, string directory, string filename,string pass,Bitmap watermark)
        {
            Workbook workbook = new Workbook();
            workbook.LoadFromFile(src);
            for (int i = 0; i < workbook.Worksheets.Count; i++)
            {
                Worksheet sheet = workbook.Worksheets[i];
                sheet.PageSetup.BackgoundImage = watermark;
                sheet.PageSetup.PrintArea = "BB12:BB13";
                sheet.Protect(pass,SheetProtectionType.None);
                
            }
            string path = directory + @"\" + filename + " PROTECTED.xlsx";
            try { 
            workbook.SaveToFile(path, FileFormat.Version2010);
            }
            catch
            {
                MessageBox.Show("Файл уже существует!", "Ошибка");
            }
            MessageBox.Show("Документ сохранён!", "Успешно");
            return path;
        }
    }
}
