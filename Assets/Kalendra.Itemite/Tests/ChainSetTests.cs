using System.Linq;
using FluentAssertions;
using Kalendra.Itemite.Runtime.Domain;
using NUnit.Framework;
using static Kalendra.Itemite.Tests.TestApi;

namespace Kalendra.Itemite.Tests
{
    public class ChainSetTests
    {
        [Test]
        public void GivenSomeItems_ThereIs_BestChain()
        {
            var sut = new ChainSet(new Chain(SomeItems));

            var result = sut.BestChain();

            result.Should().NotBeNullOrEmpty();
        }

        [Test]
        public void GivenSomeItems_BestChain_ReducesToPositiveNumber()
        {
            var sut = new ChainSet(new Chain(SomeItems));

            var result = sut.BestChain();

            result.Reduce().Should().BePositive();
        }

        [Test]
        public void BestChain_IsFirst_InOrderedChains()
        {
            var sut = new ChainSet(new Chain(SomeItems));

            sut.BestChain().Reduce().Should().Be(sut.OrderChains().First().Reduce());
        }

        [Test]
        public void DifferentStrategies_CreateDifferentBestChains()
        {
            Item.RelationStrategy = Item.FirstItemDeviation;
            var sut1 = new ChainSet(new Chain(SomeItems)).BestChain();
            Item.RelationStrategy = Item.CommonProportion;
            var sut2 = new ChainSet(new Chain(SomeItems)).BestChain();

            sut1.Should().NotBeEquivalentTo(sut2);
        }
    }
}