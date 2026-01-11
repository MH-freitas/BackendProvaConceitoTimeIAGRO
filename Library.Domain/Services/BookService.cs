using Library.Domain.Entities;
using Library.Domain.Intefaces.Repositories;
using Library.Domain.Intefaces.Services;
using Library.Shared.Dtos;
using Library.Shared.Views;
using Librery.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Reflection.Metadata.BlobBuilder;

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
            var books = _bookRepository.GetAllBooks();

            if (filter.Id.HasValue && books is not null)
            {
                books = books.Where(b => b.Id == filter.Id.Value).ToList();
            }

            if (!string.IsNullOrEmpty(filter.Name) && books is not null)
            {
                books = books.Where(b => b.Name != null && b.Name.Contains(filter.Name, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (filter.OriginallyPublished != default && books is not null)
            {
                books = books.Where(b => b.Specification != null &&
                b.Specification
                .OriginallyPublished == filter.OriginallyPublished).ToList();
            }

            if (!string.IsNullOrEmpty(filter.Author) && books is not null)
            {
                books = books.Where(b => b.Specification != null &&
                b.Specification.Author != null &&
                b.Specification.Author.Contains(filter.Author, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (filter.PagesCount.HasValue && books is not null)
            {
                books = books.Where(b => b.Specification != null &&
                b.Specification.PageCount == filter.PagesCount.Value).ToList();
            }

            if (!string.IsNullOrEmpty(filter.Illustrator) && books is not null)
            {
                books = books.Where(b => b.Specification != null &&
                b.Specification.Illustrator != null &&
                b.Specification.Illustrator.Contains(filter.Illustrator)).ToList();
            }

            if (!string.IsNullOrEmpty(filter.Genre) && books is not null)
            {
                books = books.Where(b => b.Specification != null &&
                b.Specification.Genres != null &&
                b.Specification.Genres.Contains(filter.Genre)).ToList();
            }

            if (filter.OrderBy != null && books is not null)
            {
                books = filter.OrderBy.HasValue ? filter.OrderBy.Value == Shared.Enums.EOrder.Ascending
                     ? books.OrderBy(b => b.Price).ToList()
                     : books.OrderByDescending(b => b.Price).ToList() :
                     books.OrderBy(b => b.Price).ToList()
                     ;
            }

            return await Task.FromResult(books);
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

        public PrecifiedBookView? PrecifierOneBook(int id)
        {
            var book = _bookRepository.GetBookById(id);

            if (book == null)
                return null;

            var calculatedShip = Math.Round(book.Price * 1.2, 2);

            var bookView = new PrecifiedBookView(
                book.Id,
                book.Name,
                book.Price,
                calculatedShip
            );

            return bookView;
        }


    }
}