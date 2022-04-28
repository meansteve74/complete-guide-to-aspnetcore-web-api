using System.Collections.Generic;

namespace my_books.Data.ViewModels
{
    public class BookAuthorVM
    {
        public string BookName { get; set; }
        public List<string> BookAuthors { get; set; }
    }
}