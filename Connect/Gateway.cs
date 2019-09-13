using Connect.Interface;
using IBM.CTG;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace zConnect
{
    public class Gateway : IGateway
    {
        public void EciConnect(String host, Int32 port, String login, String password, String serverName)
        {
            if (host == null) throw new ArgumentNullException(nameof(host));
            if (login == null) throw new ArgumentNullException(nameof(login));
            if (password == null) throw new ArgumentNullException(nameof(password));
            if (serverName == null) throw new ArgumentNullException(nameof(serverName));

            var account = new Account(
                () => new DnsEndPoint(host, port),
                () => new User(login, password),
                () => serverName);
            try
            {
                using (var connection = 
                    new GatewayConnection(account.Hostname.Host, account.Hostname.Port))
                {
                    var request = new EciRequest();
                    request.UserId = account.User.Login;
                    request.Password = account.User.Password;
                    request.ServerName = account.ServerName;
                    //request.ExtendMode = EciExtendMode.EciNoExtend;

                    string commarea = "L" + new string(' ', 43);
                    

                    request.Program = "program";

                    //connection.Flow(request);
                    
                }
            }
            catch(GatewayException ge)
            {
                Console.WriteLine("ERROR: {0}", ge.Message);
            }
            catch(SocketException se)
            {
                Console.WriteLine("ERROR: {0}", se.Message);
            }
        }

        public void EciConnect(DnsEndPoint server, User info, String serverName)
        {
            if (server == null) throw new ArgumentNullException(nameof(server));
            if (serverName == null) throw new ArgumentNullException(nameof(serverName));

            var account = new Account(
                () => new DnsEndPoint(server.Host, server.Port),
                () => new User(info.Login, info.Password),
                () => serverName);
            try
            {
                using (var connection =
                    new GatewayConnection(account.Hostname.Host, account.Hostname.Port))
                {
                    var request = new EciRequest();
                    request.UserId = account.User.Login;
                    request.Password = account.User.Password;
                    request.ServerName = account.ServerName;
                }
            }
            catch (GatewayException ge)
            {
                Console.WriteLine("ERROR: {0}", ge.Message);
            }
            catch (SocketException se)
            {
                Console.WriteLine("ERROR: {0}", se.Message);
            }
        }
    }
}