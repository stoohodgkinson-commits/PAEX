using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using MS.WindowsAPICodePack.Internal;

namespace Microsoft.WindowsAPICodePack.Shell
{
	// Token: 0x0200000D RID: 13
	[Guid("43826D1E-E718-42EE-BC55-A1E261C37BFE")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IShellItem
	{
		// Token: 0x06000028 RID: 40
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		HResult BindToHandler([In] IntPtr pbc, [In] ref Guid bhid, [In] ref Guid riid, [MarshalAs(UnmanagedType.Interface)] out IntPtr ppv);

		// Token: 0x06000029 RID: 41
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void GetParent([MarshalAs(UnmanagedType.Interface)] out IShellItem ppsi);

		// Token: 0x0600002A RID: 42
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		HResult GetDisplayName([In] ShellNativeMethods.ShellItemDesignNameOptions sigdnName, out IntPtr ppszName);

		// Token: 0x0600002B RID: 43
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void GetAttributes([In] ShellNativeMethods.ShellFileGetAttributesOptions sfgaoMask, out ShellNativeMethods.ShellFileGetAttributesOptions psfgaoAttribs);

		// Token: 0x0600002C RID: 44
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		HResult Compare([MarshalAs(UnmanagedType.Interface)] [In] IShellItem psi, [In] SICHINTF hint, out int piOrder);
	}
}
