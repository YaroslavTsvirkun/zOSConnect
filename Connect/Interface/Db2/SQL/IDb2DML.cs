using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect.Interface.Db2.SQL
{
    interface IDb2DML
    {
        Task SelectTableAsync(String command);
        Task InsertTableAsync(String command);
        Task UpdateTableAsync(String command);
        Task DeleteTableAsync(String command);
    }
}