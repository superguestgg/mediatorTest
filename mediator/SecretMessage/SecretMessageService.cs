using System.Text;
using mediator.Encryption;
using mediator.Hare;

namespace mediator.SecretMessage;

public class SecretMessageService : ISecretMessageService
{
    private IEncrypter _encrypter;
    public IRabbitClient _rabbitClient;
    private static readonly Dictionary<string, string> MessagesKeys = new Dictionary<string, string>();
    private static readonly Dictionary<string, string> Messages = new Dictionary<string, string>();
    private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
    
    public SecretMessageService(IEncrypter encrypter, IRabbitClient rabbitClient)
    {
        _encrypter = encrypter;
        _rabbitClient = rabbitClient;
    }

    public async Task<string> SaveMessage(string message)
    {
        var messageEncryptKey = await GenerateRandomString(100);
        var encodedMessage = _encrypter.Encode(message, messageEncryptKey);

        var privateKey = await GenerateRandomString(100);
        var privateKeyEncryptKey = await GenerateRandomString(100);

        while (Messages.ContainsKey(privateKey))
            privateKey = await GenerateRandomString(100);
        Messages[privateKey] = encodedMessage;
        var encodedPrivateKey = _encrypter.Encode(privateKey, privateKeyEncryptKey);
        
        
        var publicKey = await GenerateRandomString(50);

        while (MessagesKeys.ContainsKey(publicKey))
            publicKey = await GenerateRandomString(50);
        MessagesKeys[publicKey] = encodedPrivateKey;

        return $"{publicKey}.{privateKeyEncryptKey}.{messageEncryptKey}";
    }
    
    public async Task<string> GetPrivateKey(string publicKeyWithEncryptKey)
    {
        var publicKey = publicKeyWithEncryptKey.Split('.')[0];
        var privateKeyEncryptKey = publicKeyWithEncryptKey.Split('.')[1];
        if (MessagesKeys.TryGetValue(publicKey, out var privateKey))
            return _encrypter.Decode(privateKey, privateKeyEncryptKey);
        
        return await GenerateRandomString(100);
    }
        
    public async Task<string> GetMessage(string privateKeyWithEncryptKey)
    {
        var privateKey = privateKeyWithEncryptKey.Split('.')[0];
        var messageEncryptKey = privateKeyWithEncryptKey.Split('.')[1];
        
        if (Messages.TryGetValue(privateKey, out var message))
        {
            Messages.Remove(privateKey);
            return _encrypter.Decode(message, messageEncryptKey);
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