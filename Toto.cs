using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Toto
{
    class EredmenyElemzo
    {
        private string Eredmenyek;

        private int DontetlenekSzama
        {
            get
            {
                return Megszamol('X');
            }
        }

        private int Megszamol(char kimenet)
        {
            int darab = 0;
            foreach (var i in Eredmenyek)
            {
                if (i == kimenet) darab++;
            }
            return darab;
        }

        public bool NemvoltDontetlenMerkozes
        {
            get
            {
                return DontetlenekSzama == 0;
            }
        }

        public EredmenyElemzo(string eredmenyek) // konstruktor
        {
            Eredmenyek = eredmenyek;
        }
    }
    class FogadásiForduló
    {
        public int Év { get; private set; }
        public int Hét { get; private set; }
        public int Forduló { get; private set; }
        public int T13p1 { get; private set; }
        public int Ny13p1 { get; private set; }
        public string Eredmények { get; private set; }

        public int Nyereményösszeg => T13p1 * Ny13p1;

        public void KiírFordulóAdatait()
        {
            Console.WriteLine($"\tÉv: {Év}");
            Console.WriteLine($"\tHét: {Hét}.");
            Console.WriteLine($"\tForduló: {Forduló}.");
            Console.WriteLine($"\tTelitalálat: {T13p1} db");
            Console.WriteLine($"\tNyeremény: {Ny13p1} Ft");
            Console.WriteLine($"\tEredmények: {Eredmények}");
        }

        public FogadásiForduló(string sor)
        {
            string[] m = sor.Split(';');
            Év = int.Parse(m[0]);
            Hét = int.Parse(m[1]);
            Forduló = int.Parse(m[2]);
            T13p1 = int.Parse(m[3]);
            Ny13p1 = int.Parse(m[4]);
            Eredmények = m[5];
        }

    }
    class Toto
    {
        static void Main()
        {
            //  2. feladat:
            List<FogadásiForduló> ff = new List<FogadásiForduló>();
            foreach (var sor in File.ReadAllLines("toto.txt").Skip(1))
            {
                ff.Add(new FogadásiForduló(sor));
            }

            Console.WriteLine($"3. feladat: Fordulók száma: {ff.Count}");

            // 4. feladat:
            int ttszsz = 0;
            foreach (var fordulo in ff)
            {
                ttszsz += fordulo.T13p1;
            }
            Console.WriteLine($"4. feladat: Telitalálatos szelvények száma: {ttszsz} db");

            // 5. feladat:
            long szumNyereményösszeg = 0;
            int ttfsz = 0;
            foreach (var fordulo in ff)
            {
                if (fordulo.T13p1 > 0)
                {
                    ttfsz++;
                    checked
                    {
                        szumNyereményösszeg += fordulo.Nyereményösszeg;
                    }
                }
            }
            double nyöá = (double)szumNyereményösszeg / ttfsz;
            Console.WriteLine($"5. feladat: Átlag: {nyöá:f0} Ft");

            Console.WriteLine("6. feladat:");
            FogadásiForduló minForduló = null;
            FogadásiForduló maxForduló = null;
            foreach (var fordulo in ff)
            {
                if (fordulo.T13p1 > 0)  // Ha volt telitalálatos szelvény
                {
                    if (minForduló == null)
                    {
                        minForduló = fordulo;
                        maxForduló = fordulo;
                    }
                    else
                    {
                        if (fordulo.Ny13p1 > maxForduló.Ny13p1)
                        {
                            maxForduló = fordulo;
                        }
                        if (fordulo.Ny13p1 < minForduló.Ny13p1)
                        {
                            minForduló = fordulo;
                        }
                    }
                }
            }
            Console.WriteLine("\tLegnagyobb:");
            maxForduló.KiírFordulóAdatait();
            Console.WriteLine("\n\tLegkisebb:");
            minForduló.KiírFordulóAdatait();

            // 6. feladat megoldása Linq-el:
            //var ffRendezve = ff.Where(x => x.T13p1 > 0).OrderByDescending(x => x.Ny13p1);
            //maxForduló = ffRendezve.First();
            //minForduló = ffRendezve.Last();

            // 8. feladat:
            bool vdnf = false;
            foreach (var fordulo in ff)
            {
                EredmenyElemzo ee = new EredmenyElemzo(fordulo.Eredmények);
                if (ee.NemvoltDontetlenMerkozes)
                {
                    vdnf = true;
                    break;
                }
            }
            Console.WriteLine($"8. feadat: {(vdnf ? "Volt" : "Nem volt")} döntetlen nélküli forduló!");


            Console.ReadKey();
        }
    }
}
