using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace SharedCode
{
	// Token: 0x02000039 RID: 57
	public class BaselineEvaluator
	{
		// Token: 0x060000A3 RID: 163 RVA: 0x00002C2F File Offset: 0x00000E2F
		private void AddIfNew(BaselineItem item)
		{
			if (!this.baselineItems.ContainsKey(item.SortKey()))
			{
				this.baselineItems.Add(item.SortKey(), item);
			}
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00008970 File Offset: 0x00006B70
		public bool AddBaselineDoc(string sBaselinePath, out string sDiagnostics)
		{
			sDiagnostics = string.Empty;
			XmlDocument xmlDocument = new XmlDocument();
			try
			{
				xmlDocument.Load(sBaselinePath);
			}
			catch (Exception ex)
			{
				sDiagnostics = "Error loading " + sBaselinePath + ": " + ex.Message;
				return false;
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (object obj in xmlDocument.DocumentElement.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				try
				{
					string localName = xmlNode.LocalName;
					uint num = <PrivateImplementationDetails>.ComputeStringHash(localName);
					if (num <= 2563008617U)
					{
						if (num <= 2040575466U)
						{
							if (num != 1313566486U)
							{
								if (num != 2040575466U)
								{
									goto IL_047E;
								}
								if (!(localName == "CSE-User"))
								{
									goto IL_047E;
								}
							}
							else
							{
								if (!(localName == "ComputerConfig"))
								{
									goto IL_047E;
								}
								this.AddIfNew(new ComputerRegistryPolItem(xmlNode));
								continue;
							}
						}
						else if (num != 2180282844U)
						{
							if (num != 2563008617U)
							{
								goto IL_047E;
							}
							if (!(localName == "SecurityTemplate"))
							{
								goto IL_047E;
							}
							this.bNeedSecedit = true;
							string value = xmlNode.Attributes["Section"].Value;
							num = <PrivateImplementationDetails>.ComputeStringHash(value);
							if (num <= 1048552247U)
							{
								if (num <= 647411852U)
								{
									if (num != 78776086U)
									{
										if (num != 163014704U)
										{
											if (num != 647411852U)
											{
												goto IL_0413;
											}
											if (!(value == "Group Membership"))
											{
												goto IL_0413;
											}
											this.AddIfNew(new SecTemplateGroupMembershipItem(xmlNode));
											continue;
										}
										else if (!(value == "Event Audit"))
										{
											goto IL_0413;
										}
									}
									else
									{
										if (!(value == "Registry Keys"))
										{
											goto IL_0413;
										}
										this.AddIfNew(new SecTemplateRegistryKeysItem(xmlNode));
										continue;
									}
								}
								else if (num != 703556838U)
								{
									if (num != 721296480U)
									{
										if (num != 1048552247U)
										{
											goto IL_0413;
										}
										if (!(value == "Privilege Rights"))
										{
											goto IL_0413;
										}
										this.AddIfNew(new SecTemplatePrivilegeRightsItem(xmlNode));
										continue;
									}
									else
									{
										if (!(value == "Registry Values"))
										{
											goto IL_0413;
										}
										this.AddIfNew(new SecTemplateRegistryItem(xmlNode));
										continue;
									}
								}
								else if (!(value == "System Log"))
								{
									goto IL_0413;
								}
							}
							else if (num <= 2127435421U)
							{
								if (num != 1203692920U)
								{
									if (num != 1963923791U)
									{
										if (num != 2127435421U)
										{
											goto IL_0413;
										}
										if (!(value == "Security Log"))
										{
											goto IL_0413;
										}
									}
									else if (!(value == "Application Log"))
									{
										goto IL_0413;
									}
								}
								else
								{
									if (!(value == "Service General Setting"))
									{
										goto IL_0413;
									}
									this.AddIfNew(new SecTemplateServiceGeneralSettingItem(xmlNode));
									continue;
								}
							}
							else if (num != 2954938362U)
							{
								if (num != 3655186023U)
								{
									if (num != 4027320590U)
									{
										goto IL_0413;
									}
									if (!(value == "System Access"))
									{
										goto IL_0413;
									}
								}
								else
								{
									if (!(value == "File Security"))
									{
										goto IL_0413;
									}
									this.AddIfNew(new SecTemplateFileSecurityItem(xmlNode));
									continue;
								}
							}
							else if (!(value == "Kerberos Policy"))
							{
								goto IL_0413;
							}
							this.AddIfNew(new SecTemplateSimpleValueItem(xmlNode));
							continue;
							IL_0413:
							stringBuilder.AppendLine("Unrecognized security template section: " + xmlNode.OuterXml);
							continue;
						}
						else
						{
							if (!(localName == "UserConfig"))
							{
								goto IL_047E;
							}
							this.AddIfNew(new UserRegistryPolItem(xmlNode));
							continue;
						}
					}
					else if (num <= 2855792905U)
					{
						if (num != 2750572210U)
						{
							if (num != 2855792905U)
							{
								goto IL_047E;
							}
							if (!(localName == "GlobalAudit"))
							{
								goto IL_047E;
							}
							this.bNeedAuditpol = true;
							this.AddIfNew(new AdvAuditGlobalAuditItem(xmlNode));
							continue;
						}
						else
						{
							if (!(localName == "AuditSubcategory"))
							{
								goto IL_047E;
							}
							this.bNeedAuditpol = true;
							this.AddIfNew(new AdvAuditSubcategoryItem(xmlNode));
							continue;
						}
					}
					else if (num != 2988077430U)
					{
						if (num != 4142000321U)
						{
							goto IL_047E;
						}
						if (!(localName == "AuditOption"))
						{
							goto IL_047E;
						}
						this.bNeedAuditpol = true;
						this.AddIfNew(new AdvAuditAuditOptionItem(xmlNode));
						continue;
					}
					else if (!(localName == "CSE-Machine"))
					{
						goto IL_047E;
					}
					this.AddIfNew(new ClientSideExtensionItem(xmlNode));
					continue;
					IL_047E:
					stringBuilder.AppendLine("Unrecognized PolicyRules node: " + xmlNode.OuterXml);
				}
				catch (Exception ex2)
				{
					stringBuilder.Append("Exception: " + ex2.Message);
				}
			}
			if (stringBuilder.Length > 0)
			{
				sDiagnostics = "Warnings while processing " + sBaselinePath + ":\r\n" + stringBuilder.ToString();
			}
			return true;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00008EC4 File Offset: 0x000070C4
		public bool EvaluateEffectiveState(string sOutputRulesPath, out string sDiagnostics)
		{
			bool flag = true;
			sDiagnostics = string.Empty;
			StringBuilder stringBuilder = new StringBuilder();
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml("<PolicyRules/>");
			string text;
			EffectiveStateSources effectiveStateSources = new EffectiveStateSources(this.bNeedSecedit || this.bNeedAuditpol, out text);
			if (text.Length > 0)
			{
				stringBuilder.AppendLine("EffectiveStateSources reports: " + text);
			}
			foreach (BaselineItem baselineItem in this.baselineItems.Values)
			{
				try
				{
					baselineItem.Evaluate(effectiveStateSources, xmlDocument);
				}
				catch (Exception ex)
				{
					stringBuilder.AppendLine("Exception evaluating " + baselineItem.GetType().FullName + ": " + ex.Message);
				}
			}
			try
			{
				xmlDocument.Save(sOutputRulesPath);
			}
			catch (Exception ex2)
			{
				stringBuilder.AppendLine("Could not save to " + sOutputRulesPath + ": " + ex2.Message);
				flag = false;
			}
			sDiagnostics = stringBuilder.ToString();
			return flag;
		}

		// Token: 0x04000665 RID: 1637
		private bool bNeedSecedit;

		// Token: 0x04000666 RID: 1638
		private bool bNeedAuditpol;

		// Token: 0x04000667 RID: 1639
		private SortedList<string, BaselineItem> baselineItems = new SortedList<string, BaselineItem>();
	}
}
