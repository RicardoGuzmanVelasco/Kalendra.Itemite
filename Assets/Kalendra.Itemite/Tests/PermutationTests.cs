using System.Linq;
using FluentAssertions;
using Kalendra.Itemite.Runtime.Domain;
using NUnit.Framework;

namespace Kalendra.Itemite.Tests
{
    public class PermutationTests
    {
        [Test]
        public void Size1()
        {
            var sut = Permutation.Of(new[] { "a" });

            sut.Should().ContainEquivalentOf(new[] { "a" });
        }

        [Test]
        public void Size2()
        {
            var sut = Permutation.Of(new[] { "a", "b" });

            sut.Should().ContainEquivalentOf(new[] { "a", "b" });
            sut.Should().ContainEquivalentOf(new[] { "b", "a" });
        }

        [Test]
        public void Size3()
        {
            var sut = Permutation.Of(new[] { "a", "b", "c" });

            sut.Should().ContainEquivalentOf(new[] { "a", "b", "c" });
            sut.Should().ContainEquivalentOf(new[] { "a", "c", "b" });
            sut.Should().ContainEquivalentOf(new[] { "b", "a", "c" });
            sut.Should().ContainEquivalentOf(new[] { "b", "c", "a" });
            sut.Should().ContainEquivalentOf(new[] { "c", "a", "b" });
            sut.Should().ContainEquivalentOf(new[] { "c", "b", "a" });
        }

        [TestCase(2)]
        [TestCase(3)]
        [TestCase(5)]
        [TestCase(6)]
        public void SizeNLength(int size)
        {
            var sut = Permutation.Of(Enumerable.Range(0, size).ToList());

            sut.Should().HaveCount(size.Fact());
        }
    }
}