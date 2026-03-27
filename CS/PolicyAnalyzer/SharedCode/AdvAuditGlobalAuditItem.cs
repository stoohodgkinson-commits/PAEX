using System;
using System.Xml;

namespace SharedCode
{
	// Token: 0x02000047 RID: 71
	internal class AdvAuditGlobalAuditItem : BaselineItem
	{
		// Token: 0x060000E5 RID: 229 RVA: 0x0000B5A4 File Offset: 0x000097A4
		public AdvAuditGlobalAuditItem(XmlNode node)
		{
			base.ReadStdElems(node);
			this.m_sSaclType = node["Type"].InnerText;
			this.m_sBaselineValue = node["SACL"].InnerText;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00002EB9 File Offset: 0x000010B9
		public override string SortKey()
		{
			return (base.GetType().Name + "!" + this.m_sSaclType).ToLower();
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00002EDB File Offset: 0x000010DB
		public override void Evaluate(EffectiveStateSources sources, XmlDocument xOutputDoc)
		{
			this.RetrieveAndCompare(sources);
			base.ImplementIgnoreAlwaysPass();
			this.WriteAdvAuditGlobalAuditResults(xOutputDoc);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x0000B600 File Offset: 0x00009800
		private XmlNode GetNodeFromSeceditAndAuditpolState(EffectiveStateSources sources)
		{
			if (sources.xmlSeceditAndAuditpolState == null)
			{
				return null;
			}
			string text = "//GlobalAudit[Type='" + this.m_sSaclType + "']";
			return sources.xmlSeceditAndAuditpolState.SelectSingleNode(text);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x0000B63C File Offset: 0x0000983C
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
			AdvAuditGlobalAuditItem advAuditGlobalAuditItem = new AdvAuditGlobalAuditItem(nodeFromSeceditAndAuditpolState);
			this.m_bValueFound = true;
			this.m_sObservedValue = advAuditGlobalAuditItem.m_sBaselineValue;
			this.m_sObservedPolicyName = advAuditGlobalAuditItem.m_sGpoName;
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

		// Token: 0x060000EA RID: 234 RVA: 0x0000B820 File Offset: 0x00009A20
		private void WriteAdvAuditGlobalAuditResults(XmlDocument xOutputDoc)
		{
			XmlElement xmlElement = xOutputDoc.CreateElement("GlobalAudit");
			XmlElement xmlElement2 = xOutputDoc.CreateElement("Type");
			XmlElement xmlElement3 = xOutputDoc.CreateElement("SACL");
			XmlElement xmlElement4 = xOutputDoc.CreateElement("PolicyName");
			xmlElement2.InnerText = this.m_sSaclType;
			xmlElement3.InnerText = this.m_sObservedValue;
			xmlElement4.InnerText = this.m_sObservedPolicyName;
			xmlElement.AppendChild(xmlElement2);
			xmlElement.AppendChild(xmlElement3);
			xmlElement.AppendChild(xmlElement4);
			base.CreateResultsElements(xOutputDoc, xmlElement);
			xOutputDoc.DocumentElement.AppendChild(xmlElement);
		}

		// Token: 0x04000692 RID: 1682
		internal string m_sSaclType;

		// Token: 0x04000693 RID: 1683
		internal string m_sObservedValue = string.Empty;

		// Token: 0x04000694 RID: 1684
		internal string m_sObservedPolicyName = string.Empty;
	}
}
