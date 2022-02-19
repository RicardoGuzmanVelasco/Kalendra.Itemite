using System.Collections.Generic;

namespace Kalendra.Itemite.Runtime.Domain.Application
{
    public interface IItemRepo
    {
        IList<Item> GetRandom(int count);
    }
}