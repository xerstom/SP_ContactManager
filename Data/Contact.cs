using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    /// <summary>
    /// Class representing a contact
    /// </summary>
    public class Contact
    {
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Mail { get; set; }
        public String Company { get; set; }
        public Link Link { get; set; }
        public DateTime Creation { get; set; }
        public DateTime LastUpdate { get; set; }

        /// <summary>
        /// Constructor of contact. Transforms link from string to Enum Link.
        /// </summary>
        /// <param name="FirstName">The firstname of the contact</param>
        /// <param name="LastName">The LastName of the contact</param>
        /// <param name="Mail">The Mail of the contact</param>
        /// <param name="Company">The Company of the contact</param>
        /// <param name="Link">The Link with the contact.</param>
        public Contact(String FirstName, String LastName, String Mail, String Company, String Link)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Mail = Mail;
            this.Company = Company;
            this.Link = (Link)System.Enum.Parse(typeof(Link), Link);
            this.Creation = DateTime.Now;
            this.LastUpdate = DateTime.Now;

        }
        /// <summary>
        /// Constructor of contact. Transforms link from string to Enum Link.
        /// </summary>
        /// <param name="FirstName">The firstname of the contact</param>
        /// <param name="LastName">The LastName of the contact</param>
        /// <param name="Mail">The Mail of the contact</param>
        /// <param name="Company">The Company of the contact</param>
        /// <param name="Link">The Link with the contact.</param>
        /// <param name="Creation"> The creation date</param>
        /// <param name="LastUpdate"> The last update date</param>
        public Contact(String FirstName, String LastName, String Mail, String Company, String Link, DateTime Creation, DateTime LastUpdate) : this(FirstName,LastName,Mail,Company,Link)
        {
            this.Creation = Creation;
            this.LastUpdate = LastUpdate;

        }
    }
}
