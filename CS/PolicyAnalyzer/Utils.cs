using System;
using System.Diagnostics;

namespace PolicyAnalyzer
{
	// Token: 0x02000068 RID: 104
	internal class Utils
	{
		// Token: 0x0600017F RID: 383 RVA: 0x000033B9 File Offset: 0x000015B9
		public static int AppMajorVersion()
		{
			return Process.GetCurrentProcess().MainModule.FileVersionInfo.FileMajorPart;
		}

		// Token: 0x06000180 RID: 384 RVA: 0x000033CF File Offset: 0x000015CF
		public static int AppMinorVersion()
		{
			return Process.GetCurrentProcess().MainModule.FileVersionInfo.FileMinorPart;
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00010818 File Offset: 0x0000EA18
		public static string AppVersionString()
		{
			return Utils.AppMajorVersion().ToString() + "." + Utils.AppMinorVersion().ToString();
		}

		// Token: 0x06000182 RID: 386 RVA: 0x0001084C File Offset: 0x0000EA4C
		public static string AppVersionFullString()
		{
			FileVersionInfo fileVersionInfo = Process.GetCurrentProcess().MainModule.FileVersionInfo;
			return string.Concat(new string[]
			{
				fileVersionInfo.FileMajorPart.ToString(),
				".",
				fileVersionInfo.FileMinorPart.ToString(),
				".",
				fileVersionInfo.FileBuildPart.ToString(),
				".",
				fileVersionInfo.FilePrivatePart.ToString()
			});
		}
	}
}
