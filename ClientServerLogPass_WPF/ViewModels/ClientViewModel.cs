using ClientServerLogPass_WPF.Commands;
using ClientServerLogPass_WPF.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace ClientServerLogPass_WPF.ViewModels
{
    public class ClientViewModel : ViewModelBase
    {
        private string _status;
        private string _loginCheck;
        private string _passwordCheck;
        private string _login;
        private string _pasword;
        private ClientModel _client;
        private Socket _clientSocket;
        private static List<int> _portList = new List<int> {8081, 7021, 8084, 6504, 3436};
        private string _ip = "127.0.0.1";
        private Random _random;

        public string Status
        {
            get => _status;
            set{ _status = value; OnPropertyChanged("Status");}
        }
        public string LoginCheck
        {
            get => _loginCheck;
            set { _loginCheck = value; OnPropertyChanged("LoginCheck"); } 
        }
        public string PasswordCheck
        {
            get => _passwordCheck;
            set { _passwordCheck = value; OnPropertyChanged("PasswordCheck"); }
        }
        public string Login
        {
            get => _login;
            set { _login = value; OnPropertyChanged("Login"); }
        }
        public string Password
        {
            get => _pasword;
            set { _pasword = value; OnPropertyChanged("Password"); }
        }
        public RelayCommand SendMessageComand { get; private set; }
        public RelayCommand DisconectComand { get; private set; }

        public ClientViewModel()
        {
            _random = new Random();
            SendMessageComand = new RelayCommand(obj => ConnectingMode());
            DisconectComand = new RelayCommand(obj => CloseServerConnection());
            //ConnectingToServer();
        }

        private void ConnectingToServer()
        {
            _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _clientSocket.Connect(new IPEndPoint(IPAddress.Parse(_ip), _portList[_random.Next(0, 5)]));
        }

        private void CloseServerConnection()
        {
            _clientSocket.Shutdown(SocketShutdown.Both);
            _clientSocket.Close();
        }

        private void SendDataToServer(ClientModel client)
        {
            byte[] data = Encoding.Unicode.GetBytes(JsonSerializer.Serialize(client));
            _clientSocket.Send(data);
        }

        private void ConnectingMode()
        {
            ClientModel client = new ClientModel();
            client.Email = Login;
            client.Password = Password;
            
        }
    }
}
