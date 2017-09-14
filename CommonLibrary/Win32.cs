using System;
using System.Runtime.InteropServices;

namespace CommonLibrary
{
    /// <summary>
    /// Summary description for Win32.
    /// </summary>
    public class Win32
    {
        // The WM_COMMAND message is sent when the user selects a command item from a menu, 
        // when a control sends a notification message to its parent window, or when an 
        // accelerator keystroke is translated.
        public const int WM_MOUSEMOVE = 0x200;

        public const int WM_COMMAND = 0x111;
        public const int WM_LBUTTONDOWN = 0x201;
        public const int WM_LBUTTONUP = 0x202;
        public const int WM_LBUTTONDBLCLK = 0x203;
        public const int WM_RBUTTONDOWN = 0x204;
        public const int WM_RBUTTONUP = 0x205;
        public const int WM_RBUTTONDBLCLK = 0x206;
        public const int WM_KEYDOWN = 0x100;
        public const int WM_KEYUP = 0x101;

        public const int BM_CLICK = 0x00F5;


        public const int WM_ACTIVATEAPP = 0x001C;

        public const int WM_WINDOWPOSCHANGING = 0x0046;
        public const int WM_WINDOWPOSCHANGED = 0x0047;

        public const short SWP_NOMOVE = 0X2;
        public const short SWP_NOSIZE = 1;
        public const short SWP_NOZORDER = 0X4;
        public const int SWP_SHOWWINDOW = 0x0040;


        public const int MK_LBUTTON = 0x0001;


        public const int WM_NCACTIVATE = 0x0086;
        public const int WM_GETTEXT = 0x000D;
        public const int WM_ACTIVATE = 0x0006;
        public const int WM_KILLFOCUS = 0x0008;

        // The FindWindow function retrieves a handle to the top-level window whose class name
        // and window name match the specified strings. This function does not search child windows.
        // This function does not perform a case-sensitive search.
        [DllImport("User32.dll")]
        public static extern int FindWindow(string strClassName, string strWindowName);

        // The FindWindowEx function retrieves a handle to a window whose class name 
        // and window name match the specified strings. The function searches child windows, beginning
        // with the one following the specified child window. This function does not perform a case-sensitive search.
        [DllImport("User32.dll")]
        public static extern int FindWindowEx(int hwndParent, int hwndChildAfter, string strClassName, string strWindowName);


        // The SendMessage function sends the specified message to a 
        // window or windows. It calls the window procedure for the specified 
        // window and does not return until the window procedure has processed the message. 
        [DllImport("User32.dll")]
        public static extern Int32 SendMessage(
            int hWnd,               // handle to destination window
            int Msg,                // message
            int wParam,             // first message parameter
            [MarshalAs(UnmanagedType.LPStr)] string lParam); // second message parameter

        [DllImport("User32.dll")]
        public static extern Int32 SendMessage(
            int hWnd,               // handle to destination window
            int Msg,                // message
            int wParam,             // first message parameter
            int lParam);            // second message parameter


        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);


        [DllImport("User32.dll", EntryPoint = "SetForegroundWindow")]
        public static extern IntPtr SetForegroundWindowNative(IntPtr hWnd);


        [DllImport("User32.Dll")]
        public static extern long SetCursorPos(int x, int y);


        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hwnd, ref Rect rectangle);

        public struct Rect
        {
            public int Left { get; set; }
            public int Top { get; set; }
            public int Right { get; set; }
            public int Bottom { get; set; }

        }

        [DllImport("User32.Dll")]
        public static extern IntPtr SetActiveWindow(IntPtr hWnd);


        [DllImport("User32.Dll")]
        public static extern IntPtr SetFocus(IntPtr hWnd);

        [DllImport("User32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);


        public static void MouseLeftClick(System.Drawing.Point p, int handle = 0)
        {
            //build coordinates
            int coordinates = p.X | (p.Y << 16);
            //send left button down
            SendMessage(handle, 0x201, 0x1, coordinates);
            //send left button up
            SendMessage(handle, 0x202, 0x1, coordinates);
        }

        public Win32()
        {

        }

        ~Win32()
        {
        }

    }
}
