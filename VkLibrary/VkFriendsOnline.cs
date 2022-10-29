namespace VkLibrary;
using Newtonsoft.Json;

public class VkFriendsOnline
{
    public VkFriendsOnline(List<string> friendIds)
    {
        FriendIds = friendIds;
    }
    [JsonProperty("response")]
    public List<string> FriendIds { get; set; }
}