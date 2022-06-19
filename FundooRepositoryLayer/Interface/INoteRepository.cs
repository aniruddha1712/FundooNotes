using FundooCommonLayer;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooRepositoryLayer.Interface
{
    public interface INoteRepository
    {
        Task<NoteModel> CreateNote(NoteModel createNote,int userId);
        List<NoteModel> GetNotes(int userId);
        Task<EditNoteModel> EditNote(EditNoteModel note);
        Task<NoteModel> ChangeColour(NoteModel note);
        Task<string> NoteArchive(int noteId);
        Task<string> Pinning(int noteId);
        List<NoteModel> GetArchiveNote(int userId);
        List<NoteModel> GetTrashNote(int userId);
        Task<string> DeleteNote(int noteId);
        Task<string> RestoreNoteFromTrash(int noteId);
        Task<string> DeletePermanently(int noteId);
        Task<string> AddReminderForNote(int noteId, string remindNote);
        Task<string> DeleteReminder(int noteId);
        IEnumerable<string> GetReminderOfNote(int userId);
        Task<string> InsertImage(int noteId, IFormFile form);
        Task<string> DeleteImage(int noteId);
    }
}
