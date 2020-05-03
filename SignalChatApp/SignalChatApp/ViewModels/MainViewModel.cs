using Microsoft.AspNetCore.SignalR.Client;
using SignalChatApp.Models;
using SignalChatApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SignalChatApp.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        //private HubConnection _hubConnection;

        private string _name;
        private string _message;
        private ObservableCollection<ChatMessage> _messages;
        private bool _isConnected;

        private SignalRService _signalR;

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }


        public string Message
        {
            get
            {
                return _message;
            }
            set
            {
                _message = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ChatMessage> Messages 
        {
            get
            {
                return _messages;
            }
            set
            {
                _messages = value;
                OnPropertyChanged();
            }
        }

        public bool IsConnected
        {
            get
            {
                return _isConnected;
            }
            set
            {
                _isConnected = value;
                OnPropertyChanged();
            }
        }

        public Command SendMessageCommand => new Command(async () => { await SendMessage(Name, Message); });
        public Command ConnectCommand => new Command(async () => await Connect());
        public Command DisconnectCommand => new Command(async () => await Disconnect());


        public MainViewModel()
        {
            _signalR = new SignalRService();
            _signalR.Connected += SignalR_ConnectionChanged;
            _signalR.ConnectionFailed += SignalR_ConnectionChanged;
            _signalR.NewMessageReceived += SignalR_NewMessageReceived;

            Messages = new ObservableCollection<ChatMessage>();

            // localhost for UWP/iOS or special IP for Android
            //var ip = "localhost";
            //if (Device.RuntimePlatform == Device.Android)
            //    ip = "10.0.2.2";

            //_hubConnection = new HubConnectionBuilder()
            //    .WithUrl($"https://signalchatappapi20200125121336.azurewebsites.net/chatHub")
            //    .Build();

            //_hubConnection.On<string>("JoinChat", (user) =>
            //{
            //    Messages.Add(new ChatMessage() { User = Name, Message = $"{user} has joined the chat", IsSystemMessage = true });
            //});

            //_hubConnection.On<string>("LeaveChat", (user) =>
            //{
            //    Messages.Add(new ChatMessage() { User = Name, Message = $"{user} has left the chat", IsSystemMessage = true });
            //});

            //_hubConnection.On<string, string>("ReceiveMessage", (user, message) =>
            //{
            //    Messages.Add(new ChatMessage() { User = user, Message = message, IsSystemMessage = false, IsOwnMessage = Name == user });
            //});
        }

        private void SignalR_NewMessageReceived(object sender, Message message)
        {
            AddMessage(message.Name, message.Text);
        }


        private void SignalR_ConnectionChanged(object sender, bool successful, string user, string message)
        {
            if (successful)
            {
                AddMessage(user, $"{user}: {message}", systemMessage: true);
            }
            else
            {
                AddMessage(user, $"{user} disconnected: {message}", systemMessage: true);
            }
        }

        public async Task Connect()
        {
            //await _hubConnection.StartAsync();
            //await _hubConnection.InvokeAsync("JoinChat", Name);

            await _signalR.ConnectAsync(Name);
            IsConnected = _signalR.IsConnected;
        }

        public async Task Disconnect()
        {
            //await _hubConnection.InvokeAsync("LeaveChat", Name);
            //await _hubConnection.StopAsync();

            IsConnected = false;
        }

        public async Task SendMessage(string user, string message)
        {
            //await _hubConnection.InvokeAsync("SendMessage", user, message);
            await _signalR.SendMessageAsync(user, message);
        }

        private void AddMessage(string user, string message, bool systemMessage = false)
        {
            ChatMessage chatMessage = new ChatMessage()
            {
                User = user,
                Message = message,
                IsSystemMessage = systemMessage,
                IsOwnMessage = Name == user
            };

            Messages.Add(chatMessage);
        }

        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
