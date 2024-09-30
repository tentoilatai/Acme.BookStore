using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using Volo.Abp;
using static Acme.BookStore.Books.BookConsts;

namespace Acme.BookStore.Books
{
    public class BookManager : DomainService
    {
        private readonly IBookRepository _bookRepository;

        public BookManager(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<Book> CreateAsync(
            string name,
            BookType type,
            DateTime publishDate,
            float price)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));

            var existingBook = await _bookRepository.FindByNameAsync(name);
            if (existingBook != null)
            {
                throw new BookAlreadyExistsException(name);
            }

            return new Book(
                GuidGenerator.Create(),
                name,
                type,
                publishDate,
                price
            );
        }

        public async Task ChangeNameAsync(
            Book book,
            string newName)
        {
            Check.NotNull(book, nameof(book));
            Check.NotNullOrWhiteSpace(newName, nameof(newName));

            var existingBook = await _bookRepository.FindByNameAsync(newName);
            if (existingBook != null && existingBook.Name != book.Name)
            {
                throw new BookAlreadyExistsException(newName);
            }

            book.ChangeName(newName);
        }
    }
}
