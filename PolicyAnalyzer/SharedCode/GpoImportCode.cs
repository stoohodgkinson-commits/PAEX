using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Xml;
using Microsoft.Win32;

namespace SharedCode
{
	// Token: 0x0200004D RID: 77
	internal class GpoImportCode
	{
		// Token: 0x06000111 RID: 273 RVA: 0x0000C194 File Offset: 0x0000A394
		public static bool IdentifyGpoFiles(string sGpoFolder, out List<GpoImportCode.GpoFileInfo> gpoFiles, out string sFailureMessage)
		{
			gpoFiles = new List<GpoImportCode.GpoFileInfo>();
			sFailureMessage = string.Empty;
			DirectoryInfo directoryInfo = new DirectoryInfo(sGpoFolder);
			SortedList<string, string> sortedList = new SortedList<string, string>();
			SortedList<string, string> sortedList2 = new SortedList<string, string>();
			FileInfo[] files;
			FileInfo[] files2;
			try
			{
				files = directoryInfo.GetFiles("manifest.xml", SearchOption.AllDirectories);
				files2 = directoryInfo.GetFiles("backup.xml", SearchOption.AllDirectories);
			}
			catch (Exception ex)
			{
				sFailureMessage = ex.Message;
				return false;
			}
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			foreach (FileInfo fileInfo in files)
			{
				try
				{
					XmlDocument xmlDocument = new XmlDocument();
					xmlDocument.Load(fileInfo.FullName);
					XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(xmlDocument.NameTable);
					xmlNamespaceManager.AddNamespace("g", xmlDocument.DocumentElement.NamespaceURI);
					foreach (object obj in xmlDocument.SelectNodes("/g:Backups/g:BackupInst", xmlNamespaceManager))
					{
						XmlNode xmlNode = (XmlNode)obj;
						string innerText = xmlNode.SelectSingleNode("g:ID", xmlNamespaceManager).InnerText;
						string innerText2 = xmlNode.SelectSingleNode("g:GPODisplayName", xmlNamespaceManager).InnerText;
						if (!dictionary.ContainsKey(innerText))
						{
							dictionary.Add(innerText, innerText2);
						}
					}
				}
				catch
				{
				}
			}
			foreach (FileInfo fileInfo2 in files2)
			{
				string name = fileInfo2.Directory.Name;
				if (name.StartsWith("{") && name.EndsWith("}"))
				{
					try
					{
						XmlDocument xmlDocument2 = new XmlDocument();
						xmlDocument2.Load(fileInfo2.FullName);
						XmlNamespaceManager xmlNamespaceManager2 = new XmlNamespaceManager(xmlDocument2.NameTable);
						xmlNamespaceManager2.AddNamespace("bkp", xmlDocument2.DocumentElement.NamespaceURI);
						foreach (object obj2 in xmlDocument2.SelectNodes("/bkp:GroupPolicyBackupScheme/bkp:GroupPolicyObject/bkp:GroupPolicyCoreSettings/bkp:DisplayName", xmlNamespaceManager2))
						{
							string innerText3 = ((XmlNode)obj2).InnerText;
							if (!dictionary.ContainsKey(name))
							{
								dictionary.Add(name, innerText3);
							}
						}
						GpoImportCode.ExtractCSEInfoInternal(xmlDocument2, xmlNamespaceManager2, ref sortedList, ref sortedList2);
					}
					catch
					{
					}
				}
			}
			FileInfo[] files3 = directoryInfo.GetFiles("registry.pol", SearchOption.AllDirectories);
			FileInfo[] files4 = directoryInfo.GetFiles("GptTmpl.inf", SearchOption.AllDirectories);
			FileInfo[] files5 = directoryInfo.GetFiles("Audit.csv", SearchOption.AllDirectories);
			if (files3.Length + files4.Length + files5.Length == 0)
			{
				sFailureMessage = "No GPO policy files found in directory " + sGpoFolder;
				return false;
			}
			foreach (FileInfo fileInfo3 in files3)
			{
				string text = string.Empty;
				if (fileInfo3.DirectoryName.ToLower().EndsWith("\\machine"))
				{
					text = GpoImportCode.ComputerConfigLabel();
				}
				else if (fileInfo3.DirectoryName.ToLower().EndsWith("\\user"))
				{
					text = GpoImportCode.UserConfigLabel();
				}
				if (text.Length > 0)
				{
					string text2;
					GpoImportCode.GetGpoName(ref dictionary, fileInfo3, out text2);
					gpoFiles.Add(new GpoImportCode.GpoFileInfo(text, text2, fileInfo3));
				}
			}
			foreach (FileInfo fileInfo4 in files4)
			{
				string text3;
				GpoImportCode.GetGpoName(ref dictionary, fileInfo4, out text3);
				gpoFiles.Add(new GpoImportCode.GpoFileInfo(GpoImportCode.SecTemplateLabel(), text3, fileInfo4));
			}
			foreach (FileInfo fileInfo5 in files5)
			{
				string text4;
				GpoImportCode.GetGpoName(ref dictionary, fileInfo5, out text4);
				gpoFiles.Add(new GpoImportCode.GpoFileInfo(GpoImportCode.AuditPolicyLabel(), text4, fileInfo5));
			}
			GpoImportCode.AddCSEInfoToGpoList(ref gpoFiles, sortedList, sortedList2);
			return true;
		}

