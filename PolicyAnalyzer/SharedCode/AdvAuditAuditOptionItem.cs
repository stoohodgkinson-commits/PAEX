using System;
using System.Xml;

namespace SharedCode
{
	// Token: 0x02000048 RID: 72
	internal class AdvAuditAuditOptionItem : BaselineItem
	{
		// Token: 0x060000EB RID: 235 RVA: 0x0000B8B0 File Offset: 0x00009AB0
		public AdvAuditAuditOptionItem(XmlNode node)
		{
			base.ReadStdElems(node);
			this.m_sOption = node["Option"].InnerText;
			this.m_sBaselineValue = node["Setting"].InnerText;
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00002EF2 File Offset: 0x000010F2
		public override string SortKey()
		{
			return (base.GetType().Name + "!" + this.m_sOption).ToLower();
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00002F14 File Offset: 0x00001114
		public override void Evaluate(EffectiveStateSources sources, XmlDocument xOutputDoc)
		{
			this.RetrieveAndCompare(sources);
			base.ImplementIgnoreAlwaysPass();
			this.WriteAdvAuditAuditOptionResults(xOutputDoc);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x0000B90C File Offset: 0x00009B0C
		private XmlNode GetNodeFromSeceditAndAuditpolState(EffectiveStateSources sources)
		{
			if (sources.xmlSeceditAndAuditpolState == null)
			{
				return null;
			}
			string text = "//AuditOption[Option='" + this.m_sOption + "']";
			return sources.xmlSeceditAndAuditpolState.SelectSingleNode(text);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x0000B948 File Offset: 0x00009B48
		private void RetrieveAndCompare(EffectiveStateSources sources)
		{
			this.m_bValueFound = false;
			this.m_bEvalPassed = false;
			this.m_bInvalidComparison = false;
			XmlNode nodeFromSeceditAndAuditpolState = this.GetNodeFromSeceditAndAuditpolState(sources);
			if (nodeFromSeceditAndAuditpolState == null)
			{
				return;
			}
			AdvAuditAuditOptionItem advAuditAuditOptionItem = new AdvAuditAuditOptionItem(nodeFromSeceditAndAuditpolState);
			this.m_bValueFound = true;
			this.m_sObservedValue = advAuditAuditOptionItem.m_sBaselineValue;
			this.m_sObservedPolicyName = advAuditAuditOptionItem.m_sGpoName;
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
								this.m_bInvalidComparison = true;
								return;
							}
						}
					}
					else if (sEvalOperator == "NotEqual")
					{
						this.m_bEvalPassed = this.m_sBaselineValue != this.m_sObservedValue;
						return;
					}
				}
				else if (sEvalOperator == "Equal")
				{
					this.m_bEvalPassed = this.m_sBaselineValue == this.m_sObservedValue;
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
						foreach (string text in this.m_sEvalValues.Split(new char[] { ',' }))
						{
							if (this.m_sObservedValue == text.Trim())
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
				return;
			}
			this.m_bInvalidComparison = true;
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x0000BB2C File Offset: 0x00009D2C
		private void WriteAdvAuditAuditOptionResults(XmlDocument xOutputDoc)
		{
			XmlElement xmlElement = xOutputDoc.CreateElement("AuditOption");
			XmlElement xmlElement2 = xOutputDoc.CreateElement("Option");
			XmlElement xmlElement3 = xOutputDoc.CreateElement("Setting");
			XmlElement xmlElement4 = xOutputDoc.CreateElement("PolicyName");
			xmlElement2.InnerText = this.m_sOption;
			xmlElement3.InnerText = this.m_sObservedValue;
			xmlElement4.InnerText = this.m_sObservedPolicyName;
			xmlElement.AppendChild(xmlElement2);
			xmlElement.AppendChild(xmlElement3);
			xmlElement.AppendChild(xmlElement4);
			base.CreateResultsElements(xOutputDoc, xmlElement);
			xOutputDoc.DocumentElement.AppendChild(xmlElement);
		}

		// Token: 0x04000695 RID: 1685
		internal string m_sOption;

		// Token: 0x04000696 RID: 1686
		internal string m_sObservedValue = string.Empty;

		// Token: 0x04000697 RID: 1687
		internal string m_sObservedPolicyName = string.Empty;
	}
}
