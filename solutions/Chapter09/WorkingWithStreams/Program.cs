﻿using System.Xml;
using System.IO.Compression;

using static System.Console;
using static System.Environment;
using static System.IO.Path;

// WorkWithText();
// WorkWithXml();
WorkingWithCompression();
WorkingWithCompression(useBrotli: false);

static void WorkWithText()
{
    StreamWriter text = null;
    try
    {
        string textFile = Combine(CurrentDirectory, "streams.txt");
        text = File.CreateText(textFile);
        foreach (string item in Viper.Callsigns)
        {
            text.WriteLine(item);
        }
        text.Close(); //releases the resource and **flushes**

        WriteLine("{0} contains {1:N0} bytes",
                textFile, new FileInfo(textFile).Length);
        WriteLine(File.ReadAllText(textFile));
    }
    catch (Exception ex)
    {
        WriteLine($"{ex.GetType()} says {ex.Message}");
    }
    finally
    {
        if (text is not null)
        {
            text.Dispose();
            WriteLine("The file stream's unmanaged resources have been disposed.");
        }
    }

}

static void WorkWithXml()
{
    FileStream? xmlFileStream = null;
    XmlWriter? xml = null;
    try
    {
        // define file path to write to
        string xmlFile = Combine(CurrentDirectory, "streams.xml");
        // creates file stream (and, also file)
        xmlFileStream = File.Create(xmlFile);
        // warp the file stream in an XML writer helper and automatically
        // indent nested elements
        xml = XmlWriter.Create(xmlFileStream,
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
    catch (Exception ex)
    {
        WriteLine($"{ex.GetType()} says {ex.Message}");
    }
    finally
    {

        if (xml is not null)
        {
            xml.Dispose();
            WriteLine("The XML writer's unmanaged resources have been disposed.");
            if (xmlFileStream is not null)
            {
                xmlFileStream.Dispose();
                WriteLine("The file stream's unmanaged resources have been disposed.");
            }
        }
    }

}

void WorkingWithCompression(bool useBrotli = true)
{
    string fileExtension = useBrotli ? "brotli" : "gzip";
    string filePath = Combine(CurrentDirectory, $"streams.{fileExtension}");
    using FileStream fileToWrite = File.Create(filePath);
    Stream compressor = useBrotli switch
    {
        true => new BrotliStream(fileToWrite, CompressionMode.Compress),
        false => new GZipStream(fileToWrite, CompressionMode.Compress),
    };
    using (compressor)
    {
        using XmlWriter xml = XmlWriter.Create(compressor);
        xml.WriteStartDocument();
        xml.WriteStartElement("callsigns");
        foreach (string callsign in Viper.Callsigns)
        {
            xml.WriteElementString("callsign", callsign);
        }
        // NOTE: disposing of XMLWriter at the end of this block automatically
        // closes all open tags, if any, on all nesting levels.
    }

    WriteLine("{0} contains {1:N0} bytes.",
        filePath, new FileInfo(filePath).Length);
    WriteLine("The compressed contents:");
    WriteLine(File.ReadAllText(filePath));

    // reading while transparently decompressing
    WriteLine("Reading the compressed XML file:");
    using Stream fileToRead = File.Open(filePath, FileMode.Open);
    using Stream decompressor = useBrotli switch
    {
        true => new BrotliStream(fileToRead, CompressionMode.Decompress),
        false => new GZipStream(fileToRead, CompressionMode.Decompress),
    };
    using XmlReader reader = XmlReader.Create(decompressor);
    while (reader.Read()) // reads next XML node
    {
        if (reader.NodeType == XmlNodeType.Element && reader.Name == "callsign")
        {
            reader.Read(); // moves to text inside the element
            WriteLine(reader.Value);
        }
    }
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