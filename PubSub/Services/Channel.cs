using System;
using System.Collections.Generic;
using PubSub.Models;

namespace PubSub.Services
{
    public class Channel : IChannelSubscription, IChannelMessageHandling
    {
        protected IList<ISubscriber> Subscribers { get; }
        protected Queue<Message> Messages { get; }
        public EventHandler MessageReceivedEventHandler { get; set; }

        public Channel()
        {
            Subscribers = new List<ISubscriber>();
            Messages = new Queue<Message>();
        }

        public virtual void Subscribe(ISubscriber subscriber)
        {
            if (subscriber == null) throw new ArgumentNullException(nameof(subscriber));

            if (!Subscribers.Contains(subscriber))
                Subscribers.Add(subscriber);
        }

        public virtual void Unsubscribe(ISubscriber subscriber)
        {
            if (subscriber == null) throw new ArgumentNullException(nameof(subscriber));

            Subscribers.Remove(subscriber);
        }

        public virtual void Receive(Message message)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));

            Messages.Enqueue(message);

            MessageReceivedEventHandler?.Invoke(this, EventArgs.Empty);
        }

        public virtual void Broadcast()
        {
            while (Messages.Count != 0)
            {
                var message = Messages.Dequeue();
                foreach (var s in Subscribers)
                {
                    s.Receive(message);
                }
            }
        }
    }
}
