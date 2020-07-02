using DataProtector.Classes;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using UImageProtector.Classes;

namespace DataProtector
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public string directoryTemp = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Temp";
        public string directoryDocuments = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + @"\DataProtector";
        public string ImageFileString;
        public string ImageFileName;
        public string FileCopyString;
        public string DocumentString;
        public string DocumentName;
        public string WatermarkImageFileString;
        public string WatermarkImageFileString2;
        public string DirectoryPath;
        public string Type;
        public string path;
        public string currentLogFile;
        public string pass;
        public int Position;
        public int copyCheckBoxState = 0;
        public int logCheckBoxState = 0;
        public float OpacityValue;
        public float OpacityValue2;
        public DateTime dateTime;

        private void Window_Initialized(object sender, EventArgs e)
        {
            if (File.Exists("license.lic.xml") == true)
                try
                {
                    Spire.License.LicenseProvider.SetLicenseFileName("license.lic.xml");
                }
                catch
                {
                    MessageBox.Show("Ошибка лицензии");
                }
            if (logCheckBoxState == 1)
                currentLogFile = Logging.CreateLog();

            if (Directory.Exists(directoryTemp) == false)
                Directory.CreateDirectory(directoryTemp);
            if (Directory.Exists(directoryDocuments) == false)
            {
                Directory.CreateDirectory(directoryDocuments);
                Directory.CreateDirectory(directoryDocuments + @"\Logs");
            }
        }
        private void DrawOnImageButton_Click(object sender, RoutedEventArgs e)
        {
            if (TopLeftRadioButton.IsChecked == true)
                Position = 1;
            if (TopRightRadioButton.IsChecked == true)
                Position = 2;
            if (BottomLeftRadioButton.IsChecked == true)
                Position = 3;
            if (BottomRightRadioButton.IsChecked == true)
                Position = 4;
            if (CenterRadioButton.IsChecked == true)
                Position = 5;
            try
            {
                Type = "Image";
                OpacityValue = Convert.ToInt32(OpacityValueTextBox.Text) / 100f;
                var Watermark = WaterMarkOpacityChange.ChangeWMOpacity(WatermarkImageFileString, OpacityValue);
                var Image = ImageDraw.LoadImage(ImageFileString);
                ImageDraw.DrawImage(Image, ImageFileName, Watermark, Position);
                if (logCheckBoxState == 1)
                {
                    dateTime = DateTime.Now;
                    currentLogFile = Logging.CreateLog();
                    Logging.WriteLog(currentLogFile, Type, dateTime, ImageFileString, path, WatermarkImageFileString);
                }
            }
            catch
            {
                MessageBox.Show("Неверное значение поля, проверьте правильность данных.", "Ошибка!");
                OpacityValueTextBox.Text = null;
            }
        }

        private void ChooseImageButton_Click(object sender, RoutedEventArgs e)
        {
            var FileNames = GetFileName.OpenSourceImageFile(copyCheckBoxState);
            ImageFileString = FileNames.Item2;
            ImageFileName = FileNames.Item1;
            if (ImageFileString != null & ImageFileName != null)
                previewImage.Source = new BitmapImage(new Uri(ImageFileString));
        }

        private void ChooseWatermarkButton_Click(object sender, RoutedEventArgs e)
        {
            WatermarkImageFileString = GetFileName.OpenWatermarkFile();
            if (WatermarkImageFileString != null)
                previewWatermark.Source = new BitmapImage(new Uri(WatermarkImageFileString));
        }

        private void DocumentManageButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpacityValue2 = Convert.ToInt32(OpacityValueTextBox2.Text) / 100f;
                var Watermark = WaterMarkOpacityChange.ChangeWMOpacity(WatermarkImageFileString2, OpacityValue2);
                if (DOCXRadioButton.IsChecked == true)
                {
                    Type = "Word Document";
                    string path = WordManage.ManageWordFile(DocumentString, DirectoryPath, DocumentName, Watermark);
                }

                if (PDFRadioButton.IsChecked == true)
                {
                    Type = "Pdf Document";
                    string path = PDFManage.ManagePDF(DocumentString, DirectoryPath, DocumentName, WatermarkImageFileString2, OpacityValue2);
                }

                if (XLSXRadioButton.IsChecked == true)
                {
                    Type = "Excel Document";
                    pass = excelpassTextBox.Text.ToString();
                    string path = ExcelManage.ManageXLS(DocumentString, DirectoryPath, DocumentName, pass, Watermark);
                }

                if (logCheckBoxState == 1)
                {
                    dateTime = DateTime.Now;
                    currentLogFile = Logging.CreateLog();
                    Logging.WriteLog(currentLogFile, Type, dateTime, DocumentString, path, WatermarkImageFileString2);
                }
            }
            catch
            {
                MessageBox.Show("Неверное значение поля, проверьте правильность данных.", "Ошибка!");
                OpacityValueTextBox2.Text = null;
            }
        }
        private void OpenFileButton_Click(object sender, RoutedEventArgs e)
        {
            if (DOCXRadioButton.IsChecked == true)
            {
                var FileNamesWord = GetFileName.OpenWordFile();
                FileTextBox.Text = FileNamesWord.Item1;
                DocumentString = FileNamesWord.Item1;
                DocumentName = FileNamesWord.Item2;
            }

            if (PDFRadioButton.IsChecked == true)
            {
                var FileNamesPdf = GetFileName.OpenPdfFile();
                FileTextBox.Text = FileNamesPdf.Item1;
                DocumentString = FileNamesPdf.Item1;
                DocumentName = FileNamesPdf.Item2;
            }

            if (XLSXRadioButton.IsChecked == true)
            {
                var FileNamesExcel = GetFileName.OpenExcelFile();
                FileTextBox.Text = FileNamesExcel.Item1;
                DocumentString = FileNamesExcel.Item1;
                DocumentName = FileNamesExcel.Item2;
            }
        }

        private void OpenSaveDirectoryButton_Click(object sender, RoutedEventArgs e)
        {
            DirectoryPath = GetFileName.OpenCatalog();
            SaveDirectoryTextBox.Text = DirectoryPath;
        }

        private void ChooseWatermarkButton2_Click(object sender, RoutedEventArgs e)
        {
            WatermarkImageFileString2 = GetFileName.OpenWatermarkFile();
            if (WatermarkImageFileString2 != null)
                previewWatermark2.Source = new BitmapImage(new Uri(WatermarkImageFileString2));
        }
        private void DOCXRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            FileTextBox.Text = null;
        }

        private void PDFRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            FileTextBox.Text = null;
        }

        private void XLSXRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            FileTextBox.Text = null;
            excelpassTextBox.IsEnabled = true;
        }

        private void copyImageCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            copyCheckBoxState = 1;
        }

        private void copyImageCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            copyCheckBoxState = 0;
        }

        private void logCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            logCheckBoxState = 1;
        }

        private void logCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            logCheckBoxState = 0;
        }

        private void XLSXRadioButton_Unchecked(object sender, RoutedEventArgs e)
        {
            excelpassTextBox.IsEnabled = false;
        }
    }
}
