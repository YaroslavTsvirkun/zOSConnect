using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect.Extentions.Declarative.Monads
{
    public class Result<T> : Result
    {
        private readonly T value;

        public Result(T value, Boolean IsOk = true, String ErrorMessage = "") : base(IsOk, ErrorMessage)
        {
            this.value = value;
        }

        public Result(String ErrorMessage, Boolean IsOk = false) : base(IsOk)
        {
            this.value = default;
            this.ErrorMessage = ErrorMessage;
        }

        public T Value
        {
            get
            {
                if (!IsOk)
                    throw new InvalidOperationException(
                        $"Result is failed. Message: \"{ErrorMessage}\".");
                return value;
            }
        }

        public Result<TOut> As<TOut>() =>
            IsOk ? 
            Result.Ok((TOut)(Object)Value) : 
            Result.Failed<TOut>(ErrorMessage);

        public static implicit operator T(Result<T> result) => result.Value;

        public static implicit operator Result<Object>(Result<T> result) =>
            result.IsOk ? 
            Result.Ok((Object)result.Value) : 
            Result.Failed<Object>(result.ErrorMessage);
    }

    /// <summary>
    /// Represents value that was either succeeded or failed 
    /// (then it will have error explanation) to retrieve.
    /// </summary>
    public class Result
    {
        /// <summary>
        /// Determines whether <see cref="Result"/> is succeded.
        /// </summary>
        public Boolean IsOk { get; protected private set; }

        /// <summary>
        /// Determines whether <see cref="Result"/> is failed and has an error description.
        /// </summary>
        public Boolean IsFail => !IsOk;

        /// <summary>
        /// Description of error by which <see cref="Result"/> is treated as failed.
        /// </summary>
        public String ErrorMessage { get; internal set; }

        /// <summary>
        /// Constructor with parameters result
        /// </summary>
        /// <param name="isOk">Determines whether <see cref="Result"/> is succeded.</param>
        /// <param name="error">Description of error by which 
        /// <see cref="Result"/> is treated as failed.</param>
        public Result(Boolean IsOk, String ErrorMessage = "") 
            : this(IsOk) => this.ErrorMessage = ErrorMessage;
       
        public Result(Boolean IsOk) => this.IsOk = IsOk;
        

        /// <summary>
        /// Creates succeeded result.
        /// </summary>
        /// <returns>Instance of <see cref="Result"/>.</returns>
        public static Result Ok() => 
            new Result(true);

        /// <summary>
        /// Creates either succeeded result (if <paramref name="when"/> 
        /// is <code>true</code>) or failed (using provided <paramref name="because"/> 
        /// as error description.
        /// </summary>
        /// <param name="when">Condition that will produce <see cref="Result"/> instance.</param>
        /// <param name="because">Error description for case, when 
        /// <paramref name="when"/> is false.</param>
        /// <returns>Instance of <see cref="Result"/>.</returns>
        public static Result Ok(Boolean when, String because = "") =>
            !when ? Failed(because) : Ok();

        /// <summary>
        /// Creates succeded result using specified value.
        /// </summary>
        /// <param name="value">Value that succeded result will contain.</param>
        /// <typeparam name="T">Type of value.</typeparam>
        /// <returns>Instance of <see cref="Result"/>.</returns>
        public static Result<T> Ok<T>(T value) => new Result<T>(value);

        /// <summary>
        /// Creates failed result using provided error description.
        /// </summary>
        /// <param name="error">Description of <see cref="Result"/>'s fail.</param>
        /// <returns>Instance of <see cref="Result{T}"/>.</returns>
        public static Result Failed(String error = "") => 
            new Result(false, error);

        /// <summary>
        /// Creates failed result of specified type using provided error description.
        /// </summary>
        /// <param name="error">Description of <see cref="Result"/>'s fail.</param>
        /// <typeparam name="T">Type of value.</typeparam>
        /// <returns>Instance of <see cref="Result"/>.</returns>
        public static Result<T> Failed<T>(String error = "") => 
            new Result<T>(error);

        /// <summary>
        /// Uses function to produce a value that will be converted 
        /// to succeeded result (if not <code>null</code>) or failed (otherwise).
        /// </summary>
        /// <param name="value">Function that will produce a value.</param>
        /// <typeparam name="T">Type of produced value.</typeparam>
        /// <returns>Instance of <see cref="Result{T}"/>.</returns>
        public static Result<T> Of<T>(Func<T> value) => 
            new Result<T>(value());
    }
}