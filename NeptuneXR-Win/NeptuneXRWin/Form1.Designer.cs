namespace NeptuneXRWin
{
	// Token: 0x02000004 RID: 4
	public partial class Form1 : global::System.Windows.Forms.Form
	{
		// Token: 0x0600000B RID: 11 RVA: 0x00002558 File Offset: 0x00000758
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002590 File Offset: 0x00000790
		private void InitializeComponent()
		{
			this.button1 = new global::System.Windows.Forms.Button();
			this.button2 = new global::System.Windows.Forms.Button();
			this.label1 = new global::System.Windows.Forms.Label();
			base.SuspendLayout();
			this.button1.BackColor = global::System.Drawing.Color.Black;
			this.button1.ForeColor = global::System.Drawing.Color.Red;
			this.button1.Location = new global::System.Drawing.Point(12, 70);
			this.button1.Name = "button1";
			this.button1.Size = new global::System.Drawing.Size(336, 54);
			this.button1.TabIndex = 0;
			this.button1.Text = "RUN";
			this.button1.UseVisualStyleBackColor = false;
			this.button1.Click += new global::System.EventHandler(this.button1_Click);
			this.button2.BackColor = global::System.Drawing.Color.Black;
			this.button2.ForeColor = global::System.Drawing.Color.Lime;
			this.button2.Location = new global::System.Drawing.Point(382, 70);
			this.button2.Name = "button2";
			this.button2.Size = new global::System.Drawing.Size(338, 54);
			this.button2.TabIndex = 1;
			this.button2.Text = "EXIT";
			this.button2.UseVisualStyleBackColor = false;
			this.button2.Click += new global::System.EventHandler(this.button2_Click);
			this.label1.AutoSize = true;
			this.label1.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 16f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label1.ForeColor = global::System.Drawing.Color.White;
			this.label1.Location = new global::System.Drawing.Point(47, 9);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(629, 37);
			this.label1.TabIndex = 2;
			this.label1.Text = "are you sure you want to run this malware?";
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(9f, 20f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = global::System.Drawing.Color.Black;
			base.ClientSize = new global::System.Drawing.Size(732, 140);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.button2);
			base.Controls.Add(this.button1);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "Form1";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			this.Text = "N.E.P.T.U.N.E.W.I.N.N.E.R";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000001 RID: 1
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x04000002 RID: 2
		private global::System.Windows.Forms.Button button1;

		// Token: 0x04000003 RID: 3
		private global::System.Windows.Forms.Button button2;

		// Token: 0x04000004 RID: 4
		private global::System.Windows.Forms.Label label1;
	}
}
