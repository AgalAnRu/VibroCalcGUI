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
            string[,] menuItem = { { "item 0,0", "item 0,1", "",  "" },
                                   { "item 1,0", "item 1,1", "",  "" },
                                   { "", "", "", ""},
                                   { "Ускорение (СКЗ), м/с2:", "item 3,1", "item 3,2",    "item 3,3"},
                                   { "0123456789012345678901", "item 4,1", "", "" } };
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
                paramValues[row,colomn] = value;
        }
    }
}
