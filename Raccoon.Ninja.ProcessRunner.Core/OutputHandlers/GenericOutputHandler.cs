using System.Diagnostics;
using Raccoon.Ninja.ProcessRunner.Core.Exceptions;
using Raccoon.Ninja.ProcessRunner.Core.ExtensionMethods;

namespace Raccoon.Ninja.ProcessRunner.Core.OutputHandlers;

public class GenericOutputHandler : IDisposable
{
    private readonly TextWriter _outputWriter;
    private readonly TextWriter _errorWriter;

    public GenericOutputHandler(string processName, string basePath, string logFolder = "logs")
    {
        logFolder = Path.Combine(basePath, logFolder);

        var outputLogFile = Path.Join(logFolder, $"{processName}_output.log".ToFilename());
        var errorLogFile = Path.Join(logFolder, $"{processName}_error.log".ToFilename());
        Directory.CreateDirectory(logFolder);

        if (string.IsNullOrWhiteSpace(outputLogFile) || string.IsNullOrWhiteSpace(errorLogFile))
            throw new NullNotAllowedException("Log files cannot be null. Check configuration.");

        if (!File.Exists(outputLogFile))
            File.Create(outputLogFile).Dispose();

        if (!File.Exists(errorLogFile))
            File.Create(errorLogFile).Dispose();

        _outputWriter = new StreamWriter(outputLogFile);
        _errorWriter = new StreamWriter(errorLogFile);
    }

    public void HandleNewOutputData(object _, DataReceivedEventArgs eventArgs)
    {
        if (eventArgs.Data == null) return;
        _outputWriter.WriteLine(eventArgs.Data);
        _outputWriter.Flush();
    }

    public void HandleNewErrorData(object _, DataReceivedEventArgs eventArgs)
    {
        if (eventArgs.Data == null) return;
        _errorWriter.WriteLine(eventArgs.Data);
        _errorWriter.Flush();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposing) return;

        if (_outputWriter != null)
        {
            _outputWriter.Flush();
            _outputWriter.Dispose();
        }

        if (_errorWriter == null) return;
        _errorWriter.Flush();
        _errorWriter.Dispose();
    }

    ~GenericOutputHandler()
    {
        Dispose(false);
    }
}