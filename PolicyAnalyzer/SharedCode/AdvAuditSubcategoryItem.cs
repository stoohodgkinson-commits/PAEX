using System;
using System.Xml;

namespace SharedCode
{
	// Token: 0x02000046 RID: 70
	internal class AdvAuditSubcategoryItem : BaselineItem
	{
		// Token: 0x060000DF RID: 223 RVA: 0x0000B1F4 File Offset: 0x000093F4
		public AdvAuditSubcategoryItem(XmlNode node)
		{
			base.ReadStdElems(node);
			this.m_sGUID = node["GUID"].InnerText.Trim();
			this.m_sName = node["Name"].InnerText.Trim();
			this.m_sBaselineValue = node["Setting"].InnerText.Trim();
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00002E80 File Offset: 0x00001080
		public override string SortKey()
		{
			return (base.GetType().Name + "!" + this.m_sGUID).ToLower();
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00002EA2 File Offset: 0x000010A2
		public override void Evaluate(EffectiveStateSources sources, XmlDocument xOutputDoc)
		{
			this.RetrieveAndCompare(sources);
			base.ImplementIgnoreAlwaysPass();
			this.WriteAdvAuditSubcatResults(xOutputDoc);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x0000B278 File Offset: 0x00009478
		private XmlNode GetNodeFromSeceditAndAuditpolState(EffectiveStateSources sources)
		{
			if (sources.xmlSeceditAndAuditpolState == null)
			{
				return null;
			}
			string text = string.Concat(new string[]
			{
				"//AuditSubcategory[GUID='",
				this.m_sGUID.ToLower(),
				"' or GUID='",
				this.m_sGUID.ToUpper(),
				"']"
			});
			return sources.xmlSeceditAndAuditpolState.SelectSingleNode(text);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x0000B2DC File Offset: 0x000094DC
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
			AdvAuditSubcategoryItem advAuditSubcategoryItem = new AdvAuditSubcategoryItem(nodeFromSeceditAndAuditpolState);
			this.m_bValueFound = true;
			this.m_sObservedValue = advAuditSubcategoryItem.m_sBaselineValue;
			this.m_sObservedPolicyName = advAuditSubcategoryItem.m_sGpoName;
			uint num;
			uint num2;
			if (!uint.TryParse(this.m_sBaselineValue, out num) || !uint.TryParse(this.m_sObservedValue, out num2))
			{
				this.m_bInvalidComparison = true;
				return;
			}
			string sEvalOperator = this.m_sEvalOperator;
			uint num3 = <PrivateImplementationDetails>.ComputeStringHash(sEvalOperator);
			if (num3 <= 1519117449U)
			{
				if (num3 != 24087439U)
				{
					if (num3 != 685255070U)
					{
						if (num3 == 1519117449U)
						{
							if (sEvalOperator == "GreaterThanOrEqual")
							{
								this.m_bEvalPassed = (num & num2) == num;
								return;
							}
						}
					}
					else if (sEvalOperator == "NotEqual")
					{
						this.m_bEvalPassed = num != num2;
						return;
					}
				}
				else if (sEvalOperator == "Equal")
				{
					this.m_bEvalPassed = num == num2;
					return;
				}
			}
			else if (num3 <= 2543884473U)
			{
				if (num3 != 2007843446U)
				{
					if (num3 == 2543884473U)
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
					this.m_bEvalPassed = (num | num2) == num;
					return;
				}
			}
			else if (num3 != 2735859570U)
			{
				if (num3 == 3902070617U)
				{
					if (sEvalOperator == "OneOfThese")
					{
						foreach (string text in this.m_sEvalValues.ToLower().Split(new char[] { ',' }))
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

		// Token: 0x060000E4 RID: 228 RVA: 0x0000B4F0 File Offset: 0x000096F0
		private void WriteAdvAuditSubcatResults(XmlDocument xOutputDoc)
		{
			XmlElement xmlElement = xOutputDoc.CreateElement("AuditSubcategory");
			XmlElement xmlElement2 = xOutputDoc.CreateElement("GUID");
			XmlElement xmlElement3 = xOutputDoc.CreateElement("Name");
			XmlElement xmlElement4 = xOutputDoc.CreateElement("Setting");
			XmlElement xmlElement5 = xOutputDoc.CreateElement("PolicyName");
			xmlElement2.InnerText = this.m_sGUID;
			xmlElement3.InnerText = this.m_sName;
			xmlElement4.InnerText = this.m_sObservedValue;
			xmlElement5.InnerText = this.m_sObservedPolicyName;
			xmlElement.AppendChild(xmlElement2);
			xmlElement.AppendChild(xmlElement3);
			xmlElement.AppendChild(xmlElement4);
			xmlElement.AppendChild(xmlElement5);
			base.CreateResultsElements(xOutputDoc, xmlElement);
			xOutputDoc.DocumentElement.AppendChild(xmlElement);
		}

		// Token: 0x0400068E RID: 1678
		internal string m_sGUID;

		// Token: 0x0400068F RID: 1679
		internal string m_sName;

		// Token: 0x04000690 RID: 1680
		internal string m_sObservedValue = string.Empty;

		// Token: 0x04000691 RID: 1681
		internal string m_sObservedPolicyName = string.Empty;
	}
}
