using System.ServiceModel;

namespace April25
{
    public interface IKalkulatorCallback
    {
        [OperationContract(IsOneWay = true)]
        void Rezultat(Rezultat rezultat);

        [OperationContract(IsOneWay = true)]
        void Greska(string poruka);
    }
}