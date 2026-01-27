using Grpc.Core;

namespace GrpcDemo.Services
{
    public class GreeterService : Greeter.GreeterBase
    {
      

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context) //unary
        {
            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
        }
        public override async Task ServerStreamingSayHello(HelloRequest request, //serverStreaming
           IServerStreamWriter<HelloReply> responseStream,
           ServerCallContext context)
        {
            for (int i = 0; i < 5; i++)
            {
                await responseStream.WriteAsync(new HelloReply
                {
                    Message = "Hello " + request.Name + " " + (i+1)
                });

                await Task.Delay(500);
            }

        }
        public override async Task<HelloReply> ClientStreamingSayHello( //clientStreaming
            IAsyncStreamReader<HelloRequest> requestStream, ServerCallContext context)
        {

            int count = 0;

            await foreach (var request in requestStream.ReadAllAsync()) 
            {
                Console.WriteLine("Recieved: " + request.Name);
                count++;
            }
            return new HelloReply { Message = $"Recieved {count} names" };
        }

        public override async Task BidirectionalStreamingSayHello(IAsyncStreamReader<HelloRequest> requestStream, //Bidirectional Streaming
            IServerStreamWriter<HelloReply> responseStream,
            ServerCallContext context) 
        {
            await foreach (var request in requestStream.ReadAllAsync()) 
            {
                Console.WriteLine("Recieved: " + request.Name);

                await responseStream.WriteAsync(new HelloReply
                {
                    Message = "Hello " + request.Name
                });
            }

        }

    }
}
