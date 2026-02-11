import java.rmi.*;

public interface ClientCallback extends Remote{
	void notify(String topic,Message msg) throws RemoteException;
}