using Microsoft.Extensions.DependencyInjection;
using ReportCompiler.WPF.Infrastructure.Commands.Base;
using ReportCompiler.WPF.Services.Interfaces;
using ReportCompiler.WPF.ViewModels.UserControlViewModels;

namespace ReportCompiler.WPF.Infrastructure.Commands
{
    internal class CreateReportCommand : Command
    {
        public static ISummaryCompiler SummaryCompiler => App.Services.GetRequiredService<ISummaryCompiler>();

        protected override bool CanExecute(object? parameter)
        {
            if (parameter == null || parameter is not MetaDataViewModel metaDataViewModel) return false;
            return metaDataViewModel.IsValidMetaData;
        }

        protected override void Execute(object? parameter)
        {
            var metaData = (parameter as MetaDataViewModel).MetaData;

            SummaryCompiler.CompileSummary(metaData);
        }
    }
}
