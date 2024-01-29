using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

Console.WriteLine(
  $"Usage: convert-tokens.exe <outputSizePixels> <inputFolderPath> <outputFolderPath> <maskFileFilePath> [<borderFilePath>]");

try
{
  if (args.Length < 5)
  {
    throw new ArgumentException($"Aborted: Required parameters are missing");
  }
  string strSize = args[0];
  if (!Int32.TryParse(strSize, out int imageSize) || imageSize < 16 || imageSize > 4096)
  {
    throw new ArgumentException("Aborted: Output size must be an integer from 16 to 4096");
  }
  string inputPath = args[1];
  string outputPath = args[2];
  string maskPath = args[3];
  string borderPath = string.Empty;
  if (args.Length > 4)
  {
    borderPath = args[4];
  }

  if (!Directory.Exists(inputPath)) throw new DirectoryNotFoundException($"Aborted: input folder not found:\n{inputPath}");
  if (!File.Exists(maskPath)) throw new DirectoryNotFoundException($"Aborted: image mask not found:\n{inputPath}");
  if (!string.IsNullOrWhiteSpace(borderPath) && !File.Exists(borderPath)) throw new DirectoryNotFoundException($"Aborted: input folder not found:\n{inputPath}");

  Console.WriteLine(inputPath);

  if (!Directory.Exists(outputPath))
  {
    Directory.CreateDirectory(outputPath);
  }

  // make mask and border consistent sizes, just in case
  Bitmap imgMask = ResizeImage(Image.FromFile(maskPath), imageSize, imageSize);
  Bitmap? imgBorder = null;
  if (!string.IsNullOrWhiteSpace(borderPath))
  {
    imgBorder = ResizeImage(Image.FromFile(borderPath), imageSize, imageSize);
  }

  // do recursive search for images in input folder
  string[] imageExtensionsToConvert = { ".bmp", ".gif", ".jpe", ".jpg", ".jpeg", ".png", ".tif", ".tiff", ".webp", ".wmf" };
  string[] files =
      Directory.GetFiles(inputPath, $"*.*", SearchOption.AllDirectories)
      .Where(filepath =>
        imageExtensionsToConvert.Contains(Path.GetExtension(filepath), StringComparer.InvariantCultureIgnoreCase))
      .ToArray();

  // resize & convert images into transparent tokens by applying border and mask overlays
  Console.WriteLine($"Converting {files.Length} images from {inputPath}\nto tokens in {outputPath}");
  foreach (string filename in files)
  {
    // get token art
    FileInfo file = new(filename);
    Image imgOriginal = Image.FromFile(file.FullName);
    Bitmap imgNew = ResizeImage(imgOriginal, imageSize, imageSize);

    using Graphics graphics = Graphics.FromImage(imgNew);
    graphics.CompositingMode = CompositingMode.SourceOver;
    graphics.CompositingQuality = CompositingQuality.GammaCorrected;
    // draw the token border
    if (imgBorder != null)
    {
      graphics.DrawImage(imgBorder, 0, 0, imageSize, imageSize);
    }

    // draw background mask and make the key color transparent
    graphics.DrawImage(imgMask, 0, 0, imageSize, imageSize);
    Color backColor = imgMask.GetPixel(1, 1);
    imgNew.MakeTransparent(backColor);

    string outFileName = Path.Combine(outputPath, $"{Path.GetFileNameWithoutExtension(file.Name)}.png");
    Console.WriteLine($"Converting {filename} to {outFileName}");
    imgNew.Save(outFileName, ImageFormat.Png);
  }
}
catch (Exception ex)
{
  Console.WriteLine(ex);
  return 1;
}

return 0;

// https://stackoverflow.com/questions/1922040/how-to-resize-an-image-c-sharp
/// <summary>
/// Resize the image to the specified width and height.
/// </summary>
/// <param name="image">The image to resize.</param>
/// <param name="width">The width to resize to.</param>
/// <param name="height">The height to resize to.</param>
/// <returns>The resized image.</returns>
static Bitmap ResizeImage(Image image, int width, int height)
{
  Rectangle destRect = new(0, 0, width, height);
  Bitmap destImage = new(destRect.Width, destRect.Height);
  destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);
  using (Graphics graphics = Graphics.FromImage(destImage))
  {
    graphics.CompositingMode = CompositingMode.SourceCopy;
    graphics.CompositingQuality = CompositingQuality.HighQuality;
    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
    graphics.SmoothingMode = SmoothingMode.HighQuality;
    graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
    using var wrapMode = new ImageAttributes();
    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
  }
  return destImage;
}