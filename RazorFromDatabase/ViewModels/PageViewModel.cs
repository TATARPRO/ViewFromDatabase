using System;
using System.ComponentModel.DataAnnotations;
using RazorFromDatabase.Models;

namespace RazorFromDatabase.Areas.ViewModels
{
    public class PageViewModel
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
