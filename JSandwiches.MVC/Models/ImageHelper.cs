using Microsoft.AspNetCore.Mvc.Rendering;

namespace JSandwiches.MVC.Models
{
    public static class ImageHelper
    {
        public async static Task<string?> SaveImage(IFormFile formFile)
        {
            var ImageFile = formFile;
            if (ImageFile != null && ImageFile.Length > 0)
            {
                // Generate a unique file name 

                // 50137a25-920c-469a-b202-4ce1e2a4712c_person-02.jpg
                var uniqueFileName = Guid.NewGuid() + "_" + ImageFile.FileName;

                // Define the final file path on the API server
                var apiFilePath = Path.Combine("mvc", "server", "uploads", uniqueFileName);

                // Save the file to the server
                using (var stream = new FileStream(apiFilePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }
                string imageFile = apiFilePath != String.Empty ? apiFilePath : "";
                return imageFile;
            }
            return null;
        }
    }
}
