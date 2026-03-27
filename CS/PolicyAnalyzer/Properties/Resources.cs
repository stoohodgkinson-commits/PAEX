using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace PolicyAnalyzer.Properties
{
	// Token: 0x02000093 RID: 147
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class Resources
	{
		// Token: 0x0600026D RID: 621 RVA: 0x00002A68 File Offset: 0x00000C68
		internal Resources()
		{
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600026E RID: 622 RVA: 0x00003AE5 File Offset: 0x00001CE5
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (Resources.resourceMan == null)
				{
					Resources.resourceMan = new ResourceManager("PolicyAnalyzer.Properties.Resources", typeof(Resources).Assembly);
				}
				return Resources.resourceMan;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600026F RID: 623 RVA: 0x00003B11 File Offset: 0x00001D11
		// (set) Token: 0x06000270 RID: 624 RVA: 0x00003B18 File Offset: 0x00001D18
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return Resources.resourceCulture;
			}
			set
			{
				Resources.resourceCulture = value;
			}
		}

		// Token: 0x04000807 RID: 2055
		private static ResourceManager resourceMan;

		// Token: 0x04000808 RID: 2056
		private static CultureInfo resourceCulture;
	}
}
