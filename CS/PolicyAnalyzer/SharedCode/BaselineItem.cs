using System;
using System.Xml;

namespace SharedCode
{
	// Token: 0x0200003A RID: 58
	internal abstract class BaselineItem
	{
		// Token: 0x060000A7 RID: 167
		public abstract string SortKey();

		// Token: 0x060000A8 RID: 168 RVA: 0x00008FF4 File Offset: 0x000071F4
		internal void ReadStdElems(XmlNode node)
		{
			this.m_sSourceFile = this.OptionalInnerText(node, "SourceFile");
			this.m_sGpoName = this.OptionalInnerText(node, "PolicyName");
			this.m_sEvalOperator = this.OptionalInnerText(node, "EvalOperator");
			if (this.m_sEvalOperator.Length == 0)
			{
				this.m_sEvalOperator = "Equal";
			}
			this.m_sEvalValues = this.OptionalInnerText(node, "EvalValues");
			bool flag;
			if (this.OptionalBoolean(node, "ValueNotFound", out flag))
			{
				this.m_bValueFound = !flag;
			}
			else
			{
				this.m_bValueFound = true;
			}
			if (this.OptionalBoolean(node, "EvalResult", out flag))
			{
				this.m_bEvalPassed = flag;
			}
			if (this.OptionalBoolean(node, "InvalidComparison", out flag))
			{
				this.m_bInvalidComparison = flag;
			}
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000090B4 File Offset: 0x000072B4
		private bool OptionalBoolean(XmlNode node, string childName, out bool bValue)
		{
			bValue = false;
			XmlElement xmlElement = node[childName];
			return xmlElement != null && bool.TryParse(xmlElement.InnerText, out bValue);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000090E0 File Offset: 0x000072E0
		internal string OptionalInnerText(XmlNode node, string childName)
		{
			XmlElement xmlElement = node[childName];
			if (xmlElement != null)
			{
				return xmlElement.InnerText;
			}
			return string.Empty;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00009104 File Offset: 0x00007304
		internal void CreateResultsElements(XmlDocument xOutputDoc, XmlElement eBaseElement)
		{
			XmlElement xmlElement = xOutputDoc.CreateElement("EvalResult");
			xmlElement.InnerText = this.m_bEvalPassed.ToString();
			eBaseElement.AppendChild(xmlElement);
			XmlElement xmlElement2 = xOutputDoc.CreateElement("BaselineValue");
			xmlElement2.InnerText = this.m_sBaselineValue;
			eBaseElement.AppendChild(xmlElement2);
			XmlElement xmlElement3 = xOutputDoc.CreateElement("EvalOperator");
			xmlElement3.InnerText = this.m_sEvalOperator;
			eBaseElement.AppendChild(xmlElement3);
			if (this.m_sEvalValues.Length > 0)
			{
				XmlElement xmlElement4 = xOutputDoc.CreateElement("EvalValues");
				xmlElement4.InnerText = this.m_sEvalValues;
				eBaseElement.AppendChild(xmlElement4);
			}
			XmlElement xmlElement5 = xOutputDoc.CreateElement("ValueNotFound");
			xmlElement5.InnerText = (!this.m_bValueFound).ToString();
			eBaseElement.AppendChild(xmlElement5);
			XmlElement xmlElement6 = xOutputDoc.CreateElement("InvalidComparison");
			xmlElement6.InnerText = this.m_bInvalidComparison.ToString();
			eBaseElement.AppendChild(xmlElement6);
		}

		// Token: 0x060000AC RID: 172
		public abstract void Evaluate(EffectiveStateSources sources, XmlDocument xOutputDoc);

		// Token: 0x060000AD RID: 173 RVA: 0x00002C69 File Offset: 0x00000E69
		internal bool IgnoreAlwaysPass()
		{
			return this.m_sEvalOperator == "Ignore-AlwaysPass";
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00002C7B File Offset: 0x00000E7B
		internal bool ImplementIgnoreAlwaysPass()
		{
			if (this.IgnoreAlwaysPass())
			{
				this.m_bEvalPassed = true;
				this.m_bInvalidComparison = false;
				return true;
			}
			return false;
		}

		// Token: 0x04000668 RID: 1640
		internal string m_sSourceFile;

		// Token: 0x04000669 RID: 1641
		internal string m_sGpoName;

		// Token: 0x0400066A RID: 1642
		internal string m_sEvalOperator;

		// Token: 0x0400066B RID: 1643
		internal string m_sEvalValues;

		// Token: 0x0400066C RID: 1644
		internal string m_sBaselineValue = string.Empty;

		// Token: 0x0400066D RID: 1645
		internal bool m_bValueFound;

		// Token: 0x0400066E RID: 1646
		internal bool m_bEvalPassed;

		// Token: 0x0400066F RID: 1647
		internal bool m_bInvalidComparison;
	}
}
