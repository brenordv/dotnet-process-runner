using Raccoon.Ninja.ProcessRunner.Core.Enums;
using Raccoon.Ninja.ProcessRunner.Core.Models;

namespace Raccoon.Ninja.ProcessRunner.Core.ExtensionMethods;

public static class RuntimeProcessExtensions
{
    public static RuntimeProcess UpdateStatus(this RuntimeProcess rp, ProcessStatus newStatus)
    {
        return rp with { Status = newStatus };
    }

    public static void StopAll(this RuntimeProcess[] processes)
    {
        if (processes == null) return;

        Console.WriteLine("Stopping child processes...");
        foreach (var runtimeProcess in processes.Select(p => p.Process))
        {
            runtimeProcess.Kill();
            runtimeProcess.Dispose();
        }
    }
}