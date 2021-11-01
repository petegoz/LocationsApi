using System;

namespace Operations
{
    /// <summary>
    /// Result contains data from the operation (command, query, reader or writer) with
    /// a flag indicating success or failure and a message.
    /// </summary>
    /// <typeparam name="T">The type of data returned by the operation.</typeparam>
    public class Result<T>
    {
        /// <summary>
        /// The data returned by the operation.
        /// </summary>
        public T Data { get; private set; }

        /// <summary>
        /// A human readable success or error message.
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// True if the operation succeeded.
        /// </summary>
        public bool Success { get; private set; }

        /// <summary>
        /// If an operation causes an exception, the Result should include it.
        /// </summary>
        public Exception Exception { get; private set; }

        /// <summary>
        /// Create a result with the Success property set to true.
        /// </summary>
        /// <param name="data">The data from the operation.</param>
        /// <param name="message">A human readable success message.</param>
        /// <returns>A result containing the data.</returns>
        public static Result<T> CreateSuccessResult(T data, string message)
        {
            return new Result<T> {Data = data, Success = true, Message = message};
        }

        /// <summary>
        /// Create a result with the Success property set to false.
        /// </summary>
        /// <param name="message">A human readable error message.</param>
        /// <param name="exception">Include the exception if the failure was caused by one.</param>
        /// <returns>A result containing an error message.</returns>
        public static Result<T> CreateFailureResult(string message, Exception exception = null)
        {
            return new Result<T> { Success = false, Message = message, Exception = exception};
        }
    }
}
