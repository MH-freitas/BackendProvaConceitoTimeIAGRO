using Library.Domain.Entities;
using Library.Domain.Intefaces.Repositories;
using Library.Domain.Intefaces.Services;
using Library.Shared.Dtos;
using Library.Shared.Views;
using Librery.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Domain.Services
{
    public class BookService : IBookService
    {
        private readonly IBookReposistory _bookRepository;

        public BookService(IBookReposistory bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<IReadOnlyList<Book>?> GetAllBooksAsync(BookFilterDto filter)
        {
            return _bookRepository.GetAllBooks();
        }

        public List<PrecifiedBookView>? Precifier(PrecifierBooksDto dto)
        {
            var books = new List<PrecifiedBookView>();
            
            foreach (var id in dto.Ids)
            {
                var book = _bookRepository.GetBookById(id);
                
                if (book != null)
                {
                    var calculatedShip = Math.Round((book.Price * 1.2), 2); 

                    books.Add(new PrecifiedBookView(
                        book.Id,
                        book.Name, 
                        book.Price, 
                        calculatedShip
                    ));
                }
            }

            return books;
        }
    }
}