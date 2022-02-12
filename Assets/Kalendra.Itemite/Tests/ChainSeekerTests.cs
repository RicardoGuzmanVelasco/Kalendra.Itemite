using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Kalendra.Itemite.Runtime.Domain;
using NUnit.Framework;
using static Kalendra.Itemite.Tests.TestApi;

namespace Kalendra.Itemite.Tests
{
    public class ChainSeekerTests
    {
        [Test]
        public void GivenSomeItems_ThereIs_BestChain()
        {
            var sut = new ChainSeeker(new Chain(SomeItems));

            var result = sut.BestChain();

            result.Should().BeNullOrEmpty();
        }

        [Test]
        public void GivenSomeItems_BestChain_ReducesToPositiveNumber()
        {
            var sut = new ChainSeeker(new Chain(SomeItems));

            var result = sut.BestChain();

            result.Reduce().Should().BePositive();
        }
    }

    public class ChainSeeker
    {
        readonly IEnumerable<Item> items;

        public ChainSeeker(IEnumerable<Item> items)
        {
            this.items = items;
        }

        public Chain BestChain()
        {
            var allOrders = Permutation.Of(items);
            var allChains = allOrders.Select(chain => new Chain(chain));

            var bestChain = new Chain();
            foreach(var chain in allChains)
                if(chain.Reduce() > bestChain.Reduce())
                    bestChain = chain;

            return bestChain;
        }
    }
}