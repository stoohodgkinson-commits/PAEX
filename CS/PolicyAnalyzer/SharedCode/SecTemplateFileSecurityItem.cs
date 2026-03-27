using System;
using System.Xml;

namespace SharedCode
{
	// Token: 0x02000043 RID: 67
	internal class SecTemplateFileSecurityItem : SecTemplateItem
	{
		// Token: 0x060000D3 RID: 211 RVA: 0x0000AC88 File Offset: 0x00008E88
		public SecTemplateFileSecurityItem(XmlNode node)
		{
			base.ReadStdElems(node);
			base.ReadStdSecTemplateElems(node);
			char[] array = new char[] { '"' };
			string[] array2 = this.m_sLineItem.Split(new char[] { this.KeyValSeparator() });
			this.m_sFileSpec = array2[0].Trim().Trim(array);
			this.m_sInheritance = array2[1].Trim();
			this.m_sSDDL = array2[2].Trim().Trim(array);
			this.m_sBaselineValue = this.m_sLineItem.Substring(this.m_sLineItem.IndexOf(this.KeyValSeparator()) + 1);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00002D4F File Offset: 0x00000F4F
		internal override char KeyValSeparator()
		{
			return ',';
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00002E0D File Offset: 0x0000100D
		public override string SortKey()
		{
			return (base.GetType().Name + "!" + this.m_sFileSpec).ToLower();
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00002E2F File Offset: 0x0000102F
		public override void Evaluate(EffectiveStateSources sources, XmlDocument xOutputDoc)
		{
			throw new NotImplementedException("SecTemplateFileSecurityItem evaluation not implemented");
		}

		// Token: 0x04000686 RID: 1670
		internal string m_sFileSpec;

		// Token: 0x04000687 RID: 1671
		internal string m_sInheritance;

		// Token: 0x04000688 RID: 1672
		internal string m_sSDDL;
	}
}
