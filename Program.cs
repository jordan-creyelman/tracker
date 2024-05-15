using System;
using System.IO;
using System.Collections.Generic;
using Spectre.Console;
using System.Globalization;
class Program
{
    

static void Main()
{
    User user1 = new User();
    Validation validation = new Validation();
    // int time = user1.Time();
    // user1.Chrono(time);
    validation.DateOnly();
   
    user1.Name = AnsiConsole.Ask<string>("Enter Name: ");
    user1.Email = AnsiConsole.Ask<string>("Enter Email: ");
    User.Create(user1);

    int id = AnsiConsole.Ask<int>("Enter the ID of the user you want to read: ");
    User user = User.Read(id);

    if (user != null)
    {
        AnsiConsole.MarkupLine($"[bold red]Id:[/] {user.Id}");
        AnsiConsole.MarkupLine($"[bold red]Name:[/] {user.Name}");
        AnsiConsole.MarkupLine($"[bold red]Email:[/] {user.Email}");
    }
    else
    {
        AnsiConsole.MarkupLine("[bold red]User not found[/]");
    }
}    
   
}
