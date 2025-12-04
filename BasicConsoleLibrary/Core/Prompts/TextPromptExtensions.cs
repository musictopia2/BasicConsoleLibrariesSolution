namespace BasicConsoleLibrary.Core.Prompts;

/// <summary>
/// Contains extension methods for <see cref="TextPrompt{T}"/>.
/// </summary>
public static class TextPromptExtensions
{
    extension<T>(TextPrompt<T> obj)
        where T : notnull
    {
        /// <summary>
        /// Sets the prompt style.
        /// </summary>
        /// <typeparam name="T">The prompt result type.</typeparam>
        /// <param name="style">The prompt style.</param>
        /// <returns>The same instance so that multiple calls can be chained.</returns>
        public TextPrompt<T> PromptStyle(Style style)
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
        /// <param name="show">Whether or not the default value should be visible.</param>
        /// <returns>The same instance so that multiple calls can be chained.</returns>
        public TextPrompt<T> ShowDefaultValue(bool show)
        {
            ArgumentNullException.ThrowIfNull(obj);
            obj.ShowDefaultValue = show;
            return obj;
        }

        /// <summary>
        /// Shows the default value.
        /// </summary>
        /// <typeparam name="T">The prompt result type.</typeparam>
        /// <returns>The same instance so that multiple calls can be chained.</returns>
        public TextPrompt<T> ShowDefaultValue()
        {
            return obj.ShowDefaultValue(true);
        }

        /// <summary>
        /// Hides the default value.
        /// </summary>
        /// <typeparam name="T">The prompt result type.</typeparam>
        /// <returns>The same instance so that multiple calls can be chained.</returns>
        public TextPrompt<T> HideDefaultValue()
        {
            return obj.ShowDefaultValue(false);
        }

        /// <summary>
        /// Sets the validation error message for the prompt.
        /// </summary>
        /// <typeparam name="T">The prompt result type.</typeparam>
        /// <param name="message">The validation error message.</param>
        /// <returns>The same instance so that multiple calls can be chained.</returns>
        public TextPrompt<T> ValidationErrorMessage(string message)
        {
            ArgumentNullException.ThrowIfNull(obj);
            obj.ValidationErrorMessage = message;
            return obj;
        }

        /// <summary>
        /// Sets the default value of the prompt.
        /// </summary>
        /// <typeparam name="T">The prompt result type.</typeparam>
        /// <param name="value">The default value.</param>
        /// <returns>The same instance so that multiple calls can be chained.</returns>
        public TextPrompt<T> DefaultValue(T value)
        {
            ArgumentNullException.ThrowIfNull(obj);
            obj.DefaultValue = value;
            return obj;
        }

        /// <summary>
        /// Sets the validation criteria for the prompt.
        /// </summary>
        /// <typeparam name="T">The prompt result type.</typeparam>
        /// <param name="validator">The validation criteria.</param>
        /// <param name="message">The validation error message.</param>
        /// <returns>The same instance so that multiple calls can be chained.</returns>
        public TextPrompt<T> Validate(Func<T, bool> validator, string message)
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
        /// <param name="validator">The validation criteria.</param>
        /// <returns>The same instance so that multiple calls can be chained.</returns>
        public TextPrompt<T> Validate(Func<T, ValidationResult> validator)
        {
            ArgumentNullException.ThrowIfNull(obj);
            obj.Validator = validator;
            return obj;
        }
    }
}