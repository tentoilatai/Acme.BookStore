using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Acme.BookStore.Books;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace Acme.BookStore.Web.Pages.Books;

public class CreateModalModel : BookStorePageModel
{
    [BindProperty]
    public CreateBookViewModel Book { get; set; }

    private readonly IBookAppService _bookAppService;

    public CreateModalModel(IBookAppService bookAppService)
    {
        _bookAppService = bookAppService;
    }

    public void OnGet()
    {
        Book = new CreateBookViewModel();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var dto = ObjectMapper.Map<CreateBookViewModel, CreateBookDto>(Book);
        await _bookAppService.CreateAsync(dto);
        return NoContent();
    }

    public class CreateBookViewModel
    {
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