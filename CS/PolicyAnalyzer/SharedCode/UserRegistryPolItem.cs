using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Xml;
using Microsoft.Win32;

namespace SharedCode
{
	// Token: 0x0200003D RID: 61
	internal class UserRegistryPolItem : BaselineItem
	{
		// Token: 0x060000B9 RID: 185 RVA: 0x0000A074 File Offset: 0x00008274
		public UserRegistryPolItem(XmlNode node)
		{
			this.m_regHelper = new RegistryItemHelper(this, "UserConfig");
			base.ReadStdElems(node);
			this.m_regHelper.SetKeyValueType(node["Key"].InnerText, node["Value"].InnerText, node["RegType"].InnerText);
			this.m_sBaselineValue = node["RegData"].InnerText;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x0000A0F0 File Offset: 0x000082F0
		public override string SortKey()
		{
			return string.Concat(new string[]
			{
				base.GetType().Name,
				"!",
				this.m_regHelper.m_sKey,
				"!",
				this.m_regHelper.m_sValue
			}).ToLower();
		}

		// Token: 0x060000BB RID: 187 RVA: 0x0000A148 File Offset: 0x00008348
		public override void Evaluate(EffectiveStateSources sources, XmlDocument xOutputDoc)
		{
			if (sources.bMultiUser)
			{
				using (List<InteractiveUserSessionInfo>.Enumerator enumerator = sources.lstUserSessions.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						InteractiveUserSessionInfo interactiveUserSessionInfo = enumerator.Current;
						RegistryKey registryKey = Registry.Users.OpenSubKey(interactiveUserSessionInfo.UserSID, false);
						string text = registryKey.ToString();
						this.m_regHelper.RetrieveRegistryValue(registryKey);
						registryKey.Close();
						this.m_regHelper.EvaluateRegistryValue();
						this.m_regHelper.WriteRegPolResults(xOutputDoc, string.Empty, interactiveUserSessionInfo.UserName + " - " + text);
					}
					return;
				}
			}
			if (!sources.bInSessionZero)
			{
				this.m_regHelper.RetrieveRegistryValue(sources.hkcu);
				this.m_regHelper.EvaluateRegistryValue();
				string name = WindowsIdentity.GetCurrent().Name;
				this.m_regHelper.WriteRegPolResults(xOutputDoc, string.Empty, name + " - " + sources.hkcu.ToString());
				return;
			}
			this.m_bEvalPassed = false;
			this.m_bValueFound = false;
			this.m_regHelper.WriteRegPolResults(xOutputDoc, string.Empty, "User Configuration cannot be evaluated; no users logged on.");
		}

		// Token: 0x0400067A RID: 1658
		private RegistryItemHelper m_regHelper;
	}
}
