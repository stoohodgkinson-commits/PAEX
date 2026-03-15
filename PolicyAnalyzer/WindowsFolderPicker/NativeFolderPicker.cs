using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;
using Microsoft.WindowsAPICodePack.Shell;
using MS.WindowsAPICodePack.Internal;

namespace WindowsFolderPicker
{
	// Token: 0x02000003 RID: 3
	internal class NativeFolderPicker
	{
		// Token: 0x06000006 RID: 6 RVA: 0x00003CEC File Offset: 0x00001EEC
		public NativeFolderPicker()
		{
			this.m_SelectedFolder = (this.OkButtonLabel = (this.FileNameLabel = (this.InitialFolder = (this.Title = string.Empty))));
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000028E2 File Offset: 0x00000AE2
		public string SelectedFolder
		{
			get
			{
				return this.m_SelectedFolder;
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00003D30 File Offset: 0x00001F30
		public bool Show(IntPtr hwndOwner)
		{
			bool flag = false;
			try
			{
				IFileOpenDialog fileOpenDialog = (NativeFileOpenDialog)new FileOpenDialogRCW();
				FileOpenOptions fileOpenOptions;
				fileOpenDialog.GetOptions(out fileOpenOptions);
				fileOpenDialog.SetOptions(FileOpenOptions.PickFolders | fileOpenOptions);
				if (!string.IsNullOrEmpty(this.OkButtonLabel))
				{
					fileOpenDialog.SetOkButtonLabel(this.OkButtonLabel);
				}
				if (!string.IsNullOrEmpty(this.FileNameLabel))
				{
					fileOpenDialog.SetFileNameLabel(this.FileNameLabel);
				}
				if (!string.IsNullOrEmpty(this.Title))
				{
					fileOpenDialog.SetTitle(this.Title);
				}
				if (!string.IsNullOrEmpty(this.InitialFolder))
				{
					Guid guid = new Guid("43826D1E-E718-42EE-BC55-A1E261C37BFE");
					IShellItem shellItem;
					ShellNativeMethods.SHCreateItemFromParsingName(this.InitialFolder, IntPtr.Zero, ref guid, out shellItem);
					fileOpenDialog.SetFolder(shellItem);
				}
				if (fileOpenDialog.Show(hwndOwner) == 0)
				{
					IShellItem shellItem2;
					fileOpenDialog.GetResult(out shellItem2);
					IntPtr intPtr;
					if (CoreErrorHelper.Succeeded(shellItem2.GetDisplayName(ShellNativeMethods.ShellItemDesignNameOptions.FileSystemPath, out intPtr)))
					{
						this.m_SelectedFolder = Marshal.PtrToStringUni(intPtr);
						flag = true;
						Marshal.FreeCoTaskMem(intPtr);
					}
					Marshal.ReleaseComObject(shellItem2);
				}
				Marshal.ReleaseComObject(fileOpenDialog);
			}
			catch (Exception)
			{
				FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
				if (!string.IsNullOrEmpty(this.Title))
				{
					folderBrowserDialog.Description = this.Title;
				}
				DialogResult dialogResult = folderBrowserDialog.ShowDialog();
				if (DialogResult.OK == dialogResult)
				{
					this.m_SelectedFolder = folderBrowserDialog.SelectedPath;
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x04000003 RID: 3
		public string OkButtonLabel;

		// Token: 0x04000004 RID: 4
		public string FileNameLabel;

		// Token: 0x04000005 RID: 5
		public string InitialFolder;

		// Token: 0x04000006 RID: 6
		public string Title;

		// Token: 0x04000007 RID: 7
		private string m_SelectedFolder;
	}
}
