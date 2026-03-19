using System;
using System.Windows.Forms;

namespace NeptuneXRWin
{
	// Token: 0x02000007 RID: 7
	internal static class Program
	{
		// Token: 0x06000059 RID: 89 RVA: 0x00003E53 File Offset: 0x00002053
		[STAThread]
		private static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Form1());
		}
	}
}
