using System;

namespace Daybreaksoft.Extensions.Functions
{
    public static class ExceptionExtensions
    {
        /// <summary>
        /// Get root message if there are inner exception
        /// </summary>
        public static string GetRootMessage(this Exception exception)
        {
            return GetInnerExceptionMessage(exception);
        }

        private static string GetInnerExceptionMessage(Exception exception)
        {
            return exception.InnerException != null ? GetInnerExceptionMessage(exception.InnerException) : exception.Message;
        }
    }
}
