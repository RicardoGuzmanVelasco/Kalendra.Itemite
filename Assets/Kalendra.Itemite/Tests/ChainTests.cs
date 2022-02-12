using System;
using FluentAssertions;
using Kalendra.Itemite.Runtime.Domain;
using NUnit.Framework;
using static Kalendra.Itemite.Tests.TestApi;

namespace Kalendra.Itemite.Tests
{
    public class ChainTests
    {
        [Test]
        public void Item_CannotChain_WithItself()
        {
            var sut = new Item("A", new[] { "1, 2, 3" });

            Action act = () => sut.ChainWith(sut);

            act.Should().Throw<ArgumentException>();
        }

        [Test]
        public void SimmilarItems_CannotChain()
        {
            var sut1 = new Item("A", new[] { "1" });
            var sut2 = new Item("A", new[] { "1" });

            Action act = () => sut1.ChainWith(sut2);

            act.Should().Throw<ArgumentException>();
        }

        [Test]
        public void Chain_AddsBothItems()
        {
            var doc = new Item("Doc", new[] { "1" });
            var sut = new Item("Sut", new[] { "2" });

            var result = sut.ChainWith(doc);

            result.Should().BeEquivalentTo(new Chain(sut, doc));
        }

        [Test]
        public void Chain_ThroughFluentAPI()
        {
            var doc1 = new Item("Doc1", new[] { "1a" });
            var doc2 = new Item("Doc2", new[] { "1b" });
            var sut = new Item("Sut", new[] { "2" });

            var result = sut.ChainWith(doc1).ChainWith(doc2);

            result.Should().BeEquivalentTo(new Chain(sut, doc1, doc2));
        }

        [Test]
        public void ReduceChain_WithSomeCommonTags_IsNotZero()
        {
            var doc1 = new Item("1", new[] { "Same", "NotSame" });
            var doc2 = new Item("2", new[] { "Same", "NoSame" });
            var sut = new Chain(doc1, doc2);

            var result = sut.Reduce();

            result.Should().BePositive();
        }

        [Test]
        public void ReduceChain_WithoutCommonTags_IsZero()
        {
            var doc1 = new Item("1", new[] { "Tag1" });
            var doc2 = new Item("2", new[] { "Tag2" });
            var sut = new Chain(doc1, doc2);

            var result = sut.Reduce();

            result.Should().Be(0);
        }

        [Test]
        public void Chains_WithDifferentOrders_DiffersAlsoInFormat()
        {
            var sut1 = new Chain(Tree, Fire, Apple, Paper);
            var sut2 = new Chain(Paper, Fire, Tree, Apple);

            sut1.ToString().Should().NotBe(sut2.ToString());
        }
    }
}