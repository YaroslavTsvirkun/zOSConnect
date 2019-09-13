using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using zConnect;

namespace Connect.Interface
{
    interface IGateway
    {
        void EciConnect(String host, Int32 port, String login, String password, String serverName);
        void EciConnect(DnsEndPoint server, User info, String serverName);
    }
}
