using System;
using System.Runtime.InteropServices;
using Microsoft.WindowsAPICodePack.Shell;

namespace Microsoft.WindowsAPICodePack.Dialogs
{
	// Token: 0x02000008 RID: 8
	[Guid("D57C7288-D4AD-4768-BE02-9D969532D960")]
	[CoClass(typeof(FileOpenDialogRCW))]
	[ComImport]
	internal interface NativeFileOpenDialog : IFileOpenDialog, IFileDialog, IModalWindow
	{
	}
}
