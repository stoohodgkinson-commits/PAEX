using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Microsoft.WindowsAPICodePack.Shell
{
	// Token: 0x02000012 RID: 18
	internal static class ShellNativeMethods
	{
		// Token: 0x06000047 RID: 71
		[DllImport("shell32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern int SHCreateItemFromParsingName([MarshalAs(UnmanagedType.LPWStr)] string path, IntPtr pbc, ref Guid riid, [MarshalAs(UnmanagedType.Interface)] out IShellItem shellItem);

		// Token: 0x06000048 RID: 72
		[DllImport("Shell32", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern int SHShowManageLibraryUI([MarshalAs(UnmanagedType.Interface)] [In] IShellItem library, [In] IntPtr hwndOwner, [In] string title, [In] string instruction, [In] ShellNativeMethods.LibraryManageDialogOptions lmdOptions);

		// Token: 0x06000049 RID: 73
		[DllImport("shell32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool SHGetPathFromIDListW(IntPtr pidl, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszPath);

		// Token: 0x0600004A RID: 74
		[DllImport("shell32.dll")]
		internal static extern IntPtr SHChangeNotification_Lock(IntPtr windowHandle, int processId, out IntPtr pidl, out uint lEvent);

		// Token: 0x0600004B RID: 75
		[DllImport("shell32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool SHChangeNotification_Unlock(IntPtr hLock);

		// Token: 0x0600004C RID: 76
		[DllImport("shell32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool SHChangeNotifyDeregister(uint hNotify);

		// Token: 0x04000037 RID: 55
		internal const int CommandLink = 14;

		// Token: 0x04000038 RID: 56
		internal const uint SetNote = 5641U;

		// Token: 0x04000039 RID: 57
		internal const uint GetNote = 5642U;

		// Token: 0x0400003A RID: 58
		internal const uint GetNoteLength = 5643U;

		// Token: 0x0400003B RID: 59
		internal const uint SetShield = 5644U;

		// Token: 0x0400003C RID: 60
		internal const int MaxPath = 260;

		// Token: 0x0400003D RID: 61
		internal const int InPlaceStringTruncated = 262560;

		// Token: 0x02000013 RID: 19
		[Flags]
		internal enum FileOpenOptions
		{
			// Token: 0x0400003F RID: 63
			OverwritePrompt = 2,
			// Token: 0x04000040 RID: 64
			StrictFileTypes = 4,
			// Token: 0x04000041 RID: 65
			NoChangeDirectory = 8,
			// Token: 0x04000042 RID: 66
			PickFolders = 32,
			// Token: 0x04000043 RID: 67
			ForceFilesystem = 64,
			// Token: 0x04000044 RID: 68
			AllNonStorageItems = 128,
			// Token: 0x04000045 RID: 69
			NoValidate = 256,
			// Token: 0x04000046 RID: 70
			AllowMultiSelect = 512,
			// Token: 0x04000047 RID: 71
			PathMustExist = 2048,
			// Token: 0x04000048 RID: 72
			FileMustExist = 4096,
			// Token: 0x04000049 RID: 73
			CreatePrompt = 8192,
			// Token: 0x0400004A RID: 74
			ShareAware = 16384,
			// Token: 0x0400004B RID: 75
			NoReadOnlyReturn = 32768,
			// Token: 0x0400004C RID: 76
			NoTestFileCreate = 65536,
			// Token: 0x0400004D RID: 77
			HideMruPlaces = 131072,
			// Token: 0x0400004E RID: 78
			HidePinnedPlaces = 262144,
			// Token: 0x0400004F RID: 79
			NoDereferenceLinks = 1048576,
			// Token: 0x04000050 RID: 80
			DontAddToRecent = 33554432,
			// Token: 0x04000051 RID: 81
			ForceShowHidden = 268435456,
			// Token: 0x04000052 RID: 82
			DefaultNoMiniMode = 536870912
		}

		// Token: 0x02000014 RID: 20
		internal enum ControlState
		{
			// Token: 0x04000054 RID: 84
			Inactive,
			// Token: 0x04000055 RID: 85
			Enable,
			// Token: 0x04000056 RID: 86
			Visible
		}

		// Token: 0x02000015 RID: 21
		internal enum ShellItemDesignNameOptions
		{
			// Token: 0x04000058 RID: 88
			Normal,
			// Token: 0x04000059 RID: 89
			ParentRelativeParsing = -2147385343,
			// Token: 0x0400005A RID: 90
			DesktopAbsoluteParsing = -2147319808,
			// Token: 0x0400005B RID: 91
			ParentRelativeEditing = -2147282943,
			// Token: 0x0400005C RID: 92
			DesktopAbsoluteEditing = -2147172352,
			// Token: 0x0400005D RID: 93
			FileSystemPath = -2147123200,
			// Token: 0x0400005E RID: 94
			Url = -2147057664,
			// Token: 0x0400005F RID: 95
			ParentRelativeForAddressBar = -2146975743,
			// Token: 0x04000060 RID: 96
			ParentRelative = -2146959359
		}

		// Token: 0x02000016 RID: 22
		[Flags]
		internal enum GetPropertyStoreOptions
		{
			// Token: 0x04000062 RID: 98
			Default = 0,
			// Token: 0x04000063 RID: 99
			HandlePropertiesOnly = 1,
			// Token: 0x04000064 RID: 100
			ReadWrite = 2,
			// Token: 0x04000065 RID: 101
			Temporary = 4,
			// Token: 0x04000066 RID: 102
			FastPropertiesOnly = 8,
			// Token: 0x04000067 RID: 103
			OpensLowItem = 16,
			// Token: 0x04000068 RID: 104
			DelayCreation = 32,
			// Token: 0x04000069 RID: 105
			BestEffort = 64,
			// Token: 0x0400006A RID: 106
			MaskValid = 255
		}

		// Token: 0x02000017 RID: 23
		internal enum ShellItemAttributeOptions
		{
			// Token: 0x0400006C RID: 108
			And = 1,
			// Token: 0x0400006D RID: 109
			Or,
			// Token: 0x0400006E RID: 110
			AppCompat,
			// Token: 0x0400006F RID: 111
			Mask = 3,
			// Token: 0x04000070 RID: 112
			AllItems = 16384
		}

		// Token: 0x02000018 RID: 24
		internal enum FileDialogEventShareViolationResponse
		{
			// Token: 0x04000072 RID: 114
			Default,
			// Token: 0x04000073 RID: 115
			Accept,
			// Token: 0x04000074 RID: 116
			Refuse
		}

		// Token: 0x02000019 RID: 25
		internal enum FileDialogEventOverwriteResponse
		{
			// Token: 0x04000076 RID: 118
			Default,
			// Token: 0x04000077 RID: 119
			Accept,
			// Token: 0x04000078 RID: 120
			Refuse
		}

		// Token: 0x0200001A RID: 26
		internal enum FileDialogAddPlacement
		{
			// Token: 0x0400007A RID: 122
			Bottom,
			// Token: 0x0400007B RID: 123
			Top
		}

		// Token: 0x0200001B RID: 27
		[Flags]
		internal enum SIIGBF
		{
			// Token: 0x0400007D RID: 125
			ResizeToFit = 0,
			// Token: 0x0400007E RID: 126
			BiggerSizeOk = 1,
			// Token: 0x0400007F RID: 127
			MemoryOnly = 2,
			// Token: 0x04000080 RID: 128
			IconOnly = 4,
			// Token: 0x04000081 RID: 129
			ThumbnailOnly = 8,
			// Token: 0x04000082 RID: 130
			InCacheOnly = 16
		}

		// Token: 0x0200001C RID: 28
		[Flags]
		internal enum ThumbnailOptions
		{
			// Token: 0x04000084 RID: 132
			Extract = 0,
			// Token: 0x04000085 RID: 133
			InCacheOnly = 1,
			// Token: 0x04000086 RID: 134
			FastExtract = 2,
			// Token: 0x04000087 RID: 135
			ForceExtraction = 4,
			// Token: 0x04000088 RID: 136
			SlowReclaim = 8,
			// Token: 0x04000089 RID: 137
			ExtractDoNotCache = 32
		}

		// Token: 0x0200001D RID: 29
		[Flags]
		internal enum ThumbnailCacheOptions
		{
			// Token: 0x0400008B RID: 139
			Default = 0,
			// Token: 0x0400008C RID: 140
			LowQuality = 1,
			// Token: 0x0400008D RID: 141
			Cached = 2
		}

		// Token: 0x0200001E RID: 30
		[Flags]
		internal enum ShellFileGetAttributesOptions
		{
			// Token: 0x0400008F RID: 143
			CanCopy = 1,
			// Token: 0x04000090 RID: 144
			CanMove = 2,
			// Token: 0x04000091 RID: 145
			CanLink = 4,
			// Token: 0x04000092 RID: 146
			Storage = 8,
			// Token: 0x04000093 RID: 147
			CanRename = 16,
			// Token: 0x04000094 RID: 148
			CanDelete = 32,
			// Token: 0x04000095 RID: 149
			HasPropertySheet = 64,
			// Token: 0x04000096 RID: 150
			DropTarget = 256,
			// Token: 0x04000097 RID: 151
			CapabilityMask = 375,
			// Token: 0x04000098 RID: 152
			System = 4096,
			// Token: 0x04000099 RID: 153
			Encrypted = 8192,
			// Token: 0x0400009A RID: 154
			IsSlow = 16384,
			// Token: 0x0400009B RID: 155
			Ghosted = 32768,
			// Token: 0x0400009C RID: 156
			Link = 65536,
			// Token: 0x0400009D RID: 157
			Share = 131072,
			// Token: 0x0400009E RID: 158
			ReadOnly = 262144,
			// Token: 0x0400009F RID: 159
			Hidden = 524288,
			// Token: 0x040000A0 RID: 160
			DisplayAttributeMask = 1032192,
			// Token: 0x040000A1 RID: 161
			FileSystemAncestor = 268435456,
			// Token: 0x040000A2 RID: 162
			Folder = 536870912,
			// Token: 0x040000A3 RID: 163
			FileSystem = 1073741824,
			// Token: 0x040000A4 RID: 164
			HasSubFolder = -2147483648,
			// Token: 0x040000A5 RID: 165
			ContentsMask = -2147483648,
			// Token: 0x040000A6 RID: 166
			Validate = 16777216,
			// Token: 0x040000A7 RID: 167
			Removable = 33554432,
			// Token: 0x040000A8 RID: 168
			Compressed = 67108864,
			// Token: 0x040000A9 RID: 169
			Browsable = 134217728,
			// Token: 0x040000AA RID: 170
			Nonenumerated = 1048576,
			// Token: 0x040000AB RID: 171
			NewContent = 2097152,
			// Token: 0x040000AC RID: 172
			CanMoniker = 4194304,
			// Token: 0x040000AD RID: 173
			HasStorage = 4194304,
			// Token: 0x040000AE RID: 174
			Stream = 4194304,
			// Token: 0x040000AF RID: 175
			StorageAncestor = 8388608,
			// Token: 0x040000B0 RID: 176
			StorageCapabilityMask = 1891958792,
			// Token: 0x040000B1 RID: 177
			PkeyMask = -2130427904
		}

		// Token: 0x0200001F RID: 31
		[Flags]
		internal enum ShellFolderEnumerationOptions : ushort
		{
			// Token: 0x040000B3 RID: 179
			CheckingForChildren = 16,
			// Token: 0x040000B4 RID: 180
			Folders = 32,
			// Token: 0x040000B5 RID: 181
			NonFolders = 64,
			// Token: 0x040000B6 RID: 182
			IncludeHidden = 128,
			// Token: 0x040000B7 RID: 183
			InitializeOnFirstNext = 256,
			// Token: 0x040000B8 RID: 184
			NetPrinterSearch = 512,
			// Token: 0x040000B9 RID: 185
			Shareable = 1024,
			// Token: 0x040000BA RID: 186
			Storage = 2048,
			// Token: 0x040000BB RID: 187
			NavigationEnum = 4096,
			// Token: 0x040000BC RID: 188
			FastItems = 8192,
			// Token: 0x040000BD RID: 189
			FlatList = 16384,
			// Token: 0x040000BE RID: 190
			EnableAsync = 32768
		}

		// Token: 0x02000020 RID: 32
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		internal struct FilterSpec
		{
			// Token: 0x0600004D RID: 77 RVA: 0x00002921 File Offset: 0x00000B21
			internal FilterSpec(string name, string spec)
			{
				this.Name = name;
				this.Spec = spec;
			}

			// Token: 0x040000BF RID: 191
			[MarshalAs(UnmanagedType.LPWStr)]
			internal string Name;

			// Token: 0x040000C0 RID: 192
			[MarshalAs(UnmanagedType.LPWStr)]
			internal string Spec;
		}

		// Token: 0x02000021 RID: 33
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		internal struct ThumbnailId
		{
			// Token: 0x040000C1 RID: 193
			[MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 16)]
			private byte rgbKey;
		}

		// Token: 0x02000022 RID: 34
		internal enum LibraryFolderFilter
		{
			// Token: 0x040000C3 RID: 195
			ForceFileSystem = 1,
			// Token: 0x040000C4 RID: 196
			StorageItems,
			// Token: 0x040000C5 RID: 197
			AllItems
		}

		// Token: 0x02000023 RID: 35
		[Flags]
		internal enum LibraryOptions
		{
			// Token: 0x040000C7 RID: 199
			Default = 0,
			// Token: 0x040000C8 RID: 200
			PinnedToNavigationPane = 1,
			// Token: 0x040000C9 RID: 201
			MaskAll = 1
		}

		// Token: 0x02000024 RID: 36
		internal enum DefaultSaveFolderType
		{
			// Token: 0x040000CB RID: 203
			Detect = 1,
			// Token: 0x040000CC RID: 204
			Private,
			// Token: 0x040000CD RID: 205
			Public
		}

		// Token: 0x02000025 RID: 37
		internal enum LibrarySaveOptions
		{
			// Token: 0x040000CF RID: 207
			FailIfThere,
			// Token: 0x040000D0 RID: 208
			OverrideExisting,
			// Token: 0x040000D1 RID: 209
			MakeUniqueName
		}

		// Token: 0x02000026 RID: 38
		internal enum LibraryManageDialogOptions
		{
			// Token: 0x040000D3 RID: 211
			Default,
			// Token: 0x040000D4 RID: 212
			NonIndexableLocationWarning
		}

		// Token: 0x02000027 RID: 39
		internal struct ShellNotifyStruct
		{
			// Token: 0x040000D5 RID: 213
			internal IntPtr item1;

			// Token: 0x040000D6 RID: 214
			internal IntPtr item2;
		}

		// Token: 0x02000028 RID: 40
		internal struct SHChangeNotifyEntry
		{
			// Token: 0x040000D7 RID: 215
			internal IntPtr pIdl;

			// Token: 0x040000D8 RID: 216
			[MarshalAs(UnmanagedType.Bool)]
			internal bool recursively;
		}

		// Token: 0x02000029 RID: 41
		[Flags]
		internal enum ShellChangeNotifyEventSource
		{
			// Token: 0x040000DA RID: 218
			InterruptLevel = 1,
			// Token: 0x040000DB RID: 219
			ShellLevel = 2,
			// Token: 0x040000DC RID: 220
			RecursiveInterrupt = 4096,
			// Token: 0x040000DD RID: 221
			NewDelivery = 32768
		}
	}
}
