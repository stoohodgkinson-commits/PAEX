using System;
using System.IO;
using Microsoft.Win32;

namespace PolicyAnalyzer
{
	// Token: 0x02000074 RID: 116
	internal class Globals
	{
		// Token: 0x060001AF RID: 431 RVA: 0x000121A4 File Offset: 0x000103A4
		private static bool RegvalToBool(object obj, bool defValue)
		{
			if (obj == null)
			{
				return defValue;
			}
			if (obj is string)
			{
				string text = ((string)obj).ToLower();
				return text == "true" || text == "yes" || (!(text == "false") && !(text == "no") && defValue);
			}
			if (obj is int)
			{
				return (int)obj != 0;
			}
			return defValue;
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x0000355D File Offset: 0x0000175D
		private static int BoolToRegval(bool val)
		{
			if (!val)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0001221C File Offset: 0x0001041C
		public static void SaveConfig()
		{
			Globals.ConsistencyCheck();
			RegistryKey configKey = Globals.GetConfigKey(true);
			if (configKey != null)
			{
				configKey.SetValue("ADMX Path", Globals.sAdmxPath);
				configKey.SetValue("Last GPO Folder", Globals.sLastGpoFolder);
				configKey.SetValue("Last Policy Rules Folder", Globals.sLastPolicyRulesFolder);
				configKey.SetValue("Show Details Pane", Globals.BoolToRegval(Globals.bLastShowDetailsPane), RegistryValueKind.DWord);
				configKey.SetValue("Show GPOs in Details", Globals.BoolToRegval(Globals.bLastShowGPOsInDetails), RegistryValueKind.DWord);
				configKey.SetValue("Show GPOs and Files in Details", Globals.BoolToRegval(Globals.bLastShowGPOsAndFilesInDetails), RegistryValueKind.DWord);
				configKey.SetValue("Show Explain Text", Globals.BoolToRegval(Globals.bLastShowExplainText), RegistryValueKind.DWord);
				configKey.Close();
			}
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x000122E0 File Offset: 0x000104E0
		public static void LoadConfig()
		{
			Globals.sAdmxPath = Path.Combine(Environment.GetEnvironmentVariable("windir"), "PolicyDefinitions");
			Globals.sLastGpoFolder = string.Empty;
			Globals.sLastPolicyRulesFolder = PolicyAnalyzerDataFolder.DefaultFolderPath();
			Globals.bLastShowDetailsPane = true;
			Globals.bLastShowGPOsInDetails = true;
			Globals.bLastShowGPOsAndFilesInDetails = false;
			Globals.bLastShowExplainText = true;
			RegistryKey configKey = Globals.GetConfigKey(false);
			if (configKey != null)
			{
				Globals.sAdmxPath = (string)configKey.GetValue("ADMX Path", Globals.sAdmxPath);
				Globals.sLastGpoFolder = (string)configKey.GetValue("Last GPO Folder", Globals.sLastGpoFolder);
				Globals.sLastPolicyRulesFolder = (string)configKey.GetValue("Last Policy Rules Folder", Globals.sLastPolicyRulesFolder);
				Globals.bLastShowDetailsPane = Globals.RegvalToBool(configKey.GetValue("Show Details Pane"), Globals.bLastShowDetailsPane);
				Globals.bLastShowGPOsInDetails = Globals.RegvalToBool(configKey.GetValue("Show GPOs in Details"), Globals.bLastShowGPOsInDetails);
				Globals.bLastShowGPOsAndFilesInDetails = Globals.RegvalToBool(configKey.GetValue("Show GPOs and Files in Details"), Globals.bLastShowGPOsAndFilesInDetails);
				Globals.bLastShowExplainText = Globals.RegvalToBool(configKey.GetValue("Show Explain Text"), Globals.bLastShowExplainText);
				configKey.Close();
			}
			Globals.ConsistencyCheck();
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00003565 File Offset: 0x00001765
		private static void ConsistencyCheck()
		{
			if (Globals.bLastShowGPOsAndFilesInDetails)
			{
				Globals.bLastShowGPOsInDetails = true;
			}
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00003574 File Offset: 0x00001774
		private static RegistryKey GetConfigKey(bool bWritable)
		{
			if (!bWritable)
			{
				return Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\PolicyAnalyzer", false);
			}
			return Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\PolicyAnalyzer");
		}

		// Token: 0x04000759 RID: 1881
		public static string sAdmxPath = string.Empty;

		// Token: 0x0400075A RID: 1882
		public static string sLastGpoFolder = string.Empty;

		// Token: 0x0400075B RID: 1883
		public static string sLastPolicyRulesFolder = string.Empty;

		// Token: 0x0400075C RID: 1884
		public static bool bLastShowDetailsPane = true;

		// Token: 0x0400075D RID: 1885
		public static bool bLastShowGPOsInDetails = true;

		// Token: 0x0400075E RID: 1886
		public static bool bLastShowGPOsAndFilesInDetails = false;

		// Token: 0x0400075F RID: 1887
		public static bool bLastShowExplainText = true;

		// Token: 0x04000760 RID: 1888
		private const string ConfigKey = "Software\\Microsoft\\PolicyAnalyzer";

		// Token: 0x04000761 RID: 1889
		private const string ValAdmxPath = "ADMX Path";

		// Token: 0x04000762 RID: 1890
		private const string ValLastGpoFolder = "Last GPO Folder";

		// Token: 0x04000763 RID: 1891
		private const string ValLastPolicyRulesFolder = "Last Policy Rules Folder";

		// Token: 0x04000764 RID: 1892
		private const string ValShowDetailsPane = "Show Details Pane";

		// Token: 0x04000765 RID: 1893
		private const string ValShowGPOsInDetails = "Show GPOs in Details";

		// Token: 0x04000766 RID: 1894
		private const string ValShowGPOsAndFilesInDetails = "Show GPOs and Files in Details";

		// Token: 0x04000767 RID: 1895
		private const string ValShowExplainText = "Show Explain Text";
	}
}
