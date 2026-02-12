using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace April25
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Kalkulator" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Kalkulator.svc or Kalkulator.svc.cs at the Solution Explorer and start debugging.

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class Kalkulator : IKalkulator
    {
        private IKalkulatorCallback callback;
        private string izraz = "";
        private decimal vrednost = 0.0M;

        //protected IKalkulatorCallback CallbackZaSingle // callback u slucaju da je single instancecontextmode...
        //{
        //    get {
        //        return OperationContext.Current.GetCallbackChannel<IKalkulatorCallback>();
        //    }
        //}
        public Kalkulator() 
        {
            callback = OperationContext.Current.GetCallbackChannel<IKalkulatorCallback>();
        }

        private void DodajUIzraz(string operacija, decimal broj) 
        {
            if (string.IsNullOrEmpty(izraz))
                izraz = broj.ToString();
            else 
            {
                izraz += $" {operacija} {broj}";
            }
        }
        private void PosaljiGresku(string greska) 
        {
            if (callback == null)
                return;

            callback.Greska(greska);
        }
        private void PosaljiRezultat() 
        {

            if (callback == null)
                return;

            callback.Rezultat(new Rezultat
            {
                Vrednost = vrednost,
                Izraz = izraz
            });
        }

        private void ProveriPrvuOperaciju(string operacija) 
        {
            if (string.IsNullOrEmpty(izraz) && (operacija != "+"))
                PosaljiGresku("Prva operacija mora biti sabiranje");
        }
        public void Dodaj(decimal broj)
        {
            vrednost += broj;
            DodajUIzraz("+", broj);
            PosaljiRezultat();
        }

        public void ObrisiRacunanje()
        {
            vrednost = 0;
            izraz = "";
            PosaljiRezultat();
        }

        public void Oduzmi(decimal broj)
        {
            ProveriPrvuOperaciju("-");
            vrednost -= broj;
            DodajUIzraz("-", broj);
            PosaljiRezultat();
        }

        public void Podeli(decimal broj)
        {
            if (broj == 0.0M)
            {
                PosaljiGresku("Deljenje sa nulom nije dozvoljeno");
                return;
            }
            ProveriPrvuOperaciju("/");
            vrednost /= broj;
            DodajUIzraz("/", broj);
            PosaljiRezultat();
        }

        public void Pomnozi(decimal broj)
        {
            ProveriPrvuOperaciju("*");
            vrednost *= broj;
            DodajUIzraz("*", broj);
            PosaljiRezultat();
        }
    }
}
