using System;
using System.Linq;
using FluentAssertions;
using Kalendra.Itemite.Runtime.Domain;
using NUnit.Framework;

namespace Kalendra.Itemite.Tests
{
    public class ItemRelationshipsTests
    {
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
    }
}