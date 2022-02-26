namespace Kalendra.Pokemite.Runtime.Domain
{
    public interface IRelatable<in T>
    {
        float RelateWith(T other);
    }
}