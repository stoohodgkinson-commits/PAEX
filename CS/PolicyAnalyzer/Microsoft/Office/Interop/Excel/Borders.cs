using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Interop.Excel
{
	// Token: 0x0200009D RID: 157
	[CompilerGenerated]
	[InterfaceType(2)]
	[Guid("00020855-0000-0000-C000-000000000046")]
	[TypeIdentifier]
	[ComImport]
	public interface Borders : IEnumerable
	{
		// Token: 0x06000281 RID: 641
		void _VtblGap1_16();

		// Token: 0x1700001B RID: 27
		[DispId(0)]
		[IndexerName("_Default")]
		Border this[[In] XlBordersIndex Index]
		{
			[DispId(0)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[return: MarshalAs(UnmanagedType.Interface)]
			get;
		}
	}
}
