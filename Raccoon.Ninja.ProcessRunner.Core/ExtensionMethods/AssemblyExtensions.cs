using System.Reflection;

namespace Raccoon.Ninja.ProcessRunner.Core.ExtensionMethods;

public static class AssemblyExtensions
{
    public static string VersionAsString(this Assembly assembly, string noVersionSetValue = "0.0.0")
    {
        var version = assembly.GetName().Version;
        return version == null 
            ? noVersionSetValue 
            : $"{version.Major}.{version.Minor}.{version.Build}";
    }

    public static string GetRootPath(this Assembly assembly)
    {
        return new FileInfo(assembly.Location).Directory?.FullName;
    }
}