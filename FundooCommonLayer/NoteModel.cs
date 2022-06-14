using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FundooCommonLayer
{
    public class NoteModel
    {
        [Key]
        public int NoteId { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        //public RegisterModel registerModel { get; set; }

        public string Title { get; set; }
        public string TakeNote { get; set; }
        public string Reminder { get; set; }
        public string Colour { get; set; }
        public string Image { get; set; }

        [DefaultValue(false)]
        public bool Archieve { get; set; }

        [DefaultValue(false)]
        public bool Trash { get; set; }

        [DefaultValue(false)]
        public bool Pinned { get; set; }
    }
}
