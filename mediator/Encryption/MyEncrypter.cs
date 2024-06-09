using System.Text;

namespace mediator.Encryption;

public class MyEncrypter : IEncrypter
{
    public string Encode(string value, string key)
    {
        var result = new StringBuilder();
        var keyIndex = 0;
        foreach (var letter in value)
        {
            result.Append((char)((letter + key[keyIndex++]) % char.MaxValue));
            keyIndex %= key.Length;
        }

        return result.ToString();
    }

    public string Decode(string value, string key)
    {
        var result = new StringBuilder();
        var keyIndex = 0;
        foreach (var letter in value)
        {
            result.Append((char)((letter - key[keyIndex++] + char.MaxValue) % char.MaxValue));
            keyIndex %= key.Length;
        }

        return result.ToString();
    }
}