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
    public string FormatDate()
{
    string date;
    while(true){
        Console.WriteLine("Enter a date in the format YYYYMMDD: ");
         date = Console.ReadLine();
        if (Regex.IsMatch(date, @"^\d{4}(0[1-9]|1[0-2])(0[1-9]|[12][0-9]|3[01])$"))
        {
            Console.WriteLine("Date valide");
            break;
        }
        else
        {
            Console.WriteLine("Format est pas correcte, veuillez entrer une date valide.");
            throw new FormatException("Invalid date format");
        }
    }
   return date;
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
   public bool DateOnly()
{
    while(true)
    {
        try
        {
           string date =  FormatDate();
            if (!IsDateAvailable(date))
            {
                Console.WriteLine("Date existe pas dans la base de données.");
                return false;
            }
        }
        catch (FormatException)
        {
            continue;
        }
        return true;
    }
}
}