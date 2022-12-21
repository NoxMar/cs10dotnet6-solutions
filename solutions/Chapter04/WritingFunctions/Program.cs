using static System.Console;

static void TimesTable(byte number)
{
    WriteLine($"This is the {number} times table:");
    for (byte row = 0; row <= 12; row++)
    {
        WriteLine($"{row} x {number} = {row * number}");
    }
}

// TimesTable(12);


static decimal CalculateTax(decimal amount, string twoLetterRegionCode)
{
    decimal rate = twoLetterRegionCode switch
    {
        "CH" => 0.08M, // Switzerland
        "DK" or "NO" => 0.25M, // Denmark, Norway
        "GB" or "FR" => 0.2M, // United Kingdom, France
        "HU" => 0.27M, // Hungary
        "OR" or "AK" or "MT" => 0.0M, // Oregon, Alaska, Montana
        "ND" or "WI" or "ME" or "VA" => 0.05M, // North Dakota, Wisconsin, Maine, Virginia
        "CA" => 0.0825M, // California
        _ => 0.06M  // Most US states
    };
    return amount * rate;
}

decimal taxToPay = CalculateTax(amount: 149, twoLetterRegionCode: "FR");
WriteLine($"You must pay {taxToPay} in tax.");