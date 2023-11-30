using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicOddsCalc
{
    internal class TurnParser
    {
        private string[] Turns;

        public TurnParser(string FilePath)
        {
            string FileContents;
            using (StreamReader sr = new StreamReader(FilePath))
            {
                FileContents = sr.ReadToEnd();
            }

            Turns = FileContents.Split('\n');
        }

        public int TurnCount => Turns.Length;

        public List<Node> CheckTree(Node Root)
        {
            bool Pass = CheckNode(Root);
            bool Terminal = (Root.CardsDrawn.Length >= Turns.Length);

            if(Pass)
            {
                if(Terminal)
                {
                    //Terminal Portion for Passing Nodes
                    List<Node> list = new List<Node>();
                    list.Add(Root);
                    return list;
                } else
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
            return true; //Short circuit
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
