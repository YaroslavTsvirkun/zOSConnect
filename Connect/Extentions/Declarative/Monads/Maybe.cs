using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect.Extentions.Declarative.Monads
{
    public struct Maybe<T>
    {
        internal Result AsResult()
        {
            throw new NotImplementedException();
        }

        internal Result<T> AsResult<T>()
        {
            throw new NotImplementedException();
        }
    }
}