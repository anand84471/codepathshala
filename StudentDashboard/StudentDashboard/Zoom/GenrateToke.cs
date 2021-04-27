using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace StudentDashboard.Zoom
{
    public class GenrateToke
    {
        static readonly char[] padding = { '=' };
        public static string GenerateToken(string meetingNumber, string role)
        {
            String ts = (ToTimestamp(DateTime.UtcNow.ToUniversalTime()) - 30000).ToString();
            string apiKey = "KmY3g2mzSE2_nXmueXfw2g";
            string message = String.Format("{0}{1}{2}{3}", apiKey, meetingNumber, ts, role);
            string apiSecret = "Fv4IVPKx4iPhjxQtYuJL7lkJqmnQAgT0IkuR" ?? "";
            var encoding = new System.Text.ASCIIEncoding();
            byte[] keyByte = encoding.GetBytes(apiSecret);
            byte[] messageBytesTest = encoding.GetBytes(message);
            string msgHashPreHmac = System.Convert.ToBase64String(messageBytesTest);
            byte[] messageBytes = encoding.GetBytes(msgHashPreHmac);
            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
                string msgHash = System.Convert.ToBase64String(hashmessage);
                string token = String.Format("{0}.{1}.{2}.{3}.{4}", apiKey, meetingNumber, ts, role, msgHash);
                var tokenBytes = System.Text.Encoding.UTF8.GetBytes(token);
                return System.Convert.ToBase64String(tokenBytes).TrimEnd(padding);
            }
        }
        public static long ToTimestamp(DateTime value)
        {
            long epoch = (value.Ticks - 621355968000000000) / 10000;
            return epoch;
        }
    }
}