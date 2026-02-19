import java.rmi.*;

public interface ProstCallback extends Remote {
    void Rezultati(int broj) throws RemoteException;
}