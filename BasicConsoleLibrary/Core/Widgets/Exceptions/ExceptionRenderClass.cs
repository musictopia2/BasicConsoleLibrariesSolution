namespace BasicConsoleLibrary.Core.Widgets.Exceptions;
public partial class ExceptionRenderClass(Exception ex, ExceptionSettings settings) : JustInTimeRenderable
{
    private static readonly Regex _stackTraceRegex = ExceptionExpression();
    protected override IRenderable Build()
    {
        string exceptionString = ex.ToString();
        StyledTextBlock builder = new();
        if (string.IsNullOrEmpty(exceptionString))
        {
            builder.Add("Exception is empty", settings.Style.NonEmphasized);
            builder.Add(AnsiCodeHelper.AnsiReset);
            builder.Add(Constants.VBCrLf);
            return builder;
        }
        var lines = exceptionString.Split([Environment.NewLine], StringSplitOptions.None);
        if (lines.Length == 0)
        {
            builder.Add(exceptionString, settings.Style.NonEmphasized);
            builder.Add(AnsiCodeHelper.AnsiReset);
            builder.Add(Constants.VBCrLf);
            return builder;
        }
        WriteExceptionHeader(lines[0], builder);
        if (settings.Format.Has(EnumExceptionFormats.NoStackTrace))
        {
            builder.Add(AnsiCodeHelper.AnsiReset);
            builder.Add(Constants.VBCrLf);
            return builder;
        }
        bool isFirstStackTraceLine = true;
        for (int i = 1; i < lines.Length; i++)
        {
            string originalLine = lines[i];
            string trimmedLine = originalLine.TrimStart();
            if (trimmedLine.StartsWith("---"))
            {
                // Inner exception delimiter, indent 2 spaces, red color
                builder.Add($"  {trimmedLine}", settings.Style.Message);
            }
            else if (trimmedLine.StartsWith("at "))
            {
                var match = _stackTraceRegex.Match(trimmedLine);
                if (match.Success)
                {
                    // Indentation
                    string indent = isFirstStackTraceLine ? "  " : "     ";
                    isFirstStackTraceLine = false;
                    builder.Add($"{indent} at ", settings.Style.Dimmed);

                    string fullMethod = match.Groups["method"].Value;
                    int lastDotIndex = fullMethod.LastIndexOf('.');
                    string classPart = lastDotIndex >= 0 ? fullMethod.Substring(0, lastDotIndex + 1) : "";
                    string methodPart = lastDotIndex >= 0 ? fullMethod.Substring(lastDotIndex + 1) : fullMethod;
                    if (settings.Format.Has(EnumExceptionFormats.ShortenMethods) == false)
                    {
                        builder.Add(classPart, settings.Style.NonEmphasized);
                    }
                    builder.Add(methodPart, settings.Style.Method);
                    

                    // Parameters - split type and name, color separately
                    string parameters = match.Groups["params"].Value;
                    builder.Add("(", settings.Style.Parenthesis);
                    var paramList = parameters.Split([','], StringSplitOptions.RemoveEmptyEntries);
                    for (int p = 0; p < paramList.Length; p++)
                    {
                        string param = paramList[p].Trim();
                        var parts = param.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                        if (parts.Length == 0)
                        {
                            continue;
                        }

                        // Parameter name is last part
                        string paramName = parts[^1];
                        // Parameter type is all parts except last
                        string paramType = string.Join(" ", parts.Take(parts.Length - 1));

                        // Write type
                        builder.Add($"{paramType} ", settings.Style.ParameterType);
                        builder.Add(paramName, settings.Style.ParameterName);


                        if (p < paramList.Length - 1)
                        {
                            builder.Add(", ", settings.Style.NonEmphasized);
                        }
                    }
                    builder.Add(")", settings.Style.Parenthesis);
                    string inText;
                    if (settings.Format.Has(EnumExceptionFormats.ShortenPaths) == false)
                    {
                        inText = " in";
                    }
                    else
                    {
                        inText = " in ";
                    }
                    builder.Add(inText, settings.Style.Dimmed);

                    // If "in ..." file info exists, write it on the next line, indented more
                    if (match.Groups["file"].Success && !string.IsNullOrEmpty(match.Groups["file"].Value))
                    {
                        if (settings.Format.Has(EnumExceptionFormats.ShortenPaths) == false)
                        {
                            builder.AddLineBreak();
                        }

                        if (settings.Format.Has(EnumExceptionFormats.ShortenPaths) == false)
                        {
                            builder.Add("    "); //indent
                        }

                        string fullPath = match.Groups["file"].Value;
                        string fileName = Path.GetFileName(fullPath);
                        string directory = fullPath.Substring(0, fullPath.Length - fileName.Length);
                        if (settings.Format.Has(EnumExceptionFormats.ShortenPaths) == false)
                        {
                            builder.Add(directory, settings.Style.NonEmphasized);
                        }

                        builder.Add(fileName, settings.Style.Path);
                        builder.Add(":", settings.Style.Dimmed);
                        builder.Add(match.Groups["line"].Value, settings.Style.LineNumber);
                    }
                    else
                    {
                        builder.AddLineBreak();
                    }
                }
                else
                {
                    // If regex doesn't match, just print the line dimmed with indentation
                    builder.Add($"  {trimmedLine}", settings.Style.Dimmed);
                }
            }
            else
            {
                builder.Add($"     {trimmedLine}", settings.Style.NonEmphasized);
                builder.AddLineBreak(); //i think here.
            }
        }
        builder.Add(AnsiCodeHelper.AnsiReset);
        builder.Add(Constants.VBCrLf);
        return builder;
    }
    private void WriteExceptionHeader(string exceptionLine, StyledTextBlock builder)
    {
        var parts = exceptionLine.Split([": "], 2, StringSplitOptions.None);
        if (parts.Length < 2)
        {
            builder.Add(exceptionLine);
            return;
        }
        var fullTypeName = parts[0];
        var message = parts[1];
        var lastDot = fullTypeName.LastIndexOf('.');
        string namespacePart = lastDot >= 0 ? fullTypeName.Substring(0, lastDot + 1) : "";
        string className = lastDot >= 0 ? fullTypeName.Substring(lastDot + 1) : fullTypeName;
        if (settings.Format.Has(EnumExceptionFormats.ShortenTypes) == false)
        {
            builder.Add(namespacePart, settings.Style.NonEmphasized);
        }
        builder.Add($"{className}: ", settings.Style.Exception);
        builder.Add(message, settings.Style.Message);
        builder.AddLineBreak();
    }
    [GeneratedRegex(@"^\s*at\s+(?<method>[^(]+)\((?<params>[^\)]*)\)(?:\s+in\s+(?<file>.+):line\s+(?<line>\d+))?", RegexOptions.Compiled)]
    private static partial Regex ExceptionExpression();
}