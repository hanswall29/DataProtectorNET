using Spire.Doc;
using System.Drawing;
using System.Windows.Forms;

namespace UImageProtector.Classes
{
    public class WordManage
    {
        public static string ManageWordFile(string sourceFile, string directory, string filename, Bitmap watermarkImageFile)
        {
            Document doc = new Document();
            doc.LoadFromFile(sourceFile, FileFormat.Docx2013);

            PictureWatermark watermarkImage = new PictureWatermark
            {
                Picture = watermarkImageFile,
                IsWashout = false,
                Scaling = 100
            };
            doc.Watermark = watermarkImage;
            string path = directory + @"\" + filename + " PROTECTED.docx";
            try { 
            doc.SaveToFile(path, FileFormat.Docx2013);
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
