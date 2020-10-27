using System;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace StudentLabManager
{
    public class ActiveDirectory
    {
        public string username { get; set; }
        public PrincipalContext context { get; set; } // info about Doamin
        public string displayName { get; set; }
        public Principal user_Info { get; set; }
        public string role { get; set; }
        public ActiveDirectory(string UserName,string UserPassword, PrincipalContext ser) //Constructor methed of the Class For identity with password
        {
            this.username = UserName;
            this.context = ser;
            this.user_Info = Principal.FindByIdentity(ser, UserName);
            this.displayName = this.user_Info.DisplayName;
            this.role = this.GetRole();
        }

        public ActiveDirectory(string UserName) //Constructor method of the Class for identity without password
        {
            this.username = UserName;
            PrincipalContext ser = new PrincipalContext(ContextType.Domain, "uict.nz", "DC=uict,DC=nz");  //UCOL Domain
            
            //Assign values to object.
            this.context = ser;
            this.user_Info = Principal.FindByIdentity(ser, UserName);
            this.displayName = this.user_Info.DisplayName;
            this.role = this.GetRole();

        }

        public string[] GetGroup() //Get User's Group List. 
        {
            Array ClassList = this.user_Info.GetGroups().ToArray(); //Get group list and transfer it to be an array.
            int ClassCount = 0; //for loop function to count the class' number.
            string[] ClassGroup = new string[ClassCount]; //claim a string array with length.
            foreach (object ClassName in ClassList)
            {
                if (ClassName.ToString().Length == 4) //User's Class only have 4 word
                {
                    Array.Resize(ref ClassGroup, ClassGroup.Length + 1);//extends the array length with copy itself  before add new data.
                    ClassGroup[ClassCount] = ClassName.ToString();//add class data
                    ClassCount++;
                }
            };
            return ClassGroup;
        }

        public string GetRole() //Get User's Role
        {
            string role = this.user_Info.GetGroups().ToArray()[1].ToString(); //We use the second data on the Group List to identity user's role.
            if (role != "Student") // if they are user, the second data will be "Student".
            {
                role = "Staff"; //if not,they are Staff
            }
            return role;
        }

        public Boolean ChangeOwnPassword (string oldPassword,string newPassword) //Set User's own password
        {
            try
            {
                if (this.context.ValidateCredentials(this.user_Info.SamAccountName, oldPassword)) //Valid User by their username and password
                {
                    var uer = UserPrincipal.FindByIdentity(this.context, this.user_Info.SamAccountName);
                    uer.ChangePassword(oldPassword, newPassword);
                    uer.Save();
                    return true;
                }
                else
                {
                    return false;
                }
            } catch(Exception e)
            {
                return false;
            }

        }

        public Boolean ResetPassword(string studentName,string newPassword,string adminPassword) //Set student's password by Staff
        {
            try
            {
                PrincipalContext ser = new PrincipalContext(ContextType.Domain, "uict.nz", "DC=uict,DC=nz", this.username, adminPassword); //Check if the user is admin 
                if (ser.ValidateCredentials(this.username, adminPassword))
                {
                    var uer = UserPrincipal.FindByIdentity(ser, studentName);//Get user's info as Admin
                    uer.SetPassword(newPassword);
                    return true;
                }
                else
                {
                    return false;
                }
            } catch (Exception e)
            {
                throw (e);
            }
            
            
        }
    }
}
