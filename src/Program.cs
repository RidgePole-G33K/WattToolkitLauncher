using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

namespace WattToolkitLauncher
{
    static class Program
    {
        static void Main()
        {
            #region Init
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;

            var processPath = $"{baseDir}Steam++.exe";
            if (!File.Exists(processPath))
                return;
            #endregion
            #region Restore UISetting
            var backupPath = $"{baseDir}AppData\\Settings\\UISettings.json.save.bak";
            if (File.Exists(backupPath))
            {
                var settingPath = $"{baseDir}AppData\\Settings\\UISettings.json";
                if (File.Exists(settingPath))
                    File.Delete(settingPath);

                File.Copy(backupPath, settingPath);
            }
            #endregion
            #region Wait Process Exit
            var waiter = new AutoResetEvent(false);

            var process = new Process
            {
                EnableRaisingEvents = true,
                StartInfo = new ProcessStartInfo(processPath)
            };
            process.Exited += (o, e) =>
            {
                RefreshNotification();
                waiter.Set();
            };
            process.Start();

            waiter.WaitOne();
            #endregion
        }
        #region Util
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string className, string windowTitle);
        [DllImport("user32.dll")]
        static extern bool GetClientRect(IntPtr handle, out Rectangle rect);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr handle, uint message, int wParam, int lParam);

        static IntPtr GetNotifyAreaHandle()
        {
            var trayWndHandle = FindWindowEx(IntPtr.Zero, IntPtr.Zero, "Shell_TrayWnd", string.Empty);
            var trayNotifyWndHandle = FindWindowEx(trayWndHandle, IntPtr.Zero, "TrayNotifyWnd", string.Empty);
            var sysPagerHandle = FindWindowEx(trayNotifyWndHandle, IntPtr.Zero, "SysPager", string.Empty);
            return FindWindowEx(sysPagerHandle, IntPtr.Zero, "ToolbarWindow32", "用户提示通知区域");
        }
        static IntPtr GetNotifyOverHandle()
        {
            var overHandle = FindWindowEx(IntPtr.Zero, IntPtr.Zero, "NotifyIconOverflowWindow", string.Empty);
            return FindWindowEx(overHandle, IntPtr.Zero, "ToolbarWindow32", "溢出通知区域");
        }
        static void RefreshWindow(IntPtr windowHandle)
        {
            const uint WM_MOUSEMOVE = 0x0200;

            GetClientRect(windowHandle, out Rectangle rect);
            for (var x = 0; x < rect.right; x += 5)
            {
                for (var y = 0; y < rect.bottom; y += 5)
                    SendMessage(windowHandle, WM_MOUSEMOVE, 0, (y << 16) + x);
            }
        }

        public static void RefreshNotification()
        {
            var notifyAreaHandle = GetNotifyAreaHandle();
            if (notifyAreaHandle != IntPtr.Zero)
                RefreshWindow(notifyAreaHandle);

            var notifyOverHandle = GetNotifyOverHandle();
            if (notifyOverHandle != IntPtr.Zero)
                RefreshWindow(notifyOverHandle);
        }

        struct Rectangle
        {
            public int left, top, right, bottom;
        }
        #endregion
    }
}