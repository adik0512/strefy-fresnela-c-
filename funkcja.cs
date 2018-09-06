using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace friis
{
    class funkcja
    {

        public static double liczLambda(double f)
        {

            double lambda = (double)299792458 / f;
            return lambda;
        }

        public static double liczWzglednySpadek(double gt, double gr, double lambda, double d)
        {
           
            double spadek = Math.Log10(gt * gr * Math.Pow(lambda / (4 * Math.PI * d),2));
            return spadek;
        }

        public static System.Numerics.Complex liczWzglednySpadekWielodrogowosc(double gt, double gr, double lambda, double d, double h1, double h2, double f)
        { 
            System.Numerics.Complex x = (1 / (d1(h1, h2, d))) * System.Numerics.Complex.Pow(Math.E, (System.Numerics.Complex.ImaginaryOne * sigma(f, d1(h1, h2, d))));
            System.Numerics.Complex y = (1 / (d2(h1, h2, d))) * System.Numerics.Complex.Pow(Math.E, (System.Numerics.Complex.ImaginaryOne * sigma(f, d2(h1, h2, d))));

            System.Numerics.Complex spadekWielodrogowosc = System.Numerics.Complex.Log10(gt * gr * System.Numerics.Complex.Pow((lambda / (4 * Math.PI)), 2) * System.Numerics.Complex.Pow(System.Numerics.Complex.Abs(x - y), 2));

            return spadekWielodrogowosc;
        }

        public static double d1(double h1, double h2, double d)
        {
            double d1 = Math.Sqrt(Math.Pow(h1 - h2, 2) + Math.Pow(d, 2));
            return d1;
        }

        public static double d2(double h1, double h2, double d)
        {
            double d2 = Math.Sqrt(Math.Pow(h1 + h2, 2) + Math.Pow(d, 2));
            return d2;
        }

        public static double sigma(double f, double d)
        {
            double c = 299792458;
            double sigma = -2 * Math.PI * f * (d / c);
            return sigma;
        }

        public static double CzasDrogiPrzebytej(double t, double droga)
        {   
            double c = 299792458;
            double CzasDrogiPrzebytej = (droga / c);
            return CzasDrogiPrzebytej;
        }

        //lab2________________________________________________________________________
        public static double PromienFresnela(double d1, double d2, double n, double f)
        {
            double PromienFresnela = Math.Sqrt((n*funkcja.liczLambda(f) * d1 * d2) / (d1 + d2));
            return PromienFresnela;
        }

        public static double Tlumienie(double h, double PromienFresnera)
        {
            double Tlumienie = ((-20 * h) / PromienFresnera) + 10;
            return Tlumienie;
        }

        public static double PromienFresneraTlumienie(double d1, double d2, double f)
        {
            double PromienFresnera = (double)17.30 * Math.Sqrt((d1 * d2) / (f * (d1 + d2)));
            return PromienFresnera;
        }


    }
}
