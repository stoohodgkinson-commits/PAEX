using System;
using System.Xml;

namespace SharedCode
{
	// Token: 0x0200003F RID: 63
	internal class SecTemplateRegistryItem : SecTemplateItem
	{
		// Token: 0x060000C2 RID: 194 RVA: 0x0000A380 File Offset: 0x00008580
		public SecTemplateRegistryItem(XmlNode node)
		{
			this.m_regHelper = new RegistryItemHelper(this, "ComputerConfig");
			base.ReadStdElems(node);
			base.ReadStdSecTemplateElems(node);
			string[] array = this.m_sLineItem.Split(new char[] { this.KeyValSeparator() });
			string text = array[0].Substring(8);
			int num = text.LastIndexOf('\\');
			this.m_regHelper.m_sKey = text.Substring(0, num).Trim();
			this.m_regHelper.m_sValue = text.Substring(num + 1).Trim();
			string[] array2 = array[1].Split(new char[] { ',' }, 2);
			string text2 = array2[0];
			if (!(text2 == "1"))
			{
				if (!(text2 == "2"))
				{
					if (!(text2 == "3"))
					{
						if (!(text2 == "4"))
						{
							if (!(text2 == "7"))
							{
								if (!(text2 == "11"))
								{
									this.m_regHelper.m_sType = "regType " + array2[0];
								}
								else
								{
									this.m_regHelper.m_sType = "REG_QWORD";
								}
							}
							else
							{
								this.m_regHelper.m_sType = "REG_MULTI_SZ";
							}
						}
						else
						{
							this.m_regHelper.m_sType = "REG_DWORD";
						}
					}
					else
					{
						this.m_regHelper.m_sType = "REG_BINARY";
					}
				}
				else
				{
					this.m_regHelper.m_sType = "REG_EXPAND_SZ";
				}
			}
			else
			{
				this.m_regHelper.m_sType = "REG_SZ";
			}
			this.m_sBaselineValue = array2[1].Trim(new char[] { '"' });
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x0000A518 File Offset: 0x00008718
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

		// Token: 0x060000C4 RID: 196 RVA: 0x0000A570 File Offset: 0x00008770
		public override void Evaluate(EffectiveStateSources sources, XmlDocument xOutputDoc)
		{
			this.m_regHelper.RetrieveRegistryValue(sources.hklm64);
			this.m_regHelper.EvaluateRegistryValue();
			this.m_regHelper.WriteRegPolResults(xOutputDoc, string.Empty, Environment.MachineName + " - " + sources.hklm64.Name);
		}

		// Token: 0x0400067F RID: 1663
		private RegistryItemHelper m_regHelper;
	}
}
