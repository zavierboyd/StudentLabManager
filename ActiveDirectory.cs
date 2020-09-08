using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Threading.Tasks;

namespace StudentLabManager
{
    public class ActiveDirectory
    {
        public string username { get; set; }
        public string password { get; set; }
        public PrincipalContext context { get; set; }
        public string displayName { get; set; }
        public Principal user_Info { get; set; }
        public ActiveDirectory(string UserName,string UserPassword, PrincipalContext ser)
        {
            this.username = UserName;
            this.password = UserPassword;
            this.context = ser;
            this.user_Info = Principal.FindByIdentity(ser, UserName);
            this.displayName = this.user_Info.DisplayName;
            

        }

        public Array GetGroup(string UserName)
        {
            Array a = this.user_Info.GetGroups().ToArray();
            return a;
        }
    }
}
