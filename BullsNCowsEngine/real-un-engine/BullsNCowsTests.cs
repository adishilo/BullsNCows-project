using System;
using Xunit;
using BullsNCowsEngine.RealUnEngine;

namespace BullsNCowsEngine
{
    public class BullsNCowsTests
    {
        [Fact]
        public void WhenAllMatchExactly_4Bulls0Cows()
        {
            // Arrange & Act:
            var trial = new BullsNCows("1234", "1234");

            // Assert:
            Assert.Equal(4, trial.Bulls);
            Assert.Equal(0, trial.Cows);
        }

        [Fact]
        public void WhenNoneMatch_0Bulls0Cows()
        {
            // Arrange & Act:
            var trial = new BullsNCows("2345", "6987");

            // Assert:
            Assert.Equal(0, trial.Bulls);
            Assert.Equal(0, trial.Cows);
        }

        [Fact]
        public void WhenAllExistButNoneMatch_0Bulls4Cows()
        {
            // Arrange & Act:
            var trial = new BullsNCows("7654", "4567");

            // Assert:
            Assert.Equal(0, trial.Bulls);
            Assert.Equal(4, trial.Cows);
        }

        [Fact]
        public void WhenSomeMatchSomeExist_BullsNCows()
        {
            // Arrange & Act:
            var trial = new BullsNCows("4857", "4750");

            // Assert:
            Assert.Equal(2, trial.Bulls);
            Assert.Equal(1, trial.Cows);
        }
    }
}
