using HomeLibraryData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HomeLibraryService.Repository
{
    public class AuthorRepository: DbRepository<Author>
    {
        public override IQueryable<Author> Items => base.Items.Include(it => it.Books);
    }
}
