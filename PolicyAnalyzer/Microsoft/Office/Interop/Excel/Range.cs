using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Interop.Excel
{
	// Token: 0x020000A6 RID: 166
	[CompilerGenerated]
	[InterfaceType(2)]
	[Guid("00020846-0000-0000-C000-000000000046")]
	[TypeIdentifier]
	[ComImport]
	public interface Range : IEnumerable
	{
		// Token: 0x0600028D RID: 653
		void _VtblGap1_4();

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600028E RID: 654
		// (set) Token: 0x0600028F RID: 655
		[DispId(1063)]
		object AddIndent
		{
			[DispId(1063)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[return: MarshalAs(UnmanagedType.Struct)]
			get;
			[DispId(1063)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[param: MarshalAs(UnmanagedType.Struct)]
			[param: In]
			set;
		}

		// Token: 0x06000290 RID: 656
		void _VtblGap2_8();

		// Token: 0x06000291 RID: 657
		[DispId(793)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[return: MarshalAs(UnmanagedType.Struct)]
		object AutoFilter([MarshalAs(UnmanagedType.Struct)] [In] [Optional] object Field, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] object Criteria1, [In] XlAutoFilterOperator Operator = XlAutoFilterOperator.xlAnd, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] object Criteria2, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] object VisibleDropDown);

		// Token: 0x06000292 RID: 658
		[DispId(237)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[return: MarshalAs(UnmanagedType.Struct)]
		object AutoFit();

		// Token: 0x06000293 RID: 659
		void _VtblGap3_3();

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000294 RID: 660
		[DispId(435)]
		Borders Borders
		{
			[DispId(435)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[return: MarshalAs(UnmanagedType.Interface)]
			get;
		}

		// Token: 0x06000295 RID: 661
		void _VtblGap4_12();

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000296 RID: 662
		// (set) Token: 0x06000297 RID: 663
		[DispId(242)]
		object ColumnWidth
		{
			[DispId(242)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[return: MarshalAs(UnmanagedType.Struct)]
			get;
			[DispId(242)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[param: MarshalAs(UnmanagedType.Struct)]
			[param: In]
			set;
		}

		// Token: 0x06000298 RID: 664
		void _VtblGap5_4();

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000299 RID: 665
		[DispId(118)]
		int Count
		{
			[DispId(118)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			get;
		}

		// Token: 0x0600029A RID: 666
		void _VtblGap6_6();

		// Token: 0x17000022 RID: 34
		[DispId(0)]
		[IndexerName("_Default")]
		object this[[MarshalAs(UnmanagedType.Struct)] [In] [Optional] object RowIndex, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] object ColumnIndex]
		{
			[DispId(0)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[return: MarshalAs(UnmanagedType.Struct)]
			get;
			[DispId(0)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[param: MarshalAs(UnmanagedType.Struct)]
			[param: In]
			[param: Optional]
			set;
		}

		// Token: 0x0600029D RID: 669
		void _VtblGap7_7();

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600029E RID: 670
		[DispId(246)]
		Range EntireColumn
		{
			[DispId(246)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[return: MarshalAs(UnmanagedType.Interface)]
			get;
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600029F RID: 671
		[DispId(247)]
		Range EntireRow
		{
			[DispId(247)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[return: MarshalAs(UnmanagedType.Interface)]
			get;
		}

		// Token: 0x060002A0 RID: 672
		void _VtblGap8_7();

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060002A1 RID: 673
		[DispId(146)]
		Font Font
		{
			[DispId(146)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[return: MarshalAs(UnmanagedType.Interface)]
			get;
		}

		// Token: 0x060002A2 RID: 674
		void _VtblGap9_22();

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060002A3 RID: 675
		// (set) Token: 0x060002A4 RID: 676
		[DispId(136)]
		object HorizontalAlignment
		{
			[DispId(136)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[return: MarshalAs(UnmanagedType.Struct)]
			get;
			[DispId(136)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[param: MarshalAs(UnmanagedType.Struct)]
			[param: In]
			set;
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060002A5 RID: 677
		// (set) Token: 0x060002A6 RID: 678
		[DispId(201)]
		object IndentLevel
		{
			[DispId(201)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[return: MarshalAs(UnmanagedType.Struct)]
			get;
			[DispId(201)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[param: MarshalAs(UnmanagedType.Struct)]
			[param: In]
			set;
		}

		// Token: 0x060002A7 RID: 679
		void _VtblGap10_2();

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060002A8 RID: 680
		[DispId(129)]
		Interior Interior
		{
			[DispId(129)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[return: MarshalAs(UnmanagedType.Interface)]
			get;
		}

		// Token: 0x060002A9 RID: 681
		void _VtblGap11_12();

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060002AA RID: 682
		// (set) Token: 0x060002AB RID: 683
		[DispId(208)]
		object MergeCells
		{
			[DispId(208)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[return: MarshalAs(UnmanagedType.Struct)]
			get;
			[DispId(208)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[param: MarshalAs(UnmanagedType.Struct)]
			[param: In]
			set;
		}

		// Token: 0x060002AC RID: 684
		void _VtblGap12_6();

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060002AD RID: 685
		// (set) Token: 0x060002AE RID: 686
		[DispId(193)]
		object NumberFormat
		{
			[DispId(193)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[return: MarshalAs(UnmanagedType.Struct)]
			get;
			[DispId(193)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[param: MarshalAs(UnmanagedType.Struct)]
			[param: In]
			set;
		}

		// Token: 0x060002AF RID: 687
		void _VtblGap13_3();

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060002B0 RID: 688
		// (set) Token: 0x060002B1 RID: 689
		[DispId(134)]
		object Orientation
		{
			[DispId(134)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[return: MarshalAs(UnmanagedType.Struct)]
			get;
			[DispId(134)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[param: MarshalAs(UnmanagedType.Struct)]
			[param: In]
			set;
		}

		// Token: 0x060002B2 RID: 690
		void _VtblGap14_17();

		// Token: 0x060002B3 RID: 691
		[DispId(226)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		bool Replace([MarshalAs(UnmanagedType.Struct)] [In] object What, [MarshalAs(UnmanagedType.Struct)] [In] object Replacement, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] object LookAt, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] object SearchOrder, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] object MatchCase, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] object MatchByte, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] object SearchFormat, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] object ReplaceFormat);

		// Token: 0x060002B4 RID: 692
		void _VtblGap15_7();

		// Token: 0x060002B5 RID: 693
		[DispId(235)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[return: MarshalAs(UnmanagedType.Struct)]
		object Select();

		// Token: 0x060002B6 RID: 694
		void _VtblGap16_6();

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060002B7 RID: 695
		// (set) Token: 0x060002B8 RID: 696
		[DispId(209)]
		object ShrinkToFit
		{
			[DispId(209)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[return: MarshalAs(UnmanagedType.Struct)]
			get;
			[DispId(209)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[param: MarshalAs(UnmanagedType.Struct)]
			[param: In]
			set;
		}

		// Token: 0x060002B9 RID: 697
		void _VtblGap17_10();

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060002BA RID: 698
		[DispId(138)]
		object Text
		{
			[DispId(138)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[return: MarshalAs(UnmanagedType.Struct)]
			get;
		}

		// Token: 0x060002BB RID: 699
		void _VtblGap18_8();

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060002BC RID: 700
		// (set) Token: 0x060002BD RID: 701
		[DispId(6)]
		object Value
		{
			[DispId(6)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[return: MarshalAs(UnmanagedType.Struct)]
			get;
			[DispId(6)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[param: MarshalAs(UnmanagedType.Struct)]
			[param: In]
			[param: Optional]
			set;
		}

		// Token: 0x060002BE RID: 702
		void _VtblGap19_2();

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060002BF RID: 703
		// (set) Token: 0x060002C0 RID: 704
		[DispId(137)]
		object VerticalAlignment
		{
			[DispId(137)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[return: MarshalAs(UnmanagedType.Struct)]
			get;
			[DispId(137)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[param: MarshalAs(UnmanagedType.Struct)]
			[param: In]
			set;
		}

		// Token: 0x060002C1 RID: 705
		void _VtblGap20_9();

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060002C2 RID: 706
		// (set) Token: 0x060002C3 RID: 707
		[DispId(975)]
		int ReadingOrder
		{
			[DispId(975)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			get;
			[DispId(975)]
			[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			set;
		}
	}
}
