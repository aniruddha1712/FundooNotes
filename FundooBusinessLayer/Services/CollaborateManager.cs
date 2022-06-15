using FundooBusinessLayer.Interface;
using FundooCommonLayer;
using FundooRepositoryLayer.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooBusinessLayer.Services
{
    public class CollaborateManager : ICollaborateManager
    {
        private readonly ICollaborateReository repository;
        public IConfiguration Configuration { get; }


        public CollaborateManager(IConfiguration configuration, ICollaborateReository repository)
        {
            this.repository = repository;
            this.Configuration = configuration;
        }

        public async Task<string> AddCollaborator(CollaboratorModel collaborate)
        {
            try
            {
                return await this.repository.AddCollaborator(collaborate);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<string> RemoveCollaborator(int noteId, string collabEmail)
        {
            try
            {
                return await this.repository.RemoveCollaborator(noteId,collabEmail);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public IEnumerable<CollaboratorModel> GetAllCollaborators(int noteId)
        {
            try
            {
                return this.repository.GetAllCollaborators(noteId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
