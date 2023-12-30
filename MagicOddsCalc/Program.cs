using System.Collections.Generic;
using System.Numerics;
using MagicOddsCalc.Requirements;
using MagicOddsCalc.Tree;
using MagicOddsCalc.Util;
using Microsoft.Extensions.Configuration;

namespace MagicOddsCalc
{
    class Program
    {
        static void Main(string[] args)
        {
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
            CsvValidator Validator = new CsvValidator(Const.REQUIREMENTS_FILE);
            TreeParser tp = new TreeParser(Validator);
            List<Node> PassingNodes = tp.CheckTree(Tree);

            Fraction Total = new Fraction(0, 1);
            Console.WriteLine("----------------------------------------------------------------");
            foreach (Node N in PassingNodes)
            {
                Console.WriteLine(N.CardsDrawn + " - " + N.Odds);
                Total += N.Odds;
            }

            Console.WriteLine(Total.ToString());
        }
    }
}