using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect.Interface.Db2.SQL
{
    interface IDb2DCL
    {
        Task GrantObjectAsync(String command);
        Task RevokeObjectAsync(String command);
    }
}