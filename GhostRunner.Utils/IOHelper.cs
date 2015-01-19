using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostRunner.Utils
{
    public class IOHelper
    {
        public static void DeleteDirectory(string targetDirectory)
        {
            string[] files = Directory.GetFiles(targetDirectory);
            string[] dirs = Directory.GetDirectories(targetDirectory);

            foreach (string file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            foreach (string dir in dirs)
            {
                DeleteDirectory(dir);
            }

            Directory.Delete(targetDirectory, false);
        }
    }
}
