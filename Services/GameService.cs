using DataModels;
using Logic;
using Microsoft.ApplicationInsights.Extensibility.Implementation;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    //Aquí van las implementaciones de todos los servicios del juego
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.Single)]


    public partial class GameService : IPlayerManagerService
    {
        public void AddUserAccountToDatabase(string username, string email, string password)
        {
            using (var databaseContext = new CodeNamesBDEntities())
            {
                var user = new DataModels.Player
                {
                    Username = username,
                    Email = email,
                    Password = password
                };

                databaseContext.PlayerSet.Add(user);
                databaseContext.SaveChanges();
            }
        }

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public void ShowUsersAccounts()
        {
            using (var databaseContext = new CodeNamesBDEntities())
            {
                var user = new DataModels.Player();
                var query = from b in databaseContext.PlayerSet
                            orderby b.IdPlayer
                            select b;

                Console.WriteLine("All blogs in the database:");
                foreach (var item in query)
                {
                    Console.WriteLine(item.Username);
                    Console.WriteLine(item.Email);
                }
            }
        }

        public DataModels.Player Login(string nickname, string password)
        {
            using (var databaseContext = new CodeNamesBDEntities())
            {
                var playerAcountt = databaseContext.PlayerSet
                   .FirstOrDefault(u => u.Username == nickname && u.Password == password);

                return playerAcountt;

            }


        }

    }


    public partial class GameService : IFriendListService
    {
        public void SendFriendshipRequest(int idSender, int idReceiver)
        {
            using (var databaseContext = new CodeNamesBDEntities())
            {
                var frienshipRequest = new DataModels.FriendshipRequest
                {
                    IdReceiverPlayer = idReceiver,
                    IdSenderPlayer = idSender
                };

                databaseContext.FriendshipRequestSet.Add(frienshipRequest);
                databaseContext.SaveChanges();
            }
        }

        public void ResponseToFriendshipRequest(String response, int idRequest)
        {
            var databaseContext = new CodeNamesBDEntities();
            FriendshipRequest frienshipRequest = new FriendshipRequest();
            using (databaseContext)
            {
                frienshipRequest = databaseContext.FriendshipRequestSet.FirstOrDefault(u => u.IdFriendshipRequest == idRequest);
            }

            if (response == "accept")
            {
                using (databaseContext)
                {
                    var friendship = new DataModels.Friendship
                    {
                        IdPlayer1 = frienshipRequest.IdReceiverPlayer,
                        IdPlayer2 = frienshipRequest.IdSenderPlayer
                    };

                    databaseContext.FriendshipSet.Add(friendship);
                    databaseContext.SaveChanges();
                }
            }

            databaseContext.FriendshipRequestSet.Remove(frienshipRequest);
            databaseContext.SaveChanges();
        }

        public List<DataModels.Player> GetGlobalUsers()
        {
            throw new NotImplementedException();
        }

        public List<DataModels.Player> GetFriends(int idUser)
        {
            throw new NotImplementedException();
        }
    }

    public partial class GameService : IGameManagerService
    {
        private List<Logic.Room> globalRooms = new List<Room>();

        public bool CheckQuota(string roomId)
        {
            throw new NotImplementedException();
        }

        public void Connect(string username, string roomId, string message)
        {
            Logic.Player player = new Logic.Player()
            {
                Nickname = username,
                AOperationContext = System.ServiceModel.OperationContext.Current,
                
            };

            var room = globalRooms.FirstOrDefault(r => r.Id.Equals(roomId));
            if (room.Players.Count() > 0)
            {
                SendMessage($": {player.Nickname} {message}!", player.Nickname, roomId);
            }
            room.Players.Add(player);
            room.CurrentPlayers++;
        }

        public bool CreateRoom(string hostUsername, string roomId)
        {
            Room newRoom = new Logic.Room()
            {
                Id = roomId,
                HostUsername = hostUsername,
                MatchStatus = RoomStatus.Waiting,
                CurrentPlayers = 0,
                Players = new List<Logic.Player>(),

            };

            globalRooms.Add(newRoom);
            return true;
        }
        public Room GetRoom(String roomId)
        {
            var room = globalRooms.FirstOrDefault(r => r.Id == roomId);
            return room;
        }

        public void Disconnect(string username, string roomId, string message)
        {
            Logic.Player player = null;
            var room = globalRooms.FirstOrDefault(r => r.Id.Equals(roomId));

            if (room != null)
            {
                player = room.Players.FirstOrDefault(i => i.Nickname.Equals(username));
            }

            if (player != null)
            {
                room.Players.Remove(player);
                room.CurrentPlayers--;
                if (room.Players.Count() == 0)
                {
                    globalRooms.Remove(room);
                }
                else
                {
                    SendMessage($": {player.Nickname} {message}!", player.Nickname, roomId);
                }
            }
        }

        public string GenerateRoomCode()
        {
            Guid roomId = Guid.NewGuid();
            return roomId.ToString();
            
        }

        public List<Logic.Player> RecoverRoomPlayers(string roomId)
        {
            var roomPlayersList = globalRooms.FirstOrDefault(r => r.Id.Equals(roomId)).Players;
            return roomPlayersList;
        }

        public void SendMessage(string message, string username, string roomId)
        {
            var room = globalRooms.FirstOrDefault(r => r.Id.Equals(roomId));
            if (room != null && room.Players != null)
            {
                foreach (var player in room.Players)
                {
                    string answer = DateTime.Now.ToShortTimeString();
                    var anotherPlayer = room.Players.FirstOrDefault(i => i.Nickname.Equals(username));
                    if (anotherPlayer != null)
                    {
                        answer += $": {anotherPlayer.Nickname} ";
                    }
                    answer += message;
                    player.AOperationContext.GetCallbackChannel<IGameServiceCallback>().MessageCallBack(answer);
                }
            }
        }
    }

    partial class GameService : ILobbyService
    {
        public void SubscribeToUserEvents()
        {
            throw new NotImplementedException();
        }

        public void UnsubscribeFromUserEvents()
        {
            throw new NotImplementedException();
        }
    }



}


