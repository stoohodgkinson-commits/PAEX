using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Interop.Excel
{
	// Token: 0x020000AF RID: 175
	[CompilerGenerated]
	[DefaultMember("_Default")]
	[Guid("00024485-0000-0000-C000-000000000046")]
	[InterfaceType(2)]
	[TypeIdentifier]
	[ComImport]
	public interface WorkbookConnection
	{
		// Token: 0x060002E3 RID: 739
		void _VtblGap1_7();

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060002E4 RID: 740
		// (set) Token: 0x060002E5 RID: 741
		[DispId(0)]
		[IndexerName("_Default")]
		string _Default
		{
			[DispId(0)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[return: MarshalAs(UnmanagedType.BStr)]
			get;
			[DispId(0)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[param: MarshalAs(UnmanagedType.BStr)]
			[param: In]
			set;
		}

		// Token: 0x060002E6 RID: 742
		void _VtblGap2_4();

		// Token: 0x060002E7 RID: 743
		[DispId(117)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void Delete();
	}
}
