using System.Windows;
using WarehouseManagementSystem.Models;

public class DescriptionValidationHandler : IPackageHandler
{
    private IPackageHandler _nextHandler;

    public bool Handle(Package package)
    {
        if (string.IsNullOrWhiteSpace(package.Description))
        {
            MessageBox.Show("Description cannot be empty.");
            return false;
        }

        if (package.Description.Length < 5 || package.Description.Length > 200)
        {
            MessageBox.Show("Description must be between 10 and 200 characters.");
            return false;
        }

        return _nextHandler?.Handle(package) ?? true;
    }

    public void SetNext(IPackageHandler handler)
    {
        _nextHandler = handler;
    }
}