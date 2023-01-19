using System.Xml;

using static System.Console;
using static System.Environment;
using static System.IO.Path;

// WorkWithText();
WorkWithXml();

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

static void WorkWithXml()
{
    // define file path to write to
    string xmlFile = Combine(CurrentDirectory, "streams.xml");
    // creates file stream (and, also file)
    FileStream xmlFileStream = File.Create(xmlFile);
    // warp the file stream in an XML writer helper and automatically
    // indent nested elements
    XmlWriter xml = XmlWriter.Create(xmlFileStream,
        new XmlWriterSettings { Indent = true });

    xml.WriteStartDocument();
    xml.WriteStartElement("callsigns");
    foreach (string callsing in Viper.Callsigns)
    {
        xml.WriteElementString("callsign", callsing);
    }
    xml.WriteEndElement();
    // WARNING: closing helper doesn't close the file stream, apparently
    xml.Close();
    xmlFileStream.Close();

    WriteLine("{0} contains {1:N0} bytes.",
        xmlFile,
        new FileInfo(xmlFile).Length);
    WriteLine(File.ReadAllText(xmlFile));
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