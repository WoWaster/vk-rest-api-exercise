namespace VkLibrary;
using Newtonsoft.Json;
using System.Text;

public class VkUser
{
    public VkUser(int id, string firstName, string lastName, string? deactivated, bool isClosed, bool canAccessClosed, string? bdate)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Deactivated = deactivated;
        IsClosed = isClosed;
        CanAccessClosed = canAccessClosed;
        Bdate = bdate;
    }
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("first_name")]
    public string FirstName { get; set; }

    [JsonProperty("last_name")]
    public string LastName { get; set; }

    [JsonProperty("deactivated")]
    public string? Deactivated { get; set; }

    [JsonProperty("is_closed")]
    public bool IsClosed { get; set; }

    [JsonProperty("can_access_closed")]
    public bool CanAccessClosed { get; set; }

    [JsonProperty("bdate")]
    public string? Bdate { get; set; }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append($"Name: {FirstName} {LastName}\n");
        sb.Append($"Is account closed: {(IsClosed ? "yes" : "no")}");
        if (Bdate != null)
        {
            sb.Append($"\nDate of birth: {Bdate}");
        }
        return sb.ToString();
    }
}