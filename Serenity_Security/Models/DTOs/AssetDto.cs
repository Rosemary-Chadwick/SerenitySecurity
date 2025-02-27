namespace Serenity_Security.Models.DTOs;

public class AssetDto
{
    public int Id { get; set; }
    public string SystemName { get; set; }
    public string IpAddress { get; set; }
    public string OsVersion { get; set; }
    public string SystemTypeName { get; set; }
    public bool IsActive { get; set; }
}
