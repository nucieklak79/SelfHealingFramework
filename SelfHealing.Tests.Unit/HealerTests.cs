using Moq;
using NUnit.Framework;
using SelfHealing.Core;
using SelfHealing.Core.LLM;

namespace SelfHealing.Tests.Unit
{
    [TestFixture]
    public class HealerTests
    {
        [Test]
        public void HealLocator_ShouldReturnNewLocator_WhenLlmProvidesSuggestion()
        {
            // Arrange
            var mockLlm = new Mock<ILlmClient>();
            mockLlm.Setup(x => x.SuggestNewLocator(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                   .Returns("//*[@id='repaired-id']");
                   
            var healer = new Healer(mockLlm.Object);

            // Act
            string result = healer.HealLocator("By.Id('broken-id')", "Login button", "<html>...</html>");

            // Assert
            Assert.That(result, Is.EqualTo("//*[@id='repaired-id']"));
        }
    }
}