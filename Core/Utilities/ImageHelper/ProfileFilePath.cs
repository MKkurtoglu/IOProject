using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Utilities.ImageHelper
{
    public static class ProfileFilePath
    {

        public static string Full(string path, string root = ProfileImageConst.root, string fileType = ProfileImageConst.ek)
        {
            return Path.Combine(Directory.GetCurrentDirectory(), root + fileType, path);
        }
    }
}
