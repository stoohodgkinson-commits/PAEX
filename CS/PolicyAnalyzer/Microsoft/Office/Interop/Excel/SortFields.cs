using System;
using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Interop.Excel
{
	// Token: 0x020000AC RID: 172
	[CompilerGenerated]
	[Guid("000244AA-0000-0000-C000-000000000046")]
	[DefaultMember("_Default")]
	[InterfaceType(2)]
	[TypeIdentifier]
	[ComImport]
	public interface SortFields : IEnumerable
	{
		// Token: 0x060002D3 RID: 723
		void _VtblGap1_3();

		// Token: 0x060002D4 RID: 724
		[DispId(181)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[return: MarshalAs(UnmanagedType.Interface)]
		SortField Add([MarshalAs(UnmanagedType.Interface)] [In] Range Key, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] object SortOn, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] object Order, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] object CustomOrder, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] object DataOption);

		// Token: 0x060002D5 RID: 725
		void _VtblGap2_2();

		// Token: 0x060002D6 RID: 726
		[DispId(111)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void Clear();
	}
}
