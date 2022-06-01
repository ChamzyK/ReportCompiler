using ReportCompiler.WPF.Services.Interfaces;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;

namespace ReportCompiler.WPF.Services
{
    internal class FileService : IFile
    {
        public IUserDialog UserDialog { get; }
        public FileService(IUserDialog userDialog)
        {
            UserDialog = userDialog;
        }

        public bool CanOpen(string path) => File.Exists(path);
        public bool Open(string path)
        {
            try
            {
                var psInfo = new ProcessStartInfo
                {
                    FileName = path,
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
