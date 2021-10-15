using System;
using System.Collections.Generic;

namespace AndcultureCode.CSharp.Core.Interfaces
{
    public interface IResult<T>
    {
        /// <summary>
        /// Number of errors
        /// </summary>
        /// <value></value>
        int ErrorCount { get; }

        /// <summary>
        /// List of errors around a request
        /// </summary>
        /// <value></value>
        List<IError> Errors { get; set; }

        /// <summary>
        /// Does this result contain any errors?
        /// </summary>
        /// <value></value>
        bool HasErrors { get; }

        /// <summary>
        /// List of key value pairs to be used request the very next related Result
        /// </summary>
        /// <value></value>
        Dictionary<string, string> NextLinkParams { get; set; }

        /// <summary>
        /// Actual resulting value from the request
        /// </summary>
        /// <value></value>
        T ResultObject { get; set; }

        /// <summary>
        /// match on any case of <see cref="IResult{T}"/>.
        /// </summary>
        /// <typeparam name="TResult">The type to return from all cases of the <see cref="IResult{T}"/> to.</typeparam>
        /// <param name="success">What to do if the <see cref="IResult{T}"/> was a sucess.</param>
        /// <param name="failure">What to do if the <see cref="IResult{T}"/> was a failure.</param>
        /// <returns>The result of handling all cases.</returns>
        TResult Match<TResult>(Func<T, TResult> success, Func<List<IError>, TResult> failure);
    }
}