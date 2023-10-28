using DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    [DataContract]
    public class Player
    {
        private int playerId;
        private string nickname;
        private string password;
        private string email;

        #region Properties
        [DataMember]
        public int PlayerId { get { return playerId; } set { playerId = value; } }

        [DataMember]
        public string Nickname { get { return nickname; } set { nickname = value; } }
        [DataMember]
        public string Password { get { return password; } set { password = value; } }
        [DataMember]
        public string Email { get { return email; } set { email = value; } }
        

        #endregion

        #region Methods
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override string ToString()
        {
            return $"Nick-{nickname}Pass-{password}email-{email}";
        }
        #endregion
    }
}
