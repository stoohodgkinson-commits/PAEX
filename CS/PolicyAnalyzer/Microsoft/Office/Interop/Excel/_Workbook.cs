using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Interop.Excel
{
	// Token: 0x020000C5 RID: 197
	[CompilerGenerated]
	[Guid("000208DA-0000-0000-C000-000000000046")]
	[TypeIdentifier]
	[ComImport]
	public interface _Workbook
	{
		// Token: 0x06000335 RID: 821
		void _VtblGap1_20();

		// Token: 0x06000336 RID: 822
		[LCIDConversion(3)]
		[DispId(277)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void Close([MarshalAs(UnmanagedType.Struct)] [In] [Optional] object SaveChanges, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] object Filename, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] object RouteWorkbook);

		// Token: 0x06000337 RID: 823
		void _VtblGap2_103();

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000338 RID: 824
		[DispId(494)]
		Sheets Worksheets
		{
			[DispId(494)]
			[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[return: MarshalAs(UnmanagedType.Interface)]
			get;
		}

		// Token: 0x06000339 RID: 825
		void _VtblGap3_40();

		// Token: 0x0600033A RID: 826
		[DispId(1925)]
		[LCIDConversion(12)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void SaveAs([MarshalAs(UnmanagedType.Struct)] [In] [Optional] object Filename, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] object FileFormat, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] object Password, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] object WriteResPassword, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] object ReadOnlyRecommended, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] object CreateBackup, [In] XlSaveAsAccessMode AccessMode = XlSaveAsAccessMode.xlNoChange, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] object ConflictResolution, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] object AddToMru, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] object TextCodepage, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] object TextVisualLayout, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] object Local);

		// Token: 0x0600033B RID: 827
		void _VtblGap4_41();

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600033C RID: 828
		[DispId(2513)]
		Connections Connections
		{
			[DispId(2513)]
			[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[return: MarshalAs(UnmanagedType.Interface)]
			get;
		}
	}
}
