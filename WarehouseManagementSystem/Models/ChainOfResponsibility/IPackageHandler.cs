namespace WarehouseManagementSystem.Models
{
    public interface IPackageHandler
    {
        bool Handle(Package package);
        void SetNext(IPackageHandler handler);
    }
}