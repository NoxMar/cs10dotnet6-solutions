using System.Reflection;

// Unused declarations from different assemblies to print out info about them
System.Data.DataSet ds;
HttpClient client;

// See https://aka.ms/new-console-template for more information
Assembly? assembly = Assembly.GetEntryAssembly();
if (assembly == null) return;

foreach (AssemblyName name in assembly.GetReferencedAssemblies())
{
    Assembly a = Assembly.Load(name);

    int methodCount = 0;

    foreach (TypeInfo t in a.DefinedTypes)
    {
        methodCount += t.GetMethods().Count();
    }

    Console.WriteLine("{0:N0} types with {1:N0} methods in {2} assembly",
                  arg0: a.DefinedTypes.Count(),
                  arg1: methodCount,
                  arg2: name.Name);
}