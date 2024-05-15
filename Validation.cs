using System.Data.SQLite;
using System.Text.RegularExpressions;
public class Validation
{
    private static SQLiteConnection GetConnection()
    {
    Dictionary<string, string> configurations = Config.ReadConfigFile("config.txt");

    // Récupérer les valeurs de configuration
    string setting1 = configurations["SETTING1"];
    Console.WriteLine(setting1);

    var connectionString = $"Data Source={setting1};Version=3;";
    return new SQLiteConnection(connectionString);
    }
    public bool FormatDate(string date)
    {

        bool isValid = Regex.IsMatch(date, @"^\d{4}(0[1-9]|1[0-2])(0[1-9]|[12][0-9]|3[01])$");

        Console.WriteLine(isValid ? "La date est valide." : "La date n'est pas valide.");
        return isValid;
    }
    public  bool IsDateAvailable(string dateToCheck)
    {
        using (var connection = GetConnection())
        {
            connection.Open();
            using (var command = new SQLiteCommand(connection))
            {
                command.CommandText = "SELECT COUNT(*) FROM habits WHERE date = @Date";

                command.Parameters.AddWithValue("@Date", dateToCheck);

                int count = Convert.ToInt32(command.ExecuteScalar());
                return count > 0;
            }
        }
    }

   
}