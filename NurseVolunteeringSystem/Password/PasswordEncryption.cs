using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NurseVolunteeringSystem.Password
{
    public static class PasswordEncryption
    {
        public static string Key = "hdw7@bwdubcmxo";

        public static string ConvertToEncrypt(string Password)
        {
            Password += Key;

            var passwordBytes= Encoding.UTF8.GetBytes(Password);

            return Convert.ToBase64String(passwordBytes);
        }

        public static string ConvertToDecryption(string base64EncodeData)
        {
            var base64EncodeBytes = Convert.FromBase64String(base64EncodeData);

            var results = Encoding.UTF8.GetString(base64EncodeBytes);

            results = results.Substring(0, results.Length - Key.Length);

            return results;
        }
    }
}
