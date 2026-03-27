using System;
using System.Runtime.InteropServices;
using System.Text;

namespace SharedCode
{
	// Token: 0x0200004B RID: 75
	internal class DllResourceStringLookup
	{
		// Token: 0x060000F9 RID: 249
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		private static extern int LoadString(IntPtr hInstance, uint uID, StringBuilder lpBuffer, int nBufferMax);

		// Token: 0x060000FA RID: 250 RVA: 0x0000BD24 File Offset: 0x00009F24
		public string LookupResource(string sDllName, uint ID)
		{
			IntPtr dllHandle = DllResourceStringLookup.m_handleMap.GetDllHandle(sDllName);
			if (dllHandle == IntPtr.Zero)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder(1024);
			if (DllResourceStringLookup.LoadString(dllHandle, ID, stringBuilder, stringBuilder.Capacity) > 0)
			{
				return stringBuilder.ToString();
			}
			return string.Empty;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x0000BD78 File Offset: 0x00009F78
		public string LookupResourceOrDefault(string sDllName, uint ID, string sDefault)
		{
			string text = this.LookupResource(sDllName, ID);
			if (text.Length > 0)
			{
				return text;
			}
			return sDefault;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x0000BD9C File Offset: 0x00009F9C
		public string LookupResource(string fmtstring)
		{
			string text = "(runtime.system32)\\";
			string[] array = fmtstring.Split(new char[] { ',' });
			if (array.Length != 2)
			{
				return string.Empty;
			}
			string text2 = array[0];
			if (text2.StartsWith("@"))
			{
				text2 = text2.Substring(1);
			}
			if (text2.StartsWith(text))
			{
				text2 = text2.Substring(text.Length);
			}
			string text3 = array[1];
			if (text3.StartsWith("-"))
			{
				text3 = text3.Substring(1);
			}
			uint num;
			if (!uint.TryParse(text3, out num))
			{
				return string.Empty;
			}
			return this.LookupResource(text2, num);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x0000BE30 File Offset: 0x0000A030
		public static uint ResourceIdFromFmtString(string fmtstring)
		{
			string[] array = fmtstring.Split(new char[] { ',' });
			if (array.Length != 2)
			{
				return 0U;
			}
			string text = array[1];
			if (text.StartsWith("-"))
			{
				text = text.Substring(1);
			}
			uint num;
			if (!uint.TryParse(text, out num))
			{
				return 0U;
			}
			return num;
		}

		// Token: 0x0400069D RID: 1693
		private static DllHandleMap m_handleMap = new DllHandleMap();
	}
}
