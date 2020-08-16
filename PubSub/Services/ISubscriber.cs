using PubSub.Models;
using System;
using JetBrains.Annotations;

namespace PubSub.Services
{
    public interface ISubscriber
    {
        string Name { get; }

        /// <summary>
        /// Receives a <see cref="Message"/> from the subscribed <see cref="IChannelMessageHandling"/>
        /// </summary>
        /// <param name="message"></param>
        void Receive([NotNull] Message message);

        /// <summary>
        /// Processing all the <see cref="Message"/>(s) received since ProcessMessages was last called.
        /// </summary>
        void ProcessMessages();

        /// <summary>
        /// Handles the event of receiving a new <see cref="Message"/> from <see cref="IChannelMessageHandling"/>
        /// </summary>
        EventHandler MessageReceivedEventHandler { get; set; }
    }
}
