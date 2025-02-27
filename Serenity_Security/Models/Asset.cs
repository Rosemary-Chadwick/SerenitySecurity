namespace Serenity_Security.Models;

public class Asset
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public UserProfile User { get; set; }
    public string SystemName { get; set; }
    public string IpAddress { get; set; }
    public string OsVersion { get; set; }
    public int SystemTypeId { get; set; }
    public SystemType SystemType { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<Report> Reports { get; set; }
}
