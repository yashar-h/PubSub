using System;
using Moq;
using NUnit.Framework;
using PubSub.Models;
using PubSub.Services;

namespace PubSubTests
{
    public class PublisherTest
    {
        [Test]
        public void WhenPostCalled_ThenChannelReceivesTheMessage()
        {
            var messageContent = "My message";
            var channelMock = new Mock<IChannelMessageHandling>();
            channelMock.Setup(c => c.Receive(It.IsAny<Message>()));
            var sut = new Publisher("SUT", channelMock.Object);

            sut.Post(messageContent);

            channelMock.Verify(c =>
                c.Receive(It.Is<Message>(m => m.Content.Equals(messageContent))), Times.Once);
        }

        [Test]
        public void WhenPostCalled_ThenPublishTimeIsSetOnMessage()
        {
            var channelMock = new Mock<IChannelMessageHandling>();
            channelMock.Setup(c => c.Receive(It.IsAny<Message>()));
            var sut = new Publisher("SUT", channelMock.Object);

            sut.Post("some message");

            channelMock.Verify(c =>
                c.Receive(It.Is<Message>(m => m.PublishTime != DateTime.MinValue)));
        }

        [Test]
        public void WhenCalledWithNullMessage_ThenPostThrows()
        {
            var channelMock = new Mock<IChannelMessageHandling>();
            channelMock.Setup(c => c.Receive(It.IsAny<Message>()));
            var sut = new Publisher("SUT", channelMock.Object);

            Assert.Throws<ArgumentNullException>(() => sut.Post(null));
        }
    }
}
