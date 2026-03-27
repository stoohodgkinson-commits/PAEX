using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Interop.Excel
{
	// Token: 0x020000A2 RID: 162
	[CompilerGenerated]
	[InterfaceType(2)]
	[Guid("0002084D-0000-0000-C000-000000000046")]
	[TypeIdentifier]
	[ComImport]
	public interface Font
	{
		// Token: 0x06000285 RID: 645
		void _VtblGap1_5();

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000286 RID: 646
		// (set) Token: 0x06000287 RID: 647
		[DispId(96)]
		object Bold
		{
			[DispId(96)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[return: MarshalAs(UnmanagedType.Struct)]
			get;
			[DispId(96)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[param: MarshalAs(UnmanagedType.Struct)]
			[param: In]
			set;
		}
	}
}
