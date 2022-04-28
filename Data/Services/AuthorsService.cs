using System.Linq;
using my_books.Data.Models;
using my_books.Data.ViewModels;

namespace my_books.Data.Services
{
    public class AuthorsService
    {
        private AppDbContext _appDbContext;

        public AuthorsService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void AddAuthor(AuthorVM authorVM)
        {
            var author = new Author()
            {
                FullName = authorVM.FullName
            };

            _appDbContext.Authors.Add(author);
            _appDbContext.SaveChanges();
        }

        public AuthorWithBooksVM GetAuthorWithBooks(int authorId)
        {
            var author = _appDbContext.Authors.Where(a => a.Id == authorId).Select(a => new AuthorWithBooksVM()
            {
                FullName = a.FullName,
                BookTitles = a.BookAuthors.Select(a => a.Book.Title).ToList()
            }).FirstOrDefault();

            return author;
        }
    }
}