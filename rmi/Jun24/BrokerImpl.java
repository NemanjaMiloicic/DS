import java.rmi.*;
import java.rmi.server.*;
import java.util.*;
import java.util.concurrent.ConcurrentHashMap;


public class BrokerImpl extends UnicastRemoteObject implements Broker{
	private final Map<String,Set<ClientCallback>> topics;
	
	
	private void createTopic(String topic) {
		topics.putIfAbsent(topic, new HashSet<>());
	}
	
	public BrokerImpl() throws RemoteException {
		topics = new ConcurrentHashMap<>();
	}

	public void subscribe(String topic,ClientCallback clb) throws RemoteException{
		createTopic(topic);
		topics.get(topic).add(clb);
		System.out.println("Klijent se pretplatio na temu " + topic);
	}
	
	
	public void publish(String topic,Message msg) throws RemoteException{
		createTopic(topic);
		for(ClientCallback c : topics.get(topic)){
			c.notify(topic,msg);
		}
	}

}