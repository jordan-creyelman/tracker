using System.Runtime.InteropServices;

public class Config
{

   public static Dictionary<string, string> ReadConfigFile(string filePath)
    {
        Dictionary<string, string> configurations = new Dictionary<string, string>();

        // Vérifier si le fichier existe
        if (File.Exists(filePath))
        {
            // Lire chaque ligne du fichier
            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                // Ignorer les commentaires et les lignes vides
                if (!string.IsNullOrWhiteSpace(line) && !line.Trim().StartsWith("#"))
                {
                    // Diviser la ligne en clé et valeur
                    string[] parts = line.Split('=');
                    if (parts.Length == 2)
                    {
                        // Ajouter la clé et la valeur au dictionnaire de configurations
                        configurations.Add(parts[0], parts[1]);
                    }
                }
            }
        }

        return configurations;
    }
   
}