using System;
using System.Collections.Generic;
using System.Xml;
using GPLookup;

namespace PolicyAnalyzer
{
	// Token: 0x0200005F RID: 95
	public class PolicyRules
	{
		// Token: 0x06000156 RID: 342 RVA: 0x000031E5 File Offset: 0x000013E5
		public static string DefaultExtension()
		{
			return "PolicyRules";
		}

		// Token: 0x06000157 RID: 343 RVA: 0x000031EC File Offset: 0x000013EC
		private void AddGpoName(string sGpoName)
		{
			if (!this.m_GpoList.ContainsKey(sGpoName))
			{
				this.m_GpoList.Add(sGpoName, true);
			}
		}

		// Token: 0x06000158 RID: 344 RVA: 0x0000EFF0 File Offset: 0x0000D1F0
		private bool ShowGpo(string sGpoName)
		{
			bool flag;
			return !this.m_GpoList.TryGetValue(sGpoName, ref flag) || flag;
		}

		// Token: 0x06000159 RID: 345 RVA: 0x0000F010 File Offset: 0x0000D210
		public PolicyRules(GPLookup_t gpLookup, PolicyRules.OverrideBehavior_t overrideBehavior = PolicyRules.OverrideBehavior_t.eMultipleSettingsAllowed)
		{
			this.m_GpoList = new SortedList<string, bool>();
			this.m_gpLookup = gpLookup;
			this.m_sFilePaths = new List<string>();
			this.m_OverrideBehavior = overrideBehavior;
			this.m_MachineCSEs = new SortedList<string, string>();
			this.m_UserCSEs = new SortedList<string, string>();
		}

		// Token: 0x0600015A RID: 346 RVA: 0x0000F060 File Offset: 0x0000D260
		private void AddRegPolicyItem(string config, string keyOrGroup, string value, string dataType, string settingData, string srcFile, string gpoName)
		{
			if (!this.m_bInReload)
			{
				this.AddGpoName(gpoName);
			}
			else if (!this.ShowGpo(gpoName))
			{
				return;
			}
			bool flag = false;
			bool flag2 = false;
			if (value.ToLower().StartsWith("**delvals"))
			{
				flag = true;
			}
			else if (value.ToLower().StartsWith("**del."))
			{
				flag2 = true;
			}
			char c = '\0';
			if ("REG_MULTI_SZ" == dataType)
			{
				if (settingData.EndsWith("\\0"))
				{
					settingData = settingData.Substring(0, settingData.Length - 2);
				}
				settingData = settingData.Replace("\\0", ",");
				if ("LegalNoticeText" != value)
				{
					c = ',';
				}
			}
			else if (keyOrGroup.ToLower() == "SOFTWARE\\Policies\\Microsoft\\WindowsFirewall\\ConSecRules".ToLower())
			{
				c = '|';
			}
			else if (keyOrGroup.ToLower() == "Software\\Policies\\Microsoft\\Windows\\NetworkProvider\\HardenedPaths".ToLower())
			{
				c = ',';
			}
			PolicyRules.PolicySettingValue_t.eSpecialContent_t eSpecialContent_t = PolicyRules.PolicySettingValue_t.eSpecialContent_t.esc_None;
			if (keyOrGroup.ToLower() == "system\\currentcontrolset\\control\\lsa" && value.ToLower() == "restrictremotesam")
			{
				eSpecialContent_t = PolicyRules.PolicySettingValue_t.eSpecialContent_t.esc_SDDL;
			}
			PolicyRules.PolicyItem_t policyItem_t;
			if (!this.m_PolicyItems.TryGetValue(PolicyRules.PolicyItem_t.SortKey(config, keyOrGroup), ref policyItem_t))
			{
				PolicyRules.PolicyItem_t policyItem_t2 = new PolicyRules.PolicyItem_t(config, keyOrGroup);
				if (flag)
				{
					policyItem_t2.AddDelAllValuesCommand(srcFile, gpoName);
				}
				else if (flag2)
				{
					policyItem_t2.AddDelPolicyValue(value.Substring(6), srcFile, gpoName);
				}
				else
				{
					policyItem_t2.AddPolicyValue(value, dataType, settingData, c, srcFile, gpoName, eSpecialContent_t);
				}
				this.m_PolicyItems.Add(policyItem_t2.SortKey(), policyItem_t2);
				return;
			}
			if (flag)
			{
				policyItem_t.AddDelAllValuesCommand(srcFile, gpoName);
				return;
			}
			if (flag2)
			{
				policyItem_t.AddDelPolicyValue(value.Substring(6), srcFile, gpoName);
				return;
			}
			if (this.m_OverrideBehavior == PolicyRules.OverrideBehavior_t.eNewestOverridesPrevious)
			{
				policyItem_t.ReplacePolicyValue(value, dataType, settingData, c, srcFile, gpoName, eSpecialContent_t);
				return;
			}
			policyItem_t.AddPolicyValue(value, dataType, settingData, c, srcFile, gpoName, eSpecialContent_t);
		}

