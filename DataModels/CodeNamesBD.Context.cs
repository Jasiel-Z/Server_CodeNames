﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataModels
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CodeNamesBDEntities : DbContext
    {
        public CodeNamesBDEntities()
            : base("name=CodeNamesBDEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Player> PlayerSet { get; set; }
        public virtual DbSet<FrienshipRequest> FrienshipRequestSet { get; set; }
        public virtual DbSet<Friendship> FriendshipSet { get; set; }
    }
}
