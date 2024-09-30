using Acme.BookStore.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using static System.String;

namespace Acme.BookStore.Books
{
    public class EfCoreBookRepository : EfCoreRepository<BookStoreDbContext, Book, Guid>, IBookRepository
    {
        public EfCoreBookRepository(
            IDbContextProvider<BookStoreDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public Task DeleteAsync(string name)
        {
            throw new NotImplementedException();
        }

        public override Task DeleteAsync(Book book, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public override Task DeleteManyAsync(IEnumerable<Book> books, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Book?> FindAsync(Book book, bool includeDetails = true, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<Book?> FindByNameAsync(string name)
        {
            DbSet<Book> dbSet = await GetDbSetAsync();
            return await dbSet.FirstOrDefaultAsync(book => book.Name == name);
        }

        public Task<Book> FindByNamedAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<Book> GetAsync(Book book, bool includeDetails = true, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Book> GetAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<Book> GetAsync(string name, bool includeDetails = true, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Book>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string? filter = null)
        {
            var dbSet = await GetDbSetAsync();

            var queryable = dbSet
                .WhereIf(
                    !IsNullOrWhiteSpace(filter),
                    book => filter != null && book.Name.Contains(filter)
                );

            queryable = sorting switch
            {
                "Name" => queryable.OrderBy(book => book.Name),
                "CreationTime" => queryable.OrderBy(book => book.CreationTime),
                _ => queryable.OrderBy(book => book.Name)
            };

            return await queryable
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }
    }
}