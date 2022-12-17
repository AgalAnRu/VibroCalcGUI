using System;
using System.Collections.Generic;
using System.Globalization;

namespace VibroCalcGUI
{
    internal static class AgVibroCalcGUI
    {
        private static int startPositionX = 5;
        private static int startPositionY = 1;
        private static int cursorPositionX = startPositionX;
        private static int cursorPositionY = startPositionY;
        const int LINE_OFFSET = 3;
        const int CELLS_WIDTH = 20;
        const int CELLS_HEIGHT = 1;
        private static List<string> ParamList = new List<string> { "Ускорение (СКЗ), м/с2:", "Скорость, м/с:", "Перемещение, мкм:", "Напряжение, В" };
        private static List<string> Param2List = new List<string> { "СКЗ:", "ПИК:", "ПИК-ПИК:", "дБ:" };
        private static double[,] resultVolume = new double[4, 4];
        private static int[,] resultPositionX = new int[4, 4];
        private static int[,] resultPositionY = new int[4, 4];
        //==========
        // public static List<MenuResult> Result = new List<MenuResult>();

        private static string[,] menuItemArray;
        private static int colomnTotal = 1;
        private static int rowTotal = 1;
        private static int cellWidth = 10;
        private static int cellHeight = 3;
        //private static int offsetX = 10;
        //private static int offsetY = 1;
        //private static int currentPositionX = 5;
        //private static int carrentPositionY = 1;
        private const int SpaceBetweenWordsX = 3;
        private static double[,] resultValue;
        private static string[,] resultValueString;
        private static int[] resultXY;

        internal struct MenuResult
        {
            internal int[] selectedItem;
            internal string newValue;
        }
        internal static MenuResult Menu(string[] itemsName, string[] defaultValues, bool isVerticalMenu = true)
        {
            MenuResult menuResult = new MenuResult();

            if (itemsName.Length != defaultValues.Length)
            {
                Console.WriteLine("Массив пунктов меню не соответствует массиву результатов");
                menuResult.selectedItem[0] = -1;
                menuResult.selectedItem[1] = -1;
                return menuResult;
            }
            if (isVerticalMenu)
            {
                rowTotal = itemsName.Length;
                menuItemArray = new string[rowTotal, 1];
                resultValueString = new string[rowTotal, 1];
                for (int row = 0; row < rowTotal; row++)
                {
                    resultValueString[row, 0] = defaultValues[row].ToString();
                    menuItemArray[row, 0] = itemsName[row];
                }
            }
            if (!isVerticalMenu)
            {
                colomnTotal = itemsName.Length;
                menuItemArray = new string[1, colomnTotal];
                resultValueString = new string[1, colomnTotal];
                for (int colomn = 0; colomn < colomnTotal; colomn++)
                {
                    resultValueString[0, colomn] = defaultValues[colomn].ToString();
                    menuItemArray[0, colomn] = itemsName[colomn];
                }
            }
            PrintTemplateGUI();
            menuResult.selectedItem = GetSelectedItem();
            menuResult.newValue = GetNewValue(menuResult.selectedItem);
            return menuResult;
        }
        //internal static MenuResult Menu(string[,] itemsName, string[,] defaultValues)
        internal static MenuResult Menu(string[,] itemsName, string[,] itemsValues)
        {
            MenuResult menuResult = new MenuResult();
            if (itemsName.GetLength(0) != itemsValues.GetLength(0) || itemsName.GetLength(1) != itemsValues.GetLength(1))
            {
                Console.WriteLine("Массив пунктов меню не соответствует массиву результатов");
                menuResult.selectedItem[0] = -1;
                menuResult.selectedItem[1] = -1;
                return menuResult;
            }
            colomnTotal = itemsName.GetLength(1);
            rowTotal = itemsName.GetLength(0);
            menuItemArray = itemsName;
            resultValueString = itemsValues;
            PrintTemplateGUI();
            menuResult.selectedItem = GetSelectedItem();
            if (menuResult.selectedItem[0]<0|| menuResult.selectedItem[1] < 0)
                return menuResult;
            menuResult.newValue = GetNewValue(menuResult.selectedItem);
            return menuResult;
        }
        private static int[] GetSelectedItem()
        {
            int[] selected = new int[] { 0, 0 };
            int row = 0;
            int colomn = 0;
            ConsoleKey key = new ConsoleKey();
            MoveCursorToResult(row, colomn);
            bool isCursorVisible = Console.CursorVisible;
            Console.CursorVisible = false;
            do
            {
                key = Console.ReadKey(true).Key;
                MoveCursorToResult(row, colomn, cursorHighlighting: false);
                if (key == ConsoleKey.UpArrow)
                {
                    row = MoveToNextUpCell(row, colomn);
                }
                if (key == ConsoleKey.DownArrow)
                {
                    row = MoveToNextDownCell(row, colomn);
                }
                if (key == ConsoleKey.LeftArrow)
                {
                    colomn = MoveToNextLeftCell(row, colomn);
                }
                if (key == ConsoleKey.RightArrow)
                {
                    colomn = MoveToNextRightCell(row, colomn);
                }
                MoveCursorToResult(row, colomn);
                if (key == ConsoleKey.Enter)
                {
                    //MoveCursorToResult(row, colomn, cursorHighlighting: false);

                }
                if (key == ConsoleKey.Escape)
                {
                    colomn = -1;
                    row = -1;
                }
            }
            while (key != ConsoleKey.Escape && key != ConsoleKey.Enter);
            selected[0] = colomn;
            selected[1] = row;
            Console.CursorVisible = isCursorVisible;
            return selected;
        }

