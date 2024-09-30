using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using static Acme.BookStore.Books.BookConsts;

namespace Acme.BookStore.Books
{
    public class Book : FullAuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }
        public BookType Type { get; set; }
        public DateTime PublishDate { get; set; }
        public float Price { get; set; }

        public Book(Guid id, string name, DateTime publishDate, float price, BookType type)
        {
            Name = name;
        }


        internal Book(
            Guid id,
            string name,
            BookType type,
            DateTime publishDate,
            float price) : base(id)
        {
            SetName(name);
            Name = name;
            Type = type;
            PublishDate = publishDate;
            Price = price;
        }

        public Book()
        {
            throw new NotImplementedException();
        }


        internal Book ChangeName(string name)
        {
            SetName(name);
            return this;
        }

        private void SetName(string name)
        {
            Name = Check.NotNullOrWhiteSpace(
                name,
                nameof(name),
                maxLength: BookConsts.MaxNameLength
            );
        }
    }
}
