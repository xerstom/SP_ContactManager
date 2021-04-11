using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Data;
using Serialization.Encryption;
using Serialization.SerializableObjects;

namespace Serialization.ConcreteSerializers
{
    /// <summary>
    /// Xml serializer
    /// </summary>
    public class MyXmlSerializer : Serializer
    {
        /// <summary>
        /// Initialiaze the right encryptor and change the serialiaze path
        /// </summary>
        public MyXmlSerializer()
        {
            this.Encryptor = new RijndaelEncryptor();
            this.SerializePath += ".xml";
        }
        /// <summary>
        /// function to write an object to a stream
        /// </summary>
        /// <param name="outputStream"> The stream where to write</param>
        /// <param name="obj"> The object to write</param>
        public override void WriteObjectToStream(Stream outputStream, Object obj)
        {
            if (Object.ReferenceEquals(null, obj))
            {
                return;
            }

            XmlSerializer serializer = new XmlSerializer(typeof(SerializableFolder));
            serializer.Serialize(outputStream, obj);
        

        }

        /// <summary>
        /// function to read an object from a stream
        /// </summary>
        /// <param name="inputStream"> the stream to read from</param>
        /// <returns>The read object</returns>
        public override Object ReadObjectFromStream(Stream inputStream) { 
        
            XmlSerializer xs = new XmlSerializer(typeof(SerializableFolder));
            try
            {
                Object o = xs.Deserialize(inputStream);
                return o;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
           }

        }
    }
}
