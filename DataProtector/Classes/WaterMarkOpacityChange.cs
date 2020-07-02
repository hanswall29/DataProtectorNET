using System.Drawing;
using System.Drawing.Imaging;

namespace UImageProtector.Classes
{
    public class WaterMarkOpacityChange
    {
        public static Bitmap ChangeWMOpacity(string wmImage, float opacity)
        {
            using (var waterMarkImage = new Bitmap(wmImage))
            {
                Bitmap bmp = new Bitmap(waterMarkImage.Height, waterMarkImage.Width);
                Graphics graphics = Graphics.FromImage(bmp);
                ColorMatrix colorMatrix = new ColorMatrix
                {
                    Matrix33 = opacity
                };
                ImageAttributes imageAtr = new ImageAttributes();
                imageAtr.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                graphics.DrawImage(waterMarkImage, new Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, waterMarkImage.Width, waterMarkImage.Height, GraphicsUnit.Pixel, imageAtr);
                graphics.Dispose();

                return bmp;
            }

        }

    }
}
