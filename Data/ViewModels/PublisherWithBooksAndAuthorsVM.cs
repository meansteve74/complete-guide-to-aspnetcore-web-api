using System.Collections.Generic;

namespace my_books.Data.ViewModels
{
    public class PublisherWithBooksAndAuthorsVM
    {
        public string Name { get; set; }
        public List<BookAuthorVM> BookAuthors { get; set; }
    }
}