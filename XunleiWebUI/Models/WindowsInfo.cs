using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;

namespace XunleiWebUI.Models
{
    public class WindowsInfo
    {
        public IntPtr Handle { get; set; }
        public string Title { get; set; }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }        
}
