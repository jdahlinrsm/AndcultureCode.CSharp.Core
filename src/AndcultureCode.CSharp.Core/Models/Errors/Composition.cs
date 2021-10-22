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

        /// <summary>
        /// Compose together methods in order from left to right
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="eitherResult">The <see cref="Result{TValue}"/> of the method on the left of the "then"</param>
        /// <param name="function">The function called inside of the "then"</param>
        public static Result<T> Then<T, TValue>(this Result<TValue> eitherResult, Func<TValue, Result<T>> function) =>
            Bind(function)(eitherResult);
    }
}
