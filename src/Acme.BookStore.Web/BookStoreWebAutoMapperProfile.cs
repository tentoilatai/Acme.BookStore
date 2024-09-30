using Acme.BookStore.Authors;
using Acme.BookStore.Books;
using AutoMapper;
using static Acme.BookStore.Web.Pages.Books.EditModalModel;

namespace Acme.BookStore.Web;

public class BookStoreWebAutoMapperProfile : Profile
{
    public BookStoreWebAutoMapperProfile()
    {
        CreateMap<Pages.Books.CreateModalModel.CreateBookViewModel,
                    CreateBookDto>();
        CreateMap<BookDto, Pages.Books.EditModalModel.EditBookViewModel>();
        CreateMap<Pages.Books.EditModalModel.EditBookViewModel,
                    EditBookViewModel>();

        CreateMap<Pages.Authors.CreateModalModel.CreateAuthorViewModel,
                    CreateAuthorDto>();
        
        CreateMap<AuthorDto, Pages.Authors.EditModalModel.EditAuthorViewModel>();
        CreateMap<Pages.Authors.EditModalModel.EditAuthorViewModel,
                    UpdateAuthorDto>();
    }
}
