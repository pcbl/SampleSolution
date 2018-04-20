using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;

namespace Chat
{
    public class ChatViewModel : PropertyChangedBase
    {
        //Service Bus/Namespace connectionstring
        private readonly string _connectionString = "Endpoint=sb://pcblbus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=QpRohfDTibGF6P3Fki/tOUxqlTC76XfADt4TKcZQ8pI=";
        //We will be using this topic to handle the chat conversations
        private readonly string _topicName = "chat_history";

        private readonly NamespaceManager _busClient;
        //Topic client, will be used to send messages to the chat!
        private static TopicClient _chatClient;

        private SubscriptionClient _currentSubscription;
        //This Id will be used by this client to known when a message belongs to himself!
        private Guid _senderId;


        public ObservableCollection<ChatMessage> ChatHistory { get; private set; }

        public bool IsConnected { get; set; }

        public string Username { get; set; }

        public string Message { get; set; }

        public ChatViewModel()
        {
            IsConnected = false;
            _busClient = NamespaceManager.CreateFromConnectionString(_connectionString);
            ChatHistory = new ObservableCollection<ChatMessage>();
            //To send messages, we need a Topic Client!!
            _chatClient = TopicClient.CreateFromConnectionString(_connectionString, _topicName);
        }

        public void ConnectOrDisconnect(string username)
        {
            bool isConnecting = !IsConnected;

            IsConnected = !IsConnected;            
            if (isConnecting)
            {
                //let´s create a new SenderID
                _senderId = Guid.NewGuid();
                var subscription = _busClient.CreateSubscription(_topicName, _senderId.ToString());
                //The subscription will delete itself when iddle more than 5 minutes!
                //That will be used case the application crashes
                subscription.AutoDeleteOnIdle = new TimeSpan(0, 5, 0);

                //Now we will use the subscription we just created and will create a Subscription Client
                //The idea is to print all messages that we receive!
                _currentSubscription = SubscriptionClient.CreateFromConnectionString(_connectionString, _topicName, _senderId.ToString());
                _currentSubscription.OnMessage(WhenMessageArrives);

                //Sending has joined message
                Send("**just joined**");
            }
            else
            {
                //Close subscription Client
                _currentSubscription.Close();
                //Then Remove it
                _busClient.DeleteSubscription(_topicName,_senderId.ToString());
            }

        }

        public bool CanSend(string message)
        {
            return IsConnected && !string.IsNullOrWhiteSpace(message);
        }

        public void Send(string message)
        {
            try
            {
                var chatMessage = new ChatMessage() {Sender = Username, Message = message, SenderId = _senderId};
                var topicMessage = new BrokeredMessage(chatMessage);
                _chatClient.Send(topicMessage);
                Message = string.Empty;
            }
            catch (Exception ex)

            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void WhenMessageArrives(BrokeredMessage message)
        {
            var chatMessage = message.GetBody<ChatMessage>();
            //We must run on UI Thread!!
            Execute.OnUIThread(()=>ChatHistory.Insert(0, chatMessage));
        }

        public bool CanConnectOrDisconnect(string username)
        {
            //I can disconnect when I am connected
            return IsConnected ||
                //Or when not connected , a user name must be provided
                (!IsConnected && !string.IsNullOrWhiteSpace(username));
        }       
    }
}
