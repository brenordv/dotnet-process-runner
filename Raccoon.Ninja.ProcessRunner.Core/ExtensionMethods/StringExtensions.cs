namespace Raccoon.Ninja.ProcessRunner.Core.ExtensionMethods;

public static class StringExtensions
{
    public static string ToFilename(this string text, char newChar = '_')
    {
        return Path.GetInvalidFileNameChars()
            .Concat(Path.GetInvalidPathChars())
            .Concat(new []{' '})
            .Aggregate(text, (current, invalidChar) => current.Replace(invalidChar, newChar));
    }
}