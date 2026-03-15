using System;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Principal;
using System.ServiceProcess;
using System.Text;
using GPLookup;

namespace SharedCode
{
	// Token: 0x0200004F RID: 79
	internal class SecFmt
	{
		// Token: 0x06000125 RID: 293 RVA: 0x0000CCA0 File Offset: 0x0000AEA0
		public static string SidToName(SecurityIdentifier sid)
		{
			if (null == sid)
			{
				return "[Null]";
			}
			string text;
			try
			{
				text = ((NTAccount)sid.Translate(typeof(NTAccount))).ToString();
			}
			catch
			{
				if (sid.IsWellKnown(WellKnownSidType.BuiltinAccountOperatorsSid))
				{
					text = "BUILTIN\\Account Operators";
				}
				else if (sid.IsWellKnown(WellKnownSidType.BuiltinPrintOperatorsSid))
				{
					text = "BUILTIN\\Print Operators";
				}
				else if (sid.IsWellKnown(WellKnownSidType.BuiltinSystemOperatorsSid))
				{
					text = "BUILTIN\\Server Operators";
				}
				else
				{
					text = sid.ToString();
				}
			}
			return text;
		}

		// Token: 0x06000126 RID: 294 RVA: 0x0000CD2C File Offset: 0x0000AF2C
		public static string SidToName(string sSid)
		{
			string text;
			try
			{
				if ('*' == sSid[0])
				{
					text = SecFmt.SidToName(new SecurityIdentifier(sSid.Substring(1)));
				}
				else
				{
					text = SecFmt.SidToName(new SecurityIdentifier(sSid));
				}
			}
			catch (Exception)
			{
				text = "";
			}
			return text;
		}

		// Token: 0x06000127 RID: 295 RVA: 0x0000CD80 File Offset: 0x0000AF80
		public static string FormatSidData(string sData, char chrSortOnSeparator, bool bEscapedCRLF = false)
		{
			if (sData.Length > 0)
			{
				StringBuilder stringBuilder = new StringBuilder();
				string[] array = sData.Split(new char[] { chrSortOnSeparator });
				bool flag = true;
				foreach (string text in array)
				{
					if (!flag)
					{
						stringBuilder.Append(bEscapedCRLF ? "\\r\\n" : "\r\n");
					}
					flag = false;
					stringBuilder.Append(text + "  (" + SecFmt.SidToName(text) + ")");
				}
				sData = stringBuilder.ToString();
			}
			else
			{
				sData = "[[Empty]]";
			}
			return sData;
		}

		// Token: 0x06000128 RID: 296 RVA: 0x0000307C File Offset: 0x0000127C
		public static bool FlagPresent(ControlFlags cfMask, ControlFlags cfValue)
		{
			return cfMask == (cfMask & cfValue);
		}

		// Token: 0x06000129 RID: 297 RVA: 0x0000CE10 File Offset: 0x0000B010
		public static string FormatSddlData(string SettingData)
		{
			string[] array = SettingData.Split(new char[] { ',' }, 2);
			if (array.Length == 1 || array.Length == 2)
			{
				string text = array[array.Length - 1].Replace("\"", "");
				if (text.Length > 0)
				{
					StringBuilder stringBuilder = new StringBuilder();
					try
					{
						CommonSecurityDescriptor commonSecurityDescriptor = new CommonSecurityDescriptor(true, false, text);
						ControlFlags controlFlags = commonSecurityDescriptor.ControlFlags;
						if (text.StartsWith("S:") && !text.Contains("D:") && SecFmt.FlagPresent(ControlFlags.DiscretionaryAclPresent, controlFlags))
						{
							controlFlags -= 4;
						}
						if (null != commonSecurityDescriptor.Owner)
						{
							stringBuilder.Append(string.Concat(new string[]
							{
								"Owner:  ",
								commonSecurityDescriptor.Owner.ToString(),
								" (",
								SecFmt.SidToName(commonSecurityDescriptor.Owner),
								")\r\n"
							}));
						}
						stringBuilder.Append("Control flags:  " + controlFlags.ToString() + "\r\n");
						if (SecFmt.FlagPresent(ControlFlags.DiscretionaryAclPresent, controlFlags))
						{
							SecFmt.FormatAcl("DACL", commonSecurityDescriptor.DiscretionaryAcl, ref stringBuilder);
						}
						if (SecFmt.FlagPresent(ControlFlags.SystemAclPresent, controlFlags))
						{
							SecFmt.FormatAcl("SACL", commonSecurityDescriptor.SystemAcl, ref stringBuilder);
						}
						return stringBuilder.ToString().Trim();
					}
					catch (Exception)
					{
					}
				}
			}
			return string.Empty;
		}

