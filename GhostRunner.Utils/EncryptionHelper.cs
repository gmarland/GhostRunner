using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostRunner.Utils
{
    public class EncryptionHelper
    {
        #region Public Methods

        /// <summary>
        /// Hash a selected string into a form which cannot be returned from
        /// </summary>
        /// <param name="inputString">The string to be hashed</param>
        /// <param name="salt">A constant used in hashing the input</param>
        public static String Hash(String inputString, String salt)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider cryptographer = new System.Security.Cryptography.MD5CryptoServiceProvider();

            byte[] data = System.Text.Encoding.ASCII.GetBytes(inputString + salt);
            data = cryptographer.ComputeHash(data);

            return BitConverter.ToString(data);
        }

        #endregion
    }
}
