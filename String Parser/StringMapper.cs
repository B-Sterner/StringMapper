using System.Reflection;

namespace String_Parser;

public class StringMapper(string dataString, string tokenSeparator, string keyValueSeparator)
{
    private readonly string _dataString = dataString;
    private readonly string _tokenSplit = tokenSeparator;
    private readonly string _keyValueSplit = keyValueSeparator;
    public DataMessageModel DataMessage = new();

    public DataMessageModel ParseString()
    {
        foreach (var chunk in _dataString.Split(_tokenSplit))
        {
            var keyValuePair = chunk.Split(_keyValueSplit, StringSplitOptions.TrimEntries);

            if (keyValuePair.Length == 1)
            {
                DataMessage.GetType().GetProperty(keyValuePair[0], BindingFlags.NonPublic | BindingFlags.Instance)
                    ?.SetValue(DataMessage, true);
            }
            else if (keyValuePair.Length == 2)
            {
                var p = DataMessage.GetType().GetProperty(keyValuePair[0]);
                if (p != null)
                {
                    SetPropertyByType(p, keyValuePair[1]);
                }
            }

        }
        return DataMessage;
    }

    private void SetPropertyByType(PropertyInfo p, string v)
    {
        //Can use GetConverter if needing to actually convert
        //however convert is slower than casting
        //var convertedVal = TypeDescriptor.GetConverter(p.PropertyType).ConvertFromString(v);

        var castVal = Convert.ChangeType(v, p.PropertyType);
        p.SetValue(DataMessage, castVal);
    }

    public DataMessageModel ParseStringWithSpan()
    {
        ReadOnlySpan<char> spanString = _dataString.AsSpan();
        var startIndex = 0;
        var separatorIndex = spanString.IndexOf(_tokenSplit);

        while (startIndex < spanString.Length)
        {
            if (separatorIndex == -1)
            {
                SetProperty(spanString[startIndex..]);
                startIndex = spanString.Length;
            }
            else
            {
                SetProperty(spanString.Slice(startIndex, separatorIndex));
                startIndex += separatorIndex + 1;
                separatorIndex = spanString[startIndex..].IndexOf(_tokenSplit);
            }
        }
        return DataMessage;
    }

    private void SetProperty(ReadOnlySpan<char> tokenSpan)
    {
        if (tokenSpan.IndexOf(_keyValueSplit) == -1)
        {
            DataMessage.GetType().GetProperty(tokenSpan.Trim().ToString(), BindingFlags.NonPublic | BindingFlags.Instance)
                ?.SetValue(DataMessage, true);
        }
        else
        {
            var property = DataMessage.GetType().GetProperty(tokenSpan[..tokenSpan.IndexOf(_keyValueSplit)].Trim().ToString());
            if (property != null)
            {
                SetPropertyByType(property, tokenSpan[(tokenSpan.IndexOf(_keyValueSplit) + 1)..].Trim().ToString());
            }
        }
    }
}