		// Token: 0x0600012A RID: 298 RVA: 0x0000CF80 File Offset: 0x0000B180
		public static void FormatAcl(string aclName, CommonAcl acl, ref StringBuilder sb)
		{
			sb.Append(aclName + " - " + acl.Count.ToString() + " entries:\r\n");
			for (int i = 0; i < acl.Count; i++)
			{
				CommonAce commonAce = acl[i] as CommonAce;
				string text;
				switch (commonAce.AceQualifier)
				{
				case AceQualifier.AccessAllowed:
					text = "Allow";
					break;
				case AceQualifier.AccessDenied:
					text = "Deny ";
					break;
				case AceQualifier.SystemAudit:
					text = "Audit";
					break;
				case AceQualifier.SystemAlarm:
					text = "Alarm";
					break;
				default:
					text = "[Unknown]";
					break;
				}
				string text2;
				if (null == commonAce)
				{
					text2 = "[Unexpected ACE construct]";
				}
				else
				{
					text2 = SecFmt.SidToName(commonAce.SecurityIdentifier);
				}
				sb.Append(string.Concat(new string[]
				{
					"\t",
					text,
					" ",
					text2,
					", Perms 0x",
					commonAce.AccessMask.ToString("X"),
					"\r\n\t\tACE flags:  ",
					commonAce.AceFlags.ToString(),
					"\r\n\t\tPropagation:  ",
					commonAce.PropagationFlags.ToString(),
					"\r\n"
				}));
			}
		}

		// Token: 0x0600012B RID: 299 RVA: 0x0000D0D4 File Offset: 0x0000B2D4
		public static string SvcStartConfig(string sCfgValue)
		{
			string text = sCfgValue.Split(new char[] { ',' }, 2)[0];
			if (text == "2")
			{
				return "Automatic";
			}
			if (text == "3")
			{
				return "Manual";
			}
			if (!(text == "4"))
			{
				return "[invalid setting]";
			}
			return "Disabled";
		}

		// Token: 0x0600012C RID: 300
		[DllImport("advapi32.dll", SetLastError = true)]
		private static extern bool LookupPrivilegeDisplayName(string systemName, string privilegeName, StringBuilder displayName, ref uint cbDisplayName, out uint languageId);

