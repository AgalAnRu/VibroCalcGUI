using System;

namespace VibroCalcGUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //string[] menuItem = { "0123456789012345", "item1", "item2", "item3", "item4" };
            //string[] menuItem = { "0123456789012345" };

            string[,] menuItem = { { "item 0,0", "item 0,1", "",  "" },
                                   { "item 1,0", "item 1,1", "",  "" },
                                   { "", "", "", ""},
                                   { "Ускорение (СКЗ), м/с2:", "item 3,1", "item 3,2",    "item 3,3"},
                                   { "0123456789012345678901", "item 4,1", "", "" } };
            double[,] defaultValues = new double[menuItem.GetLength(0), menuItem.GetLength(1)];
            defaultValues[3, 0] = 9.807;
            //int menuItem=1
            do
            {
                AgVibroCalcGUI.MenuResult resultGUI = AgVibroCalcGUI.Menu(menuItem, defaultValues);

                Console.WriteLine(resultGUI.selectedItem);
                Console.WriteLine(resultGUI.newValue);


            }
            while (Console.ReadKey().Key != ConsoleKey.Escape);

            //Console.ReadLine();
            //AgVibroCalcGUI.Menu(menuItem);
            //AgVibroCalcGUI.PrintTemplateGUI();
            //AgVibroCalcGUI.DrawCell(0, 0, 3, 4);
            //Console.ReadKey();
            //AgVibroCalcGUI.PrintResult();
            //Console.ReadLine();
        }
    }
}
