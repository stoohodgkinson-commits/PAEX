using System;

namespace Microsoft.WindowsAPICodePack.Shell
{
	// Token: 0x02000010 RID: 16
	[Flags]
	internal enum FileOpenOptions
	{
		// Token: 0x04000021 RID: 33
		OverwritePrompt = 2,
		// Token: 0x04000022 RID: 34
		StrictFileTypes = 4,
		// Token: 0x04000023 RID: 35
		NoChangeDirectory = 8,
		// Token: 0x04000024 RID: 36
		PickFolders = 32,
		// Token: 0x04000025 RID: 37
		ForceFilesystem = 64,
		// Token: 0x04000026 RID: 38
		AllNonStorageItems = 128,
		// Token: 0x04000027 RID: 39
		NoValidate = 256,
		// Token: 0x04000028 RID: 40
		AllowMultiSelect = 512,
		// Token: 0x04000029 RID: 41
		PathMustExist = 2048,
		// Token: 0x0400002A RID: 42
		FileMustExist = 4096,
		// Token: 0x0400002B RID: 43
		CreatePrompt = 8192,
		// Token: 0x0400002C RID: 44
		ShareAware = 16384,
		// Token: 0x0400002D RID: 45
		NoReadOnlyReturn = 32768,
		// Token: 0x0400002E RID: 46
		NoTestFileCreate = 65536,
		// Token: 0x0400002F RID: 47
		HideMruPlaces = 131072,
		// Token: 0x04000030 RID: 48
		HidePinnedPlaces = 262144,
		// Token: 0x04000031 RID: 49
		NoDereferenceLinks = 1048576,
		// Token: 0x04000032 RID: 50
		DontAddToRecent = 33554432,
		// Token: 0x04000033 RID: 51
		ForceShowHidden = 268435456,
		// Token: 0x04000034 RID: 52
		DefaultNoMiniMode = 536870912
	}
}
