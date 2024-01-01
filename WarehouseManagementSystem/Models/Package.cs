public class Package
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Storage { get; set; }

    public Package()
    {
        Reset();
    }

    public void Reset()
    {
        Name = null;
        Description = null;
        Storage = null;
    }
}