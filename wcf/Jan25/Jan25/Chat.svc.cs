using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Jan25
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Chat" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Chat.svc or Chat.svc.cs at the Solution Explorer and start debugging.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class Chat : IChat
    {
        private static readonly Dictionary<string, IChatCallback> callbacks =
            new Dictionary<string, IChatCallback>();


        public void Register(string nickName)
        {
            var callback = OperationContext.Current.GetCallbackChannel<IChatCallback>();
            if (callbacks.ContainsKey(nickName)) 
                callbacks.Remove(nickName); //prethodna sesija isti nickname ne prima vise poruke
            callbacks.Add(nickName , callback);
        }

        public void SendMessage(Message message)
        {
            var currentCallback = OperationContext.Current.GetCallbackChannel<IChatCallback>();
            if (!callbacks.ContainsValue(currentCallback)) //onemogucava send kada je precrtan callback
                return;
            if (message.FromUser == message.ToUser)
                return;
            if (!callbacks.ContainsKey(message.ToUser))
                return;
            message.SentAt = DateTime.Now;
            callbacks[message.ToUser].ReceiveMessage(message);
            callbacks[message.FromUser].ReceiveMessage(message);
        }


    }
}
