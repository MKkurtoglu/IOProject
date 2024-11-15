using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Base.Utilities.ImageHelper.GuidHelper;

namespace Base.Utilities.ImageHelper
{
    public static class ProfileGuidHelper
    {
        public static void CreaterProfileGuid(IFormFile imageFile, out string _filepath)
        {
            Guid imageId = Guid.NewGuid();

            // Dosya uzantısını al
            string fileExtension = Path.GetExtension(imageFile.FileName);

            // Yeni dosya adını oluştur
            string newFileName = $"{imageId}{fileExtension}";

            // Tam dosya yolunu oluştur
            string filePath = ProfileFilePath.Full(newFileName);
            _filepath = filePath;

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                imageFile.CopyTo(stream);
                stream.Flush();
            }


        }

        public static void Update(IFormFile formFile, string imagepath, out string _filepath)
        {
            var path = ProfileFilePath.Full(imagepath);
            _filepath= path;
            if (File.Exists(path))
            {
                using FileStream fileStream = new(path, FileMode.Create);
                //FileMode.Create burada üzerine yazma işlemi yapar.
                formFile.CopyTo(fileStream);
                fileStream.Flush();
            }
            else
            {
                throw new Exception("Veri bulunamadı.");
            }
        }


        public static void Delete(string imagePath)
        {
            if (Path.Exists(ProfileFilePath.Full(imagePath)))
            {
                File.Delete(ProfileFilePath.Full(imagePath));
            }
            else
            {
                throw new DirectoryNotFoundException("Resim silinemedi");
            }
        }
    }
}
