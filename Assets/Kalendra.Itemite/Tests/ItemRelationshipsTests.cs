using System;
using System.Linq;
using FluentAssertions;
using Kalendra.Itemite.Runtime.Domain;
using NUnit.Framework;
using UnityEngine;

namespace Kalendra.Itemite.Tests
{
    public class ItemRelationshipsTests
    {
        #region Fixture
        static Item Tree => new Item("Tree", new[] { "Plant", "Vegetable", "Natural", "Raw materal", "Wooden" });
        static Item Fire => new Item("Fire", new[] { "Natural", "Flammable" });
        static Item Apple => new Item("Apple", new[] { "Natural", "Fruit", "Vegetable", "Food" });
        static Item Paper => new Item("Paper", new[] { "Raw material", "Flammable", "Wooden" });
        static Item Car => new Item("Car", new[] { "Vehicle", "Transport", "Invention", "Tech" });
        #endregion
        
        #region Creation
        [Test]
        public void Item_Creation()
        {
            var sut = new Item("Tree", new[]{ "Plant", "Natural" });

            sut.Name.Should().Be("Tree");
            sut.Tags.Should().BeEquivalentTo("Plant", "Natural");
        }

        [Test]
        public void Item_CannotHave_EmptyTags()
        {
            Action act = () => new Item("Empty", Enumerable.Empty<string>());

            act.Should().Throw<ArgumentException>();
        }
        
        [Test]
        public void Item_CannotHave_NullTags()
        {
            Action act = () => new Item("Empty", null);

            act.Should().Throw<ArgumentException>();
        }

        [Test]
        public void Item_Ignores_RepeatedTags()
        {
            var sut = new Item("Tree", new[] { "Plant", "Plant" });

            sut.Tags.Should().HaveCount(1);
        }
        #endregion

        #region Items relationship
        [Test]
        public void Relating_ItemsWithSomeCommonTag_IsNotZero()
        {
            var doc = new Item("Home", new[]{ "Shelter, Human" });
            var sut = new Item("Tree", new[]{ "Plant", "Natural" });

            sut.RelateWith(doc).Should().BeApproximately(0, float.Epsilon);
        }

        [Test]
        public void Relating_DisjointItems_IsAroundZero()
        {
            var doc = new Item("Fire", new[]{ "Natural" });
            var sut = new Item("Tree", new[]{ "Plant", "Natural" });

            sut.RelateWith(doc).Should().BePositive();
        }
        #endregion

        #region Chaining
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
            var doc1 = new Item("1", new[] { "Tag1"});
            var doc2 = new Item("2", new[] { "Tag2"});
            var sut = new Chain(doc1, doc2);

            var result = sut.Reduce();

            result.Should().Be(0);
        }
        #endregion

        [Test]
        public void Chains_WithDifferentOrders_DiffersAlsoInFormat()
        {
            var sut1 = new Chain(Tree, Fire, Apple, Paper);
            var sut2 = new Chain(Paper, Fire, Tree, Apple);
            
            sut1.ToString().Should().NotBe(sut2.ToString());
        }
    }
}