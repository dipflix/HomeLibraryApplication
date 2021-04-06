using HomeLibraryData.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace HomeLibraryService.Repository
{
    public class BookRepository : DbRepository<Book>
    {
        public override IQueryable<Book> Items => base.Items.Include(it => it.Genres).Include(it => it.Authors);
        public void AttactArrayGenre(Genre entity) {
            _context.Genres.Attach(entity);
        }

        public void AttactArrayGenreItems(ICollection<Genre> entities)
        {
            foreach(var entity in entities)
                _context.Genres.Attach(entity);
        }
    }
}
