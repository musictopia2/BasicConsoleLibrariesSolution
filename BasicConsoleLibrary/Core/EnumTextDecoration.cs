namespace BasicConsoleLibrary.Core;
[Flags]
public enum EnumTextDecoration
{
    None = 0,
    Italic = 1 << 0, // 1
    Underline = 1 << 1, // 2
    DoubleUnderline = 1 << 2,
    Dim = 1 << 3, // 4
    Bold = 1 << 4, // 8
    Strikethrough = 1 << 5, // 16
    Blink = 1 << 6, // 32
    Invert = 1 << 7, // 64
    Conceal = 1 << 8, // 128
    Overline = 1 << 9, // 256
}