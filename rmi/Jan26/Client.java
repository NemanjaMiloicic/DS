import java.rmi.RemoteException;
import java.rmi.server.UnicastRemoteObject;

public class Client {

    public static void main(String[] args) {
        try {
            Prost prost = (Prost) java.rmi.Naming.lookup("rmi://localhost/ProstiBrojevi");
            ProstCallback clb = new CallbackImpl();
            Podatak p = new Podatak();
            p.setM(100);
            p.setN(10);
            p.setClb(clb);
            prost.ProstiBrojevi(p);
        } catch (Exception e) {
            e.printStackTrace();
        }
    }


    public static class CallbackImpl extends UnicastRemoteObject implements ProstCallback {
        public CallbackImpl() throws RemoteException {
            super();
        }

        
        public void Rezultati(int broj) throws RemoteException {
            System.out.println("Prost broj: " + broj);
        }
    }
}
