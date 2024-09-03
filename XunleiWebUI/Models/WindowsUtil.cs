using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using System.Text;
using System.Runtime.InteropServices;

namespace XunleiWebUI.Models
{
    public class WindowsUtil
    {
        #region user32
        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        private static extern IntPtr FindWindow(string className, string windowName);
        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        private static extern bool EnumWindows(EnumWindowsProc enumProc, IntPtr lParam);
        [DllImport("User32.dll", CharSet = CharSet.Unicode, EntryPoint = "GetWindowText")]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpWindowText, int nMaxCount);
        [DllImport("User32.dll", EntryPoint = "IsWindowVisible")]
        private static extern bool IsWindowVisible(IntPtr hWnd);
        [DllImport("User32.dll", CharSet = CharSet.Unicode, EntryPoint = "GetWindowTextLength")]
        private static extern int GetWindowTextLength(IntPtr hWnd);
        [DllImport("User32.dll", EntryPoint = "SetForegroundWindow")]
        private static extern IntPtr SetForegroundWindow(IntPtr hWnd);
        [DllImport("User32.dll", EntryPoint = "mouse_event")]
        private static extern IntPtr mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, int dwExtraInfo);
        [DllImport("User32.dll", EntryPoint = "GetWindowRect")]
        private static extern IntPtr GetWindowRect(IntPtr hWnd, out RECT lpRect);
        [DllImport("User32.dll", EntryPoint = "SetCursorPos")]
        private static extern IntPtr SetCursorPos(int dx, int dy);
        #endregion

        private const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const uint MOUSEEVENTF_LEFTUP = 0x0004;

        private static List<WindowsInfo> windowsList;
        private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        public static List<WindowsInfo> GetAllWindows()
        {
            windowsList = new List<WindowsInfo>();
            EnumWindows(EnumWindowsCallback, IntPtr.Zero);
            return windowsList;
        }

        public static bool generateAndSubmitAXunleiDownloadTask(string magenet)
        {
            try
            {
                ThunderAgentLib.Agent agent = new ThunderAgentLib.Agent();
                agent.AddTask(magenet, "", "", "", "", 1);
                agent.CommitTasks();
                Thread.Sleep(5000);

                IntPtr newAddTaskWindowHandle = RefreshXunleiAddTaskWindowHandle();
                if (newAddTaskWindowHandle == IntPtr.Zero)
                {
                    return false;
                }
                SetXunleiDownloadDialogToFrontAndClickSubmit(newAddTaskWindowHandle);
                return true;
            }
            catch
            {
                return false;
            }
        }

        #region private functions
        private static bool EnumWindowsCallback(IntPtr hWnd, IntPtr lParam)
        {
            if (IsWindowVisible(hWnd))
            {
                int length = GetWindowTextLength(hWnd);
                if (length == 0) return true;

                StringBuilder titlebuilder = new StringBuilder(length);
                GetWindowText(hWnd, titlebuilder, length + 1);
                windowsList.Add(new WindowsInfo { Handle = hWnd, Title = titlebuilder.ToString() });
            }

            return true;
        }
        private static IntPtr RefreshXunleiAddTaskWindowHandle()
        {
            GetAllWindows();
            IntPtr res = IntPtr.Zero;

            if (windowsList.Count > 0)
            {
                foreach (WindowsInfo window in windowsList)
                {
                    if (window.Title == "新建任务面板")
                    {
                        res = window.Handle;
                        break;
                    }
                }
            }

            return res;
        }
        private static void SetXunleiDownloadDialogToFrontAndClickSubmit(IntPtr handle)
        {
            SetForegroundWindow(handle);
            RECT windowRect;
            GetWindowRect(handle, out windowRect);

            int x = windowRect.Left + (windowRect.Right - windowRect.Left) / 2;
            int y = windowRect.Bottom - (windowRect.Bottom - windowRect.Top) / 8;

            SetCursorPos(x, y);
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }
        #endregion
    }
}