using System;
using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Interop.Excel
{
	// Token: 0x0200009E RID: 158
	[CompilerGenerated]
	[InterfaceType(2)]
	[Guid("00024486-0000-0000-C000-000000000046")]
	[DefaultMember("_Default")]
	[TypeIdentifier]
	[ComImport]
	public interface Connections : IEnumerable
	{
		// Token: 0x06000283 RID: 643
		void _VtblGap1_6();

		// Token: 0x06000284 RID: 644
		[DispId(170)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[return: MarshalAs(UnmanagedType.Interface)]
		WorkbookConnection Item([MarshalAs(UnmanagedType.Struct)] [In] object Index);
	}
}
