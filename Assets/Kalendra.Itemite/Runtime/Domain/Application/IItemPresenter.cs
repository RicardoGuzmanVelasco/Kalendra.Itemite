namespace Kalendra.Itemite.Runtime.Domain.Application
{
    public interface ISelectable
    {
        bool Selected { get; set; }
    }

    public interface IItemPresenter : ISelectable
    {
        void Inject(Item item);
    }
}