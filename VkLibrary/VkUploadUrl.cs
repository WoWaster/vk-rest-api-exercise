namespace VkLibrary;
using Newtonsoft.Json;

public class VkUploadUrl
{
    public VkUploadUrl(string upload_url)
    {
        UploadUrl = upload_url;
    }

    [JsonProperty("upload_url")]
    public string UploadUrl { get; set; }
}