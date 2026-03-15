using System;
using System.Collections.Generic;
using System.Xml;

namespace SharedCode
{
	// Token: 0x02000040 RID: 64
	internal class SecTemplatePrivilegeRightsItem : SecTemplateItem
	{
		// Token: 0x060000C5 RID: 197 RVA: 0x0000A5C4 File Offset: 0x000087C4
		public SecTemplatePrivilegeRightsItem(XmlNode node)
		{
			base.ReadStdElems(node);
			base.ReadStdSecTemplateElems(node);
			string[] array = this.m_sLineItem.Split(new char[] { this.KeyValSeparator() });
			this.m_sPrivName = array[0].Trim();
			this.m_sBaselineValue = array[1].Trim();
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00002D4F File Offset: 0x00000F4F
		internal override char MultiValueSeparator()
		{
			return ',';
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00002D53 File Offset: 0x00000F53
		public override string SortKey()
		{
			return (base.GetType().Name + "!" + this.m_sPrivName).ToLower();
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00002D75 File Offset: 0x00000F75
		public override void Evaluate(EffectiveStateSources sources, XmlDocument xOutputDoc)
		{
			this.RetrieveAndCompare(sources);
			base.ImplementIgnoreAlwaysPass();
			base.WriteSecTemplateResults(xOutputDoc);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x0000A61C File Offset: 0x0000881C
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
			SecTemplatePrivilegeRightsItem secTemplatePrivilegeRightsItem = new SecTemplatePrivilegeRightsItem(nodeFromSeceditAndAuditpolState);
			this.m_bValueFound = true;
			this.m_sObservedLineItem = secTemplatePrivilegeRightsItem.m_sLineItem;
			this.m_sObservedPolicyName = secTemplatePrivilegeRightsItem.m_sGpoName;
			string text = this.m_sBaselineValue.ToLower().Trim();
			string text2 = secTemplatePrivilegeRightsItem.m_sBaselineValue.ToLower().Trim();
			SortedSet<string> sortedSet = new SortedSet<string>();
			SortedSet<string> sortedSet2 = new SortedSet<string>();
			string[] array = text.Split(new char[] { this.MultiValueSeparator() });
			for (int i = 0; i < array.Length; i++)
			{
				string text3 = array[i].Trim();
				if (!sortedSet.Contains(text3))
				{
					sortedSet.Add(text3);
				}
			}
			array = text2.Split(new char[] { this.MultiValueSeparator() });
			for (int i = 0; i < array.Length; i++)
			{
				string text4 = array[i].Trim();
				if (!sortedSet2.Contains(text4))
				{
					sortedSet2.Add(text4);
				}
			}
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
								this.m_bEvalPassed = sortedSet.IsSubsetOf(sortedSet2);
								return;
							}
						}
					}
					else if (sEvalOperator == "NotEqual")
					{
						this.m_bInvalidComparison = true;
						return;
					}
				}
				else if (sEvalOperator == "Equal")
				{
					this.m_bEvalPassed = sortedSet.SetEquals(sortedSet2);
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
					this.m_bEvalPassed = sortedSet.IsSupersetOf(sortedSet2);
					return;
				}
			}
			else if (num != 2735859570U)
			{
				if (num == 3902070617U)
				{
					if (sEvalOperator == "OneOfThese")
					{
						this.m_bInvalidComparison = true;
						return;
					}
				}
			}
			else if (sEvalOperator == "Range")
			{
				this.m_bInvalidComparison = true;
				return;
			}
			this.m_bInvalidComparison = true;
		}

		// Token: 0x04000680 RID: 1664
		internal string m_sPrivName;
	}
}
