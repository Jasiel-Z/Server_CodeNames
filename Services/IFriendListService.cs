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
        void SendFriendRequest(string toUser);

        [OperationContract]
        void AcceptFriendRequest(string fromUser);

        [OperationContract(IsOneWay = true)]
        void GetFriendList();

    }

    [ServiceContract]
    public interface IFriendListServiceCallback
    {

        [OperationContract]
        void FriendRequestReceived(string fromUser);

        [OperationContract]
        void FriendRequestAccepted(string fromUser);

        [OperationContract]
        void FriendListUpdated(List<string> friends);

        void ShowFriendList();

       
    }
}
