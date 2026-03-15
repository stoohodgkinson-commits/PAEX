using System;
using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Interop.Excel
{
	// Token: 0x020000A5 RID: 165
	[CompilerGenerated]
	[InterfaceType(2)]
	[DefaultMember("_Default")]
	[Guid("00024429-0000-0000-C000-000000000046")]
	[TypeIdentifier]
	[ComImport]
	public interface QueryTables : IEnumerable
	{
		// Token: 0x0600028B RID: 651
		void _VtblGap1_4();

		// Token: 0x0600028C RID: 652
		[DispId(181)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[return: MarshalAs(UnmanagedType.Interface)]
		QueryTable Add([MarshalAs(UnmanagedType.Struct)] [In] object Connection, [MarshalAs(UnmanagedType.Interface)] [In] Range Destination, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] object Sql);
	}
}
