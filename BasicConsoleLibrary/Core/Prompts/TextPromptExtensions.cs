namespace BasicConsoleLibrary.Core.Prompts;

/// <summary>
/// Contains extension methods for <see cref="TextPrompt{T}"/>.
/// </summary>
public static class TextPromptExtensions
{
    
    /// <summary>
    /// Sets the prompt style.
    /// </summary>
    /// <typeparam name="T">The prompt result type.</typeparam>
    /// <param name="obj">The prompt.</param>
    /// <param name="style">The prompt style.</param>
    /// <returns>The same instance so that multiple calls can be chained.</returns>
    public static TextPrompt<T> PromptStyle<T>(this TextPrompt<T> obj, Style style)
        where T : notnull
    {
        ArgumentNullException.ThrowIfNull(obj);
        ArgumentNullException.ThrowIfNull(style);
        obj.PromptStyle = style;
        return obj;
    }

    


    /// <summary>
    /// Show or hide the default value.
    /// </summary>
    /// <typeparam name="T">The prompt result type.</typeparam>
    /// <param name="obj">The prompt.</param>
    /// <param name="show">Whether or not the default value should be visible.</param>
    /// <returns>The same instance so that multiple calls can be chained.</returns>
    public static TextPrompt<T> ShowDefaultValue<T>(this TextPrompt<T> obj, bool show)
        where T : notnull
    {
        ArgumentNullException.ThrowIfNull(obj);
        obj.ShowDefaultValue = show;
        return obj;
    }

    /// <summary>
    /// Shows the default value.
    /// </summary>
    /// <typeparam name="T">The prompt result type.</typeparam>
    /// <param name="obj">The prompt.</param>
    /// <returns>The same instance so that multiple calls can be chained.</returns>
    public static TextPrompt<T> ShowDefaultValue<T>(this TextPrompt<T> obj)
        where T : notnull
    {
        return obj.ShowDefaultValue(true);
    }

    /// <summary>
    /// Hides the default value.
    /// </summary>
    /// <typeparam name="T">The prompt result type.</typeparam>
    /// <param name="obj">The prompt.</param>
    /// <returns>The same instance so that multiple calls can be chained.</returns>
    public static TextPrompt<T> HideDefaultValue<T>(this TextPrompt<T> obj)
        where T : notnull
    {
        return obj.ShowDefaultValue(false);
    }

    /// <summary>
    /// Sets the validation error message for the prompt.
    /// </summary>
    /// <typeparam name="T">The prompt result type.</typeparam>
    /// <param name="obj">The prompt.</param>
    /// <param name="message">The validation error message.</param>
    /// <returns>The same instance so that multiple calls can be chained.</returns>
    public static TextPrompt<T> ValidationErrorMessage<T>(this TextPrompt<T> obj, string message)
        where T : notnull
    {
        ArgumentNullException.ThrowIfNull(obj);
        obj.ValidationErrorMessage = message;
        return obj;
    }

    /// <summary>
    /// Sets the default value of the prompt.
    /// </summary>
    /// <typeparam name="T">The prompt result type.</typeparam>
    /// <param name="obj">The prompt.</param>
    /// <param name="value">The default value.</param>
    /// <returns>The same instance so that multiple calls can be chained.</returns>
    public static TextPrompt<T> DefaultValue<T>(this TextPrompt<T> obj, T value)
        where T : notnull
    {
        ArgumentNullException.ThrowIfNull(obj);

        obj.DefaultValue = value;
        return obj;
    }

    /// <summary>
    /// Sets the validation criteria for the prompt.
    /// </summary>
    /// <typeparam name="T">The prompt result type.</typeparam>
    /// <param name="obj">The prompt.</param>
    /// <param name="validator">The validation criteria.</param>
    /// <param name="message">The validation error message.</param>
    /// <returns>The same instance so that multiple calls can be chained.</returns>
    public static TextPrompt<T> Validate<T>(this TextPrompt<T> obj, Func<T, bool> validator, string message)
        where T : notnull
    {
        ArgumentNullException.ThrowIfNull(obj);
        obj.Validator = result =>
        {
            if (validator(result))
            {
                return ValidationResult.Success();
            }
            return ValidationResult.Error(message);
        };

        return obj;
    }

    /// <summary>
    /// Sets the validation criteria for the prompt.
    /// </summary>
    /// <typeparam name="T">The prompt result type.</typeparam>
    /// <param name="obj">The prompt.</param>
    /// <param name="validator">The validation criteria.</param>
    /// <returns>The same instance so that multiple calls can be chained.</returns>
    public static TextPrompt<T> Validate<T>(this TextPrompt<T> obj, Func<T, ValidationResult> validator)
        where T : notnull
    {
        ArgumentNullException.ThrowIfNull(obj);
        obj.Validator = validator;
        return obj;
    }

}