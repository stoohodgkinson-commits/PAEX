using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Interop.Excel
{
	// Token: 0x020000C3 RID: 195
	[CompilerGenerated]
	[DefaultMember("_Default")]
	[Guid("000208D5-0000-0000-C000-000000000046")]
	[TypeIdentifier]
	[ComImport]
	public interface _Application
	{
		// Token: 0x060002EA RID: 746
		void _VtblGap1_10();

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060002EB RID: 747
		[DispId(759)]
		Window ActiveWindow
		{
			[DispId(759)]
			[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[return: MarshalAs(UnmanagedType.Interface)]
			get;
		}

		// Token: 0x060002EC RID: 748
		void _VtblGap2_26();

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060002ED RID: 749
		[DispId(147)]
		object Selection
		{
			[DispId(147)]
			[LCIDConversion(0)]
			[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[return: MarshalAs(UnmanagedType.IDispatch)]
			get;
		}

		// Token: 0x060002EE RID: 750
		void _VtblGap3_7();

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060002EF RID: 751
		[DispId(572)]
		Workbooks Workbooks
		{
			[DispId(572)]
			[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[return: MarshalAs(UnmanagedType.Interface)]
			get;
		}

		// Token: 0x060002F0 RID: 752
		void _VtblGap4_60();

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060002F1 RID: 753
		[DispId(0)]
		[IndexerName("_Default")]
		string _Default
		{
			[DispId(0)]
			[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[return: MarshalAs(UnmanagedType.BStr)]
			get;
		}

		// Token: 0x060002F2 RID: 754
		void _VtblGap5_5();

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060002F3 RID: 755
		// (set) Token: 0x060002F4 RID: 756
		[DispId(343)]
		bool DisplayAlerts
		{
			[DispId(343)]
			[LCIDConversion(0)]
			[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			get;
			[LCIDConversion(0)]
			[DispId(343)]
			[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[param: In]
			set;
		}

		// Token: 0x060002F5 RID: 757
		void _VtblGap6_109();

		// Token: 0x060002F6 RID: 758
		[DispId(302)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void Quit();

		// Token: 0x060002F7 RID: 759
		void _VtblGap7_51();

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060002F8 RID: 760
		// (set) Token: 0x060002F9 RID: 761
		[DispId(558)]
		bool Visible
		{
			[LCIDConversion(0)]
			[DispId(558)]
			[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			get;
			[LCIDConversion(0)]
			[DispId(558)]
			[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[param: In]
			set;
		}
	}
}
