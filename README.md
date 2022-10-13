# ReportCompiler
Application for compiling a summary report in the employment department.

## App starting view

![Starting](https://github.com/ChamzyK/ReportCompiler/blob/dev/Images/Starting.png)

## Code description

The following technologies were used in the project:

- C# 10
- WPF (with MVVM)
- [EPPlus 4](https://www.epplussoftware.com/)
- [Material design in XAML](https://github.com/MaterialDesignInXAML/MaterialDesignInXamlToolkit)

The following approaches were used to build a scalable code structure:

- DI (Dependency Injection)[^1]
- Pattern MVVM
- Pattern Command (for MVVM)
- Pattern Service Locator

## Examples

### Application menu

![Menu image](https://github.com/ChamzyK/ReportCompiler/blob/dev/Images/Menu.png)

### Report creating process

![Report creating image](https://github.com/ChamzyK/ReportCompiler/blob/dev/Images/Example1.png)

### Created report

![Created report image](https://github.com/ChamzyK/ReportCompiler/blob/dev/Images/ReportCreated.png)

### Errors

If reports have errors.

![Errors image](https://github.com/ChamzyK/ReportCompiler/blob/dev/Images/Errors.png)

### Resulting file

![Resulting file path image](https://github.com/ChamzyK/ReportCompiler/blob/dev/Images/CreatedReportFile.png)


[^1]: DI implemented with using ```Microsoft.Extensions.Hosting (6.0.1)```
