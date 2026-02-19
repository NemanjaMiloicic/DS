import java.rmi.*;
import java.rmi.server.UnicastRemoteObject;

public class ProstImpl extends UnicastRemoteObject implements Prost {
    public ProstImpl() throws RemoteException {
        super();
    }
    public void ProstiBrojevi(Podatak p) throws RemoteException {
        for(int i = p.n ; i < p.m ; i++)
        {
            boolean prost = true;
            for(int j = 2 ; j <= Math.sqrt(i) ; j++)
            {
                if(i % j == 0){
                    prost = false;
                    break;
                }
            }
            if(prost)
                p.clb.Rezultati(i);
        }
    }
}