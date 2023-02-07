namespace GreetCount.Test;

using GreetCount;

public class GreetCountTest
{
    [Fact]
    public void Test1()
    {
        IGreetCount greetCount = new SQLGreetCount("");

        Assert.Equal(1, greetCount.Count("Andy"));

    }
}