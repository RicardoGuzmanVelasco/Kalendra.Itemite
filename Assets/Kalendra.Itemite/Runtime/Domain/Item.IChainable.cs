namespace Kalendra.Itemite.Runtime.Domain
{
        public interface IChainable
        {
            Chain ChainWith(Item item);
        }
}