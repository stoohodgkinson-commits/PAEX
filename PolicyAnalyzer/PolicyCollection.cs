using System;
using System.Collections.Generic;
using GPLookup;

namespace PolicyAnalyzer
{
	// Token: 0x0200005E RID: 94
	public class PolicyCollection
	{
		// Token: 0x06000150 RID: 336 RVA: 0x00003186 File Offset: 0x00001386
		public PolicyCollection()
		{
			this.m_polCollectionData = new SortedDictionary<string, PolicyItemCollection_t>();
			this.m_nameAndPolicyRules = null;
		}

		// Token: 0x06000151 RID: 337 RVA: 0x000031A0 File Offset: 0x000013A0
		public int CollectionCount()
		{
			if (this.m_nameAndPolicyRules != null)
			{
				return this.m_nameAndPolicyRules.Length;
			}
			return 0;
		}

		// Token: 0x06000152 RID: 338 RVA: 0x000031B4 File Offset: 0x000013B4
		public SortedDictionary<string, PolicyItemCollection_t>.KeyCollection Keys()
		{
			return this.m_polCollectionData.Keys;
		}

		// Token: 0x06000153 RID: 339 RVA: 0x000031C1 File Offset: 0x000013C1
		public PolicyItemCollection_t KeyLookup(string key)
		{
			return this.m_polCollectionData[key];
		}

		// Token: 0x06000154 RID: 340 RVA: 0x000031CF File Offset: 0x000013CF
		public void LoadData(GPLookup_t gpLookup, NameAndPolicyRules_t[] nameAndPolicyRules)
		{
			this.m_gpLookup = gpLookup;
			this.m_nameAndPolicyRules = nameAndPolicyRules;
			this.ReloadData();
		}

		// Token: 0x06000155 RID: 341 RVA: 0x0000ED60 File Offset: 0x0000CF60
		public void ReloadData()
		{
			this.m_polCollectionData.Clear();
			int num = this.m_nameAndPolicyRules.Length;
			for (int i = 0; i < num; i++)
			{
				foreach (PolicyRules.PolicyItem_t policyItem_t in this.m_nameAndPolicyRules[i].m_rules.m_PolicyItems.Values)
				{
					if (policyItem_t.DeleteAllVals.Count > 0)
					{
						string text = PolicyItemCollection_t.SortKey(policyItem_t.Config, policyItem_t.KeyOrGroup, string.Empty);
						PolicyItemCollection_t policyItemCollection_t;
						if (this.m_polCollectionData.ContainsKey(text))
						{
							policyItemCollection_t = this.m_polCollectionData[text];
						}
						else
						{
							policyItemCollection_t = new PolicyItemCollection_t(this.m_gpLookup, num, policyItem_t.Config, policyItem_t.KeyOrGroup);
							this.m_polCollectionData.Add(policyItemCollection_t.SortKey(), policyItemCollection_t);
						}
						SortedList<string, PolicyRules.PolicySettingData_t> sortedList = (policyItemCollection_t.m_settingData[i] = new SortedList<string, PolicyRules.PolicySettingData_t>());
						foreach (PolicyRules.FileAndGpo fileAndGpo in policyItem_t.DeleteAllVals)
						{
							string text2 = (fileAndGpo.gpoName + "!" + fileAndGpo.srcFile).ToUpper();
							if (!sortedList.ContainsKey(text2))
							{
								sortedList.Add(text2, new PolicyRules.PolicySettingData_t(fileAndGpo.srcFile, fileAndGpo.gpoName));
							}
						}
					}
					foreach (PolicyRules.PolicySettingValue_t policySettingValue_t in policyItem_t.PolicyValueItems.Values)
					{
						string text3 = PolicyItemCollection_t.SortKey(policyItem_t.Config, policyItem_t.KeyOrGroup, policySettingValue_t.Value);
						PolicyItemCollection_t policyItemCollection_t2;
						if (this.m_polCollectionData.ContainsKey(text3))
						{
							policyItemCollection_t2 = this.m_polCollectionData[text3];
						}
						else
						{
							policyItemCollection_t2 = new PolicyItemCollection_t(this.m_gpLookup, num, policyItem_t.Config, policyItem_t.KeyOrGroup, policySettingValue_t.Value, policySettingValue_t.specialContent);
							this.m_polCollectionData.Add(policyItemCollection_t2.SortKey(), policyItemCollection_t2);
						}
						policyItemCollection_t2.m_settingData[i] = policySettingValue_t.SettingDataItems;
					}
				}
			}
		}

		// Token: 0x0400070E RID: 1806
		private SortedDictionary<string, PolicyItemCollection_t> m_polCollectionData;

		// Token: 0x0400070F RID: 1807
		private GPLookup_t m_gpLookup;

		// Token: 0x04000710 RID: 1808
		public NameAndPolicyRules_t[] m_nameAndPolicyRules;
	}
}
