using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat
{
    public class ChatMessage
    {

        public ChatMessage()
        {
            Timestamp = DateTime.Now;
        }

        public DateTime Timestamp { get; set; }
        public string Sender { get; set; }
        public string Message { get; set; }
        public Guid SenderId { get; set; }
        public override string ToString()
        {
            return $"{Timestamp:T}\t{Sender.ToUpper()}: {Message}";
        }
    }
}
