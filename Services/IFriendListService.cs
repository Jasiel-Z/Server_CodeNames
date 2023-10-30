using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    [ServiceContract(CallbackContract = typeof(IFriendListServiceCallback))]
    public interface IFriendListService
    {
        [OperationContract]
        void SendFriendshipRequest(int idSender, int idReceiver);

        [OperationContract]
        void ResponseToFriendshipRequest(String response, int idRequest);

    }

    [ServiceContract]
    public interface IFriendListServiceCallback
    {
        [OperationContract]
        void showGlobalUsers();

        [OperationContract]
        void showFriends();




    }
}
