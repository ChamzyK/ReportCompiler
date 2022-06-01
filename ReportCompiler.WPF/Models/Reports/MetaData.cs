using System;

namespace ReportCompiler.WPF.Models.Reports
{
    internal class MetaData
    {
        public MetaData(string month, string name, DateTime compileDate)
        {
            Month = month;
            Name = name;
            CompileDate = compileDate;
        }

        public string Month { get; set; }
        public string Name { get; set; }
        public DateTime CompileDate { get; set; }

       
    }
}
