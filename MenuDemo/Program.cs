ComboMenu menu = new ("Choose an option");

for (int i = 1; i <= 24; i++)
{
    int index = i; // capture variable for closure
    menu = menu.Add($"Option {index}", async () =>
    {
        //i need to eventually allow old fashioned way (so samples work).
        //StyledTextBuilder builder = new();

        AnsiConsole.WriteLine($"You selected option {index}", cc1.Lime);

        //AnsiConsole.MarkupLine($"[green]You selected Option {index}[/]");
        await Task.Delay(500); // simulate some async work
    });
}

await AnsiConsole.ShowMenuAsync(menu);
Console.WriteLine("Bye");
