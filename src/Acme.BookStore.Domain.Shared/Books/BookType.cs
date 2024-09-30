using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.BookStore.Books
{
    public static class BookConsts
    {
        public const int MaxNameLength = 128;

        public enum BookType
        {
            Undefined,
            Adventure,
            Biography,
            Dystopia,
            Fantastic,
            Horror,
            Science,
            ScienceFiction,
            Poetry
        }
    }
}
