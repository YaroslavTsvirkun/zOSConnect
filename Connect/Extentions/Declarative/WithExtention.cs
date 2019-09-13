using Connect.Extentions.Declarative.Monads;
using System;

namespace Connect.Extentions.Declarative
{
    public static class WithExtention
    {
        public static Result<T> With<T>(this Result self, T other) =>
            self.With(() => other);
        public static Result<T> With<T>(this Result self, Func<T> other) =>
            self.IsOk ?
            Result.Ok(other()) :
            Result.Failed<T>(self.ErrorMessage);
        public static Result<T> With<T>(this Result self, Maybe<T> other) =>
            self.With(() => other);
        public static Result<T> With<T>(this Result self, Func<Maybe<T>> other) =>
            self.IsOk ?
            other().AsResult<T>() :
            Result.Failed<T>(self.ErrorMessage);
        public static Result<T> With<T>(this Result self, Result<T> other) =>
            self.With(() => other);
        public static Result<T> With<T>(this Result self, Func<Result<T>> other) =>
            self.IsOk ?
            other() :
            Result.Failed<T>(self.ErrorMessage);
        public static Result With(this Result self, Func<Result> other) =>
            self.IsOk ? other() : self;
        public static Result With(this Result self, Result other) =>
            self.With(() => other);

        public static Result OnSuccess(this Result self, Action action)
        {
            if (self.IsOk) action();
            return self;
        }

    }
}