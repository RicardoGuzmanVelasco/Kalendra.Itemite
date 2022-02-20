namespace Kalendra.Pokemite.Domain
{
    public interface IRelatable<in T>
    {
        float RelateWith(T other);
    }
}