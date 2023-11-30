using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicOddsCalc
{
    internal class TurnParser
    {
        private string FileContents;
        private string[] Turns;

        public TurnParser(string FilePath)
        {
            using (StreamReader sr = new StreamReader(FilePath))
            {
                FileContents = sr.ReadToEnd();
            }

            Turns = FileContents.Split('\n');
        }

        public void DebugPrint()
        {
            foreach (string Turn in Turns)
            {
                Console.WriteLine("LINE v");
                Console.WriteLine(Turn);
            }
        }
    }
}
