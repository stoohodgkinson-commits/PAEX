using System;
using System.Xml;

namespace SharedCode
{
	// Token: 0x02000045 RID: 69
	internal class SecTemplateSimpleValueItem : SecTemplateItem
	{
		// Token: 0x060000DB RID: 219 RVA: 0x0000ADD0 File Offset: 0x00008FD0
		public SecTemplateSimpleValueItem(XmlNode node)
		{
			base.ReadStdElems(node);
			base.ReadStdSecTemplateElems(node);
			string[] array = this.m_sLineItem.Split(new char[] { this.KeyValSeparator() });
			this.m_sPolicy = array[0].Trim();
			this.m_sBaselineValue = array[1].Trim();
			this.m_bStringVal = this.m_sBaselineValue[0] == '"';
			if (this.m_bStringVal)
			{
				this.m_sBaselineValue = this.m_sBaselineValue.Trim(new char[] { '"' });
			}
		}

		// Token: 0x060000DC RID: 220 RVA: 0x0000AE64 File Offset: 0x00009064
		public override string SortKey()
		{
			return string.Concat(new string[]
			{
				base.GetType().Name,
				"!",
				this.m_sSection,
				"!",
				this.m_sPolicy
			}).ToLower();
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00002E69 File Offset: 0x00001069
		public override void Evaluate(EffectiveStateSources sources, XmlDocument xOutputDoc)
		{
			this.RetrieveAndCompare(sources);
			base.ImplementIgnoreAlwaysPass();
			base.WriteSecTemplateResults(xOutputDoc);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x0000AEB4 File Offset: 0x000090B4
		private void RetrieveAndCompare(EffectiveStateSources sources)
		{
			this.m_bValueFound = false;
			this.m_bEvalPassed = false;
			this.m_bInvalidComparison = false;
			XmlNode nodeFromSeceditAndAuditpolState = base.GetNodeFromSeceditAndAuditpolState(sources);
			if (nodeFromSeceditAndAuditpolState == null)
			{
				return;
			}
			SecTemplateSimpleValueItem secTemplateSimpleValueItem = new SecTemplateSimpleValueItem(nodeFromSeceditAndAuditpolState);
			this.m_bValueFound = true;
			this.m_sObservedLineItem = secTemplateSimpleValueItem.m_sLineItem;
			this.m_sObservedPolicyName = secTemplateSimpleValueItem.m_sGpoName;
			string text = this.m_sBaselineValue.ToLower().Trim();
			string text2 = secTemplateSimpleValueItem.m_sBaselineValue.ToLower().Trim();
			string sEvalOperator = this.m_sEvalOperator;
			uint num = <PrivateImplementationDetails>.ComputeStringHash(sEvalOperator);
			if (num <= 1519117449U)
			{
				if (num != 24087439U)
				{
					if (num != 685255070U)
					{
						if (num == 1519117449U)
						{
							if (sEvalOperator == "GreaterThanOrEqual")
							{
								uint num2;
								uint num3;
								if (!this.m_bStringVal && uint.TryParse(text, out num2) && uint.TryParse(text2, out num3))
								{
									this.m_bEvalPassed = num3 >= num2;
									return;
								}
								this.m_bInvalidComparison = true;
								return;
							}
						}
					}
					else if (sEvalOperator == "NotEqual")
					{
						this.m_bEvalPassed = text != text2;
						return;
					}
				}
				else if (sEvalOperator == "Equal")
				{
					this.m_bEvalPassed = text == text2;
					return;
				}
			}
			else if (num <= 2543884473U)
			{
				if (num != 2007843446U)
				{
					if (num == 2543884473U)
					{
						if (sEvalOperator == "NonNull")
						{
							this.m_bEvalPassed = true;
							return;
						}
					}
				}
				else if (sEvalOperator == "LessThanOrEqual")
				{
					uint num2;
					uint num3;
					if (!this.m_bStringVal && uint.TryParse(text, out num2) && uint.TryParse(text2, out num3))
					{
						this.m_bEvalPassed = num3 <= num2;
						return;
					}
					this.m_bInvalidComparison = true;
					return;
				}
			}
			else if (num != 2735859570U)
			{
				if (num == 3902070617U)
				{
					if (sEvalOperator == "OneOfThese")
					{
						foreach (string text3 in this.m_sEvalValues.ToLower().Split(new char[] { ',' }))
						{
							if (text2 == text3.Trim())
							{
								this.m_bEvalPassed = true;
								return;
							}
						}
						return;
					}
				}
			}
			else if (sEvalOperator == "Range")
			{
				this.m_bInvalidComparison = true;
				if (this.m_bStringVal)
				{
					return;
				}
				string[] array2;
				if (this.m_sEvalValues.Contains("-"))
				{
					array2 = this.m_sEvalValues.Split(new char[] { '-' });
				}
				else if (this.m_sEvalValues.Contains(","))
				{
					array2 = this.m_sEvalValues.Split(new char[] { ',' });
				}
				else
				{
					array2 = this.m_sEvalValues.Split(" ".ToCharArray(), 2);
				}
				if (array2.Length != 2)
				{
					return;
				}
				array2[0] = array2[0].Trim();
				array2[1] = array2[1].Trim();
				uint num3;
				uint num4;
				uint num5;
				if (!uint.TryParse(text2, out num3) || !uint.TryParse(array2[0], out num4) || !uint.TryParse(array2[1], out num5))
				{
					return;
				}
				this.m_bInvalidComparison = false;
				if (num4 <= num3 && num3 <= num5)
				{
					this.m_bEvalPassed = true;
					return;
				}
				return;
			}
			this.m_bInvalidComparison = true;
		}

		// Token: 0x0400068C RID: 1676
		internal string m_sPolicy;

		// Token: 0x0400068D RID: 1677
		internal bool m_bStringVal;
	}
}
