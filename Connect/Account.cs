using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Security.Cryptography;
using Connect.Extentions;

namespace zConnect
{
    delegate Account ReceiveInfo(String host, Int32 port, String login, String password);

    public struct Account
    {
        /// <summary>
        /// The host name of the Gateway
        /// </summary>
        public DnsEndPoint Hostname { get; set; }

        /// <summary>
        /// The name of the CICS server to call the program on
        /// </summary>
        public String ServerName { get; set; }

        /// <summary>
        /// Stores user information
        /// </summary>
        public User User;

        /// <summary>
        /// The connect to the CICS transaction gateway using TCP/IP SSL
        /// </summary>
        public Boolean Ssl { get; set; }

        public Account(Func<DnsEndPoint> h, Func<User> l, Func<String> s)
        {
            this.Hostname = h();
            this.User = l();
            this.ServerName = s();
            this.Ssl = false;
        }
    }

    public struct User
    {
        /// <summary>
        /// The username to the connect using gateway CICS
        /// </summary>
        public String Login { get; set; }

        /// <summary>
        /// The user password to the connect using gateway CICS
        /// </summary>
        public String Password { get; set; }

        public User(String Login, String Password)
        {
            this.Login = Login;
            this.Password = Password;
        }


        internal static String HashPassword(String password)
        {
            Byte[] salt;
            Byte[] buffer2;
            if (password == null) throw new ArgumentNullException("password");
            
            using(var bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(0x20);
            }
            Byte[] dst = new Byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
            return Convert.ToBase64String(dst);
        }

        internal static Boolean VerifyHashedPassword(String hashedPassword, String password)
        {
            Byte[] buffer4;
            if (hashedPassword == null) return false;
            if (password == null) throw new ArgumentNullException("password");

            Byte[] src = Convert.FromBase64String(hashedPassword);
            if ((src.Length != 0x31) || (src[0] != 0)) return false;

            Byte[] dst = new Byte[0x10];
            Buffer.BlockCopy(src, 1, dst, 0, dst.Count());
            Byte[] buffer3 = new Byte[0x20];
            var countBuf = buffer3.Count();
            Buffer.BlockCopy(src, countBuf++, buffer3, 0, countBuf);
            using (var bytes = new Rfc2898DeriveBytes(password, dst, 0x3e8))
            {
                buffer4 = bytes.GetBytes(0x20);
            }
            return ByteArraysEqual(buffer3, buffer4);
        }

        private static Boolean ByteArraysEqual(Byte[] a, Byte[] b)
        {
            if (a == null && b == null) return true;
            if (a == null || b == null || a.Length != b.Length) return false;
            
            var areSame = true;
            for (var i = 0; i < a.Length; i++) areSame &= (a[i] == b[i]);
            return areSame;
        }
    }
}