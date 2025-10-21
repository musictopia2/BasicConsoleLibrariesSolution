namespace BasicConsoleLibrary.Core.Internal;
internal static class SimpleCharWidth
{
    public static int GetWidth(char ch)
    {
        if (ch < 32 || (ch >= 0x7F && ch < 0xA0))
        {
            return -1; // Control characters, non-printable
        }

        // Basic example: treat CJK Unified Ideographs as width 2
        if (ch >= 0x4E00 && ch <= 0x9FFF)
        {
            return 2;
        }
        return 1; // Default to width 1
    }
}