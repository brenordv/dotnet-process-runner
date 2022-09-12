
using Microsoft.Extensions.Configuration;
using Raccoon.Ninja.ProcessRunner.Core.Models;

namespace Raccoon.Ninja.ProcessRunner.Cli.Utils;

public static class SettingsHelper
{
    public static RuntimeOrganizer LoadAppSettings()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("config.json", optional: false);

        var config = builder.Build();
        return config.Get<RuntimeOrganizer>();
    }
    
    // public static RuntimeOrganizer GetRuntimeOrganizer()
    // {
    //     return new RuntimeOrganizer(
    //         1000,
    //         new List<ManagedProcess>
    //         {
    //             new ManagedProcess(
    //                 "Proc 1",
    //                 @"Z:\dev\projects\ProcessRunner\Raccoon.Ninja.ProcessRunner.Mock.Cli\bin\Debug\net6.0\Raccoon.Ninja.ProcessRunner.Mock.Cli.exe",
    //                 "PROC-01",
    //                 true),
    //             new ManagedProcess(
    //                 "Proc 2",
    //                 @"Z:\dev\projects\ProcessRunner\Raccoon.Ninja.ProcessRunner.Mock.Cli\bin\Debug\net6.0\Raccoon.Ninja.ProcessRunner.Mock.Cli.exe",
    //                 "PROC-02",
    //                 true),
    //             new ManagedProcess(
    //                 "Proc 3",
    //                 @"Z:\dev\projects\ProcessRunner\Raccoon.Ninja.ProcessRunner.Mock.Cli\bin\Debug\net6.0\Raccoon.Ninja.ProcessRunner.Mock.Cli.exe",
    //                 "PROC-03",
    //                 true),
    //             new ManagedProcess(
    //                 "Proc 4",
    //                 "dotnet",
    //                 "run",
    //                 true,
    //                 @"Z:\dev\projects\ProcessRunner\Raccoon.Ninja.ProcessRunner.Mock.Cli")
    //         }
    //     );
    // }
}