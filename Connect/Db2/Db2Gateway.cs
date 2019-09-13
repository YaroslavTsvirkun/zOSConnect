using Connect.Interface.Db2;
using IBM.Data.DB2;
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Connect.Extentions.Declarative;

namespace Connect.Db2
{
    public class Db2Gateway : IDb2Gateway
    {
        public async Task CreateObjectAsync(String command)
        {
            using(var connection = new DB2Connection(GetConnectionString()))
            {
                using(var dB2Command = connection.CreateCommand())
                {
                    dB2Command.CommandText = command;
                    if(connection.IsOpen) await dB2Command.ExecuteNonQueryAsync();
                    else
                    {
                        await connection.OpenAsync();
                        await dB2Command.ExecuteNonQueryAsync();
                    }
                }
            }
        }


        public Task AlterObjectAsync(String command)
        {
            throw new NotImplementedException();
        }

        public Task DropObjectAsync(String command)
        {
            throw new NotImplementedException();
        }

        public String GetConnectionString()
        {
            var settings = ConfigurationManager.ConnectionStrings as ConnectionStringSettingsCollection;
            String connectionString = default;
            foreach (ConnectionStringSettings setting in settings)
            {
                connectionString = setting.ConnectionString;
            }
            return connectionString;
        }
    }
}