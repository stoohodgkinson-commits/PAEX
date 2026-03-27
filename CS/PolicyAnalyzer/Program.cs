using System;
using System.Windows.Forms;

namespace PolicyAnalyzer
{
	// Token: 0x02000092 RID: 146
	internal static class Program
	{
		// Token: 0x0600026C RID: 620 RVA: 0x00003AC4 File Offset: 0x00001CC4
		[STAThread]
		private static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Globals.LoadConfig();
			Application.Run(new PolicyAnalyzerMain2());
			Globals.SaveConfig();
		}
	}
}
