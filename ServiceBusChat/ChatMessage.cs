using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBusChat
{
    public class ChatMessage
    {
        public string Sender { get; set; }
        public string Message { get; set; }
        public Guid SenderId { get; set; }
        public override string ToString()
        {
            return $"{Sender.ToUpper()}: {Message}";
        }
    }
}
