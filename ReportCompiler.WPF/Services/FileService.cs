using OfficeOpenXml;
using ReportCompiler.WPF.Models.Reports;
using ReportCompiler.WPF.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace ReportCompiler.WPF.Services
{
    internal class FileService : IFile
    {
        public IUserDialog UserDialog { get; init; }
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
