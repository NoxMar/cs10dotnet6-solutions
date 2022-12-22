namespace CalculatorLibUnitTests;

using Packt;

public class CalculatorUnitTest
{
    [Fact]
    public void TestAdding2And2()
    {
        // arrange
        double a = 2, b = 2;
        double expected = 4;
        Calculator calc = new();
        // act
        double actual = calc.Add(a, b);
        // assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void TestAdding2And3()
    {
        // arrange
        double a = 2, b = 3;
        double expected = 5;
        Calculator calc = new();
        // act
        double actual = calc.Add(a, b);
        // assert
        Assert.Equal(expected, actual);
    }
}