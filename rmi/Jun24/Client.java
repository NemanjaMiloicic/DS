import java.rmi.*;
import java.rmi.server.*;
import java.util.*;


public class Client{
	
	public static void main(String[] args){
		try{
			Broker manager = (Broker) Naming.lookup("rmi://localhost/BrokerService");
		
		ClientCallbackImpl clb = new ClientCallbackImpl();
		
		manager.subscribe("sport",clb);
		
		Scanner sc = new Scanner(System.in);
		while (true) {
                System.out.print("Unesi naslov poruke (ili 'exit' za kraj): ");
                String title = sc.nextLine();
                if (title.equalsIgnoreCase("exit")) break;

                System.out.print("Unesi sadržaj poruke: ");
                String body = sc.nextLine();

                Message msg = new Message(title, body);
                manager.publish("sport", msg);
            }
		}
		catch(Exception e)
		{
			e.printStackTrace();
		}
	}
	
	public static class ClientCallbackImpl extends UnicastRemoteObject implements ClientCallback {

    public ClientCallbackImpl() throws RemoteException {
        super();
    }

    public void notify(String topic, Message msg) throws RemoteException {
        System.out.println("Stiglo je obavestenje, Topic: " + topic + ": " + msg);
    }
}

}