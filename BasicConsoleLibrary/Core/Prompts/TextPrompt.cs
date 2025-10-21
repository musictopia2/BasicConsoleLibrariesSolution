namespace BasicConsoleLibrary.Core.Prompts;
/// <summary>
/// Initializes a new instance of the <see cref="TextPrompt{T}"/> class.
/// </summary>
/// <param name="prompt">The prompt markup text.</param>
public class TextPrompt<T>(string prompt) : IPrompt<T>
    where T : notnull
{


    /// <summary>
    /// Gets or sets the prompt style.
    /// </summary>
    public Style? PromptStyle { get; set; }

    /// <summary>
    /// Gets or sets the validator.
    /// </summary>
    public Func<T, ValidationResult>? Validator { get; set; }
    /// <summary>
    /// Gets or sets the validation error message.
    /// </summary>
    public string ValidationErrorMessage { get; set; } = "Invalid input";
    public Style ValidationErrorStyle { get; set; } = cc2.Red;

    public T? DefaultValue { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether or not
    /// default values should be shown.
    /// </summary>
    public bool ShowDefaultValue { get; set; } = false;

    //if i want masks, that has to wait until later (?)


    public T Show()
    {
        return ShowAsync(CancellationToken.None).GetAwaiter().GetResult();
    }

    public async Task<T> ShowAsync(CancellationToken cancellationToken)
    {
        var output = await ConsoleExclusive.RunAsync(async () =>
        {
            
            var promptStyle = PromptStyle ?? Style.Plain;
            while (true)
            {
                ITypeParsingProvider<T>? parses = CustomTypeParsingHelpers<T>.MasterContext ?? throw new CustomBasicException("No register parser");
                BasicList<string> choices = parses.GetSupportedList;
                if (choices.Count > 0)
                {
                    ConsoleComboBox combos = new(choices, false, 10);
                    combos.PromptString = prompt;
                    combos.PromptStyle = promptStyle;
                    string? value = await combos.ShowInsideExclusive(cancellationToken);
                    if (value is null)
                    {
                        Text text = new("You chose no value.  Please try again", ValidationErrorStyle);
                        AnsiConsole.Write(text);
                        Console.ResetColor(); //just in case.
                        continue;
                    }
                    else
                    {
                        if (parses.TryParse(value, out T result2, out string? errorMessage2) == false)
                        {
                            Text text = new($"Invalid input: {errorMessage2}", ValidationErrorStyle);
                            AnsiConsole.Write(text);
                            Console.ResetColor(); //just in case.
                            continue;
                        }
                        return result2;
                    }
                }
                //this means no choices.
                WritePrompt();
                //var input = await console.ReadLine(promptStyle, IsSecret, Mask, choices, cancellationToken).ConfigureAwait(false);
                //for now, no masks or secrets.
                string input = Console.ReadLine()!;
                
                bool successful = parses.TryParse(input, out T result, out string? errorMessage);
                if (ShowDefaultValue && successful == false)
                {
                    AnsiConsole.WriteLine();
                    return DefaultValue; //no validation because you are using default value.
                }
                if (successful)
                {
                    if (Validator is not null)
                    {
                        ValidationResult vs = Validator.Invoke(result);
                        if (vs.Successful)
                        {
                            AnsiConsole.WriteLine();
                            return result;
                            //this means 
                        }
                        if (string.IsNullOrEmpty(vs.Message))
                        {
                            throw new CustomBasicException("Error message cannot be blank or null");
                        }
                        //validation failed.
                        Text text = new(vs.Message, ValidationErrorStyle);
                        AnsiConsole.WriteLine();
                        AnsiConsole.Write(text);
                        AnsiConsole.WriteLine();
                        WritePrompt();
                        continue;
                    }
                    Console.WriteLine();
                    return result;
                }
                else
                {
                    Text text = new(ValidationErrorMessage, ValidationErrorStyle);
                    AnsiConsole.WriteLine();
                    AnsiConsole.Write(text);
                    AnsiConsole.WriteLine();
                    WritePrompt();
                }
            }
        }).ConfigureAwait(false) ?? throw new CustomBasicException("Cannot be null for showing");
        return output;
    }

    /// <summary>
    /// Writes the prompt to the console.
    /// </summary>
    private void WritePrompt()
    {
        var promptStyle = PromptStyle ?? Style.Plain;
        var builder = new StringBuilder();
        builder.Append(prompt.TrimEnd());
        var markup = builder.ToString().Trim();
        markup = $"{markup} ";
        Text text = new(markup, promptStyle);
        AnsiConsole.Write(text);
    }
}