using System;

namespace VibroCalcGUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] selected = new int[] { 0, 0 };
            int row;
            int colomn;
            // Пример для использования GUI
            // Элементы меню задаются одно- или двух-мерным массивом строк (любое количество строк и столбцов отличное от нуля)
            // (размер окна консоли в данной версии не учитывается!)
            // Неиспользуемые элементы заполняются пустым значением ("" или string.Empty)
            // Ширина ячеек меню выравнивается по наиболее широкому элементу
            // количество разрядов значений расчёта = ширине элементов меню (для увеличения разрядов добавить в элемент меню пробелы)

            string[,] menuItem = { { "item 0,0", "item 0,1", "",  "" },
                                   { "item 1,0", "item 1,1", "",  "" },
                                   { "", "", "", ""},
                                   { "item 3,0", "item 3,1", "item 3,2","item 3,3"},
                                   { "item 4,0", "item 4,1", "","item 4,3" },
                                   { "item 5,0", "item 5,1", "item 5,2","item 5,3" },
                                   { "","","item 6,2", "item 6,3" } };

            //Массив рассчитанных значений для отправки в GUI:
            double[,] paramValues = new double[menuItem.GetLength(0), menuItem.GetLength(1)];
            string[,] paramValuesString;
            paramValues[3, 0] = 9.807;
            do
            {

                paramValuesString = AgVibroCalcGUI.ConvertToStringArray(paramValues);
                AgVibroCalcGUI.MenuResult resultGUI = AgVibroCalcGUI.Menu(menuItem, paramValuesString);
                row = resultGUI.selectedItem[1];
                colomn = resultGUI.selectedItem[0];
                if (row < 0 || colomn < 0)
                    continue;
                CalculateNewValue(paramValues, row, colomn, resultGUI.newValue);
            }
            while (row >= 0 && colomn >= 0);
        }

        static void CalculateNewValue(double[,] paramValues, int row, int colomn, string newValue)
        {
            if (double.TryParse(newValue, out double value))
                paramValues[row, colomn] = value;
            // Код ниже заменить методом расчёта остальных значений
            // (Как вариант - использовать делегаты с массивом [row,colomn] методов Set и массивом [row,colomn] методов Set)
            if (row != 0 || colomn != 0)
                paramValues[0, 0] = value + 10;
            for (int r = 0; r < paramValues.GetLength(0); r++)
            {
                for (int c = 0; c < paramValues.GetLength(1); c++)
                {
                    if ((r == 0 && c == 0) || (r == row && c == colomn))
                        continue;
                    paramValues[r, c] = paramValues[0, 0] + 10 * r + c;
                }
            }
        }
    }
}
