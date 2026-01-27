using Grpc.Core;
using Okt2;
namespace Okt225.Server.Services
{
    public class SquareService : Square.SquareBase
    {
        public override async Task IsSquare(
            IAsyncStreamReader<Numbers> numbers,
            IServerStreamWriter<Response> response,
            ServerCallContext context)
        {
            await foreach (var number in numbers.ReadAllAsync())
            {
                if (number.A == number.B * number.B)
                    await response.WriteAsync(new Response { Text = "Da" });
                else
                    await response.WriteAsync(new Response { Text = "Ne" });
                await Task.Delay(500); //illusion wooo
            }
        }
    }
}
