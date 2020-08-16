using System;
using Moq;
using NUnit.Framework;
using PubSub.Models;
using PubSub.Services;

namespace PubSubTests
{
    public class ChannelTest
    {
        [Test]
        public void WhenSubscribed_ThenSubscriberReceivesMessages()
        {
            var subscriberMock = new Mock<ISubscriber>();
            subscriberMock.Setup(c => c.Receive(It.IsAny<Message>()));
            var sut = new Channel();
            sut.Subscribe(subscriberMock.Object);
            sut.Receive(new Message("content", "sender"));

            sut.Broadcast();

            subscriberMock.Verify(c =>
                c.Receive(It.IsAny<Message>()), Times.Once);
        }

        [Test]
        public void WhenUnSubscribed_ThenSubscriberNoLongerReceivesMessages()
        {
            var subscriberMock = new Mock<ISubscriber>();
            subscriberMock.Setup(c => c.Receive(It.IsAny<Message>()));
            var sut = new Channel();
            sut.Subscribe(subscriberMock.Object);

            //Calling BroadCast twice, once before and once after unsubscribed
            sut.Receive(new Message("Should receive", "sender"));
            sut.Broadcast();
            
            sut.Unsubscribe(subscriberMock.Object);
            
            sut.Receive(new Message("Should not receive", "sender"));
            sut.Broadcast();

            //Expecting to only receive the one before
            subscriberMock.Verify(c =>
                c.Receive(It.Is<Message>(m=>m.Content.Equals("Should receive"))), Times.Once);
            subscriberMock.Verify(c =>
                c.Receive(It.Is<Message>(m => m.Content.Equals("Should not receive"))), Times.Never);
        }

        [Test]
        public void WhenMoreThanOneSubscribed_ThenAllSubscribersReceivesMessage()
        {
            var subscriber1Mock = new Mock<ISubscriber>();
            subscriber1Mock.Setup(c => c.Receive(It.IsAny<Message>()));
            var subscriber2Mock = new Mock<ISubscriber>();
            subscriber2Mock.Setup(c => c.Receive(It.IsAny<Message>()));
            var sut = new Channel();
            sut.Subscribe(subscriber1Mock.Object);
            sut.Subscribe(subscriber2Mock.Object);
            sut.Receive(new Message("content", "sender"));

            sut.Broadcast();

            subscriber1Mock.Verify(c =>
                c.Receive(It.IsAny<Message>()), Times.Once);
            subscriber2Mock.Verify(c =>
                c.Receive(It.IsAny<Message>()), Times.Once);
        }

        [Test]
        public void WhenMoreThanOneMessageExists_ThenSubscriberReceivesAllMessages()
        {
            var subscriberMock = new Mock<ISubscriber>();
            subscriberMock.Setup(c => c.Receive(It.IsAny<Message>()));
            var sut = new Channel();
            sut.Subscribe(subscriberMock.Object);
            sut.Receive(new Message("message 1", "sender"));
            sut.Receive(new Message("message 2", "sender"));

            sut.Broadcast();

            subscriberMock.Verify(c =>
                c.Receive(It.IsAny<Message>()), Times.Exactly(2));
            subscriberMock.Verify(c =>
                c.Receive(It.Is<Message>(m => m.Content.Equals("message 1"))), Times.Once);
            subscriberMock.Verify(c =>
                c.Receive(It.Is<Message>(m => m.Content.Equals("message 2"))), Times.Once);
        }

        [Test]
        public void WhenCalledWithNullSubscriber_ThenSubscribeThrows()
        {
            var sut = new Channel();

            Assert.Throws<ArgumentNullException>(() => sut.Subscribe(null));
        }

        [Test]
        public void WhenCalledWithNullSubscriber_ThenUnsubscribeThrows()
        {
            var sut = new Channel();

            Assert.Throws<ArgumentNullException>(() => sut.Unsubscribe(null));
        }

        [Test]
        public void WhenCalledWithNullMessage_ThenReceiveThrows()
        {
            var sut = new Channel();

            Assert.Throws<ArgumentNullException>(() => sut.Receive(null));
        }
    }
}
