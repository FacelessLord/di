using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloudContainer.Api;

namespace TagCloudContainer.Implementations
{
    public class LayoutVisualizer : IRectangleVisualizer
    {
        private IRectanglePenProvider penProvider;
        private DrawingOptions options;
        private List<Rectangle> layout;

        public LayoutVisualizer(IRectanglePenProvider penProvider, List<Rectangle> layout, DrawingOptions options)
        {
            this.layout = layout;
            this.options = options;
            this.penProvider = penProvider;
        }

        public Image CreateImageWithRectangles()
        {
            var bmp = layout.CreateSizedBitmap();
            var graphics = Graphics.FromImage(bmp);
            FillBackground(graphics, bmp, options);

            DrawRectanglesOnImage(graphics, bmp);

            graphics.Flush();
            return bmp;
        }

        private void DrawRectanglesOnImage(Graphics graphics, Image img)
        {
            foreach (var rect in layout)
            {
                rect.Offset(new Point(img.Width / 2, img.Height / 2));
                graphics.DrawRectangle(penProvider.CreatePenForRectangle(rect), rect);
            }
        }

        private static void FillBackground(Graphics graphics, Image img, DrawingOptions options)
        {
            graphics.FillRegion(options.BackgroundBrush, new Region(new Rectangle(0, 0, img.Width, img.Width)));
        }
    }
}