using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Serialization.Encryption
{
    /// <summary>
    /// Abstract encryptor class
    /// </summary>
    public abstract class Encryptor
    {
        /// <summary>
        /// Encryption key
        /// </summary>
        public string Key { get;  set; }

        /// <summary>
        /// Constructor that takes a key.
        /// </summary>
        /// <param name="Key"> The key</param>
        public Encryptor(string Key)
        {
            this.Key = Key;
        }

        /// <summary>
        /// Default constructor taking Windows SID as a key
        /// </summary>
        public Encryptor() : this(WindowsIdentity.GetCurrent().User.Value){ }

        /// <summary>
        ///  Function that creates an encryption stream from a regular stream
        /// </summary>
        /// <param name="outputStream"> The regular stream </param>
        /// <returns>the encryption stream</returns>
        public abstract CryptoStream CreateEncryptionStream(Stream outputStream);

        /// <summary>
        /// Function that creates an decryption stream from a regular stream
        /// </summary>
        /// <param name="inputStream"> The regular stream</param>
        /// <returns>the decryption stream<</returns>
        public abstract CryptoStream CreateDecryptionStream(Stream inputStream);
    }
}
