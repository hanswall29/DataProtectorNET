using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace UImageProtector.Classes
{
    public class ImageDraw
    {

        public static Bitmap LoadImage(string filename)
        {
            using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
                return new Bitmap(fs);
        }
        public static void DrawImage(Bitmap sourceFile, string FileName, Bitmap wmImage, int Position)
        {
            using (var sourceImageFile = new Bitmap(sourceFile))
            using (var waterMarkImage = new Bitmap(wmImage))
            using (var graph = Graphics.FromImage(sourceImageFile))
            {
                switch (Position)
                {
                    case 1:
                        graph.DrawImage(waterMarkImage, new Rectangle(0, 0, waterMarkImage.Width, waterMarkImage.Height)); 
                        break;
                    case 2:
                        graph.DrawImage(waterMarkImage, new Rectangle(sourceImageFile.Width - waterMarkImage.Width, 0, waterMarkImage.Width, waterMarkImage.Height)); 
                        break;
                    case 3:
                        graph.DrawImage(waterMarkImage, new Rectangle(0, sourceImageFile.Height - waterMarkImage.Height, waterMarkImage.Width, waterMarkImage.Height)); 
                        break;
                    case 4:
                        graph.DrawImage(waterMarkImage, new Rectangle(sourceImageFile.Width - waterMarkImage.Width, sourceImageFile.Height - waterMarkImage.Height, waterMarkImage.Width, waterMarkImage.Height)); 
                        break;
                    case 5:
                        graph.DrawImage(waterMarkImage, new Rectangle(sourceImageFile.Width / 2 - waterMarkImage.Width / 2, sourceImageFile.Height / 2 - waterMarkImage.Height / 2, waterMarkImage.Width, waterMarkImage.Height)); 
                        break;
                }
                sourceFile.Dispose();
                sourceImageFile.Save(FileName);
                sourceImageFile.Dispose();
                MessageBox.Show("Изображение сохранёно!", "Успех");
            }
        }
    }
}
