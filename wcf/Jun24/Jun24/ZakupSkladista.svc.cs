using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Jun24
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ZakupSkladista" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ZakupSkladista.svc or ZakupSkladista.svc.cs at the Solution Explorer and start debugging.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ZakupSkladista : IZakupSkladista
    {
        private static List<Zakup> zakupi = new List<Zakup>();
        public List<Skladiste> VratiAktivnaSkladistaVlasnika(string jmbg)
        {
            DateTime trenutnoVreme = DateTime.Now;
            return zakupi
                .Where(z => z.Vlasnik.Jmbg == jmbg &&
                z.Skladiste.PocetakZakupa <= trenutnoVreme &&
                z.Skladiste.KrajZakupa > trenutnoVreme)
                .Select(z => z.Skladiste)
                .ToList();
        }

        public List<Zakup> VratiIstorijuZakupa()
        {
            return zakupi;
        }

        public List<Vlasnik> VratiVlasnikeAktivnihSkladista()
        {
            DateTime trenutnoVreme = DateTime.Now;
            return zakupi.Where(z => z.Skladiste.PocetakZakupa <= trenutnoVreme &&
            z.Skladiste.KrajZakupa > trenutnoVreme)
            .Select(z => z.Vlasnik)
            .Distinct()
            .ToList();
        }

        public void ZakupiSkladiste(Vlasnik vlasnik, Skladiste skladiste)
        {
            Zakup noviZakup = new Zakup
            {
                Vlasnik = vlasnik,
                Skladiste = skladiste
            };
            zakupi.Add(noviZakup);
        }
    }
}
