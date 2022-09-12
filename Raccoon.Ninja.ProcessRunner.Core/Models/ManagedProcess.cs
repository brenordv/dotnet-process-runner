namespace Raccoon.Ninja.ProcessRunner.Core.Models;

public class ManagedProcess
{
    public string Name {get; init;}
    public string Path {get; init;}
    public string Arguments {get; init;}
    public bool CaptureOutput {get; init;}
    public string? ChangeWorkingDirTo {get; set;} = null;
}