        private static string GetNewValue(int[] rowColomn)
        {
            string newValue;
            int row = rowColomn[1];
            int colomn = rowColomn[0];
            int x = startPositionX + colomn * cellWidth + 2;
            int y = startPositionY + row * cellHeight + 2;
            MoveCursorToPositionXY(x, y);
            string resultLine = String.Empty.PadLeft(cellWidth - SpaceBetweenWordsX, ' ');
            Console.Write(resultLine);
            MoveCursorToPositionXY(x, y);
            newValue = Console.ReadLine();
            return newValue;
        }
        private static void SetCellWidth()
        {
            int itemWidth = 0;
            foreach (string item in menuItemArray)
            {
                if (itemWidth < item.Length)
                    itemWidth = item.Length;
                if (cellWidth < itemWidth + SpaceBetweenWordsX)
                    cellWidth = itemWidth + SpaceBetweenWordsX;
            }
        }
        private static void PrintTemplateGUI()
        {
            SetCellWidth();
            for (int row = 0; row < rowTotal; row++)
            {
                for (int colomn = 0; colomn < colomnTotal; colomn++)
                {
                    PrintFormatedCell(row, colomn);
                }
            }
        }
        private static void PrintFormatedCell(int row, int colomn)
        {
            if (menuItemArray[row, colomn] == String.Empty)
                return;
            MoveCursorToCellTopLeft(row, colomn);
            int[] cellTopLeftXY = GetCursorPozitionXY();
            string horizontalLine = "+".PadRight(cellWidth, '-') + "+";
            string menuItemLine = "| " + menuItemArray[row, colomn].PadLeft(cellWidth - SpaceBetweenWordsX, ' ') + " |";
            string resultLine = "| " + resultValueString[row, colomn].PadLeft(cellWidth - SpaceBetweenWordsX, ' ') + " |";
            Console.Write(horizontalLine);
            MoveCursorToPositionXY(cellTopLeftXY[1], cellTopLeftXY[0] + 1);
            Console.Write(menuItemLine);
            MoveCursorToPositionXY(cellTopLeftXY[1], cellTopLeftXY[0] + 2);
            Console.Write(resultLine);
            MoveCursorToPositionXY(cellTopLeftXY[1], cellTopLeftXY[0] + 3);
            Console.WriteLine(horizontalLine);
        }
        private static int MoveToNextLeftCell(int row, int colomn)
        {
            int colomnNext = colomn;
            do
            {
                colomnNext--;
                if (colomnNext < 0)
                    return colomn;
            }
            while (menuItemArray[row, colomnNext] == String.Empty);
            return colomnNext;
        }
        private static int MoveToNextRightCell(int row, int colomn)
        {
            int colomnNext = colomn;
            do
            {
                colomnNext++;
                if (colomnNext == colomnTotal)
                    return colomn;
            }
            while (menuItemArray[row, colomnNext] == String.Empty);
            return colomnNext;
        }
        private static int MoveToNextUpCell(int row, int colomn)
        {
            int rowNext = row;
            do
            {
                rowNext--;
                if (rowNext < 0)
                    return row;
            }
            while (menuItemArray[rowNext, colomn] == String.Empty);
            return rowNext;
        }
        private static int MoveToNextDownCell(int row, int colomn)
        {
            int rowNext = row;
            do
            {
                rowNext++;
                if (rowNext == rowTotal)
                    return row;
            }
            while (menuItemArray[rowNext, colomn] == String.Empty);
            return rowNext;
        }
        private static void MoveCursorToPositionXY(int x, int y)
        {
            Console.CursorLeft = x;
            Console.CursorTop = y;
        }
        private static void MoveCursorToPositionXY(int[] xy)
        {
            Console.CursorLeft = xy[1];
            Console.CursorTop = xy[0];
        }
        private static void MoveCursorToCellTopLeft(int row, int colomn)
        {
            int x = startPositionX + colomn * cellWidth;
            int y = startPositionY + row * cellHeight;
            MoveCursorToPositionXY(x, y);
        }
        private static void MoveCursorToResult(int row, int colomn, bool cursorHighlighting = true)
        {
            int x = startPositionX + colomn * cellWidth + 2;
            int y = startPositionY + row * cellHeight + 2;
            MoveCursorToPositionXY(x, y);
            if (cursorHighlighting)
                InvertColor();
            string resultLine = resultValueString[row, colomn].PadLeft(cellWidth - SpaceBetweenWordsX, ' ');
            Console.Write(resultLine);
            if (cursorHighlighting)
                InvertColor();
        }
        private static int[] GetCursorPozitionXY()
        {
            int[] xy = new int[2];
            xy[0] = Console.CursorTop;
            xy[1] = Console.CursorLeft;
            return xy;
        }
        private static void InvertColor()
        {
            ConsoleColor color = Console.BackgroundColor;
            Console.BackgroundColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
        }
        internal static string[,] ConvertToStringArray(double[,] values)
        {
            string[,] strValues = new string[values.GetLength(0), values.GetLength(1)];
            for (int row = 0; row < values.GetLength(0); row++)
                for (int colomn = 0; colomn < values.GetLength(1); colomn++)
                    strValues[row, colomn] = values[row, colomn].ToString();
            return strValues;
        }
        internal static string[] ConvertToStringArray(double[] values)
        {
            string[] strValues = new string[values.Length];
            for (int i = 0; i < values.Length; i++)
                    strValues[i] = values[i].ToString();
            return strValues;
        }

