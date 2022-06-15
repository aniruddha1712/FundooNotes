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
    public class LabelController : Controller
    {
        private readonly ILabelManager manager;
        public LabelController(ILabelManager manager)
        {
            this.manager = manager;
        }

        [HttpPost]
        [Route("addlabelwuserid")]
        public async Task<IActionResult> AddLabelWithUserId([FromBody] LabelModel labelModel)
        {
            try
            {
                var result = await this.manager.AddLabelWithUserId(labelModel);
                if (result == "Label Added")
                {
                    return this.Ok(new { Status = true, Message = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string> { Status = false, Message = "Cannot add label" });
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpPost]
        [Route("addlabelwnoteid")]
        public async Task<IActionResult> AddLabelWithNoteId([FromBody] LabelModel labelModel)
        {
            try
            {
                var result = await this.manager.AddLabelWithNoteId(labelModel);
                if (result == "Label Added")
                {
                    return this.Ok(new { Status = true, Message = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string> { Status = false, Message = "Cannot add label" });
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpDelete]
        [Route("deletelabel")]
        public async Task<IActionResult> DeleteLabel(int userId, string labelName)
        {
            try
            {
                var result = await this.manager.DeleteLabel(userId, labelName);
                if (result == "Label Deleted")
                {
                    return this.Ok(new { Status = true, Message = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string> { Status = false, Message = result});
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        [HttpDelete]
        [Route("removelabel")]
        public async Task<IActionResult> RemoveLabel(int labelId)
        {
            try
            {
                var result = await this.manager.RemoveLabel(labelId);
                if (result == "Label Removed")
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
        [HttpPut]
        [Route("editlabel")]
        public async Task<IActionResult> EditLabel(LabelModel labelModel)
        {
            try
            {
                var result = await this.manager.EditLabel(labelModel);
                if (result == "Label Edited")
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
        [Route("getlabelwuserid")]
        public IActionResult GetLabelUserId(int userId)
        {
            try
            {
                var result = this.manager.GetLabelUserId(userId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string> { Status = false, Message = "Cannot get labels" });
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpGet]
        [Route("getlabelwnoteid")]
        public IActionResult GetLabelNoteId(int noteId)
        {
            try
            {
                var result = this.manager.GetLabelNoteId(noteId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string> { Status = false, Message = "Cannot get labels" });
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
