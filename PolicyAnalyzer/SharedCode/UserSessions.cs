using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace SharedCode
{
	// Token: 0x02000051 RID: 81
	internal class UserSessions
	{
		// Token: 0x06000132 RID: 306 RVA: 0x0000DD14 File Offset: 0x0000BF14
		public static List<InteractiveUserSessionInfo> GetInteractiveUserSessionInfo(bool bRequireSID = false)
		{
			List<InteractiveUserSessionInfo> list = new List<InteractiveUserSessionInfo>();
			IntPtr intPtr = UserSessions.WTSOpenServer(null);
			try
			{
				IntPtr zero = IntPtr.Zero;
				int num = 0;
				bool flag = UserSessions.WTSEnumerateSessions(intPtr, 0, 1, ref zero, ref num) != 0;
				int num2 = Marshal.SizeOf(typeof(UserSessions.WTS_SESSION_INFO));
				long num3 = (long)zero;
				if (flag)
				{
					for (int i = 0; i < num; i++)
					{
						UserSessions.WTS_SESSION_INFO wts_SESSION_INFO = (UserSessions.WTS_SESSION_INFO)Marshal.PtrToStructure((IntPtr)num3, typeof(UserSessions.WTS_SESSION_INFO));
						num3 += (long)num2;
						if (wts_SESSION_INFO.SessionID != 0 && (wts_SESSION_INFO.State == UserSessions.WTS_CONNECTSTATE_CLASS.WTSActive || wts_SESSION_INFO.State == UserSessions.WTS_CONNECTSTATE_CLASS.WTSDisconnected || wts_SESSION_INFO.State == UserSessions.WTS_CONNECTSTATE_CLASS.WTSConnected))
						{
							InteractiveUserSessionInfo interactiveUserSessionInfo = default(InteractiveUserSessionInfo);
							interactiveUserSessionInfo.SessionID = wts_SESSION_INFO.SessionID;
							IntPtr zero2 = IntPtr.Zero;
							uint num4;
							UserSessions.WTSQuerySessionInformation(intPtr, wts_SESSION_INFO.SessionID, UserSessions.WTS_INFO_CLASS.WTSUserName, out zero2, out num4);
							interactiveUserSessionInfo.UserName = Marshal.PtrToStringAnsi(zero2);
							interactiveUserSessionInfo.UserSID = string.Empty;
							IntPtr zero3 = IntPtr.Zero;
							if (UserSessions.WTSQueryUserToken(wts_SESSION_INFO.SessionID, out zero3))
							{
								int num5 = 0;
								UserSessions.GetTokenInformation(zero3, UserSessions.TOKEN_INFORMATION_CLASS.TokenUser, IntPtr.Zero, num5, out num5);
								IntPtr intPtr2 = Marshal.AllocHGlobal(num5);
								if (UserSessions.GetTokenInformation(zero3, UserSessions.TOKEN_INFORMATION_CLASS.TokenUser, intPtr2, num5, out num5))
								{
									ref UserSessions.TOKEN_USER ptr = (UserSessions.TOKEN_USER)Marshal.PtrToStructure(intPtr2, typeof(UserSessions.TOKEN_USER));
									IntPtr zero4 = IntPtr.Zero;
									UserSessions.ConvertSidToStringSid(ptr.User.Sid, out zero4);
									interactiveUserSessionInfo.UserSID = Marshal.PtrToStringAuto(zero4);
									UserSessions.LocalFree(zero4);
								}
								Marshal.FreeHGlobal(intPtr2);
								UserSessions.CloseHandle(zero3);
							}
							if (interactiveUserSessionInfo.UserSID.Length > 0 || !bRequireSID)
							{
								list.Add(interactiveUserSessionInfo);
							}
						}
					}
					UserSessions.WTSFreeMemory(zero);
				}
			}
			finally
			{
				UserSessions.WTSCloseServer(intPtr);
			}
			return list;
		}

		// Token: 0x06000133 RID: 307 RVA: 0x0000DEF4 File Offset: 0x0000C0F4
		public static bool GetCurrentSessionId(out int sessionId)
		{
			bool flag = false;
			sessionId = 0;
			IntPtr intPtr;
			if (UserSessions.OpenProcessToken(UserSessions.GetCurrentProcess(), 8U, out intPtr))
			{
				int num = 0;
				UserSessions.GetTokenInformation(intPtr, UserSessions.TOKEN_INFORMATION_CLASS.TokenSessionId, IntPtr.Zero, 0, out num);
				if (num > 0)
				{
					IntPtr intPtr2 = Marshal.AllocHGlobal(num);
					if (UserSessions.GetTokenInformation(intPtr, UserSessions.TOKEN_INFORMATION_CLASS.TokenSessionId, intPtr2, num, out num))
					{
						sessionId = Marshal.ReadInt32(intPtr2);
						flag = true;
					}
					Marshal.FreeHGlobal(intPtr2);
				}
				UserSessions.CloseHandle(intPtr);
			}
			return flag;
		}

		// Token: 0x06000134 RID: 308
		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern bool CloseHandle(IntPtr hObject);

		// Token: 0x06000135 RID: 309
		[DllImport("kernel32.dll")]
		private static extern IntPtr LocalFree(IntPtr hMem);

		// Token: 0x06000136 RID: 310
		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern IntPtr GetCurrentProcess();

		// Token: 0x06000137 RID: 311
		[DllImport("advapi32", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern bool ConvertSidToStringSid(IntPtr pSID, out IntPtr ptrSid);

		// Token: 0x06000138 RID: 312
		[DllImport("advapi32.dll", SetLastError = true)]
		private static extern bool GetTokenInformation(IntPtr TokenHandle, UserSessions.TOKEN_INFORMATION_CLASS TokenInformationClass, IntPtr TokenInformation, int TokenInformationLength, out int ReturnLength);

		// Token: 0x06000139 RID: 313
		[DllImport("advapi32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool OpenProcessToken(IntPtr ProcessHandle, uint DesiredAccess, out IntPtr TokenHandle);

		// Token: 0x0600013A RID: 314
		[DllImport("wtsapi32.dll", SetLastError = true)]
		private static extern IntPtr WTSOpenServer([MarshalAs(UnmanagedType.LPStr)] string pServerName);

		// Token: 0x0600013B RID: 315
		[DllImport("wtsapi32.dll")]
		private static extern void WTSCloseServer(IntPtr hServer);

		// Token: 0x0600013C RID: 316
		[DllImport("wtsapi32.dll", SetLastError = true)]
		private static extern int WTSEnumerateSessions(IntPtr hServer, [MarshalAs(UnmanagedType.U4)] int Reserved, [MarshalAs(UnmanagedType.U4)] int Version, ref IntPtr ppSessionInfo, [MarshalAs(UnmanagedType.U4)] ref int pCount);

		// Token: 0x0600013D RID: 317
		[DllImport("Wtsapi32.dll")]
		private static extern bool WTSQuerySessionInformation(IntPtr hServer, int sessionId, UserSessions.WTS_INFO_CLASS wtsInfoClass, out IntPtr ppBuffer, out uint pBytesReturned);

		// Token: 0x0600013E RID: 318
		[DllImport("wtsapi32.dll")]
		private static extern bool WTSQueryUserToken(int sessionId, out IntPtr Token);

		// Token: 0x0600013F RID: 319
		[DllImport("wtsapi32.dll")]
		private static extern void WTSFreeMemory(IntPtr pMemory);

		// Token: 0x040006AE RID: 1710
		public const uint TOKEN_QUERY = 8U;

		// Token: 0x02000052 RID: 82
		private enum TOKEN_INFORMATION_CLASS
		{
			// Token: 0x040006B0 RID: 1712
			TokenUser = 1,
			// Token: 0x040006B1 RID: 1713
			TokenGroups,
			// Token: 0x040006B2 RID: 1714
			TokenPrivileges,
			// Token: 0x040006B3 RID: 1715
			TokenOwner,
			// Token: 0x040006B4 RID: 1716
			TokenPrimaryGroup,
			// Token: 0x040006B5 RID: 1717
			TokenDefaultDacl,
			// Token: 0x040006B6 RID: 1718
			TokenSource,
			// Token: 0x040006B7 RID: 1719
			TokenType,
			// Token: 0x040006B8 RID: 1720
			TokenImpersonationLevel,
			// Token: 0x040006B9 RID: 1721
			TokenStatistics,
			// Token: 0x040006BA RID: 1722
			TokenRestrictedSids,
			// Token: 0x040006BB RID: 1723
			TokenSessionId,
			// Token: 0x040006BC RID: 1724
			TokenGroupsAndPrivileges,
			// Token: 0x040006BD RID: 1725
			TokenSessionReference,
			// Token: 0x040006BE RID: 1726
			TokenSandBoxInert,
			// Token: 0x040006BF RID: 1727
			TokenAuditPolicy,
			// Token: 0x040006C0 RID: 1728
			TokenOrigin
		}

		// Token: 0x02000053 RID: 83
		public struct TOKEN_USER
		{
			// Token: 0x040006C1 RID: 1729
			public UserSessions.SID_AND_ATTRIBUTES User;
		}

		// Token: 0x02000054 RID: 84
		public struct SID_AND_ATTRIBUTES
		{
			// Token: 0x040006C2 RID: 1730
			public IntPtr Sid;

			// Token: 0x040006C3 RID: 1731
			public int Attributes;
		}

		// Token: 0x02000055 RID: 85
		private struct WTS_SESSION_INFO
		{
			// Token: 0x040006C4 RID: 1732
			public int SessionID;

			// Token: 0x040006C5 RID: 1733
			[MarshalAs(UnmanagedType.LPStr)]
			public string pWinStationName;

			// Token: 0x040006C6 RID: 1734
			public UserSessions.WTS_CONNECTSTATE_CLASS State;
		}

		// Token: 0x02000056 RID: 86
		private enum WTS_INFO_CLASS
		{
			// Token: 0x040006C8 RID: 1736
			WTSInitialProgram,
			// Token: 0x040006C9 RID: 1737
			WTSApplicationName,
			// Token: 0x040006CA RID: 1738
			WTSWorkingDirectory,
			// Token: 0x040006CB RID: 1739
			WTSOEMId,
			// Token: 0x040006CC RID: 1740
			WTSSessionId,
			// Token: 0x040006CD RID: 1741
			WTSUserName,
			// Token: 0x040006CE RID: 1742
			WTSWinStationName,
			// Token: 0x040006CF RID: 1743
			WTSDomainName,
			// Token: 0x040006D0 RID: 1744
			WTSConnectState,
			// Token: 0x040006D1 RID: 1745
			WTSClientBuildNumber,
			// Token: 0x040006D2 RID: 1746
			WTSClientName,
			// Token: 0x040006D3 RID: 1747
			WTSClientDirectory,
			// Token: 0x040006D4 RID: 1748
			WTSClientProductId,
			// Token: 0x040006D5 RID: 1749
			WTSClientHardwareId,
			// Token: 0x040006D6 RID: 1750
			WTSClientAddress,
			// Token: 0x040006D7 RID: 1751
			WTSClientDisplay,
			// Token: 0x040006D8 RID: 1752
			WTSClientProtocolType,
			// Token: 0x040006D9 RID: 1753
			WTSIdleTime,
			// Token: 0x040006DA RID: 1754
			WTSLogonTime,
			// Token: 0x040006DB RID: 1755
			WTSIncomingBytes,
			// Token: 0x040006DC RID: 1756
			WTSOutgoingBytes,
			// Token: 0x040006DD RID: 1757
			WTSIncomingFrames,
			// Token: 0x040006DE RID: 1758
			WTSOutgoingFrames,
			// Token: 0x040006DF RID: 1759
			WTSClientInfo,
			// Token: 0x040006E0 RID: 1760
			WTSSessionInfo,
			// Token: 0x040006E1 RID: 1761
			WTSSessionInfoEx,
			// Token: 0x040006E2 RID: 1762
			WTSConfigInfo,
			// Token: 0x040006E3 RID: 1763
			WTSValidationInfo,
			// Token: 0x040006E4 RID: 1764
			WTSSessionAddressV4,
			// Token: 0x040006E5 RID: 1765
			WTSIsRemoteSession
		}

		// Token: 0x02000057 RID: 87
		public enum WTS_CONNECTSTATE_CLASS
		{
			// Token: 0x040006E7 RID: 1767
			WTSActive,
			// Token: 0x040006E8 RID: 1768
			WTSConnected,
			// Token: 0x040006E9 RID: 1769
			WTSConnectQuery,
			// Token: 0x040006EA RID: 1770
			WTSShadow,
			// Token: 0x040006EB RID: 1771
			WTSDisconnected,
			// Token: 0x040006EC RID: 1772
			WTSIdle,
			// Token: 0x040006ED RID: 1773
			WTSListen,
			// Token: 0x040006EE RID: 1774
			WTSReset,
			// Token: 0x040006EF RID: 1775
			WTSDown,
			// Token: 0x040006F0 RID: 1776
			WTSInit
		}
	}
}
