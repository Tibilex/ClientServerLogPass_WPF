using Server.ViewModels;

namespace Server.Models
{
    public class ServerModel : ViewModelBase
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Ip { get; set; }
        public int Port { get; set; }
        public bool IsLogin { get; set; }
        public bool IsRegister { get; set; }

        public ServerModel(){}
        public ServerModel(int id)
        {
            this.Id = id;
        }
        public ServerModel(int id, string email, string ip, int port)
        {
            this.Id = id;
            this.Email = email;
            this.Ip = ip;
            this.Port = port;
        }
    }
}
