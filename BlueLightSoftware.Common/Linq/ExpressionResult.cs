using System;

namespace BlueLightSoftware.Common.Linq
{
    /// <summary>
    /// A class that is used to describe the result of an expression string
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ExpressionResult<T>
    {
        /// <summary>
        /// Gets the expression string used to fetch this <see cref="ExpressionResult{T}"/>
        /// </summary>
        public string ExpressionString { get; internal set; }

        /// <summary>
        /// Indicates whether the expression string parsed, compiled and 
        /// executed without error
        /// </summary>
        public bool Success { get; internal set; }

        /// <summary>
        /// Gets the <see cref="Exception"/> instance if the expression string
        /// failed to compile or execute successfully.
        /// </summary>
        public Exception InnerException { get; internal set; }

        /// <summary>
        /// Gets the value returned by the expression string
        /// </summary>
        public T Value { get; internal set; }

        /// <summary>
        /// Private constructor
        /// </summary>
        internal ExpressionResult()
        {

        }
    }
}
