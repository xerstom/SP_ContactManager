using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Data;
using Serialization.ConcreteSerializers;
using System.IO;

namespace ContactManager
{
    /// <summary>
    /// Main program running the contact manager
    /// </summary>
    class Program
    {
        /// <summary>
        /// Manager of the datas
        /// </summary>
        static DataManager mg;

        /// <summary>
        /// Class that handles serialization
        /// </summary>
        static Serializer Serializer;

        /// <summary>
        /// Main method running the contact manager
        /// </summary>
        /// <param name="args"> Arguments given when starting the app</param>
        static void Main(string[] args)
        {
            mg = new DataManager();
            Serializer = new BinarySerializer();
            int ret = 0;
            String instruction;

            while (ret != -1)
            {
                DisplayOptions();
                instruction = Console.ReadLine();
                ret = Menu(instruction);
            }
        }

        /// <summary>
        /// Function that displays the menu options
        /// </summary>
        static void DisplayOptions()
        {
            Console.WriteLine("----------Menu----------\n");
            Console.WriteLine("Pour quitter : \"sortir\"");
            Console.WriteLine("Pour afficher les dossiers : \"afficher\"");
            Console.WriteLine("Pour charger : \"charger\"");
            Console.WriteLine("Pour enregistrer : \"enregistrer\"");
            Console.WriteLine("Pour ajouter un dossier : \"ajouterdossier\"");
            Console.WriteLine("Pour ajouter un contact : \"ajoutercontact\"");
        }

        /// <summary>
        /// Function to handle menu
        /// </summary>
        /// <param name="instruction"> Instruction given by the user</param>
        /// <returns></returns>
        static int Menu(String instruction)
        {
        
            Console.Clear();
            switch (instruction)
            {
                case "sortir":
                    return -1;
                case "afficher":
                    display(mg.Root,0);
                    Console.WriteLine("Case afficher");
                    return 0;
                case "charger":
                    LoadMenu();
                    return 0;
                case "enregistrer":
                    SaveMenu();
                    return 0;
                case "ajouterdossier":
                    return AddFolderMenu();
                case "ajoutercontact":
                    return AddContactMenu();
                default:
                    Console.WriteLine("Instruction inconnue.");
                    return -2;
            }
        }

        /// <summary>
        /// Recursive function to display tree from the folder
        /// </summary>
        /// <param name="f"> Folder from where to start</param>
        /// <param name="depth"> depth in the tree</param>
        public static void display(Folder f, int depth)
        {
            DisplayFolder(f,depth);

            foreach (Folder cur in f.Folders)
            {
                display(cur, depth + 1);
            }
            DisplayContacts(f.Contacts, depth + 1);
        }

        //Methods to Handle menu actions

        /// <summary>
        /// Handle add contact from menu
        /// </summary>
        public static int AddContactMenu()
        {
            String path;

            Console.WriteLine("Ou souhaitez vous l'ajouter ?");
            path = Console.ReadLine();
            path = path != "" ? path : null;

            Contact c = CreateContact();
            DisplayContact(c,0);

            Console.ReadLine();
            mg.AddContact(path, c);
            Console.WriteLine("Case ajoutercontact");

            return 0;
        }

        /// <summary>
        /// Handle add folder from menu
        /// </summary>
        public static int AddFolderMenu()
        {
            String path;

            Console.WriteLine("Ou souhaitez vous l'ajouter ?");
            path = Console.ReadLine();
            path = path != "" ? path : null ;

            Folder f = CreateFolder();
            DisplayFolder(f,0);
            Console.ReadLine();
            if (mg.AddFolder(path, f) == 0) {
                Console.WriteLine("Dossier ajouté avec succès");
            }
            else
            { 
                Console.WriteLine("Chemin introuvable");
            }
            return 0;
        }

