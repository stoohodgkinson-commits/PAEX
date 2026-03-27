using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Interop.Excel
{
	// Token: 0x020000C6 RID: 198
	[CompilerGenerated]
	[Guid("000208D8-0000-0000-C000-000000000046")]
	[TypeIdentifier]
	[ComImport]
	public interface _Worksheet
	{
		// Token: 0x0600033D RID: 829
		void _VtblGap1_5();

		// Token: 0x0600033E RID: 830
		[LCIDConversion(0)]
		[DispId(117)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void Delete();

		// Token: 0x0600033F RID: 831
		void _VtblGap2_5();

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000340 RID: 832
		// (set) Token: 0x06000341 RID: 833
		[DispId(110)]
		string Name
		{
			[DispId(110)]
			[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[return: MarshalAs(UnmanagedType.BStr)]
			get;
			[DispId(110)]
			[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[param: MarshalAs(UnmanagedType.BStr)]
			[param: In]
			set;
		}

		// Token: 0x06000342 RID: 834
		void _VtblGap3_32();

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000343 RID: 835
		[DispId(238)]
		Range Cells
		{
			[DispId(238)]
			[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[return: MarshalAs(UnmanagedType.Interface)]
			get;
		}

		// Token: 0x06000344 RID: 836
		void _VtblGap4_5();

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000345 RID: 837
		[DispId(241)]
		Range Columns
		{
			[DispId(241)]
			[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[return: MarshalAs(UnmanagedType.Interface)]
			get;
		}

		// Token: 0x06000346 RID: 838
		void _VtblGap5_41();

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000347 RID: 839
		[DispId(197)]
		Range Range
		{
			[DispId(197)]
			[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[return: MarshalAs(UnmanagedType.Interface)]
			get;
		}

		// Token: 0x06000348 RID: 840
		void _VtblGap6_1();

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000349 RID: 841
		[DispId(258)]
		Range Rows
		{
			[DispId(258)]
			[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[return: MarshalAs(UnmanagedType.Interface)]
			get;
		}

		// Token: 0x0600034A RID: 842
		void _VtblGap7_14();

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600034B RID: 843
		[DispId(412)]
		Range UsedRange
		{
			[DispId(412)]
			[LCIDConversion(0)]
			[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[return: MarshalAs(UnmanagedType.Interface)]
			get;
		}

		// Token: 0x0600034C RID: 844
		void _VtblGap8_2();

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600034D RID: 845
		[DispId(1434)]
		QueryTables QueryTables
		{
			[DispId(1434)]
			[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[return: MarshalAs(UnmanagedType.Interface)]
			get;
		}

		// Token: 0x0600034E RID: 846
		void _VtblGap9_8();

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600034F RID: 847
		[DispId(793)]
		AutoFilter AutoFilter
		{
			[DispId(793)]
			[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[return: MarshalAs(UnmanagedType.Interface)]
			get;
		}
	}
}
