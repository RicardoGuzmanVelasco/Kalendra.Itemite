using System;

namespace Kalendra.Itemite.Runtime.Domain.Application
{
    public interface IItemInput
    {
        event Action<Item> Clicked;
    }
}