using static System.Console;
// See https://aka.ms/new-console-template for more information



/*
ValueType[] types = new[] { typeof(sbyte), typeof(byte), typeof(short), typeof(ushort)
                       , typeof(int), typeof(uint), typeof(long), typeof(ulong)
                       , typeof(float), typeof(double), typeof(decimal) };*/

string lineSep = new('-', 95);

WriteLine(lineSep);
WriteLine("{0,-9} {1,19} {2,32} {3,32}", "Type", "Byte(s) of memory", "Min", "Max");
WriteLine(lineSep);
WriteLine("{0,-9} {1,19} {2,32} {3,32}", "sbyte", sizeof(sbyte), sbyte.MinValue, sbyte.MaxValue);
WriteLine("{0,-9} {1,19} {2,32} {3,32}", "byte", sizeof(byte), byte.MinValue, byte.MaxValue);
WriteLine("{0,-9} {1,19} {2,32} {3,32}", "short", sizeof(short), short.MinValue, short.MaxValue);
WriteLine("{0,-9} {1,19} {2,32} {3,32}", "ushort", sizeof(ushort), ushort.MinValue, ushort.MaxValue);
WriteLine("{0,-9} {1,19} {2,32} {3,32}", "int", sizeof(int), int.MinValue, int.MaxValue);
WriteLine("{0,-9} {1,19} {2,32} {3,32}", "uint", sizeof(uint), uint.MinValue, uint.MaxValue);
WriteLine("{0,-9} {1,19} {2,32} {3,32}", "long", sizeof(long), long.MinValue, long.MaxValue);
WriteLine("{0,-9} {1,19} {2,32} {3,32}", "ulong", sizeof(ulong), ulong.MinValue, ulong.MaxValue);
WriteLine("{0,-9} {1,19} {2,32} {3,32}", "float", sizeof(float), float.MinValue, float.MaxValue);
WriteLine("{0,-9} {1,19} {2,32} {3,32}", "double", sizeof(double), double.MinValue, double.MaxValue);
WriteLine("{0,-9} {1,19} {2,32} {3,32}", "decimal", sizeof(decimal), decimal.MinValue, decimal.MaxValue);
WriteLine(lineSep);