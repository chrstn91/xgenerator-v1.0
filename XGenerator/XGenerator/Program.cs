using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace XGenerator
{
    class Program
    {
        static Random random = new Random();
        static Bitmap DrawXAtPosition(Bitmap image, int x, int y, int sizeOfX)
        {
            int drawFromX = Math.Max(0, x - sizeOfX);
            int drawFromY = Math.Max(0, y - sizeOfX);
            int drawToX = Math.Min(image.Width - 1, x + sizeOfX);
            int drawToY = Math.Min(image.Height - 1, y + sizeOfX);

            for (int _x = drawFromX; _x <= drawToX; _x++)
            {
                for (int _y = drawFromY; _y <= drawToY; _y++)
                {
                    if (_x == x || _y == y)
                    {
                        image.SetPixel(_x, _y, Color.Black);
                    }
                }
            }

            return image;
        }

        static Point GenerateImageWithX(int index, int width, int height, int sizeOfX)
        {
            Bitmap bmp = new Bitmap(width, height);
            
            // create a white image
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    bmp.SetPixel(x, y, Color.White);
                }
            }

            Point xPosition = new Point(
                random.Next(0, width + 1),
                random.Next(0, height + 1));

            bmp = DrawXAtPosition(bmp, xPosition.X, xPosition.Y, sizeOfX);
            var imageFormat = ImageFormat.Png;
            bmp.Save($"img{index:000}.png", imageFormat);

            return xPosition;
        }

        static void Main(string[] args)
        {
            Console.Write("Number of images to generate: ");
            int imageCount = Convert.ToInt32(Console.ReadLine());

            Console.Write("Radius of cross in pixels: ");
            int sizeOfX = Convert.ToInt32(Console.ReadLine());

            using (StreamWriter output = new StreamWriter("coords.txt"))
            {
                for (int i = 0; i < imageCount; i++)
                {
                    var xPosition = GenerateImageWithX(i + 1, 50, 50, sizeOfX);

                    output.WriteLine($"{xPosition.X} {xPosition.Y}");
                }
            }

            Console.WriteLine("Done.");
            Console.ReadKey();
        }
    }
}