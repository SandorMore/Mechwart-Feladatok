using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Gyarkorlas1.Models;
using Gyarkorlas1.Data;

namespace Gyarkorlas1.Pages.KonyvPages;

public class IndexModel : PageModel
{
    private readonly KonyvtarDBContext _context;

    public IndexModel(KonyvtarDBContext context)
    {
        _context = context;
    }

    public IList<Konyv> Konyv { get; set; } = default!;

    public async Task OnGetAsync()
    {
        Konyv = await _context.konyvek.ToListAsync();
    }
}
