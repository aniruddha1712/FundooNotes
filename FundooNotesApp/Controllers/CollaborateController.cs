using FundooBusinessLayer.Interface;
using FundooCommonLayer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotesApp.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class CollaborateController : Controller
    {

        private readonly ICollaborateManager manager;
        public CollaborateController(ICollaborateManager manager)
        {
            this.manager = manager;
        }

        [HttpPost]
        [Route("addcollaborator")]
        public async Task<IActionResult> AddCollaborator([FromBody] CollaboratorModel collaborate)
        {
            try
            {
                var result = await this.manager.AddCollaborator(collaborate);
                if (result == "Collaborator Added")
                {
                    return this.Ok(new { Status = true, Message = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string> { Status = false, Message = "Cannot Add Collaborator" });
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        [HttpDelete]
        [Route("removecollaborator")]
        public async Task<IActionResult> RemoveCollaborator(int noteId, string collabEmail)
        {
            try
            {
                var result = await this.manager.RemoveCollaborator(noteId, collabEmail);
                if (result == "Collaborator Removed")
                {
                    return this.Ok(new { Status = true, Message = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string> { Status = false, Message = "Cannot remove Collaborator" });
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        [HttpGet]
        [Route("getcollaborator")]
        public IActionResult GetAllCollaborators(int noteId)
        {
            try
            {
                var result = this.manager.GetAllCollaborators(noteId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string> { Status = false, Message = "Cannot get Collaborator" });
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}