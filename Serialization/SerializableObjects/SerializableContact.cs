using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace Serialization.SerializableObjects
{
    /// <summary>
    /// A serializable contact
    /// </summary>
    [Serializable]
    public class SerializableContact
    {
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Mail { get; set; }
        public String Company { get; set; }
        public Link Link { get; set; }
        public DateTime Creation { get; set; }
        public DateTime LastUpdate { get; set; }

        /// <summary>
        /// Constructor of SerializableContact
        /// </summary>
        /// <param name="FirstName">The firstname of the contact</param>
        /// <param name="LastName">The LastName of the contact</param>
        /// <param name="Mail">The Mail of the contact</param>
        /// <param name="Company">The Company of the contact</param>
        /// <param name="Link">The Link with the contact.</param>
        /// <param name="Creation"> The creation date</param>
        /// <param name="LastUpdate"> The last update date</param>

        public SerializableContact(String FirstName, String LastName,String Mail, String Company, Link Link, DateTime Creation, DateTime LastUpdate)
        {
            this.FirstName = FirstName;
            this.LastName = LastName ;
            this.Mail = Mail;
            this.Company = Company;
            this.Link = Link;
            this.Creation = Creation;
            this.LastUpdate = LastUpdate;
        }

        /// <summary>
        /// Default constructor needed to serialiaze
        /// </summary>
        public SerializableContact() { }

    }
}
