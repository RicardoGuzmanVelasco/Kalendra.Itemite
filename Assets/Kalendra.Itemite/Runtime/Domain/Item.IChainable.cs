namespace Kalendra.Itemite.Runtime.Domain
{
        public interface IChainable
        {
            Item.Chain ChainWith(Item item);
        }
}