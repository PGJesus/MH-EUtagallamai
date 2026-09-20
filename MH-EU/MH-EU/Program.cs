using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace EU
{
    class Orszag
    {
        public string Nev { get; set; }
        public DateTime Csatlakozas { get; set; }

        public Orszag(string nev, DateTime csatlakozas)
        {
            Nev = nev;
            Csatlakozas = csatlakozas;
        }
    }

    class EuManager
    {
        private List<Orszag> orszagok = new List<Orszag>();

        public void Beolvas(string fajlnev)
        {
            string[] sorok = File.ReadAllLines(fajlnev, Encoding.UTF8);

            foreach (string sor in sorok)
            {
                if (string.IsNullOrWhiteSpace(sor))
                {
                    continue;
                }

                string[] mezok = sor.Split(';');
                string nev = mezok[0];
                DateTime datum = DateTime.ParseExact(mezok[1], "yyyy.MM.dd", CultureInfo.InvariantCulture);

                orszagok.Add(new Orszag(nev, datum));
            }
        }

        // 3. feladat: hány tagállama volt 2018-ban az Európai Uniónak
        public void TagallamokSzama()
        {
            Console.WriteLine($"3. feladat: EU tagállamainak száma: {orszagok.Count} db");
        }

        // 4. feladat: 2007-ben csatlakozott országok száma
        public void Csatlakozas2007Ben()
        {
            int szam = orszagok.Count(o => o.Csatlakozas.Year == 2007);
            Console.WriteLine($"4. feladat: 2007-ben {szam} ország csatlakozott.");
        }

        // 5. feladat: Magyarország csatlakozási dátuma
        public void MagyarorszagCsatlakozasa()
        {
            var magyarorszag = orszagok.First(o => o.Nev == "Magyarország");
            Console.WriteLine($"5. feladat: Magyarország csatlakozásának dátuma: {magyarorszag.Csatlakozas:yyyy.MM.dd}");
        }

        // 6. feladat: volt-e májusi csatlakozás
        public void VoltEMajusiCsatlakozas()
        {
            bool volt = orszagok.Any(o => o.Csatlakozas.Month == 5);
            Console.WriteLine(volt ? "6. feladat: Májusban volt csatlakozás!" : "6. feladat: Májusban nem volt csatlakozás!");
        }

        // 7. feladat: utoljára csatlakozott tagállam
        public void UtoljaraCsatlakozottOrszag()
        {
            var utolso = orszagok.OrderByDescending(o => o.Csatlakozas).First();
            Console.WriteLine($"7. feladat: Legutoljára csatlakozott ország: {utolso.Nev}");
        }

        // 8. feladat: évenkénti statisztika a csatlakozott országok számáról
        public void EvenkentiStatisztika()
        {
            Console.WriteLine("8. feladat: Statiszika");

            var csoportok = orszagok.GroupBy(o => o.Csatlakozas.Year);

            foreach (var csoport in csoportok)
            {
                Console.WriteLine($"        {csoport.Key} - {csoport.Count()} ország");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var manager = new EuManager();
            manager.Beolvas("EUcsatlakozas.txt");

            manager.TagallamokSzama();
            manager.Csatlakozas2007Ben();
            manager.MagyarorszagCsatlakozasa();
            manager.VoltEMajusiCsatlakozas();
            manager.UtoljaraCsatlakozottOrszag();
            manager.EvenkentiStatisztika();
        }
    }
}