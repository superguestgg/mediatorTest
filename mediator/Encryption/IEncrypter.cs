namespace mediator.Encryption;

public interface IEncrypter
{ 
    string Encode(string value, string key);
    
    string Decode(string value, string key);
}