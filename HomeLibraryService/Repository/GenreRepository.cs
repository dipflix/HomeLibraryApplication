using HomeLibraryData.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace HomeLibraryService.Repository
{
    public class GenreRepository : DbRepository<Genre>
    {
        public override IQueryable<Genre> Items => base.Items.Include(it => it.Books);
    }
}
