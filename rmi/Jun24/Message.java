import java.rmi.*;
import java.io.*;
import java.util.*;

public class Message implements Serializable{
	private String naslov;
	private String poruka;
	
	public Message(String naslov,String poruka){
		this.naslov = naslov;
		this.poruka = poruka;
	}
	
	public String getNaslov() { return naslov; }
    public String getPoruka()  { return poruka; }
	
	public String toString() {
		return this.naslov + " " + this.poruka + "\n";
	}
}