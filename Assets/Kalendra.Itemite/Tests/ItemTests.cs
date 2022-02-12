using System;
using System.Collections.Generic;
using FluentAssertions;
using Kalendra.Itemite.Runtime.Domain;
using NUnit.Framework;

namespace Kalendra.Itemite.Tests
{
    public class ItemTests
    {
        #region Creation
        [Test]
        public void Item_Creation()
        {
            var sut = new Item("Tree", "Plant, Natural");

            sut.Name.Should().Be("Tree");
            sut.Tags.Should().BeEquivalentTo("Plant", "Natural");
        }

        [Test]
        public void Item_CannotHave_EmptyTags()
        {
            Action act = () => new Item("Empty", new List<string>());

            act.Should().Throw<ArgumentException>();
        }

        [Test]
        public void Item_CannotHave_NullTags()
        {
            string nullTags = null;
            Action act = () => new Item("Empty", nullTags);

            act.Should().Throw<ArgumentException>();
        }

        [Test]
        public void Item_Ignores_RepeatedTags()
        {
            var sut = new Item("Tree", "Plant, Plant");

            sut.Tags.Should().HaveCount(1);
        }
        #endregion

        #region Items relationship
        [Test]
        public void Relating_ItemsWithSomeCommonTag_IsNotZero()
        {
            var doc = new Item("Home", "Shelter, Human");
            var sut = new Item("Tree", "Plant, Natural");

            sut.RelateWith(doc).Should().BeApproximately(0, float.Epsilon);
        }

        [Test]
        public void Relating_DisjointItems_IsAroundZero()
        {
            var doc = new Item("Fire", "Natural");
            var sut = new Item("Tree", "Plant, Natural");

            sut.RelateWith(doc).Should().BePositive();
        }

        [Test]
        public void ItemsEquality_IsTagsOrderIndependent()
        {
            var sut1 = new Item("", "1, 2");
            var sut2 = new Item("", "2, 1");

            sut1.Should().BeEquivalentTo(sut2);
        }
        #endregion
    }
}