using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using SharedCode;

namespace GPLookup
{
	// Token: 0x02000035 RID: 53
	public class SecSettingsDetails_t
	{
		// Token: 0x06000091 RID: 145 RVA: 0x00007F98 File Offset: 0x00006198
		public string FormatValue(string sValueRepresentation)
		{
			sValueRepresentation = sValueRepresentation.Trim().Trim(new char[] { '"' });
			switch (this.DisplayType)
			{
			case SecSettingsDetails_t.DispType_t.DisplayBoolean:
				if (!(sValueRepresentation == "0"))
				{
					return GPLookup_t.StrEnabled();
				}
				return GPLookup_t.StrDisabled();
			case SecSettingsDetails_t.DispType_t.DisplayNumber:
				if (this.sDisplayUnit.Length > 0)
				{
					return sValueRepresentation + " " + this.sDisplayUnit;
				}
				return sValueRepresentation;
			case SecSettingsDetails_t.DispType_t.DisplayString:
				if (sValueRepresentation.StartsWith("D:") || sValueRepresentation.StartsWith("O:") || sValueRepresentation.StartsWith("S:"))
				{
					string text = SecFmt.FormatSddlData(sValueRepresentation);
					if (text.Length > 0)
					{
						return text;
					}
				}
				return sValueRepresentation;
			case SecSettingsDetails_t.DispType_t.DisplayChoice:
				sValueRepresentation = this.displayChoices[sValueRepresentation];
				if (sValueRepresentation != null)
				{
					return sValueRepresentation;
				}
				return string.Empty;
			case SecSettingsDetails_t.DispType_t.DisplayMultiSz:
				return sValueRepresentation.Replace(",", "\r\n");
			case SecSettingsDetails_t.DispType_t.DisplayFlags:
			{
				int num;
				if (int.TryParse(sValueRepresentation, out num))
				{
					return this.LookupFlags(num);
				}
				return string.Empty;
			}
			default:
				return string.Empty;
			}
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000080A4 File Offset: 0x000062A4
		private string LookupFlags(int flags)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (int num in this.flagChoices.Keys)
			{
				if (num == (flags & num))
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append(this.flagChoices[num]);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00008130 File Offset: 0x00006330
		public SecSettingsDetails_t(RegistryKey hKeySecRegValue)
		{
			this.LoadFromRegkey(hKeySecRegValue);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00008188 File Offset: 0x00006388
		public SecSettingsDetails_t(string sName, SecSettingsDetails_t.DispType_t displayType)
		{
			this.sDisplayName = sName;
			this.DisplayType = displayType;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000081E8 File Offset: 0x000063E8
		private void LoadFromRegkey(RegistryKey hKeySecRegValue)
		{
			string text;
			if (this.GetRegSz(hKeySecRegValue, "DisplayName", out text))
			{
				this.sDisplayName = this.LookupResourceIfPossible(text).Trim();
				uint num = DllResourceStringLookup.ResourceIdFromFmtString(text);
				if (num != 0U)
				{
					new DllResourceStringLookup();
					uint num2;
					if (WSecEdit.HelpIdFromResourceId(num, out num2))
					{
						this.sExplainText = WSecEdit.Lookup(num2);
					}
					if (this.sExplainText.Length == 0 && WSecEdit.HelpIdFromRegkeyName(hKeySecRegValue.Name.Substring(hKeySecRegValue.Name.LastIndexOf('\\') + 1).Replace('/', '\\'), out num2))
					{
						this.sExplainText = WSecEdit.Lookup(num2);
					}
					this.sExplainText = this.sExplainText.Trim().Replace("\r", "\\r").Replace("\n", "\\n")
						.Replace("\t", "\\t");
				}
			}
			if (this.GetRegSz(hKeySecRegValue, "DisplayUnit", out text))
			{
				this.sDisplayUnit = this.LookupResourceIfPossible(text);
			}
			int num3;
			this.GetRegDword(hKeySecRegValue, "DisplayType", out num3);
			this.DisplayType = (SecSettingsDetails_t.DispType_t)num3;
			string[] array;
			if (this.GetRegMultiSz(hKeySecRegValue, "DisplayChoices", out array))
			{
				string[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					string[] array3 = array2[i].Split(new char[] { '|' });
					this.displayChoices.Add(array3[0], this.LookupResourceIfPossible(array3[1]));
				}
			}
			if (this.GetRegMultiSz(hKeySecRegValue, "DisplayFlags", out array))
			{
				string[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					string[] array4 = array2[i].Split(new char[] { '|' });
					int num4;
					if (int.TryParse(array4[0], out num4))
					{
						this.flagChoices.Add(num4, this.LookupResourceIfPossible(array4[1]));
					}
				}
			}
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000083B4 File Offset: 0x000065B4
		private bool GetRegSz(RegistryKey hkey, string sValueName, out string data)
		{
			data = string.Empty;
			object value = hkey.GetValue(sValueName, null);
			if (value != null)
			{
				RegistryValueKind valueKind = hkey.GetValueKind(sValueName);
				if (RegistryValueKind.String == valueKind)
				{
					data = value.ToString();
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x000083EC File Offset: 0x000065EC
		private bool GetRegDword(RegistryKey hkey, string sValueName, out int data)
		{
			data = 0;
			object value = hkey.GetValue(sValueName, null);
			if (value != null)
			{
				RegistryValueKind valueKind = hkey.GetValueKind(sValueName);
				if (RegistryValueKind.DWord == valueKind)
				{
					data = (int)value;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00008420 File Offset: 0x00006620
		private bool GetRegMultiSz(RegistryKey hkey, string sValueName, out string[] data)
		{
			data = null;
			object value = hkey.GetValue(sValueName, null);
			if (value != null)
			{
				RegistryValueKind valueKind = hkey.GetValueKind(sValueName);
				if (RegistryValueKind.MultiString == valueKind)
				{
					data = (string[])value;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00008454 File Offset: 0x00006654
		private string LookupResourceIfPossible(string fmtstring)
		{
			string text = new DllResourceStringLookup().LookupResource(fmtstring);
			if (text.Length <= 0)
			{
				return fmtstring;
			}
			return text;
		}

		// Token: 0x04000132 RID: 306
		public string sDisplayName = string.Empty;

		// Token: 0x04000133 RID: 307
		public SecSettingsDetails_t.DispType_t DisplayType = SecSettingsDetails_t.DispType_t.Unrecognized;

		// Token: 0x04000134 RID: 308
		public string sDisplayUnit = string.Empty;

		// Token: 0x04000135 RID: 309
		public string sExplainText = string.Empty;

		// Token: 0x04000136 RID: 310
		public Dictionary<string, string> displayChoices = new Dictionary<string, string>();

		// Token: 0x04000137 RID: 311
		public Dictionary<int, string> flagChoices = new Dictionary<int, string>();

		// Token: 0x02000036 RID: 54
		public enum DispType_t
		{
			// Token: 0x04000139 RID: 313
			DisplayBoolean,
			// Token: 0x0400013A RID: 314
			DisplayNumber,
			// Token: 0x0400013B RID: 315
			DisplayString,
			// Token: 0x0400013C RID: 316
			DisplayChoice,
			// Token: 0x0400013D RID: 317
			DisplayMultiSz,
			// Token: 0x0400013E RID: 318
			DisplayFlags,
			// Token: 0x0400013F RID: 319
			Unrecognized = -1
		}
	}
}
