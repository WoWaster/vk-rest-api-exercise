using Newtonsoft.Json;
using VkLibrary;

VkUserAuth.OpenVkAuth();
Console.WriteLine("Please enter url from your browser: ");
var url = Console.ReadLine();
if (string.IsNullOrEmpty(url))
{
    throw new ArgumentNullException(url, "Please enter correct token");
}
var credentials = VkUserAuth.ParseVkResponse(url);
var token = credentials.Token;
var userId = credentials.UserId;

var httpClient = new HttpClient();
var uploadServerRequest = await httpClient.GetAsync($"https://api.vk.com/method/photos.getOwnerPhotoUploadServer?owner_id={userId}&access_token={token}&v={Constants.VkApiVersion}");
if (!uploadServerRequest.IsSuccessStatusCode)
{
    throw new HttpRequestException();
}

var uploadServerContent = await uploadServerRequest.Content.ReadAsStringAsync();
var uploadUrlJson = uploadServerContent[12..(uploadServerContent.Length - 1)];
string uploadUrl = JsonConvert.DeserializeObject<VkUploadUrl>(uploadUrlJson).UploadUrl;

var avatar = new MultipartFormDataContent { { new ByteArrayContent(File.ReadAllBytes("avatar.jpg")), "photo", "avatar.jpg" } };
var uploadPhotoRequest = await httpClient.PostAsync(uploadUrl, avatar);
if (!uploadPhotoRequest.IsSuccessStatusCode)
{
    throw new Exception();
}
var uploadPhotoContent = await uploadPhotoRequest.Content.ReadAsStringAsync();
var photo = JsonConvert.DeserializeObject<VkOwnerPhoto>(uploadPhotoContent);

var savePhotoResponse = await httpClient.GetAsync($"https://api.vk.com/method/photos.saveOwnerPhoto?server={photo.Server}&hash={photo.Hash}&photo={photo.Photo}&access_token={token}&v={Constants.VkApiVersion}");
if (!savePhotoResponse.IsSuccessStatusCode)
{
    throw new Exception();
}
var savePhotoContent = await savePhotoResponse.Content.ReadAsStringAsync();
if (!savePhotoContent.Contains("error"))
{
    Console.WriteLine("Photo updated successfully");
}
else
{
    Console.WriteLine("Something went wrong...");
    Console.WriteLine(savePhotoContent);
}
