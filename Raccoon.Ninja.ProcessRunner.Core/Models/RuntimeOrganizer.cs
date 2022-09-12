namespace Raccoon.Ninja.ProcessRunner.Core.Models;

public class RuntimeOrganizer
{
    public int DelayBetweenChecks {
        get;
        init;
    }
    public List<ManagedProcess> Processes {
        get;
        init;
    }

}