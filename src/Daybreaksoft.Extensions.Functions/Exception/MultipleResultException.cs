using System;

namespace Daybreaksoft.Extensions.Functions
{
    /// <summary>
    /// The exception that is thrown when there are more than one result
    /// </summary>
    public class MultipleResultException : Exception
    {
        public MultipleResultException(string message) : base(message)
        {
        }
    }
}
