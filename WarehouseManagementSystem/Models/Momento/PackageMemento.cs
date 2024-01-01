using System.Collections.Generic;
using System.Collections.ObjectModel;

public class PackageMemento
{
    public ObservableCollection<Package> PackagesState { get; private set; }
    public Dictionary<string, (int Capacity, int CurrentCount)> WarehouseState { get; private set; }

    public PackageMemento(ObservableCollection<Package> packages, Dictionary<string, (int, int)> warehouse)
    {
        PackagesState = new ObservableCollection<Package>(packages);
        WarehouseState = new Dictionary<string, (int, int)>(warehouse);
    }
}