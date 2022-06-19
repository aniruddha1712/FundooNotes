using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using FundooCommonLayer;
using FundooRepositoryLayer.Context;
using FundooRepositoryLayer.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace FundooRepositoryLayer.Services
{
    public class NoteRepository : INoteRepository
    {
        private readonly UserContext userContext;

        public NoteRepository(IConfiguration configuration, UserContext userContext)
        {
            this.Configuration = configuration;
            this.userContext = userContext;
        }
        public IConfiguration Configuration { get; }

        public async Task<NoteModel> CreateNote(NoteModel createNote,int userId)
        {
            try
            {
                var user = userContext.Users.Where(u => u.UserId == userId).FirstOrDefault();
                NoteModel note = new NoteModel();
                note.UserId = user.UserId;
                note.Title = createNote.Title;
                note.TakeNote = createNote.TakeNote;
                note.Reminder = createNote.Reminder;
                note.Pinned = createNote.Pinned;
                note.Archieve = createNote.Archieve;
                note.Trash = createNote.Trash;
                note.Colour = createNote.Colour;
                note.Image = createNote.Image;

                if (note.Title != null || note.TakeNote != null)
                {
                    this.userContext.Notes.Add(note);
                    await this.userContext.SaveChangesAsync();
                    return note;
                }
                else
                {
                    return null;
                }
            }
            catch (ArgumentException e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<NoteModel> GetNotes(int userId)
        {
            try
            {
                //var myNotes = this.userContext.Notes.Where(x => x.UserId == userId && x.Trash == false && x.Archieve == false).ToList();
                var myNotes = (from note in this.userContext.Notes
                               where note.UserId == userId && note.Archieve == false
                               && note.Trash == false select note).ToList();

                if (myNotes.Count != 0)
                {
                    return myNotes;
                }
                else
                    return null;
            }
            catch (ArgumentException e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<EditNoteModel> EditNote(EditNoteModel note)
        {
            try
            {
                var isNoteId = this.userContext.Notes.Where(x => x.NoteId == note.NoteId).FirstOrDefault();
                if (isNoteId != null)
                {
                    if (note != null)
                    {
                        isNoteId.Title = note.Title;
                        isNoteId.TakeNote = note.TakeNote;
                        this.userContext.Notes.Update(isNoteId);
                        await this.userContext.SaveChangesAsync();
                        return note;
                    }
                    return null;
                }
                else
                {
                    return null;
                }
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
                var isNoteId = this.userContext.Notes.Where(x => x.NoteId == note.NoteId).FirstOrDefault();
                if (isNoteId != null)
                {
                    if (note != null)
                    {
                        isNoteId.Colour = note.Colour;
                        this.userContext.Notes.Update(isNoteId);
                        await this.userContext.SaveChangesAsync();
                        return note;
                    }
                    return null;
                }
                else
                {
                    return null;
                }
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
                string res;
                var isNoteId = this.userContext.Notes.Where(x => x.NoteId == noteId).FirstOrDefault();
                if (isNoteId != null)
                {
                    if (isNoteId.Archieve == false)
                    {
                        isNoteId.Archieve = true;
                        if (isNoteId.Pinned == true)
                        {
                            isNoteId.Pinned = false;
                            res = "Archived";
                        }
                        else
                            res = "Archived";
                    }
                    else
                    {
                        isNoteId.Archieve = false;
                        res = "Unarchived";
                    }
                    this.userContext.Notes.Update(isNoteId);
                    await this.userContext.SaveChangesAsync();
                }
                else
                {
                    res = "cannot Archive";
                }
                return res;
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
                string res;
                var isNoteId = this.userContext.Notes.Where(x => x.NoteId == noteId).FirstOrDefault();
                if (isNoteId != null)
                {
                    if (isNoteId.Pinned == false)
                    {
                        isNoteId.Pinned = true;
                        if (isNoteId.Archieve == true)
                        {
                            isNoteId.Archieve = false;
                            res = "Note unarchived and pinned";
                        }
                        else
                        {
                            res = "Note pinned";
                        }
                    }
                    else
                    {
                        isNoteId.Pinned = false;
                        res = "Note unpinned";
                    }
                    this.userContext.Notes.Update(isNoteId);
                    await this.userContext.SaveChangesAsync();
                }
                else
                {
                    res = "Note does not exist";
                }
                return res;
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
                var myNotes = this.userContext.Notes.Where(x => x.UserId == userId &&
                        x.Trash == false && x.Archieve == true).ToList();

                if (myNotes != null)
                {
                    return myNotes;
                }
                else
                    return null;
            }
            catch (ArgumentException e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<NoteModel> GetTrashNote(int userId)
        {
            try
            {
                var myNotes = this.userContext.Notes.Where(x => x.UserId == userId &&
                        x.Trash == true).ToList();

                if (myNotes != null)
                {
                    return myNotes;
                }
                else
                    return null;
            }
            catch (ArgumentException e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<string> DeleteNote(int noteId)
        {
            try
            {
                var isNoteId = this.userContext.Notes.Where(x => x.NoteId == noteId).SingleOrDefault();
                if (isNoteId != null)
                {
                    isNoteId.Trash = true;
                    if (isNoteId.Pinned == true)
                    {
                        isNoteId.Pinned = false;
                        this.userContext.Notes.Update(isNoteId);
                        await this.userContext.SaveChangesAsync();
                        return "Note unpinned and Trashed";
                    }
                    else
                    {
                        this.userContext.Notes.Update(isNoteId);
                        await this.userContext.SaveChangesAsync();
                        return "Note Trashed";
                    }
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
        public async Task<string> RestoreNoteFromTrash(int noteId)
        {
            try
            {
                var isNoteId = this.userContext.Notes.Where(t => t.NoteId == noteId && t.Trash == true).FirstOrDefault();
                if (isNoteId != null)
                {
                    isNoteId.Trash = false;
                    this.userContext.Notes.Update(isNoteId);
                    await this.userContext.SaveChangesAsync();
                    return "Note Restored from trash";
                }
                else
                {
                    return "Note does not exist in trash";
                }
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
                var isNoteId = this.userContext.Notes.Where(t => t.NoteId == noteId && t.Trash == true).FirstOrDefault();
                if (isNoteId != null)
                {
                    this.userContext.Notes.Remove(isNoteId);
                    await this.userContext.SaveChangesAsync();
                    return "Note Deleted permanently";
                }
                else
                {
                    return "Note does not exist in trash";
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<string> AddReminderForNote(int noteId, string remindNote)
        {
            try
            {
                var isNoteId = this.userContext.Notes.Where(t => t.NoteId == noteId).FirstOrDefault();
                if (isNoteId != null)
                {
                    isNoteId.Reminder = remindNote;
                    this.userContext.Notes.Update(isNoteId);
                    await this.userContext.SaveChangesAsync();
                    return "Reminder Added";
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
        public async Task<string> DeleteReminder(int noteId)
        {
            try
            {
                var isNoteId = this.userContext.Notes.Where(t => t.NoteId == noteId).FirstOrDefault();
                if (isNoteId != null)
                {
                    isNoteId.Reminder = null;
                    this.userContext.Notes.Update(isNoteId);
                    await this.userContext.SaveChangesAsync();
                    return "Reminder deleted";
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
        public IEnumerable<string> GetReminderOfNote(int userId)
        {
            try
            {
                var myNotes = this.userContext.Notes.Where(x => x.UserId == userId &&
                        x.Reminder != null).Select(r => r.Reminder).ToList();

                if (myNotes.Count != 0)
                {
                    return myNotes;
                }
                else
                    return null;
            }
            catch (ArgumentException e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<string> InsertImage(int noteId,IFormFile form)
        {
            try
            {
                var isNoteId = this.userContext.Notes.Where(x => x.NoteId == noteId).SingleOrDefault();
                if(isNoteId != null)
                {
                    //Account account = new Account(Configuration["Cloudinary:CloudName"], Configuration["Cloudinary:ApiKey"], Configuration["Cloudinary:ApiSecret"]);
                    //Cloudinary cloudObj = new Cloudinary(account);
                    var cloudObj = new Cloudinary(new Account("digitral-hyderabad", "559957723662338", "iPytFttGEQrvYg9wf3NG_0MXckk"));
                    var uploadImage = new ImageUploadParams()
                    {
                        File = new FileDescription(form.FileName, form.OpenReadStream()),
                    };
                    var uploadRes = cloudObj.Upload(uploadImage);
                    var uploadPath = uploadRes.Url;
                    isNoteId.Image = uploadPath.ToString();
                    this.userContext.Notes.Update(isNoteId);
                    await this.userContext.SaveChangesAsync();
                    return "Image Added";
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
        public async Task<string> DeleteImage(int noteId)
        {
            try
            {
                var isNoteId = this.userContext.Notes.Where(n => n.NoteId == noteId).SingleOrDefault();
                if(isNoteId != null)
                {
                    isNoteId.Image = null;
                    this.userContext.Notes.Update(isNoteId);
                    await this.userContext.SaveChangesAsync();
                    return "Image Removed";
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

    }
}
