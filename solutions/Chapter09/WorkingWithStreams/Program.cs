using System.Xml;

using static System.Console;
using static System.Environment;
using static System.IO.Path;

WorkWithText();

static void WorkWithText()
{
    string textFile = Combine(CurrentDirectory, "streams.txt");
    StreamWriter text = File.CreateText(textFile);
    foreach (string item in Viper.Callsigns)
    {
        text.WriteLine(item);
    }
    text.Close(); //releases the resource and **flushes**

    WriteLine("{0} contains {1:N0} bytes",
              textFile, new FileInfo(textFile).Length);
    WriteLine(File.ReadAllText(textFile));
}
static class Viper
{
    // define an array of Viper pilot call signs
    public static string[] Callsigns = new[]
    {
        "Husker", "Starbuck", "Apollo", "Boomer",
        "Bulldog", "Athena", "Helo", "Racetrack"
    };
}