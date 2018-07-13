using System;

namespace Daybreaksoft.Extensions.Functions
{
    /// <summary>
    /// The exception that is thrown when type converted failed.Such as string to int.
    /// </summary>
    public class InvalidTypeConvertedException : Exception
    {
        public InvalidTypeConvertedException(string message) : base(message)
        {
        }
    }
}
