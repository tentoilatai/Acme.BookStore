using System;
using System.ComponentModel.DataAnnotations;
using static Acme.BookStore.Books.BookConsts;

namespace Acme.BookStore.Books
{
    public class CreateBookDto
    {
        [Required]
        [StringLength(BookConsts.MaxNameLength)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public BookType Type { get; set; } = BookType.Undefined;

        [Required]
        [DataType(DataType.Date)]
        public DateTime PublishDate { get; set; } = DateTime.Now;

        [Required]
        public float Price { get; set; }
    }
}
