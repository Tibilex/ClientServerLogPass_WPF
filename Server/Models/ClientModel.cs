using Server.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Models
{
    public class ClientModel : ViewModelBase
    {
        public int UserId { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public string UserIp { get; set; }
        public int UserPort { get; set; }

        public ClientModel() { }
        public ClientModel(int id, string email, string ip, int port) 
        {
            this.UserId = id;
            this.UserEmail = email;
            this.UserIp = ip;
            this.UserPort = port;
        }
    }
}
