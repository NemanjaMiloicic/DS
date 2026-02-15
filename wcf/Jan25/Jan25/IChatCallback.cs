using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Jan25
{
    public interface IChatCallback
    {
        [OperationContract(IsOneWay = true)]
        void ReceiveMessage(Message message);


    }
}
