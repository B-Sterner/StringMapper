using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace String_Parser;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class BenchMark_StringMapper
{
    private readonly string dataString = UserInterface.ExampleDataString();

    [Benchmark(Baseline = true)]
    public DataMessageModel ParseString()
    {
        var stringMapper = new StringMapper(dataString, ";", "=");
        return stringMapper.ParseString();
    }

    [Benchmark]
    public DataMessageModel ParseStringWithSpan()
    {
        var stringMapperWithSpan = new StringMapper(dataString, ";", "=");
        return stringMapperWithSpan.ParseStringWithSpan();
    }
}
