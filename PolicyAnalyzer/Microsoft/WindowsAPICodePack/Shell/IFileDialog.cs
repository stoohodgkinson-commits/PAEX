using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.WindowsAPICodePack.Shell
{
	// Token: 0x0200000F RID: 15
	[Guid("42F85136-DB7E-439C-85F1-E4075D135FC8")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IFileDialog : IModalWindow
	{
		// Token: 0x0600002E RID: 46
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int Show([In] IntPtr parent);

		// Token: 0x0600002F RID: 47
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void SetFileTypes([In] uint cFileTypes, [MarshalAs(UnmanagedType.LPArray)] [In] FilterSpec[] rgFilterSpec);

		// Token: 0x06000030 RID: 48
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void SetFileTypeIndex([In] uint iFileType);

		// Token: 0x06000031 RID: 49
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void GetFileTypeIndex(out uint piFileType);

		// Token: 0x06000032 RID: 50
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void Advise([MarshalAs(UnmanagedType.Interface)] [In] IntPtr pfde, out uint pdwCookie);

		// Token: 0x06000033 RID: 51
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void Unadvise([In] uint dwCookie);

		// Token: 0x06000034 RID: 52
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void SetOptions([In] FileOpenOptions fos);

		// Token: 0x06000035 RID: 53
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void GetOptions(out FileOpenOptions pfos);

		// Token: 0x06000036 RID: 54
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void SetDefaultFolder([MarshalAs(UnmanagedType.Interface)] [In] IShellItem psi);

		// Token: 0x06000037 RID: 55
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void SetFolder([MarshalAs(UnmanagedType.Interface)] [In] IShellItem psi);

		// Token: 0x06000038 RID: 56
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void GetFolder([MarshalAs(UnmanagedType.Interface)] out IShellItem ppsi);

		// Token: 0x06000039 RID: 57
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void GetCurrentSelection([MarshalAs(UnmanagedType.Interface)] out IShellItem ppsi);

		// Token: 0x0600003A RID: 58
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void SetFileName([MarshalAs(UnmanagedType.LPWStr)] [In] string pszName);

		// Token: 0x0600003B RID: 59
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void GetFileName([MarshalAs(UnmanagedType.LPWStr)] out string pszName);

		// Token: 0x0600003C RID: 60
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void SetTitle([MarshalAs(UnmanagedType.LPWStr)] [In] string pszTitle);

		// Token: 0x0600003D RID: 61
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void SetOkButtonLabel([MarshalAs(UnmanagedType.LPWStr)] [In] string pszText);

		// Token: 0x0600003E RID: 62
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void SetFileNameLabel([MarshalAs(UnmanagedType.LPWStr)] [In] string pszLabel);

		// Token: 0x0600003F RID: 63
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void GetResult([MarshalAs(UnmanagedType.Interface)] out IShellItem ppsi);

		// Token: 0x06000040 RID: 64
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void AddPlace([MarshalAs(UnmanagedType.Interface)] [In] IShellItem psi, ShellNativeMethods.FileDialogAddPlacement fdap);

		// Token: 0x06000041 RID: 65
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void SetDefaultExtension([MarshalAs(UnmanagedType.LPWStr)] [In] string pszDefaultExtension);

		// Token: 0x06000042 RID: 66
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void Close([MarshalAs(UnmanagedType.Error)] int hr);

		// Token: 0x06000043 RID: 67
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void SetClientGuid([In] ref Guid guid);

		// Token: 0x06000044 RID: 68
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void ClearClientData();

		// Token: 0x06000045 RID: 69
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void SetFilter([MarshalAs(UnmanagedType.Interface)] IntPtr pFilter);
	}
}
