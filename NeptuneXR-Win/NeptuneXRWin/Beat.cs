using System;
using System.IO;
using System.Media;

namespace NeptuneXRWin
{
	// Token: 0x02000002 RID: 2
	internal class Beat
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public static void player()
		{
			Func<double, double>[] array = new Func<double, double>[5];
			array[0] = (double t) => (double)(((int)t * (((int)t * 4096 != 0) ? (((int)t * 65536 < 59392) ? 7 : ((int)t >> 6)) : 16) + (1 & ((int)t >> 14)) >> (3 & (-(int)t >> (((((int)t & 2048) != 0) ? 2 : 10) & 31)) & 31)) | ((int)t >> (((((int)t & 16384) != 0) ? ((((int)t & 4096) != 0) ? 4 : 3) : 2) & 31)));
			array[1] = (double t) => (double)((1 - (int)t % 5 % 10 + (((int)t / 5) ^ 9)) * (int)t);
			array[2] = (double t) => (double)((1 + (int)t % 3 % 8 + (((int)t / 3) ^ 3)) * (int)t >> 12);
			array[3] = (double t) => t / t + 60000.0 % (t % 1500.0) / 2.0;
			array[4] = (double t) => t * 7.0 / t / t * 6.0 * 1.0 - 4.0 * (t * t / 13.0 % t / 50.0);
			Func<double, double>[] array2 = array;
			int[] array3 = new int[] { 25, 27, 30, 20, 32 };
			int[] array4 = new int[] { 8000, 22050, 22050, 8000, 8000 };
			for (int i = 0; i < array2.Length + 1; i++)
			{
				Func<double, double> func = array2[i];
				int num = array3[i];
				int num2 = array4[i];
				int num3 = num2 * num;
				byte[] array5 = new byte[num3];
				for (int j = 0; j < num3; j++)
				{
					array5[j] = (byte)((int)func((double)j) & 255);
				}
				using (MemoryStream memoryStream = new MemoryStream())
				{
					using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
					{
						binaryWriter.Write(new byte[] { 82, 73, 70, 70 });
						binaryWriter.Write(36 + array5.Length);
						binaryWriter.Write(new byte[] { 87, 65, 86, 69 });
						binaryWriter.Write(new byte[] { 102, 109, 116, 32 });
						binaryWriter.Write(16);
						binaryWriter.Write(1);
						binaryWriter.Write(1);
						binaryWriter.Write(num2);
						binaryWriter.Write(num2 * 8 / 8);
						binaryWriter.Write(1);
						binaryWriter.Write(8);
						binaryWriter.Write(new byte[] { 100, 97, 116, 97 });
						binaryWriter.Write(array5.Length);
						binaryWriter.Write(array5);
						memoryStream.Position = 0L;
						using (SoundPlayer soundPlayer = new SoundPlayer(memoryStream))
						{
							soundPlayer.PlaySync();
						}
					}
				}
			}
		}
	}
}
