using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    /// <summary>
    /// Function representing a Folder in a tree
    /// </summary>
    public class Folder
    {
        public String Name { get; set; }
        public DateTime Creation { get; set; }
        public DateTime LastUpdate { get; set; }
        public List<Folder> Folders { get; set; }
        public List<Contact> Contacts { get; set; }

        /// <summary>
        /// Constructor for a folder
        /// </summary>
        /// <param name="Name"> The name of the folder</param>
        public Folder(String Name)
        {
            this.Name = Name;
            this.Creation = DateTime.Now;
            this.LastUpdate = DateTime.Now;
            this.Folders = new List<Folder>();
            this.Contacts = new List<Contact>();
        }

        /// <summary>
        /// Constructor for a folder
        /// </summary>
        /// <param name="Name"> The name of the folder</param>
        /// <param name="Creation"> The date of the creation</param>
        /// <param name="LastUpdate">The date of the last update</param>
        public Folder(String Name, DateTime Creation, DateTime LastUpdate): this(Name)
        {
            this.Creation = Creation;
            this.LastUpdate = LastUpdate;
        }

        /// <summary>
        /// function to add a contact to this folder
        /// </summary>
        /// <param name="c">the contact to add</param>
        public void addContact(Contact c)
        {
            Contacts.Add(c);
        }
        /// <summary>
        /// function to add a folder to this folder
        /// </summary>
        /// <param name="f"> the folder to add</param>
        public void addFolder(Folder f)
        {
            Folders.Add(f);
        }
    
        /// <summary>
        /// Function to get a children of this folder
        /// </summary>
        /// <param name="Name">The name of the child</param>
        /// <returns> The found children folder</returns>
        public Folder getChildren(String Name)
        {
            if(this.Folders.Exists(x => x.Name == Name)){
                return this.Folders.Find(x => x.Name == Name);
            }
            return null;
        }


    }
}
