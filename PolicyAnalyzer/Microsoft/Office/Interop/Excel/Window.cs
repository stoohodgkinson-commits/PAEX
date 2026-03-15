using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Interop.Excel
{
	// Token: 0x020000AD RID: 173
	[CompilerGenerated]
	[InterfaceType(2)]
	[Guid("00020893-0000-0000-C000-000000000046")]
	[TypeIdentifier]
	[ComImport]
	public interface Window
	{
		// Token: 0x060002D7 RID: 727
		void _VtblGap1_33();

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060002D8 RID: 728
		// (set) Token: 0x060002D9 RID: 729
		[DispId(650)]
		bool FreezePanes
		{
			[DispId(650)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			get;
			[DispId(650)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			set;
		}

		// Token: 0x060002DA RID: 730
		void _VtblGap2_27();

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060002DB RID: 731
		// (set) Token: 0x060002DC RID: 732
		[DispId(658)]
		int SplitColumn
		{
			[DispId(658)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			get;
			[DispId(658)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			set;
		}

		// Token: 0x060002DD RID: 733
		void _VtblGap3_2();

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060002DE RID: 734
		// (set) Token: 0x060002DF RID: 735
		[DispId(660)]
		int SplitRow
		{
			[DispId(660)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			get;
			[DispId(660)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			set;
		}

		// Token: 0x060002E0 RID: 736
		void _VtblGap4_17();

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060002E1 RID: 737
		// (set) Token: 0x060002E2 RID: 738
		[DispId(663)]
		object Zoom
		{
			[DispId(663)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[return: MarshalAs(UnmanagedType.Struct)]
			get;
			[DispId(663)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[param: MarshalAs(UnmanagedType.Struct)]
			[param: In]
			set;
		}
	}
}
