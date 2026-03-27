using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security.Principal;
using System.Xml;
using Microsoft.Win32;

namespace SharedCode
{
	// Token: 0x0200004C RID: 76
	public class EffectiveStateSources
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000100 RID: 256 RVA: 0x00002F8A File Offset: 0x0000118A
		// (set) Token: 0x06000101 RID: 257 RVA: 0x00002F92 File Offset: 0x00001192
		public XmlDocument xmlSeceditAndAuditpolState { get; private set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000102 RID: 258 RVA: 0x00002F9B File Offset: 0x0000119B
		// (set) Token: 0x06000103 RID: 259 RVA: 0x00002FA3 File Offset: 0x000011A3
		public RegistryKey hklm64 { get; private set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000104 RID: 260 RVA: 0x00002FAC File Offset: 0x000011AC
		// (set) Token: 0x06000105 RID: 261 RVA: 0x00002FB4 File Offset: 0x000011B4
		public bool bInSessionZero { get; private set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000106 RID: 262 RVA: 0x00002FBD File Offset: 0x000011BD
		// (set) Token: 0x06000107 RID: 263 RVA: 0x00002FC5 File Offset: 0x000011C5
		public bool bMultiUser { get; private set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000108 RID: 264 RVA: 0x00002FCE File Offset: 0x000011CE
		// (set) Token: 0x06000109 RID: 265 RVA: 0x00002FD6 File Offset: 0x000011D6
		public RegistryKey hkcu { get; private set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600010A RID: 266 RVA: 0x00002FDF File Offset: 0x000011DF
		// (set) Token: 0x0600010B RID: 267 RVA: 0x00002FE7 File Offset: 0x000011E7
		public string sCurrUserName { get; private set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600010C RID: 268 RVA: 0x00002FF0 File Offset: 0x000011F0
		// (set) Token: 0x0600010D RID: 269 RVA: 0x00002FF8 File Offset: 0x000011F8
		public List<InteractiveUserSessionInfo> lstUserSessions { get; private set; }

		// Token: 0x0600010E RID: 270 RVA: 0x0000BE80 File Offset: 0x0000A080
		public EffectiveStateSources(bool bNeedSeceditAuditpol, out string sDiagnostics)
		{
			sDiagnostics = string.Empty;
			this.hklm64 = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
			int num;
			this.bInSessionZero = UserSessions.GetCurrentSessionId(out num) && num == 0;
			this.lstUserSessions = UserSessions.GetInteractiveUserSessionInfo(true);
			this.bMultiUser = this.lstUserSessions.Count > 0;
			this.hkcu = Registry.CurrentUser;
			this.sCurrUserName = WindowsIdentity.GetCurrent().Name;
			this.xmlSeceditAndAuditpolState = null;
			if (bNeedSeceditAuditpol)
			{
				string tempFileName = Path.GetTempFileName();
				if (EffectiveStateSources.AuditpolAndSeceditToPolicyrules(tempFileName, out sDiagnostics))
				{
					this.xmlSeceditAndAuditpolState = new XmlDocument();
					try
					{
						this.xmlSeceditAndAuditpolState.Load(tempFileName);
					}
					catch (Exception ex)
					{
						sDiagnostics = sDiagnostics + "Failure loading auditpol/secedit PolicyRules: " + ex.Message + "\r\n";
						this.xmlSeceditAndAuditpolState = null;
					}
				}
				try
				{
					File.Delete(tempFileName);
				}
				catch
				{
				}
			}
		}

		// Token: 0x0600010F RID: 271 RVA: 0x0000BF80 File Offset: 0x0000A180
		private static bool AuditpolAndSeceditToPolicyrules(string sPolicyRulesOutput, out string sDiagnostics)
		{
			sDiagnostics = string.Empty;
			string tempFileName = Path.GetTempFileName();
			string tempFileName2 = Path.GetTempFileName();
			File.Delete(tempFileName2);
			string tempFileName3 = Path.GetTempFileName();
			string text = EffectiveStateSources.Quoted(tempFileName) + " " + EffectiveStateSources.Quoted(tempFileName2);
			bool flag;
			try
			{
				string directoryName = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
				ProcessStartInfo processStartInfo = new ProcessStartInfo();
				string text2 = "PolicyAnalyzer_GetLocalPolicy.exe";
				processStartInfo.FileName = Path.Combine(directoryName, text2);
				processStartInfo.Arguments = text;
				processStartInfo.CreateNoWindow = false;
				processStartInfo.UseShellExecute = true;
				processStartInfo.WindowStyle = 2;
				Process process = Process.Start(processStartInfo);
				if (process == null)
				{
					sDiagnostics = "Failed to start " + processStartInfo.FileName;
					flag = false;
				}
				else if (!process.WaitForExit(15000))
				{
					sDiagnostics = text2 + " still running after 15 seconds; giving up";
					flag = false;
				}
				else
				{
					int exitCode = process.ExitCode;
					if (exitCode != 0)
					{
						flag = false;
					}
					else
					{
						StreamWriter streamWriter = File.CreateText(tempFileName3);
						string machineName = Environment.MachineName;
						streamWriter.WriteLine("Sec Template\t" + machineName + " - secedit /export\t" + tempFileName);
						streamWriter.WriteLine("Audit Policy\t" + machineName + " - auditpol /backup\t" + tempFileName2);
						streamWriter.Close();
						processStartInfo.FileName = Path.Combine(directoryName, "PolicyRulesFileBuilder.exe");
						processStartInfo.Arguments = EffectiveStateSources.Quoted(tempFileName3) + " " + EffectiveStateSources.Quoted(sPolicyRulesOutput);
						process = Process.Start(processStartInfo);
						if (process == null)
						{
							sDiagnostics = "Failed to start " + processStartInfo.FileName;
							flag = false;
						}
						else if (!process.WaitForExit(5000))
						{
							sDiagnostics = processStartInfo.FileName + " still running after 5 seconds; giving up";
							flag = false;
						}
						else
						{
							flag = true;
						}
					}
				}
			}
			catch (Exception ex)
			{
				sDiagnostics = ex.Message;
				flag = false;
			}
			finally
			{
				File.Delete(tempFileName);
				File.Delete(tempFileName2);
				File.Delete(tempFileName3);
			}
			return flag;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00003001 File Offset: 0x00001201
		private static string Quoted(string sInput)
		{
			if (sInput.Contains(" "))
			{
				return "\"" + sInput + "\"";
			}
			return sInput;
		}
	}
}
