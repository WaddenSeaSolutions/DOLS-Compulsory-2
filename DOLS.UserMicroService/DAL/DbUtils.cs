using MySqlConnector;

namespace DOLS_Compulsory_2.Server.DAL;

public static class DbUtils
{
    public static readonly string ProperlyFormattedConnectionString;

    static DbUtils()
    {
        ProperlyFormattedConnectionString = "Server=mariadb-user;Port=3306;User=myuser;Password=mypassword;Database=userdb;Pooling=true;";

        try
        {
            using var connection = new MySqlConnection(ProperlyFormattedConnectionString);
            connection.Open();
            connection.Close();
            Console.WriteLine("Connected to db");
        }
        catch (Exception e)
        {
            throw new Exception($@"
        {e.Message}
        ");
        }

    }
}

