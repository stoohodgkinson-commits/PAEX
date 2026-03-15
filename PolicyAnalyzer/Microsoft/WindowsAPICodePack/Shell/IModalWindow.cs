using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.WindowsAPICodePack.Shell
{
	// Token: 0x0200000E RID: 14
	[Guid("B4DB1657-70D7-485E-8E3E-6FCB5A5C1802")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IModalWindow
	{
		// Token: 0x0600002D RID: 45
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int Show([In] IntPtr parent);
	}
}
