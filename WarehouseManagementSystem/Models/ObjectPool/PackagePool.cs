using System.Collections.Generic;

public class PackagePool
{
    public readonly List<Package> available = new List<Package>();
    public readonly List<Package> inUse = new List<Package>();

    public Package GetPackage()
    {
        Package package;
        if (available.Count == 0)
        {
            package = new Package();
        }
        else
        {
            package = available[0];
            available.RemoveAt(0);
        }
    
        package.Reset();

        inUse.Add(package);
        return package;
    }

    public void ReleasePackage(Package package)
    {
        inUse.Remove(package);
        available.Add(package);
    }
}