using System.Diagnostics;
using System.Globalization;
using BetterConsoleTables;
using Raccoon.Ninja.ProcessRunner.Core.Enums;
using Raccoon.Ninja.ProcessRunner.Core.ExtensionMethods;
using Raccoon.Ninja.ProcessRunner.Core.Models;
using Raccoon.Ninja.ProcessRunner.Core.OutputHandlers;

namespace Raccoon.Ninja.ProcessRunner.Cli.Utils;

public static class RuntimeHelper
{
    public static RuntimeProcess[] InstantiateProcesses(RuntimeOrganizer runtimeOrganizer, string path)
    {
        var procs = new RuntimeProcess[runtimeOrganizer.Processes.Count];
        Parallel.ForEach(runtimeOrganizer.Processes, delegate(ManagedProcess process, ParallelLoopState _, long index)
        {
            var proc = new Process();
            proc.StartInfo.FileName = process.Path;
            proc.StartInfo.Arguments = process.Arguments;
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.CreateNoWindow = false;
            proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.RedirectStandardError = true;
            
            if (process.CaptureOutput)
            {
                var outputHandler = new GenericOutputHandler(process.Name, path);

                proc.OutputDataReceived += outputHandler.HandleNewOutputData;
                proc.ErrorDataReceived += outputHandler.HandleNewErrorData;
            }

            if (!string.IsNullOrWhiteSpace(process.ChangeWorkingDirTo))
                Directory.SetCurrentDirectory(process.ChangeWorkingDirTo);

            var started = proc.Start();

            if (process.CaptureOutput)
            {
                proc.BeginOutputReadLine();
                proc.BeginErrorReadLine();
            }

            procs[index] = new RuntimeProcess(proc, process, started ? ProcessStatus.Running : ProcessStatus.Stopped);
            Console.WriteLine($"[{process.Name}: {started}");
        });
        return procs;
    }

    public static void MonitorProcesses(IList<RuntimeProcess> procs, RuntimeOrganizer toRun)
    {
        const int maxLoopsWithNothingToMonitor = 5;
        const string alive = "✔";
        const string dead = "💀";
        var loops = 0;
        var loopsWithNothingToMonitor = 0;
        var timer = new Stopwatch();
        timer.Start();

        var tableHeaders = new ColumnHeader[]
        {
            new("PID", Alignment.Right, Alignment.Center),
            new("Process", Alignment.Left, Alignment.Center),
            new("Status", Alignment.Left, Alignment.Center),
            new("Memory (MBytes)", Alignment.Right, Alignment.Center),
            new("Total CPU time", Alignment.Right, Alignment.Center),
            new("Started At", Alignment.Right, Alignment.Center),
            new("Exited At", Alignment.Right, Alignment.Center),
            new("Exit Code", Alignment.Right, Alignment.Center)
        };

        while (true)
        {
            loops++;

            Console.Clear();
            var table = new Table(tableHeaders) { Config = TableConfiguration.Unicode() };

            for (var i = 0; i < procs.Count; i++)
            {
                var process = procs[i].Process;
                var processConfig = procs[i].ProcessConfig;
                var status = procs[i].Status;
                var hasExisted = process.HasExited;
                var statusIcon = status == ProcessStatus.Running ? alive : dead;
                var responding = process.Responding ? "Responding" : "Not Responding";

                table.AddRow(
                    process.Id.ToString(),
                    processConfig.Name,
                    $"{status}/{responding} {statusIcon} ",
                    Math.Round(process.WorkingSet64.ToMBytes(), 2),
                    process.TotalProcessorTime,
                    process.StartTime.ToString(CultureInfo.InvariantCulture),
                    hasExisted ? process.ExitTime.ToString(CultureInfo.InvariantCulture) : "-",
                    hasExisted ? process.ExitCode.ToString() : "-"
                );

                procs[i] = procs[i].UpdateStatus(hasExisted ? ProcessStatus.Stopped : ProcessStatus.Running);
            }

            Console.Write(table);
            Console.WriteLine($"Check #{loops:000000} @ {DateTime.Now} | Uptime: {timer.Elapsed}");

            Thread.Sleep(toRun.DelayBetweenChecks);

            if (procs.All(p => p.Process.HasExited))
                loopsWithNothingToMonitor++;

            if (loopsWithNothingToMonitor >= maxLoopsWithNothingToMonitor)
                break;
        }
    }
}