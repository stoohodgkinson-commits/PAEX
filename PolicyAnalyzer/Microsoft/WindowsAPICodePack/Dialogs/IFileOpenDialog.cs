using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.WindowsAPICodePack.Shell;

namespace Microsoft.WindowsAPICodePack.Dialogs
{
	// Token: 0x02000006 RID: 6
	[Guid("D57C7288-D4AD-4768-BE02-9D969532D960")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IFileOpenDialog : IFileDialog, IModalWindow
	{
		// Token: 0x0600000D RID: 13
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int Show([In] IntPtr parent);

		// Token: 0x0600000E RID: 14
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void SetFileTypes([In] uint cFileTypes, [In] ref ShellNativeMethods.FilterSpec rgFilterSpec);

		// Token: 0x0600000F RID: 15
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void SetFileTypeIndex([In] uint iFileType);

		// Token: 0x06000010 RID: 16
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void GetFileTypeIndex(out uint piFileType);

		// Token: 0x06000011 RID: 17
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void Advise([MarshalAs(UnmanagedType.Interface)] [In] IntPtr pfde, out uint pdwCookie);

		// Token: 0x06000012 RID: 18
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void Unadvise([In] uint dwCookie);

		// Token: 0x06000013 RID: 19
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void SetOptions([In] ShellNativeMethods.FileOpenOptions fos);

		// Token: 0x06000014 RID: 20
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void GetOptions(out ShellNativeMethods.FileOpenOptions pfos);

		// Token: 0x06000015 RID: 21
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void SetDefaultFolder([MarshalAs(UnmanagedType.Interface)] [In] IShellItem psi);

		// Token: 0x06000016 RID: 22
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void SetFolder([MarshalAs(UnmanagedType.Interface)] [In] IShellItem psi);

		// Token: 0x06000017 RID: 23
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void GetFolder([MarshalAs(UnmanagedType.Interface)] out IShellItem ppsi);

		// Token: 0x06000018 RID: 24
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void GetCurrentSelection([MarshalAs(UnmanagedType.Interface)] out IShellItem ppsi);

		// Token: 0x06000019 RID: 25
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void SetFileName([MarshalAs(UnmanagedType.LPWStr)] [In] string pszName);

		// Token: 0x0600001A RID: 26
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void GetFileName([MarshalAs(UnmanagedType.LPWStr)] out string pszName);

		// Token: 0x0600001B RID: 27
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void SetTitle([MarshalAs(UnmanagedType.LPWStr)] [In] string pszTitle);

		// Token: 0x0600001C RID: 28
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void SetOkButtonLabel([MarshalAs(UnmanagedType.LPWStr)] [In] string pszText);

		// Token: 0x0600001D RID: 29
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void SetFileNameLabel([MarshalAs(UnmanagedType.LPWStr)] [In] string pszLabel);

		// Token: 0x0600001E RID: 30
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void GetResult([MarshalAs(UnmanagedType.Interface)] out IShellItem ppsi);

		// Token: 0x0600001F RID: 31
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void AddPlace([MarshalAs(UnmanagedType.Interface)] [In] IShellItem psi, ShellNativeMethods.FileDialogAddPlacement fdap);

		// Token: 0x06000020 RID: 32
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void SetDefaultExtension([MarshalAs(UnmanagedType.LPWStr)] [In] string pszDefaultExtension);

		// Token: 0x06000021 RID: 33
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void Close([MarshalAs(UnmanagedType.Error)] int hr);

		// Token: 0x06000022 RID: 34
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void SetClientGuid([In] ref Guid guid);

		// Token: 0x06000023 RID: 35
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void ClearClientData();

		// Token: 0x06000024 RID: 36
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void SetFilter([MarshalAs(UnmanagedType.Interface)] IntPtr pFilter);

		// Token: 0x06000025 RID: 37
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void GetResults([MarshalAs(UnmanagedType.Interface)] out IntPtr ppenum);

		// Token: 0x06000026 RID: 38
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void GetSelectedItems([MarshalAs(UnmanagedType.Interface)] out IntPtr ppsai);
	}
}
