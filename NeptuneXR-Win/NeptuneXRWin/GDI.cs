using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace NeptuneXRWin
{
	// Token: 0x02000005 RID: 5
	internal class GDI
	{
		// Token: 0x0600000D RID: 13
		[DllImport("user32.dll")]
		public static extern IntPtr GetDC(IntPtr hWnd);

		// Token: 0x0600000E RID: 14
		[DllImport("gdi32.dll")]
		public static extern IntPtr DeleteDC(IntPtr hdc);

		// Token: 0x0600000F RID: 15
		[DllImport("gdi32.dll")]
		public static extern IntPtr CreateSolidBrush(uint color);

		// Token: 0x06000010 RID: 16
		[DllImport("gdi32.dll")]
		public static extern IntPtr CreateCompatibleDC(IntPtr hdc);

		// Token: 0x06000011 RID: 17
		[DllImport("gdi32.dll")]
		public static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int cx, int cy);

		// Token: 0x06000012 RID: 18
		[DllImport("gdi32.dll", EntryPoint = "GdiAlphaBlend", SetLastError = true)]
		public static extern bool AlphaBlend(IntPtr hdcDest, int xoriginDest, int yoriginDest, int wDest, int hDest, IntPtr hdcSrc, int xoriginSrc, int yoriginSrc, int wSrc, int hSrc, GDI.BLENDFUNCTION ftn);

		// Token: 0x06000013 RID: 19
		[DllImport("gdi32.dll", SetLastError = true)]
		public static extern bool BitBlt(IntPtr hdc, int x, int y, int cx, int cy, IntPtr hdcSrc, int x1, int y1, uint rop);

		// Token: 0x06000014 RID: 20
		[DllImport("user32.dll")]
		public static extern int ReleaseDC(IntPtr hwnd, IntPtr hdc);

		// Token: 0x06000015 RID: 21
		[DllImport("gdi32.dll", SetLastError = true)]
		public static extern bool Rectangle(IntPtr hdc, int left, int top, int right, int bottom);

		// Token: 0x06000016 RID: 22
		[DllImport("gdi32.dll", SetLastError = true)]
		public static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

		// Token: 0x06000017 RID: 23
		[DllImport("gdi32.dll", SetLastError = true)]
		public static extern IntPtr DeleteObject(IntPtr ho);

		// Token: 0x06000018 RID: 24
		[DllImport("gdi32.dll")]
		public static extern bool PatBlt(IntPtr hdc, int x, int y, int width, int height, uint rop);

		// Token: 0x06000019 RID: 25
		[DllImport("user32.dll")]
		public static extern bool EnumWindows(GDI.EnumWindowsProc lpEnumFunc, IntPtr lParam);

		// Token: 0x0600001A RID: 26
		[DllImport("user32.dll")]
		public static extern int GetSystemMetrics(int nIndex);

		// Token: 0x0600001B RID: 27
		[DllImport("user32.dll")]
		public static extern IntPtr GetDesktopWindow();

		// Token: 0x0600001C RID: 28
		[DllImport("user32.dll")]
		public static extern IntPtr GetWindowDC(IntPtr hWnd);

		// Token: 0x0600001D RID: 29
		[DllImport("user32.dll")]
		public static extern bool ExtractIconEx(string lpszFile, int nIconIndex, out IntPtr phiconLarge, out IntPtr phiconSmall, uint nIcons);

		// Token: 0x0600001E RID: 30
		[DllImport("user32.dll")]
		public static extern IntPtr LoadIcon(IntPtr hInstance, IntPtr lpIconName);

		// Token: 0x0600001F RID: 31
		[DllImport("user32.dll")]
		public static extern bool DrawIcon(IntPtr hdc, int x, int y, IntPtr hIcon);

		// Token: 0x06000020 RID: 32
		[DllImport("gdi32.dll", SetLastError = true)]
		public static extern bool StretchBlt(IntPtr hdcDest, int xDest, int yDest, int wDest, int hDest, IntPtr hdcSrc, int xSrc, int ySrc, int wSrc, int hSrc, uint rop);

		// Token: 0x06000021 RID: 33
		[DllImport("gdi32.dll")]
		private static extern IntPtr CreateDIBSection(IntPtr hdc, ref GDI.BITMAPINFO pbmi, uint iUsage, out IntPtr ppvBits, IntPtr hSection, uint dwOffset);

		// Token: 0x06000022 RID: 34 RVA: 0x00002864 File Offset: 0x00000A64
		private static GDI.HSL RGBtoHSL(byte r, byte g, byte b)
		{
			float num = (float)r / 255f;
			float num2 = (float)g / 255f;
			float num3 = (float)b / 255f;
			float num4 = Math.Max(num, Math.Max(num2, num3));
			float num5 = Math.Min(num, Math.Min(num2, num3));
			float num6 = (num4 + num5) / 2f;
			bool flag = num4 == num5;
			float num7;
			float num8;
			if (flag)
			{
				num7 = 0f;
				num8 = 0f;
			}
			else
			{
				float num9 = num4 - num5;
				num8 = ((num6 > 0.5f) ? (num9 / (2f - num4 - num5)) : (num9 / (num4 + num5)));
				bool flag2 = num4 == num;
				if (flag2)
				{
					num7 = (num2 - num3) / num9 + (float)((num2 < num3) ? 6 : 0);
				}
				else
				{
					bool flag3 = num4 == num2;
					if (flag3)
					{
						num7 = (num3 - num) / num9 + 2f;
					}
					else
					{
						num7 = (num - num2) / num9 + 4f;
					}
				}
				num7 /= 6f;
			}
			return new GDI.HSL
			{
				h = num7,
				s = num8,
				l = num6
			};
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002980 File Offset: 0x00000B80
		private static void HSLtoRGB(GDI.HSL hsl, out byte r, out byte g, out byte b)
		{
			float h = hsl.h;
			float s = hsl.s;
			float l = hsl.l;
			bool flag = s == 0f;
			float num3;
			float num2;
			float num;
			if (flag)
			{
				num = (num2 = (num3 = l));
			}
			else
			{
				float num4 = ((l < 0.5f) ? (l * (1f + s)) : (l + s - l * s));
				float num5 = 2f * l - num4;
				num2 = GDI.HueToRGB(num5, num4, h + 0.33333334f);
				num = GDI.HueToRGB(num5, num4, h);
				num3 = GDI.HueToRGB(num5, num4, h - 0.33333334f);
			}
			r = (byte)(num2 * 255f);
			g = (byte)(num * 255f);
			b = (byte)(num3 * 255f);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002A38 File Offset: 0x00000C38
		private static float HueToRGB(float p, float q, float t)
		{
			bool flag = t < 0f;
			if (flag)
			{
				t += 1f;
			}
			bool flag2 = t > 1f;
			if (flag2)
			{
				t -= 1f;
			}
			bool flag3 = t < 0.16666667f;
			float num;
			if (flag3)
			{
				num = p + (q - p) * 6f * t;
			}
			else
			{
				bool flag4 = t < 0.5f;
				if (flag4)
				{
					num = q;
				}
				else
				{
					bool flag5 = t < 0.6666667f;
					if (flag5)
					{
						num = p + (q - p) * (0.6666667f - t) * 6f;
					}
					else
					{
						num = p;
					}
				}
			}
			return num;
		}

		// Token: 0x06000025 RID: 37
		[DllImport("user32.dll")]
		public static extern bool InvalidateRect(IntPtr hWnd, IntPtr lpRect, bool bErase);

		// Token: 0x06000026 RID: 38 RVA: 0x00002AC8 File Offset: 0x00000CC8
		public static uint GetRandomHEXColor()
		{
			byte b = (byte)GDI.rand.Next(255);
			byte b2 = (byte)GDI.rand.Next(255);
			byte b3 = (byte)GDI.rand.Next(255);
			return (uint)(((int)b << 16) | ((int)b2 << 8) | (int)b3);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002B18 File Offset: 0x00000D18
		public static void Siney()
		{
			DateTime now = DateTime.Now;
			TimeSpan timeSpan = TimeSpan.FromSeconds(25.0);
			while (DateTime.Now - now < timeSpan)
			{
				IntPtr dc = GDI.GetDC(IntPtr.Zero);
				int systemMetrics = GDI.GetSystemMetrics(0);
				int systemMetrics2 = GDI.GetSystemMetrics(1);
				double num = 0.0;
				double num2 = num;
				for (int i = 0; i < systemMetrics2; i++)
				{
					int num3 = (int)(Math.Sin(num2) * 30.0);
					GDI.BitBlt(dc, 0, i, systemMetrics, 1, dc, num3, i, 13369376U);
					num2 += 0.5;
				}
				num += 0.5;
				Thread.Sleep(1);
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002BEC File Offset: 0x00000DEC
		public static void Inverter3()
		{
			DateTime now = DateTime.Now;
			TimeSpan timeSpan = TimeSpan.FromSeconds(25.0);
			while (DateTime.Now - now < timeSpan)
			{
				IntPtr dc = GDI.GetDC(IntPtr.Zero);
				int systemMetrics = GDI.GetSystemMetrics(0);
				int systemMetrics2 = GDI.GetSystemMetrics(1);
				GDI.BitBlt(dc, 0, 0, systemMetrics, systemMetrics2, dc, 0, 0, 5570569U);
				Thread.Sleep(5);
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002C60 File Offset: 0x00000E60
		public static void GDIword()
		{
			DateTime now = DateTime.Now;
			TimeSpan timeSpan = TimeSpan.FromSeconds(27.0);
			while (DateTime.Now - now < timeSpan)
			{
				IntPtr dc = GDI.GetDC(IntPtr.Zero);
				GDI.BITMAPINFO bitmapinfo = default(GDI.BITMAPINFO);
				bitmapinfo.bmiHeader.biSize = (uint)Marshal.SizeOf(typeof(GDI.BITMAPINFOHEADER));
				bitmapinfo.bmiHeader.biWidth = GDI.w;
				bitmapinfo.bmiHeader.biHeight = -GDI.hgt;
				bitmapinfo.bmiHeader.biPlanes = 1;
				bitmapinfo.bmiHeader.biBitCount = 32;
				bitmapinfo.bmiHeader.biCompression = 0U;
				IntPtr intPtr2;
				IntPtr intPtr = GDI.CreateDIBSection(dc, ref bitmapinfo, 0U, out intPtr2, IntPtr.Zero, 0U);
				IntPtr intPtr3 = GDI.CreateCompatibleDC(dc);
				GDI.SelectObject(intPtr3, intPtr);
				GDI.BitBlt(intPtr3, 0, 0, GDI.w, GDI.hgt, dc, 0, 0, 13369376U);
				int num = GDI.w * GDI.hgt;
				int[] array = new int[num];
				Marshal.Copy(intPtr2, array, 0, num);
				Random random = new Random();
				float num2 = 0.08f;
				while (DateTime.Now - now < timeSpan)
				{
					for (int i = 0; i < num; i++)
					{
						int num3 = array[i];
						byte b = (byte)(num3 & 255);
						byte b2 = (byte)((num3 >> 5) & 255);
						byte b3 = (byte)((num3 >> 16) & 255);
						GDI.HSL hsl = GDI.RGBtoHSL(b3, b2, b);
						hsl.h += num2;
						bool flag = hsl.h > 1f;
						if (flag)
						{
							hsl.h -= 1f;
						}
						GDI.HSLtoRGB(hsl, out b3, out b2, out b);
						array[i] = (int)b | ((int)b2 << 5) | ((int)b3 << 16);
					}
					Marshal.Copy(array, 0, intPtr2, num);
					GDI.BitBlt(dc, 0, 0, GDI.w, GDI.hgt, intPtr3, 0, 0, 13369376U);
					Thread.Sleep(1);
				}
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002E8C File Offset: 0x0000108C
		public static void GDIword2()
		{
			DateTime now = DateTime.Now;
			TimeSpan timeSpan = TimeSpan.FromSeconds(27.0);
			while (DateTime.Now - now < timeSpan)
			{
				IntPtr dc = GDI.GetDC(IntPtr.Zero);
				Graphics graphics = Graphics.FromHdc(dc);
				graphics.SmoothingMode = SmoothingMode.AntiAlias;
				string[] array = new string[] { "NeptuneXRWin.exe", "NeptuneXRWin.exe", "NeptuneXRWin.exe", "NeptuneXRWin.exe", "NeptuneXRWin.exe" };
				Color[] array2 = new Color[]
				{
					Color.Red,
					Color.Orange,
					Color.Yellow,
					Color.Green,
					Color.Blue
				};
				float[] array3 = new float[] { -30f, 15f, 45f, -60f, 75f };
				int width = Screen.PrimaryScreen.Bounds.Width;
				int height = Screen.PrimaryScreen.Bounds.Height;
				while (DateTime.Now - now < timeSpan)
				{
					for (int i = 0; i < 5; i++)
					{
						using (Font font = new Font("Arial", 67f, FontStyle.Bold | FontStyle.Underline))
						{
							using (Brush brush = new SolidBrush(array2[i]))
							{
								graphics.ResetTransform();
								int num = GDI.rnd.Next(0, width);
								int num2 = GDI.rnd.Next(0, height);
								graphics.TranslateTransform((float)num, (float)num2);
								graphics.RotateTransform(array3[i]);
								graphics.ScaleTransform(2f, 1f);
								graphics.DrawString(array[i], font, brush, 0f, 0f);
							}
						}
					}
					Thread.Sleep(100);
				}
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000030AC File Offset: 0x000012AC
		public static void LOL()
		{
			DateTime now = DateTime.Now;
			TimeSpan timeSpan = TimeSpan.FromSeconds(30.0);
			while (DateTime.Now - now < timeSpan)
			{
				IntPtr dc = GDI.GetDC(IntPtr.Zero);
				int systemMetrics = GDI.GetSystemMetrics(0);
				int systemMetrics2 = GDI.GetSystemMetrics(1);
				int num = GDI.rand.Next(-5, 5);
				int num2 = GDI.rand.Next(-5, 5);
				GDI.BitBlt(dc, num, num2, systemMetrics, systemMetrics2, dc, 0, 0, 13369376U);
				Thread.Sleep(5);
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00003140 File Offset: 0x00001340
		public static void zoomer()
		{
			DateTime now = DateTime.Now;
			TimeSpan timeSpan = TimeSpan.FromSeconds(30.0);
			while (DateTime.Now - now < timeSpan)
			{
				IntPtr dc = GDI.GetDC(IntPtr.Zero);
				int width = Screen.PrimaryScreen.Bounds.Width;
				int height = Screen.PrimaryScreen.Bounds.Height;
				GDI.BitBlt(dc, 0, height / 2, width, height / 2, dc, 0, 0, 13369376U);
				Thread.Sleep(1000);
				GDI.BitBlt(dc, 0, 0, width, height / 2, dc, 0, height / 2, 13369376U);
				Thread.Sleep(1000);
				GDI.BitBlt(dc, 0, 0, width, height / 4, dc, 0, height / 4, 13369376U);
				Thread.Sleep(1000);
				GDI.BitBlt(dc, 0, 0, width, height / 6, dc, 0, height / 6, 13369376U);
				Thread.Sleep(1000);
				GDI.DeleteDC(dc);
			}
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00003250 File Offset: 0x00001450
		public static void DrawLOLZ()
		{
			DateTime now = DateTime.Now;
			TimeSpan timeSpan = TimeSpan.FromSeconds(20.0);
			while (DateTime.Now - now < timeSpan)
			{
				IntPtr dc = GDI.GetDC(IntPtr.Zero);
				int systemMetrics = GDI.GetSystemMetrics(0);
				int systemMetrics2 = GDI.GetSystemMetrics(1);
				int num = GDI.rand.Next(-5, 5);
				int num2 = GDI.rand.Next(-5, 5);
				GDI.BitBlt(dc, num, num2, systemMetrics, systemMetrics2, dc, 0, 0, 13369376U);
				Thread.Sleep(5);
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000032E4 File Offset: 0x000014E4
		public static void SHAKE()
		{
			DateTime now = DateTime.Now;
			TimeSpan timeSpan = TimeSpan.FromSeconds(20.0);
			while (DateTime.Now - now < timeSpan)
			{
				IntPtr dc = GDI.GetDC(IntPtr.Zero);
				int systemMetrics = GDI.GetSystemMetrics(0);
				int systemMetrics2 = GDI.GetSystemMetrics(1);
				int num = GDI.rand.Next(-10, 10);
				int num2 = GDI.rand.Next(-10, 10);
				GDI.BitBlt(dc, num, num2, systemMetrics, systemMetrics2, dc, 0, 0, 13369376U);
				Thread.Sleep(5);
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x0000337C File Offset: 0x0000157C
		public static void Inverter()
		{
			DateTime now = DateTime.Now;
			TimeSpan timeSpan = TimeSpan.FromSeconds(20.0);
			while (DateTime.Now - now < timeSpan)
			{
				IntPtr dc = GDI.GetDC(IntPtr.Zero);
				int systemMetrics = GDI.GetSystemMetrics(0);
				int systemMetrics2 = GDI.GetSystemMetrics(1);
				GDI.BitBlt(dc, 0, 0, systemMetrics, systemMetrics2, dc, 0, 0, 5570569U);
				Thread.Sleep(5);
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000033F0 File Offset: 0x000015F0
		public static void RAIN()
		{
			DateTime now = DateTime.Now;
			TimeSpan timeSpan = TimeSpan.FromSeconds(20.0);
			while (DateTime.Now - now < timeSpan)
			{
				IntPtr dc = GDI.GetDC(IntPtr.Zero);
				GDI.BITMAPINFO bitmapinfo = default(GDI.BITMAPINFO);
				bitmapinfo.bmiHeader.biSize = (uint)Marshal.SizeOf(typeof(GDI.BITMAPINFOHEADER));
				bitmapinfo.bmiHeader.biWidth = GDI.w;
				bitmapinfo.bmiHeader.biHeight = -GDI.hgt;
				bitmapinfo.bmiHeader.biPlanes = 1;
				bitmapinfo.bmiHeader.biBitCount = 32;
				bitmapinfo.bmiHeader.biCompression = 0U;
				IntPtr intPtr2;
				IntPtr intPtr = GDI.CreateDIBSection(dc, ref bitmapinfo, 0U, out intPtr2, IntPtr.Zero, 0U);
				IntPtr intPtr3 = GDI.CreateCompatibleDC(dc);
				GDI.SelectObject(intPtr3, intPtr);
				GDI.BitBlt(intPtr3, 0, 0, GDI.w, GDI.hgt, dc, 0, 0, 13369376U);
				int num = GDI.w * GDI.hgt;
				int[] array = new int[num];
				Marshal.Copy(intPtr2, array, 0, num);
				Random random = new Random();
				float num2 = 0.08f;
				while (DateTime.Now - now < timeSpan)
				{
					for (int i = 0; i < num; i++)
					{
						int num3 = array[i];
						byte b = (byte)(num3 & 255);
						byte b2 = (byte)((num3 >> 8) & 255);
						byte b3 = (byte)((num3 >> 16) & 255);
						GDI.HSL hsl = GDI.RGBtoHSL(b3, b2, b);
						hsl.h += num2;
						bool flag = hsl.h > 1f;
						if (flag)
						{
							hsl.h -= 1f;
						}
						GDI.HSLtoRGB(hsl, out b3, out b2, out b);
						array[i] = (int)b | ((int)b2 << 8) | ((int)b3 << 16);
					}
					Marshal.Copy(array, 0, intPtr2, num);
					GDI.BitBlt(dc, 0, 0, GDI.w, GDI.hgt, intPtr3, 0, 0, 13369376U);
					Thread.Sleep(1);
				}
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x0000361C File Offset: 0x0000181C
		public static void finalwarn()
		{
			DateTime now = DateTime.Now;
			TimeSpan timeSpan = TimeSpan.FromSeconds(32.0);
			while (DateTime.Now - now < timeSpan)
			{
				IntPtr dc = GDI.GetDC(IntPtr.Zero);
				int systemMetrics = GDI.GetSystemMetrics(0);
				int systemMetrics2 = GDI.GetSystemMetrics(1);
				double num = 0.0;
				double num2 = num;
				for (int i = 0; i < systemMetrics2; i++)
				{
					int num3 = (int)(Math.Tan(num2) * 1.0);
					GDI.BitBlt(dc, 0, i, systemMetrics, 1, dc, num3, i, 13369376U);
					num2 += 0.01;
				}
				num += 0.01;
				Thread.Sleep(1);
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000036F0 File Offset: 0x000018F0
		public static void Inverter2()
		{
			DateTime now = DateTime.Now;
			TimeSpan timeSpan = TimeSpan.FromSeconds(32.0);
			while (DateTime.Now - now < timeSpan)
			{
				IntPtr dc = GDI.GetDC(IntPtr.Zero);
				int systemMetrics = GDI.GetSystemMetrics(0);
				int systemMetrics2 = GDI.GetSystemMetrics(1);
				GDI.BitBlt(dc, 0, 0, systemMetrics, systemMetrics2, dc, 0, 0, 5570569U);
				Thread.Sleep(5);
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00003764 File Offset: 0x00001964
		public static void CUBE()
		{
			float num = 0f;
			float num2 = 0f;
			float num3 = 0f;
			PointF[] array = new PointF[8];
			DateTime now = DateTime.Now;
			TimeSpan timeSpan = TimeSpan.FromSeconds(20.0);
			while (DateTime.Now - now < timeSpan)
			{
				IntPtr dc = GDI.GetDC(IntPtr.Zero);
				int systemMetrics = GDI.GetSystemMetrics(0);
				int systemMetrics2 = GDI.GetSystemMetrics(1);
				using (Graphics graphics = Graphics.FromHdc(dc))
				{
					float num4 = 80f;
					float[,] array2 = new float[8, 3];
					array2[0, 0] = -num4;
					array2[0, 1] = -num4;
					array2[0, 2] = -num4;
					array2[1, 0] = num4;
					array2[1, 1] = -num4;
					array2[1, 2] = -num4;
					array2[2, 0] = num4;
					array2[2, 1] = num4;
					array2[2, 2] = -num4;
					array2[3, 0] = -num4;
					array2[3, 1] = num4;
					array2[3, 2] = -num4;
					array2[4, 0] = -num4;
					array2[4, 1] = -num4;
					array2[4, 2] = num4;
					array2[5, 0] = num4;
					array2[5, 1] = -num4;
					array2[5, 2] = num4;
					array2[6, 0] = num4;
					array2[6, 1] = num4;
					array2[6, 2] = num4;
					array2[7, 0] = -num4;
					array2[7, 1] = num4;
					array2[7, 2] = num4;
					float[,] array3 = array2;
					num += 0.03f;
					num2 += 0.02f;
					num3 += 0.04f;
					float num5 = (float)(Math.Sin((double)num) * 150.0);
					float num6 = (float)(Math.Cos((double)num2) * 150.0);
					for (int i = 0; i < 8; i++)
					{
						float num7 = array3[i, 0];
						float num8 = array3[i, 1];
						float num9 = array3[i, 2];
						float num10 = (float)((double)num8 * Math.Cos((double)num) - (double)num9 * Math.Sin((double)num));
						float num11 = (float)((double)num8 * Math.Sin((double)num) + (double)num9 * Math.Cos((double)num));
						float num12 = num7;
						float num13 = (float)((double)num12 * Math.Cos((double)num2) + (double)num11 * Math.Sin((double)num2));
						float num14 = num10;
						float num15 = (float)((double)(-(double)num12) * Math.Sin((double)num2) + (double)num11 * Math.Cos((double)num2));
						float num16 = (float)((double)num13 * Math.Cos((double)num3) - (double)num14 * Math.Sin((double)num3));
						float num17 = (float)((double)num13 * Math.Sin((double)num3) + (double)num14 * Math.Cos((double)num3));
						float num18 = num15;
						float num19 = 300f / (300f + num18);
						array[i] = new PointF((float)(systemMetrics / 2) + num16 * num19 + num5, (float)(systemMetrics2 / 2) + num17 * num19 + num6);
					}
					int[][] array4 = new int[12][];
					array4[0] = new int[] { 0, 1 };
					array4[1] = new int[] { 1, 2 };
					array4[2] = new int[] { 2, 3 };
					int num20 = 3;
					int[] array5 = new int[2];
					array5[0] = 3;
					array4[num20] = array5;
					array4[4] = new int[] { 4, 5 };
					array4[5] = new int[] { 5, 6 };
					array4[6] = new int[] { 6, 7 };
					array4[7] = new int[] { 7, 4 };
					array4[8] = new int[] { 0, 4 };
					array4[9] = new int[] { 1, 5 };
					array4[10] = new int[] { 2, 6 };
					array4[11] = new int[] { 3, 7 };
					int[][] array6 = array4;
					using (Pen pen = new Pen(Color.Cyan, 2f))
					{
						foreach (int[] array8 in array6)
						{
							graphics.DrawLine(pen, array[array8[0]], array[array8[1]]);
						}
					}
				}
				GDI.ReleaseDC(IntPtr.Zero, dc);
			}
		}

		// Token: 0x04000005 RID: 5
		public const uint SRCCOPY = 13369376U;

		// Token: 0x04000006 RID: 6
		public const uint SRCPAINT = 15597702U;

		// Token: 0x04000007 RID: 7
		public const uint SRCAND = 8913094U;

		// Token: 0x04000008 RID: 8
		public const uint SRCINVERT = 6684742U;

		// Token: 0x04000009 RID: 9
		public const uint SRCERASE = 4457256U;

		// Token: 0x0400000A RID: 10
		public const uint NOTSRCCOPY = 3342344U;

		// Token: 0x0400000B RID: 11
		public const uint NOTSRCERASE = 1114278U;

		// Token: 0x0400000C RID: 12
		public const uint MERGECOPY = 12583114U;

		// Token: 0x0400000D RID: 13
		public const uint MERGEPAINT = 12255782U;

		// Token: 0x0400000E RID: 14
		public const uint PATCOPY = 15728673U;

		// Token: 0x0400000F RID: 15
		public const uint PATPAINT = 16452105U;

		// Token: 0x04000010 RID: 16
		public const uint PATINVERT = 5898313U;

		// Token: 0x04000011 RID: 17
		public const uint DSTINVERT = 5570569U;

		// Token: 0x04000012 RID: 18
		public const uint BLACKNESS = 66U;

		// Token: 0x04000013 RID: 19
		public const uint WHITENESS = 16711778U;

		// Token: 0x04000014 RID: 20
		public const uint CAPTUREBLT = 1073741824U;

		// Token: 0x04000015 RID: 21
		public const uint CUSTOM = 1051781U;

		// Token: 0x04000016 RID: 22
		private static readonly IntPtr NULL;

		// Token: 0x04000017 RID: 23
		private static Random rnd = new Random();

		// Token: 0x04000018 RID: 24
		private static IntPtr g_hdcScreen;

		// Token: 0x04000019 RID: 25
		private static IntPtr g_hdcMem;

		// Token: 0x0400001A RID: 26
		private static IntPtr g_hbmTemp;

		// Token: 0x0400001B RID: 27
		private static int g_w;

		// Token: 0x0400001C RID: 28
		private static int g_h;

		// Token: 0x0400001D RID: 29
		public static Random rand = new Random();

		// Token: 0x0400001E RID: 30
		private static int w = 1920;

		// Token: 0x0400001F RID: 31
		private static int hgt = 1080;

		// Token: 0x04000020 RID: 32
		private const int BI_RGB = 0;

		// Token: 0x0200000C RID: 12
		// (Invoke) Token: 0x06000069 RID: 105
		public delegate bool EnumWindowsProc(IntPtr hwnd, IntPtr lParam);

		// Token: 0x0200000D RID: 13
		public struct BLENDFUNCTION
		{
			// Token: 0x0600006C RID: 108 RVA: 0x00004050 File Offset: 0x00002250
			public BLENDFUNCTION(byte op, byte flags, byte alpha, byte format)
			{
				this.BlendOp = op;
				this.BlendFlags = flags;
				this.SourceConstantAlpha = alpha;
				this.AlphaFormat = format;
			}

			// Token: 0x0400004A RID: 74
			private byte BlendOp;

			// Token: 0x0400004B RID: 75
			private byte BlendFlags;

			// Token: 0x0400004C RID: 76
			private byte SourceConstantAlpha;

			// Token: 0x0400004D RID: 77
			private byte AlphaFormat;
		}

		// Token: 0x0200000E RID: 14
		public struct POINT
		{
			// Token: 0x0600006D RID: 109 RVA: 0x00004070 File Offset: 0x00002270
			public POINT(int x, int y)
			{
				this.X = x;
				this.Y = y;
			}

			// Token: 0x0600006E RID: 110 RVA: 0x00004084 File Offset: 0x00002284
			public static implicit operator Point(GDI.POINT p)
			{
				return new Point(p.X, p.Y);
			}

			// Token: 0x0600006F RID: 111 RVA: 0x000040A8 File Offset: 0x000022A8
			public static implicit operator GDI.POINT(Point p)
			{
				return new GDI.POINT(p.X, p.Y);
			}

			// Token: 0x0400004E RID: 78
			public int X;

			// Token: 0x0400004F RID: 79
			public int Y;
		}

		// Token: 0x0200000F RID: 15
		public struct BITMAPINFOHEADER
		{
			// Token: 0x04000050 RID: 80
			public uint biSize;

			// Token: 0x04000051 RID: 81
			public int biWidth;

			// Token: 0x04000052 RID: 82
			public int biHeight;

			// Token: 0x04000053 RID: 83
			public ushort biPlanes;

			// Token: 0x04000054 RID: 84
			public ushort biBitCount;

			// Token: 0x04000055 RID: 85
			public uint biCompression;

			// Token: 0x04000056 RID: 86
			public uint biSizeImage;

			// Token: 0x04000057 RID: 87
			public int biXPelsPerMeter;

			// Token: 0x04000058 RID: 88
			public int biYPelsPerMeter;

			// Token: 0x04000059 RID: 89
			public uint biClrUsed;

			// Token: 0x0400005A RID: 90
			public uint biClrImportant;
		}

		// Token: 0x02000010 RID: 16
		public struct BITMAPINFO
		{
			// Token: 0x0400005B RID: 91
			public GDI.BITMAPINFOHEADER bmiHeader;

			// Token: 0x0400005C RID: 92
			public uint bmiColors;
		}

		// Token: 0x02000011 RID: 17
		public struct HSL
		{
			// Token: 0x0400005D RID: 93
			public float h;

			// Token: 0x0400005E RID: 94
			public float s;

			// Token: 0x0400005F RID: 95
			public float l;
		}
	}
}
