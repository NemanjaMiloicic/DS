import java.rmi.Naming;
import java.rmi.registry.LocateRegistry;

public class Server {
    public static void main(String[] args) {
        try {
            LocateRegistry.createRegistry(1099);
        } catch (Exception e) {
            e.printStackTrace();
        }

        try {
            Prost prost = new ProstImpl();
            Naming.rebind("rmi://localhost/ProstiBrojevi", prost);
            System.out.println("Server je pokrenut.");
        } catch (Exception e) {
            e.printStackTrace();
        }
    }
}
