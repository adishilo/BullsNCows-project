using System;
using Xunit;

namespace BullsNCowsEngine.RealUnEngine
{
    public class SherlockTests
    {
        [Fact]
        public void WhenNumberLegal_AcceptNumber()
        {
            // Arrange, Act & Assert:
            Assert.True(Sherlock.IsLegalNumber("1234"));
        }

        [Fact]
        public void WhenLengthTooBig_NotAcceptNumber()
        {
            // Arrange, Act & Assert:
            Assert.False(Sherlock.IsLegalNumber("12345"));
        }

        [Fact]
        public void WhenLengthTooSmall_NotAcceptNumber()
        {
            // Arrange, Act & Assert:
            Assert.False(Sherlock.IsLegalNumber("123"));
        }

        [Fact]
        public void WhenThereIsADoubleNumber_NotAcceptNumber()
        {
            // Arrange, Act & Assert:
            Assert.False(Sherlock.IsLegalNumber("1223"));
        }
    }
}