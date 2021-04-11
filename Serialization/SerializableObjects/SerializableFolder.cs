using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace Serialization.SerializableObjects
{
    /// <summary>
    /// A serializable Folder
    /// </summary>
    [Serializable]
    public class SerializableFolder
    {
        public String Name { get; set; }
        public DateTime Creation { get; set; }
        public DateTime LastUpdate { get; set; }
        public List<SerializableFolder> Folders { get; set; }
        public List<SerializableContact> Contacts { get; set; }

        /// <summary>
        /// Constructor for a SerializableFolder
        /// </summary>
        /// <param name="Name">Name of the folder</param>
        /// <param name="Creation"></param>
        /// <param name="LastUpdate"></param>
        public SerializableFolder(String Name, DateTime Creation, DateTime LastUpdate)
        {
            this.Name = Name;
            this.Creation = Creation;
            this.LastUpdate = LastUpdate;
            this.Folders = new List<SerializableFolder>();
            this.Contacts = new List<SerializableContact>();
        }

        /// <summary>
        /// Default constructor needed to serialiaze
        /// </summary>
        public SerializableFolder() { }
    }
}
