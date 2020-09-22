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
        public string password { get; set; }
        public PrincipalContext context { get; set; }
        public string displayName { get; set; }
        public Principal user_Info { get; set; }
        public string role { get; set; }
        public ActiveDirectory(string UserName, string UserPassword, PrincipalContext ser)
        {
            this.username = UserName;
            this.password = UserPassword;
            this.context = ser;
            this.user_Info = Principal.FindByIdentity(ser, UserName);
            this.displayName = this.user_Info.DisplayName;
            this.role = this.getRole();


        }

        public ActiveDirectory(string UserName)
        {
            this.username = UserName;
            PrincipalContext ser = new PrincipalContext(ContextType.Domain, "uict.nz", "DC=uict,DC=nz");
            this.context = ser;
            this.user_Info = Principal.FindByIdentity(ser, UserName);
            this.displayName = this.user_Info.DisplayName;
            this.role = this.getRole();

        }

        public string[] GetGroup(string UserName)
        {
            Array ClassList = this.user_Info.GetGroups().ToArray();
            int ClassCount = 0;
            string[] ClassGroup = new string[ClassCount];
            foreach (object ClassName in ClassList)
            {
                if (ClassName.ToString().Length == 4)
                {
                    Array.Resize(ref ClassGroup, ClassGroup.Length + 1);
                    ClassGroup[ClassCount] = ClassName.ToString();
                    ClassCount++;
                }
            };
            return ClassGroup;
        }

        public string getRole()
        {
            string role = this.user_Info.GetGroups().ToArray()[1].ToString();
            if (role != "Student")
            {
                role = "Staff";
            }
            return role;
        }

    }
}
