using HomeLibraryData.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace HomeLibraryService.Repository
{
    public class BookRepository : DbRepository<Book>
    {
        public override IQueryable<Book> Items => base.Items.Include(it => it.Genres).Include(it => it.Authors);

    }
}
