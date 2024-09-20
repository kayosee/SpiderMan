using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WeChatExport.Singleton
{
    public class Executable
    {
        public string fullPath;
        private void ReadFile(string path)
        {
            using (var stream = File.OpenRead(path))
            {

            }
        }
        public Executable(string filePath) 
        {
            fullPath = filePath;
            ReadFile(fullPath);
        }

        public Architecture GetArchitecture()
        {
            
        }
    }
}
