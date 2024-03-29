﻿using static System.Console;

internal class Program
{
    static void TimesTable(byte number)
    {
        WriteLine($"This is the {number} times table:");
        for (byte row = 0; row <= 12; row++)
        {
            WriteLine($"{row} x {number} = {row * number}");
        }
    }
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

    /// <summary>
    /// Pass a 32-bit integer and it will be converted into its ordinal equivalent.
    /// </summary>
    /// <param name="number">Number is a cardinal value e.g. 1, 2, 3, and so on.</param>
    /// <returns>Number as an ordinal value e.g. 1st, 2nd, 3rd, and so on.</returns>
    static string CardinalToOrdinal(int number)
    {
        switch (number)
        {
            case 11:
            case 12:
            case 13:
                return $"{number}th";
            default:
                int lastDigit = number % 10;
                string suffix = lastDigit switch
                {
                    1 => "st",
                    2 => "nd",
                    3 => "rd",
                    _ => "th"
                };
                return $"{number}{suffix}";
        }
    }

    static void RunCardinalToOrdinal()
    {
        for (int number = 1; number <= 40; number++)
        {
            Write($"{CardinalToOrdinal(number)} ");
        }
        WriteLine();
    }

    static int Factorial(int number)
    {
        if (number < 1)
        {
            return 0;
        }
        else if (number == 1)
        {
            return 1;
        }
        else
        {
            checked
            {

                return number * Factorial(number - 1);
            }
        }
    }

    static void RunFactorial()
    {
        for (int i = 1; i <= 14; i++)
        {
            try
            {
                WriteLine($"{i}! = {Factorial(i):N0}");
            }
            catch (System.OverflowException)
            {
                WriteLine($"{i}! is to big for a 32-bit integer.");
            }
        }
    }

    static int FibImperative(int term)
    {
        if (term == 1)
        {
            return 0;
        }
        else if (term == 2)
        {
            return 1;
        }
        else
        {
            return FibImperative(term - 1) + FibImperative(term - 2);
        }
    }

    static void RunFibImperative()
    {
        for (int i = 1; i <= 30; i++)
        {
            WriteLine("The {0} term of the Fibonacci sequence is {1:N0}",
                CardinalToOrdinal(i),
                FibImperative(i));
        }
    }

    static int FibFunctional(int term) =>
    term switch
    {
        <= 0 => 0,
        2 => 1,
        _ => FibFunctional(term - 1) + FibFunctional(term - 2)
    };

    static void RunFibFunctional()
    {
        for (int i = 1; i <= 30; i++)
        {
            WriteLine("The {0} term of the Fibonacci sequence is {1:N0}",
                CardinalToOrdinal(i),
                FibFunctional(i));
        }
    }


    public static void Main(string[] args)
    {
        // TimesTable(12);

        // decimal taxToPay = CalculateTax(amount: 149, twoLetterRegionCode: "FR");
        // WriteLine($"You must pay {taxToPay} in tax.");

        // RunCardinalToOrdinal();

        // RunFactorial();

        // RunFibImperative();

        RunFibFunctional();
    }
}