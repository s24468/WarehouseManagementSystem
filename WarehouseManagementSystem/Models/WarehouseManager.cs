using System;
using System.Collections.Generic;

public class WarehouseManager
{
    public Dictionary<string, (int Capacity, int CurrentCount)> _storages;

    public WarehouseManager(Dictionary<string, (int Capacity, int CurrentCount)> storages)
    {
        _storages = storages;
    }

    public bool IsStorageValid(string storageName)
    {
        return _storages.ContainsKey(storageName);
    }

    public bool CanAddPackageToStorage(string warehouseName)
    {
        if (!IsStorageValid(warehouseName))
        {
            return false;
        }

        var (capacity, currentCount) = _storages[warehouseName];
        return currentCount < capacity;
    }

    public void AddPackageToWarehouse(string warehouseName)
    {
        if (CanAddPackageToStorage(warehouseName))
        {
            _storages[warehouseName] =
                (_storages[warehouseName].Capacity, _storages[warehouseName].CurrentCount + 1);
        }
    }
}