using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Przepisy.Models
{
    public class AddNewReceip
    {
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        [MinLength(50)]
        [MaxLength(5000)]
        public string Content { get; set; }

        [Required]
        [MinLength(20)]
        [MaxLength(70)]
        public string ShortDescription { get; set; }

        [Required(ErrorMessage = "Please select File!")]
        public HttpPostedFileBase File { get; set; }
    }

    public class ListViewModel
    {
        public List<ItemOnList> ListViewModels { get; set; }
    }

    public class ItemOnList
    {
        public int ID { get; set; }
        public byte[] ImageData { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
    }

    public class ListItemModelView
    {
        public int ID { get; set; }
        public byte[] ImageData { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Content { get; set; }
    }
}