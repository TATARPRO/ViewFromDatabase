using System;
using System.ComponentModel.DataAnnotations;
using ViewsFromDatabase.Models;

namespace ViewsFromDatabase.Areas.ViewModels
{
    public class CMSPageViewModel
    {
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

        public Publicity Publicity { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }
    }
}
