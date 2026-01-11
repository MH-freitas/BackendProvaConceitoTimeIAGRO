using Library.Domain.Intefaces.Services;
using Library.Domain.Services;
using Library.Shared.Enums;
using Library.UnitTest.Factories;
using Librery.Shared.Dtos;
using System;
using System.Collections;
using System.Collections.Generic;
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
        public async Task Test_GetAllBooks_ReturnsAllBooks()
        {
            var bookFilter = new BookFilterDto(null, null, null, null, null, null, null, null, null);

            var books = await _bookService.GetAllBooksAsync(bookFilter);

            Assert.NotNull(books);
            Assert.Equal(3, books.Count);
        }

        [Fact]
        public async Task Test_GetAllBooks_FilterById_ReturnsCorrectBook()
        {
            var bookFilter = new BookFilterDto(null, null, null, null, null, null, null, null, EOrder.Ascending);

            var books = await _bookService.GetAllBooksAsync(bookFilter);

            Assert.NotNull(books);
            Assert.Equal(3, books.Count);
            Assert.True(books.SequenceEqual(books.OrderBy(x => x.Price)));
        }

        [Theory]
        [MemberData(nameof(GetBookFilterData))]
        public async Task Test_GetAllBooks_ReturnsBooksWithCorrespondentFilter(BookFilterDto bookFilter)
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
    }
}
