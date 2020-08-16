using System;
using PubSub.Services;
using PubSubUsage.IO;

namespace PubSubUsage
{
    class Program
    {
        static void Main(string[] args)
        {
            IInputOutput io = new ConsoleInputOutput();

            #region Configure Subscribers

            ISubscriber customerBob = new Subscriber("Customer Bob",
                (m) => io.Write($"Customer Bob received message: {m.Content}"));
            customerBob.MessageReceivedEventHandler += MessageReceivedBySubscriber;

            ISubscriber customerAlice = new Subscriber("Customer Alice",
                (m) => io.Write($"Customer Alice received message: {m.Content}"));
            customerAlice.MessageReceivedEventHandler += MessageReceivedBySubscriber;

            #endregion

            #region Configure Channel

            var channel = new Channel();
            channel.MessageReceivedEventHandler += MessageReceivedByChannel;

            channel.Subscribe(customerBob);
            channel.Subscribe(customerAlice);

            #endregion

            #region Configure Publisher

            IPublisher chefJohn = new Publisher("Chef John", channel);

            #endregion

            while (true)
            {
                io.Write("Which food is ready?");
                var input = io.Read();

                if (string.IsNullOrEmpty(input)) continue;

                if (input.Equals("q", StringComparison.InvariantCultureIgnoreCase))
                {
                    break;
                }

                chefJohn.Post($"{chefJohn.Name} says {input} is ready for delivery.");

                io.Write(new string('_', 20));
            }

            channel.Subscribe(customerBob);
            channel.Subscribe(customerAlice);
        }

        static void MessageReceivedByChannel(object sender, EventArgs e)
        {
            if (sender is IChannelMessageHandling channel)
                channel.Broadcast();
        }

        static void MessageReceivedBySubscriber(object sender, EventArgs e)
        {
            if (sender is ISubscriber sub)
                sub.ProcessMessages();
        }
    }
}