        /// <summary>
        /// Function to ask for a key to the user.
        /// </summary>
        /// <returns>The key</returns>
        private static string getKey()
        {
            String key = "";
            Console.WriteLine("Voulez vous entrer une clé ? (o/n)");
            key = Console.ReadLine();
            while ( key != "o" && key != "n")
            {
                Console.WriteLine("Valeur Invalide");
                Console.WriteLine("Voulez vous entrer une clé ? (o/n)");
                key = Console.ReadLine();
            }
            if (key == "o")
            {
                Console.WriteLine("Entrez votre clé ? ");
                key = Console.ReadLine();
                
            return key;
            }
            return null;
        }
        /// <summary>
        /// Handle Save tree from menu
        /// </summary>
        public static void SaveMenu()
        {

            String key = getKey();
            if(key != null)
            {
                Serializer.setKey(key);
            }
            Serializer.Serialize(mg.Root);

        }
        /// <summary>
        /// Handle Load tree from menu
        /// </summary>
        public static void LoadMenu()
        {
            String key = getKey();
            if (key != null)
            {
                Serializer.setKey(key);
            }
            try
            {
                mg.Root = Serializer.Deserialize();
            }
            catch (CryptographicException)
            {
                Console.WriteLine("Clé invalide");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Fichier introuvable");
            }
        }

        //Functions to Handle Contact

        /// <summary>
        /// Function to create contact in a wizard way
        /// </summary>
        /// <returns> The created Contact</returns>
        public static Contact CreateContact() { 
            String FirstName, LastName, Mail, Company, Link;
            Console.WriteLine("---------Creation de contact---------");

            Console.WriteLine("Entrez le prénom:");
            FirstName = Console.ReadLine();

            Console.WriteLine("Entrez le nom:");
            LastName = Console.ReadLine();

            Console.WriteLine("Entrez le mail:");
            Mail = Console.ReadLine();

            Console.WriteLine("Entrez la société:");
            Company = Console.ReadLine();

            Console.WriteLine("Entrez le lien (ami,collègue,relation,réseau):");
            Link = Console.ReadLine();

            return mg.CreateContact(FirstName, LastName, Mail, Company, Link);

        }

        /// <summary>
        /// Function to display a list of contacts
        /// </summary>
        /// <param name="Contacts"> The list of Contacts to display</param>
        /// <param name="depth"> The depth in the tree</param>
        public static void DisplayContacts(List<Contact> Contacts , int depth)
        {
            foreach (Contact c in Contacts)
            {
                DisplayContact(c,depth);
            }
        }

        /// <summary>
        /// Function to display a contact
        /// </summary>
        /// <param name="Contacts"> The Contact to display</param>
        /// <param name="depth"> The depth in the tree</param>
        public static void DisplayContact(Contact c,int depth)
        {
            String tabs = "";
            for (int i = 0; i < depth; i++)
            {
                tabs += '\t';
            }

            Console.WriteLine("{0}[C] {1}, {2} ({3}), Email:{4}, Link:{5}",tabs,c.FirstName,c.LastName,c.Company,c.Mail,c.Link);
        }

        //Functions to Handle Folder


        /// <summary>
        /// Function to create Folder in a wizard way
        /// </summary>
        /// <returns> The created Folder</returns>
        public static Folder CreateFolder()
        {
            String Name;
            Console.WriteLine("---------Creation de Dossier---------");

            Console.WriteLine("Entrez le nom du dossier:");
            Name = Console.ReadLine();


            return mg.CreateFolder(Name);

        }

        /// <summary>
        /// Function to display a list of Folders
        /// </summary>
        /// <param name="Folders"> The list of folders to display</param>
        /// <param name="depth"> The depth in the tree</param>
        public static void DisplayFolders(List<Folder> Folders,int depth)
        {
            foreach (Folder f in Folders)
            {
                DisplayFolder(f,depth);
            }
        }
        /// <summary>
        /// Function to display a folder
        /// </summary>
        /// <param name="f"> the folder to display</param>
        /// <param name="depth"> the depth in the tree</param>
        public static void DisplayFolder(Folder f,int depth)
        {
            String tabs = "";
            for(int i = 0; i < depth; i++)
            {
                tabs += '\t';
            }
            Console.WriteLine("{0}[D] {1} (création {2})",tabs,f.Name,f.Creation);
        }
    }

}