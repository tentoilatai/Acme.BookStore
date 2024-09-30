using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Acme.BookStore.Books;
using Microsoft.AspNetCore.Mvc;

namespace Acme.BookStore.Web.Pages.Books;

public class EditModalModel : BookStorePageModel
{
    [BindProperty]
    public EditBookViewModel Book { get; set; }

    private readonly IBookAppService _bookAppService;

    public EditModalModel(IBookAppService bookAppService)
    {
        _bookAppService = bookAppService;
    }

    public async Task<IActionResult> OnGetAsync(Guid id)
    {
        var bookDto = await _bookAppService.GetAsync(id);
        Book = ObjectMapper.Map<BookDto, EditBookViewModel>(bookDto);
        return Page();  // This fixes the missing return value error
    }

    public async Task<NoContentResult> OnPostAsync()
    {
        await _bookAppService.UpdateAsync(
            Book.Id, 
            ObjectMapper.Map<EditBookViewModel, UpdateBookDto>(Book)
        );
        return NoContent();
    }

    public class EditBookViewModel
    {
        [HiddenInput]
        public Guid Id { get; set; }

        [Required]
        [StringLength(BookConsts.MaxNameLength)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public BookConsts.BookType Type { get; set; } = BookConsts.BookType.Undefined;

        [Required]
        [DataType(DataType.Date)]
        public DateTime PublishDate { get; set; } = DateTime.Now;

        [Required]
        public float Price { get; set; }
    }
}