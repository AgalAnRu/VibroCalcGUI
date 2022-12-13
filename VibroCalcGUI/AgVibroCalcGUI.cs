using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VibroCalcGUI
{
    internal static class AgVibroCalcGUI
    {
        private static int startPositionX = 5;
        private static int startPositionY = 1;
        private static int cursorPositionX = startPositionX;
        private static int cursorPositionY = startPositionY;
        const int LINE_OFFSET = 3;
        internal static void PrintTemplateGUI()
        {
            Console.CursorLeft = startPositionX;
            Console.CursorTop = startPositionY;
            Console.Write("Вводные данные:");
            TabCursorPosition(0, 2 * LINE_OFFSET);
            Console.Write("Результат расчёта:");
            TabCursorPosition(0, 1);
            PrintGorisontalLine(length: 20);
            TabCursorPosition(0, 2);
            Console.Write("Ускорение, м/с2:");
            TabCursorPosition(0, LINE_OFFSET);
            Console.Write("Скорость, м/с:");
            TabCursorPosition(0, LINE_OFFSET);
            Console.Write("Перемещение, мкм:");
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
    }
}
