using System;
using System.IO;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        
      
         User user1 = new User();
        Console.Write("Enter Name: ");
        user1.Name = Console.ReadLine();

        Console.Write("Enter Email: ");
        user1.Email = Console.ReadLine();
        User.Create(user1);
           User user = User.Read(1); // Remplacez 1 par l'ID de l'utilisateur que vous voulez lire

        if (user != null)
        {
            Console.WriteLine($"Id: {user.Id}");
            Console.WriteLine($"Name: {user.Name}");
            Console.WriteLine($"Email: {user.Email}");
        }
        else
        {
            Console.WriteLine("User not found");
        }
    }        
}
