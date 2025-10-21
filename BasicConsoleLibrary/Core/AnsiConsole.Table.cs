namespace BasicConsoleLibrary.Core;
public partial class AnsiConsole
{
    /// <summary>
    /// Writes a simple paged table to the console with horizontal and vertical scrolling support.
    /// This method does not render any borders or advanced styling, focusing on minimal and efficient output.
    /// Intended for scenarios with many columns and rows where a lightweight table display is sufficient.
    /// </summary>
    /// <typeparam name="T">The type of items to display in the table.</typeparam>
    /// <param name="list">The list of items to display.</param>
    /// <param name="enablePaging">Whether to enable paging (row-wise navigation).</param>
    /// <param name="enableHorizontalScroll">Whether to enable horizontal scrolling for columns.</param>
    /// <param name="pageSize">The number of rows to display per page when paging is enabled.</param>
    public static void WriteSimplePagedTable<T>(BasicList<T> list, bool enablePaging = false, bool enableHorizontalScroll = false, int pageSize = 20)
    {
        System.Console.OutputEncoding = Encoding.UTF8;
        var table =
            FlatDataHelpers<T>.MasterContext ?? throw new InvalidOperationException("Flat data provider is not registered.");
        int columnCount = table.ColumnCount;

        // Step 1: Get headers and initialize widths
        BasicList<string> headers = [];
        BasicList<int> colWidths = [];

        for (int i = 0; i < columnCount; i++)
        {
            var header = table.GetHeader(i);
            headers.Add(header);
            colWidths.Add(header.Length);
        }

        // Step 2: Adjust column widths based on data
        foreach (var item in list)
        {
            for (int i = 0; i < columnCount; i++)
            {
                var value = table.GetValue(item, i);
                if (value.Length > colWidths[i])
                {
                    colWidths[i] = value.Length;
                }
            }
        }

        // Variables for scrolling
        int horizontalOffset = 0;
        int verticalOffset = 0;
        int maxVerticalOffset = Math.Max(0, list.Count - pageSize);

        ConsoleKeyInfo key;

        void PrintHeader(int firstCol, int lastCol)
        {
            for (int i = firstCol; i <= lastCol; i++)
            {
                System.Console.Write(headers[i].PadRight(colWidths[i] + 2));
            }
            System.Console.WriteLine();

            for (int i = firstCol; i <= lastCol; i++)
            {
                System.Console.Write(new string('-', colWidths[i]) + "  ");
            }
            System.Console.WriteLine();
        }

        void PrintRows(int firstCol, int lastCol, int startRow, int rowsToPrint)
        {
            int endRow = Math.Min(startRow + rowsToPrint, list.Count);
            for (int rowIndex = startRow; rowIndex < endRow; rowIndex++)
            {
                var item = list[rowIndex];
                for (int colIndex = firstCol; colIndex <= lastCol; colIndex++)
                {
                    var value = table.GetValue(item, colIndex);
                    System.Console.Write(value.PadRight(colWidths[colIndex] + 2));
                }
                System.Console.WriteLine();
            }
        }

        (int firstCol, int lastCol) GetVisibleColumns()
        {
            int consoleWidth = System.Console.WindowWidth;
            int currentWidth = 0;
            int firstCol = horizontalOffset;
            int lastCol = firstCol;

            for (int i = firstCol; i < columnCount; i++)
            {
                int colWidth = colWidths[i] + 2;
                if (currentWidth + colWidth > consoleWidth)
                {
                    break;
                }
                currentWidth += colWidth;
                lastCol = i;
            }
            return (firstCol, lastCol);
        }

        do
        {
            System.Console.Clear();

            int firstCol = 0;
            int lastCol = columnCount - 1;

            if (enableHorizontalScroll)
            {
                (firstCol, lastCol) = GetVisibleColumns();
            }

            int rowsToPrint = enablePaging ? pageSize : list.Count;
            int startRow = enablePaging ? verticalOffset : 0;

            PrintHeader(firstCol, lastCol);
            PrintRows(firstCol, lastCol, startRow, rowsToPrint);

            // Footer with ASCII instructions
            string footer;
            if (enablePaging && enableHorizontalScroll)
            {
                int currentPage = verticalOffset / pageSize + 1;
                int pageCount = (list.Count + pageSize - 1) / pageSize;
                footer = $"Page {currentPage}/{pageCount}, Columns {firstCol + 1}-{lastCol + 1}/{columnCount} - PgUp/PgDn to scroll page, Up/Down to scroll row, Left/Right to scroll column, Esc to exit.";
            }
            else if (enablePaging)
            {
                int currentPage = verticalOffset / pageSize + 1;
                int pageCount = (list.Count + pageSize - 1) / pageSize;
                footer = $"Page {currentPage}/{pageCount} - PgUp/PgDn to scroll page, Up/Down to scroll row, Esc to exit.";
            }
            else if (enableHorizontalScroll)
            {
                footer = $"Columns {firstCol + 1}-{lastCol + 1}/{columnCount} - Left/Right to scroll column, Esc to exit.";
            }
            else
            {
                footer = "Esc to exit.";
            }

            System.Console.WriteLine();
            System.Console.WriteLine(footer);

            key = System.Console.ReadKey(true);

            if (enablePaging)
            {
                switch (key.Key)
                {
                    case ConsoleKey.PageDown:
                        verticalOffset = Math.Min(verticalOffset + pageSize, maxVerticalOffset);
                        break;
                    case ConsoleKey.PageUp:
                        verticalOffset = Math.Max(verticalOffset - pageSize, 0);
                        break;
                    case ConsoleKey.DownArrow:
                        verticalOffset = Math.Min(verticalOffset + 1, maxVerticalOffset);
                        break;
                    case ConsoleKey.UpArrow:
                        verticalOffset = Math.Max(verticalOffset - 1, 0);
                        break;
                }
            }

            if (enableHorizontalScroll)
            {
                switch (key.Key)
                {
                    case ConsoleKey.RightArrow:
                        if (horizontalOffset < columnCount - 1)
                        {
                            horizontalOffset++;
                        }

                        break;
                    case ConsoleKey.LeftArrow:
                        if (horizontalOffset > 0)
                        {
                            horizontalOffset--;
                        }

                        break;
                }
            }

        } while (key.Key != ConsoleKey.Escape);
    }
}