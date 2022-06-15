using FundooCommonLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooRepositoryLayer.Context
{
    public class UserContext: DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {

        }

        public DbSet<RegisterModel> Users { get; set; }
        public DbSet<NoteModel> Notes { get; set; }
        public DbSet<CollaboratorModel> Collaborator { get; set; }

    }
}
