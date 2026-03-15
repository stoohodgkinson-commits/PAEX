using System;
using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Interop.Excel
{
	// Token: 0x020000B2 RID: 178
	[CompilerGenerated]
	[Guid("000208DB-0000-0000-C000-000000000046")]
	[DefaultMember("_Default")]
	[TypeIdentifier]
	[ComImport]
	public interface Workbooks : IEnumerable
	{
		// Token: 0x060002E8 RID: 744
		void _VtblGap1_3();

		// Token: 0x060002E9 RID: 745
		[DispId(181)]
		[LCIDConversion(1)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[return: MarshalAs(UnmanagedType.Interface)]
		Workbook Add([MarshalAs(UnmanagedType.Struct)] [In] [Optional] object Template);
	}
}
