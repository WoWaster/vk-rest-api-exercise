namespace VkLibrary;
using Newtonsoft.Json;
using System.Text;

public class VkUserGetResponse
{
    public VkUserGetResponse(HttpClient client, string user_id, string token)
    {
        HttpClient = client;
        UserID = user_id;
        Token = token;
    }
    public HttpClient HttpClient { get; set; }
    public string UserID { get; set; }
    public string Token { get; set; }

    public async Task<VkUserList?> getData()
    {
        var response = await HttpClient.GetAsync($"https://api.vk.com/method/users.get?user_ids={UserID}&fields=bdate&access_token={Token}&v={Constants.VkApiVersion}");
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<VkUserList>(content);
        }
        return null;
    }
}