using System;
using System.Xml;

namespace SharedCode
{
	// Token: 0x02000041 RID: 65
	internal class SecTemplateGroupMembershipItem : SecTemplateItem
	{
		// Token: 0x060000CA RID: 202 RVA: 0x0000A870 File Offset: 0x00008A70
		public SecTemplateGroupMembershipItem(XmlNode node)
		{
			base.ReadStdElems(node);
			base.ReadStdSecTemplateElems(node);
			string[] array = this.m_sLineItem.Split(new char[] { this.KeyValSeparator() });
			this.m_sGroupSpec = array[0].Trim();
			this.m_sBaselineValue = array[1].Trim();
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00002D8C File Offset: 0x00000F8C
		public override string SortKey()
		{
			return (base.GetType().Name + "!" + this.m_sGroupSpec).ToLower();
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00002DAE File Offset: 0x00000FAE
		public override void Evaluate(EffectiveStateSources sources, XmlDocument xOutputDoc)
		{
			throw new NotImplementedException("SecTemplateGroupMembershipItem evaluation not implemented");
		}

		// Token: 0x04000681 RID: 1665
		internal string m_sGroupSpec;
	}
}
