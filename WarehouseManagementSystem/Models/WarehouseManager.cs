using System;
using System.Collections.Generic;

public class WarehouseManager
{
    public readonly Dictionary<string, (int Capacity, int CurrentCount)> _storages =
        new Dictionary<string, (int, int)>
        {
            { "X", (1, 0) }, // (maksymalna pojemność, aktualna liczba paczek)
            { "Y", (2, 0) },
        };

    public WarehouseManager()
    {
    }

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
        Console.WriteLine(capacity + " " + currentCount);
        return currentCount < capacity;
    }

    public void AddPackageToWarehouse(string warehouseName)
    {
        if (CanAddPackageToStorage(warehouseName))
        {
            Console.WriteLine(_storages[warehouseName].CurrentCount + 1);
            _storages[warehouseName] =
                (_storages[warehouseName].Capacity, _storages[warehouseName].CurrentCount + 1);
        }
    }
}