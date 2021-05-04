using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;

namespace Pseudo_Twitter
{
    public static class GetSign
    {
        public static string Sign(string s)
        {
            MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();
            byte[] hash = provider.ComputeHash(Encoding.Default.GetBytes(s));

            return BitConverter.ToString(hash).ToLower().Replace("-","");
        }
    }
}