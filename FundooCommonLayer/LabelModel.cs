using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FundooCommonLayer
{
    public class LabelModel
    {
        [Key]
        public int LabelId { get; set; }
        public int NoteId { get; set; }
        [ForeignKey("NoteId")]

        //public NoteModel NoteModel { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]

        //public RegisterModel RegisterModel { get; set; }
        [Required]
        public string LabelName { get; set; }
    }
}
