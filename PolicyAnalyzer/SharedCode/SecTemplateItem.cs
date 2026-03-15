using System;
using System.Xml;

namespace SharedCode
{
	// Token: 0x0200003E RID: 62
	internal abstract class SecTemplateItem : BaselineItem
	{
		// Token: 0x060000BC RID: 188 RVA: 0x00002CF7 File Offset: 0x00000EF7
		internal void ReadStdSecTemplateElems(XmlNode node)
		{
			this.m_sSection = node.Attributes["Section"].InnerText;
			this.m_sLineItem = node["LineItem"].InnerText;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00002D2A File Offset: 0x00000F2A
		internal virtual char KeyValSeparator()
		{
			return '=';
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00002D2E File Offset: 0x00000F2E
		internal virtual char MultiValueSeparator()
		{
			return '\0';
		}

		// Token: 0x060000BF RID: 191 RVA: 0x0000A278 File Offset: 0x00008478
		internal XmlNode GetNodeFromSeceditAndAuditpolState(EffectiveStateSources sources)
		{
			if (sources.xmlSeceditAndAuditpolState == null)
			{
				return null;
			}
			string text = this.m_sLineItem.Substring(0, this.m_sLineItem.IndexOf(this.KeyValSeparator())).Trim();
			string text2 = string.Concat(new string[] { "//SecurityTemplate[@Section='", this.m_sSection, "'][starts-with(LineItem, '", text, "')]" });
			return sources.xmlSeceditAndAuditpolState.SelectSingleNode(text2);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x0000A2F0 File Offset: 0x000084F0
		internal void WriteSecTemplateResults(XmlDocument xOutputDoc)
		{
			XmlElement xmlElement = xOutputDoc.CreateElement("SecurityTemplate");
			XmlElement xmlElement2 = xOutputDoc.CreateElement("LineItem");
			XmlElement xmlElement3 = xOutputDoc.CreateElement("PolicyName");
			xmlElement.SetAttribute("Section", this.m_sSection);
			xmlElement2.InnerText = (this.m_bValueFound ? this.m_sObservedLineItem : this.m_sLineItem);
			xmlElement3.InnerText = this.m_sObservedPolicyName;
			xmlElement.AppendChild(xmlElement2);
			xmlElement.AppendChild(xmlElement3);
			base.CreateResultsElements(xOutputDoc, xmlElement);
			xOutputDoc.DocumentElement.AppendChild(xmlElement);
		}

		// Token: 0x0400067B RID: 1659
		internal string m_sSection;

		// Token: 0x0400067C RID: 1660
		internal string m_sLineItem;

		// Token: 0x0400067D RID: 1661
		internal string m_sObservedLineItem = string.Empty;

		// Token: 0x0400067E RID: 1662
		internal string m_sObservedPolicyName = string.Empty;
	}
}
