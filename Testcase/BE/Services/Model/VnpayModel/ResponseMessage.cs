using Repositories.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP391Project.Services.Model.VnpayModel
{
    public class ResponseMessage
    {
        public string ResponseCode { get; set; }
        public string Message { get; set; }
        public Payment Payment { get; set; }
    }
}
