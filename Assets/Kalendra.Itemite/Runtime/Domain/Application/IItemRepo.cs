using System.Collections.Generic;

namespace Kalendra.Itemite.Runtime.Domain.Application
{
    public interface IItemRepo
    {
        IEnumerable<Item> GetRandom(int count);
    }
}