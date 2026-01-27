# gRPC

- U okviru Grpc demo projekta pokrivena su sva 4 tipa komunikacije (unarna, serverstream, clientstream, bidirectionalstream)  
- Rešeni blanketi: Jun2024, Okt2_2025  
- Za ispit nije neophodno pisati client program.cs niti server program.cs, samo proto fajlove i servise!  
- Setup projekta:  
  1. Kreirati novi gRPC projekat  
  2. U okviru istog solution-a dodati novi konzolni projekat za klijenta, dodati sledeće NuGet pakete: Google.Protobuf, Grpc.Net.Client, Grpc.Tools  
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
