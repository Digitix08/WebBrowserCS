using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebBrowserCS.IEMode
{
    public static class PopupCatcher
    {
        private delegate bool EnumWindowsProc(IntPtr hwnd, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern bool EnumChildWindows(IntPtr hwndParent, EnumWindowsProc lpEnumFunc, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern IntPtr FindWindowEx(
            IntPtr parentHandle,
            IntPtr childAfter,
            string className,
            string windowTitle);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern uint GetWindowThreadProcessId(
            IntPtr parentHandle,
            out uint processid);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        private const int BM_CLICK = 0x00F5;
        public static IntPtr GetIEServerHwnd(WebBrowser wb)
        {
            return FindWindowEx(wb.Handle, IntPtr.Zero, "Internet Explorer_Server", null);
        }
        public static uint GetThread(IntPtr hwnd)
        {
            uint pid;
            return GetWindowThreadProcessId(hwnd, out pid);
        }

        public static bool SameThread(IntPtr popup, IntPtr ieServer)
        {
            MessageBox.Show(GetThread(popup).ToString(), GetThread(ieServer).ToString());
            return GetThread(popup) == GetThread(ieServer);
        }
        public static T FindControlRecursive<T>(Control parent) where T : Control
        {
            foreach (Control child in parent.Controls)
            {
                if (child is T t)
                    return t;

                T result = FindControlRecursive<T>(child);
                if (result != null)
                    return result;
            }

            return null;
        }
        public static List<IntPtr> GetWindowsByClassName(string className)
        {
            List<IntPtr> results = new List<IntPtr>();

            EnumWindows((hwnd, lParam) =>
            {
                StringBuilder sb = new StringBuilder(256);
                GetClassName(hwnd, sb, sb.Capacity);

                if (sb.ToString() == className)
                    results.Add(hwnd);

                return true;
            }, IntPtr.Zero);

            return results;
        }
        public static void Start(Control tabControl)//starts the big thing
        {
            IntPtr tabHwnd = tabControl.Handle;
            
            WebBrowser wb = FindControlRecursive<WebBrowser>(tabControl);

            IntPtr IEServer = GetIEServerHwnd(wb);
            MessageBox.Show("begun", IEServer.ToInt32().ToString());
            Task.Run(async () =>
            {
                while (true)
                {
                    EnumWindows((hwnd, lParam) =>
                    {
                        StringBuilder sb = new StringBuilder(256);
                        GetWindowText(hwnd, sb, sb.Capacity);

                        string title = sb.ToString();

                        if (title.Contains("Script Error"))
                        {
                            HandlePopup(hwnd, IEServer);
                        }

                        return true;
                    }, IntPtr.Zero);

                    await Task.Delay(2); // fast but safe
                }
            });
        }
        private static bool BelongsToTab(IntPtr popup, IntPtr tab)
        {
            MessageBox.Show(SameThread(popup, tab).ToString());
            return SameThread(popup, tab);
        }
        private static void HandlePopup(IntPtr popupHwnd, IntPtr tabHwnd)
        {
            //if (!BelongsToTab(popupHwnd, tabHwnd))
            //return; // popup belongs to another tab
            MessageBox.Show(popupHwnd.ToInt32().ToString());
            EnumChildWindows(popupHwnd, (child, lParam) =>
            {
                StringBuilder sb = new StringBuilder(512);
                GetWindowText(child, sb, sb.Capacity);

                string txt = sb.ToString();

                if (!string.IsNullOrWhiteSpace(txt))
                {
                    Debug.WriteLine("SCRIPT ERROR: " + txt);
                }

                if (txt.Equals("Yes", StringComparison.OrdinalIgnoreCase))
                {
                    SendMessage(child, BM_CLICK, IntPtr.Zero, IntPtr.Zero);
                }

                return true;
            }, IntPtr.Zero);
        }
    }
}
