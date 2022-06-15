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
    public class LabelRepository : ILabelRepository
    {
        private readonly UserContext userContext;

        public LabelRepository(IConfiguration configuration, UserContext userContext)
        {
            this.Configuration = configuration;
            this.userContext = userContext;
        }
        public IConfiguration Configuration { get; }

        public async Task<string> AddLabelWithUserId(LabelModel labelModel)
        {
            try
            {
                var isLabel = this.userContext.Labels.Where(l => l.UserId == labelModel.UserId && l.LabelName != labelModel.LabelName && l.NoteId == labelModel.NoteId).SingleOrDefault();
                if(isLabel == null)
                {
                    this.userContext.Labels.Add(labelModel);
                    await this.userContext.SaveChangesAsync();
                    return "Label Added";
                }
                else
                {
                    return "Label exists";
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<string> AddLabelWithNoteId(LabelModel labelModel)
        {
            try
            {
                var isLabel = this.userContext.Labels.Where(l => l.UserId == labelModel.UserId && l.NoteId == labelModel.NoteId).SingleOrDefault();
                if (isLabel == null)
                {
                    this.userContext.Labels.Add(labelModel);
                    await this.userContext.SaveChangesAsync();
                    return "Label Added";
                }
                else
                {
                    return "Label exists";
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<string> DeleteLabel(int userId, string labelName)
        {
            try
            {
                var isLabel = this.userContext.Labels.Where(l => l.UserId == userId && l.LabelName == labelName).SingleOrDefault();
                if (isLabel != null)
                {
                    this.userContext.Labels.RemoveRange(isLabel);
                    await this.userContext.SaveChangesAsync();
                    return "Label Deleted";
                }
                else
                {
                    return "Label does not exist";
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<string> RemoveLabel(int labelId)
        {
            try
            {
                var isLabel = this.userContext.Labels.Where(l => l.LabelId == labelId).SingleOrDefault();
                if (isLabel != null)
                {
                    this.userContext.Labels.Remove(isLabel);
                    await this.userContext.SaveChangesAsync();
                    return "Label Removed";
                }
                else
                {
                    return "Label does not exist";
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<string> EditLabel(LabelModel labelModel)
        {
            try
            {
                var isLabel = this.userContext.Labels.Where(l => l.UserId == labelModel.UserId && l.LabelId == labelModel.LabelId && l.NoteId == labelModel.NoteId).Select(l => l.LabelName).SingleOrDefault();
                var pastLabel = this.userContext.Labels.Where(l => l.LabelName == isLabel).ToList();
                pastLabel.ForEach(l => l.LabelName = labelModel.LabelName);
                this.userContext.Labels.UpdateRange(pastLabel);
                await this.userContext.SaveChangesAsync();
                return "Label Edited";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<string> GetLabelUserId(int userId)
        {
            try
            {
                IEnumerable<string> isLabel = this.userContext.Labels.Where(l => l.UserId == userId).Select(l => l.LabelName).ToList();
                if (isLabel != null)
                {
                    return isLabel;
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

        public IEnumerable<string> GetLabelNoteId(int noteId)
        {
            try
            {
                var isLabel = this.userContext.Labels.Where(l => l.NoteId == noteId).Select(l=> l.LabelName).ToList();
                if (isLabel.Count != 0)
                {
                    return isLabel;
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
    }
}
