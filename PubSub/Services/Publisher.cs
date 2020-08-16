using System;
using JetBrains.Annotations;
using PubSub.Models;

namespace PubSub.Services
{
    public class Publisher : IPublisher
    {
        public string Name { get; set; }
        protected IChannelMessageHandling Channel { get; set; }

        public Publisher([NotNull] string name, [NotNull] IChannelMessageHandling channel)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Channel = channel ?? throw new ArgumentNullException(nameof(channel));
        }

        public virtual void Post(string message)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));

            var msgObj = new Message(message, Name) { PublishTime = DateTime.Now };
            Channel.Receive(msgObj);
        }
    }
}
