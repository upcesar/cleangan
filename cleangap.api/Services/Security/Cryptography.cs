using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Web;

namespace cleangap.api.Services.Security
{

    public class Cryptography
    {

        private string GetMd5Hash(MD5 md5Hash, string input)
        {
            if (input != null)
            {
                // Convert the input string to a byte array and compute the hash.
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Create a new Stringbuilder to collect the bytes
                // and create a string.
                StringBuilder sBuilder = new StringBuilder();

                // Loop through each byte of the hashed data 
                // and format each one as a hexadecimal string.
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                // Return the hexadecimal string.
                return sBuilder.ToString();
            }

            return null;
        }

        public string EncodeMD5(string plainText)
        {
            string stringMD5 = string.Empty;

            using (MD5 md5Hash = MD5.Create())
            {
                stringMD5 = GetMd5Hash(md5Hash, plainText);
            }

            return stringMD5;
        }


    }
}