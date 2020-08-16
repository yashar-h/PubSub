using System;
using NUnit.Framework;
using PubSub.Models;
using PubSub.Services;

namespace PubSubTests
{
    public class SubscriberTest
    {
        [Test]
        public void WhenMessageReceived_ThenEventIsInvoked()
        {
            var eventRaised = false;
            var sut = new Subscriber("SUT", (m) => { });
            sut.MessageReceivedEventHandler += (sender, args) => { eventRaised = true; };

            sut.Receive(new Message("content", "sender"));

            Assert.IsTrue(eventRaised);
        }

        [Test]
        public void WhenMessageReceived_ThenDeliveryTimeIsSet()
        {
            var sut = new Subscriber("SUT", (m) => { });
            var message = new Message("content", "sender");

            sut.Receive(message);

            Assert.AreNotEqual(message.DeliveryTime, DateTime.MinValue);
        }

        [Test]
        public void WhenMessageIsNull_ThenReceiveMethodThrows()
        {
            var sut = new Subscriber("SUT", (m) => { });

            Assert.Throws<ArgumentNullException>(() => sut.Receive(null));
        }

        [Test]
        public void MessageProcessActionIsCalledByTheNumberOfMessagesInQueue()
        {
            var messagesProcessed = 0;
            var sut = new Subscriber("SUT", (m) => { messagesProcessed++; });
            sut.Receive(new Message("content", "sender"));
            sut.Receive(new Message("content", "sender"));
            sut.Receive(new Message("content", "sender"));

            sut.ProcessMessages();

            Assert.AreEqual(3, messagesProcessed);
        }
    }
}