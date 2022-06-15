using FundooCommonLayer;
using FundooRepositoryLayer.Context;
using FundooRepositoryLayer.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundooRepositoryLayer.Services
{
    public class CollaborateReository : ICollaborateReository
    {
        private readonly UserContext userContext;

        public CollaborateReository(IConfiguration configuration, UserContext userContext)
        {
            this.Configuration = configuration;
            this.userContext = userContext;
        }
        public IConfiguration Configuration { get; }

        public async Task<string> AddCollaborator(CollaboratorModel collaborate)
        {
            try
            {
                var result = this.userContext.Notes.Where(c => c.NoteId == collaborate.NoteId).SingleOrDefault();
                if(result != null)
                {
                    this.userContext.Collaborator.Add(collaborate);
                    await this.userContext.SaveChangesAsync();
                    return "Collaborator Added";
                }
                else
                {
                    return "Note does not exist";
                }
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
                var removeCollabEmail = this.userContext.Collaborator.Where(c => c.NoteId == noteId && c.CollabEmail == collabEmail).FirstOrDefault();
                this.userContext.Collaborator.Remove(removeCollabEmail);
                await this.userContext.SaveChangesAsync();
                return "Collaborator Removed";
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
                var getCollabs = this.userContext.Collaborator.Where(x => x.NoteId == noteId).ToList();
                if(getCollabs.Count != 0)
                {
                    return getCollabs;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
