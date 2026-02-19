import java.rmi.*;

public interface Prost extends Remote {
    void ProstiBrojevi(Podatak p) throws RemoteException;
}
