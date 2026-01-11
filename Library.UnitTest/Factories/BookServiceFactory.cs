using Library.Domain.Entities;
using Library.Domain.Intefaces.Repositories;
using Library.Shared.Enums;
using Librery.Shared.Dtos;
using Moq;

namespace Library.UnitTest.Factories
{
    public class BookServiceFactory
    {
        protected readonly Mock<IBookReposistory> _bookRepositoryMock = new();
        public BookServiceFactory()
        {
            var specification = new Specification
            {
                OriginallyPublished = "January 1, 2000",
                Author = "John Doe",
                PageCount = 350,
                Illustrator = new List<string> { "Jane Smith", "Emily Johnson" },
                Genres = new List<string> { "Fiction", "Adventure" }
            };

            var booksMockData = new List<Book>
            {
                new Book { Id = 1, Name = "Book One", Price = 13.01, Specification = specification },
                new Book { Id = 2, Name = "Book Two", Price = 11.0, Specification = specification },
                new Book { Id = 3, Name = "Book Three", Price = 15.2, Specification = specification },
            };

            _bookRepositoryMock.Setup(repo => repo.GetAllBooks())
                .Returns(booksMockData);

            _bookRepositoryMock.Setup(repo => repo.GetBookById(It.IsAny<int>()))
                .Returns((int id) => booksMockData.FirstOrDefault(b => b.Id == id));
        }

        public static IEnumerable<object[]> GetBookFilterData()
        {
            yield return new object[] { new BookFilterDto(null, null, null, null, null, null, null, null, EOrder.Ascending) };
            yield return new object[] { new BookFilterDto(null, null, null, null, null, null, null, null, EOrder.Descending) };
            yield return new object[] { new BookFilterDto(1, null, null, null, null, null, null, "Fantasia", null) };
            yield return new object[] { new BookFilterDto(null, "Book One", null, null, null, null, null, null, null) };
            yield return new object[] { new BookFilterDto(1, null, 13.01, null, null, null, null, null, null) };
            yield return new object[] { new BookFilterDto(null, null, null, "January 1, 2000", null, null, null, null, null) };
            yield return new object[] { new BookFilterDto(null, null, null, null, "John Doe", null, null, null, null) };
            yield return new object[] { new BookFilterDto(null, null, null, null, null, 350, null, null, null) };
            yield return new object[] { new BookFilterDto(null, null, null, null, null, null, "Jane Smith", null, null) };
            yield return new object[] { new BookFilterDto(null, null, null, null, null, null, null, "Fiction", null) };
            yield return new object[] { new BookFilterDto(2, "Book Two", 11.0, "January 1, 2000", "John Doe", 350, "Emily Johnson", "Adventure", EOrder.Ascending) };
        }

    }
}
