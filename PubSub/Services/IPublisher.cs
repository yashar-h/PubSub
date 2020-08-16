using JetBrains.Annotations;

namespace PubSub.Services
{
    public interface IPublisher
    {
        string Name { get; }

        /// <summary>
        /// Posts a <see cref="Models.Message"/> to <see cref="IChannelMessageHandling"/>
        /// </summary>
        /// <param name="message"></param>
        void Post([NotNull] string message);
    }
}
