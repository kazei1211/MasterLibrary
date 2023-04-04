using MasterLibrary.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterLibrary.DTOs
{
    public class CustomerDTO
    {
        public CustomerDTO()
        {
            IDROLE = ROLE.Customer;
        }

        public CustomerDTO(int id, string name, string email, string diachi, string user, string password)
        {
            MAKH = id;
            TENKH = name;
            EMAIL = email;
            USERNAME = user;
            USERPASSWORD = password;
            DIACHI = diachi;
            IDROLE = ROLE.Customer;
        }

        public int MAKH { get; set; }
        public string TENKH { get; set; }
        public string EMAIL { get; set; }
        public string USERNAME { get; set; }
        public string DeCodeUSERPASSWORD { get; set; }
        public string USERPASSWORD { get; set; }
        public string IDROLE { get; set; }
        public string DIACHI { get; set; }
    }
}
