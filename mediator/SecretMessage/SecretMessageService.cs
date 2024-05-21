using System.Text;

namespace mediator.SecretMessage;

public class SecretMessageService : ISecretMessageService
{
    private static readonly Dictionary<string, string> MessagesKeys = new Dictionary<string, string>();
    private static readonly Dictionary<string, string> Messages = new Dictionary<string, string>();
    private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
    
    public SecretMessageService()
    {
        
    }

    public async Task<string> SaveMessage(string message)
    {
        var privateKey = await GenerateRandomString(100);
        while (Messages.ContainsKey(privateKey))
            privateKey = await GenerateRandomString(100);
        Messages[privateKey] = message;
        
        var publicKey = await GenerateRandomString(50);
        while (MessagesKeys.ContainsKey(publicKey))
            publicKey = await GenerateRandomString(50);
        MessagesKeys[publicKey] = privateKey;
        return publicKey;
    }
    
    public async Task<string> GetPrivateKey(string publicKey)
    {
        if (MessagesKeys.TryGetValue(publicKey, out var privateKey))
            return privateKey;
        
        return await GenerateRandomString(100);
    }
        
    public async Task<string> GetMessage(string privateKey)
    {
        if (Messages.TryGetValue(privateKey, out var message))
        {
            Messages.Remove(privateKey);
            return message;
        }
        return await GenerateRandomString(10);
    }
    
    private async Task<string> GenerateRandomString(int length = 50)
    {
        var random = new Random();
        var stringBuilder = new StringBuilder();
        while (length-- > 0)
        {
            stringBuilder.Append(Chars[random.Next(Chars.Length)]);
        }
        
        return stringBuilder.ToString();
    }
}