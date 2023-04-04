using MasterLibrary.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterLibrary.DTOs
{
    public class AdminDTO
    {
        public AdminDTO()
        {
            IDROLE = ROLE.Admin;
        }

        public AdminDTO(int makh, string name, string email, string user, string password)
        {
            MAKH = makh;
            TENKH = name;
            EMAIL = email;
            USERNAME = user;
            USERPASSWORD = password;
            IDROLE = ROLE.Admin;
        }

        public int MAKH { get; set; }
        public string TENKH { get; set; }
        public string EMAIL { get; set; }
        public string USERNAME { get; set; }
        public string USERPASSWORD { get; set; }
        public string IDROLE { get; set; }
    }
}
