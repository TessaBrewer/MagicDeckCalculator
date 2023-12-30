using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;

namespace MagicOddsCalc.Requirements
{
    public class CsvValidator : IDrawValidator
    {
        private List<Dictionary<string, CsvReq>> Requirements = new List<Dictionary<String, CsvReq>>();
        public int TurnCount => Requirements.Count;

        public CsvValidator(String FilePath) {
            try
            {
                using (TextFieldParser CsvReader = new TextFieldParser(FilePath))
                {
                    CsvReader.SetDelimiters(new String[] { "," });
                    CsvReader.HasFieldsEnclosedInQuotes = false;

                    String[]? Headers = CsvReader.ReadFields();
                    if (Headers == null)
                        throw new ArgumentException("Headers was null");

                    while (!CsvReader.EndOfData)
                    {
                        String[]? DrawReq = CsvReader.ReadFields();
                        if (DrawReq == null)
                            throw new ArgumentException("DrawReq was null");

                        CsvReq[] ParsedReq = Array.ConvertAll(DrawReq, CsvReq.MakeCsvReq);

                        Dictionary<String, CsvReq> DrawReqs = Headers.Zip(ParsedReq, (s, i) => new { s, i }).ToDictionary(item => item.s, item => item.i); //ziiiiiiiiiiiiip

                        Requirements.Add(DrawReqs);
                    }
                }
            }
            catch (Exception _)
            {
                throw;
            }

            int i = 0;
            foreach(Dictionary<string, CsvReq> d in Requirements) {
                i++;

                if (Const.DEBUG_MODE)
                {
                    Console.WriteLine("Draw #" + i + "---------------");
                    foreach (KeyValuePair<string, CsvReq> kvp in d)
                    {
                        Console.WriteLine(kvp.Key + " " + kvp.Value.ToString());
                    }
                }
            }
        }

        public bool Check(int DrawNumber, Dictionary<String, int> Draws) {
            if (DrawNumber == -1)
                return true; //Case where we've drawn no cards

            if (DrawNumber >= Requirements.Count)
                DrawNumber = Requirements.Count - 1;

            Dictionary<String, CsvReq> Reqs = Requirements[DrawNumber];

            foreach (String Category in Reqs.Keys) {
                int Drawn;
                if (!Draws.ContainsKey(Category))
                {
                    Drawn = 0;
                }
                else
                {
                    Drawn = Draws[Category];
                }

                
                bool Check = Reqs[Category].Check(Drawn);

                if(Const.DEBUG_MODE)
                    Console.WriteLine("Check: " + Check + ", " + Reqs[Category].ToString());

                if (!Check)
                    return false;
            }

            return true;
        }
    }

    internal class CsvReq
    {
        private int AcceptanceValue;
        private int Comparison; //-1, 0, 1
        private bool Bypass; //Always pass this test?

        public static CsvReq MakeCsvReq(String Req)
        {
            return new CsvReq(Req);
        }

        private CsvReq(String Req)
        {
            Req = Req.Trim();
            if (Req.Length == 0)
            {
                Bypass = true;
                return;
            }

            if(Req.Length == 1)
                throw new ArgumentException("Illegal CsvReq created with Req.Length == 1");

            char Comparitor = Req[0];
            switch(Comparitor)
            {
                case '>':
                    Comparison = 1;
                    break;
                case '<':
                    Comparison = -1;
                    break;
                case '=':
                    Comparison = 0;
                    break;
                default:
                    throw new ArgumentException(Comparitor + " is not an accepted comparitor");
            }

            AcceptanceValue = int.Parse(Req.Substring(1));
        }

        public bool Check(int Value)
        {
            if (Bypass)
                return true;

            int Relation = Value.CompareTo(AcceptanceValue);

            return (Relation == Comparison);
        }

        public override String ToString()
        {
            return "Bypass: " + Bypass + ", Comparison: " + Comparison + ", Acceptance Value: " + AcceptanceValue;
        }
    }
}
