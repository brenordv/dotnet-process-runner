using System.Diagnostics;
using Raccoon.Ninja.ProcessRunner.Core.Enums;

namespace Raccoon.Ninja.ProcessRunner.Core.Models;

public record RuntimeProcess(
    Process Process,
    ManagedProcess ProcessConfig,
    ProcessStatus Status
);