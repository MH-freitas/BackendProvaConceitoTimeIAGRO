using Library.Domain.Entities;
using Library.Shared.Dtos;
using Library.Shared.Views;
using Librery.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Domain.Intefaces.Services
{
    public interface IBookService
    {
        Task<IReadOnlyList<Book>?> GetAllBooksAsync(BookFilterDto filter);
        List<PrecifiedBookView>? Precifier(PrecifierBooksDto ids);

        PrecifiedBookView? PrecifierOneBook(int id);
    }
}

