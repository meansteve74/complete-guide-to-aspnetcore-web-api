using System;
using System.Collections.Generic;
using System.Linq;
using my_books.Data.Models;
using my_books.Data.ViewModels;

namespace my_books.Data.Services
{
    public class BooksService
    {
        private AppDbContext _appDbContext;

        public BooksService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void AddBookWithAuthors(BookVM bookVM)
        {
            var book = new Book()
            {
                Title = bookVM.Title,
                Description = bookVM.Description,
                IsRead = bookVM.IsRead,
                DateRead = bookVM.IsRead ? bookVM.DateRead.Value : null,
                Rate = bookVM.IsRead ? bookVM.Rate.Value : null,
                Genre = bookVM.Genre,
                CoverUrl = bookVM.CoverUrl,
                DateAdded = DateTime.Now,
                PublisherId = bookVM.PublisherId
            };

            _appDbContext.Books.Add(book);
            _appDbContext.SaveChanges();

            foreach (var id in bookVM.AuthorIds)
            {
                var bookAuthor = new BooksAuthor()
                {
                    BookId = book.Id,
                    AuthorId = id
                };
                _appDbContext.BooksAuthors.Add(bookAuthor);
                _appDbContext.SaveChanges();
            }
        }

        public List<Book> GetAllBooks() => _appDbContext.Books.ToList();

        public BookWithAuthorsVM GetBookWithAuthorById(int bookId)
        {
            var booksWithAuthors = _appDbContext.Books.Where(b => b.Id == bookId).Select(b => new BookWithAuthorsVM()
            {
                Title = b.Title,
                Description = b.Description,
                IsRead = b.IsRead,
                DateRead = b.IsRead ? b.DateRead.Value : null,
                Rate = b.IsRead ? b.Rate.Value : null,
                Genre = b.Genre,
                CoverUrl = b.CoverUrl,
                PublisherName = b.Publisher.Name,
                AuthorNames = b.BookAuthors.Select(x => x.Author.FullName).ToList()
            }).FirstOrDefault();

            return booksWithAuthors;
        }

        public Book UpdateBookById(int bookId, BookVM bookVM)
        {
            var book = _appDbContext.Books.FirstOrDefault(x => x.Id == bookId);

            if (book != null)
            {
                book.Title = bookVM.Title;
                book.Description = bookVM.Description;
                book.IsRead = bookVM.IsRead;
                book.DateRead = bookVM.IsRead ? bookVM.DateRead.Value : null;
                book.Rate = bookVM.IsRead ? bookVM.Rate.Value : null;
                book.Genre = bookVM.Genre;
                book.CoverUrl = bookVM.CoverUrl;

                _appDbContext.SaveChanges();
            }

            return book;
        }

        public void DeleteBookById(int bookId)
        {
            var book = _appDbContext.Books.FirstOrDefault(x => x.Id == bookId);

            if (book != null)
            {
                _appDbContext.Books.Remove(book);
                _appDbContext.SaveChanges();
            }
        }
    }
}