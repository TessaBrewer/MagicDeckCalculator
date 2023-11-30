using System.Collections.Generic;
using System.Numerics;

namespace MagicOddsCalc
{
    class Program
    {
        private static readonly string TURN_FILE_NAME = "./Turns.txt";
        static void Main(string[] args)
        {
            //Check that we have an even number of args
            if(args.Length % 2 == 0)
            {
                throw new Exception("Must have an odd number of args");
            }

            int TreeDepth = int.Parse(args[0]);

            //Build the symbol : count map
            Dictionary<char, int> SymbolCounts = new Dictionary<char, int>();
            for (int i = 1; i < args.Length; i += 2)
            {
                char Symbol = char.Parse(args[i]);
                int Count = int.Parse(args[i + 1]);
                SymbolCounts.Add(Symbol, Count);
            }

            //Build root node
            Node Tree = new Node(SymbolCounts);
            Tree.Spawn(TreeDepth);

            //Print!
            //Tree.PrintPretty("", false);

            //Parse turn file
            TurnParser tp = new TurnParser(TURN_FILE_NAME);
            tp.DebugPrint();
        }
    }
}