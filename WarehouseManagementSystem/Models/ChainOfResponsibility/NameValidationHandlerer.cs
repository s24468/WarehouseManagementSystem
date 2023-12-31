using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using WarehouseManagementSystem.Models;

public class NameValidationHandler : IPackageHandler
{
    private IPackageHandler _nextHandler;
    public ObservableCollection<Package> Packages { get; set; }

    public NameValidationHandler(ObservableCollection<Package> packages)
    {
        Packages = packages;
    }

    public bool Handle(Package package)
    {
        if (string.IsNullOrWhiteSpace(package.Name))
        {
            MessageBox.Show("Name cannot be empty.");
            return false;
        }

        if (package.Name.Length > 50)
        {
            MessageBox.Show("Name must be less than 50 characters.");
            return false;
        }

        if (!IsNameValid(package.Name))
        {
            MessageBox.Show("Name contains invalid characters.");
            return false;
        }

        if (!IsNameUnique(package.Name))
        {
            MessageBox.Show("Name must be unique.");
            return false;
        }

        return _nextHandler?.Handle(package) ?? true;
    }

    private bool IsNameValid(string name)
    {
        return name.All(char.IsLetterOrDigit);
    }

    private bool IsNameUnique(string name)
    {
        Console.WriteLine(name);
        return Packages.All(p => p.Name != name);
    }

    public void SetNext(IPackageHandler handler)
    {
        _nextHandler = handler;
    }
}