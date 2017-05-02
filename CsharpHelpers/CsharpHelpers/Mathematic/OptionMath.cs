using System;

namespace CsharpHelpers.Mathematic
{
    public static class OptionMath
    {
        public static long CallPrice(double strike, double baseContractPrice, double volatility, int daysleft)
        {
            var nd1 = DeltaCall(strike, baseContractPrice, volatility, daysleft);
            var nd2 = nd1 - 1;

            return (long)Math.Round((baseContractPrice * nd1 - strike * nd2) / 10) * 10;
        }

        public static double NormSDist(double x)
        {
            // constants
            const double a1 = 0.254829592;
            const double a2 = -0.284496736;
            const double a3 = 1.421413741;
            const double a4 = -1.453152027;
            const double a5 = 1.061405429;
            const double p = 0.3275911;

            // Save the sign of x
            int sign = 1;
            if (x < 0)
                sign = -1;
            x = Math.Abs(x) / Math.Sqrt(2.0);

            // A&S formula 7.1.26
            double t = 1.0 / (1.0 + p * x);
            double y = 1.0 - (((((a5 * t + a4) * t) + a3) * t + a2) * t + a1) * t * Math.Exp(-x * x);

            return 0.5 * (1.0 + sign * y);
        }

        public static double DeltaCall(double strike, double baseContractPrice, double volatility, int daysToExpiration)
        {
            var v = volatility / 100.0;
            var t = (daysToExpiration + 1) / 364.0;
            var d1 = (Math.Log(baseContractPrice / strike) + 0.5 * Math.Pow(v, 2) * t) / (v * Math.Sqrt(t));
            return NormSDist(d1);
        }

        public static double DeltaPut(double strike, double baseContractPrice, double volatility, int daysToExpiration)
        {
            return DeltaCall(strike, baseContractPrice, volatility, daysToExpiration) - 1;
        }
    }
}
