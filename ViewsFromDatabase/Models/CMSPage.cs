using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ViewsFromDatabase.Models
{
    public class CMSPage
    {
        public long Id { get; set; }

        [StringLength(200)]
        public string Location { get; set; }

        public string Name { get; set; }
        public Publicity Publicity { get; set; }

        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        [DataType(DataType.Date)]
        public DateTime LastModified { get; set; }

        [DataType(DataType.Date)]
        public DateTime? LastRequested { get; set; }

        public CMSPage(string name)
        {
            Location = $"/Areas/CMS/Views/InnertPages/{name}";
            LastModified = DateTime.Now;
            Publicity = Publicity.InPreview;
        }
    }
}
