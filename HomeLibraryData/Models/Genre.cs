using HomeLibraryData.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HomeLibraryData.Models
{
    public class Genre: Entity
    {
        [Required]
        public string Description { get; set; }
        public ICollection<Book> Books { get; set; }

    }
}
