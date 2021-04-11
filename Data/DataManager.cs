using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    /// <summary>
    /// Class to manage Datas. Wrote as a design pattern "Mediator".
    /// </summary>
    public class DataManager
    {
        public Folder Root { get; set; }
        public Folder Cur { get; set; }
        public DataManager()
        {
            this.Root = new Folder("root");
            this.Cur = this.Root;
        }


        /// <summary>
        /// Function to find a folder from a path
        /// </summary>
        /// <param name="path">The given path</param>
        /// <returns> The found folder, null if it doesn't exist or the last used folder if no path given</returns>
        private Folder FindFolderFromPath(String path)
        {
            String[] splited = new String[] { };
            if (path == null)
            {
                return this.Cur;
            }
            else if (path == "/")
            {
                return this.Root;
            }
            splited = path.Split('/');
            Folder cur = this.Root;
            for (int i = 0; i < splited.Length; i++)
            {
                cur = cur.getChildren(splited[i]);
                if (cur == null)
                {
                    return null;
                }
            }
            return cur;
        }

        //Contact

        /// <summary>
        /// Function to add a contact at a given path
        /// </summary>
        /// <param name="path">path where to add the contact </param>
        /// <param name="c"> the contact to add</param>
        /// <returns> 0 if it went well or -1 if path not found</returns>
        public int AddContact(String path, Contact c)
        {
            Folder cur = FindFolderFromPath(path);
            if(cur != null)
            {
                cur.addContact(c);
                return 0;
            }
            return -1;
        }
        /// <summary>
        /// Function to create a contact. 
        /// </summary>
        /// <param name="FirstName">The firstname of the contact</param>
        /// <param name="LastName">The LastName of the contact</param>
        /// <param name="Mail">The Mail of the contact</param>
        /// <param name="Company">The Company of the contact</param>
        /// <param name="Link">The Link with the contact.</param>
        public Contact CreateContact(String FirstName, String LastName, String Mail, String Company, String Link)
        {
            return new Contact(FirstName, LastName,Mail, Company,Link);
        }


        //Folder

        /// <summary>
        /// Function to add a folder at a given path
        /// </summary>
        /// <param name="path">path where to add the folder </param>
        /// <param name="c"> the folder to add</param>
        /// <returns> 0 if it went well or -1 if path not found</returns>
        public int AddFolder(String path, Folder f)
        {
            
            Folder cur = FindFolderFromPath(path);
            if (cur != null) { 
                cur.addFolder(f);
                this.Cur = f;
                return 0;
            }
            return -1;
        }
        /// <summary>
        /// Function to create a folder.
        /// </summary>
        /// <param name="Name">The name of the folder</param>
        /// <returns> The created Folder</returns>
        public Folder CreateFolder(String Name)
        {
            return new Folder(Name);
        }

    }
}
