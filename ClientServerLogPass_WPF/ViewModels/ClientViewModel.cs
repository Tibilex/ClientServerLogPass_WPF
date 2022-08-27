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
        private bool _signInUpSwitcher;
        private ClientModel _client;
        private Socket _clientSocket;
        private string _ip = "127.0.0.1";
        private int _port = 8088;

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
        public bool SignInUpSwitcher { get; set; }
        public RelayCommand SendMessageComand { get; private set; }
        public RelayCommand DisconectComand { get; private set; }
        public RelayCommand SignInUpSwitcherComand { get; private set; }
        public RelayCommand LoginComand { get; private set; }

        public ClientViewModel()
        {
            SendMessageComand = new RelayCommand(obj => ConnectingMode());
            DisconectComand = new RelayCommand(obj => CloseServerConnection());
            ConnectingToServer();
        }

        private void ConnectingToServer()
        {
            _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _clientSocket.Connect(new IPEndPoint(IPAddress.Parse(_ip), _port));
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
            client.Ip = _ip;
            client.Port = _port;
            if (SignInUpSwitcher)
            {
                client.IsLogin = true;
                SendDataToServer(client);
                //Recive();
            }
            if (!SignInUpSwitcher)
            {
                client.IsRegister = true;
                SendDataToServer(client);
                //Recive();
            }
        }
  
        private void Recive()
        {
            int bytes = 0;
            byte[] buffer = new byte[1024];
            StringBuilder builder = new StringBuilder();
            do
            {
                bytes = _clientSocket.Receive(buffer);
                builder.Append(Encoding.Unicode.GetString(buffer, 0, bytes));
            } while (_clientSocket.Available > 0);
            string message = builder.ToString();
            if (message == "Уже зарегестрирован") { LoginCheck = "Уже зарегестрирован";}
            else { PasswordCheck = "Успешно зарегестрирован"; }
            if (message == "Успешный вход") { LoginCheck = "Успешный вход"; }
            else { PasswordCheck = "Пароль не верен"; }

        }
    }
}
