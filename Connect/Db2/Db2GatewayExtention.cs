using Connect.Extentions.Declarative;
using IBM.Data.DB2;
using System;
using System.Threading.Tasks;

namespace Connect.Db2
{
    public static class Db2GatewayExtention
    {
        public static async Task CreateObjectAsync(
            this DB2Connection self, String command) => await self
            .As(connection => new DB2Connection())
            .Do(x => x.ConnectionString = new Db2Gateway().GetConnectionString())
            .Using(connection => connection
                .Do(async x => await x.OpenAsync())
                .Using(x => connection.CreateCommand())
                    .Do(x => x.CommandText = command)
                    .As(async x => await x.ExecuteNonQueryAsync()));
    }
}