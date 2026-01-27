using Grpc.Core;
using Grpc.Net.Client;
using Okt2;



var channel = GrpcChannel.ForAddress("https://localhost:7119");
for (int i = 0; i < 5; i++)
{
    try 
    {
        var client = new Square.SquareClient(channel);
        var call = client.IsSquare();

        var readTask = Task.Run(async () =>
        {
            await foreach (var reply in call.ResponseStream.ReadAllAsync()) 
            {
                Console.WriteLine(reply.Text);
            }
        });

        var request1 = new Numbers { A = 9 , B = 3 };
        var request2 = new Numbers { A = 9 , B = 0 };
        var request3 = new Numbers { A = 4 , B = 2 };
        var request4 = new Numbers { A = 25 , B = 5 };
        var request5 = new Numbers { A = 68, B = 9 };
        List<Numbers> numberPairs = [request1, request2, request3, request4, request5];
        foreach (var numberPair in numberPairs) 
        {
            await call.RequestStream.WriteAsync(numberPair);
        }
        await call.RequestStream.CompleteAsync();
        await readTask;
        break;
    }
    catch (HttpRequestException) 
    {
        Console.WriteLine("Server not ready, retrying in 1s");
        await Task.Delay(1000);
    }
}

