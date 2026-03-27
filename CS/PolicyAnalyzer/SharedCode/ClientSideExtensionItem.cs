using System;
using System.Xml;

namespace SharedCode
{
	// Token: 0x02000049 RID: 73
	internal class ClientSideExtensionItem : BaselineItem
	{
		// Token: 0x060000F1 RID: 241 RVA: 0x00002F2B File Offset: 0x0000112B
		public ClientSideExtensionItem(XmlNode node)
		{
			this.m_sCSEType = node.LocalName;
			this.m_sGuid = node["GUID"].InnerText;
			this.m_sName = node["Name"].InnerText;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x0000BBBC File Offset: 0x00009DBC
		public override string SortKey()
		{
			return string.Concat(new string[]
			{
				base.GetType().Name,
				"!",
				this.m_sCSEType,
				"!",
				this.m_sGuid
			}).ToLower();
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x0000BC0C File Offset: 0x00009E0C
		public override void Evaluate(EffectiveStateSources sources, XmlDocument xOutputDoc)
		{
			XmlElement xmlElement = xOutputDoc.CreateElement(this.m_sCSEType);
			XmlElement xmlElement2 = xOutputDoc.CreateElement("GUID");
			XmlElement xmlElement3 = xOutputDoc.CreateElement("Name");
			xmlElement2.InnerText = this.m_sGuid;
			xmlElement3.InnerText = this.m_sName;
			xmlElement.AppendChild(xmlElement2);
			xmlElement.AppendChild(xmlElement3);
			xOutputDoc.DocumentElement.AppendChild(xmlElement);
		}

		// Token: 0x04000698 RID: 1688
		private string m_sCSEType;

		// Token: 0x04000699 RID: 1689
		private string m_sGuid;

		// Token: 0x0400069A RID: 1690
		private string m_sName;
	}
}
