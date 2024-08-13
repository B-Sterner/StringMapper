using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using System.Reflection;

namespace String_Parser;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class MessageParser
{
    const string DataString = "Val1=ok1; Val2='ok2'; DecVal=1.21; Enabled; Val4=here;Val3=dog;";
    private const char breakChar = ';';
    private const char SplitChar = '=';
    public DataMessage DataMessage = new();

    public MessageParser()
    {

    }

    [Benchmark]
    public DataMessage ParseString()
    {
        foreach (var chunk in DataString.Split(breakChar))
        {
            var keyValuePair = chunk.Split(SplitChar, StringSplitOptions.TrimEntries);

            if (keyValuePair.Length != 2)
            {
                DataMessage.GetType().GetProperty(keyValuePair[0])
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

    [Benchmark]
    public DataMessage ParseStringWithSpan()
    {
        ReadOnlySpan<char> spanString = DataString;
        var start = 0;
        var end = spanString.IndexOf(breakChar);
        ReadOnlySpan<char> currentSpan;

        while (start < spanString.Length)
        {
            if (end < 0)
            {
                currentSpan = spanString[start..];
                start = spanString.Length;
            }
            else
            {
                currentSpan = spanString.Slice(start, end);
                start = start + end + 1;
            }

            SetProperty(currentSpan);
            end = spanString[start..].IndexOf(breakChar);
        }
        return DataMessage;
    }

    private void SetProperty(ReadOnlySpan<char> currentSpan)
    {
        if (currentSpan.IndexOf(SplitChar) < 0)
        {
            DataMessage.GetType().GetProperty(currentSpan.Trim().ToString())
                ?.SetValue(DataMessage, true);
        }
        else
        {
            var p = DataMessage.GetType().GetProperty(currentSpan[..currentSpan.IndexOf(SplitChar)].Trim().ToString());
            if (p != null)
            {
                SetPropertyByType(p, currentSpan[(currentSpan.IndexOf(SplitChar) + 1)..].Trim().ToString());
            }
        }
    }

    private void SetPropertyByType(PropertyInfo p, string v)
    {
        var t = p.PropertyType.Name;
        if (t == "Decimal")
        {
            p.SetValue(DataMessage, Decimal.Parse(v));
        }
        else
        {
            p.SetValue(DataMessage, v);
        }
    }
}
