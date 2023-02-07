namespace GreetCount.Test;

using GreetCount;
using Npgsql;
using Dapper;

public class GreetCountTest
{
    private static string cn = "Host=localhost;Username=counter;Password=counter123;Database=counter_app";

    static string GetConnectionString() {
        var theCN = Environment.GetEnvironmentVariable("PSQLConnectionString");
        if (theCN == "" || theCN == null) {
            theCN = cn;
        }
        return theCN;
    }


    public GreetCountTest(){

        using(var connection = new NpgsqlConnection(GetConnectionString())){
            connection.Execute("delete from greet_count");
        }

    }

    [Fact]
    public void GreetOneUser()
    {

        IGreetCount greetCount = new SQLGreetCount(GetConnectionString());
        greetCount.Greet("Andy");
        Assert.Equal(1, greetCount.Count("Andy"));

    }

    [Fact]
    public void GreetUserMoreThanOnce()
    {


        IGreetCount greetCount = new SQLGreetCount(GetConnectionString());
        greetCount.Greet("Andy");
        greetCount.Greet("Andy");
        Assert.Equal(2, greetCount.Count("Andy"));

    }

    [Fact]
    public void GreetThreeUsers()
    {


        IGreetCount greetCount = new SQLGreetCount(GetConnectionString());
        greetCount.Greet("Andy");
        greetCount.Greet("Bob");
        greetCount.Greet("Kate");

        Assert.Equal(1, greetCount.Count("Andy"));
        Assert.Equal(1, greetCount.Count("Bob"));
        Assert.Equal(1, greetCount.Count("Kate"));

    }
}