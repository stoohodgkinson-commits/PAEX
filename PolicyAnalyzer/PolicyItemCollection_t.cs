using System;
using System.Collections.Generic;
using System.Text;
using GPLookup;
using SharedCode;

namespace PolicyAnalyzer
{
	// Token: 0x0200005D RID: 93
	public class PolicyItemCollection_t
	{
		// Token: 0x06000149 RID: 329 RVA: 0x0000E6E4 File Offset: 0x0000C8E4
		public PolicyItemCollection_t(GPLookup_t gpLook, int nDataColCount, string sPolType, string sPolGroupOrKey, string sPolSetting, PolicyRules.PolicySettingValue_t.eSpecialContent_t specialContent = PolicyRules.PolicySettingValue_t.eSpecialContent_t.esc_None)
		{
			this.m_gpLookup = gpLook;
			this.m_sPolType = sPolType;
			this.m_sPolGroupOrKey = sPolGroupOrKey;
			this.m_sPolSetting = sPolSetting;
			this.m_specialContent = specialContent;
			this.m_bDeleteAllValues = false;
			this.m_settingData = new SortedList<string, PolicyRules.PolicySettingData_t>[nDataColCount];
			this.InitPolicyConfigAndPath(this.m_gpLookup);
		}

		// Token: 0x0600014A RID: 330 RVA: 0x0000E748 File Offset: 0x0000C948
		public PolicyItemCollection_t(GPLookup_t gpLook, int nDataColCount, string sPolType, string sPolGroupOrKey)
		{
			this.m_gpLookup = gpLook;
			this.m_sPolType = sPolType;
			this.m_sPolGroupOrKey = sPolGroupOrKey;
			this.m_sPolSetting = string.Empty;
			this.m_specialContent = PolicyRules.PolicySettingValue_t.eSpecialContent_t.esc_None;
			this.m_bDeleteAllValues = true;
			this.m_settingData = new SortedList<string, PolicyRules.PolicySettingData_t>[nDataColCount];
			this.InitPolicyConfigAndPath(this.m_gpLookup);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x0000E7B0 File Offset: 0x0000C9B0
		public PolicyItemInContext_t PolicyItemInContext(int itemIndex)
		{
			if (this.m_policyItemsInContext == null)
			{
				this.m_policyItemsInContext = new PolicyItemInContext_t[this.m_settingData.Length];
				bool flag = false;
				List<PolicyItemInContext_t> list = new List<PolicyItemInContext_t>();
				for (int i = 0; i < this.m_settingData.Length; i++)
				{
					this.m_policyItemsInContext[i] = default(PolicyItemInContext_t);
					this.m_policyItemsInContext[i].DisplayText = string.Empty;
					this.m_policyItemsInContext[i].representativeData = null;
					SortedList<string, PolicyRules.PolicySettingData_t> sortedList = this.m_settingData[i];
					if (sortedList == null)
					{
						this.m_policyItemsInContext[i].InItemResult = (this.m_policyItemsInContext[i].InSetResult = eInSetResult_t.eNoSetting);
					}
					else
					{
						foreach (PolicyRules.PolicySettingData_t policySettingData_t in sortedList.Values)
						{
							if (this.m_policyItemsInContext[i].representativeData == null)
							{
								eInSetResult_t eInSetResult_t = eInSetResult_t.eSingleSetting;
								this.m_policyItemsInContext[i].representativeData = policySettingData_t;
								if (policySettingData_t.bDeleteValue && !this.m_bDeleteAllValues)
								{
									this.m_policyItemsInContext[i].DisplayText = "[[[delete]]]";
									eInSetResult_t = eInSetResult_t.eSingleDelete;
								}
								else if (this.m_sPolSetting.Length == 0 && policySettingData_t.SettingType == "REG_NONE")
								{
									this.m_policyItemsInContext[i].DisplayText = "[[[create key]]]";
								}
								else
								{
									this.m_policyItemsInContext[i].DisplayText = policySettingData_t.SettingData;
								}
								this.m_policyItemsInContext[i].InItemResult = (this.m_policyItemsInContext[i].InSetResult = eInSetResult_t);
							}
							else
							{
								if (!this.m_policyItemsInContext[i].representativeData.Equivalent(policySettingData_t))
								{
									this.m_policyItemsInContext[i].representativeData = null;
									this.m_policyItemsInContext[i].DisplayText = "***CONFLICT***";
									this.m_policyItemsInContext[i].InItemResult = (this.m_policyItemsInContext[i].InSetResult = eInSetResult_t.eConflictingSettings);
									flag = true;
									break;
								}
								this.m_policyItemsInContext[i].InItemResult = (this.m_policyItemsInContext[i].InSetResult = (policySettingData_t.bDeleteValue ? eInSetResult_t.eDuplicateDelete : eInSetResult_t.eDuplicateSetting));
							}
						}
						if (this.m_policyItemsInContext[i].representativeData != null)
						{
							list.Add(this.m_policyItemsInContext[i]);
						}
					}
				}
				if (!flag && list.Count > 1)
				{
					PolicyRules.PolicySettingData_t representativeData = list[0].representativeData;
					foreach (PolicyItemInContext_t policyItemInContext_t in list)
					{
						if (!representativeData.Equivalent(policyItemInContext_t.representativeData))
						{
							flag = true;
							break;
						}
					}
				}
				if (flag)
				{
					for (int j = 0; j < this.m_policyItemsInContext.Length; j++)
					{
						if (this.m_policyItemsInContext[j].representativeData != null)
						{
							this.m_policyItemsInContext[j].InSetResult = eInSetResult_t.eConflictingSettings;
						}
					}
				}
			}
			return this.m_policyItemsInContext[itemIndex];
		}

		// Token: 0x0600014C RID: 332 RVA: 0x0000313F File Offset: 0x0000133F
		public string SortKey()
		{
			return PolicyItemCollection_t.SortKey(this.m_sPolType, this.m_sPolGroupOrKey, this.m_sPolSetting);
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00003158 File Offset: 0x00001358
		public static string SortKey(string sPolType, string sPolGroupOrKey, string sPolSetting)
		{
			return string.Concat(new string[] { sPolType, " !", sPolGroupOrKey, " !!", sPolSetting }).ToLower();
		}

		// Token: 0x0600014E RID: 334 RVA: 0x0000EB1C File Offset: 0x0000CD1C
		public string FormattedData(PolicyRules.PolicySettingData_t settingData)
		{
			if (this.m_bDeleteAllValues)
			{
				return string.Empty;
			}
			if (settingData.bDeleteValue)
			{
				return "[[[delete]]]";
			}
			if (this.m_sPolGroupOrKey.ToLower() == "service general setting")
			{
				StringBuilder stringBuilder = new StringBuilder();
				string text = SecFmt.SvcStartConfig(settingData.SettingData);
				string text2 = SecFmt.FormatSddlData(settingData.SettingData);
				if (text.Length > 0)
				{
					stringBuilder.Append("Service start: " + text);
				}
				stringBuilder.Append(text2);
				return stringBuilder.ToString();
			}
			if (this.m_specialContent == PolicyRules.PolicySettingValue_t.eSpecialContent_t.esc_SDDL)
			{
				return SecFmt.FormatSddlData(settingData.SettingData);
			}
			if (this.m_specialContent == PolicyRules.PolicySettingValue_t.eSpecialContent_t.esc_SIDs)
			{
				return SecFmt.FormatSidData(settingData.SettingDataToString(false).Trim(), settingData.chrSortOnSeparator, false);
			}
			List<PolSettingChoice_t> list;
			if (this.m_polInfo != null && this.m_gpLookup.PolSettingChoiceLookup(this.m_polInfo, settingData.SettingData, out list) && list.Count > 0)
			{
				StringBuilder stringBuilder2 = new StringBuilder();
				foreach (PolSettingChoice_t polSettingChoice_t in list)
				{
					if (stringBuilder2.Length > 0)
					{
						stringBuilder2.Append("\r\n");
					}
					stringBuilder2.Append(polSettingChoice_t.SettingChoice);
				}
				return stringBuilder2.ToString();
			}
			return settingData.SettingDataToString(true);
		}

		// Token: 0x0600014F RID: 335 RVA: 0x0000EC84 File Offset: 0x0000CE84
		private void InitPolicyConfigAndPath(GPLookup_t gpLook)
		{
			PolInfo_t polInfo_t = null;
			string text;
			if (gpLook.PolSettingLookup(this.m_sPolType, this.m_sPolGroupOrKey, this.m_sPolSetting, out polInfo_t, out text))
			{
				this.m_polInfo = polInfo_t;
				foreach (PolLocation_t polLocation_t in polInfo_t.polLocations.Values)
				{
					PolicyPath_t policyPath_t = new PolicyPath_t();
					policyPath_t.m_sPolConfig = text;
					policyPath_t.m_sPolName = polLocation_t.dispName;
					policyPath_t.m_sPolNameSubOption = polLocation_t.dispNameSubOption;
					policyPath_t.m_sExplainText = polLocation_t.explainText;
					CatPathInfo_t catPathInfo_t;
					gpLook.CategoryLookup(polLocation_t.category, out catPathInfo_t);
					if (catPathInfo_t != null)
					{
						policyPath_t.m_sPolPath = catPathInfo_t.catPath;
					}
					this.m_polPaths.Add(policyPath_t);
				}
			}
		}

		// Token: 0x04000704 RID: 1796
		public string m_sPolType;

		// Token: 0x04000705 RID: 1797
		public string m_sPolGroupOrKey;

		// Token: 0x04000706 RID: 1798
		public string m_sPolSetting;

		// Token: 0x04000707 RID: 1799
		public PolInfo_t m_polInfo;

		// Token: 0x04000708 RID: 1800
		public List<PolicyPath_t> m_polPaths = new List<PolicyPath_t>();

		// Token: 0x04000709 RID: 1801
		public bool m_bDeleteAllValues;

		// Token: 0x0400070A RID: 1802
		public SortedList<string, PolicyRules.PolicySettingData_t>[] m_settingData;

		// Token: 0x0400070B RID: 1803
		public PolicyRules.PolicySettingValue_t.eSpecialContent_t m_specialContent;

		// Token: 0x0400070C RID: 1804
		private GPLookup_t m_gpLookup;

		// Token: 0x0400070D RID: 1805
		private PolicyItemInContext_t[] m_policyItemsInContext;
	}
}
