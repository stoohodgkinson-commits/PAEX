using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Microsoft.Win32;

namespace SharedCode
{
	// Token: 0x0200003B RID: 59
	internal class RegistryItemHelper
	{
		// Token: 0x060000B0 RID: 176 RVA: 0x00002CA9 File Offset: 0x00000EA9
		public RegistryItemHelper(BaselineItem baselineItem, string sConfig)
		{
			this.m_baselineItem = baselineItem;
			this.m_sConfig = sConfig;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00002CE0 File Offset: 0x00000EE0
		public void SetKeyValueType(string sKey, string sValue, string sType)
		{
			this.m_sKey = sKey;
			this.m_sValue = sValue;
			this.m_sType = sType;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x000091FC File Offset: 0x000073FC
		public void RetrieveRegistryValue(RegistryKey baseKey)
		{
			this.m_sActualValueName = this.m_sValue;
			this.m_bCheckForValueNotFound = false;
			this.m_baselineItem.m_bValueFound = false;
			if (this.m_sActualValueName.ToLower().StartsWith("**delvals"))
			{
				this.m_sActualValueName = string.Empty;
				this.m_baselineItem.m_sEvalOperator = "Ignore-AlwaysPass";
			}
			else if (this.m_sActualValueName.ToLower().StartsWith("**del."))
			{
				this.m_sActualValueName = this.m_sActualValueName.Substring(6);
				this.m_bCheckForValueNotFound = true;
			}
			if (this.m_sActualValueName.Length > 0)
			{
				RegistryKey registryKey = null;
				string text = string.Empty;
				try
				{
					registryKey = baseKey.OpenSubKey(this.m_sKey, false);
				}
				catch (Exception ex)
				{
					text = ex.GetType().Name;
				}
				StringBuilder stringBuilder = new StringBuilder();
				if (registryKey != null)
				{
					object value = registryKey.GetValue(this.m_sActualValueName);
					if (value != null)
					{
						this.m_baselineItem.m_bValueFound = true;
						RegistryValueKind valueKind = registryKey.GetValueKind(this.m_sActualValueName);
						switch (valueKind)
						{
						case RegistryValueKind.String:
							this.m_sObservedType = "REG_SZ";
							stringBuilder.Append(value.ToString().Trim(new char[1]));
							goto IL_026F;
						case RegistryValueKind.ExpandString:
							this.m_sObservedType = "REG_EXPAND_SZ";
							stringBuilder.Append(value.ToString());
							goto IL_026F;
						case RegistryValueKind.Binary:
							this.m_sObservedType = "REG_BINARY";
							foreach (byte b in (byte[])value)
							{
								if (stringBuilder.Length > 0)
								{
									stringBuilder.Append(",");
								}
								stringBuilder.Append(b.ToString("x2"));
							}
							goto IL_026F;
						case RegistryValueKind.DWord:
							this.m_sObservedType = "REG_DWORD";
							stringBuilder.Append(value.ToString());
							goto IL_026F;
						case RegistryValueKind.MultiString:
							this.m_sObservedType = "REG_MULTI_SZ";
							foreach (string text2 in (string[])value)
							{
								if (stringBuilder.Length > 0)
								{
									stringBuilder.Append(",");
								}
								stringBuilder.Append(text2.Trim(new char[1]));
							}
							goto IL_026F;
						case RegistryValueKind.QWord:
							this.m_sObservedType = "REG_QWORD";
							stringBuilder.Append(value.ToString());
							goto IL_026F;
						}
						this.m_sObservedType = valueKind.ToString();
					}
					IL_026F:
					registryKey.Close();
				}
				else if (text.Length > 0)
				{
					stringBuilder.Append("[[[" + text + "]]]");
				}
				this.m_sObservedValue = stringBuilder.ToString();
			}
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x000094BC File Offset: 0x000076BC
		private void SpecialCasePreprocessing()
		{
			if (this.m_sKey.ToLower().StartsWith("Software\\Policies\\Microsoft\\Windows\\SrpV2\\".ToLower()))
			{
				this.m_sObservedValue = this.m_sObservedValue.Replace("\r\n", "\\r\\n");
				return;
			}
			if (this.m_sKey.ToLower() == "Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System".ToLower() && this.m_sValue == "LegalNoticeText" && this.m_sType == "REG_MULTI_SZ" && this.m_sObservedType == "REG_SZ")
			{
				this.m_sType = "REG_SZ";
				this.m_baselineItem.m_sBaselineValue = this.m_baselineItem.m_sBaselineValue.Replace("\",\"", "~ESC~COMMA~").Replace(",", "\r\n").Replace("~ESC~COMMA~", ",");
			}
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x000095A4 File Offset: 0x000077A4
		public void EvaluateRegistryValue()
		{
			if (!this.m_baselineItem.m_bValueFound)
			{
				this.m_baselineItem.m_bEvalPassed = this.m_bCheckForValueNotFound;
			}
			else
			{
				this.m_baselineItem.m_bEvalPassed = false;
				this.SpecialCasePreprocessing();
				if (this.m_sType == this.m_sObservedType)
				{
					string text = this.m_baselineItem.m_sBaselineValue.ToLower();
					string text2 = this.m_sObservedValue.ToLower();
					char c = '\0';
					if (text != text2)
					{
						if ((this.m_sType == "REG_MULTI_SZ" && this.m_sValue != "LegalNoticeText") || this.m_sKey.ToLower() == "Software\\Policies\\Microsoft\\Windows\\NetworkProvider\\HardenedPaths".ToLower())
						{
							c = ',';
						}
						else if (this.m_sKey.ToLower() == "SOFTWARE\\Policies\\Microsoft\\WindowsFirewall\\ConSecRules".ToLower())
						{
							c = '|';
						}
					}
					if (c != '\0')
					{
						SortedSet<string> sortedSet = new SortedSet<string>();
						SortedSet<string> sortedSet2 = new SortedSet<string>();
						string[] array = text.Split(new char[] { c });
						for (int i = 0; i < array.Length; i++)
						{
							string text3 = array[i].Trim();
							if (text3.Length > 0 && !sortedSet.Contains(text3))
							{
								sortedSet.Add(text3);
							}
						}
						array = text2.Split(new char[] { c });
						for (int i = 0; i < array.Length; i++)
						{
							string text4 = array[i].Trim();
							if (text4.Length > 0 && !sortedSet2.Contains(text4))
							{
								sortedSet2.Add(text4);
							}
						}
						string text5 = this.m_baselineItem.m_sEvalOperator;
						uint num = <PrivateImplementationDetails>.ComputeStringHash(text5);
						if (num <= 1519117449U)
						{
							if (num != 24087439U)
							{
								if (num != 685255070U)
								{
									if (num == 1519117449U)
									{
										if (!(text5 == "GreaterThanOrEqual"))
										{
										}
									}
								}
								else if (!(text5 == "NotEqual"))
								{
								}
							}
							else if (text5 == "Equal")
							{
								this.m_baselineItem.m_bEvalPassed = sortedSet.SetEquals(sortedSet2);
								goto IL_087B;
							}
						}
						else if (num <= 2543884473U)
						{
							if (num != 2007843446U)
							{
								if (num == 2543884473U)
								{
									if (text5 == "NonNull")
									{
										this.m_baselineItem.m_bEvalPassed = true;
										goto IL_087B;
									}
								}
							}
							else if (!(text5 == "LessThanOrEqual"))
							{
							}
						}
						else if (num != 2735859570U)
						{
							if (num == 3902070617U)
							{
								if (!(text5 == "OneOfThese"))
								{
								}
							}
						}
						else if (!(text5 == "Range"))
						{
						}
						this.m_baselineItem.m_bInvalidComparison = true;
					}
					else
					{
						string text5 = this.m_baselineItem.m_sEvalOperator;
						uint num = <PrivateImplementationDetails>.ComputeStringHash(text5);
						if (num <= 1519117449U)
						{
							if (num != 24087439U)
							{
								if (num != 685255070U)
								{
									if (num == 1519117449U)
									{
										if (text5 == "GreaterThanOrEqual")
										{
											uint num2;
											uint num3;
											if (this.m_sObservedType == "REG_DWORD" && uint.TryParse(this.m_sObservedValue, out num2) && uint.TryParse(this.m_baselineItem.m_sBaselineValue, out num3))
											{
												this.m_baselineItem.m_bEvalPassed = num2 >= num3;
												goto IL_087B;
											}
											ulong num4;
											ulong num5;
											if (this.m_sObservedType == "REG_QWORD" && ulong.TryParse(this.m_sObservedValue, out num4) && ulong.TryParse(this.m_baselineItem.m_sBaselineValue, out num5))
											{
												this.m_baselineItem.m_bEvalPassed = num4 >= num5;
												goto IL_087B;
											}
											this.m_baselineItem.m_bInvalidComparison = true;
											goto IL_087B;
										}
									}
								}
								else if (text5 == "NotEqual")
								{
									this.m_baselineItem.m_bEvalPassed = text != text2;
									goto IL_087B;
								}
							}
							else if (text5 == "Equal")
							{
								this.m_baselineItem.m_bEvalPassed = text == text2;
								goto IL_087B;
							}
						}
						else if (num <= 2543884473U)
						{
							if (num != 2007843446U)
							{
								if (num == 2543884473U)
								{
									if (text5 == "NonNull")
									{
										this.m_baselineItem.m_bEvalPassed = this.m_baselineItem.m_bValueFound;
										goto IL_087B;
									}
								}
							}
							else if (text5 == "LessThanOrEqual")
							{
								uint num2;
								uint num3;
								if (this.m_sObservedType == "REG_DWORD" && uint.TryParse(this.m_sObservedValue, out num2) && uint.TryParse(this.m_baselineItem.m_sBaselineValue, out num3))
								{
									this.m_baselineItem.m_bEvalPassed = num2 <= num3;
									goto IL_087B;
								}
								ulong num4;
								ulong num5;
								if (this.m_sObservedType == "REG_QWORD" && ulong.TryParse(this.m_sObservedValue, out num4) && ulong.TryParse(this.m_baselineItem.m_sBaselineValue, out num5))
								{
									this.m_baselineItem.m_bEvalPassed = num4 <= num5;
									goto IL_087B;
								}
								this.m_baselineItem.m_bInvalidComparison = true;
								goto IL_087B;
							}
						}
						else if (num != 2735859570U)
						{
							if (num == 3902070617U)
							{
								if (text5 == "OneOfThese")
								{
									uint num2;
									if (this.m_sObservedType == "REG_DWORD" && uint.TryParse(this.m_sObservedValue, out num2))
									{
										string[] array = this.m_baselineItem.m_sEvalValues.Split(new char[] { ',' });
										for (int i = 0; i < array.Length; i++)
										{
											uint num6;
											if (uint.TryParse(array[i].Trim(), out num6) && num2 == num6)
											{
												this.m_baselineItem.m_bEvalPassed = true;
												break;
											}
										}
										goto IL_087B;
									}
									ulong num4;
									if (this.m_sObservedType == "REG_QWORD" && ulong.TryParse(this.m_sObservedValue, out num4))
									{
										string[] array = this.m_baselineItem.m_sEvalValues.Split(new char[] { ',' });
										for (int i = 0; i < array.Length; i++)
										{
											ulong num7;
											if (ulong.TryParse(array[i].Trim(), out num7) && num4 == num7)
											{
												this.m_baselineItem.m_bEvalPassed = true;
												break;
											}
										}
										goto IL_087B;
									}
									if (this.m_sObservedType == "REG_SZ")
									{
										foreach (string text6 in this.m_baselineItem.m_sEvalValues.ToLower().Split(new char[] { ',' }))
										{
											if (text2 == text6.Trim())
											{
												this.m_baselineItem.m_bEvalPassed = true;
												break;
											}
										}
										goto IL_087B;
									}
									this.m_baselineItem.m_bInvalidComparison = true;
									goto IL_087B;
								}
							}
						}
						else if (text5 == "Range")
						{
							this.m_baselineItem.m_bInvalidComparison = true;
							string[] array2;
							if (this.m_baselineItem.m_sEvalValues.Contains("-"))
							{
								array2 = this.m_baselineItem.m_sEvalValues.Split(new char[] { '-' });
							}
							else if (this.m_baselineItem.m_sEvalValues.Contains(","))
							{
								array2 = this.m_baselineItem.m_sEvalValues.Split(new char[] { ',' });
							}
							else
							{
								array2 = this.m_baselineItem.m_sEvalValues.Split(" ".ToCharArray(), 2);
							}
							if (array2.Length != 2)
							{
								goto IL_087B;
							}
							array2[0] = array2[0].Trim();
							array2[1] = array2[1].Trim();
							uint num2;
							if (this.m_sObservedType == "REG_DWORD" && uint.TryParse(this.m_sObservedValue, out num2))
							{
								uint num8;
								uint num9;
								if (!uint.TryParse(array2[0], out num8) || !uint.TryParse(array2[1], out num9))
								{
									goto IL_087B;
								}
								this.m_baselineItem.m_bInvalidComparison = false;
								if (num8 <= num2 && num2 <= num9)
								{
									this.m_baselineItem.m_bEvalPassed = true;
									goto IL_087B;
								}
								goto IL_087B;
							}
							else
							{
								ulong num4;
								ulong num10;
								ulong num11;
								if (!(this.m_sObservedType == "REG_QWORD") || !ulong.TryParse(this.m_sObservedValue, out num4) || !ulong.TryParse(array2[0], out num10) || !ulong.TryParse(array2[1], out num11))
								{
									goto IL_087B;
								}
								this.m_baselineItem.m_bInvalidComparison = false;
								if (num10 <= num4 && num4 <= num11)
								{
									this.m_baselineItem.m_bEvalPassed = true;
									goto IL_087B;
								}
								goto IL_087B;
							}
						}
						this.m_baselineItem.m_bInvalidComparison = true;
					}
				}
			}
			IL_087B:
			this.m_baselineItem.ImplementIgnoreAlwaysPass();
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00009E38 File Offset: 0x00008038
		public void WriteRegPolResults(XmlDocument xOutputDoc, string sSourceFile, string sPolicyName)
		{
			XmlElement xmlElement = xOutputDoc.CreateElement(this.m_sConfig);
			XmlElement xmlElement2 = xOutputDoc.CreateElement("Key");
			XmlElement xmlElement3 = xOutputDoc.CreateElement("Value");
			XmlElement xmlElement4 = xOutputDoc.CreateElement("RegType");
			XmlElement xmlElement5 = xOutputDoc.CreateElement("RegData");
			XmlElement xmlElement6 = xOutputDoc.CreateElement("SourceFile");
			XmlElement xmlElement7 = xOutputDoc.CreateElement("PolicyName");
			xmlElement2.InnerText = this.m_sKey;
			xmlElement3.InnerText = this.m_sValue;
			xmlElement4.InnerText = (this.m_baselineItem.m_bValueFound ? this.m_sObservedType : this.m_sType);
			xmlElement5.InnerText = this.m_sObservedValue;
			xmlElement6.InnerText = sSourceFile;
			xmlElement7.InnerText = sPolicyName;
			xmlElement.AppendChild(xmlElement2);
			xmlElement.AppendChild(xmlElement3);
			xmlElement.AppendChild(xmlElement4);
			xmlElement.AppendChild(xmlElement5);
			if (sSourceFile.Length > 0)
			{
				xmlElement.AppendChild(xmlElement6);
			}
			xmlElement.AppendChild(xmlElement7);
			this.m_baselineItem.CreateResultsElements(xOutputDoc, xmlElement);
			xOutputDoc.DocumentElement.AppendChild(xmlElement);
		}

		// Token: 0x04000670 RID: 1648
		public BaselineItem m_baselineItem;

		// Token: 0x04000671 RID: 1649
		public string m_sConfig;

		// Token: 0x04000672 RID: 1650
		public string m_sKey;

		// Token: 0x04000673 RID: 1651
		public string m_sValue;

		// Token: 0x04000674 RID: 1652
		public string m_sType;

		// Token: 0x04000675 RID: 1653
		internal string m_sActualValueName = string.Empty;

		// Token: 0x04000676 RID: 1654
		internal bool m_bCheckForValueNotFound;

		// Token: 0x04000677 RID: 1655
		internal string m_sObservedValue = string.Empty;

		// Token: 0x04000678 RID: 1656
		internal string m_sObservedType = string.Empty;
	}
}