		// Token: 0x0600015B RID: 347 RVA: 0x0000F22C File Offset: 0x0000D42C
		private void AddSecurityTemplateItem(string sSection, string sLineItem, string sSourceFile, string sGpoName, char cKeyValSeparator, char cMultiValueSeparator, PolicyRules.PolicySettingValue_t.eSpecialContent_t content)
		{
			if (!this.m_bInReload)
			{
				this.AddGpoName(sGpoName);
			}
			else if (!this.ShowGpo(sGpoName))
			{
				return;
			}
			string[] array = sLineItem.Split(new char[] { cKeyValSeparator }, 2);
			string text = array[0].Trim();
			string text2;
			if (array.Length < 2)
			{
				text2 = "";
			}
			else if (cMultiValueSeparator == '\0')
			{
				text2 = array[1].Trim();
			}
			else
			{
				string[] array2 = array[1].Split(new char[] { cMultiValueSeparator });
				for (int i = 0; i < array2.Length; i++)
				{
					array2[i] = array2[i].Trim();
				}
				Array.Sort<string>(array2);
				text2 = string.Join(cMultiValueSeparator.ToString() ?? "", array2);
			}
			string text3 = text;
			string text4 = text2;
			PolicyRules.PolicyItem_t policyItem_t;
			if (!this.m_PolicyItems.TryGetValue(PolicyRules.PolicyItem_t.SortKey("Security Template", sSection), ref policyItem_t))
			{
				PolicyRules.PolicyItem_t policyItem_t2 = new PolicyRules.PolicyItem_t("Security Template", sSection);
				policyItem_t2.AddPolicyValue(text3, "", text4, cMultiValueSeparator, sSourceFile, sGpoName, content);
				this.m_PolicyItems.Add(policyItem_t2.SortKey(), policyItem_t2);
				return;
			}
			if (this.m_OverrideBehavior == PolicyRules.OverrideBehavior_t.eNewestOverridesPrevious)
			{
				policyItem_t.ReplacePolicyValue(text3, "", text4, cMultiValueSeparator, sSourceFile, sGpoName, content);
				return;
			}
			policyItem_t.AddPolicyValue(text3, "", text4, cMultiValueSeparator, sSourceFile, sGpoName, content);
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0000F380 File Offset: 0x0000D580
		private void AddAuditPolicyItem(string key, string value, string data, PolicyRules.PolicySettingValue_t.eSpecialContent_t content, string sSourceFile, string sGpoName)
		{
			if (!this.m_bInReload)
			{
				this.AddGpoName(sGpoName);
			}
			else if (!this.ShowGpo(sGpoName))
			{
				return;
			}
			PolicyRules.PolicyItem_t policyItem_t;
			if (!this.m_PolicyItems.TryGetValue(PolicyRules.PolicyItem_t.SortKey("Audit Policy", key), ref policyItem_t))
			{
				PolicyRules.PolicyItem_t policyItem_t2 = new PolicyRules.PolicyItem_t("Audit Policy", key);
				policyItem_t2.AddPolicyValue(value, "", data, '\0', sSourceFile, sGpoName, content);
				this.m_PolicyItems.Add(policyItem_t2.SortKey(), policyItem_t2);
				return;
			}
			if (this.m_OverrideBehavior == PolicyRules.OverrideBehavior_t.eNewestOverridesPrevious)
			{
				policyItem_t.ReplacePolicyValue(value, "", data, '\0', sSourceFile, sGpoName, content);
				return;
			}
			policyItem_t.AddPolicyValue(value, "", data, '\0', sSourceFile, sGpoName, content);
		}

		// Token: 0x0600015D RID: 349 RVA: 0x0000F430 File Offset: 0x0000D630
		private void AddAuditSubcategoryItem(string sGuid, string sName, string sSetting, string sSourceFile, string sGpoName)
		{
			GPLookup_t.AdvAuditData_t advAuditData_t;
			string sCategoryName;
			string text;
			if (this.m_gpLookup.AdvAuditLookupByGuid(sGuid, out advAuditData_t) && advAuditData_t.sSubcategoryName.Length > 0)
			{
				sCategoryName = advAuditData_t.sCategoryName;
				text = advAuditData_t.sSubcategoryName;
			}
			else
			{
				text = AuditPolText.GetSubcategoryName(sGuid, out sCategoryName);
			}
			if (text.Length == 0)
			{
				text = sName;
			}
			string text2 = AuditPolText.AuditSetting(sSetting);
			this.AddAuditPolicyItem(sCategoryName, text, text2, PolicyRules.PolicySettingValue_t.eSpecialContent_t.esc_None, sSourceFile, sGpoName);
		}

		// Token: 0x0600015E RID: 350 RVA: 0x0000F494 File Offset: 0x0000D694
		private void AddGlobalAuditItem(string sType, string sSACL, string sSourceFile, string sGpoName)
		{
			string text = "Global audit - " + sType;
			this.AddAuditPolicyItem(text, sType, sSACL, PolicyRules.PolicySettingValue_t.eSpecialContent_t.esc_SDDL, sSourceFile, sGpoName);
		}

		// Token: 0x0600015F RID: 351 RVA: 0x0000F4C0 File Offset: 0x0000D6C0
		private void AddAuditOptionItem(string sOption, string sSetting, string sSourceFile, string sGpoName)
		{
			string text = sOption.Replace(":", " - ");
			string text2 = AuditPolText.AuditOptionSetting(sSetting);
			this.AddAuditPolicyItem(text, sOption, text2, PolicyRules.PolicySettingValue_t.eSpecialContent_t.esc_None, sSourceFile, sGpoName);
		}

		// Token: 0x06000160 RID: 352 RVA: 0x0000F4F4 File Offset: 0x0000D6F4
		public bool Reload(out string sErrorText)
		{
			sErrorText = string.Empty;
			try
			{
				bool flag = false;
				this.m_bInReload = true;
				foreach (string text in this.m_sFilePaths)
				{
					if (!this.LoadFromFile(text, flag, out sErrorText))
					{
						return false;
					}
					flag = true;
				}
			}
			finally
			{
				this.m_bInReload = false;
			}
			return true;
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00003209 File Offset: 0x00001409
		private bool ValueNotFound(XmlNode node)
		{
			return this.OptionalInnerText(node, "ValueNotFound").ToLower() == "true";
		}

		// Token: 0x06000162 RID: 354 RVA: 0x0000F578 File Offset: 0x0000D778
		public bool LoadFromFile(string sFilePath, bool bAppend, out string sErrorText)
		{
			sErrorText = string.Empty;
			if (this.m_PolicyItems == null || !bAppend)
			{
				this.m_PolicyItems = new SortedList<string, PolicyRules.PolicyItem_t>();
			}
			if (!this.m_bInReload)
			{
				this.m_sFilePaths.Add(sFilePath);
			}
			try
			{
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.Load(sFilePath);
				foreach (object obj in xmlDocument.SelectNodes("//ComputerConfig"))
				{
					XmlNode xmlNode = (XmlNode)obj;
					if (!this.ValueNotFound(xmlNode))
					{
						this.AddRegPolicyItem("HKLM", xmlNode["Key"].InnerText, xmlNode["Value"].InnerText, xmlNode["RegType"].InnerText, xmlNode["RegData"].InnerText, this.OptionalInnerText(xmlNode, "SourceFile"), this.OptionalInnerText(xmlNode, "PolicyName"));
					}
				}
				foreach (object obj2 in xmlDocument.SelectNodes("//UserConfig"))
				{
					XmlNode xmlNode2 = (XmlNode)obj2;
					if (!this.ValueNotFound(xmlNode2))
					{
						this.AddRegPolicyItem("HKCU", xmlNode2["Key"].InnerText, xmlNode2["Value"].InnerText, xmlNode2["RegType"].InnerText, xmlNode2["RegData"].InnerText, this.OptionalInnerText(xmlNode2, "SourceFile"), this.OptionalInnerText(xmlNode2, "PolicyName"));
					}
				}
				foreach (object obj3 in xmlDocument.SelectNodes("//SecurityTemplate[@Section='Registry Values']"))
				{
					XmlNode xmlNode3 = (XmlNode)obj3;
					if (!this.ValueNotFound(xmlNode3))
					{
						string[] array = xmlNode3["LineItem"].InnerText.Split(new char[] { '=' });
						string[] array2 = array[1].Split(new char[] { ',' }, 2);
						string text = array[0].Substring(8);
						int num = text.LastIndexOf('\\');
						string text2 = text.Substring(0, num).Trim();
						string text3 = text.Substring(num + 1).Trim();
						string text4 = array2[0];
						string text5;
						if (!(text4 == "1"))
						{
							if (!(text4 == "2"))
							{
								if (!(text4 == "3"))
								{
									if (!(text4 == "4"))
									{
										if (!(text4 == "7"))
										{
											if (!(text4 == "11"))
											{
												text5 = "RegType " + array2[0];
											}
											else
											{
												text5 = "REG_QWORD";
											}
										}
										else
										{
											text5 = "REG_MULTI_SZ";
										}
									}
									else
									{
										text5 = "REG_DWORD";
									}
								}
								else
								{
									text5 = "REG_BINARY";
								}
							}
							else
							{
								text5 = "REG_EXPAND_SZ";
							}
						}
						else
						{
							text5 = "REG_SZ";
						}
						this.AddRegPolicyItem("HKLM", text2, text3, text5, array2[1].Trim(), this.OptionalInnerText(xmlNode3, "SourceFile"), this.OptionalInnerText(xmlNode3, "PolicyName"));
					}
				}
				foreach (object obj4 in xmlDocument.SelectNodes("//SecurityTemplate[@Section!='Registry Values']"))
				{
					XmlNode xmlNode4 = (XmlNode)obj4;
					if (!this.ValueNotFound(xmlNode4))
					{
						string text6 = xmlNode4.Attributes["Section"].Value;
						string innerText = xmlNode4["LineItem"].InnerText;
						string text7 = this.OptionalInnerText(xmlNode4, "SourceFile");
						string text8 = this.OptionalInnerText(xmlNode4, "PolicyName");
						char c = '\0';
						char c2 = '\0';
						PolicyRules.PolicySettingValue_t.eSpecialContent_t eSpecialContent_t = PolicyRules.PolicySettingValue_t.eSpecialContent_t.esc_None;
						string text4 = text6.ToLower();
						uint num2 = <PrivateImplementationDetails>.ComputeStringHash(text4);
						if (num2 <= 2777354874U)
						{
							if (num2 <= 629924151U)
							{
								if (num2 != 259916189U)
								{
									if (num2 != 629924151U)
									{
										goto IL_0578;
									}
									if (!(text4 == "privilege rights"))
									{
										goto IL_0578;
									}
									c = '=';
									c2 = ',';
									eSpecialContent_t = PolicyRules.PolicySettingValue_t.eSpecialContent_t.esc_SIDs;
								}
								else
								{
									if (!(text4 == "security log"))
									{
										goto IL_0578;
									}
									goto IL_0572;
								}
							}
							else if (num2 != 1707345048U)
							{
								if (num2 != 2496337679U)
								{
									if (num2 != 2777354874U)
									{
										goto IL_0578;
									}
									if (!(text4 == "kerberos policy"))
									{
										goto IL_0578;
									}
									goto IL_0572;
								}
								else
								{
									if (!(text4 == "application log"))
									{
										goto IL_0578;
									}
									goto IL_0572;
								}
							}
							else
							{
								if (!(text4 == "service general setting"))
								{
									goto IL_0578;
								}
								c = ',';
								eSpecialContent_t = PolicyRules.PolicySettingValue_t.eSpecialContent_t.esc_SDDL;
							}
						}
						else if (num2 <= 3830909927U)
						{
							if (num2 != 3637906928U)
							{
								if (num2 != 3684593292U)
								{
									if (num2 != 3830909927U)
									{
										goto IL_0578;
									}
									if (!(text4 == "file security"))
									{
										goto IL_0578;
									}
									c = ',';
									eSpecialContent_t = PolicyRules.PolicySettingValue_t.eSpecialContent_t.esc_SDDL;
								}
								else
								{
									if (!(text4 == "group membership"))
									{
										goto IL_0578;
									}
									c = '=';
									c2 = ',';
									eSpecialContent_t = PolicyRules.PolicySettingValue_t.eSpecialContent_t.esc_SIDs;
								}
							}
							else
							{
								if (!(text4 == "event audit"))
								{
									goto IL_0578;
								}
								goto IL_0572;
							}
						}
						else if (num2 != 3924500558U)
						{
							if (num2 != 3996202070U)
							{
								if (num2 != 4088932390U)
								{
									goto IL_0578;
								}
								if (!(text4 == "system log"))
								{
									goto IL_0578;
								}
								goto IL_0572;
							}
							else
							{
								if (!(text4 == "registry keys"))
								{
									goto IL_0578;
								}
								c = ',';
								eSpecialContent_t = PolicyRules.PolicySettingValue_t.eSpecialContent_t.esc_SDDL;
							}
						}
						else
						{
							if (!(text4 == "system access"))
							{
								goto IL_0578;
							}
							goto IL_0572;
						}
						IL_05AC:
						this.AddSecurityTemplateItem(text6, innerText, text7, text8, c, c2, eSpecialContent_t);
						continue;
						IL_0572:
						c = '=';
						goto IL_05AC;
						IL_0578:
						if (innerText.Contains("="))
						{
							c = '=';
						}
						else if (innerText.Contains(","))
						{
							c = ',';
						}
						text6 += "        [[UNRECOGNIZED SECTION NAME]]";
						goto IL_05AC;
					}
				}
				foreach (object obj5 in xmlDocument.SelectNodes("//AuditSubcategory"))
				{
					XmlNode xmlNode5 = (XmlNode)obj5;
					if (!this.ValueNotFound(xmlNode5))
					{
						this.AddAuditSubcategoryItem(xmlNode5["GUID"].InnerText, xmlNode5["Name"].InnerText, xmlNode5["Setting"].InnerText, this.OptionalInnerText(xmlNode5, "SourceFile"), this.OptionalInnerText(xmlNode5, "PolicyName"));
					}
				}
				foreach (object obj6 in xmlDocument.SelectNodes("//GlobalAudit"))
				{
					XmlNode xmlNode6 = (XmlNode)obj6;
					if (!this.ValueNotFound(xmlNode6))
					{
						this.AddGlobalAuditItem(xmlNode6["Type"].InnerText, xmlNode6["SACL"].InnerText, this.OptionalInnerText(xmlNode6, "SourceFile"), this.OptionalInnerText(xmlNode6, "PolicyName"));
					}
				}
				foreach (object obj7 in xmlDocument.SelectNodes("//AuditOption"))
				{
					XmlNode xmlNode7 = (XmlNode)obj7;
					if (!this.ValueNotFound(xmlNode7))
					{
						this.AddAuditOptionItem(xmlNode7["Option"].InnerText, xmlNode7["Setting"].InnerText, this.OptionalInnerText(xmlNode7, "SourceFile"), this.OptionalInnerText(xmlNode7, "PolicyName"));
					}
				}
				foreach (object obj8 in xmlDocument.SelectNodes("//CSE-Machine"))
				{
					XmlNode xmlNode8 = (XmlNode)obj8;
					string innerText2 = xmlNode8["GUID"].InnerText;
					string innerText3 = xmlNode8["Name"].InnerText;
					if (!this.m_MachineCSEs.ContainsKey(innerText2))
					{
						this.m_MachineCSEs.Add(innerText2, innerText3);
					}
				}
				foreach (object obj9 in xmlDocument.SelectNodes("//CSE-User"))
				{
					XmlNode xmlNode9 = (XmlNode)obj9;
					string innerText4 = xmlNode9["GUID"].InnerText;
					string innerText5 = xmlNode9["Name"].InnerText;
					if (!this.m_UserCSEs.ContainsKey(innerText4))
					{
						this.m_UserCSEs.Add(innerText4, innerText5);
					}
				}
			}
			catch (Exception ex)
			{
				sErrorText = ex.Message + "\n\nin file: " + sFilePath;
				return false;
			}
			return true;
		}

		// Token: 0x06000163 RID: 355 RVA: 0x000090E0 File Offset: 0x000072E0
		private string OptionalInnerText(XmlNode node, string childName)
		{
			XmlElement xmlElement = node[childName];
			if (xmlElement != null)
			{
				return xmlElement.InnerText;
			}
			return string.Empty;
		}

		// Token: 0x04000711 RID: 1809
		public const char cNulChar = '\0';

		// Token: 0x04000712 RID: 1810
		public SortedList<string, PolicyRules.PolicyItem_t> m_PolicyItems;

		// Token: 0x04000713 RID: 1811
		public SortedList<string, bool> m_GpoList;

		// Token: 0x04000714 RID: 1812
		public SortedList<string, string> m_MachineCSEs;

		// Token: 0x04000715 RID: 1813
		public SortedList<string, string> m_UserCSEs;

		// Token: 0x04000716 RID: 1814
		private bool m_bInReload;

		// Token: 0x04000717 RID: 1815
		private List<string> m_sFilePaths;

		// Token: 0x04000718 RID: 1816
		private GPLookup_t m_gpLookup;

		// Token: 0x04000719 RID: 1817
		private PolicyRules.OverrideBehavior_t m_OverrideBehavior;

		// Token: 0x02000060 RID: 96
		public class PolicySettingData_t
		{
			// Token: 0x06000164 RID: 356 RVA: 0x0000FF14 File Offset: 0x0000E114
			public PolicySettingData_t(string dataType, string settingData, string srcFile, string gpoName)
			{
				this.bDeleteValue = false;
				this.SettingType = dataType;
				if ("REG_SZ" == dataType && settingData.Length >= 2 && settingData[0] == '"' && settingData[settingData.Length - 1] == '"')
				{
					this.SettingData = settingData.Substring(1, settingData.Length - 2);
				}
				else
				{
					this.SettingData = settingData;
				}
				this.chrSortOnSeparator = '\0';
				this.SourceFile = srcFile;
				this.GpoName = gpoName;
			}

			// Token: 0x06000165 RID: 357 RVA: 0x00003226 File Offset: 0x00001426
			public PolicySettingData_t(string dataType, string settingData, string srcFile, string gpoName, char chrSortOnSep)
			{
				this.bDeleteValue = false;
				this.SettingType = dataType;
				this.chrSortOnSeparator = chrSortOnSep;
				this.SettingData = PolicyRules.PolicySettingData_t.Ordered(settingData, chrSortOnSep);
				this.SourceFile = srcFile;
				this.GpoName = gpoName;
			}

			// Token: 0x06000166 RID: 358 RVA: 0x0000FF9C File Offset: 0x0000E19C
			public PolicySettingData_t(string srcFile, string gpoName)
			{
				this.bDeleteValue = true;
				this.SettingType = (this.SettingData = "");
				this.chrSortOnSeparator = '\0';
				this.SourceFile = srcFile;
				this.GpoName = gpoName;
			}

			// Token: 0x06000167 RID: 359 RVA: 0x00003261 File Offset: 0x00001461
			public bool Equivalent(PolicyRules.PolicySettingData_t other)
			{
				return this.bDeleteValue == other.bDeleteValue && this.SettingType == other.SettingType && this.SettingData == other.SettingData;
			}

			// Token: 0x06000168 RID: 360 RVA: 0x0000FFE0 File Offset: 0x0000E1E0
			public string SettingDataToString(bool bSepToMultiLine)
			{
				string text;
				if (this.SettingDataToHex(out text))
				{
					return this.SettingData + " (" + text + ")";
				}
				if (bSepToMultiLine && this.chrSortOnSeparator != '\0')
				{
					return this.SettingData.Replace(this.chrSortOnSeparator.ToString() ?? "", "\r\n");
				}
				return this.SettingData;
			}

			// Token: 0x06000169 RID: 361 RVA: 0x00010044 File Offset: 0x0000E244
			public bool SettingDataToHex(out string sHex)
			{
				int num;
				if ("REG_DWORD" == this.SettingType && int.TryParse(this.SettingData, out num) && num > 9)
				{
					sHex = "0x" + num.ToString("X");
					return true;
				}
				long num2;
				if ("REG_QWORD" == this.SettingType && long.TryParse(this.SettingData, out num2) && num2 > 9L)
				{
					sHex = "0x" + num2.ToString("X");
					return true;
				}
				sHex = string.Empty;
				return false;
			}

			// Token: 0x0600016A RID: 362 RVA: 0x00003297 File Offset: 0x00001497
			public string SettingDataToStringNoHex(bool bSepToMultiLine)
			{
				if (bSepToMultiLine && this.chrSortOnSeparator != '\0')
				{
					return this.SettingData.Replace(this.chrSortOnSeparator.ToString() ?? "", "\r\n");
				}
				return this.SettingData;
			}

			// Token: 0x0600016B RID: 363 RVA: 0x000100DC File Offset: 0x0000E2DC
			public static string Ordered(string sData, char sep)
			{
				if (sep == '\0')
				{
					return sData;
				}
				string text = sep.ToString() ?? "";
				string[] array = sData.Split(new char[] { sep });
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = array[i].Trim();
				}
				Array.Sort<string>(array);
				return string.Join(text, array);
			}

			// Token: 0x0400071A RID: 1818
			public string SourceFile;

			// Token: 0x0400071B RID: 1819
			public string GpoName;

			// Token: 0x0400071C RID: 1820
			public bool bDeleteValue;

			// Token: 0x0400071D RID: 1821
			public string SettingType;

			// Token: 0x0400071E RID: 1822
			public string SettingData;

			// Token: 0x0400071F RID: 1823
			public char chrSortOnSeparator;
		}

		// Token: 0x02000061 RID: 97
		public struct PolicySettingValue_t
		{
			// Token: 0x0600016C RID: 364 RVA: 0x000032CF File Offset: 0x000014CF
			public PolicySettingValue_t(string value, PolicyRules.PolicySettingValue_t.eSpecialContent_t content)
			{
				this.Value = value;
				this.SettingDataItems = new SortedList<string, PolicyRules.PolicySettingData_t>();
				this.specialContent = content;
			}

			// Token: 0x0600016D RID: 365 RVA: 0x00010138 File Offset: 0x0000E338
			public void AddSettingValue(string dataType, string settingData, char sep, string srcFile, string gpoName)
			{
				string text = string.Concat(new string[] { gpoName, "!", dataType, "!", settingData, "!", srcFile }).ToUpper();
				if (this.SettingDataItems.ContainsKey(text))
				{
					return;
				}
				if (sep != '\0')
				{
					this.SettingDataItems.Add(text, new PolicyRules.PolicySettingData_t(dataType, settingData, srcFile, gpoName, sep));
					return;
				}
				this.SettingDataItems.Add(text, new PolicyRules.PolicySettingData_t(dataType, settingData, srcFile, gpoName));
			}

			// Token: 0x0600016E RID: 366 RVA: 0x000101C4 File Offset: 0x0000E3C4
			public void DelSettingValue(string srcFile, string gpoName)
			{
				string text = (gpoName + "!" + srcFile).ToUpper();
				if (this.SettingDataItems.ContainsKey(text))
				{
					return;
				}
				this.SettingDataItems.Add(text, new PolicyRules.PolicySettingData_t(srcFile, gpoName));
			}

			// Token: 0x04000720 RID: 1824
			public string Value;

			// Token: 0x04000721 RID: 1825
			public SortedList<string, PolicyRules.PolicySettingData_t> SettingDataItems;

			// Token: 0x04000722 RID: 1826
			public PolicyRules.PolicySettingValue_t.eSpecialContent_t specialContent;

			// Token: 0x02000062 RID: 98
			public enum eSpecialContent_t
			{
				// Token: 0x04000724 RID: 1828
				esc_None,
				// Token: 0x04000725 RID: 1829
				esc_SIDs,
				// Token: 0x04000726 RID: 1830
				esc_SDDL
			}
		}

		// Token: 0x02000063 RID: 99
		public struct FileAndGpo
		{
			// Token: 0x0600016F RID: 367 RVA: 0x000032EA File Offset: 0x000014EA
			public FileAndGpo(string sFile, string sGpo)
			{
				this.srcFile = sFile;
				this.gpoName = sGpo;
			}

			// Token: 0x04000727 RID: 1831
			public string srcFile;

			// Token: 0x04000728 RID: 1832
			public string gpoName;
		}

		// Token: 0x02000064 RID: 100
		public struct PolicyItem_t
		{
			// Token: 0x06000170 RID: 368 RVA: 0x000032FA File Offset: 0x000014FA
			public PolicyItem_t(string config, string keyOrGroup)
			{
				this.Config = config;
				this.KeyOrGroup = keyOrGroup;
				this.DeleteAllVals = new List<PolicyRules.FileAndGpo>();
				this.PolicyValueItems = new SortedList<string, PolicyRules.PolicySettingValue_t>();
			}

			// Token: 0x06000171 RID: 369 RVA: 0x00003320 File Offset: 0x00001520
			public string SortKey()
			{
				return PolicyRules.PolicyItem_t.SortKey(this.Config, this.KeyOrGroup);
			}

			// Token: 0x06000172 RID: 370 RVA: 0x00003333 File Offset: 0x00001533
			public static string SortKey(string config, string keyOrGroup)
			{
				return (config + "!" + keyOrGroup).ToUpper();
			}

			// Token: 0x06000173 RID: 371 RVA: 0x00003346 File Offset: 0x00001546
			public void AddDelAllValuesCommand(string srcFile, string gpoName)
			{
				this.DeleteAllVals.Add(new PolicyRules.FileAndGpo(srcFile, gpoName));
			}

			// Token: 0x06000174 RID: 372 RVA: 0x00010208 File Offset: 0x0000E408
			public void AddPolicyValue(string value, string dataType, string settingData, char sep, string srcFile, string gpoName, PolicyRules.PolicySettingValue_t.eSpecialContent_t content)
			{
				PolicyRules.PolicySettingValue_t policySettingValue_t;
				if (this.PolicyValueItems.TryGetValue(value, ref policySettingValue_t))
				{
					policySettingValue_t.AddSettingValue(dataType, settingData, sep, srcFile, gpoName);
					return;
				}
				PolicyRules.PolicySettingValue_t policySettingValue_t2 = new PolicyRules.PolicySettingValue_t(value, content);
				policySettingValue_t2.AddSettingValue(dataType, settingData, sep, srcFile, gpoName);
				this.PolicyValueItems.Add(value, policySettingValue_t2);
			}

			// Token: 0x06000175 RID: 373 RVA: 0x0001025C File Offset: 0x0000E45C
			public void AddDelPolicyValue(string value, string srcFile, string gpoName)
			{
				PolicyRules.PolicySettingValue_t policySettingValue_t;
				if (this.PolicyValueItems.TryGetValue(value, ref policySettingValue_t))
				{
					policySettingValue_t.DelSettingValue(srcFile, gpoName);
					return;
				}
				PolicyRules.PolicySettingValue_t policySettingValue_t2 = new PolicyRules.PolicySettingValue_t(value, PolicyRules.PolicySettingValue_t.eSpecialContent_t.esc_None);
				policySettingValue_t2.DelSettingValue(srcFile, gpoName);
				this.PolicyValueItems.Add(value, policySettingValue_t2);
			}

			// Token: 0x06000176 RID: 374 RVA: 0x000102A4 File Offset: 0x0000E4A4
			public void ReplacePolicyValue(string value, string dataType, string settingData, char sep, string srcFile, string gpoName, PolicyRules.PolicySettingValue_t.eSpecialContent_t content)
			{
				PolicyRules.PolicySettingValue_t policySettingValue_t;
				if (this.PolicyValueItems.TryGetValue(value, ref policySettingValue_t))
				{
					this.PolicyValueItems.Remove(value);
				}
				PolicyRules.PolicySettingValue_t policySettingValue_t2 = new PolicyRules.PolicySettingValue_t(value, content);
				policySettingValue_t2.AddSettingValue(dataType, settingData, sep, srcFile, gpoName);
				this.PolicyValueItems.Add(value, policySettingValue_t2);
			}

			// Token: 0x04000729 RID: 1833
			public string Config;

			// Token: 0x0400072A RID: 1834
			public string KeyOrGroup;

			// Token: 0x0400072B RID: 1835
			public List<PolicyRules.FileAndGpo> DeleteAllVals;

			// Token: 0x0400072C RID: 1836
			public SortedList<string, PolicyRules.PolicySettingValue_t> PolicyValueItems;
		}

		// Token: 0x02000065 RID: 101
		public enum OverrideBehavior_t
		{
			// Token: 0x0400072E RID: 1838
			eNewestOverridesPrevious,
			// Token: 0x0400072F RID: 1839
			eMultipleSettingsAllowed
		}
	}
}
