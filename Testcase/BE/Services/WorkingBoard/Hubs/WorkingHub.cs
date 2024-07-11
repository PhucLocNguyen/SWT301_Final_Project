using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repositories;
using Repositories.Entity;
using SWP391Project.Services.BusinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP391Project.Services.WorkingBoard.Hubs
{
    public class WorkingHub :Hub
    {
        private readonly MyDbContext _context;
        public WorkingHub(MyDbContext context)
        {
            if (context == null)
            {
                _context = new MyDbContext();
            }
            else { _context = context; }
        }
        public async Task NotifyOrderUpdated(int orderId)
        {
            await Clients.All.SendAsync("ReceiveOrderUpdate", orderId);
        }
    }
}
