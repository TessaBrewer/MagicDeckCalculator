﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MagicOddsCalc.Util
{
    internal class Fraction
    {
        private long Numerator;
        private long Denominator;
        public Fraction()
        {
            Numerator = 1;
            Denominator = 1;
        }

        public Fraction(int n, int d)
        {
            Numerator = n;
            Denominator = d;
            Simplify();
        }

        public Fraction(long n, long d)
        {
            Numerator = n;
            Denominator = d;
            Simplify();
        }

        public static Fraction operator *(Fraction a, Fraction b)
        {
            Fraction NewFraction = new Fraction(a.Numerator * b.Numerator, a.Denominator * b.Denominator);
            return NewFraction;
        }

        public static Fraction operator +(Fraction a, Fraction b)
        {
            long NumA = a.Numerator;
            long NumB = b.Numerator;
            long DenA = a.Denominator;
            long DenB = b.Denominator;

            long NewNumA = NumA * DenB;
            long NewDenA = DenA * DenB;

            long NewNumB = NumB * DenA;
            NewNumA += NewNumB;

            return new Fraction(NewNumA, NewDenA);
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

        public override string ToString()
        {
            return Numerator + " / " + Denominator;
        }
    }
}
