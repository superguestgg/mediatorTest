namespace mediator.SecretMessage;

public interface ISecretMessageService
{
    Task<string> SaveMessage(string message);

    Task<string> GetPrivateKey(string publicKey);

    Task<string> GetMessage(string privateKey);
}