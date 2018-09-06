using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace friis
{
    class CzytajCV
    {

        public static List<friis.pkt> CzytajPlikCV(string nazwaPliku)
        {

            var pola = new List<Tuple<double, double>>();
            try
            {

                string[] wiersz = File.ReadAllLines(nazwaPliku);
                List<pkt> punkty = new List<pkt>();
                    foreach (string linia in wiersz)

                    {
                        string[] podzielona = linia.Split(';');

                        if (podzielona.Count() > 1)

                        {
                            double liczba1 = double.Parse(podzielona[0].Trim());
                            double liczba2 = double.Parse(podzielona[1].Trim());

                        pkt punkt = new pkt(liczba1, liczba2);

                        punkty.Add(punkt);

                    }
                    }
                return punkty;
                
            }
            catch (Exception e)
            {

                MessageBox.Show("Błąd odczytu pliku " + nazwaPliku + "\nOpis wyjątku: " + e.Message, "Błą przy wczytywaniu pliku", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;

            }

        }

        
    }
}
