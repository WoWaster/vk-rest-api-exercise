using Newtonsoft.Json;
using VkLibrary;

VkUserAuth.OpenVkAuth();
Console.WriteLine("Please enter url from your browser: ");
var url = Console.ReadLine();
if (string.IsNullOrEmpty(url))
{
    throw new ArgumentNullException(url, "Please enter correct token");
}
var token = VkUserAuth.ParseVkResponse(url).Token;

var httpClient = new HttpClient();
var response = await httpClient.GetAsync($"https://api.vk.com/method/friends.getOnline?&access_token={token}&v={Constants.VkApiVersion}");
if (response.IsSuccessStatusCode)
{
    var content = await response.Content.ReadAsStringAsync();

    VkFriendsOnline onlineIds = JsonConvert.DeserializeObject<VkFriendsOnline>(content);
    VkUserGetResponse userResponse = new VkUserGetResponse(httpClient, string.Join(", ", onlineIds.FriendIds), token);
    VkUserList usersData = await userResponse.getData();
    foreach (var friend in usersData.Users)
    {
        Console.WriteLine($"Id: {friend.Id} Name: {friend.FirstName} {friend.LastName}");
    }
}

