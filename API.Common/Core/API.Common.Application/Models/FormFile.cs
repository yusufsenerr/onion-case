using Microsoft.AspNetCore.Http;

namespace API.Common.Application.Models
{
    public static class FileHelper
    {
        public static byte[] FormFileToBytes(IFormFile file)
        {
            if (file == null) return null;

            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }
}
