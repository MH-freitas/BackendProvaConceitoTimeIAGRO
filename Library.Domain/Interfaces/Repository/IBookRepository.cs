using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Library.Domain.Intefaces.Repositories
{
    public interface IBookReposistory
    {
        public IReadOnlyList<Book>? GetAllBooks();

        public Book? GetBookById(int id);

    }
}
