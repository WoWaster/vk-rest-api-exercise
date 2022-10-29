using Newtonsoft.Json;
using VkLibrary;

var token = Environment.GetEnvironmentVariable("VK_AUTH_TOKEN");
if (token == null)
{
    throw new ArgumentNullException(token, "Please provide token in VK_AUTH_TOKEN environment variable.");
}

var user_id = Console.ReadLine();
if (string.IsNullOrEmpty(user_id))
{
    throw new ArgumentNullException(user_id, "Please provide user id.");
}

var httpClient = new HttpClient();
VkUserGetResponse userResponse = new VkUserGetResponse(httpClient, user_id, token);
VkUserList userData = await userResponse.getData();
Console.WriteLine(userData?.Users[0].ToString());
