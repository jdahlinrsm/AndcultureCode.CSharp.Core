using System;

namespace AndcultureCode.CSharp.Core.Models.Errors
{
    /// <summary>
    /// Object to provide composition extension methods for the Result object
    /// </summary>
    public static class Composition
    {
        private static Func<Result<T>, Result<TSuccess>> Bind<T, TSuccess>(Func<T, Result<TSuccess>> function)
        {
            return eitherResult => eitherResult.Match(t => function(t), e => new Result<TSuccess>(e));
        }

        private static Func<PagedResult<T>, PagedResult<TSuccess>> Bind<T, TSuccess>(Func<T, PagedResult<TSuccess>> function)
        {
            return eitherResult => eitherResult.Match(t => function(t), e => new PagedResult<TSuccess>(e));
        }

        /// <summary>
        /// Compose together methods that Produce Results in order from left to right
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="eitherResult">The <see cref="Result{TValue}"/> of the method on the left of the "then"</param>
        /// <param name="function">The function called inside of the "then"</param>
        public static Result<T> Then<T, TValue>(this Result<TValue> eitherResult, Func<TValue, Result<T>> function) =>
            Bind(function)(eitherResult);

        /// <summary>
        /// Compose together methods that Produce Paged Results  in order from left to right
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="eitherResult">The <see cref="PagedResult{TValue}"/> of the method on the left of the "then"</param>
        /// <param name="function">The function called inside of the "then"</param>
        public static PagedResult<T> Then<T, TValue>(this PagedResult<TValue> eitherResult, Func<TValue, PagedResult<T>> function) =>
            Bind(function)(eitherResult);
    }
}
