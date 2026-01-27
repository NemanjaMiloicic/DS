using Grpc.Core;
using Grpc.Net.Client;
using Messages;
var channel = GrpcChannel.ForAddress("https://localhost:7200");
for (int i = 0; i < 5; i++)
{
    try
    {
        var client = new MessageService.MessageServiceClient(channel);
        var requestSend1 = new Message { Text = "Hej1" };
        var requestSend2 = new Message { Text = "Hej2" };
        var requestSend3 = new Message { Text = "Hej3" };
        var requestSend4 = new Message { Text = "Hej4" };
        var requestSend5 = new Message { Text = "Hej5" };
        var requestSend6 = new Message { Text = "Hej6" };
        var requestDel1 = new MessageDeleteRequest { Id = 2 };
        var requestDel2 = new MessageDeleteRequest { Id = 6 };

        Console.WriteLine(client.SendMessage(requestSend1));
        Console.WriteLine(client.SendMessage(requestSend2));
        Console.WriteLine(client.SendMessage(requestSend3));
        Console.WriteLine(client.SendMessage(requestSend4));
        Console.WriteLine(client.SendMessage(requestSend5));
        Console.WriteLine(client.SendMessage(requestSend6));

        Console.WriteLine(client.DeleteMessage(requestDel1));
        Console.WriteLine(client.DeleteMessage(requestDel2));

        var call = client.ListMessages(new EmptyRequest { });
        await foreach (var message in call.ResponseStream.ReadAllAsync()) 
        {
            Console.WriteLine(message.Text);
        }
        break;
    }
    catch (HttpRequestException)
    {
        Console.WriteLine("Server not ready, retrying in 1s");
        await Task.Delay(1000);
    }
}



