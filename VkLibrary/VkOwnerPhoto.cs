namespace VkLibrary;
using Newtonsoft.Json;
public class VkOwnerPhoto
{
    public VkOwnerPhoto(string server, string photo, string mid, string hash, string messageCode, string profileAid)
    {
        Server = server;
        Photo = photo;
        Mid = mid;
        Hash = hash;
        MessageCode = messageCode;
        ProfileAid = profileAid;
    }

    [JsonProperty("server")]
    public string Server { get; set; }

    [JsonProperty("photo")]
    public string Photo { get; set; }

    [JsonProperty("mid")]
    public string Mid { get; set; }

    [JsonProperty("hash")]
    public string Hash { get; set; }

    [JsonProperty("message_code")]
    public string MessageCode { get; set; }

    [JsonProperty("profile_aid")]
    public string ProfileAid { get; set; }
}