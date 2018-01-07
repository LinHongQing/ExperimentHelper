using ExperimentHelper.Basic;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace ExperimentHelper.Util
{
    public class Win32
    {

        public enum MouseEventFlags
        {
            Move = 0x0001,
            LeftDown = 0x0002,
            LeftUp = 0x0004,
            RightDown = 0x0008,
            RightUp = 0x0010,
            MiddleDown = 0x0020,
            MiddleUp = 0x0040,
            Wheel = 0x0080,
            XDown = 0x0100,
            XUp = 0x0200,
            Absolute = 0x8000
        }

        public enum MessageFlags : int
        {
            WM_LBUTTONDOWN = 0x201, //Left mousebutton down
            WM_LBUTTONUP = 0x202, //Left mousebutton up
            WM_LBUTTONDBLCLK = 0x203, //Left mousebutton doubleclick
            WM_RBUTTONDOWN = 0x204, //Right mousebutton down
            WM_RBUTTONUP = 0x205, //Right mousebutton up
            WM_RBUTTONDBLCLK = 0x206, //Right mousebutton doubleclick
            WM_KEYDOWN = 0x100, //Key down
            WM_KEYUP = 0x101, //Key up
            WM_SETTEXT = 0x000C,
            CB_SETCURSEL = 0x014E,
        }

        [DllImport("user32.dll")]
        public static extern int GetWindowText(IntPtr WinHandle, StringBuilder Title, int size);
        [DllImport("user32.dll")]
        public static extern int GetClassName(IntPtr WinHandle, StringBuilder Type, int size);
        public delegate bool EnumWindow(IntPtr hWnd, string lParam);
        public delegate bool EnumChildWindow(IntPtr WindowHandle, string name);
        [DllImport("user32.dll")]
        public static extern int EnumWindows(EnumWindow ew, string lParam);
        [DllImport("user32.dll")]
        public static extern int EnumChildWindows(IntPtr WinHandle, EnumChildWindow ecw, string name);
        [DllImport("user32.dll")]
        public static extern int PostMessage(IntPtr hWnd, int Msg, string wParam, string lParam);
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int WM_CHAR, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int WM_CHAR, string wParam, string lParam);
        [DllImport("user32.dll")]
        public extern static IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);
        [DllImport("user32.dll")]
        public static extern int PostMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool SetCursorPos(int X, int Y);
        [DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, UIntPtr dwExtraInfo);
        [DllImport("user32.dll")]
        public static extern bool GetClientRect(IntPtr hwnd, out StructComponent.RECT lpRect);
        [DllImport("user32.dll")]
        public static extern int ClientToScreen(IntPtr hwnd, ref StructComponent.POINT lpPoint);
    }
}

