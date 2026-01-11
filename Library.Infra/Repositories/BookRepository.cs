using Library.Domain.Entities;
using Library.Domain.Intefaces.Repositories;
using Library.Infra.AccessConfiguration;

namespace Library.Infra.Repositories
{
    public class BookRepository : IBookReposistory
    {
        private readonly IReadOnlyList<Book> _books;

        public BookRepository()
        {
            var booksTask = DataAccessConfiguration.GetBooksAsync();
            booksTask.Wait();

            _books = booksTask.Result ?? [];
        }

        public IReadOnlyList<Book> GetAllBooks()
        {
            return _books;
        }

        public Book? GetBookById(int id)
        {
            return _books.FirstOrDefault(b => b.Id == id);
        }
    }
}
