using System.Linq;
using my_books.Data.Models;
using my_books.Data.ViewModels;

namespace my_books.Data.Services
{
    public class PublishersService
    {
        private AppDbContext _appDbContext;

        public PublishersService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public Publisher AddPublisher(PublisherVM publisherVM)
        {
            var publisher = new Publisher()
            {
                Name = publisherVM.Name
            };

            _appDbContext.Publishers.Add(publisher);
            _appDbContext.SaveChanges();

            return publisher;
        }

        public Publisher GetPublisherById(int publisherId) => _appDbContext.Publishers.FirstOrDefault(x => x.Id == publisherId);

        public PublisherWithBooksAndAuthorsVM GetPublisherData(int publisherId)
        {
            var publisherData = _appDbContext.Publishers.Where(p => p.Id == publisherId)
                .Select(p => new PublisherWithBooksAndAuthorsVM()
                {
                    Name = p.Name,
                    BookAuthors = p.Books.Select(p => new BookAuthorVM()
                    {
                        BookName = p.Title,
                        BookAuthors = p.BookAuthors.Select(p => p.Author.FullName).ToList()
                    }).ToList()
                }).FirstOrDefault();

            return publisherData;
        }

        public void DeletePublisherById(int id)
        {
            var publisher = _appDbContext.Publishers.FirstOrDefault(p => p.Id == id);

            if (publisher != null)
            {
                _appDbContext.Publishers.Remove(publisher);
                _appDbContext.SaveChanges();
            }
        }
    }
}