using Avalonia;
using Avalonia.Media.Imaging;
using System;
using System.IO;

namespace ApoFisher.Helpers;

public static class BitmapHelper
{

    public static Bitmap Load(string source)
    {
        var path = Path.Combine(
            AppContext.BaseDirectory,
            "../../../",
            source
        );
        Bitmap bmp = new Bitmap(path);
        // WriteableBitmap edit = new WriteableBitmap(bmp.PixelSize, bmp.Dpi, bmp.Format, bmp.AlphaFormat);
        return bmp;
    }

    // This function creates an icon from spriteSheet
    // with size of SIZExSIZE
    // from spriteSheet fragment of SIZExSIZE offset by XOFFSETxSIZE and YOFFSETxSIZE
    public static Bitmap LoadBitmapFromSpriteSheet(string source, int size, int x, int y) 
    {
        PixelRect region = new PixelRect(x*size, y*size, size, size);

        var target = new RenderTargetBitmap(new PixelSize(region.Width, region.Height));

        var path = Path.Combine(
            AppContext.BaseDirectory,
            "../../../",
            source
        );
        Bitmap spritesheet = new Bitmap(path);

        using (var ctx = target.CreateDrawingContext(false)) 
        {
            ctx.DrawImage(
                spritesheet,
                new Rect(region.X, region.Y, region.Width, region.Height),
                new Rect(0, 0, region.Width, region.Height)
                );
        }

        return target;
    }
}