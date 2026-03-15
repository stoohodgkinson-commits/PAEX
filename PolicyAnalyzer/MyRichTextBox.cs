using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PolicyAnalyzer
{
	// Token: 0x02000076 RID: 118
	public class MyRichTextBox : UserControl
	{
		// Token: 0x060001C0 RID: 448 RVA: 0x00012DD0 File Offset: 0x00010FD0
		public MyRichTextBox()
		{
			this.InitializeComponent();
			this.MyRTBox.WordWrap = true;
			this.headlineFont = new Font(this.MyRTBox.Font.FontFamily, this.MyRTBox.Font.Size + 1.5f, FontStyle.Bold);
			this.normalFont = new Font(this.MyRTBox.Font, FontStyle.Regular);
			this.boldFont = new Font(this.MyRTBox.Font, FontStyle.Bold);
			this.italicFont = new Font(this.MyRTBox.Font, FontStyle.Italic);
			this.boldItalicFont = new Font(this.MyRTBox.Font, FontStyle.Bold | FontStyle.Italic);
			MenuItem menuItem = new MenuItem("Copy", new EventHandler(this.RTCopy));
			MenuItem menuItem2 = new MenuItem("Select all", new EventHandler(this.RTSelectAll));
			this.MyRTBox.ContextMenu = new ContextMenu(new MenuItem[] { menuItem, menuItem2 });
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x000035F8 File Offset: 0x000017F8
		public RichTextBox RTBox
		{
			get
			{
				return this.MyRTBox;
			}
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00003600 File Offset: 0x00001800
		public void RTCopy(object sender, EventArgs e)
		{
			this.MyRTBox.Copy();
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x0000360D File Offset: 0x0000180D
		public void RTSelectAll(object sender, EventArgs e)
		{
			this.MyRTBox.SelectAll();
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x0000361A File Offset: 0x0000181A
		public void Clear()
		{
			this.MyRTBox.Text = "";
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00012ED0 File Offset: 0x000110D0
		public void AppendHeadline(string sHeadline, bool bEOL = false)
		{
			this.MyRTBox.SelectionStart = this.MyRTBox.Text.Length;
			this.MyRTBox.SelectionIndent = 0;
			this.MyRTBox.SelectionFont = this.headlineFont;
			this.MyRTBox.SelectedText = (bEOL ? (sHeadline + "\r\n") : sHeadline);
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00012F34 File Offset: 0x00011134
		private void AppendTextWithFont(Font font, string sText, bool bEOL)
		{
			int textLength = this.MyRTBox.TextLength;
			this.MyRTBox.SelectionStart = textLength;
			if (textLength == 0 || '\n' == this.MyRTBox.Text[textLength - 1])
			{
				this.MyRTBox.SelectionIndent = 16;
				int num = 64;
				if (this.MyRTBox.SelectionTabs.Length != 0)
				{
					num = this.MyRTBox.SelectionTabs[0] - 16;
				}
				this.MyRTBox.SelectionHangingIndent = num;
			}
			this.MyRTBox.SelectionFont = font;
			this.MyRTBox.SelectedText = (bEOL ? (sText + "\r\n") : sText);
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x0000362C File Offset: 0x0000182C
		public void AppendText(string sText, bool bEOL = false)
		{
			this.AppendTextWithFont(this.normalFont, sText, bEOL);
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x0000363C File Offset: 0x0000183C
		public void AppendBoldText(string sText, bool bEOL = false)
		{
			this.AppendTextWithFont(this.boldFont, sText, bEOL);
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x0000364C File Offset: 0x0000184C
		public void AppendItalicText(string sText, bool bEOL = false)
		{
			this.AppendTextWithFont(this.italicFont, sText, bEOL);
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0000365C File Offset: 0x0000185C
		public void AppendBoldItalicText(string sText, bool bEOL = false)
		{
			this.AppendTextWithFont(this.boldItalicFont, sText, bEOL);
		}

		// Token: 0x060001CB RID: 459 RVA: 0x0000366C File Offset: 0x0000186C
		public void AppendBoldItalicAndNormalText(string sBoldItalicText, string sNormalText, bool bEOL = false)
		{
			this.AppendTextWithFont(this.boldItalicFont, sBoldItalicText, false);
			this.AppendTextWithFont(this.normalFont, sNormalText, bEOL);
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00012FD8 File Offset: 0x000111D8
		public void AppendItalicBlock(string sText, bool bEOL = false)
		{
			int textLength = this.MyRTBox.TextLength;
			this.MyRTBox.SelectionStart = textLength;
			if (textLength == 0 || '\n' == this.MyRTBox.Text[textLength - 1])
			{
				this.MyRTBox.SelectionIndent = 32;
			}
			this.MyRTBox.SelectionFont = this.italicFont;
			this.MyRTBox.SelectedText = (bEOL ? (sText + "\r\n") : sText);
		}

		// Token: 0x060001CD RID: 461 RVA: 0x0000368A File Offset: 0x0000188A
		public void SetTabs(int tabStop)
		{
			this.SetTabs(new int[] { tabStop });
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00013054 File Offset: 0x00011254
		public void SetTabs(int[] tabStops)
		{
			int selectionStart = this.MyRTBox.SelectionStart;
			int selectionLength = this.MyRTBox.SelectionLength;
			this.MyRTBox.SelectAll();
			this.MyRTBox.SelectionTabs = tabStops;
			this.MyRTBox.Select(selectionStart, selectionLength);
		}

		// Token: 0x060001CF RID: 463 RVA: 0x0000369C File Offset: 0x0000189C
		public void GoToTop()
		{
			this.MyRTBox.Select(0, 0);
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x000130A0 File Offset: 0x000112A0
		public void SetBGColor(Color color)
		{
			Control myRTBox = this.MyRTBox;
			this.BackColor = color;
			myRTBox.BackColor = color;
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x000036AB File Offset: 0x000018AB
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x000130C4 File Offset: 0x000112C4
		private void InitializeComponent()
		{
			this.MyRTBox = new RichTextBox();
			base.SuspendLayout();
			this.MyRTBox.BackColor = SystemColors.Window;
			this.MyRTBox.BorderStyle = BorderStyle.None;
			this.MyRTBox.DetectUrls = false;
			this.MyRTBox.Dock = DockStyle.Fill;
			this.MyRTBox.HideSelection = false;
			this.MyRTBox.Location = new Point(0, 0);
			this.MyRTBox.Name = "MyRTBox";
			this.MyRTBox.ReadOnly = true;
			this.MyRTBox.Size = new Size(350, 214);
			this.MyRTBox.TabIndex = 0;
			this.MyRTBox.Text = "";
			this.MyRTBox.WordWrap = false;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackColor = SystemColors.Window;
			base.Controls.Add(this.MyRTBox);
			base.Name = "MyRichTextBox";
			base.Size = new Size(350, 214);
			base.ResumeLayout(false);
		}

		// Token: 0x04000772 RID: 1906
		private Font headlineFont;

		// Token: 0x04000773 RID: 1907
		private Font normalFont;

		// Token: 0x04000774 RID: 1908
		private Font boldFont;

		// Token: 0x04000775 RID: 1909
		private Font italicFont;

		// Token: 0x04000776 RID: 1910
		private Font boldItalicFont;

		// Token: 0x04000777 RID: 1911
		private const string sEOL = "\r\n";

		// Token: 0x04000778 RID: 1912
		private IContainer components;

		// Token: 0x04000779 RID: 1913
		private RichTextBox MyRTBox;
	}
}
