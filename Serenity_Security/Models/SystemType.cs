namespace Serenity_Security.Models;

public class SystemType
{
    public int Id { get; set; }
    public string Name { get; set; }

    public List<Asset> Assets { get; set; }
}
