using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PetRescue.Data;
using PetRescue.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace PetRescue.Pages
{
  
    public class FavoritesModel : PageModel
    {
        private readonly PetRescueDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public FavoritesModel(PetRescueDbContext context,UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public List<FavoritePet> FavoritePets { get; set; }



public async Task<IActionResult> OnGetAsync()
    {
        var userId = _userManager.GetUserId(User);

        if (string.IsNullOrEmpty(userId))
        {
            return Challenge(); // Redirect to login page 
        }

        FavoritePets = await _context.FavoritePets
            .Where(f => f.UserIdentifier == userId)
            .ToListAsync();

        return Page();
    }




         public async Task<IActionResult> OnPostRemoveFavoriteAsync(string petId)
        {
            var favoritePet = await _context.FavoritePets.FirstOrDefaultAsync(p => p.PetId == petId);
            if (favoritePet != null)
            {
                _context.FavoritePets.Remove(favoritePet);
                await _context.SaveChangesAsync();
            }
            
            return RedirectToPage();
        }
    }
}
