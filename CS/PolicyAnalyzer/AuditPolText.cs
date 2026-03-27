using System;
using GPLookup;
using SharedCode;

namespace PolicyAnalyzer
{
	// Token: 0x02000058 RID: 88
	public class AuditPolText
	{
		// Token: 0x06000141 RID: 321 RVA: 0x0000DF5C File Offset: 0x0000C15C
		public static string GetSubcategoryName(string sSubcatGuid, out string sCategoryName)
		{
			sSubcatGuid = sSubcatGuid.ToUpper();
			sCategoryName = "System";
			if (sSubcatGuid == "{0CCE9210-69AE-11D9-BED3-505054503030}")
			{
				return "Security State Change";
			}
			if (sSubcatGuid == "{0CCE9211-69AE-11D9-BED3-505054503030}")
			{
				return "Security System Extension";
			}
			if (sSubcatGuid == "{0CCE9212-69AE-11D9-BED3-505054503030}")
			{
				return "System Integrity";
			}
			if (sSubcatGuid == "{0CCE9213-69AE-11D9-BED3-505054503030}")
			{
				return "IPsec Driver";
			}
			if (sSubcatGuid == "{0CCE9214-69AE-11D9-BED3-505054503030}")
			{
				return "Other System Events";
			}
			sCategoryName = "Logon/Logoff";
			uint num = <PrivateImplementationDetails>.ComputeStringHash(sSubcatGuid);
			if (num <= 1927843811U)
			{
				if (num <= 1268853769U)
				{
					if (num != 717925556U)
					{
						if (num == 1268853769U)
						{
							if (sSubcatGuid == "{0CCE9215-69AE-11D9-BED3-505054503030}")
							{
								return "Logon";
							}
						}
					}
					else if (sSubcatGuid == "{0CCE9243-69AE-11D9-BED3-505054503030}")
					{
						return "Network Policy Server";
					}
				}
				else if (num != 1716813584U)
				{
					if (num != 1775334974U)
					{
						if (num == 1927843811U)
						{
							if (sSubcatGuid == "{0CCE9217-69AE-11D9-BED3-505054503030}")
							{
								return "Account Lockout";
							}
						}
					}
					else if (sSubcatGuid == "{0CCE9249-69AE-11D9-BED3-505054503030}")
					{
						return "Group Membership";
					}
				}
				else if (sSubcatGuid == "{0CCE9247-69AE-11D9-BED3-505054503030}")
				{
					return "User / Device Claims";
				}
			}
			else if (num <= 2475143375U)
			{
				if (num != 1944335557U)
				{
					if (num != 2165112858U)
					{
						if (num == 2475143375U)
						{
							if (sSubcatGuid == "{0CCE921C-69AE-11D9-BED3-505054503030}")
							{
								return "Other Logon/Logoff Events";
							}
						}
					}
					else if (sSubcatGuid == "{0CCE921B-69AE-11D9-BED3-505054503030}")
					{
						return "Special Logon";
					}
				}
				else if (sSubcatGuid == "{0CCE921A-69AE-11D9-BED3-505054503030}")
				{
					return "IPsec Extended Mode";
				}
			}
			else if (num != 3241410078U)
			{
				if (num != 3317890285U)
				{
					if (num == 3734523376U)
					{
						if (sSubcatGuid == "{0CCE9218-69AE-11D9-BED3-505054503030}")
						{
							return "IPsec Main Mode";
						}
					}
				}
				else if (sSubcatGuid == "{0CCE9219-69AE-11D9-BED3-505054503030}")
				{
					return "IPsec Quick Mode";
				}
			}
			else if (sSubcatGuid == "{0CCE9216-69AE-11D9-BED3-505054503030}")
			{
				return "Logoff";
			}
			sCategoryName = "Object Access";
			num = <PrivateImplementationDetails>.ComputeStringHash(sSubcatGuid);
			if (num <= 1973193862U)
			{
				if (num <= 708877049U)
				{
					if (num != 27027470U)
					{
						if (num != 377673101U)
						{
							if (num == 708877049U)
							{
								if (sSubcatGuid == "{0CCE921E-69AE-11D9-BED3-505054503030}")
								{
									return "Registry";
								}
							}
						}
						else if (sSubcatGuid == "{0CCE9246-69AE-11D9-BED3-505054503030}")
						{
							return "Central Policy Staging";
						}
					}
					else if (sSubcatGuid == "{0CCE921F-69AE-11D9-BED3-505054503030}")
					{
						return "Kernel Object";
					}
				}
				else if (num <= 1465638706U)
				{
					if (num != 1430463296U)
					{
						if (num == 1465638706U)
						{
							if (sSubcatGuid == "{0CCE9227-69AE-11D9-BED3-505054503030}")
							{
								return "Other Object Access Events";
							}
						}
					}
					else if (sSubcatGuid == "{0CCE9225-69AE-11D9-BED3-505054503030}")
					{
						return "Filtering Platform Packet Drop";
					}
				}
				else if (num != 1733688898U)
				{
					if (num == 1973193862U)
					{
						if (sSubcatGuid == "{0CCE9223-69AE-11D9-BED3-505054503030}")
						{
							return "Handle Manipulation";
						}
					}
				}
				else if (sSubcatGuid == "{0CCE9245-69AE-11D9-BED3-505054503030}")
				{
					return "Removable Storage";
				}
			}
			else if (num <= 3322735851U)
			{
				if (num != 2002463972U)
				{
					if (num != 2786192519U)
					{
						if (num == 3322735851U)
						{
							if (sSubcatGuid == "{0CCE9222-69AE-11D9-BED3-505054503030}")
							{
								return "Application Generated";
							}
						}
					}
					else if (sSubcatGuid == "{0CCE9226-69AE-11D9-BED3-505054503030}")
					{
						return "Filtering Platform Connection";
					}
				}
				else if (sSubcatGuid == "{0CCE9221-69AE-11D9-BED3-505054503030}")
				{
					return "Certification Services";
				}
			}
			else if (num <= 3690239052U)
			{
				if (num != 3551749655U)
				{
					if (num == 3690239052U)
					{
						if (sSubcatGuid == "{0CCE921D-69AE-11D9-BED3-505054503030}")
						{
							return "File System";
						}
					}
				}
				else if (sSubcatGuid == "{0CCE9244-69AE-11D9-BED3-505054503030}")
				{
					return "Detailed File Share";
				}
			}
			else if (num != 4110281981U)
			{
				if (num == 4114343889U)
				{
					if (sSubcatGuid == "{0CCE9220-69AE-11D9-BED3-505054503030}")
					{
						return "SAM";
					}
				}
			}
			else if (sSubcatGuid == "{0CCE9224-69AE-11D9-BED3-505054503030}")
			{
				return "File Share";
			}
			sCategoryName = "Privilege Use";
			if (sSubcatGuid == "{0CCE9228-69AE-11D9-BED3-505054503030}")
			{
				return "Sensitive Privilege Use";
			}
			if (sSubcatGuid == "{0CCE9229-69AE-11D9-BED3-505054503030}")
			{
				return "Non Sensitive Privilege Use";
			}
			if (sSubcatGuid == "{0CCE922A-69AE-11D9-BED3-505054503030}")
			{
				return "Other Privilege Use Events";
			}
			sCategoryName = "Detailed Tracking";
			if (sSubcatGuid == "{0CCE922B-69AE-11D9-BED3-505054503030}")
			{
				return "Process Creation";
			}
			if (sSubcatGuid == "{0CCE922C-69AE-11D9-BED3-505054503030}")
			{
				return "Process Termination";
			}
			if (sSubcatGuid == "{0CCE922D-69AE-11D9-BED3-505054503030}")
			{
				return "DPAPI Activity";
			}
			if (sSubcatGuid == "{0CCE922E-69AE-11D9-BED3-505054503030}")
			{
				return "RPC Events";
			}
			if (sSubcatGuid == "{0CCE9248-69AE-11D9-BED3-505054503030}")
			{
				return "Plug and Play Events";
			}
			if (sSubcatGuid == "{0CCE924A-69AE-11D9-BED3-505054503030}")
			{
				return "Token Right Adjusted";
			}
			sCategoryName = "Policy Change";
			if (sSubcatGuid == "{0CCE922F-69AE-11D9-BED3-505054503030}")
			{
				return "Audit Policy Change";
			}
			if (sSubcatGuid == "{0CCE9230-69AE-11D9-BED3-505054503030}")
			{
				return "Authentication Policy Change";
			}
			if (sSubcatGuid == "{0CCE9231-69AE-11D9-BED3-505054503030}")
			{
				return "Authorization Policy Change";
			}
			if (sSubcatGuid == "{0CCE9232-69AE-11D9-BED3-505054503030}")
			{
				return "MPSSVC Rule-Level Policy Change";
			}
			if (sSubcatGuid == "{0CCE9233-69AE-11D9-BED3-505054503030}")
			{
				return "Filtering Platform Policy Change";
			}
			if (sSubcatGuid == "{0CCE9234-69AE-11D9-BED3-505054503030}")
			{
				return "Other Policy Change Events";
			}
			sCategoryName = "Account Management";
			if (sSubcatGuid == "{0CCE9235-69AE-11D9-BED3-505054503030}")
			{
				return "User Account Management";
			}
			if (sSubcatGuid == "{0CCE9236-69AE-11D9-BED3-505054503030}")
			{
				return "Computer Account Management";
			}
			if (sSubcatGuid == "{0CCE9237-69AE-11D9-BED3-505054503030}")
			{
				return "Security Group Management";
			}
			if (sSubcatGuid == "{0CCE9238-69AE-11D9-BED3-505054503030}")
			{
				return "Distribution Group Management";
			}
			if (sSubcatGuid == "{0CCE9239-69AE-11D9-BED3-505054503030}")
			{
				return "Application Group Management";
			}
			if (sSubcatGuid == "{0CCE923A-69AE-11D9-BED3-505054503030}")
			{
				return "Other Account Management Events";
			}
			sCategoryName = "DS Access";
			if (sSubcatGuid == "{0CCE923B-69AE-11D9-BED3-505054503030}")
			{
				return "Directory Service Access";
			}
			if (sSubcatGuid == "{0CCE923C-69AE-11D9-BED3-505054503030}")
			{
				return "Directory Service Changes";
			}
			if (sSubcatGuid == "{0CCE923D-69AE-11D9-BED3-505054503030}")
			{
				return "Directory Service Replication";
			}
			if (sSubcatGuid == "{0CCE923E-69AE-11D9-BED3-505054503030}")
			{
				return "Detailed Directory Service Replication";
			}
			sCategoryName = "Account Logon";
			if (sSubcatGuid == "{0CCE923F-69AE-11D9-BED3-505054503030}")
			{
				return "Credential Validation";
			}
			if (sSubcatGuid == "{0CCE9240-69AE-11D9-BED3-505054503030}")
			{
				return "Kerberos Service Ticket Operations";
			}
			if (sSubcatGuid == "{0CCE9241-69AE-11D9-BED3-505054503030}")
			{
				return "Other Account Logon Events";
			}
			if (!(sSubcatGuid == "{0CCE9242-69AE-11D9-BED3-505054503030}"))
			{
				sCategoryName = "(Unrecognized GUID)";
				return "";
			}
			return "Kerberos Authentication Service";
		}

