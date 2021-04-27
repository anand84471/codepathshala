using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace StudentDashboard.Utilities
{
    public static class SHA256Encryption
    {
        public static string ComputeSha256Hash(string rawData)
        {
            if(MvcApplication._saltForSHA256Encryption!=null)
            {
                rawData+= MvcApplication._saltForSHA256Encryption;
            }
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}