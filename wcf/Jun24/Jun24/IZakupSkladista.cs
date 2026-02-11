using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Jun24
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IZakupSkladista" in both code and config file together.
    [ServiceContract]
    public interface IZakupSkladista
    {
        [OperationContract]
        void ZakupiSkladiste(Vlasnik vlasnik, Skladiste skladiste);

        [OperationContract]
        List<Skladiste> VratiAktivnaSkladistaVlasnika(string jmbg);

        [OperationContract]
        List<Vlasnik> VratiVlasnikeAktivnihSkladista();

        [OperationContract]
        List<Zakup> VratiIstorijuZakupa();

    }
}
