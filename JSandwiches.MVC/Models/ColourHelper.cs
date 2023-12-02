namespace JSandwiches.MVC.Models
{
    using SixLabors.ImageSharp;
    using SixLabors.ImageSharp.Processing;
    using SixLabors.ImageSharp.PixelFormats;
    using SixLabors.ImageSharp.Advanced;

    public static class ColourHelper
    {
        public static Rgba32 GetDominantColor(string imagePath)
        {
            using (var image = Image.Load<Rgba32>(imagePath))
            {
                // Resize the image to a small size for faster processing
                image.Mutate(x => x.Resize(new ResizeOptions
                {
                    Size = new Size(50, 50),
                    Mode = ResizeMode.Max
                }));

                // Get the pixel data
                var pixelData = image.GetPixelMemoryGroup();

                // Calculate the average color
                int totalRed = 0, totalGreen = 0, totalBlue = 0;
                int pixelCount = 0;

                foreach (var memory in pixelData)
                {
                    var span = memory.Span;
                    for (int i = 0; i < span.Length; i++)
                    {
                        var pixel = span[i];
                        totalRed += pixel.R;
                        totalGreen += pixel.G;
                        totalBlue += pixel.B;
                        pixelCount++;
                    }
                }

                byte averageRed = (byte)(totalRed / pixelCount);
                byte averageGreen = (byte)(totalGreen / pixelCount);
                byte averageBlue = (byte)(totalBlue / pixelCount);

                return new Rgba32(averageRed, averageGreen, averageBlue);
            }
        }

        public static string GetFontColor(Rgba32 backgroundColor)
        {
            // Calculate the luminance of the background color
            double luminance = (0.299 * backgroundColor.R) + (0.587 * backgroundColor.G) + (0.114 * backgroundColor.B);

            // Normalize the luminance to be between 0 and 1
            luminance /= 255;

            // If the luminance is less than 0.5, the background color is dark, so return white
            // Otherwise, return an ashy shade of black
            return luminance < 0.5 ? "#FFFFFF" : "#2a2c37";
        }
    }
}