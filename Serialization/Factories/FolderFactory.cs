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
    /// Folder factory
    /// </summary>
    public static class FolderFactory
    {

        /// <summary>
        /// Extension method to create a SerializableFolder from a folder
        /// </summary>
        /// <param name="folder"> the folder</param>
        /// <returns> The SerializableFolder</returns>
        public static SerializableFolder CreateSerializableFolder(this Folder folder)
        {
            SerializableFolder SerialFolder = new SerializableFolder(folder.Name, folder.Creation, folder.LastUpdate);

            foreach ( var Folder in folder.Folders)
            {
                SerialFolder.Folders.Add(Folder.CreateSerializableFolder());
            }

            foreach (var Contact in folder.Contacts)
            {
                SerialFolder.Contacts.Add(Contact.CreateSerializableContact());
            }

            return SerialFolder;
        }

        /// <summary>
        /// Extension method to create a folder  from a SerializableFolder
        /// </summary>
        /// <param name="folder">  The SerializableFolder</param>
        /// <returns> the folder </returns>
        public static Folder CreateFolder(this SerializableFolder folder)
        {
            Folder NormalFolder = new Folder(folder.Name, folder.Creation, folder.LastUpdate);

            foreach (var Folder in folder.Folders)
            {
                NormalFolder.addFolder(Folder.CreateFolder());
            }

            foreach (var Contact in folder.Contacts)
            {
                NormalFolder.Contacts.Add(Contact.CreateContact());
            }

            return NormalFolder;
        }
    }
}
