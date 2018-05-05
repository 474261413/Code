using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiErLan.Data.Model
{
    public class ClientReq
    {
        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }
        public string Name { get; set; }
        public int? Type { get; set; }
    }
}
