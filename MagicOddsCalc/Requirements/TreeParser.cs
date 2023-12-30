using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MagicOddsCalc.Tree;

namespace MagicOddsCalc.Requirements
{
    internal class TreeParser
    {
        IDrawValidator Validator;

        public TreeParser(IDrawValidator Validator)
        {
            this.Validator = Validator;
        }

        public List<Node> CheckTree(Node Root)
        {
            bool Pass = CheckNode(Root);
            bool Terminal = Root.CardsDrawn.Length >= Validator.TurnCount;

            if (Pass)
            {
                if (Terminal)
                {
                    //Terminal Portion for Passing Nodes
                    List<Node> list = new List<Node>();
                    list.Add(Root);
                    return list;
                }
                else
                {
                    //Recursive Portion
                    List<Node> Output = new List<Node>();
                    foreach (Node Child in Root.Children)
                    {
                        List<Node> ChildrenPassed = CheckTree(Child);
                        ChildrenPassed.ForEach(x => Output.Add(x));
                    }
                    return Output;
                }
            }
            //Terminal Portion for Failing Nodes
            return new List<Node>();
        }

        public bool CheckNode(Node N)
        {
            Dictionary<string, int> Draws = new Dictionary<string, int>();

            foreach (char c in N.CardsDrawn)
            {
                string s = c.ToString();

                if (Draws.ContainsKey(s))
                {
                    Draws[s]++;
                }
                else
                {
                    Draws[s] = 1;
                }
            }

            if (Const.DEBUG_MODE)
            {
                Console.WriteLine("-----------------------\n" + N.CardsDrawn);
                foreach (KeyValuePair<string, int> p in Draws)
                    Console.WriteLine(p.Key + " --- " + p.Value);
            }

            return Validator.Check(N.CardsDrawn.Length - 1, Draws);
        }

        public void DebugPrint()
        {
            //TODO
        }
    }
}
