using FundooBusinessLayer.Interface;
using FundooCommonLayer;
using FundooRepositoryLayer.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooBusinessLayer.Services
{
    public class NoteManager : INoteManager
    {
        private readonly INoteRepository repository;
        public IConfiguration Configuration { get; }


        public NoteManager(IConfiguration configuration, INoteRepository repository)
        {
            this.repository = repository;
            this.Configuration = configuration;
        }

        public async Task<NoteModel> CreateNote(NoteModel note)
        {
            try
            {
                return await this.repository.CreateNote(note);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<NoteModel> GetNotes(int userId)
        {
            try
            {
                return this.repository.GetNotes(userId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<EditNoteModel> EditNote(EditNoteModel note)
        {
            try
            {
                return await this.repository.EditNote(note);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<NoteModel> ChangeColour(NoteModel note)
        {
            try
            {
                return await this.repository.ChangeColour(note);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<string> NoteArchive(int noteId)
        {
            try
            {
                return await this.repository.NoteArchive(noteId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<string> Pinning(int noteId)
        {
            try
            {
                return await this.repository.Pinning(noteId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<NoteModel> GetArchiveNote(int userId)
        {
            try
            {
                return this.repository.GetArchiveNote(userId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<NoteModel> GetTrashNote(int userId)
        {
            try
            {
                return this.repository.GetTrashNote(userId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<string> DeleteNote(int noteId)
        {
            try
            {
                return await this.repository.DeleteNote(noteId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<string> RestoreNoteFromTrash(int noteId)
        {
            try
            {
                return await this.repository.RestoreNoteFromTrash(noteId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<string> DeletePermanently(int noteId)
        {
            try
            {
                return await this.repository.DeletePermanently(noteId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<string> AddReminderForNote(int noteId,string remindNote)
        {
            try
            {
                return await this.repository.AddReminderForNote(noteId, remindNote);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<string> DeleteReminder(int noteId)
        {
            try
            {
                return await this.repository.DeleteReminder(noteId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public IEnumerable<NoteModel> GetReminderOfNote(int userId)
        {
            try
            {
                return this.repository.GetReminderOfNote(userId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<string> InsertImage(int noteId, IFormFile form)
        {
            try
            {
                return await this.repository.InsertImage(noteId, form);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<string> DeleteImage(int noteId)
        {
            try
            {
                return await this.repository.DeleteImage(noteId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
