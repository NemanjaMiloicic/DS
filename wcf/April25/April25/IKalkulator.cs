using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace April25
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IKalkulator" in both code and config file together.
    [ServiceContract(CallbackContract = typeof(IKalkulatorCallback) , SessionMode = SessionMode.Required)]
    public interface IKalkulator
    {
        [OperationContract(IsOneWay = true)]
        void ObrisiRacunanje();

        [OperationContract(IsOneWay = true)]
        void Dodaj(decimal broj);

        [OperationContract(IsOneWay = true)]
        void Oduzmi(decimal broj);

        [OperationContract(IsOneWay = true)]
        void Pomnozi(decimal broj);

        [OperationContract(IsOneWay = true)]
        void Podeli(decimal broj);


    }
}
