using System;
using System.CodeDom.Compiler;
using System.Configuration;
using System.Runtime.CompilerServices;

namespace PolicyAnalyzer.Properties
{
	// Token: 0x02000094 RID: 148
	[CompilerGenerated]
	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "14.0.0.0")]
	internal sealed partial class Settings : ApplicationSettingsBase
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000271 RID: 625 RVA: 0x00003B20 File Offset: 0x00001D20
		public static Settings Default
		{
			get
			{
				return Settings.defaultInstance;
			}
		}

		// Token: 0x04000809 RID: 2057
		private static Settings defaultInstance = (Settings)SettingsBase.Synchronized(new Settings());
	}
}
