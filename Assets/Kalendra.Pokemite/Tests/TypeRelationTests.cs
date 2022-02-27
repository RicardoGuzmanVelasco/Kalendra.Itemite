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

            var result = sut.Relate(await Bulbasaur, await Squirtle);

            result.Should().Be(0);
        }

        [Test]
        public async Task JustMonotype_ButCommon_IsPositive()
        {
            var sut = new ByTypeRelation();

            var result = sut.Relate(await Squirtle, await Wartortle);

            result.Should().BePositive();
        }

        [Test]
        public async Task TwoCommonTypes_IsBetterThanJustOne()
        {
            var sut = new ByTypeRelation();

            var result = sut.Relate(await Amaura, await Aurorus);

            result.Should().BeGreaterThan(sut.Relate(await Squirtle, await Wartortle));
        }

        [Test]
        public async Task TwoCommonTypes_IsBetterThan_JustOneCommonOfTwoTypes()
        {
            var sut = new ByTypeRelation();

            var result = sut.Relate(await Amaura, await Aurorus);

            result.Should().BeGreaterThan(sut.Relate(await Amaura, await Graveler));
        }

        [Test]
        public async Task JustOneTypeButCommon_IsBetterThan_DualTypeButOneCommon()
        {
            var sut = new ByTypeRelation();

            var result = sut.Relate(await Squirtle, await Wartortle);

            result.Should().BeGreaterThan(sut.Relate(await Amaura, await Graveler));
        }
    }
}