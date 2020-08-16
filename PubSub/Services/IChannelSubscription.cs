using JetBrains.Annotations;

namespace PubSub.Services
{
    public interface IChannelSubscription
    {
        /// <summary>
        /// Subscribing a new <see cref="ISubscriber"/> to the Channel.
        /// </summary>
        /// <param name="subscriber"></param>
        void Subscribe([NotNull] ISubscriber subscriber);

        /// <summary>
        /// Unsubscribing an <see cref="ISubscriber"/> from the Channel.
        /// </summary>
        /// <param name="subscriber"></param>
        void Unsubscribe([NotNull] ISubscriber subscriber);
    }
}
