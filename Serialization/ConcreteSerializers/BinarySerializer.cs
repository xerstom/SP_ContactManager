using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Serialization.Encryption;

namespace Serialization.ConcreteSerializers
{
    /// <summary>
    /// Binary Serializer 
    /// </summary>
    public class BinarySerializer : Serializer
    {

        /// <summary>
        /// Constructor that instanciate an encryptor and that add the right extension file
        /// </summary>
        public BinarySerializer()
        {
            this.Encryptor = new RijndaelEncryptor();
            this.SerializePath += ".dat";
    }

        /// <summary>
        /// Function that write an object to a stream
        /// </summary>
        /// <param name="outputStream"> the stream to write to</param>
        /// <param name="obj"> the object to write</param>
        public override void WriteObjectToStream(Stream outputStream, Object obj)
        {
            if (Object.ReferenceEquals(null, obj))
            {
                return;
            }

            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(outputStream, obj);
        }

        /// <summary>
        /// Function that reads an object from a stream
        /// </summary>
        /// <param name="inputStream"> the stream to reads from</param>
        /// <returns>The read object</returns>
        public override Object ReadObjectFromStream(Stream inputStream)
        {
            BinaryFormatter binForm = new BinaryFormatter();
            return binForm.Deserialize(inputStream);
        }

    }
}
