using System;
using System.Xml;

namespace SharedCode
{
	// Token: 0x02000044 RID: 68
	internal class SecTemplateRegistryKeysItem : SecTemplateItem
	{
		// Token: 0x060000D7 RID: 215 RVA: 0x0000AD2C File Offset: 0x00008F2C
		public SecTemplateRegistryKeysItem(XmlNode node)
		{
			base.ReadStdElems(node);
			base.ReadStdSecTemplateElems(node);
			char[] array = new char[] { '"' };
			string[] array2 = this.m_sLineItem.Split(new char[] { this.KeyValSeparator() });
			this.m_sKeySpec = array2[0].Trim().Trim(array);
			this.m_sInheritance = array2[1].Trim();
			this.m_sSDDL = array2[2].Trim().Trim(array);
			this.m_sBaselineValue = this.m_sLineItem.Substring(this.m_sLineItem.IndexOf(this.KeyValSeparator()) + 1);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00002D4F File Offset: 0x00000F4F
		internal override char KeyValSeparator()
		{
			return ',';
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00002E3B File Offset: 0x0000103B
		public override string SortKey()
		{
			return (base.GetType().Name + "!" + this.m_sKeySpec).ToLower();
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00002E5D File Offset: 0x0000105D
		public override void Evaluate(EffectiveStateSources sources, XmlDocument xOutputDoc)
		{
			throw new NotImplementedException("SecTemplateRegistryKeysItem evaluation not implemented");
		}

		// Token: 0x04000689 RID: 1673
		internal string m_sKeySpec;

		// Token: 0x0400068A RID: 1674
		internal string m_sInheritance;

		// Token: 0x0400068B RID: 1675
		internal string m_sSDDL;
	}
}
