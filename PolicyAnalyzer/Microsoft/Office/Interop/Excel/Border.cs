using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Interop.Excel
{
	// Token: 0x0200009C RID: 156
	[CompilerGenerated]
	[Guid("00020854-0000-0000-C000-000000000046")]
	[InterfaceType(2)]
	[TypeIdentifier]
	[ComImport]
	public interface Border
	{
		// Token: 0x06000277 RID: 631
		void _VtblGap1_5();

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000278 RID: 632
		// (set) Token: 0x06000279 RID: 633
		[DispId(97)]
		object ColorIndex
		{
			[DispId(97)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[return: MarshalAs(UnmanagedType.Struct)]
			get;
			[DispId(97)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[param: MarshalAs(UnmanagedType.Struct)]
			[param: In]
			set;
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600027A RID: 634
		// (set) Token: 0x0600027B RID: 635
		[DispId(119)]
		object LineStyle
		{
			[DispId(119)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[return: MarshalAs(UnmanagedType.Struct)]
			get;
			[DispId(119)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[param: MarshalAs(UnmanagedType.Struct)]
			[param: In]
			set;
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600027C RID: 636
		// (set) Token: 0x0600027D RID: 637
		[DispId(120)]
		object Weight
		{
			[DispId(120)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[return: MarshalAs(UnmanagedType.Struct)]
			get;
			[DispId(120)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[param: MarshalAs(UnmanagedType.Struct)]
			[param: In]
			set;
		}

		// Token: 0x0600027E RID: 638
		void _VtblGap2_2();

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600027F RID: 639
		// (set) Token: 0x06000280 RID: 640
		[DispId(2366)]
		object TintAndShade
		{
			[DispId(2366)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[return: MarshalAs(UnmanagedType.Struct)]
			get;
			[DispId(2366)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[param: MarshalAs(UnmanagedType.Struct)]
			[param: In]
			set;
		}
	}
}
