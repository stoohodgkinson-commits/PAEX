using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Interop.Excel
{
	// Token: 0x020000A3 RID: 163
	[CompilerGenerated]
	[InterfaceType(2)]
	[Guid("00020870-0000-0000-C000-000000000046")]
	[TypeIdentifier]
	[ComImport]
	public interface Interior
	{
		// Token: 0x06000288 RID: 648
		void _VtblGap1_3();

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000289 RID: 649
		// (set) Token: 0x0600028A RID: 650
		[DispId(99)]
		object Color
		{
			[DispId(99)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[return: MarshalAs(UnmanagedType.Struct)]
			get;
			[DispId(99)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[param: MarshalAs(UnmanagedType.Struct)]
			[param: In]
			set;
		}
	}
}
