// See https://aka.ms/new-console-template for more information

// unsigned integer means positive whole number or 0
uint naturalNumber = 23;
// integer means negative or positive whole number or 0
int integerNumber = -23;
// float means single-precision floating point
// F suffix makes it a float literal
float realNumber = 2.3F;
// double means double-precision floating point
double anotherRealNumber = 2.3; // double literal

// three variables that store the number 2 million
int decimalNotation = 2_000_000;
int binaryNotation = 0b_0001_1110_1000_0100_1000_0000;
int hexadecimalNotation = 0x_001E_8480;
// check the three variables have the same value
// both statements output true 
Console.WriteLine($"{decimalNotation == binaryNotation}");
Console.WriteLine($"{decimalNotation == hexadecimalNotation}");

Console.WriteLine("Using doubles:");
double a = 0.1;
double b = 0.2;
if (a + b == 0.3)
{
    Console.WriteLine($"{a} + {b} equals {0.3}");
}
else
{
    Console.WriteLine($"{a} + {b} does NOT equal {0.3}");
}

Console.WriteLine("Using decimals:");
decimal c = 0.1M; // M suffix means a decimal literal value
decimal d = 0.2M;
if (c + d == 0.3M)
{
    Console.WriteLine($"{c} + {d} equals {0.3M}");
}
else
{
    Console.WriteLine($"{c} + {d} does NOT equal {0.3M}");
}