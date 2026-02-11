# gRPC

- U okviru Grpc demo projekta pokrivena su sva 4 tipa komunikacije (unarna, serverstream, clientstream, bidirectionalstream)  
- Rešeni blanketi: Jun2024, Okt2_2025  
- Za ispit nije neophodno pisati client program.cs niti server program.cs, `samo proto fajlove i servise!`  
- Setup projekta:  
  1. Kreirati novi gRPC projekat  
  2. U okviru istog solution-a dodati novi konzolni projekat za klijenta, dodati sledeće NuGet pakete: `Google.Protobuf`, `Grpc.Net.Client`, `Grpc.Tools`  
  3. Edit project file (csproj) u klijentu i serveru  
     - Primer za server:  
       ```xml
       <ItemGroup>
           <Protobuf Include="Protos\nazivprotofajla.proto" GrpcServices="Server" />
       </ItemGroup>
       ```  
     - Primer za client:  
       ```xml
       <ItemGroup>
           <Protobuf Include="Protos\nazivprotofajla.proto" GrpcServices="Client" />
       </ItemGroup>
       ```  
  4. Napisati proto fajl u server-u, potom napraviti folder `Protos` u client-u i onda prekopirati isti fajl tamo  
  5. Desni klik na solution → Properties → štiklirati **Multiple Startup Projects** i postaviti oba projekta na startup  
  6. Napisati servis na serveru  
  7. U `Program.cs` na serveru ubaciti taj servis umesto `GreeterService`  
  8. Napisati client `Program.cs` random (pogledajte u gRPC demo primer client `Program.cs`) i pokrenuti app **CTRL+F5**


# RMI

- Rešeni blanketi: Jun2024
- Setup projekta:
  1. Nije neophodno okruženje (ja sam u vscode pisao), kada ispišete sve fajlove pokrenite terminal u folderu gde vam se nalaze svi java fajlovi i napišite sleću komandu
 ```javac *.java
 ```  
  2. Prethodna komanda kompajluje sve java fajlove i kreira .class fajlove, za startovanje servera koristimo
   ```java Server
   ``` 
   a za klijent:
   ```java Client
   ``` 
   **Bez ekstenzija**

- Napomene:
- Za ispit nije neophodno pisati u serveru setovanje localRegistry!
1. Potrebno je identifikovati Remote objekat, kad to ustanovimo, potrebno je najpre kreirati interfejs za njega bitno je napomenuti da extenduje **Remote** a metode bacuju grešku **RemoteException**
2. Potom pravimo implementaciju Remote interfejsa, koji se zove isto kao i interfejs sa sufiksom Impl, on implementuje naš malopre definisani servis, a extenduje **UnicastRemoteObject**
3. Ukoliko postoji objekat koji Remote objekat koristi ( a da nije callback)  on extenduje **Serializable**
4. Ukoliko je neophodan callback ( callback je metod kojim server poziva metodu na klijentu i to uglavnom određenom klijentu) pravimo interfejs za njega on takođe kao i Remote interfejs extenduje **Remote** a metode bacaju **RemoteException**.
5. Njegova implementacija piše se u okviru klijentske aplikacije! Kao unutrašnja klasa. Ona implementira malopre definisani servis i extenduje **UnicastRemoteObject** kao i Remote objekat.
6. Na serveru je potrebno iskoristiti **rebind** kako bi  se startovao servis na određenoj lokaciji
    ```
	RemoteObj remoteObj = new remoteObjImpl();
				Naming.rebind("rmi://localhost/ServiceName",remoteObj);
				
    ```  
7. Klijenta je potrebno povezati na servis korišćenjem **lookup** metode na istoj lokaciji kao i na serveru, i u istoj liniji koda dobijamo pristup Remote objektu koji može pozivati metode sa servera
   ```
   RemoteObj manager = (RemoteObj) Naming.lookup("rmi://localhost/ServiceName");
   manager.HelloWorld(); // HelloWorld je metoda remote objekta koja je definisana u interfejsu remote objekta a implementirana u Impl.
   ```
   