using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Final_C_
{
    public class Utils
    {
        public Utils()
        {
        }

        public static string Hash(string text, string alg)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            byte[] hashBytes = null;
            // MD5
            if (alg.ToLower().Equals("md5"))
            {
                MD5 md5 = MD5.Create();
                hashBytes = md5.ComputeHash(bytes);
            }
            else if (alg.ToLower().Equals("sha512"))
            {
                SHA512 sha = SHA512.Create();
                hashBytes = sha.ComputeHash(bytes);
            }

            return Convert.ToHexString(hashBytes);
        }
    }
}
