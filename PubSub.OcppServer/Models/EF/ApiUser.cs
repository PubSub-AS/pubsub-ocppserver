using Microsoft.AspNetCore.Components.Web;

namespace PubSub.OcppServer.Models.EF;

public class ApiUser : User
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string? RefreshToken { get; set; }
    public DateTimeOffset? TokenExpiry { get; set; }
    public bool IsAdmin { get; set; }
    public ICollection<IdTag> IdTags { get; set; }
    public ICollection<ChargingPoint> ChargingPoints { get; set; }
    public ICollection<Facility> Facilities { get; set; }
}