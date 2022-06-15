using FundooBusinessLayer.Interface;
using FundooCommonLayer;
using FundooRepositoryLayer.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooBusinessLayer.Services
{
    public class LabelManager : ILabelManager
    {
        private readonly ILabelRepository repository;
        public IConfiguration Configuration { get; }

        public LabelManager(IConfiguration configuration, ILabelRepository repository)
        {
            this.repository = repository;
            this.Configuration = configuration;
        }

        public async Task<string> AddLabelWithUserId(LabelModel labelModel)
        {
            try
            {
                return await this.repository.AddLabelWithUserId(labelModel);
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
                return await this.repository.AddLabelWithNoteId(labelModel);
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
                return await this.repository.DeleteLabel(userId,labelName);
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
                return await this.repository.RemoveLabel(labelId);
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
                return await this.repository.EditLabel(labelModel);
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
                return this.repository.GetLabelUserId(userId);
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
                return this.repository.GetLabelNoteId(noteId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
