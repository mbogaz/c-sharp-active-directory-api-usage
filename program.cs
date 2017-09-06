using System;
using System.Collections;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;

namespace ConsoleApp3
{
    class Program
    {
        static String ldapPath = "LDAP://localhost:389/";
        static String ldapUserName = "cn=Manager,dc=maxcrc,dc=com";
        static String ldapPassword = "secret";
        
        static void Main(string[] args)
        {
            activeDirectoryProcess();
            Console.WriteLine("press any to exit");
            Console.ReadLine();
        }

        public static void activeDirectoryProcess(){
            /*
            //kayıt update etme % update record
            //CnDn -> objenin yolu % path of the object
            String CnDn = "CN=Panji Pratomo,OU=People,DC=maxcrc,DC=com";
            DirectoryEntry entry = new DirectoryEntry(ldapPath+CnDn, ldapUserName, ldapPassword, AuthenticationTypes.None);
            entry.Rename("CN=" + "isim");
            */

            /*
            //bir dizindeki recordları getirir, type içerir % print records under a path
            //OuDn -> folder%dir yolu
            try{
                String OuDn = "ou=People,dc=maxcrc,dc=com";
                DirectoryEntry directoryObject = new DirectoryEntry(ldapPath + OuDn, ldapUserName, ldapPassword, AuthenticationTypes.None);
                foreach (DirectoryEntry child in directoryObject.Children){
                    Console.WriteLine(child.Path.ToString());
                    child.Close();
                    child.Dispose();
                }
                directoryObject.Close();
                directoryObject.Dispose();
            }
            catch (DirectoryServicesCOMException e){
                Console.WriteLine("An Error Occurred: " + e.Message.ToString());
            }
            */

            //yeni kayıt % new record
            //properties ksımından özellik eklenebilir // properties for new values
            try{
                String userName = "try";
                String userPassword = "1234";
                String oGUID = string.Empty;
                String connectionPrefix = ldapPath+"ou=People,dc=maxcrc,dc=com";
                DirectoryEntry dirEntry = new DirectoryEntry(connectionPrefix, ldapUserName, ldapPassword, AuthenticationTypes.None);
                DirectoryEntry newUser = dirEntry.Children.Add("CN=" + userName, "person");
                newUser.Properties["SN"].Value = userName;
                newUser.CommitChanges();
                oGUID = newUser.Guid.ToString();

                newUser.Invoke("SetPassword", new object[] { userPassword });
                newUser.CommitChanges();
                dirEntry.Close();
                newUser.Close();
            }
            catch (DirectoryServicesCOMException e){
                Console.WriteLine("An Error Occurred: " + e.Message.ToString());
            }
        }
    }
}
