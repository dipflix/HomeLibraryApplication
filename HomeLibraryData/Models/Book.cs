using HomeLibraryData.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HomeLibraryData.Models
{
    public class Book : Entity
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public DateTime PublisingDate { get; set; }

        [Required]
        public string PictureName { get; set; }


        [Range(1, 10, ErrorMessage = "Damaged should be between 1 and 10")]
        public int Damaged { get; set; }
        public ICollection<Author> Authors { get; set; }
        public ICollection<Genre> Genres { get; set; }

        public Book()
        {
            Authors = new List<Author>();
            Genres = new List<Genre>();
        }
        public override string ToLiteText() => Title;
    }
}
