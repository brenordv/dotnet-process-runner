namespace Raccoon.Ninja.ProcessRunner.Core.ExtensionMethods;

public static class LongExtensions
{
    public static double ToMBytes(this long bytes)
    {
        return (bytes / 1024f) / 1024f;
    }
}