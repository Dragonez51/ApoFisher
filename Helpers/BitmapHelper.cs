using Avalonia;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using ApoFisher.DataStructures;
using SkiaSharp;
using System.Runtime.InteropServices;

namespace ApoFisher.Helpers;

public static class BitmapHelper
{

    public static Bitmap Load(string source)
    {
        Bitmap bmp = new Bitmap("../../../"+source);
        // WriteableBitmap edit = new WriteableBitmap(bmp.PixelSize, bmp.Dpi, bmp.Format, bmp.AlphaFormat);
        return bmp;
    }
    
    public static WriteableBitmap CreateBitmap(int width, int height)
    {
        var bmp = new WriteableBitmap(
            new PixelSize(width, height),
            new Vector(96, 96),
            PixelFormat.Bgra8888,
            AlphaFormat.Opaque);

        int stride = width * 4;
        byte[] pixels = new byte[stride * height];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                int index = y * stride + x * 4;

                pixels[index + 0] = (byte)(y % 256); // B
                pixels[index + 1] = (byte)(y % 256); // G
                pixels[index + 2] = 128;             // R
                pixels[index + 3] = 255;             // A
            }
        }

        using (var fb = bmp.Lock())
        {
            Marshal.Copy(pixels, 0, fb.Address, pixels.Length);
        }

        return bmp;
    }

    public static WriteableBitmap SetWhite(WriteableBitmap bmp)
    {
        int stride = bmp.PixelSize.Width * 4;
        byte[] pixels = new byte[stride * bmp.PixelSize.Height];

        for (int y = 0; y < bmp.PixelSize.Height; y++)
        {
            for (int x = 0; x < bmp.PixelSize.Width; x++)
            {
                int index = y * stride + x * 4;
                
                pixels[index + 0] = 255;
                pixels[index + 1] = 255;
                pixels[index + 2] = 255;
                pixels[index + 3] = 255;
            }
        }

        using (var fb = bmp.Lock())
        {
            Marshal.Copy(pixels, 0, fb.Address, pixels.Length);
        }

        return bmp;
    }

    public static WriteableBitmap SetPixels(WriteableBitmap bmp, RGBA[][] pixels)
    {
        int stride = bmp.PixelSize.Width * 4;
        byte[] bmpPixels = new byte[stride * bmp.PixelSize.Height];

        for (int y = 0; y < bmp.PixelSize.Height; y++)
        {
            for (int x = 0; x < bmp.PixelSize.Width; x++)
            {
                int index = y * stride + x * 4;
                
                bmpPixels[index + 0] = (byte)pixels[y][x].red;
                bmpPixels[index + 1] = (byte)pixels[y][x].green;
                bmpPixels[index + 2] = (byte)pixels[y][x].blue;
                bmpPixels[index + 3] = (byte)pixels[y][x].alpha;
            }
        }

        using (var fb = bmp.Lock())
        {
            Marshal.Copy(bmpPixels, 0, fb.Address, pixels.Length);
        }

        return bmp;
    }

    // This function creates an icon from spriteSheet
    // with size of SIZExSIZE
    // from spriteSheet fragment of SIZExSIZE offset by XOFFSETxSIZE and YOFFSETxSIZE
    public static CroppedBitmap LoadCroppedBitmap(string source, int size, int xOffset, int yOffset)
    {
        Bitmap bmp = new Bitmap("../../../"+source);
        var cropped = new CroppedBitmap(bmp, new PixelRect(new PixelPoint(xOffset*size, yOffset*size), new PixelSize(size, size)));
        return cropped;
    }

    public static Bitmap LoadBitmapFromSpriteSheet(string source, int size, int x, int y) 
    {
        PixelRect region = new PixelRect(x*size, y*size, size, size);

        var target = new RenderTargetBitmap(new PixelSize(region.Width, region.Height));

        Bitmap spritesheet = new Bitmap("../../../" + source);

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