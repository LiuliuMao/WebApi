using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewEntitys.QueryParameter
{
    public class UserParameter: UserViewModel
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
    }
}
