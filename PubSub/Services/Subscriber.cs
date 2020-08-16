using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using PubSub.Models;

namespace PubSub.Services
{
    public class Subscriber : ISubscriber
    {
        public string Name { get; }

        /// <summary>
        /// Messages received by the subscriber and not yet processed.
        /// </summary>
        protected Queue<Message> PendingMessages { get; } = new Queue<Message>();
        protected Action<Message> MessageProcessAction { get; }
        public EventHandler MessageReceivedEventHandler { get; set; }

        public Subscriber([NotNull] string name, [NotNull] Action<Message> messageProcess)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            MessageProcessAction = messageProcess ?? throw new ArgumentNullException(nameof(messageProcess));
        }

        public virtual void Receive(Message message)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));

            message.DeliveryTime = DateTime.Now;
            PendingMessages.Enqueue(message);

            MessageReceivedEventHandler?.Invoke(this, EventArgs.Empty);
        }

        public virtual void ProcessMessages()
        {
            while (PendingMessages.Count != 0)
            {
                var message = PendingMessages.Dequeue();
                MessageProcessAction(message);
            }
        }
    }
}
