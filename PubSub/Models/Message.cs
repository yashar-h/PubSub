using System;
using JetBrains.Annotations;

namespace PubSub.Models
{
    public class Message
    {
        public Message([NotNull] string content, [NotNull] string sender)
        {
            Content = content ?? throw new ArgumentNullException(nameof(content));
            Sender = sender ?? throw new ArgumentNullException(nameof(sender));
            CreationTime = DateTime.Now;
        }

        public DateTime CreationTime { get; }
        public DateTime PublishTime { get; internal set; }
        public DateTime DeliveryTime { get; internal set; }

        public string Content { get; set; }
        public string Sender { get; set; }
    }
}
