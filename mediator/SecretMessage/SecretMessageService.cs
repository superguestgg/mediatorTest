using System.Text;

namespace mediator;

public class SecretMessageService
{
    private static readonly Dictionary<string, string> MessagesKeys = new Dictionary<string, string>();
    private static readonly Dictionary<string, string> Messages = new Dictionary<string, string>();
    
    public SecretMessageService()
    {
        
    }

    public async Task<string> SaveMessage(string message)
    {
        var privateKey = await GenerateRandomString(100);
        while (Messages.ContainsKey(privateKey))
            privateKey = await GenerateRandomString(100);
        Messages[privateKey] = message;
        
        var publicKey = await GenerateRandomString();
        while (MessagesKeys.ContainsKey(publicKey))
            publicKey = await GenerateRandomString(100);
        MessagesKeys[publicKey] = privateKey;
        return publicKey;
    }
    
    public async Task<string> GetPrivateKey(string publicKey)
    {
        if (MessagesKeys.TryGetValue(publicKey, out var privateKey))
        {
            MessagesKeys.Remove(publicKey);
            return privateKey;
        }
            
        return await GenerateRandomString(100);
    }
        
    public async Task<string> GetMessage(string privateKey)
    {
        if (Messages.TryGetValue(privateKey, out var message))
        {
            MessagesKeys.Remove(privateKey);
            return message;
        }
        return await GenerateRandomString(10);
    }
    
    private async Task<string> GenerateRandomString(int length = 50)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var random = new Random();
        var stringBuilder = new StringBuilder();
        while (length-- > 0)
        {
            stringBuilder.Append(chars[random.Next(chars.Length)]);
        }
        
        return stringBuilder.ToString();
    }
}