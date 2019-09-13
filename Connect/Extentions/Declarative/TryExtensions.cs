using Connect.Extentions.Declarative.Monads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect.Extentions.Declarative
{
    public static class TryExtensions
    {
        public static Try<TResult> SelectMany<TSource, TSelector, TResult>(
            this Try<TSource> source,
            Func<TSource, Try<TSelector>> selector,
            Func<TSource, TSelector, TResult> resultSelector) =>
                new Try<TResult>(() =>
                {
                    if (source.HasException)
                    {
                        return (default, source.Exception);
                    }
                    Try<TSelector> result = selector(source.Value);
                    if (result.HasException)
                    {
                        return (default, result.Exception);
                    }
                    return (resultSelector(source.Value, result.Value), (Exception)null);
                });

        public static Try<TSource> Try<TSource>(this TSource value) => value;

        public static Try<TResult> Select<TSource, TResult>(
            this Try<TSource> source, 
            Func<TSource, TResult> selector) =>
                source.SelectMany(value => selector(value).Try(), (value, result) => result);

        public static Try<T> Throw<T>(
            this Exception exception) => 
                new Try<T>(() => (default, exception));

        public static Try<T> Try<T>(Func<T> function) =>
            new Try<T>(() => (function(), (Exception)null));

        public static Try<T> Catch<T, TException>(
            this Try<T> source, 
            Func<TException, Try<T>> handler, 
            Func<TException, bool> when = null)
            where TException : Exception =>
                new Try<T>(() =>
                {
                    if (source.HasException && 
                        source.Exception is TException exception && 
                        exception != null && (
                        when == null || 
                        when(exception)))
                    {
                        source = handler(exception);
                    }
                    return source.HasException ? (default, source.Exception) : (source.Value, (Exception)null);
                });

        public static Try<T> Catch<T>(
            this Try<T> source, 
            Func<Exception, Try<T>> handler, 
            Func<Exception, bool> when = null) =>
                Catch<T, Exception>(source, handler, when);

        public static TResult Finally<T, TResult>(
            this Try<T> source, 
            Func<Try<T>, TResult> action) => action(source);

        public static void Finally<T>(
            this Try<T> source, 
            Action<Try<T>> action) => action(source);
    }
}
