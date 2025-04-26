using BenchmarkDotNet.Running;
using String_Parser;


ConsoleKey? userInputKey = null;

while (userInputKey != ConsoleKey.Escape)
{
    UserInterface.WelcomeText();
    userInputKey = Console.ReadKey().Key;

    if(userInputKey == ConsoleKey.Enter)
    {
        Console.WriteLine("\n");
        UserInterface.DisplayResults();

        Console.WriteLine("\n");
        UserInterface.DisplayWithSpanResults();
    }
    else if (userInputKey == ConsoleKey.B)
    {
        Console.WriteLine("\n");
        BenchmarkRunner.Run<BenchMark_StringMapper>();
    }
    else
    {
        Console.WriteLine(" is Not valid");
    }
    Console.WriteLine("\n");
}

Environment.Exit(0);


