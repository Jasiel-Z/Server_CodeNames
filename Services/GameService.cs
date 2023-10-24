using DataModels;
using Logic;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    //Aquí van las implementaciones de todos los servicios del juego
    public partial class GameService : IPlayerManager
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
                            orderby b.Player_Id
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

}
