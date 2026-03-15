using System;

namespace Microsoft.WindowsAPICodePack.Shell
{
	// Token: 0x0200002A RID: 42
	[Flags]
	public enum AccessModes
	{
		// Token: 0x040000DF RID: 223
		Direct = 0,
		// Token: 0x040000E0 RID: 224
		Transacted = 65536,
		// Token: 0x040000E1 RID: 225
		Simple = 134217728,
		// Token: 0x040000E2 RID: 226
		Read = 0,
		// Token: 0x040000E3 RID: 227
		Write = 1,
		// Token: 0x040000E4 RID: 228
		ReadWrite = 2,
		// Token: 0x040000E5 RID: 229
		ShareDenyNone = 64,
		// Token: 0x040000E6 RID: 230
		ShareDenyRead = 48,
		// Token: 0x040000E7 RID: 231
		ShareDenyWrite = 32,
		// Token: 0x040000E8 RID: 232
		ShareExclusive = 16,
		// Token: 0x040000E9 RID: 233
		Priority = 262144,
		// Token: 0x040000EA RID: 234
		DeleteOnRelease = 67108864,
		// Token: 0x040000EB RID: 235
		NoScratch = 1048576,
		// Token: 0x040000EC RID: 236
		Create = 4096,
		// Token: 0x040000ED RID: 237
		Convert = 131072,
		// Token: 0x040000EE RID: 238
		FailIfThere = 0,
		// Token: 0x040000EF RID: 239
		NoSnapshot = 2097152,
		// Token: 0x040000F0 RID: 240
		DirectSingleWriterMultipleReader = 4194304
	}
}
