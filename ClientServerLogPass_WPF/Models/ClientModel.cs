using ClientServerLogPass_WPF.ViewModels;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ClientServerLogPass_WPF.Models
{
    public class ClientModel : ViewModelBase
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool EmailCheck { get; set; }
        public bool PasswordCheck { get; set; }
    }
}
