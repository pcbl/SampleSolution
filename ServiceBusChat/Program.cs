using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;

namespace ServiceBusChat
{
    class Program
    {
        //Topic client, will be used to send messages to the chat!
        private static TopicClient _chatClient;
        //User Name
        private static string _userName;
        //Unique Sender ID!
        private static Guid _senderId;

        static  void Main(string[] args)
        {
            //Service Bus/Namespace connectionstring
            var connectionString = "Endpoint=sb://pcblbus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=QpRohfDTibGF6P3Fki/tOUxqlTC76XfADt4TKcZQ8pI=";
            //We will be using this topic to handle the chat conversations
            var topicName = "chat_history";
            //This Id will be used by this client to known when a message belongs to himself!
            _senderId = Guid.NewGuid();

            //let´s get the UserName
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Type your Name: ");
            _userName = Console.ReadLine();

            //Every Chat member will have an subscription to get copies of messages sent to the chat!
            var subscriptionId = $"client_{Guid.NewGuid().ToString()}";
            //To do that, let´s create the subscription
            var busClient = NamespaceManager.CreateFromConnectionString(connectionString);
            var subscription = busClient.CreateSubscription(topicName, subscriptionId);
            //The subscription will delete itself when iddle more than 5 minutes!
            //That will be used case the application crashes
            subscription.AutoDeleteOnIdle = new TimeSpan(0,5,0);
            
            //Now we will use the subscription we just created and will create a Subscription Client
            //The idea is to print all messages that we receive!
            var chatSubscription = SubscriptionClient.CreateFromConnectionString(connectionString, topicName,subscriptionId);
            chatSubscription.OnMessage((message) =>
            {
                var fullMessage = message.GetBody<ChatMessage>();
                //Print own messages in Dar gray, external as yellow
                Console.ForegroundColor = fullMessage.SenderId.Equals(_senderId) ? ConsoleColor.DarkGray : ConsoleColor.Yellow;
                Console.WriteLine(fullMessage);
                //Revert to white to avoid that the user input is on the just printed collor
                Console.ForegroundColor = ConsoleColor.White;
            });

            //Now that we have a Subscription, let´s sending a message to announce we joined the chat!            
            var chatMessage = new ChatMessage() { Sender = _userName, Message = "**just joined**", SenderId = _senderId };
            var queueMessage = new BrokeredMessage(chatMessage);

            //To send messages, we need a Topic Client!!
            _chatClient = TopicClient.CreateFromConnectionString(connectionString, topicName);

            //Now We can send the Join message!
            _chatClient.Send(queueMessage);

            //Let´s get into the Input Look
            InputLoopProcessor();          
        }

        private static async Task InputLoopProcessor()
        {
            var message = "";

            while (string.Compare(message, "exit", StringComparison.InvariantCultureIgnoreCase) != 0)
            {
                Console.ForegroundColor = ConsoleColor.White;
                message = Console.ReadLine();
                var chatMessage = new ChatMessage() { Sender = _userName, Message = message, SenderId = _senderId };
                var queueMessage = new BrokeredMessage(chatMessage);
                _chatClient.Send(queueMessage);
            }

        }
    }
}
