using Library.Domain.Intefaces.Services;
using Library.Domain.Services;
using Library.Shared.Dtos;
using Library.Shared.Enums;
using Library.UnitTest.Factories;
using Librery.Shared.Dtos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Library.UnitTest.Services
{
    public class BookServiceTests : BookServiceFactory
    {
        private readonly IBookService _bookService;

        public BookServiceTests()
        {
            _bookService = new BookService(_bookRepositoryMock.Object);
        }

        [Fact]
        public async Task GetAllBooksReturnsAllBooks()
        {
            var bookFilter = new BookFilterDto(null, null, null, null, null, null, null, null, null);

            var books = await _bookService.GetAllBooksAsync(bookFilter);

            Assert.NotNull(books);
            Assert.Equal(3, books.Count);
        }

        [Fact]
        public async Task GetAllBooksFilterByIdReturnsCorrectBook()
        {
            var bookFilter = new BookFilterDto(null, null, null, null, null, null, null, null, EOrder.Ascending);

            var books = await _bookService.GetAllBooksAsync(bookFilter);

            Assert.NotNull(books);
            Assert.Equal(3, books.Count);
            Assert.True(books.SequenceEqual(books.OrderBy(x => x.Price)));
        }

        [Theory]
        [MemberData(nameof(GetBookFilterData))]
        public async Task GetAllBooksReturnsBooksWithCorrespondentFilter(BookFilterDto bookFilter)
        {
            var books = await _bookService.GetAllBooksAsync(bookFilter);

            Assert.NotNull(books);
            Assert.All(books, book =>
            {
                if (bookFilter.Id.HasValue)
                    Assert.Equal(bookFilter.Id.Value, book.Id);

                if (!string.IsNullOrEmpty(bookFilter.Name))
                    Assert.Contains(bookFilter.Name, book.Name, 
                        StringComparison.OrdinalIgnoreCase);
                
                if (bookFilter.Price.HasValue)
                    Assert.Equal(bookFilter.Price.Value, book.Price);
                
                if (!string.IsNullOrEmpty(bookFilter.OriginallyPublished))
                    Assert.Contains(bookFilter.OriginallyPublished, 
                        book.Specification.OriginallyPublished, StringComparison.OrdinalIgnoreCase);
                
                if (!string.IsNullOrEmpty(bookFilter.Author))
                    Assert.Contains(bookFilter.Author, book.Specification.Author, 
                        StringComparison.OrdinalIgnoreCase);
                
                if (bookFilter.PagesCount.HasValue)
                    Assert.Equal(bookFilter.PagesCount.Value, book.Specification.PageCount);
                
                if (!string.IsNullOrEmpty(bookFilter.Illustrator))
                    Assert.Contains(bookFilter.Illustrator, book!.Specification!.Illustrator!);
                
                if (!string.IsNullOrEmpty(bookFilter.Genre))
                    Assert.Contains(bookFilter.Genre, book.Specification.Genres!);
            });
        }

        [Fact]
        public void ShouldBePossiblePrecifyListOfBooks() 
        {
            var books = _bookRepositoryMock.Object.GetAllBooks();

            var precifiedBooksForVerification = books!
                .Select(b => new 
                { b.Id, Price = Math.Round((b.Price * 1.2), 2) })
                .ToList();

            var booksId = new PrecifierBooksDto([.. books!.Select(b => b.Id)]);

            var precifiedBooks = _bookService.Precifier(booksId);
            
            foreach(var precifiedBook in precifiedBooksForVerification)
            {
                Assert.NotNull(books);
                Assert.Equal(
                    precifiedBooksForVerification[precifiedBook.Id - 1].Price, 
                    precifiedBooks![precifiedBook.Id - 1].PriceWithShipping);
            }
        }
    }
}
