using System;
using Volo.Abp.Application.Dtos;
using static Acme.BookStore.Books.BookConsts;

namespace Acme.BookStore.Books;

public class BookDto : EntityDto<Guid>
{
    public required string Name { get; set; }
    public BookType Type { get; set; }
    public DateTime PublishDate { get; set; }
    public float Price { get; set; }
    public string AuthorName { get; set; }
}
