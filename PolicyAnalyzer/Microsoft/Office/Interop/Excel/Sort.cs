using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Interop.Excel
{
	// Token: 0x020000AA RID: 170
	[CompilerGenerated]
	[InterfaceType(2)]
	[Guid("000244AB-0000-0000-C000-000000000046")]
	[TypeIdentifier]
	[ComImport]
	public interface Sort
	{
		// Token: 0x060002C8 RID: 712
		void _VtblGap1_4();

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060002C9 RID: 713
		// (set) Token: 0x060002CA RID: 714
		[DispId(895)]
		XlYesNoGuess Header
		{
			[DispId(895)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			get;
			[DispId(895)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			set;
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060002CB RID: 715
		// (set) Token: 0x060002CC RID: 716
		[DispId(426)]
		bool MatchCase
		{
			[DispId(426)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			get;
			[DispId(426)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			set;
		}

		// Token: 0x060002CD RID: 717
		void _VtblGap2_2();

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060002CE RID: 718
		// (set) Token: 0x060002CF RID: 719
		[DispId(897)]
		XlSortMethod SortMethod
		{
			[DispId(897)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			get;
			[DispId(897)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			set;
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060002D0 RID: 720
		[DispId(2749)]
		SortFields SortFields
		{
			[DispId(2749)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[return: MarshalAs(UnmanagedType.Interface)]
			get;
		}

		// Token: 0x060002D1 RID: 721
		void _VtblGap3_1();

		// Token: 0x060002D2 RID: 722
		[DispId(1675)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void Apply();
	}
}
