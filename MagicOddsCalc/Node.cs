using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicOddsCalc
{
    internal class Node
    {
        private Dictionary<char, int> Deck;
        private string CardsDrawn;
        private double Odds;
        private List<Node> Children = new List<Node>();

        public Node(Dictionary<char, int> Deck)
        {
            Odds = 1.0;

            CardsDrawn = "";

            this.Deck = Deck;
        }

        public Node(Dictionary<char, int> PrevDeck, string PreviousDraws, char TopCard, double Odds)
        {
            this.Odds = Odds;

            CardsDrawn = PreviousDraws + TopCard;

            int CategoryCount = PrevDeck[TopCard];
            int NewCount = CategoryCount - 1;

            Deck = PrevDeck.ToDictionary(e => e.Key, e => e.Value);
            Deck[TopCard] = NewCount;
        }

        public void Spawn(int DepthRemaining)
        {
            int CardsInDeck = 0;
            foreach (KeyValuePair<char, int> kv in Deck) { CardsInDeck += kv.Value; }

            foreach (KeyValuePair <char, int> kv in Deck)
            {
                if (kv.Value <= 0)
                    continue;

                double NewOdds = (double)kv.Value / CardsInDeck;
                NewOdds = Odds * NewOdds;
                Node Child = new Node(Deck, CardsDrawn, kv.Key, NewOdds);
                Children.Add(Child);
            }

            if (DepthRemaining > 0) { foreach (Node Child in Children) { Child.Spawn(DepthRemaining - 1); } }
        }

        public void PrintPretty(string indent, bool last)
        {
            Console.Write(indent);
            if (last)
            {
                Console.Write("\\-");
                indent += "  ";
            } 
            else
            {
                Console.Write("|-");
                indent += "| ";
            }
            Console.WriteLine(CardsDrawn + " : " + Odds);

            for (int i = 0; i < Children.Count; i++)
            {
                Children[i].PrintPretty(indent, i == Children.Count - 1);
            }
        }
    }
}
