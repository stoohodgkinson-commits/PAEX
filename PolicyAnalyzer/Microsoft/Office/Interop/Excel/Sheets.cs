using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Interop.Excel
{
	// Token: 0x020000A9 RID: 169
	[CompilerGenerated]
	[Guid("000208D7-0000-0000-C000-000000000046")]
	[TypeIdentifier]
	[ComImport]
	public interface Sheets : IEnumerable
	{
		// Token: 0x060002C4 RID: 708
		void _VtblGap1_5();

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060002C5 RID: 709
		[DispId(118)]
		int Count
		{
			[DispId(118)]
			[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			get;
		}

		// Token: 0x060002C6 RID: 710
		void _VtblGap2_12();

		// Token: 0x17000032 RID: 50
		[DispId(0)]
		[IndexerName("_Default")]
		object this[[MarshalAs(UnmanagedType.Struct)] [In] object Index]
		{
			[DispId(0)]
			[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[return: MarshalAs(UnmanagedType.IDispatch)]
			get;
		}
	}
}
