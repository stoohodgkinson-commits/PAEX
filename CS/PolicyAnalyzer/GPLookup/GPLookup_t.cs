using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Win32;
using PolicyAnalyzer;
using SharedCode;

namespace GPLookup
{
	// Token: 0x0200002B RID: 43
	public class GPLookup_t
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00002939 File Offset: 0x00000B39
		// (set) Token: 0x0600004E RID: 78 RVA: 0x00002931 File Offset: 0x00000B31
		public static GPLookup_t.ProgressIndicator st_ProgIndicatorDelegate { private get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00002940 File Offset: 0x00000B40
		// (set) Token: 0x06000051 RID: 81 RVA: 0x00002947 File Offset: 0x00000B47
		public static string AdmxDirectory
		{
			get
			{
				return GPLookup_t.s_AdmxDirectory;
			}
			set
			{
				GPLookup_t.s_AdmxDirectory = value;
				GPLookup_t.s_initialized = false;
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002955 File Offset: 0x00000B55
		public static GPLookup_t GPLookup(out string sErrorText)
		{
			sErrorText = string.Empty;
			if (!GPLookup_t.s_initialized)
			{
				GPLookup_t.s_GPLookup.Initialize(out sErrorText);
				GPLookup_t.s_initialized = true;
			}
			return GPLookup_t.s_GPLookup;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x0000297B File Offset: 0x00000B7B
		public static void Reinitialize()
		{
			GPLookup_t.s_initialized = false;
			GPLookup_t.s_GPLookup = new GPLookup_t();
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003E80 File Offset: 0x00002080
		private GPLookup_t()
		{
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003ED8 File Offset: 0x000020D8
		private void Initialize(out string sErrorText)
		{
			sErrorText = string.Empty;
			List<string> list = new List<string>();
			this.m_catPaths.Clear();
			this.m_polInfo.Clear();
			this.m_AdmxLookups.Clear();
			this.m_AdmlLookups.Clear();
			this.InitSecSettings();
			this.InitAdvAuditingSettings();
			Dictionary<string, CatInfo_t> dictionary = new Dictionary<string, CatInfo_t>();
			string admxDirectory = GPLookup_t.AdmxDirectory;
			string[] files = Directory.GetFiles(admxDirectory, "*.admx");
			int num = files.Length;
			int num2 = 0;
			foreach (string text in files)
			{
				if (GPLookup_t.st_ProgIndicatorDelegate != null)
				{
					GPLookup_t.st_ProgIndicatorDelegate(0, num, num2++);
				}
				string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(text);
				string empty = string.Empty;
				if (!this.GetAdmlFile(admxDirectory, text, out empty))
				{
					list.Add(text);
				}
				else
				{
					GPLookup_t.XDocAndNSMgr xdocAndNSMgr = new GPLookup_t.XDocAndNSMgr(text, "g");
					GPLookup_t.XDocAndNSMgr xdocAndNSMgr2 = new GPLookup_t.XDocAndNSMgr(empty, "g");
					this.m_AdmxLookups.Add(fileNameWithoutExtension, xdocAndNSMgr);
					this.m_AdmlLookups.Add(fileNameWithoutExtension, xdocAndNSMgr2);
					XmlNode xmlNode = xdocAndNSMgr.xdoc.SelectSingleNode("/g:policyDefinitions/g:policyNamespaces/g:target", xdocAndNSMgr.ns);
					string value = xmlNode.Attributes["prefix"].Value;
					string value2 = xmlNode.Attributes["namespace"].Value;
					Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
					dictionary2.Add(value, value2);
					foreach (object obj in xdocAndNSMgr.xdoc.SelectNodes("/g:policyDefinitions/g:policyNamespaces/g:using", xdocAndNSMgr.ns))
					{
						XmlNode xmlNode2 = (XmlNode)obj;
						dictionary2.Add(xmlNode2.Attributes["prefix"].Value, xmlNode2.Attributes["namespace"].Value);
					}
					foreach (object obj2 in xdocAndNSMgr.xdoc.SelectNodes("/g:policyDefinitions/g:categories/g:category", xdocAndNSMgr.ns))
					{
						XmlNode xmlNode3 = (XmlNode)obj2;
						CatInfo_t catInfo_t = new CatInfo_t();
						catInfo_t.filename = fileNameWithoutExtension;
						catInfo_t.catKey = value2 + ":" + xmlNode3.Attributes["name"].Value;
						string text2 = xmlNode3.Attributes["displayName"].Value;
						if (text2.StartsWith("$(string."))
						{
							text2 = text2.Substring(9, text2.Length - 10);
						}
						XmlNode xmlNode4 = xdocAndNSMgr2.xdoc.SelectSingleNode("/g:policyDefinitionResources/g:resources/g:stringTable/g:string[@id=\"" + text2 + "\"]", xdocAndNSMgr2.ns);
						if (xmlNode4 != null)
						{
							catInfo_t.catDispName = xmlNode4.InnerText;
						}
						XmlNode xmlNode5 = xmlNode3.SelectSingleNode("g:parentCategory", xdocAndNSMgr.ns);
						if (xmlNode5 != null)
						{
							catInfo_t.catParent = xmlNode5.Attributes["ref"].Value;
							if (!Enumerable.Contains<char>(catInfo_t.catParent, ':'))
							{
								catInfo_t.catParent = value2 + ":" + catInfo_t.catParent;
							}
							else
							{
								string[] array2 = catInfo_t.catParent.Split(new char[] { ':' });
								if (dictionary2.ContainsKey(array2[0]))
								{
									catInfo_t.catParent = dictionary2[array2[0]] + ":" + array2[1];
								}
							}
						}
						if (!dictionary.ContainsKey(catInfo_t.catKey))
						{
							dictionary.Add(catInfo_t.catKey, catInfo_t);
						}
					}
					foreach (object obj3 in xdocAndNSMgr.xdoc.SelectNodes("/g:policyDefinitions/g:policies/g:policy", xdocAndNSMgr.ns))
					{
						XmlNode xmlNode6 = (XmlNode)obj3;
						string value3 = xmlNode6.Attributes["name"].Value;
						string value4 = xmlNode6.Attributes["class"].Value;
						string value5 = xmlNode6.Attributes["key"].Value;
						XmlNode xmlNode7 = null;
						XmlAttribute xmlAttribute = xmlNode6.Attributes["presentation"];
						if (xmlAttribute != null)
						{
							string text3 = xmlAttribute.Value;
							if (text3.StartsWith("$(presentation."))
							{
								text3 = text3.Substring(15, text3.Length - 16);
								xmlNode7 = xdocAndNSMgr2.xdoc.SelectSingleNode("/g:policyDefinitionResources/g:resources/g:presentationTable/g:presentation[@id=\"" + text3 + "\"]", xdocAndNSMgr2.ns);
							}
						}
						string text4 = xmlNode6.Attributes["displayName"].Value;
						if (text4.StartsWith("$(string."))
						{
							text4 = text4.Substring(9, text4.Length - 10);
						}
						XmlNode xmlNode8 = xdocAndNSMgr2.xdoc.SelectSingleNode("/g:policyDefinitionResources/g:resources/g:stringTable/g:string[@id=\"" + text4 + "\"]", xdocAndNSMgr2.ns);
						string text5 = ((xmlNode8 != null) ? xmlNode8.InnerText : ("[ERROR - missing display name for " + text4 + "]"));
						string text6 = string.Empty;
						XmlAttribute xmlAttribute2 = xmlNode6.Attributes["explainText"];
						if (xmlAttribute2 != null)
						{
							string text7 = xmlAttribute2.Value;
							if (text7.StartsWith("$(string."))
							{
								text7 = text7.Substring(9, text7.Length - 10);
							}
							XmlNode xmlNode9 = xdocAndNSMgr2.xdoc.SelectSingleNode("/g:policyDefinitionResources/g:resources/g:stringTable/g:string[@id=\"" + text7 + "\"]", xdocAndNSMgr2.ns);
							if (xmlNode9 != null)
							{
								text6 = xmlNode9.InnerText;
								text6 = text6.Trim().Replace("\r", "\\r").Replace("\n", "\\n")
									.Replace("\t", "\\t");
							}
						}
						string text8 = "";
						XmlNode xmlNode10 = xmlNode6.SelectSingleNode("g:parentCategory", xdocAndNSMgr.ns);
						if (xmlNode10 != null)
						{
							text8 = xmlNode10.Attributes["ref"].Value;
							if (!Enumerable.Contains<char>(text8, ':'))
							{
								text8 = value2 + ":" + text8;
							}
							else
							{
								string[] array3 = text8.Split(new char[] { ':' });
								if (dictionary2.ContainsKey(array3[0]))
								{
									text8 = dictionary2[array3[0]] + ":" + array3[1];
								}
							}
						}
						foreach (object obj4 in xmlNode6.SelectNodes("descendant-or-self::g:*[@valueName]", xdocAndNSMgr.ns))
						{
							XmlNode xmlNode11 = (XmlNode)obj4;
							string text9 = value5;
							string value6 = xmlNode11.Attributes["valueName"].Value;
							string text10 = string.Empty;
							XmlNode xmlNode12 = xmlNode11;
							while (xmlNode12 != xmlNode6 && xmlNode12 != null)
							{
								if (xmlNode12.Attributes != null)
								{
									if (xmlNode7 != null && xmlNode12.Attributes["id"] != null)
									{
										string value7 = xmlNode12.Attributes["id"].Value;
										XmlNode xmlNode13 = xmlNode7.SelectSingleNode("*[@refId='" + value7 + "']");
										if (xmlNode13 != null)
										{
											XmlNode xmlNode14 = xmlNode13.SelectSingleNode("g:label", xdocAndNSMgr.ns);
											if (xmlNode14 != null)
											{
												text10 = xmlNode14.InnerText;
											}
											else
											{
												text10 = xmlNode13.InnerText;
											}
										}
									}
									if (xmlNode12.Attributes["key"] != null)
									{
										text9 = xmlNode12.Attributes["key"].Value;
										break;
									}
								}
								xmlNode12 = xmlNode12.ParentNode;
							}
							this.AddToPolInfo(value3, value4, text9, value6, text8, text5, text10, text6, fileNameWithoutExtension);
						}
						if (xmlNode6.Attributes["valueName"] != null)
						{
							string value8 = xmlNode6.Attributes["valueName"].Value;
							this.AddToPolInfo(value3, value4, value5, value8, text8, text5, string.Empty, text6, fileNameWithoutExtension);
						}
						XmlNodeList xmlNodeList = xmlNode6.SelectNodes("g:elements/g:*[@valueName]", xdocAndNSMgr.ns);
						if (xmlNodeList.Count > 0)
						{
							foreach (object obj5 in xmlNodeList)
							{
								XmlNode xmlNode15 = (XmlNode)obj5;
								string value9 = xmlNode15.Attributes["valueName"].Value;
								string text11 = value5;
								if (xmlNode15.Attributes["key"] != null)
								{
									text11 = xmlNode15.Attributes["key"].Value;
								}
								string text12 = string.Empty;
								if (xmlNode7 != null && xmlNode15.Attributes["id"] != null)
								{
									string value10 = xmlNode15.Attributes["id"].Value;
									XmlNode xmlNode16 = xmlNode7.SelectSingleNode("*[@refId='" + value10 + "']");
									if (xmlNode16 != null)
									{
										text12 = xmlNode16.InnerText;
									}
								}
								this.AddToPolInfo(value3, value4, text11, value9, text8, text5, text12, text6, fileNameWithoutExtension);
							}
						}
						xmlNodeList = xmlNode6.SelectNodes("g:elements/g:list", xdocAndNSMgr.ns);
						if (xmlNodeList.Count > 0)
						{
							this.AddToPolInfo(value3, value4, value5, "", text8, text5, string.Empty, text6, fileNameWithoutExtension);
							foreach (object obj6 in xmlNodeList)
							{
								XmlNode xmlNode17 = (XmlNode)obj6;
								if (xmlNode17.Attributes["key"] != null)
								{
									string value11 = xmlNode17.Attributes["key"].Value;
									string text13 = string.Empty;
									if (xmlNode7 != null && xmlNode17.Attributes["id"] != null)
									{
										string value12 = xmlNode17.Attributes["id"].Value;
										XmlNode xmlNode18 = xmlNode7.SelectSingleNode("*[@refId='" + value12 + "']");
										if (xmlNode18 != null)
										{
											text13 = xmlNode18.InnerText;
										}
									}
									this.AddToPolInfo(value3, value4, value11, "", text8, text5, text13, text6, fileNameWithoutExtension);
								}
							}
						}
						xmlNodeList = xmlNode6.SelectNodes("g:enabledList/g:item[@key]", xdocAndNSMgr.ns);
						if (xmlNodeList.Count > 0)
						{
							foreach (object obj7 in xmlNodeList)
							{
								XmlNode xmlNode19 = (XmlNode)obj7;
								string text14 = string.Empty;
								if (xmlNode7 != null && xmlNode19.Attributes["id"] != null)
								{
									string value13 = xmlNode19.Attributes["id"].Value;
									XmlNode xmlNode20 = xmlNode7.SelectSingleNode("*[@refId='" + value13 + "']");
									if (xmlNode20 != null)
									{
										text14 = xmlNode20.InnerText;
									}
								}
								this.AddToPolInfo(value3, value4, xmlNode19.Attributes["key"].Value, xmlNode19.Attributes["valueName"].Value, text8, text5, text14, text6, fileNameWithoutExtension);
							}
							foreach (object obj8 in xmlNode6.SelectNodes("g:disabledList/g:item[@key]", xdocAndNSMgr.ns))
							{
								XmlNode xmlNode21 = (XmlNode)obj8;
								string text15 = string.Empty;
								if (xmlNode7 != null && xmlNode21.Attributes["id"] != null)
								{
									string value14 = xmlNode21.Attributes["id"].Value;
									XmlNode xmlNode22 = xmlNode7.SelectSingleNode("*[@refId='" + value14 + "']");
									if (xmlNode22 != null)
									{
										text15 = xmlNode22.InnerText;
									}
								}
								this.AddToPolInfo(value3, value4, xmlNode21.Attributes["key"].Value, xmlNode21.Attributes["valueName"].Value, text8, text5, text15, text6, fileNameWithoutExtension);
							}
						}
					}
				}
			}
			foreach (string text16 in dictionary.Keys)
			{
				Stack<string> stack = new Stack<string>();
				CatInfo_t catInfo_t2 = dictionary[text16];
				string filename = catInfo_t2.filename;
				while (catInfo_t2 != null)
				{
					stack.Push(catInfo_t2.catDispName);
					if (catInfo_t2.catParent != null && catInfo_t2.catParent != catInfo_t2.catKey && dictionary.ContainsKey(catInfo_t2.catParent))
					{
						catInfo_t2 = dictionary[catInfo_t2.catParent];
					}
					else
					{
						if (catInfo_t2.catParent != null)
						{
							stack.Push("[[[" + catInfo_t2.catParent + "]]]");
						}
						catInfo_t2 = null;
					}
				}
				StringBuilder stringBuilder = new StringBuilder();
				while (stack.Count > 0)
				{
					stringBuilder.Append(stack.Pop() + "\\");
				}
				CatPathInfo_t catPathInfo_t = new CatPathInfo_t();
				catPathInfo_t.catKey = text16;
				catPathInfo_t.catPath = stringBuilder.ToString();
				catPathInfo_t.filename = filename;
				this.m_catPaths.Add(text16, catPathInfo_t);
			}
			if (list.Count > 0)
			{
				StringBuilder stringBuilder2 = new StringBuilder();
				stringBuilder2.AppendLine("Cannot find language resource files for the following policy files:");
				foreach (string text17 in list)
				{
					stringBuilder2.AppendLine(text17);
				}
				sErrorText = stringBuilder2.ToString();
			}
		}

		// Token: 0x06000056 RID: 86
		[DllImport("Kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern bool GetFileMUIPath(uint dwFlags, string pcwszFilePath, StringBuilder pwszLanguage, ref uint pcchLanguage, StringBuilder pwszFileMUIPath, ref uint pcchFileMUIPath, ref ulong pululEnumerator);

		// Token: 0x06000057 RID: 87 RVA: 0x00004DA8 File Offset: 0x00002FA8
		private bool GetAdmlFile(string sAdmxDir, string sAdmxFilePath, out string sAdmlFilePath)
		{
			sAdmlFilePath = string.Empty;
			string text = sAdmxFilePath.Replace(".admx", ".adml");
			uint num = 24U;
			StringBuilder stringBuilder = new StringBuilder(300);
			StringBuilder stringBuilder2 = new StringBuilder(300);
			uint num2 = 300U;
			uint num3 = 300U;
			ulong num4 = 0UL;
			if (GPLookup_t.GetFileMUIPath(num, text, stringBuilder, ref num2, stringBuilder2, ref num3, ref num4) && File.Exists(stringBuilder2.ToString()))
			{
				sAdmlFilePath = stringBuilder2.ToString();
				return true;
			}
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(sAdmxFilePath);
			foreach (string text2 in new string[] { "", "en-us", "en" })
			{
				string text3 = Path.Combine(Path.Combine(sAdmxDir, text2), fileNameWithoutExtension + ".adml");
				if (File.Exists(text3))
				{
					sAdmlFilePath = text3;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00004E8C File Offset: 0x0000308C
		private bool IsAggregatedSettingToIgnore(PolLocation_t pl)
		{
			if (pl.category == "Microsoft.Policies.InternetExplorer:IZ_SecurityPage")
			{
				string text = pl.policyName;
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
				if (num <= 2227221448U)
				{
					if (num <= 752483095U)
					{
						if (num != 332424127U)
						{
							if (num != 752483095U)
							{
								goto IL_014D;
							}
							if (!(text == "IZ_PolicyTrustedSitesZoneLockdownTemplate"))
							{
								goto IL_014D;
							}
						}
						else if (!(text == "IZ_PolicyRestrictedSitesZoneLockdownTemplate"))
						{
							goto IL_014D;
						}
					}
					else if (num != 1926248258U)
					{
						if (num != 2012301969U)
						{
							if (num != 2227221448U)
							{
								goto IL_014D;
							}
							if (!(text == "IZ_PolicyTrustedSitesZoneTemplate"))
							{
								goto IL_014D;
							}
						}
						else if (!(text == "IZ_PolicyLocalMachineZoneTemplate"))
						{
							goto IL_014D;
						}
					}
					else if (!(text == "IZ_PolicyLocalMachineZoneLockdownTemplate"))
					{
						goto IL_014D;
					}
				}
				else if (num <= 2277289769U)
				{
					if (num != 2270124383U)
					{
						if (num != 2277289769U)
						{
							goto IL_014D;
						}
						if (!(text == "IZ_PolicyIntranetZoneLockdownTemplate"))
						{
							goto IL_014D;
						}
					}
					else if (!(text == "IZ_PolicyInternetZoneLockdownTemplate"))
					{
						goto IL_014D;
					}
				}
				else if (num != 2523055712U)
				{
					if (num != 3271259330U)
					{
						if (num != 3574776512U)
						{
							goto IL_014D;
						}
						if (!(text == "IZ_PolicyInternetZoneTemplate"))
						{
							goto IL_014D;
						}
					}
					else if (!(text == "IZ_PolicyIntranetZoneTemplate"))
					{
						goto IL_014D;
					}
				}
				else if (!(text == "IZ_PolicyRestrictedSitesZoneTemplate"))
				{
					goto IL_014D;
				}
				return true;
			}
			IL_014D:
			if (pl.category == "Microsoft.Policies.Windows:InternetManagement")
			{
				string text = pl.policyName;
				if (text == "InternetManagement_RestrictCommunication_1" || text == "InternetManagement_RestrictCommunication_2")
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x0000501C File Offset: 0x0000321C
		public bool PolSettingLookup(string sHive, string sKey, string sValue, out PolInfo_t pi, out string sConfig)
		{
			pi = null;
			sConfig = string.Empty;
			if (sHive == "Security Template")
			{
				pi = this.SecTemplatePathLookup(sKey, sValue);
				if (pi == null)
				{
					return false;
				}
			}
			else if (sHive == "Audit Policy")
			{
				string text = AuditPolText.SystemAuditPolicies() + "\\" + sKey;
				string text2 = this.AdvAuditGetExplainText(sKey, sValue);
				string text3 = text.ToLower();
				if (!this.m_catPaths.ContainsKey(text3))
				{
					CatPathInfo_t catPathInfo_t = new CatPathInfo_t();
					catPathInfo_t.catKey = text3;
					catPathInfo_t.catPath = text;
					catPathInfo_t.filename = string.Empty;
					this.m_catPaths.Add(text3, catPathInfo_t);
				}
				pi = new PolInfo_t(PolInfo_t.class_t.AdvAudit, sKey, sValue);
				pi.AddLoc(string.Empty, text3, sValue, string.Empty, text2, string.Empty);
			}
			else if (!this.FirewallPathLookup(sHive, sKey, sValue, out pi) && !this.AppLockerPathLookup(sHive, sKey, sValue, out pi))
			{
				PolInfo_t polInfo_t;
				if (!this.m_polInfo.TryGetValue(PolInfo_t.SortKey(sHive, sKey, sValue), out polInfo_t) && !this.m_polInfo.TryGetValue(PolInfo_t.SortKey(sHive, sKey, ""), out polInfo_t))
				{
					return false;
				}
				pi = new PolInfo_t(polInfo_t.polClass, polInfo_t.polKey, sValue);
				pi.secSettingsDetails = polInfo_t.secSettingsDetails;
				foreach (PolLocation_t polLocation_t in polInfo_t.polLocations.Values)
				{
					if (polInfo_t.polLocations.Count <= 1 || !this.IsAggregatedSettingToIgnore(polLocation_t))
					{
						pi.polLocations.Add(polLocation_t.SortKey(), (PolLocation_t)polLocation_t.Clone());
					}
				}
			}
			DllResourceStringLookup dllResourceStringLookup = new DllResourceStringLookup();
			switch (pi.polClass)
			{
			case PolInfo_t.class_t.Machine:
				sConfig = dllResourceStringLookup.LookupResourceOrDefault("gpedit.dll", 29U, "Computer Configuration");
				break;
			case PolInfo_t.class_t.User:
				sConfig = dllResourceStringLookup.LookupResourceOrDefault("gpedit.dll", 30U, "User Configuration");
				break;
			case PolInfo_t.class_t.SecuritySettings:
				sConfig = WSecEdit.LookupWithDefault(26U, "Security Settings");
				break;
			case PolInfo_t.class_t.AdvAudit:
				sConfig = AuditPolText.AdvancedAuditPolicyConfiguration();
				break;
			}
			return true;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00005264 File Offset: 0x00003464
		public bool PolSettingChoiceLookup(PolInfo_t pi, string sData, out List<PolSettingChoice_t> results)
		{
			results = null;
			if (pi.polLocations.Count == 0)
			{
				return false;
			}
			if (pi.polClass == PolInfo_t.class_t.SecuritySettings)
			{
				return this.PolSettingChoiceLookupSecSettings(pi, sData, out results);
			}
			if (this.PolSettingChoiceLookupFirewall(pi, sData, out results))
			{
				return true;
			}
			if (this.PolSettingChoiceLookupAppLocker(pi, sData, out results))
			{
				return true;
			}
			if (this.PolSettingChoiceLookupAttackSurfaceReduction(pi, sData, out results))
			{
				return true;
			}
			results = new List<PolSettingChoice_t>();
			if (pi.polClass == PolInfo_t.class_t.AdvAudit)
			{
				return false;
			}
			string text = SecFmt.EncodeForXml(sData);
			foreach (PolLocation_t polLocation_t in pi.polLocations.Values)
			{
				GPLookup_t.XDocAndNSMgr xdocAndNSMgr;
				if (this.m_AdmxLookups.TryGetValue(polLocation_t.filename, out xdocAndNSMgr))
				{
					bool flag = true;
					string text2 = string.Concat(new string[]
					{
						"/g:policyDefinitions/g:policies/g:policy[@name='",
						polLocation_t.policyName,
						"' and (@class='Both' or @class='",
						(pi.polClass == PolInfo_t.class_t.User) ? "User" : "Machine",
						"')]/descendant-or-self::g:*[@key = '",
						pi.polKey,
						"']/descendant-or-self::g:*[@valueName='",
						pi.polValue,
						"' or @valueName='",
						pi.polValue.ToLower(),
						"' or @valueName='",
						pi.polValue.ToUpper(),
						"']"
					});
					string text3 = string.Concat(new string[] { "/descendant::g:*[@value='", text, "' or *='", text, "']" });
					XmlNodeList xmlNodeList = xdocAndNSMgr.xdoc.SelectNodes(text2 + text3, xdocAndNSMgr.ns);
					if (xmlNodeList.Count == 0)
					{
						flag = false;
						xmlNodeList = xdocAndNSMgr.xdoc.SelectNodes(text2, xdocAndNSMgr.ns);
					}
					if (xmlNodeList.Count > 0)
					{
						List<string> list = new List<string>();
						foreach (object obj in xmlNodeList)
						{
							XmlNode xmlNode = (XmlNode)obj;
							bool flag2 = false;
							bool flag3 = false;
							bool flag4 = false;
							string text4 = string.Empty;
							if ((xmlNode.Name == "decimal" || xmlNode.Name == "text") && !flag)
							{
								flag4 = true;
								text4 = sData;
								XmlAttribute xmlAttribute;
								if ((xmlAttribute = xmlNode.Attributes["valueName"]) != null && pi.polValue == xmlAttribute.Value)
								{
									flag2 = true;
								}
								if ((xmlAttribute = xmlNode.Attributes["key"]) != null && pi.polKey == xmlAttribute.Value)
								{
									flag3 = true;
								}
							}
							else if (xmlNode.Name == "policy")
							{
								XmlAttribute xmlAttribute2;
								if ((xmlAttribute2 = xmlNode.Attributes["valueName"]) != null && pi.polValue == xmlAttribute2.Value)
								{
									flag2 = true;
								}
								if ((xmlAttribute2 = xmlNode.Attributes["key"]) != null && pi.polKey == xmlAttribute2.Value)
								{
									flag3 = true;
								}
								if (flag3 && flag2)
								{
									flag4 = true;
									text4 = sData;
									if (list.Count == 0)
									{
										if (!(sData == "0"))
										{
											if (sData == "1")
											{
												text4 = GPLookup_t.StrEnabled();
											}
										}
										else
										{
											text4 = GPLookup_t.StrDisabled();
										}
										list.Add(text4);
									}
									else if (!list.Contains(text4))
									{
										list.Add(text4);
									}
								}
							}
							bool flag5 = xmlNode == null || "policy" == xmlNode.Name;
							while (!flag5)
							{
								if (!flag4 && xmlNode.Attributes["displayName"] != null)
								{
									string text5 = xmlNode.Attributes["displayName"].Value;
									if (text5.StartsWith("$(string."))
									{
										text5 = text5.Substring(9, text5.Length - 10);
									}
									GPLookup_t.XDocAndNSMgr xdocAndNSMgr2;
									if (!this.m_AdmlLookups.TryGetValue(polLocation_t.filename, out xdocAndNSMgr2))
									{
										return true;
									}
									text4 = xdocAndNSMgr2.xdoc.SelectSingleNode("/g:policyDefinitionResources/g:resources/g:stringTable/g:string[@id=\"" + text5 + "\"]", xdocAndNSMgr2.ns).InnerText;
									flag4 = true;
								}
								if (!flag4)
								{
									string name = xmlNode.Name;
									if (!(name == "enabledList") && !(name == "enabledValue"))
									{
										if (!(name == "disabledList") && !(name == "disabledValue"))
										{
											if (!(name == "trueValue"))
											{
												if (name == "falseValue")
												{
													text4 = "False";
													flag4 = true;
												}
											}
											else
											{
												text4 = "True";
												flag4 = true;
											}
										}
										else
										{
											text4 = GPLookup_t.StrDisabled();
											flag4 = true;
										}
									}
									else
									{
										text4 = GPLookup_t.StrEnabled();
										flag4 = true;
									}
								}
								xmlNode = xmlNode.ParentNode;
								if (xmlNode == null)
								{
									flag5 = true;
								}
								else
								{
									XmlAttribute xmlAttribute3;
									if (!flag2 && (xmlAttribute3 = xmlNode.Attributes["valueName"]) != null)
									{
										if (pi.polValue.ToLower() != xmlAttribute3.Value.ToLower())
										{
											flag5 = true;
										}
										else
										{
											flag2 = true;
										}
									}
									if (!flag3 && (xmlAttribute3 = xmlNode.Attributes["key"]) != null)
									{
										if (pi.polKey != xmlAttribute3.Value)
										{
											flag5 = true;
										}
										else
										{
											flag3 = true;
										}
									}
									if (flag3 && flag2 && flag4)
									{
										if (!list.Contains(text4))
										{
											list.Add(text4);
										}
										flag5 = true;
									}
									if ("policy" == xmlNode.Name)
									{
										flag5 = true;
									}
								}
							}
						}
						if (list.Count > 0)
						{
							string text6;
							if (list.Count == 1)
							{
								text6 = list[0];
							}
							else
							{
								StringBuilder stringBuilder = new StringBuilder("[Multiple possible: ");
								bool flag6 = true;
								foreach (string text7 in list)
								{
									if (!flag6)
									{
										stringBuilder.Append(", ");
									}
									flag6 = false;
									stringBuilder.Append(text7);
								}
								stringBuilder.Append("]");
								text6 = stringBuilder.ToString();
							}
							results.Add(new PolSettingChoice_t(polLocation_t, text6));
						}
					}
				}
			}
			return true;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x0000298D File Offset: 0x00000B8D
		public bool CategoryLookup(string sCategory, out CatPathInfo_t catPath)
		{
			catPath = null;
			if (this.m_catPaths.ContainsKey(sCategory))
			{
				catPath = this.m_catPaths[sCategory];
				return true;
			}
			return false;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00005904 File Offset: 0x00003B04
		private void AddToPolInfo(PolInfo_t.class_t cls, string sKey, string sValue, string sPolicyName, string sCategory, string sDispName, string sDispNameSubOption, string sExplainText, string sFilename, SecSettingsDetails_t ssd)
		{
			sDispName = sDispName.Trim().Replace("\r", "\\r").Replace("\n", "\\n")
				.Replace("\t", "\\t");
			sDispNameSubOption = sDispNameSubOption.Trim().Replace("\r", "\\r").Replace("\n", "\\n")
				.Replace("\t", "\\t");
			PolInfo_t polInfo_t = new PolInfo_t(cls, sKey, sValue);
			polInfo_t.secSettingsDetails = ssd;
			PolInfo_t polInfo_t2;
			if (this.m_polInfo.TryGetValue(polInfo_t.SortKey(), out polInfo_t2))
			{
				polInfo_t2.AddLoc(sPolicyName, sCategory, sDispName, sDispNameSubOption, sExplainText, sFilename);
				return;
			}
			polInfo_t.AddLoc(sPolicyName, sCategory, sDispName, sDispNameSubOption, sExplainText, sFilename);
			this.m_polInfo.Add(polInfo_t.SortKey(), polInfo_t);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000059DC File Offset: 0x00003BDC
		private void AddToPolInfo(string sPolicyName, string sClass, string sKey, string sValue, string sCategory, string sDispName, string sDispNameSubOption, string sExplainText, string sFilename)
		{
			if ("Both" == sClass || "User" == sClass)
			{
				this.AddToPolInfo(PolInfo_t.class_t.User, sKey, sValue, sPolicyName, sCategory, sDispName, sDispNameSubOption, sExplainText, sFilename, null);
			}
			if ("Both" == sClass || "Machine" == sClass)
			{
				this.AddToPolInfo(PolInfo_t.class_t.Machine, sKey, sValue, sPolicyName, sCategory, sDispName, sDispNameSubOption, sExplainText, sFilename, null);
			}
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000029B1 File Offset: 0x00000BB1
		public bool AdvAuditLookupByGuid(string sGuid, out GPLookup_t.AdvAuditData_t aad)
		{
			aad = null;
			sGuid = sGuid.ToLower();
			if (this.m_AdvAuditLookupByGuid.ContainsKey(sGuid))
			{
				aad = this.m_AdvAuditLookupByGuid[sGuid];
				return true;
			}
			return false;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00005A4C File Offset: 0x00003C4C
		public string AdvAuditGetExplainText(string sCategoryName, string sSubcategoryName)
		{
			string text = this.AdvAuditLookupKey(sCategoryName, sSubcategoryName);
			if (this.m_AdvAuditLookupExplainText.ContainsKey(text))
			{
				return this.m_AdvAuditLookupExplainText[text];
			}
			return string.Empty;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x000029DD File Offset: 0x00000BDD
		private string AdvAuditLookupKey(string sCategoryName, string sSubcategoryName)
		{
			return (sCategoryName + "!" + sSubcategoryName).ToLower();
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00005A84 File Offset: 0x00003C84
		private bool GetStringValue(RegistryKey hKey, string sValueName, out string sData)
		{
			bool flag = false;
			sData = string.Empty;
			object value = hKey.GetValue(sValueName);
			if (value != null && hKey.GetValueKind(sValueName) == RegistryValueKind.String)
			{
				sData = value.ToString();
				flag = true;
			}
			return flag;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00005ABC File Offset: 0x00003CBC
		private void InitAdvAuditingSettings()
		{
			DllResourceStringLookup dllResourceStringLookup = new DllResourceStringLookup();
			RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Audit\\SystemPolicy", false);
			if (registryKey != null)
			{
				foreach (string text in registryKey.GetSubKeyNames())
				{
					RegistryKey registryKey2 = registryKey.OpenSubKey(text, false);
					string text2;
					if (registryKey != null && this.GetStringValue(registryKey2, "DisplayName", out text2))
					{
						string text3 = dllResourceStringLookup.LookupResource(text2);
						if (text3.Length > 0)
						{
							foreach (string text4 in registryKey2.GetSubKeyNames())
							{
								RegistryKey registryKey3 = registryKey2.OpenSubKey(text4, false);
								string text5;
								string text6;
								if (registryKey3 != null && this.GetStringValue(registryKey3, "GUID", out text5) && this.GetStringValue(registryKey3, "DisplayName", out text6))
								{
									string text7 = dllResourceStringLookup.LookupResource(text6);
									if (text7.Length > 0)
									{
										if (text7.StartsWith("Audit "))
										{
											text7 = text7.Substring(6);
										}
										string text8 = string.Empty;
										string text9;
										if (this.GetStringValue(registryKey3, "HelpText", out text9))
										{
											text8 = dllResourceStringLookup.LookupResource(text9).Trim().Replace("\r", "\\r")
												.Replace("\n", "\\n")
												.Replace("\t", "\\t");
										}
										GPLookup_t.AdvAuditData_t advAuditData_t = new GPLookup_t.AdvAuditData_t();
										advAuditData_t.sCategoryName = text3;
										advAuditData_t.sSubcategoryName = text7;
										advAuditData_t.sExplainText = text8;
										this.m_AdvAuditLookupByGuid.Add(text5.ToLower(), advAuditData_t);
										this.m_AdvAuditLookupExplainText.Add(this.AdvAuditLookupKey(text3, text7), text8);
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000029F0 File Offset: 0x00000BF0
		public static string StrEnabled()
		{
			return WSecEdit.LookupWithDefault(201U, "Enabled");
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002A01 File Offset: 0x00000C01
		public static string StrDisabled()
		{
			return WSecEdit.LookupWithDefault(202U, "Disabled");
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002A12 File Offset: 0x00000C12
		private static string FirewallTitleLookup()
		{
			return new DllResourceStringLookup().LookupResourceOrDefault("authfwgp.dll", 10U, "Windows Defender Firewall with Advanced Security");
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002A2A File Offset: 0x00000C2A
		private static string FirewallStringLookup(uint ID, string sDefault)
		{
			return new DllResourceStringLookup().LookupResourceOrDefault("authfwcfg.dll", ID, sDefault);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00005C78 File Offset: 0x00003E78
		private bool FirewallPathLookup(string sHive, string sKey, string sValue, out PolInfo_t pi)
		{
			pi = null;
			string text = "...\\" + GPLookup_t.FirewallTitleLookup() + "\\";
			if (sHive.ToLower() != "hklm")
			{
				return false;
			}
			string text2 = sKey.ToLower();
			if (!text2.StartsWith("software\\policies\\microsoft\\windowsfirewall\\"))
			{
				return false;
			}
			string text3 = string.Empty;
			string text4 = string.Empty;
			string text5 = string.Empty;
			string text6 = string.Empty;
			bool flag = false;
			string[] array = text2.Substring("software\\policies\\microsoft\\windowsfirewall\\".Length).Split(new char[] { '\\' });
			if (array.Length != 1 && array.Length != 2)
			{
				return false;
			}
			string text7 = array[0];
			if (!(text7 == "domainprofile"))
			{
				if (!(text7 == "privateprofile"))
				{
					if (!(text7 == "publicprofile"))
					{
						if (!(text7 == "firewallrules"))
						{
							return false;
						}
						text6 = text + "...Rules";
						text5 = sValue;
					}
					else
					{
						text3 = GPLookup_t.FirewallStringLookup(536882007U, "Public Profile");
					}
				}
				else
				{
					text3 = GPLookup_t.FirewallStringLookup(536881926U, "Private Profile");
				}
			}
			else
			{
				text3 = GPLookup_t.FirewallStringLookup(536881925U, "Domain Profile");
			}
			if (array.Length == 2)
			{
				if (array[1] != "logging")
				{
					return false;
				}
				flag = true;
			}
			if (text6.Length == 0)
			{
				if (!flag)
				{
					text7 = sValue.ToLower();
					uint num = <PrivateImplementationDetails>.ComputeStringHash(text7);
					if (num <= 2767350853U)
					{
						if (num <= 1946529693U)
						{
							if (num != 1493681445U)
							{
								if (num == 1946529693U)
								{
									if (text7 == "disablenotifications")
									{
										text5 = "Display a notification";
										text4 = "Settings";
										goto IL_039E;
									}
								}
							}
							else if (text7 == "allowlocalipsecpolicymerge")
							{
								text5 = "Apply local connection security rules";
								text4 = "Settings";
								goto IL_039E;
							}
						}
						else if (num != 2163167192U)
						{
							if (num == 2767350853U)
							{
								if (text7 == "defaultinboundaction")
								{
									text5 = "Inbound connections";
									text4 = "State";
									goto IL_039E;
								}
							}
						}
						else if (text7 == "donotallowexceptions")
						{
							text5 = "Inbound connections";
							text4 = "State";
							goto IL_039E;
						}
					}
					else if (num <= 3211669745U)
					{
						if (num != 3040397098U)
						{
							if (num == 3211669745U)
							{
								if (text7 == "allowlocalpolicymerge")
								{
									text5 = "Apply local firewall rules";
									text4 = "Settings";
									goto IL_039E;
								}
							}
						}
						else if (text7 == "enablefirewall")
						{
							text5 = "Firewall state";
							text4 = "State";
							goto IL_039E;
						}
					}
					else if (num != 3465850272U)
					{
						if (num == 4067950496U)
						{
							if (text7 == "disableunicastresponsestomulticastbroadcast")
							{
								text5 = "Allow unicast response";
								text4 = "Settings";
								goto IL_039E;
							}
						}
					}
					else if (text7 == "defaultoutboundaction")
					{
						text5 = "Outbound connections";
						text4 = "State";
						goto IL_039E;
					}
					return false;
				}
				text7 = sValue.ToLower();
				if (!(text7 == "logfilepath"))
				{
					if (!(text7 == "logfilesize"))
					{
						if (!(text7 == "logdroppedpackets"))
						{
							if (!(text7 == "logsuccessfulconnections"))
							{
								return false;
							}
							text5 = "Log successful packets";
							text4 = "Logging";
						}
						else
						{
							text5 = "Log dropped packets";
							text4 = "Logging";
						}
					}
					else
					{
						text5 = "Size limit (KB)";
						text4 = "Logging";
					}
				}
				else
				{
					text5 = "Log name";
					text4 = "Logging";
				}
				IL_039E:
				text6 = text + text3 + "\\" + text4;
			}
			string text8 = text6.ToLower();
			if (!this.m_catPaths.ContainsKey(text8))
			{
				CatPathInfo_t catPathInfo_t = new CatPathInfo_t();
				catPathInfo_t.catKey = text8;
				catPathInfo_t.catPath = text6;
				catPathInfo_t.filename = string.Empty;
				this.m_catPaths.Add(text8, catPathInfo_t);
			}
			pi = new PolInfo_t(PolInfo_t.class_t.Machine, sKey, sValue);
			pi.AddLoc(string.Empty, text8, text5, string.Empty, string.Empty, string.Empty);
			return true;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x000060AC File Offset: 0x000042AC
		private bool PolSettingChoiceLookupFirewall(PolInfo_t pi, string sData, out List<PolSettingChoice_t> results)
		{
			results = null;
			if (pi.polHive.ToLower() != "hklm")
			{
				return false;
			}
			if (!pi.polKey.ToLower().StartsWith("software\\policies\\microsoft\\windowsfirewall\\"))
			{
				return false;
			}
			results = new List<PolSettingChoice_t>();
			PolLocation_t polLocation_t = pi.polLocations.Values[0];
			string text = string.Empty;
			if (pi.polKey.ToLower() == "software\\policies\\microsoft\\windowsfirewall\\firewallrules")
			{
				DllResourceStringLookup dllResourceStringLookup = new DllResourceStringLookup();
				string text2 = string.Empty;
				string text3 = string.Empty;
				StringBuilder stringBuilder = new StringBuilder();
				StringBuilder stringBuilder2 = new StringBuilder();
				string empty = string.Empty;
				string[] array = sData.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
				bool flag = true;
				foreach (string text4 in array)
				{
					if (!flag)
					{
						stringBuilder2.AppendLine();
					}
					flag = false;
					string[] array3 = text4.Split(new char[] { '=' }, 2);
					if (array3.Length == 1)
					{
						stringBuilder2.Append(text4);
					}
					else
					{
						string text5 = string.Empty;
						if (array3[1].StartsWith("@"))
						{
							text5 = dllResourceStringLookup.LookupResource(array3[1]);
						}
						if (text5.Length == 0)
						{
							text5 = array3[1];
						}
						string text6 = array3[0].ToLower();
						if (!(text6 == "dir"))
						{
							if (text6 == "name")
							{
								text2 = text5;
							}
						}
						else if (text5.ToLower().StartsWith("in"))
						{
							text3 = "Inbound";
						}
						else if (text5.ToLower().StartsWith("out"))
						{
							text3 = "Outbound";
						}
						stringBuilder2.Append(array3[0] + " = " + text5);
					}
				}
				if (text2.Length == 0)
				{
					text2 = pi.polValue;
				}
				if (text3.Length > 0)
				{
					stringBuilder.Append(text3 + ": ");
				}
				stringBuilder.AppendLine(text2);
				text = stringBuilder.ToString() + stringBuilder2.ToString();
			}
			else
			{
				bool flag2 = sData == "0";
				string text6 = pi.polValue.ToLower();
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text6);
				if (num <= 2767350853U)
				{
					if (num <= 1946529693U)
					{
						if (num != 1190081730U)
						{
							if (num != 1493681445U)
							{
								if (num != 1946529693U)
								{
									goto IL_047E;
								}
								if (!(text6 == "disablenotifications"))
								{
									goto IL_047E;
								}
								text = (flag2 ? "Yes" : "No");
								goto IL_047E;
							}
							else
							{
								if (!(text6 == "allowlocalipsecpolicymerge"))
								{
									goto IL_047E;
								}
								goto IL_0456;
							}
						}
						else if (!(text6 == "logdroppedpackets"))
						{
							goto IL_047E;
						}
					}
					else if (num != 2163167192U)
					{
						if (num != 2715968444U)
						{
							if (num != 2767350853U)
							{
								goto IL_047E;
							}
							if (!(text6 == "defaultinboundaction"))
							{
								goto IL_047E;
							}
							text = (flag2 ? "Allow" : "Block (default)");
							goto IL_047E;
						}
						else if (!(text6 == "logsuccessfulconnections"))
						{
							goto IL_047E;
						}
					}
					else
					{
						if (!(text6 == "donotallowexceptions"))
						{
							goto IL_047E;
						}
						text = (flag2 ? "" : "Block all connections");
						goto IL_047E;
					}
					text = (flag2 ? "No (default)" : "Yes");
					goto IL_047E;
				}
				if (num > 3465850272U)
				{
					if (num != 3467830528U)
					{
						if (num != 3469851822U)
						{
							if (num != 4067950496U)
							{
								goto IL_047E;
							}
							if (!(text6 == "disableunicastresponsestomulticastbroadcast"))
							{
								goto IL_047E;
							}
							text = (flag2 ? "Yes (default)" : "No");
							goto IL_047E;
						}
						else if (!(text6 == "logfilesize"))
						{
							goto IL_047E;
						}
					}
					else if (!(text6 == "logfilepath"))
					{
						goto IL_047E;
					}
					text = sData;
					goto IL_047E;
				}
				if (num != 3040397098U)
				{
					if (num != 3211669745U)
					{
						if (num != 3465850272U)
						{
							goto IL_047E;
						}
						if (!(text6 == "defaultoutboundaction"))
						{
							goto IL_047E;
						}
						text = (flag2 ? "Allow (default)" : "Block");
						goto IL_047E;
					}
					else if (!(text6 == "allowlocalpolicymerge"))
					{
						goto IL_047E;
					}
				}
				else
				{
					if (!(text6 == "enablefirewall"))
					{
						goto IL_047E;
					}
					text = (flag2 ? "Off" : "On (recommended)");
					goto IL_047E;
				}
				IL_0456:
				text = (flag2 ? "No" : "Yes (default)");
			}
			IL_047E:
			if (text.Length > 0)
			{
				results.Add(new PolSettingChoice_t(polLocation_t, text));
				return true;
			}
			return false;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00006554 File Offset: 0x00004754
		private bool AppLockerPathLookup(string sHive, string sKey, string sValue, out PolInfo_t pi)
		{
			pi = null;
			string text = "...\\AppLocker";
			string text2 = string.Empty;
			if (sHive.ToLower() != "hklm")
			{
				return false;
			}
			string text3 = sKey.ToLower();
			if (!text3.ToLower().StartsWith("software\\policies\\microsoft\\windows\\srpv2\\"))
			{
				return false;
			}
			string[] array = text3.Substring("software\\policies\\microsoft\\windows\\srpv2\\".Length).Split(new char[] { '\\' });
			string empty = string.Empty;
			string text4 = array[0].ToLower();
			if (!(text4 == "exe"))
			{
				if (!(text4 == "dll"))
				{
					if (!(text4 == "msi"))
					{
						if (!(text4 == "script"))
						{
							if (!(text4 == "appx"))
							{
								return false;
							}
							text += "\\Packaged app Rules";
						}
						else
						{
							text += "\\Script Rules";
						}
					}
					else
					{
						text += "\\Windows Installer Rules";
					}
				}
				else
				{
					text += "\\DLL Rules";
				}
			}
			else
			{
				text += "\\Executable Rules";
			}
			text4 = sValue.ToLower();
			if (text4 == "enforcementmode")
			{
				text2 = "Configured";
			}
			else
			{
				text2 = "Rule " + array[array.Length - 1];
			}
			string text5 = text.ToLower();
			if (!this.m_catPaths.ContainsKey(text5))
			{
				CatPathInfo_t catPathInfo_t = new CatPathInfo_t();
				catPathInfo_t.catKey = text5;
				catPathInfo_t.catPath = text;
				catPathInfo_t.filename = string.Empty;
				this.m_catPaths.Add(text5, catPathInfo_t);
			}
			pi = new PolInfo_t(PolInfo_t.class_t.Machine, sKey, sValue);
			pi.AddLoc(string.Empty, text5, text2, string.Empty, string.Empty, string.Empty);
			return true;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00006708 File Offset: 0x00004908
		private bool PolSettingChoiceLookupAppLocker(PolInfo_t pi, string sData, out List<PolSettingChoice_t> results)
		{
			results = null;
			if (pi.polHive.ToLower() != "hklm")
			{
				return false;
			}
			if (!pi.polKey.ToLower().StartsWith("software\\policies\\microsoft\\windows\\srpv2\\"))
			{
				return false;
			}
			results = new List<PolSettingChoice_t>();
			PolLocation_t polLocation_t = pi.polLocations.Values[0];
			string text = string.Empty;
			string text2 = pi.polValue.ToLower();
			if (!(text2 == "enforcementmode"))
			{
				if (text2 == "value")
				{
					try
					{
						string text3 = sData;
						while (text3.EndsWith("\\r") || text3.EndsWith("\\n"))
						{
							text3 = text3.Substring(0, text3.Length - 2);
						}
						XDocument xdocument = XDocument.Parse(text3);
						string localName = xdocument.Root.Name.LocalName;
						string value = xdocument.Root.Attribute("Action").Value;
						string text4 = SecFmt.SidToName(xdocument.Root.Attribute("UserOrGroupSid").Value);
						string value2 = xdocument.Root.Attribute("Name").Value;
						text = string.Concat(new string[]
						{
							localName,
							" | ",
							value,
							" | ",
							text4,
							" | ",
							value2,
							"\r\n",
							xdocument.ToString()
						});
					}
					catch (Exception ex)
					{
						text = ex.Message;
					}
				}
			}
			else if (!(sData == "0"))
			{
				if (sData == "1")
				{
					text = "Enforce rules";
				}
			}
			else
			{
				text = "Audit only";
			}
			if (text.Length > 0)
			{
				results.Add(new PolSettingChoice_t(polLocation_t, text));
				return true;
			}
			return false;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000068F0 File Offset: 0x00004AF0
		private string AsrGuidToRuleName(string sGuid)
		{
			string text = sGuid.ToLower();
			uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
			if (num <= 1627407579U)
			{
				if (num <= 406843019U)
				{
					if (num != 75692966U)
					{
						if (num != 383358393U)
						{
							if (num == 406843019U)
							{
								if (text == "d4f940ab-401b-4efc-aadc-ad5f3c50688a")
								{
									return "Block all Office applications from creating child processes";
								}
							}
						}
						else if (text == "c1db55ab-c21a-4637-bb3f-a12568109d35")
						{
							return "Use advanced protection against ransomware";
						}
					}
					else if (text == "b2b3f03d-6a65-4f7b-a9c7-1c7ef74a9ba4")
					{
						return "Block untrusted and unsigned processes that run from USB";
					}
				}
				else if (num <= 1341068184U)
				{
					if (num != 965149936U)
					{
						if (num == 1341068184U)
						{
							if (text == "5beb7efe-fd9a-4556-801d-275e5ffc04cc")
							{
								return "Block execution of potentially obfuscated scripts";
							}
						}
					}
					else if (text == "01443614-cd74-433a-b99e-2ecdc07bfc25")
					{
						return "Block executable files from running unless they meet a prevalence, age, or trusted list criterion";
					}
				}
				else if (num != 1590009225U)
				{
					if (num == 1627407579U)
					{
						if (text == "e6db77e5-3df2-4cf1-b95a-636979351e5b")
						{
							return "Block persistence through WMI event subscription";
						}
					}
				}
				else if (text == "be9ba2d9-53ea-4cdc-84e5-9b1eeee46550")
				{
					return "Block executable content from email client and webmail";
				}
			}
			else if (num <= 2393702564U)
			{
				if (num <= 2336109825U)
				{
					if (num != 1992799736U)
					{
						if (num == 2336109825U)
						{
							if (text == "75668c1f-73b5-4cf0-bb93-3ecf5cb7cc84")
							{
								return "Block Office applications from injecting code into other processes";
							}
						}
					}
					else if (text == "d3e037e1-3eb8-44c8-a917-57927947596d")
					{
						return "Block JavaScript or VBScript from launching downloaded executable content";
					}
				}
				else if (num != 2369082782U)
				{
					if (num == 2393702564U)
					{
						if (text == "92e97fa1-2edf-4476-bdd6-9dd0b4dddc7b")
						{
							return "Block Win32 API calls from Office macro";
						}
					}
				}
				else if (text == "7674ba52-37eb-4a4f-a9a1-f0f9a1619a2c")
				{
					return "Block Adobe Reader from creating child processes";
				}
			}
			else if (num <= 3705636373U)
			{
				if (num != 3539369025U)
				{
					if (num == 3705636373U)
					{
						if (text == "9e6c4e1f-7d60-472f-ba1a-a39ef669e4b2")
						{
							return "Block credential stealing from the Windows local security authority subsystem (lsass.exe)";
						}
					}
				}
				else if (text == "3b576869-a4ec-4529-8536-b80a7769e899")
				{
					return "Block Office applications from creating executable content";
				}
			}
			else if (num != 4191794662U)
			{
				if (num == 4262230905U)
				{
					if (text == "d1e49aac-8f56-4280-b9ba-993a6d77406c")
					{
						return "Block process creations originating from PSExec and WMI commands";
					}
				}
			}
			else if (text == "26190899-1602-49e8-8b27-eb1d0a1ce869")
			{
				return "Block Office communication application from creating child processes";
			}
			return string.Empty;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00006B88 File Offset: 0x00004D88
		private bool PolSettingChoiceLookupAttackSurfaceReduction(PolInfo_t pi, string sData, out List<PolSettingChoice_t> results)
		{
			results = null;
			if (pi.polHive.ToLower() != "hklm")
			{
				return false;
			}
			if (pi.polKey.ToLower() != "software\\policies\\microsoft\\windows defender\\windows defender exploit guard\\asr\\rules")
			{
				return false;
			}
			string text = this.AsrGuidToRuleName(pi.polValue);
			if (text.Length == 0)
			{
				return false;
			}
			results = new List<PolSettingChoice_t>();
			PolLocation_t polLocation_t = pi.polLocations.Values[0];
			string text2 = string.Empty;
			if (!(sData == "0"))
			{
				if (!(sData == "1"))
				{
					if (!(sData == "2"))
					{
						text2 = "Unrecognized code (" + sData + ")";
					}
					else
					{
						text2 = "Audit";
					}
				}
				else
				{
					text2 = "Block (enable ASR rule)";
				}
			}
			else
			{
				text2 = "Disable";
			}
			results.Add(new PolSettingChoice_t(polLocation_t, "Rule: " + text + "\r\nState: " + text2));
			return true;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00006C74 File Offset: 0x00004E74
		private void InitSecSettings()
		{
			this.AddSecurityCategories();
			int num = 8;
			RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("software\\microsoft\\windows nt\\currentversion\\SeCEdit\\Reg Values", false);
			if (registryKey != null)
			{
				foreach (string text in registryKey.GetSubKeyNames())
				{
					string text2 = text.Replace('/', '\\');
					if (text2.StartsWith("MACHINE\\"))
					{
						text2 = text2.Substring(num);
					}
					RegistryKey registryKey2 = registryKey.OpenSubKey(text, false);
					if (registryKey2 != null)
					{
						SecSettingsDetails_t secSettingsDetails_t = new SecSettingsDetails_t(registryKey2);
						int num2 = text2.LastIndexOf('\\');
						if (num2 > 0)
						{
							string text3 = text2.Substring(num2 + 1);
							string text4 = text2.Substring(0, num2);
							string sExplainText = secSettingsDetails_t.sExplainText;
							this.AddToPolInfo(PolInfo_t.class_t.SecuritySettings, text4, text3, string.Empty, "SecurityOptions", secSettingsDetails_t.sDisplayName, string.Empty, sExplainText, string.Empty, secSettingsDetails_t);
						}
						registryKey2.Close();
					}
				}
				registryKey.Close();
			}
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00006D68 File Offset: 0x00004F68
		private void AddSecurityCategory(string key, string path)
		{
			CatPathInfo_t catPathInfo_t = new CatPathInfo_t();
			catPathInfo_t.catKey = key;
			catPathInfo_t.catPath = path;
			catPathInfo_t.filename = string.Empty;
			this.m_catPaths.Add(catPathInfo_t.catKey, catPathInfo_t);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00006DA8 File Offset: 0x00004FA8
		private PolInfo_t SecTemplatePathLookup(string sKey, string sValue)
		{
			PolInfo_t polInfo_t = new PolInfo_t();
			polInfo_t.polClass = PolInfo_t.class_t.SecuritySettings;
			string text = string.Empty;
			string empty = string.Empty;
			string text2 = sKey.ToLower();
			if (text2 == "privilege rights")
			{
				if (!SecFmt.PrivilegeDisplayName(sValue, out text))
				{
					text = sValue;
				}
				SecFmt.PrivilegeExplainText(sValue, out empty);
				polInfo_t.AddLoc(string.Empty, "privilege rights", text, string.Empty, empty, string.Empty);
				return polInfo_t;
			}
			if (text2 == "service general setting")
			{
				if (!SecFmt.GetServiceDisplayName(sValue, out text))
				{
					text = sValue;
				}
				polInfo_t.AddLoc(string.Empty, "service general setting", text, string.Empty, empty, string.Empty);
				return polInfo_t;
			}
			if (text2 == "file security")
			{
				polInfo_t.AddLoc(string.Empty, "file security", sValue, string.Empty, empty, string.Empty);
				return polInfo_t;
			}
			if (text2 == "registry keys")
			{
				polInfo_t.AddLoc(string.Empty, "registry keys", sValue, string.Empty, empty, string.Empty);
				return polInfo_t;
			}
			if (text2 == "group membership")
			{
				polInfo_t.AddLoc(string.Empty, "group membership", sValue, string.Empty, empty, string.Empty);
				return polInfo_t;
			}
			if (this.HardcodedNameLookup(sKey, sValue, ref polInfo_t))
			{
				return polInfo_t;
			}
			return null;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00006EEC File Offset: 0x000050EC
		private bool HardcodedNameLookup(string sKey, string sValue, ref PolInfo_t pi)
		{
			string text = string.Empty;
			string text2 = string.Empty;
			SecSettingsDetails_t.DispType_t dispType_t = SecSettingsDetails_t.DispType_t.DisplayNumber;
			string text3 = sKey.ToLower() + "!" + sValue.ToLower();
			string text4 = string.Empty;
			uint num = <PrivateImplementationDetails>.ComputeStringHash(text3);
			if (num <= 1856454257U)
			{
				if (num <= 1051089007U)
				{
					if (num <= 393526568U)
					{
						if (num <= 190915026U)
						{
							if (num != 96007788U)
							{
								if (num == 190915026U)
								{
									if (text3 == "system access!lockoutduration")
									{
										text = "AccountLockout";
										text2 = WSecEdit.LookupUpToLineBreak(60U, "Account lockout duration");
										text4 = WSecEdit.Lookup(1906U);
										goto IL_0C56;
									}
								}
							}
							else if (text3 == "application log!auditlogretentionperiod")
							{
								text = "application log";
								text2 = WSecEdit.LookupUpToLineBreak(78U, "Retention method for application log");
								text4 = WSecEdit.Lookup(2038U);
								goto IL_0C56;
							}
						}
						else if (num != 198325513U)
						{
							if (num != 251389769U)
							{
								if (num == 393526568U)
								{
									if (text3 == "system access!resetlockoutcount")
									{
										text = "AccountLockout";
										text2 = WSecEdit.LookupUpToLineBreak(59U, "Reset account lockout counter after");
										text4 = WSecEdit.Lookup(1908U);
										goto IL_0C56;
									}
								}
							}
							else if (text3 == "system access!minimumpasswordage")
							{
								text = "PasswordPolicy";
								text2 = WSecEdit.LookupUpToLineBreak(52U, "Minimum password age");
								text4 = WSecEdit.Lookup(1902U);
								goto IL_0C56;
							}
						}
						else if (text3 == "system log!restrictguestaccess")
						{
							text = "system log";
							text2 = WSecEdit.LookupUpToLineBreak(275U, "Prevent local guests group from accessing system log");
							text4 = WSecEdit.Lookup(2034U);
							dispType_t = SecSettingsDetails_t.DispType_t.DisplayBoolean;
							goto IL_0C56;
						}
					}
					else if (num <= 430817975U)
					{
						if (num != 407932136U)
						{
							if (num == 430817975U)
							{
								if (text3 == "kerberos policy!maxserviceage")
								{
									text = "kerberos policy";
									text2 = WSecEdit.LookupUpToLineBreak(363U, "Maximum lifetime for service ticket");
									text4 = WSecEdit.Lookup(1910U);
									goto IL_0C56;
								}
							}
						}
						else if (text3 == "system access!lockoutbadcount")
						{
							text = "AccountLockout";
							text2 = WSecEdit.LookupUpToLineBreak(58U, "Account lockout threshold");
							text4 = WSecEdit.Lookup(1907U);
							goto IL_0C56;
						}
					}
					else if (num != 481573214U)
					{
						if (num != 605916470U)
						{
							if (num == 1051089007U)
							{
								if (text3 == "kerberos policy!maxclockskew")
								{
									text = "kerberos policy";
									text2 = WSecEdit.LookupUpToLineBreak(362U, "Maximum tolerance for computer clock synchronization");
									text4 = WSecEdit.Lookup(1913U);
									goto IL_0C56;
								}
							}
						}
						else if (text3 == "event audit!auditaccountmanage")
						{
							text = "event audit";
							text2 = WSecEdit.LookupUpToLineBreak(88U, "Audit account management");
							text4 = WSecEdit.Lookup(1915U);
							goto IL_0C56;
						}
					}
					else if (text3 == "system access!forcelogoffwhenhourexpire")
					{
						text = "SecurityOptions";
						text2 = WSecEdit.LookupUpToLineBreak(63U, "Network security: Force logoff when logon hours expire");
						text4 = WSecEdit.Lookup(2013U);
						dispType_t = SecSettingsDetails_t.DispType_t.DisplayBoolean;
						goto IL_0C56;
					}
				}
				else if (num <= 1358787996U)
				{
					if (num <= 1150017674U)
					{
						if (num != 1135582032U)
						{
							if (num == 1150017674U)
							{
								if (text3 == "event audit!auditprocesstracking")
								{
									text = "event audit";
									text2 = WSecEdit.LookupUpToLineBreak(89U, "Audit process tracking");
									text4 = WSecEdit.Lookup(1921U);
									goto IL_0C56;
								}
							}
						}
						else if (text3 == "event audit!auditlogonevents")
						{
							text = "event audit";
							text2 = WSecEdit.LookupUpToLineBreak(84U, "Audit logon events");
							text4 = WSecEdit.Lookup(1917U);
							goto IL_0C56;
						}
					}
					else if (num != 1199873852U)
					{
						if (num != 1334591101U)
						{
							if (num == 1358787996U)
							{
								if (text3 == "system access!enableadminaccount")
								{
									text = "SecurityOptions";
									text2 = WSecEdit.LookupUpToLineBreak(432U, "Accounts: Administrator account status");
									text4 = WSecEdit.Lookup(1962U);
									dispType_t = SecSettingsDetails_t.DispType_t.DisplayBoolean;
									goto IL_0C56;
								}
							}
						}
						else if (text3 == "system access!enableguestaccount")
						{
							text = "SecurityOptions";
							text2 = WSecEdit.LookupUpToLineBreak(433U, "Accounts: Guest account status");
							text4 = WSecEdit.Lookup(1963U);
							dispType_t = SecSettingsDetails_t.DispType_t.DisplayBoolean;
							goto IL_0C56;
						}
					}
					else if (text3 == "kerberos policy!maxticketage")
					{
						text = "kerberos policy";
						text2 = WSecEdit.LookupUpToLineBreak(364U, "Maximum lifetime for user ticket");
						text4 = WSecEdit.Lookup(1911U);
						goto IL_0C56;
					}
				}
				else if (num <= 1607242576U)
				{
					if (num != 1468891201U)
					{
						if (num == 1607242576U)
						{
							if (text3 == "application log!restrictguestaccess")
							{
								text = "application log";
								text2 = WSecEdit.LookupUpToLineBreak(277U, "Prevent local guests group from accessing application log");
								text4 = WSecEdit.Lookup(2032U);
								dispType_t = SecSettingsDetails_t.DispType_t.DisplayBoolean;
								goto IL_0C56;
							}
						}
					}
					else if (text3 == "security log!maximumlogsize")
					{
						text = "security log";
						text2 = WSecEdit.LookupUpToLineBreak(74U, "Maximum security log size");
						text4 = WSecEdit.Lookup(2030U);
						goto IL_0C56;
					}
				}
				else if (num != 1616513653U)
				{
					if (num != 1693564937U)
					{
						if (num == 1856454257U)
						{
							if (text3 == "kerberos policy!maxrenewage")
							{
								text = "kerberos policy";
								text2 = WSecEdit.LookupUpToLineBreak(365U, "Maximum lifetime for user ticket renewal");
								text4 = WSecEdit.Lookup(1912U);
								goto IL_0C56;
							}
						}
					}
					else if (text3 == "system access!passwordhistorysize")
					{
						text = "PasswordPolicy";
						text2 = WSecEdit.LookupUpToLineBreak(54U, "Enforce password history");
						text4 = WSecEdit.Lookup(1900U);
						goto IL_0C56;
					}
				}
				else if (text3 == "security log!retentiondays")
				{
					text = "security log";
					text2 = WSecEdit.LookupUpToLineBreak(76U, "Retain security log");
					text4 = WSecEdit.Lookup(2036U);
					goto IL_0C56;
				}
			}
			else if (num <= 2965650830U)
			{
				if (num <= 2536825487U)
				{
					if (num <= 2087917701U)
					{
						if (num != 1856608175U)
						{
							if (num == 2087917701U)
							{
								if (text3 == "system access!newadministratorname")
								{
									text = "SecurityOptions";
									text2 = WSecEdit.LookupUpToLineBreak(65U, "Accounts: Rename administrator account");
									text4 = WSecEdit.Lookup(1965U);
									dispType_t = SecSettingsDetails_t.DispType_t.DisplayString;
									goto IL_0C56;
								}
							}
						}
						else if (text3 == "application log!maximumlogsize")
						{
							text = "application log";
							text2 = WSecEdit.LookupUpToLineBreak(77U, "Maximum application log size");
							text4 = WSecEdit.Lookup(2029U);
							goto IL_0C56;
						}
					}
					else if (num != 2481188812U)
					{
						if (num != 2532288785U)
						{
							if (num == 2536825487U)
							{
								if (text3 == "event audit!auditobjectaccess")
								{
									text = "event audit";
									text2 = WSecEdit.LookupUpToLineBreak(85U, "Audit object access");
									text4 = WSecEdit.Lookup(1918U);
									goto IL_0C56;
								}
							}
						}
						else if (text3 == "event audit!auditdsaccess")
						{
							text = "event audit";
							text2 = WSecEdit.LookupUpToLineBreak(272U, "Audit directory service access");
							text4 = WSecEdit.Lookup(1916U);
							goto IL_0C56;
						}
					}
					else if (text3 == "system access!cleartextpassword")
					{
						text = "PasswordPolicy";
						text2 = WSecEdit.LookupUpToLineBreak(359U, "Store passwords using reversible encryption");
						text4 = WSecEdit.Lookup(1905U);
						dispType_t = SecSettingsDetails_t.DispType_t.DisplayBoolean;
						goto IL_0C56;
					}
				}
				else if (num <= 2843025161U)
				{
					if (num != 2684488534U)
					{
						if (num == 2843025161U)
						{
							if (text3 == "system access!lsaanonymousnamelookup")
							{
								text = "SecurityOptions";
								text2 = WSecEdit.LookupUpToLineBreak(57458U, "Network access: Allow anonymous SID/Name translation");
								text4 = WSecEdit.Lookup(2001U);
								dispType_t = SecSettingsDetails_t.DispType_t.DisplayBoolean;
								goto IL_0C56;
							}
						}
					}
					else if (text3 == "security log!restrictguestaccess")
					{
						text = "security log";
						text2 = WSecEdit.LookupUpToLineBreak(276U, "Prevent local guests group from accessing security log");
						text4 = WSecEdit.Lookup(2033U);
						dispType_t = SecSettingsDetails_t.DispType_t.DisplayBoolean;
						goto IL_0C56;
					}
				}
				else if (num != 2876513750U)
				{
					if (num != 2942423058U)
					{
						if (num == 2965650830U)
						{
							if (text3 == "system log!retentiondays")
							{
								text = "system log";
								text2 = WSecEdit.LookupUpToLineBreak(73U, "Retain system log");
								text4 = WSecEdit.Lookup(2037U);
								goto IL_0C56;
							}
						}
					}
					else if (text3 == "security log!auditlogretentionperiod")
					{
						text = "security log";
						text2 = WSecEdit.LookupUpToLineBreak(75U, "Retention method for security log");
						text4 = WSecEdit.Lookup(2039U);
						goto IL_0C56;
					}
				}
				else if (text3 == "event audit!auditsystemevents")
				{
					text = "event audit";
					text2 = WSecEdit.LookupUpToLineBreak(83U, "Audit system events");
					text4 = WSecEdit.Lookup(1922U);
					goto IL_0C56;
				}
			}
			else if (num <= 3391356481U)
			{
				if (num <= 3105508956U)
				{
					if (num != 3041151816U)
					{
						if (num == 3105508956U)
						{
							if (text3 == "event audit!auditaccountlogon")
							{
								text = "event audit";
								text2 = WSecEdit.LookupUpToLineBreak(273U, "Audit account logon events");
								text4 = WSecEdit.Lookup(1914U);
								goto IL_0C56;
							}
						}
					}
					else if (text3 == "system access!passwordcomplexity")
					{
						text = "PasswordPolicy";
						text2 = WSecEdit.LookupUpToLineBreak(55U, "Password must meet complexity requirement");
						text4 = WSecEdit.Lookup(1904U);
						dispType_t = SecSettingsDetails_t.DispType_t.DisplayBoolean;
						goto IL_0C56;
					}
				}
				else if (num != 3192741020U)
				{
					if (num != 3199603362U)
					{
						if (num == 3391356481U)
						{
							if (text3 == "system log!auditlogretentionperiod")
							{
								text = "system log";
								text2 = WSecEdit.LookupUpToLineBreak(72U, "Retention method for system log");
								text4 = WSecEdit.Lookup(2040U);
								goto IL_0C56;
							}
						}
					}
					else if (text3 == "system access!minimumpasswordlength")
					{
						text = "PasswordPolicy";
						text2 = WSecEdit.LookupUpToLineBreak(53U, "Minimum password length");
						text4 = WSecEdit.Lookup(1903U);
						goto IL_0C56;
					}
				}
				else if (text3 == "system access!newguestname")
				{
					text = "SecurityOptions";
					text2 = WSecEdit.LookupUpToLineBreak(67U, "Accounts: Rename guest account");
					text4 = WSecEdit.Lookup(1966U);
					dispType_t = SecSettingsDetails_t.DispType_t.DisplayString;
					goto IL_0C56;
				}
			}
			else if (num <= 3855885944U)
			{
				if (num != 3452817044U)
				{
					if (num != 3776202551U)
					{
						if (num == 3855885944U)
						{
							if (text3 == "kerberos policy!ticketvalidateclient")
							{
								text = "kerberos policy";
								text2 = WSecEdit.LookupUpToLineBreak(361U, "Enforce user logon restrictions");
								text4 = WSecEdit.Lookup(1909U);
								dispType_t = SecSettingsDetails_t.DispType_t.DisplayBoolean;
								goto IL_0C56;
							}
						}
					}
					else if (text3 == "system access!maximumpasswordage")
					{
						text = "PasswordPolicy";
						text2 = WSecEdit.LookupUpToLineBreak(51U, "Maximum password age");
						text4 = WSecEdit.Lookup(1901U);
						goto IL_0C56;
					}
				}
				else if (text3 == "system log!maximumlogsize")
				{
					text = "system log";
					text2 = WSecEdit.LookupUpToLineBreak(71U, "Maximum system log size");
					text4 = WSecEdit.Lookup(2031U);
					goto IL_0C56;
				}
			}
			else if (num != 3974531932U)
			{
				if (num != 4125653643U)
				{
					if (num == 4271391932U)
					{
						if (text3 == "event audit!auditpolicychange")
						{
							text = "event audit";
							text2 = WSecEdit.LookupUpToLineBreak(87U, "Audit policy change");
							text4 = WSecEdit.Lookup(1919U);
							goto IL_0C56;
						}
					}
				}
				else if (text3 == "application log!retentiondays")
				{
					text = "application log";
					text2 = WSecEdit.LookupUpToLineBreak(79U, "Retain application log");
					text4 = WSecEdit.Lookup(2035U);
					goto IL_0C56;
				}
			}
			else if (text3 == "event audit!auditprivilegeuse")
			{
				text = "event audit";
				text2 = WSecEdit.LookupUpToLineBreak(86U, "Audit privilege use");
				text4 = WSecEdit.Lookup(1920U);
				goto IL_0C56;
			}
			return false;
			IL_0C56:
			text4 = text4.Trim().Replace("\r", "\\r").Replace("\n", "\\n")
				.Replace("\t", "\\t");
			pi.secSettingsDetails = new SecSettingsDetails_t(text2, dispType_t);
			pi.AddLoc(string.Empty, text, text2, string.Empty, text4, string.Empty);
			return true;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00007BB0 File Offset: 0x00005DB0
		private void AddSecurityCategories()
		{
			string text = WSecEdit.LookupWithDefault(266U, "Account Policies");
			string text2 = WSecEdit.LookupWithDefault(50U, "Password Policy");
			string text3 = WSecEdit.LookupWithDefault(57U, "Account Lockout Policy");
			string text4 = WSecEdit.LookupWithDefault(360U, "Kerberos Policy");
			string text5 = WSecEdit.LookupWithDefault(268U, "Local Policies");
			string text6 = WSecEdit.LookupWithDefault(81U, "Audit Policy");
			string text7 = WSecEdit.LookupWithDefault(13U, "User Rights Assignment");
			string text8 = WSecEdit.LookupWithDefault(90U, "Security Options");
			string text9 = WSecEdit.LookupWithDefault(70U, "Event Log");
			string text10 = WSecEdit.LookupWithDefault(14U, "Restricted Groups");
			string text11 = WSecEdit.LookupWithDefault(15U, "System Services");
			string text12 = WSecEdit.LookupWithDefault(16U, "Registry");
			string text13 = WSecEdit.LookupWithDefault(17U, "File System");
			this.AddSecurityCategory("PasswordPolicy", text + "\\" + text2);
			this.AddSecurityCategory("AccountLockout", text + "\\" + text3);
			this.AddSecurityCategory("kerberos policy", text + "\\" + text4);
			this.AddSecurityCategory("event audit", text5 + "\\" + text6);
			this.AddSecurityCategory("privilege rights", text5 + "\\" + text7);
			this.AddSecurityCategory("SecurityOptions", text5 + "\\" + text8);
			this.AddSecurityCategory("application log", text9);
			this.AddSecurityCategory("security log", text9);
			this.AddSecurityCategory("system log", text9);
			this.AddSecurityCategory("group membership", text10);
			this.AddSecurityCategory("service general setting", text11);
			this.AddSecurityCategory("registry keys", text12);
			this.AddSecurityCategory("file security", text13);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00007D64 File Offset: 0x00005F64
		private bool PolSettingChoiceLookupSecSettings(PolInfo_t pi, string sData, out List<PolSettingChoice_t> results)
		{
			results = new List<PolSettingChoice_t>();
			PolLocation_t polLocation_t = pi.polLocations.Values[0];
			if (pi.secSettingsDetails != null)
			{
				results.Add(new PolSettingChoice_t(polLocation_t, pi.secSettingsDetails.FormatValue(sData)));
				return true;
			}
			if (polLocation_t.category == "service general setting")
			{
				StringBuilder stringBuilder = new StringBuilder();
				string text = SecFmt.SvcStartConfig(sData);
				string text2 = SecFmt.FormatSddlData(sData);
				if (text.Length > 0)
				{
					stringBuilder.Append("Service start: " + text);
				}
				stringBuilder.Append(text2);
				results.Add(new PolSettingChoice_t(polLocation_t, stringBuilder.ToString()));
				return true;
			}
			return true;
		}

		// Token: 0x06000073 RID: 115
		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern IntPtr LoadLibraryEx(string lpFileName, IntPtr hFile, uint dwFlags);

		// Token: 0x06000074 RID: 116
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		private static extern int LoadString(IntPtr hInstance, uint uID, StringBuilder lpBuffer, int nBufferMax);

		// Token: 0x06000075 RID: 117
		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern bool FreeLibrary(IntPtr hModule);

		// Token: 0x06000076 RID: 118
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
		private static extern uint GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, uint nSize, string lpFileName);

		// Token: 0x06000077 RID: 119
		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		private static extern uint GetPrivateProfileSection(string lpAppName, IntPtr lpReturnedString, uint nSize, string lpFileName);

		// Token: 0x06000078 RID: 120 RVA: 0x00007E0C File Offset: 0x0000600C
		private static bool GetPrivateProfileSection(string appName, string fileName, out string[] section)
		{
			section = null;
			if (!File.Exists(fileName))
			{
				return false;
			}
			uint num = 32767U;
			IntPtr intPtr = Marshal.AllocCoTaskMem((int)num);
			uint privateProfileSection = GPLookup_t.GetPrivateProfileSection(appName, intPtr, num, fileName);
			if (privateProfileSection == num - 2U || privateProfileSection == 0U)
			{
				Marshal.FreeCoTaskMem(intPtr);
				return false;
			}
			string text = Marshal.PtrToStringAuto(intPtr, (int)(privateProfileSection - 1U));
			section = text.Split(new char[1]);
			Marshal.FreeCoTaskMem(intPtr);
			return true;
		}

		// Token: 0x040000F1 RID: 241
		public const string sLookupHKLM = "HKLM";

		// Token: 0x040000F2 RID: 242
		public const string sLookupHKCU = "HKCU";

		// Token: 0x040000F3 RID: 243
		public const string sLookupSecTemplate = "Security Template";

		// Token: 0x040000F4 RID: 244
		public const string sLookupAudit = "Audit Policy";

		// Token: 0x040000F6 RID: 246
		private static bool s_initialized = false;

		// Token: 0x040000F7 RID: 247
		private static GPLookup_t s_GPLookup = new GPLookup_t();

		// Token: 0x040000F8 RID: 248
		private static string s_AdmxDirectory = Path.Combine(Environment.GetEnvironmentVariable("windir"), "PolicyDefinitions");

		// Token: 0x040000F9 RID: 249
		private const uint MUI_LANGUAGE_NAME = 8U;

		// Token: 0x040000FA RID: 250
		private const uint MUI_USER_PREFERRED_UI_LANGUAGES = 16U;

		// Token: 0x040000FB RID: 251
		private Dictionary<string, CatPathInfo_t> m_catPaths = new Dictionary<string, CatPathInfo_t>();

		// Token: 0x040000FC RID: 252
		private Dictionary<string, PolInfo_t> m_polInfo = new Dictionary<string, PolInfo_t>();

		// Token: 0x040000FD RID: 253
		private Dictionary<string, GPLookup_t.XDocAndNSMgr> m_AdmxLookups = new Dictionary<string, GPLookup_t.XDocAndNSMgr>();

		// Token: 0x040000FE RID: 254
		private Dictionary<string, GPLookup_t.XDocAndNSMgr> m_AdmlLookups = new Dictionary<string, GPLookup_t.XDocAndNSMgr>();

		// Token: 0x040000FF RID: 255
		private Dictionary<string, GPLookup_t.AdvAuditData_t> m_AdvAuditLookupByGuid = new Dictionary<string, GPLookup_t.AdvAuditData_t>();

		// Token: 0x04000100 RID: 256
		private Dictionary<string, string> m_AdvAuditLookupExplainText = new Dictionary<string, string>();

		// Token: 0x04000101 RID: 257
		private const uint MSG_FRIENDLY_DOMAIN_PROFILE = 536881925U;

		// Token: 0x04000102 RID: 258
		private const uint MSG_FRIENDLY_STANDARD_PROFILE = 536881926U;

		// Token: 0x04000103 RID: 259
		private const uint MSG_FRIENDLY_PUBLIC_PROFILE = 536882007U;

		// Token: 0x04000104 RID: 260
		private const string m_sCatPasswordPolicy = "PasswordPolicy";

		// Token: 0x04000105 RID: 261
		private const string m_sCatAccountLockout = "AccountLockout";

		// Token: 0x04000106 RID: 262
		private const string m_sCatUserRightsAssignment = "privilege rights";

		// Token: 0x04000107 RID: 263
		private const string m_sCatSecurityOptions = "SecurityOptions";

		// Token: 0x04000108 RID: 264
		private const string m_sCatServiceGeneralSetting = "service general setting";

		// Token: 0x04000109 RID: 265
		private const string m_sCatFileSecurity = "file security";

		// Token: 0x0400010A RID: 266
		private const string m_sCatRegistryKeys = "registry keys";

		// Token: 0x0400010B RID: 267
		private const string m_sCatGroupMembership = "group membership";

		// Token: 0x0400010C RID: 268
		private const string m_sCatLegacyAudit = "event audit";

		// Token: 0x0400010D RID: 269
		private const string m_sCatSystemAccess = "system access";

		// Token: 0x0400010E RID: 270
		private const string m_sCatKerberosPolicy = "kerberos policy";

		// Token: 0x0400010F RID: 271
		private const string m_sCatApplicationLog = "application log";

		// Token: 0x04000110 RID: 272
		private const string m_sCatSystemLog = "system log";

		// Token: 0x04000111 RID: 273
		private const string m_sCatSecurityLog = "security log";

		// Token: 0x04000112 RID: 274
		private const int LOAD_LIBRARY_AS_DATAFILE = 2;

		// Token: 0x0200002C RID: 44
		// (Invoke) Token: 0x0600007B RID: 123
		public delegate void ProgressIndicator(int minimum, int maximum, int progress);

		// Token: 0x0200002D RID: 45
		private struct XDocAndNSMgr
		{
			// Token: 0x0600007E RID: 126 RVA: 0x00007E70 File Offset: 0x00006070
			public XDocAndNSMgr(string filename, string defNamespace)
			{
				this.xdoc = new XmlDocument();
				this.xdoc.Load(filename);
				this.ns = new XmlNamespaceManager(this.xdoc.NameTable);
				this.ns.AddNamespace(defNamespace, this.xdoc.DocumentElement.NamespaceURI);
			}

			// Token: 0x04000113 RID: 275
			public XmlDocument xdoc;

			// Token: 0x04000114 RID: 276
			public XmlNamespaceManager ns;
		}

		// Token: 0x0200002E RID: 46
		public class AdvAuditData_t
		{
			// Token: 0x04000115 RID: 277
			public string sCategoryName;

			// Token: 0x04000116 RID: 278
			public string sSubcategoryName;

			// Token: 0x04000117 RID: 279
			public string sExplainText;
		}
	}
}