        //TO_DELETE
        /*
        internal static void _PrintTemplateGUI()
        {
            Console.CursorLeft = startPositionX;
            Console.CursorTop = startPositionY;
            Console.Write("Вводные данные:");
            TabCursorPosition(0, 2 * LINE_OFFSET);
            Console.Write("Результат расчёта:");
            TabCursorPosition(0, 1);
            PrintGorisontalLine(length: 20);
            TabCursorPosition(0, LINE_OFFSET - 1);
            PrintTablePallet();
        }
        private static void PrintTablePallet()
        {
            int x;
            int y;
            for (int row = 0; row < ParamList.Count; row++)
            {
                Console.Write(ParamList[row]);
                TabCursorPosition(0, 1);
                x = Console.CursorLeft;
                y = Console.CursorTop;
                DrawFourCellsTable();
                MoveCursorToPositionXY(x, y);
                for (int colomn = 0; colomn < Param2List.Count; colomn++)
                {
                    MoveCursorToPositionXY((x + 1) + colomn * (CELLS_WIDTH + 1), y + 1);
                    resultPositionX[row, colomn] = Console.CursorLeft + CELLS_WIDTH / 2;
                    resultPositionY[row, colomn] = Console.CursorTop;
                    Console.Write(Param2List[colomn]);
                    //Console.Write($"\t{resultVolume[row, colomn]}");
                }
                MoveCursorToPositionXY(x, y);
                TabCursorPosition(0, LINE_OFFSET);
            }
        }
        private static void GetTableVolumes()
        {
            resultVolume[0, 0] = 1;
            resultVolume[0, 1] = 2;
            resultVolume[0, 2] = 333333;
        }
        internal static void PrintResult()
        {
            string result = String.Empty;
            GetTableVolumes();
            for (int colomn = 0; colomn < Param2List.Count; colomn++)
            {
                for (int row = 0; row < ParamList.Count; row++)
                {
                    MoveCursorToPositionXY(resultPositionX[colomn, row], resultPositionY[colomn, row]);
                    result = resultVolume[colomn, row].ToString();
                    result = result.PadLeft(7);
                    Console.Write(result);
                }
            }
        }
        private static void TabCursorPosition(int offsetX, int offsetY)
        {
            cursorPositionX = cursorPositionX + offsetX;
            cursorPositionY = cursorPositionY + offsetY;
            Console.CursorLeft = cursorPositionX;
            Console.CursorTop = cursorPositionY;
        }

        private static void PrintGorisontalLine(int length)
        {
            for (int i = 0; i < length; i++)
                Console.Write("-");
        }
        private static void DrawCell(int x, int y, int width, int height)
        {
            height++;
            MoveCursorToPositionXY(x, y);
            Console.Write("+");
            for (int i = 0; i < width; i++)
                Console.Write("-");
            Console.Write("+");

            for (int i = 1; i < height; i++)
            {
                MoveCursorToPositionXY(x, y + i);
                Console.Write("|");
                MoveCursorToPositionXY((x + 1) + width, y + i);
                Console.Write("|");
            }
            MoveCursorToPositionXY(x, y + height);
            Console.Write("+");
            for (int i = 0; i < width; i++)
                Console.Write("-");
            Console.Write("+");
        }
        internal static void DrawTable(int x, int y, int colomnTotal, int rowTotal, int colomnWidth, int rowHeigt)
        {
            for (int i = 0; i < colomnTotal; i++)
            {
                for (int j = 0; j < rowTotal; j++)
                {
                    DrawCell(x + i * colomnWidth + i, y + j * rowHeigt + j, colomnWidth, rowHeigt);
                }
            }
        }
        private static void DrawFourCellsTable(int x, int y)
        {
            DrawTable(x, y, colomnTotal: 4, rowTotal: 1, colomnWidth: CELLS_WIDTH, rowHeigt: CELLS_HEIGHT);
        }
        private static void DrawFourCellsTable()
        {
            int x = Console.CursorLeft;
            int y = Console.CursorTop;
            DrawTable(x, y, colomnTotal: 4, rowTotal: 1, colomnWidth: CELLS_WIDTH, rowHeigt: CELLS_HEIGHT);
        }
        */
    }
}
