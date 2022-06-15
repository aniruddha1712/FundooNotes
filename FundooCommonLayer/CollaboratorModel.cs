using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FundooCommonLayer
{
    public class CollaboratorModel
    {
        [Key]
        public int CollabId { get; set; }
        public int NoteId { get; set; }

        [ForeignKey("NoteId")]
        //public NoteModel noteModel { get; set; }
        public string CollabEmail { get; set; }
    }
}
