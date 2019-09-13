using Connect.Extentions.Declarative.Monads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect.Extentions.Declarative
{
    public static class Keywords
    {
        public static TOutput As<TInput, TOutput>(
            this TInput self, Func<TInput, TOutput> map) => map(self);

        public async static Task<TOutput> AsAsync<TInput, TOutput>(
            this Task<TInput> self, Func<Task<TInput>, Task<TOutput>> map) => await map(self);

        public static TOutput Using<TInput, TOutput>(
            this TInput self, Func<TInput, TOutput> map) where TInput : IDisposable
        {
            var result = map(self);
            self.Dispose();
            return result;
        }

        public async static Task<TOutput> UsingAsync<TInput, TOutput>(
            this Task<TInput> self, Func<Task<TInput>, Task<TOutput>> map) where TInput : IDisposable
        {
            var result = await map(self);
            self.Dispose();
            return result;
        }

        public static TInput Do<TInput>(this TInput self, Action<TInput> action)
        {
            if (self != null) action(self);
            return self;
        }

        public async static Task<TInput> DoAsync<TInput>(
            this Task<TInput> self, Action<Task<TInput>> action)
        {
            if (self != null) action(self);
            return await self;
        }
    }
}