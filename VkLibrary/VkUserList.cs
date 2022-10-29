namespace VkLibrary;
using Newtonsoft.Json;

public class VkUserList
{
    public VkUserList(List<VkUser> users)
    {
        Users = users;
    }

    [JsonProperty("response")]
    public List<VkUser> Users { get; set; }
}