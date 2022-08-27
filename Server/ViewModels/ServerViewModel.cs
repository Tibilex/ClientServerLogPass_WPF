using Server.Models;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Windows.Threading;
using System;

namespace Server.ViewModels
{
    public class ServerViewModel : ViewModelBase
    {
        private int _clientId = 1;
        private ClientModel _clientModel;
        private const int PORT = 8088;
        private const string IP = "127.0.0.1";
        private IPEndPoint iPEnd = new IPEndPoint(IPAddress.Parse(IP), PORT);
        private Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        

        public ObservableCollection<ClientModel> UserList { get; set; } = new ObservableCollection<ClientModel>();
        //public int UserId
        //{
        //    get => _clientModel.UserId;
        //    set
        //    {
        //        _clientModel.UserId = value; OnPropertyChanged("UserId");
        //    }
        //}
        public string UserEmail
        {
            get => _clientModel.UserEmail;
            set
            {
                _clientModel.UserEmail = value; OnPropertyChanged("UserEmail");
            }
        }
        //public string UserIp
        //{
        //    get => _clientModel.Ip;
        //    set
        //    {
        //        _clientModel.Ip = value; OnPropertyChanged("UserIp");
        //    }
        //}
        //public int UserPort
        //{
        //    get => _clientModel.Port;
        //    set
        //    {
        //        _clientModel.Port = value; OnPropertyChanged("UserPort");
        //    }
        //}

        public ServerViewModel()
        {
            _clientModel = new ClientModel();
            ServerListen();
            UserList.Add(new ClientModel() { UserId = 1, UserEmail = "Bozasas", UserIp = "127.0.0.0", UserPort = 8080 });
            ConnectedClients(121, "2131", 123);

        }
        private void ServerListen()
        {
            Task.Run(() =>
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(IP), PORT);
                Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                serverSocket.Bind(endPoint);
                serverSocket.Listen(10);
                while (true)
                {
                    Connect(serverSocket.Accept(), _clientId);
                }
            });
        }
        private void Connect(Socket clientSocket, int clientId)
        {
            Task.Run(() =>
            {
                while (true)
                {
                    int bytes = 0;
                    byte[] buffer = new byte[1024];
                    StringBuilder builder = new StringBuilder();
                    do
                    {
                        bytes = clientSocket.Receive(buffer);
                        builder.Append(Encoding.Unicode.GetString(buffer, 0, bytes));
                    } while (clientSocket.Available > 0);

                    if (builder.ToString() != null)
                    {
                        ServerModel reciveData = JsonSerializer.Deserialize<ServerModel>(builder.ToString());
                        User user = new();
                        user.Email = reciveData.Email;
                        if (reciveData.IsRegister)
                        {
                            if (ConnectToDB.LoginCheck(user.Email))
                            {
                                byte[] data = Encoding.Unicode.GetBytes("Уже зарегестрирован");
                                clientSocket.Send(data);
                            }
                            else
                            {
                                user.Password = PasswordCrypt.Generate("152541");
                                ConnectToDB.Create(user);
                                byte[] data = Encoding.Unicode.GetBytes("Успешно зарегестрирован");
                                clientSocket.Send(data);
                            }
                        }
                        if (reciveData.IsLogin)
                        {
                            if (ConnectToDB.LoginCheck(user.Email))
                            {
                                if (ConnectToDB.PasswordCheck(user.Password))
                                {
                                    byte[] data = Encoding.Unicode.GetBytes("Успешный вход");
                                    clientSocket.Send(data);
                                }
                                else
                                {
                                    byte[] data = Encoding.Unicode.GetBytes("Пароль не верен");
                                    clientSocket.Send(data);
                                }
                            }
                        }
                    }
                }
            });
        }
        private void AddClientToList(int id, string email, string ip, int port)
        {
            Dispatcher.CurrentDispatcher.Invoke(new Action(() => {
                UserList.Add(new ClientModel(id, "Не залогинен", ip, port));
            }));
        }

        private void ConnectedClients(int id, string ip, int port)
        {
            Dispatcher.CurrentDispatcher.Invoke(new Action(() => {
                UserList.Add(new ClientModel(id, "Не залогинен", ip, port));
            }));
        }
    }
}
