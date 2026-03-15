using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Interop.Excel
{
	// Token: 0x020000C4 RID: 196
	[CompilerGenerated]
	[InterfaceType(2)]
	[Guid("00024428-0000-0000-C000-000000000046")]
	[TypeIdentifier]
	[ComImport]
	public interface _QueryTable
	{
		// Token: 0x060002FA RID: 762
		void _VtblGap1_3();

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060002FB RID: 763
		// (set) Token: 0x060002FC RID: 764
		[DispId(110)]
		string Name
		{
			[DispId(110)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[return: MarshalAs(UnmanagedType.BStr)]
			get;
			[DispId(110)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[param: MarshalAs(UnmanagedType.BStr)]
			[param: In]
			set;
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060002FD RID: 765
		// (set) Token: 0x060002FE RID: 766
		[DispId(1584)]
		bool FieldNames
		{
			[DispId(1584)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			get;
			[DispId(1584)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			set;
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060002FF RID: 767
		// (set) Token: 0x06000300 RID: 768
		[DispId(1585)]
		bool RowNumbers
		{
			[DispId(1585)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			get;
			[DispId(1585)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			set;
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000301 RID: 769
		// (set) Token: 0x06000302 RID: 770
		[DispId(1586)]
		bool FillAdjacentFormulas
		{
			[DispId(1586)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			get;
			[DispId(1586)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			set;
		}

		// Token: 0x06000303 RID: 771
		void _VtblGap2_2();

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000304 RID: 772
		// (set) Token: 0x06000305 RID: 773
		[DispId(1479)]
		bool RefreshOnFileOpen
		{
			[DispId(1479)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			get;
			[DispId(1479)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			set;
		}

		// Token: 0x06000306 RID: 774
		void _VtblGap3_5();

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000307 RID: 775
		// (set) Token: 0x06000308 RID: 776
		[DispId(1590)]
		XlCellInsertionMode RefreshStyle
		{
			[DispId(1590)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			get;
			[DispId(1590)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			set;
		}

		// Token: 0x06000309 RID: 777
		void _VtblGap4_2();

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600030A RID: 778
		// (set) Token: 0x0600030B RID: 779
		[DispId(1481)]
		bool SavePassword
		{
			[DispId(1481)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			get;
			[DispId(1481)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			set;
		}

		// Token: 0x0600030C RID: 780
		void _VtblGap5_9();

		// Token: 0x0600030D RID: 781
		[DispId(1417)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		bool Refresh([MarshalAs(UnmanagedType.Struct)] [In] [Optional] object BackgroundQuery);

		// Token: 0x0600030E RID: 782
		void _VtblGap6_3();

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600030F RID: 783
		// (set) Token: 0x06000310 RID: 784
		[DispId(692)]
		bool SaveData
		{
			[DispId(692)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			get;
			[DispId(692)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			set;
		}

		// Token: 0x06000311 RID: 785
		void _VtblGap7_4();

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000312 RID: 786
		// (set) Token: 0x06000313 RID: 787
		[DispId(1855)]
		int TextFilePlatform
		{
			[DispId(1855)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			get;
			[DispId(1855)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			set;
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000314 RID: 788
		// (set) Token: 0x06000315 RID: 789
		[DispId(1856)]
		int TextFileStartRow
		{
			[DispId(1856)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			get;
			[DispId(1856)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			set;
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000316 RID: 790
		// (set) Token: 0x06000317 RID: 791
		[DispId(1857)]
		XlTextParsingType TextFileParseType
		{
			[DispId(1857)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			get;
			[DispId(1857)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			set;
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000318 RID: 792
		// (set) Token: 0x06000319 RID: 793
		[DispId(1858)]
		XlTextQualifier TextFileTextQualifier
		{
			[DispId(1858)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			get;
			[DispId(1858)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			set;
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600031A RID: 794
		// (set) Token: 0x0600031B RID: 795
		[DispId(1859)]
		bool TextFileConsecutiveDelimiter
		{
			[DispId(1859)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			get;
			[DispId(1859)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			set;
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600031C RID: 796
		// (set) Token: 0x0600031D RID: 797
		[DispId(1860)]
		bool TextFileTabDelimiter
		{
			[DispId(1860)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			get;
			[DispId(1860)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			set;
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600031E RID: 798
		// (set) Token: 0x0600031F RID: 799
		[DispId(1861)]
		bool TextFileSemicolonDelimiter
		{
			[DispId(1861)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			get;
			[DispId(1861)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			set;
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000320 RID: 800
		// (set) Token: 0x06000321 RID: 801
		[DispId(1862)]
		bool TextFileCommaDelimiter
		{
			[DispId(1862)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			get;
			[DispId(1862)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			set;
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000322 RID: 802
		// (set) Token: 0x06000323 RID: 803
		[DispId(1863)]
		bool TextFileSpaceDelimiter
		{
			[DispId(1863)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			get;
			[DispId(1863)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			set;
		}

		// Token: 0x06000324 RID: 804
		void _VtblGap8_2();

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000325 RID: 805
		// (set) Token: 0x06000326 RID: 806
		[DispId(1865)]
		object TextFileColumnDataTypes
		{
			[DispId(1865)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[return: MarshalAs(UnmanagedType.Struct)]
			get;
			[DispId(1865)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[param: MarshalAs(UnmanagedType.Struct)]
			[param: In]
			set;
		}

		// Token: 0x06000327 RID: 807
		void _VtblGap9_4();

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000328 RID: 808
		// (set) Token: 0x06000329 RID: 809
		[DispId(1500)]
		bool PreserveFormatting
		{
			[DispId(1500)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			get;
			[DispId(1500)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			set;
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600032A RID: 810
		// (set) Token: 0x0600032B RID: 811
		[DispId(1868)]
		bool AdjustColumnWidth
		{
			[DispId(1868)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			get;
			[DispId(1868)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			set;
		}

		// Token: 0x0600032C RID: 812
		void _VtblGap10_4();

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600032D RID: 813
		// (set) Token: 0x0600032E RID: 814
		[DispId(1869)]
		bool TextFilePromptOnRefresh
		{
			[DispId(1869)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			get;
			[DispId(1869)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			set;
		}

		// Token: 0x0600032F RID: 815
		void _VtblGap11_7();

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000330 RID: 816
		// (set) Token: 0x06000331 RID: 817
		[DispId(1833)]
		int RefreshPeriod
		{
			[DispId(1833)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			get;
			[DispId(1833)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			set;
		}

		// Token: 0x06000332 RID: 818
		void _VtblGap12_25();

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000333 RID: 819
		// (set) Token: 0x06000334 RID: 820
		[DispId(2164)]
		bool TextFileTrailingMinusNumbers
		{
			[DispId(2164)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			get;
			[DispId(2164)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			set;
		}
	}
}
