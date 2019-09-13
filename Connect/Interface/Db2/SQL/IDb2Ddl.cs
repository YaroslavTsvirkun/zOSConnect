using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect.Interface.Db2.SQL
{
    interface IDb2DDL
    {
        Task CreateObjectAsync(String command);
        Task AlterObjectAsync(String command);
        Task DropObjectAsync(String command);
    }
}