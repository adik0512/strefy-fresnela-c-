using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace friis
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonLab1_Click(object sender, EventArgs e)
        {
            if (textBoxGt.Text == "" || textBoxGt.Text == "" || textBoxF1.Text == "" || textBoxF2.Text == "" || textBoxOdlegloscF1.Text == "" || textBoxOdlegloscF2.Text == "" || textBoxWysokoscH1.Text == "" || textBoxWysokoscH2.Text == "" || textBoxWykres1.Text == "" || textBoxWykres2.Text == "" || textBoxWykres3.Text == "" || textBoxWykres4.Text == "")
            {
                DialogResult dr = MessageBox.Show("Need more data !", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                switch (dr)
                {
                    case DialogResult.OK:
                        break;
                }

            }
            else if (textBoxWykres1.Text == "0" || textBoxWykres2.Text == "0" || textBoxWykres3.Text == "0" || textBoxWykres4.Text == "0")
            {
                DialogResult dr = MessageBox.Show("Próbkowanie = 0 !", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                switch (dr)
                {
                    case DialogResult.OK:
                        break;
                }
            }
            else
            {
                double gt = double.Parse(textBoxGt.Text);
                double gr = double.Parse(textBoxGr.Text);
                double f1 = double.Parse(textBoxF1.Text);
                double f2 = double.Parse(textBoxF2.Text);
                double odlegloscF1 = double.Parse(textBoxOdlegloscF1.Text);
                double odlegloscF2 = double.Parse(textBoxOdlegloscF2.Text);
                double odlegloscOpoznienie = double.Parse(textBoxOdlegloscOpoznienie.Text);
                double wysokoscH1 = double.Parse(textBoxWysokoscH1.Text);
                double wysokoscH2 = double.Parse(textBoxWysokoscH2.Text);
                double ProbkowanieWykres1 = double.Parse(textBoxWykres1.Text);
                double ProbkowanieWykres2 = double.Parse(textBoxWykres2.Text);
                double ProbkowanieWykres3 = double.Parse(textBoxWykres3.Text);
                double ProbkowanieWykres4 = double.Parse(textBoxWykres4.Text);
                double ProbkowanieWykres5 = double.Parse(textBoxWykres5.Text);

                chartF1.Series["F1"].Points.Clear();
                chartF1.Series["F2"].Points.Clear();
                chartF2.Series["F1"].Points.Clear();
                chartF2.Series["F2"].Points.Clear();
                chartF1v2.Series["F1"].Points.Clear();
                chartF1v2.Series["F2"].Points.Clear();
                chartF2v2.Series["F1"].Points.Clear();
                chartF2v2.Series["F2"].Points.Clear();
                chartOpoznienie1.Series["F1"].Points.Clear();

                //zad1_____________________________________________________________________________________
                double dF1Wykres1 = 0.01;
                double dF2Wykres1 = 0.01;
                for (double i = 0; i <= ProbkowanieWykres1; i++)
                {
                    double spadekF1 = funkcja.liczWzglednySpadek(gt, gr, funkcja.liczLambda(f1), dF1Wykres1);
                    double spadekF2 = funkcja.liczWzglednySpadek(gt, gr, funkcja.liczLambda(f2), dF2Wykres1);

                    chartF1.Series["F1"].Points.AddXY(dF1Wykres1, spadekF1);
                    dF1Wykres1 = dF1Wykres1 + (odlegloscF1 / ProbkowanieWykres1);

                    chartF1.Series["F2"].Points.AddXY(dF2Wykres1, spadekF2);
                    dF2Wykres1 = dF2Wykres1 + (odlegloscF1 / ProbkowanieWykres1);
                }

                double dF1Wykres2 = 0.01;
                double dF2Wykres2 = 0.01;
                for (double i = 0; i <= ProbkowanieWykres2; i++)
                {
                    double spadekF1 = funkcja.liczWzglednySpadek(gt, gr, funkcja.liczLambda(f1), dF1Wykres2);
                    double spadekF2 = funkcja.liczWzglednySpadek(gt, gr, funkcja.liczLambda(f2), dF2Wykres2);

                    chartF2.Series["F1"].Points.AddXY(dF1Wykres2, spadekF1);
                    dF1Wykres2 = dF1Wykres2 + (odlegloscF2 / ProbkowanieWykres2);

                    chartF2.Series["F2"].Points.AddXY(dF2Wykres2, spadekF2);
                    dF2Wykres2 = dF2Wykres2 + (odlegloscF2 / ProbkowanieWykres2);
                }
                double OdlegloscOpoznieniaStart = 0.01;
                double t = 0;
                for (int i = 0; i < ProbkowanieWykres5; i++)
                {
                    double droga = funkcja.CzasDrogiPrzebytej(t, OdlegloscOpoznieniaStart);
                    chartOpoznienie1.Series["F1"].Points.AddXY(OdlegloscOpoznieniaStart, droga * 1000);
                    OdlegloscOpoznieniaStart = OdlegloscOpoznieniaStart + (odlegloscOpoznienie / ProbkowanieWykres5);
                    t++;
                }
                //zad 2___________________________________________________________________________________________________________

                double dF1Wykres3 = 0.01;
                double dF2Wykres3 = 0.01;
                for (double i = 0; i <= ProbkowanieWykres3; i++)
                {

                    System.Numerics.Complex spadekF1v2 = funkcja.liczWzglednySpadekWielodrogowosc(gt, gr, funkcja.liczLambda(f1), dF1Wykres3, wysokoscH1, wysokoscH2, f1);
                    System.Numerics.Complex spadekF2v2 = funkcja.liczWzglednySpadekWielodrogowosc(gt, gr, funkcja.liczLambda(f2), dF2Wykres3, wysokoscH1, wysokoscH2, f2);

                    chartF1v2.Series["F1"].Points.AddXY(dF1Wykres3, spadekF1v2.Real);
                    dF1Wykres3 = dF1Wykres3 + (odlegloscF1 / ProbkowanieWykres3);

                    chartF1v2.Series["F2"].Points.AddXY(dF2Wykres3, spadekF2v2.Real);
                    dF2Wykres3 = dF2Wykres3 + (odlegloscF1 / ProbkowanieWykres3);
                }

                double dF1Wykres4 = 0.01;
                double dF2Wykres4 = 0.01;
                for (double i = 0; i <= ProbkowanieWykres4; i++)
                {
                    System.Numerics.Complex spadekF1v2 = funkcja.liczWzglednySpadekWielodrogowosc(gt, gr, funkcja.liczLambda(f1), dF1Wykres4, wysokoscH1, wysokoscH2, f1);
                    System.Numerics.Complex spadekF2v2 = funkcja.liczWzglednySpadekWielodrogowosc(gt, gr, funkcja.liczLambda(f2), dF2Wykres4, wysokoscH1, wysokoscH2, f2);

                    chartF2v2.Series["F1"].Points.AddXY(dF1Wykres4, spadekF1v2.Real);
                    dF1Wykres4 = dF1Wykres4 + (odlegloscF2 / ProbkowanieWykres4);

                    chartF2v2.Series["F2"].Points.AddXY(dF2Wykres4, spadekF2v2.Real);
                    dF2Wykres4 = dF2Wykres4 + (odlegloscF2 / ProbkowanieWykres4);
                }

                chartOpoznienie1.Series["F1"].ChartType = SeriesChartType.Line;
                chartOpoznienie1.Series["F1"].Color = Color.DarkMagenta;
                chartOpoznienie1.ChartAreas["ChartArea1"].AxisX.Minimum = 0;
                chartOpoznienie1.ChartAreas["ChartArea1"].AxisX.Maximum = odlegloscOpoznienie;

                chartF1.Series["F1"].ChartType = SeriesChartType.Line;
                chartF1.Series["F2"].ChartType = SeriesChartType.Line;
                chartF1.Series["F1"].Color = Color.Red;
                chartF1.Series["F2"].Color = Color.Blue;

                chartF2.Series["F1"].ChartType = SeriesChartType.Line;
                chartF2.Series["F2"].ChartType = SeriesChartType.Line;
                chartF2.Series["F1"].Color = Color.Red;
                chartF2.Series["F2"].Color = Color.Blue;

                chartF1v2.Series["F1"].ChartType = SeriesChartType.Line;
                chartF1v2.Series["F2"].ChartType = SeriesChartType.Line;
                chartF1v2.Series["F1"].Color = Color.Red;
                chartF1v2.Series["F2"].Color = Color.Blue;

                chartF2v2.Series["F1"].ChartType = SeriesChartType.Line;
                chartF2v2.Series["F2"].ChartType = SeriesChartType.Line;
                chartF2v2.Series["F1"].Color = Color.Red;
                chartF2v2.Series["F2"].Color = Color.Blue;

                if (radioButtonLogarytmiczna.Checked)
                {
                    chartF1.ChartAreas["ChartArea1"].AxisX.Minimum = 1;
                    chartF1.ChartAreas["ChartArea1"].AxisX.Maximum = odlegloscF1;
                    chartF1.ChartAreas["ChartArea1"].AxisX.IsLogarithmic = true;
                    chartF1.ChartAreas["ChartArea1"].AxisX.LogarithmBase = 10;

                    chartF2.ChartAreas["ChartArea1"].AxisX.Minimum = 1;
                    chartF2.ChartAreas["ChartArea1"].AxisX.Maximum = odlegloscF2;
                    chartF2.ChartAreas["ChartArea1"].AxisX.IsLogarithmic = true;
                    chartF2.ChartAreas["ChartArea1"].AxisX.LogarithmBase = 10;

                    chartF1v2.ChartAreas["ChartArea1"].AxisX.Minimum = 1;
                    chartF1v2.ChartAreas["ChartArea1"].AxisX.Maximum = odlegloscF1;
                    chartF1v2.ChartAreas["ChartArea1"].AxisX.IsLogarithmic = true;
                    chartF1v2.ChartAreas["ChartArea1"].AxisX.LogarithmBase = 10;

                    chartF2v2.ChartAreas["ChartArea1"].AxisX.Minimum = 1;
                    chartF2v2.ChartAreas["ChartArea1"].AxisX.Maximum = odlegloscF2;
                    chartF2v2.ChartAreas["ChartArea1"].AxisX.IsLogarithmic = true;
                    chartF2v2.ChartAreas["ChartArea1"].AxisX.LogarithmBase = 10;
                }
                if (radioButtonLiniowa.Checked)
                {
                    chartF1.ChartAreas["ChartArea1"].AxisX.Minimum = 0;
                    chartF1.ChartAreas["ChartArea1"].AxisX.IsLogarithmic = false;
                    chartF1.ChartAreas["ChartArea1"].AxisX.Maximum = odlegloscF1;
                    chartF1.ChartAreas["ChartArea1"].RecalculateAxesScale();

                    chartF2.ChartAreas["ChartArea1"].AxisX.Minimum = 0;
                    chartF2.ChartAreas["ChartArea1"].AxisX.IsLogarithmic = false;
                    chartF2.ChartAreas["ChartArea1"].AxisX.Maximum = odlegloscF2;
                    chartF2.ChartAreas["ChartArea1"].RecalculateAxesScale();

                    chartF1v2.ChartAreas["ChartArea1"].AxisX.Minimum = 0;
                    chartF1v2.ChartAreas["ChartArea1"].AxisX.IsLogarithmic = false;
                    chartF1v2.ChartAreas["ChartArea1"].AxisX.Maximum = odlegloscF1;
                    chartF1v2.ChartAreas["ChartArea1"].RecalculateAxesScale();

                    chartF2v2.ChartAreas["ChartArea1"].AxisX.Minimum = 0;
                    chartF2v2.ChartAreas["ChartArea1"].AxisX.IsLogarithmic = false;
                    chartF2v2.ChartAreas["ChartArea1"].AxisX.Maximum = odlegloscF2;
                    chartF2v2.ChartAreas["ChartArea1"].RecalculateAxesScale();
                }
            }
        }
        //lab2_______________________________________________________________
        //zad1
        private void buttonOblicz_Click(object sender, EventArgs e)
        {
            double f1lab2 = double.Parse(textBoxF1lab2.Text);

            double OdleglsocMiedzyAntenami = double.Parse(textBoxOdlegloscMiedzyAntenamilab2.Text);
            double WysokoscAnteny1 = double.Parse(textBoxWysokoscAnteny1.Text);
            double WysokoscAnteny2 = double.Parse(textBoxWysokoscAnteny2.Text);
            double WysokoscGruntu = double.Parse(textBoxWysokoscGruntu.Text);
            double Probkowanie = double.Parse(textBoxProbkowanie.Text);
            int StrefaFresnela = int.Parse(numericUpDownStrefaFresnela.Text);

            List<friis.pkt> Punkty = new List<friis.pkt>();
            double SzerokoscPixeli = OdleglsocMiedzyAntenami / Probkowanie;

            for (int i = 0; i < Probkowanie; i++)
            {
                double x = i * SzerokoscPixeli;
                Punkty.Add(new pkt(x, WysokoscGruntu));

            }
            Punkty.Add(new pkt(OdleglsocMiedzyAntenami, WysokoscGruntu));


            chartFresnela1.Series["F1"].Points.Clear();
            chartFresnela1.Series["Antena1"].Points.Clear();
            chartFresnela1.Series["Antena2"].Points.Clear();
            chartFresnela1.Series["Dystans"].Points.Clear();
            chartFresnela1.Series["MinDystans"].Points.Clear();
            chartFresnela1.Series["Dystans"].Points.Clear();
            chartFresnela1.Series["Grunt"].Points.Clear();
            chartFresnela1.ChartAreas["ChartArea1"].AxisX.Minimum = 0;
            chartFresnela1.ChartAreas["ChartArea1"].AxisX.Maximum = OdleglsocMiedzyAntenami;


            for (int i = 0; i < OdleglsocMiedzyAntenami; i++)
            {
                chartFresnela1.Series["Grunt"].Points.AddXY(i, WysokoscGruntu);
            }


            for (int i = 0; i < 25; i++)
            {
                chartFresnela1.Series["Antena1"].Points.AddXY((double)i * (0.5), WysokoscAnteny1 + WysokoscGruntu);
            }

            for (int i = 0; i < 25; i++)
            {
                chartFresnela1.Series["Antena2"].Points.AddXY(OdleglsocMiedzyAntenami - (double)i * (0.5), WysokoscAnteny2 + WysokoscGruntu);
            }


            List<double> PunktyX = new List<double>();
            List<double> PunktyY = new List<double>();
            List<double> PunktyGruntuY = new List<double>();
            double tangens = (WysokoscAnteny2 - WysokoscAnteny1) / (OdleglsocMiedzyAntenami - 0);
            for (int i = 0; i < Punkty.Count; i++)
            {
                double d = Punkty[i].x - Punkty[0].x;
                double h = Punkty[0].y;
                double y = (d * tangens) + h;
                chartFresnela1.Series["Dystans"].Points.AddXY(Punkty[i].x, y + (WysokoscAnteny1));
                PunktyX.Add(Punkty[i].x);
                PunktyY.Add(y + (WysokoscAnteny1));
                PunktyGruntuY.Add(Punkty[i].y);
            }

            double OdlegloscMiedzyAntenamiPrawdziwa = Math.Sqrt((Math.Pow(PunktyX[PunktyX.Count - 1] - PunktyX[0] * SzerokoscPixeli, 2)) + (Math.Pow(PunktyY[PunktyX.Count - 1] * SzerokoscPixeli - PunktyY[0], 2)));

            double minPktX = 0;
            double minPktY = 0;


            double minDystans = 100000;
            chartFresnela1.Series["F1"].YValuesPerPoint = 2;
            for (int i = 0; i < PunktyX.Count; i++)
            {
                double d1 = Math.Sqrt((Math.Pow(PunktyX[i] - PunktyX[0], 2)) + (Math.Pow(PunktyY[i] - PunktyY[0], 2)));
                double d2 = Math.Sqrt((Math.Pow(PunktyX[i] - PunktyX[PunktyX.Count - 1], 2)) + (Math.Pow(PunktyY[i] - PunktyY[PunktyY.Count - 1], 2)));
                double SzerokoscStrefy = Math.Sqrt((StrefaFresnela * funkcja.liczLambda(f1lab2) * d1 * d2) / (d1 + d2));
                chartFresnela1.Series["F1"].Points.AddXY(PunktyX[i], new object[] { PunktyY[i] - SzerokoscStrefy, PunktyY[i] + SzerokoscStrefy });
                double currentDistance = (PunktyY[i] - SzerokoscStrefy) - PunktyGruntuY[i];
                if (currentDistance < minDystans)
                {
                    minDystans = currentDistance;
                    minPktX = PunktyX[i];
                    minPktY = (PunktyY[i] - SzerokoscStrefy);

                }
            }

            chartFresnela1.Series["MinDystans"].Points.AddXY(minPktX, WysokoscGruntu);
            chartFresnela1.Series["MinDystans"].Points.AddXY(minPktX, minPktY);
            chartFresnela1.Series["MinDystans"].Points[1].Label = "Min odległość: " + Math.Round(minDystans, 3) + " m";

            chartFresnela1.Series["F1"].ChartType = SeriesChartType.SplineRange;
            chartFresnela1.Series["Antena1"].ChartType = SeriesChartType.Column;
            chartFresnela1.Series["Antena2"].ChartType = SeriesChartType.Column;

            chartFresnela1.Series["F1"].Color = Color.FromArgb(50, Color.DarkBlue);
            chartFresnela1.Series["Antena1"].Color = Color.Red;
            chartFresnela1.Series["Antena2"].Color = Color.Red;
            chartFresnela1.Series["Grunt"].Color = Color.DarkGreen;
            chartFresnela1.Series["MinDystans"].Color = Color.Blue;
            chartFresnela1.Series["Dystans"].Color = Color.Red;

        }



        private void buttonObliczF2_Click(object sender, EventArgs e)
        {

            double f2lab2 = double.Parse(textBoxF2lab2.Text);
            double OdleglsocMiedzyAntenami = double.Parse(textBoxOdlegloscMiedzyAntenamilab2.Text);
            double WysokoscAnteny1 = double.Parse(textBoxWysokoscAnteny1.Text);
            double WysokoscAnteny2 = double.Parse(textBoxWysokoscAnteny2.Text);
            double WysokoscGruntu = double.Parse(textBoxWysokoscGruntu.Text);
            double Probkowanie = double.Parse(textBoxProbkowanie.Text);
            int StrefaFresnela = int.Parse(numericUpDownStrefaFresnela.Text);

            List<friis.pkt> Punkty = new List<friis.pkt>();
            double PixelWidth = OdleglsocMiedzyAntenami / Probkowanie;

            for (int i = 0; i < Probkowanie; i++)
            {
                double x = i * PixelWidth;
                Punkty.Add(new pkt(x, WysokoscGruntu));

            }
            Punkty.Add(new pkt(OdleglsocMiedzyAntenami, WysokoscGruntu));


            chartFresnela2.Series["F1"].Points.Clear();
            chartFresnela2.Series["Antena1"].Points.Clear();
            chartFresnela2.Series["Antena2"].Points.Clear();
            chartFresnela2.Series["Dystans"].Points.Clear();
            chartFresnela2.Series["MinDystans"].Points.Clear();
            chartFresnela2.Series["Dystans"].Points.Clear();
            chartFresnela2.Series["Grunt"].Points.Clear();
            chartFresnela2.ChartAreas["ChartArea1"].AxisX.Minimum = 0;
            chartFresnela2.ChartAreas["ChartArea1"].AxisX.Maximum = OdleglsocMiedzyAntenami;


            for (int i = 0; i < OdleglsocMiedzyAntenami; i++)
            {
                chartFresnela2.Series["Grunt"].Points.AddXY(i, WysokoscGruntu);
            }


            for (int i = 0; i < 25; i++)
            {
                chartFresnela2.Series["Antena1"].Points.AddXY((double)i * (0.5), WysokoscAnteny1 + WysokoscGruntu);
            }

            for (int i = 0; i < 25; i++)
            {
                chartFresnela2.Series["Antena2"].Points.AddXY(OdleglsocMiedzyAntenami - (double)i * (0.5), WysokoscAnteny2 + WysokoscGruntu);
            }


            List<double> PunktyX = new List<double>();
            List<double> PunktyY = new List<double>();
            List<double> PunktyGruntuY = new List<double>();
            double tangens = (WysokoscAnteny2 - WysokoscAnteny1) / (OdleglsocMiedzyAntenami - 0);
            for (int i = 0; i < Punkty.Count; i++)
            {
                double d = Punkty[i].x - Punkty[0].x;
                double h = Punkty[0].y;
                double y = (d * tangens) + h;
                chartFresnela2.Series["Dystans"].Points.AddXY(Punkty[i].x, y + (WysokoscAnteny1));
                PunktyX.Add(Punkty[i].x);
                PunktyY.Add(y + (WysokoscAnteny1));
                PunktyGruntuY.Add(Punkty[i].y);
            }

            double OdlegloscMiedzyAntenamiPrawdziwa = Math.Sqrt((Math.Pow(PunktyX[PunktyX.Count - 1] - PunktyX[0] * PixelWidth, 2)) + (Math.Pow(PunktyY[PunktyX.Count - 1] * PixelWidth - PunktyY[0], 2)));

            double minPktX = 0;
            double minPktY = 0;


            double minDystans = 100000;
            chartFresnela2.Series["F1"].YValuesPerPoint = 2;
            for (int i = 0; i < PunktyX.Count; i++)
            {
                double d1 = Math.Sqrt((Math.Pow(PunktyX[i] - PunktyX[0], 2)) + (Math.Pow(PunktyY[i] - PunktyY[0], 2)));
                double d2 = Math.Sqrt((Math.Pow(PunktyX[i] - PunktyX[PunktyX.Count - 1], 2)) + (Math.Pow(PunktyY[i] - PunktyY[PunktyY.Count - 1], 2)));
                double SzerokoscStrefy = Math.Sqrt((StrefaFresnela * funkcja.liczLambda(f2lab2) * d1 * d2) / (d1 + d2));
                chartFresnela2.Series["F1"].Points.AddXY(PunktyX[i], new object[] { PunktyY[i] - SzerokoscStrefy, PunktyY[i] + SzerokoscStrefy });
                double currentDistance = (PunktyY[i] - SzerokoscStrefy) - PunktyGruntuY[i];
                if (currentDistance < minDystans)
                {
                    minDystans = currentDistance;
                    minPktX = PunktyX[i];
                    minPktY = (PunktyY[i] - SzerokoscStrefy);

                }
            }

            chartFresnela2.Series["MinDystans"].Points.AddXY(minPktX, WysokoscGruntu);
            chartFresnela2.Series["MinDystans"].Points.AddXY(minPktX, minPktY);
            chartFresnela2.Series["MinDystans"].Points[1].Label = "Min odległość: " + Math.Round(minDystans, 3) + " m";

            chartFresnela2.Series["F1"].ChartType = SeriesChartType.SplineRange;
            chartFresnela2.Series["Antena1"].ChartType = SeriesChartType.Column;
            chartFresnela2.Series["Antena2"].ChartType = SeriesChartType.Column;

            chartFresnela2.Series["F1"].Color = Color.FromArgb(50, Color.DarkBlue);
            chartFresnela2.Series["Antena1"].Color = Color.Red;
            chartFresnela2.Series["Antena2"].Color = Color.Red;
            chartFresnela2.Series["Grunt"].Color = Color.DarkGreen;
            chartFresnela2.Series["MinDystans"].Color = Color.Blue;
            chartFresnela2.Series["Dystans"].Color = Color.Red;

        }

        private void buttonCzytajCV_Click(object sender, EventArgs e)
        {

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {




                string nazwaPliku = openFileDialog1.FileName;
                List<friis.pkt> PunktyTerenu = CzytajCV.CzytajPlikCV(nazwaPliku);

                double f1lab2 = double.Parse(textBoxF1lab2.Text);

                double OdleglsocMiedzyAntenami = double.Parse(textBoxOdlegloscMiedzyAntenamilab2.Text);
                double WysokoscAnteny1 = double.Parse(textBoxWysokoscAnteny1.Text);
                double WysokoscAnteny2 = double.Parse(textBoxWysokoscAnteny2.Text);
                //   double WysokoscGruntu = double.Parse(textBoxWysokoscGruntu.Text);
                double Probkowanie = double.Parse(textBoxProbkowanie.Text);
                int StrefaFresnela = int.Parse(numericUpDownStrefaFresnela.Text);

                List<friis.pkt> Punkty = new List<friis.pkt>();
                double SzerokoscPixeli = OdleglsocMiedzyAntenami / Probkowanie;

                for (int i = 0; i < PunktyTerenu.Count; i++)
                {
                    double x = i * SzerokoscPixeli;
                    Punkty.Add(new pkt(PunktyTerenu[i].x, PunktyTerenu[i].y));

                }
                // Punkty.Add(new pkt(OdleglsocMiedzyAntenami, WysokoscGruntu));


                chartFresnela3.Series["F1"].Points.Clear();
                chartFresnela3.Series["Antena1"].Points.Clear();
                chartFresnela3.Series["Antena2"].Points.Clear();
                chartFresnela3.Series["Dystans"].Points.Clear();
                chartFresnela3.Series["MinDystans"].Points.Clear();
                chartFresnela3.Series["Dystans"].Points.Clear();
                chartFresnela3.Series["Grunt"].Points.Clear();
                chartFresnela3.ChartAreas["ChartArea1"].AxisX.Minimum = 0;
                chartFresnela3.ChartAreas["ChartArea1"].AxisX.Maximum = OdleglsocMiedzyAntenami;


                for (int i = 0; i < PunktyTerenu.Count; i++)
                {
                    chartFresnela3.Series["Grunt"].Points.AddXY(PunktyTerenu[i].x, PunktyTerenu[i].y);
                }


                for (int i = 0; i < 25; i++)
                {
                    chartFresnela3.Series["Antena1"].Points.AddXY((double)i * (0.5), WysokoscAnteny1 + PunktyTerenu[0].y);
                }

                for (int i = 0; i < 25; i++)
                {
                    chartFresnela3.Series["Antena2"].Points.AddXY(OdleglsocMiedzyAntenami - (double)i * (0.5), WysokoscAnteny2 + (double)61.1111111111111);
                }


                List<double> PunktyX = new List<double>();
                List<double> PunktyY = new List<double>();
                List<double> PunktyGruntuY = new List<double>();
                double tangens = (WysokoscAnteny2 - WysokoscAnteny1) / (OdleglsocMiedzyAntenami - 0);
                for (int i = 0; i < Punkty.Count; i++)
                {
                    double d = Punkty[i].x - Punkty[0].x;
                    double h = Punkty[0].y;
                    double y = (d * tangens) + h;
                    chartFresnela3.Series["Dystans"].Points.AddXY(Punkty[i].x, y + (WysokoscAnteny1));
                    PunktyX.Add(Punkty[i].x);
                    PunktyY.Add(y + (WysokoscAnteny1));
                    PunktyGruntuY.Add(Punkty[i].y);
                }

                double OdlegloscMiedzyAntenamiPrawdziwa = Math.Sqrt((Math.Pow(PunktyX[PunktyX.Count - 1] - PunktyX[0] * SzerokoscPixeli, 2)) + (Math.Pow(PunktyY[PunktyX.Count - 1] * SzerokoscPixeli - PunktyY[0], 2)));

                double minPktX = 0;
                double minPktY = 0;


                double minDystans = 100000;
                chartFresnela3.Series["F1"].YValuesPerPoint = 2;
                for (int i = 0; i < PunktyX.Count; i++)
                {
                    double d1 = Math.Sqrt((Math.Pow(PunktyX[i] - PunktyX[0], 2)) + (Math.Pow(PunktyY[i] - PunktyY[0], 2)));
                    double d2 = Math.Sqrt((Math.Pow(PunktyX[i] - PunktyX[PunktyX.Count - 1], 2)) + (Math.Pow(PunktyY[i] - PunktyY[PunktyY.Count - 1], 2)));
                    double SzerokoscStrefy = Math.Sqrt((StrefaFresnela * funkcja.liczLambda(f1lab2) * d1 * d2) / (d1 + d2));
                    chartFresnela3.Series["F1"].Points.AddXY(PunktyX[i], new object[] { PunktyY[i] - SzerokoscStrefy, PunktyY[i] + SzerokoscStrefy });
                    double currentDistance = (PunktyY[i] - SzerokoscStrefy) - PunktyGruntuY[i];
                    if (currentDistance < minDystans)
                    {
                        minDystans = currentDistance;
                        minPktX = PunktyX[i];
                        minPktY = (PunktyY[i] - SzerokoscStrefy);

                    }
                }

                chartFresnela3.Series["MinDystans"].Points.AddXY(minPktX, PunktyTerenu[0].y);
                chartFresnela3.Series["MinDystans"].Points.AddXY(minPktX, minPktY);
                chartFresnela3.Series["MinDystans"].Points[1].Label = "Min odległość: " + Math.Round(minDystans, 3) + " m";

                chartFresnela3.Series["F1"].ChartType = SeriesChartType.SplineRange;
                chartFresnela3.Series["Antena1"].ChartType = SeriesChartType.Column;
                chartFresnela3.Series["Antena2"].ChartType = SeriesChartType.Column;

                chartFresnela3.Series["Dystans"].Color = Color.Red;
                chartFresnela3.Series["F1"].Color = Color.FromArgb(50, Color.DarkBlue);
                chartFresnela3.Series["Antena1"].Color = Color.Red;
                chartFresnela3.Series["Antena2"].Color = Color.Red;
                chartFresnela3.Series["Grunt"].Color = Color.DarkGreen;
                chartFresnela3.Series["MinDystans"].Color = Color.Blue;


            }

        }

        private void buttonTerenF2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                string nazwaPliku = openFileDialog1.FileName;
                List<friis.pkt> PunktyTerenu = CzytajCV.CzytajPlikCV(nazwaPliku);

                double f2lab2 = double.Parse(textBoxF2lab2.Text);

                double OdleglsocMiedzyAntenami = double.Parse(textBoxOdlegloscMiedzyAntenamilab2.Text);
                double WysokoscAnteny1 = double.Parse(textBoxWysokoscAnteny1.Text);
                double WysokoscAnteny2 = double.Parse(textBoxWysokoscAnteny2.Text);
                //   double WysokoscGruntu = double.Parse(textBoxWysokoscGruntu.Text);
                double Probkowanie = double.Parse(textBoxProbkowanie.Text);
                int StrefaFresnela = int.Parse(numericUpDownStrefaFresnela.Text);

                List<friis.pkt> Punkty = new List<friis.pkt>();
                double PixelWidth = OdleglsocMiedzyAntenami / Probkowanie;

                for (int i = 0; i < PunktyTerenu.Count; i++)
                {
                    double x = i * PixelWidth;
                    Punkty.Add(new pkt(PunktyTerenu[i].x, PunktyTerenu[i].y));

                }
                // Punkty.Add(new pkt(OdleglsocMiedzyAntenami, WysokoscGruntu));


                chartFresnela4.Series["F1"].Points.Clear();
                chartFresnela4.Series["Antena1"].Points.Clear();
                chartFresnela4.Series["Antena2"].Points.Clear();
                chartFresnela4.Series["Dystans"].Points.Clear();
                chartFresnela4.Series["MinDystans"].Points.Clear();
                chartFresnela4.Series["Dystans"].Points.Clear();
                chartFresnela4.Series["Grunt"].Points.Clear();
                chartFresnela4.ChartAreas["ChartArea1"].AxisX.Minimum = 0;
                chartFresnela4.ChartAreas["ChartArea1"].AxisX.Maximum = OdleglsocMiedzyAntenami;


                for (int i = 0; i < PunktyTerenu.Count; i++)
                {
                    chartFresnela4.Series["Grunt"].Points.AddXY(PunktyTerenu[i].x, PunktyTerenu[i].y);
                }


                for (int i = 0; i < 25; i++)
                {
                    chartFresnela4.Series["Antena1"].Points.AddXY((double)i * (0.5), WysokoscAnteny1 + PunktyTerenu[0].y);
                }

                for (int i = 0; i < 25; i++)
                {
                    chartFresnela4.Series["Antena2"].Points.AddXY(OdleglsocMiedzyAntenami - (double)i * (0.5), WysokoscAnteny2 + (double)61.1111111111111);
                }


                List<double> PunktyX = new List<double>();
                List<double> PunktyY = new List<double>();
                List<double> PunktyGruntuY = new List<double>();
                double tangens = (WysokoscAnteny2 - WysokoscAnteny1) / (OdleglsocMiedzyAntenami - 0);
                for (int i = 0; i < Punkty.Count; i++)
                {
                    double d = Punkty[i].x - Punkty[0].x;
                    double h = Punkty[0].y;
                    double y = (d * tangens) + h;
                    chartFresnela4.Series["Dystans"].Points.AddXY(Punkty[i].x, y + (WysokoscAnteny1));
                    PunktyX.Add(Punkty[i].x);
                    PunktyY.Add(y + (WysokoscAnteny1));
                    PunktyGruntuY.Add(Punkty[i].y);
                }

                double OdlegloscMiedzyAntenamiPrawdziwa = Math.Sqrt((Math.Pow(PunktyX[PunktyX.Count - 1] - PunktyX[0] * PixelWidth, 2)) + (Math.Pow(PunktyY[PunktyX.Count - 1] * PixelWidth - PunktyY[0], 2)));

                double minPktX = 0;
                double minPktY = 0;


                double minDystans = 100000;
                chartFresnela4.Series["F1"].YValuesPerPoint = 2;
                for (int i = 0; i < PunktyX.Count; i++)
                {
                    double d1 = Math.Sqrt((Math.Pow(PunktyX[i] - PunktyX[0], 2)) + (Math.Pow(PunktyY[i] - PunktyY[0], 2)));
                    double d2 = Math.Sqrt((Math.Pow(PunktyX[i] - PunktyX[PunktyX.Count - 1], 2)) + (Math.Pow(PunktyY[i] - PunktyY[PunktyY.Count - 1], 2)));
                    double SzerokoscStrefy = Math.Sqrt((StrefaFresnela * funkcja.liczLambda(f2lab2) * d1 * d2) / (d1 + d2));
                    chartFresnela4.Series["F1"].Points.AddXY(PunktyX[i], new object[] { PunktyY[i] - SzerokoscStrefy, PunktyY[i] + SzerokoscStrefy });
                    double currentDistance = (PunktyY[i] - SzerokoscStrefy) - PunktyGruntuY[i];
                    if (currentDistance < minDystans)
                    {
                        minDystans = currentDistance;
                        minPktX = PunktyX[i];
                        minPktY = (PunktyY[i] - SzerokoscStrefy);

                    }
                }

                chartFresnela4.Series["MinDystans"].Points.AddXY(minPktX, PunktyTerenu[0].y);
                chartFresnela4.Series["MinDystans"].Points.AddXY(minPktX, minPktY);
                chartFresnela4.Series["MinDystans"].Points[1].Label = "Min odległość: " + Math.Round(minDystans, 3) + " m";

                chartFresnela4.Series["F1"].ChartType = SeriesChartType.SplineRange;
                chartFresnela4.Series["Antena1"].ChartType = SeriesChartType.Column;
                chartFresnela4.Series["Antena2"].ChartType = SeriesChartType.Column;

                chartFresnela4.Series["Dystans"].Color = Color.Red;
                chartFresnela4.Series["F1"].Color = Color.FromArgb(50, Color.DarkBlue);
                chartFresnela4.Series["Antena1"].Color = Color.Red;
                chartFresnela4.Series["Antena2"].Color = Color.Red;
                chartFresnela4.Series["Grunt"].Color = Color.DarkGreen;
                chartFresnela4.Series["MinDystans"].Color = Color.Blue;


            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double F1Tlumienie = double.Parse(textBoxF1lab2.Text);
            double F2Tlumienie = double.Parse(textBoxF2lab2.Text);
            double OdleglsocMiedzyAntenami = double.Parse(textBoxOdlegloscMiedzyAntenamilab2.Text);
            double ZasloniecieFresnera = double.Parse(textBoxZasloniecieFresnera.Text);
            double OdlegloscPrzysloniecia = double.Parse(textBoxOdlegloscPrzysloniecia.Text);
            double ProbkowanieTlumienie = double.Parse(textBoxProbkowanieTlumienie.Text);
            double d1 = OdleglsocMiedzyAntenami * OdlegloscPrzysloniecia;
            double d2 = OdleglsocMiedzyAntenami - d1;
            


            double PromienFresnera1 = funkcja.PromienFresneraTlumienie(d1, d2, F1Tlumienie);
            double RozcnicaWysokosci1 = (((PromienFresnera1 * 2) / 100) * ZasloniecieFresnera) - PromienFresnera1;
            double Tlumienie1 = funkcja.Tlumienie(RozcnicaWysokosci1, PromienFresnera1);
            chart1.Series["Tlumienie"].Points.Clear(); ;
            chart1.Series["Tlumienie"].Points.AddXY(d1, Tlumienie1);

            double PromienFresnera2 = funkcja.PromienFresneraTlumienie(d1, d2, F2Tlumienie);
            double RozcnicaWysokosci2 = (((PromienFresnera2 * 2) / 100) * ZasloniecieFresnera) - PromienFresnera2;
            double Tlumienie2 = funkcja.Tlumienie(RozcnicaWysokosci2, PromienFresnera2);
            chart2.Series["Tlumienie"].Points.Clear(); ;
            chart2.Series["Tlumienie"].Points.AddXY(d1, Tlumienie2);




        }

        private void button2_Click(object sender, EventArgs e)
        {

            chart3.Series["Tlumienie"].Points.Clear(); 
            chart4.Series["Tlumienie"].Points.Clear(); 
            double F1Tlumienie = double.Parse(textBoxF1lab2.Text);
            double F2Tlumienie = double.Parse(textBoxF2lab2.Text);
            double OdleglsocMiedzyAntenami = double.Parse(textBoxOdlegloscMiedzyAntenamilab2.Text);
            double ZasloniecieFresnera = double.Parse(textBoxZasloniecieFresnera.Text);
            double OdlegloscPrzysloniecia = double.Parse(textBoxOdlegloscPrzysloniecia.Text);
            double ProbkowanieTlumienie = double.Parse(textBoxProbkowanieTlumienie.Text);
            

           
            for (int i = 0; i < OdleglsocMiedzyAntenami; i++)
            {
                double d1 = i ;
                double d2 = OdleglsocMiedzyAntenami - d1;
                double PromienFresnera1 = funkcja.PromienFresneraTlumienie(d1, d2, F1Tlumienie);
                double RozcnicaWysokosci1 = (((PromienFresnera1 * 2) / 100) * ZasloniecieFresnera) - PromienFresnera1;
                double Tlumienie1 = funkcja.Tlumienie(RozcnicaWysokosci1, PromienFresnera1);
                chart3.Series["Tlumienie"].Points.AddXY(d1, Tlumienie1);
               
            }
            chart3.Series["Tlumienie"].Color = Color.Red;
            chart3.ChartAreas["ChartArea1"].AxisX.Minimum = 0;
            chart3.ChartAreas["ChartArea1"].AxisX.Maximum = OdleglsocMiedzyAntenami;

          


            for (int i = 0; i < OdleglsocMiedzyAntenami; i++)
            {
                double d1 = i;
                double d2 = OdleglsocMiedzyAntenami - d1;
                double PromienFresnera1 = funkcja.PromienFresneraTlumienie(d1, d2, F2Tlumienie);
                double RozcnicaWysokosci1 = (((PromienFresnera1 * 2) / 100) * ZasloniecieFresnera) - PromienFresnera1;
                double Tlumienie1 = funkcja.Tlumienie(RozcnicaWysokosci1, PromienFresnera1);
                chart4.Series["Tlumienie"].Points.AddXY(d1, Tlumienie1);

            }
            chart4.Series["Tlumienie"].Color = Color.Red;
            chart4.ChartAreas["ChartArea1"].AxisX.Minimum = 0;
            chart4.ChartAreas["ChartArea1"].AxisX.Maximum = OdleglsocMiedzyAntenami;

        }                                             
    }






}
