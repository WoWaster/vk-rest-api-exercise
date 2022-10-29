namespace VkLibrary;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Net;

public struct VkCredentials
{
    public VkCredentials(string token, string userId)
    {
        Token = token;
        UserId = int.Parse(userId);
    }
    public string Token;
    public int UserId;
}
public class VkUserAuth
{
    // code from: https://brockallen.com/2016/09/24/process-start-for-urls-on-net-core/
    private static void OpenBrowser(string url)
    {
        try
        {
            Process.Start(url);
        }
        catch
        {
            // hack because of this: https://github.com/dotnet/corefx/issues/10361
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                url = url.Replace("&", "^&");
                Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                Process.Start("xdg-open", url);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                Process.Start("open", url);
            }
            else
            {
                throw;
            }
        }
    }

    public static void OpenVkAuth()
    {
        var clientId = Environment.GetEnvironmentVariable("VK_APP_ID");
        if (clientId == null)
        {
            throw new ArgumentNullException(clientId, "Please provide token in VK_APP_ID environment variable");
        }

        OpenBrowser($"https://oauth.vk.com/authorize?client_id={clientId}&display=page&redirect_uri=https://oauth.vk.com/blank.html&scope=friends,photos&response_type=token&v={Constants.VkApiVersion}");
    }

    public static VkCredentials ParseVkResponse(string response)
    {
        // find token
        var indexOfEquals = response.IndexOf("=") + 1;
        var indexOfAnd = response.IndexOf("&");

        // find userId
        var indexOfLastEquals = response.LastIndexOf("=") + 1;
        return new VkCredentials(response[indexOfEquals..indexOfAnd], response[indexOfLastEquals..]);
    }
}
