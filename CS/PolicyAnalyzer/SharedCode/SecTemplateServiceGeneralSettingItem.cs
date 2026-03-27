using System;
using System.Xml;
using Microsoft.Win32;

namespace SharedCode
{
	// Token: 0x02000042 RID: 66
	internal class SecTemplateServiceGeneralSettingItem : SecTemplateItem
	{
		// Token: 0x060000CD RID: 205 RVA: 0x0000A8C8 File Offset: 0x00008AC8
		public SecTemplateServiceGeneralSettingItem(XmlNode node)
		{
			base.ReadStdElems(node);
			base.ReadStdSecTemplateElems(node);
			char[] array = new char[] { '"' };
			string[] array2 = this.m_sLineItem.Split(new char[] { this.KeyValSeparator() });
			this.m_sServiceName = array2[0].Trim().Trim(array);
			this.m_sStartType = array2[1].Trim();
			this.m_sSDDL = array2[2].Trim().Trim(array);
			this.m_sBaselineValue = this.m_sLineItem.Substring(this.m_sLineItem.IndexOf(this.KeyValSeparator()) + 1);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00002D4F File Offset: 0x00000F4F
		internal override char KeyValSeparator()
		{
			return ',';
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00002DBA File Offset: 0x00000FBA
		public override string SortKey()
		{
			return (base.GetType().Name + "!" + this.m_sServiceName).ToLower();
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00002DDC File Offset: 0x00000FDC
		public override void Evaluate(EffectiveStateSources sources, XmlDocument xOutputDoc)
		{
			this.RetrieveAndCompare(sources);
			base.ImplementIgnoreAlwaysPass();
			this.WriteServiceResults(xOutputDoc, Environment.MachineName + " - " + sources.hklm64.Name);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x0000A96C File Offset: 0x00008B6C
		private void RetrieveAndCompare(EffectiveStateSources sources)
		{
			this.m_bEvalPassed = false;
			this.m_bInvalidComparison = false;
			this.m_bValueFound = false;
			this.m_sObservedStartType = this.m_sStartType;
			try
			{
				RegistryKey registryKey = sources.hklm64.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\" + this.m_sServiceName, false);
				if (registryKey != null)
				{
					string text = "Start";
					object value = registryKey.GetValue(text);
					if (value != null)
					{
						this.m_sObservedStartType = value.ToString();
						uint num;
						uint num2;
						if (RegistryValueKind.DWord == registryKey.GetValueKind(text) && uint.TryParse(this.m_sStartType, out num) && uint.TryParse(value.ToString(), out num2))
						{
							this.m_bValueFound = true;
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
												this.m_bInvalidComparison = true;
												goto IL_0243;
											}
										}
									}
									else if (sEvalOperator == "NotEqual")
									{
										this.m_bEvalPassed = num != num2;
										goto IL_0243;
									}
								}
								else if (sEvalOperator == "Equal")
								{
									this.m_bEvalPassed = num == num2;
									goto IL_0243;
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
											goto IL_0243;
										}
									}
								}
								else if (sEvalOperator == "LessThanOrEqual")
								{
									this.m_bInvalidComparison = true;
									goto IL_0243;
								}
							}
							else if (num3 != 2735859570U)
							{
								if (num3 == 3902070617U)
								{
									if (sEvalOperator == "OneOfThese")
									{
										if (this.m_sEvalValues.Length > 0)
										{
											string[] array = this.m_sEvalValues.Split(new char[] { ',' });
											for (int i = 0; i < array.Length; i++)
											{
												uint num4;
												if (uint.TryParse(array[i].Trim(), out num4) && num2 == num4)
												{
													this.m_bEvalPassed = true;
													break;
												}
											}
											goto IL_0243;
										}
										goto IL_0243;
									}
								}
							}
							else if (sEvalOperator == "Range")
							{
								this.m_bInvalidComparison = true;
								goto IL_0243;
							}
							this.m_bInvalidComparison = true;
						}
					}
				}
				IL_0243:;
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x0000ABE0 File Offset: 0x00008DE0
		private void WriteServiceResults(XmlDocument xOutputDoc, string sPolName)
		{
			XmlElement xmlElement = xOutputDoc.CreateElement("SecurityTemplate");
			xmlElement.SetAttribute("Section", "Service General Setting");
			XmlElement xmlElement2 = xOutputDoc.CreateElement("LineItem");
			xmlElement2.InnerText = string.Concat(new string[] { "\"", this.m_sServiceName, "\",", this.m_sObservedStartType, ",\"\"" });
			XmlElement xmlElement3 = xOutputDoc.CreateElement("PolicyName");
			xmlElement3.InnerText = sPolName;
			xmlElement.AppendChild(xmlElement2);
			xmlElement.AppendChild(xmlElement3);
			base.CreateResultsElements(xOutputDoc, xmlElement);
			xOutputDoc.DocumentElement.AppendChild(xmlElement);
		}

		// Token: 0x04000682 RID: 1666
		internal string m_sServiceName;

		// Token: 0x04000683 RID: 1667
		internal string m_sStartType;

		// Token: 0x04000684 RID: 1668
		internal string m_sSDDL;

		// Token: 0x04000685 RID: 1669
		private string m_sObservedStartType;
	}
}
