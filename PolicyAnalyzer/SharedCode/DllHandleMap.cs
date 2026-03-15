using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace SharedCode
{
	// Token: 0x0200004A RID: 74
	internal class DllHandleMap
	{
		// Token: 0x060000F4 RID: 244
		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern IntPtr LoadLibraryEx(string lpFileName, IntPtr hFile, uint dwFlags);

		// Token: 0x060000F5 RID: 245
		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern bool FreeLibrary(IntPtr hModule);

		// Token: 0x060000F6 RID: 246 RVA: 0x0000BC74 File Offset: 0x00009E74
		public IntPtr GetDllHandle(string sDllName)
		{
			sDllName = sDllName.ToLower();
			if (this.m_map.ContainsKey(sDllName))
			{
				return this.m_map[sDllName];
			}
			IntPtr intPtr = DllHandleMap.LoadLibraryEx(sDllName, IntPtr.Zero, 2U);
			this.m_map.Add(sDllName, intPtr);
			return intPtr;
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x0000BCC0 File Offset: 0x00009EC0
		public void ClearMap()
		{
			foreach (IntPtr intPtr in this.m_map.Values)
			{
				DllHandleMap.FreeLibrary(intPtr);
			}
			this.m_map.Clear();
		}

		// Token: 0x0400069B RID: 1691
		private const int LOAD_LIBRARY_AS_DATAFILE = 2;

		// Token: 0x0400069C RID: 1692
		private Dictionary<string, IntPtr> m_map = new Dictionary<string, IntPtr>();
	}
}
