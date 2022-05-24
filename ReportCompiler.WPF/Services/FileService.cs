using ReportCompiler.WPF.Models;
using ReportCompiler.WPF.Services.Interfaces;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;

namespace ReportCompiler.WPF.Services
{
    internal class FileService : IFile
    {
        public IUserDialog UserDialog { get; init; }
        public FileService(IUserDialog userDialog)
        {
            UserDialog = userDialog;
        }

        public bool CanOpen(DirectoryItem directoryItem) => File.Exists(directoryItem.Path);
        public bool Open(DirectoryItem directoryItem)
        {
            try
            {
                var psInfo = new ProcessStartInfo
                {
                    FileName = directoryItem.Path,
                    UseShellExecute = true
                };
                Process.Start(psInfo);
                return true;
            }
            catch(Win32Exception e)
            {
                UserDialog.ShowMessage("Ошибка запуска", e.Message);
                return false;
            }
            catch (ObjectDisposedException e)
            {
                UserDialog.ShowMessage("Ошибка запуска", e.Message);
                return false;
            }
            catch (PlatformNotSupportedException e)
            {
                UserDialog.ShowMessage("Ошибка запуска", e.Message);
                return false;
            }
        }
    }
}
