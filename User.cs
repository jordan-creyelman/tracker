using System;
using System.Data.SQLite;
using System.Diagnostics;
public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public int Time(){
        Console.WriteLine("pour une heure on met 1 ");
        int  time = Convert.ToInt32(Console.ReadLine());

        return time;
    }
        public  void Chrono(int time){
        Stopwatch stopwatch = new Stopwatch();
        int  millisecondes = time *60*60*1000;	
        // Commence le chronomètre
      // Commence le chronomètre
        stopwatch.Start();

        // Exécute une tâche qui attend pendant un certain temps
        Task.Run(() =>
        {
            Thread.Sleep(millisecondes);
        });

        // Affiche le temps écoulé toutes les secondes
        while (stopwatch.IsRunning)
        {
            Console.WriteLine("Temps écoulé: {0}ms", stopwatch.ElapsedMilliseconds);
            Thread.Sleep(1000);
        }

        // Arrête le chronomètre
        stopwatch.Stop();
    }
    private static SQLiteConnection GetConnection()
{
    Dictionary<string, string> configurations = Config.ReadConfigFile("config.txt");

    // Récupérer les valeurs de configuration
    string setting1 = configurations["SETTING1"];
    Console.WriteLine(setting1);

    var connectionString = $"Data Source={setting1};Version=3;";
    return new SQLiteConnection(connectionString);
}


    public static void Create(User user)
{
    using (var connection = GetConnection())
    {
        connection.Open();

        // Create table if not exists
        string createTableQuery = @"
        CREATE TABLE IF NOT EXISTS Users (
            Id INTEGER PRIMARY KEY,
            Name TEXT NOT NULL,
            Email TEXT NOT NULL
        );";

        using (var createTableCommand = new SQLiteCommand(createTableQuery, connection))
        {
            createTableCommand.ExecuteNonQuery();
        }

        using (var command = new SQLiteCommand(connection))
        {
            command.CommandText = "INSERT INTO Users (Name, Email) VALUES (@Name, @Email)";
            command.Parameters.AddWithValue("@Name", user.Name);
            command.Parameters.AddWithValue("@Email", user.Email);
            command.ExecuteNonQuery();
        }
    }
}

    public static User Read(int id)
    {
        using (var connection = GetConnection())
        {
            connection.Open();
            using (var command = new SQLiteCommand(connection))
            {
                command.CommandText = "SELECT * FROM Users WHERE Id = @Id";
                command.Parameters.AddWithValue("@Id", id);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new User
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Email = reader.GetString(2)
                        };
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
    }

    public static void Update(User user)
    {
        using (var connection = GetConnection())
        {
            connection.Open();
            using (var command = new SQLiteCommand(connection))
            {
                command.CommandText = "UPDATE Users SET Name = @Name, Email = @Email WHERE Id = @Id";
                command.Parameters.AddWithValue("@Name", user.Name);
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@Id", user.Id);
                command.ExecuteNonQuery();
            }
        }
    }

    public static void Delete(int id)
    {
        using (var connection = GetConnection())
        {
            connection.Open();
            using (var command = new SQLiteCommand(connection))
            {
                command.CommandText = "DELETE FROM Users WHERE Id = @Id";
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
            }
        }
    }
}