using ClientServerLogPass_WPF.ViewModels;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ClientServerLogPass_WPF.Models
{
    public class ClientModel : ViewModelBase
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Ip { get; set; }
        public int Port { get; set; }
        public bool IsLogin { get; set; }
        public bool IsRegister { get; set; }
    }
}
