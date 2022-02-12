using FluentAssertions;
using Kalendra.Itemite.Runtime.Domain;
using NUnit.Framework;

namespace Kalendra.Itemite.Tests
{
    public class FactorialTests
    {
        [TestCase(0, 1)]
        [TestCase(1, 1)]
        [TestCase(2, 2)]
        [TestCase(3, 6)]
        [TestCase(4, 24)]
        [TestCase(5, 120)]
        public void _(int doc, int expected)
        {
            Factorial.Of(doc).Should().Be(expected);
        }
    }
}