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

            sut.Should().HaveCount(1);
            sut.Should().ContainEquivalentOf(new[] { "a" });
        }

        [Test]
        public void Size2()
        {
            var sut = Permutation.Of(new[] { "a", "b" });

            sut.Should().HaveCount(2);
            sut.Should().ContainEquivalentOf(new[] { "a", "b" });
            sut.Should().ContainEquivalentOf(new[] { "b", "a" });
        }

        [Test]
        public void Size3()
        {
            var sut = Permutation.Of(new[] { "a", "b", "c" });

            sut.Should().HaveCount(6);
            sut.Should().ContainEquivalentOf(new[] { "a", "b", "c" });
            sut.Should().ContainEquivalentOf(new[] { "a", "c", "b" });
            sut.Should().ContainEquivalentOf(new[] { "b", "a", "c" });
            sut.Should().ContainEquivalentOf(new[] { "b", "c", "a" });
            sut.Should().ContainEquivalentOf(new[] { "c", "a", "b" });
            sut.Should().ContainEquivalentOf(new[] { "c", "b", "a" });
        }
    }
}