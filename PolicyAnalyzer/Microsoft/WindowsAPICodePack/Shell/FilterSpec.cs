using System;
using System.Runtime.InteropServices;

namespace Microsoft.WindowsAPICodePack.Shell
{
	// Token: 0x02000011 RID: 17
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
	internal struct FilterSpec
	{
		// Token: 0x06000046 RID: 70 RVA: 0x00002911 File Offset: 0x00000B11
		internal FilterSpec(string name, string spec)
		{
			this.Name = name;
			this.Spec = spec;
		}

		// Token: 0x04000035 RID: 53
		[MarshalAs(UnmanagedType.LPWStr)]
		internal string Name;

		// Token: 0x04000036 RID: 54
		[MarshalAs(UnmanagedType.LPWStr)]
		internal string Spec;
	}
}
