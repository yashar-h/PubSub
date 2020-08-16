using PubSub.Models;
using System;
using JetBrains.Annotations;

namespace PubSub.Services
{
    public interface IChannelMessageHandling
    {
        /// <summary>
        /// Receives a <see cref="Message"/> from <see cref="IPublisher"/>
        /// </summary>
        /// <param name="message"></param>
        void Receive([NotNull] Message message);

        /// <summary>
        /// Broadcasts all the received <see cref="Message"/>(s) to the subscribed <see cref="ISubscriber"/>(s) since Broadcast was last called.
        /// </summary>
        void Broadcast();

        /// <summary>
        /// Handles the event of receiving a new <see cref="Message"/> from <see cref="IPublisher"/>
        /// </summary>
        EventHandler MessageReceivedEventHandler { get; set; }
    }
}
