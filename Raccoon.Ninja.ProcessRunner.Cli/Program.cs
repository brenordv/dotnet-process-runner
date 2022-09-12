using System.Reflection;
using Raccoon.Ninja.ProcessRunner.Cli.Utils;
using Raccoon.Ninja.ProcessRunner.Core.Exceptions;
using Raccoon.Ninja.ProcessRunner.Core.ExtensionMethods;

var assembly = Assembly.GetExecutingAssembly();
var version = assembly.VersionAsString();
var path = assembly.GetRootPath();
var interruptedByUser = false;
if (string.IsNullOrWhiteSpace(path))
    throw new NullNotAllowedException("Could not get this application's location. Cannot continue.");

Console.WriteLine($"[{version}] (Auto) Process Runner...");
Console.CancelKeyPress += (_, _) =>
{
    Console.WriteLine("Interrupted by user...");
    interruptedByUser = true;
};

//var toRun = SettingsHelper.GetRuntimeOrganizer();
var toRun = SettingsHelper.LoadAppSettings();
var processes = RuntimeHelper.InstantiateProcesses(toRun, path);

Console.CancelKeyPress += (_, _) => { processes?.StopAll(); };

try
{
    RuntimeHelper.MonitorProcesses(processes, toRun);
}
catch (ThreadInterruptedException)
{
    Console.WriteLine("Interrupted by user...");
}
catch (InvalidOperationException ioe)
{
    if (interruptedByUser) return;
    Console.WriteLine(ioe);
    throw;
}
catch (Exception e)
{
    Console.WriteLine(e);
    throw;
}
finally
{
    if (!interruptedByUser)
        processes.StopAll();
    Console.WriteLine("No more processes to monitor.\nAll done! Bye! 🦝");
}