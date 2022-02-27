using System.Threading.Tasks;
using FluentAssertions;
using Kalendra.Pokemite.Runtime.Domain;
using NUnit.Framework;
using static Kalendra.Pokemite.Tests.TestApi;

namespace Kalendra.Pokemite.Tests
{
    public class TypeRelationTests
    {
        [Test]
        public async Task NoCommonTypes_IsZero()
        {
            var sut = new ByTypeRelation();

            var result = sut.Relate(await For("bulbasaur"), await For("charmander"));

            result.Should().Be(0);
        }

        [Test]
        public async Task JustOneType_WhenIsCommon_IsPositive()
        {
            var sut = new ByTypeRelation();

            var result = sut.Relate(await For("bulbasaur"), await For("chikorita"));

            result.Should().BePositive();
        }

        [Test]
        public async Task TwoCommonTypes_IsBetterThanJustOne()
        {
            var sut = new ByTypeRelation();

            var result = sut.Relate(await For("amaura"), await For("aurorus"));

            result.Should().BeGreaterThan(sut.Relate(await For("bulbasaur"), await For("chikorita")));
        }

        [Test]
        public async Task TwoCommonTypes_IsBetterThan_JustOneCommonOfTwoTypes()
        {
            var sut = new ByTypeRelation();

            var result = sut.Relate(await For("amaura"), await For("aurorus"));

            result.Should().BeGreaterThan(sut.Relate(await For("amaura"), await For("graveler")));
        }
    }
}