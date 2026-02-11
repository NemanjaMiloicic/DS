import java.rmi.*;

public interface Broker extends Remote{
	void subscribe(String topic,ClientCallback clb) throws RemoteException;
	void publish(String topic,Message msg) throws RemoteException;
}