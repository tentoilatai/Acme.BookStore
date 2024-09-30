using Acme.BookStore.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace Acme.BookStore.Books
{
    [Authorize(BookStorePermissions.Books.Default)]
    public class BookAppService : BookStoreAppService, IBookAppService
    {
        private readonly IBookRepository _bookRepository;
        private readonly BookManager _bookManager;

        public BookAppService(
            IBookRepository bookRepository,
            BookManager bookManager)
        {

            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
            _bookManager = bookManager ?? throw new ArgumentNullException(nameof(bookManager));
        }

        public async Task<BookDto> GetAsync(Guid id)
        {
           var book = await _bookRepository.GetAsync(id);
            if (book == null)
            {
                throw new KeyNotFoundException($"Book with Id {id} not found.");
            }
            return ObjectMapper.Map<Book, BookDto>(book);
        }

        public async Task<PagedResultDto<BookDto>> GetListAsync(GetBookListDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(Book.Name);
            }

            var books = await _bookRepository.GetListAsync(
                input.SkipCount,
                input.MaxResultCount,
                input.Sorting,
                input.Filter
            );

            var totalCount = input.Filter == null
                ? await _bookRepository.CountAsync()
                : await _bookRepository.CountAsync(
                    book => book.Name.Contains(input.Filter));

            return new PagedResultDto<BookDto>(
                totalCount,
                ObjectMapper.Map<List<Book>, List<BookDto>>(books)
            );
        }

        [Authorize(BookStorePermissions.Books.Create)]
        public async Task<BookDto> CreateAsync(CreateBookDto input)
        {
            var book = await _bookManager.CreateAsync(
                input.Name,
                input.Type,
                input.PublishDate,
                input.Price
            );

            await _bookRepository.InsertAsync(book);

            return ObjectMapper.Map<Book, BookDto>(book);
        }

        [Authorize(BookStorePermissions.Books.Edit)]
        public async Task UpdateAsync(string name, UpdateBookDto input)
        {
            var book = await _bookRepository.GetAsync(name);

            if (book.Name != input.Name)
            {
                await _bookManager.ChangeNameAsync(book, input.Name);
            }

            book.Name = input.Name;
            book.Type = input.Type;
            book.PublishDate = input.PublishDate;
            book.Price = input.Price;

            await _bookRepository.UpdateAsync(book);
        }

        public Task UpdateAsync(Guid id, UpdateBookDto input)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
