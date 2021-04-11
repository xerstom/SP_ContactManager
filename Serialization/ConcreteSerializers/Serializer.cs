using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using Serialization.Factories;
using Data;
using Serialization.SerializableObjects;
using Serialization.Encryption;

namespace Serialization.ConcreteSerializers
{
    /// <summary>
    /// Abstract serializer
    /// </summary>
    public abstract class Serializer
    {
        protected Encryptor Encryptor { get; set; }

        protected String SerializePath { get; set; } = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\ContactManager\\{Environment.UserName}";
        public void setKey(String Key)
        {
            Encryptor.Key = Key;
        }

        /// <summary>
        /// Function that write an object to a stream
        /// </summary>
        /// <param name="outputStream"> the stream to write to</param>
        /// <param name="obj"> the object to write</param>
        public abstract void WriteObjectToStream(Stream outputStream, Object obj);

        /// <summary>
        /// Function that reads an object from a stream
        /// </summary>
        /// <param name="inputStream"> the stream to reads from</param>
        /// <returns>The read object</returns>
        public abstract Object ReadObjectFromStream(Stream inputStream);


        // https://stackoverflow.com/questions/28791185/encrypt-net-binary-serialization-stream

        /// <summary>
        /// Function to serialize a folder
        /// </summary>
        /// <param name="f"> The folder to serialize</param>
        public void Serialize(Folder f)
        {

            Directory.CreateDirectory(Path.GetDirectoryName(this.SerializePath));
            using (FileStream file = new FileStream(this.SerializePath, FileMode.Create))
            {
                using (CryptoStream cryptoStream = Encryptor.CreateEncryptionStream(file))
                {
                    WriteObjectToStream(cryptoStream, f.CreateSerializableFolder());
                }
            }
        }

        /// <summary>
        /// Function to deserialize a folder
        /// </summary>
        /// <returns>The deserialized folder</returns>
        public Folder Deserialize()
        {

            SerializableFolder folder;

            using (FileStream file = new FileStream(this.SerializePath, FileMode.Open))
            {
                using (CryptoStream cryptoStream = Encryptor.CreateDecryptionStream(file))
                {
                    folder = (SerializableFolder)ReadObjectFromStream(cryptoStream);
                }
            }
            return folder.CreateFolder();
        }
    }
    
}
