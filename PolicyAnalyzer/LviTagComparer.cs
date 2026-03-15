using System;
using System.Collections;
using System.Windows.Forms;

// Token: 0x02000002 RID: 2
internal class LviTagComparer : IComparer
{
	// Token: 0x06000001 RID: 1 RVA: 0x00003B48 File Offset: 0x00001D48
	public static void PrepareHeaders(ListView lv)
	{
		foreach (object obj in lv.Columns)
		{
			((ColumnHeader)obj).Tag = true;
		}
	}

	// Token: 0x06000002 RID: 2 RVA: 0x00003BA4 File Offset: 0x00001DA4
	public static void SetComparer(ListView lv, ColumnClickEventArgs e)
	{
		bool flag = (bool)lv.Columns[e.Column].Tag;
		lv.Columns[e.Column].Tag = !flag;
		lv.ListViewItemSorter = new LviTagComparer(e.Column, flag);
	}

	// Token: 0x06000003 RID: 3 RVA: 0x000028B0 File Offset: 0x00000AB0
	public LviTagComparer(int column, bool ascending)
	{
		this.m_col = column;
		this.m_bAscending = ascending;
	}

	// Token: 0x06000004 RID: 4 RVA: 0x000028C6 File Offset: 0x00000AC6
	public int Compare(object x, object y)
	{
		if (!this.m_bAscending)
		{
			return -this.CompareImpl(x, y);
		}
		return this.CompareImpl(x, y);
	}

	// Token: 0x06000005 RID: 5 RVA: 0x00003C00 File Offset: 0x00001E00
	private int CompareImpl(object x, object y)
	{
		object tag = ((ListViewItem)x).SubItems[this.m_col].Tag;
		object tag2 = ((ListViewItem)y).SubItems[this.m_col].Tag;
		if (tag == null)
		{
			return string.Compare(((ListViewItem)x).SubItems[this.m_col].Text, ((ListViewItem)y).SubItems[this.m_col].Text);
		}
		if (tag is DateTime)
		{
			return DateTime.Compare((DateTime)tag, (DateTime)tag2);
		}
		if (tag is int)
		{
			return (int)tag - (int)tag2;
		}
		if (!(tag is long))
		{
			return string.Compare((string)tag, (string)tag2);
		}
		long num = (long)tag;
		long num2 = (long)tag2;
		if (num < num2)
		{
			return -1;
		}
		if (num == num2)
		{
			return 0;
		}
		return 1;
	}

	// Token: 0x04000001 RID: 1
	private int m_col;

	// Token: 0x04000002 RID: 2
	private bool m_bAscending;
}
