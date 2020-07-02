using System;
using System.IO;
using System.Windows.Forms;

namespace UImageProtector.Classes
{
    public class GetFileName
    {
        public static (string, string) OpenSourceImageFile(int copyCheckBoxState)
        {
            OpenFileDialog of = new OpenFileDialog
            {
                Filter = "Изображения(*.BMP;*.JPG;*.PNG)|*.BMP;*.JPG;*.PNG",
                Title = "Выбор исходного изображения"
            };

            if (of.ShowDialog() == DialogResult.Cancel)
                return (null, null);
            string filestring = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Temp\" + of.SafeFileName.ToString();
            if (File.Exists(filestring) == true)
                File.Delete(filestring);
            File.Copy(of.FileName.ToString(), filestring);
            if (copyCheckBoxState == 1)
            {
                if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + @"\DataProtector\" + of.SafeFileName.ToString()) == true)
                    File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + @"\DataProtector\" + of.SafeFileName.ToString());
                File.Copy(of.FileName.ToString(), Environment.GetFolderPath(Environment.SpecialFolder.Personal) + @"\DataProtector\" + of.SafeFileName.ToString());
            }
            return (of.FileName.ToString(), Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Temp\" + of.SafeFileName.ToString());
        }

        public static string OpenWatermarkFile()
        {
            OpenFileDialog of = new OpenFileDialog
            {
                Filter = "Изображения(*.BMP;*.JPG;*.PNG)|*.BMP;*.JPG;*.PNG",
                Title = "Выбор водяного знака"
            };
            if (of.ShowDialog() == DialogResult.Cancel)
                return null;
            return of.FileName.ToString();
        }

        public static (string, string) OpenWordFile()
        {
            OpenFileDialog of = new OpenFileDialog
            {
                Filter = "Документы Word(*.docx)|*.docx",
                Title = "Выбор документа"
            };
            of.ShowDialog();
            return (of.FileName.ToString(), of.SafeFileName.ToString());
        }

        public static (string, string) OpenPdfFile()
        {
            OpenFileDialog of = new OpenFileDialog
            {
                Filter = "Документы Pdf(*.pdf)|*.pdf",
                Title = "Выбор документа"
            };
            of.ShowDialog();
            return (of.FileName.ToString(), of.SafeFileName.ToString());
        }

        public static (string, string) OpenExcelFile()
        {
            OpenFileDialog of = new OpenFileDialog
            {
                Filter = "Документы XLS(*.xlsx)|*.xlsx",
                Title = "Выбор документа"
            };
            of.ShowDialog();
            return (of.FileName.ToString(), of.SafeFileName.ToString());
        }

        public static string OpenCatalog()
        {
            FolderBrowserDialog of = new FolderBrowserDialog();
            of.ShowDialog();
            return of.SelectedPath.ToString();
        }
    }
}
