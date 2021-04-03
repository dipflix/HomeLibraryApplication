using HomeLibraryData.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HomeLibraryData.Models
{
    public class Author: Entity
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        public ICollection<Book> Books { get; set; }

        public Author() {
            Books = new List<Book>();
        }

        public override string ToLiteText() => $"{FirstName} {LastName}";
    }
}
