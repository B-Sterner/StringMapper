using System.Reflection;

namespace String_Parser;

class UserInterface
{
    public static void WelcomeText()
    {
        Console.WriteLine("=============================================================");
        Console.WriteLine("Press the \"B\" to run the benchmark");
        Console.WriteLine("Press the \"Enter\" key show parsed results");
        Console.WriteLine("Press the \"Esc\" key to exit");
    }

    public static void DisplayResults()
    {
        var stringMapper = new StringMapper(ExampleDataString());
        var dataMessage = stringMapper.ParseString();

        Console.WriteLine("== Without Span ==");
        foreach (PropertyInfo prop in dataMessage.GetType().GetProperties())
        {
            var propertyName = prop.Name;
            var propertyValue = prop.GetValue(dataMessage);

            Console.WriteLine($"{propertyName}: {propertyValue}");
        }
    }

    public static void DisplayWithSpanResults()
    {
        var stringMapperWithSpan = new StringMapper(ExampleDataString());
        var dataMessageFromSpan = stringMapperWithSpan.ParseStringWithSpan();

        Console.WriteLine($"== Using Span ==");
        foreach (PropertyInfo prop in dataMessageFromSpan.GetType().GetProperties())
        {
            var propertyName = prop.Name;
            var propertyValue = prop.GetValue(dataMessageFromSpan);

            Console.WriteLine($"{propertyName}: {propertyValue}");
        }
    }

    public static string ExampleDataString()
    {
        return """
            Notneeded1: Notneeded1;
            Notneeded2: Notneeded2;
            Notneeded3: Notneeded3;
            Notneeded4: Notneeded4;
            Notneeded5: Notneeded5;
            NotNeededKeyword1;
            
            Val1=ok1; 
            Val2='ok2';
            Val3=dog;
            Val4=here;
            StringProp1='I am a longer string';
            StringProp2=  another string;
            BoolVal1= false;
            BoolVal2= true;
            IntVal1=36;  
            DecVal=1.215942;
            Enabled;
            
            Notneeded11: Notneeded1;
            Notneeded12: Notneeded2;
            Notneeded13: Notneeded3;
            Notneeded14: Notneeded4;
            Notneeded15: Notneeded5;
            NotNeededKeyword11;
            """;
    }
}
