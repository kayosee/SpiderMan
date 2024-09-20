using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WXExport.Singleton
{
    public static class WXExecutable
    {
        [DllImport("Imagehlp.dll")]
        public extern static LOADED_IMAGE ImageLoad(string dllName, string dllPath);
    }
}
