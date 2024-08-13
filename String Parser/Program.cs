// See https://aka.ms/new-console-template for more information



using BenchmarkDotNet.Running;
using Microsoft.Diagnostics.Runtime.Utilities;
using String_Parser;
using System.Security.Cryptography.X509Certificates;


//BenchmarkRunner.Run<MessageParser>();

var stringParser = new MessageParser();
var result = stringParser.ParseString();
var result2 = stringParser.ParseStringWithSpan();


Console.WriteLine(result.Val1);
Console.WriteLine(result.Val2);
Console.WriteLine(result.Val3);
Console.WriteLine(result.Val4);
Console.WriteLine(result.DecVal);
Console.WriteLine(result.IsEnabled);

Console.WriteLine("");

Console.WriteLine(result2.Val1);
Console.WriteLine(result2.Val2);
Console.WriteLine(result2.Val3);
Console.WriteLine(result2.Val4);
Console.WriteLine(result2.DecVal);
Console.WriteLine(result2.IsEnabled);
