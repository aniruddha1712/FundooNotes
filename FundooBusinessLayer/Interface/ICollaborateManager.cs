using FundooCommonLayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooBusinessLayer.Interface
{
    public interface ICollaborateManager
    {
        Task<string> AddCollaborator(CollaboratorModel collaborate);
        Task<string> RemoveCollaborator(int noteId, string collabEmail);
        IEnumerable<CollaboratorModel> GetAllCollaborators(int noteId);
    }
}
