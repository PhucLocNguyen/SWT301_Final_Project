using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP391Project.Services.Model.UserModel
{
    public class RequestSearchUserModel
    {
        public string? RoleFromInput { get; set; }
        public int pageIndex { get; set; } = 1;

        public int pageSize { get; set; } = int.MaxValue;
    }
    
}
