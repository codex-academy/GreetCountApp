namespace GreetCount;

using Dapper;
using Npgsql;
public class SQLGreetCount : IGreetCount
{

    private string connectionString;
    public SQLGreetCount(string cn)
    {
        connectionString = cn;
    }

    public void Greet(string username)
    {
        using (var connection = new NpgsqlConnection(connectionString))
        {
           var userCount = connection.ExecuteScalar<int>(
                @"select count(*) from greet_count where username = @Username", new {
                    Username = username
                });

            if (userCount == 0)
            {
                connection.Execute(@"insert into greet_count (username, counter) values (@Username, 1)", new
                {
                    Username = username
                });
            }
            else
            {
                connection.Execute(@"update greet_count set counter = counter + 1 where username = @Username", new
                {
                    Username = username
                });
            }
        }
    }
    public int Count(string username)
    {
        using (var connection = new NpgsqlConnection(connectionString))
        {
            var userCount = connection.ExecuteScalar<int>(@"select counter from greet_count where username = @Username", new
            {
                Username = username
            });
            return userCount;
        }
    }

}