		// Token: 0x0600012D RID: 301 RVA: 0x0000D138 File Offset: 0x0000B338
		public static bool PrivilegeDisplayName(string sPrivilegeName, out string sDisplayName)
		{
			sDisplayName = "";
			uint num = 128U;
			StringBuilder stringBuilder = new StringBuilder(128);
			uint num2;
			if (SecFmt.LookupPrivilegeDisplayName(string.Empty, sPrivilegeName, stringBuilder, ref num, out num2))
			{
				sDisplayName = stringBuilder.ToString();
				return true;
			}
			string text = sPrivilegeName.ToLower();
			uint num3 = <PrivateImplementationDetails>.ComputeStringHash(text);
			if (num3 <= 1863015355U)
			{
				if (num3 <= 331571366U)
				{
					if (num3 != 215693542U)
					{
						if (num3 == 331571366U)
						{
							if (text == "senetworklogonright")
							{
								sDisplayName = WSecEdit.LookupWithDefault(233U, "Access this computer from the network");
								return true;
							}
						}
					}
					else if (text == "seremoteinteractivelogonright")
					{
						sDisplayName = WSecEdit.LookupWithDefault(57450U, "Allow log on through Remote Desktop Services");
						return true;
					}
				}
				else if (num3 != 1443783762U)
				{
					if (num3 != 1634348090U)
					{
						if (num3 == 1863015355U)
						{
							if (text == "seservicelogonright")
							{
								sDisplayName = WSecEdit.LookupWithDefault(287U, "Log on as a service");
								return true;
							}
						}
					}
					else if (text == "sedenyinteractivelogonright")
					{
						sDisplayName = WSecEdit.LookupWithDefault(407U, "Deny log on locally");
						return true;
					}
				}
				else if (text == "sedenybatchlogonright")
				{
					sDisplayName = WSecEdit.LookupWithDefault(410U, "Deny log on as a batch job");
					return true;
				}
			}
			else if (num3 <= 2678265617U)
			{
				if (num3 != 2134564668U)
				{
					if (num3 == 2678265617U)
					{
						if (text == "sedenyservicelogonright")
						{
							sDisplayName = WSecEdit.LookupWithDefault(409U, "Deny log on as a service");
							return true;
						}
					}
				}
				else if (text == "seinteractivelogonright")
				{
					sDisplayName = WSecEdit.LookupWithDefault(234U, "Allow log on locally");
					return true;
				}
			}
			else if (num3 != 3278487500U)
			{
				if (num3 != 4013713772U)
				{
					if (num3 == 4200944696U)
					{
						if (text == "sedenynetworklogonright")
						{
							sDisplayName = WSecEdit.LookupWithDefault(408U, "Deny access to this computer from the network");
							return true;
						}
					}
				}
				else if (text == "sedenyremoteinteractivelogonright")
				{
					sDisplayName = WSecEdit.LookupWithDefault(57449U, "Deny log on through Remote Desktop Services");
					return true;
				}
			}
			else if (text == "sebatchlogonright")
			{
				sDisplayName = WSecEdit.LookupWithDefault(286U, "Log on as a batch job");
				return true;
			}
			return false;
		}