		// Token: 0x06000112 RID: 274 RVA: 0x0000C580 File Offset: 0x0000A780
		private static void ExtractCSEInfoInternal(XmlDocument xbkp, XmlNamespaceManager ns, ref SortedList<string, string> machineExtGuids, ref SortedList<string, string> userExtGuids)
		{
			try
			{
				foreach (object obj in xbkp.SelectNodes("/bkp:GroupPolicyBackupScheme/bkp:GroupPolicyObject/bkp:GroupPolicyCoreSettings/bkp:MachineExtensionGuids", ns))
				{
					foreach (string text in ((XmlNode)obj).InnerText.Split(new char[] { '[', ']' }))
					{
						if (text.Trim().Length > 0)
						{
							string text2 = text.Trim().Substring(0, 38);
							if (!machineExtGuids.ContainsKey(text2))
							{
								machineExtGuids.Add(text2, text2);
							}
						}
					}
				}
				foreach (object obj2 in xbkp.SelectNodes("/bkp:GroupPolicyBackupScheme/bkp:GroupPolicyObject/bkp:GroupPolicyCoreSettings/bkp:UserExtensionGuids", ns))
				{
					foreach (string text3 in ((XmlNode)obj2).InnerText.Split(new char[] { '[', ']' }))
					{
						if (text3.Trim().Length > 0)
						{
							string text4 = text3.Trim().Substring(0, 38);
							if (!userExtGuids.ContainsKey(text4))
							{
								userExtGuids.Add(text4, text4);
							}
						}
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x06000113 RID: 275 RVA: 0x0000C720 File Offset: 0x0000A920
		private static void AddCSEInfoToGpoList(ref List<GpoImportCode.GpoFileInfo> gpoFiles, SortedList<string, string> machineExtGuids, SortedList<string, string> userExtGuids)
		{
			RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Winlogon\\GPExtensions");
			SortedList<string, string>[] array = new SortedList<string, string>[] { machineExtGuids, userExtGuids };
			for (int i = 0; i < array.Length; i++)
			{
				foreach (string text in array[i].Keys)
				{
					gpoFiles.Add(new GpoImportCode.GpoFileInfo(i == 0, text, GpoImportCode.CSEGuidToName(registryKey, text)));
				}
			}
			registryKey.Close();
		}

		// Token: 0x06000114 RID: 276 RVA: 0x0000C7B8 File Offset: 0x0000A9B8
		public static bool ExtractCSEInfo(string sBackupXmlPath, out List<GpoImportCode.GpoFileInfo> gpoFiles, out string sFailureMessage)
		{
			gpoFiles = new List<GpoImportCode.GpoFileInfo>();
			sFailureMessage = string.Empty;
			SortedList<string, string> sortedList = new SortedList<string, string>();
			SortedList<string, string> sortedList2 = new SortedList<string, string>();
			try
			{
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.Load(sBackupXmlPath);
				XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(xmlDocument.NameTable);
				xmlNamespaceManager.AddNamespace("bkp", xmlDocument.DocumentElement.NamespaceURI);
				GpoImportCode.ExtractCSEInfoInternal(xmlDocument, xmlNamespaceManager, ref sortedList, ref sortedList2);
				GpoImportCode.AddCSEInfoToGpoList(ref gpoFiles, sortedList, sortedList2);
			}
			catch (Exception ex)
			{
				sFailureMessage = ex.Message;
				return false;
			}
			return true;
		}

		// Token: 0x06000115 RID: 277 RVA: 0x0000C848 File Offset: 0x0000AA48
		public static bool BuildPolicyRulesFile(List<GpoImportCode.GpoFileInfo> gpoFiles, string sTargetFile, out string sFailureMessage)
		{
			sFailureMessage = string.Empty;
			string tempFileName = Path.GetTempFileName();
			TextWriter textWriter = File.CreateText(tempFileName);
			foreach (GpoImportCode.GpoFileInfo gpoFileInfo in gpoFiles)
			{
				if (gpoFileInfo.IsCSE())
				{
					textWriter.WriteLine(string.Concat(new string[] { gpoFileInfo.PolicyType, "\t", gpoFileInfo.CSEName, "\t", gpoFileInfo.CSEGuid }));
				}
				else
				{
					textWriter.WriteLine(string.Concat(new string[] { gpoFileInfo.PolicyType, "\t", gpoFileInfo.PolicyName, "\t", gpoFileInfo.PolicyFileName }));
				}
			}
			textWriter.Close();
			string text = Path.Combine(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName), "PolicyRulesFileBuilder.exe");
			string text2 = string.Concat(new string[] { "\"", tempFileName, "\" \"", sTargetFile, "\"" });
			try
			{
				Process process = Process.Start(text, text2);
				process.WaitForExit();
				if (process.ExitCode != 0)
				{
					sFailureMessage = "Helper program PolicyRulesFileBuilder.exe reports failure.";
					return false;
				}
				try
				{
					File.Delete(tempFileName);
				}
				catch
				{
				}
			}
			catch (Exception ex)
			{
				sFailureMessage = "Error:  cannot start PolicyRulesFileBuilder.exe:\r\n\r\n" + ex.Message;
				return false;
			}
			return true;
		}

		// Token: 0x06000116 RID: 278 RVA: 0x0000C9F4 File Offset: 0x0000ABF4
		public static string CSEGuidToName(string sCseGuid)
		{
			RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Winlogon\\GPExtensions");
			string text = GpoImportCode.CSEGuidToName(registryKey, sCseGuid);
			registryKey.Close();
			return text;
		}

		// Token: 0x06000117 RID: 279 RVA: 0x0000CA20 File Offset: 0x0000AC20
		private static string CSEGuidToName(RegistryKey key, string sCseGuid)
		{
			object obj = null;
			RegistryKey registryKey = null;
			try
			{
				new StringBuilder();
				registryKey = key.OpenSubKey(sCseGuid);
				if (registryKey != null)
				{
					string text = string.Empty;
					string text2 = string.Empty;
					object value = registryKey.GetValue("DisplayName");
					if (value != null)
					{
						text = (string)value;
						if (text.StartsWith("@"))
						{
							string text3 = new DllResourceStringLookup().LookupResource(text);
							if (text3.Length > 0)
							{
								text = text3;
							}
							else
							{
								text = string.Empty;
							}
						}
					}
					object value2 = registryKey.GetValue(null);
					if (value2 != null)
					{
						text2 = (string)value2;
					}
					if (text.Length > text2.Length)
					{
						obj = text;
					}
					else if (text2.Length > 0)
					{
						obj = text2;
					}
					if (obj == null)
					{
						obj = registryKey.GetValue("ProcessGroupPolicy");
					}
					if (obj == null)
					{
						obj = registryKey.GetValue("ProcessGroupPolicyEx");
					}
					if (obj == null)
					{
						obj = registryKey.GetValue("DllName");
					}
				}
				if (obj == null)
				{
					if ("{35378EAC-683F-11D2-A89A-00C04FBBCFA2}" == sCseGuid)
					{
						obj = "Registry Policy";
					}
					else if ("{D76B9641-3288-4F75-942D-087DE603E3EA}" == sCseGuid)
					{
						obj = "Local Administrator Password Solution (LAPS)";
					}
					else
					{
						obj = "Unknown GP Extension";
					}
				}
			}
			catch
			{
			}
			finally
			{
				if (registryKey != null)
				{
					registryKey.Close();
				}
			}
			return obj.ToString();
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00003022 File Offset: 0x00001222
		public static string ComputerConfigLabel()
		{
			return "Computer";
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00003029 File Offset: 0x00001229
		public static string UserConfigLabel()
		{
			return "User";
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00003030 File Offset: 0x00001230
		public static string SecTemplateLabel()
		{
			return "Sec Template";
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00003037 File Offset: 0x00001237
		public static string AuditPolicyLabel()
		{
			return "Audit Policy";
		}

		// Token: 0x0600011C RID: 284 RVA: 0x0000303E File Offset: 0x0000123E
		public static string MachineExtGuids()
		{
			return "CSE-Machine";
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00003045 File Offset: 0x00001245
		public static string UserExtGuids()
		{
			return "CSE-User";
		}

		// Token: 0x0600011E RID: 286 RVA: 0x0000CB5C File Offset: 0x0000AD5C
		private static bool GetGpoName(ref Dictionary<string, string> map, FileInfo fi, out string sGpoName)
		{
			sGpoName = string.Empty;
			foreach (KeyValuePair<string, string> keyValuePair in map)
			{
				if (fi.DirectoryName.Contains(keyValuePair.Key))
				{
					sGpoName = keyValuePair.Value;
					return true;
				}
			}
			return false;
		}

		// Token: 0x0200004E RID: 78
		public struct GpoFileInfo
		{
			// Token: 0x06000120 RID: 288 RVA: 0x0000CBD0 File Offset: 0x0000ADD0
			public GpoFileInfo(string sPolType, string sPolName, FileInfo polfileInfo)
			{
				this.PolicyType = sPolType;
				this.PolicyName = sPolName;
				this.PolicyFile = polfileInfo;
				this.PolicyFileName = polfileInfo.FullName;
				this.CSEGuid = (this.CSEName = string.Empty);
			}

			// Token: 0x06000121 RID: 289 RVA: 0x0000CC14 File Offset: 0x0000AE14
			public GpoFileInfo(string sPolType, string sPolName, string sPolFilename)
			{
				this.PolicyType = sPolType;
				this.PolicyName = sPolName;
				this.PolicyFile = null;
				this.PolicyFileName = sPolFilename;
				this.CSEGuid = (this.CSEName = string.Empty);
			}

			// Token: 0x06000122 RID: 290 RVA: 0x0000CC54 File Offset: 0x0000AE54
			public GpoFileInfo(bool isCseMachine, string cseGuid, string cseName)
			{
				this.PolicyType = (isCseMachine ? GpoImportCode.MachineExtGuids() : GpoImportCode.UserExtGuids());
				this.CSEGuid = cseGuid;
				this.CSEName = cseName;
				this.PolicyName = (this.PolicyFileName = string.Empty);
				this.PolicyFile = null;
			}

			// Token: 0x06000123 RID: 291 RVA: 0x0000304C File Offset: 0x0000124C
			public bool IsCSE()
			{
				return this.CSEGuid.Length > 0;
			}

			// Token: 0x06000124 RID: 292 RVA: 0x0000305C File Offset: 0x0000125C
			public static bool IsCSELabel(string sLabel, out bool bIsMachineExtLabel)
			{
				bIsMachineExtLabel = sLabel == GpoImportCode.MachineExtGuids();
				return bIsMachineExtLabel || sLabel == GpoImportCode.UserExtGuids();
			}

			// Token: 0x040006A5 RID: 1701
			public string PolicyType;

			// Token: 0x040006A6 RID: 1702
			public string PolicyName;

			// Token: 0x040006A7 RID: 1703
			public string PolicyFileName;

			// Token: 0x040006A8 RID: 1704
			public FileInfo PolicyFile;

			// Token: 0x040006A9 RID: 1705
			public string CSEGuid;

			// Token: 0x040006AA RID: 1706
			public string CSEName;
		}
	}
}
