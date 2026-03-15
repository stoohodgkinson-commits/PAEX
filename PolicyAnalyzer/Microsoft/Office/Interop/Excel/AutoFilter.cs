using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Interop.Excel
{
	// Token: 0x0200009B RID: 155
	[CompilerGenerated]
	[InterfaceType(2)]
	[Guid("00024432-0000-0000-C000-000000000046")]
	[TypeIdentifier]
	[ComImport]
	public interface AutoFilter
	{
		// Token: 0x06000275 RID: 629
		void _VtblGap1_6();

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000276 RID: 630
		[DispId(880)]
		Sort Sort
		{
			[DispId(880)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[return: MarshalAs(UnmanagedType.Interface)]
			get;
		}
	}
}
