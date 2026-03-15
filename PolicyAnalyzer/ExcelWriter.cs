using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Microsoft.CSharp.RuntimeBinder;
using Microsoft.Office.Interop.Excel;

namespace PolicyAnalyzer
{
	// Token: 0x0200006A RID: 106
	internal class ExcelWriter
	{
		// Token: 0x06000188 RID: 392 RVA: 0x0001090C File Offset: 0x0000EB0C
		public static void WriteDocToExcel(ExcelWriter.Doc_t xDoc, ExcelWriter.ProgressIndicator progIndicator = null)
		{
			string text;
			object excelInstance = ExcelWriter.GetExcelInstance(out text);
			if (excelInstance != null)
			{
				try
				{
					ExcelWriter.WriteDocToExcelInternal(excelInstance, xDoc, progIndicator);
					return;
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, "Export all data to Excel", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					return;
				}
			}
			MessageBox.Show("Microsoft Excel 2007 or newer is not installed.\r\n\r\nDetails:\r\n" + text, "Export all data to Excel", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}

		// Token: 0x06000189 RID: 393 RVA: 0x0001096C File Offset: 0x0000EB6C
		private static void WriteDocToExcelInternal(object oExcel, ExcelWriter.Doc_t xDoc, ExcelWriter.ProgressIndicator progIndicator)
		{
			CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
			Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
			Microsoft.Office.Interop.Excel.Application application = (Microsoft.Office.Interop.Excel.Application)oExcel;
			Workbook workbook = null;
			Worksheet worksheet = null;
			bool flag = false;
			try
			{
				application.DisplayAlerts = false;
				workbook = application.Workbooks.Add(Type.Missing);
				while (workbook.Worksheets.Count > 1)
				{
					if (ExcelWriter.<>o__4.<>p__0 == null)
					{
						ExcelWriter.<>o__4.<>p__0 = CallSite<Func<CallSite, object, Worksheet>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof(Worksheet), typeof(ExcelWriter)));
					}
					ExcelWriter.<>o__4.<>p__0.Target(ExcelWriter.<>o__4.<>p__0, workbook.Worksheets[2]).Delete();
				}
				if (ExcelWriter.<>o__4.<>p__1 == null)
				{
					ExcelWriter.<>o__4.<>p__1 = CallSite<Func<CallSite, object, Worksheet>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof(Worksheet), typeof(ExcelWriter)));
				}
				worksheet = ExcelWriter.<>o__4.<>p__1.Target(ExcelWriter.<>o__4.<>p__1, workbook.Worksheets[1]);
				worksheet.Name = "Policy Analyzer";
				worksheet.Cells.Select();
				if (ExcelWriter.<>o__4.<>p__2 == null)
				{
					ExcelWriter.<>o__4.<>p__2 = CallSite<Func<CallSite, object, Range>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof(Range), typeof(ExcelWriter)));
				}
				ExcelWriter.<>o__4.<>p__2.Target(ExcelWriter.<>o__4.<>p__2, application.Selection).NumberFormat = "@";
				if (ExcelWriter.<>o__4.<>p__3 == null)
				{
					ExcelWriter.<>o__4.<>p__3 = CallSite<Func<CallSite, object, Range>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof(Range), typeof(ExcelWriter)));
				}
				ExcelWriter.<>o__4.<>p__3.Target(ExcelWriter.<>o__4.<>p__3, application.Selection).VerticalAlignment = XlVAlign.xlVAlignTop;
				ExcelColumnName excelColumnName = new ExcelColumnName();
				int num = 1;
				foreach (string text in xDoc.m_colHeaders)
				{
					Range range = worksheet.get_Range(excelColumnName.ToString() + num.ToString(), Type.Missing);
					excelColumnName = ExcelColumnName.op_Increment(excelColumnName);
					range.set_Value(XlRangeValueDataType.xlRangeValueDefault, text);
					range.Font.Bold = true;
				}
				excelColumnName.Reset();
				num = 2;
				foreach (ExcelWriter.Row_t row_t in xDoc.m_rows)
				{
					if (progIndicator != null)
					{
						progIndicator(0, xDoc.m_rows.Count + 1, num - 1);
					}
					foreach (ExcelWriter.Cell_t cell_t in row_t.m_cells)
					{
						string text2 = excelColumnName.ToString() + num.ToString();
						excelColumnName = ExcelColumnName.op_Increment(excelColumnName);
						Range range2 = worksheet.get_Range(text2, text2);
						StringBuilder stringBuilder = new StringBuilder();
						bool flag2 = false;
						using (List<string>.Enumerator enumerator2 = cell_t.m_content.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								if (enumerator2.Current.Contains("\n"))
								{
									flag2 = true;
								}
							}
						}
						foreach (string text3 in cell_t.m_content)
						{
							if (stringBuilder.Length > 0)
							{
								if (flag2)
								{
									stringBuilder.Append("\r\n-----\r\n");
								}
								else
								{
									stringBuilder.Append("\r\n");
								}
							}
							stringBuilder.Append(text3);
						}
						range2.set_Value(XlRangeValueDataType.xlRangeValueDefault, stringBuilder.ToString());
						if (!cell_t.m_color.IsEmpty)
						{
							Color color = cell_t.m_color;
							int num2 = (int)color.B * 65536 + (int)color.G * 256 + (int)color.R;
							if (num2 != 0)
							{
								range2.Interior.Color = num2;
							}
						}
					}
					excelColumnName.Reset();
					num++;
				}
				worksheet.UsedRange.Select();
				foreach (XlBordersIndex xlBordersIndex in new XlBordersIndex[]
				{
					XlBordersIndex.xlEdgeTop,
					XlBordersIndex.xlEdgeBottom,
					XlBordersIndex.xlEdgeLeft,
					XlBordersIndex.xlEdgeRight,
					XlBordersIndex.xlInsideHorizontal,
					XlBordersIndex.xlInsideVertical
				})
				{
					Border border = worksheet.UsedRange.Borders[xlBordersIndex];
					border.LineStyle = XlLineStyle.xlContinuous;
					border.ColorIndex = 0;
					border.TintAndShade = 0;
					border.Weight = XlBorderWeight.xlHairline;
				}
				worksheet.UsedRange.AutoFilter(1, Type.Missing, XlAutoFilterOperator.xlAnd, Type.Missing, Type.Missing);
				application.ActiveWindow.SplitColumn = 0;
				application.ActiveWindow.SplitRow = 1;
				application.ActiveWindow.FreezePanes = true;
				worksheet.Cells.EntireColumn.AutoFit();
				worksheet.get_Range("A2", Type.Missing).Select();
				application.ActiveWindow.Zoom = 80;
				excelColumnName.Reset();
				Sort sort = worksheet.AutoFilter.Sort;
				sort.SortFields.Clear();
				for (int j = 0; j < xDoc.m_colHeaders.Length; j++)
				{
					string text4 = excelColumnName.ToString() + ":" + excelColumnName.ToString();
					Range range3 = worksheet.get_Range(text4, Type.Missing);
					if (ExcelWriter.<>o__4.<>p__5 == null)
					{
						ExcelWriter.<>o__4.<>p__5 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, 83, typeof(ExcelWriter), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
					}
					Func<CallSite, object, bool> target = ExcelWriter.<>o__4.<>p__5.Target;
					CallSite <>p__ = ExcelWriter.<>o__4.<>p__5;
					if (ExcelWriter.<>o__4.<>p__4 == null)
					{
						ExcelWriter.<>o__4.<>p__4 = CallSite<Func<CallSite, object, double, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, 15, typeof(ExcelWriter), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
						}));
					}
					if (target(<>p__, ExcelWriter.<>o__4.<>p__4.Target(ExcelWriter.<>o__4.<>p__4, range3.EntireColumn.ColumnWidth, 50.0)))
					{
						range3.EntireColumn.ColumnWidth = 50.0;
					}
					if (j < 6)
					{
						sort.SortFields.Add(range3, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
					}
					excelColumnName = ExcelColumnName.op_Increment(excelColumnName);
				}
				worksheet.Cells.EntireRow.AutoFit();
				sort.Header = XlYesNoGuess.xlYes;
				sort.MatchCase = false;
				sort.SortMethod = XlSortMethod.xlPinYin;
				sort.Apply();
				application.Visible = true;
				flag = true;
				application.DisplayAlerts = true;
			}
			finally
			{
				if (!flag)
				{
					if (workbook != null)
					{
						workbook.Close(false, string.Empty, Type.Missing);
					}
					if (application != null)
					{
						application.Quit();
					}
				}
				Thread.CurrentThread.CurrentCulture = currentCulture;
			}
		}

		// Token: 0x0600018A RID: 394 RVA: 0x000110A8 File Offset: 0x0000F2A8
		public static void WriteDataGridViewToExcel(DataGridView dataGridView1, ExcelWriter.ProgressIndicator progIndicator = null)
		{
			string text;
			object excelInstance = ExcelWriter.GetExcelInstance(out text);
			if (excelInstance != null)
			{
				try
				{
					ExcelWriter.WriteDataGridViewToExcelInternal(excelInstance, dataGridView1, progIndicator);
					return;
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, "Export to Excel", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					return;
				}
			}
			MessageBox.Show("Microsoft Excel 2007 or newer is not installed.\r\n\r\nDetails:\r\n" + text, "Export to Excel", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00011108 File Offset: 0x0000F308
		private static object GetExcelInstance(out string sErrorDetails)
		{
			sErrorDetails = "";
			object obj;
			try
			{
				obj = ExcelWriter.GetExcelInstanceInternal();
			}
			catch (Exception ex)
			{
				sErrorDetails = ex.Message;
				obj = null;
			}
			return obj;
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00003438 File Offset: 0x00001638
		private static object GetExcelInstanceInternal()
		{
			return (Microsoft.Office.Interop.Excel.Application)Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid("00024500-0000-0000-C000-000000000046")));
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00011144 File Offset: 0x0000F344
		private static void WriteDataGridViewToExcelInternal(object oExcel, DataGridView dataGridView1, ExcelWriter.ProgressIndicator progIndicator)
		{
			CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
			Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
			Microsoft.Office.Interop.Excel.Application application = (Microsoft.Office.Interop.Excel.Application)oExcel;
			Workbook workbook = null;
			Worksheet worksheet = null;
			bool flag = false;
			try
			{
				application.DisplayAlerts = false;
				workbook = application.Workbooks.Add(Type.Missing);
				while (workbook.Worksheets.Count > 1)
				{
					if (ExcelWriter.<>o__9.<>p__0 == null)
					{
						ExcelWriter.<>o__9.<>p__0 = CallSite<Func<CallSite, object, Worksheet>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof(Worksheet), typeof(ExcelWriter)));
					}
					ExcelWriter.<>o__9.<>p__0.Target(ExcelWriter.<>o__9.<>p__0, workbook.Worksheets[2]).Delete();
				}
				if (ExcelWriter.<>o__9.<>p__1 == null)
				{
					ExcelWriter.<>o__9.<>p__1 = CallSite<Func<CallSite, object, Worksheet>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof(Worksheet), typeof(ExcelWriter)));
				}
				worksheet = ExcelWriter.<>o__9.<>p__1.Target(ExcelWriter.<>o__9.<>p__1, workbook.Worksheets[1]);
				worksheet.Name = "Policy Analyzer";
				worksheet.Cells.Select();
				if (ExcelWriter.<>o__9.<>p__2 == null)
				{
					ExcelWriter.<>o__9.<>p__2 = CallSite<Func<CallSite, object, Range>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof(Range), typeof(ExcelWriter)));
				}
				ExcelWriter.<>o__9.<>p__2.Target(ExcelWriter.<>o__9.<>p__2, application.Selection).NumberFormat = "@";
				ExcelColumnName excelColumnName = new ExcelColumnName();
				int num = 1;
				foreach (object obj in dataGridView1.Columns)
				{
					DataGridViewColumn dataGridViewColumn = (DataGridViewColumn)obj;
					Range range = worksheet.get_Range(excelColumnName.ToString() + num.ToString(), Type.Missing);
					excelColumnName = ExcelColumnName.op_Increment(excelColumnName);
					range.set_Value(XlRangeValueDataType.xlRangeValueDefault, dataGridViewColumn.HeaderText);
					range.Font.Bold = true;
				}
				excelColumnName.Reset();
				num = 2;
				foreach (object obj2 in ((IEnumerable)dataGridView1.Rows))
				{
					DataGridViewRow dataGridViewRow = (DataGridViewRow)obj2;
					if (dataGridViewRow.Visible)
					{
						if (progIndicator != null)
						{
							progIndicator(0, dataGridView1.RowCount, num - 1);
						}
						foreach (object obj3 in dataGridViewRow.Cells)
						{
							DataGridViewCell dataGridViewCell = (DataGridViewCell)obj3;
							Range range2 = worksheet.get_Range(excelColumnName.ToString() + num.ToString(), Type.Missing);
							excelColumnName = ExcelColumnName.op_Increment(excelColumnName);
							range2.set_Value(XlRangeValueDataType.xlRangeValueDefault, dataGridViewCell.Value);
							Color backColor = dataGridViewCell.Style.BackColor;
							int num2 = (int)backColor.B * 65536 + (int)backColor.G * 256 + (int)backColor.R;
							if (num2 != 0)
							{
								range2.Interior.Color = num2;
							}
						}
						excelColumnName.Reset();
						num++;
					}
				}
				worksheet.UsedRange.Select();
				worksheet.UsedRange.AutoFilter(1, Type.Missing, XlAutoFilterOperator.xlAnd, Type.Missing, Type.Missing);
				application.ActiveWindow.SplitColumn = 0;
				application.ActiveWindow.SplitRow = 1;
				application.ActiveWindow.FreezePanes = true;
				worksheet.Cells.EntireColumn.AutoFit();
				worksheet.Cells.EntireRow.AutoFit();
				worksheet.get_Range("A2", Type.Missing).Select();
				excelColumnName.Reset();
				for (int i = 1; i <= dataGridView1.Columns.Count; i++)
				{
					string text = excelColumnName + "1";
					excelColumnName = ExcelColumnName.op_Increment(excelColumnName);
					if (ExcelWriter.<>o__9.<>p__3 == null)
					{
						ExcelWriter.<>o__9.<>p__3 = CallSite<Func<CallSite, object, double>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof(double), typeof(ExcelWriter)));
					}
					if (ExcelWriter.<>o__9.<>p__3.Target(ExcelWriter.<>o__9.<>p__3, worksheet.get_Range(text, Type.Missing).ColumnWidth) > 45.0)
					{
						worksheet.get_Range(text, Type.Missing).ColumnWidth = 45;
					}
				}
				application.Visible = true;
				flag = true;
				application.DisplayAlerts = true;
			}
			finally
			{
				if (!flag)
				{
					if (workbook != null)
					{
						workbook.Close(false, string.Empty, Type.Missing);
					}
					if (application != null)
					{
						application.Quit();
					}
				}
				Thread.CurrentThread.CurrentCulture = currentCulture;
			}
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00003453 File Offset: 0x00001653
		public static bool IsExcelAvailable()
		{
			return true;
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00011678 File Offset: 0x0000F878
		public static void ConvertToExcel(string csvSource, string xlsxTarget, bool bVisible, ExcelWriter.ExcelConditionalFormattingDelegate ConditionalFormatting)
		{
			Microsoft.Office.Interop.Excel.Application application = null;
			Workbook workbook = null;
			Worksheet worksheet = null;
			bool flag = false;
			try
			{
				application = (Microsoft.Office.Interop.Excel.Application)Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid("00024500-0000-0000-C000-000000000046")));
				application.DisplayAlerts = false;
				workbook = application.Workbooks.Add(Type.Missing);
				while (workbook.Worksheets.Count > 1)
				{
					if (ExcelWriter.<>o__12.<>p__0 == null)
					{
						ExcelWriter.<>o__12.<>p__0 = CallSite<Func<CallSite, object, Worksheet>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof(Worksheet), typeof(ExcelWriter)));
					}
					ExcelWriter.<>o__12.<>p__0.Target(ExcelWriter.<>o__12.<>p__0, workbook.Worksheets[2]).Delete();
				}
				if (ExcelWriter.<>o__12.<>p__1 == null)
				{
					ExcelWriter.<>o__12.<>p__1 = CallSite<Func<CallSite, object, Worksheet>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof(Worksheet), typeof(ExcelWriter)));
				}
				worksheet = ExcelWriter.<>o__12.<>p__1.Target(ExcelWriter.<>o__12.<>p__1, workbook.Worksheets[1]);
				worksheet.Name = "PolicyAnalyzer";
				QueryTables queryTables = worksheet.QueryTables;
				object obj = "TEXT;" + csvSource;
				if (ExcelWriter.<>o__12.<>p__2 == null)
				{
					ExcelWriter.<>o__12.<>p__2 = CallSite<Func<CallSite, object, Range>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof(Range), typeof(ExcelWriter)));
				}
				QueryTable queryTable = queryTables.Add(obj, ExcelWriter.<>o__12.<>p__2.Target(ExcelWriter.<>o__12.<>p__2, worksheet.Cells[1, 1]), Type.Missing);
				queryTable.Name = "PolicyAnalyzer output";
				queryTable.FieldNames = true;
				queryTable.RowNumbers = false;
				queryTable.FillAdjacentFormulas = false;
				queryTable.PreserveFormatting = true;
				queryTable.RefreshOnFileOpen = false;
				queryTable.RefreshStyle = XlCellInsertionMode.xlInsertDeleteCells;
				queryTable.SavePassword = false;
				queryTable.SaveData = true;
				queryTable.AdjustColumnWidth = true;
				queryTable.RefreshPeriod = 0;
				queryTable.TextFilePromptOnRefresh = false;
				queryTable.TextFilePlatform = 1252;
				queryTable.TextFileStartRow = 1;
				queryTable.TextFileParseType = XlTextParsingType.xlDelimited;
				queryTable.TextFileTextQualifier = XlTextQualifier.xlTextQualifierDoubleQuote;
				queryTable.TextFileConsecutiveDelimiter = false;
				queryTable.TextFileTabDelimiter = true;
				queryTable.TextFileSemicolonDelimiter = false;
				queryTable.TextFileCommaDelimiter = false;
				queryTable.TextFileSpaceDelimiter = false;
				queryTable.TextFileColumnDataTypes = new object[]
				{
					2, 2, 2, 2, 2, 2, 2, 2, 2, 2,
					2, 2, 2, 2, 2, 2, 2, 2, 2, 2,
					2, 2, 2
				};
				queryTable.TextFileTrailingMinusNumbers = true;
				queryTable.Refresh(false);
				workbook.Connections.Item(1).Delete();
				worksheet.Cells.Replace("^", "\n", XlLookAt.xlPart, XlSearchOrder.xlByRows, false, false, false, false);
				Window activeWindow = application.ActiveWindow;
				activeWindow.Zoom = 80;
				worksheet.UsedRange.Select();
				worksheet.Cells.EntireColumn.AutoFit();
				worksheet.Cells.EntireRow.AutoFit();
				if (ExcelWriter.<>o__12.<>p__3 == null)
				{
					ExcelWriter.<>o__12.<>p__3 = CallSite<Func<CallSite, object, Range>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof(Range), typeof(ExcelWriter)));
				}
				ExcelWriter.<>o__12.<>p__3.Target(ExcelWriter.<>o__12.<>p__3, worksheet.Cells[1, 1]).ColumnWidth = 0;
				for (int i = 2; i <= worksheet.Columns.Count; i++)
				{
					if (ExcelWriter.<>o__12.<>p__4 == null)
					{
						ExcelWriter.<>o__12.<>p__4 = CallSite<Func<CallSite, object, Range>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof(Range), typeof(ExcelWriter)));
					}
					Range range = ExcelWriter.<>o__12.<>p__4.Target(ExcelWriter.<>o__12.<>p__4, worksheet.Cells[1, i]);
					if (ExcelWriter.<>o__12.<>p__5 == null)
					{
						ExcelWriter.<>o__12.<>p__5 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof(string), typeof(ExcelWriter)));
					}
					if (ExcelWriter.<>o__12.<>p__5.Target(ExcelWriter.<>o__12.<>p__5, range.Text).Length == 0)
					{
						break;
					}
					if (ExcelWriter.<>o__12.<>p__6 == null)
					{
						ExcelWriter.<>o__12.<>p__6 = CallSite<Func<CallSite, object, double>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof(double), typeof(ExcelWriter)));
					}
					if (ExcelWriter.<>o__12.<>p__6.Target(ExcelWriter.<>o__12.<>p__6, range.ColumnWidth) > 80.0)
					{
						range.ColumnWidth = 80;
					}
				}
				if (ExcelWriter.<>o__12.<>p__7 == null)
				{
					ExcelWriter.<>o__12.<>p__7 = CallSite<Func<CallSite, object, Range>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof(Range), typeof(ExcelWriter)));
				}
				Range range2 = ExcelWriter.<>o__12.<>p__7.Target(ExcelWriter.<>o__12.<>p__7, application.Selection);
				range2.HorizontalAlignment = Constants.xlLeft;
				range2.VerticalAlignment = Constants.xlTop;
				range2.Orientation = 0;
				range2.AddIndent = false;
				range2.IndentLevel = 0;
				range2.ShrinkToFit = false;
				range2.ReadingOrder = -5002;
				range2.MergeCells = false;
				range2.NumberFormat = "#";
				if (ConditionalFormatting != null)
				{
					ConditionalFormatting(ref application, ref worksheet);
				}
				if (ExcelWriter.<>o__12.<>p__8 == null)
				{
					ExcelWriter.<>o__12.<>p__8 = CallSite<Func<CallSite, object, Range>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof(Range), typeof(ExcelWriter)));
				}
				ExcelWriter.<>o__12.<>p__8.Target(ExcelWriter.<>o__12.<>p__8, worksheet.Rows[1, Type.Missing]).Select();
				if (ExcelWriter.<>o__12.<>p__9 == null)
				{
					ExcelWriter.<>o__12.<>p__9 = CallSite<Func<CallSite, object, Range>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof(Range), typeof(ExcelWriter)));
				}
				Range range3 = ExcelWriter.<>o__12.<>p__9.Target(ExcelWriter.<>o__12.<>p__9, application.Selection);
				range3.Font.Bold = true;
				range3.AutoFilter(1, Type.Missing, XlAutoFilterOperator.xlAnd, Type.Missing, true);
				activeWindow.SplitColumn = 0;
				activeWindow.SplitRow = 1;
				activeWindow.FreezePanes = true;
				if (ExcelWriter.<>o__12.<>p__10 == null)
				{
					ExcelWriter.<>o__12.<>p__10 = CallSite<Func<CallSite, object, Range>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof(Range), typeof(ExcelWriter)));
				}
				ExcelWriter.<>o__12.<>p__10.Target(ExcelWriter.<>o__12.<>p__10, worksheet.Cells[2, 2]).Select();
				worksheet = null;
				workbook.SaveAs(xlsxTarget, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
				if (bVisible)
				{
					application.Visible = true;
					flag = true;
					application.DisplayAlerts = true;
				}
			}
			finally
			{
				if (!flag)
				{
					if (workbook != null)
					{
						workbook.Close(false, string.Empty, Type.Missing);
					}
					if (application != null)
					{
						application.Quit();
					}
				}
			}
		}

		// Token: 0x0200006B RID: 107
		public class Cell_t
		{
			// Token: 0x1700000D RID: 13
			// (get) Token: 0x06000191 RID: 401 RVA: 0x00003456 File Offset: 0x00001656
			// (set) Token: 0x06000192 RID: 402 RVA: 0x0000345E File Offset: 0x0000165E
			public List<string> m_content { get; private set; }

			// Token: 0x06000193 RID: 403 RVA: 0x00003467 File Offset: 0x00001667
			public Cell_t()
			{
				this.m_content = new List<string>();
			}

			// Token: 0x06000194 RID: 404 RVA: 0x0000347A File Offset: 0x0000167A
			public void AddText(string sText)
			{
				this.m_content.Add(sText);
			}

			// Token: 0x04000738 RID: 1848
			public Color m_color;
		}

		// Token: 0x0200006C RID: 108
		public class Row_t
		{
			// Token: 0x1700000E RID: 14
			// (get) Token: 0x06000195 RID: 405 RVA: 0x00003488 File Offset: 0x00001688
			// (set) Token: 0x06000196 RID: 406 RVA: 0x00003490 File Offset: 0x00001690
			public ExcelWriter.Cell_t[] m_cells { get; private set; }

			// Token: 0x06000197 RID: 407 RVA: 0x00011E64 File Offset: 0x00010064
			public Row_t(int nColumns)
			{
				this.m_cells = new ExcelWriter.Cell_t[nColumns];
				for (int i = 0; i < nColumns; i++)
				{
					this.m_cells[i] = new ExcelWriter.Cell_t();
				}
			}
		}

		// Token: 0x0200006D RID: 109
		public class Doc_t
		{
			// Token: 0x1700000F RID: 15
			// (get) Token: 0x06000198 RID: 408 RVA: 0x00003499 File Offset: 0x00001699
			// (set) Token: 0x06000199 RID: 409 RVA: 0x000034A1 File Offset: 0x000016A1
			public string[] m_colHeaders { get; private set; }

			// Token: 0x17000010 RID: 16
			// (get) Token: 0x0600019A RID: 410 RVA: 0x000034AA File Offset: 0x000016AA
			// (set) Token: 0x0600019B RID: 411 RVA: 0x000034B2 File Offset: 0x000016B2
			public List<ExcelWriter.Row_t> m_rows { get; private set; }

			// Token: 0x0600019C RID: 412 RVA: 0x000034BB File Offset: 0x000016BB
			public Doc_t(int nColumns)
			{
				this.m_nColumns = nColumns;
				this.m_colHeaders = new string[nColumns];
				this.m_rows = new List<ExcelWriter.Row_t>();
			}

			// Token: 0x0600019D RID: 413 RVA: 0x00011E9C File Offset: 0x0001009C
			public ExcelWriter.Row_t AddRow()
			{
				ExcelWriter.Row_t row_t = new ExcelWriter.Row_t(this.m_nColumns);
				this.m_rows.Add(row_t);
				return row_t;
			}

			// Token: 0x0400073B RID: 1851
			private int m_nColumns;
		}

		// Token: 0x0200006E RID: 110
		// (Invoke) Token: 0x0600019F RID: 415
		public delegate void ProgressIndicator(int minimum, int maximum, int progress);

		// Token: 0x0200006F RID: 111
		// (Invoke) Token: 0x060001A6 RID: 422
		// (Invoke) Token: 0x060001A3 RID: 419
		public delegate void ExcelConditionalFormattingDelegate(ref Microsoft.Office.Interop.Excel.Application excel, ref Worksheet ws, ref Microsoft.Office.Interop.Excel.Application excel, ref Worksheet ws);
	}
}
