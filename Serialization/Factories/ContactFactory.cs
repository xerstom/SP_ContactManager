using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Data;
using Serialization.SerializableObjects;

namespace Serialization.Factories
{
        /// <summary>
        /// Contact Factory
        /// </summary>
        public static class ContactFactory
        {

            /// <summary>
            /// Extension method that creates a serializableContact from a contact
            /// </summary>
            /// <param name="Contact"> The contact </param>
            /// <returns> The serializableContact</returns>
            public static SerializableContact CreateSerializableContact(this Contact Contact)
            {
                return new SerializableContact(Contact.FirstName, Contact.LastName, Contact.Mail,Contact.Company,Contact.Link, Contact.Creation, Contact.LastUpdate);
            }

        /// <summary>
        /// Extension method that creates a contact from a serializableContact
        /// </summary>
        /// <param name="SerializableContact"> The serializableContact </param>
        /// <returns> The contact</returns>
        public static Contact CreateContact(this SerializableContact SerializableContact)
            {
                return new Contact(SerializableContact.FirstName, SerializableContact.LastName, SerializableContact.Mail, SerializableContact.Company, SerializableContact.Link.ToString(), SerializableContact.Creation, SerializableContact.LastUpdate);
            }
    }
}
