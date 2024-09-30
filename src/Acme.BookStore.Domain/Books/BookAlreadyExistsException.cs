using Volo.Abp;

namespace Acme.BookStore.Books
{
    public class BookAlreadyExistsException : BusinessException
    {
        public BookAlreadyExistsException(string name)
            : base(BookStoreDomainErrorCodes.BookAlreadyExists)
        {
            WithData("name", name);
        }
    }
}