		// Token: 0x06000142 RID: 322 RVA: 0x0000E638 File Offset: 0x0000C838
		public static string AuditSetting(string sCode)
		{
			DllResourceStringLookup dllResourceStringLookup = new DllResourceStringLookup();
			if (sCode == "0" || sCode == "4")
			{
				return dllResourceStringLookup.LookupResourceOrDefault("auditpolmsg.dll", 4U, "No Auditing");
			}
			if (sCode == "1")
			{
				return dllResourceStringLookup.LookupResourceOrDefault("auditpolmsg.dll", 1U, "Success");
			}
			if (sCode == "2")
			{
				return dllResourceStringLookup.LookupResourceOrDefault("auditpolmsg.dll", 2U, "Failure");
			}
			if (!(sCode == "3"))
			{
				return "Unrecognized: " + sCode;
			}
			return dllResourceStringLookup.LookupResourceOrDefault("auditpolmsg.dll", 3U, "Success and Failure");
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00003084 File Offset: 0x00001284
		public static string AdvancedAuditPolicyConfiguration()
		{
			return new DllResourceStringLookup().LookupResourceOrDefault("auditpolmsg.dll", 400U, "Advanced Audit Policy Configuration");
		}

		// Token: 0x06000144 RID: 324 RVA: 0x0000309F File Offset: 0x0000129F
		public static string SystemAuditPolicies()
		{
			return new DllResourceStringLookup().LookupResourceOrDefault("auditpolmsg.dll", 100U, "System Audit Policies");
		}

		// Token: 0x06000145 RID: 325 RVA: 0x000030B7 File Offset: 0x000012B7
		public static string AuditOptionSetting(string sCode)
		{
			if (sCode == "0")
			{
				return GPLookup_t.StrDisabled();
			}
			if (!(sCode == "1"))
			{
				return "Unrecognized: " + sCode;
			}
			return GPLookup_t.StrEnabled();
		}
	}
}
