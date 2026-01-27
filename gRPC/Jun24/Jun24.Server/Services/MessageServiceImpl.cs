using Grpc.Core;
using Messages;
using System.Collections.Concurrent;


public class MessageServiceImpl : MessageService.MessageServiceBase
{
    private static  ConcurrentDictionary<int, Message> messages = new();
    private static int  id = 0;
    public override Task<DefaultResponse> SendMessage(Message message, ServerCallContext context) 
    {
        var newId = Interlocked.Increment(ref id);
        messages[newId] = message;
        return Task.FromResult(new DefaultResponse { Id = newId, Text = "Added" });
    }

    public override Task<DefaultResponse> DeleteMessage(MessageDeleteRequest messageDeleteRequest, ServerCallContext context) 
    {
        messages.TryRemove(messageDeleteRequest.Id, out _);
        return Task.FromResult(new DefaultResponse { Id = messageDeleteRequest.Id, Text = "Deleted" });
    }

    public override async Task ListMessages(EmptyRequest emptyRequest, IServerStreamWriter<Message> response, ServerCallContext context) 
    {
        foreach (var message in messages.Values)
        {
            await response.WriteAsync(message);
            await Task.Delay(500);
        }
    }
}
