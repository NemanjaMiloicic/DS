using ServiceReference2;

namespace Jun24Client 
{
    class Program 
    {
        static void Main(string[] args)
        {
            ZakupSkladistaClient proxy;
            proxy = new ZakupSkladistaClient();

            Console.WriteLine("Unesite podatke o vlasniku:");
            Console.Write("Ime: ");
            string ime = Console.ReadLine()!;
            Console.Write("Prezime: ");
            string prezime = Console.ReadLine()!;
            Console.Write("JMBG: ");
            string jmbg = Console.ReadLine()!;

            Vlasnik vlasnik = new Vlasnik
            {
                Ime = ime,
                Prezime = prezime,
                Jmbg = jmbg
            };
            Console.WriteLine("Unesite podatke o skladistu:");
            Console.Write("ID skladista: ");
            int id = int.Parse(Console.ReadLine()!);
            Console.Write("Cena: ");
            decimal cena = decimal.Parse(Console.ReadLine()!);
            Console.Write("Pocetak zakupa (yyyy-MM-dd HH:mm): ");
            DateTime pocetak = DateTime.Parse(Console.ReadLine()!);
            Console.Write("Kraj zakupa (yyyy-MM-dd HH:mm): ");
            DateTime kraj = DateTime.Parse(Console.ReadLine()!);

            Skladiste skladiste = new Skladiste
            {
                IdSkladiste = id,
                Cena = cena,
                PocetakZakupa = pocetak,
                KrajZakupa = kraj
            };

            proxy.ZakupiSkladiste(vlasnik, skladiste);
            var aktivna = proxy.VratiAktivnaSkladistaVlasnika(vlasnik.Jmbg);
            Console.WriteLine("Vrati aktivna skladista vlasnika...");
            foreach (Skladiste s in aktivna)
            {
                Console.WriteLine($"Cena:{s.Cena} , pocetak:{s.PocetakZakupa} , kraj:{s.KrajZakupa}");
            }
            var vlasnici = proxy.VratiVlasnikeAktivnihSkladista();
            Console.WriteLine("Vrati vlasnike aktivnih skladista...");
            foreach (Vlasnik v in vlasnici) 
            {
                Console.WriteLine($"Ime:{v.Ime} , prezime:{v.Prezime}, jmbg:{v.Jmbg}");
            }
            Console.WriteLine("Istorija zakupa...");
            var istorijaZakupa = proxy.VratiIstorijuZakupa();
            foreach (Zakup z in istorijaZakupa) 
            {
                Console.WriteLine($"id:{z.Skladiste.IdSkladiste}, jmbg:{z.Vlasnik.Jmbg}");
            }
            Console.ReadLine();
            proxy.Close();
        }
    }
}
