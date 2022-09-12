using System.Runtime.Serialization;

namespace Raccoon.Ninja.ProcessRunner.Core.Exceptions;

[Serializable]
public class NullNotAllowedException: ProcessRunnerException
{
    public NullNotAllowedException(string message) : base(message)
    {
    }

    public NullNotAllowedException(string message, Exception exception) : base(message, exception)
    {
    }

    protected NullNotAllowedException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}