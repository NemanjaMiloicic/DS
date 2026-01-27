using Grpc.Core;
using Grpc.Net.Client;
using GrpcDemo;

var channel = GrpcChannel.ForAddress("https://localhost:7060");

for (int i = 0; i < 5; i++)
{
    try
    {   
        var client = new Greeter.GreeterClient(channel);

        #region Unary
        //var request = new HelloRequest { Name = "Nemanja" };
        //var response = await client.SayHelloAsync(request);
        //Console.WriteLine(response.Message);
        //break;
        #endregion
        #region ServerStreaming
        //var request = new HelloRequest { Name = "Nemanja" };
        //var call = client.ServerStreamingSayHello(request);
        //await foreach (var reply in call.ResponseStream.ReadAllAsync()) 
        //{
        //    Console.WriteLine(reply.Message);
        //}
        //Console.WriteLine("Stream ended");
        //break;
        #endregion
        #region ClientStreaming
        //var call = client.ClientStreamingSayHello();
        //foreach (var name in new[] { "Nemanja", "Lazar", "Pera"})
        //{
        //    await call.RequestStream.WriteAsync(new HelloRequest { Name = name });
        //}
        //await call.RequestStream.CompleteAsync();

        //var response = await call;
        //Console.WriteLine(response.Message);
        //break;
        #endregion
        #region BidirectionalStreaming
        var call = client.BidirectionalStreamingSayHello();
        string[] names = { "Nemanja", "Lazar", "Pera" };
        //Read replies, while requesting
        var readTask = Task.Run(async () =>
        {
            await foreach (var reply in call.ResponseStream.ReadAllAsync())
            {
                Console.WriteLine(reply.Message);
            }
        });

        // Request names
        foreach (var name in names)
        {
            await call.RequestStream.WriteAsync(new HelloRequest { Name = name });
            await Task.Delay(300);
        }

        await call.RequestStream.CompleteAsync(); //finished sending
        await readTask; //finished recieving

        Console.WriteLine("Bidirectional streaming ended");
        break;

        #endregion

    }
    catch (HttpRequestException) 
    {
        Console.WriteLine("Server not ready, retrying in 1s");
        await Task.Delay(1000);
    }

}
Console.ReadLine();