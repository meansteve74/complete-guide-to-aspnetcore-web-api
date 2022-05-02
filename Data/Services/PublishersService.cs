using System;
using System.Collections.Generic;
using System.Linq;
using my_books.Data.Models;
using my_books.Data.Paging;
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

        public List<Publisher> GetAllPublishers(string sortBy, string searchString, int? pageNumber)
        {
            var allPublishers = _appDbContext.Publishers.OrderBy(n => n.Name).ToList();

            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "name_desc":
                        allPublishers = allPublishers.OrderByDescending(n => n.Name).ToList();
                        break;
                }
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                allPublishers = allPublishers.Where(n => n.Name.Contains(searchString, StringComparison.InvariantCultureIgnoreCase)).ToList();
            }

            // Paging
            var pageSize = 5;
            allPublishers = PaginatedList<Publisher>.Create(allPublishers.AsQueryable(), pageNumber ?? 1, pageSize);

            return allPublishers;
        }
    }
}