using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiErLan.Data.Model
{
    public class LoginModel
    {
        public string LoginName { set; get; }
        public string Pwd { set; get; }
        public bool RememberPassword { set; get; }
        public string SecurityCode { set; get; }
    }
}
