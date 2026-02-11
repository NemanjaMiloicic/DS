	import java.rmi.*;
	import java.rmi.server.*;
	import java.net.*;
	import java.rmi.registry.*;

	public class Server{
		public static void main(String[] args){
			
			try {
				LocateRegistry.createRegistry(1099);
				System.out.println("Java RMI registry created.");
			} catch (RemoteException e) {
				System.out.println("Java RMI registry already exists.");
			}
			
			try{
				Broker broker = new BrokerImpl();
				Naming.rebind("rmi://localhost/BrokerService",broker);
				
				System.out.println("Server startovan...");
			}
			catch(Exception e)
			{
				e.printStackTrace();
			}
		}
	}