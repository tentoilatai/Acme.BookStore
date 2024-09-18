using Acme.BookStore.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Linq;  // Thêm thư viện này để hỗ trợ IsNullOrWhiteSpace

namespace Acme.BookStore.Authors
{
    [Authorize(BookStorePermissions.Authors.Default)]
    public class AuthorAppService : BookStoreAppService, IAuthorAppService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly AuthorManager _authorManager;

        public AuthorAppService(
            IAuthorRepository authorRepository,
            AuthorManager authorManager)
        {
            _authorRepository = authorRepository ?? throw new ArgumentNullException(nameof(authorRepository));
            _authorManager = authorManager ?? throw new ArgumentNullException(nameof(authorManager));
        }

        public async Task<AuthorDto> GetAsync(Guid id)
        {
            var author = await _authorRepository.GetAsync(id);
            if (author == null)
            {
                throw new KeyNotFoundException($"Author with Id {id} not found.");
            }
            return ObjectMapper.Map<Author, AuthorDto>(author);
        }

        public async Task<PagedResultDto<AuthorDto>> GetListAsync(GetAuthorListDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(Author.Name);
            }

            var authors = await _authorRepository.GetListAsync(
                input.SkipCount,
                input.MaxResultCount,
                input.Sorting,
                input.Filter
            );

            var totalCount = input.Filter == null
                ? await _authorRepository.CountAsync()
                : await _authorRepository.CountAsync(
                    author => author.Name.Contains(input.Filter));

            return new PagedResultDto<AuthorDto>(
                totalCount,
                ObjectMapper.Map<List<Author>, List<AuthorDto>>(authors)
            );
        }

        [Authorize(BookStorePermissions.Authors.Create)]
        public async Task<AuthorDto> CreateAsync(CreateAuthorDto input)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));

            var author = await _authorManager.CreateAsync(
                input.Name,
                input.BirthDate,
                input.ShortBio
            );

            await _authorRepository.InsertAsync(author);

            return ObjectMapper.Map<Author, AuthorDto>(author);
        }

        [Authorize(BookStorePermissions.Authors.Edit)]
        public async Task UpdateAsync(Guid id, UpdateAuthorDto input)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));

            var author = await _authorRepository.GetAsync(id);
            if (author == null)
            {
                throw new KeyNotFoundException($"Author with Id {id} not found.");
            }

            if (author.Name != input.Name)
            {
                await _authorManager.ChangeNameAsync(author, input.Name);
            }

            author.BirthDate = input.BirthDate;
            author.ShortBio = input.ShortBio;

            await _authorRepository.UpdateAsync(author);
        }

        [Authorize(BookStorePermissions.Authors.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            var author = await _authorRepository.FindAsync(id);
            if (author == null)
            {
                throw new KeyNotFoundException($"Author with Id {id} not found.");
            }

            await _authorRepository.DeleteAsync(id);
        }
    }
}
