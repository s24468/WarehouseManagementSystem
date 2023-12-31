using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using WarehouseManagementSystem.Models;

public class StorageValidationHandler : IPackageHandler
{
    private IPackageHandler _nextHandler;
    public readonly WarehouseManager _warehouseManager;

    public StorageValidationHandler(WarehouseManager warehouseManager)
    {
        _warehouseManager = warehouseManager;
    }

    public bool Handle(Package package)
    {
        string x = package.Storage;
        if (!_warehouseManager.IsStorageValid(x))
        {
            MessageBox.Show("The specified storage does not match any known warehouse.");
            return false;
        }
        
        if (!_warehouseManager.CanAddPackageToStorage(package.Storage))
        {
            MessageBox.Show("The specified storage is full and cannot accommodate more packages.");
            return false;
        }

        return _nextHandler?.Handle(package) ?? true;
    }

    public void SetNext(IPackageHandler handler)
    {
        _nextHandler = handler;
    }
}