using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Jan25
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IChat" in both code and config file together.
    [ServiceContract(CallbackContract = typeof(IChatCallback) , SessionMode = SessionMode.Required)]
    public interface IChat
    {
        [OperationContract(IsOneWay = true)]
        void SendMessage(Message message);

        [OperationContract]
        void Register(string nickName);
    }
}
