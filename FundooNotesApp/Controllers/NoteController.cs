using FundooBusinessLayer.Interface;
using FundooCommonLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotesApp.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class NoteController : Controller
    {
        private readonly INoteManager manager;
        public NoteController(INoteManager manager)
        {
            this.manager = manager;
        }

        [HttpPost]
        [Route("createnote")]
        public async Task<IActionResult> CreateNote([FromBody] NoteModel mynotes)
        {
            try
            {
                var result = await this.manager.CreateNote(mynotes);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Note Created", Data = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string> { Status = false, Message = "Cannot create Note" });
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpGet]
        [Route("getnotes")]
        public IActionResult GetNotes(int userId)
        {
            try
            {
                var result = this.manager.GetNotes(userId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Here is your Note", Data = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string> { Status = false, Message = "Note does not exist" });
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        [HttpPut]
        [Route("editnote")]
        public async Task<IActionResult> EditNote([FromBody] EditNoteModel note)
        {
            try
            {
                var result = await this.manager.EditNote(note);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Note Edited", Data = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string> { Status = false, Message = "Cannot edit Note" });
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        [HttpPut]
        [Route("changecolour")]
        public async Task<IActionResult> ChangeColour(NoteModel note)
        {
            try
            {
                var result = await this.manager.ChangeColour(note);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Colour changed", Data = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string> { Status = false, Message = "Cannot change colour " });
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        [HttpPatch]
        [Route("archivenote")]
        public async Task<IActionResult> NoteArchive(int noteId)
        {
            try
            {
                string result = await this.manager.NoteArchive(noteId);
                if (result.Equals("Archived") || result.Equals("Unarchived"))
                {
                    return this.Ok(new { Status = true, Message = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string> { Status = false, Message = result });
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        [HttpPatch]
        [Route("pinning")]
        public async Task<IActionResult> Pinning(int noteId)
        {
            try
            {
                string result = await this.manager.Pinning(noteId);
                if (result.Equals("Note pinned") || result.Equals("Note unpinned") || result.Equals("Note unarchived and pinned"))
                {
                    return this.Ok(new { Status = true, Message = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string> { Status = false, Message = result });
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        [HttpGet]
        [Route("getarchivenotes")]
        public IActionResult GetArchiveNote(int userId)
        {
            try
            {
                var result = this.manager.GetArchiveNote(userId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Here is your Note", Data = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string> { Status = false, Message = "Cannot read notes" });
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        [HttpDelete]
        [Route("movetotrash")]
        public async Task<IActionResult> DeleteNote(int noteId)
        {
            try
            {
                string result = await this.manager.DeleteNote(noteId);
                if (result.Equals("Note unpinned and Trashed") || result.Equals("Note Trashed"))
                {
                    return this.Ok(new { Status = true, Message = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string> { Status = false, Message = result });
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpGet]
        [Route("gettrashnotes")]
        public IActionResult GetTrashNote(int userId)
        {
            try
            {
                var result = this.manager.GetTrashNote(userId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Here is your Note", Data = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string> { Status = false, Message = "Cannot read notes" });
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        [HttpGet]
        [Route("restorefromtrash")]
        public async Task<IActionResult> RestoreNoteFromTrash(int noteId)
        {
            try
            {
                string result = await this.manager.RestoreNoteFromTrash(noteId);
                if (result.Equals("Note Restored from trash"))
                {
                    return this.Ok(new { Status = true, Message = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string> { Status = false, Message = result });
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        [HttpDelete]
        [Route("deleteforever")]
        public async Task<IActionResult> DeletePermanently(int noteId)
        {
            try
            {
                string result = await this.manager.DeletePermanently(noteId);
                if (result.Equals("Note Deleted permanently"))
                {
                    return this.Ok(new { Status = true, Message = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string> { Status = false, Message = result });
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        [HttpPatch]
        [Route("addreminder")]
        public async Task<IActionResult> AddReminderForNote(int noteId,string remindNote)
        {
            try
            {
                string result = await this.manager.AddReminderForNote(noteId, remindNote);
                if (result.Equals("Reminder Added"))
                {
                    return this.Ok(new { Status = true, Message = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string> { Status = false, Message = result });
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        [HttpPatch]
        [Route("deletereminder")]
        public async Task<IActionResult> DeleteReminder(int noteId)
        {
            try
            {
                string result = await this.manager.DeleteReminder(noteId);
                if (result.Equals("Reminder deleted"))
                {
                    return this.Ok(new { Status = true, Message = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string> { Status = false, Message = result });
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        [HttpGet]
        [Route("showreminder")]
        public IActionResult GetReminderOfNote(int userId)
        {
            try
            {
                var result = this.manager.GetReminderOfNote(userId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Displaying reminder", Data = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string> { Status = false, Message = "User does not exist/Reminder is not added" });
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        [HttpPatch]
        [Route("addimage")]
        public async Task<IActionResult> InsertImage(int noteId, IFormFile form)
        {
            try
            {
                string result = await this.manager.InsertImage(noteId, form);
                if (result.Equals("Image Added"))
                {
                    return this.Ok(new { Status = true, Message = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string> { Status = false, Message = result });
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        [HttpPatch]
        [Route("deleteimage")]
        public async Task<IActionResult> DeleteImage(int noteId)
        {
            try
            {
                string result = await this.manager.DeleteImage(noteId);
                if (result.Equals("Image Removed"))
                {
                    return this.Ok(new { Status = true, Message = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string> { Status = false, Message = result });
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
