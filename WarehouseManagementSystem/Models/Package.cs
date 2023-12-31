public class Package
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Storage { get; set; }

    public Package(string name, string description, string storage)
    {
        Name = name;
        Description = description;
        Storage = storage;
    }
}