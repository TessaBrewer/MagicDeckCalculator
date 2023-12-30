using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicOddsCalc.Requirements
{
    public interface IDrawValidator
    {
        public int TurnCount { get; }
        public bool Check(int DrawNumber, Dictionary<String, int> Draws);
    }
}
