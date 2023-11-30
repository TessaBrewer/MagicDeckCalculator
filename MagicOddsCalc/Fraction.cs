using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MagicOddsCalc
{
    internal class Fraction
    {
        private long Numerator;
        private long Denominator;
        public Fraction() 
        {
            this.Numerator = 1;
            this.Denominator = 1;
        }

        public Fraction(int n, int d)
        {
            this.Numerator = n; 
            this.Denominator = d;
            this.Simplify();
        }

        public Fraction(long n, long d)
        {
            this.Numerator = n;
            this.Denominator = d;
            this.Simplify();
        }

        public static Fraction operator *(Fraction a, Fraction b)
        {
            Fraction NewFraction = new Fraction(a.Numerator * b.Numerator, a.Denominator * b.Denominator);
            NewFraction.Simplify();
            return NewFraction;
        }

        public void Simplify()
        {
            long GCD = CalcGCD();

            Numerator = Numerator / GCD;
            Denominator = Denominator / GCD;
        }

        private long CalcGCD()
        {
            return CalcGCD(Denominator, Numerator);
        }

        private long CalcGCD(long p, long q)
        {
            if (q == 0)
            {
                return p;
            }

            long r = p % q;

            return CalcGCD(q, r);
        }

        public string ToString()
        {
            return Numerator + " / " + Denominator;
        }
    }
}
