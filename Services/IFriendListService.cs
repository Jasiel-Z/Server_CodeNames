using DataModels;
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

        [OperationContract]
        List<Player> GetGlobalUsers();

        [OperationContract]
        List<Player> GetFriends(int idUser);

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
