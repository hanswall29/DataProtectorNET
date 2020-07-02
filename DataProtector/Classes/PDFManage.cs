using Spire.Pdf;
using System.Drawing;
using System.Windows;

namespace UImageProtector.Classes
{
    public class PDFManage
    {
        public static string ManagePDF(string src, string directory, string filename, string img, float opacity)
        {
            PdfDocument pdfDocument = new PdfDocument();
            pdfDocument.LoadFromFile(src);
            Image bg = Image.FromFile(img);
            for (int i = 0; i < pdfDocument.Pages.Count; i++)
            {
                PdfPageBase page = pdfDocument.Pages[i];
                page.BackgroundImage = bg;
                page.BackgroundRegion = new RectangleF(200, 200, bg.Width, bg.Height);
                page.BackgroudOpacity = opacity;
            }
            string path = directory + @"\" + filename + " PROTECTED.pdf";
            try
            {
                pdfDocument.SaveToFile(path);
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
