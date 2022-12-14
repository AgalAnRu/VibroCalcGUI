using System;

namespace VibroCalcGUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AgVibroCalcGUI.PrintTemplateGUI();
            //AgVibroCalcGUI.DrawCell(0, 0, 3, 4);
            Console.ReadKey();
            AgVibroCalcGUI.PrintResult();
            Console.ReadKey();
            //Console.ReadLine();
        }
    }
}
