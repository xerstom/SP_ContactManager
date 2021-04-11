using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Serialization.Encryption
{
    public class RijndaelEncryptor : Encryptor
    {

        private const int KEY_SIZE = 256;
        private const int IV_SIZE = 16;

        //To convert anything to a valid key
        private static readonly byte[] SALT = new byte[] { 10, 20, 30, 40, 50, 60, 70, 80 };
        const int ITERATION = 300;

        public byte[] KeyBytes
        {
            get;
            set;
        }
        
        public RijndaelEncryptor()
        {
            CreateKey();
        }

        //https://stackoverflow.com/questions/17195969/generating-aes-256-bit-key-value
        /// <summary>
        /// Function to create a valid key.
        /// </summary>
        /// <param name="keyBytes"></param>
        private void CreateKey(int keyBytes = KEY_SIZE)
        {
            using (var keyGenerator = new Rfc2898DeriveBytes(Key, SALT, ITERATION))
            {
                KeyBytes = keyGenerator.GetBytes(keyBytes / 8);
            }
        }

        /// <summary>
        ///  Function that creates an encryption stream from a regular stream
        /// </summary>
        /// <param name="outputStream"> The regular stream </param>
        /// <returns>the encryption stream</returns>
        public override CryptoStream CreateEncryptionStream(Stream outputStream)
        {
            

            byte[] iv = new byte[IV_SIZE];

            using (var rng = new RNGCryptoServiceProvider())
            {
                // Using a cryptographic random number generator
                rng.GetNonZeroBytes(iv);
            }

            // Write IV to the start of the stream
            outputStream.Write(iv, 0, iv.Length);

            Rijndael rijndael = new RijndaelManaged();
            rijndael.KeySize = KEY_SIZE;

            CryptoStream encryptor = new CryptoStream(
                outputStream,
                rijndael.CreateEncryptor(this.KeyBytes, iv),
                CryptoStreamMode.Write);
            return encryptor;
        }

        /// <summary>
        /// Function that creates an decryption stream from a regular stream
        /// </summary>
        /// <param name="inputStream"> The regular stream</param>
        /// <returns>the decryption stream<</returns>
        public override CryptoStream CreateDecryptionStream(Stream inputStream)
        {

            byte[] iv = new byte[IV_SIZE];

            if (inputStream.Read(iv, 0, iv.Length) != iv.Length)
            {
                throw new ApplicationException("Failed to read IV from stream.");
            }

            Rijndael rijndael = new RijndaelManaged();
            rijndael.KeySize = KEY_SIZE;

            CryptoStream decryptor = new CryptoStream(
                inputStream,
                rijndael.CreateDecryptor(this.KeyBytes, iv),
                CryptoStreamMode.Read);
            return decryptor;
        }
    }
}
