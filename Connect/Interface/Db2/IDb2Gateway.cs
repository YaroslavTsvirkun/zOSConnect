using Connect.Interface.Db2.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect.Interface.Db2
{
    interface IDb2Gateway : IDb2DDL
    {
        String GetConnectionString();
    }
}