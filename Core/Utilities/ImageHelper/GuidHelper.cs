using Microsoft.AspNetCore.Http;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Base.Utilities.ImageHelper.GuidHelper;
using static System.Net.Mime.MediaTypeNames;

namespace Base.Utilities.ImageHelper
{
    public static partial class GuidHelper
    {
        public static void CreaterGuid(IFormFile imageFile,string entityType, out string _filepath)
        {
            Guid imageId = Guid.NewGuid();

            // Dosya uzantısını al
            string fileExtension = Path.GetExtension(imageFile.FileName);

            // Yeni dosya adını oluştur
            string newFileName = $"{imageId}{fileExtension}";

            // Tam dosya yolunu oluştur
            string filePath = FilePath.Full(newFileName,entityType);
            _filepath = filePath;

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                imageFile.CopyTo(stream);
                stream.Flush();
            }


        }

        

        public static void Update(IFormFile formFile,string entityType, string imagepath, out string _filepath)
        {
            var path = FilePath.Full(imagepath,entityType);
            if (File.Exists(path))
            {
                _filepath = path;
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

        public static void Delete(string imagePath, string entityType)
        {
            if (Path.Exists(FilePath.Full(imagePath,entityType)))
            {
                File.Delete(FilePath.Full(imagePath, entityType));
            }
            else
            {
                throw new DirectoryNotFoundException("Resim silinemedi");
            }
        }
    }
}
