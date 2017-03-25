using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication7
{
    public partial class Form1 : Form
    {
        double[] tablicaNieparzysta = new double[] { 2.5, 2, 3, 1.5, 3.5, 1, 4, 0.5, 4.5 };
        double[] tablicaParzysta = new double[] { 2.25, 2.75, 1.75, 3.25, 1.25, 3.75, 0.75, 4.25, 0.25, 4.75 };

        double[][] tablica = new double[][] { 
        new double[] { 2.5 }, 
        new double[] {2.25, 2.75},
        new double[] {2.5, 2, 3},
        new double[] {2.25, 2.75, 1.75, 3.25},
        new double[] {2.5, 2, 3, 1.5, 3.5},
        new double[] {2.25, 2.75, 1.75, 3.25, 1.25, 3.75},
        new double[] {2.5, 2, 3, 1.5, 3.5, 1, 4},
        new double[] {2.25, 2.75, 1.75, 3.25, 1.25, 3.75, 0.75, 4.25},
        new double[] {2.5, 2, 3, 1.5, 3.5, 1, 4, 0.5, 4.5},
        new double[] { 2.25, 2.75, 1.75, 3.25, 1.25, 3.75, 0.75, 4.25, 0.25, 4.75}
        };
        
        
        
        string[] dol = new string[] { 
"-1.57;0;2.5;4;0;2.5;1",

@"-1.57;0;1.67;4;0;1.67;1
-1.57;0;3.33;4;0;3.33;1",

@"-1.57;0;1.25;4;0;1.25;1
-1.57;0;2.5;4;0;2.5;1
-1.57;0;3.75;4;0;3.75;1",

@"-1.57;0;1;4;0;1;1
-1.57;0;2;4;0;2;1
-1.57;0;3;4;0;3;1
-1.57;0;4;4;0;4;1",

@"-1.57;0;0.83;4;0;0.83;1
-1.57;0;1.67;4;0;1.67;1
-1.57;0;2.5;4;0;2.5;1
-1.57;0;3.33;4;0;3.33;1
-1.57;0;4.17;4;0;4.17;1",

@"-1.57;0;0.71;4;0;0.71;1
-1.57;0;1.43;4;0;1.43;1
-1.57;0;2.14;4;0;2.14;1
-1.57;0;2.86;4;0;2.86;1
-1.57;0;3.57;4;0;3.57;1
-1.57;0;4.29;4;0;4.29;1",

@"-1.57;0;0.63;4;0;0.63;1
-1.57;0;1.25;4;0;1.25;1
-1.57;0;1.88;4;0;1.88;1
-1.57;0;2.5;4;0;2.5;1
-1.57;0;3.13;4;0;3.13;1
-1.57;0;3.75;4;0;3.75;1
-1.57;0;4.38;4;0;4.38;1",

@"-1.57;0;0.56;4;0;0.56;1
-1.57;0;1.11;4;0;1.11;1
-1.57;0;1.67;4;0;1.67;1
-1.57;0;2.22;4;0;2.22;1
-1.57;0;2.78;4;0;2.78;1
-1.57;0;3.33;4;0;3.33;1
-1.57;0;3.89;4;0;3.89;1
-1.57;0;4.44;4;0;4.44;1",

@"-1.57;0;0.5;4;0;0.5;1
-1.57;0;1;4;0;1;1
-1.57;0;1.5;4;0;1.5;1
-1.57;0;2;4;0;2;1
-1.57;0;2.5;4;0;2.5;1
-1.57;0;3;4;0;3;1
-1.57;0;3.5;4;0;3.5;1
-1.57;0;4;4;0;4;1
-1.57;0;4.5;4;0;4.5;1",

@"-1.57;0;0.45;4;0;0.45;1
-1.57;0;0.91;4;0;0.91;1
-1.57;0;1.36;4;0;1.36;1
-1.57;0;1.82;4;0;1.82;1
-1.57;0;2.27;4;0;2.27;1
-1.57;0;2.73;4;0;2.73;1
-1.57;0;3.18;4;0;3.18;1
-1.57;0;3.64;4;0;3.64;1
-1.57;0;4.09;4;0;4.09;1
-1.57;0;4.55;4;0;4.55;1"       
};



        string[] gora = new string[] { 

"1.57;0;2.5;1;0;2.5;4",

@"1.57;0;1.67;1;0;1.67;4
1.57;0;3.33;1;0;3.33;4",

@"1.57;0;1.25;1;0;1.25;4
1.57;0;2.5;1;0;2.5;4
1.57;0;3.75;1;0;3.75;4",

@"1.57;0;1;1;0;1;4
1.57;0;2;1;0;2;4
1.57;0;3;1;0;3;4
1.57;0;4;1;0;4;4",

@"1.57;0;0.83;1;0;0.83;4
1.57;0;1.67;1;0;1.67;4
1.57;0;2.5;1;0;2.5;4
1.57;0;3.33;1;0;3.33;4
1.57;0;4.17;1;0;4.17;4",

@"1.57;0;0.71;1;0;0.71;4
1.57;0;1.43;1;0;1.43;4
1.57;0;2.14;1;0;2.14;4
1.57;0;2.86;1;0;2.86;4
1.57;0;3.57;1;0;3.57;4
1.57;0;4.29;1;0;4.29;4",

@"1.57;0;0.63;1;0;0.63;4
1.57;0;1.25;1;0;1.25;4
1.57;0;1.88;1;0;1.88;4
1.57;0;2.5;1;0;2.5;4
1.57;0;3.13;1;0;3.13;4
1.57;0;3.75;1;0;3.75;4
1.57;0;4.38;1;0;4.38;4",

@"1.57;0;0.56;1;0;0.56;4
1.57;0;1.11;1;0;1.11;4
1.57;0;1.67;1;0;1.67;4
1.57;0;2.22;1;0;2.22;4
1.57;0;2.78;1;0;2.78;4
1.57;0;3.33;1;0;3.33;4
1.57;0;3.89;1;0;3.89;4
1.57;0;4.44;1;0;4.44;4",

@"1.57;0;0.5;1;0;0.5;4
1.57;0;1;1;0;1;4
1.57;0;1.5;1;0;1.5;4
1.57;0;2;1;0;2;4
1.57;0;2.5;1;0;2.5;4
1.57;0;3;1;0;3;4
1.57;0;3.5;1;0;3.5;4
1.57;0;4;1;0;4;4
1.57;0;4.5;1;0;4.5;4",

@"1.57;0;0.45;1;0;0.45;4
1.57;0;0.91;1;0;0.91;4
1.57;0;1.36;1;0;1.36;4
1.57;0;1.82;1;0;1.82;4
1.57;0;2.27;1;0;2.27;4
1.57;0;2.73;1;0;2.73;4
1.57;0;3.18;1;0;3.18;4
1.57;0;3.64;1;0;3.64;4
1.57;0;4.09;1;0;4.09;4
1.57;0;4.55;1;0;4.55;4"

};
        
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Konfiguracja dla otwartej przestrzeni
            //WygenerujKonfiguracje();
            //OtwartaPrzestrzenConfig();


            //Konfiguracja dla Przejscia przez drzwi
            //WygenerujKonfiguracjePrzejsciePrzezDrzwi();
            //PrzejsciePrzezDrzwiConfig();

            //Konfiuracja pojedyncze roboty
            // WygenerujKonfiguracjeTestowaAlgorytmuPojedynczeRoboty();
            // WaskiePrzejscieeTestowaAlgorytmuPojedynczeRobotyConfig();

            //
            // WygenerujPrzypadkiDlaOkregu();

            //Przypadki testowe orgianlne RVO
            rvoBenchmarkCircle();

            //Przypadki testowe orgianlne RVO
           // rvoBenchmarkBlocks();

            //WąskiePrzejścieMijankaNowa
            //WaskiePrzejscieMijankaNowa();

            //Konfiguracja dla Waskiego Przejscia 
           // WygenerujKonfiguracjeWaskiKorytarz();
           // WaskiKorytarzConfig();

            //Przypadki testowe dla skrzyżowania
             //WygenerujKonfiguracjeSkrzyzowanie();

            //Przypadki testowe dla skrzyżowania typu 8
            //WygenerujKonfiguracjeSkrzyzowanieTypu8();

        }


        //private void OtwartaPrzestrzenConfigNowa()
        //{
            
            
        //    double odlegloscMiedzyRobotami = 0.3;
        //    double[] pozycjeRobotowGornych;
        //    double[] pozycjeRobotowDolnych;

        //    double xOsSymetrii = 2.5;
        //    double maxX;

        //    double y0 = 1.0;
        //    double y1 = 4.0;

        //    for (int i = 1; i < 11; i++)
        //    {
        //        for (int j = 1; j < 11; j++)
        //        {                    
        //            pozycjeRobotowGornych = new double[i];

        //            maxX = xOsSymetrii - ((pozycjeRobotowGornych.Length -1) * odlegloscMiedzyRobotami) / 2;
        //            for (int k = 0; k < pozycjeRobotowGornych.Length; k++)
        //                pozycjeRobotowGornych[k] += odlegloscMiedzyRobotami * k + maxX;


        //            pozycjeRobotowDolnych = new double[j];

        //            maxX = xOsSymetrii - ((pozycjeRobotowDolnych.Length -1) * odlegloscMiedzyRobotami) / 2;
        //            for (int k = 0; k < pozycjeRobotowDolnych.Length; k++)
        //                pozycjeRobotowDolnych[k] += odlegloscMiedzyRobotami * k + maxX;

        //        }
        //    }

        //}


        private void wypelnijTablice()
        {
            double odlegloscMiedzyRobotami = 0.5;
            double[] pozycjeRobotowGornych;

            double xOsSymetrii = 3.5;
            double maxX;
            
            for (int i = 1; i < 11; i++)
            {
                for (int j = 1; j < 11; j++)
                {
                    pozycjeRobotowGornych = new double[i];

                    maxX = xOsSymetrii - ((pozycjeRobotowGornych.Length - 1) * odlegloscMiedzyRobotami) / 2;
                    for (int k = 0; k < pozycjeRobotowGornych.Length; k++)
                        pozycjeRobotowGornych[k] += odlegloscMiedzyRobotami * k + maxX;

                    tablica[i-1] = pozycjeRobotowGornych;
                }
            }
        }

        private void rvoBenchmarkCircle()
        {
            StringBuilder output = new StringBuilder();
            string decimalFrmat = "0.##";
            int iloscRobotow = 250; //250

            double x_Start, y_Start;
            double x_End, y_End;

            double kat_Start = -3.14;
            double kat_End = 1.57;
            double kat_Step = (Math.Abs(kat_Start) + Math.Abs(kat_End)) / (iloscRobotow - 1);

            double shift = 210;

            for (int i = 0; i < iloscRobotow; ++i)
            {
                x_Start = 200.0f * (Math.Cos(i * 2.0f * Math.PI / iloscRobotow));
                y_Start = 200.0f * (Math.Sin(i * 2.0f * Math.PI / iloscRobotow));

                x_End = -x_Start;
                y_End = -y_Start;

                //shift
                x_Start += shift;
                y_Start += shift;
                x_End += shift;
                y_End += shift;

                output.AppendLine(string.Format("{0};{1}; 0;{2};{3}; 0;{4};{5}", i, kat_Start.ToString(decimalFrmat), x_Start.ToString(decimalFrmat), y_Start.ToString(decimalFrmat), x_End.ToString(decimalFrmat), y_End.ToString(decimalFrmat)).Replace(",", "."));


                kat_Start += kat_Step;
            }


            string ss = output.ToString();
           
            saveConfig(string.Format("BenchmarkCircle {0}",iloscRobotow.ToString()), ss);
        }

        private void rvoBenchmarkBlocks()
        {
            StringBuilder output = new StringBuilder();
            string decimalFrmat = "0.##";

            double[] katy = new double[] { -2.355, -0.785, 0.785, 2.355 };

            double x_Start, y_Start;
            double x_End, y_End;
            double kat_Start;

            double shift = 210;

            int iRobotID = -1;

            for (int i = 0; i < 4; ++i)
            {
                for (int j = 0; j < 4; ++j)
                {
                    iRobotID++;
                    x_Start = 55.0f + i * 10.0f;
                    y_Start = 55.0f + j * 10.0f;
                    kat_Start = katy[0];

                    x_End = -75.0f + i * 10.0f;
                    y_End = -75.0f + j * 10.0f;


                    //shift
                    x_Start += shift;
                    y_Start += shift;
                    x_End += shift;
                    y_End += shift;
                    output.AppendLine(string.Format("{0};{1}; 0;{2};{3}; 0;{4};{5}", iRobotID, kat_Start.ToString(decimalFrmat), x_Start.ToString(decimalFrmat), y_Start.ToString(decimalFrmat), x_End.ToString(decimalFrmat), y_End.ToString(decimalFrmat)).Replace(",", "."));


                    iRobotID++;
                    x_Start = -55.0f + i * 10.0f;
                    y_Start = 55.0f + j * 10.0f;
                    kat_Start = katy[1];

                    x_End = 75.0f + i * 10.0f;
                    y_End = -75.0f + j * 10.0f;

                    //shift
                    x_Start += shift;
                    y_Start += shift;
                    x_End += shift;
                    y_End += shift;
                    output.AppendLine(string.Format("{0};{1}; 0;{2};{3}; 0;{4};{5}", iRobotID, kat_Start.ToString(decimalFrmat), x_Start.ToString(decimalFrmat), y_Start.ToString(decimalFrmat), x_End.ToString(decimalFrmat), y_End.ToString(decimalFrmat)).Replace(",", "."));

                    iRobotID++;
                    x_Start = 55.0f + i * 10.0f;
                    y_Start = -55.0f + j * 10.0f;
                    kat_Start = katy[2];

                    x_End = -75.0f + i * 10.0f;
                    y_End = 75.0f + j * 10.0f;

                    //shift
                    x_Start += shift;
                    y_Start += shift;
                    x_End += shift;
                    y_End += shift;
                    output.AppendLine(string.Format("{0};{1}; 0;{2};{3}; 0;{4};{5}", iRobotID, kat_Start.ToString(decimalFrmat), x_Start.ToString(decimalFrmat), y_Start.ToString(decimalFrmat), x_End.ToString(decimalFrmat), y_End.ToString(decimalFrmat)).Replace(",", "."));

                    iRobotID++;
                    x_Start = -55.0f + i * 10.0f;
                    y_Start = -55.0f + j * 10.0f;
                    kat_Start = katy[2];

                    x_End = 75.0f + i * 10.0f;
                    y_End = 75.0f + j * 10.0f;

                    //shift
                    x_Start += shift;
                    y_Start += shift;
                    x_End += shift;
                    y_End += shift;
                    output.AppendLine(string.Format("{0};{1}; 0;{2};{3}; 0;{4};{5}", iRobotID, kat_Start.ToString(decimalFrmat), x_Start.ToString(decimalFrmat), y_Start.ToString(decimalFrmat), x_End.ToString(decimalFrmat), y_End.ToString(decimalFrmat)).Replace(",", "."));
                }
            }


            string ss = output.ToString();
        }

        private void WygenerujPrzypadkiDlaOkregu()
        {
            StringBuilder output = new StringBuilder();
            
            double x = 3;
            double y = 3;
            double R = 2;

            const int length = 13;

            double[] katy = new double[length] { -3.14, -2.75, -2.36, -1.97, -1.57,-1.19, -0.78, -0.39, 0.0, 0.39, 0.78, 1.17, 1.57 }; // { -3.14, -2.36, -1.57, -0.78, 0.0, 0.78, 1.57 }; //{ -3.14, -1.57, 0.0, 1.57 };
          //  double[] katyStartowe =new double[length] {1.57,0.0,-1.57, -3.14 };


            double[] XprimStart = new double[length];
            double[] YprimtStart = new double[length];

            double[] XprimEnd = new double[length];
            double[] YprimtEnd = new double[length];

            for (int i = 0; i < katy.Length; i++)
            {
                XprimStart[i] = Math.Round(x + R * Math.Sin(katy[i]),2);
                YprimtStart[i] = Math.Round(y + R * Math.Cos(katy[i]),2);

                XprimEnd[i] = Math.Round(x + R * Math.Sin(katy[i] + 3.14),2);
                YprimtEnd[i] = Math.Round(y + R * Math.Cos(katy[i] + 3.14), 2);
            }

            // normalize(katy[i] + 4.71)
            for (int i = 0; i < katy.Length; i++)
            {
                output.AppendLine(string.Format("{0};{1}; 0;{2};{3}; 0;{4};{5}", i, katy[katy.Length - 1 - i], XprimStart[i], YprimtStart[i], XprimEnd[i], YprimtEnd[i]).Replace(",", "."));
            }


            string ss = output.ToString();


        }


        private double normalize(double angle)
        {
            while (angle > 3.14)
                angle -= 3.14;

            return angle;
        }

        /// <summary>
        /// Wygenerowanie przypadkow z otwarta przestrzenia 100 dla gornego wiekszego wspolczynnika respektu oraz 100 dla mniejszego wspolczynnika respektu
        /// </summary>
        private void OtwartaPrzestrzenConfig()
        {
            string nazwaPrzypadku;
            string zlaczenie;
            string zapisDoBazy;

            for (int i = 0; i < dol.Length; i++)
            {
                for (int j = 0; j < gora.Length; j++)
                {
                    nazwaPrzypadku = string.Format("OtwartaPrzestrzeń {0}-H{1}", i + 1, j + 1);
                    zlaczenie = string.Format("{0}\r\n{1}", dol[i], gora[j]);

                    zapisDoBazy = ponumeruj(zlaczenie);

                    saveConfig(nazwaPrzypadku, zapisDoBazy);
                }
            }

            nazwaPrzypadku = string.Empty;
            zlaczenie = string.Empty;
            zapisDoBazy = string.Empty;

            for (int i = 0; i < gora.Length; i++)
            {
                for (int j = 0; j < dol.Length; j++)
                {
                    nazwaPrzypadku = string.Format("OtwartaPrzestrzeń H{0}-{1}", i + 1, j + 1);
                    zlaczenie = string.Format("{0}\r\n{1}", gora[i], dol[j]);

                    zapisDoBazy = ponumeruj(zlaczenie);

                    saveConfig(nazwaPrzypadku, zapisDoBazy);
                }
            }
        }

       /// <summary>
       /// Przypadki dla waskiego przejscia 100 gdy jest wiekszy u gory wpolczynnik respektu oraz 100 gdy jest na dole
       /// </summary>
        private void PrzejsciePrzezDrzwiConfig()
        {
            string nazwaPrzypadku;
            string zlaczenie;
            string zapisDoBazy;

            for (int i = 0; i < dol.Length; i++)
            {
                for (int j = 0; j < gora.Length; j++)
                {
                    nazwaPrzypadku = string.Format("PrzejściePrzezDrzwi {0}-H{1}", i + 1, j + 1);
                    zlaczenie = string.Format("{0}\r\n{1}", dol[i], gora[j]);

                    zapisDoBazy = ponumeruj(zlaczenie);

                    saveConfig(nazwaPrzypadku, zapisDoBazy);
                }
            }

            nazwaPrzypadku = string.Empty;
            zlaczenie = string.Empty;
            zapisDoBazy = string.Empty;

            for (int i = 0; i < gora.Length; i++)
            {
                for (int j = 0; j < dol.Length; j++)
                {
                    nazwaPrzypadku = string.Format("PrzejściePrzezDrzwi H{0}-{1}", i + 1, j + 1);
                    zlaczenie = string.Format("{0}\r\n{1}", gora[i], dol[j]);

                    zapisDoBazy = ponumeruj(zlaczenie);

                    saveConfig(nazwaPrzypadku, zapisDoBazy);
                }
            }
        }


        private void WygenerujKonfiguracje()
        {
            wypelnijTablice();
            
            double katDol = -1.57;
            double startY = 4.0;
            double endY = 1.0;

            string[] Dolne = new string[10];

            for (int i = 0; i < tablica.Length; i++)
            {
                string temp = string.Empty;

                for (int j = 0; j < tablica[i].Length; j++)
                {
                    temp += string.Format("{0}; 0;{1};{2}; 0;{3};{4}\r\n", katDol, tablica[i][j], startY, tablica[i][j], endY);

                }
                Dolne[i] = temp.Remove(temp.Length - 2, 2).Replace(",", ".");
            }

            dol = Dolne;

            double katGorny = 1.57;

            startY = 1.0;
            endY = 4.0;

            string[] Gorny = new string[10];

            for (int i = 0; i < tablica.Length; i++)
            {
                string temp = string.Empty;

                for (int j = 0; j < tablica[i].Length; j++)
                {
                    temp += string.Format("{0}; 0;{1};{2}; 0;{3};{4}\r\n", katGorny, tablica[i][j], startY, tablica[i][j], endY);

                }
                Gorny[i] = temp.Remove(temp.Length - 2, 2).Replace(",", ".");
            }

            gora = Gorny;

        }

        private void WygenerujKonfiguracjePrzejsciePrzezDrzwi()
        {
            wypelnijTablice();
            
            double katDol = -1.57;
            double startY = 4.0;
            double endY = 1.0;

            string dolnePuktyPrzejscia = " 0;3.5;2.6; 0;3.5;2.39; ";
           // string dolnePuktyPrzejscia = " 0;3.5;3.0; 0;3.5;1.99; ";


            string[] Dolne = new string[10];

            for (int i = 0; i < tablica.Length; i++)
            {
                string temp = string.Empty;

                for (int j = 0; j < tablica[i].Length; j++)
                {
                    temp += string.Format("{0}; 0;{1};{2}; {5} 0;{3};{4}\r\n", katDol, tablica[i][j], startY, tablica[i][j], endY, dolnePuktyPrzejscia);

                }
                Dolne[i] = temp.Remove(temp.Length - 2, 2).Replace(",", ".");
            }

            dol = Dolne;

            double katGorny = 1.57;

            startY = 1.0;
            endY = 4.0;

            string[] Gorny = new string[10];

            string gornePuktyPrzejscia = " 0;3.5;2.39; 0;3.5;2.6; ";
           // string gornePuktyPrzejscia = " 0;3.5;2.99; 0;3.5;3.0; ";

            for (int i = 0; i < tablica.Length; i++)
            {
                string temp = string.Empty;

                for (int j = 0; j < tablica[i].Length; j++)
                {
                    temp += string.Format("{0}; 0;{1};{2}; {5} 0;{3};{4}\r\n", katGorny, tablica[i][j], startY, tablica[i][j], endY, gornePuktyPrzejscia);

                }
                Gorny[i] = temp.Remove(temp.Length - 2, 2).Replace(",", ".");
            }

            gora = Gorny;

        }



        private void WaskiePrzejscieeTestowaAlgorytmuPojedynczeRobotyConfig()
        {
            string nazwaPrzypadku;
            string zlaczenie;
            string zapisDoBazy;

            for (int i = 0; i < dol.Length; i++)
            {
                nazwaPrzypadku = string.Format("Pojedycze Roboty dół {0}", i + 1);
                zlaczenie = string.Format("{0}\r\n", dol[i]);

                zapisDoBazy = ponumeruj(zlaczenie);

                 saveConfig(nazwaPrzypadku, zapisDoBazy);
            }

            for (int i = 0; i < gora.Length; i++)
            {
                nazwaPrzypadku = string.Format("Pojedycze Roboty góra {0}", i + 1);
                zlaczenie = string.Format("{0}\r\n", gora[i]);

                zapisDoBazy = ponumeruj(zlaczenie);

                 saveConfig(nazwaPrzypadku, zapisDoBazy);
            }
        }

        private void WygenerujKonfiguracjeTestowaAlgorytmuPojedynczeRoboty()
        {
            double katDol = -1.57;
            double startY = 4.0;
            double endY = 1.0;

            string[] Dolne = new string[10];

            for (int i = 0; i < tablica.Length; i++)
            {
                string temp = string.Empty;

                for (int j = 0; j < tablica[i].Length; j++)
                {
                    temp += string.Format("{0}; 0;{1};{2}; 0;{3};{4}\r\n", katDol, tablica[i][j], startY, tablica[i][j], endY);

                }
                Dolne[i] = temp.Remove(temp.Length - 2, 2).Replace(",", ".");
            }

            dol = Dolne;

            double katGorny = 1.57;

            startY = 1.0;
            endY = 4.0;

            string[] Gorny = new string[10];

            for (int i = 0; i < tablica.Length; i++)
            {
                string temp = string.Empty;

                for (int j = 0; j < tablica[i].Length; j++)
                {
                    temp += string.Format("{0}; 0;{1};{2}; 0;{3};{4}\r\n", katGorny, tablica[i][j], startY, tablica[i][j], endY);

                }
                Gorny[i] = temp.Remove(temp.Length - 2, 2).Replace(",", ".");
            }

            gora = Gorny;

        }


        private void WaskiePrzejscieMijankaNowa()
        {
            string sName = "Mijanka 1 robot gora";
            string sConfig = "0;1.57; 0;0.4;0.3; 0;0.4;4.7";
            saveConfig(sName, sConfig);

            sName = "Mijanka 1 robot dol";
            sConfig = "0;-1.57; 0;0.4;4.7; 0;0.4;0.3";
            saveConfig(sName, sConfig);

            sName = "Mijanka 1-1H";
            sConfig = @"0;1.57; 0;0.4;0.3; 0;0.4;4.7
1;-1.57; 0;0.4;4.7; 0;0.4;0.3";
            saveConfig(sName, sConfig);

            sName = "Mijanka 1H-1";
            sConfig = @"1;1.57; 0;0.4;0.3; 0;0.4;4.7
0;-1.57; 0;0.4;4.7; 0;0.4;0.3";
            saveConfig(sName, sConfig);
        }



        private void WygenerujKonfiguracjeWaskiKorytarz()
        {
            wypelnijTablice();

            double katDol = -1.57;
            double startY = 4.5;
            double endY = 0.5;

            string dolnePuktyPrzejscia = " 0;3.5;3.5; 0;3.5;1.5; ";
            // string dolnePuktyPrzejscia = " 0;3.5;3.0; 0;3.5;1.99; ";


            string[] Dolne = new string[10];

            for (int i = 0; i < tablica.Length; i++)
            {
                string temp = string.Empty;

                for (int j = 0; j < tablica[i].Length; j++)
                {
                    temp += string.Format("{0}; 0;{1};{2}; {5} 0;{3};{4}\r\n", katDol, tablica[i][j], startY, tablica[i][j], endY, dolnePuktyPrzejscia);

                }
                Dolne[i] = temp.Remove(temp.Length - 2, 2).Replace(",", ".");
            }

            dol = Dolne;

            double katGorny = 1.57;

            startY = 0.5;
            endY = 4.5;

            string[] Gorny = new string[10];

            string gornePuktyPrzejscia = " 0;3.5;1.5; 0;3.5;3.5; ";
            // string gornePuktyPrzejscia = " 0;3.5;2.99; 0;3.5;3.0; ";

            for (int i = 0; i < tablica.Length; i++)
            {
                string temp = string.Empty;

                for (int j = 0; j < tablica[i].Length; j++)
                {
                    temp += string.Format("{0}; 0;{1};{2}; {5} 0;{3};{4}\r\n", katGorny, tablica[i][j], startY, tablica[i][j], endY, gornePuktyPrzejscia);

                }
                Gorny[i] = temp.Remove(temp.Length - 2, 2).Replace(",", ".");
            }

            gora = Gorny;

        }

        private void WaskiKorytarzConfig()
        {
            string nazwaPrzypadku;
            string zlaczenie;
            string zapisDoBazy;

            for (int i = 0; i < dol.Length; i++)
            {
                for (int j = 0; j < gora.Length; j++)
                {
                    nazwaPrzypadku = string.Format("WąskiKorytarz {0}-H{1}", i + 1, j + 1);
                    zlaczenie = string.Format("{0}\r\n{1}", dol[i], gora[j]);

                    zapisDoBazy = ponumeruj(zlaczenie);

                    saveConfig(nazwaPrzypadku, zapisDoBazy);
                }
            }

            nazwaPrzypadku = string.Empty;
            zlaczenie = string.Empty;
            zapisDoBazy = string.Empty;

            for (int i = 0; i < gora.Length; i++)
            {
                for (int j = 0; j < dol.Length; j++)
                {
                    nazwaPrzypadku = string.Format("WąskiKorytarz H{0}-{1}", i + 1, j + 1);
                    zlaczenie = string.Format("{0}\r\n{1}", gora[i], dol[j]);

                    zapisDoBazy = ponumeruj(zlaczenie);

                    saveConfig(nazwaPrzypadku, zapisDoBazy);
                }
            }
        }

        private string ponumeruj(string ustawienie)
        {
            string[] splited = ustawienie.Split(new string[] {"\r\n"},StringSplitOptions.RemoveEmptyEntries);
            StringBuilder result = new StringBuilder();


            for (int i = 0; i < splited.Length; i++)
            {
                result.AppendLine(string.Format("{0};{1}",i + 1,splited[i]));
                

            }

            result = result.Remove(result.Length - 2, 2);


            return result.ToString();
        }

        private void saveConfig(string sName, string sConfig)
        {
            string txtServerName = @"SZSZ\SQLEXPRESS";//"SZYMON-KOMPUTER"; // //string txtServerName = "SZYMON-KOMPUTER";
                string txtUser = "szsz";
                string txtPassword = "szsz"; 

            string sConnectionString = string.Format("Server={0};Database=Doktorat;User Id={1};Password={2};", txtServerName, txtUser, txtPassword);

            using (SqlConnection con = new SqlConnection(sConnectionString))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand("", con))
                {
                    cmd.CommandText = "INSERT INTO [Doktorat].[dbo].[Config]([Name],[ConfigFile],[IsActive]) VALUES(@sName,@ConfigFile,1)";
                    cmd.Parameters.AddWithValue("@sName", sName);
                    cmd.Parameters.AddWithValue("@ConfigFile", sConfig);

                    cmd.ExecuteNonQuery();    
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private string[] permutacjaWR(int iloscRobotow)
        {
            string sequence = string.Empty;

            for (int i = 0; i < iloscRobotow; i++)
                sequence += (i + 1).ToString();

            List<string> result = new List<string>();

            // our sequence of characters


            // variables aiding us in char[] <-> string conversion
            int n = sequence.Length;
            char[] chars = new char[n];
            string permutation;

            // variables necessary for our algorithm
            int[] positions = new int[n];
            bool[] used = new bool[n];
            bool last;

            // initialize positions
            for (int i = 0; i < n; i++)
                positions[i] = i;

            do
            {
                // make permutation according to positions
                for (int i = 0; i < n; i++)
                    chars[i] = sequence[positions[i]];
                permutation = new string(chars);

                // output it
                result.Add(permutation);

                // recalculate positions
                last = false;
                int k = n - 2;
                while (k >= 0)
                {
                    if (positions[k] < positions[k + 1])
                    {
                        for (int i = 0; i < n; i++)
                            used[i] = false;
                        for (int i = 0; i < k; i++)
                            used[positions[i]] = true;
                        do positions[k]++; while (used[positions[k]]);
                        used[positions[k]] = true;
                        for (int i = 0; i < n; i++)
                            if (!used[i]) positions[++k] = i;
                        break;
                    }
                    else k--;
                }
                last = (k < 0);
            } while (!last);

            return result.ToArray();
        }


        public void WygenerujKonfiguracjeSkrzyzowanie()
        {
            Vector3D StartRobot1 = new Vector3D(2.4, 4.5, -1.57);
            Vector3D EndRobot1 = new Vector3D(2.4, 0.3, 0);

            Vector3D StartRobot2 = new Vector3D(4.5, 2.4, -3.14);
            Vector3D EndRobot2 = new Vector3D(0.3, 2.4, 0);

            Vector3D StartRobot3 = new Vector3D(2.4, 0.3, 1.57);
            Vector3D EndRobot3 = new Vector3D(2.4, 4.5, 0);

            Vector3D StartRobot4 = new Vector3D(0.3, 2.4, 0);
            Vector3D EndRobot4 = new Vector3D(4.5, 2.4, 0);

            string puntCentralny = "0;2.4;2.4;";

            string sName = "";

            Vector3D[] StartRobots = new Vector3D[] { StartRobot1, StartRobot2, StartRobot3, StartRobot4 };
            Vector3D[] EndRobots = new Vector3D[] { EndRobot1, EndRobot2, EndRobot3, EndRobot4 };

            for (int i = 0; i < 4; i++)
            {
                string[] temp = permutacjaWR(i + 1);

                for (int j = 0; j < temp.Length; j++)
                {
                    string sConfig = "";
                    string tmp = temp[j];
                    sName = string.Format("Skrzyżowanie równorzędne {0}; Ustawienie {1}", i + 1, j + 1);

                    for (int k = 0; k < tmp.Length; k++)
                        sConfig += string.Format("{0};{1}; 0;{2};{3}; {6} 0;{4};{5};\r\n", tmp[k], StartRobots[k].Z, StartRobots[k].X, StartRobots[k].Y, EndRobots[k].X, EndRobots[k].Y, puntCentralny);


                    sConfig = sConfig.Remove(sConfig.Length - 2, 2).Replace(",", ".");
                    saveConfig(sName, sConfig);
                }
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            string[] table = permutacjaWR(1);
        }

        public void WygenerujKonfiguracjeSkrzyzowanieTypu8()
        {
            Vector3D StartRobot1 = new Vector3D(1, 2, -2.5);
            Vector3D EndRobot1 = new Vector3D(2, 1, 0);

            Vector3D StartRobot2 = new Vector3D(3, 4, 5.0);
            Vector3D EndRobot2 = new Vector3D(4, 3, 0);

            string sName = "";

            Vector3D[] StartRobots = new Vector3D[] { StartRobot1, StartRobot2 };
            Vector3D[] EndRobots = new Vector3D[] { EndRobot1, EndRobot2 };

            for (int i = 0; i < 2; i++)
            {
                string[] temp = permutacjaWR(i + 1);

                for (int j = 0; j < temp.Length; j++)
                {
                    string sConfig = "";
                    string tmp = temp[j];
                    sName = string.Format("Skrzyżowanie typu 8 {0}; Ustawienie {1}", i + 1, j + 1);

                    for (int k = 0; k < tmp.Length; k++)
                        sConfig += string.Format("{0};{1}; 0;{2};{3}; 0;{4};{5}\r\n", tmp[k], StartRobots[k].Z, StartRobots[k].X, StartRobots[k].Y, EndRobots[k].X, EndRobots[k].Y);


                    sConfig = sConfig.Remove(sConfig.Length - 2, 2).Replace(",", ".");
                    // saveConfig(sName, sConfig);
                }
            }
        }


    }
}