		// Token: 0x0600012E RID: 302 RVA: 0x0000D3BC File Offset: 0x0000B5BC
		public static bool PrivilegeExplainText(string sPrivilegeName, out string sExplainText)
		{
			sExplainText = string.Empty;
			string text = sPrivilegeName.ToLower();
			uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
			uint num2;
			if (num <= 1675162006U)
			{
				if (num <= 669355730U)
				{
					if (num <= 220281442U)
					{
						if (num <= 56801106U)
						{
							if (num != 11963686U)
							{
								if (num == 56801106U)
								{
									if (text == "seprofilesingleprocessprivilege")
									{
										num2 = 1954U;
										goto IL_0846;
									}
								}
							}
							else if (text == "seassignprimarytokenprivilege")
							{
								num2 = 1957U;
								goto IL_0846;
							}
						}
						else if (num != 181452120U)
						{
							if (num != 215693542U)
							{
								if (num == 220281442U)
								{
									if (text == "seshutdownprivilege")
									{
										num2 = 1959U;
										goto IL_0846;
									}
								}
							}
							else if (text == "seremoteinteractivelogonright")
							{
								num2 = 1928U;
								goto IL_0846;
							}
						}
						else if (text == "semachineaccountprivilege")
						{
							num2 = 1925U;
							goto IL_0846;
						}
					}
					else if (num <= 331571366U)
					{
						if (num != 228041941U)
						{
							if (num != 282637017U)
							{
								if (num == 331571366U)
								{
									if (text == "senetworklogonright")
									{
										num2 = 1923U;
										goto IL_0846;
									}
								}
							}
							else if (text == "sedelegatesessionuserimpersonateprivilege")
							{
								num2 = 2080U;
								goto IL_0846;
							}
						}
						else if (text == "setimezoneprivilege")
						{
							num2 = 2061U;
							goto IL_0846;
						}
					}
					else if (num != 530503033U)
					{
						if (num != 569221867U)
						{
							if (num == 669355730U)
							{
								if (text == "sesecurityprivilege")
								{
									num2 = 1951U;
									goto IL_0846;
								}
							}
						}
						else if (text == "seenabledelegationprivilege")
						{
							num2 = 1942U;
							goto IL_0846;
						}
					}
					else if (text == "setcbprivilege")
					{
						num2 = 1924U;
						goto IL_0846;
					}
				}
				else if (num <= 1331099051U)
				{
					if (num <= 1056747575U)
					{
						if (num != 848561286U)
						{
							if (num == 1056747575U)
							{
								if (text == "sechangenotifyprivilege")
								{
									num2 = 1930U;
									goto IL_0846;
								}
							}
						}
						else if (text == "sebackupprivilege")
						{
							num2 = 1929U;
							goto IL_0846;
						}
					}
					else if (num != 1069057348U)
					{
						if (num != 1212286811U)
						{
							if (num == 1331099051U)
							{
								if (text == "sedebugprivilege")
								{
									num2 = 1936U;
									goto IL_0846;
								}
							}
						}
						else if (text == "semanagevolumeprivilege")
						{
							num2 = 1953U;
							goto IL_0846;
						}
					}
					else if (text == "sesystemtimeprivilege")
					{
						num2 = 1931U;
						goto IL_0846;
					}
				}
				else if (num <= 1454289968U)
				{
					if (num != 1443783762U)
					{
						if (num != 1452920809U)
						{
							if (num == 1454289968U)
							{
								if (text == "seremoteshutdownprivilege")
								{
									num2 = 1943U;
									goto IL_0846;
								}
							}
						}
						else if (text == "secreateglobalprivilege")
						{
							num2 = 1934U;
							goto IL_0846;
						}
					}
					else if (text == "sedenybatchlogonright")
					{
						num2 = 1938U;
						goto IL_0846;
					}
				}
				else if (num != 1525098717U)
				{
					if (num != 1634348090U)
					{
						if (num == 1675162006U)
						{
							if (text == "sesyncagentprivilege")
							{
								num2 = 1960U;
								goto IL_0846;
							}
						}
					}
					else if (text == "sedenyinteractivelogonright")
					{
						num2 = 1940U;
						goto IL_0846;
					}
				}
				else if (text == "secreatetokenprivilege")
				{
					num2 = 1933U;
					goto IL_0846;
				}
			}
			else if (num <= 2678265617U)
			{
				if (num <= 2134564668U)
				{
					if (num <= 1961695428U)
					{
						if (num != 1863015355U)
						{
							if (num == 1961695428U)
							{
								if (text == "seundockprivilege")
								{
									num2 = 1956U;
									goto IL_0846;
								}
							}
						}
						else if (text == "seservicelogonright")
						{
							num2 = 1950U;
							goto IL_0846;
						}
					}
					else if (num != 2008455841U)
					{
						if (num != 2079307178U)
						{
							if (num == 2134564668U)
							{
								if (text == "seinteractivelogonright")
								{
									num2 = 1927U;
									goto IL_0846;
								}
							}
						}
						else if (text == "seincreasequotaprivilege")
						{
							num2 = 1926U;
							goto IL_0846;
						}
					}
					else if (text == "serelabelprivilege")
					{
						num2 = 2058U;
						goto IL_0846;
					}
				}
				else if (num <= 2571349092U)
				{
					if (num != 2186393723U)
					{
						if (num != 2488973499U)
						{
							if (num == 2571349092U)
							{
								if (text == "secreatepermanentprivilege")
								{
									num2 = 1935U;
									goto IL_0846;
								}
							}
						}
						else if (text == "seauditprivilege")
						{
							num2 = 1944U;
							goto IL_0846;
						}
					}
					else if (text == "secreatepagefileprivilege")
					{
						num2 = 1932U;
						goto IL_0846;
					}
				}
				else if (num != 2592092058U)
				{
					if (num != 2652477411U)
					{
						if (num == 2678265617U)
						{
							if (text == "sedenyservicelogonright")
							{
								num2 = 1939U;
								goto IL_0846;
							}
						}
					}
					else if (text == "seincreaseworkingsetprivilege")
					{
						num2 = 2062U;
						goto IL_0846;
					}
				}
				else if (text == "setakeownershipprivilege")
				{
					num2 = 1961U;
					goto IL_0846;
				}
			}
			else if (num <= 3278487500U)
			{
				if (num <= 2894546309U)
				{
					if (num != 2772283970U)
					{
						if (num != 2880180020U)
						{
							if (num == 2894546309U)
							{
								if (text == "setrustedcredmanaccessprivilege")
								{
									num2 = 2060U;
									goto IL_0846;
								}
							}
						}
						else if (text == "secreatesymboliclinkprivilege")
						{
							num2 = 2057U;
							goto IL_0846;
						}
					}
					else if (text == "seloaddriverprivilege")
					{
						num2 = 1947U;
						goto IL_0846;
					}
				}
				else if (num != 2987892356U)
				{
					if (num != 3134063243U)
					{
						if (num == 3278487500U)
						{
							if (text == "sebatchlogonright")
							{
								num2 = 1949U;
								goto IL_0846;
							}
						}
					}
					else if (text == "seincreasebasepriorityprivilege")
					{
						num2 = 1946U;
						goto IL_0846;
					}
				}
				else if (text == "sesystemenvironmentprivilege")
				{
					num2 = 1952U;
					goto IL_0846;
				}
			}
			else if (num <= 3946148709U)
			{
				if (num != 3406567288U)
				{
					if (num != 3860507380U)
					{
						if (num == 3946148709U)
						{
							if (text == "seimpersonateprivilege")
							{
								num2 = 1945U;
								goto IL_0846;
							}
						}
					}
					else if (text == "selockmemoryprivilege")
					{
						num2 = 1948U;
						goto IL_0846;
					}
				}
				else if (text == "serestoreprivilege")
				{
					num2 = 1958U;
					goto IL_0846;
				}
			}
			else if (num != 4013713772U)
			{
				if (num != 4174809392U)
				{
					if (num == 4200944696U)
					{
						if (text == "sedenynetworklogonright")
						{
							num2 = 1937U;
							goto IL_0846;
						}
					}
				}
				else if (text == "sesystemprofileprivilege")
				{
					num2 = 1955U;
					goto IL_0846;
				}
			}
			else if (text == "sedenyremoteinteractivelogonright")
			{
				num2 = 1941U;
				goto IL_0846;
			}
			return false;
			IL_0846:
			if (num2 != 0U)
			{
				sExplainText = WSecEdit.Lookup(num2).Trim().Replace("\r", "\\r")
					.Replace("\n", "\\n")
					.Replace("\t", "\\t");
			}
			return true;
		}

		// Token: 0x0600012F RID: 303 RVA: 0x0000DC50 File Offset: 0x0000BE50
		public static bool GetServiceDisplayName(string sSvcName, out string sSvcDisplayName)
		{
			sSvcDisplayName = string.Empty;
			ServiceController serviceController = null;
			bool flag;
			try
			{
				serviceController = new ServiceController(sSvcName.Trim(new char[] { '"' }));
				sSvcDisplayName = serviceController.DisplayName;
				flag = true;
			}
			catch (Exception)
			{
				flag = false;
			}
			finally
			{
				if (serviceController != null)
				{
					serviceController.Dispose();
				}
			}
			return flag;
		}

		// Token: 0x06000130 RID: 304 RVA: 0x0000DCB8 File Offset: 0x0000BEB8
		public static string EncodeForXml(string str)
		{
			return str.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;")
				.Replace("'", "&apos;")
				.Replace("\"", "&quot;");
		}
	}
}
