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

        public void AddBook(BookVM bookVM)
        {
            var book = new Book()
            {
                Title = bookVM.Title,
                Description = bookVM.Description,
                IsRead = bookVM.IsRead,
                DateRead = bookVM.IsRead ? bookVM.DateRead.Value : null,
                Rate = bookVM.IsRead ? bookVM.Rate.Value : null,
                Genre = bookVM.Genre,
                Author = bookVM.Author,
                CoverUrl = bookVM.CoverUrl,
                DateAdded = DateTime.Now
            };

            _appDbContext.Books.Add(book);
            _appDbContext.SaveChanges();
        }

        public List<Book> GetAllBooks() => _appDbContext.Books.ToList();

        public Book GetBookById(int bookId) => _appDbContext.Books.FirstOrDefault(x => x.Id == bookId);

        public Book UpdateBookById(int bookId, BookVM bookVM)
        {
            var book = GetBookById(bookId);

            if (book != null)
            {
                book.Title = bookVM.Title;
                book.Description = bookVM.Description;
                book.IsRead = bookVM.IsRead;
                book.DateRead = bookVM.IsRead ? bookVM.DateRead.Value : null;
                book.Rate = bookVM.IsRead ? bookVM.Rate.Value : null;
                book.Genre = bookVM.Genre;
                book.Author = bookVM.Author;
                book.CoverUrl = bookVM.CoverUrl;

                _appDbContext.SaveChanges();
            }

            return book;
        }

        public void DeleteBookById(int bookId)
        {
            var book = GetBookById(bookId);

            if (book != null)
            {
                _appDbContext.Books.Remove(book);
                _appDbContext.SaveChanges();
            }
        }
    }
}