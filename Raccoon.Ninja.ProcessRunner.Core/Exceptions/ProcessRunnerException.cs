using System.Runtime.Serialization;

namespace Raccoon.Ninja.ProcessRunner.Core.Exceptions;

[Serializable]
public class ProcessRunnerException: Exception
{
    public ProcessRunnerException(string message): base(message)
    { }
    
    public ProcessRunnerException(string message, Exception exception): base(message, exception)
    { }
    
    protected ProcessRunnerException(SerializationInfo info, StreamingContext context) 
        : base(info, context)
    {
    }    
}