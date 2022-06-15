using FundooCommonLayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooRepositoryLayer.Interface
{
    public interface ILabelRepository
    {
        Task<string> AddLabelWithUserId(LabelModel labelModel);
        Task<string> AddLabelWithNoteId(LabelModel labelModel);
        Task<string> DeleteLabel(int userId, string labelName);
        Task<string> RemoveLabel(int labelId);
        Task<string> EditLabel(LabelModel labelModel);
        IEnumerable<string> GetLabelUserId(int userId);
        IEnumerable<string> GetLabelNoteId(int noteId);
    }
}
