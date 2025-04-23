using MySqlConnector;

namespace DOLS_Compulsory_2.Server.DAL;

public static class DbUtils
{
    public static readonly string ProperlyFormattedConnectionString;

    static DbUtils()
    {
        ProperlyFormattedConnectionString = "Server=mariadb_container;User=myuser;Password=mypassword;Port=3306;Database=DB;Pooling=true;";

        try
        {
            using var connection = new MySqlConnection(ProperlyFormattedConnectionString);
            connection.Open();
            connection.Close();
            Console.WriteLine("✅ Forbindelse til MariaDB virker!");
        }
        catch (Exception e)
        {
            throw new Exception($@"
        Forbindelsesstrengen kunne ikke bruges. Er du sikker på, at din MariaDB kører? 

        Fejlbesked:
        {e.Message}
        ");
        }

    }
}

