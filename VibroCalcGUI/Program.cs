using System;

namespace VibroCalcGUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] menuItem = { "item1", "item2", "item3", "item4" };
            AgVibroCalcGUI.Menu(menuItem);

            string[,] menuItem2 = { { "item1", "item2" }, { "item3", "item4" } };
            AgVibroCalcGUI.Menu(menuItem2);
            //AgVibroCalcGUI.PrintTemplateGUI();
            //AgVibroCalcGUI.DrawCell(0, 0, 3, 4);
            //Console.ReadKey();
            //AgVibroCalcGUI.PrintResult();
            Console.ReadKey();
            //Console.ReadLine();
        }
    }
}
