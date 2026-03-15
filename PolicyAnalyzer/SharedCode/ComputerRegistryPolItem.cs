using System;
using System.Xml;

namespace SharedCode
{
	// Token: 0x0200003C RID: 60
	internal class ComputerRegistryPolItem : BaselineItem
	{
		// Token: 0x060000B6 RID: 182 RVA: 0x00009F4C File Offset: 0x0000814C
		public ComputerRegistryPolItem(XmlNode node)
		{
			this.m_regHelper = new RegistryItemHelper(this, "ComputerConfig");
			base.ReadStdElems(node);
			this.m_regHelper.SetKeyValueType(node["Key"].InnerText, node["Value"].InnerText, node["RegType"].InnerText);
			this.m_sBaselineValue = node["RegData"].InnerText;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00009FC8 File Offset: 0x000081C8
		public override string SortKey()
		{
			return string.Concat(new string[]
			{
				base.GetType().Name,
				"!",
				this.m_regHelper.m_sKey,
				"!",
				this.m_regHelper.m_sValue
			}).ToLower();
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x0000A020 File Offset: 0x00008220
		public override void Evaluate(EffectiveStateSources sources, XmlDocument xOutputDoc)
		{
			this.m_regHelper.RetrieveRegistryValue(sources.hklm64);
			this.m_regHelper.EvaluateRegistryValue();
			this.m_regHelper.WriteRegPolResults(xOutputDoc, string.Empty, Environment.MachineName + " - " + sources.hklm64.Name);
		}

		// Token: 0x04000679 RID: 1657
		private RegistryItemHelper m_regHelper;
	}
}
